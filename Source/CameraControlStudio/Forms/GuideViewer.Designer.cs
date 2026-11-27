using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace CameraControlStudio
{
    partial class GuideViewer
    {
        private TableLayoutPanel layout;
        private Panel sidebarHost;
        private FlowLayoutPanel sidebar;
        private Label guidesHeading;
        private Label guideLanguageLabel;
        private ListBox guideLanguage;
        private CheckBox useOnlineGuides;
        private Label developmentHeading;
        private Label linksHeading;
        private LinkLabel translationsLink;
        private LinkLabel githubLink;
        private LinkLabel issuesLink;
        private LinkLabel dailyLink;
        private LinkLabel setupLink;
        private LinkLabel troubleshootingLink;
        private LinkLabel bugGuideLink;
        private LinkLabel featureGuideLink;
        private LinkLabel developmentLink;
        private LinkLabel buildReleaseLink;
        private GitHubActions githubActions;
        private BlueButton print;
        private WebView2 page;
        private PrintDialog printDialog;
        private SaveFileDialog savePdfDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                printDialog?.Dispose();
                savePdfDialog?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuideViewer));
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.sidebarHost = new System.Windows.Forms.Panel();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.guideLanguageLabel = new System.Windows.Forms.Label();
            this.guideLanguage = new System.Windows.Forms.ListBox();
            this.useOnlineGuides = new System.Windows.Forms.CheckBox();
            this.guidesHeading = new System.Windows.Forms.Label();
            this.dailyLink = new System.Windows.Forms.LinkLabel();
            this.setupLink = new System.Windows.Forms.LinkLabel();
            this.troubleshootingLink = new System.Windows.Forms.LinkLabel();
            this.bugGuideLink = new System.Windows.Forms.LinkLabel();
            this.featureGuideLink = new System.Windows.Forms.LinkLabel();
            this.linksHeading = new System.Windows.Forms.Label();
            this.githubActions = new CameraControlStudio.GitHubActions();
            this.developmentHeading = new System.Windows.Forms.Label();
            this.developmentLink = new System.Windows.Forms.LinkLabel();
            this.buildReleaseLink = new System.Windows.Forms.LinkLabel();
            this.translationsLink = new System.Windows.Forms.LinkLabel();
            this.githubLink = new System.Windows.Forms.LinkLabel();
            this.issuesLink = new System.Windows.Forms.LinkLabel();
            this.print = new BlueButton();
            this.page = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.savePdfDialog = new System.Windows.Forms.SaveFileDialog();
            this.layout.SuspendLayout();
            this.sidebarHost.SuspendLayout();
            this.sidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.page)).BeginInit();
            this.SuspendLayout();
            //
            // layout
            //
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Controls.Add(this.sidebarHost, 0, 0);
            this.layout.Controls.Add(this.page, 1, 0);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.layout.RowCount = 1;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Size = new System.Drawing.Size(940, 813);
            this.layout.TabIndex = 0;
            //
            // sidebarHost
            //
            this.sidebarHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.sidebarHost.Controls.Add(this.sidebar);
            this.sidebarHost.Controls.Add(this.print);
            this.sidebarHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebarHost.Location = new System.Drawing.Point(3, 3);
            this.sidebarHost.Name = "sidebarHost";
            this.sidebarHost.Size = new System.Drawing.Size(244, 807);
            this.sidebarHost.TabIndex = 0;
            //
            // sidebar
            //
            this.sidebar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sidebar.AutoScroll = true;
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.sidebar.Controls.Add(this.guideLanguageLabel);
            this.sidebar.Controls.Add(this.guideLanguage);
            this.sidebar.Controls.Add(this.useOnlineGuides);
            this.sidebar.Controls.Add(this.guidesHeading);
            this.sidebar.Controls.Add(this.dailyLink);
            this.sidebar.Controls.Add(this.setupLink);
            this.sidebar.Controls.Add(this.troubleshootingLink);
            this.sidebar.Controls.Add(this.bugGuideLink);
            this.sidebar.Controls.Add(this.featureGuideLink);
            this.sidebar.Controls.Add(this.linksHeading);
            this.sidebar.Controls.Add(this.githubActions);
            this.sidebar.Controls.Add(this.developmentHeading);
            this.sidebar.Controls.Add(this.developmentLink);
            this.sidebar.Controls.Add(this.buildReleaseLink);
            this.sidebar.Controls.Add(this.translationsLink);
            this.sidebar.Controls.Add(this.githubLink);
            this.sidebar.Controls.Add(this.issuesLink);
            this.sidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new System.Windows.Forms.Padding(10);
            this.sidebar.Size = new System.Drawing.Size(244, 757);
            this.sidebar.TabIndex = 0;
            this.sidebar.WrapContents = false;
            //
            // guideLanguageLabel
            //
            this.guideLanguageLabel.AutoSize = true;
            this.guideLanguageLabel.ForeColor = System.Drawing.Color.White;
            this.guideLanguageLabel.Location = new System.Drawing.Point(13, 10);
            this.guideLanguageLabel.Name = "guideLanguageLabel";
            this.guideLanguageLabel.Size = new System.Drawing.Size(114, 20);
            this.guideLanguageLabel.TabIndex = 0;
            this.guideLanguageLabel.Text = "Guide language";
            //
            // guideLanguage
            //
            this.guideLanguage.AccessibleName = "Guide language";
            this.guideLanguage.BackColor = System.Drawing.Color.White;
            this.guideLanguage.ForeColor = System.Drawing.Color.Black;
            this.guideLanguage.IntegralHeight = false;
            this.guideLanguage.ItemHeight = 20;
            this.guideLanguage.Location = new System.Drawing.Point(13, 33);
            this.guideLanguage.Name = "guideLanguage";
            this.guideLanguage.Size = new System.Drawing.Size(170, 81);
            this.guideLanguage.TabIndex = 0;
            //
            // useOnlineGuides
            //
            this.useOnlineGuides.AutoSize = true;
            this.useOnlineGuides.ForeColor = System.Drawing.Color.White;
            this.useOnlineGuides.Location = new System.Drawing.Point(13, 120);
            this.useOnlineGuides.MaximumSize = new System.Drawing.Size(170, 0);
            this.useOnlineGuides.Name = "useOnlineGuides";
            this.useOnlineGuides.Size = new System.Drawing.Size(148, 24);
            this.useOnlineGuides.TabIndex = 1;
            this.useOnlineGuides.Text = "Use online guides";
            this.useOnlineGuides.UseVisualStyleBackColor = false;
            //
            // guidesHeading
            //
            this.guidesHeading.AutoSize = true;
            this.guidesHeading.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guidesHeading.ForeColor = System.Drawing.Color.White;
            this.guidesHeading.Location = new System.Drawing.Point(13, 159);
            this.guidesHeading.Margin = new System.Windows.Forms.Padding(3, 12, 3, 5);
            this.guidesHeading.Name = "guidesHeading";
            this.guidesHeading.Size = new System.Drawing.Size(57, 20);
            this.guidesHeading.TabIndex = 2;
            this.guidesHeading.Text = "Guides";
            //
            // dailyLink
            //
            this.dailyLink.ActiveLinkColor = System.Drawing.Color.White;
            this.dailyLink.AutoSize = true;
            this.dailyLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.dailyLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.dailyLink.Location = new System.Drawing.Point(13, 184);
            this.dailyLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.dailyLink.Name = "dailyLink";
            this.dailyLink.Size = new System.Drawing.Size(69, 20);
            this.dailyLink.TabIndex = 2;
            this.dailyLink.TabStop = true;
            this.dailyLink.Text = "Daily use";
            this.dailyLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // setupLink
            //
            this.setupLink.ActiveLinkColor = System.Drawing.Color.White;
            this.setupLink.AutoSize = true;
            this.setupLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.setupLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.setupLink.Location = new System.Drawing.Point(13, 204);
            this.setupLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.setupLink.Name = "setupLink";
            this.setupLink.Size = new System.Drawing.Size(89, 20);
            this.setupLink.TabIndex = 3;
            this.setupLink.TabStop = true;
            this.setupLink.Text = "Setup guide";
            this.setupLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // troubleshootingLink
            //
            this.troubleshootingLink.ActiveLinkColor = System.Drawing.Color.White;
            this.troubleshootingLink.AutoSize = true;
            this.troubleshootingLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.troubleshootingLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.troubleshootingLink.Location = new System.Drawing.Point(13, 224);
            this.troubleshootingLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.troubleshootingLink.Name = "troubleshootingLink";
            this.troubleshootingLink.Size = new System.Drawing.Size(117, 20);
            this.troubleshootingLink.TabIndex = 4;
            this.troubleshootingLink.TabStop = true;
            this.troubleshootingLink.Text = "Troubleshooting";
            this.troubleshootingLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // bugGuideLink
            //
            this.bugGuideLink.ActiveLinkColor = System.Drawing.Color.White;
            this.bugGuideLink.AutoSize = true;
            this.bugGuideLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.bugGuideLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.bugGuideLink.Location = new System.Drawing.Point(13, 244);
            this.bugGuideLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.bugGuideLink.Name = "bugGuideLink";
            this.bugGuideLink.Size = new System.Drawing.Size(96, 20);
            this.bugGuideLink.TabIndex = 5;
            this.bugGuideLink.TabStop = true;
            this.bugGuideLink.Text = "Report a bug";
            this.bugGuideLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // featureGuideLink
            //
            this.featureGuideLink.ActiveLinkColor = System.Drawing.Color.White;
            this.featureGuideLink.AutoSize = true;
            this.featureGuideLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.featureGuideLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.featureGuideLink.Location = new System.Drawing.Point(13, 264);
            this.featureGuideLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.featureGuideLink.Name = "featureGuideLink";
            this.featureGuideLink.Size = new System.Drawing.Size(125, 20);
            this.featureGuideLink.TabIndex = 6;
            this.featureGuideLink.TabStop = true;
            this.featureGuideLink.Text = "Request a feature";
            this.featureGuideLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // linksHeading
            //
            this.linksHeading.AutoSize = true;
            this.linksHeading.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.linksHeading.ForeColor = System.Drawing.Color.White;
            this.linksHeading.Location = new System.Drawing.Point(13, 300);
            this.linksHeading.Margin = new System.Windows.Forms.Padding(3, 16, 3, 5);
            this.linksHeading.Name = "linksHeading";
            this.linksHeading.Size = new System.Drawing.Size(59, 20);
            this.linksHeading.TabIndex = 7;
            this.linksHeading.Text = "GitHub";
            //
            // githubActions
            //
            this.githubActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.githubActions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.githubActions.ForeColor = System.Drawing.Color.White;
            this.githubActions.Location = new System.Drawing.Point(13, 328);
            this.githubActions.Name = "githubActions";
            this.githubActions.Size = new System.Drawing.Size(210, 246);
            this.githubActions.TabIndex = 7;
            //
            // developmentHeading
            //
            this.developmentHeading.AutoSize = true;
            this.developmentHeading.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.developmentHeading.ForeColor = System.Drawing.Color.White;
            this.developmentHeading.Location = new System.Drawing.Point(13, 593);
            this.developmentHeading.Margin = new System.Windows.Forms.Padding(3, 16, 3, 5);
            this.developmentHeading.Name = "developmentHeading";
            this.developmentHeading.Size = new System.Drawing.Size(153, 20);
            this.developmentHeading.TabIndex = 8;
            this.developmentHeading.Text = "Development guides";
            //
            // developmentLink
            //
            this.developmentLink.ActiveLinkColor = System.Drawing.Color.White;
            this.developmentLink.AutoSize = true;
            this.developmentLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.developmentLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.developmentLink.Location = new System.Drawing.Point(13, 618);
            this.developmentLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.developmentLink.Name = "developmentLink";
            this.developmentLink.Size = new System.Drawing.Size(141, 20);
            this.developmentLink.TabIndex = 8;
            this.developmentLink.TabStop = true;
            this.developmentLink.Text = "Development guide";
            this.developmentLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // buildReleaseLink
            //
            this.buildReleaseLink.ActiveLinkColor = System.Drawing.Color.White;
            this.buildReleaseLink.AutoSize = true;
            this.buildReleaseLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.buildReleaseLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.buildReleaseLink.Location = new System.Drawing.Point(13, 638);
            this.buildReleaseLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.buildReleaseLink.Name = "buildReleaseLink";
            this.buildReleaseLink.Size = new System.Drawing.Size(165, 20);
            this.buildReleaseLink.TabIndex = 9;
            this.buildReleaseLink.TabStop = true;
            this.buildReleaseLink.Text = "Build and release guide";
            this.buildReleaseLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // translationsLink
            //
            this.translationsLink.ActiveLinkColor = System.Drawing.Color.White;
            this.translationsLink.AutoSize = true;
            this.translationsLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.translationsLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.translationsLink.Location = new System.Drawing.Point(13, 658);
            this.translationsLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.translationsLink.Name = "translationsLink";
            this.translationsLink.Size = new System.Drawing.Size(123, 20);
            this.translationsLink.TabIndex = 10;
            this.translationsLink.TabStop = true;
            this.translationsLink.Text = "Translate the app";
            this.translationsLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // githubLink
            //
            this.githubLink.ActiveLinkColor = System.Drawing.Color.White;
            this.githubLink.AutoSize = true;
            this.githubLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.githubLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.githubLink.Location = new System.Drawing.Point(13, 678);
            this.githubLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(149, 20);
            this.githubLink.TabIndex = 11;
            this.githubLink.TabStop = true;
            this.githubLink.Text = "GitHub project guide";
            this.githubLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // issuesLink
            //
            this.issuesLink.ActiveLinkColor = System.Drawing.Color.White;
            this.issuesLink.AutoSize = true;
            this.issuesLink.DisabledLinkColor = System.Drawing.Color.LightSkyBlue;
            this.issuesLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.issuesLink.Location = new System.Drawing.Point(13, 698);
            this.issuesLink.MaximumSize = new System.Drawing.Size(170, 0);
            this.issuesLink.Name = "issuesLink";
            this.issuesLink.Size = new System.Drawing.Size(142, 40);
            this.issuesLink.TabIndex = 12;
            this.issuesLink.TabStop = true;
            this.issuesLink.Text = "Report or follow an issue";
            this.issuesLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            //
            // print
            //
            this.print.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.print.Location = new System.Drawing.Point(10, 767);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(184, 30);
            this.print.TabIndex = 1;
            this.print.Text = "🖨  Print";
            this.print.UseVisualStyleBackColor = false;
            //
            // page
            //
            this.page.AllowExternalDrop = false;
            this.page.CreationProperties = null;
            this.page.DefaultBackgroundColor = System.Drawing.Color.White;
            this.page.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page.Location = new System.Drawing.Point(253, 3);
            this.page.Name = "page";
            this.page.Size = new System.Drawing.Size(684, 807);
            this.page.TabIndex = 1;
            this.page.ZoomFactor = 1D;
            //
            // printDialog
            //
            this.printDialog.UseEXDialog = true;
            //
            // savePdfDialog
            //
            this.savePdfDialog.DefaultExt = "pdf";
            this.savePdfDialog.Filter = "PDF (*.pdf)|*.pdf";
            this.savePdfDialog.RestoreDirectory = true;
            //
            // GuideViewer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.ClientSize = new System.Drawing.Size(940, 813);
            this.Controls.Add(this.layout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(590, 360);
            this.Name = "GuideViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User guides";
            this.layout.ResumeLayout(false);
            this.sidebarHost.ResumeLayout(false);
            this.sidebar.ResumeLayout(false);
            this.sidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.page)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
