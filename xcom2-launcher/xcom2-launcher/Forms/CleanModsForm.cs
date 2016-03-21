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
    public partial class CleanModsForm : Form
    {
        ModList Mods { get; set; }
        public CleanModsForm(Settings settings)
        {
            InitializeComponent();

            // 
            Mods = settings.Mods;

            // todo save cleaning settings?
            // Register Events
            button1.Click += onStartButtonClicked;
        }

        private void onStartButtonClicked(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?\r\nThis might cause problems and can not be undone.", "Confirm", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK)
                return;

            string source_mode = source_groupbox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;
            string shader_mode = shader_groupbox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            foreach (ModEntry m in Mods.All)
            {
                // Source Files
                if (hasSourceFiles(m))
                {
                    if (source_mode == "src_all_radiobutton")
                        deleteSourceFiles(m);

                    else if (source_mode == "src_xcomgame_radiobutton")
                        if (hasXComGameSourceFiles(m))
                            deleteXComGameSourceFiles(m);
                }

                // Shader Cache
                if (hasModShaderCache(m))
                {
                    if (shader_mode == "shadercache_all_radiobutton")
                        deleteModShaderCache(m);

                    else if (shader_mode == "shadercache_empty_radiobutton")
                        if (hasEmptyModShaderCache(m))
                            deleteModShaderCache(m);
                }
            }

            Close();
        }
        

        internal static bool hasModShaderCache(ModEntry m)
        {
            return File.Exists(Path.Combine(m.Path, "Content", m.ID + "_ModShaderCache.upk"));
        }

        internal static bool hasEmptyModShaderCache(ModEntry m)
        {
            string file = Path.Combine(m.Path, "Content", m.ID + "_ModShaderCache.upk");
            return File.Exists(file) && new FileInfo(file).Length == 371;
        }

        internal static void deleteModShaderCache(ModEntry m)
        {
            File.Delete(Path.Combine(m.Path, "Content", m.ID + "_ModShaderCache.upk"));
        }


        internal static bool hasSourceFiles(ModEntry m)
        {
            return Directory.Exists(Path.Combine(m.Path, "src"));
        }

        internal static bool hasXComGameSourceFiles(ModEntry m)
        {
            return Directory.Exists(Path.Combine(m.Path, "src", "XComGame"));
        }

        internal static void deleteSourceFiles(ModEntry m)
        {
            Directory.Delete(Path.Combine(m.Path, "src"), true);
        }

        internal static void deleteXComGameSourceFiles(ModEntry m)
        {
            Directory.Delete(Path.Combine(m.Path, "src", "XComGame"), true);
        }
    }
}