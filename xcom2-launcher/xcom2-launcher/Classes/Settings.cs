using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Serialization;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    public class Settings
    {
		[JsonIgnore]
	    private static Settings _instance;

	    public static Settings Instance
	    {
		    get
			{
				if (_instance != null) return _instance;
				try
				{
					_instance = Settings.FromFile("settings.json");
				}
				catch (FileNotFoundException e)
				{
					MessageBox.Show("Could not find file " + e.FileName);
				}
				catch (JsonSerializationException)
				{
					MessageBox.Show(@"settings.json could not be read.\r\nPlease delete or rename that file and try again.");
					return null;
				}
				return _instance;
		    }
	    }

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

        public string Arguments { get; set; } = "-review -noRedscreens";

        public bool CheckForUpdates { get; set; } = true;

	    public bool ShowUpgradeWarning { get; set; } = true;

        public bool ShowHiddenElements { get; set; } = false;

        public bool CloseAfterLaunch { get; set; } = false;

	    public bool AutoNumberIndexes { get; set; } = true;

        public bool UseSpecifiedCategories { get; set; } = true;

        public bool LastLaunchedWotC { get; set; } = false;

        public ModList Mods { get; set; } = new ModList();

        public Dictionary<string, ModTag> Tags { get; set; } = new Dictionary<string, ModTag>();

		/// <summary>
		/// Mod ID
		/// </summary>
		//public Dictionary<string, ModSettingsList> ModSettings { get; set; } = new Dictionary<string, ModSettingsList>();

        public Dictionary<string, WindowSettings> Windows { get; set; } = new Dictionary<string, WindowSettings>();


	    public Settings()
	    {
		    _instance = this;
	    }


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