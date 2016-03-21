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
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.Forms
{
    public partial class SettingsDialog : Form
    {
        protected Settings Settings { get; set; }
        protected bool Changed { get; set; } = false;

        public SettingsDialog(Settings settings)
        {
            InitializeComponent();
            //

            Settings = settings;
            propertyGrid1.SelectedObject = settings;


            propertyGrid1.ContextMenu = new ContextMenu();
            propertyGrid1.ContextMenu.MenuItems.Add("Reset", new EventHandler(propertyGrid_reset_Click));
            propertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;

            Shown += SettingsDialog_Shown;
            FormClosing += SettingsDialog_FormClosing;
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Changed = true;
        }

        private void SettingsDialog_Shown(object sender, EventArgs e)
        {
            if (Settings.Windows.ContainsKey("settings"))
                Bounds = Settings.Windows["settings"].Bounds;

            propertyGrid1.SetLabelColumnWidth(170);
        }

        private void SettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean bad mods
            var brokenMods = Settings.Mods.All.Where(m => !Directory.Exists(m.Path) || !File.Exists(m.getModInfoFile())).ToList();
            foreach (ModEntry m in brokenMods)
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
    }
}

