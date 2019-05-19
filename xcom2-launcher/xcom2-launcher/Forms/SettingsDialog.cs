using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using XCOM2Launcher.XCOM;
using XCOM2Launcher.Mod;

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
	        autoNumberModIndexesCheckBox.Checked = settings.AutoNumberIndexes;
            useModSpecifiedCategoriesCheckBox.Checked = settings.UseSpecifiedCategories;
            ShowQuickLaunchArgumentsToggle.Checked = settings.ShowQuickLaunchArguments;

            foreach (var modPath in settings.ModPaths)
                modPathsListbox.Items.Add(modPath);

            argumentsTextBox.Text = settings.Arguments;

            foreach (var cat in settings.Mods.Categories)
                categoriesListBox.Items.Add(cat);

			// Create autofill values for arguments box
	        List<string> arguments = new List<string>();
	        foreach (var propertyInfo in typeof(Arguments).GetProperties())
	        {
		        var attrs = propertyInfo.GetCustomAttributes(true);
		        arguments.AddRange(
					from attrName in attrs.OfType<DisplayNameAttribute>()
					where !propertyInfo.Name.Equals("Custom")
					select attrName.DisplayName);
	        }

	        argumentsTextBox.Values = arguments.ToArray();



        }

        protected Settings Settings { get; set; }

		private void addCategoryButton_Click(object sender, EventArgs e)
		{
			var newName = Interaction.InputBox($"Please enter the name for new the category.", "New category", "");

			if (string.IsNullOrEmpty(newName))
				return;

			// If no category with the given name exists add it, otherweise select exiting entry.
			if (!Settings.Mods.Categories.Contains(newName)) {
				categoriesListBox.Items.Add(newName);
				categoriesListBox.SelectedItem = newName;

				Settings.Mods.Entries.Add(newName, new ModCategory());
			} else {
				categoriesListBox.SelectedItem = newName;
			}
		}

		private void RemoveCategoryButtonOnClick(object sender, EventArgs eventArgs)
        {
            var index = categoriesListBox.SelectedIndex;
            if (index == -1)
                return;

            var category = (string) categoriesListBox.Items[index];

			if (category == ModInfo.DEFAULT_CATEGORY_NAME) {
				MessageBox.Show($"Default category '{ModInfo.DEFAULT_CATEGORY_NAME}' can not be removed.", "Info");
				return;
			}

            if (MessageBox.Show($"Are you sure you want to remove the category '{category}'?", "Delete category", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            var entry = Settings.Mods.Entries[category];

			foreach (var m in entry.Entries)
                Settings.Mods.AddMod(ModInfo.DEFAULT_CATEGORY_NAME, m);

            Settings.Mods.Entries.Remove(category);
            categoriesListBox.Items.RemoveAt(index);
        }

        private void RenameCategoryButtonOnClick(object sender, EventArgs eventArgs)
        {
			RenameSelectedCategory();
		}

		private void categoriesListBox_DoubleClick(object sender, EventArgs e) 
		{
			RenameSelectedCategory();
		}

		private void RenameSelectedCategory() 
		{
			var index = categoriesListBox.SelectedIndex;
			if (index == -1)
				return;

			var oldName = (string)categoriesListBox.Items[index];
			var newName = Interaction.InputBox($"Enter the new name for the category '{oldName}'.", "Rename category", oldName);

			if (string.IsNullOrEmpty(newName))
				return;

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

            Settings.ModPaths.Add(dialog.SelectedPath + "\\");
            modPathsListbox.Items.Add(dialog.SelectedPath + "\\");
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
	        Settings.AutoNumberIndexes = autoNumberModIndexesCheckBox.Checked;
            Settings.UseSpecifiedCategories = useModSpecifiedCategoriesCheckBox.Checked;
            Settings.ShowQuickLaunchArguments = ShowQuickLaunchArgumentsToggle.Checked;

            Settings.Arguments = argumentsTextBox.Text;

            // Save dimensions
            Settings.Windows["settings"] = new WindowSettings(this);
        }
    }
}