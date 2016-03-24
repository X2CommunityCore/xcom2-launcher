using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XCOM2Launcher.GitHub;

namespace XCOM2Launcher.Forms
{
    public partial class UpdateAvailableDialog : Form
    {
        public UpdateAvailableDialog(Release release, string currentVersion)
        {
            InitializeComponent();
            CurrentVersion = currentVersion;
            Release = release;

            UpdateLabels();
        }

        public string CurrentVersion { get; }

        public Release Release { get; }

        private void UpdateLabels()
        {
            version_current_value_label.Text = CurrentVersion;
            version_new_value_label.Text = Release.tag_name;
            changelog_textbox.Text = Release.body;
            date_value_label.Text = Release.published_at.ToString(CultureInfo.CurrentCulture);


            var asset = Release.assets.FirstOrDefault(a => a.name.EndsWith(".zip"));

            filesize_value_label.Text = asset == null ? "No download available yet." : Helper.FileSizeFormatExtension.FormatAsFileSize(asset.size);

        }

        private void close_button_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void show_button_Click(object sender, System.EventArgs e)
        {
            Process.Start(Release.html_url);
        }
    }
}