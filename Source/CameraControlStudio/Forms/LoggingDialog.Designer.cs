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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggingDialog));
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.folderLabel = new System.Windows.Forms.Label();
            this.folder = new System.Windows.Forms.TextBox();
            this.folderButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.browse = new System.Windows.Forms.Button();
            this.automatic = new System.Windows.Forms.Button();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.maxSize = new System.Windows.Forms.NumericUpDown();
            this.filesLabel = new System.Windows.Forms.Label();
            this.maxFiles = new System.Windows.Forms.NumericUpDown();
            this.buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.folderPicker = new System.Windows.Forms.FolderBrowserDialog();
            this.layout.SuspendLayout();
            this.folderButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxFiles)).BeginInit();
            this.buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.AutoSize = true;
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Controls.Add(this.folderLabel, 0, 0);
            this.layout.Controls.Add(this.folder, 1, 0);
            this.layout.Controls.Add(this.folderButtons, 1, 1);
            this.layout.Controls.Add(this.sizeLabel, 0, 2);
            this.layout.Controls.Add(this.maxSize, 1, 2);
            this.layout.Controls.Add(this.filesLabel, 0, 3);
            this.layout.Controls.Add(this.maxFiles, 1, 3);
            this.layout.Controls.Add(this.buttons, 1, 4);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Padding = new System.Windows.Forms.Padding(12);
            this.layout.RowCount = 5;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.Size = new System.Drawing.Size(625, 216);
            this.layout.TabIndex = 0;
            // 
            // folderLabel
            // 
            this.folderLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.folderLabel.AutoSize = true;
            this.folderLabel.Location = new System.Drawing.Point(15, 18);
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Size = new System.Drawing.Size(78, 20);
            this.folderLabel.TabIndex = 0;
            this.folderLabel.Text = "Log folder";
            // 
            // folder
            // 
            this.folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.folder.BackColor = System.Drawing.SystemColors.Window;
            this.folder.ForeColor = System.Drawing.SystemColors.WindowText;
            this.folder.Location = new System.Drawing.Point(278, 15);
            this.folder.Name = "folder";
            this.folder.ReadOnly = true;
            this.folder.Size = new System.Drawing.Size(332, 27);
            this.folder.TabIndex = 0;
            this.folder.Text = "Automatic (application folder)";
            // 
            // folderButtons
            // 
            this.folderButtons.AutoSize = true;
            this.folderButtons.Controls.Add(this.browse);
            this.folderButtons.Controls.Add(this.automatic);
            this.folderButtons.Location = new System.Drawing.Point(278, 48);
            this.folderButtons.Name = "folderButtons";
            this.folderButtons.Size = new System.Drawing.Size(212, 36);
            this.folderButtons.TabIndex = 1;
            this.folderButtons.WrapContents = false;
            // 
            // browse
            // 
            this.browse.AutoSize = true;
            this.browse.BackColor = System.Drawing.SystemColors.Control;
            this.browse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.browse.Location = new System.Drawing.Point(3, 3);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(112, 30);
            this.browse.TabIndex = 0;
            this.browse.Text = "Choose folder";
            this.browse.UseVisualStyleBackColor = true;
            // 
            // automatic
            // 
            this.automatic.AutoSize = true;
            this.automatic.BackColor = System.Drawing.SystemColors.Control;
            this.automatic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.automatic.Location = new System.Drawing.Point(121, 3);
            this.automatic.Name = "automatic";
            this.automatic.Size = new System.Drawing.Size(88, 30);
            this.automatic.TabIndex = 1;
            this.automatic.Text = "Automatic";
            this.automatic.UseVisualStyleBackColor = true;
            // 
            // sizeLabel
            // 
            this.sizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(15, 93);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(195, 20);
            this.sizeLabel.TabIndex = 2;
            this.sizeLabel.Text = "Maximum size per file (MiB)";
            // 
            // maxSize
            // 
            this.maxSize.BackColor = System.Drawing.SystemColors.Window;
            this.maxSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maxSize.Location = new System.Drawing.Point(278, 90);
            this.maxSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxSize.Name = "maxSize";
            this.maxSize.Size = new System.Drawing.Size(120, 27);
            this.maxSize.TabIndex = 2;
            this.maxSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // filesLabel
            // 
            this.filesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.filesLabel.AutoSize = true;
            this.filesLabel.Location = new System.Drawing.Point(15, 126);
            this.filesLabel.Name = "filesLabel";
            this.filesLabel.Size = new System.Drawing.Size(257, 20);
            this.filesLabel.TabIndex = 3;
            this.filesLabel.Text = "Maximum log files (including current)";
            // 
            // maxFiles
            // 
            this.maxFiles.BackColor = System.Drawing.SystemColors.Window;
            this.maxFiles.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maxFiles.Location = new System.Drawing.Point(278, 123);
            this.maxFiles.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.maxFiles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxFiles.Name = "maxFiles";
            this.maxFiles.Size = new System.Drawing.Size(120, 27);
            this.maxFiles.TabIndex = 3;
            this.maxFiles.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // buttons
            // 
            this.buttons.AutoSize = true;
            this.buttons.Controls.Add(this.save);
            this.buttons.Controls.Add(this.cancel);
            this.buttons.Location = new System.Drawing.Point(278, 156);
            this.buttons.Name = "buttons";
            this.buttons.Size = new System.Drawing.Size(162, 36);
            this.buttons.TabIndex = 4;
            this.buttons.WrapContents = false;
            // 
            // save
            // 
            this.save.AutoSize = true;
            this.save.BackColor = System.Drawing.SystemColors.Control;
            this.save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.save.Location = new System.Drawing.Point(3, 3);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 30);
            this.save.TabIndex = 0;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.AutoSize = true;
            this.cancel.BackColor = System.Drawing.SystemColors.Control;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cancel.Location = new System.Drawing.Point(84, 3);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 30);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // LoggingDialog
            // 
            this.AcceptButton = this.save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(625, 216);
            this.Controls.Add(this.layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoggingDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log settings";
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.folderButtons.ResumeLayout(false);
            this.folderButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxFiles)).EndInit();
            this.buttons.ResumeLayout(false);
            this.buttons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
