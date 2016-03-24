using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XCOM2Launcher.PropertyGrid;

namespace XCOM2Launcher.Forms
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog(Settings settings)
        {
            InitializeComponent();
            //

            Settings = settings;
            propertyGrid1.SelectedObject = settings;


            propertyGrid1.ContextMenu = new ContextMenu();
            propertyGrid1.ContextMenu.MenuItems.Add("Reset", propertyGrid_reset_Click);
            propertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;

            Shown += SettingsDialog_Shown;
            FormClosing += SettingsDialog_FormClosing;
        }

        protected Settings Settings { get; set; }
        protected bool Changed { get; set; }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Changed = true;
        }

        private void SettingsDialog_Shown(object sender, EventArgs e)
        {
            if (Settings.Windows.ContainsKey("settings"))
                Bounds = Settings.Windows["settings"].Bounds;

        }

        private void SettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean bad mods
            var brokenMods = Settings.Mods.All.Where(m => !Directory.Exists(m.Path) || !File.Exists(m.GetModInfoFile())).ToList();
            foreach (var m in brokenMods)
            {
                MessageBox.Show($"Mod '{m.Name}' is invalid.");
                Settings.Mods.RemoveMod(m);
            }

            // Save dimensions
            Settings.Windows["settings"] = new WindowSettings(this);
        }

        private void propertyGrid_reset_Click(object sender, EventArgs e)
        {
            if (propertyGrid1.SelectedGridItem == null)
                return;

            propertyGrid1.ResetSelectedProperty();
        }

        private void propertyGrid1_Layout(object sender, LayoutEventArgs e)
        {
            propertyGrid1.SetLabelColumnWidth(170);
        }
    }
}