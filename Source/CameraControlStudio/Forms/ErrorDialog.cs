using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class ErrorDialog : AppForm
    {
        const string NewIssueUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/new";
        string logPath;

        public ErrorDialog()
        {
            InitializeComponent();
            if (InDesigner)
                return;
            FormText.Translate(this);
            report.Click += (sender, args) => Report(this, logPath);
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

        internal static void Report(IWin32Window owner, string logPath)
        {
            try
            {
                var folder = SystemReport.Export(logPath);
                Open(owner, folder);
            }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Prepare report attachments", ex);
                MessageBox.Show(owner, UiText.T("Report files could not be prepared.", "Rapportfilerne kunne ikke klargøres."),
                    "Camera Control Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1, AppForm.MessageDirection);
            }
            const string body = "## What happened?\n\n## Steps to reproduce\n\n## Diagnostics\n\nReview and drag CameraControlStudio-report.log and CameraControlStudio-system.log into this issue if available.";
            Open(owner, NewIssueUrl + "?body=" + Uri.EscapeDataString(body));
        }

        static void Open(IWin32Window owner, string file, string arguments = "")
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
