using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(Settings));
        [JsonIgnore]
        private static Settings _instance;
        [JsonIgnore]
        private string _gamePath;
        [JsonIgnore]
        private List<string> _argumentList = new List<string>();

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
                    Log.Warn("settings.json not found", e);
                    MessageBox.Show("Could not find file " + e.FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (JsonSerializationException ex)
                {
                    Log.Error("Unable to parse settings.json", ex);
                    MessageBox.Show(@"settings.json could not be read.\r\nPlease delete or rename that file and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return _instance;
            }
        }

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

        // Only required for backwards compatibility to preserve argument list from older versions.
        [Obsolete][JsonProperty]
        private string Arguments { get; set; } = "";

        /// <summary>
        /// List of XCOM Comamnd line arguments that will be used when the game is started.
        /// </summary>
        public ReadOnlyCollection<string> ArgumentList
        {
            get => _argumentList.AsReadOnly();
            set => _argumentList = value.ToList();
        }

        public bool CheckForUpdates { get; set; } = true;

        public bool CheckForPreReleaseUpdates { get; set; } = false;

        public bool ShowUpgradeWarning { get; set; } = true;

        public bool ShowHiddenElements { get; set; } = false;

        public bool CloseAfterLaunch { get; set; } = false;

        public bool AutoNumberIndexes { get; set; } = true;

        public bool UseSpecifiedCategories { get; set; } = true;

        public bool ShowQuickLaunchArguments { get; set; }

        public bool LastLaunchedWotC { get; set; } = false;

        public bool NeverImportTags { get; set; } = false;

        public bool AllowMultipleInstances { get; set; } = false;

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

            _argumentList.Add("-review");
            _argumentList.Add("-noRedscreens");
        }

        internal void ImportMods()
        {
            Mods.ImportMods(ModPaths);
        }

        public string GetWorkshopPath()
        {
            return ModPaths.FirstOrDefault(modPath => modPath.IndexOf("steamapps\\workshop\\content\\268500\\", StringComparison.OrdinalIgnoreCase) != -1);
        }

        /// <summary>
        /// Concatenates the command line arguments from the ArgumentList (separated by a space) and returns a single string.
        /// </summary>
        /// <returns>Concatenated command line arguments.</returns>
        public string GetArgumentString()
        {
            return string.Join(" ", _argumentList);
        }

        /// <summary>
        /// Adds the specified command line argument to the ArgumentList if it does not exists. 
        /// </summary>
        /// <param name="argument">Command line argument.</param>
        public void AddArgument(string argument)
        {
            // prevent duplicates
            if (!_argumentList.Any(arg => arg.Equals(argument, StringComparison.OrdinalIgnoreCase)))
                _argumentList.Add(argument);
        }

        /// <summary>
        /// Removes the specified command line argument from the ArgumentList if it exists. 
        /// </summary>
        /// <param name="argument">Command line argument.</param>
        public void RemoveArgument(string argument)
        {
            _argumentList.Remove(argument);
        }

        #region Serialization

        public static Settings FromFile(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException(file);

            Settings settings;

            using (var stream = File.OpenRead(file))
            {
                using (var reader = new StreamReader(stream))
                {
                    var serializer = new JsonSerializer();
                    serializer.Converters.Add(new ModListConverter());

                    settings = (Settings) serializer.Deserialize(reader, typeof(Settings));
                }
            }

            // for backwards compatibility, convert obsolete Arguments string to individual list entries and set it to ""
            #pragma warning disable 612     // disable Obsolete warning for Arguments property
            if (!string.IsNullOrEmpty(settings.Arguments))
            {
                settings._argumentList = settings.Arguments.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                settings.Arguments = "";
            }
            #pragma warning restore 612

            return settings;
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