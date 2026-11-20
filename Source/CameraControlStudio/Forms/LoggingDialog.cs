using System.Drawing;
using System.Windows.Forms;

namespace CameraControlStudio
{
    internal static class LoggingDialog
    {
        internal static void Show(IWin32Window owner)
        {
            var settings = DiagnosticLog.Current.Settings;
            string defaultFolder = UiText.T("Automatic (application folder)", "Automatisk (programmappen)");
            string directory = settings.DirectoryPath;
            using (var dialog = new AppForm
            {
                Text = UiText.T("Log settings", "Logindstillinger"),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ShowInTaskbar = false
            })
            {
                var layout = new TableLayoutPanel { AutoSize = true, Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(12) };
                var folder = new TextBox { ReadOnly = true, Width = 355, Text = directory ?? defaultFolder };
                var browse = new Button { AutoSize = true, Text = UiText.T("Choose folder", "Vælg mappe") };
                var automatic = new Button { AutoSize = true, Text = UiText.T("Automatic", "Automatisk") };
                var maxSize = new NumericUpDown { Minimum = 1, Maximum = DiagnosticLogSettings.MaxSizeMiB, Value = settings.SizeMiB };
                var maxFiles = new NumericUpDown { Minimum = 1, Maximum = DiagnosticLogSettings.MaxFiles, Value = settings.FileCount };
                var save = new Button { AutoSize = true, Text = UiText.T("Save", "Gem"), DialogResult = DialogResult.OK };
                var cancel = new Button { AutoSize = true, Text = UiText.T("Cancel", "Annuller"), DialogResult = DialogResult.Cancel };
                browse.Click += (sender, args) =>
                {
                    using (var picker = new FolderBrowserDialog { SelectedPath = directory ?? System.AppDomain.CurrentDomain.BaseDirectory })
                    {
                        if (picker.ShowDialog(dialog) != DialogResult.OK)
                            return;
                        directory = picker.SelectedPath;
                        folder.Text = directory;
                    }
                };
                automatic.Click += (sender, args) =>
                {
                    directory = null;
                    folder.Text = defaultFolder;
                };
                layout.Controls.Add(new Label { AutoSize = true, Text = UiText.T("Log folder", "Logmappe") }, 0, 0);
                layout.Controls.Add(folder, 1, 0);
                var folderButtons = new FlowLayoutPanel { AutoSize = true, WrapContents = false };
                folderButtons.Controls.AddRange(new Control[] { browse, automatic });
                layout.Controls.Add(folderButtons, 1, 1);
                layout.Controls.Add(new Label { AutoSize = true, Text = UiText.T("Maximum size per file (MiB)", "Maksimal størrelse pr. fil (MiB)") }, 0, 2);
                layout.Controls.Add(maxSize, 1, 2);
                layout.Controls.Add(new Label { AutoSize = true, Text = UiText.T("Maximum log files (including current)", "Maksimalt antal logfiler (inklusive den aktive)") }, 0, 3);
                layout.Controls.Add(maxFiles, 1, 3);
                var buttons = new FlowLayoutPanel { AutoSize = true, WrapContents = false };
                buttons.Controls.AddRange(new Control[] { save, cancel });
                layout.Controls.Add(buttons, 1, 4);
                dialog.Controls.Add(layout);
                dialog.AcceptButton = save;
                dialog.CancelButton = cancel;
                if (dialog.ShowDialog(owner) != DialogResult.OK)
                    return;
                var updated = new DiagnosticLogSettings(directory, (int)maxSize.Value, (int)maxFiles.Value);
                updated.Save();
                DiagnosticLog.Current.Configure(updated);
            }
        }
    }
}
