using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.Forms
{
    public partial class CategoryManager : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(CategoryManager));
        protected Settings Settings { get; set; }

        public CategoryManager(Settings settings)
        {
            InitializeComponent();

            Settings = settings;

            foreach (var cat in settings.Mods.Categories)
                categoriesListBox.Items.Add(cat);
        }

        private void AddCategoryButtonClick(object sender, EventArgs e)
        {
            var newName = Interaction.InputBox($"Please enter the name for new the category.", "New category", "");

            if (string.IsNullOrEmpty(newName))
                return;

            // If no category with the given name exists add it, otherweise select exiting entry.
            if (!Settings.Mods.Categories.Contains(newName))
            {
                Log.Info($"Adding category '{newName}'");

                categoriesListBox.Items.Add(newName);
                categoriesListBox.SelectedItem = newName;

                Settings.Mods.Entries.Add(newName, new ModCategory());
            }
            else
            {
                categoriesListBox.SelectedItem = newName;
            }
        }

        private void RemoveCategoryButtonOnClick(object sender, EventArgs eventArgs)
        {
            var index = categoriesListBox.SelectedIndex;
            if (index == -1)
                return;

            var category = (string) categoriesListBox.Items[index];

            if (category == ModInfo.DEFAULT_CATEGORY_NAME)
            {
                MessageBox.Show($"Default category '{ModInfo.DEFAULT_CATEGORY_NAME}' can not be removed.", "Info");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to remove the category '{category}'?", "Delete category", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            Log.Info($"Deleting category '{category}'");

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

        private void CategoriesListBoxDoubleClick(object sender, EventArgs e)
        {
            RenameSelectedCategory();
        }

        private void RenameSelectedCategory()
        {
            var index = categoriesListBox.SelectedIndex;
            if (index == -1)
                return;

            var oldName = (string) categoriesListBox.Items[index];

            if (oldName == ModInfo.DEFAULT_CATEGORY_NAME) {
                MessageBox.Show($"Default category '{ModInfo.DEFAULT_CATEGORY_NAME}' can not be renamed.", "Info");
                return;
            }

            var newName = Interaction.InputBox($"Enter the new name for the category '{oldName}'.", "Rename category", oldName);

            if (string.IsNullOrEmpty(newName))
                return;

            Log.Info($"Renaming category '{oldName}' to '{newName}'");

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
    }
}