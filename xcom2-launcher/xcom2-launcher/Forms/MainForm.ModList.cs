using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Microsoft.VisualBasic;
using XCOM2Launcher.Classes.Mod;
using XCOM2Launcher.Helper;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Steam;

namespace XCOM2Launcher.Forms
{
    public partial class MainForm : Form
    {
        public ModList Mods => Settings.Mods;
        public Dictionary<string, ModTag> AvailableTags => Settings.Tags;
        public TypedObjectListView<ModEntry> ModList { get; private set; }
        public ModEntry CurrentMod;

        private bool _CheckTriggeredFromContextMenu;

        private void InitModListView()
        {
            var categoryGroupingDelegate = new GroupKeyGetterDelegate(o => Mods.GetCategory(o as ModEntry));

            var categoryFormatterDelegate = new GroupFormatterDelegate((@group, parameters) =>
            {
                var groupName = group.Key as string;
                if (groupName == null)
                    return;

                // Restore collapsed state
                group.Collapsed = Mods.Entries[groupName].Collapsed;

                // Sort Categories
                parameters.GroupComparer = Comparer<OLVGroup>.Create((a, b) => Mods.Entries[(string) a.Key].Index.CompareTo(Mods.Entries[(string) b.Key].Index));
            });

            modlist_ListObjectListView.GroupStateChanged += delegate(object o, GroupStateChangedEventArgs args)
            {
                // Remember group expanded/collapsed state when grouping by category (name or id column)
                if (modlist_ListObjectListView.PrimarySortColumn == olvcName || modlist_ListObjectListView.PrimarySortColumn == olvcID)
                {
                    if (args.Group.Key is string key)
                    {
                        if (Mods.Entries.ContainsKey(key))
                        {
                            Mods.Entries[key].Collapsed = args.Group.Collapsed;
                        }
                    }
                }
            };

            olvcActive.GroupKeyGetter = categoryGroupingDelegate;
            olvcActive.GroupFormatter = categoryFormatterDelegate;

            olvcName.GroupKeyGetter = categoryGroupingDelegate;
            olvcName.GroupFormatter = categoryFormatterDelegate;

            olvcID.GroupKeyGetter = categoryGroupingDelegate;
            olvcID.GroupFormatter = categoryFormatterDelegate;

            olvcOrder.GroupKeyGetter = categoryGroupingDelegate;
            olvcOrder.GroupFormatter = categoryFormatterDelegate;

            olvcCategory.AspectGetter = o => Mods.GetCategory((ModEntry) o);
            
            olvcTags.Renderer = new TagRenderer(modlist_ListObjectListView, AvailableTags);
            olvcTags.AspectPutter = (rowObject, value) =>
            {
                var tags = ((string) value).Split(';');

                tags.ToList().ForEach(t => AddTag((ModEntry) rowObject, t.Trim()));
            };
            olvcTags.SearchValueGetter = rowObject => ((ModEntry)rowObject).Tags.Select(s => s.ToLower()).ToArray();
            olvcTags.AspectGetter = rowObject => "";

            olvcState.AspectGetter = StateAspectGetter;
            olvcSize.AspectToStringConverter = size => ((long)size).FormatAsFileSize();

            olvcLastUpdated.DataType = typeof(DateTime?);
            olvcDateAdded.DataType = typeof(DateTime?);
            olvcDateCreated.DataType = typeof(DateTime?);

            olvcPath.GroupKeyGetter = o => Path.GetDirectoryName((o as ModEntry)?.Path);

            olvcSource.AspectGetter = rowObject =>
            {
                if (rowObject is ModEntry mod)
                {
                    switch (mod.Source)
                    {
                        case ModSource.Unknown:
                            return "Unknown";
                        case ModSource.SteamWorkshop:
                            return "Steam";
                        case ModSource.Manual:
                            return "Local";
                        default:
                            throw new ArgumentOutOfRangeException(nameof(mod.Source), "Unhandled ModSource");
                    }
                }

                return "";
            };

            // size groupies
            var columns = modlist_ListObjectListView.AllColumns.ToArray();
            columns.Single(c => c.AspectName == "Size").MakeGroupies(
                new[] {1024, 1024*1024, (long) 50*1024*1024, (long) 100*1024*1024},
                new[] {"< 1 KB", "< 1MB", "< 50 MB", "< 100 MB", "> 100 MB"}
                );
            columns.Single(c => c.AspectName == "isHidden").MakeGroupies(
                new [] {false, true},
                new [] { "wut?", "Not Hidden", "Hidden"});
            columns.Single(c => c.AspectName == "isActive").MakeGroupies(
                new[] { false, true },
                new[] { "wut?", "Disabled", "Enabled" });

            olvcActive.AspectToStringConverter = active => "";
            olvcActive.GroupFormatter = (g, param) => { param.GroupComparer = Comparer<OLVGroup>.Create((a, b) => (param.GroupByOrder == SortOrder.Descending ? 1 : -1) * a.Header.CompareTo(b.Header)); };

            olvcName.AutoCompleteEditor = false;
            
            // Sort by Order or WorkshopID column removes groups
            modlist_ListObjectListView.BeforeSorting += (sender, args) =>
            {
                bool isGroupableColumn = CheckIfGroupableColumn(args.ColumnToSort);
                bool useGrouping = cEnableGrouping.Checked && isGroupableColumn;
                modlist_ListObjectListView.ShowGroups = useGrouping;
                modlist_toggleGroupsButton.Enabled = useGrouping;
                cEnableGrouping.Enabled = isGroupableColumn;
            };

            modlist_ListObjectListView.BooleanCheckStatePutter = ModListBooleanCheckStatePutter;
            
            // Init DateTime columns
            foreach (var column in columns.Where(c => c.DataType == typeof (DateTime?)))
            {
                column.AspectToStringConverter = d => (d as DateTime?)?.ToLocalTime().ToString(CultureInfo.CurrentCulture);
                column.MakeGroupies(
                    new[] { DateTime.Now.Subtract(TimeSpan.FromHours(24 * 30)), DateTime.Now.Subtract(TimeSpan.FromHours(24 * 7)), DateTime.Now.Date },
                    new[] { "Older Than One Month", "This Month", "This Week", "Today" });

                // Sord Desc
                column.GroupFormatter = (g, param) => { param.GroupComparer = Comparer<OLVGroup>.Create((a, b) => (param.GroupByOrder == SortOrder.Descending ? -1 : 1)*a.Header.CompareTo(b.Header)); };
            }

            // Start out sorted by name
            modlist_ListObjectListView.Sort(olvcName, SortOrder.Ascending);
            
            // Wrapper
            ModList = new TypedObjectListView<ModEntry>(modlist_ListObjectListView);

            // Restore State
            if (Settings.Windows.ContainsKey("main") && Settings.Windows["main"].Data != null)
                modlist_ListObjectListView.RestoreState(Settings.Windows["main"].Data);
            
            RefreshModList();
        }

