using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Steamworks;
using XCOM2Launcher.Helper;
using FilePath = System.IO.Path;

namespace XCOM2Launcher.Mod
{
    public class ModEntry
    {
        [JsonIgnore] public const string DEFAULT_AUTHOR_NAME = "Unknown";
        [JsonIgnore] public const string MODFILE_DISABLE_POSTFIX = "-disabled";

        [JsonIgnore] private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(ModEntry));

        [JsonIgnore] private string _image;

        [JsonIgnore] private IEnumerable<ModClassOverride> _overrides;

        /// <summary>
        ///     Index to determine mod load order
        /// </summary>
        [DefaultValue(-1)]
        public int Index { get; set; } = -1;

        /// <summary>
        /// This state is re-evaluated on each application start.
        /// </summary>
        [JsonIgnore]
        public ModState State { get; private set; } = ModState.None;
        
        /// <summary>
        /// The value of <see cref="State"/> is copied to this property when the settings are saved.
        /// This can be used to compare the re-evaluated mod state against its previous state on application start.
        /// </summary>
        public ModState PreviousState { get; set; } = ModState.None;

        public string ID { get; set; }
        public string Name { get; set; }
        public bool ManualName { get; set; } = false;

        public string Author { get; set; } = DEFAULT_AUTHOR_NAME;
	    public string Description { get; set; } = "";

	    public string Path { get; set; } = "";

        /// <summary>
        ///     Size in bytes
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

        /// <summary>
        /// Contains workshop id's from all mods, that this mod requires to run properly (as reported from workshop).
        /// </summary>
        public List<long> Dependencies { get; set; } = new List<long>();

        /// <summary>
        /// Contains workshop id's from mods, that the should be ignored during dependency checks (optional).
        /// </summary>
        public List<long> IgnoredDependencies { get; set; } = new List<long>();

        /// <summary>
        /// Contains the tags that were downloaded from steam.
        /// </summary>
        public List<string> SteamTags { get; set; } = new List<string>();

		[JsonIgnore]
	    public bool HasBackedUpSettings => Settings.Count > 0;

	    public List<ModSettingsEntry> Settings { get; set; } = new List<ModSettingsEntry>();

        [JsonIgnore]
        public string Image
        {
            get { return _image ?? FilePath.Combine(Path ?? "", "ModPreview.jpg"); }
            set { _image = value; }
        }

	    [JsonIgnore]
	    public string SteamLink => GetSteamLink();

	    [JsonIgnore]
	    public string BrowserLink => GetWorkshopLink();

        [Browsable(false)]
        public IList<string> Tags { get; set; } = new List<string>();

        public bool BuiltForWOTC { get; set; } = false;

        public ModEntry() {}

        public ModEntry(SteamUGCDetails_t workshopDetails)
        {
            if (workshopDetails.m_eResult != EResult.k_EResultOK)
            {
                return;
            }

            State = ModState.NotInstalled;
            WorkshopID = (long)workshopDetails.m_nPublishedFileId.m_PublishedFileId;
            Source = ModSource.SteamWorkshop;
            Name = workshopDetails.m_rgchTitle;
            Description = workshopDetails.m_rgchDescription;
        }

        public Classes.Mod.ModProperty GetProperty()
        {
            return new Classes.Mod.ModProperty(this);
        }

		#region Mod

