using System;
using System.Drawing;
using System.Windows.Forms;
namespace CameraControlStudio {
    partial class MainForm {
        private System.ComponentModel.IContainer components;
        protected override void Dispose(bool disposing) {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.mainToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.root = new System.Windows.Forms.TableLayoutPanel();
            this.body = new System.Windows.Forms.TableLayoutPanel();
            this.mainSplit = new CameraControlStudio.MainViewSplitter();
            this.motionPanel = new System.Windows.Forms.TableLayoutPanel();
            this.title = new System.Windows.Forms.Label();
            this.wheel = new CameraControlStudio.PanWheel();
            this.zoomPanel = new System.Windows.Forms.TableLayoutPanel();
            this.normalIn = new CameraControlStudio.ZoomButton();
            this.zoomSlider = new CameraControlStudio.ZoomSlider();
            this.normalOut = new CameraControlStudio.ZoomButton();
            this.presetPanel = new System.Windows.Forms.TableLayoutPanel();
            this.presetHeader = new System.Windows.Forms.TableLayoutPanel();
            this.presetTitle = new System.Windows.Forms.Label();
            this.deleteTemporaryButton = new CameraControlStudio.BlueButton();
            this.temporaryButton = new CameraControlStudio.BlueButton();
            this.presetGrid = new System.Windows.Forms.DataGridView();
            this.settingsTabs = new System.Windows.Forms.TabControl();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.generalFields = new System.Windows.Forms.TableLayoutPanel();
            this.panStepsLabel = new System.Windows.Forms.Label();
            this.panSteps = new System.Windows.Forms.NumericUpDown();
            this.fastPanStepsLabel = new System.Windows.Forms.Label();
            this.fastPanSteps = new System.Windows.Forms.NumericUpDown();
            this.zoomStepsLabel = new System.Windows.Forms.Label();
            this.zoomSteps = new System.Windows.Forms.NumericUpDown();
            this.fastZoomStepsLabel = new System.Windows.Forms.Label();
            this.fastZoomSteps = new System.Windows.Forms.NumericUpDown();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.seconds = new System.Windows.Forms.NumericUpDown();
            this.reverseX = new System.Windows.Forms.CheckBox();
            this.reverseY = new System.Windows.Forms.CheckBox();
            this.streamingCheck = new System.Windows.Forms.CheckBox();
            this.alwaysTop = new System.Windows.Forms.CheckBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.language = new System.Windows.Forms.ComboBox();
            this.imageTab = new System.Windows.Forms.TabPage();
            this.imageFields = new System.Windows.Forms.FlowLayoutPanel();
            this.presetsTab = new System.Windows.Forms.TabPage();
            this.permanentPanel = new System.Windows.Forms.TableLayoutPanel();
            this.permanentList = new System.Windows.Forms.ListBox();
            this.addPermanent = new CameraControlStudio.BlueButton();
            this.editPermanent = new CameraControlStudio.BlueButton();
            this.updatePermanent = new CameraControlStudio.BlueButton();
            this.deletePermanent = new CameraControlStudio.BlueButton();
            this.profilesTab = new System.Windows.Forms.TabPage();
            this.profilesPanel = new System.Windows.Forms.TableLayoutPanel();
            this.profilesList = new System.Windows.Forms.ListBox();
            this.newProfile = new CameraControlStudio.BlueButton();
            this.copyProfile = new CameraControlStudio.BlueButton();
            this.renameProfile = new CameraControlStudio.BlueButton();
            this.profileNote = new System.Windows.Forms.Label();
            this.aboutTab = new System.Windows.Forms.TabPage();
            this.aboutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.credit = new System.Windows.Forms.Label();
            this.repoLink = new System.Windows.Forms.LinkLabel();
            this.issuesLink = new System.Windows.Forms.LinkLabel();
            this.discussionLink = new System.Windows.Forms.LinkLabel();
            this.coffeeLink = new System.Windows.Forms.LinkLabel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.footerSeparator = new CameraControlStudio.MainViewSeparator();
            this.footer = new System.Windows.Forms.TableLayoutPanel();
            this.footerLabels = new System.Windows.Forms.TableLayoutPanel();
            this.cameraStatus = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.profileButton = new CameraControlStudio.BlueButton();
            this.helpButton = new CameraControlStudio.BlueButton();
            this.settingsButton = new CameraControlStudio.BlueButton();
            this.root.SuspendLayout();
            this.body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            this.motionPanel.SuspendLayout();
            this.zoomPanel.SuspendLayout();
            this.presetPanel.SuspendLayout();
            this.presetHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.presetGrid)).BeginInit();
            this.settingsTabs.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.generalFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPanSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastZoomSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).BeginInit();
            this.imageTab.SuspendLayout();
            this.presetsTab.SuspendLayout();
            this.permanentPanel.SuspendLayout();
            this.profilesTab.SuspendLayout();
            this.profilesPanel.SuspendLayout();
            this.aboutTab.SuspendLayout();
            this.aboutPanel.SuspendLayout();
            this.footer.SuspendLayout();
            this.footerLabels.SuspendLayout();
            this.SuspendLayout();
            // 
            // root
            // 
            this.root.ColumnCount = 1;
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.Controls.Add(this.body, 0, 0);
            this.root.Controls.Add(this.footerSeparator, 0, 1);
            this.root.Controls.Add(this.footer, 0, 2);
            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.root.Location = new System.Drawing.Point(0, 0);
            this.root.Margin = new System.Windows.Forms.Padding(0);
            this.root.Name = "root";
            this.root.Padding = new System.Windows.Forms.Padding(18, 14, 18, 10);
            this.root.RowCount = 3;
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.root.Size = new System.Drawing.Size(820, 510);
            this.root.TabIndex = 0;
            // 
            // body
            // 
            this.body.ColumnCount = 2;
            this.body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.body.Controls.Add(this.mainSplit, 0, 0);
            this.body.Controls.Add(this.settingsTabs, 1, 0);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(18, 14);
            this.body.Margin = new System.Windows.Forms.Padding(0);
            this.body.Name = "body";
            this.body.RowCount = 1;
            this.body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.body.Size = new System.Drawing.Size(784, 411);
            this.body.TabIndex = 1;
            // 
            // mainSplit
            // 
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.Location = new System.Drawing.Point(0, 0);
            this.mainSplit.Margin = new System.Windows.Forms.Padding(0);
            this.mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            this.mainSplit.Panel1.Controls.Add(this.motionPanel);
            this.mainSplit.Panel1MinSize = 185;
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.Controls.Add(this.presetPanel);
            this.mainSplit.Panel2MinSize = 150;
            this.mainSplit.Size = new System.Drawing.Size(784, 411);
            this.mainSplit.SplitterDistance = 430;
            this.mainSplit.SplitterWidth = 15;
            this.mainSplit.TabIndex = 0;
            // 
            // motionPanel
            // 
            this.motionPanel.ColumnCount = 2;
            this.motionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.motionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.motionPanel.Controls.Add(this.title, 0, 0);
            this.motionPanel.Controls.Add(this.wheel, 0, 1);
            this.motionPanel.Controls.Add(this.zoomPanel, 1, 1);
            this.motionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionPanel.Location = new System.Drawing.Point(0, 0);
            this.motionPanel.Margin = new System.Windows.Forms.Padding(0);
            this.motionPanel.Name = "motionPanel";
            this.motionPanel.Padding = new System.Windows.Forms.Padding(0, 0, 19, 12);
            this.motionPanel.RowCount = 2;
            this.motionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.motionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.motionPanel.Size = new System.Drawing.Size(430, 411);
            this.motionPanel.TabIndex = 0;
            // 
            // title
            // 
            this.title.AutoEllipsis = true;
            this.motionPanel.SetColumnSpan(this.title, 2);
            this.title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.title.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.title.Location = new System.Drawing.Point(3, 0);
            this.title.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(408, 49);
            this.title.TabIndex = 0;
            this.title.Text = "Camera Control Studio";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.title.UseMnemonic = false;
            // 
            // wheel
            // 
            this.wheel.AccessibleName = "Camera direction wheel. Inner ring normal, outer ring fast.";
            this.wheel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.wheel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wheel.Location = new System.Drawing.Point(0, 52);
            this.wheel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.wheel.Name = "wheel";
            this.wheel.Size = new System.Drawing.Size(327, 347);
            this.wheel.TabIndex = 0;
            // 
            // zoomPanel
            // 
            this.zoomPanel.ColumnCount = 1;
            this.zoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.zoomPanel.Controls.Add(this.normalIn, 0, 0);
            this.zoomPanel.Controls.Add(this.zoomSlider, 0, 1);
            this.zoomPanel.Controls.Add(this.normalOut, 0, 2);
            this.zoomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomPanel.Location = new System.Drawing.Point(337, 72);
            this.zoomPanel.Margin = new System.Windows.Forms.Padding(10, 23, 1, 23);
            this.zoomPanel.Name = "zoomPanel";
            this.zoomPanel.RowCount = 3;
            this.zoomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.zoomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.zoomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.zoomPanel.Size = new System.Drawing.Size(73, 304);
            this.zoomPanel.TabIndex = 1;
            // 
            // normalIn
            // 
            this.normalIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.normalIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.normalIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.normalIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.normalIn.ForeColor = System.Drawing.Color.White;
            this.normalIn.Location = new System.Drawing.Point(0, 1);
            this.normalIn.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.normalIn.Name = "normalIn";
            this.normalIn.Primary = false;
            this.normalIn.Size = new System.Drawing.Size(73, 46);
            this.normalIn.TabIndex = 0;
            this.normalIn.UseVisualStyleBackColor = false;
            // 
            // zoomSlider
            // 
            this.zoomSlider.AccessibleRole = System.Windows.Forms.AccessibleRole.Slider;
            this.zoomSlider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomSlider.Location = new System.Drawing.Point(5, 48);
            this.zoomSlider.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.zoomSlider.Name = "zoomSlider";
            this.zoomSlider.Size = new System.Drawing.Size(63, 208);
            this.zoomSlider.TabIndex = 1;
            // 
            // normalOut
            // 
            this.normalOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.normalOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.normalOut.Direction = CameraControlStudio.ZoomDirection.Out;
            this.normalOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.normalOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.normalOut.ForeColor = System.Drawing.Color.White;
            this.normalOut.Location = new System.Drawing.Point(0, 257);
            this.normalOut.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.normalOut.Name = "normalOut";
            this.normalOut.Primary = false;
            this.normalOut.Size = new System.Drawing.Size(73, 46);
            this.normalOut.TabIndex = 2;
            this.normalOut.UseVisualStyleBackColor = false;
            // 
            // presetPanel
            // 
            this.presetPanel.ColumnCount = 1;
            this.presetPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.presetPanel.Controls.Add(this.presetHeader, 0, 0);
            this.presetPanel.Controls.Add(this.presetGrid, 0, 1);
            this.presetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.presetPanel.Location = new System.Drawing.Point(0, 0);
            this.presetPanel.Margin = new System.Windows.Forms.Padding(0);
            this.presetPanel.Name = "presetPanel";
            this.presetPanel.Padding = new System.Windows.Forms.Padding(24, 0, 0, 17);
            this.presetPanel.RowCount = 2;
            this.presetPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.presetPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.presetPanel.Size = new System.Drawing.Size(339, 411);
            this.presetPanel.TabIndex = 0;
            // 
            // presetHeader
            // 
            this.presetHeader.ColumnCount = 3;
            this.presetHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.presetHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.presetHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.presetHeader.Controls.Add(this.presetTitle, 0, 0);
            this.presetHeader.Controls.Add(this.deleteTemporaryButton, 1, 0);
            this.presetHeader.Controls.Add(this.temporaryButton, 2, 0);
            this.presetHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.presetHeader.Location = new System.Drawing.Point(24, 0);
            this.presetHeader.Margin = new System.Windows.Forms.Padding(0);
            this.presetHeader.Name = "presetHeader";
            this.presetHeader.RowCount = 1;
            this.presetHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.presetHeader.Size = new System.Drawing.Size(315, 49);
            this.presetHeader.TabIndex = 0;
            // 
            // presetTitle
            // 
            this.presetTitle.AutoEllipsis = true;
            this.presetTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.presetTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.presetTitle.Location = new System.Drawing.Point(0, 0);
            this.presetTitle.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.presetTitle.Name = "presetTitle";
            this.presetTitle.Size = new System.Drawing.Size(267, 49);
            this.presetTitle.TabIndex = 0;
            this.presetTitle.Text = "Presets";
            this.presetTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.presetTitle.UseMnemonic = false;
            // 
            // deleteTemporaryButton
            // 
            this.deleteTemporaryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.deleteTemporaryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteTemporaryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deleteTemporaryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteTemporaryButton.ForeColor = System.Drawing.Color.White;
            this.deleteTemporaryButton.Location = new System.Drawing.Point(271, 4);
            this.deleteTemporaryButton.Margin = new System.Windows.Forms.Padding(0, 4, 4, 6);
            this.deleteTemporaryButton.Name = "deleteTemporaryButton";
            this.deleteTemporaryButton.Primary = false;
            this.deleteTemporaryButton.Size = new System.Drawing.Size(1, 39);
            this.deleteTemporaryButton.TabIndex = 1;
            this.deleteTemporaryButton.Text = "×";
            this.deleteTemporaryButton.UseVisualStyleBackColor = false;
            this.deleteTemporaryButton.Visible = false;
            // 
            // temporaryButton
            // 
            this.temporaryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.temporaryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.temporaryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temporaryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.temporaryButton.ForeColor = System.Drawing.Color.White;
            this.temporaryButton.Location = new System.Drawing.Point(271, 4);
            this.temporaryButton.Margin = new System.Windows.Forms.Padding(0, 4, 0, 6);
            this.temporaryButton.Name = "temporaryButton";
            this.temporaryButton.Primary = false;
            this.temporaryButton.Size = new System.Drawing.Size(44, 39);
            this.temporaryButton.TabIndex = 2;
            this.temporaryButton.Text = "+";
            this.temporaryButton.UseVisualStyleBackColor = false;
            // 
            // presetGrid
            // 
            this.presetGrid.AllowUserToAddRows = false;
            this.presetGrid.AllowUserToDeleteRows = false;
            this.presetGrid.AllowUserToResizeRows = false;
            this.presetGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.presetGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.presetGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.presetGrid.ColumnHeadersHeight = 29;
            this.presetGrid.ColumnHeadersVisible = false;
            this.presetGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.presetGrid.Location = new System.Drawing.Point(24, 49);
            this.presetGrid.Margin = new System.Windows.Forms.Padding(0);
            this.presetGrid.MultiSelect = false;
            this.presetGrid.Name = "presetGrid";
            this.presetGrid.ReadOnly = true;
            this.presetGrid.RowHeadersVisible = false;
            this.presetGrid.RowHeadersWidth = 51;
            this.presetGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.presetGrid.Size = new System.Drawing.Size(315, 345);
            this.presetGrid.TabIndex = 1;
            // 
            // settingsTabs
            // 
            this.settingsTabs.Controls.Add(this.generalTab);
            this.settingsTabs.Controls.Add(this.imageTab);
            this.settingsTabs.Controls.Add(this.presetsTab);
            this.settingsTabs.Controls.Add(this.profilesTab);
            this.settingsTabs.Controls.Add(this.aboutTab);
            this.settingsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTabs.Location = new System.Drawing.Point(794, 0);
            this.settingsTabs.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.settingsTabs.Multiline = true;
            this.settingsTabs.Name = "settingsTabs";
            this.settingsTabs.SelectedIndex = 0;
            this.settingsTabs.Size = new System.Drawing.Size(1, 411);
            this.settingsTabs.TabIndex = 1;
            this.settingsTabs.Visible = false;
            // 
            // generalTab
            // 
            this.generalTab.Controls.Add(this.generalFields);
            this.generalTab.Location = new System.Drawing.Point(4, 129);
            this.generalTab.Margin = new System.Windows.Forms.Padding(4);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(10);
            this.generalTab.Size = new System.Drawing.Size(0, 278);
            this.generalTab.TabIndex = 0;
            this.generalTab.Text = "Settings";
            // 
            // generalFields
            // 
            this.generalFields.AutoScroll = true;
            this.generalFields.ColumnCount = 2;
            this.generalFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.generalFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.generalFields.Controls.Add(this.panStepsLabel, 0, 0);
            this.generalFields.Controls.Add(this.panSteps, 1, 0);
            this.generalFields.Controls.Add(this.fastPanStepsLabel, 0, 1);
            this.generalFields.Controls.Add(this.fastPanSteps, 1, 1);
            this.generalFields.Controls.Add(this.zoomStepsLabel, 0, 2);
            this.generalFields.Controls.Add(this.zoomSteps, 1, 2);
            this.generalFields.Controls.Add(this.fastZoomStepsLabel, 0, 3);
            this.generalFields.Controls.Add(this.fastZoomSteps, 1, 3);
            this.generalFields.Controls.Add(this.secondsLabel, 0, 4);
            this.generalFields.Controls.Add(this.seconds, 1, 4);
            this.generalFields.Controls.Add(this.reverseX, 0, 5);
            this.generalFields.Controls.Add(this.reverseY, 0, 6);
            this.generalFields.Controls.Add(this.streamingCheck, 0, 7);
            this.generalFields.Controls.Add(this.alwaysTop, 0, 8);
            this.generalFields.Controls.Add(this.languageLabel, 0, 9);
            this.generalFields.Controls.Add(this.language, 0, 10);
            this.generalFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalFields.Location = new System.Drawing.Point(10, 10);
            this.generalFields.Margin = new System.Windows.Forms.Padding(0);
            this.generalFields.Name = "generalFields";
            this.generalFields.RowCount = 12;
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.generalFields.Size = new System.Drawing.Size(0, 258);
            this.generalFields.TabIndex = 0;
            // 
            // panStepsLabel
            // 
            this.panStepsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panStepsLabel.Location = new System.Drawing.Point(4, 0);
            this.panStepsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.panStepsLabel.Name = "panStepsLabel";
            this.panStepsLabel.Size = new System.Drawing.Size(1, 42);
            this.panStepsLabel.TabIndex = 0;
            this.panStepsLabel.Text = "Normal pan steps";
            this.panStepsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panSteps
            // 
            this.panSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panSteps.Location = new System.Drawing.Point(-95, 4);
            this.panSteps.Margin = new System.Windows.Forms.Padding(4);
            this.panSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.panSteps.Name = "panSteps";
            this.panSteps.Size = new System.Drawing.Size(92, 27);
            this.panSteps.TabIndex = 1;
            this.panSteps.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // fastPanStepsLabel
            // 
            this.fastPanStepsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastPanStepsLabel.Location = new System.Drawing.Point(4, 42);
            this.fastPanStepsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fastPanStepsLabel.Name = "fastPanStepsLabel";
            this.fastPanStepsLabel.Size = new System.Drawing.Size(1, 42);
            this.fastPanStepsLabel.TabIndex = 2;
            this.fastPanStepsLabel.Text = "Fast pan steps";
            this.fastPanStepsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fastPanSteps
            // 
            this.fastPanSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastPanSteps.Location = new System.Drawing.Point(-95, 46);
            this.fastPanSteps.Margin = new System.Windows.Forms.Padding(4);
            this.fastPanSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fastPanSteps.Name = "fastPanSteps";
            this.fastPanSteps.Size = new System.Drawing.Size(92, 27);
            this.fastPanSteps.TabIndex = 3;
            this.fastPanSteps.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // zoomStepsLabel
            // 
            this.zoomStepsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomStepsLabel.Location = new System.Drawing.Point(4, 84);
            this.zoomStepsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.zoomStepsLabel.Name = "zoomStepsLabel";
            this.zoomStepsLabel.Size = new System.Drawing.Size(1, 42);
            this.zoomStepsLabel.TabIndex = 4;
            this.zoomStepsLabel.Text = "Normal zoom steps";
            this.zoomStepsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zoomSteps
            // 
            this.zoomSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomSteps.Location = new System.Drawing.Point(-95, 88);
            this.zoomSteps.Margin = new System.Windows.Forms.Padding(4);
            this.zoomSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zoomSteps.Name = "zoomSteps";
            this.zoomSteps.Size = new System.Drawing.Size(92, 27);
            this.zoomSteps.TabIndex = 5;
            this.zoomSteps.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // fastZoomStepsLabel
            // 
            this.fastZoomStepsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastZoomStepsLabel.Location = new System.Drawing.Point(4, 126);
            this.fastZoomStepsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fastZoomStepsLabel.Name = "fastZoomStepsLabel";
            this.fastZoomStepsLabel.Size = new System.Drawing.Size(1, 42);
            this.fastZoomStepsLabel.TabIndex = 6;
            this.fastZoomStepsLabel.Text = "Fast zoom steps";
            this.fastZoomStepsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fastZoomSteps
            // 
            this.fastZoomSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastZoomSteps.Location = new System.Drawing.Point(-95, 130);
            this.fastZoomSteps.Margin = new System.Windows.Forms.Padding(4);
            this.fastZoomSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fastZoomSteps.Name = "fastZoomSteps";
            this.fastZoomSteps.Size = new System.Drawing.Size(92, 27);
            this.fastZoomSteps.TabIndex = 7;
            this.fastZoomSteps.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // secondsLabel
            // 
            this.secondsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondsLabel.Location = new System.Drawing.Point(4, 168);
            this.secondsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(1, 42);
            this.secondsLabel.TabIndex = 8;
            this.secondsLabel.Text = "Transition (sec.)";
            this.secondsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // seconds
            // 
            this.seconds.DecimalPlaces = 1;
            this.seconds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seconds.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.seconds.Location = new System.Drawing.Point(-95, 172);
            this.seconds.Margin = new System.Windows.Forms.Padding(4);
            this.seconds.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(92, 27);
            this.seconds.TabIndex = 9;
            this.seconds.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // reverseX
            // 
            this.generalFields.SetColumnSpan(this.reverseX, 2);
            this.reverseX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reverseX.Location = new System.Drawing.Point(4, 214);
            this.reverseX.Margin = new System.Windows.Forms.Padding(4);
            this.reverseX.Name = "reverseX";
            this.reverseX.Size = new System.Drawing.Size(1, 34);
            this.reverseX.TabIndex = 10;
            this.reverseX.Text = "Reverse left / right";
            // 
            // reverseY
            // 
            this.generalFields.SetColumnSpan(this.reverseY, 2);
            this.reverseY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reverseY.Location = new System.Drawing.Point(4, 256);
            this.reverseY.Margin = new System.Windows.Forms.Padding(4);
            this.reverseY.Name = "reverseY";
            this.reverseY.Size = new System.Drawing.Size(1, 34);
            this.reverseY.TabIndex = 11;
            this.reverseY.Text = "Reverse up / down";
            // 
            // streamingCheck
            // 
            this.generalFields.SetColumnSpan(this.streamingCheck, 2);
            this.streamingCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streamingCheck.Location = new System.Drawing.Point(4, 298);
            this.streamingCheck.Margin = new System.Windows.Forms.Padding(4);
            this.streamingCheck.Name = "streamingCheck";
            this.streamingCheck.Size = new System.Drawing.Size(1, 34);
            this.streamingCheck.TabIndex = 12;
            this.streamingCheck.Text = "Ask whether streaming has started before using the camera";
            // 
            // alwaysTop
            // 
            this.generalFields.SetColumnSpan(this.alwaysTop, 2);
            this.alwaysTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alwaysTop.Location = new System.Drawing.Point(4, 340);
            this.alwaysTop.Margin = new System.Windows.Forms.Padding(4);
            this.alwaysTop.Name = "alwaysTop";
            this.alwaysTop.Size = new System.Drawing.Size(1, 34);
            this.alwaysTop.TabIndex = 13;
            this.alwaysTop.Text = "Always on top";
            // 
            // languageLabel
            // 
            this.languageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.languageLabel.Location = new System.Drawing.Point(4, 378);
            this.languageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(1, 42);
            this.languageLabel.TabIndex = 14;
            this.languageLabel.Text = "Language";
            this.languageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // language
            // 
            this.generalFields.SetColumnSpan(this.language, 2);
            this.language.Dock = System.Windows.Forms.DockStyle.Fill;
            this.language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.language.Location = new System.Drawing.Point(4, 424);
            this.language.Margin = new System.Windows.Forms.Padding(4);
            this.language.Name = "language";
            this.language.Size = new System.Drawing.Size(1, 28);
            this.language.TabIndex = 15;
            // 
            // imageTab
            // 
            this.imageTab.Controls.Add(this.imageFields);
            this.imageTab.Location = new System.Drawing.Point(4, 129);
            this.imageTab.Margin = new System.Windows.Forms.Padding(4);
            this.imageTab.Name = "imageTab";
            this.imageTab.Padding = new System.Windows.Forms.Padding(10);
            this.imageTab.Size = new System.Drawing.Size(0, 278);
            this.imageTab.TabIndex = 1;
            this.imageTab.Text = "Image";
            // 
            // imageFields
            // 
            this.imageFields.AutoScroll = true;
            this.imageFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageFields.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.imageFields.Location = new System.Drawing.Point(10, 10);
            this.imageFields.Margin = new System.Windows.Forms.Padding(4);
            this.imageFields.Name = "imageFields";
            this.imageFields.Size = new System.Drawing.Size(0, 258);
            this.imageFields.TabIndex = 0;
            this.imageFields.WrapContents = false;
            // 
            // presetsTab
            // 
            this.presetsTab.Controls.Add(this.permanentPanel);
            this.presetsTab.Location = new System.Drawing.Point(4, 129);
            this.presetsTab.Margin = new System.Windows.Forms.Padding(4);
            this.presetsTab.Name = "presetsTab";
            this.presetsTab.Padding = new System.Windows.Forms.Padding(10);
            this.presetsTab.Size = new System.Drawing.Size(0, 278);
            this.presetsTab.TabIndex = 2;
            this.presetsTab.Text = "Presets";
            // 
            // permanentPanel
            // 
            this.permanentPanel.ColumnCount = 1;
            this.permanentPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.permanentPanel.Controls.Add(this.permanentList, 0, 0);
            this.permanentPanel.Controls.Add(this.addPermanent, 0, 1);
            this.permanentPanel.Controls.Add(this.editPermanent, 0, 2);
            this.permanentPanel.Controls.Add(this.updatePermanent, 0, 3);
            this.permanentPanel.Controls.Add(this.deletePermanent, 0, 4);
            this.permanentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permanentPanel.Location = new System.Drawing.Point(10, 10);
            this.permanentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.permanentPanel.Name = "permanentPanel";
            this.permanentPanel.RowCount = 5;
            this.permanentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.permanentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.permanentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.permanentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.permanentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.permanentPanel.Size = new System.Drawing.Size(0, 258);
            this.permanentPanel.TabIndex = 0;
            // 
            // permanentList
            // 
            this.permanentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permanentList.IntegralHeight = false;
            this.permanentList.ItemHeight = 20;
            this.permanentList.Location = new System.Drawing.Point(4, 4);
            this.permanentList.Margin = new System.Windows.Forms.Padding(4);
            this.permanentList.Name = "permanentList";
            this.permanentList.Size = new System.Drawing.Size(1, 58);
            this.permanentList.TabIndex = 0;
            // 
            // addPermanent
            // 
            this.addPermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.addPermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addPermanent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addPermanent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addPermanent.ForeColor = System.Drawing.Color.White;
            this.addPermanent.Location = new System.Drawing.Point(4, 70);
            this.addPermanent.Margin = new System.Windows.Forms.Padding(4);
            this.addPermanent.Name = "addPermanent";
            this.addPermanent.Primary = false;
            this.addPermanent.Size = new System.Drawing.Size(1, 40);
            this.addPermanent.TabIndex = 1;
            this.addPermanent.Text = "Add permanent preset";
            this.addPermanent.UseVisualStyleBackColor = false;
            // 
            // editPermanent
            // 
            this.editPermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.editPermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editPermanent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editPermanent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editPermanent.ForeColor = System.Drawing.Color.White;
            this.editPermanent.Location = new System.Drawing.Point(4, 118);
            this.editPermanent.Margin = new System.Windows.Forms.Padding(4);
            this.editPermanent.Name = "editPermanent";
            this.editPermanent.Primary = false;
            this.editPermanent.Size = new System.Drawing.Size(1, 40);
            this.editPermanent.TabIndex = 2;
            this.editPermanent.Text = "Edit name and symbol";
            this.editPermanent.UseVisualStyleBackColor = false;
            // 
            // updatePermanent
            // 
            this.updatePermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.updatePermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updatePermanent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updatePermanent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updatePermanent.ForeColor = System.Drawing.Color.White;
            this.updatePermanent.Location = new System.Drawing.Point(4, 166);
            this.updatePermanent.Margin = new System.Windows.Forms.Padding(4);
            this.updatePermanent.Name = "updatePermanent";
            this.updatePermanent.Primary = false;
            this.updatePermanent.Size = new System.Drawing.Size(1, 40);
            this.updatePermanent.TabIndex = 3;
            this.updatePermanent.Text = "Replace with current position";
            this.updatePermanent.UseVisualStyleBackColor = false;
            // 
            // deletePermanent
            // 
            this.deletePermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.deletePermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deletePermanent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deletePermanent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deletePermanent.ForeColor = System.Drawing.Color.White;
            this.deletePermanent.Location = new System.Drawing.Point(4, 214);
            this.deletePermanent.Margin = new System.Windows.Forms.Padding(4);
            this.deletePermanent.Name = "deletePermanent";
            this.deletePermanent.Primary = false;
            this.deletePermanent.Size = new System.Drawing.Size(1, 40);
            this.deletePermanent.TabIndex = 4;
            this.deletePermanent.Text = "Delete permanent preset";
            this.deletePermanent.UseVisualStyleBackColor = false;
            // 
            // profilesTab
            // 
            this.profilesTab.Controls.Add(this.profilesPanel);
            this.profilesTab.Location = new System.Drawing.Point(4, 129);
            this.profilesTab.Margin = new System.Windows.Forms.Padding(4);
            this.profilesTab.Name = "profilesTab";
            this.profilesTab.Padding = new System.Windows.Forms.Padding(10);
            this.profilesTab.Size = new System.Drawing.Size(0, 278);
            this.profilesTab.TabIndex = 3;
            this.profilesTab.Text = "Profiles";
            // 
            // profilesPanel
            // 
            this.profilesPanel.ColumnCount = 1;
            this.profilesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.profilesPanel.Controls.Add(this.profilesList, 0, 0);
            this.profilesPanel.Controls.Add(this.newProfile, 0, 1);
            this.profilesPanel.Controls.Add(this.copyProfile, 0, 2);
            this.profilesPanel.Controls.Add(this.renameProfile, 0, 3);
            this.profilesPanel.Controls.Add(this.profileNote, 0, 4);
            this.profilesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profilesPanel.Location = new System.Drawing.Point(10, 10);
            this.profilesPanel.Margin = new System.Windows.Forms.Padding(0);
            this.profilesPanel.Name = "profilesPanel";
            this.profilesPanel.RowCount = 5;
            this.profilesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.profilesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.profilesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.profilesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.profilesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.profilesPanel.Size = new System.Drawing.Size(0, 258);
            this.profilesPanel.TabIndex = 0;
            // 
            // profilesList
            // 
            this.profilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profilesList.IntegralHeight = false;
            this.profilesList.ItemHeight = 20;
            this.profilesList.Location = new System.Drawing.Point(4, 4);
            this.profilesList.Margin = new System.Windows.Forms.Padding(4);
            this.profilesList.Name = "profilesList";
            this.profilesList.Size = new System.Drawing.Size(1, 65);
            this.profilesList.TabIndex = 0;
            // 
            // newProfile
            // 
            this.newProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.newProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.newProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newProfile.ForeColor = System.Drawing.Color.White;
            this.newProfile.Location = new System.Drawing.Point(4, 77);
            this.newProfile.Margin = new System.Windows.Forms.Padding(4);
            this.newProfile.Name = "newProfile";
            this.newProfile.Primary = false;
            this.newProfile.Size = new System.Drawing.Size(1, 37);
            this.newProfile.TabIndex = 1;
            this.newProfile.Text = "New profile";
            this.newProfile.UseVisualStyleBackColor = false;
            // 
            // copyProfile
            // 
            this.copyProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.copyProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.copyProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.copyProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyProfile.ForeColor = System.Drawing.Color.White;
            this.copyProfile.Location = new System.Drawing.Point(4, 122);
            this.copyProfile.Margin = new System.Windows.Forms.Padding(4);
            this.copyProfile.Name = "copyProfile";
            this.copyProfile.Primary = false;
            this.copyProfile.Size = new System.Drawing.Size(1, 37);
            this.copyProfile.TabIndex = 2;
            this.copyProfile.Text = "Copy profile";
            this.copyProfile.UseVisualStyleBackColor = false;
            // 
            // renameProfile
            // 
            this.renameProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.renameProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.renameProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renameProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renameProfile.ForeColor = System.Drawing.Color.White;
            this.renameProfile.Location = new System.Drawing.Point(4, 167);
            this.renameProfile.Margin = new System.Windows.Forms.Padding(4);
            this.renameProfile.Name = "renameProfile";
            this.renameProfile.Primary = false;
            this.renameProfile.Size = new System.Drawing.Size(1, 37);
            this.renameProfile.TabIndex = 3;
            this.renameProfile.Text = "Rename profile";
            this.renameProfile.UseVisualStyleBackColor = false;
            // 
            // profileNote
            // 
            this.profileNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileNote.Location = new System.Drawing.Point(4, 208);
            this.profileNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.profileNote.Name = "profileNote";
            this.profileNote.Size = new System.Drawing.Size(1, 50);
            this.profileNote.TabIndex = 4;
            this.profileNote.Text = "Edit the active profile in Settings.";
            // 
            // aboutTab
            // 
            this.aboutTab.Controls.Add(this.aboutPanel);
            this.aboutTab.Location = new System.Drawing.Point(4, 129);
            this.aboutTab.Margin = new System.Windows.Forms.Padding(4);
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Padding = new System.Windows.Forms.Padding(10);
            this.aboutTab.Size = new System.Drawing.Size(0, 278);
            this.aboutTab.TabIndex = 4;
            this.aboutTab.Text = "About";
            // 
            // aboutPanel
            // 
            this.aboutPanel.AutoScroll = true;
            this.aboutPanel.Controls.Add(this.credit);
            this.aboutPanel.Controls.Add(this.repoLink);
            this.aboutPanel.Controls.Add(this.issuesLink);
            this.aboutPanel.Controls.Add(this.discussionLink);
            this.aboutPanel.Controls.Add(this.coffeeLink);
            this.aboutPanel.Controls.Add(this.versionLabel);
            this.aboutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.aboutPanel.Location = new System.Drawing.Point(10, 10);
            this.aboutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.aboutPanel.Name = "aboutPanel";
            this.aboutPanel.Size = new System.Drawing.Size(0, 258);
            this.aboutPanel.TabIndex = 0;
            this.aboutPanel.WrapContents = false;
            // 
            // credit
            // 
            this.credit.AutoSize = true;
            this.credit.Location = new System.Drawing.Point(5, 10);
            this.credit.Margin = new System.Windows.Forms.Padding(5, 10, 5, 15);
            this.credit.MaximumSize = new System.Drawing.Size(350, 0);
            this.credit.Name = "credit";
            this.credit.Size = new System.Drawing.Size(321, 40);
            this.credit.TabIndex = 0;
            this.credit.Text = "Developed as open source by Brian Kristensen, Vejle Uhrhøj, DK.";
            // 
            // repoLink
            // 
            this.repoLink.AutoSize = true;
            this.repoLink.Location = new System.Drawing.Point(5, 75);
            this.repoLink.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.repoLink.Name = "repoLink";
            this.repoLink.Size = new System.Drawing.Size(56, 20);
            this.repoLink.TabIndex = 1;
            this.repoLink.TabStop = true;
            this.repoLink.Text = "GitHub";
            // 
            // issuesLink
            // 
            this.issuesLink.AutoSize = true;
            this.issuesLink.Location = new System.Drawing.Point(5, 115);
            this.issuesLink.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.issuesLink.Name = "issuesLink";
            this.issuesLink.Size = new System.Drawing.Size(219, 20);
            this.issuesLink.TabIndex = 2;
            this.issuesLink.TabStop = true;
            this.issuesLink.Text = "Report bugs or missing features";
            // 
            // discussionLink
            // 
            this.discussionLink.AutoSize = true;
            this.discussionLink.Location = new System.Drawing.Point(5, 155);
            this.discussionLink.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.discussionLink.Name = "discussionLink";
            this.discussionLink.Size = new System.Drawing.Size(182, 20);
            this.discussionLink.TabIndex = 3;
            this.discussionLink.TabStop = true;
            this.discussionLink.Text = "Join the project discussion";
            // 
            // coffeeLink
            // 
            this.coffeeLink.AutoSize = true;
            this.coffeeLink.Location = new System.Drawing.Point(5, 195);
            this.coffeeLink.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.coffeeLink.Name = "coffeeLink";
            this.coffeeLink.Size = new System.Drawing.Size(116, 20);
            this.coffeeLink.TabIndex = 4;
            this.coffeeLink.TabStop = true;
            this.coffeeLink.Text = "Buy me a coffee";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(4, 225);
            this.versionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(34, 20);
            this.versionLabel.TabIndex = 5;
            this.versionLabel.Text = "MIT";
            // 
            // footerSeparator
            // 
            this.footerSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerSeparator.Location = new System.Drawing.Point(18, 425);
            this.footerSeparator.Margin = new System.Windows.Forms.Padding(0);
            this.footerSeparator.Name = "footerSeparator";
            this.footerSeparator.Size = new System.Drawing.Size(784, 13);
            this.footerSeparator.TabIndex = 2;
            this.footerSeparator.TabStop = false;
            // 
            // footer
            // 
            this.footer.ColumnCount = 4;
            this.footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.footer.Controls.Add(this.footerLabels, 0, 0);
            this.footer.Controls.Add(this.profileButton, 1, 0);
            this.footer.Controls.Add(this.helpButton, 2, 0);
            this.footer.Controls.Add(this.settingsButton, 3, 0);
            this.footer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footer.Location = new System.Drawing.Point(18, 438);
            this.footer.Margin = new System.Windows.Forms.Padding(0);
            this.footer.Name = "footer";
            this.footer.RowCount = 1;
            this.footer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footer.Size = new System.Drawing.Size(784, 62);
            this.footer.TabIndex = 1;
            // 
            // footerLabels
            // 
            this.footerLabels.ColumnCount = 1;
            this.footerLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerLabels.Controls.Add(this.cameraStatus, 0, 0);
            this.footerLabels.Controls.Add(this.status, 0, 1);
            this.footerLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerLabels.Location = new System.Drawing.Point(0, 3);
            this.footerLabels.Margin = new System.Windows.Forms.Padding(0, 3, 7, 0);
            this.footerLabels.Name = "footerLabels";
            this.footerLabels.RowCount = 2;
            this.footerLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.footerLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerLabels.Size = new System.Drawing.Size(520, 59);
            this.footerLabels.TabIndex = 0;
            // 
            // cameraStatus
            // 
            this.cameraStatus.AutoEllipsis = true;
            this.cameraStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraStatus.Location = new System.Drawing.Point(4, 0);
            this.cameraStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cameraStatus.Name = "cameraStatus";
            this.cameraStatus.Size = new System.Drawing.Size(512, 25);
            this.cameraStatus.TabIndex = 0;
            this.cameraStatus.Text = "Camera: not connected";
            this.cameraStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status
            // 
            this.status.AutoEllipsis = true;
            this.status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.status.Location = new System.Drawing.Point(4, 25);
            this.status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(512, 34);
            this.status.TabIndex = 1;
            this.status.Text = "Ready";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // profileButton
            // 
            this.profileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.profileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.profileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profileButton.ForeColor = System.Drawing.Color.White;
            this.profileButton.Location = new System.Drawing.Point(530, 9);
            this.profileButton.Margin = new System.Windows.Forms.Padding(3, 9, 0, 7);
            this.profileButton.Name = "profileButton";
            this.profileButton.Primary = false;
            this.profileButton.Size = new System.Drawing.Size(147, 46);
            this.profileButton.TabIndex = 1;
            this.profileButton.Text = "Profiles";
            this.profileButton.UseVisualStyleBackColor = false;
            // 
            // helpButton
            // 
            this.helpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.helpButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.helpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.helpButton.ForeColor = System.Drawing.Color.White;
            this.helpButton.Location = new System.Drawing.Point(683, 9);
            this.helpButton.Margin = new System.Windows.Forms.Padding(6, 9, 0, 7);
            this.helpButton.Name = "helpButton";
            this.helpButton.Primary = false;
            this.helpButton.Size = new System.Drawing.Size(42, 46);
            this.helpButton.TabIndex = 2;
            this.helpButton.Text = "?";
            this.helpButton.UseVisualStyleBackColor = false;
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.settingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settingsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.Font = new System.Drawing.Font("Segoe UI Symbol", 17F);
            this.settingsButton.ForeColor = System.Drawing.Color.White;
            this.settingsButton.Location = new System.Drawing.Point(731, 9);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(6, 9, 0, 7);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Primary = false;
            this.settingsButton.Size = new System.Drawing.Size(53, 46);
            this.settingsButton.TabIndex = 3;
            this.settingsButton.Text = "⚙";
            this.settingsButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(820, 510);
            this.Controls.Add(this.root);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Camera Control Studio";
            this.root.ResumeLayout(false);
            this.body.ResumeLayout(false);
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            this.motionPanel.ResumeLayout(false);
            this.zoomPanel.ResumeLayout(false);
            this.presetPanel.ResumeLayout(false);
            this.presetHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.presetGrid)).EndInit();
            this.settingsTabs.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.generalFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPanSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastZoomSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).EndInit();
            this.imageTab.ResumeLayout(false);
            this.presetsTab.ResumeLayout(false);
            this.permanentPanel.ResumeLayout(false);
            this.profilesTab.ResumeLayout(false);
            this.profilesPanel.ResumeLayout(false);
            this.aboutTab.ResumeLayout(false);
            this.aboutPanel.ResumeLayout(false);
            this.aboutPanel.PerformLayout();
            this.footer.ResumeLayout(false);
            this.footerLabels.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private TableLayoutPanel root;
        private TableLayoutPanel presetHeader;
        private Label presetTitle;
        private Label title;
        private BlueButton profileButton;
        private TableLayoutPanel body;
        private MainViewSplitter mainSplit;
        private TableLayoutPanel motionPanel;
        private PanWheel wheel;
        private TableLayoutPanel zoomPanel;
        private ZoomSlider zoomSlider;
        private ZoomButton normalOut;
        private ZoomButton normalIn;
        private TableLayoutPanel presetPanel;
        private BlueButton temporaryButton;
        private BlueButton deleteTemporaryButton;
        private TabControl settingsTabs;
        private TabPage generalTab;
        private TabPage imageTab;
        private TabPage presetsTab;
        private TabPage profilesTab;
        private TabPage aboutTab;
        private TableLayoutPanel generalFields;
        private Label panStepsLabel;
        private NumericUpDown panSteps;
        private Label fastPanStepsLabel;
        private NumericUpDown fastPanSteps;
        private Label zoomStepsLabel;
        private NumericUpDown zoomSteps;
        private Label fastZoomStepsLabel;
        private NumericUpDown fastZoomSteps;
        private Label secondsLabel;
        private NumericUpDown seconds;
        private CheckBox reverseX;
        private CheckBox reverseY;
        private CheckBox streamingCheck;
        private CheckBox alwaysTop;
        private Label languageLabel;
        private ComboBox language;
        private FlowLayoutPanel imageFields;
        private TableLayoutPanel permanentPanel;
        private ListBox permanentList;
        private BlueButton addPermanent;
        private BlueButton editPermanent;
        private BlueButton updatePermanent;
        private BlueButton deletePermanent;
        private TableLayoutPanel profilesPanel;
        private ListBox profilesList;
        private BlueButton newProfile;
        private BlueButton copyProfile;
        private BlueButton renameProfile;
        private Label profileNote;
        private FlowLayoutPanel aboutPanel;
        private Label credit;
        private LinkLabel repoLink;
        private LinkLabel issuesLink;
        private LinkLabel discussionLink;
        private LinkLabel coffeeLink;
        private Label versionLabel;
        private TableLayoutPanel footerLabels;
        private TableLayoutPanel footer;
        private MainViewSeparator footerSeparator;
        private ToolTip mainToolTips;
        private Label cameraStatus;
        private Label status;
        private BlueButton settingsButton;
        private BlueButton helpButton;
        private DataGridView presetGrid;
    }
}
