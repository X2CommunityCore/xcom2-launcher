using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Steamworks;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Steam;
using XCOM2Launcher.XCOM;

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

#if !DEBUG
            // hide config tab
            modinfo_config_tab.Parent.Controls.Remove(modinfo_config_tab);
#endif

            // Init interface
            InitObjectListView();
            UpdateInterface();
            RegisterEvents();

            //Other intialization
            InitializeTabImages();

            // Check for Updates
            CheckSteamForUpdates();

            // Check for running downloads
#if DEBUG
            if (Settings.GetWorkshopPath() != null)
            {
                CheckSteamForNewMods();

                var t = new Timer();
                t.Tick += (sender, e) => { CheckSteamForNewMods(); };
                t.Interval = 30000;
                t.Start();
            }
#endif
        }

        private void InitializeTabImages()
        {
            tabImageList.Images.Add(ExclamationIconKey, error_provider.Icon);
        }

        public Settings Settings { get; set; }

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
                    Path = Path.Combine(Settings.GetWorkshopPath(), "" + id),
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

        #region Basic

        private void Reset()
        {
            _updateWorker.CancelAsync();
            // let's hope it cancels fast enough...

            modlist_objectlistview.Clear();

            Settings = Program.InitializeSettings();

            InitObjectListView();

            //UpdateWorker.RunWorkerAsync();
            //RefreshModList();
        }

        private void Save()
        {
            XCOM2.SetActiveMods(Mods.Active.ToList());
            Settings.SaveFile("settings.json");
        }

        private void RunGame()
        {
            _updateWorker.CancelAsync();
            Save();

            XCOM2.RunGame(Settings.GamePath, Settings.Arguments.ToString());

            if (Settings.CloseAfterLaunch)
                Close();
        }

        #endregion

        #region Interface updates

        private void UpdateInterface()
        {
            error_provider.Clear();

            // ModEntry list
            RefreshModList();

            // Incompability warnings and overwrites grid
            UpdateConflicts();

            // ModEntry details
            UpdateModInfo(modlist_objectlistview.SelectedObject as ModEntry);

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            //
            UpdateExport();

            //
            bool hasConflicts = (NumConflicts > 0);
            modlist_tab.Text = $"Mods ({Mods.Active.Count()} / {Mods.All.Count()})";
            conflicts_tab.Text = "Overrides" + (hasConflicts ? $" ({NumConflicts} Conflicts)" : "");
            conflicts_tab.ImageKey = (hasConflicts ? ExclamationIconKey : null);
        }


        public int NumConflicts;

        private void UpdateConflicts()
        {
            NumConflicts = 0;

            // Datagrid
            conflicts_datagrid.Rows.Clear();

            foreach (var m in Mods.Active)
            {
                foreach (var classOverride in m.GetOverrides(true))
                {
                    string oldClass = classOverride.OldClass;
                    if (classOverride.OverrideType == ModClassOverrideType.UIScreenListener)
                    {
                        oldClass += " (UIScreenListener)";
                    }
                    conflicts_datagrid.Rows.Add(m.Name, oldClass, classOverride.NewClass);
                }
            }

            // Conflict log
            Mods.MarkDuplicates();

            conflicts_textbox.Text = GetDuplicatesString() + GetOverridesString();
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
            StringBuilder str = new StringBuilder();

            bool showUIScreenListenerMessage = false;

            var conflicts = Mods.GetActiveConflicts().ToList();
            if(conflicts.Any())
            {
                str.AppendLine("Mods with colliding overrides found!");
                str.AppendLine("These mods will not (fully) work when run together.");
                str.AppendLine();

                foreach(var conflict in conflicts)
                {
                    str.AppendLine($"Conflict found for '{conflict.ClassName}':");
                    bool hasMultipleUIScreenListeners = (conflict.Overrides.Count(o => o.OverrideType == ModClassOverrideType.UIScreenListener) > 1);

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

                if(showUIScreenListenerMessage)
                {
                    str.AppendLine("* (These mods use UIScreenListeners, meaning they do not conflict with each other)");
                    str.AppendLine();
                }
            }

            return str.ToString();
        }

        private void UpdateModInfo(ModEntry m)
        {
            if (m == null)
            {
                // hide panel
                horizontal_splitcontainer.Panel2Collapsed = true;
                return;
            }

            // show panel
            horizontal_splitcontainer.Panel2Collapsed = false;


            // 
            modinfo_title_textbox.Text = m.Name;
            modinfo_author_textbox.Text = m.Author;
            modinfo_date_created_textbox.Text = m.DateCreated?.ToString() ?? "";
            modinfo_date_added_textbox.Text = m.DateAdded?.ToString() ?? "";
            modinfo_description_richtextbox.Text = m.GetDescription();
            modinfo_readme_richtextbox.Text = m.GetReadMe();
            modinfo_image_picturebox.ImageLocation = m.Image;

            modinfo_inspect_propertygrid.SelectedObject = m;

            #region Config

            // config files
            //string[] configFiles = m.getConfigFiles();

            //// clear
            //modinfo_config_propertygrid.SelectedObjects = new object[] { };

            //if (configFiles.Length > 0)
            //{
            //    List<ConfigFile> configs = new List<ConfigFile>();

            //    foreach (string configFile in configFiles)
            //    {

            //        ConfigFile config = new ConfigFile
            //        {
            //            Name = Path.GetFileName(configFile)
            //        };

            //        var setting = new ConfigSetting
            //        {
            //            Name = "Unknown",
            //            Category = "Unknown",
            //            Value = 100,
            //            DefaultValue = 10,
            //            Desc = "123"
            //        };

            //        config.Settings.Add(setting);
            //        configs.Add(config);
            //    }

            //    modinfo_config_propertygrid.SelectedObjects = configs.ToArray
            //}

            #endregion
        }

        #endregion

        #region Export

        private void export_group_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateExport();
        }

        private void export_workshop_link_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateExport();
        }

        private void UpdateExport()
        {
            var str = new StringBuilder();

            if (!Mods.Active.Any())
            {
                export_richtextbox.Text = "";
                return;
            }

            int name_length = Mods.Active.Max(m => m.Name.Length) + 1;
            int id_length = Mods.Active.Max(m => m.ID.Length) + 1;
            foreach (var entry in Mods.Entries.Where(e => e.Value.Entries.Any(m => m.isActive)))
            {
                var mods = entry.Value.Entries.Where(m => m.isActive).ToList();

                if (export_group_checkbox.Checked)
                    str.AppendLine($"{entry.Key} ({mods.Count()}):");

                foreach (var mod in mods)
                {
                    str.Append(String.Format("\t{0,-" + name_length + "} ", mod.Name));
                    str.Append(String.Format("{0,-" + id_length + "} ", mod.ID));

                    if (mod.WorkshopID == -1)
                        str.Append("Unknown");

                    else if (export_workshop_link_checkbox.Checked)
                        str.Append(mod.GetWorkshopLink());

                    else
                        str.Append(mod.WorkshopID.ToString());

                    str.AppendLine();
                }

                if (export_group_checkbox.Checked)
                    str.AppendLine();
            }

            export_richtextbox.Text = str.ToString();
        }

        #endregion
    }
}