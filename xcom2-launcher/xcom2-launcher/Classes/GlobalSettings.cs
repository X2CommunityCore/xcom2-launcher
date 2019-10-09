using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sentry;

namespace XCOM2Launcher.Classes {
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class GlobalSettings {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(GlobalSettings));

        [JsonProperty] 
        public Version MaxVersion { get; set; }

        [JsonProperty]
        public bool IsSentryEnabled { get; set; }

        [JsonProperty]
        public string Guid { get; set; }

        [JsonProperty]
        public string UserName { get; set; }

        private const string FILE = @"AML\AMLSettings.json";
        private static GlobalSettings _Instance;
        private static readonly object _Lock = new object();

        private GlobalSettings() {
            MaxVersion = new Version(0, 0, 0);
            IsSentryEnabled = false;
            Guid = System.Guid.NewGuid().ToString();
            UserName = "";
        }

        /// <summary>
        /// Returns the actual instance.
        /// </summary>
        public static GlobalSettings Instance {
            get {
                lock (_Lock) {
                    if (_Instance == null) {
                        _Instance = Load();
                    }

                    return _Instance;
                }
            }
        }

        private static string GetFileLocation() {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fileLocation = Path.Combine(localAppDataPath, FILE);
            return fileLocation;
        }

        private static List<JsonConverter> GetRequiredConverters() {
            return new List<JsonConverter> {
                new VersionConverter()
            };
        }

        private static GlobalSettings Load() {
            Log.Debug("Initializing global settings");

            var fileLocation = GetFileLocation();
            GlobalSettings settings = null;

            if (File.Exists(fileLocation)) {
                Log.Debug($"Loading global settings from {fileLocation}.");

                try {
                    using (var stream = File.OpenRead(fileLocation)) {
                        using (var reader = new StreamReader(stream)) {
                            var serializer = new JsonSerializer();

                            foreach (var converter in GetRequiredConverters()) {
                                serializer.Converters.Add(converter);
                            }

                            settings = (GlobalSettings)serializer.Deserialize(reader, typeof(GlobalSettings));

                            if (settings == null) {
                                Log.Warn("Deserialization result was NULL.");
                            }
                        }
                    }
                } catch (JsonSerializationException ex) {
                    Log.Warn("Settings file seems to contain incompatible data.", ex);
                } catch (JsonReaderException ex) {
                    Log.Warn("Settings file format seems to be invalid.", ex);
                } catch (Exception ex) {
                    Log.Warn("Failed to deserialize settings file.", ex);
                }
            }

            if (settings == null) {
                Log.Info("Using default settings.");
                settings = new GlobalSettings();
            }

            return settings;
        }

        public void Save() {
            var fileLocation = GetFileLocation();
            Log.Debug($"Saving global settings to {fileLocation}.");

            var settings = new JsonSerializerSettings {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = GetRequiredConverters()
            };

            string dir = Path.GetDirectoryName(fileLocation);

            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(this, Formatting.Indented, settings));
        }
    }
}