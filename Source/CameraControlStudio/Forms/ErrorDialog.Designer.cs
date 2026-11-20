using System.Drawing;
using System.Windows.Forms;

namespace CameraControlStudio
{
    partial class ErrorDialog
    {
        private FlowLayoutPanel content;
        private FlowLayoutPanel buttons;
        private Label messageLabel;
        private Label detailLabel;
        private Button report;
        private Button showLog;
        private Button close;

        private void InitializeComponent()
        {
            this.content = new FlowLayoutPanel();
            this.buttons = new FlowLayoutPanel();
            this.messageLabel = new Label();
            this.detailLabel = new Label();
            this.report = new Button();
            this.showLog = new Button();
            this.close = new Button();
            this.content.SuspendLayout();
            this.buttons.SuspendLayout();
            this.SuspendLayout();
            this.content.AutoSize = true;
            this.content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.content.Dock = DockStyle.Fill;
            this.content.FlowDirection = FlowDirection.TopDown;
            this.content.Padding = new Padding(14);
            this.content.WrapContents = false;
            this.content.Name = "content";
            this.content.Controls.Add(this.messageLabel);
            this.content.Controls.Add(this.detailLabel);
            this.content.Controls.Add(this.buttons);
            this.messageLabel.AutoSize = true;
            this.messageLabel.MaximumSize = new Size(440, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Text = "An unexpected error prevented this action.";
            this.detailLabel.AutoSize = true;
            this.detailLabel.MaximumSize = new Size(440, 0);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Text = "Review the diagnostic log before attaching it to a public issue.";
            this.buttons.AutoSize = true;
            this.buttons.WrapContents = false;
            this.buttons.Name = "buttons";
            this.buttons.Controls.Add(this.report);
            this.buttons.Controls.Add(this.showLog);
            this.buttons.Controls.Add(this.close);
            this.report.AutoSize = true;
            this.report.BackColor = SystemColors.Control;
            this.report.ForeColor = SystemColors.ControlText;
            this.report.UseVisualStyleBackColor = true;
            this.report.Name = "report";
            this.report.TabIndex = 0;
            this.report.Text = "Report on GitHub";
            this.showLog.AutoSize = true;
            this.showLog.BackColor = SystemColors.Control;
            this.showLog.ForeColor = SystemColors.ControlText;
            this.showLog.UseVisualStyleBackColor = true;
            this.showLog.Name = "showLog";
            this.showLog.TabIndex = 1;
            this.showLog.Text = "Show log file";
            this.close.AutoSize = true;
            this.close.BackColor = SystemColors.Control;
            this.close.ForeColor = SystemColors.ControlText;
            this.close.UseVisualStyleBackColor = true;
            this.close.DialogResult = DialogResult.OK;
            this.close.Name = "close";
            this.close.TabIndex = 2;
            this.close.Text = "Close";
            this.AcceptButton = this.close;
            this.CancelButton = this.close;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ClientSize = new Size(460, 120);
            this.Controls.Add(this.content);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorDialog";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Camera Control Studio";
            this.buttons.ResumeLayout(false);
            this.buttons.PerformLayout();
            this.content.ResumeLayout(false);
            this.content.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
