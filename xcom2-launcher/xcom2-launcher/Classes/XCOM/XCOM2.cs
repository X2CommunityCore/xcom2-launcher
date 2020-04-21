using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Steamworks;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.XCOM
{
    public static class XCOM2
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(XCOM2));

        public const uint APPID = 268500;

        private static string _gameDir;

        public static string GameDir
        {
            get { return _gameDir ?? (_gameDir = DetectGameDir()); }
            set { _gameDir = value; }
        }

        public static string UserConfigDir
            => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\XCOM2\XComGame\Config";

        public static string WotCUserConfigDir
            => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            @"\my games\XCOM2 War of the Chosen\XComGame\Config";

        public static string DefaultConfigDir => Path.Combine(GameDir, @"XComGame\Config");

        public static string WotCDefaultConfigDir => Path.Combine(GameDir, @"XCom2-WarOfTheChosen\XComGame\Config");

        /// <summary>
        /// Tries to find the directory of the game and the mod folders, both workshop and default
        /// </summary>
        /// <returns></returns>
        public static string DetectGameDir()
        {
            // This works even if the application is not installed, based on where the game
            // would be installed with the default Steam library location.
            // Use 260 as max path length as this is the default windows limit.
            if (SteamApps.GetAppInstallDir((AppId_t)APPID, out var gamedir, 260) > 0)
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
            foreach (var dir in dirs.Where(dir => dir.ToLower().Contains("\\steamapps\\")))
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
        public static void RunGame(string gameDir, string args)
        {
            Log.Info("Starting XCOM 2 (vanilla)");

            if (!SteamAPIWrapper.Init())
                MessageBox.Show("Could not connect to steam.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var p = new Process
            {
                StartInfo =
                {
                    Arguments = args,
                    FileName = gameDir + @"\Binaries\Win64\XCom2.exe",
                    WorkingDirectory = gameDir
                }
            };

            try
            {
                p.Start();
            }
            catch (Win32Exception ex)
            {
                Log.Warn("Failed to start game process", ex);
                MessageBox.Show("An error occured while trying to run the game. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SteamAPIWrapper.Shutdown();
        }

        /// <summary>
        /// Runs War of the Chosen with the selected arguments
        /// </summary>
        /// <param name="gameDir"></param>
        /// <param name="args"></param>
        public static void RunWotC(string gameDir, string args)
        {
            Log.Info("Starting WotC");

            if (!SteamAPIWrapper.Init())
                MessageBox.Show("Could not connect to steam.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var p = new Process
            {
                StartInfo =
                {
                    Arguments = args,
                    FileName = gameDir + @"\XCom2-WarOfTheChosen\Binaries\Win64\XCom2.exe",
                    WorkingDirectory = gameDir + @"\XCom2-WarOfTheChosen"
                }
            };

            try
            {
                p.Start();
            }
            catch (Win32Exception ex)
            {
                Log.Warn("Failed to start game process", ex);
                MessageBox.Show("An error occured while trying to run the game. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SteamAPIWrapper.Shutdown();
        }

        internal static void ImportActiveMods(Settings settings, bool wotc)
        {
            var activeMods = GetActiveMods(wotc);

            // load active mods
            foreach (var internalName in activeMods)
                foreach (var mod in settings.Mods.All.Where(m => m.ID == internalName))
                    mod.isActive = true;
        }

        /// <summary>
        /// Reads the mod directories from the XComEngine.ini file.
        /// </summary>
        /// <returns>List of mod directories. NULL if the ini file is missing or couldn't be accessed.</returns>
        public static IEnumerable<string> DetectModDirs()
        {
            // Prevent stack overflow (Issue #19)
            if (_gameDir == null)
                return new string[0];

            List<string> currentModDirs;
            var validModDirs = new List<string>();

            try
            {
                currentModDirs = new DefaultConfigFile("Engine").Get("Engine.DownloadableContentEnumerator", "ModRootDirs");
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
          
            if (currentModDirs == null) {
                Log.Warn("No mod directories found in XComEngine.ini");
                return new string[0];
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
        /// Returns all mods, that are currently listed as "ActiveMods" from the XComModOptions.ini file.
        /// </summary>
        /// <returns></returns>
        public static string[] GetActiveMods(bool wotc)
        {
            try
            {
                return new DefaultConfigFile("ModOptions", wotc).Get("Engine.XComModOptions", "ActiveMods")?.ToArray() ?? new string[0];
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
        public static void SaveChanges(Settings settings, bool WotC, bool disableMods)
        {
            // XComModOptions
            var modOptions = new DefaultConfigFile("ModOptions", WotC, false);

            if (!disableMods)
            {
                foreach (var m in settings.Mods.Active.OrderBy(m => m.Index))
                    modOptions.Add("Engine.XComModOptions", "ActiveMods", m.ID);
            }

            modOptions.Save();

            // XComEngine
            var engine = new DefaultConfigFile("Engine", WotC);

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