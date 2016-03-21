using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using FilePath = System.IO.Path;
using System.Linq;
using System.Text.RegularExpressions;
using XCOM2Launcher.Helper;
using System.ComponentModel;

namespace XCOM2Launcher.Mod
{
    public class ModEntry
    {
        /// <summary>
        /// Index to determine mod load order
        /// </summary>
        [DefaultValue(-1)]
        public int Index { get; set; } = -1;

        [JsonIgnore]
        public ModState State { get; set; } = ModState.None;
        public string ID { get; set; }
        public string Name { get; set; }
        public bool ManualName { get; set; } = false;

        public string Author { get; set; } = "Unknown";

        public string Path { get; set; }

        /// <summary>
        /// Size in bytes
        /// </summary>
        [DefaultValue(-1)]
        public long Size { get; set; } = -1;
        
        public bool isActive { get; set; } = false;
        public bool isHidden { get; set; } = false;

        public ModSource Source { get; set; } = ModSource.Unknown;

        [DefaultValue(-1)]
        public long WorkshopID { get; set; } = -1;

        public DateTime? DateAdded { get; set; } = null;
        public DateTime? DateCreated { get; set; } = null;
        public DateTime? DateUpdated { get; set; } = null;

        public string Note { get; set; } = null;

        [JsonIgnore]
        public string _image = null;
        [JsonIgnore]
        public string Image
        {
            get { return _image ?? FilePath.Combine(Path ?? "", "ModPreview.jpg"); }
            set { _image = value; }
        }

        public object Category { get; internal set; }


        #region Files
        public string[] getConfigFiles()
        {
            return Directory.GetFiles(FilePath.Combine(Path, "Config"), "*.ini");
        }
        internal string getModInfoFile()
        {
            return FilePath.Combine(Path, ID + ".XComMod");
        }
        public string GetReadMe()
        {
            try
            {
                return File.ReadAllText(FilePath.Combine(Path, "ReadMe.txt"));
            }
            catch (IOException)
            {
                return "No ReadMe found.";
            }
        }
        #endregion


        public string GetDescription()
        {
            ModInfo info = new ModInfo(getModInfoFile());

            return info.Description;
        }

        public ModClassOverride[] GetClassOverrides()
        {
            // string 
            string file = FilePath.Combine(Path, "Config", "XComEngine.ini");

            if (!File.Exists(file))
                return new ModClassOverride[0];


            List<ModClassOverride> list = new List<ModClassOverride>();

            Regex r = new Regex("^[+]?ModClassOverrides=\\(BaseGameClass=\"([^\"]+)\",ModClass=\"([^\"]+)\"\\)");

            foreach (string line in File.ReadLines(file))
            {
                Match m = r.Match(line.Replace(" ", ""));

                if (m.Success)
                {
                    list.Add(new ModClassOverride
                    {
                        OldClass = m.Groups[1].Value,
                        NewClass = m.Groups[2].Value
                    });
                }
            }

            return list.ToArray();
        }

        public void ShowOnSteam()
        {
            System.Diagnostics.Process.Start("explorer",  "steam://url/CommunityFilePage/" + WorkshopID);
        }
        public void ShowInExplorer()
        {
            System.Diagnostics.Process.Start("explorer", Path);
        }

        public string GetWorkshopLink()
        {
            return "https://steamcommunity.com/sharedfiles/filedetails/?id=" + WorkshopID;
        }

        public override string ToString()
        {
            return ID;
        }
    }
}