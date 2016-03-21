using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM2Launcher.XCOM;
using XCOM2Launcher.Forms;
using System.Windows.Forms;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Serialization;

namespace XCOM2Launcher
{
    public class Settings
    {
        private string _game_path;
        [Category("Paths")]
        [Description("Game Path (should be set automatically)")]
        public string GamePath
        {
            get { return _game_path; }
            set
            {
                _game_path = value;
                XCOM2.GameDir = value;
            }
        }

        [Category("Paths")]
        [Description("Ignore for now")]
        public string ConfigPath { get; set; }

        [Category("Paths")]
        [Description("Ignore for now")]
        public string SavePath { get; set; }

        [Category("Paths")]
        [Description("Directories with Mod dirs in them")]
        public BindingList<string> ModPaths { get; set; } = new BindingList<string>();

        [Category("Game")]
        [Description("XCOM 2 commandline options")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Arguments Arguments { get; set; } = new Arguments();

        [Category("Launcher")]
        [Description("Display hidden elements in the list?")]
        public bool ShowHiddenElements { get; set; } = false;

        [Category("Launcher")]
        [Description("Close the launcher after starting XCOM2?")]
        public bool CloseAfterLaunch { get; set; } = false;

        [Category("Mods")]
        [Browsable(false)]
        public ModList Mods { get; set; } = new ModList();


        [Browsable(false)]
        public Dictionary<string, WindowSettings> Windows { get; set; } = new Dictionary<string, WindowSettings>();

        internal void ImportMods()
        {
            foreach (string dir in ModPaths)
                Mods.ImportMods(dir);
        }

        public string GetWorkshopPath()
        {
            foreach (string modPath in ModPaths)
            {
                if (modPath.IndexOf("steamapps\\workshop\\content\\268500\\", StringComparison.OrdinalIgnoreCase) != -1)
                    return modPath;
            }

            return null;
        }

        #region Serialization
        public static Settings fromFile(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException(file);


            using (FileStream stream = File.OpenRead(file))
            using (StreamReader reader = new StreamReader(stream))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new ModListConverter());

                return (Settings)serializer.Deserialize(reader, typeof(Settings));
            }
        }
        public void saveFile(string file)
        {
            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new List<JsonConverter> { new ModListConverter() }
            };

            File.WriteAllText(file, JsonConvert.SerializeObject(this, Formatting.Indented, settings));
        }
        #endregion
    }
}
