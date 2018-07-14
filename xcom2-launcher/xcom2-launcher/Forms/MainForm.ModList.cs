using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Microsoft.VisualBasic;
using XCOM2Launcher.Helper;
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.Forms
{
    public partial class MainForm : Form
    {
        public ModList Mods => Settings.Mods;
        public Dictionary<string, ModTag> AvailableTags => Settings.Tags;

        public TypedObjectListView<ModEntry> ModList { get; private set; }

        public List<ModEntry> Downloads { get; } = new List<ModEntry>();

        public ModEntry CurrentMod;

        public void InitObjectListView()
        {
            //modlist_objectlistview?.Dispose();

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

                tags.All(t => AddTag((ModEntry) rowObject, t.Trim()));
            };
            olvcTags.SearchValueGetter = rowObject => ((ModEntry)rowObject).Tags.ToArray();
            olvcTags.AspectGetter = rowObject => "";


            olvcState.AspectGetter = o =>
            {
                var mod = (ModEntry)o;

                if (mod.State.HasFlag(ModState.NotLoaded))
                    return "Not Loaded";

                if (mod.State.HasFlag(ModState.NotInstalled))
                    return "Not Installed";

                if (mod.State.HasFlag(ModState.ModConflict))
                    return "Conflict";

                if (mod.State.HasFlag(ModState.DuplicateID))
                    return "Duplicate ID";

                if (mod.State.HasFlag(ModState.New))
                    return "New";

                if (mod.State.HasFlag(ModState.UpdateAvailable))
                    return "Update Available";

                return "OK";
            };

            olvcSize.AspectToStringConverter = size => ((long)size).FormatAsFileSize();

            olvcLastUpdated.DataType = typeof(DateTime?);
            olvcDateAdded.DataType = typeof(DateTime?);
            olvcDateCreated.DataType = typeof(DateTime?);

            olvcPath.GroupKeyGetter = o => Path.GetDirectoryName((o as ModEntry)?.Path);


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
            
            // Sort by Order or WorkshopID column removes groups
            modlist_ListObjectListView.BeforeSorting += (sender, args) =>
            {
                modlist_ListObjectListView.ShowGroups = 
                !(args.ColumnToSort.Equals(olvcOrder) || args.ColumnToSort.Equals(olvcWorkshopID));
            };

            // Init DateTime columns
            foreach (var column in columns.Where(c => c.DataType == typeof (DateTime?)))
            {
                column.AspectToStringConverter = d => (d as DateTime?)?.ToLocalTime().ToString(CultureInfo.CurrentCulture);
                column.MakeGroupies(
                    new[] { DateTime.Now.Subtract(TimeSpan.FromHours(24 * 30)), DateTime.Now.Subtract(TimeSpan.FromHours(24 * 7)), DateTime.Now.Date },
                    new[] { "Older Than One Month", "This Month", "This Week", "Today" });

                // Sord Desc
                column.GroupFormatter = (g, param) => { param.GroupComparer = Comparer<OLVGroup>.Create((a, b) => (param.GroupByOrder == SortOrder.Descending ? 1 : -1)*a.Header.CompareTo(b.Header)); };
            }


            // Wrapper
            ModList = new TypedObjectListView<ModEntry>(modlist_ListObjectListView);

            // Events
            //modlist_objectlistview.SelectionChanged += ModListSelectionChanged;
            //modlist_objectlistview.ItemChecked += ModListItemChecked;
            //modlist_objectlistview.CellRightClick += ModListCellRightClick;
            //modlist_objectlistview.CellEditFinished += ModListEditFinished;
            //modlist_objectlistview.CellToolTipShowing += ModListCellToolTipShowing;
            //modlist_objectlistview.FormatRow += ModListFormatRow;
            //modlist_objectlistview.GroupExpandingCollapsing += ModListGroupExpandingCollapsing;
            //modlist_objectlistview.KeyUp += ModListKeyUp;
            //modlist_objectlistview.KeyDown += ModListKeyDown;

            // Restore State
            if (Settings.Windows.ContainsKey("main") && Settings.Windows["main"].Data != null)
                modlist_ListObjectListView.RestoreState(Settings.Windows["main"].Data);

            RefreshModList();

            // Start out sorted by name
            modlist_ListObjectListView.Sort(olvcName, SortOrder.Ascending);
        }

        private void RenameTag(ModTag tag, string newTag)
        {
            if (tag != null && string.IsNullOrEmpty(newTag) == false)
            {
                var oldTag = tag.Label;

                if (AvailableTags.ContainsKey(newTag) == false)
                {
                    tag.Label = newTag;

                    AvailableTags.Remove(oldTag);
                    AvailableTags[newTag] = tag;
                }

                foreach (var mod in Mods.All)
                {
                    if (mod.Tags.Contains(oldTag))
                    {
                        mod.Tags.Remove(oldTag);
                        AddTag(mod, newTag);
                    }
                }
            }
        }

        private bool AddTag(ModEntry mod, string newTag)
        {
            if (mod != null && string.IsNullOrEmpty(newTag) == false && mod.Tags.Contains(newTag) == false)
            {
                if (AvailableTags.ContainsKey(newTag) == false)
                {
                    AvailableTags[newTag] = new ModTag(newTag);
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

        private void ModListKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void ModListGroupExpandingCollapsing(object sender, GroupExpandingCollapsingEventArgs e)
        {
            // only handle if grouped by name or id
            if (modlist_ListObjectListView.PrimarySortColumn.AspectName != "Name" &&
                modlist_ListObjectListView.PrimarySortColumn.AspectName != "ID")
                return;

            if (e.Group.Key == null)
                return;

            var key = e.Group.Key as string;
            Contract.Assume(key != null);

            if (Mods.Entries.ContainsKey(key))
                Mods.Entries[key].Collapsed = !e.IsExpanding;
        }

        private void ModListFormatRow(object sender, FormatRowEventArgs e)
        {
            var mod = e.Model as ModEntry;
            Contract.Assume(mod != null);

            var item = e.Item;


            if (mod.State.HasFlag(ModState.NotLoaded))
            {
                item.BackColor = SystemColors.GradientInactiveCaption;
                item.ForeColor = SystemColors.InactiveCaptionText;
            }

            else if (mod.State.HasFlag(ModState.ModConflict))
            {
                item.BackColor = Color.PaleVioletRed;
                item.ForeColor = Color.Black;
            }


            else if (mod.State.HasFlag(ModState.DuplicateID))
            {
                item.BackColor = Color.PaleVioletRed;
                item.ForeColor = Color.Black;
            }


            else if (mod.isHidden)
                item.BackColor = SystemColors.InactiveBorder;

            else if (mod.State.HasFlag(ModState.New))
                item.BackColor = SystemColors.Info;
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

        private void UpdateMod(ModEntry m)
        {
            if (m.isHidden && !Settings.ShowHiddenElements)
                // cant update what's not there
                return;

            modlist_ListObjectListView.UpdateObject(m);
        }


        private void DeleteMods()
        {
            // Confirmation dialog
            var text = modlist_ListObjectListView.SelectedObjects.Count == 1
                ? $"Are you sure you want to delete '{ModList.SelectedObjects[0]?.Name}'?"
                : $"Are you sure you want to delete {modlist_ListObjectListView.SelectedObjects.Count} mods?";

            text += "\r\nThis can not be undone.";

            var r = MessageBox.Show(text, "Confirm deletion", MessageBoxButtons.OKCancel);
            if (r != DialogResult.OK)
                return;

            // Delete
            var mods = ModList.SelectedObjects.ToList();
            foreach (var mod in mods)
            {
                // unsubscribe
                if (mod.Source == ModSource.SteamWorkshop)
                    Steam.Workshop.Unsubscribe((ulong) mod.WorkshopID);

                // delete model
                modlist_ListObjectListView.RemoveObject(mod);
                Mods.RemoveMod(mod);

                // delete files
                try
                {
                    Directory.Delete(mod.Path, true);
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    // the directory was already removed
                }
            }

            RefreshModList();
            UpdateConflicts();
        }

        private void MoveMods(string category)
        {
            modlist_ListObjectListView.BeginUpdate();
            foreach (var mod in ModList.SelectedObjects)
            {
                Mods.RemoveMod(mod);
                Mods.AddMod(category, mod);
                modlist_ListObjectListView.UpdateObject(mod);
            }
            modlist_ListObjectListView.EndUpdate();
        }


        private void RefreshModList()
        {
            // Un-register events
            modlist_ListObjectListView.SelectionChanged -= ModListSelectionChanged;
            modlist_ListObjectListView.ItemChecked -= ModListItemChecked;

            modlist_ListObjectListView.BeginUpdate();

            // add elements
            modlist_ListObjectListView.ClearObjects();

            modlist_ListObjectListView.Objects = Settings.ShowHiddenElements ? Mods.All : Mods.All.Where(m => !m.isHidden);

            modlist_ListObjectListView.EndUpdate();

            // Re-register events
            modlist_ListObjectListView.SelectionChanged += ModListSelectionChanged;
            modlist_ListObjectListView.ItemChecked += ModListItemChecked;
        }

        private void RenameTagPrompt(ModEntry m, ModTag tag, bool renameAll)
        {
            var prompt = renameAll ? $"Rename all instances of tag '{tag.Label}' ?"
                                   : $"Rename tag '{tag.Label}' for '{m.Name}' ?";
            var newTag = Interaction.InputBox(prompt, "Rename tag", tag.Label);

            if (string.IsNullOrEmpty(newTag) || (renameAll && MessageBox.Show($@"Are you sure you want to rename all instances of tag '{tag.Label}' to {newTag}?",
                                                                               @"Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes))
            { 
                return;
            }

            if (newTag != tag.Label)
            {
                if (renameAll)
                {
                    RenameTag(tag, newTag);
                }
                else
                {
                    m.Tags.Remove(tag.Label);

                    if (m.Tags.Contains(newTag) == false)
                    {
                        AddTag(m, newTag);
                    }
                }
            }
        }
        
        private ContextMenu CreateModListContextMenu(ModEntry m, ModTag tag)
        {
            var menu = new ContextMenu();
            if (m?.ID == null || tag == null)
                return menu;
            
            // change color
            var changeColorItem = new MenuItem("Change color");

            var editColor = new MenuItem("Edit");

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

            changeColorItem.MenuItems.Add(editColor);

            var makePastelItem = new MenuItem("Make pastel");

            makePastelItem.Click += (sender, e) => tag.Color = tag.Color.GetPastelShade();

            changeColorItem.MenuItems.Add(makePastelItem);

            var changeShadeItem = new MenuItem("Random shade");

            changeShadeItem.Click += (sender, e) => tag.Color = tag.Color.GetRandomShade(0.33, 1.0);

            changeColorItem.MenuItems.Add(changeShadeItem);

            var randomColorItem = new MenuItem("Random color");

            randomColorItem.Click += (sender, e) => tag.Color = ModTag.RandomColor();

            changeColorItem.MenuItems.Add(randomColorItem);
            menu.MenuItems.Add(changeColorItem);

            menu.MenuItems.Add("-");

            // renaming tags
            var renameTagItem = new MenuItem($"Rename '{tag.Label}'");

            renameTagItem.Click += (sender, e) => RenameTagPrompt(m, tag, false);

            menu.MenuItems.Add(renameTagItem);

            var renameAllTagItem = new MenuItem($"Rename all '{tag.Label}'");

            renameAllTagItem.Click += (sender, e) => RenameTagPrompt(m, tag, true);
            menu.MenuItems.Add(renameAllTagItem);

            menu.MenuItems.Add("-");

            // removing tags
            var removeTagItem = new MenuItem($"Remove '{tag.Label}'");

            removeTagItem.Click += (sender, args) => m.Tags.Remove(tag.Label);
            menu.MenuItems.Add(removeTagItem);
            
            var removeAllTagItem = new MenuItem($"Remove all '{tag.Label}'");

            removeAllTagItem.Click += (sender, args) =>
            {
                if (MessageBox.Show($@"Are you sure you want to remove all instances of tag '{tag.Label}'?",
                        @"Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                foreach (var mod in Mods.All)
                {
                    mod.Tags.Remove(tag.Label);
                }
            };
            menu.MenuItems.Add(removeAllTagItem);

            return menu;
        }
        
        private ContextMenu CreateModListContextMenu(ModEntry m, OLVListItem currentItem)
        {
            var menu = new ContextMenu();
            if (m?.ID == null)
                return menu;

            var item = new MenuItem("Rename");
            item.Click += (a, b) => { modlist_ListObjectListView.EditModel(m); };
            menu.MenuItems.Add(item);

            // Add tag
            if (currentItem != null)
            {
                var selectedCount = modlist_ListObjectListView.SelectedItems.Count;
                var addTagItem = new MenuItem("Add tag");

                if (selectedCount > 1)
                {
                    addTagItem.Click += (sender, args) =>
                    {
                        var newTag = Interaction.InputBox($"Add a tag to {selectedCount} mods?", "Add tag");

                        if (newTag == "")
                            return;

                        var tags = newTag.Split(';');

                        foreach (var selectedItem in modlist_ListObjectListView.SelectedItems)
                        {
                            var listItem = selectedItem as OLVListItem;

                            if (listItem == null)
                                continue;

                            tags.All(t => AddTag(listItem.RowObject as ModEntry, t.Trim()));
                        }
                    };
                }
                else
                {
                    addTagItem.Click += (sender, args) => modlist_ListObjectListView.StartCellEdit(currentItem, olvcTags.Index);
                }

                menu.MenuItems.Add(addTagItem);
            }
            // Move to ...
            var moveToCategory = new MenuItem("Move to Category...");
            // ... new category
            moveToCategory.MenuItems.Add("New category", delegate
            {
                var category = Interaction.InputBox("Please enter the name of the new category", "Create category", "New category");
                if (category == "")
                    return;

                MoveMods(category);
            });

            moveToCategory.MenuItems.Add("-");

            // ... existing category
            foreach (var category in Settings.Mods.Categories.OrderBy(c => c))
            {
                if (category == Mods.GetCategory(m))
                    continue;

                moveToCategory.MenuItems.Add(category, delegate { MoveMods(category); });
            }
            menu.MenuItems.Add(moveToCategory);


            var toggleVisibility = new MenuItem {Text = m.isHidden ? "Unhide" : "Hide"};
            toggleVisibility.Click += delegate
            {
                // save as new list so we can remove mods if they are being hidden
                foreach (var mod in ModList.SelectedObjects.ToList())
                {
                    mod.isHidden = !m.isHidden;

                    if (!Settings.ShowHiddenElements && mod.isHidden)
                        modlist_ListObjectListView.RemoveObject(mod);
                }

                RefreshModList();
            };

            menu.MenuItems.Add(toggleVisibility);

            menu.MenuItems.Add("-");

            menu.MenuItems.Add(new MenuItem("Delete / Unsubscribe", delegate { DeleteMods(); }));


            menu.MenuItems.Add("-");

            menu.MenuItems.Add(new MenuItem("Show in Explorer", delegate { m.ShowInExplorer(); }));

            if (m.WorkshopID != -1)
            {
                menu.MenuItems.Add(new MenuItem("Show on Steam", delegate { m.ShowOnSteam(); }));
                menu.MenuItems.Add(new MenuItem("Show in Browser", delegate { m.ShowInBrowser(); }));
            }

            menu.MenuItems.Add(new MenuItem("Update", delegate { Settings.Mods.UpdateMod(m, Settings); }));


            return menu;
        }

        #region Events

        private ModTag HitTest(IEnumerable<string> tags, Graphics g, CellEventArgs e)
        {
            if (tags == null || e.SubItem == null)
                return null;

            var bounds = e.SubItem.Bounds;
            var offset = new Point(bounds.X + TagRenderInfo.rX,
                                   bounds.Y + TagRenderInfo.rY);
            var tagList = AvailableTags.Select(kvp => kvp.Value)
                                       .Where(t => tags.Contains(t.Label));

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

        private void ModListItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var modChecked = ModList.GetModelObject(e.Item.Index);

            List<ModEntry> updatedMods = new List<ModEntry>();
            
            // If there is a duplicate id conflict check all
            // at the same time and mark them as updated
            if (modChecked.State.HasFlag(ModState.DuplicateID))
            {
                foreach (var mod in Mods.All.Where(@mod => mod.ID == modChecked.ID))
                {
                    mod.isActive = modChecked.isActive;
                    updatedMods.Add(mod);
                    UpdateMod(mod);
                }
            }
            // Otherwise just mark the one as updated
            else
            {
                updatedMods.Add(modChecked);
            }

            UpdateConflictsForMods(updatedMods);
        }

        private void ModListSelectionChanged(object sender, EventArgs e)
        {
            if (modlist_ListObjectListView.SelectedObjects.Count > 1)
            {
                CurrentMod = null;
                return;
            }

            var selected = modlist_ListObjectListView.SelectedObject as ModEntry;

            if (CurrentMod == selected) return;

            CurrentMod = selected;


            UpdateModInfo(CurrentMod);
            CheckAndUpdateChangeLog(modinfo_tabcontrol.SelectedTab, CurrentMod);

            if (CurrentMod != null)
            {
                CurrentMod.State &= ~ModState.New;
                modlist_ListObjectListView.EnsureModelVisible(CurrentMod);
            }
        }

        private void ModListEditFinished(object sender, CellEditEventArgs e)
        {
            var mod = e.RowObject as ModEntry;
            if (mod == null) return;

            switch (e.Column.AspectName)
            {
                case "Name":
                    mod.ManualName = !string.IsNullOrEmpty(e.NewValue as string);

                    if (!mod.ManualName)
                        // Restore name
                        Mods.UpdateMod(mod, Settings);

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
                    MoveMods((string)e.NewValue);
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