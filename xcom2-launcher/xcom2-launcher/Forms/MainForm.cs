using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Steamworks;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Steam;
using XCOM2Launcher.XCOM;
using JR.Utils.GUI.Forms;

namespace XCOM2Launcher.Forms
{
	public partial class MainForm
	{
        private const string StatusBarIdleString = "Ready.";
        private const string ExclamationIconKey = "Exclamation";

		public MainForm(Settings settings)
        {
            //
            InitializeComponent();

            // Settings
            SteamAPIWrapper.InitSafe();
            Settings = settings;

            // Restore states 
            showHiddenModsToolStripMenuItem.Checked = settings.ShowHiddenElements;

            // Hide WotC button if necessary
            runWarOfTheChosenToolStripMenuItem.Visible = Directory.Exists(settings.GamePath + @"\XCom2-WarOfTheChosen");

            // Init interface
            InitObjectListView();
            UpdateInterface();
            RegisterEvents();

            //Other intialization
            InitializeTabImages();

#if !DEBUG
			// Check for Updates
			CheckSteamForUpdates();
#endif

            // Run callbacks
            var t1 = new Timer();
            t1.Tick += (sender, e) => { SteamAPIWrapper.RunCallbacks(); };
            t1.Interval = 10;
            t1.Start();

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
        }

		public Settings Settings { get; set; }

        private void InitializeTabImages()
        {
            tabImageList.Images.Add(ExclamationIconKey, error_provider.Icon);
        }

        private void CheckSteamForNewMods()
        {
            status_toolstrip_label.Text = "Checking for new mods...";

            ulong[] subscribedIDs;
            try
            {
                subscribedIDs = Workshop.GetSubscribedItems();
            }
            catch (InvalidOperationException)
            {
                // Steamworks not initialized?
                // Game taking over?
                status_toolstrip_label.Text = "Error checking for new mods.";
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
                var details = detailsRequest.Result;
                var link = detailsRequest.GetPreviewURL();

                var downloadMod = new ModEntry
                {
                    Name = details.m_rgchTitle,
                    DateCreated = DateTimeOffset.FromUnixTimeSeconds(details.m_rtimeCreated).DateTime,
                    DateUpdated = DateTimeOffset.FromUnixTimeSeconds(details.m_rtimeUpdated).DateTime,
                    //Path = Path.Combine(Settings.GetWorkshopPath(), "" + id),
                    Image = link,
                    Source = ModSource.SteamWorkshop,
                    WorkshopID = (int) id,
                    State = ModState.New | ModState.NotInstalled
                };

                // Start download
                Workshop.DownloadItem(id);
                //
                Downloads.Add(downloadMod);
                change = true;
            }

            if (change)
                RefreshModList();

            status_toolstrip_label.Text = StatusBarIdleString;
        }

        private void CheckSteamForUpdates()
        {
            progress_toolstrip_progressbar.Value = 0;
            progress_toolstrip_progressbar.Maximum = Mods.All.Count();
            progress_toolstrip_progressbar.Visible = true;
            _updateWorker.RunWorkerAsync();
        }

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
            _updateWorker.CancelAsync();
            // let's hope it cancels fast enough...

            modlist_ListObjectListView.Clear();

            Settings = Program.InitializeSettings();

            InitObjectListView();

            //UpdateWorker.RunWorkerAsync();
            //RefreshModList();
        }

        private void Save(bool WotC)
        {
            XCOM2.SaveChanges(Settings, WotC);
            Settings.SaveFile("settings.json");
        }

