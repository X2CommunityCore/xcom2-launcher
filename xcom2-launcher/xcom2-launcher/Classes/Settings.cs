using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using XCOM2Launcher.Helper;
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
                    _instance = FromFile("settings.json");
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

        /// <summary>
        /// Path to installation directory (i.e. ...\steamapps\common\XCOM 2)
        /// </summary>
        public string GamePath
        {
            get => _gamePath;
            set
            {
                _gamePath = value;
                Program.XEnv.GameDir = value;
            }
        }

        public GameId Game = GameId.X2;

        public List<string> ModPaths { get; set; } = new List<string>();

        // Only required for backwards compatibility to preserve argument list from older versions.
        [Obsolete][JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string Arguments { get; set; }

        /// <summary>
        /// List of XCOM Command line arguments that will be used when the game is started.
        /// </summary>
        public ReadOnlyCollection<string> ArgumentList
        {
            get => _argumentList.AsReadOnly();
            set => _argumentList = value.ToList();
        }

        /// <summary>
        /// List of XCOM Command line arguments that will be displayed in the quick toggle drop down.
        /// </summary>
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public List<string> QuickToggleArguments { get; set; } = new List<string>();

        public bool CheckForUpdates { get; set; } = true;
        public bool CheckForPreReleaseUpdates { get; set; }
        public bool IncludeAlphaVersions { get; set; }
        public bool ShowHiddenElements { get; set; }
        public bool ShowStateFilter { get; set; } = true;
        public bool UseTranslucentModListSelection { get; set; } = true;
        public bool ShowPrimaryDuplicateAsDependency { get; set; } = true;
        public bool ShowModListGroups { get; set; } = true;
        public bool CloseAfterLaunch { get; set; }
        public bool AutoNumberIndexes { get; set; } = true;
        public bool UseSpecifiedCategories { get; set; } = true;
        public bool ShowQuickLaunchArguments { get; set; }
        public bool LastLaunchedWotC { get; set; }
        public bool NeverImportTags { get; set; }
        public bool AllowMultipleInstances { get; set; }
        public bool EnableDuplicateModIdWorkaround { get; set; }
        public bool HideXcom2Button { get; set; } = true;
        public bool HideChallengeModeButton { get; set; } = true;
        public ModList Mods { get; set; } = new ModList();
        public Dictionary<string, ModTag> Tags { get; set; } = new Dictionary<string, ModTag>();
        public Dictionary<string, WindowSettings> Windows { get; set; } = new Dictionary<string, WindowSettings>();

        public Settings()
        {
            _instance = this;

            _argumentList.AddRange(Argument.DefaultArguments.Where(arg => arg.IsEnabledByDefault).Select(arg => arg.Parameter));
            QuickToggleArguments.AddRange(Argument.DefaultArguments.Where(arg => arg.IsDefaultQuickArg).Select(arg => arg.Parameter));
        }

        internal List<ModEntry> ImportMods()
        {
            return Mods.ImportMods(ModPaths);
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
                settings.Arguments = null;
            }
            #pragma warning restore 612

            return settings;
        }

        public void SaveFile(string file)
        {
            // Remember current state for next session.
            Mods?.All?.ToList().ForEach(mod => mod.PreviousState = mod.State);

            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new List<JsonConverter> {new ModListConverter()}
            };

            Tools.WriteTextToFileSave(file, JsonConvert.SerializeObject(this, Formatting.Indented, settings));
        }

        #endregion
    }
}