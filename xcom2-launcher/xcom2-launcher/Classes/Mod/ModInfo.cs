using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XCOM2Launcher.Mod
{
    public class ModInfo
    {
        public int publishedFileID { get; set; } = -1;
        public string Title { get; set; } = null;
        public string Category { get; set; } = "Unsorted";
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string ContentImage { get; set; } = "ModPreview.jpg";

        public ModInfo(string filepath)
        {
            loadFile(filepath);
        }

        protected void loadFile(string filepath)
        {
            if (!File.Exists(filepath))
                return;

            string[] keys = new string[] { "publishedfileid", "title", "category", "description", "tags", "contentimage" };
            Dictionary<string, string> values = new Dictionary<string, string>();

            using (FileStream stream = new FileStream(filepath, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                string key = null;
                string val;

                reader.ReadLine(); // skip [mod] line

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (key == null || line.Contains("="))
                    {
                        var data = line.Split(new char[] {'='}, 2);
                        string temp = data[0].Trim().ToLower();

                        if (key == null || keys.Contains(temp))
                        {
                            // probably right
                            key = temp;
                            val = data[1];

                            while (line.Last() == '\\')
                            {
                                // wow, someone knew what they were doing ?!?!
                                line = reader.ReadLine();
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
                    publishedFileID = int.Parse(values["publishedfileid"]);
            } catch (FormatException)
            {
                publishedFileID = -1;
            }

            if (values.ContainsKey("description"))
                Description = values["description"];

            if (values.ContainsKey("tags"))
                Tags = values["tags"];

            if (values.ContainsKey("contentimage"))
            {
                string val = values["contentimage"].Trim(new char[] { '\r', '\n', '\t', ' ' });
                // todo fix illegal chars?
                try
                {
                    if (val.Length > 0 && File.Exists(Path.Combine(Path.GetDirectoryName(filepath), val)))
                        ContentImage = values["contentimage"];
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
