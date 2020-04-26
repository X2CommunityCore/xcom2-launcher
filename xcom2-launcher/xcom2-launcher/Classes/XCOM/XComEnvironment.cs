using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Steamworks;

namespace XCOM2Launcher.XCOM
{
    public enum GameId
    {
        X2 = 268500,
        ChimeraSquad = 882100
    }

    abstract class XcomEnvironment
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(XcomEnvironment));

        public GameId Game { get; }
        public uint SteamAppId => (uint)Game;
        private string _gameDir;

        public string GameDir
        {
            get { return _gameDir ?? (_gameDir = DetectGameDir()); }
            set { _gameDir = value; }
        }

        protected XcomEnvironment(GameId game)
        {
            Game = game;
        }

        public abstract string DefaultConfigDir { get; }

        protected abstract DefaultConfigFile GetConfigFile(string file, bool load = true);

        /// <summary>
        /// Tries to find the directory of the game and the mod folders, both workshop and default
        /// </summary>
        /// <returns></returns>
        public string DetectGameDir()
        {
            // This works even if the application is not installed, based on where the game
            // would be installed with the default Steam library location.
            // Use 260 as max path length as this is the default windows limit.
            if (SteamApps.GetAppInstallDir((AppId_t)SteamAppId, out var gamedir, 260) > 0)
            {
                // Check if game is really available/installed
                if (Directory.Exists(gamedir))
                {
                    Log.Info("Game directory detected using Steam " + gamedir);
                    _gameDir = gamedir;
                    return gamedir;
                }

                Log.Warn("Steam returned the (default) installation directory, but the game is probably not installed.");
            }

            Log.Error("Steam API failed to detect game directory.");
            
            // Try to deduce game path from available mod directories
            var dirs = DetectModDirs();
            foreach (var dir in Enumerable.Where<string>(dirs, dir => dir.ToLower().Contains("\\steamapps\\")))
            {
                // Assume steamapps folder to have \workshop\content\268500 subfolders
                gamedir = Path.GetFullPath(Path.Combine(dir, "../../..", "common", "XCOM 2"));

                if (Directory.Exists(gamedir))
                {
                    Log.Warn("Game directory detected from modding directory " + _gameDir);
                    _gameDir = gamedir;
                    return gamedir;
                }
            }

            Log.Error("Unable to detect game directory");
            return "";
        }

        /// <summary>
        /// Runs the game with the selected arguments
        /// </summary>
        /// <param name="gameDir"></param>
        /// <param name="args"></param>
        public abstract void RunGame(string gameDir, string args);

        internal void ImportActiveMods(Settings settings)
        {
            var activeMods = GetActiveMods();

            // load active mods
            foreach (var internalName in activeMods)
                foreach (var mod in settings.Mods.All.Where(m => m.ID == internalName))
                    mod.isActive = true;
        }

        /// <summary>
        /// Reads the mod directories from the XComEngine.ini file.
        /// </summary>
        /// <returns>List of mod directories. NULL if the ini file is missing or couldn't be accessed.</returns>
        public IEnumerable<string> DetectModDirs()
        {
            // Prevent stack overflow (Issue #19)
            if (_gameDir == null)
                return new string[0];

            List<string> currentModDirs;
            var validModDirs = new List<string>();

            try
            {
                currentModDirs = GetConfigFile("Engine").Get("Engine.DownloadableContentEnumerator", "ModRootDirs");
            }
            catch (IOException ex)
            {
                Log.Warn("Unable to access 'XComEngine.ini'", ex);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Warn("Unable to access 'XComEngine.ini'", ex);
                return null;
            }
          
            // Add default Steam Workshop mod path if it is missing.
            var appId = SteamAppId.ToString();
            if (!currentModDirs.Any(dir => dir.EndsWith(appId) || dir.EndsWith(appId + "\\")))
            {
                var workShopModPath = Path.GetFullPath(Path.Combine(_gameDir, "../..", "workshop", "content", appId));
                currentModDirs.Add(workShopModPath);
                Log.Info("Added default Steam Workshop mod directory: " + workShopModPath);
            }

            foreach (var modDir in currentModDirs)
            {
                try
                {
                    string dir;

                    if (Path.IsPathRooted(modDir))
                    {
                        // make sure all directories end with '\' (can only happen if someone adds a dir manually?)
                        dir = modDir.EndsWith(@"\") ? modDir : modDir + @"\";
                    }
                    else
                    {
                        dir = Path.GetFullPath(Path.Combine(GameDir, "bin", "Win64", modDir));
                        Log.Debug($"Changed non rooted mod directory from '{modDir}' to '{dir}'");
                    }

                    if (Directory.Exists(dir))
                        validModDirs.Add(dir);
                }
                catch (ArgumentException ex)
                {
                    Log.Error($"Invalid mod directory '{modDir}'", ex);
                }
            }

            return validModDirs;
        }

        /// <summary>
        /// Returns all mods, that are currently listed as "ActiveMods" in the XComModOptions.ini and the DefaultModOptions.ini files.
        /// </summary>
        /// <returns></returns>
        public string[] GetActiveMods()
        {
            try
            {
                // Retrieve entires from XComModOptions
                var configFile = GetConfigFile("ModOptions");
                var mods = configFile.Get("Engine.XComModOptions", "ActiveMods") ?? new List<string>();
                
                // Retrieve entires from DefaultModOptions
                configFile.Entries.Clear();
                configFile.CreateFromDefault("ModOptions");
                mods.AddRange(configFile.Get("Engine.XComModOptions", "ActiveMods")?.ToArray() ?? new string[0]);
                
                // Prepare result
                mods = mods.Distinct().ToList();
                mods = mods.ConvertAll(x => x.TrimStart('"').TrimEnd('"'));     // default config may contain entries encapsulated in ""
                return mods.ToArray();
            }
            catch (IOException ex)
            {
                Log.Error("Unable to access XComModOptions.ini", ex);
                Debug.Fail(ex.Message);
                return new string[0];
            }
        }

        /// <summary>
        /// Updates the XComModOptions.ini and XComEngine.ini according
        /// to currently enabled mods and configured mod directories.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="WotC"></param>
        /// <param name="disableMods"></param>
        public void SaveChanges(Settings settings, bool disableMods)
        {
            // XComModOptions
            var modOptions = GetConfigFile("ModOptions", false);

            if (!disableMods)
            {
                foreach (var m in settings.Mods.Active.OrderBy(m => m.Index))
                    modOptions.Add("Engine.XComModOptions", "ActiveMods", m.ID);
            }

            modOptions.Save();

            // XComEngine
            var engine = GetConfigFile("Engine", false);

            // Remove old ModClassOverrides
            engine.Remove("Engine.Engine", "ModClassOverrides");

            // Set Mod Paths
            engine.Remove("Engine.DownloadableContentEnumerator", "ModRootDirs");
            foreach (var modPath in settings.ModPaths)
                engine.Add("Engine.DownloadableContentEnumerator", "ModRootDirs", modPath);

            // Save
            engine.Save();
        }
    }
}