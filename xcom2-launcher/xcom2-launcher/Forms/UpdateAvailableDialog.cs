using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using HeyRed.MarkdownSharp;
using Semver;
using XCOM2Launcher.GitHub;
using XCOM2Launcher.Helper;

namespace XCOM2Launcher.Forms
{
    public partial class UpdateAvailableDialog : Form
    {
        public string CurrentVersion { get; }
        public string NewVersion { get; }
        public Release Release { get; }

        public UpdateAvailableDialog(Release release, SemVersion currentVersion, SemVersion newVersion)
        {
            InitializeComponent();

            AcceptButton = bClose;
            CancelButton = bClose;
            releaseNoteBrowser.Navigating += Tools.HandleNavigateWebBrowserControl;

            CurrentVersion = currentVersion.ToString();
            NewVersion = newVersion.ToString();
            Release = release;

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            version_current_value_label.Text = CurrentVersion;
            version_new_value_label.Text = NewVersion;
            
            Markdown markdown = new Markdown();
            var body = markdown.Transform(Release.body);
            changelog_label.Text = Release.name;
            releaseNoteBrowser.DocumentText = body;

            date_value_label.Text = Release.published_at.ToString(CultureInfo.CurrentCulture);

            lBetaVersion.Visible = Release.prerelease;

            var asset = Release.assets.FirstOrDefault(a => a.name.EndsWith(".zip"));

            filesize_value_label.Text = asset == null ? "No download available yet." : Helper.FileSizeFormatExtension.FormatAsFileSize(asset.size);

        }

        private void show_button_Click(object sender, System.EventArgs e)
        {
            Tools.StartProcess(Release.html_url);
        }
    }
}