using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM2Launcher.XCOM
{
    public class ConfigFile
    {
        public string FileName { get; private set; }

        public string Path { get { return $"{XCOM2.UserConfigDir}/XCom{FileName}.ini"; } }

        /// <summary>
        /// Default{FileName}.ini from which this one is build
        /// </summary>
        public string DefaultFile { get; set; }


        public Dictionary<string, Dictionary<string, List<string>>> Entries { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();


        public ConfigFile(string filename, bool loadData)
        {
            FileName = filename;
            DefaultFile = $"{XCOM2.DefaultConfigDir}/Default{FileName}.ini";

            if (loadData)
                Load(Path);
        }

        public void Load(string file)
        {
            using (FileStream stream = new FileStream(file, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                string line = "";
                string current_section = "";
                while (!reader.EndOfStream)
                {

                    line = reader.ReadLine().Trim();

                    if (line.Length == 0)
                        continue;

                    if (line.StartsWith("[") && line.EndsWith("]"))
                        current_section = line.Substring(1, line.Length - 2);

                    else
                    {
                        int pos = line.IndexOf('=');

                        if (pos == -1)
                            // invalid syntax, previous line possibly missing \
                            // -> skip
                            continue;

                        string current_key = line.Substring(0, pos);
                        string current_value = line.Substring(pos + 1);

                        // multi line
                        while (current_value.Length > 2 && current_value.Substring(current_value.Length - 2) == "\\\\")
                            current_value = current_value.Substring(0, current_value.Length - 2) + "\n" + reader.ReadLine();

                        Add(current_section, current_key, current_value);
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
                var newSection = new Dictionary<string, List<string>>();
                newSection.Add(key, value);
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
                var newSection = new Dictionary<string, List<string>>();
                newSection.Add(key, new List<string> { value });
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

        private void Remove(string section, string key)
        {
            Entries[section].Remove(key);
        }

        public void UpdateTimestamp()
        {
            long timestamp = (new DateTimeOffset(File.GetLastWriteTimeUtc(DefaultFile))).ToUnixTimeSeconds();

            Remove("IniVersion");
            Add("IniVersion", "0", timestamp + ".000000");
        }

    }
}
