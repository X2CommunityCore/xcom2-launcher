using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using XCOM2Launcher.Classes.Steam;

namespace XCOM2Launcher.XCOM
{
    internal class Xcom2Env : XcomEnvironment
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(Xcom2Env));
        public string DataDir => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\XCOM2";
        public string DataDirWotC => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +  @"\my games\XCOM2 War of the Chosen";
        public string LogFilePath => DataDir + @"\XComGame\Logs\Launch.log";
        public string LogFilePathWotC => DataDirWotC + @"\XComGame\Logs\Launch.log";
        private string UserConfigDir => DataDir + @"\my games\XCOM2\XComGame\Config";
        private string WotCUserConfigDir => DataDirWotC +  @"\my games\XCOM2 War of the Chosen\XComGame\Config";
        public override string DefaultConfigDir => Path.Combine(GameDir, @"XComGame\Config");
        private string WotCDefaultConfigDir => Path.Combine(GameDir, @"XCom2-WarOfTheChosen\XComGame\Config");

        public bool UseWotC { get; set; }

        public Xcom2Env() : base(GameId.X2)
        {
        }

        protected override DefaultConfigFile GetConfigFile(string file, bool load = true)
        {
            if (UseWotC)
            {
                return new DefaultConfigFile(file, WotCUserConfigDir, WotCDefaultConfigDir, load);
            }

            return new DefaultConfigFile(file, UserConfigDir, DefaultConfigDir, load);
        }

        /// <summary>
        /// Runs the game with the selected arguments
        /// </summary>
        /// <param name="gameDir"></param>
        /// <param name="args"></param>
        public override void RunGame(string gameDir, string args)
        {
            if (UseWotC)
            {
                RunWotC(gameDir, args);
            }
            else
            {
                RunVanilla(gameDir, args);
            }
        }

        private void RunVanilla(string gameDir, string args)
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
        private void RunWotC(string gameDir, string args)
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

        
    }
}