        private object StateAspectGetter(object rowobject)
        {
            var mod = (ModEntry) rowobject;

            if (mod.State.HasFlag(ModState.Downloading))
                return "Downloading";

            if (mod.State.HasFlag(ModState.NotInstalled))
                return "Not installed";

            if (mod.State.HasFlag(ModState.NotLoaded))
                return "Not loaded";

            if (mod.State.HasFlag(ModState.MissingDependencies) && mod.isActive)
                return "Missing dep";

            if (mod.State.HasFlag(ModState.ModConflict))
                return "Conflict";

            if (mod.State.HasFlag(ModState.DuplicateID))
                return "Duplicate ID";

            if (mod.State.HasFlag(ModState.New))
                return "New";

            if (mod.State.HasFlag(ModState.UpdateAvailable))
                return "Update available";

            if (mod.State.HasFlag(ModState.DuplicateDisabled))
                return "Duplicate (disabled)";

            if (mod.State.HasFlag(ModState.DuplicatePrimary))
                return "Duplicate (primary)";

            return "OK";
        }

        /// <summary>
        /// We do not want to use grouping for columns where it doesn't make sense.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool CheckIfGroupableColumn(OLVColumn column)
        {
            return column != null && !(column.Equals(olvcOrder) || column.Equals(olvcWorkshopID));
        }

        private void RenameTag(ModTag tag, string newTag)
        {
            if (tag != null && string.IsNullOrEmpty(newTag) == false)
            {
                var oldTag = tag.Label;

                if (AvailableTags.ContainsKey(newTag.ToLower()) == false)
                {
                    tag.Label = newTag;

                    AvailableTags.Remove(oldTag.ToLower());
                    AvailableTags[newTag.ToLower()] = tag;
                }
                else if (oldTag.ToLower().Equals(newTag.ToLower()))
                {
                    AvailableTags[oldTag.ToLower()].Label = newTag;
                }

                foreach (var mod in Mods.All)
                {
                    if (mod.Tags.Select(t => t.ToLower()).Contains(oldTag.ToLower()))
                    {
                        mod.Tags.Remove(mod.Tags.FirstOrDefault(t => t.ToLower().Equals(oldTag.ToLower())));
                        AddTag(mod, newTag);
                    }
                }
            }
        }

        private bool AddTag(ModEntry mod, string newTag)
        {
            if (mod != null && string.IsNullOrEmpty(newTag) == false && mod.Tags.Contains(newTag) == false)
            {
                if (AvailableTags.ContainsKey(newTag.ToLower()) == false)
                {
                    AvailableTags[newTag.ToLower()] = new ModTag(newTag);
                }

                mod.Tags.Add(newTag);

                return true;
            }

            return false;
        }