		public string GetDescription(bool CleanBBCode = false)
		{
            string dsc;
            if (!string.IsNullOrEmpty(Description))
                dsc = Description;
            else
                dsc = new ModInfo(GetModInfoFile()).Description;

            if (CleanBBCode)
            {
                dsc = dsc.Replace(@"\", @"\'5c");
                dsc = dsc.Replace(@"{", @"\'7b");
                dsc = dsc.Replace(@"}", @"\'7d");
                Regex Regexp = new Regex(@"(?<!\\\\)\[(/?)(.*?)(?<!\\\\)\]");
                dsc = Regexp.Replace(dsc, RTFEvaluator);
                Regex replace_linebreaks = new Regex(@"[\r\n]{1,2}");
                dsc = replace_linebreaks.Replace(dsc, @"\line ");
                return @"{\rtf1\ansi " + dsc + "}";
            }
            return Description;
        }

        private string RTFEvaluator(Match match)
        {
            String output;
            if (match.Groups[2].Value.StartsWith("url"))
            {
                if (match.Groups[1].Value == "/")
                {
                    return @"}}}";
                }
                else if (match.Groups[2].Value.Length > 3 && match.Groups[2].Value[3] == '=')
                {
                    return @"{\field{\*\fldinst{HYPERLINK """ + match.Groups[2].Value.Substring(4) + @"""}}{\fldrslt{\ul ";
                }
                else
                {
                    return @"{{{";
                }
            }
            else
            {
                switch (match.Groups[2].Value)
                {
                    case "h1":
                        output = @"{\fs20 \b ";
                        break;
                    case "strike":
                        output = @"{\strike ";
                        break;
                    case "b":
                        output = @"{\b ";
                        break;
                    case "i":
                        output = @"{\i ";
                        break;
                    case "u":
                        output = @"{\ul ";
                        break;
                    default:
                        output = "";
                        break;
                }
            }
            if (output != "" && match.Groups[1].Value == "/")
                output = "}";
            return output;
        }

        public IEnumerable<ModClassOverride> GetOverrides(bool forceUpdate = false)
        {
            if (_overrides == null || forceUpdate)
            {
                _overrides = GetUIScreenListenerOverrides().Union(GetClassOverrides()).ToList();
            }
            return _overrides;
        }

        private IEnumerable<ModClassOverride> GetUIScreenListenerOverrides()
        {
            var sourceDirectory = FilePath.Combine(Path, "Src");
            var overrides = new List<ModClassOverride>();

            if (!Directory.Exists(sourceDirectory))
            {
                return overrides;
            }


            string[] sourceFiles;

            try
            {
                sourceFiles = Directory.GetFiles(sourceDirectory, "*.uc", SearchOption.AllDirectories);
            }
            catch (IOException ex)
            {
                // We expect IOException because it happened in rare cases (Issue #122).
                Log.Error("Unable to get files from directory: " + sourceDirectory, ex);
                return overrides;
            }

            Parallel.ForEach(sourceFiles, sourceFile =>
            {
                //The XComGame directory usually contains ALL the source files for the game.  Leaving it in is a common mistake.
                if (sourceFile.IndexOf(@"Src\XComGame", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return;
                }

                var screenClassRegex = new Regex(@"(?i)^\s*ScreenClass\s*=\s*(?:class')?([a-z_]+)");

                foreach (var line in File.ReadLines(sourceFile))
                {
                    var match = screenClassRegex.Match(line);
                    if (match.Success)
                    {
                        var oldClass = match.Groups[1].Value;
                        if (oldClass.ToLower() == "none")
                        {
                            //'ScreenClass = none' means it runs against every UI screen
                            continue;
                        }

                        var newClass = FilePath.GetFileNameWithoutExtension(sourceFile);
                        lock (overrides)
                        {
                            overrides.Add(new ModClassOverride(this, newClass, oldClass, ModClassOverrideType.UIScreenListener, line));
                        }
                    }
                }
            });

            return overrides;
        }

        private IEnumerable<ModClassOverride> GetClassOverrides()
        {
            var file = FilePath.Combine(Path, "Config", "XComEngine.ini");

            if (!File.Exists(file))
                return new ModClassOverride[0];

            var r = new Regex("^[+]?ModClassOverrides=\\(BaseGameClass=\"([^\"]+)\",ModClass=\"([^\"]+)\"\\)");

            return from line in File.ReadLines(file)
                select (l: line, match: r.Match(Regex.Replace(line, "\\s+", "")))
                into m
                where m.match.Success
                select new ModClassOverride(this, m.match.Groups[2].Value, m.match.Groups[1].Value, ModClassOverrideType.Class, m.l);
        }

        public void ShowOnSteam()
        {
            Tools.StartProcess("explorer", GetSteamLink());
        }

        public void ShowInBrowser()
        {
            Tools.StartProcess(GetWorkshopLink());
        }

        public void ShowInExplorer()
        {
            Tools.StartProcess("explorer", Path);
        }

        public string GetWorkshopLink()
        {
            if (WorkshopID > 0)
            {
                return "https://steamcommunity.com/sharedfiles/filedetails/?id=" + WorkshopID;
            }
            return "";
        }

	    public string GetSteamLink()
	    {
            if (WorkshopID > 0)
            {
                return "steam://url/CommunityFilePage/" + WorkshopID;
            }
            return "";
	    }

        public override string ToString()
        {
            return ID;
        }

        public bool IsInModPath(string modPath)
        {
            return string.Equals(modPath.TrimEnd('/', '\\'), FilePath.GetDirectoryName(Path), StringComparison.OrdinalIgnoreCase);
        }

        #endregion Mod

        #region Public Setters
        public void SetState(ModState newState)
        {
            State = newState;
        }

        public void AddState(ModState newState)
        {
            State |= newState;
        }

        public void RemoveState(ModState newState)
        {
            State &= ~newState;
        }

        public void SetRequiresWOTC(bool NeedWOTC)
        {
            BuiltForWOTC = NeedWOTC;
        }

        public void RealizeSize(long newSize)
        {
            Size = newSize;
        }

        public void SetSource(ModSource newSource)
        {
            Source = newSource;
        }

        #endregion


        #region Files

        public string[] GetConfigFiles()
        {
			if (Directory.Exists(FilePath.Combine(Path,"Config")))
				return Directory.GetFiles(FilePath.Combine(Path, "Config"), "*.ini", SearchOption.AllDirectories);
	        return new string[0];
        }

        public void EnableModFile()
        {
            string path = FilePath.Combine(Path, ID + ".XComMod" + MODFILE_DISABLE_POSTFIX);

            if (File.Exists(path))
            {
                try
                {
                    File.Move(path, path.Replace(MODFILE_DISABLE_POSTFIX, ""));
                }
                catch (Exception ex)
                {
                    Log.Warn($"Error renaming mod info file {ex.Message}");
                }
            }
        }

        public void DisableModFile()
        {
            string path = FilePath.Combine(Path, ID + ".XComMod");

            if (File.Exists(path))
            {
                try
                {
                    File.Move(path, path + MODFILE_DISABLE_POSTFIX);
                }
                catch (Exception ex)
                {
                    Log.Warn($"Error renaming mod info file {ex.Message}");
                }
            }
        }

        public bool CheckModFileDisabled()
        {
            return File.Exists(FilePath.Combine(Path, ID + ".XComMod" + MODFILE_DISABLE_POSTFIX));
        }

        internal string GetModInfoFile()
        {
            string path = FilePath.Combine(Path, ID + ".XComMod");
            
            if (File.Exists(path))
                return path;

            path = FilePath.Combine(Path, ID + ".XComMod" + MODFILE_DISABLE_POSTFIX);

            if (File.Exists(path))
                return path;

            return null;
        }

        public string GetReadMe()
        {
            try
            {
                var readmePathTxt = FilePath.Combine(Path, "ReadMe.txt");
                var readmePathMd = FilePath.Combine(Path, "ReadMe.md");

                if (File.Exists(readmePathTxt))
                {
                    return File.ReadAllText(readmePathTxt);
                }
                
                if (File.Exists(readmePathMd))
                {
                    return File.ReadAllText(readmePathMd);
                }

                return "No ReadMe found.";            }
            catch (Exception ex)
            {
                return "Unable to access ReadMe file - " + ex.Message;
            }
        }

		/// <summary>
		/// Returns a relative path to this mod's folder.
		/// </summary>
		/// <param name="relativeTo"></param>
		/// <returns></returns>
		public string GetPathRelative(string relativeTo)
		{
			Uri modPath = new Uri(relativeTo);
			Uri filePath = new Uri(Path);
			var relativePath = filePath.MakeRelativeUri(modPath).ToString();

			// Trim off the mod ID number, it's not useful here
			int i = relativePath.IndexOf("Config", StringComparison.Ordinal);
			relativePath = relativePath.Substring(i);

			return Uri.UnescapeDataString(relativePath);
		}

		/// <summary>
		/// Returns the full path to the provided relative path.
		/// </summary>
		/// <param name="relativePath"></param>
		/// <returns></returns>
	    public string GetPathFull(string relativePath)
	    {
		    return FilePath.Combine(Path, relativePath);
	    }

		#endregion Files


		#region Settings

		public bool AddSetting(string path, string contents)
	    {
			if (Settings == null) Settings = new List<ModSettingsEntry>();
			// Check if this belongs to this mod
			var fullpath = GetPathFull(path);
			if (!File.Exists(fullpath))
			{
				MessageBox.Show($"Error!\nThe file {path} does not belong to mod {Name}.\nNothing was saved.", @"Error", MessageBoxButtons.OK);
				return false;
			}

			var setting = GetSetting(path);
			if (setting == null)
			{
				setting = new ModSettingsEntry(path, FilePath.GetFileName(path), contents);
				Settings.Add(setting);
			}
			else
				setting.Contents = contents;

            try
            {
                using (var stream = new StreamWriter(fullpath))
                {
                    stream.Write(contents);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Warn("Error saving configuration file " + fullpath, ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;

	    }

	    public bool RemoveSetting(string path)
	    {
		    ModSettingsEntry toRemove = Settings.FirstOrDefault(setting => setting.FilePath.Equals(path));

		    if (toRemove == null) return false;
		    Settings.Remove(toRemove);
		    return true;
	    }

		/// <summary>
		/// Returns a setting given a fully qualified path to a file
		/// </summary>
		/// <param name="path">Path to the setting to retreive</param>
		/// <returns></returns>
	    public ModSettingsEntry GetSetting(string path)
		{
			if (Settings == null) Settings = new List<ModSettingsEntry>();
			return Settings.FirstOrDefault(setting => setting.FilePath.Equals(path));
		}

		#endregion Settings
	}
}
