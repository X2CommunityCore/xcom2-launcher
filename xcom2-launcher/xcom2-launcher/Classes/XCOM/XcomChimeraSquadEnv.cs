using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using XCOM2Launcher.Classes.Steam;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher.Classes
{
    internal class XComChimeraSquadEnv : XcomEnvironment
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(nameof(Xcom2Env));
        
        private string UserConfigDir => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\XCOM Chimera Squad\XComGame\Config";
        public override string DefaultConfigDir => Path.Combine(GameDir, @"XComGame\Config");

        public XComChimeraSquadEnv() : base(GameId.ChimeraSquad)
        {
        }

        protected override DefaultConfigFile GetConfigFile(string file, bool load = true)
        {
            return new DefaultConfigFile(file, UserConfigDir, DefaultConfigDir, load);
        }

        public override void RunGame(string gameDir, string args)
        {
            Log.Info("Starting XCOM Chimera Squad");

            if (!SteamAPIWrapper.Init())
                MessageBox.Show("Could not connect to steam.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var p = new Process
            {
                StartInfo =
                {
                    Arguments = args,
                    FileName = gameDir + @"\Binaries\Win64\xcom.exe",
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

    }
}
