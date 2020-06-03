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

            string[] validKeys = { "publishedfileid", "title", "category", "description", "tags", "contentimage", "requiresxpack" };
            var keyValPairs = new Dictionary<string, string>();

            using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
            {
                string currentKey = null;

                reader.ReadLine(); // skip [mod] line

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    Contract.Assume(line != null);

                    // Expected format is "key=value"
                    if (currentKey == null || line.Contains("="))
                    {
                        var keyValPair = line.Split(new[] { '=' }, 2);
                        var newKey = keyValPair[0].Trim().ToLower();

                        if (currentKey == null || validKeys.Contains(newKey))
                        {
                            // probably right
                            currentKey = newKey;
                            var currentValue = keyValPair[1];

                            // A multi-line value should be indicated by a trailing '\' (source?) but most of the time isn't !?
                            while (line.Last() == '\\')
                            {
                                line = reader.ReadLine();
                                Contract.Assume(line != null);
                                currentValue += "\r\n" + line;
                            }

                            // add or update key (if a key appears multiple times the last entry is effectively used)
                            keyValPairs[currentKey] = currentValue;
                        }
                        else
                        {
                            // When the current line contains a '=' but has no valid key as prefix,
                            // it is assumed that it is part of the previous tag.
                            keyValPairs[currentKey] += "\r\n" + line;
                        }
                    }
                    else
                    {
                        // If the current line does not contain a '=', it is assumed that it is part of the previous tag.
                        // But only if the current value is not empty (i.e. there was content directly after the initial '=').
                        if (!string.IsNullOrEmpty(keyValPairs[currentKey]))
                            keyValPairs[currentKey] += "\r\n" + line;
                    }
                }
            }

            if (keyValPairs.ContainsKey("title"))
                Title = keyValPairs["title"];

            if (Settings.Instance.UseSpecifiedCategories && keyValPairs.ContainsKey("category") && keyValPairs["category"].Length > 0)
                Category = keyValPairs["category"];

            if (keyValPairs.ContainsKey("publishedfileid") && !string.IsNullOrEmpty(keyValPairs["publishedfileid"]))
            {
                if (int.TryParse(keyValPairs["publishedfileid"], out var publishId))
                {
                    PublishedFileID = publishId;
                }
                else
                {
                    var modFileContent = File.ReadAllText(filepath);
                    Log.Error($"Error while parsing 'publishedfileid' field in '{filepath}'" + Environment.NewLine + modFileContent);
                    PublishedFileID = -1;
                }
            }
            else
            {
                Log.Warn($"Key 'publishedfileid' in '{filepath}' is missing or value is empty.");
                PublishedFileID = -1;
            }

            if (keyValPairs.ContainsKey("description"))
                Description = keyValPairs["description"];

            if (keyValPairs.ContainsKey("tags"))
                Tags = keyValPairs["tags"];

            if (keyValPairs.ContainsKey("requiresxpack"))
                RequiresXPACK = keyValPairs["requiresxpack"].ToLower().Trim('\r', '\n', '\t', ' ') == "true";

            if (keyValPairs.ContainsKey("contentimage"))
            {
                var val = keyValPairs["contentimage"].Trim('\r', '\n', '\t', ' ');
                // todo fix illegal chars?
                try
                {
                    var path = Path.GetDirectoryName(filepath);
                    Contract.Assert(path != null);

                    if (val.Length > 0 && File.Exists(Path.Combine(path, val)))
                        ContentImage = keyValPairs["contentimage"];
                }
                catch (Exception ex)
                {
                    var modFileContent = File.ReadAllText(filepath);
                    Log.Warn($"Error while parsing 'contentimage' field in '{filepath}'" + Environment.NewLine + modFileContent, ex);
                }
            }
        }
    }
}