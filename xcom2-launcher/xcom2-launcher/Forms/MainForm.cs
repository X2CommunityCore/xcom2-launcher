using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using XCOM2Launcher.Forms;
using System.Reflection;
using XCOM2Launcher.Classes.PropertyGrid;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Helper;
using XCOM2Launcher.Mod;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    public partial class MainForm : Form
    {
        private const string StatusBarIdleString = "Ready.";

        public Settings Settings { get; set; }

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
            initObjectListView();
            UpdateInterface();
            RegisterEvents();

            // Check for Updates
            CheckSteamForUpdates();

            // Check for running downloads
#if DEBUG
            if (Settings.GetWorkshopPath() != null)
            {
                CheckSteamForNewMods();

                Timer t = new Timer();
                t.Tick += (sender, e) => { CheckSteamForNewMods(); };
                t.Interval = 30000;
                t.Start();
            }
#endif
        }

        private void CheckSteamForNewMods()
        {
            status_toolstrip_label.Text = "Checking for new mods...";

            ulong[] subscribedIDs;
            try
            {
                subscribedIDs = Steam.Workshop.GetSubscribedItems();
            }
            catch (InvalidOperationException)
            {
                // Steamworks not initialized?
                // Game taking over?
                status_toolstrip_label.Text = "Error checking for new mods.";
                return;
            }

            var change = false;
            foreach (ulong id in subscribedIDs)
            {
                var status = Steam.Workshop.GetDownloadStatus(id);
                if (id == 622560612)
                    status.ToString();

                if (status.HasFlag(Steamworks.EItemState.k_EItemStateInstalled))
                    // already installed
                    continue;

                if (Downloads.Any(d => d.WorkshopID == (long)id))
                    // already observing
                    continue;

                // Get info
                var detailsRequest = new Steam.ItemDetailsRequest(id).Send().waitForResult();
                var details = detailsRequest.Result;
                var link = detailsRequest.GetPreviewURL();

                ModEntry downloadMod = new ModEntry
                {
                    Name = details.m_rgchTitle,
                    DateCreated = DateTimeOffset.FromUnixTimeSeconds(details.m_rtimeCreated).DateTime,
                    DateUpdated = DateTimeOffset.FromUnixTimeSeconds(details.m_rtimeUpdated).DateTime,


                    Path = Path.Combine(Settings.GetWorkshopPath(), "" + id),
                    Image = link,
                    Source = ModSource.SteamWorkshop,
                    WorkshopID = (int)id,
                    State = ModState.New | ModState.NotInstalled
                };

                // Start download
                Steam.Workshop.DownloadItem(id);
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
            UpdateWorker.RunWorkerAsync();
        }



        #region Basic
        private void Reset()
        {
            UpdateWorker.CancelAsync();
            // let's hope it cancels fast enough...

            modlist_objectlistview.Clear();

            Settings = Program.initializeSettings();

            initObjectListView();

            //UpdateWorker.RunWorkerAsync();
            //RefreshModList();
        }

        private void Save()
        {
            XCOM2.setActiveMods(Mods.Active.ToList());
            Settings.saveFile("settings.json");
        }

        private void RunGame()
        {
            UpdateWorker.CancelAsync();
            Save();

            XCOM2.RunGame(Settings.GamePath, Settings.Arguments.ToString());

            if (Settings.CloseAfterLaunch)
                this.Close();
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
            modlist_tab.Text = $"Mods ({Mods.Active.Count()} / {Mods.All.Count()})";
            conflicts_tab.Text = "Conflicts";
            if (num_conflicts > 0)
                conflicts_tab.Text += $" ({num_conflicts})";
        }



        public int num_conflicts = 0;

        private void UpdateConflicts()
        {
            num_conflicts = 0;

            // Datagrid
            conflicts_datagrid.Rows.Clear();

            foreach (ModEntry m in Mods.Active)
            {
                foreach (ModClassOverride classOverride in m.GetClassOverrides())
                {
                    conflicts_datagrid.Rows.Add(new object[] { m.Name, classOverride.OldClass, classOverride.NewClass });
                }
            }

            // Conflict log
            Mods.MarkDuplicates();

            StringBuilder str = new StringBuilder();

            var duplicates = Mods.GetDuplicates();
            if (duplicates.Count() > 0)
            {
                str.AppendLine("Mods with colliding package ids found!");
                str.AppendLine("These can only be (de-)activated together.");
                str.AppendLine();

                foreach (var grouping in duplicates)
                {
                    num_conflicts++;

                    str.AppendLine(grouping.Key);

                    foreach (ModEntry m in grouping)
                        str.AppendLine($"\t{m.Name}");


                    str.AppendLine();
                }

                str.AppendLine();
            }

            // Override conflicts
            Dictionary<string, List<ModEntry>> overrides = Mods.getOverrides(Mods.Active);
            var conflicts = from a in overrides where a.Value.Count > 1 select a;

            if (conflicts.Any())
            {
                str.AppendLine("Mods with colliding overrides found!");
                str.AppendLine("These mods will not (fully) work when run together.");
                str.AppendLine();

                foreach (KeyValuePair<string, List<ModEntry>> entry in conflicts)
                {
                    str.AppendLine($"Conflict found for '{entry.Key}':");

                    foreach (ModEntry m in entry.Value)
                        str.AppendLine($"\t{m.Name}");

                    str.AppendLine();

                    num_conflicts++;
                }

                error_provider.SetError(label3, "Found " + num_conflicts + " conflicts");
            }

            conflicts_textbox.Text = str.ToString();
            UpdateLabels();
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
            modinfo_date_created_textbox.Text = (m.DateCreated == null) ? "" : m.DateCreated.Value.ToString();
            modinfo_date_added_textbox.Text = (m.DateAdded == null) ? "" : m.DateAdded.Value.ToString();
            modinfo_description_richtextbox.Text = m.GetDescription();
            modinfo_readme_richtextbox.Text = m.GetReadMe();
            modinfo_image_picturebox.ImageLocation = m.Image;

            modinfo_details_propertygrid.SelectedObject = m;
            return;


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
            StringBuilder str = new StringBuilder();

            if (!Mods.Active.Any())
            {
                export_richtextbox.Text = "";
                return;
            }

            int name_length = Mods.Active.Max(m => m.Name.Length) + 1;
            int id_length = Mods.Active.Max(m => m.ID.Length) + 1;
            foreach (var entry in Mods.Entries.Where(e => e.Value.Entries.Any(m => m.isActive)))
            {
                var mods = entry.Value.Entries.Where(m => m.isActive);

                if (export_group_checkbox.Checked)
                    str.AppendLine($"{entry.Key} ({mods.Count()}):");

                foreach (ModEntry mod in mods)
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