        private void RunGame()
        {
            _updateWorker.CancelAsync();
            Settings.Instance.LastLaunchedWotC = false;

            // Check for WOTC only mods
            if (Settings.Mods.Active.Count(e => e.BuiltForWOTC) > 0)
            {
                if (FlexibleMessageBox.Show(this, 
                    "Are you sure you want to proceed? Please be warned that this is very likely to crash your game. Offending mods:\r\n" + 
                    String.Join("\r\n", Settings.Mods.Active.Where(e => e.BuiltForWOTC).Select(e => e.Name)),
                    "You are trying to launch vanilla game with mods built for WOTC", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    RunVanilla();
            }
            else
            {
                RunVanilla();
            }
        }
        
        private void RunVanilla()
        { 
            Save(false);

            XCOM2.RunGame(Settings.GamePath, Settings.Arguments.ToString());

            if (Settings.CloseAfterLaunch)
                Close();
        }

        private void RunWotC()
        {
            _updateWorker.CancelAsync();
            Settings.Instance.LastLaunchedWotC = true;
            Save(true);

            XCOM2.RunWotC(Settings.GamePath, Settings.Arguments.ToString());

            if (Settings.CloseAfterLaunch)
                Close();
        }

        #endregion


        #region Interface updates

        private void UpdateInterface()
        {
            error_provider.Clear();

            // Incompability warnings and overwrites grid
            UpdateConflicts();

            // ModEntry list
            // RefreshModList();

            // ModEntry details
            UpdateModInfo(modlist_ListObjectListView.SelectedObject as ModEntry);

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            //
            var hasConflicts = NumConflicts > 0;
            modlist_tab.Text = $"Mods ({Mods.Active.Count()} / {Mods.All.Count()})";
            conflicts_tab.Text = "Overrides" + (hasConflicts ? $" ({NumConflicts} Conflicts)" : "");
            conflicts_tab.ImageKey = hasConflicts ? ExclamationIconKey : null;
        }

        public int NumConflicts;

        private void UpdateConflicts()
        {
            // Incremented later in GetDuplicatesString() and GetOverridesString()
            NumConflicts = 0;

            // Clear and refill conflicts_datagrid
            conflicts_datagrid.Rows.Clear();

            foreach (var m in Mods.Active)
            {
                foreach (var classOverride in m.GetOverrides(true))
                {
                    var oldClass = classOverride.OldClass;

                    if (classOverride.OverrideType == ModClassOverrideType.UIScreenListener)
                        oldClass += " (UIScreenListener)";

                    conflicts_datagrid.Rows.Add(m.Name, oldClass, classOverride.NewClass);
                }
            }

            // Conflict log
            conflicts_textbox.Text = GetDuplicatesString() + GetOverridesString();

            // Update interface
            modlist_ListObjectListView.UpdateObjects(ModList.Objects.ToList());
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
                    foreach (var classOverride in m.GetOverrides(true))
                    {
                        var oldClass = classOverride.OldClass;

                        if (classOverride.OverrideType == ModClassOverrideType.UIScreenListener)
                            oldClass += " (UIScreenListener)";

                        conflicts_datagrid.Rows.Add(m.Name, oldClass, classOverride.NewClass);
                    }
                }
                else
                {
                    foreach (var classOverride in m.GetOverrides(true))
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

            // Conflict log
            conflicts_textbox.Text = GetDuplicatesString() + GetOverridesString();

            // Update interface
            modlist_ListObjectListView.UpdateObjects(mods);
            UpdateLabels();
        }

        private string GetDuplicatesString()
        {
            var str = new StringBuilder();

            var duplicates = Mods.GetDuplicates().ToList();
            if (duplicates.Any())
            {
                str.AppendLine("Mods with colliding package ids found!");
                str.AppendLine("These can only be (de-)activated together.");
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

            str.AppendLine("Mods with colliding overrides found!");
            str.AppendLine("These mods will not (fully) work when run together.");
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

        private void UpdateModInfo(ModEntry m)
        {
            if (m == null)
            {
                // hide panel
                //horizontal_splitcontainer.Panel2Collapsed = true;
                return;
            }

            // show panel
            horizontal_splitcontainer.Panel2Collapsed = false;

            // Update data
            modinfo_info_TitleTextBox.Text = m.Name;
            modinfo_info_AuthorTextBox.Text = m.Author;
            modinfo_info_DateCreatedTextBox.Text = m.DateCreated?.ToString() ?? "";
            modinfo_info_InstalledTextBox.Text = m.DateAdded?.ToString() ?? "";
            modinfo_info_DescriptionRichTextBox.Text = m.GetDescription();
            modinfo_readme_RichTextBox.Text = m.GetReadMe();
            modinfo_image_picturebox.ImageLocation = m.Image;

            modinfo_inspect_propertygrid.SelectedObject = m;

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
		}

		#endregion
	}
}