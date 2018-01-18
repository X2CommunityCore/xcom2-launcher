using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using FastColoredTextBoxNS;
using JR.Utils.GUI.Forms;
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
            // run buttons
            runXCOM2ToolStripMenuItem.Click += (a, b) => { RunGame(); };
            runWarOfTheChosenToolStripMenuItem.Click += (a, b) => { RunWotC(); };

            // save on close
            //Shown += MainForm_Shown;
            //FormClosing += MainForm_FormClosing;

            // Menu
            // -> File
            saveToolStripMenuItem.Click += delegate { Save(Settings.Instance.LastLaunchedWotC); };
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

                CheckSteamForUpdates();
            };
            // -> Settings
            // show hidden
            showHiddenModsToolStripMenuItem.Click += delegate
            {
                Settings.ShowHiddenElements = showHiddenModsToolStripMenuItem.Checked;
				olvcHidden.IsVisible = showHiddenModsToolStripMenuItem.Checked;
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

            resubscribeToModsToolStripMenuItem.Click += delegate
            {
                var modsToDownload = Mods.All.Where(m => m.State.HasFlag(ModState.NotInstalled) && m.Source == ModSource.SteamWorkshop).ToList();
                var choice = false;

                if (modsToDownload.Count == 0)
                    MessageBox.Show("No uninstalled workshop mods were found.");
                else if (modsToDownload.Count == 1)
                    choice = MessageBox.Show($"Are you sure you want to download the mod {modsToDownload[0].Name}?", "Confirm Download", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK;
                else
                    choice = MessageBox.Show($"Are you sure you want to download {modsToDownload.Count} mods?", "Confirm Download", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK;
                
                if (choice)
                {
                    foreach (var m in modsToDownload)
                    {
                        Workshop.Subscribe((ulong)m.WorkshopID);
                        Workshop.DownloadItem((ulong)m.WorkshopID);
                    }
                    MessageBox.Show("Launch XCOM 2 after the download is finished in order to use the mod" + (modsToDownload.Count == 1 ? "." : "s."));
                }
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
            Workshop.OnItemDownloaded += Resubscribe_OnItemDownloaded;

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

        private void Resubscribe_OnItemDownloaded(object sender, Workshop.DownloadItemEventArgs e)
        {
            var mod = Mods.All.SingleOrDefault(m => m.WorkshopID == (long)e.Result.m_nPublishedFileId.m_PublishedFileId);

            if (mod.State == ModState.NotInstalled && e.Result.m_eResult == EResult.k_EResultOK)
            {
                mod.State &= ~ModState.NotInstalled;
                RefreshModList();
            }
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

            Save(Settings.Instance.LastLaunchedWotC);
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

			var categoryRegex = new Regex(@"^(?<category>.*?)\s\(\d*\):$", RegexOptions.Compiled | RegexOptions.Multiline);
            var modEntryRegex = new Regex(@"^\s*(?<name>.*?)[ ]*\t(?<id>.*?)[ ]*\t(?:.*=)?(?<sourceID>\d+)([ ]*\t(?<tags>.*?))?$", RegexOptions.Compiled | RegexOptions.Multiline);

            var mods = Mods.All.ToList();
            var activeMods = new List<ModEntry>();
            var missingMods = new List<Match>();
	        var categoryName = "";

            foreach (var line in File.ReadAllLines(dialog.FileName))
            {
	            var categoryMatch = categoryRegex.Match(line);
	            if (categoryMatch.Success)
		            categoryName = categoryMatch.Groups["category"].Value;

                var modMatch = modEntryRegex.Match(line);
                if (!modMatch.Success)
                    continue;

	    //        var active = false;
	    //        if (modMatch.Groups["active"].Success)
	    //        {
					//// Try just in case the value doesn't work, failsafe value is false, so this shouldn't be an issue
		   //         try
					//{
					//	active = bool.Parse(modMatch.Groups["active"].Value);
					//}
					//catch
					//{ }
	    //        }

                var entries = mods.Where(mod => mod.ID == modMatch.Groups["id"].Value).ToList();

                if (entries.Count == 0)
                {
                    // Mod missing
                    // -> add to list
                    missingMods.Add(modMatch);
                    continue;
                }

                activeMods.AddRange(entries);

                var tags = modMatch.Groups["tags"].Value.Split(';');

                foreach (var tag in tags)
                {
                    if (AvailableTags.ContainsKey(tag) == false)
                    {
                        AvailableTags[tag] = new ModTag(tag);
                    }
                }

	            foreach (var modEntry in entries)
	            {
	                modEntry.Tags = tags.ToList();
		            Mods.RemoveMod(modEntry);
					Mods.AddMod(categoryName, modEntry);
		            //modEntry.isActive = active;
	            }

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

                    var result = FlexibleMessageBox.Show(this, text, "Mods missing!", MessageBoxButtons.YesNoCancel);

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

                    if (FlexibleMessageBox.Show(this, text, "Mods missing!", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
            }

            // Confirm
            if (FlexibleMessageBox.Show(this, $"Adopt profile? {activeMods.Count} mods found.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
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

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			//var duplicates = Mods.All.GroupBy(m => m.Index).Where(g => g.Count() > 1).ToList();
			//modlist_ListObjectListView.ModelFilter = new ModelFilter(delegate (object x)
			//{
			//	ModEntry mod = x as ModEntry;
			//	return duplicates.Contains();
			//});
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


		private void modinfo_config_FileSelectCueComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (CurrentMod == null) return;

			// Invalid selection, somehow
			if (modinfo_config_FileSelectCueComboBox.SelectedIndex <= -1) return;

			string filePath = modinfo_config_FileSelectCueComboBox.Text;

			using (var sr = new StreamReader(CurrentMod.GetPathFull(filePath)))
			{
				modinfo_ConfigFCTB.Text = sr.ReadToEnd();
			}

			// Check if this file has values saved, and enable/disable load button
			bool exists = CurrentMod.GetSetting(filePath) != null;
			modinfo_config_LoadButton.Enabled = exists;
			modinfo_config_RemoveButton.Enabled = exists;
			modinfo_config_CompareButton.Enabled = exists;
		}

		private void modinfo_config_SaveButton_Click(object sender, EventArgs e)
		{
			// Get necessary data
			if (CurrentMod == null) return;
			
			string contents = modinfo_ConfigFCTB.Text;

			// If the data is invalid, just do nothing
			if (string.IsNullOrEmpty(contents)) return;
			
			if (CurrentMod.AddSetting(modinfo_config_FileSelectCueComboBox.Text, contents))
			{
				// For consistency enable the button
				modinfo_config_LoadButton.Enabled = true;
				modinfo_config_RemoveButton.Enabled = true;
				modinfo_config_CompareButton.Enabled = true;
			}
		}

		private void modinfo_config_LoadButton_Click(object sender, EventArgs e)
		{
			// Get necessary data
			if (CurrentMod == null) return;
			
			// If data is not valid
			var setting = CurrentMod.GetSetting(modinfo_config_FileSelectCueComboBox.Text);
			if (setting == null) return;

			modinfo_ConfigFCTB.Text = setting.Contents;
		}

		private void modinfo_config_RemoveButton_Click(object sender, EventArgs e)
		{
			// Get necessary data
			if (CurrentMod == null) return;
			
			if (CurrentMod.RemoveSetting(modinfo_config_FileSelectCueComboBox.Text))
			{
				// For consistency enable the button
				modinfo_config_LoadButton.Enabled = false;
				modinfo_config_RemoveButton.Enabled = false;
				modinfo_config_CompareButton.Enabled = false;
			}
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
				toolTip.SetToolTip(modinfo_config_ExpandButton, "Collapse the INI editor to normal size");
			}
			else
			{
				layout.Parent = modinfo_config_tab;
				layout.Dock = DockStyle.Fill;
				layout.BringToFront();
				fillPanel.Visible = false;
				fillPanel.SendToBack();
				modinfo_config_ExpandButton.Text = "Expand";
				toolTip.SetToolTip(modinfo_config_ExpandButton, "Expand the INI editor to fill the window");

			}
		}

		private void modinfo_ConfigFCTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			IniLanguage.Process(e);
		}

		private void modinfo_config_CompareButton_Click(object sender, EventArgs e)
		{
			string filepath = modinfo_config_FileSelectCueComboBox.Text;
			try
			{
				ConfigDiff.Instance.CompareStrings(CurrentMod.GetSetting(filepath).Contents, modinfo_ConfigFCTB.Text);
				ConfigDiff.Instance.Show();
			}
			catch (Exception configerror)
			{
				FlexibleMessageBox.Show("An exception occured. See error.log for additional details.");
				File.WriteAllText("error.log", configerror.Message + "\r\nStack:\r\n" + configerror.StackTrace);
			}
		}

		private void modinfo_info_DescriptionRichTextBox_TextChanged(object sender, EventArgs e)
		{
			var contents = modinfo_info_DescriptionRichTextBox.Text;
			if (!CurrentMod.Description.Equals(contents))
				CurrentMod.Description = contents;
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
	}
}