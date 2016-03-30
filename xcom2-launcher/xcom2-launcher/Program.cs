using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Forms;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    internal static class Program
    {
        /// <summary>
        ///     Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            try
            {
#endif
            if (!SteamAPIWrapper.Init())
            {
                MessageBox.Show("Please start steam first!");
                return;
            }
            // SteamWorkshop.StartCallbackService();


            // Load settings
            var settings = InitializeSettings();
            if (settings == null)
                return;

#if !DEBUG
    // Check for update
            if (settings.CheckForUpdates)
            {
                try
                {
                    using (var client = new System.Net.WebClient())
                    {
                        client.Headers.Add("User-Agent: Other");
                        var json = client.DownloadString("https://api.github.com/repos/aEnigmatic/xcom2-launcher/releases/latest");
                        var release = Newtonsoft.Json.JsonConvert.DeserializeObject<GitHub.Release>(json);
                        var currentVersion = GetCurrentVersion();

                        if (currentVersion != release.tag_name)
                            // New version available
                            new UpdateAvailableDialog(release, currentVersion).ShowDialog();
                    }
                }
                catch (System.Net.WebException)
                {
                    // No internet?
                }
            }
#endif

            // clean up old files
            if (File.Exists(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak"))
            {
                // Restore backup
                File.Copy(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak", XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini", true);
                File.Delete(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak");
            }

            Application.Run(new MainForm(settings));

            SteamAPIWrapper.Shutdown();
#if !DEBUG
            }
            catch (Exception e)
            {
                MessageBox.Show("An exception occured. See error.log for additional details.");
                File.WriteAllText("error.log", e.Message + "\r\nStack:\r\n" + e.StackTrace);
            }
#endif
        }

        public static Settings InitializeSettings()
        {
            var firstRun = !File.Exists("settings.json");

            Settings settings;
            if (firstRun)
                settings = new Settings();

            else
                try
                {
                    settings = Settings.FromFile("settings.json");
                }
                catch (JsonSerializationException)
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
            foreach (var modPath in oldPaths)
                settings.ModPaths.Remove(modPath);

            foreach (var modPath in XCOM2.DetectModDirs())
                if (!settings.ModPaths.Contains(modPath))
                    settings.ModPaths.Add(modPath);


            if (settings.ModPaths.Count == 0)
                MessageBox.Show("Could not find XCOM 2 mod directories. Please fill them in manually in the settings.");

            // Verify categories
            var index = settings.Mods.Entries.Values.Max(c => c.Index);
            foreach (var cat in settings.Mods.Entries.Values.Where(c => c.Index == -1))
                cat.Index = ++index;

            // Verify Mods 
            var brokenMods = settings.Mods.All.Where(m => !Directory.Exists(m.Path) || !File.Exists(m.GetModInfoFile())).ToList();
            if (brokenMods.Count > 0)
            {
                MessageBox.Show($"{brokenMods.Count} mods no longer exists and have been removed:\r\n\r\n" + string.Join("\r\n", brokenMods.Select(m => m.Name)));

                foreach (var m in brokenMods)
                    settings.Mods.RemoveMod(m);
            }

            // import mods
            settings.ImportMods();

            return settings;
        }

        public static string GetCurrentVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fields = assembly.GetType("XCOM2Launcher.GitVersionInformation").GetFields();

            var major = fields.Single(f => f.Name == "Major").GetValue(null);
            var minor = fields.Single(f => f.Name == "Minor").GetValue(null);
            var patch = fields.Single(f => f.Name == "Patch").GetValue(null);


            return $"v{major}.{minor}.{patch}";
        }
    }
}