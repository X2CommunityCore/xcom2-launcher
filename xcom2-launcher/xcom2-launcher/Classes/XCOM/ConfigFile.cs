using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XCOM2Launcher.XCOM
{
    public class ConfigFile : IniFile
    {
        public ConfigFile(string filename, bool load = true) : base($"{XCOM2.UserConfigDir}/XCom{filename}.ini", false)
        {
            FileName = filename;
            DefaultFile = $"{XCOM2.DefaultConfigDir}/Default{FileName}.ini";

            if (load)
                Load();
        }

        public string FileName { get; }

        /// <summary>
        ///     Default{FileName}.ini from which this one is build
        /// </summary>
        public string DefaultFile { get; set; }

        public void UpdateTimestamp(string baseFile)
        {
            var numTimestamps = 0;
            if (Entries.ContainsKey("IniVersion"))
                numTimestamps = Entries["IniVersion"].Count;

            var newTimestamp = new DateTimeOffset(File.GetLastWriteTimeUtc(baseFile)).ToUnixTimeSeconds() + ".000000";

            Add("IniVersion", numTimestamps.ToString(), newTimestamp);
        }


        public new void Load()
        {
            if (File.Exists(Path))
            {
                Load(Path);
            }
            else
            {
                CreateFromDefault(Path);
                Save();
            }
        }

        public new void Load(string path)
        {
            base.Load(path);

            if (Entries.ContainsKey("IniVersion") && Entries["IniVersion"].Count > 0)
                Entries["IniVersion"].Remove(Entries["IniVersion"].Last().Key);
        }

        public new void Save()
        {
            UpdateTimestamp(DefaultFile);
            base.Save();
        }

        public void CreateFromDefault(string name)
        {
            Load(DefaultFile);

            if (!Has("Configuration", "BasedOn"))
                return;


            var baseFile = Get("Configuration", "BasedOn").First();
            Remove("Configuration", "BasedOn");

            var file = System.IO.Path.GetFullPath(System.IO.Path.Combine(XCOM2.GameDir, "XComGame", baseFile));

            // Create config from base baseFile
            var baseConfig = new IniFile(file, true);

            // Overwrite values
            foreach (var section in Entries)
                foreach (var entries in section.Value)
                {
                    var op = entries.Key[0];

                    switch (op)
                    {
                        case '+':
                        case '.':
                            foreach (var value in entries.Value)
                                baseConfig.Add(section.Key, entries.Key.Substring(1), value.Replace("%GAME%", "XCom"));
                            break;

                        case '-':
                            foreach (var value in entries.Value)
                                baseConfig.Remove(section.Key, entries.Key.Substring(1), value.Replace("%GAME%", "XCom"));
                            break;

                        case '!':
                            // !key=ClearArray
                            baseConfig.Remove(section.Key, entries.Key.Substring(1));
                            break;

                        case ';':
                            break;

                        default:
                            foreach (var value in entries.Value)
                                baseConfig.Set(section.Key, entries.Key, new List<string> { value });
                            break;
                    }
                }

            Entries.Clear();
            Entries = baseConfig.Entries;
            UpdateTimestamp(file);
        }
    }
}