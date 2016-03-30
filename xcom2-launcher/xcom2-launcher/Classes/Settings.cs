using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Serialization;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    public class Settings
    {
        private string _gamePath;

        public string GamePath
        {
            get { return _gamePath; }
            set
            {
                _gamePath = value;
                XCOM2.GameDir = value;
            }
        }

        public string SavePath { get; set; }

        public List<string> ModPaths { get; set; } = new List<string>();

        public string Arguments { get; set; } = "-Review -NoRedscreens";

        public bool CheckForUpdates { get; set; } = true;

        public bool ShowHiddenElements { get; set; } = false;

        public bool CloseAfterLaunch { get; set; } = false;

        public ModList Mods { get; set; } = new ModList();

        public Dictionary<string, WindowSettings> Windows { get; set; } = new Dictionary<string, WindowSettings>();

        internal void ImportMods()
        {
            foreach (var dir in ModPaths)
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


            using (var stream = File.OpenRead(file))
            using (var reader = new StreamReader(stream))
            {
                var serializer = new JsonSerializer();
                serializer.Converters.Add(new ModListConverter());

                return (Settings) serializer.Deserialize(reader, typeof (Settings));
            }
        }

        public void SaveFile(string file)
        {
            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new List<JsonConverter> {new ModListConverter()}
            };

            File.WriteAllText(file, JsonConvert.SerializeObject(this, Formatting.Indented, settings));
        }

        #endregion
    }
}