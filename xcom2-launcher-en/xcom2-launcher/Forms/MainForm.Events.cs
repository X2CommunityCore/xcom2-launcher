using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using JR.Utils.GUI.Forms;
using Steamworks;
using XCOM2Launcher.Classes;
using XCOM2Launcher.Helper;
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
            runXCOM2ToolStripMenuItem.Click += (a, b) => { RunVanilla(); };
            runWarOfTheChosenToolStripMenuItem.Click += (a, b) => { RunWotC(); };
            runChallengeModeToolStripMenuItem.Click += (a, b) => { RunChallengeMode(); };
            runChimeraSquadToolStripMenuItem.Click += (sender, args) => RunChimeraSquad();

            #region Menu->File

            saveToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->File->Save settings");
                Save(Settings.Instance.LastLaunchedWotC);
            };

            reloadToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->File->Reset settings");
                // Confirmation dialog
                var r = MessageBox.Show("Unsaved changes will be lost.\r\nAre you sure?", "Reload settings?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (r != DialogResult.OK)
                    return;

                Reset();
            };
            
            searchForModsToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->File->Search for new mods");

                if (IsModUpdateTaskRunning)
                {
                    ShowModUpdateRunningMessageBox();
                    return;
                }
                
                var importedMods = Settings.ImportMods();
                
                if (importedMods.Any())
                {
                    UpdateMods(importedMods, () =>
                    {
                        Invoke(new Action(() => 
                        {
                            modlist_ListObjectListView.EnsureModelVisible(importedMods.FirstOrDefault());
                        }));
                        
                        return Task.CompletedTask;
                    });
                }
            };

            updateEntriesToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->File->Update mod info");
                
                if (IsModUpdateTaskRunning)
                {
                    ShowModUpdateRunningMessageBox();
                    return;
                }
                
                SetStatus("Updating all mods...");

                var mods = Settings.Mods.All.ToList();
                UpdateMods(mods);
            };

            exitToolStripMenuItem.Click += (sender, e) =>
            {
                Log.Info("Menu->Close");
                Close();
            };

            #region Menu->File->Open Log
            amlLogFileToolStripMenuItem1.Click += delegate
            {
                Tools.StartProcess(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, "AML.log"));
            };

            x2LogFileToolStripMenuItem.Visible = Program.XEnv.Game == GameId.X2;
            x2LogFileToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is Xcom2Env env) {
                    Tools.StartProcess(env.LogFilePath);
                }
            };

            wotcLogFileToolStripMenuItem.Visible = Program.XEnv.Game == GameId.X2;
            wotcLogFileToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is Xcom2Env env) {
                    Tools.StartProcess(env.LogFilePathWotC);
                }
            };

            chimeraLogFileToolStripMenuItem.Visible = Program.XEnv.Game == GameId.ChimeraSquad;
            chimeraLogFileToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is XComChimeraSquadEnv env) {
                    Tools.StartProcess(env.LogFilePath);
                }
            };

            #endregion

            #region Menu->File->Open Folder

            folderToAmlToolStripMenuItem.Click += delegate
            {
                Tools.StartProcess("explorer", Path.GetDirectoryName(Application.ExecutablePath));
            };

            folderToX2InstallToolStripMenuItem.Visible = Program.XEnv.Game == GameId.X2;
            folderToX2InstallToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is Xcom2Env env)
                {
                    Tools.StartProcess("explorer", env.GameDir);
                }
            };

            folderToX2DataToolStripMenuItem.Visible = Program.XEnv.Game == GameId.X2;
            folderToX2DataToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is Xcom2Env env)
                {
                    Tools.StartProcess("explorer", env.DataDir);
                }
            };

            folderToWotcDataToolStripMenuItem.Visible = Program.XEnv.Game == GameId.X2;
            folderToWotcDataToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is Xcom2Env env)
                {
                    Tools.StartProcess("explorer", env.DataDirWotC);
                }
            };

            folderToChimeraInstallToolStripMenuItem.Visible = Program.XEnv.Game == GameId.ChimeraSquad;
            folderToChimeraInstallToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is XComChimeraSquadEnv env)
                {
                    Tools.StartProcess("explorer", env.GameDir);
                }
            };

            folderToChimeraDataToolStripMenuItem.Visible = Program.XEnv.Game == GameId.ChimeraSquad;
            folderToChimeraDataToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is XComChimeraSquadEnv env)
                {
                    Tools.StartProcess("explorer", env.DataDir);
                }
            };

            #endregion

            #endregion Menu->File

            #region Menu->Options

            // show hidden
            showHiddenModsToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->File->Update mod info");
                Settings.ShowHiddenElements = showHiddenModsToolStripMenuItem.Checked;
                olvcHidden.IsVisible = showHiddenModsToolStripMenuItem.Checked;
                RefreshModList(true);
            };

            // open Settings
            editOptionsToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->Options->Settings");
                using var dialog = new SettingsDialog(Settings);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // If the duplicate mod workaround is disabled, make sure that all mod info files are enabled.
                    if (!Settings.EnableDuplicateModIdWorkaround)
                    {
                        foreach (var mod in Mods.All)
                        {
                            mod.EnableModFile();
                        }

                        Mods.MarkDuplicates();
                    }

                    // refresh/update settings dependent functions
                    RefreshModList();
                    InitMainGui(Settings);
                    InitQuickArgumentsMenu(Settings);

                    if (dialog.IsRestartRequired)
                    {
                        appRestartPendingLabel.Visible = true;
                        MessageBox.Show("Some changes won't take effect, until after the application has been restarted.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            };

            manageCategoriesToolStripMenuItem.Click += ManageCategoriesToolStripMenuItem_Click;

            #endregion Menu->Options
            
            #region Menu->Tools

            // -> Tools
            cleanModsToolStripMenuItem.Click += delegate
            {
                using var dlg = new CleanModsForm(Settings);
                dlg.ShowDialog();
            };

            importFromXCOM2ToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is Xcom2Env x2Env)
                {
                    x2Env.UseWotC = false;
                    Log.Info("Menu->Tools->Import vanilla");
                    Program.XEnv.ImportActiveMods(Settings);
                    RefreshModList();
                }
            };

            importFromWotCToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv is Xcom2Env x2Env)
                {
                    x2Env.UseWotC = true;
                    Log.Info("Menu->Tools->Import WotC");
                    Program.XEnv.ImportActiveMods(Settings);
                    RefreshModList();
                }
            };

            importFromChimeraSquadToolStripMenuItem.Click += delegate
            {
                if (Program.XEnv.Game == GameId.ChimeraSquad)
                {
                    Log.Info("Menu->Tools->Import Chimera Squad");
                    Program.XEnv.ImportActiveMods(Settings);
                    RefreshModList();
                }
            };

            resubscribeToModsToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->Tools->Resubscribe");
                var modsToDownload = Mods.All.Where(m => m.State.HasFlag(ModState.NotInstalled) && m.Source == ModSource.SteamWorkshop).ToList();

                if (modsToDownload.Count == 0)
                {
                    MessageBox.Show("No uninstalled workshop mods were found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ResubscribeToMods(modsToDownload);
            };

            #endregion Menu->Tools

            #region Menu->About

            infoToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->About->About");
                using var about = new AboutBox();
                about.ShowDialog();
            };

            checkForUpdatesToolStripMenuItem.Click += delegate
            {
                Log.Info("Menu->About->Check Update");

                if (!Program.CheckForUpdate())
                {
                    MessageBox.Show("No updates available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            openHomepageToolStripMenuItem.Click += delegate { Tools.StartProcess(@"https://github.com/X2CommunityCore/xcom2-launcher"); };

            amlWikiToolStripMenuItem.Click += delegate { Tools.StartProcess(@"https://github.com/X2CommunityCore/xcom2-launcher/wiki"); };

            openDiscordToolStripMenuItem.Click += delegate { Tools.StartProcess(@"https://discord.gg/QHSVGRn"); };

            #endregion

            // RichTextBox clickable links
            //modinfo_readme_RichTextBox.LinkClicked += ControlLinkClicked;
            //modinfo_info_DescriptionRichTextBox.LinkClicked += ControlLinkClicked;
            //export_richtextbox.LinkClicked += ControlLinkClicked;
            //modinfo_changelog_richtextbox.LinkClicked += ControlLinkClicked;

            // Tab Controls
            //main_tabcontrol.Selected += MainTabSelected;
            //modinfo_tabcontrol.Selected += ModInfoTabSelected;

            // Steam Events
            Workshop.OnItemDownloaded += Resubscribe_OnItemDownloaded;

            // Main Tabs
            // Export
            export_workshop_link_checkbox.CheckedChanged += ExportCheckboxCheckedChanged;
            export_group_checkbox.CheckedChanged += ExportCheckboxCheckedChanged;
            export_save_button.Click += ExportSaveButtonClick;
            export_load_button.Click += ExportLoadButtonClick;

            horizontal_splitcontainer.Paint += (sender, args) =>
            {
                if (sender is SplitContainer s)
                {
                    // Make the "splitter" from the SplitContainer slightly visible.
                    args.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), s.SplitterRectangle);
                }
            };
        }

        private void QuickArgumentItemClick(object sender, EventArgs e) {
            // Add or remove the respective argument from the argument list, depending on its check-state.
            if (sender is ToolStripMenuItem item)
            {
                if (item.Checked)
                    Settings.AddArgument(item.Text);
                else
                    Settings.RemoveArgument(item.Text);
            }
        }

        private void ManageCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Info("Menu->Options->Categories");

            using var catManager = new CategoryManager(Settings);
            var result = catManager.ShowDialog();

            if (result == DialogResult.OK)
            {
                var collapsedGroups = modlist_ListObjectListView.CollapsedGroups.ToList();
                modlist_ListObjectListView.BeginUpdate();
                modlist_ListObjectListView.BuildGroups();
                // Restore previously collapsed groups
                collapsedGroups.ForEach(g => g.Collapsed = true);
                modlist_ListObjectListView.EndUpdate();
            }
        }

        private void Resubscribe_OnItemDownloaded(object sender, Workshop.DownloadItemEventArgs e)
        {
            var mod = Mods.All.SingleOrDefault(m => m.WorkshopID == (long)e.Result.m_nPublishedFileId.m_PublishedFileId);

            if (mod != null && (mod.State & ModState.NotInstalled) != ModState.None && e.Result.m_eResult == EResult.k_EResultOK)
            {
                mod.RemoveState(ModState.NotInstalled | ModState.Downloading);
                mod.isHidden = false;
                modlist_ListObjectListView.RefreshObject(mod);
            }
        }

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
            // If the mod update task is still running we cancel it and prevent the form from getting closed immediately
            // to prevent disposed resources getting accessed asynchronously (task cancellation does not happen instantly).
            if (IsModUpdateTaskRunning)
            {
                UseWaitCursor = true;
                Log.Info("Cancelled FormClosing because ModUpdateTask is still running");

                ModUpdateCancelSource?.Cancel();
                
                // We asynchronously wait (can't block UI thread at this point) for the ModUpdateTask to complete
                var waitForUpdateTaskToComplete = Task.Run(async () =>
                {
                    await ModUpdateTask;
                });
                
                // Close the form as soon as the ModUpdateTask finished
                waitForUpdateTaskToComplete.ContinueWith((x) =>
                {
                    Log.Info("Closing form (mod update task completed)");
                    Close();
                }, TaskScheduler.FromCurrentSynchronizationContext());

                // Prevent the user from any further interactions while waiting to close
                main_tabcontrol.Enabled = false;
                main_menustrip.Enabled = false;
                e.Cancel = true;
                return;
            }
            
            Log.Info("MainForm is about to close");

            // Save dimensions
            Settings.Windows["main"] = new WindowSettings(this) { Data = modlist_ListObjectListView.SaveState() };
            Settings.ShowStateFilter = cShowStateFilter.Checked;
            Settings.ShowModListGroups = cEnableGrouping.Checked;
            Settings.ShowPrimaryDuplicateAsDependency = cShowPrimaryDuplicates.Checked;

            Save(Settings.Instance.LastLaunchedWotC);
        }

        // Make sure property grid columns are properly sized
        private void modinfo_inspect_propertygrid_Layout(object sender, LayoutEventArgs e)
        {
            modinfo_inspect_propertygrid.SetLabelColumnWidth(100);
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
            using var dialog = new OpenFileDialog
            {
                Filter = "Text files|*.txt",
                DefaultExt = "txt",
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = false,
            };
            
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            bool OverrideTags;

            if (!Settings.NeverImportTags)
            {
                OverrideTags = MessageBox.Show("Do you want to override the tags and categories of your current mods with the tags saved in your profile?\r\n" +
                    "Warning: This action cannot be undone", "Importing profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }
            else
            {
                OverrideTags = false;
            }
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

                var entries = mods.Where(mod => mod.ID == modMatch.Groups["id"].Value).ToList();

                if (entries.Count == 0)
                {
                    // Mod missing
                    // -> add to list
                    missingMods.Add(modMatch);
                    continue;
                }

                activeMods.AddRange(entries);

                if (OverrideTags)
                {
                    var tags = modMatch.Groups["tags"].Value.Split(';').Where(t => !string.IsNullOrWhiteSpace(t)).ToList();

                    foreach (var tag in tags)
                    {
                        if (AvailableTags.ContainsKey(tag.ToLower()) == false)
                        {
                            AvailableTags[tag.ToLower()] = new ModTag(tag);
                        }
                    }

                    foreach (var modEntry in entries)
                    {
                        modEntry.Tags = tags;
                        Mods.RemoveMod(modEntry);
                        Mods.AddMod(categoryName, modEntry);
                    }
                }
            }

            // Check entries
            if (activeMods.Count == 0 && missingMods.Count == 0)
            {
                MessageBox.Show("No mods found. Bad profile?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                        MessageBox.Show("Done. Close the launcher, wait for steam to download the mod(s) and try again.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            modlist_ListObjectListView.RefreshObjects(mods);

            UpdateExport();
            UpdateLabels();
        }

        private void ExportSaveButtonClick(object sender, EventArgs eventArgs)
        {
            using var dialog = new SaveFileDialog
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
            UpdateModChangeLog(ModList.SelectedObject);
        }

        private async void UpdateModChangeLog(ModEntry m)
        {
            if (modinfo_tabcontrol.SelectedTab != modinfo_changelog_tab || m == null)
                return;

            modinfo_changelog_richtextbox.Text = "Loading...";
            modinfo_changelog_richtextbox.Text = await ModChangelogCache.GetChangeLogAsync(m.WorkshopID);
        }

        private void ControlLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Tools.StartProcess(e.LinkText);
        }

        private void filterMods_TextChanged(object sender, EventArgs e)
        {
            RefreshModelFilter();
        }

        private void cEnableGrouping_CheckedChanged(object sender, EventArgs e)
        {
            modlist_toggleGroupsButton.Enabled = cEnableGrouping.Checked;
            modlist_ListObjectListView.ShowGroups = cEnableGrouping.Checked && CheckIfGroupableColumn(modlist_ListObjectListView.LastSortColumn);
            modlist_ListObjectListView.BuildGroups();
        }

        private void cShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            pModsLegend.Visible = cShowStateFilter.Checked;
        }

        private void AdjustWidthComboBox_DropDown(object sender, EventArgs e)
        {
            var senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Font font = senderComboBox.Font;

            int vertScrollBarWidth = (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth
                : 0;

            var itemsList = senderComboBox.Items.Cast<object>().Select(item => item.ToString());

            foreach (string s in itemsList)
            {
                int newWidth;
                using (Graphics g = senderComboBox.CreateGraphics())
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
            if (CurrentMod == null)
                return;

            // Invalid selection, somehow
            if (modinfo_config_FileSelectCueComboBox.SelectedIndex <= -1)
                return;

            string filePath = modinfo_config_FileSelectCueComboBox.Text;
            bool exists = false;

            try
            {
                using (var sr = new StreamReader(CurrentMod.GetPathFull(filePath)))
                {
                    modinfo_ConfigFCTB.Text = sr.ReadToEnd();
                }

                // Check if this file has values saved, and enable/disable load button
                exists = CurrentMod.GetSetting(filePath) != null;
                modinfo_ConfigFCTB.ReadOnly = false;
            }
            catch (Exception ex)
            {
                Log.Warn("Failed to read selected ini file", ex);
                modinfo_ConfigFCTB.Text = ex.Message;
                modinfo_ConfigFCTB.ReadOnly = true;
            }

            modinfo_config_LoadButton.Enabled = exists;
            modinfo_config_RemoveButton.Enabled = exists;
            modinfo_config_CompareButton.Enabled = exists;
        }

        private void modinfo_config_SaveButton_Click(object sender, EventArgs e)
        {
            // Get necessary data
            if (CurrentMod == null)
                return;

            string contents = modinfo_ConfigFCTB.Text;

            // If the data is invalid, just do nothing
            if (string.IsNullOrEmpty(contents))
                return;

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

        private void modlist_toggleGroupsButton_Click(object sender, EventArgs e)
        {
            if (modlist_ListObjectListView.OLVGroups == null)
                return;

            var collapsedGroups = modlist_ListObjectListView.OLVGroups.Where(group => group.Collapsed).ToList();
            var expandedGroups = modlist_ListObjectListView.OLVGroups.Where(group => !group.Collapsed).ToList();

            if (collapsedGroups.Count == 0 || expandedGroups.Count == 0)
                modlist_ListObjectListView.OLVGroups.ToList().ForEach(g => g.Collapsed = !g.Collapsed);
            else if (collapsedGroups.Count > expandedGroups.Count)
                expandedGroups.ForEach(g => g.Collapsed = true);
            else
                collapsedGroups.ForEach(g => g.Collapsed = false);
        }

        private void modlist_filterClearButton_Click(object sender, EventArgs e)
        {
            modlist_FilterCueTextBox.Text = "";
        }

        private void cStateFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Font = checkBox.Checked ? new Font(checkBox.Font, FontStyle.Bold) : new Font(checkBox.Font, FontStyle.Regular);
            }

            RefreshModelFilter();
        }

        private void bRefreshStateFilter_Click(object sender, EventArgs e)
        {
            RefreshModelFilter();
        }

        private void bClearStateFilter_Click(object sender, EventArgs e)
        {
            cFilterConflicted.Checked = false;
            cFilterDuplicate.Checked = false;
            cFilterHidden.Checked = false;
            cFilterMissingDependency.Checked = false;
            cFilterNew.Checked = false;
            cFilterNotInstalled.Checked = false;
            cFilterNotLoaded.Checked = false;
        }

        private void cShowPrimaryDuplicates_CheckedChanged(object sender, EventArgs e)
        {
            if (modlist_ListObjectListView.SelectedObject is ModEntry mod)
            {
                UpdateDependencyInformation(mod);
            }
        }
        
        private void modInfoNotesText_TextChanged(object sender, EventArgs e)
        {
            if (CurrentMod != null)
            {
                CurrentMod.Note = modInfoNotesText.Text;
                modlist_ListObjectListView.RefreshObject(CurrentMod);
            }
        }

        #endregion
    }
}