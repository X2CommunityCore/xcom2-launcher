using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using FastColoredTextBoxNS;
using Steamworks;
using XCOM2Launcher.Mod;
using XCOM2Launcher.PropertyGrid;
using XCOM2Launcher.Steam;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher.Forms
{
    partial class MainForm
    {

		readonly Style CommentStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
		readonly Style SectionStyle = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Bold);
		readonly Style SeparatorStyle = new TextStyle(Brushes.Chocolate, null, FontStyle.Bold);

		internal void RegisterEvents()
        {
            // Register Events
            // run button
            runXCOM2ToolStripMenuItem.Click += (a, b) => { RunGame(); };

            // save on close
            //Shown += MainForm_Shown;
            //FormClosing += MainForm_FormClosing;

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
            updateEntriesToolStripMenuItem.Click += delegate
            {
                if (_updateWorker.IsBusy)
                    return;

                _updateWorker.RunWorkerAsync();
            };
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
            //modinfo_readme_RichTextBox.LinkClicked += ControlLinkClicked;
            //modinfo_info_DescriptionRichTextBox.LinkClicked += ControlLinkClicked;
            //export_richtextbox.LinkClicked += ControlLinkClicked;
            //modinfo_changelog_richtextbox.LinkClicked += ControlLinkClicked;

            // Tab Controls
            //main_tabcontrol.Selected += MainTabSelected;
            //modinfo_tabcontrol.Selected += ModInfoTabSelected;

            // Mod Updater
            _updateWorker.DoWork += Updater_DoWork;
            _updateWorker.ProgressChanged += Updater_ProgressChanged;
            _updateWorker.RunWorkerCompleted += Updater_RunWorkerCompleted;

            // Steam Events
#if DEBUG
            Workshop.OnItemDownloaded += SteamWorkshop_OnItemDownloaded;
#endif

            // Main Tabs
            // Export
            export_workshop_link_checkbox.CheckedChanged += ExportCheckboxCheckedChanged;
            export_group_checkbox.CheckedChanged += ExportCheckboxCheckedChanged;
            export_save_button.Click += ExportSaveButtonClick;
            export_load_button.Click += ExportLoadButtonClick;
        }

#if DEBUG
        private void SteamWorkshop_OnItemDownloaded(object sender, Workshop.DownloadItemEventArgs e)
        {
            if (e.Result.m_eResult != EResult.k_EResultOK)
            {
                MessageBox.Show($"{e.Result.m_nPublishedFileId}: {e.Result.m_eResult}");
                return;
            }

            var m = Downloads.SingleOrDefault(x => x.WorkshopID == (long)e.Result.m_nPublishedFileId.m_PublishedFileId);

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
            m = Mods.All.Single(x => x.WorkshopID == (long)e.Result.m_nPublishedFileId.m_PublishedFileId);

            MessageBox.Show($"{m.Name} finished download.");
        }
#endif

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
            Settings.Windows["main"] = new WindowSettings(this) { Data = modlist_ListObjectListView.SaveState() };

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

                Mods.UpdateMod(mod, Settings);

                lock (_updateWorker)
                {
                    numCompletedMods++;
                    _updateWorker.ReportProgress(numCompletedMods, mod);
                }
            });
        }

        private void Updater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (((BackgroundWorker)sender).CancellationPending)
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

        #region Event Handlers
        private void MainTabSelected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == export_tab)
                UpdateExport();
        }

        private void ExportCheckboxCheckedChanged(object sender, EventArgs e)
        {
            UpdateExport();
        }
        private void ExportLoadButtonClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text files|*.txt",
                DefaultExt = "txt",
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = false,
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            // parse file

            var regex = new Regex(@"^\s*(?<name>.*?)[ ]*\t(?<id>.*?)[ ]*\t(?:.*=)?(?<sourceID>\d+)$", RegexOptions.Compiled | RegexOptions.Multiline);

            var mods = Mods.All.ToList();
            var activeMods = new List<ModEntry>();
            var missingMods = new List<Match>();

            foreach (var line in File.ReadAllLines(dialog.FileName))
            {
                var match = regex.Match(line);
                if (!match.Success)
                    continue;

                var entries = mods.Where(mod => mod.ID == match.Groups["id"].Value).ToList();

                if (entries.Count == 0)
                {
                    // Mod missing
                    // -> add to list
                    missingMods.Add(match);
                    continue;
                }

                activeMods.AddRange(entries);

                if (entries.Count > 1)
                {
                    // More than 1 mod
                    // Add warning?
                }
            }

            // Check entries
            if (activeMods.Count == 0)
            {
                MessageBox.Show("No mods found. Bad profile?");
                return;
            }

            // Check missing
            if (missingMods.Count > 0)
            {
                var steamMissingMods = missingMods.Where(match => match.Groups["sourceID"].Value != "Unknown").ToList();

                var text = $"This profile contains {missingMods.Count} mod(s) that are not currently installed:\r\n\r\n";

                foreach (var match in missingMods)
                {
                    text += match.Groups["name"].Value;

                    if (steamMissingMods.Contains(match))
                        text += "*";

                    text += "\r\n";
                }

                if (steamMissingMods.Count != 0)
                {
                    text += "\r\nDo you want to subscribe to the mods marked with an asterisk on Steam?";

                    var result = MessageBox.Show(this, text, "Mods missing!", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Cancel)
                        return;

                    if (result == DialogResult.Yes)
                    {
                        // subscribe
                        foreach (var id in steamMissingMods.Select(match => ulong.Parse(match.Groups["sourceID"].Value)))
                        {
                            SteamUGC.SubscribeItem(id.ToPublishedFileID());
                        }

                        MessageBox.Show("Done. Close the launcher, wait for steam to download the mod(s) and try again.");
                        return;
                    }
                }
                else
                {
                    text += "\r\nDo you wish to continue?";

                    if (MessageBox.Show(this, text, "Mods missing!", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
            }

            // Confirm
            if (MessageBox.Show(this, $"Adopt profile? {activeMods.Count} mods found.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            // Apply changes
            foreach (var mod in mods)
                mod.isActive = false;

            foreach (var mod in activeMods)
                mod.isActive = true;

            modlist_ListObjectListView.UpdateObjects(mods);

            UpdateExport();
            UpdateLabels();
        }

        private void ExportSaveButtonClick(object sender, EventArgs eventArgs)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Text files|*.txt",
                DefaultExt = "txt",
                OverwritePrompt = true,
                AddExtension = true,
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllText(dialog.FileName, export_richtextbox.Text);
        }

        private void ModInfoTabSelected(object sender, TabControlEventArgs e)
        {
            CheckAndUpdateChangeLog(e.TabPage, modlist_ListObjectListView.SelectedObject as ModEntry);
        }

        private async void CheckAndUpdateChangeLog(TabPage tab, ModEntry m)
        {
            if (tab != modinfo_changelog_tab || m == null)
                return;

            modinfo_changelog_richtextbox.Text = "Loading...";
            modinfo_changelog_richtextbox.Text = await ModChangelogCache.GetChangeLogAsync(m.WorkshopID);
        }

        private void ControlLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

		private void filterMods_TextChanged(object sender, EventArgs e)
		{
			TextMatchFilter filter = null;
			int matchKind = 0;
			string txt = ((TextBox) sender).Text;
			if (!String.IsNullOrEmpty(txt))
			{
				switch (matchKind)
				{
					case 0:
					default:
						filter = TextMatchFilter.Contains(modlist_ListObjectListView, txt);
						break;
					case 1:
						filter = TextMatchFilter.Prefix(modlist_ListObjectListView, txt);
						break;
					case 2:
						filter = TextMatchFilter.Regex(modlist_ListObjectListView, txt);
						break;
				}
			}

			// Text highlighting requires at least a default renderer
			if (modlist_ListObjectListView.DefaultRenderer == null)
				modlist_ListObjectListView.DefaultRenderer = new HighlightTextRenderer(filter);

			modlist_ListObjectListView.AdditionalFilter = filter;

		}

		private void modConfigListComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			var comboBox = (ComboBox)sender;
			ModEntry mod = modlist_ListObjectListView.SelectedObject as ModEntry;

			// Invalid selection, somehow
			if (comboBox.SelectedIndex <= -1) return;

			// Get the selected file name and try to get the list of config files for the mod.
			// Then try to get the file path that matches the currently selected name and load that file
			string fileName = comboBox.Text;
			string[] configFiles = mod?.GetConfigFiles();

			if (configFiles == null) return;
			string filePath = Array.Find(configFiles, element => element.Contains(fileName));
			if (filePath == null) return;

			using (var sr = new StreamReader(filePath))
			{
				modinfo_ConfigFCTB.Text = sr.ReadToEnd();
			}

			// Check if this file has values saved, and enable/disable load button
			modinfo_config_LoadButton.Enabled = ModSettingExists(mod.ID, fileName);
		}

		private void modinfo_config_SaveButton_Click(object sender, EventArgs e)
		{
			// Get necessary data
			ModEntry mod = modlist_ListObjectListView.SelectedObject as ModEntry;
			string filename = modinfo_config_FileSelectCueComboBox.Text;
			string text = modinfo_ConfigFCTB.Text;

			// If the data is invalid, just do nothing
			if (mod == null || String.IsNullOrEmpty(text)) return;

			// If the current file exists already, save to it, otherwise create a new one
			if (ModSettingExists(mod.ID, filename))
				Settings.ModSettings[mod.ID][filename] = text;
			else
				Settings.ModSettings.Add(mod.ID, new Dictionary<string, string> { { filename, text } });
		}

		private void modinfo_config_LoadButton_Click(object sender, EventArgs e)
		{
			// Get necessary data
			ModEntry mod = modlist_ListObjectListView.SelectedObject as ModEntry;
			string filename = modinfo_config_FileSelectCueComboBox.Text;

			// If data is not valid
			if (mod == null || ModSettingExists(mod.ID, filename)) return;

			modinfo_ConfigFCTB.Text = Settings.ModSettings[mod.ID][filename];
		}

		private void modinfo_config_ExpandButton_Click(object sender, EventArgs e)
		{
			var layout = modinfo_config_TableLayoutPanel;
			if (layout.Parent == modinfo_config_tab)
			{
				layout.Parent = fillPanel;
				fillPanel.Visible = true;
				fillPanel.BringToFront();
				layout.Dock = DockStyle.Fill;
				modinfo_config_ExpandButton.Text = "Collapse";
				toolTip.SetToolTip(modinfo_config_ExpandButton, "Collapse the INI editor to normal size.");
			}
			else
			{
				layout.Parent = modinfo_config_tab;
				layout.Dock = DockStyle.Fill;
				layout.BringToFront();
				fillPanel.Visible = false;
				fillPanel.SendToBack();
				modinfo_config_ExpandButton.Text = "Expand";
				toolTip.SetToolTip(modinfo_config_ExpandButton, "Expand the INI editor to fill the window.");

			}
		}

		private void modinfo_ConfigFCTB_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
		{
			e.ChangedRange.ClearStyle(CommentStyle);
			e.ChangedRange.ClearStyle(SectionStyle);
			e.ChangedRange.ClearStyle(SeparatorStyle);

			e.ChangedRange.SetStyle(CommentStyle, @";.*$", RegexOptions.Multiline);
			e.ChangedRange.SetStyle(SectionStyle, @"^\[.*\]", RegexOptions.Multiline);
			e.ChangedRange.SetStyle(SeparatorStyle, @"^.*?(?<range>=)", RegexOptions.Multiline);
		}

		private void AdjustWidthComboBox_DropDown(object sender, EventArgs e)
		{
			var senderComboBox = (ComboBox)sender;
			int width = senderComboBox.DropDownWidth;
			Graphics g = senderComboBox.CreateGraphics();
			Font font = senderComboBox.Font;

			int vertScrollBarWidth = (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
					? SystemInformation.VerticalScrollBarWidth : 0;

			var itemsList = senderComboBox.Items.Cast<object>().Select(item => item.ToString());

			foreach (string s in itemsList)
			{
				int newWidth;
				using (g = senderComboBox.CreateGraphics())
				{
					newWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;
				}

				if (width >= newWidth) continue;
				width = newWidth;
			}

			senderComboBox.DropDownWidth = width;
		}

		private void modlist_toggleGroupsButton_Click(object sender, EventArgs e)
		{
			var numGroups = modlist_ListObjectListView.OLVGroups.Count;
			var collapsedGroups = modlist_ListObjectListView.OLVGroups.Count(@group => @group.Collapsed);

			if (collapsedGroups < (numGroups / 2))
			{
				foreach (var group in modlist_ListObjectListView.OLVGroups)
					group.Collapsed = true;
			}
			else
			{
				foreach (var group in modlist_ListObjectListView.OLVGroups)
					group.Collapsed = false;
			}
		}

		private void modlist_filterClearButton_Click(object sender, EventArgs e)
		{
			modlist_FilterCueTextBox.Text = "";
		}
		#endregion

		private bool ModSettingExists(string modId, string filename)
		{
			if (!Settings.ModSettings.ContainsKey(modId)) return false;
			if (!Settings.ModSettings[modId].ContainsKey(filename)) return false;
			return true;
		}



	}
}