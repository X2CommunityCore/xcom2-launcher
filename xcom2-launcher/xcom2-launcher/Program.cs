using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Semver;
using Sentry;
using XCOM2Launcher.Classes;
using XCOM2Launcher.Classes.Helper;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.Forms;
using XCOM2Launcher.Helper;
using XCOM2Launcher.Mod;
using XCOM2Launcher.Steam;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher
{
    internal static class Program
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(Program));
        public static readonly bool IsDebugBuild;
        public static XcomEnvironment XEnv;

        static Program()
        {
            #if DEBUG
                IsDebugBuild = true;
            #else
                IsDebugBuild = false;
            #endif

            Log.Info($"Application started (AML {GitVersionInfo.FullSemVer} {GitVersionInfo.Sha})");
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
                if (File.Exists(Program.XEnv.DefaultConfigDir + @"\DefaultModOptions.ini.bak"))
                {
                    // Restore backup
                    File.Copy(Program.XEnv.DefaultConfigDir + @"\DefaultModOptions.ini.bak", Program.XEnv.DefaultConfigDir + @"\DefaultModOptions.ini", true);
                    File.Delete(Program.XEnv.DefaultConfigDir + @"\DefaultModOptions.ini.bak");
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
            File.WriteAllText("error.log", $"Version: {GetCurrentVersionString(true)}\n" +
                                           $"Sentry GUID: {GlobalSettings.Instance.Guid}\n" +
                                           $"Source: {source}\n" +
                                           $"Message: {e.Message}\n\n" +
                                           $"Stack:\n{e.StackTrace}");
            
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
                #endif

                sentrySdkInstance = SentrySdk.Init(o =>
                {
                    o.Dsn = "https://3864ad83bed947a2bc16d88602ac0d87@o269373.ingest.sentry.io/1478084";
                    o.Release = "AML@" + GitVersionInfo.SemVer;     // prefix because releases are global per organization
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
        private static void InitAppSettings()
        {
            var appSettings = GlobalSettings.Instance;
            var currentVersion = new Version(GitVersionInfo.MajorMinorPatch);

            if (appSettings.MaxVersion < currentVersion)
            {
                appSettings.MaxVersion = currentVersion;
            }

            // AML will either be used for XCOM2 or Chimera Squad
            // Create Steam application id file if it does not exist (depending on game choice of the user)
            if (!File.Exists(Workshop.APPID_FILENAME))
            {
                // Show Welcome Dialog and ask user to opt-in for Sentry error reporting.
                WelcomeDialog dlg = new WelcomeDialog();
                dlg.ShowDialog();
                appSettings.IsSentryEnabled = dlg.UseSentry;

                try
                {
                    using (var file = File.CreateText(Workshop.APPID_FILENAME))
                    {
                        file.WriteLine((uint)dlg.Game);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unable to create {Workshop.APPID_FILENAME}. {Environment.NewLine} {ex.Message} ");
                    return;
                }
            }

            // Use Steam Application id file to determine which game this AML installation is used for.
            string appIdStr;

            try
            {
                appIdStr = File.ReadAllText(Workshop.APPID_FILENAME);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to access {Workshop.APPID_FILENAME}. {Environment.NewLine} {ex.Message} ");
                return;
            }

            if (uint.TryParse(appIdStr, out uint appId))
            {
                switch ((GameId)appId)
                {
                    case GameId.X2:
                        XEnv = new Xcom2Env();
                        break;
                    case GameId.ChimeraSquad:
                        XEnv = new XComChimeraSquadEnv();
                        break;
                    default:
                        MessageBox.Show($"The {Workshop.APPID_FILENAME} file contains an unexpected application id: {appId}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
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

            if (!firstRun)
            {
                // Check if settings file matches target game
                if (settings.Game != XEnv.Game)
                {
                    var targetGame = settings.Game == GameId.X2 ? "XCOM 2" : "XCOM Chimera Squad";
                    var activeGame = XEnv.Game == GameId.X2 ? "XCOM 2" : "XCOM Chimera Squad";
                    MessageBox.Show($"The current settings were created for '{targetGame}', but this copy of AML was configured to run '{activeGame}'. " +
                                    "To resolve this close AML and:\n\n" + 
                                    "a) delete the file 'settings.json' to reset the settings\n\n" +
                                    "    OR\n\n" +
                                    $"b) delete the file 'steam_appid.txt' and select '{targetGame}' on startup", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Environment.Exit(0);
                }
            }

            settings.Game = XEnv.Game;

            // Verify Game Path
            if (!Directory.Exists(settings.GamePath))
                settings.GamePath = Program.XEnv.DetectGameDir();

            if (settings.GamePath == "")
            {
                Log.Warn("Unable to detect installation path");
                MessageBox.Show(@"Could not detect path to installation directory. Please select it manually from the settings.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Make sure, that all mod paths have a trailing backslash
            var pathsWithMissingTrailingBackSlash = settings.ModPaths.Where(m => !m.EndsWith(@"\")).ToList();
            for (var i = 0; i < pathsWithMissingTrailingBackSlash.Count; i++)
            {
                pathsWithMissingTrailingBackSlash[i] += @"\";
            }

            // Check and potentially add new mod paths from XCOM ini file.
            var modDirs = XEnv.DetectModDirs();

            if (modDirs != null)
            {
                settings.ModPaths.AddRange(modDirs.Where(modPath => !settings.ModPaths.Contains(modPath)));
            }
            else
            {
                MessageBox.Show("Unable to read mod directories from 'XComEngine.ini'. See file 'AML.log' for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Remove obsolete mod paths
            settings.ModPaths.RemoveAll(modPath => !Directory.Exists(modPath));

            if (settings.ModPaths.Count == 0)
            {
                Log.Warn("No mod directories configured");
                MessageBox.Show(@"Unable to detect mod directories. Please select them manually from the settings dialog.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (settings.Mods.Entries.Count > 0)
            {
                settings.Mods.MarkDuplicates();
                settings.Mods.InitCategoryIndices();

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

                    // If the duplicate mod workaround is disabled, make sure that all mod info files are enabled.
                    if (!settings.EnableDuplicateModIdWorkaround)
                    {
                        mod.EnableModFile();
                    }

                    settings.Mods.UpdatedModDependencyState(mod);
                }

                var newMissingMods = settings.Mods.All.Where(m => (m.State.HasFlag(ModState.NotLoaded) || m.State.HasFlag(ModState.NotInstalled)) &&
                                                               (!m.PreviousState.HasFlag(ModState.NotLoaded) && !m.PreviousState.HasFlag(ModState.NotInstalled))).ToList();

                // Ask if newly missing mods should be hidden
                if (newMissingMods.Any(m => !m.isHidden))
                {
                    string message;
                    
                    if (newMissingMods.Count == 1)
                    {
                        message = $"The mod '{newMissingMods.FirstOrDefault()?.Name}' no longer exists.\n\nDo you want to hide this mod from the mod list?";
                    }
                    else
                    {
                        message = $"{newMissingMods.Count} mods no longer exist:\n\n- " + string.Join("\n- ", newMissingMods.Select(m => m.Name)) + "\n\nDo you want to hide these mods from the mod list?";
                    }

                    var result = MessageBox.Show(message, "Missing mods", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                        newMissingMods.ForEach(m => m.isHidden = true);
                    }
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

                    bool parsingSucceeded = SemVersion.TryParse(GitVersionInfo.SemVer, out SemVersion currentVersion);
                    parsingSucceeded &= SemVersion.TryParse(release.tag_name.TrimStart('v'), out SemVersion newVersion);

                    if (parsingSucceeded)
                    {
                        // If not explicitly enabled, we ignore alpha versions.
                        if (!Settings.Instance.IncludeAlphaVersions && newVersion.Prerelease.Contains("alpha"))
                            return false;

                        if (currentVersion < newVersion)
                        {
                            // New version available
                            Log.Info("New version available " + newVersion);
                            new UpdateAvailableDialog(release, currentVersion, newVersion).ShowDialog();
                            return true;
                        }
                    }
                    else
                    {
                        var message = $"{nameof(CheckForUpdate)}: Error parsing release version information '{release.tag_name}'.";
                        Log.Error(message);
                        Debug.Fail(message);
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Log.Warn("Web request failed", ex);
            }
            catch (JsonReaderException ex)
            {
                Log.Warn("Failed to parse version information from Github", ex);
            }

            return false;
        }

        public static string GetCurrentVersionString(bool includePostfix = false)
        {
            var result = GitVersionInfo.SemVer;

            if (includePostfix && IsDebugBuild)
            {
                result += " [DEBUG]";
            }

            return result;
        }
    }
}