using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XCOM2Launcher.Mod;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            try
            {
#endif
            if (!Steamworks.SteamAPI.Init())
                {
                    MessageBox.Show("Please start steam first!");
                    return;
                }
                // SteamWorkshop.StartCallbackService();


                Settings settings = initializeSettings();
                if (settings == null)
                    return;

                // clean up old files
                if (File.Exists(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak"))
                {
                    // Restore backup
                    File.Copy(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak", XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini", true);
                    File.Delete(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak");
                }

                Application.Run(new MainForm(settings));

                Steamworks.SteamAPI.Shutdown();
#if !DEBUG
            }
            catch (Exception e)
            {
                MessageBox.Show("An exception occured. See error.log for additional details.");
                File.WriteAllText("error.log", e.Message + "\r\nStack:\r\n" + e.StackTrace);
            }
#endif
        }

        public static Settings initializeSettings()
        {
            bool first_run = !File.Exists("settings.json");

            Settings settings;
            if (first_run)
                settings = new Settings();

            else try
                {
                    settings = Settings.fromFile("settings.json");
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    MessageBox.Show("settings.json could not be read.\r\nPlease delete or rename that file and try again.");
                    return null;
                }

            // Verify Game Path
            if (!Directory.Exists(settings.GamePath))
                settings.GamePath = XCOM2.DetectGameDir();

            if (settings.GamePath == "")
                MessageBox.Show("Could not find XCOM 2 installation path. Please fill it manually in the settings.");

            // Verify Mod Paths
            var oldPaths = settings.ModPaths.Where(modPath => !Directory.Exists(modPath)).ToList();
            foreach (string modPath in oldPaths)
                    settings.ModPaths.Remove(modPath);

            foreach (string modPath in XCOM2.DetectModDirs())
                if (!settings.ModPaths.Contains(modPath))
                    settings.ModPaths.Add(modPath);


            if (settings.ModPaths.Count == 0)
                MessageBox.Show("Could not find XCOM 2 mod directories. Please fill them in manually in the settings.");

            // Verify Mods 
            var brokenMods = settings.Mods.All.Where(m => !Directory.Exists(m.Path) || !File.Exists(m.getModInfoFile())).ToList();
            if (brokenMods.Count > 0)
            {
                MessageBox.Show($"{brokenMods.Count} mods no longer exists and have been removed:\r\n\r\n" + String.Join("\r\n", brokenMods.Select(m => m.Name)));

                foreach (ModEntry m in brokenMods)
                    settings.Mods.RemoveMod(m);
            }


            // import mods
            settings.ImportMods();

            return settings;
        }

    }
}
