using System;
using System.Collections;
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
using XCOM2Launcher.Steam;

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
            
#if !DEBUG
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
#else
            InitializeInterface();
#endif
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
            cFilterMissingDependency.Text = $"Missing dependencies ({allMods.Count(m => m.isActive && m.State.HasFlag(ModState.MissingDependencies))})";
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

            btnDescSave.Enabled = false;
            btnDescUndo.Enabled = false;
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

                // Add the mod id to or remove it from the IgnoredDependencies list.
                if (checkState)
                {
                    if (!CurrentMod.IgnoredDependencies.Contains(mod.WorkshopID))
                    {
                        CurrentMod.IgnoredDependencies.Add(mod.WorkshopID);
                    }
                }
                else
                {
                    if (CurrentMod.IgnoredDependencies.Contains(mod.WorkshopID))
                    {
                        CurrentMod.IgnoredDependencies.Remove(mod.WorkshopID);
                    }
                }

                Mods.UpdatedModDependencyState(CurrentMod);
                modlist_ListObjectListView.RefreshObject(CurrentMod);
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

        #endregion
    }
}