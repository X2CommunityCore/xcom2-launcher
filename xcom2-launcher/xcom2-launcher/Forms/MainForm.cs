using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using XCOM2Launcher.Mod;
using XCOM2Launcher.XCOM;
using JR.Utils.GUI.Forms;
using XCOM2Launcher.Classes.Mod;
using XCOM2Launcher.UserElements;

namespace XCOM2Launcher.Forms
{
    public partial class MainForm
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(MainForm));
        private const string ExclamationIconKey = "Exclamation";
        
        private Task ModUpdateTask;
        private CancellationTokenSource ModUpdateCancelSource;
        private bool IsModUpdateTaskRunning => (ModUpdateTask != null && !ModUpdateTask.IsCompleted);

        public Settings Settings { get; set; }

        public MainForm(Settings settings)
        {
            InitializeComponent();

            appRestartPendingLabel.Visible = false;
            progress_toolstrip_progressbar.Visible = false;
            aboutToolStripMenuItem.DropDownDirection = ToolStripDropDownDirection.BelowLeft;

            // Settings
            Settings = settings;

            // Restore states 
            InitMainGui(settings);

            // Init interface
            InitModListView();
            InitDependencyListViews();
            
            RegisterEvents();

            // Other intialization
            InitializeTabImages();

            // Init the argument checkboxes
            InitQuickArgumentsMenu(settings);

/*
            // Check for running downloads
#if DEBUG
            if (Settings.GetWorkshopPath() != null)
            {
                CheckSteamForNewMods();

                var t2 = new Timer();
                t2.Tick += (sender, e) => { CheckSteamForNewMods(); };
                t2.Interval = 30000;
                t2.Start();
            }
#endif
*/
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text += " " + Program.GetCurrentVersionString(true);
            
            if (Settings.UpdateModsOnStartup)
            {
                // Update mod information
                var mods = Settings.Mods.All.ToList();

                if (Settings.OnlyUpdateEnabledOrNewModsOnStartup)
                {
                    mods = mods.Where(mod => mod.isActive || mod.State.HasFlag(ModState.New)).ToList();
                }

                UpdateMods(mods, () =>
                {
                    InitializeInterface();
                    return Task.CompletedTask;
                });
            }
            else
            {
                InitializeInterface();
            }
        }

        private void InitializeTabImages()
        {
            tabImageList.Images.Add(ExclamationIconKey, error_provider.Icon);
        }

