using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace XCOM2Launcher.Forms
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog(Settings settings)
        {
            InitializeComponent();

            //
            Settings = settings;

            // Restore states
            gamePathTextBox.Text = settings.GamePath;

            closeAfterLaunchCheckBox.Checked = settings.CloseAfterLaunch;
            searchForUpdatesCheckBox.Checked = settings.CheckForUpdates;
            showHiddenEntriesCheckBox.Checked = settings.ShowHiddenElements;

            foreach (var modPath in settings.ModPaths)
                modPathsListbox.Items.Add(modPath);

            argumentsTextBox.Text = settings.Arguments;
            argumentsTextBox.Values = new[]
            {
                "-Review",
                "-NoRedScreens",
                "-Log",
                "-CrashDumpWatcher",
                "-NoStartupMovies",
                "-Language=",
                "-AllowConsole",
                "-AutoDebug"
            };

            foreach (var cat in settings.Mods.Categories)
                categoriesListBox.Items.Add(cat);

            // Register Events
            Shown += SettingsDialog_Shown;
            FormClosing += SettingsDialog_FormClosing;

            browseGamePathButton.Click += BrowseGamePathButtonOnClick;

            addModPathButton.Click += AddModPathButtonOnClick;
            removeModPathButton.Click += RemoveModPathButtonOnClick;

            moveCategoryDownButton.Click += MoveCategoryDownButtonOnClick;
            moveCategoryUpButton.Click += MoveCategoryUpButtonOnClick;

            renameCategoryButton.Click += RenameCategoryButtonOnClick;
            removeCategoryButton.Click += RemoveCategoryButtonOnClick;
        }

        protected Settings Settings { get; set; }

        private void RemoveCategoryButtonOnClick(object sender, EventArgs eventArgs)
        {
            var index = categoriesListBox.SelectedIndex;
            if (index == -1)
                return;

            var category = (string) categoriesListBox.Items[index];

            if (MessageBox.Show($"Are you sure you want to remove the category '{category}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            var entry = Settings.Mods.Entries[category];
            foreach (var m in entry.Entries)
                Settings.Mods.AddMod("Unsorted", m);

            Settings.Mods.Entries.Remove(category);
            categoriesListBox.Items.RemoveAt(index);
        }

        private void RenameCategoryButtonOnClick(object sender, EventArgs eventArgs)
        {
            var index = categoriesListBox.SelectedIndex;
            if (index == -1)
                return;

            var oldName = (string) categoriesListBox.Items[index];
            var newName = Interaction.InputBox($"Enter the new name for the category '{oldName}'");

            categoriesListBox.Items[index] = newName;
            var entry = Settings.Mods.Entries[oldName];
            Settings.Mods.Entries.Remove(oldName);
            Settings.Mods.Entries.Add(newName, entry);
        }

        private void MoveCategoryUpButtonOnClick(object sender, EventArgs eventArgs)
        {
            var index = categoriesListBox.SelectedIndex;
            if (index == -1 || index == 0)
                return;


            // Update models
            var selectedKey = (string) categoriesListBox.SelectedItem;
            var selectedCat = Settings.Mods.Entries[selectedKey];
            var prevKey = (string) categoriesListBox.Items[index - 1];
            var prevCat = Settings.Mods.Entries[prevKey];

            var temp = selectedCat.Index;
            selectedCat.Index = prevCat.Index;
            prevCat.Index = temp;

            // Update Interface
            categoriesListBox.Items.RemoveAt(index);
            categoriesListBox.Items.Insert(index - 1, selectedKey);
            categoriesListBox.SelectedIndex = index - 1;
        }

        private void MoveCategoryDownButtonOnClick(object sender, EventArgs eventArgs)
        {
            var index = categoriesListBox.SelectedIndex;
            if (index == -1 || index == categoriesListBox.Items.Count - 1)
                return;

            // Update models
            var selectedKey = (string) categoriesListBox.SelectedItem;
            var selectedCat = Settings.Mods.Entries[selectedKey];
            var nextKey = (string) categoriesListBox.Items[index + 1];
            var nextCat = Settings.Mods.Entries[nextKey];

            var temp = selectedCat.Index;
            selectedCat.Index = nextCat.Index;
            nextCat.Index = temp;

            // Update Interface
            categoriesListBox.Items.RemoveAt(index);
            categoriesListBox.Items.Insert(index + 1, selectedKey);
            categoriesListBox.SelectedIndex = index + 1;
        }

        private void BrowseGamePathButtonOnClick(object sender, EventArgs eventArgs)
        {
            var dialog = new OpenFileDialog
            {
                FileName = "XCom2.exe",
                Filter = @"XCOM 2 Executable|XCom2.exe",
                RestoreDirectory = true,
                InitialDirectory = gamePathTextBox.Text
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var path = Path.GetFullPath(Path.Combine(dialog.FileName, "../../.."));
            gamePathTextBox.Text = path;
            Settings.GamePath = path;
        }

        private void RemoveModPathButtonOnClick(object sender, EventArgs e)
        {
            if (modPathsListbox.SelectedItem == null)
                return;

            var path = (string) modPathsListbox.SelectedItem;
            modPathsListbox.Items.Remove(path);
            Settings.ModPaths.Remove(path);
        }

        private void AddModPathButtonOnClick(object sender, EventArgs eventArgs)
        {
            var dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                RootFolder = Environment.SpecialFolder.MyComputer,
                Description = "Add a new mod path. Note: This should be the directory that contains the mod directories."
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            Settings.ModPaths.Add(dialog.SelectedPath);
            modPathsListbox.Items.Add(dialog.SelectedPath);
        }

        private void SettingsDialog_Shown(object sender, EventArgs e)
        {
            // if (Settings.Windows.ContainsKey("settings"))
            //     Bounds = Settings.Windows["settings"].Bounds;
        }

        private void SettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save states 
            Settings.GamePath = Path.GetFullPath(gamePathTextBox.Text);

            Settings.CloseAfterLaunch = closeAfterLaunchCheckBox.Checked;
            Settings.CheckForUpdates = searchForUpdatesCheckBox.Checked;
            Settings.ShowHiddenElements = showHiddenEntriesCheckBox.Checked;

            Settings.Arguments = argumentsTextBox.Text;

            // Save dimensions
            Settings.Windows["settings"] = new WindowSettings(this);
        }
    }
}