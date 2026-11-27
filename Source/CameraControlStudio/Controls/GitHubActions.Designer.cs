using System.Drawing;
using System.Windows.Forms;

namespace CameraControlStudio
{
    partial class GitHubActions
    {
        private TableLayoutPanel actions;
        private Button reportIssue;
        private Button requestFeature;
        private Button savePackage;
        private LinkLabel repository;
        private LinkLabel releases;
        private LinkLabel discussions;
        private Label reviewNote;
        private SaveFileDialog packageDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                packageDialog?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.actions = new TableLayoutPanel();
            this.reportIssue = new Button();
            this.requestFeature = new Button();
            this.savePackage = new Button();
            this.repository = new LinkLabel();
            this.releases = new LinkLabel();
            this.discussions = new LinkLabel();
            this.reviewNote = new Label();
            this.packageDialog = new SaveFileDialog();
            this.actions.SuspendLayout();
            this.SuspendLayout();
            this.actions.ColumnCount = 1;
            this.actions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.actions.Controls.Add(this.reportIssue, 0, 0);
            this.actions.Controls.Add(this.requestFeature, 0, 1);
            this.actions.Controls.Add(this.savePackage, 0, 2);
            this.actions.Controls.Add(this.repository, 0, 3);
            this.actions.Controls.Add(this.releases, 0, 4);
            this.actions.Controls.Add(this.discussions, 0, 5);
            this.actions.Controls.Add(this.reviewNote, 0, 6);
            this.actions.Dock = DockStyle.Fill;
            this.actions.Name = "actions";
            this.actions.RowCount = 7;
            this.actions.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.actions.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.actions.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.actions.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.actions.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.actions.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.actions.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.reportIssue.AutoSize = true;
            this.reportIssue.Dock = DockStyle.Fill;
            this.reportIssue.FlatStyle = FlatStyle.Flat;
            this.reportIssue.MinimumSize = new Size(0, 30);
            this.reportIssue.Name = "reportIssue";
            this.reportIssue.TabIndex = 0;
            this.reportIssue.Text = "Report an issue";
            this.reportIssue.UseVisualStyleBackColor = false;
            this.requestFeature.AutoSize = true;
            this.requestFeature.Dock = DockStyle.Fill;
            this.requestFeature.FlatStyle = FlatStyle.Flat;
            this.requestFeature.MinimumSize = new Size(0, 30);
            this.requestFeature.Name = "requestFeature";
            this.requestFeature.TabIndex = 1;
            this.requestFeature.Text = "Request a new feature";
            this.requestFeature.UseVisualStyleBackColor = false;
            this.savePackage.AutoSize = true;
            this.savePackage.Dock = DockStyle.Fill;
            this.savePackage.FlatStyle = FlatStyle.Flat;
            this.savePackage.MinimumSize = new Size(0, 30);
            this.savePackage.Name = "savePackage";
            this.savePackage.TabIndex = 2;
            this.savePackage.Text = "Save support ZIP";
            this.savePackage.UseVisualStyleBackColor = false;
            this.reviewNote.AutoSize = true;
            this.reviewNote.Margin = new Padding(3, 4, 3, 8);
            this.reviewNote.MaximumSize = new Size(162, 0);
            this.reviewNote.Name = "reviewNote";
            this.reviewNote.Text = "Review the screenshot, settings and logs before sharing the ZIP.";
            this.repository.ActiveLinkColor = Color.White;
            this.repository.AutoSize = true;
            this.repository.LinkColor = Color.LightSkyBlue;
            this.repository.Margin = new Padding(3, 2, 3, 2);
            this.repository.MaximumSize = new Size(162, 0);
            this.repository.Name = "repository";
            this.repository.TabIndex = 3;
            this.repository.TabStop = true;
            this.repository.Text = "Project on GitHub";
            this.repository.VisitedLinkColor = Color.LightSkyBlue;
            this.releases.ActiveLinkColor = Color.White;
            this.releases.AutoSize = true;
            this.releases.LinkColor = Color.LightSkyBlue;
            this.releases.Margin = new Padding(3, 2, 3, 2);
            this.releases.MaximumSize = new Size(162, 0);
            this.releases.Name = "releases";
            this.releases.TabIndex = 4;
            this.releases.TabStop = true;
            this.releases.Text = "GitHub releases";
            this.releases.VisitedLinkColor = Color.LightSkyBlue;
            this.discussions.ActiveLinkColor = Color.White;
            this.discussions.AutoSize = true;
            this.discussions.LinkColor = Color.LightSkyBlue;
            this.discussions.Margin = new Padding(3, 2, 3, 2);
            this.discussions.MaximumSize = new Size(162, 0);
            this.discussions.Name = "discussions";
            this.discussions.TabIndex = 5;
            this.discussions.TabStop = true;
            this.discussions.Text = "Join the project discussion";
            this.discussions.VisitedLinkColor = Color.LightSkyBlue;
            this.packageDialog.DefaultExt = "zip";
            this.packageDialog.Filter = "ZIP (*.zip)|*.zip";
            this.packageDialog.RestoreDirectory = true;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(9, 52, 94);
            this.Controls.Add(this.actions);
            this.Font = new Font("Segoe UI", 9F);
            this.ForeColor = Color.White;
            this.Name = "GitHubActions";
            this.Size = new Size(170, 266);
            this.actions.ResumeLayout(false);
            this.actions.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
