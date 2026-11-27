using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        Task<GitHubReleases.AvailableRelease> releaseCheck;

        Task<GitHubReleases.AvailableRelease> CheckForNewerRelease(bool refresh = false)
        {
            if (releaseCheck == null || refresh && releaseCheck.IsCompleted)
                releaseCheck = GitHubReleases.CheckAsync(DiagnosticLog.AppVersion);
            return releaseCheck;
        }

        void WireContact()
        {
            logSettingsLink.LinkClicked += (sender, args) => Run(() => LoggingDialog.ShowSettings(this));
            showLogLink.LinkClicked += (sender, args) => Run(() => ErrorDialog.OpenLog(this, DiagnosticLog.Current.FindLatest()));
            saveReportPackage.Click += async (sender, args) => await RunContactAction(SaveReportPackage);
            reportIssueButton.Click += async (sender, args) => await RunContactAction(() => OpenIssueDraft(false));
            requestFeatureButton.Click += async (sender, args) => await RunContactAction(() => OpenIssueDraft(true));
        }

        async Task RunContactAction(Func<Task> action)
        {
            reportIssueButton.Enabled = requestFeatureButton.Enabled = saveReportPackage.Enabled = false;
            try
            {
                await action();
            }
            finally
            {
                if (!IsDisposed)
                    reportIssueButton.Enabled = requestFeatureButton.Enabled = saveReportPackage.Enabled = true;
            }
        }

        async Task OpenIssueDraft(bool feature)
        {
            var release = await CheckForNewerRelease(refresh: true);
            if (IsDisposed)
                return;
            var title = feature ? requestFeatureButton.Text : reportIssueButton.Text;
            if (release != null)
            {
                string check = feature
                    ? UiText.T("Try it first to see whether the feature is already included.",
                        "Prøv den først for at se, om funktionen allerede er tilføjet.")
                    : UiText.T("Try it first to see whether the issue is fixed.",
                        "Prøv den først for at se, om fejlen er rettet.");
                string question = string.Format(UiText.T("Version {0} is available.", "Version {0} er tilgængelig."), release.Version)
                    + Environment.NewLine + check + Environment.NewLine
                    + UiText.T("Open the latest release?", "Åbn den seneste udgivelse?");
                if (ShowMessage(question, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ErrorDialog.OpenRelease(this, release.Url);
                    return;
                }
            }

            if (feature)
            {
                ErrorDialog.OpenFeatureRequest(this);
                return;
            }

            if (ShowMessage(UiText.T("Save a support ZIP to attach to the GitHub issue?",
                "Vil du gemme en ZIP-pakke til at vedhæfte på GitHub?"), reportIssueButton.Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                await SaveReportPackage();

            if (!IsDisposed)
                ErrorDialog.OpenBugReport(this);
        }

        async Task SaveReportPackage()
        {
            try
            {
                reportPackageDialog.Title = saveReportPackage.Text;
                reportPackageDialog.FileName = "CameraControlStudio-support-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
                if (reportPackageDialog.ShowDialog(this) != DialogResult.OK)
                    return;

                var layout = config?.Layout.Copy() ?? new ProfileLayout();
                ProfileWindow.Capture(this, layout, wasMaximized, settingsWidthAdded, settingsLeftOffset);
                var destination = reportPackageDialog.FileName;
                var logs = DiagnosticLog.Current.GetLogDirectories();
                var alternateDataDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CameraPressets");
                using (var screenshot = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(Width, Height))
                    {
                        DrawToBitmap(bitmap, new Rectangle(Point.Empty, Size));
                        bitmap.Save(screenshot, ImageFormat.Png);
                    }
                    screenshot.Position = 0;
                    await Task.Run(() => SupportPackage.Export(destination, screenshot, store.FilePath, layout, logs, alternateDataDirectory));
                }
                if (!IsDisposed)
                    ShowMessage(UiText.T("Support ZIP saved. Review it before sharing.",
                        "ZIP-pakken er gemt. Gennemgå den, før du deler den.") + Environment.NewLine + destination,
                        saveReportPackage.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Save support ZIP", ex);
                if (!IsDisposed)
                    ShowMessage(UiText.T("Could not save the support ZIP. Try a different location.",
                        "Kunne ikke gemme ZIP-pakken. Prøv at gemme den et andet sted."),
                        saveReportPackage.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
