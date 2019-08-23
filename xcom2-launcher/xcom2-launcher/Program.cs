using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Forms;
using XCOM2Launcher.Mod;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    internal static class Program
    {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        private static void Main()
        {
#if !DEBUG
            try
            {
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!CheckDotNet4_6()) {
                var result = MessageBox.Show("This program requires Microsoft .NET Framework v4.6 or newer. Do you want to open the download page now?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                    Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=56115");

                return;
            }

            if (!SteamAPIWrapper.Init()) {
                StringBuilder message = new StringBuilder();
                message.AppendLine("Please make sure that:");
                message.AppendLine("- Steam is running");
                message.AppendLine("- the file steam_appid.txt exists in the AML folder");
                message.AppendLine("- neither (or both) of Steam and AML are running\n  with admin privileges");
                MessageBox.Show(message.ToString(), "Error - unable to detect Steam!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Load settings
            var settings = InitializeSettings();
            if (settings == null)
                return;

#if !DEBUG
    // Check for update
            if (settings.CheckForUpdates)
            {
                CheckForUpdate();
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

        /// <summary>
        /// Checks if .Net Framework 4.6 or later installed.
        /// It verifies if the method DateTimeOffset.FromUnixTimeSeconds() (which was added with 4.6) is available.
        /// </summary>
        /// <returns>true if at least 4.6</returns>
        private static bool CheckDotNet4_6() {
            try {
                return typeof(DateTimeOffset).GetMethod("FromUnixTimeSeconds") != null;
            } catch (AmbiguousMatchException) {
                // ambiguous means there is more than one result
                return true;
            }
        }

        public static Settings InitializeSettings()
        {
            var firstRun = !File.Exists("settings.json");

            var settings = firstRun ? new Settings() : Settings.Instance;

            if (settings.ShowUpgradeWarning && !firstRun)
            {
                MessageBoxManager.Cancel = "Exit";
                MessageBoxManager.OK = "Continue";
                MessageBoxManager.Register();
                var choice = MessageBox.Show(
                    "WARNING!!\n\nThis launcher is NOT COMPATIBLE with the old 'settings.json' file.\nStop NOW and launch the old version to export a profile of your mods WITH GROUPS!\nOnce that is done, move the old 'settings.json' file to a SAFE PLACE and then proceed.\nAfter loading, import the profile you saved to recover groups.\n\nIf you are not ready to do this, click 'Exit' to leave with no changes.",
                    "WARNING!", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
                if (choice == DialogResult.Cancel) Environment.Exit(0);
                MessageBoxManager.Unregister();
            }

            settings.ShowUpgradeWarning = false;

            // Verify Game Path
            if (!Directory.Exists(settings.GamePath))
                settings.GamePath = XCOM2.DetectGameDir();

            if (settings.GamePath == "")
                MessageBox.Show(@"Could not find XCOM 2 installation path. Please fill it manually in the settings.");

            // Verify Mod Paths
            var pathsToEdit = settings.ModPaths.Where(m => !m.EndsWith("\\")).ToList();
            foreach (var modPath in pathsToEdit)
            {
                settings.ModPaths.Add(modPath + "\\");
                settings.ModPaths.Remove(modPath);
            }

            var oldPaths = settings.ModPaths.Where(modPath => !Directory.Exists(modPath)).ToList();
            foreach (var modPath in oldPaths)
                settings.ModPaths.Remove(modPath);

            foreach (var modPath in XCOM2.DetectModDirs())
            {
                if (!settings.ModPaths.Contains(modPath))
                {
                    if (!settings.ModPaths.Contains(modPath + "\\"))
                    {
                        settings.ModPaths.Add(modPath);
                    }
                }

            }


            if (settings.ModPaths.Count == 0)
                MessageBox.Show(@"Could not find XCOM 2 mod directories. Please fill them in manually in the settings.");

            if (settings.Mods.Entries.Count > 0)
            {
                // Verify categories
                var index = settings.Mods.Entries.Values.Max(c => c.Index);
                foreach (var cat in settings.Mods.Entries.Values.Where(c => c.Index == -1))
                    cat.Index = ++index;

                // Verify Mods 
                foreach (var mod in settings.Mods.All)
                {
                    if (!settings.ModPaths.Any(mod.IsInModPath))
                        mod.AddState(ModState.NotLoaded);

                    if (!Directory.Exists(mod.Path) || !File.Exists(mod.GetModInfoFile()))
                    {
                        mod.AddState(ModState.NotInstalled);
                    }
                    else if (!File.Exists(mod.GetModInfoFile()))
                    {
                        string newModInfo = settings.Mods.FindModInfo(mod.Path);
                        if (newModInfo != null)
                            mod.ID = Path.GetFileNameWithoutExtension(newModInfo);
                        else
                            mod.AddState(ModState.NotInstalled);
                    }

                    // tags clean up
                    mod.Tags = mod.Tags.Where(t => settings.Tags.ContainsKey(t.ToLower())).ToList();
                }

                var newlyBrokenMods = settings.Mods.All.Where(m => (m.State == ModState.NotLoaded || m.State == ModState.NotInstalled) && !m.isHidden).ToList();
                if (newlyBrokenMods.Count > 0)
                {
                    if (newlyBrokenMods.Count == 1)
                        FlexibleMessageBox.Show($"The mod '{newlyBrokenMods[0].Name}' no longer exists and has been hidden.");
                    else
                        FlexibleMessageBox.Show($"{newlyBrokenMods.Count} mods no longer exist and have been hidden:\r\n\r\n" + string.Join("\r\n", newlyBrokenMods.Select(m => m.Name)));

                    foreach (var m in newlyBrokenMods)
                        m.isHidden = true;
                }
            }

            // import mods
            settings.ImportMods();

            return settings;
        }

        public static bool CheckForUpdate()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Headers.Add("User-Agent: Other");
                    GitHub.Release release;

                    if (Settings.Instance.CheckForPreReleaseUpdates)
                    {
                        // fetch all releases including pre-releases and select the first/newest 
                        var jsonAllReleases = client.DownloadString("https://api.github.com/repos/X2CommunityCore/xcom2-launcher/releases");
                        var allReleases = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GitHub.Release>>(jsonAllReleases);
                        release = allReleases.FirstOrDefault();
                    }
                    else
                    {
                        // fetch latest non-pre-release
                        var json = client.DownloadString("https://api.github.com/repos/X2CommunityCore/xcom2-launcher/releases/latest");
                        release = Newtonsoft.Json.JsonConvert.DeserializeObject<GitHub.Release>(json);
                    }

                    if (release == null)
                        return false;

                    var regexVersionNumber = new Regex("[^0-9.]");

                    string currentVersionString = GetCurrentVersion();
                    string releaseVersionString = release.tag_name;
                    Version.TryParse(regexVersionNumber.Replace(currentVersionString, ""), out Version currentVersion);
                    Version.TryParse(regexVersionNumber.Replace(releaseVersionString, ""), out Version newVersion);

                    if (currentVersion != null && newVersion != null)
                    {
                        if (currentVersion.CompareTo(newVersion) < 0)
                        {
                            // New version available
                            new UpdateAvailableDialog(release, currentVersion, newVersion).ShowDialog();
                            return true;
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"{nameof(CheckForUpdate)}: Error parsing version information '{currentVersion}'/'{releaseVersionString}'.");
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Debug.WriteLine(nameof(CheckForUpdate) + ": " + ex.Message);
            }

            return false;
		}

        /// <summary>
        /// Returns versions information that was generated by GitVersionTask
        /// </summary>
        /// <returns></returns>
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