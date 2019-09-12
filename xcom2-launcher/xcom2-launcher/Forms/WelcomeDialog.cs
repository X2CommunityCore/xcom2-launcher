using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCOM2Launcher.Forms
{
    public partial class WelcomeDialog : Form
    {
        public bool UseSentry => rSentryEnabled.Checked;

        public WelcomeDialog()
        {
            InitializeComponent();
        }

        private void bContinue_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
