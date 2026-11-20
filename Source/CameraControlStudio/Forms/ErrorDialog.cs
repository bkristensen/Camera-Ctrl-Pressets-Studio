using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CameraControlStudio
{
    internal static class ErrorDialog
    {
        const string NewIssueUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/new";

        internal static void Show(IWin32Window owner, string logPath, bool closing = false)
        {
            string message = closing
                ? UiText.T("An unexpected error occurred. The application will close.", "Der opstod en uventet fejl. Programmet lukker.")
                : UiText.T("An unexpected error prevented this action.", "En uventet fejl forhindrede handlingen.");
            string detail = logPath == null
                ? UiText.T("No diagnostic log is available.", "Der er ingen diagnoselog tilgængelig.")
                : UiText.T("Review the diagnostic log before attaching it to a public issue.", "Gennemgå diagnoseloggen, før du vedhæfter den til en offentlig fejlrapport.");
            using (var dialog = new AppForm
            {
                Text = "Camera Control Studio",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ShowInTaskbar = owner == null
            })
            {
                var content = new FlowLayoutPanel
                {
                    AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.TopDown, WrapContents = false, Padding = new Padding(14)
                };
                var buttons = new FlowLayoutPanel { AutoSize = true, WrapContents = false };
                var report = new Button { AutoSize = true, Text = UiText.T("Report on GitHub", "Rapportér på GitHub") };
                var showLog = new Button { AutoSize = true, Text = UiText.T("Show log file", "Vis logfil"), Enabled = logPath != null };
                var close = new Button { AutoSize = true, Text = UiText.T("Close", "Luk"), DialogResult = DialogResult.OK };
                report.Click += (sender, args) => Report(dialog, logPath);
                showLog.Click += (sender, args) => OpenLog(dialog, logPath);
                buttons.Controls.AddRange(new Control[] { report, showLog, close });
                content.Controls.Add(new Label { AutoSize = true, MaximumSize = new Size(440, 0), Text = message });
                content.Controls.Add(new Label { AutoSize = true, MaximumSize = new Size(440, 0), Text = detail });
                content.Controls.Add(buttons);
                dialog.Controls.Add(content);
                dialog.AcceptButton = close;
                dialog.CancelButton = close;
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
