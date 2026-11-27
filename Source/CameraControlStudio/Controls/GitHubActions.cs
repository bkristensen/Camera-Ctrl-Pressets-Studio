using System;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class GitHubActions : UserControl
    {
        internal const string RepositoryUrl = "https://github.com/" + GitHubReleases.Repository;
        internal const string ReleasesUrl = RepositoryUrl + "/releases";

        public GitHubActions()
        {
            InitializeComponent();
            repository.LinkClicked += (sender, args) => ErrorDialog.Open(FindForm(), RepositoryUrl);
            releases.LinkClicked += async (sender, args) =>
            {
                var url = await AppUpdates.ResolveDownloadUrlAsync();
                if (IsDisposed || url == null)
                    return;
                ConfigureDownloadLink();
                ErrorDialog.OpenRelease(FindForm(), url);
            };
            discussions.LinkClicked += (sender, args) => ErrorDialog.Open(FindForm(), RepositoryUrl + "/discussions");
            reportIssue.Click += (sender, args) => ReportIssue?.Invoke(this, args);
            requestFeature.Click += (sender, args) => RequestFeature?.Invoke(this, args);
            savePackage.Click += (sender, args) => SavePackage?.Invoke(this, args);
        }

        internal void ConfigureDownloadLink()
        {
            releases.Visible = AppUpdates.DownloadUrl != null;
            if (AppUpdates.OffersStoreMigration)
                releases.Text = UiText.T("Get it from Microsoft Store", "Hent i Microsoft Store");
            else if (AppUpdates.Channel == UpdateChannel.MicrosoftStore)
                releases.Text = UiText.T("Microsoft Store updates", "Opdateringer i Microsoft Store");
        }

        internal event EventHandler ReportIssue;
        internal event EventHandler RequestFeature;
        internal event EventHandler SavePackage;

        internal string ReportTitle => reportIssue.Text;
        internal string FeatureTitle => requestFeature.Text;
        internal string PackageTitle => savePackage.Text;

        internal void SetBusy(bool busy)
        {
            reportIssue.Enabled = requestFeature.Enabled = savePackage.Enabled = !busy;
        }

        internal string ChoosePackagePath(Form owner)
        {
            packageDialog.Title = PackageTitle;
            packageDialog.FileName = "CameraControlStudio-support-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            return packageDialog.ShowDialog(owner) == DialogResult.OK ? packageDialog.FileName : null;
        }
    }
}
