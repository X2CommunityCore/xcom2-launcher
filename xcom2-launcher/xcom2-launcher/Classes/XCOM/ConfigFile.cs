using System;
using System.Collections.Generic;
using System.IO;

namespace XCOM2Launcher.XCOM
{
    public class ConfigFile
    {
        public string FileName { get; }

        public string Path => $"{XCOM2.UserConfigDir}/XCom{FileName}.ini";

        /// <summary>
        /// Default{FileName}.ini from which this one is build
        /// </summary>
        public string DefaultFile { get; set; }


        public Dictionary<string, Dictionary<string, List<string>>> Entries { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();


        public ConfigFile(string filename, bool loadData = true)
        {
            FileName = filename;
            DefaultFile = $"{XCOM2.DefaultConfigDir}/Default{FileName}.ini";

            if (loadData)
                Load(Path);
        }

        public void Load(string file)
        {
            using (var stream = new FileStream(file, FileMode.Open))
            using (var reader = new StreamReader(stream))
            {
                var currentSection = "";
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.StartsWith("[") && line.EndsWith("]"))
                        currentSection = line.Substring(1, line.Length - 2);

                    else
                    {
                        var pos = line.IndexOf('=');

                        if (pos == -1)
                            // invalid syntax, previous line possibly missing \
                            // -> skip
                            continue;

                        var currentKey = line.Substring(0, pos);
                        var currentValue = line.Substring(pos + 1);

                        // multi line
                        while (currentValue.Length > 2 && currentValue.Substring(currentValue.Length - 2) == "\\\\")
                            currentValue = currentValue.Substring(0, currentValue.Length - 2) + "\n" + reader.ReadLine();

                        Add(currentSection, currentKey, currentValue);
                    }
                }
            }
        }



        public void Save()
        {
            using (FileStream stream = new FileStream(Path, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream))
            {

                foreach (KeyValuePair<string, Dictionary<string, List<string>>> section in Entries)
                {
                    //if (sectionEntry.Value.Count == 0)
                    //    continue;

                    writer.WriteLine($"[{section.Key}]");

                    foreach (KeyValuePair<string, List<string>> entry in section.Value)
                    {
                        foreach (string val in entry.Value)
                        {
                            writer.Write(entry.Key);
                            writer.Write("=");
                            writer.Write(val.Replace("\n", "\\\\\n"));
                            writer.WriteLine();
                        }
                    }

                    writer.WriteLine();
                }
            }
        }

        public void Set(string section, string key, List<string> value)
        {
            if (Entries.ContainsKey(section))
                // section exists
                if (Entries[section].ContainsKey(key))
                    Entries[section][key] = value;

                else
                    Entries[section].Add(key, value);

            else
            {
                var newSection = new Dictionary<string, List<string>> {{key, value}};
                Entries.Add(section, newSection);
            }
        }

        public void Add(string section, string key, string value)
        {
            if (Entries.ContainsKey(section))
                // section exists
                if (Entries[section].ContainsKey(key))
                    Entries[section][key].Add(value);

                else
                    Entries[section].Add(key, new List<string> { value });

            else
            {
                var newSection = new Dictionary<string, List<string>> {{key, new List<string> {value}}};
                Entries.Add(section, newSection);
            }
        }

        public List<string> Get(string section, string key)
        {
            try
            {
                return Entries[section][key];
            }
            catch
            {
                return null;
            }
        }

        private void Remove(string section)
        {
            Entries.Remove(section);
        }

        public bool Remove(string section, string key)
        {
            if (!Entries.ContainsKey(section) || !Entries[section].ContainsKey(key))
                return false;

            Entries[section][key].Clear();
            return true;
        }

        public void UpdateTimestamp()
        {
            var timestamp = (new DateTimeOffset(File.GetLastWriteTimeUtc(DefaultFile))).ToUnixTimeSeconds();

            Remove("IniVersion");
            Add("IniVersion", "0", timestamp + ".000000");
        }

    }
}