        private void ModListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                //horizontal_splitcontainer.Panel2Collapsed = true;
            }
        }

        /// <summary>
        /// Adjust fore- and backgroundcolor of the OLVListItem, depending on the state of the given ModEntry.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="mod"></param>
        private void SetModListItemColor(OLVListItem item, ModEntry mod)
        {
            if (mod.State.HasFlag(ModState.NotInstalled))
            {
                item.BackColor = Color.LightGray;
                item.ForeColor = Color.Black;
            }
            else if (mod.State.HasFlag(ModState.NotLoaded))
            {
                item.BackColor = Color.LightSteelBlue;
                item.ForeColor = Color.Black;
            }
            else if (mod.isActive && mod.State.HasFlag(ModState.MissingDependencies))
            {
                item.BackColor = Color.LightSalmon;
                item.ForeColor = Color.Black;
            }
            else if (mod.State.HasFlag(ModState.ModConflict))
            {
                item.BackColor = Color.LightCoral;
                item.ForeColor = Color.Black;
            }
            else if (mod.State.HasFlag(ModState.DuplicateID))
            {
                item.BackColor = Color.Plum;
                item.ForeColor = Color.Black;
            }
            else if (mod.isHidden)
            {
                //item.BackColor = SystemColors.InactiveBorder;
                item.ForeColor = Color.Gray;
            }
            else if (mod.State.HasFlag(ModState.New))
            {
                item.BackColor = Color.LightGreen;
            }
        }

        private void ModListFormatRow(object sender, FormatRowEventArgs e)
        {
            var mod = e.Model as ModEntry;
            Contract.Assume(mod != null);

            SetModListItemColor(e.Item, mod);
        }

        private void ModListCellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            var mod = (ModEntry) e.Model;
            if (e.Column.Text != "State")
                return;

            string tooltip = null;

            if (mod.State.HasFlag(ModState.NotLoaded))
                tooltip = "This mod is not being loaded. Check your Mod Path settings.";


            else if (mod.State.HasFlag(ModState.ModConflict))
                tooltip = "This mod makes changes that conflict with another mod.";


            else if (mod.State.HasFlag(ModState.DuplicateID))
                tooltip = "This mods id is not unique. Mods with same id can only be (de-)activated together.";

            e.Text = tooltip;
        }

        /// <summary>
		/// Asynchronously updates the the provided mods and refreshes the ModList content.
		/// Returns immediately if an update task is already running.
        /// </summary>
		/// <param name="mods">Mods that should be updated.</param>
		/// <param name="afterUpdateAction">This Action will be executed after the update processing completed.</param>
        private void UpdateMods(List<ModEntry> mods, Func<Task> afterUpdateAction = null)
        {
            if (IsModUpdateTaskRunning)
            {
                return;
            }
            
            Log.Info($"Updating {mods.Count} mods...");
            SetStatus($"Updating {mods.Count} mods...");
            progress_toolstrip_progressbar.Visible = true;
            UseWaitCursor = true;
            
            var reporter = new Progress<ModUpdateProgress>();
            reporter.ProgressChanged += UpdateProgress;

            void UpdateProgress(object sender, ModUpdateProgress progress)
            {
                if (InvokeRequired) Invoke(new Action(() => UpdateProgress(sender, progress)));
                
                try
                {
                    
                    progress_toolstrip_progressbar.Maximum = progress.Max;
                    progress_toolstrip_progressbar.Value = progress.Current;
                    status_toolstrip_label.Text = progress.Message;
                }
                catch (Exception ex) when (ex is ObjectDisposedException || ex is NullReferenceException)
                {
                    // This can happen, when the main form is closed and the wrapped progress bar control of
                    // the ToolStripProgressBar has already been disposed while the mod update task is still reporting progress.
                }
            }

            ModUpdateCancelSource = new CancellationTokenSource();
            ModUpdateTask = Settings.Mods.UpdateModsAsync(mods, Settings, reporter, ModUpdateCancelSource.Token);
                                    
            ModUpdateTask.ContinueWith(async e =>
            {
                switch (e.Status)
                {
                    case TaskStatus.RanToCompletion:
                        Log.Info("ModUpdateTask completed");
                        SetStatus("Post processing mod list...");
                        await PostProcessModUpdateTask();
                        break;
                    case TaskStatus.Canceled:
                        Log.Info("ModUpdateTask was cancelled");
                        SetStatus("Mod updating aborted");
                        break;
                    case TaskStatus.Faulted:
                        Log.Warn("ModUpdateTask faulted");

                        var aggregateException = e.Exception;

                        if (e.Exception?.InnerException is AggregateException)
                            aggregateException = e.Exception?.GetBaseException() as AggregateException;

                        Log.Error("At least one mod failed to update", aggregateException);
                        SetStatus("At least one mod failed to update");
                        
                        await PostProcessModUpdateTask();

                        MessageBox.Show("At least one mod failed to update: " + 
                                        Environment.NewLine + Environment.NewLine +
                                        aggregateException?.InnerException?.Message +
                                        Environment.NewLine + Environment.NewLine +
                                        "See AML.log for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
            
            return;

            async Task PostProcessModUpdateTask()
            {
                foreach (var mod in Settings.Mods.All)
                {
                    Settings.Mods.UpdatedModDependencyState(mod);
                }
                
                RefreshModList();
                
                if (afterUpdateAction != null) await afterUpdateAction();
                
                SetStatusIdle();
                UseWaitCursor = false;
                
                Log.Info("ModUpdateTask post processing completed");
            }
        }

        void DeleteMods(List<ModEntry> mods, bool keepEntries)
        {
            // Delete / unsubscribe
            foreach (var mod in mods)
            {
                Log.Info("Deleting mod " + mod.ID);

                // Set State for all mods that depend on this one to MissingDependencies
                var dependentMods = Mods.GetDependentMods(mod);
                dependentMods.ForEach(m =>
                {
                    m.SetState(ModState.MissingDependencies);
                    modlist_ListObjectListView.RefreshObject(m);
                });

                // unsubscribe
                if (mod.Source == ModSource.SteamWorkshop)
                    Workshop.Unsubscribe((ulong)mod.WorkshopID);

                if (!keepEntries)
                { 
                    // delete model
                    modlist_ListObjectListView.RemoveObject(mod);
                    Mods.RemoveMod(mod);
                }
                else
                {
                    mod.AddState(ModState.NotInstalled);
                    modlist_ListObjectListView.RefreshObject(mod);
                }

                // delete files
                try
                {
                    Directory.Delete(mod.Path, true);
                }
                catch (DirectoryNotFoundException) 
                {
                    // the directory was already removed
                } 
                catch (Exception ex) 
                {
                    // inform the user if something went wrong
                    string message = $"Error while deleting mod folder: {Environment.NewLine}";
                    message += $"'{mod.Path}' {Environment.NewLine} {Environment.NewLine} {ex.Message}";
                    Log.Warn(message, ex);
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            RefreshModList();
        }

        private void ResubscribeToMods(List<ModEntry> mods)
        {
            if (mods == null || !mods.Any())
            {
                return;
            }

            // Confirmation dialog
            var text = mods.Count == 1
                ? $"Are you sure you want to resubscribe and download the Workshop mod '{mods[0]?.Name}'?"
                : $"Are you sure you want to resubscribe and download {mods.Count} Workshop mods?";

            var result = MessageBox.Show(text, "Confirm download", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            
            if (result != DialogResult.OK)
                return;

            foreach (var mod in mods)
            {
                if (mod.Source == ModSource.SteamWorkshop && mod.State.HasFlag(ModState.NotInstalled)) {
                    Log.Info("Resubscribing to mod " + mod.ID);
                    mod.AddState(ModState.Downloading);
                    modlist_ListObjectListView.RefreshObject(mod);
                    Workshop.Subscribe((ulong) mod.WorkshopID);
                    Workshop.DownloadItem((ulong) mod.WorkshopID);
                }
            }

            string plural = (mods.Count == 1 ? "" : "s");
            MessageBox.Show($"You will have to wait for the download{plural} to finish in order to use the mod{plural}.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConfirmDeleteMods(List<ModEntry> mods)
        {
            if (mods == null || !mods.Any())
            {
                return;
            }

            // Confirmation dialog
            var text = mods.Count == 1
                ? $"Are you sure you want to delete '{mods[0]?.Name}'?"
                : $"Are you sure you want to delete {mods.Count} mods?";

            var result = MessageBox.Show(text, "Confirm deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            
            if (result != DialogResult.OK)
                return;

            DeleteMods(mods, false);
        }

        private void ConfirmUnsubscribeMods(List<ModEntry> mods)
        {
            if (mods == null || !mods.Any())
            {
                return;
            }

            mods = mods.Where(mod => mod.Source == ModSource.SteamWorkshop && !mod.State.HasFlag(ModState.NotInstalled)).ToList();
            
            if (!mods.Any())
            {
                MessageBox.Show("No subscribed Workshop mods selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirmation dialog
            var text = mods.Count == 1
                ? $"Are you sure you want to unsubscribe from the Workshop mod '{mods[0]?.Name}'?"
                : $"Are you sure you want to unsubscribe from {mods.Count} Workshop mods?";

            var result = MessageBox.Show(text, "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            
            if (result != DialogResult.OK)
                return;

            DeleteMods(mods, true);
        }

        private void MoveSelectedModsToCategory(string category)
        {
            modlist_ListObjectListView.BeginUpdate();
            
            // Remember collapsed groups to restore them later, because all groups
            // are expanded when updating a mod from a collapsed group.
            var collapsedGroups = modlist_ListObjectListView.CollapsedGroups.ToList();
            
            foreach (var mod in ModList.SelectedObjects)
            {
                Mods.RemoveMod(mod);
                Mods.AddMod(category, mod);
                modlist_ListObjectListView.UpdateObject(mod);
            }
            
            // Restore previously collapsed groups
            collapsedGroups.ForEach(g => g.Collapsed = true);
            modlist_ListObjectListView.EndUpdate();
        }

        private void RefreshModelFilter()
        {
            var stateFlags = new List<ModState>();

            if (cFilterDuplicate.Checked)
            {
                stateFlags.Add(ModState.DuplicateID);
            }

            if (cFilterConflicted.Checked)
            {
                stateFlags.Add(ModState.ModConflict);
            }

            if (cFilterNew.Checked)
            {
                stateFlags.Add(ModState.New);
            }

            if (cFilterNotInstalled.Checked)
            {
                stateFlags.Add(ModState.NotInstalled);
            }

            if (cFilterNotLoaded.Checked)
            {
                stateFlags.Add(ModState.NotLoaded);
            }

            if (cFilterMissingDependency.Checked)
            {
                stateFlags.Add(ModState.MissingDependencies);
            }

            modlist_ListObjectListView.ModelFilter = new ModListFilter(modlist_ListObjectListView, modlist_FilterCueTextBox.Text, stateFlags, cFilterHidden.Checked);
        }

        /// <summary>
        /// Clears the Mod list view and reloads the ModEntry model objects.
        /// </summary>
        /// <param name="rebuildColumns">Set to true if visibility for some columns was changed for example.</param>
        private void RefreshModList(bool rebuildColumns = false)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => RefreshModList(rebuildColumns)));
                return;
            }
            
            var selectedMod = ModList.SelectedObject;

            // Un-register events
            modlist_ListObjectListView.SelectionChanged -= ModListSelectionChanged;
            modlist_ListObjectListView.ItemChecked -= ModListItemChecked;

            modlist_ListObjectListView.BeginUpdate();

            // add elements
            modlist_ListObjectListView.ClearObjects();

            modlist_ListObjectListView.Objects = Settings.ShowHiddenElements ? Mods.All : Mods.All.Where(m => !m.isHidden);

            if (rebuildColumns)
            {
                modlist_ListObjectListView.RebuildColumns();
            }
            
            modlist_ListObjectListView.Sort();

            modlist_ListObjectListView.EndUpdate();

            // Re-register events
            modlist_ListObjectListView.SelectionChanged += ModListSelectionChanged;
            modlist_ListObjectListView.ItemChecked += ModListItemChecked;

            // restore last selection
            if (selectedMod != null)
                modlist_ListObjectListView.SelectObject(selectedMod);

            UpdateStateFilterLabels();
            
            UpdateConflictInfo();
        }

        private void RenameTagPrompt(ModEntry m, ModTag tag, bool renameAll)
        {
            var prompt = renameAll ? $"Enter new name for all instances of tag '{tag.Label}'."
                                   : $"Enter new name for tag '{tag.Label}' on mod '{m.Name}'.";

            var newTag = Interaction.InputBox(prompt, "Rename tag", tag.Label);

            if (newTag == tag.Label)
                return;

            if (string.IsNullOrEmpty(newTag) || (renameAll && MessageBox.Show($@"Are you sure you want to rename all instances of tag '{tag.Label}' to '{newTag}'?",
                                                                               @"Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes))
            { 
                return;
            }

            if (renameAll)
            {
                RenameTag(tag, newTag);
            }
            else
            {
                m.Tags.Remove(m.Tags.FirstOrDefault(t => t.ToLower().Equals(tag.Label.ToLower())));

                if (m.Tags.Contains(newTag) == false)
                {
                    AddTag(m, newTag);
                }
            }
        }
        
        private ContextMenuStrip CreateModListContextMenu(ModEntry m, ModTag tag)
        {
            var menu = new ContextMenuStrip();
            if (m?.ID == null || tag == null)
                return menu;
            
            // change color
            var changeColorItem = new ToolStripMenuItem("Change color");

            var editColor = new ToolStripMenuItem("Edit");

            editColor.Click += (sender, e) =>
            {
                var colorPicker = new ColorDialog
                {
                    AllowFullOpen = true,
                    Color = tag.Color,
                    AnyColor = true,
                    FullOpen = true
                };

                if (colorPicker.ShowDialog() == DialogResult.OK)
                    tag.Color = colorPicker.Color;
            };

            changeColorItem.DropDownItems.Add(editColor);

            var makePastelItem = new ToolStripMenuItem("Make pastel");

            makePastelItem.Click += (sender, e) => tag.Color = tag.Color.GetPastelShade();

            changeColorItem.DropDownItems.Add(makePastelItem);

            var changeShadeItem = new ToolStripMenuItem("Random shade");

            changeShadeItem.Click += (sender, e) => tag.Color = tag.Color.GetRandomShade(0.33, 1.0);

            changeColorItem.DropDownItems.Add(changeShadeItem);

            var randomColorItem = new ToolStripMenuItem("Random color");

            randomColorItem.Click += (sender, e) => tag.Color = ModTag.RandomColor();

            changeColorItem.DropDownItems.Add(randomColorItem);
            menu.Items.Add(changeColorItem);

            menu.Items.Add("-");

            // renaming tags
            var renameTagItem = new ToolStripMenuItem($"Rename '{tag.Label}'");

            renameTagItem.Click += (sender, e) => RenameTagPrompt(m, tag, false);

            menu.Items.Add(renameTagItem);

            var renameAllTagItem = new ToolStripMenuItem($"Rename all '{tag.Label}'");

            renameAllTagItem.Click += (sender, e) => RenameTagPrompt(m, tag, true);
            menu.Items.Add(renameAllTagItem);

            menu.Items.Add("-");

            // removing tags
            var removeTagItem = new ToolStripMenuItem($"Remove '{tag.Label}'");

            removeTagItem.Click += (sender, args) => m.Tags.Remove(tag.Label);
            menu.Items.Add(removeTagItem);
            
            var removeAllTagItem = new ToolStripMenuItem($"Remove all '{tag.Label}'");

            removeAllTagItem.Click += (sender, args) =>
            {
                if (MessageBox.Show($@"Are you sure you want to remove all instances of tag '{tag.Label}'?",
                        @"Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                foreach (var mod in Mods.All)
                {
                    for (int i = 0; i < mod.Tags.Count; ++i)
                    {
                        if (mod.Tags[i].ToLower().Equals(tag.Label.ToLower()))
                        {
                            mod.Tags.RemoveAt(i--);
                        }
                    }
                }
            };
            menu.Items.Add(removeAllTagItem);

            return menu;
        }

        private ContextMenuStrip CreateModListContextMenu(ModEntry m, OLVListItem currentItem)
        {
            var menu = new ContextMenuStrip();
            
            if (m?.ID == null)
                return menu;

            var selectedMods = ModList.SelectedObjects.ToList();

            ToolStripMenuItem renameItem = null;
            ToolStripMenuItem showInExplorerItem = null;
            ToolStripMenuItem showOnSteamItem = null;
            ToolStripMenuItem showInBrowser = null;
            ToolStripMenuItem fetchWorkshopTagsItem = null;
            ToolStripMenuItem enableAllItem = null;
            ToolStripMenuItem disableAllItem = null;
            ToolStripMenuItem disableDuplicates = null;
            ToolStripMenuItem restoreDuplicates = null;
            ToolStripMenuItem resubscribeItem = null;
            ToolStripMenuItem unsubscribeItem = null;
            ToolStripMenuItem copyToClipboard = new ToolStripMenuItem("Copy to clipboard");

            copyToClipboard.DropDownItems.Add("Name", null, delegate
            {
                StringBuilder sb = new StringBuilder();
                selectedMods.Aggregate(sb, (result, item) => sb.Append(item.Name + Environment.NewLine));
                Clipboard.SetText(sb.ToString().TrimEnd(Environment.NewLine.ToCharArray()));
            });

            copyToClipboard.DropDownItems.Add("Path", null, delegate
            {
                StringBuilder sb = new StringBuilder();
                selectedMods.Aggregate(sb, (result, item) => sb.Append(item.Path + Environment.NewLine));
                Clipboard.SetText(sb.ToString().TrimEnd(Environment.NewLine.ToCharArray()));
            });

            copyToClipboard.DropDownItems.Add("Steam URL", null, delegate
            {
                StringBuilder sb = new StringBuilder();
                selectedMods.ForEach(mod =>
                {
                    sb.Append(mod.WorkshopID > 0 ? mod.GetSteamLink() : "N/A");
                    sb.Append(Environment.NewLine);
                });
                Clipboard.SetText(sb.ToString().TrimEnd(Environment.NewLine.ToCharArray()));
            });
        
            copyToClipboard.DropDownItems.Add("Browser URL", null, delegate
            {
                StringBuilder sb = new StringBuilder();
                selectedMods.ForEach(mod =>
                {
                    sb.Append(mod.WorkshopID > 0 ? mod.GetWorkshopLink() : "N/A");
                    sb.Append(Environment.NewLine);
                });
                Clipboard.SetText(sb.ToString().TrimEnd(Environment.NewLine.ToCharArray()));
            });

            // create items that appear only when a single mod is selected
            if (selectedMods.Count == 1)
            {
                renameItem = new ToolStripMenuItem("Rename");
                renameItem.Click += (a, b) => { modlist_ListObjectListView.EditSubItem(currentItem, olvcName.Index); };

                if (!m.State.HasFlag(ModState.NotInstalled))
                {
                    showInExplorerItem = new ToolStripMenuItem("Show in Explorer", null, delegate { m.ShowInExplorer(); });
                }

                if (m.WorkshopID > 0)
                {
                    showOnSteamItem = new ToolStripMenuItem("Show on Steam", null, delegate { m.ShowOnSteam(); });
                    showInBrowser = new ToolStripMenuItem("Show in Browser", null, delegate { m.ShowInBrowser(); });
                }

                var duplicateMods = Mods.All.Where(mod => mod.ID == m.ID && mod != m).ToList();
                if (duplicateMods.Any())
                {
                    if (!m.State.HasFlag(ModState.DuplicatePrimary))
                    {
                        disableDuplicates = new ToolStripMenuItem("Prefer this duplicate");
                        disableDuplicates.Click += delegate
                        {
                            // disable all other duplicates
                            foreach (var duplicate in duplicateMods)
                            {
                                duplicate.DisableModFile();
                                duplicate.RemoveState(ModState.DuplicateID);
                                duplicate.RemoveState(ModState.DuplicatePrimary);
                                duplicate.AddState(ModState.DuplicateDisabled);
                                duplicate.isActive = false;
                                modlist_ListObjectListView.RefreshObject(duplicate);
                            }

                            // mark selected mod as primary duplicate
                            m.EnableModFile();
                            m.RemoveState(ModState.DuplicateID);
                            m.RemoveState(ModState.DuplicateDisabled);
                            m.AddState(ModState.DuplicatePrimary);
                            m.isActive = true;
                            modlist_ListObjectListView.RefreshObject(m);
                            ProcessModListItemCheckChanged(m);
                        };
                    }

                    if (m.State.HasFlag(ModState.DuplicatePrimary) || m.State.HasFlag(ModState.DuplicateDisabled))
                    {
                        restoreDuplicates = new ToolStripMenuItem("Restore duplicates");
                        restoreDuplicates.Click += delegate
                        {
                            // restore normal duplicate state
                            foreach (var duplicate in duplicateMods)
                            {
                                duplicate.EnableModFile();
                                duplicate.RemoveState(ModState.DuplicateDisabled);
                                duplicate.RemoveState(ModState.DuplicatePrimary);
                                duplicate.AddState(ModState.DuplicateID);
                                duplicate.isActive = false;
                                modlist_ListObjectListView.RefreshObject(duplicate);
                            }

                            // mark selected mod as primary duplicate
                            m.EnableModFile();
                            m.RemoveState(ModState.DuplicateDisabled);
                            m.RemoveState(ModState.DuplicatePrimary);
                            m.AddState(ModState.DuplicateID);
                            m.isActive = false;
                            modlist_ListObjectListView.RefreshObject(m);
                            ProcessModListItemCheckChanged(m);
                        };
                    }
                }
            }

            var addTagItem = new ToolStripMenuItem("Add tag(s)...");
            addTagItem.Click += (sender, args) =>
            {
                var newTag = Interaction.InputBox($"Please specify one or more tags (separated by a semicolon) that should be added to {selectedMods.Count} selected mod(s).", "Add tag(s)");

                if (newTag == "")
                    return;

                var tags = newTag.Split(';');

                foreach (ModEntry mod in modlist_ListObjectListView.SelectedObjects)
                {
                    foreach (string tag in tags)
                    {
                        AddTag(mod, tag.Trim());
                    }
                }
            };

            // Move to ...
            var moveToCategoryItem = new ToolStripMenuItem("Move to category");
            // ... new category
            moveToCategoryItem.DropDownItems.Add("New category", null, delegate
            {
                var category = Interaction.InputBox("Please enter the name of the new category", "Create category", "New category");

                if (string.IsNullOrEmpty(category))
                    return;

                MoveSelectedModsToCategory(category);
            });

            moveToCategoryItem.DropDownItems.Add("-");

            // ... existing category
            foreach (var category in Settings.Mods.CategoryNames.OrderBy(c => c))
            {
                if (category == Mods.GetCategory(m))
                    continue;

                moveToCategoryItem.DropDownItems.Add(category, null, delegate { MoveSelectedModsToCategory(category); });
            }

            // Hide/unhide
            var toggleVisibility = new ToolStripMenuItem {Text = m.isHidden ? "Unhide" : "Hide"};
            toggleVisibility.Click += delegate
            {
                // save as new list so we can remove mods if they are being hidden
                foreach (var mod in selectedMods)
                {
                    mod.isHidden = !m.isHidden;

                    if (!Settings.ShowHiddenElements && mod.isHidden)
                    {
                        modlist_ListObjectListView.RemoveObject(mod);
                        RefreshModelFilter();
                    }
                    else
                    {
                        modlist_ListObjectListView.RefreshObject(mod);
                        RefreshModelFilter();
                    }
                }
            };

            // Update mods
            var updateItem = new ToolStripMenuItem("Update", null, delegate
            {
                if (IsModUpdateTaskRunning)
                {
                    ShowModUpdateRunningMessageBox();
                    return;
                }

                UpdateMods(selectedMods);
            });

            if (selectedMods.Any(mod => mod.WorkshopID > 0))
            {
                List<ModEntry> modsToUpdate = new List<ModEntry>(selectedMods.Where(mod => mod.WorkshopID > 0));

                fetchWorkshopTagsItem = new ToolStripMenuItem("Use workshop tags");
                fetchWorkshopTagsItem.Click += delegate
                {
                    if (modsToUpdate.Count > 1)
                    {
                        var result = MessageBox.Show($"Tags from the workshop will replace the existing tags for {modsToUpdate.Count} mods." + 
                                                     Environment.NewLine + "Do you want to continue?",
                                                     "Use workshop tags", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                            return;
                    }

                    Log.Info($"Using workshop tags for {modsToUpdate.Count} mods.");

                    foreach (var selItem in modsToUpdate)
                    {
                        var tags = selItem.SteamTags;
                        if (tags.Any())
                        {
                            selItem.Tags.Clear();
                            
                            foreach (string tag in tags)
                            {
                                AddTag(selItem, tag);
                            }
                        }
                    }

                };
            }

            var workShopMods = selectedMods.Where(mod => mod.Source == ModSource.SteamWorkshop).ToList();
            if (workShopMods.Any())
            {
                var nonInstalledWorkShopMods = workShopMods.Where(mod => mod.State.HasFlag(ModState.NotInstalled)).ToList();
                if (nonInstalledWorkShopMods.Any())
                {
                    resubscribeItem = new ToolStripMenuItem("Resubscribe", null, delegate { ResubscribeToMods(nonInstalledWorkShopMods); });
                    resubscribeItem.ToolTipText = "Re-subscribes to the selected the mod(s) in the Workshop and starts downloading.";
                }

                var installedWorkShopMods = workShopMods.Where(mod => !mod.State.HasFlag(ModState.NotInstalled)).ToList();
                if (installedWorkShopMods.Any())
                {
                    unsubscribeItem = new ToolStripMenuItem("Unsubscribe", null, delegate { ConfirmUnsubscribeMods(installedWorkShopMods); });
                    unsubscribeItem.ToolTipText = "Unsubscribes the selected the mod(s) from the Workshop, but keeps the mod(s) listed in AML, so you can re-subscribe later.";
                }
            }

            var modsNotActive = selectedMods.Where(mod => !mod.isActive).ToList();
            if (modsNotActive.Any())
            {
                enableAllItem = new ToolStripMenuItem("Enable");
                enableAllItem.Click += delegate
                {
                    // If mods get enabled with UpdateModsOnStartup disabled or OnlyUpdateEnabledOrNewModsOnStartup active, we perform an update because mod data could be outdated.
                    if (!Settings.UpdateModsOnStartup || Settings.OnlyUpdateEnabledOrNewModsOnStartup)
                    {
                        if (IsModUpdateTaskRunning)
                        {
                            ShowModUpdateRunningMessageBox();
                            return;
                        }

                        Log.Info($"Updating selected mods before enabling because {nameof(Settings.OnlyUpdateEnabledOrNewModsOnStartup)} is enabled");
                        Cursor.Current = Cursors.WaitCursor;
                        UpdateMods(modsNotActive, () =>
                        {
                            Invoke(new Action(() => 
                            {
                                EnabledModsInModList(modsNotActive);
                            }));

                            return Task.CompletedTask;
                        });
                    }
                    else
                    {
                        EnabledModsInModList(modsNotActive);
                    }

                    void EnabledModsInModList(List<ModEntry> mods)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        foreach (var mod in mods)
                        {
                            _CheckTriggeredFromContextMenu = true;
                            modlist_ListObjectListView.CheckObject(mod);
                        }
                        Cursor.Current = Cursors.Default;
                    }
                };
            }
            
            var modsActive = selectedMods.Where(mod => mod.isActive).ToList();
            if (modsActive.Any())
            {
                disableAllItem = new ToolStripMenuItem("Disable");
                disableAllItem.Click += delegate
                {
                    Cursor.Current = Cursors.WaitCursor;
                    foreach (var mod in modsActive)
                    {
                        _CheckTriggeredFromContextMenu = true;
                        modlist_ListObjectListView.UncheckObject(mod);
                    }
                    Cursor.Current = Cursors.Default;
                };
            }

            var deleteItem = new ToolStripMenuItem("Delete", null, delegate { ConfirmDeleteMods(selectedMods); });
            deleteItem.ToolTipText = "Unsubscribes the selected mod(s) from the Workshop, deletes the mod folder(s) and removes the mod(s) from AML.";

            // create menu structure
            if (enableAllItem != null)
                menu.Items.Add(enableAllItem);

            if (disableAllItem != null)
                menu.Items.Add(disableAllItem);

            if (renameItem != null)
                menu.Items.Add(renameItem);

            menu.Items.Add(updateItem);
            menu.Items.Add("-");
            menu.Items.Add(addTagItem);

            if (fetchWorkshopTagsItem != null)
                menu.Items.Add(fetchWorkshopTagsItem);

            menu.Items.Add(moveToCategoryItem);
            menu.Items.Add("-");

            if (showInExplorerItem != null)
                menu.Items.Add(showInExplorerItem);

            if (showOnSteamItem != null)
                menu.Items.Add(showOnSteamItem);

            if (showInBrowser != null)
                menu.Items.Add(showInBrowser);

            // prevent double separator
            if (menu.Items[menu.Items.Count - 1].Text != @"-")
                menu.Items.Add("-");

            menu.Items.Add(toggleVisibility);

            if (resubscribeItem != null)
                menu.Items.Add(resubscribeItem);

            if (unsubscribeItem != null)
                menu.Items.Add(unsubscribeItem);

            menu.Items.Add(deleteItem);

            if (Settings.EnableDuplicateModIdWorkaround)
            {
                if (disableDuplicates != null)
                {
                    menu.Items.Add("-");
                    menu.Items.Add(disableDuplicates);
                }

                if (restoreDuplicates != null)
                {
                    // prevent double separator
                    if (menu.Items[menu.Items.Count - 1] != disableDuplicates)
                        menu.Items.Add("-");

                    menu.Items.Add(restoreDuplicates);
                }
            }

            if (copyToClipboard.DropDownItems.Count > 0)
            {
                // prevent double separator
                if (menu.Items[menu.Items.Count - 1].Text != @"-")
                    menu.Items.Add("-");

                menu.Items.Add(copyToClipboard);
            }

            return menu;
        }

        /// <summary>
        /// Check if the specified <param name="mod"></param> is eligible to switch its "enabled" state to <param name="newState"></param>.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        bool ProcessNewModState(ModEntry mod, bool newState)
        {
            if (newState)
            {
                // Mod can not be enabled if it is a disabled duplicate
                if (mod.State.HasFlag(ModState.DuplicateDisabled))
                {
                    MessageBox.Show("Disabled duplicates can not be used. Make this the primary duplicate or remove all other duplicates to use this mod.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                // Mod can not be enabled if it is not installed
                if (mod.State.HasFlag(ModState.NotInstalled))
                {
                    return false;
                }
            }

            return newState;
        }

        void ProcessModListItemCheckChanged(ModEntry modChecked)
        {
            //Debug.WriteLine("ProcessModListItemCheckChanged " + modChecked.Name);

            // If a mod gets enabled with Settings.UpdateModsOnStartup disabled or OnlyUpdateEnabledOrNewModsOnStartup active, we perform an update because mod data could be outdated.
            if (modChecked.isActive && (!Settings.UpdateModsOnStartup || Settings.OnlyUpdateEnabledOrNewModsOnStartup) && !_CheckTriggeredFromContextMenu)
            {
                Log.Info($"Updating mod before enabling because {nameof(Settings.OnlyUpdateEnabledOrNewModsOnStartup)} is enabled");
                
                Task.Run(() => Mods.UpdateModAsync(modChecked, Settings)).GetAwaiter().GetResult();
            }

            _CheckTriggeredFromContextMenu = false;

            List<ModEntry> checkedMods = new List<ModEntry>();
            
            // If there is a duplicate id conflict check all
            // at the same time and mark them as updated
            if (modChecked.State.HasFlag(ModState.DuplicateID))
            {
                foreach (var mod in Mods.All.Where(@mod => mod.ID == modChecked.ID && modChecked.State.HasFlag(ModState.DuplicateID)))
                {
                    mod.isActive = modChecked.isActive;
                    checkedMods.Add(mod);
                }
            }
            // Otherwise just mark the one as updated
            else
            {
                checkedMods.Add(modChecked);
            }

            UpdateConflictsForMods(checkedMods);

            // refresh dependent mods for every mod where the checkstate changed
            foreach (var mod in checkedMods)
            {
                mod.RemoveState(ModState.New);
                modlist_ListObjectListView.RefreshObject(mod);

                // refresh dependent mods
                var dependentMods = Mods.GetDependentMods(mod, false);
                foreach (var m in dependentMods)
                {
                    Mods.UpdatedModDependencyState(m);
                }
                
                modlist_ListObjectListView.RefreshObjects(dependentMods);
            }

            UpdateStateFilterLabels();
            UpdateLabels();
            UpdateDependencyInformation(ModList.SelectedObject);
        }

        #region Events

        private ModTag HitTest(IEnumerable<string> tags, Graphics g, CellEventArgs e)
        {
            if (tags == null || e.SubItem == null)
                return null;

            var bounds = e.SubItem.Bounds;
            var offset = new Point(bounds.X + TagRenderInfo.rX,
                                   bounds.Y + TagRenderInfo.rY);
            var tagList = AvailableTags.Where(t => tags.Select(s => s.ToLower()).Contains(t.Key)).Select(kvp => kvp.Value);

            foreach (var tag in tagList)
            {
                var tagSize = g.MeasureString(tag.Label, e.SubItem.Font).ToSize();
                var renderInfo = new TagRenderInfo(offset, bounds, tagSize, Color.Black);

                if (renderInfo.HitBox.Contains(e.Location))
                {
                    return tag;
                }

                offset.X += renderInfo.HitBox.Width + TagRenderInfo.rX;
                // stop drawing outside of the column bounds
                if (offset.X > bounds.Right)
                    break;
            }

            return null;
        }

        private void ModListCellRightClick(object sender, CellRightClickEventArgs e)
        {
            var mod = e.Model as ModEntry;
            var graphics = e.ListView.CreateGraphics();
            var selectedTag = e.SubItem != null && e.Column == olvcTags 
                            ? HitTest(mod?.Tags, graphics, e) : null;
            var menu = selectedTag == null 
                ? CreateModListContextMenu(mod, e.Item)
                : CreateModListContextMenu(mod, selectedTag);

            menu.Show(e.ListView, e.Location);
        }

        private bool ModListBooleanCheckStatePutter(object rowobject, bool newValue)
        {
            if (!(rowobject is ModEntry mod)) 
                return !newValue;

            newValue = ProcessNewModState(mod, newValue);
            mod.isActive = newValue;

            // If the mod is not visible due to filtering, the check state changed event
            // will not fire and we have to process the new state manually
            if (!ModList.Objects.Contains(mod))
            {
                // run on new thread and wait to not deadlock ui
                ProcessModListItemCheckChanged(mod);
            }

            return newValue;
        }

        private void ModListItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var mod = ModList.GetModelObject(e.Item.Index);
            ProcessModListItemCheckChanged(mod);
        }

        private void ModListItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!Settings.UpdateModsOnStartup || Settings.OnlyUpdateEnabledOrNewModsOnStartup)
            {
                // With OnlyUpdateEnabledOrNewModsOnStartup enabled, we prevent multiple mods from getting enabled
                // by multiselecting and clicking on the check box. This would cause every checked mod to get updated individually,
                // which is really slow in comparison to batch requests.
                if (e.NewValue == CheckState.Checked && modlist_ListObjectListView.SelectedObjects.Count > 1 && !_CheckTriggeredFromContextMenu)
                {
                    MessageBox.Show("Use 'enable' from the right click menu to enable multiple mods at once.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Deselect all mods except this one to prevent multiple check changes
                    modlist_ListObjectListView.SelectedIndex = e.Index;

                    // Do not change the check state
                    e.NewValue = e.CurrentValue;
                }
            }
        }

        private void ModListSelectionChanged(object sender, EventArgs e)
        {
            CurrentMod = ModList.SelectedObjects.Count != 1 ? null : ModList.SelectedObject;

            UpdateModInfo(CurrentMod);
            
            if (CurrentMod != null)
            {
                CurrentMod.RemoveState(ModState.New);
                modlist_ListObjectListView.EnsureModelVisible(CurrentMod);
                modlist_ListObjectListView.RefreshObject(CurrentMod);
            }

            UpdateStateFilterLabels();
        }

        private async void ModListEditFinished(object sender, CellEditEventArgs e)
        {
            var mod = e.RowObject as ModEntry;
            if (mod == null) return;

            switch (e.Column.AspectName)
            {
                case "Name":
                    mod.ManualName = !string.IsNullOrEmpty(e.NewValue as string);

                    if (!mod.ManualName)
                    {
                        // Restore name
                        await Mods.UpdateModAsync(mod, Settings);
                    }

                    break;
                case "Index":
                    if (Settings.AutoNumberIndexes == false) break;
                    if ((int)e.NewValue == (int)e.Value) break;
                    modlist_ListObjectListView.BeginUpdate();
                    ReorderIndexes(mod, (int)e.Value);
                    modlist_ListObjectListView.Sort();
                    modlist_ListObjectListView.EndUpdate();
                    break;
                case "Category":
                    MoveSelectedModsToCategory((string)e.NewValue);
                    break;
                    
            }
        }

        #endregion

        /// <summary>
        /// Reorders mod indexes if a mod has its index changed.
        /// </summary>
        /// <param name="mod">The mod object that was updated</param>
        /// <param name="oldIndex">The old index the mod had</param>
        private void ReorderIndexes(ModEntry mod, int oldIndex)
        {
            var currentIndex = mod.Index;
            var modList = Mods.All.ToList();
            int startPos = (currentIndex > oldIndex) ? oldIndex : currentIndex;
            int endPos = (currentIndex < oldIndex) ? oldIndex : currentIndex;
            int i = 0;
            
            // Make sure the old indexes go from 0 to Length - 1
            mod.Index = oldIndex;
            foreach (var modEntry in modList.OrderBy(m => m.Index))
                modEntry.Index = i++;
            
            // Fix new indexes outside of the valid range
            if (currentIndex < 0)
                currentIndex = 0;
            else if (currentIndex >= Mods.All.ToArray().Length)
                currentIndex = Mods.All.ToArray().Length - 1;
            
            // Set the new indexes
            mod.Index = currentIndex;
            foreach (var modEntry in modList.Where(m => m.Index >= startPos && m.Index <= endPos && m != mod))
                modEntry.Index += (currentIndex - oldIndex > 0) ? -1 : 1;
            
            RefreshModList();
        }
    }
}