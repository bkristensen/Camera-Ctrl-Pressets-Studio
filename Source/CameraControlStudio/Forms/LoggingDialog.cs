using System;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class LoggingDialog : AppForm
    {
        string directory;

        public LoggingDialog()
        {
            InitializeComponent();
            if (InDesigner)
                return;
            FormText.Translate(this);
            var settings = DiagnosticLog.Current.Settings;
            directory = settings.DirectoryPath;
            folder.Text = directory ?? folder.Text;
            maxSize.Value = settings.SizeMiB;
            maxFiles.Value = settings.FileCount;
            string defaultFolder = UiText.T("Automatic (application folder)", "Automatisk (programmappen)");
            toolTips.SetToolTip(browse, browse.Text);
            toolTips.SetToolTip(automatic, defaultFolder);
            toolTips.SetToolTip(save, save.Text);
            toolTips.SetToolTip(cancel, cancel.Text);
            browse.Click += (sender, args) =>
            {
                folderPicker.SelectedPath = directory ?? AppDomain.CurrentDomain.BaseDirectory;
                if (folderPicker.ShowDialog(this) == DialogResult.OK)
                    folder.Text = directory = folderPicker.SelectedPath;
            };
            automatic.Click += (sender, args) =>
            {
                directory = null;
                folder.Text = defaultFolder;
            };
        }

        internal static void ShowSettings(IWin32Window owner)
        {
            using (var dialog = new LoggingDialog())
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK)
                    return;
                var updated = new DiagnosticLogSettings(dialog.directory, (int)dialog.maxSize.Value, (int)dialog.maxFiles.Value);
                updated.Save();
                DiagnosticLog.Current.Configure(updated);
            }
        }
    }
}
