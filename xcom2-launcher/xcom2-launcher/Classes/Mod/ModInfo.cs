using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace XCOM2Launcher.Mod
{
    public class ModInfo
    {
        public ModInfo(string filepath)
        {
            LoadFile(filepath);
        }

        public int PublishedFileID { get; set; } = -1;
        public string Title { get; set; }
        public string Category { get; set; } = "Unsorted";
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string ContentImage { get; set; } = "ModPreview.jpg";

        protected void LoadFile(string filepath)
        {
            if (!File.Exists(filepath))
                return;

            string[] keys = { "publishedfileid", "title", "category", "description", "tags", "contentimage" };
            var values = new Dictionary<string, string>();

            using (var stream = new FileStream(filepath, FileMode.Open))
            using (var reader = new StreamReader(stream))
            {
                string key = null;
                string val;

                reader.ReadLine(); // skip [mod] line

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    Contract.Assume(line != null);

                    if (key == null || line.Contains("="))
                    {
                        var data = line.Split(new[] { '=' }, 2);
                        var temp = data[0].Trim().ToLower();

                        if (key == null || keys.Contains(temp))
                        {
                            // probably right
                            key = temp;
                            val = data[1];

                            while (line.Last() == '\\')
                            {
                                // wow, someone knew what they were doing ?!?!
                                line = reader.ReadLine();
                                Contract.Assume(line != null);
                                val += "\r\n" + line;
                            }

                            if (values.ContainsKey(key))
                                values[key] = val;
                            else
                                values.Add(key, val);
                        }
                        else
                        {
                            // probably wrong
                            values[key] += "\r\n" + line;
                        }
                    }
                    else
                    {
                        // definitely wrong
                        values[key] += "\r\n" + line;
                    }
                }
            }


            Title = values["title"];

            if (values.ContainsKey("category") && values["category"].Length > 0)
                Category = values["category"];

            try
            {
                if (values.ContainsKey("publishedfileid"))
                    PublishedFileID = int.Parse(values["publishedfileid"]);
            }
            catch (FormatException)
            {
                PublishedFileID = -1;
            }

            if (values.ContainsKey("description"))
                Description = values["description"];

            if (values.ContainsKey("tags"))
                Tags = values["tags"];

            if (values.ContainsKey("contentimage"))
            {
                var val = values["contentimage"].Trim('\r', '\n', '\t', ' ');
                // todo fix illegal chars?
                try
                {
                    var path = Path.GetDirectoryName(filepath);
                    Contract.Assert(path != null);

                    if (val.Length > 0 && File.Exists(Path.Combine(path, val)))
                        ContentImage = values["contentimage"];
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}