/*
        // This was only called for DEBUG builds, but currently isn't required (maybe part of an abandoned feature?)
        private void CheckSteamForNewMods()
        {
            SetStatus("Checking for new mods...");

            ulong[] subscribedIDs;
            try
            {
                subscribedIDs = Workshop.GetSubscribedItems();
            }
            catch (InvalidOperationException ex)
            {
                // Steamworks not initialized?
                // Game taking over?
                Log.Error("Error checking for new mods", ex);
                SetStatus("Error checking for new mods.");
                return;
            }

            var change = false;
            foreach (var id in subscribedIDs)
            {
                var status = Workshop.GetDownloadStatus(id);

                if (status.HasFlag(EItemState.k_EItemStateInstalled))
                    // already installed
                    continue;

                if (Downloads.Any(d => d.WorkshopID == (long) id))
                    // already observing
                    continue;

                // Get info
                var detailsRequest = new ItemDetailsRequest(id).Send().WaitForResult();
                var details = detailsRequest.Result[0];
                var link = detailsRequest.GetPreviewURL();

                var downloadMod = new ModEntry
                {
                    Name = details.m_rgchTitle,
                    DateCreated = DateTimeOffset.FromUnixTimeSeconds(details.m_rtimeCreated).DateTime,
                    DateUpdated = DateTimeOffset.FromUnixTimeSeconds(details.m_rtimeUpdated).DateTime,
                    //Path = Path.Combine(Settings.GetWorkshopPath(), "" + id),
                    Image = link,
                    WorkshopID = (int) id
                };

                downloadMod.SetSource(ModSource.SteamWorkshop);
                downloadMod.AddState(ModState.New | ModState.NotInstalled);

                // Start download
                Workshop.DownloadItem(id);
                //
                Downloads.Add(downloadMod);
                change = true;
            }

            if (change)
                RefreshModList();

            SetStatusIdle();
        }
*/
        #region GUI

        /// <summary>
        /// Initializes GUI elements that depend on current settings and selected game type.
        /// </summary>
        /// <param name="settings"></param>
        private void InitMainGui(Settings settings)
        {
            showHiddenModsToolStripMenuItem.Checked = settings.ShowHiddenElements;
            cShowStateFilter.Checked = settings.ShowStateFilter;
            cEnableGrouping.Checked = settings.ShowModListGroups;
            cShowPrimaryDuplicates.Checked = settings.ShowPrimaryDuplicateAsDependency;
            cShowPrimaryDuplicates.Visible = settings.EnableDuplicateModIdWorkaround;
            modlist_ListObjectListView.UseTranslucentSelection = settings.UseTranslucentModListSelection;
            olvRequiredMods.UseTranslucentSelection = settings.UseTranslucentModListSelection;
            olvDependentMods.UseTranslucentSelection = settings.UseTranslucentModListSelection;

            // Set visibility of some controls depending on game type
            var wotcAvailable = Directory.Exists(settings.GamePath + @"\XCom2-WarOfTheChosen");
            runXCOM2ToolStripMenuItem.Visible = Program.XEnv.Game == GameId.X2 && (!settings.HideXcom2Button || !wotcAvailable);
            runWarOfTheChosenToolStripMenuItem.Visible = wotcAvailable && Program.XEnv.Game == GameId.X2;
            runChallengeModeToolStripMenuItem.Visible = wotcAvailable && Program.XEnv.Game == GameId.X2 && !settings.HideChallengeModeButton;
            importFromWotCToolStripMenuItem.Visible = wotcAvailable && Program.XEnv.Game == GameId.X2;
            importFromXCOM2ToolStripMenuItem.Visible = Program.XEnv.Game == GameId.X2;
            runChimeraSquadToolStripMenuItem.Visible = Program.XEnv.Game == GameId.ChimeraSquad;
            importFromChimeraSquadToolStripMenuItem.Visible = Program.XEnv.Game == GameId.ChimeraSquad;

            if (Program.XEnv.Game != GameId.X2)
            {
                modlist_ListObjectListView.AllColumns.Remove(olvForWOTC);
                modlist_ListObjectListView.RebuildColumns();
                olvDependentMods.AllColumns.Remove(olvColDepModsWotc);
                olvDependentMods.RebuildColumns();
                olvRequiredMods.AllColumns.Remove(olvColReqModsWotc);
                olvRequiredMods.RebuildColumns();
            }

            // If game path is not configured, hide several function/options.
            if (string.IsNullOrEmpty(settings.GamePath))
            {
                runWarOfTheChosenToolStripMenuItem.Enabled = false;
                runChallengeModeToolStripMenuItem.Enabled = false;
                importFromWotCToolStripMenuItem.Enabled = false;
                importFromXCOM2ToolStripMenuItem.Enabled = false;
                importFromChimeraSquadToolStripMenuItem.Enabled = false;
            }
        }

        private void SetStatus(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetStatus(text)));
                return;
            }

            status_toolstrip_label.Text = text;
        }

        private void SetStatusIdle()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(SetStatusIdle));
                return;
            }

            progress_toolstrip_progressbar.Visible = false;
            status_toolstrip_label.Text = "Ready.";
            main_statusstrip.Update();
        }

        #endregion

		#region Export

        private void UpdateExport()
        {
            var str = new StringBuilder();

            if (!Mods.Active.Any())
            {
                export_richtextbox.Text = "No active mods.";
                return;
            }
			
			var showCategories = export_group_checkbox.Checked;
			var showLink = export_workshop_link_checkbox.Checked;
			var showAllMods = export_all_mods_checkbox.Checked;

			var nameLength = showAllMods ? Mods.All.Max(m => m.Name.Length) : Mods.Active.Max(m => m.Name.Length);
			var idLength = showAllMods ? Mods.All.Max(m => m.ID.Length) : Mods.Active.Max(m => m.ID.Length);
            var workshopIDLength = showAllMods ? Mods.All.Max(m => m.WorkshopID.ToString().Length) : Mods.Active.Max(m => m.WorkshopID.ToString().Length);


            foreach (var entry in Mods.Entries.Where(e => e.Value.Entries.Any(m => m.isActive)))
            {
                List<ModEntry> mods;
	   //         if (showAllMods)
		  //          mods = entry.Value.Entries.ToList();
				//else
					mods = entry.Value.Entries.Where(m => m.isActive).ToList();

                if (showCategories)
                    str.AppendLine($"{entry.Key} ({mods.Count()}):");

                foreach (var mod in mods)
                {
                    if (showCategories)
                        str.Append("\t");

                    str.Append(string.Format("{0,-" + nameLength + "} ", mod.Name));
                    str.Append("\t");
                    str.Append(string.Format("{0,-" + idLength + "} ", mod.ID));
                    str.Append("\t");

					// add workshop ID or link
                    if (mod.WorkshopID == -1)
                        str.Append("Unknown");

                    else if (showLink)
                        str.Append(string.Format("{0,-" + workshopIDLength + "} ", mod.GetWorkshopLink()));

                    else
                        str.Append(string.Format("{0,-" + workshopIDLength + "} ", mod.WorkshopID));
	                str.Append("\t");

                    str.Append(string.Join(";", mod.Tags));

                    str.AppendLine();
                }

                if (export_group_checkbox.Checked)
                    str.AppendLine();
            }

            export_richtextbox.Text = str.ToString();
        }

		#endregion


		#region Basic

        private void Reset()
        {
            if (IsModUpdateTaskRunning)
            {
                ShowModUpdateRunningMessageBox();
                return;
            }

            modlist_ListObjectListView.Clear();
            Settings = Program.InitializeSettings();
            InitModListView();
        }

        private void Save(bool WotC = false)
        {
            try
            {
                if (Program.XEnv is Xcom2Env x2Env)
                {
                    x2Env.UseWotC = WotC;
                }

                Program.XEnv.SaveChanges(Settings, ChallengeMode);
                Settings.SaveFile("settings.json");
            }
            catch (Exception ex)
            {
                // lets report any issues that occur while writing the settings.json or the ini files
                Log.Warn("Failed so save/apply settings", ex);
                MessageBox.Show("An error occurred while saving changes." +
                                Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowModUpdateRunningMessageBox()
        {
            MessageBox.Show("Mod update in progress, please wait for it to finish.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RunChimeraSquad()
        {
            if (IsModUpdateTaskRunning)
            {
                ShowModUpdateRunningMessageBox();
                return;
            }

            Save();

            Program.XEnv.RunGame(Settings.GamePath, Settings.GetArgumentString());

            if (Settings.CloseAfterLaunch)
                Close();
        }

        private void RunVanilla()
        {
            if (IsModUpdateTaskRunning)
            {
                ShowModUpdateRunningMessageBox();
                return;
            }

            // Check for WOTC only mods
            if (Settings.Mods.Active.Count(e => e.BuiltForWOTC) > 0)
            {
                if (FlexibleMessageBox.Show(this,
                                            "Are you sure you want to proceed? Please be warned that this is very likely to crash your game. Offending mods:\r\n" +
                                            String.Join("\r\n", Settings.Mods.Active.Where(e => e.BuiltForWOTC).Select(e => e.Name)),
                                            "You are trying to launch vanilla game with mods built for WOTC", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Log.Warn("User chose to run Vanilla XCOM with WotC mods");
                }
                else
                {
                    return;
                }
            }
            
            Settings.Instance.LastLaunchedWotC = false;
            ChallengeMode = false;
            Save();

            if (Program.XEnv is Xcom2Env x2Env)
            {
                x2Env.UseWotC = false;
                Program.XEnv.RunGame(Settings.GamePath, Settings.GetArgumentString());
            }


            if (Settings.CloseAfterLaunch)
                Close();
        }
        
        private void RunWotC()
        {
            if (IsModUpdateTaskRunning)
            {
                ShowModUpdateRunningMessageBox();
                return;
            }

            Settings.Instance.LastLaunchedWotC = true;
            ChallengeMode = false;
            Save(true);

            if (Program.XEnv is Xcom2Env x2Env)
            {
                x2Env.UseWotC = true;
                Program.XEnv.RunGame(Settings.GamePath, Settings.GetArgumentString());
            }

            if (Settings.CloseAfterLaunch)
                Close();
        }

        private bool ChallengeMode = false;
        
        private void RunChallengeMode()
        {
            if (IsModUpdateTaskRunning)
            {
                ShowModUpdateRunningMessageBox();
                return;
            }

            Settings.Instance.LastLaunchedWotC = true;
            ChallengeMode = true;
            Save(true);

            if (Program.XEnv is Xcom2Env x2Env)
            {
                x2Env.UseWotC = true;
                Program.XEnv.RunGame(Settings.GamePath, Settings.GetArgumentString().ToLower().Replace("-allowconsole", ""));
            }

            if (Settings.CloseAfterLaunch)
                Close();
        }

        #endregion


        #region Interface updates

        private void InitializeInterface()
        {
            error_provider.Clear();
            
            UpdateModInfo(modlist_ListObjectListView.SelectedObject as ModEntry);
            UpdateLabels();
            UpdateStateFilterLabels();
        }

        private void UpdateLabels()
        {
            //
            var hasConflicts = NumConflicts > 0;
            modlist_tab.Text = $"Mods ({Mods.Active.Count()} / {Mods.All.Count()})";
            conflicts_tab.Text = "Overrides" + (hasConflicts ? $" ({NumConflicts} Conflicts)" : "");
            conflicts_tab.ImageKey = hasConflicts ? ExclamationIconKey : null;
        }

        /// <summary>
        /// Updates labels from filter controls with number of currently matching mods.
        /// </summary>
        private void UpdateStateFilterLabels()
        {
            var allMods = Mods.All.ToList();
            cFilterConflicted.Text = $"Conflicts ({allMods.Count(m => m.State.HasFlag(ModState.ModConflict))})";
            cFilterDuplicate.Text = $"Duplicates ({allMods.Count(m => m.State.HasFlag(ModState.DuplicateID))})";
            cFilterNew.Text = $"New ({allMods.Count(m => m.State.HasFlag(ModState.New))})";
            cFilterNotInstalled.Text = $"Not installed ({allMods.Count(m => m.State.HasFlag(ModState.NotInstalled))})";
            cFilterNotLoaded.Text = $"Not loaded ({allMods.Count(m => m.State.HasFlag(ModState.NotLoaded))})";
            cFilterMissingDependency.Text = $"Missing dep ({allMods.Count(m => m.isActive && m.State.HasFlag(ModState.MissingDependencies))})";
            cFilterIgnoredDependencies.Text = $"Ignored ({allMods.Count(m => m.IgnoredDependencies != null && m.IgnoredDependencies.Count > 0)})";
            cFilterHidden.Text = $"Hidden ({allMods.Count(m => m.isHidden)})";
        }

        public int NumConflicts;

        /// <summary>
        /// Update incompatibility warnings and overrides grid.
        /// </summary>
        private void UpdateConflictInfo()
        {
            // Incremented later in GetDuplicatesString() and GetOverridesString()
            NumConflicts = 0;

            // Clear and refill conflicts_datagrid
            conflicts_datagrid.Rows.Clear();

            foreach (var m in Mods.Active)
            {
                foreach (var classOverride in m.GetOverrides())
                {
                    var oldClass = classOverride.OldClass;

                    if (classOverride.OverrideType == ModClassOverrideType.UIScreenListener)
                        oldClass += " (UIScreenListener)";

                    conflicts_datagrid.Rows.Add(m.Name, oldClass, classOverride.NewClass);
                }
            }
            
            var changedMods = Mods.UpdateModsConflictState();
            modlist_ListObjectListView.RefreshObjects(changedMods);

            // Conflict log
            conflicts_textbox.Text = GetDuplicatesString() + GetOverridesString();
            
            UpdateLabels();
        }

        private void UpdateConflictsForMods(List<ModEntry> mods)
        {
            // Incremented later in GetDuplicatesString() and GetOverridesString()
            NumConflicts = 0;

            // Update conflicts_datagrid
            foreach (var m in mods)
            {
                if (m.isActive)
                {
                    foreach (var classOverride in m.GetOverrides())
                    {
                        var oldClass = classOverride.OldClass;

                        if (classOverride.OverrideType == ModClassOverrideType.UIScreenListener)
                            oldClass += " (UIScreenListener)";

                        conflicts_datagrid.Rows.Add(m.Name, oldClass, classOverride.NewClass);
                    }
                }
                else
                {
                    foreach (var classOverride in m.GetOverrides())
                    {
                        foreach (var row in conflicts_datagrid.Rows.Cast<DataGridViewRow>())
                        {
                            var oldClass = classOverride.OldClass;

                            if (classOverride.OverrideType == ModClassOverrideType.UIScreenListener)
                                oldClass += " (UIScreenListener)";

                            if ((string)row.Cells[0].Value == m.Name && (string)row.Cells[1].Value == oldClass && (string)row.Cells[2].Value == classOverride.NewClass)
                            {
                                conflicts_datagrid.Rows.Remove(row);
                                break;
                            }
                        }
                    }
                }
            }

            var changedMods = Mods.UpdateModsConflictState();
            modlist_ListObjectListView.RefreshObjects(changedMods);
            
            // Conflict log
            conflicts_textbox.Text = GetDuplicatesString() + GetOverridesString();
        }

        private string GetDuplicatesString()
        {
            var str = new StringBuilder();

            var duplicates = Mods.GetDuplicates().Where(delegate(IGrouping<string, ModEntry> entries)
            {
                // Only show duplicate-groups that have not been resolved (ModState.DuplicateDisabled / ModState.DuplicatePrimary)
                return entries?.All(m => m.State.HasFlag(ModState.DuplicateID)) == true;
            }).ToList();


            if (duplicates.Any())
            {
                str.AppendLine("Mods with identical package ids found!");
                if (Settings.EnableDuplicateModIdWorkaround)
                {
                    str.AppendLine("You can set a preferred duplicate from the mod list context menu to resolve this.");
                }
                else
                {
                    str.AppendLine("These can only be (de-)activated together.");
                }

                str.AppendLine();

                foreach (var grouping in duplicates)
                {
                    NumConflicts++;

                    str.AppendLine(grouping.Key);

                    foreach (var m in grouping)
                        str.AppendLine($"\t{m.Name}");


                    str.AppendLine();
                }

                str.AppendLine();
            }
            return str.ToString();
        }

        private string GetOverridesString()
        {
            var conflicts = Mods.GetActiveConflicts().ToList();
            if (!conflicts.Any())
                return "";

            var showUIScreenListenerMessage = false;

            var str = new StringBuilder();

            str.AppendLine("Mods with conflicting overrides found!");
            str.AppendLine("These mods will probably not work as intended, or cause instabilities when run together.");
            str.AppendLine();

            foreach (var conflict in conflicts)
            {
                str.AppendLine($"Conflict found for '{conflict.ClassName}':");
                var hasMultipleUIScreenListeners = conflict.Overrides.Count(o => o.OverrideType == ModClassOverrideType.UIScreenListener) > 1;

                foreach (var classOverride in conflict.Overrides.OrderBy(o => o.OverrideType).ThenBy(o => o.Mod.Name))
                {
                    if (hasMultipleUIScreenListeners && classOverride.OverrideType == ModClassOverrideType.UIScreenListener)
                    {
                        showUIScreenListenerMessage = true;
                        str.AppendLine($"\t* {classOverride.Mod.Name}");
                    }
                    else
                    {
                        str.AppendLine($"\t{classOverride.Mod.Name}");
                    }
                }

                str.AppendLine();

                NumConflicts++;
            }

            error_provider.SetError(conflicts_log_label, "Found " + NumConflicts + " conflicts");

            if (showUIScreenListenerMessage)
            {
                str.AppendLine("* (These mods use UIScreenListeners, meaning they do not conflict with each other)");
                str.AppendLine();
            }

            return str.ToString();
        }

        /// <summary>
        /// Updates the mod description user interface with the description from the provided Mod.
        /// </summary>
        /// <param name="m">The desired mod. Use null to clear/reset description.</param>
        private void UpdateModDescription(ModEntry m)
        {
            modinfo_info_DescriptionRichTextBox.Clear();

            if (m != null)
            {
                modinfo_info_DescriptionRichTextBox.Font = DefaultFont;
                modinfo_info_DescriptionRichTextBox.Rtf = m.GetDescription(true);
            }
        }

        private void UpdateDependencyInformation(ModEntry m)
        {
            if (m == null)
                return;

            // update dependency information
            olvRequiredMods.ClearObjects();
            olvRequiredMods.AddObjects(Mods.GetRequiredMods(m, cShowPrimaryDuplicates.Checked));
            olvDependentMods.ClearObjects();
            olvDependentMods.AddObjects(Mods.GetDependentMods(m, false));
        }

        /// <summary>
        /// Update mod information panel with data from specified mod.
        /// </summary>
        /// <param name="m"></param>
        private void UpdateModInfo(ModEntry m)
        {
            if (m == null)
            {
                modinfo_info_TitleTextBox.Text = "No mod or multiple mods selected";
                modinfo_info_AuthorTextBox.Clear();
                modinfo_info_DateCreatedTextBox.Clear();
                modinfo_info_InstalledTextBox.Clear();
                modinfo_readme_RichTextBox.Clear();
                modinfo_changelog_richtextbox.Clear();
                modInfoNotesText.Clear();
                modInfoNotesText.ReadOnly = true;
                UpdateModDescription(null);
                modinfo_image_picturebox.ImageLocation = null;
                modinfo_inspect_propertygrid.SelectedObject = null;
                modinfo_config_FileSelectCueComboBox.Items.Clear();
                modinfo_config_LoadButton.Enabled = false;
                modinfo_config_RemoveButton.Enabled = false;
                modinfo_ConfigFCTB.Clear();
                modinfo_ConfigFCTB.ReadOnly = true;
                olvRequiredMods.ClearObjects();
                olvDependentMods.ClearObjects();
                return;
            }

            // show panel
            horizontal_splitcontainer.Panel2Collapsed = false;

            // Update data
            modinfo_info_TitleTextBox.Text = m.Name;
            modinfo_info_AuthorTextBox.Text = m.Author;
            modinfo_info_DateCreatedTextBox.Text = m.DateCreated?.ToString() ?? "";
            modinfo_info_InstalledTextBox.Text = m.DateAdded?.ToString() ?? "";
            modInfoNotesText.Text = m.Note;
            modInfoNotesText.ReadOnly = false;
            UpdateModDescription(m);
            UpdateModChangeLog(m);
            modinfo_readme_RichTextBox.Text = m.GetReadMe();
            modinfo_image_picturebox.ImageLocation = m.Image;
            
            // Init handler for property changes
            var sel_obj = m.GetProperty();
            
            sel_obj.PropertyChanged += async (sender, e) =>
            {
                // Update steam info when clearing the Name
                var prop = (ModProperty)sender;
                if (e.PropertyName == "Name" && string.IsNullOrEmpty(prop.Name))
                {
                    await Mods.UpdateModAsync(prop.ModEntry, Settings);
                }
                
                RefreshModList(); 
                modinfo_inspect_propertygrid.Refresh();
            };
            
            modinfo_inspect_propertygrid.SelectedObject = sel_obj;

            #region Config

            // config files
            string[] configFiles = m.GetConfigFiles();

            // clear
            modinfo_config_FileSelectCueComboBox.Items.Clear();
            modinfo_ConfigFCTB.Text = "";
            modinfo_config_LoadButton.Enabled = false;
            modinfo_config_RemoveButton.Enabled = false;

            if (configFiles.Length > 0)
            {
                foreach (var configFile in configFiles)
                {
                    if (configFile != null) modinfo_config_FileSelectCueComboBox.Items.Add(CurrentMod.GetPathRelative(configFile));
                }
            }

            #endregion
            
            UpdateDependencyInformation(m);
        }

        /// <summary>
        /// Updates the quick launch menu items check-states, depending on if the the respective argument is enabled in the settings.
        /// </summary>
        private void InitQuickArgumentsMenu(Settings settings)
        {
            LauchOptionsPanel.Visible = settings.ShowQuickLaunchArguments && settings.QuickToggleArguments.Any();
            
            quickLaunchToolstripButton.DropDownItems.Clear();
            foreach (var arg in settings.QuickToggleArguments)
            {
                var item = new ToolStripMenuItem(arg) {CheckOnClick = true};
                item.Click += QuickArgumentItemClick;
                quickLaunchToolstripButton.DropDownItems.Add(item);
            }

            foreach (ToolStripMenuItem item in quickLaunchToolstripButton.DropDownItems) {
                item.Checked = Settings.ArgumentList.Any(arg => arg.Equals(item.Text, StringComparison.OrdinalIgnoreCase));
            }
        }

        #endregion

        #region Dependency ObjectListViews

        private void InitDependencyListViews()
        {
            olvColReqModsState.AspectGetter = StateAspectGetter;
            olvColDepModsState.AspectGetter = StateAspectGetter;

            olvRequiredMods.BooleanCheckStatePutter = BooleanCheckStatePutter;
            olvDependentMods.BooleanCheckStatePutter = BooleanCheckStatePutter;
            
            olvColReqModsIgnore.AspectGetter += rowObject =>
            {
                if (CurrentMod == null || !(rowObject is ModEntry mod))
                    return false;

                return CurrentMod.IgnoredDependencies.Contains(mod.WorkshopID);
            };

            olvColReqModsIgnore.AspectPutter += (rowObject, value) =>
            {
                if (CurrentMod == null || !(rowObject is ModEntry mod) || !(value is bool checkState))
                    return;

                if (IgnoreDependencyOnMod(CurrentMod, mod.WorkshopID, checkState))
                {
                    Mods.UpdatedModDependencyState(CurrentMod);
                    RefreshDependencyState(new[] { CurrentMod });
                }
            };

            olvRequiredMods.SubItemChecking += (sender, args) =>
            {
                if (CurrentMod == null && !(args.RowObject is ModEntry))
                {
                    args.NewValue = args.CurrentValue;
                }
            };
        }

        private bool BooleanCheckStatePutter(object rowobject, bool newValue)
        {
            var mod = rowobject as ModEntry;
            newValue = ProcessNewModState(mod, newValue);

            // change check state for the mod in main list accordingly
            if (newValue)
            {
                modlist_ListObjectListView.CheckObject(mod);
            }
            else
            {
                modlist_ListObjectListView.UncheckObject(mod);
            }

            return newValue;
        }

        private void olvDependencyMods_ItemActivate(object sender, EventArgs e)
        {
            if (sender is ObjectListView olv)
            {
                // view mod in main mod list on double-click
                if (ModList.Objects.Contains(olv.SelectedObject))
                {
                    modlist_ListObjectListView.SelectedObject = olv.SelectedObject;
                    modlist_ListObjectListView.EnsureModelVisible(olv.SelectedObject);
                }
            }
        }

        private void olvRequiredMods_FormatRow(object sender, FormatRowEventArgs e)
        {
            var mod = e.Model as ModEntry;
            Contract.Assume(mod != null);

            SetModListItemColor(e.Item, mod);
        }

        /// <summary>
        /// Adds (ignore=true) or removes (ignore=false) <paramref name="workshopId"/> to/from
        /// <paramref name="mod"/>'s IgnoredDependencies. Caller is responsible for the state
        /// recompute and refresh, so multiple calls can batch into a single recompute.
        /// </summary>
        /// <returns>true iff the list was modified.</returns>
        private bool IgnoreDependencyOnMod(ModEntry mod, long workshopId, bool ignore)
        {
            if (mod == null || workshopId <= 0) return false;

            if (ignore)
            {
                if (mod.IgnoredDependencies.Contains(workshopId)) return false;
                mod.IgnoredDependencies.Add(workshopId);
                return true;
            }

            return mod.IgnoredDependencies.Remove(workshopId);
        }

        private void BulkIgnoreOnCurrentMod(ModEntry mod, IList<ModEntry> selectedDeps, bool ignore)
        {
            if (mod == null || selectedDeps == null || selectedDeps.Count == 0) return;

            var changed = new List<ModEntry>();
            foreach (var dep in selectedDeps)
            {
                if (dep == null || dep.WorkshopID <= 0) continue;
                if (IgnoreDependencyOnMod(mod, dep.WorkshopID, ignore))
                    changed.Add(dep);
            }

            if (changed.Count == 0) return;

            Mods.UpdatedModDependencyState(mod);
            RefreshDependencyState(new[] { mod });
            olvRequiredMods.RefreshObjects(changed);
        }

        private void IgnoreDependencyEverywhereWithConfirm(ModEntry dep, bool ignore)
        {
            if (dep == null || dep.WorkshopID <= 0) return;

            if (ignore)
            {
                var dependents = Mods.GetDependentMods(dep, false);
                var count = dependents.Count;
                if (count == 0) return;

                var prompt = count == 1 && dependents[0] != null
                    ? $"Ignore '{dep.Name}' as a dependency on '{dependents[0].Name}'?"
                    : $"Ignore '{dep.Name}' on the {count} mods that require it?";

                if (FlexibleMessageBox.Show(this, prompt, "Confirm bulk ignore", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            var affected = ignore
                ? Mods.IgnoreDependencyEverywhere(dep.WorkshopID)
                : Mods.UnignoreDependencyEverywhere(dep.WorkshopID);

            if (affected.Count == 0) return;

            RefreshDependencyState(affected);
            olvRequiredMods.RefreshObject(dep);
        }

        /// <summary>
        /// Opens a modal picker letting the user toggle which installed mod(s) substitute
        /// (alias) for the given Workshop dep. Filter-as-you-type, sortable columns, checkbox
        /// column for current alias state. On close, recomputes dep state for every mod that
        /// listed the dep's WorkshopID and refreshes the UI once.
        /// </summary>
        private void OpenAliasPicker(ModEntry dep)
        {
            if (dep == null || dep.WorkshopID <= 0) return;

            var depId = dep.WorkshopID;
            var candidates = Mods.All
                .Where(m => m != null && m.WorkshopID != depId)
                .OrderBy(m => m.Source == ModSource.SteamWorkshop ? 1 : 0)
                .ThenBy(m => m.Name ?? m.ID ?? string.Empty, StringComparer.OrdinalIgnoreCase)
                .ToList();

            using (var picker = new System.Windows.Forms.Form())
            {
                picker.Text = $"Substitute for '{dep.Name}'  (Workshop ID {depId})";
                picker.Size = new System.Drawing.Size(640, 520);
                picker.StartPosition = FormStartPosition.CenterParent;
                picker.MinimumSize = new System.Drawing.Size(420, 320);
                picker.ShowInTaskbar = false;
                picker.MinimizeBox = false;
                picker.MaximizeBox = false;

                var instructions = new Label
                {
                    Text = "Pick a mod that should satisfy this Workshop dependency, then click Substitute. Click again on the same row (now showing 'Yes') to remove. A real Workshop install of this id always wins over a substitute.",
                    Dock = DockStyle.Top,
                    Padding = new Padding(8, 8, 8, 4),
                    Height = 56,
                    AutoEllipsis = true
                };

                var filterBox = new CueTextBox
                {
                    Dock = DockStyle.Top,
                    Margin = new Padding(8, 4, 8, 4),
                    CueText = "Filter mods (e.g. \"lwotc\", \"highlander\")...",
                    ShowCueTextWithFocus = true
                };

                var olv = new ObjectListView
                {
                    Dock = DockStyle.Fill,
                    FullRowSelect = true,
                    UseFiltering = true,
                    UseAlternatingBackColors = true,
                    AlternateRowBackColor = System.Drawing.Color.WhiteSmoke,
                    View = System.Windows.Forms.View.Details,
                    UseCompatibleStateImageBehavior = false,
                    HeaderStyle = ColumnHeaderStyle.Clickable,
                    OwnerDraw = false,
                    HasCollapsibleGroups = false,
                    ShowGroups = false
                };

                var nameCol = new OLVColumn("Mod", "Name") { Width = 280, Sortable = true };
                var sourceCol = new OLVColumn("Source", null) { Width = 100, Sortable = true };
                sourceCol.AspectGetter = m =>
                {
                    var entry = m as ModEntry;
                    if (entry == null) return string.Empty;
                    if (entry.Source == ModSource.SteamWorkshop) return "Workshop";
                    if (entry.Source == ModSource.Manual) return "Local / Manual";
                    return "Unknown";
                };
                var workshopIdCol = new OLVColumn("Workshop ID", null) { Width = 110, Sortable = true };
                workshopIdCol.AspectGetter = m =>
                {
                    var entry = m as ModEntry;
                    if (entry == null || entry.WorkshopID <= 0) return "(none)";
                    return entry.WorkshopID.ToString();
                };
                var aliasCol = new OLVColumn("Substitutes?", null) { Width = 100, Sortable = true };
                aliasCol.AspectGetter = m =>
                {
                    var entry = m as ModEntry;
                    return entry != null && entry.WorkshopIdAliases.Contains(depId) ? "Yes" : "";
                };

                olv.AllColumns.Add(nameCol);
                olv.AllColumns.Add(sourceCol);
                olv.AllColumns.Add(workshopIdCol);
                olv.AllColumns.Add(aliasCol);
                olv.Columns.Add(nameCol);
                olv.Columns.Add(sourceCol);
                olv.Columns.Add(workshopIdCol);
                olv.Columns.Add(aliasCol);
                olv.MultiSelect = false;
                olv.SetObjects(candidates);
                olv.Sort(nameCol, SortOrder.Ascending);

                filterBox.TextChanged += (s, a) =>
                {
                    olv.AdditionalFilter = string.IsNullOrEmpty(filterBox.Text)
                        ? null
                        : TextMatchFilter.Contains(olv, filterBox.Text);
                    olv.UpdateColumnFiltering();
                };

                var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 50 };
                var substituteButton = new Button
                {
                    Text = "Substitute",
                    Enabled = false,
                    Anchor = AnchorStyles.Right | AnchorStyles.Top,
                    Size = new System.Drawing.Size(140, 30)
                };
                var closeButton = new Button
                {
                    Text = "Close",
                    DialogResult = DialogResult.OK,
                    Anchor = AnchorStyles.Right | AnchorStyles.Top,
                    Size = new System.Drawing.Size(100, 30)
                };

                Action layoutButtons = () =>
                {
                    closeButton.Location = new System.Drawing.Point(bottomPanel.ClientSize.Width - 116, 10);
                    substituteButton.Location = new System.Drawing.Point(bottomPanel.ClientSize.Width - 264, 10);
                };
                layoutButtons();
                bottomPanel.Resize += (s, a) => layoutButtons();
                bottomPanel.Controls.Add(substituteButton);
                bottomPanel.Controls.Add(closeButton);

                Action updateButton = () =>
                {
                    var sel = olv.SelectedObject as ModEntry;
                    if (sel == null)
                    {
                        substituteButton.Enabled = false;
                        substituteButton.Text = "Substitute";
                    }
                    else
                    {
                        substituteButton.Enabled = true;
                        substituteButton.Text = sel.WorkshopIdAliases.Contains(depId)
                            ? $"Remove substitute"
                            : $"Substitute";
                    }
                };
                olv.SelectionChanged += (s, a) => updateButton();
                updateButton();

                substituteButton.Click += (s, a) =>
                {
                    var sel = olv.SelectedObject as ModEntry;
                    if (sel == null) return;

                    if (sel.WorkshopIdAliases.Contains(depId))
                    {
                        sel.WorkshopIdAliases.Remove(depId);
                        Log.Info($"Removed alias {depId} ('{dep.Name}') from '{sel.Name}'.");
                    }
                    else
                    {
                        sel.WorkshopIdAliases.Add(depId);
                        Log.Info($"Added alias {depId} ('{dep.Name}') on '{sel.Name}'.");
                    }
                    olv.RefreshObject(sel);
                    updateButton();
                };

                picker.AcceptButton = closeButton;
                picker.Controls.Add(olv);
                picker.Controls.Add(filterBox);
                picker.Controls.Add(instructions);
                picker.Controls.Add(bottomPanel);

                picker.ShowDialog(this);
            }

            // Single batch state recompute + UI refresh after the picker closes.
            var affectedDependents = Mods.All
                .Where(m => m.Dependencies.Contains(depId))
                .ToList();

            foreach (var m in affectedDependents)
                Mods.UpdatedModDependencyState(m);

            RefreshDependencyState(affectedDependents);
            olvRequiredMods.RefreshObject(dep);
        }

        /// <summary>
        /// Common refresh tail for any operation that mutates a mod's dependency-state-flags.
        /// Re-renders affected rows in the main list, recomputes the filter-label counters,
        /// and re-applies the active filter so a row stops appearing under e.g. "Missing
        /// dependencies" the moment its state actually changes.
        /// </summary>
        private void RefreshDependencyState(IEnumerable<ModEntry> affected)
        {
            if (affected != null)
            {
                var list = affected as List<ModEntry> ?? affected.ToList();
                if (list.Count > 0)
                    modlist_ListObjectListView.RefreshObjects(list);
            }
            UpdateStateFilterLabels();
            RefreshModelFilter();
        }

        private void RequiredModsCellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (CurrentMod == null) return;

            var rightClicked = e.Model as ModEntry;
            if (rightClicked == null || rightClicked.WorkshopID <= 0) return;

            var selected = olvRequiredMods.SelectedObjects.Cast<ModEntry>().Where(m => m != null).ToList();
            var menu = CreateRequiredModsContextMenu(CurrentMod, rightClicked, selected);
            if (menu.Items.Count == 0) return;

            menu.Show(e.ListView, e.Location);
        }

        private ContextMenuStrip CreateRequiredModsContextMenu(ModEntry currentMod, ModEntry dep, IList<ModEntry> selectedDeps)
        {
            var menu = new ContextMenuStrip();
            if (currentMod == null || dep == null || dep.WorkshopID <= 0)
                return menu;

            // Phase 3 / Feature B — open a real picker dialog so the user can search & sort.
            var findSubstitute = new ToolStripMenuItem($"Find substitute for '{dep.Name}'…");
            findSubstitute.Click += (s, a) => OpenAliasPicker(dep);
            menu.Items.Add(findSubstitute);

            menu.Items.Add(new ToolStripSeparator());

            // Phase 2 / Feature A — cross-mod ignore toggle for the right-clicked dep.
            var ignoreAll = new ToolStripMenuItem($"Ignore '{dep.Name}' for all mods that require it");
            ignoreAll.Click += (s, a) => IgnoreDependencyEverywhereWithConfirm(dep, true);
            menu.Items.Add(ignoreAll);

            var stopIgnoreAll = new ToolStripMenuItem($"Stop ignoring '{dep.Name}' for all mods");
            stopIgnoreAll.Click += (s, a) => IgnoreDependencyEverywhereWithConfirm(dep, false);
            menu.Items.Add(stopIgnoreAll);

            // Phase 1 / Feature C — bulk on selection for the currently-viewed mod.
            if (selectedDeps != null && selectedDeps.Count > 1)
            {
                menu.Items.Add(new ToolStripSeparator());

                var ignoreSelected = new ToolStripMenuItem($"Ignore selected ({selectedDeps.Count}) on this mod");
                ignoreSelected.Click += (s, a) => BulkIgnoreOnCurrentMod(currentMod, selectedDeps, true);
                menu.Items.Add(ignoreSelected);

                var unignoreSelected = new ToolStripMenuItem($"Stop ignoring selected ({selectedDeps.Count}) on this mod");
                unignoreSelected.Click += (s, a) => BulkIgnoreOnCurrentMod(currentMod, selectedDeps, false);
                menu.Items.Add(unignoreSelected);
            }

            return menu;
        }

        #endregion
    }
}