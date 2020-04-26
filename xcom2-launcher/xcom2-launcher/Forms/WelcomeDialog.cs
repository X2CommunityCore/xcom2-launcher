using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher.Forms
{
    public partial class WelcomeDialog : Form
    {
        public bool UseSentry => rSentryEnabled.Checked;
        public GameId Game { get; private set; }

        public WelcomeDialog()
        {
            InitializeComponent();
            Game = GameId.X2;
        }

        private void bContinue_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WelcomeDialog_Load(object sender, EventArgs e) {
            Text += " " + Program.GetCurrentVersionString(true);
        }

        private void GameSelectionChanged(object sender, EventArgs e)
        {
            bContinue.Enabled = true;
            Game = rGameChimera.Checked ? GameId.ChimeraSquad : GameId.X2;
        }
    }
}
