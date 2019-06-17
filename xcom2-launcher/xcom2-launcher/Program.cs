using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using Sentry;
using Sentry.Protocol;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Forms;
using XCOM2Launcher.Mod;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    internal static class Program
    {
        public static bool IsDebugBuild;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            #if DEBUG
                IsDebugBuild = true;
            #else
                IsDebugBuild = false;

                // Capture all unhandled Exceptions
                AppDomain.CurrentDomain.UnhandledException += (sender, args) => HandleUnhandledException(args.ExceptionObject as Exception, "UnhandledException");
                Application.ThreadException += (sender, args) => HandleUnhandledException(args.Exception, "ThreadException");
            #endif

            IDisposable sentrySdkInstance = null;

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                InitAppSettings();
                sentrySdkInstance = InitSentry();

                if (!CheckDotNet4_6())
                {
                    var result = MessageBox.Show("This program requires Microsoft .NET Framework v4.6 or newer. Do you want to open the download page now?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.Yes)
                        Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=56115");

                }

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
                
                // Check for update
                if (!IsDebugBuild && settings.CheckForUpdates)
                {
                    try
                    {
                        using (var client = new System.Net.WebClient())
                        {
                            client.Headers.Add("User-Agent: Other");
                            var regex = new Regex("[^0-9.]");
                            var json = client.DownloadString("https://api.github.com/repos/X2CommunityCore/xcom2-launcher/releases/latest");
                            var release = Newtonsoft.Json.JsonConvert.DeserializeObject<GitHub.Release>(json);
                            var currentVersion = GetCurrentVersion();
                            var newVersion = new Version(regex.Replace(release.tag_name, ""));

                            if (currentVersion.CompareTo(newVersion) < 0)
                                // New version available
                                new UpdateAvailableDialog(release, currentVersion.ToString()).ShowDialog();
                        }
                    }
                    catch (System.Net.WebException)
                    {
                        // No internet?
                    }
                }

                // clean up old files
                if (File.Exists(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak"))
                {
                    // Restore backup
                    File.Copy(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak", XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini", true);
                    File.Delete(XCOM2.DefaultConfigDir + @"\DefaultModOptions.ini.bak");
                }

                Application.Run(new MainForm(settings));
                SteamAPIWrapper.Shutdown();
            }
            finally
            {
                sentrySdkInstance?.Dispose();
                Properties.Settings.Default.Save();
            }
        }

#if !DEBUG
        static void HandleUnhandledException(Exception e, string source)
        {
            //SentrySdk.CaptureException(e);
            File.WriteAllText("error.log", $"Sentry GUID: {Properties.Settings.Default.Guid}\nSource: {source}\nMessage: {e.Message}\n\nStack:\n{e.StackTrace}");
            MessageBox.Show("An unhandled exception occured. See 'error.log' in application folder for additional details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
#endif

        /// <summary>
        /// Initializes the Sentry environment.
        /// Sentry is an open-source application monitoring platform that help to identify issues.
        /// </summary>
        /// <returns></returns>
        private static IDisposable InitSentry()
        {
            if (!Properties.Settings.Default.IsSentryEnabled || IsDebugBuild)
                return null;

            IDisposable sentrySdkInstance = null;

            try
            {
                sentrySdkInstance = SentrySdk.Init(o =>
                {
                    o.Dsn = new Dsn("https://3864ad83bed947a2bc16d88602ac0d87@sentry.io/1478084");
                    o.Release = "AML@" + GetCurrentVersionString();     // prefix because releases are global per organization
                    o.Debug = false;
                    o.Environment = IsDebugBuild ? "Debug" : "Release"; // Maybe use "Beta" for Pre-Release version (new/separate build configuration)
                    o.BeforeSend = sentryEvent =>
                    {
                        sentryEvent.User.Email = null;
                        return sentryEvent;
                    };
                });

                SentrySdk.ConfigureScope(scope =>
                {
                    scope.User = new User
                    {
                        Id = Properties.Settings.Default.Guid,
                        Username = Properties.Settings.Default.Username,
                        IpAddress = null
                    };
                });

                // SentrySdk.CaptureMessage("Sentry test message", SentryLevel.Debug);
            }
            catch (Exception ex)
            {
                // If Sentry wasn't initialized correctly we at least try to send one message to report this.
                // (this won't throw another Ex, even if Init() failed)
                SentrySdk.CaptureException(ex);
                SentrySdk.Close();
                Debug.WriteLine(ex.Message);
            }

            return sentrySdkInstance;
        }


		/// <summary>
		/// Initializes the Properties.Settings .NET applications settings.
		/// Used for all settings that we want to persist, even if the user decides to delete the
		/// json settings file or starts AML from different folders.
		/// </summary>
		private static void InitAppSettings()
        {
            var appSettings = Properties.Settings.Default;

            // Upgrade settings from previous version if required
            if (appSettings.IsSettingsUpgradeRequired) {
                appSettings.Upgrade();
                appSettings.IsSettingsUpgradeRequired = false;
            }

            // Initialize GUID (used for error reporting)
            if (string.IsNullOrEmpty(appSettings.Guid)) {
                appSettings.Guid = Guid.NewGuid().ToString();
            }

            // Version information can be used to perform version specific migrations if required.
            if (appSettings.Version != GetCurrentVersionString()) {
                // IF required at some point
                appSettings.Version = GetCurrentVersionString();
            }

            appSettings.Save();
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
						mod.AddState(ModState.NotInstalled);
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
						//settings.Mods.RemoveMod(m);
				}
            }

            // import mods
            settings.ImportMods();

            return settings;
        }

        public static Version GetCurrentVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fields = assembly.GetType("XCOM2Launcher.GitVersionInformation").GetFields();

            int.TryParse(fields.Single(f => f.Name == "Major").GetValue(null).ToString(), out var major);
            int.TryParse(fields.Single(f => f.Name == "Minor").GetValue(null).ToString(), out var minor);
            int.TryParse(fields.Single(f => f.Name == "Patch").GetValue(null).ToString(), out var patch);

            return new Version(major, minor, patch);
        }

        public static string GetCurrentVersionString(bool includeDebugPostfix = false) {
            var ver = GetCurrentVersion();

            var result = $"v{ver.Major}.{ver.Minor}.{ver.Build}";

            if (IsDebugBuild && includeDebugPostfix)
                result += " DEBUG";
            
            return result;
        }
    }
}