using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CameraControlStudio
{
    partial class LoggingDialog
    {
        private IContainer components;
        private TableLayoutPanel layout;
        private Label folderLabel;
        private TextBox folder;
        private FlowLayoutPanel folderButtons;
        private Button browse;
        private Button automatic;
        private Label sizeLabel;
        private NumericUpDown maxSize;
        private Label filesLabel;
        private NumericUpDown maxFiles;
        private FlowLayoutPanel buttons;
        private Button save;
        private Button cancel;
        private ToolTip toolTips;
        private FolderBrowserDialog folderPicker;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                folderPicker?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggingDialog));
            this.components = new Container();
            this.layout = new TableLayoutPanel();
            this.folderLabel = new Label();
            this.folder = new TextBox();
            this.folderButtons = new FlowLayoutPanel();
            this.browse = new Button();
            this.automatic = new Button();
            this.sizeLabel = new Label();
            this.maxSize = new NumericUpDown();
            this.filesLabel = new Label();
            this.maxFiles = new NumericUpDown();
            this.buttons = new FlowLayoutPanel();
            this.save = new Button();
            this.cancel = new Button();
            this.toolTips = new ToolTip(this.components);
            this.folderPicker = new FolderBrowserDialog();
            this.layout.SuspendLayout();
            this.folderButtons.SuspendLayout();
            ((ISupportInitialize)this.maxSize).BeginInit();
            ((ISupportInitialize)this.maxFiles).BeginInit();
            this.buttons.SuspendLayout();
            this.SuspendLayout();
            this.layout.AutoSize = true;
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.layout.RowCount = 5;
            this.layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.layout.Controls.Add(this.folderLabel, 0, 0);
            this.layout.Controls.Add(this.folder, 1, 0);
            this.layout.Controls.Add(this.folderButtons, 1, 1);
            this.layout.Controls.Add(this.sizeLabel, 0, 2);
            this.layout.Controls.Add(this.maxSize, 1, 2);
            this.layout.Controls.Add(this.filesLabel, 0, 3);
            this.layout.Controls.Add(this.maxFiles, 1, 3);
            this.layout.Controls.Add(this.buttons, 1, 4);
            this.layout.Dock = DockStyle.Fill;
            this.layout.Name = "layout";
            this.layout.Padding = new Padding(12);
            this.folderLabel.Anchor = AnchorStyles.Left;
            this.folderLabel.AutoSize = true;
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Text = "Log folder";
            this.folder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.folder.Name = "folder";
            this.folder.BackColor = SystemColors.Window;
            this.folder.ForeColor = SystemColors.WindowText;
            this.folder.ReadOnly = true;
            this.folder.Size = new Size(355, 24);
            this.folder.TabIndex = 0;
            this.folder.Text = "Automatic (application folder)";
            this.folderButtons.AutoSize = true;
            this.folderButtons.Controls.Add(this.browse);
            this.folderButtons.Controls.Add(this.automatic);
            this.folderButtons.Name = "folderButtons";
            this.folderButtons.TabIndex = 1;
            this.folderButtons.WrapContents = false;
            this.browse.AutoSize = true;
            this.browse.BackColor = SystemColors.Control;
            this.browse.ForeColor = SystemColors.ControlText;
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Name = "browse";
            this.browse.TabIndex = 0;
            this.browse.Text = "Choose folder";
            this.automatic.AutoSize = true;
            this.automatic.BackColor = SystemColors.Control;
            this.automatic.ForeColor = SystemColors.ControlText;
            this.automatic.UseVisualStyleBackColor = true;
            this.automatic.Name = "automatic";
            this.automatic.TabIndex = 1;
            this.automatic.Text = "Automatic";
            this.sizeLabel.Anchor = AnchorStyles.Left;
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Text = "Maximum size per file (MiB)";
            this.maxSize.Minimum = 1;
            this.maxSize.Maximum = 100;
            this.maxSize.Name = "maxSize";
            this.maxSize.BackColor = SystemColors.Window;
            this.maxSize.ForeColor = SystemColors.WindowText;
            this.maxSize.TabIndex = 2;
            this.maxSize.Value = 1;
            this.filesLabel.Anchor = AnchorStyles.Left;
            this.filesLabel.AutoSize = true;
            this.filesLabel.Name = "filesLabel";
            this.filesLabel.Text = "Maximum log files (including current)";
            this.maxFiles.Minimum = 1;
            this.maxFiles.Maximum = 20;
            this.maxFiles.Name = "maxFiles";
            this.maxFiles.BackColor = SystemColors.Window;
            this.maxFiles.ForeColor = SystemColors.WindowText;
            this.maxFiles.TabIndex = 3;
            this.maxFiles.Value = 2;
            this.buttons.AutoSize = true;
            this.buttons.Controls.Add(this.save);
            this.buttons.Controls.Add(this.cancel);
            this.buttons.Name = "buttons";
            this.buttons.TabIndex = 4;
            this.buttons.WrapContents = false;
            this.save.AutoSize = true;
            this.save.BackColor = SystemColors.Control;
            this.save.ForeColor = SystemColors.ControlText;
            this.save.UseVisualStyleBackColor = true;
            this.save.DialogResult = DialogResult.OK;
            this.save.Name = "save";
            this.save.TabIndex = 0;
            this.save.Text = "Save";
            this.cancel.AutoSize = true;
            this.cancel.BackColor = SystemColors.Control;
            this.cancel.ForeColor = SystemColors.ControlText;
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.DialogResult = DialogResult.Cancel;
            this.cancel.Name = "cancel";
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.AcceptButton = this.save;
            this.CancelButton = this.cancel;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ClientSize = new Size(625, 180);
            this.Controls.Add(this.layout);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoggingDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Log settings";
            ((ISupportInitialize)this.maxSize).EndInit();
            ((ISupportInitialize)this.maxFiles).EndInit();
            this.folderButtons.ResumeLayout(false);
            this.folderButtons.PerformLayout();
            this.buttons.ResumeLayout(false);
            this.buttons.PerformLayout();
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
