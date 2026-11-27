using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class ErrorDialog : AppForm
    {
        const string NewIssueUrl = "https://github.com/" + GitHubReleases.Repository + "/issues/new";
        string logPath;
        MainForm main;

        public ErrorDialog()
        {
            InitializeComponent();
            if (InDesigner)
                return;
            FormText.Translate(this);
            report.Click += async (sender, args) => await ReportAsync();
            FormClosing += (sender, args) =>
            {
                if (!close.Enabled && args.CloseReason == CloseReason.UserClosing)
                    args.Cancel = true;
            };
            showLog.Click += (sender, args) => OpenLog(this, logPath);
        }

        internal static void Show(IWin32Window owner, string logPath, bool closing = false)
        {
            string message = closing
                ? UiText.T("An unexpected error occurred. The application will close.", "Der opstod en uventet fejl. Programmet lukker.")
                : UiText.T("An unexpected error prevented this action.", "En uventet fejl forhindrede handlingen.");
            string detail = logPath == null
                ? UiText.T("No diagnostic log is available.", "Der er ingen diagnoselog tilgængelig.")
                : UiText.T("Review the diagnostic log before attaching it to a public issue.", "Gennemgå diagnoseloggen, før du vedhæfter den til en offentlig fejlrapport.");
            using (var dialog = new ErrorDialog())
            {
                dialog.logPath = logPath;
                for (var form = owner as Form; form != null; form = form.Owner)
                {
                    if (form is MainForm main)
                    {
                        dialog.main = main;
                        break;
                    }
                }
                dialog.messageLabel.Text = message;
                dialog.detailLabel.Text = detail;
                dialog.showLog.Enabled = logPath != null;
                dialog.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
                dialog.ShowInTaskbar = owner == null;
                dialog.ShowDialog(owner);
            }
        }

        internal static void OpenLog(IWin32Window owner, string path)
        {
            if (File.Exists(path))
                Open(owner, "explorer.exe", "/select,\"" + path + "\"");
            else
                MessageBox.Show(owner, UiText.T("No diagnostic log is available.", "Der er ingen diagnoselog tilgængelig."),
                    "Camera Control Studio", MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, AppForm.MessageDirection);
        }

        async Task ReportAsync()
        {
            report.Enabled = close.Enabled = false;
            try
            {
                if (SupportPackage.AskToSave(this, report.Text) == DialogResult.Yes)
                {
                    string title = UiText.T("Save support ZIP", "Gem support-ZIP");
                    string destination = SupportPackage.ChooseDestination(packageDialog, this, title);
                    if (destination != null)
                    {
                        if (main != null && !main.IsDisposed)
                            await main.SaveSupportPackage(this, destination, title, logPath);
                        else
                            await SupportPackage.SaveAsync(this, destination, title, () => MainForm.CaptureForm(this),
                                new ProfileStore().FilePath, new ProfileLayout(), logPath, includeSystemReport: true);
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Prepare report attachments", ex);
                if (!IsDisposed && Visible)
                    MessageBox.Show(this, UiText.T("Report files could not be prepared.", "Rapportfilerne kunne ikke klargøres."),
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1, AppForm.MessageDirection);
            }
            finally
            {
                if (!IsDisposed && Visible)
                {
                    report.Enabled = close.Enabled = true;
                    OpenBugReport(this);
                }
            }
        }

        internal static void OpenBugReport(IWin32Window owner) => Open(owner, NewIssueUrl + "?template=bugs.md");

        internal static void OpenFeatureRequest(IWin32Window owner) => Open(owner, NewIssueUrl + "?template=feature.md");

        internal static void OpenRelease(IWin32Window owner, Uri url) => Open(owner, url.AbsoluteUri);

        internal static void Open(IWin32Window owner, string file, string arguments = "")
        {
            try
            {
                using (Process.Start(new ProcessStartInfo(file, arguments) { UseShellExecute = true })) { }
            }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Open support link or log folder", ex);
                MessageBox.Show(owner, UiText.T("The link or folder could not be opened.", "Linket eller mappen kunne ikke åbnes."),
                    "Camera Control Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1, AppForm.MessageDirection);
            }
        }
    }
}
