using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace CameraControlStudio
{
    partial class GuideViewer
    {
        private TableLayoutPanel layout;
        private FlowLayoutPanel sidebar;
        private LinkLabel dailyLink;
        private LinkLabel setupLink;
        private LinkLabel developmentLink;
        private LinkLabel buildReleaseLink;
        private LinkLabel releasesLink;
        private LinkLabel onlineGuidesLink;
        private Button print;
        private WebView2 page;
        private PrintDialog printDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                printDialog?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.layout = new TableLayoutPanel();
            this.sidebar = new FlowLayoutPanel();
            this.dailyLink = new LinkLabel();
            this.setupLink = new LinkLabel();
            this.developmentLink = new LinkLabel();
            this.buildReleaseLink = new LinkLabel();
            this.releasesLink = new LinkLabel();
            this.onlineGuidesLink = new LinkLabel();
            this.print = new Button();
            this.page = new WebView2();
            this.printDialog = new PrintDialog();
            this.layout.SuspendLayout();
            this.sidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.page).BeginInit();
            this.SuspendLayout();
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
            this.layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.layout.RowCount = 1;
            this.layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.layout.Controls.Add(this.sidebar, 0, 0);
            this.layout.Controls.Add(this.page, 1, 0);
            this.layout.Dock = DockStyle.Fill;
            this.layout.Name = "layout";
            this.layout.RightToLeft = RightToLeft.No;
            this.sidebar.AutoScroll = true;
            this.sidebar.BackColor = Color.FromArgb(9, 52, 94);
            this.sidebar.Controls.Add(this.dailyLink);
            this.sidebar.Controls.Add(this.setupLink);
            this.sidebar.Controls.Add(this.developmentLink);
            this.sidebar.Controls.Add(this.buildReleaseLink);
            this.sidebar.Controls.Add(this.releasesLink);
            this.sidebar.Controls.Add(this.onlineGuidesLink);
            this.sidebar.Controls.Add(this.print);
            this.sidebar.Dock = DockStyle.Fill;
            this.sidebar.FlowDirection = FlowDirection.TopDown;
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new Padding(10);
            this.sidebar.WrapContents = false;
            this.dailyLink.AutoSize = true;
            this.dailyLink.LinkColor = Color.LightSkyBlue;
            this.dailyLink.ActiveLinkColor = Color.White;
            this.dailyLink.VisitedLinkColor = Color.LightSkyBlue;
            this.dailyLink.MaximumSize = new Size(170, 0);
            this.dailyLink.Name = "dailyLink";
            this.dailyLink.TabIndex = 0;
            this.dailyLink.TabStop = true;
            this.dailyLink.Text = "Daily use";
            this.setupLink.AutoSize = true;
            this.setupLink.LinkColor = Color.LightSkyBlue;
            this.setupLink.ActiveLinkColor = Color.White;
            this.setupLink.VisitedLinkColor = Color.LightSkyBlue;
            this.setupLink.MaximumSize = new Size(170, 0);
            this.setupLink.Name = "setupLink";
            this.setupLink.TabIndex = 1;
            this.setupLink.TabStop = true;
            this.setupLink.Text = "Setup guide";
            this.developmentLink.AutoSize = true;
            this.developmentLink.LinkColor = Color.LightSkyBlue;
            this.developmentLink.ActiveLinkColor = Color.White;
            this.developmentLink.VisitedLinkColor = Color.LightSkyBlue;
            this.developmentLink.MaximumSize = new Size(170, 0);
            this.developmentLink.Name = "developmentLink";
            this.developmentLink.TabIndex = 2;
            this.developmentLink.TabStop = true;
            this.developmentLink.Text = "Development guide";
            this.buildReleaseLink.AutoSize = true;
            this.buildReleaseLink.LinkColor = Color.LightSkyBlue;
            this.buildReleaseLink.ActiveLinkColor = Color.White;
            this.buildReleaseLink.VisitedLinkColor = Color.LightSkyBlue;
            this.buildReleaseLink.MaximumSize = new Size(170, 0);
            this.buildReleaseLink.Name = "buildReleaseLink";
            this.buildReleaseLink.TabIndex = 3;
            this.buildReleaseLink.TabStop = true;
            this.buildReleaseLink.Text = "Build and release guide";
            this.releasesLink.AutoSize = true;
            this.releasesLink.LinkColor = Color.LightSkyBlue;
            this.releasesLink.ActiveLinkColor = Color.White;
            this.releasesLink.VisitedLinkColor = Color.LightSkyBlue;
            this.releasesLink.MaximumSize = new Size(170, 0);
            this.releasesLink.Name = "releasesLink";
            this.releasesLink.TabIndex = 4;
            this.releasesLink.TabStop = true;
            this.releasesLink.Text = "GitHub releases";
            this.onlineGuidesLink.AutoSize = true;
            this.onlineGuidesLink.LinkColor = Color.LightSkyBlue;
            this.onlineGuidesLink.ActiveLinkColor = Color.White;
            this.onlineGuidesLink.VisitedLinkColor = Color.LightSkyBlue;
            this.onlineGuidesLink.MaximumSize = new Size(170, 0);
            this.onlineGuidesLink.Name = "onlineGuidesLink";
            this.onlineGuidesLink.TabIndex = 5;
            this.onlineGuidesLink.TabStop = true;
            this.onlineGuidesLink.Text = "User guides on GitHub";
            this.print.AutoSize = true;
            this.print.BackColor = SystemColors.Control;
            this.print.ForeColor = SystemColors.ControlText;
            this.print.UseVisualStyleBackColor = true;
            this.print.Name = "print";
            this.print.TabIndex = 6;
            this.print.Text = "Print";
            this.page.AllowExternalDrop = false;
            this.page.Dock = DockStyle.Fill;
            this.page.Name = "page";
            this.page.TabIndex = 1;
            this.printDialog.UseEXDialog = true;
            this.ClientSize = new Size(940, 650);
            this.Controls.Add(this.layout);
            this.MinimumSize = new Size(590, 360);
            this.Name = "GuideViewer";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "User guides";
            ((System.ComponentModel.ISupportInitialize)this.page).EndInit();
            this.sidebar.ResumeLayout(false);
            this.sidebar.PerformLayout();
            this.layout.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
