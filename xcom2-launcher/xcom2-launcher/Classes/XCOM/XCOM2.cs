using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Steamworks;
using System.Windows.Forms;
using System.Linq;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.XCOM
{
    public static class XCOM2
    {
        public const uint APPID = 268500;

        private static string _game_dir = null;
        public static string GameDir
        {
            get
            {
                if (_game_dir == null)
                    _game_dir = DetectGameDir();

                return _game_dir;
            }

            set { _game_dir = value; }
        }

        public static string UserConfigDir
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\XCOM2\XComGame\Config";
            }
        }

        public static string DefaultConfigDir
        {
            get
            {
                // Todo improve
                return Path.Combine(GameDir, @"XComGame\Config");
            }
        }

        public static string DetectGameDir()
        {

            // try steam
            string gamedir;
            if (SteamApps.GetAppInstallDir((AppId_t)APPID, out gamedir, 100) > 0 && Directory.Exists(gamedir))
            {
                _game_dir = gamedir;
                return gamedir;
            }

            // try modding dirs
            string[] dirs = DetectModDirs();
            foreach (string dir in dirs)
                if (dir.ToLower().Contains("\\steamapps\\"))
                {
                    _game_dir = Path.GetFullPath(Path.Combine(dir, "../../..", "common", "XCOM 2"));
                    return _game_dir;
                }

            // abandon hope
            return "";
        }

        public static void RunGame(string game_dir, string args)
        {
            if (!SteamAPIWrapper.Init())
            {
                MessageBox.Show("Could not connect to steam.");
            }

            string bin_dir = game_dir + @"\Binaries\Win64";

            Process p = new Process();

            p.StartInfo.Arguments = args;
            p.StartInfo.FileName = bin_dir + @"\XCom2.exe";
            p.StartInfo.WorkingDirectory = game_dir;
            p.Start();

            SteamAPIWrapper.Shutdown();
        }

        internal static void ImportActiveMods(Settings settings)
        {
            // load active mods
            foreach (string internalName in XCOM2.getActiveMods())
                foreach (ModEntry mod in settings.Mods.All.Where(m => m.ID == internalName))
                    mod.isActive = true;
        }

        public static string[] DetectModDirs()
        {
            List<string> dirs = new List<string>();

            foreach (string line in File.ReadLines(Path.Combine(UserConfigDir, "XComEngine.ini")))
            {
                if (!line.StartsWith("ModRootDirs="))
                    continue;

                string dir = line.Substring(12);

                if (!Path.IsPathRooted(dir))
                    dir = Path.GetFullPath(Path.Combine(GameDir, "bin", "Win64", dir));

                if (Directory.Exists(dir))
                    dirs.Add(dir);
            }

            return dirs.ToArray();
        }

        public static string[] getActiveMods()
        {
            List<string> list = new List<string>();

            foreach (string line in File.ReadLines(Path.Combine(UserConfigDir, "XComModOptions.ini")))
            {
                if (!line.StartsWith("ActiveMods="))
                    continue;

                list.Add(line.Substring(11));
            }

            return list.ToArray();
        }

        public static void setActiveMods(List<ModEntry> mods)
        {
            string file;


            // XComModOptions
            file = Path.Combine(UserConfigDir, "XComModOptions.ini");

            if (!File.Exists(file + ".bak"))
                // create backup
                File.Copy(file, file + ".bak");

            ConfigFile ModOptions = new ConfigFile("ModOptions", false);

            foreach (ModEntry m in mods.OrderBy(m => m.Index))
                ModOptions.Add("Engine.XComModOptions", "ActiveMods", m.ID);

            ModOptions.UpdateTimestamp();
            ModOptions.Save();

            //// XComEngine
            file = Path.Combine(UserConfigDir, "XComEngine.ini");

            // Create back up
            string backup = file + ".bak";
            File.Copy(file, backup, true);

            // Write
            using (FileStream in_stream = new FileStream(backup, FileMode.Open))
            using (FileStream out_stream = new FileStream(file, FileMode.Truncate))
            using (StreamReader reader = new StreamReader(in_stream))
            using (StreamWriter writer = new StreamWriter(out_stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.StartsWith("+ModClassOverrides=") || line.StartsWith("ModClassOverrides="))
                        // Skip old mod entries
                        continue;

                    writer.WriteLine(line);
                }
            }
        }

    }
}
