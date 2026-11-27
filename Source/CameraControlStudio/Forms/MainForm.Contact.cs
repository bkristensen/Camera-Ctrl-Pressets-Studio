using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        Task<AppUpdate> releaseCheck;

        Task<AppUpdate> CheckForNewerRelease(bool refresh = false)
        {
            if (releaseCheck == null || refresh && releaseCheck.IsCompleted)
                releaseCheck = AppUpdates.CheckAsync(Handle);
            return releaseCheck;
        }

        void WireContact()
        {
            settings.logSettingsLink.LinkClicked += (sender, args) => RunSettings(() => LoggingDialog.ShowSettings(settings));
            settings.showLogLink.LinkClicked += (sender, args) => RunSettings(() => ErrorDialog.OpenLog(settings, DiagnosticLog.Current.FindLatest()));
            BindGitHubActions(settings.githubActions, settings);
        }

        bool contactInProgress;

        void BindGitHubActions(GitHubActions actions, Form owner)
        {
            actions.ConfigureDownloadLink();
            actions.ReportIssue += async (sender, args) => await RunContactAction(actions, owner, () => OpenIssueDraft(false, actions, owner));
            actions.RequestFeature += async (sender, args) => await RunContactAction(actions, owner, () => OpenIssueDraft(true, actions, owner));
            actions.SavePackage += async (sender, args) => await RunContactAction(actions, owner, () => SaveReportPackage(actions, owner));
        }

        async Task RunContactAction(GitHubActions actions, Form owner, Func<Task> action)
        {
            if (contactInProgress)
                return;

            contactInProgress = true;
            actions.SetBusy(true);
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("ERROR", "Contact action", ex);
                if (!IsDisposed && !owner.IsDisposed && owner.Visible)
                    ShowContactMessage(owner, ActionFailedMessage(), owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                contactInProgress = false;
                if (!actions.IsDisposed)
                    actions.SetBusy(false);
            }
        }

        async Task OpenIssueDraft(bool feature, GitHubActions actions, Form owner)
        {
            var release = await CheckForNewerRelease(refresh: true);
            if (IsDisposed || owner.IsDisposed || !owner.Visible)
                return;
            var title = feature ? actions.FeatureTitle : actions.ReportTitle;
            if (release != null && !release.IsStoreMigration)
            {
                string check = feature
                    ? UiText.T("Try it first to see whether the feature is already included.",
                        "Prøv den først for at se, om funktionen allerede er tilføjet.")
                    : UiText.T("Try it first to see whether the issue is fixed.",
                        "Prøv den først for at se, om fejlen er rettet.");
                string available = release.Version == null
                    ? UiText.T("New release available", "Ny version tilgængelig")
                    : string.Format(UiText.T("Version {0} is available.", "Version {0} er tilgængelig."), release.Version);
                string openUpdate = AppUpdates.Channel == UpdateChannel.MicrosoftStore
                    ? UiText.T("Open Microsoft Store updates?", "Åbn opdateringer i Microsoft Store?")
                    : UiText.T("Open the latest release?", "Åbn den seneste udgivelse?");
                string question = available + Environment.NewLine + check + Environment.NewLine + openUpdate;
                if (ShowContactMessage(owner, question, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ErrorDialog.OpenRelease(owner, release.Url);
                    return;
                }
            }

            if (feature)
            {
                ErrorDialog.OpenFeatureRequest(owner);
                return;
            }

            if (SupportPackage.AskToSave(owner, actions.ReportTitle) == DialogResult.Yes)
                await SaveReportPackage(actions, owner);

            if (!IsDisposed && !owner.IsDisposed && owner.Visible)
                ErrorDialog.OpenBugReport(owner);
        }

        DialogResult ShowContactMessage(Form owner, string message, string title, MessageBoxButtons buttons,
            MessageBoxIcon icon, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1) =>
            MessageBox.Show(owner, message, title, buttons, icon, defaultButton, AppForm.MessageDirection);

        Task SaveReportPackage(GitHubActions actions, Form owner) =>
            SaveSupportPackage(owner, actions.ChoosePackagePath(owner), actions.PackageTitle);

        internal Task SaveSupportPackage(Form owner, string destination, string title, string diagnosticLog = null)
        {
            if (destination == null)
                return Task.CompletedTask;

            var layout = config?.Layout.Copy() ?? new ProfileLayout();
            ProfileWindow.Capture(this, layout, wasMaximized);
            return owner is ErrorDialog
                ? SupportPackage.SaveAsync(owner, destination, title, () => CaptureForm(owner), store.FilePath,
                    layout, diagnosticLog, includeSystemReport: true)
                : SupportPackage.SaveAsync(owner, destination, title, () => CaptureWindows(settings.Visible), store.FilePath, layout);
        }
    }
}
