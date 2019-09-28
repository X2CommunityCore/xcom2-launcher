using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(ModInfo));
        public const string DEFAULT_CATEGORY_NAME = "Unsorted";

        public int PublishedFileID { get; set; } = -1;
        public string Title { get; set; }
        public string Category { get; set; } = DEFAULT_CATEGORY_NAME;
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public bool RequiresXPACK { get; set; }
        public string ContentImage { get; set; } = "ModPreview.jpg";

        protected void LoadFile(string filepath)
        {
            if (!File.Exists(filepath))
                return;

            string[] keys = { "publishedfileid", "title", "category", "description", "tags", "contentimage", "requiresxpack" };
            var values = new Dictionary<string, string>();

            using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
            {
                string key = null;

                reader.ReadLine(); // skip [mod] line

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    Contract.Assume(line != null);

                    // Expected format is "key=value"
                    if (key == null || line.Contains("="))
                    {
                        var data = line.Split(new[] { '=' }, 2);
                        var temp = data[0].Trim().ToLower();

                        if (key == null || keys.Contains(temp))
                        {
                            // probably right
                            key = temp;
                            var val = data[1];

                            // A multi-line value should be indicated by a trailing '\' (source?) but most of the time isn't !?
                            while (line.Last() == '\\')
                            {
                                line = reader.ReadLine();
                                Contract.Assume(line != null);
                                val += "\r\n" + line;
                            }

                            if (values.ContainsKey(key))
                            {
                                // This only happens if a tag appears multiple times.
                                values[key] = val;
                            }
                            else
                            {
                                values.Add(key, val);
                            }
                        }
                        else
                        {
                            // When the current line contains a '=' but has no valid key as prefix,
                            // it is assumed that it is part of the previous tag.
                            values[key] += "\r\n" + line;
                        }
                    }
                    else
                    {
                        // If the current line does not contain a '=', it is assumed that it is part of the previous tag.
                        // But only if the current value is not empty (i.e. there was content directly after the initial '=').
                        if (!string.IsNullOrEmpty(values[key]))
                            values[key] += "\r\n" + line;
                    }
                }
            }

            if (values.ContainsKey("title"))
                Title = values["title"];

            if (Settings.Instance.UseSpecifiedCategories && values.ContainsKey("category") && values["category"].Length > 0)
                Category = values["category"];

            if (values.ContainsKey("publishedfileid") && int.TryParse(values["publishedfileid"], out var publishId))
            {
                PublishedFileID = publishId;
            }
            else
            {
                var modFileContent = File.ReadAllText(filepath);
                Log.Error($"Error while parsing 'publishedfileid' field in '{filepath}'" + Environment.NewLine + modFileContent);
                PublishedFileID = -1;
            }

            if (values.ContainsKey("description"))
                Description = values["description"];

            if (values.ContainsKey("tags"))
                Tags = values["tags"];

            if (values.ContainsKey("requiresxpack"))
                RequiresXPACK = values["requiresxpack"].ToLower().Trim('\r', '\n', '\t', ' ') == "true";

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
                catch (Exception ex)
                {
                    var modFileContent = File.ReadAllText(filepath);
                    Log.Error($"Error while parsing 'contentimage' field in '{filepath}'" + Environment.NewLine + modFileContent, ex);
                    Debug.Fail(ex.Message);
                }
            }
        }
    }
}