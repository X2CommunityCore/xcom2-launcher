using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using Sentry;
using Sentry.Protocol;
using XCOM2Launcher.Classes;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Forms;
using XCOM2Launcher.Helper;
using XCOM2Launcher.Mod;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    internal static class Program
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(Program));
        public static readonly bool IsDebugBuild;

        static Program()
        {
            #if DEBUG
                IsDebugBuild = true;
            #else
                IsDebugBuild = false;
            #endif

            Log.Info($"Application started (AML {GetCurrentVersionString(true)})");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!IsDebugBuild)
            {
                // Capture all unhandled Exceptions
                AppDomain.CurrentDomain.UnhandledException += (sender, args) => HandleUnhandledException(args.ExceptionObject as Exception, "UnhandledException");
                Application.ThreadException += (sender, args) => HandleUnhandledException(args.Exception, "ThreadException");
            }

            // Mutex is used to check if another instance of AML is already running
            Mutex mutex = new Mutex(true, "E3241D27-3DD8-4615-888A-502252B9E2A1", out var isFirstInstance);
            IDisposable sentrySdkInstance = null;

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                InitAppSettings();
                sentrySdkInstance = InitSentry();
                
                if (!CheckDotNet4_7_2())
                {
                    Log.Warn(".NET Framework v4.7.2 required");

                    var result = MessageBox.Show("This program requires Microsoft .NET Framework v4.7.2 or newer. Do you want to open the download page now?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.Yes)
                        Tools.StartProcess("https://dotnet.microsoft.com/download/dotnet-framework");

                    return;
                }

                if (!SteamAPIWrapper.Init()) {
                    Log.Warn("Failed to detect Steam");

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
                {
                    Log.Error("Failed to initialize settings");
                    return;
                }

                // Exit if another instance of AML is already running and multiple instances are disabled.
                if (!settings.AllowMultipleInstances && !isFirstInstance) {
                    MessageBox.Show("Another instance of AML is already running.", "AML already started", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Check for update
                if (!IsDebugBuild && settings.CheckForUpdates)
                {
                    CheckForUpdate();
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
                Log.Info("Shutting down...");
                sentrySdkInstance?.Dispose();
                GlobalSettings.Instance.Save();
                GC.KeepAlive(mutex);    // prevent the mutex from being garbage collected early
                mutex.Dispose();
            }
        }

        static void HandleUnhandledException(Exception e, string source)
        {
            Log.Fatal("Unhandled exception", e);
            File.WriteAllText("error.log", $"Sentry GUID: {GlobalSettings.Instance.Guid}\nSource: {source}\nMessage: {e.Message}\n\nStack:\n{e.StackTrace}");
            var dlg = new UnhandledExceptionDialog(e);
            dlg.ShowDialog();
            Application.Exit();
        }

        /// <summary>
        /// Initializes the Sentry environment.
        /// Sentry is an open-source application monitoring platform that help to identify issues.
        /// </summary>
        /// <returns></returns>
        private static IDisposable InitSentry()
        {
            if (!GlobalSettings.Instance.IsSentryEnabled || IsDebugBuild)
            {
                Log.Info("Sentry is disabled");
                return null;
            }

            Log.Info("Initializing Sentry");

            IDisposable sentrySdkInstance = null;
            
            try
            {
                string environment = "Release";
                
                #if DEBUG
                    environment = "Debug";
                #elif BETA
                    environment = "Beta";
                #endif

                sentrySdkInstance = SentrySdk.Init(o =>
                {
                    o.Dsn = new Dsn("https://3864ad83bed947a2bc16d88602ac0d87@sentry.io/1478084");
                    o.Release = "AML@" + GetCurrentVersionString();     // prefix because releases are global per organization
                    o.Debug = false;
                    o.Environment = environment;
                    o.MaxBreadcrumbs = 50;
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
                        Id = GlobalSettings.Instance.Guid,
                        Username = GlobalSettings.Instance.UserName,
                        IpAddress = null
                    };
                });

                SentrySdk.CaptureMessage("Sentry initialized");
                Log.Info($"Sentry initialized ({GlobalSettings.Instance.Guid})");
            }
            catch (Exception ex)
            {
                // If Sentry wasn't initialized correctly we at least try to send one message to report this.
                // (this won't throw another Ex, even if Init() failed)
                Log.Error("Sentry setup failed", ex);
                SentrySdk.Close();
                Debug.WriteLine(ex.Message);
            }

            return sentrySdkInstance;
        }


        /// <summary>
        /// Initializes GlobalSettings class.
        /// Used for all settings that we want to persist, even if the user decides to delete the
        /// json settings file or starts AML from different folders.
        /// </summary>
        private static void InitAppSettings() {
            var appSettings = GlobalSettings.Instance;
            var currentVersion = GetCurrentVersion();

            if (appSettings.MaxVersion < currentVersion) {
                Log.Info($"AML was upgraded from '{appSettings.MaxVersion}' to '{currentVersion}'.");

                // Show Welcome Dialog and ask user to opt-in for Sentry error reporting.
                WelcomeDialog dlg = new WelcomeDialog();
                dlg.ShowDialog();
                appSettings.IsSentryEnabled = dlg.UseSentry;

                appSettings.MaxVersion = currentVersion;
            }

            appSettings.Save();
        }

        /// <summary>
        /// Checks if .Net Framework 4.7.2 or later installed.
        /// Verifies that the method SortedSet.TryGetValue() (which was added with 4.7.2) is available.
        /// </summary>
        /// <returns>true if version 4.7.2 or later is installed</returns>
        private static bool CheckDotNet4_7_2()
        {
            try
            {
                return typeof(SortedSet<>).GetMethod("TryGetValue") != null;
                // return typeof(DateTimeOffset).GetMethod("FromUnixTimeSeconds") != null; // obsolete .NET 4.6 check
            }
            catch (AmbiguousMatchException)
            {
                // ambiguous means there is more than one result
                return true;
            }
        }

        public static Settings InitializeSettings()
        {
            var firstRun = !File.Exists("settings.json");

            var settings = firstRun ? new Settings() : Settings.Instance;

            // Logic behind this:
            // If the field ShowUpgradeWarning doesn't exists in the loaded settings file; it will be initialized to its default value "true".
            // In that case, an old incompatible settings version is assumed and we report a warning.
            if (settings.ShowUpgradeWarning && !firstRun)
            {
                Log.Warn("Incompatible settings.json");

                MessageBoxManager.Cancel = "Exit";
                MessageBoxManager.OK = "Continue";
                MessageBoxManager.Register();
                var choice = MessageBox.Show("This launcher version is NOT COMPATIBLE with the old 'settings.json' file.\n" +
                                             "Stop NOW and launch the old version to export a profile of your mods INCLUDING GROUPS!\n" +
                                             "Once that is done, move the old 'settings.json' file to a SAFE PLACE and then proceed.\n" +
                                             "After loading, import the profile you saved to recover groups.\n\n" +
                                             "If you are not ready to do this, click 'Exit' to leave with no changes.",
                                             "WARNING!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (choice == DialogResult.Cancel)
                    Environment.Exit(0);

                Log.Warn("User ignored incompatibility");
                MessageBoxManager.Unregister();
            }

            settings.ShowUpgradeWarning = false;

            // Verify Game Path
            if (!Directory.Exists(settings.GamePath))
                settings.GamePath = XCOM2.DetectGameDir();

            if (settings.GamePath == "")
            {
                Log.Warn("Unable to detect XCOM 2 installation path");
                MessageBox.Show(@"Could not find XCOM 2 installation path. Please fill it manually in the settings.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Make sure, that all mod paths have a trailing backslash
            var pathsWithMissingTrailingBackSlash = settings.ModPaths.Where(m => !m.EndsWith(@"\")).ToList();
            for (var i = 0; i < pathsWithMissingTrailingBackSlash.Count; i++)
            {
                pathsWithMissingTrailingBackSlash[i] += @"\";
            }

            // Check and potentially add new mod paths from XCOM ini file.
            var modPathsFromIni = XCOM2.DetectModDirs();

            if (modPathsFromIni != null)
            {
                settings.ModPaths.AddRange(modPathsFromIni.Where(modPath => !settings.ModPaths.Contains(modPath)));
            }
            else
            {
                MessageBox.Show("Unable to read mod directories from 'XComEngine.ini'. See file 'AML.log' for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Remove obsolete mod paths
            settings.ModPaths.RemoveAll(modPath => !Directory.Exists(modPath));

            if (settings.ModPaths.Count == 0)
            {
                Log.Warn("No XCOM 2 mod directories configured");
                MessageBox.Show(@"Could not find XCOM 2 mod directories. Please fill them in manually in the settings.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
                    {
                        Log.Warn($"The mod {mod.ID} is not located in any of the configured mod directories -> ModState.NotLoaded");
                        mod.AddState(ModState.NotLoaded);
                    }

                    if (!Directory.Exists(mod.Path))
                    {
                        Log.Warn($"The mod {mod.ID} is no longer available in the directory {mod.Path} -> ModState.NotInstalled");
                        mod.AddState(ModState.NotInstalled);
                    }
                    else if (!File.Exists(mod.GetModInfoFile()))
                    {
                        string newModInfo = settings.Mods.FindModInfo(mod.Path);
                        if (newModInfo != null)
                        {
                            mod.ID = Path.GetFileNameWithoutExtension(newModInfo);
                        }
                        else
                        {
                            Log.Warn($"The XComMod file for the mod {mod.ID} is missing -> ModState.NotInstalled");
                            mod.AddState(ModState.NotInstalled);
                        }
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
            Log.Info("Checking for Updates...");

            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Headers.Add("User-Agent: Other");
                    GitHub.Release release;

                    if (Settings.Instance.CheckForPreReleaseUpdates)
                    {
                        Log.Info("Pre-Release updates enabled");
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
                    {
                        Log.Warn("No release information found");
                        return false;
                    }

                    var regexVersionNumber = new Regex("[^0-9.]");

                    var currentVersion = GetCurrentVersion();
                    string releaseVersionString = release.tag_name;
                    Version.TryParse(regexVersionNumber.Replace(releaseVersionString, ""), out Version newVersion);

                    if (currentVersion != null && newVersion != null)
                    {
                        if (currentVersion.CompareTo(newVersion) < 0)
                        {
                            // New version available
                            Log.Info("New version available " + newVersion);
                            new UpdateAvailableDialog(release, currentVersion, newVersion).ShowDialog();
                            return true;
                        }
                    }
                    else
                    {
                        string message = $"{nameof(CheckForUpdate)}: Error parsing version information '{releaseVersionString}'.";
                        Log.Error(message);
                        Debug.Fail(message);
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Log.Warn("Web request failed", ex);
                Debug.WriteLine(nameof(CheckForUpdate) + ": " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Returns versions information that was generated by GitVersionTask
        /// </summary>
        /// <returns></returns>
        public static Version GetCurrentVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fields = assembly.GetType("XCOM2Launcher.GitVersionInformation").GetFields();

            int.TryParse(fields.Single(f => f.Name == "Major").GetValue(null).ToString(), out var major);
            int.TryParse(fields.Single(f => f.Name == "Minor").GetValue(null).ToString(), out var minor);
            int.TryParse(fields.Single(f => f.Name == "Patch").GetValue(null).ToString(), out var patch);

            return new Version(major, minor, patch);
        }

        public static string GetCurrentVersionString(bool includePostfix = false) {
            var ver = GetCurrentVersion();
            var result = $"v{ver.Major}.{ver.Minor}.{ver.Build}";

            if (includePostfix)
            {
                if (IsDebugBuild)
                    result += " DEBUG";

                #if BETA
                    result += " BETA";
                #endif
            }

            return result;
        }
    }
}