using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using XCOM2Launcher.XCOM;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Serialization;

namespace XCOM2Launcher
{
    public class Settings
    {
        private string _gamePath;
        [Category("Paths")]
        [Description("Game Path (should be set automatically)")]
        public string GamePath
        {
            get { return _gamePath; }
            set
            {
                _gamePath = value;
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
        [Description("Check for updates on start-up?")]
        public bool CheckForUpdates { get; set; } = true;

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

            Mods.MarkDuplicates();
        }

        public string GetWorkshopPath()
        {
            return ModPaths.FirstOrDefault(modPath => modPath.IndexOf("steamapps\\workshop\\content\\268500\\", StringComparison.OrdinalIgnoreCase) != -1);
        }

        #region Serialization
        public static Settings FromFile(string file)
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
        public void SaveFile(string file)
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
