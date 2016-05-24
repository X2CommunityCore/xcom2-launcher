using System;
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
        public const uint APPID = 268500;

        private static string _gameDir;

        public static string GameDir
        {
            get { return _gameDir ?? (_gameDir = DetectGameDir()); }
            set { _gameDir = value; }
        }

        public static string UserConfigDir
            => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\XCOM2\XComGame\Config";

        public static string DefaultConfigDir => Path.Combine(GameDir, @"XComGame\Config");

        public static string DetectGameDir()
        {
            // try steam
            string gamedir;
            if (SteamApps.GetAppInstallDir((AppId_t) APPID, out gamedir, 100) > 0 && Directory.Exists(gamedir))
            {
                _gameDir = gamedir;
                return gamedir;
            }

            // try modding dirs
            var dirs = DetectModDirs();
            foreach (var dir in dirs.Where(dir => dir.ToLower().Contains("\\steamapps\\")))
            {
                _gameDir = Path.GetFullPath(Path.Combine(dir, "../../..", "common", "XCOM 2"));
                return _gameDir;
            }

            // abandon hope
            return "";
        }

        public static void RunGame(string gameDir, string args)
        {
            if (!SteamAPIWrapper.Init())
                MessageBox.Show("Could not connect to steam.");


            var p = new Process
            {
                StartInfo =
                {
                    Arguments = args,
                    FileName = gameDir + @"\Binaries\Win64\XCom2.exe",
                    WorkingDirectory = gameDir
                }
            };

            p.Start();

            SteamAPIWrapper.Shutdown();
        }

        internal static void ImportActiveMods(Settings settings)
        {
            // load active mods
            foreach (var internalName in GetActiveMods())
                foreach (var mod in settings.Mods.All.Where(m => m.ID == internalName))
                    mod.isActive = true;
        }

        public static string[] DetectModDirs()
        {

            return
                new ConfigFile("Engine").Get("Engine.DownloadableContentEnumerator", "ModRootDirs")?
                    .Select(
                        path => Path.IsPathRooted(path)
                            ? path
                            : Path.GetFullPath(Path.Combine(GameDir, "bin", "Win64", path))
                    )
                    .Where(Directory.Exists)
                    .ToArray()
                ?? new string[0];
        }

        public static string[] GetActiveMods()
        {
            try
            {
                return new ConfigFile("ModOptions").Get("Engine.XComModOptions", "ActiveMods")?.ToArray() ?? new string[0];
            }
            catch (IOException)
            {
                return new string[0];
            }
        }

        public static void SaveChanges(Settings settings)
        {
            // XComModOptions
            var modOptions = new ConfigFile("ModOptions", false);

            foreach (var m in settings.Mods.Active.OrderBy(m => m.Index))
                modOptions.Add("Engine.XComModOptions", "ActiveMods", m.ID);

            modOptions.Save();

            // XComEngine
            var engine = new ConfigFile("Engine");

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