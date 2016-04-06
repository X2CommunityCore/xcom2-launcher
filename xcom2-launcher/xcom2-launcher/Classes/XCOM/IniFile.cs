using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XCOM2Launcher.XCOM
{
    public class IniFile
    {
        public IniFile(string path, bool load = false)
        {
            Path = path;
            
            if (load)
                Load();
        }

        public string Path { get; }


        public Dictionary<string, Dictionary<string, List<string>>> Entries { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();

        public void Load() => Load(Path);

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

                    // section header
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        currentSection = line.Substring(1, line.Length - 2).Trim();
                    }
                    else
                    {
                        // entries
                        var pos = line.IndexOf('=');

                        if (pos == -1)
                            // invalid syntax, previous line possibly missing \\
                            // -> skip
                            continue;

                        var currentKey = line.Substring(0, pos);
                        var currentValue = line.Substring(pos + 1);

                        if (currentKey.StartsWith(";"))
                            continue;


                        // multi line
                        while (currentValue.Length > 2 && currentValue.Substring(currentValue.Length - 2) == "\\\\")
                            currentValue = currentValue.Substring(0, currentValue.Length - 2) + "\n" + reader.ReadLine();

                        currentValue = currentValue.Replace("%GAME%", "XCom");

                        Add(currentSection, currentKey.TrimEnd(), currentValue.TrimStart());
                    }
                }
            }
        }


        public void Save()
        {
            //// Apply filters
            //if (Entries.ContainsKey("ConfigCoalesceFilter"))
            //{
            //    foreach (var section in Get("ConfigCoalesceFilter", "FilterSection"))
            //        Remove(section);

            //    Remove("ConfigCoalesceFilter", "FilterSection");
            //}

            using (var stream = new FileStream(Path, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                foreach (var section in Entries.Where(section => section.Value.Count > 0))
                {
                    writer.WriteLine($"[{section.Key}]");

                    foreach (var entry in section.Value)
                    {
                        foreach (var val in entry.Value)
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
                var newSection = new Dictionary<string, List<string>> { { key, value } };
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
                var newSection = new Dictionary<string, List<string>> { { key, new List<string> { value } } };
                Entries.Add(section, newSection);
            }
        }

        public List<string> Get(string section, string key)
        {
            if (!Has(section, key))
                return null;

            return Entries[section][key];
        }

        public bool Has(string section) => Entries.ContainsKey(section);
        public bool Has(string section, string key) => Has(section) && Entries[section].ContainsKey(key);


        public bool Remove(string section)
        {
            return Entries.Remove(section);
        }

        public bool Remove(string section, string key)
        {
            if (!Entries.ContainsKey(section) || !Entries[section].ContainsKey(key))
                return false;

            Entries[section][key].Clear();
            return true;
        }

        public bool Remove(string section, string key, string value)
        {
            if (!Entries.ContainsKey(section) || !Entries[section].ContainsKey(key))
                return false;

            var entry = Entries[section][key];
            var count = entry.Count;

            entry.RemoveAll(v => v == value);

            return entry.Count() < count;
        }
    }
}