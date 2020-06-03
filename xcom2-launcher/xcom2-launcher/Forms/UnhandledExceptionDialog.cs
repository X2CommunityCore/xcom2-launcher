using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCOM2Launcher.Classes;
using XCOM2Launcher.Helper;

namespace XCOM2Launcher.Forms {
    public partial class UnhandledExceptionDialog : Form {
        public UnhandledExceptionDialog(Exception ex) 
        {
            InitializeComponent();
            
            tException.AppendText($"AML version: {Program.GetCurrentVersionString(true)}" + Environment.NewLine);
            tException.AppendText($"Sentry GUID: {GlobalSettings.Instance.Guid}" + Environment.NewLine);
            tException.AppendText($"Message: {ex.Message}" + Environment.NewLine + Environment.NewLine);
            tException.AppendText($"Stack: {Environment.NewLine} {ex.StackTrace}");
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) 
        {
            Tools.StartProcess(@"https://github.com/X2CommunityCore/xcom2-launcher");
        }

        private void linkDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) 
        {
            Tools.StartProcess(@"https://discord.gg/QHSVGRn");
        }

        private void linkAppFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) 
        {
            Tools.StartProcess(Application.StartupPath);
        }

        private void bClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tException.Text);
        }
    }
}
