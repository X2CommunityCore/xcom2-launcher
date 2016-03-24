using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Steamworks;
using XCOM2Launcher.Mod;
using XCOM2Launcher.PropertyGrid;
using XCOM2Launcher.Steam;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher.Forms
{
    partial class MainForm
    {
        internal void RegisterEvents()
        {
            // Register Events
            // run button
            run_game_button.Click += (a, b) => { RunGame(); };

            // save on close
            Shown += MainForm_Shown;
            FormClosing += MainForm_FormClosing;
            // Menu
            // -> File
            saveToolStripMenuItem.Click += delegate { Save(); };
            reloadToolStripMenuItem.Click += delegate
            {
                // Confirmation dialog
                var r = MessageBox.Show("Unsaved changes will be lost.\r\nAre you sure?", "Reload mod list?", MessageBoxButtons.OKCancel);
                if (r != DialogResult.OK)
                    return;

                Reset();
            };
            searchForModsToolStripMenuItem.Click += delegate { Settings.ImportMods(); };

            // -> Settings
            // show hidden
            showHiddenModsToolStripMenuItem.Click += delegate
            {
                Settings.ShowHiddenElements = showHiddenModsToolStripMenuItem.Checked;
                RefreshModList();
            };

            // Edit
            editSettingsToolStripMenuItem.Click += delegate
            {
                new SettingsDialog(Settings).ShowDialog();
                RefreshModList();
            };

            exitToolStripMenuItem.Click += (sender, e) => { Close(); };

            // -> Tools
            cleanModsToolStripMenuItem.Click += delegate { new CleanModsForm(Settings).ShowDialog(); };
            importActiveModsToolStripMenuItem.Click += delegate
            {
                XCOM2.ImportActiveMods(Settings);
                RefreshModList();
            };

            // RichTextBox clickable links
            modinfo_readme_richtextbox.LinkClicked += ControlLinkClicked;
            modinfo_description_richtextbox.LinkClicked += ControlLinkClicked;
            export_richtextbox.LinkClicked += ControlLinkClicked;
            modinfo_changelog_richtextbox.LinkClicked += ControlLinkClicked;

            // TabControl (mainly for the changelog)
            modinfo_tabcontrol.Selected += Tab_Selected;

            // Mod Updater
            _updateWorker.DoWork += Updater_DoWork;
            _updateWorker.ProgressChanged += Updater_ProgressChanged;
            _updateWorker.RunWorkerCompleted += Updater_RunWorkerCompleted;

            // Steam Events
            Workshop.OnItemDownloaded += SteamWorkshop_OnItemDownloaded;
        }


        private void SteamWorkshop_OnItemDownloaded(object sender, Workshop.DownloadItemEventArgs e)
        {
            if (e.Result.m_eResult != EResult.k_EResultOK)
            {
                MessageBox.Show($"{e.Result.m_nPublishedFileId}: {e.Result.m_eResult}");
                return;
            }

            var m = Downloads.SingleOrDefault(x => x.WorkshopID == (long) e.Result.m_nPublishedFileId.m_PublishedFileId);

            if (m != null)
            {
                // look for .XComMod file
                var infoFile = Directory.GetFiles(m.Path, "*.XComMod", SearchOption.TopDirectoryOnly).SingleOrDefault();
                if (infoFile == null)
                    throw new Exception("Invalid Download");

                // Fill fields
                m.State &= ~ModState.NotInstalled;
                m.ID = Path.GetFileNameWithoutExtension(infoFile);
                m.Image = null; // Use default image again

                // load info
                var info = new ModInfo(m.GetModInfoFile());

                // Move mod
                Downloads.Remove(m);
                Mods.AddMod(info.Category, m);

                // update listitem
                //var item = modlist_listview.Items.Cast<ListViewItem>().Single(i => (i.Tag as ModEntry).SourceID == m.SourceID);
                //UpdateModListItem(item, info.Category);
            }
            m = Mods.All.Single(x => x.WorkshopID == (long) e.Result.m_nPublishedFileId.m_PublishedFileId);

            MessageBox.Show($"{m.Name} finished download.");
        }

        private void Tab_Selected(object sender, TabControlEventArgs e)
        {
            CheckAndUpdateChangeLog(e.TabPage, modlist_objectlistview.SelectedObject as ModEntry);
        }

        private async void CheckAndUpdateChangeLog(TabPage tab, ModEntry m)
        {
            if (tab == modinfo_changelog_tab && m != null)
            {
                /*ModChangelogCache.GetChangeLogAsync(m.WorkshopID).ContinueWith((changelog) =>
                {
                    modinfo_changelog_richtextbox.Text = changelog.Result;
                });*/
                modinfo_changelog_richtextbox.Text = "Loading...";
                modinfo_changelog_richtextbox.Text = await ModChangelogCache.GetChangeLogAsync(m.WorkshopID);
            }
        }

        #region Event Handlers

        private void ControlLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        #endregion

        #region Form

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Settings.Windows.ContainsKey("main"))
            {
                var setting = Settings.Windows["main"];
                DesktopBounds = setting.Bounds;
                WindowState = setting.State;
            }
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateWorker.CancelAsync();

            // Save dimensions
            Settings.Windows["main"] = new WindowSettings(this) {Data = modlist_objectlistview.SaveState()};

            Save();
        }

        // Make sure property grid columns are properly sized
        private void modinfo_inspect_propertygrid_Layout(object sender, LayoutEventArgs e)
        {
            modinfo_inspect_propertygrid.SetLabelColumnWidth(100);
        }

        #endregion

        #region Mod Updater

        private readonly BackgroundWorker _updateWorker = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        private void Updater_DoWork(object sender, DoWorkEventArgs e)
        {
            _updateWorker.ReportProgress(0);
            var numCompletedMods = 0;
            Parallel.ForEach(Mods.All.ToList(), mod =>
            {
                if (_updateWorker.CancellationPending || Disposing || IsDisposed)
                {
                    e.Cancel = true;
                    return;
                }

                Mods.UpdateMod(mod);

                lock (_updateWorker)
                {
                    numCompletedMods++;
                    _updateWorker.ReportProgress(numCompletedMods, mod);
                }
            });
        }

        private void Updater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (((BackgroundWorker) sender).CancellationPending)
                return;

            progress_toolstrip_progressbar.Value = e.ProgressPercentage;
            status_toolstrip_label.Text = $"Updating Mods... ({e.ProgressPercentage} / {progress_toolstrip_progressbar.Maximum})";

            // Update list item
            var m = e.UserState as ModEntry;
            if (m == null)
                return;

            UpdateMod(m);
        }

        private void Updater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                status_toolstrip_label.Text = "Cancelled.";
                return;
            }

            progress_toolstrip_progressbar.Visible = false;
            status_toolstrip_label.Text = StatusBarIdleString;
            RefreshModList();
        }

        #endregion
    }
}