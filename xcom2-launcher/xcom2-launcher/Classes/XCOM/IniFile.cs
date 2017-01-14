using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace XCOM2Launcher.XCOM
{

    public class IniFile
    {
		/// <summary>
		/// Gets an INI file from disk
		/// </summary>
		/// <param name="path">Path to file</param>
		/// <param name="load">Load the file on creation</param>
        public IniFile(string path, bool load = false)
        {
            Path = path;
            
            if (load)
                Load();
        }

	    /// <summary>
	    /// Convert a string to an INI file
	    /// </summary>
	    /// <param name="contents">String to convert</param>
	    public IniFile(string contents)
	    {
		    Load(contents);
	    }

	    public string Path { get; } = "";

		/// <summary>
		/// List of Sections, Keys, and Values contained within this INI file
		/// </summary>
        public Dictionary<string, Dictionary<string, List<string>>> Entries { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();

		/// <summary>
		/// Initiates loading of an INI from disk
		/// </summary>
        public void Load() => Load(Path);

        public void Load(string file)
        {
            using (var stream = File.Exists(file) ? new FileStream(file, FileMode.Open) : GenerateStreamFromString(file))
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
            // Create Dir
	        if (Path == "") return;
            var dir = System.IO.Path.GetDirectoryName(Path);
            Debug.Assert(dir != null, "dir != null");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Write File
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

		/// <summary>
		/// Sets the value list for the given key in the given section
		/// </summary>
		/// <param name="section">Section to look in</param>
		/// <param name="key">Key to look for</param>
		/// <param name="value">List of values to set for given key</param>
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

		/// <summary>
		/// Returns a key from a section
		/// </summary>
		/// <param name="section">Section to look in</param>
		/// <param name="key">Key to look for</param>
		/// <returns></returns>
		public List<string> Get(string section, string key)
		{
			if (!Has(section, key))
				return null;

			return Entries[section][key];
		}
		/// <summary>
		/// Adds a value to a given key in a given section. If the section or key do not exist, they will be created and the value set.
		/// </summary>
		/// <param name="section">Section to add to or create</param>
		/// <param name="key">Key to add to or create</param>
		/// <param name="value">Value to set</param>
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

        public bool Has(string section) => Entries.ContainsKey(section);
        public bool Has(string section, string key) => Has(section) && Entries[section].ContainsKey(key);

	    public bool Has(string section, string key, string value)
		    => Has(section, key) && Entries[section][key].Contains(value);

		/// <summary>
		/// Removes the section with the specified name.
		/// </summary>
		/// <param name="section">Name of the section to remove</param>
		/// <returns>Returns true if the section is removed</returns>
        public bool Remove(string section)
        {
            return Entries.Remove(section);
        }

		/// <summary>
		/// Removes all values associated with the given key in the given section
		/// </summary>
		/// <param name="section">Section to look in</param>
		/// <param name="key">Key to look for</param>
		/// <returns>Returns true if key is found</returns>
        public bool Remove(string section, string key)
        {
            if (!Entries.ContainsKey(section) || !Entries[section].ContainsKey(key))
                return false;

            Entries[section][key].Clear();
            return true;
        }

		/// <summary>
		/// Removes all given values from the given key in the given section
		/// </summary>
		/// <param name="section">Section to look in</param>
		/// <param name="key">Key to look for</param>
		/// <param name="value">Value to remove</param>
		/// <returns>Returns true if successfully removed any items</returns>
        public bool Remove(string section, string key, string value)
        {
            if (!Entries.ContainsKey(section) || !Entries[section].ContainsKey(key))
                return false;

            var entry = Entries[section][key];
            var count = entry.Count;

            entry.RemoveAll(v => v == value);

            return entry.Count() < count;
        }


		/// <summary>
		/// Create a stream from a provided string
		/// </summary>
		/// <param name="s">String to create a stream for</param>
		/// <returns>String as a stream</returns>
		private static Stream GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}