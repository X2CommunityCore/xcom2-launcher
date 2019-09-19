using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using XCOM2Launcher.GitHub;
using XCOM2Launcher.Helper;

namespace XCOM2Launcher.Forms
{
    public partial class UpdateAvailableDialog : Form
    {
        public string CurrentVersion { get; }
        public string NewVersion { get; }
        public Release Release { get; }

        public UpdateAvailableDialog(Release release, Version currentVersion, Version newVersion)
        {
            InitializeComponent();
            CurrentVersion = currentVersion.ToString();
            NewVersion = newVersion.ToString();
            Release = release;

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            version_current_value_label.Text = CurrentVersion;
            version_new_value_label.Text = NewVersion;
            changelog_textbox.Text = Release.body;
            date_value_label.Text = Release.published_at.ToString(CultureInfo.CurrentCulture);

            lBetaVersion.Visible = Release.prerelease;

            var asset = Release.assets.FirstOrDefault(a => a.name.EndsWith(".zip"));

            filesize_value_label.Text = asset == null ? "No download available yet." : Helper.FileSizeFormatExtension.FormatAsFileSize(asset.size);

        }

        private void close_button_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void show_button_Click(object sender, System.EventArgs e)
        {
            Tools.StartProcess(Release.html_url);
        }
    }
}