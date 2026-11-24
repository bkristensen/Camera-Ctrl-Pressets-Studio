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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.fullFrame = new CameraControlStudio.BlueButton();
            this.wheel = new CameraControlStudio.PanWheel();
            this.movement = new System.Windows.Forms.Timer(this.components);
            this.held = new System.Windows.Forms.Timer(this.components);
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.motionLimits = new System.Windows.Forms.Timer(this.components);
            this.documentationTimer = new System.Windows.Forms.Timer(this.components);
            this.settingsCategories = new System.Windows.Forms.TabControl();
            this.motionTab = new System.Windows.Forms.TabPage();
            this.motionGroup = new System.Windows.Forms.GroupBox();
            this.movementScroll = new System.Windows.Forms.Panel();
            this.generalFields = new System.Windows.Forms.TableLayoutPanel();
            this.finePanLabel = new System.Windows.Forms.Label();
            this.finePanPercent = new System.Windows.Forms.NumericUpDown();
            this.panStepsLabel = new System.Windows.Forms.Label();
            this.panSteps = new System.Windows.Forms.NumericUpDown();
            this.fastPanStepsLabel = new System.Windows.Forms.Label();
            this.fastPanSteps = new System.Windows.Forms.NumericUpDown();
            this.zoomStepsLabel = new System.Windows.Forms.Label();
            this.zoomSteps = new System.Windows.Forms.NumericUpDown();
            this.useTransitionSpeed = new System.Windows.Forms.CheckBox();
            this.transitionSpeedLabel = new System.Windows.Forms.Label();
            this.transitionSpeedPercent = new System.Windows.Forms.NumericUpDown();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.seconds = new System.Windows.Forms.NumericUpDown();
            this.reverseX = new System.Windows.Forms.CheckBox();
            this.reverseY = new System.Windows.Forms.CheckBox();
            this.startupFovLabel = new System.Windows.Forms.Label();
            this.startupFov = new System.Windows.Forms.ListBox();
            this.allowFov = new System.Windows.Forms.CheckBox();
            this.adjustFovAtLimits = new System.Windows.Forms.CheckBox();
            this.applicationTab = new System.Windows.Forms.TabPage();
            this.applicationScroll = new System.Windows.Forms.Panel();
            this.applicationFields = new System.Windows.Forms.TableLayoutPanel();
            this.windowGroup = new System.Windows.Forms.GroupBox();
            this.windowFields = new System.Windows.Forms.TableLayoutPanel();
            this.streamingCheck = new System.Windows.Forms.CheckBox();
            this.alwaysTop = new System.Windows.Forms.CheckBox();
            this.languageGroup = new System.Windows.Forms.GroupBox();
            this.language = new System.Windows.Forms.ComboBox();
            this.videoLayout = new System.Windows.Forms.TableLayoutPanel();
            this.videoStatus = new System.Windows.Forms.FlowLayoutPanel();
            this.loadVideoControls = new CameraControlStudio.BlueButton();
            this.noVideoControls = new System.Windows.Forms.Label();
            this.imagePages = new System.Windows.Forms.TableLayoutPanel();
            this.imageCategories = new System.Windows.Forms.TabControl();
            this.focusTab = new System.Windows.Forms.TabPage();
            this.focusPanel = new System.Windows.Forms.Panel();
            this.gainControl = new System.Windows.Forms.GroupBox();
            this.gainAutomatic = new System.Windows.Forms.CheckBox();
            this.gainSlider = new System.Windows.Forms.TrackBar();
            this.exposureControl = new System.Windows.Forms.GroupBox();
            this.exposureAutomatic = new System.Windows.Forms.CheckBox();
            this.exposureSlider = new System.Windows.Forms.TrackBar();
            this.focusControl = new System.Windows.Forms.GroupBox();
            this.focusAutomatic = new System.Windows.Forms.CheckBox();
            this.focusSlider = new System.Windows.Forms.TrackBar();
            this.colourTab = new System.Windows.Forms.TabPage();
            this.colourPanel = new System.Windows.Forms.Panel();
            this.whiteBalanceControl = new System.Windows.Forms.GroupBox();
            this.whiteBalanceAutomatic = new System.Windows.Forms.CheckBox();
            this.whiteBalanceSlider = new System.Windows.Forms.TrackBar();
            this.saturationControl = new System.Windows.Forms.GroupBox();
            this.saturationAutomatic = new System.Windows.Forms.CheckBox();
            this.saturationSlider = new System.Windows.Forms.TrackBar();
            this.gammaControl = new System.Windows.Forms.GroupBox();
            this.gammaAutomatic = new System.Windows.Forms.CheckBox();
            this.gammaSlider = new System.Windows.Forms.TrackBar();
            this.hueControl = new System.Windows.Forms.GroupBox();
            this.hueAutomatic = new System.Windows.Forms.CheckBox();
            this.hueSlider = new System.Windows.Forms.TrackBar();
            this.lightTab = new System.Windows.Forms.TabPage();
            this.lightPanel = new System.Windows.Forms.Panel();
            this.contrastControl = new System.Windows.Forms.GroupBox();
            this.contrastAutomatic = new System.Windows.Forms.CheckBox();
            this.contrastSlider = new System.Windows.Forms.TrackBar();
            this.brightnessControl = new System.Windows.Forms.GroupBox();
            this.brightnessAutomatic = new System.Windows.Forms.CheckBox();
            this.brightnessSlider = new System.Windows.Forms.TrackBar();
            this.backlightControl = new System.Windows.Forms.GroupBox();
            this.backlightAutomatic = new System.Windows.Forms.CheckBox();
            this.backlightSlider = new System.Windows.Forms.TrackBar();
            this.sharpnessControl = new System.Windows.Forms.GroupBox();
            this.sharpnessAutomatic = new System.Windows.Forms.CheckBox();
            this.sharpnessSlider = new System.Windows.Forms.TrackBar();
            this.resetVideoControls = new CameraControlStudio.BlueButton();
            this.issuesTab = new System.Windows.Forms.TabPage();
            this.issueGroups = new System.Windows.Forms.FlowLayoutPanel();
            this.logGroup = new System.Windows.Forms.GroupBox();
            this.logActions = new System.Windows.Forms.FlowLayoutPanel();
            this.logSettingsLink = new System.Windows.Forms.LinkLabel();
            this.showLogLink = new System.Windows.Forms.LinkLabel();
            this.reportGroup = new System.Windows.Forms.GroupBox();
            this.reportActions = new System.Windows.Forms.FlowLayoutPanel();
            this.reportNote = new System.Windows.Forms.Label();
            this.reportLink = new System.Windows.Forms.LinkLabel();
            this.releasesLink = new System.Windows.Forms.LinkLabel();
            this.guideIndexLink = new System.Windows.Forms.LinkLabel();
            this.openGuideButton = new CameraControlStudio.BlueButton();
            this.iconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.positionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recallColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.root = new System.Windows.Forms.TableLayoutPanel();
            this.body = new System.Windows.Forms.TableLayoutPanel();
            this.mainSplit = new CameraControlStudio.MainViewSplitter();
            this.motionPanel = new System.Windows.Forms.TableLayoutPanel();
            this.title = new System.Windows.Forms.Label();
            this.fovDisplay = new System.Windows.Forms.Panel();
            this.fieldOfViewLabel = new System.Windows.Forms.Label();
            this.fieldOfViewPicker = new System.Windows.Forms.ComboBox();
            this.zoomPanel = new System.Windows.Forms.TableLayoutPanel();
            this.normalIn = new CameraControlStudio.ZoomButton();
            this.zoomSlider = new CameraControlStudio.ZoomSlider();
            this.normalOut = new CameraControlStudio.ZoomButton();
            this.motionPosition = new System.Windows.Forms.Label();
            this.presetPanel = new System.Windows.Forms.TableLayoutPanel();
            this.presetHeader = new System.Windows.Forms.TableLayoutPanel();
            this.presetTitle = new System.Windows.Forms.Label();
            this.deleteTemporaryButton = new CameraControlStudio.BlueButton();
            this.temporaryButton = new CameraControlStudio.BlueButton();
            this.presetGrid = new System.Windows.Forms.DataGridView();
            this.settingsTabs = new System.Windows.Forms.TabControl();
            this.presetsTab = new System.Windows.Forms.TabPage();
            this.permanentPanel = new System.Windows.Forms.TableLayoutPanel();
            this.permanentTitle = new System.Windows.Forms.Label();
            this.permanentList = new System.Windows.Forms.ListBox();
            this.permanentActions = new System.Windows.Forms.FlowLayoutPanel();
            this.addPermanent = new CameraControlStudio.BlueButton();
            this.editPermanent = new CameraControlStudio.BlueButton();
            this.updatePermanent = new CameraControlStudio.BlueButton();
            this.deletePermanent = new CameraControlStudio.BlueButton();
            this.movePermanentUp = new CameraControlStudio.BlueButton();
            this.movePermanentDown = new CameraControlStudio.BlueButton();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.imageTab = new System.Windows.Forms.TabPage();
            this.profilesTab = new System.Windows.Forms.TabPage();
            this.profilesPanel = new System.Windows.Forms.TableLayoutPanel();
            this.profilesList = new System.Windows.Forms.ListBox();
            this.profileActions = new System.Windows.Forms.FlowLayoutPanel();
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
            this.releaseNotice = new System.Windows.Forms.LinkLabel();
            this.status = new System.Windows.Forms.Label();
            this.profileButton = new CameraControlStudio.BlueButton();
            this.helpButton = new CameraControlStudio.BlueButton();
            this.settingsButton = new CameraControlStudio.BlueButton();
            this.settingsCategories.SuspendLayout();
            this.motionTab.SuspendLayout();
            this.motionGroup.SuspendLayout();
            this.movementScroll.SuspendLayout();
            this.generalFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.finePanPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPanSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transitionSpeedPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).BeginInit();
            this.applicationTab.SuspendLayout();
            this.applicationScroll.SuspendLayout();
            this.applicationFields.SuspendLayout();
            this.windowGroup.SuspendLayout();
            this.windowFields.SuspendLayout();
            this.languageGroup.SuspendLayout();
            this.videoLayout.SuspendLayout();
            this.videoStatus.SuspendLayout();
            this.imagePages.SuspendLayout();
            this.imageCategories.SuspendLayout();
            this.focusTab.SuspendLayout();
            this.focusPanel.SuspendLayout();
            this.gainControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gainSlider)).BeginInit();
            this.exposureControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exposureSlider)).BeginInit();
            this.focusControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.focusSlider)).BeginInit();
            this.colourTab.SuspendLayout();
            this.colourPanel.SuspendLayout();
            this.whiteBalanceControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.whiteBalanceSlider)).BeginInit();
            this.saturationControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saturationSlider)).BeginInit();
            this.gammaControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gammaSlider)).BeginInit();
            this.hueControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hueSlider)).BeginInit();
            this.lightTab.SuspendLayout();
            this.lightPanel.SuspendLayout();
            this.contrastControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contrastSlider)).BeginInit();
            this.brightnessControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessSlider)).BeginInit();
            this.backlightControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backlightSlider)).BeginInit();
            this.sharpnessControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sharpnessSlider)).BeginInit();
            this.issuesTab.SuspendLayout();
            this.issueGroups.SuspendLayout();
            this.logGroup.SuspendLayout();
            this.logActions.SuspendLayout();
            this.reportGroup.SuspendLayout();
            this.reportActions.SuspendLayout();
            this.root.SuspendLayout();
            this.body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            this.motionPanel.SuspendLayout();
            this.fovDisplay.SuspendLayout();
            this.zoomPanel.SuspendLayout();
            this.presetPanel.SuspendLayout();
            this.presetHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.presetGrid)).BeginInit();
            this.settingsTabs.SuspendLayout();
            this.presetsTab.SuspendLayout();
            this.permanentPanel.SuspendLayout();
            this.permanentActions.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.imageTab.SuspendLayout();
            this.profilesTab.SuspendLayout();
            this.profilesPanel.SuspendLayout();
            this.profileActions.SuspendLayout();
            this.aboutTab.SuspendLayout();
            this.aboutPanel.SuspendLayout();
            this.footer.SuspendLayout();
            this.footerLabels.SuspendLayout();
            this.SuspendLayout();
            // 
            // fullFrame
            // 
            this.fullFrame.AccessibleName = "Restore full frame";
            this.fullFrame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.fullFrame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fullFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fullFrame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fullFrame.ForeColor = System.Drawing.Color.White;
            this.fullFrame.Image = ((System.Drawing.Image)(resources.GetObject("fullFrame.Image")));
            this.fullFrame.Location = new System.Drawing.Point(0, 1);
            this.fullFrame.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.fullFrame.Name = "fullFrame";
            this.fullFrame.Primary = false;
            this.fullFrame.Size = new System.Drawing.Size(84, 46);
            this.fullFrame.TabIndex = 3;
            this.mainToolTips.SetToolTip(this.fullFrame, "Restore full frame\nUse the widest view and minimum zoom.");
            this.mainToolTips.SetToolTip(this.fullFrame, "Restore full frame\nUse the widest view and minimum zoom.");
            this.fullFrame.UseVisualStyleBackColor = false;
            // 
            // wheel
            // 
            this.wheel.AccessibleName = "Camera direction wheel";
            this.wheel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wheel.Location = new System.Drawing.Point(0, 100);
            this.wheel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.wheel.Name = "wheel";
            this.wheel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.wheel.Size = new System.Drawing.Size(291, 251);
            this.wheel.TabIndex = 0;
            this.mainToolTips.SetToolTip(this.wheel, "Click the camera button to choose a source. Drag it to pan.");
            this.mainToolTips.SetToolTip(this.wheel, "Click the camera button to choose a source. Drag it to pan.");
            // 
            // movement
            // 
            this.movement.Interval = 33;
            // 
            // held
            // 
            this.held.Interval = 150;
            // 
            // saveTimer
            // 
            this.saveTimer.Interval = 600;
            // 
            // motionLimits
            // 
            this.motionLimits.Interval = 250;
            // 
            // documentationTimer
            // 
            this.documentationTimer.Interval = 900;
            // 
            // settingsCategories
            // 
            this.settingsCategories.Controls.Add(this.motionTab);
            this.settingsCategories.Controls.Add(this.applicationTab);
            this.settingsCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsCategories.Location = new System.Drawing.Point(10, 10);
            this.settingsCategories.Name = "settingsCategories";
            this.settingsCategories.SelectedIndex = 0;
            this.settingsCategories.Size = new System.Drawing.Size(302, 333);
            this.settingsCategories.TabIndex = 0;
            // 
            // motionTab
            // 
            this.motionTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.motionTab.Controls.Add(this.motionGroup);
            this.motionTab.Location = new System.Drawing.Point(4, 29);
            this.motionTab.Name = "motionTab";
            this.motionTab.Size = new System.Drawing.Size(294, 300);
            this.motionTab.TabIndex = 0;
            this.motionTab.Text = "Movement";
            // 
            // motionGroup
            // 
            this.motionGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.motionGroup.Controls.Add(this.movementScroll);
            this.motionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionGroup.ForeColor = System.Drawing.Color.White;
            this.motionGroup.Location = new System.Drawing.Point(0, 0);
            this.motionGroup.Name = "motionGroup";
            this.motionGroup.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.motionGroup.Size = new System.Drawing.Size(294, 300);
            this.motionGroup.TabIndex = 0;
            this.motionGroup.TabStop = false;
            this.motionGroup.Text = "Pan, tilt and zoom";
            // 
            // movementScroll
            // 
            this.movementScroll.AutoScroll = true;
            this.movementScroll.Controls.Add(this.generalFields);
            this.movementScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movementScroll.Location = new System.Drawing.Point(5, 24);
            this.movementScroll.Name = "movementScroll";
            this.movementScroll.Size = new System.Drawing.Size(284, 272);
            this.movementScroll.TabIndex = 0;
            // 
            // generalFields
            // 
            this.generalFields.AutoSize = true;
            this.generalFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.generalFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.generalFields.ColumnCount = 2;
            this.generalFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.generalFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.generalFields.Controls.Add(this.finePanLabel, 0, 0);
            this.generalFields.Controls.Add(this.finePanPercent, 1, 0);
            this.generalFields.Controls.Add(this.panStepsLabel, 0, 1);
            this.generalFields.Controls.Add(this.panSteps, 1, 1);
            this.generalFields.Controls.Add(this.fastPanStepsLabel, 0, 2);
            this.generalFields.Controls.Add(this.fastPanSteps, 1, 2);
            this.generalFields.Controls.Add(this.zoomStepsLabel, 0, 3);
            this.generalFields.Controls.Add(this.zoomSteps, 1, 3);
            this.generalFields.Controls.Add(this.useTransitionSpeed, 0, 4);
            this.generalFields.Controls.Add(this.transitionSpeedLabel, 0, 5);
            this.generalFields.Controls.Add(this.transitionSpeedPercent, 1, 5);
            this.generalFields.Controls.Add(this.secondsLabel, 0, 6);
            this.generalFields.Controls.Add(this.seconds, 1, 6);
            this.generalFields.Controls.Add(this.reverseX, 0, 7);
            this.generalFields.Controls.Add(this.reverseY, 0, 8);
            this.generalFields.Controls.Add(this.startupFovLabel, 0, 9);
            this.generalFields.Controls.Add(this.startupFov, 1, 9);
            this.generalFields.Controls.Add(this.allowFov, 0, 10);
            this.generalFields.Controls.Add(this.adjustFovAtLimits, 0, 11);
            this.generalFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.generalFields.Location = new System.Drawing.Point(0, 0);
            this.generalFields.Margin = new System.Windows.Forms.Padding(0);
            this.generalFields.Name = "generalFields";
            this.generalFields.RowCount = 12;
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.generalFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.generalFields.Size = new System.Drawing.Size(263, 547);
            this.generalFields.TabIndex = 0;
            // 
            // finePanLabel
            // 
            this.finePanLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.finePanLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.finePanLabel.Location = new System.Drawing.Point(4, 0);
            this.finePanLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.finePanLabel.Name = "finePanLabel";
            this.finePanLabel.Size = new System.Drawing.Size(155, 46);
            this.finePanLabel.TabIndex = 0;
            this.finePanLabel.Text = "Fine speed (% of Normal)";
            this.finePanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // finePanPercent
            // 
            this.finePanPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.finePanPercent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.finePanPercent.ForeColor = System.Drawing.Color.White;
            this.finePanPercent.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.finePanPercent.Location = new System.Drawing.Point(167, 4);
            this.finePanPercent.Margin = new System.Windows.Forms.Padding(4);
            this.finePanPercent.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.finePanPercent.MinimumSize = new System.Drawing.Size(65, 0);
            this.finePanPercent.Name = "finePanPercent";
            this.finePanPercent.Size = new System.Drawing.Size(92, 27);
            this.finePanPercent.TabIndex = 1;
            this.finePanPercent.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // panStepsLabel
            // 
            this.panStepsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.panStepsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panStepsLabel.Location = new System.Drawing.Point(4, 46);
            this.panStepsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.panStepsLabel.Name = "panStepsLabel";
            this.panStepsLabel.Size = new System.Drawing.Size(155, 36);
            this.panStepsLabel.TabIndex = 2;
            this.panStepsLabel.Text = "Normal pan steps";
            this.panStepsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panSteps
            // 
            this.panSteps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.panSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panSteps.ForeColor = System.Drawing.Color.White;
            this.panSteps.Location = new System.Drawing.Point(167, 50);
            this.panSteps.Margin = new System.Windows.Forms.Padding(4);
            this.panSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.panSteps.MinimumSize = new System.Drawing.Size(65, 0);
            this.panSteps.Name = "panSteps";
            this.panSteps.Size = new System.Drawing.Size(92, 27);
            this.panSteps.TabIndex = 3;
            this.panSteps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fastPanStepsLabel
            // 
            this.fastPanStepsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.fastPanStepsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastPanStepsLabel.Location = new System.Drawing.Point(4, 82);
            this.fastPanStepsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fastPanStepsLabel.Name = "fastPanStepsLabel";
            this.fastPanStepsLabel.Size = new System.Drawing.Size(155, 36);
            this.fastPanStepsLabel.TabIndex = 4;
            this.fastPanStepsLabel.Text = "Fast pan steps";
            this.fastPanStepsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fastPanSteps
            // 
            this.fastPanSteps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.fastPanSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastPanSteps.ForeColor = System.Drawing.Color.White;
            this.fastPanSteps.Location = new System.Drawing.Point(167, 86);
            this.fastPanSteps.Margin = new System.Windows.Forms.Padding(4);
            this.fastPanSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fastPanSteps.MinimumSize = new System.Drawing.Size(65, 0);
            this.fastPanSteps.Name = "fastPanSteps";
            this.fastPanSteps.Size = new System.Drawing.Size(92, 27);
            this.fastPanSteps.TabIndex = 5;
            this.fastPanSteps.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // zoomStepsLabel
            // 
            this.zoomStepsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.zoomStepsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomStepsLabel.Location = new System.Drawing.Point(4, 118);
            this.zoomStepsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.zoomStepsLabel.Name = "zoomStepsLabel";
            this.zoomStepsLabel.Size = new System.Drawing.Size(155, 36);
            this.zoomStepsLabel.TabIndex = 6;
            this.zoomStepsLabel.Text = "Zoom steps";
            this.zoomStepsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zoomSteps
            // 
            this.zoomSteps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.zoomSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomSteps.ForeColor = System.Drawing.Color.White;
            this.zoomSteps.Location = new System.Drawing.Point(167, 122);
            this.zoomSteps.Margin = new System.Windows.Forms.Padding(4);
            this.zoomSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zoomSteps.MinimumSize = new System.Drawing.Size(65, 0);
            this.zoomSteps.Name = "zoomSteps";
            this.zoomSteps.Size = new System.Drawing.Size(92, 27);
            this.zoomSteps.TabIndex = 7;
            this.zoomSteps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // useTransitionSpeed
            // 
            this.useTransitionSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.generalFields.SetColumnSpan(this.useTransitionSpeed, 2);
            this.useTransitionSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.useTransitionSpeed.Location = new System.Drawing.Point(4, 156);
            this.useTransitionSpeed.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.useTransitionSpeed.Name = "useTransitionSpeed";
            this.useTransitionSpeed.Size = new System.Drawing.Size(255, 36);
            this.useTransitionSpeed.TabIndex = 8;
            this.useTransitionSpeed.Text = "Use speed for preset transitions";
            this.useTransitionSpeed.UseVisualStyleBackColor = false;
            // 
            // transitionSpeedLabel
            // 
            this.transitionSpeedLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.transitionSpeedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transitionSpeedLabel.Location = new System.Drawing.Point(4, 194);
            this.transitionSpeedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.transitionSpeedLabel.Name = "transitionSpeedLabel";
            this.transitionSpeedLabel.Size = new System.Drawing.Size(155, 46);
            this.transitionSpeedLabel.TabIndex = 9;
            this.transitionSpeedLabel.Text = "Transition speed (%/s)";
            this.transitionSpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // transitionSpeedPercent
            // 
            this.transitionSpeedPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.transitionSpeedPercent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transitionSpeedPercent.Enabled = false;
            this.transitionSpeedPercent.ForeColor = System.Drawing.Color.White;
            this.transitionSpeedPercent.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.transitionSpeedPercent.Location = new System.Drawing.Point(167, 198);
            this.transitionSpeedPercent.Margin = new System.Windows.Forms.Padding(4);
            this.transitionSpeedPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.transitionSpeedPercent.MinimumSize = new System.Drawing.Size(65, 0);
            this.transitionSpeedPercent.Name = "transitionSpeedPercent";
            this.transitionSpeedPercent.Size = new System.Drawing.Size(92, 27);
            this.transitionSpeedPercent.TabIndex = 10;
            this.transitionSpeedPercent.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // secondsLabel
            // 
            this.secondsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.secondsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondsLabel.Location = new System.Drawing.Point(4, 240);
            this.secondsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(155, 36);
            this.secondsLabel.TabIndex = 11;
            this.secondsLabel.Text = "Transition (sec.)";
            this.secondsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // seconds
            // 
            this.seconds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.seconds.DecimalPlaces = 1;
            this.seconds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seconds.ForeColor = System.Drawing.Color.White;
            this.seconds.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.seconds.Location = new System.Drawing.Point(167, 244);
            this.seconds.Margin = new System.Windows.Forms.Padding(4);
            this.seconds.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.seconds.MinimumSize = new System.Drawing.Size(65, 0);
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(92, 27);
            this.seconds.TabIndex = 12;
            this.seconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // reverseX
            // 
            this.reverseX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.generalFields.SetColumnSpan(this.reverseX, 2);
            this.reverseX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reverseX.Location = new System.Drawing.Point(4, 280);
            this.reverseX.Margin = new System.Windows.Forms.Padding(4);
            this.reverseX.Name = "reverseX";
            this.reverseX.Size = new System.Drawing.Size(255, 34);
            this.reverseX.TabIndex = 13;
            this.reverseX.Text = "Camera pan reverse left / right";
            this.reverseX.UseVisualStyleBackColor = false;
            // 
            // reverseY
            // 
            this.reverseY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.generalFields.SetColumnSpan(this.reverseY, 2);
            this.reverseY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reverseY.Location = new System.Drawing.Point(4, 322);
            this.reverseY.Margin = new System.Windows.Forms.Padding(4);
            this.reverseY.Name = "reverseY";
            this.reverseY.Size = new System.Drawing.Size(255, 34);
            this.reverseY.TabIndex = 14;
            this.reverseY.Text = "Camera pan reverse up / down";
            this.reverseY.UseVisualStyleBackColor = false;
            // 
            // startupFovLabel
            // 
            this.startupFovLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startupFovLabel.Location = new System.Drawing.Point(3, 360);
            this.startupFovLabel.Name = "startupFovLabel";
            this.startupFovLabel.Size = new System.Drawing.Size(157, 95);
            this.startupFovLabel.TabIndex = 15;
            this.startupFovLabel.Text = "Field of view at startup";
            this.startupFovLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startupFov
            // 
            this.startupFov.AccessibleName = "Field of view at startup";
            this.startupFov.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startupFov.IntegralHeight = false;
            this.startupFov.ItemHeight = 20;
            this.startupFov.Location = new System.Drawing.Point(166, 363);
            this.startupFov.Name = "startupFov";
            this.startupFov.Size = new System.Drawing.Size(94, 89);
            this.startupFov.TabIndex = 9;
            // 
            // allowFov
            // 
            this.generalFields.SetColumnSpan(this.allowFov, 2);
            this.allowFov.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allowFov.Location = new System.Drawing.Point(3, 458);
            this.allowFov.Name = "allowFov";
            this.allowFov.Size = new System.Drawing.Size(257, 36);
            this.allowFov.TabIndex = 10;
            this.allowFov.Text = "Allow manual FOV changes";
            // 
            // adjustFovAtLimits
            // 
            this.adjustFovAtLimits.Checked = true;
            this.adjustFovAtLimits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generalFields.SetColumnSpan(this.adjustFovAtLimits, 2);
            this.adjustFovAtLimits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adjustFovAtLimits.Location = new System.Drawing.Point(3, 500);
            this.adjustFovAtLimits.Name = "adjustFovAtLimits";
            this.adjustFovAtLimits.Size = new System.Drawing.Size(257, 44);
            this.adjustFovAtLimits.TabIndex = 11;
            this.adjustFovAtLimits.Text = "Adjust field of view at zoom and pan limits";
            // 
            // applicationTab
            // 
            this.applicationTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.applicationTab.Controls.Add(this.applicationScroll);
            this.applicationTab.Location = new System.Drawing.Point(4, 29);
            this.applicationTab.Name = "applicationTab";
            this.applicationTab.Size = new System.Drawing.Size(294, 300);
            this.applicationTab.TabIndex = 1;
            this.applicationTab.Text = "Application";
            // 
            // applicationScroll
            // 
            this.applicationScroll.AutoScroll = true;
            this.applicationScroll.Controls.Add(this.applicationFields);
            this.applicationScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationScroll.Location = new System.Drawing.Point(0, 0);
            this.applicationScroll.Name = "applicationScroll";
            this.applicationScroll.Size = new System.Drawing.Size(294, 300);
            this.applicationScroll.TabIndex = 0;
            // 
            // applicationFields
            // 
            this.applicationFields.AutoSize = true;
            this.applicationFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.applicationFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.applicationFields.ColumnCount = 1;
            this.applicationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.applicationFields.Controls.Add(this.windowGroup, 0, 0);
            this.applicationFields.Controls.Add(this.languageGroup, 0, 1);
            this.applicationFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.applicationFields.Location = new System.Drawing.Point(0, 0);
            this.applicationFields.Name = "applicationFields";
            this.applicationFields.Padding = new System.Windows.Forms.Padding(4);
            this.applicationFields.RowCount = 2;
            this.applicationFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.applicationFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.applicationFields.Size = new System.Drawing.Size(294, 183);
            this.applicationFields.TabIndex = 0;
            // 
            // windowGroup
            // 
            this.windowGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.windowGroup.Controls.Add(this.windowFields);
            this.windowGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowGroup.ForeColor = System.Drawing.Color.White;
            this.windowGroup.Location = new System.Drawing.Point(7, 7);
            this.windowGroup.Name = "windowGroup";
            this.windowGroup.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.windowGroup.Size = new System.Drawing.Size(280, 108);
            this.windowGroup.TabIndex = 0;
            this.windowGroup.TabStop = false;
            this.windowGroup.Text = "Window and streaming";
            // 
            // windowFields
            // 
            this.windowFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.windowFields.ColumnCount = 1;
            this.windowFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.windowFields.Controls.Add(this.streamingCheck, 0, 0);
            this.windowFields.Controls.Add(this.alwaysTop, 0, 1);
            this.windowFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowFields.Location = new System.Drawing.Point(5, 24);
            this.windowFields.Name = "windowFields";
            this.windowFields.RowCount = 2;
            this.windowFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.windowFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.windowFields.Size = new System.Drawing.Size(270, 80);
            this.windowFields.TabIndex = 0;
            // 
            // streamingCheck
            // 
            this.streamingCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.streamingCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streamingCheck.Location = new System.Drawing.Point(4, 4);
            this.streamingCheck.Margin = new System.Windows.Forms.Padding(4);
            this.streamingCheck.Name = "streamingCheck";
            this.streamingCheck.Size = new System.Drawing.Size(262, 44);
            this.streamingCheck.TabIndex = 10;
            this.streamingCheck.Text = "Ask whether streaming has started before using the camera";
            this.streamingCheck.UseVisualStyleBackColor = false;
            // 
            // alwaysTop
            // 
            this.alwaysTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.alwaysTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alwaysTop.Location = new System.Drawing.Point(4, 56);
            this.alwaysTop.Margin = new System.Windows.Forms.Padding(4);
            this.alwaysTop.Name = "alwaysTop";
            this.alwaysTop.Size = new System.Drawing.Size(262, 21);
            this.alwaysTop.TabIndex = 11;
            this.alwaysTop.Text = "Always on top";
            this.alwaysTop.UseVisualStyleBackColor = false;
            // 
            // languageGroup
            // 
            this.languageGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.languageGroup.Controls.Add(this.language);
            this.languageGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.languageGroup.ForeColor = System.Drawing.Color.White;
            this.languageGroup.Location = new System.Drawing.Point(7, 121);
            this.languageGroup.Name = "languageGroup";
            this.languageGroup.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.languageGroup.Size = new System.Drawing.Size(280, 55);
            this.languageGroup.TabIndex = 1;
            this.languageGroup.TabStop = false;
            this.languageGroup.Text = "Profile language";
            // 
            // language
            // 
            this.language.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.language.Dock = System.Windows.Forms.DockStyle.Top;
            this.language.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.language.ForeColor = System.Drawing.Color.White;
            this.language.ItemHeight = 22;
            this.language.Location = new System.Drawing.Point(5, 24);
            this.language.Margin = new System.Windows.Forms.Padding(4);
            this.language.Name = "language";
            this.language.Size = new System.Drawing.Size(270, 28);
            this.language.TabIndex = 13;
            // 
            // videoLayout
            // 
            this.videoLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.videoLayout.ColumnCount = 1;
            this.videoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.videoLayout.Controls.Add(this.videoStatus, 0, 0);
            this.videoLayout.Controls.Add(this.imagePages, 0, 1);
            this.videoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoLayout.Location = new System.Drawing.Point(10, 10);
            this.videoLayout.Name = "videoLayout";
            this.videoLayout.RowCount = 2;
            this.videoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.videoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.videoLayout.Size = new System.Drawing.Size(302, 333);
            this.videoLayout.TabIndex = 0;
            // 
            // videoStatus
            // 
            this.videoStatus.AutoSize = true;
            this.videoStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.videoStatus.Controls.Add(this.loadVideoControls);
            this.videoStatus.Controls.Add(this.noVideoControls);
            this.videoStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoStatus.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.videoStatus.Location = new System.Drawing.Point(3, 3);
            this.videoStatus.Name = "videoStatus";
            this.videoStatus.Size = new System.Drawing.Size(296, 78);
            this.videoStatus.TabIndex = 0;
            this.videoStatus.WrapContents = false;
            // 
            // loadVideoControls
            // 
            this.loadVideoControls.AutoSize = true;
            this.loadVideoControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.loadVideoControls.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadVideoControls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadVideoControls.ForeColor = System.Drawing.Color.White;
            this.loadVideoControls.Location = new System.Drawing.Point(3, 3);
            this.loadVideoControls.Name = "loadVideoControls";
            this.loadVideoControls.Primary = false;
            this.loadVideoControls.Size = new System.Drawing.Size(157, 32);
            this.loadVideoControls.TabIndex = 0;
            this.loadVideoControls.Text = "Load image controls";
            this.loadVideoControls.UseVisualStyleBackColor = false;
            // 
            // noVideoControls
            // 
            this.noVideoControls.AutoSize = true;
            this.noVideoControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.noVideoControls.Location = new System.Drawing.Point(3, 38);
            this.noVideoControls.MaximumSize = new System.Drawing.Size(246, 0);
            this.noVideoControls.Name = "noVideoControls";
            this.noVideoControls.Size = new System.Drawing.Size(200, 40);
            this.noVideoControls.TabIndex = 1;
            this.noVideoControls.Text = "No image controls available through the driver.";
            // 
            // imagePages
            // 
            this.imagePages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.imagePages.ColumnCount = 1;
            this.imagePages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.imagePages.Controls.Add(this.imageCategories, 0, 0);
            this.imagePages.Controls.Add(this.resetVideoControls, 0, 1);
            this.imagePages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePages.Location = new System.Drawing.Point(3, 87);
            this.imagePages.Name = "imagePages";
            this.imagePages.RowCount = 2;
            this.imagePages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.imagePages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.imagePages.Size = new System.Drawing.Size(296, 243);
            this.imagePages.TabIndex = 1;
            // 
            // imageCategories
            // 
            this.imageCategories.Controls.Add(this.focusTab);
            this.imageCategories.Controls.Add(this.colourTab);
            this.imageCategories.Controls.Add(this.lightTab);
            this.imageCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageCategories.Location = new System.Drawing.Point(3, 3);
            this.imageCategories.Name = "imageCategories";
            this.imageCategories.SelectedIndex = 0;
            this.imageCategories.Size = new System.Drawing.Size(290, 205);
            this.imageCategories.TabIndex = 0;
            // 
            // focusTab
            // 
            this.focusTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.focusTab.Controls.Add(this.focusPanel);
            this.focusTab.Location = new System.Drawing.Point(4, 29);
            this.focusTab.Name = "focusTab";
            this.focusTab.Size = new System.Drawing.Size(282, 172);
            this.focusTab.TabIndex = 0;
            this.focusTab.Text = "Focus and exposure";
            // 
            // focusPanel
            // 
            this.focusPanel.AutoScroll = true;
            this.focusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.focusPanel.Controls.Add(this.gainControl);
            this.focusPanel.Controls.Add(this.exposureControl);
            this.focusPanel.Controls.Add(this.focusControl);
            this.focusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.focusPanel.Location = new System.Drawing.Point(0, 0);
            this.focusPanel.Name = "focusPanel";
            this.focusPanel.Size = new System.Drawing.Size(282, 172);
            this.focusPanel.TabIndex = 0;
            // 
            // gainControl
            // 
            this.gainControl.AutoSize = true;
            this.gainControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gainControl.Controls.Add(this.gainAutomatic);
            this.gainControl.Controls.Add(this.gainSlider);
            this.gainControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.gainControl.ForeColor = System.Drawing.Color.White;
            this.gainControl.Location = new System.Drawing.Point(0, 150);
            this.gainControl.Name = "gainControl";
            this.gainControl.Size = new System.Drawing.Size(261, 75);
            this.gainControl.TabIndex = 2;
            this.gainControl.TabStop = false;
            this.gainControl.Tag = "Gain";
            this.gainControl.Text = "Gain";
            // 
            // gainAutomatic
            // 
            this.gainAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.gainAutomatic.Location = new System.Drawing.Point(3, 49);
            this.gainAutomatic.Name = "gainAutomatic";
            this.gainAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.gainAutomatic.Size = new System.Drawing.Size(255, 23);
            this.gainAutomatic.TabIndex = 1;
            this.gainAutomatic.Text = "Automatic";
            this.gainAutomatic.UseVisualStyleBackColor = true;
            // 
            // gainSlider
            // 
            this.gainSlider.AutoSize = false;
            this.gainSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.gainSlider.Location = new System.Drawing.Point(3, 23);
            this.gainSlider.Maximum = 1000;
            this.gainSlider.Name = "gainSlider";
            this.gainSlider.Size = new System.Drawing.Size(255, 26);
            this.gainSlider.TabIndex = 0;
            this.gainSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // exposureControl
            // 
            this.exposureControl.AutoSize = true;
            this.exposureControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.exposureControl.Controls.Add(this.exposureAutomatic);
            this.exposureControl.Controls.Add(this.exposureSlider);
            this.exposureControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.exposureControl.ForeColor = System.Drawing.Color.White;
            this.exposureControl.Location = new System.Drawing.Point(0, 75);
            this.exposureControl.Name = "exposureControl";
            this.exposureControl.Size = new System.Drawing.Size(261, 75);
            this.exposureControl.TabIndex = 1;
            this.exposureControl.TabStop = false;
            this.exposureControl.Tag = "Exposure";
            this.exposureControl.Text = "Exposure";
            // 
            // exposureAutomatic
            // 
            this.exposureAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.exposureAutomatic.Location = new System.Drawing.Point(3, 49);
            this.exposureAutomatic.Name = "exposureAutomatic";
            this.exposureAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.exposureAutomatic.Size = new System.Drawing.Size(255, 23);
            this.exposureAutomatic.TabIndex = 1;
            this.exposureAutomatic.Text = "Automatic";
            this.exposureAutomatic.UseVisualStyleBackColor = true;
            // 
            // exposureSlider
            // 
            this.exposureSlider.AutoSize = false;
            this.exposureSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.exposureSlider.Location = new System.Drawing.Point(3, 23);
            this.exposureSlider.Maximum = 1000;
            this.exposureSlider.Name = "exposureSlider";
            this.exposureSlider.Size = new System.Drawing.Size(255, 26);
            this.exposureSlider.TabIndex = 0;
            this.exposureSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // focusControl
            // 
            this.focusControl.AutoSize = true;
            this.focusControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.focusControl.Controls.Add(this.focusAutomatic);
            this.focusControl.Controls.Add(this.focusSlider);
            this.focusControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.focusControl.ForeColor = System.Drawing.Color.White;
            this.focusControl.Location = new System.Drawing.Point(0, 0);
            this.focusControl.Name = "focusControl";
            this.focusControl.Size = new System.Drawing.Size(261, 75);
            this.focusControl.TabIndex = 0;
            this.focusControl.TabStop = false;
            this.focusControl.Tag = "Focus";
            this.focusControl.Text = "Focus";
            // 
            // focusAutomatic
            // 
            this.focusAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.focusAutomatic.Location = new System.Drawing.Point(3, 49);
            this.focusAutomatic.Name = "focusAutomatic";
            this.focusAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.focusAutomatic.Size = new System.Drawing.Size(255, 23);
            this.focusAutomatic.TabIndex = 1;
            this.focusAutomatic.Text = "Automatic";
            this.focusAutomatic.UseVisualStyleBackColor = true;
            // 
            // focusSlider
            // 
            this.focusSlider.AutoSize = false;
            this.focusSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.focusSlider.Location = new System.Drawing.Point(3, 23);
            this.focusSlider.Maximum = 1000;
            this.focusSlider.Name = "focusSlider";
            this.focusSlider.Size = new System.Drawing.Size(255, 26);
            this.focusSlider.TabIndex = 0;
            this.focusSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // colourTab
            // 
            this.colourTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.colourTab.Controls.Add(this.colourPanel);
            this.colourTab.Location = new System.Drawing.Point(4, 29);
            this.colourTab.Name = "colourTab";
            this.colourTab.Size = new System.Drawing.Size(282, 180);
            this.colourTab.TabIndex = 1;
            this.colourTab.Text = "Colour";
            // 
            // colourPanel
            // 
            this.colourPanel.AutoScroll = true;
            this.colourPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.colourPanel.Controls.Add(this.whiteBalanceControl);
            this.colourPanel.Controls.Add(this.saturationControl);
            this.colourPanel.Controls.Add(this.gammaControl);
            this.colourPanel.Controls.Add(this.hueControl);
            this.colourPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colourPanel.Location = new System.Drawing.Point(0, 0);
            this.colourPanel.Name = "colourPanel";
            this.colourPanel.Size = new System.Drawing.Size(282, 180);
            this.colourPanel.TabIndex = 0;
            // 
            // whiteBalanceControl
            // 
            this.whiteBalanceControl.AutoSize = true;
            this.whiteBalanceControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.whiteBalanceControl.Controls.Add(this.whiteBalanceAutomatic);
            this.whiteBalanceControl.Controls.Add(this.whiteBalanceSlider);
            this.whiteBalanceControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.whiteBalanceControl.ForeColor = System.Drawing.Color.White;
            this.whiteBalanceControl.Location = new System.Drawing.Point(0, 225);
            this.whiteBalanceControl.Name = "whiteBalanceControl";
            this.whiteBalanceControl.Size = new System.Drawing.Size(261, 75);
            this.whiteBalanceControl.TabIndex = 3;
            this.whiteBalanceControl.TabStop = false;
            this.whiteBalanceControl.Tag = "White balance";
            this.whiteBalanceControl.Text = "White balance";
            // 
            // whiteBalanceAutomatic
            // 
            this.whiteBalanceAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.whiteBalanceAutomatic.Location = new System.Drawing.Point(3, 49);
            this.whiteBalanceAutomatic.Name = "whiteBalanceAutomatic";
            this.whiteBalanceAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.whiteBalanceAutomatic.Size = new System.Drawing.Size(255, 23);
            this.whiteBalanceAutomatic.TabIndex = 1;
            this.whiteBalanceAutomatic.Text = "Automatic";
            this.whiteBalanceAutomatic.UseVisualStyleBackColor = true;
            // 
            // whiteBalanceSlider
            // 
            this.whiteBalanceSlider.AutoSize = false;
            this.whiteBalanceSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.whiteBalanceSlider.Location = new System.Drawing.Point(3, 23);
            this.whiteBalanceSlider.Maximum = 1000;
            this.whiteBalanceSlider.Name = "whiteBalanceSlider";
            this.whiteBalanceSlider.Size = new System.Drawing.Size(255, 26);
            this.whiteBalanceSlider.TabIndex = 0;
            this.whiteBalanceSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // saturationControl
            // 
            this.saturationControl.AutoSize = true;
            this.saturationControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saturationControl.Controls.Add(this.saturationAutomatic);
            this.saturationControl.Controls.Add(this.saturationSlider);
            this.saturationControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.saturationControl.ForeColor = System.Drawing.Color.White;
            this.saturationControl.Location = new System.Drawing.Point(0, 150);
            this.saturationControl.Name = "saturationControl";
            this.saturationControl.Size = new System.Drawing.Size(261, 75);
            this.saturationControl.TabIndex = 2;
            this.saturationControl.TabStop = false;
            this.saturationControl.Tag = "Saturation";
            this.saturationControl.Text = "Saturation";
            // 
            // saturationAutomatic
            // 
            this.saturationAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.saturationAutomatic.Location = new System.Drawing.Point(3, 49);
            this.saturationAutomatic.Name = "saturationAutomatic";
            this.saturationAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.saturationAutomatic.Size = new System.Drawing.Size(255, 23);
            this.saturationAutomatic.TabIndex = 1;
            this.saturationAutomatic.Text = "Automatic";
            this.saturationAutomatic.UseVisualStyleBackColor = true;
            // 
            // saturationSlider
            // 
            this.saturationSlider.AutoSize = false;
            this.saturationSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.saturationSlider.Location = new System.Drawing.Point(3, 23);
            this.saturationSlider.Maximum = 1000;
            this.saturationSlider.Name = "saturationSlider";
            this.saturationSlider.Size = new System.Drawing.Size(255, 26);
            this.saturationSlider.TabIndex = 0;
            this.saturationSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // gammaControl
            // 
            this.gammaControl.AutoSize = true;
            this.gammaControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gammaControl.Controls.Add(this.gammaAutomatic);
            this.gammaControl.Controls.Add(this.gammaSlider);
            this.gammaControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.gammaControl.ForeColor = System.Drawing.Color.White;
            this.gammaControl.Location = new System.Drawing.Point(0, 75);
            this.gammaControl.Name = "gammaControl";
            this.gammaControl.Size = new System.Drawing.Size(261, 75);
            this.gammaControl.TabIndex = 1;
            this.gammaControl.TabStop = false;
            this.gammaControl.Tag = "Gamma";
            this.gammaControl.Text = "Gamma";
            // 
            // gammaAutomatic
            // 
            this.gammaAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.gammaAutomatic.Location = new System.Drawing.Point(3, 49);
            this.gammaAutomatic.Name = "gammaAutomatic";
            this.gammaAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.gammaAutomatic.Size = new System.Drawing.Size(255, 23);
            this.gammaAutomatic.TabIndex = 1;
            this.gammaAutomatic.Text = "Automatic";
            this.gammaAutomatic.UseVisualStyleBackColor = true;
            // 
            // gammaSlider
            // 
            this.gammaSlider.AutoSize = false;
            this.gammaSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.gammaSlider.Location = new System.Drawing.Point(3, 23);
            this.gammaSlider.Maximum = 1000;
            this.gammaSlider.Name = "gammaSlider";
            this.gammaSlider.Size = new System.Drawing.Size(255, 26);
            this.gammaSlider.TabIndex = 0;
            this.gammaSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // hueControl
            // 
            this.hueControl.AutoSize = true;
            this.hueControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hueControl.Controls.Add(this.hueAutomatic);
            this.hueControl.Controls.Add(this.hueSlider);
            this.hueControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.hueControl.ForeColor = System.Drawing.Color.White;
            this.hueControl.Location = new System.Drawing.Point(0, 0);
            this.hueControl.Name = "hueControl";
            this.hueControl.Size = new System.Drawing.Size(261, 75);
            this.hueControl.TabIndex = 0;
            this.hueControl.TabStop = false;
            this.hueControl.Tag = "Hue";
            this.hueControl.Text = "Hue";
            // 
            // hueAutomatic
            // 
            this.hueAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.hueAutomatic.Location = new System.Drawing.Point(3, 49);
            this.hueAutomatic.Name = "hueAutomatic";
            this.hueAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.hueAutomatic.Size = new System.Drawing.Size(255, 23);
            this.hueAutomatic.TabIndex = 1;
            this.hueAutomatic.Text = "Automatic";
            this.hueAutomatic.UseVisualStyleBackColor = true;
            // 
            // hueSlider
            // 
            this.hueSlider.AutoSize = false;
            this.hueSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.hueSlider.Location = new System.Drawing.Point(3, 23);
            this.hueSlider.Maximum = 1000;
            this.hueSlider.Name = "hueSlider";
            this.hueSlider.Size = new System.Drawing.Size(255, 26);
            this.hueSlider.TabIndex = 0;
            this.hueSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // lightTab
            // 
            this.lightTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.lightTab.Controls.Add(this.lightPanel);
            this.lightTab.Location = new System.Drawing.Point(4, 29);
            this.lightTab.Name = "lightTab";
            this.lightTab.Size = new System.Drawing.Size(282, 180);
            this.lightTab.TabIndex = 2;
            this.lightTab.Text = "Light and detail";
            // 
            // lightPanel
            // 
            this.lightPanel.AutoScroll = true;
            this.lightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.lightPanel.Controls.Add(this.contrastControl);
            this.lightPanel.Controls.Add(this.brightnessControl);
            this.lightPanel.Controls.Add(this.backlightControl);
            this.lightPanel.Controls.Add(this.sharpnessControl);
            this.lightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lightPanel.Location = new System.Drawing.Point(0, 0);
            this.lightPanel.Name = "lightPanel";
            this.lightPanel.Size = new System.Drawing.Size(282, 180);
            this.lightPanel.TabIndex = 0;
            // 
            // contrastControl
            // 
            this.contrastControl.AutoSize = true;
            this.contrastControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contrastControl.Controls.Add(this.contrastAutomatic);
            this.contrastControl.Controls.Add(this.contrastSlider);
            this.contrastControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.contrastControl.ForeColor = System.Drawing.Color.White;
            this.contrastControl.Location = new System.Drawing.Point(0, 225);
            this.contrastControl.Name = "contrastControl";
            this.contrastControl.Size = new System.Drawing.Size(261, 75);
            this.contrastControl.TabIndex = 3;
            this.contrastControl.TabStop = false;
            this.contrastControl.Tag = "Contrast";
            this.contrastControl.Text = "Contrast";
            // 
            // contrastAutomatic
            // 
            this.contrastAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.contrastAutomatic.Location = new System.Drawing.Point(3, 49);
            this.contrastAutomatic.Name = "contrastAutomatic";
            this.contrastAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.contrastAutomatic.Size = new System.Drawing.Size(255, 23);
            this.contrastAutomatic.TabIndex = 1;
            this.contrastAutomatic.Text = "Automatic";
            this.contrastAutomatic.UseVisualStyleBackColor = true;
            // 
            // contrastSlider
            // 
            this.contrastSlider.AutoSize = false;
            this.contrastSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.contrastSlider.Location = new System.Drawing.Point(3, 23);
            this.contrastSlider.Maximum = 1000;
            this.contrastSlider.Name = "contrastSlider";
            this.contrastSlider.Size = new System.Drawing.Size(255, 26);
            this.contrastSlider.TabIndex = 0;
            this.contrastSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // brightnessControl
            // 
            this.brightnessControl.AutoSize = true;
            this.brightnessControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.brightnessControl.Controls.Add(this.brightnessAutomatic);
            this.brightnessControl.Controls.Add(this.brightnessSlider);
            this.brightnessControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.brightnessControl.ForeColor = System.Drawing.Color.White;
            this.brightnessControl.Location = new System.Drawing.Point(0, 150);
            this.brightnessControl.Name = "brightnessControl";
            this.brightnessControl.Size = new System.Drawing.Size(261, 75);
            this.brightnessControl.TabIndex = 2;
            this.brightnessControl.TabStop = false;
            this.brightnessControl.Tag = "Brightness";
            this.brightnessControl.Text = "Brightness";
            // 
            // brightnessAutomatic
            // 
            this.brightnessAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.brightnessAutomatic.Location = new System.Drawing.Point(3, 49);
            this.brightnessAutomatic.Name = "brightnessAutomatic";
            this.brightnessAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.brightnessAutomatic.Size = new System.Drawing.Size(255, 23);
            this.brightnessAutomatic.TabIndex = 1;
            this.brightnessAutomatic.Text = "Automatic";
            this.brightnessAutomatic.UseVisualStyleBackColor = true;
            // 
            // brightnessSlider
            // 
            this.brightnessSlider.AutoSize = false;
            this.brightnessSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.brightnessSlider.Location = new System.Drawing.Point(3, 23);
            this.brightnessSlider.Maximum = 1000;
            this.brightnessSlider.Name = "brightnessSlider";
            this.brightnessSlider.Size = new System.Drawing.Size(255, 26);
            this.brightnessSlider.TabIndex = 0;
            this.brightnessSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // backlightControl
            // 
            this.backlightControl.AutoSize = true;
            this.backlightControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.backlightControl.Controls.Add(this.backlightAutomatic);
            this.backlightControl.Controls.Add(this.backlightSlider);
            this.backlightControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.backlightControl.ForeColor = System.Drawing.Color.White;
            this.backlightControl.Location = new System.Drawing.Point(0, 75);
            this.backlightControl.Name = "backlightControl";
            this.backlightControl.Size = new System.Drawing.Size(261, 75);
            this.backlightControl.TabIndex = 1;
            this.backlightControl.TabStop = false;
            this.backlightControl.Tag = "Backlight compensation";
            this.backlightControl.Text = "Backlight compensation";
            // 
            // backlightAutomatic
            // 
            this.backlightAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.backlightAutomatic.Location = new System.Drawing.Point(3, 49);
            this.backlightAutomatic.Name = "backlightAutomatic";
            this.backlightAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.backlightAutomatic.Size = new System.Drawing.Size(255, 23);
            this.backlightAutomatic.TabIndex = 1;
            this.backlightAutomatic.Text = "Automatic";
            this.backlightAutomatic.UseVisualStyleBackColor = true;
            // 
            // backlightSlider
            // 
            this.backlightSlider.AutoSize = false;
            this.backlightSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.backlightSlider.Location = new System.Drawing.Point(3, 23);
            this.backlightSlider.Maximum = 1000;
            this.backlightSlider.Name = "backlightSlider";
            this.backlightSlider.Size = new System.Drawing.Size(255, 26);
            this.backlightSlider.TabIndex = 0;
            this.backlightSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // sharpnessControl
            // 
            this.sharpnessControl.AutoSize = true;
            this.sharpnessControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sharpnessControl.Controls.Add(this.sharpnessAutomatic);
            this.sharpnessControl.Controls.Add(this.sharpnessSlider);
            this.sharpnessControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.sharpnessControl.ForeColor = System.Drawing.Color.White;
            this.sharpnessControl.Location = new System.Drawing.Point(0, 0);
            this.sharpnessControl.Name = "sharpnessControl";
            this.sharpnessControl.Size = new System.Drawing.Size(261, 75);
            this.sharpnessControl.TabIndex = 0;
            this.sharpnessControl.TabStop = false;
            this.sharpnessControl.Tag = "Sharpness";
            this.sharpnessControl.Text = "Sharpness";
            // 
            // sharpnessAutomatic
            // 
            this.sharpnessAutomatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.sharpnessAutomatic.Location = new System.Drawing.Point(3, 49);
            this.sharpnessAutomatic.Name = "sharpnessAutomatic";
            this.sharpnessAutomatic.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.sharpnessAutomatic.Size = new System.Drawing.Size(255, 23);
            this.sharpnessAutomatic.TabIndex = 1;
            this.sharpnessAutomatic.Text = "Automatic";
            this.sharpnessAutomatic.UseVisualStyleBackColor = true;
            // 
            // sharpnessSlider
            // 
            this.sharpnessSlider.AutoSize = false;
            this.sharpnessSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.sharpnessSlider.Location = new System.Drawing.Point(3, 23);
            this.sharpnessSlider.Maximum = 1000;
            this.sharpnessSlider.Name = "sharpnessSlider";
            this.sharpnessSlider.Size = new System.Drawing.Size(255, 26);
            this.sharpnessSlider.TabIndex = 0;
            this.sharpnessSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // resetVideoControls
            // 
            this.resetVideoControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.resetVideoControls.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetVideoControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetVideoControls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetVideoControls.ForeColor = System.Drawing.Color.White;
            this.resetVideoControls.Location = new System.Drawing.Point(3, 214);
            this.resetVideoControls.Name = "resetVideoControls";
            this.resetVideoControls.Primary = false;
            this.resetVideoControls.Size = new System.Drawing.Size(290, 26);
            this.resetVideoControls.TabIndex = 1;
            this.resetVideoControls.Text = "Reset image to driver defaults";
            this.resetVideoControls.UseVisualStyleBackColor = false;
            // 
            // issuesTab
            // 
            this.issuesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.issuesTab.Controls.Add(this.issueGroups);
            this.issuesTab.Location = new System.Drawing.Point(4, 54);
            this.issuesTab.Name = "issuesTab";
            this.issuesTab.Size = new System.Drawing.Size(322, 353);
            this.issuesTab.TabIndex = 4;
            this.issuesTab.Text = "Issues";
            // 
            // issueGroups
            // 
            this.issueGroups.AutoScroll = true;
            this.issueGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.issueGroups.Controls.Add(this.logGroup);
            this.issueGroups.Controls.Add(this.reportGroup);
            this.issueGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.issueGroups.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.issueGroups.Location = new System.Drawing.Point(0, 0);
            this.issueGroups.Name = "issueGroups";
            this.issueGroups.Size = new System.Drawing.Size(322, 353);
            this.issueGroups.TabIndex = 0;
            this.issueGroups.WrapContents = false;
            // 
            // logGroup
            // 
            this.logGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.logGroup.Controls.Add(this.logActions);
            this.logGroup.ForeColor = System.Drawing.Color.White;
            this.logGroup.Location = new System.Drawing.Point(3, 3);
            this.logGroup.Name = "logGroup";
            this.logGroup.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.logGroup.Size = new System.Drawing.Size(270, 95);
            this.logGroup.TabIndex = 0;
            this.logGroup.TabStop = false;
            this.logGroup.Text = "Diagnostic logs";
            // 
            // logActions
            // 
            this.logActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.logActions.Controls.Add(this.logSettingsLink);
            this.logActions.Controls.Add(this.showLogLink);
            this.logActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logActions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.logActions.Location = new System.Drawing.Point(5, 24);
            this.logActions.Name = "logActions";
            this.logActions.Size = new System.Drawing.Size(260, 67);
            this.logActions.TabIndex = 0;
            this.logActions.WrapContents = false;
            // 
            // logSettingsLink
            // 
            this.logSettingsLink.ActiveLinkColor = System.Drawing.Color.White;
            this.logSettingsLink.AutoSize = true;
            this.logSettingsLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.logSettingsLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.logSettingsLink.Location = new System.Drawing.Point(5, 3);
            this.logSettingsLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.logSettingsLink.MaximumSize = new System.Drawing.Size(250, 0);
            this.logSettingsLink.Name = "logSettingsLink";
            this.logSettingsLink.Size = new System.Drawing.Size(89, 20);
            this.logSettingsLink.TabIndex = 0;
            this.logSettingsLink.TabStop = true;
            this.logSettingsLink.Text = "Log settings";
            this.logSettingsLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // showLogLink
            // 
            this.showLogLink.ActiveLinkColor = System.Drawing.Color.White;
            this.showLogLink.AutoSize = true;
            this.showLogLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.showLogLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.showLogLink.Location = new System.Drawing.Point(5, 29);
            this.showLogLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.showLogLink.MaximumSize = new System.Drawing.Size(250, 0);
            this.showLogLink.Name = "showLogLink";
            this.showLogLink.Size = new System.Drawing.Size(96, 20);
            this.showLogLink.TabIndex = 1;
            this.showLogLink.TabStop = true;
            this.showLogLink.Text = "Show log file";
            this.showLogLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // reportGroup
            // 
            this.reportGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.reportGroup.Controls.Add(this.reportActions);
            this.reportGroup.ForeColor = System.Drawing.Color.White;
            this.reportGroup.Location = new System.Drawing.Point(3, 104);
            this.reportGroup.Name = "reportGroup";
            this.reportGroup.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.reportGroup.Size = new System.Drawing.Size(270, 124);
            this.reportGroup.TabIndex = 1;
            this.reportGroup.TabStop = false;
            this.reportGroup.Text = "Report a problem";
            // 
            // reportActions
            // 
            this.reportActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.reportActions.Controls.Add(this.reportNote);
            this.reportActions.Controls.Add(this.reportLink);
            this.reportActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportActions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.reportActions.Location = new System.Drawing.Point(5, 24);
            this.reportActions.Name = "reportActions";
            this.reportActions.Size = new System.Drawing.Size(260, 96);
            this.reportActions.TabIndex = 0;
            this.reportActions.WrapContents = false;
            // 
            // reportNote
            // 
            this.reportNote.AutoSize = true;
            this.reportNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.reportNote.Location = new System.Drawing.Point(3, 0);
            this.reportNote.MaximumSize = new System.Drawing.Size(246, 0);
            this.reportNote.Name = "reportNote";
            this.reportNote.Size = new System.Drawing.Size(229, 40);
            this.reportNote.TabIndex = 0;
            this.reportNote.Text = "Review the exported logs and attach them to your GitHub issue.";
            // 
            // reportLink
            // 
            this.reportLink.ActiveLinkColor = System.Drawing.Color.White;
            this.reportLink.AutoSize = true;
            this.reportLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.reportLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.reportLink.Location = new System.Drawing.Point(5, 43);
            this.reportLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.reportLink.MaximumSize = new System.Drawing.Size(250, 0);
            this.reportLink.Name = "reportLink";
            this.reportLink.Size = new System.Drawing.Size(126, 20);
            this.reportLink.TabIndex = 0;
            this.reportLink.TabStop = true;
            this.reportLink.Text = "Report on GitHub";
            this.reportLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // releasesLink
            // 
            this.releasesLink.ActiveLinkColor = System.Drawing.Color.White;
            this.releasesLink.AutoSize = true;
            this.releasesLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.releasesLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.releasesLink.Location = new System.Drawing.Point(5, 179);
            this.releasesLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.releasesLink.MaximumSize = new System.Drawing.Size(250, 0);
            this.releasesLink.Name = "releasesLink";
            this.releasesLink.Size = new System.Drawing.Size(113, 20);
            this.releasesLink.TabIndex = 6;
            this.releasesLink.TabStop = true;
            this.releasesLink.Text = "GitHub releases";
            this.releasesLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // guideIndexLink
            // 
            this.guideIndexLink.ActiveLinkColor = System.Drawing.Color.White;
            this.guideIndexLink.AutoSize = true;
            this.guideIndexLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.guideIndexLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.guideIndexLink.Location = new System.Drawing.Point(5, 205);
            this.guideIndexLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.guideIndexLink.MaximumSize = new System.Drawing.Size(250, 0);
            this.guideIndexLink.Name = "guideIndexLink";
            this.guideIndexLink.Size = new System.Drawing.Size(158, 20);
            this.guideIndexLink.TabIndex = 7;
            this.guideIndexLink.TabStop = true;
            this.guideIndexLink.Text = "User guides on GitHub";
            this.guideIndexLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // openGuideButton
            // 
            this.openGuideButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.openGuideButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openGuideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openGuideButton.ForeColor = System.Drawing.Color.White;
            this.openGuideButton.Location = new System.Drawing.Point(3, 231);
            this.openGuideButton.Name = "openGuideButton";
            this.openGuideButton.Primary = false;
            this.openGuideButton.Size = new System.Drawing.Size(225, 30);
            this.openGuideButton.TabIndex = 8;
            this.openGuideButton.Text = "Open guide viewer";
            this.openGuideButton.UseVisualStyleBackColor = false;
            // 
            // iconColumn
            // 
            this.iconColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.NullValue = null;
            this.iconColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.iconColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.iconColumn.MinimumWidth = 6;
            this.iconColumn.Name = "iconColumn";
            this.iconColumn.ReadOnly = true;
            this.iconColumn.Width = 64;
            // 
            // positionColumn
            // 
            this.positionColumn.MinimumWidth = 6;
            this.positionColumn.Name = "positionColumn";
            this.positionColumn.ReadOnly = true;
            // 
            // recallColumn
            // 
            this.recallColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.recallColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recallColumn.MinimumWidth = 6;
            this.recallColumn.Name = "recallColumn";
            this.recallColumn.ReadOnly = true;
            this.recallColumn.Text = "▶";
            this.recallColumn.UseColumnTextForButtonValue = true;
            this.recallColumn.Width = 42;
            // 
            // root
            // 
            this.root.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
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
            this.root.Size = new System.Drawing.Size(1160, 510);
            this.root.TabIndex = 0;
            // 
            // body
            // 
            this.body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.body.ColumnCount = 2;
            this.body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.body.Controls.Add(this.mainSplit, 0, 0);
            this.body.Controls.Add(this.settingsTabs, 1, 0);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(18, 14);
            this.body.Margin = new System.Windows.Forms.Padding(0);
            this.body.Name = "body";
            this.body.RowCount = 1;
            this.body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.body.Size = new System.Drawing.Size(1124, 411);
            this.body.TabIndex = 1;
            // 
            // mainSplit
            // 
            this.mainSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
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
            this.mainSplit.SplitterDistance = 394;
            this.mainSplit.SplitterWidth = 15;
            this.mainSplit.TabIndex = 0;
            // 
            // motionPanel
            // 
            this.motionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.motionPanel.ColumnCount = 2;
            this.motionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.motionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.motionPanel.Controls.Add(this.title, 0, 0);
            this.motionPanel.Controls.Add(this.fovDisplay, 0, 1);
            this.motionPanel.Controls.Add(this.zoomPanel, 1, 1);
            this.motionPanel.Controls.Add(this.wheel, 0, 2);
            this.motionPanel.Controls.Add(this.motionPosition, 0, 3);
            this.motionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionPanel.Location = new System.Drawing.Point(0, 0);
            this.motionPanel.Margin = new System.Windows.Forms.Padding(0);
            this.motionPanel.Name = "motionPanel";
            this.motionPanel.Padding = new System.Windows.Forms.Padding(0, 0, 19, 12);
            this.motionPanel.RowCount = 4;
            this.motionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.motionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.motionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.motionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.motionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.motionPanel.Size = new System.Drawing.Size(394, 411);
            this.motionPanel.TabIndex = 0;
            // 
            // title
            // 
            this.title.AutoEllipsis = true;
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.motionPanel.SetColumnSpan(this.title, 2);
            this.title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.title.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.title.Location = new System.Drawing.Point(3, 0);
            this.title.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(372, 49);
            this.title.TabIndex = 0;
            this.title.Text = "Camera Control Studio";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.title.UseMnemonic = false;
            // 
            // fovDisplay
            // 
            this.fovDisplay.Controls.Add(this.fieldOfViewLabel);
            this.fovDisplay.Controls.Add(this.fieldOfViewPicker);
            this.fovDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fovDisplay.Location = new System.Drawing.Point(3, 52);
            this.fovDisplay.Name = "fovDisplay";
            this.fovDisplay.Size = new System.Drawing.Size(285, 42);
            this.fovDisplay.TabIndex = 2;
            // 
            // fieldOfViewLabel
            // 
            this.fieldOfViewLabel.AutoEllipsis = true;
            this.fieldOfViewLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldOfViewLabel.Location = new System.Drawing.Point(0, 0);
            this.fieldOfViewLabel.Name = "fieldOfViewLabel";
            this.fieldOfViewLabel.Size = new System.Drawing.Size(285, 42);
            this.fieldOfViewLabel.TabIndex = 0;
            this.fieldOfViewLabel.Text = "FOV unavailable";
            this.fieldOfViewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldOfViewPicker
            // 
            this.fieldOfViewPicker.AccessibleName = "Field of view";
            this.fieldOfViewPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldOfViewPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldOfViewPicker.Location = new System.Drawing.Point(0, 0);
            this.fieldOfViewPicker.Name = "fieldOfViewPicker";
            this.fieldOfViewPicker.Size = new System.Drawing.Size(285, 28);
            this.fieldOfViewPicker.TabIndex = 2;
            this.fieldOfViewPicker.Visible = false;
            // 
            // zoomPanel
            // 
            this.zoomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.zoomPanel.ColumnCount = 1;
            this.zoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.zoomPanel.Controls.Add(this.fullFrame, 0, 0);
            this.zoomPanel.Controls.Add(this.normalIn, 0, 1);
            this.zoomPanel.Controls.Add(this.zoomSlider, 0, 2);
            this.zoomPanel.Controls.Add(this.normalOut, 0, 3);
            this.zoomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomPanel.Location = new System.Drawing.Point(291, 49);
            this.zoomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.zoomPanel.Name = "zoomPanel";
            this.zoomPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zoomPanel.RowCount = 4;
            this.motionPanel.SetRowSpan(this.zoomPanel, 3);
            this.zoomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.zoomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.zoomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.zoomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.zoomPanel.Size = new System.Drawing.Size(84, 350);
            this.zoomPanel.TabIndex = 1;
            // 
            // normalIn
            // 
            this.normalIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.normalIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.normalIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.normalIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.normalIn.ForeColor = System.Drawing.Color.White;
            this.normalIn.Location = new System.Drawing.Point(0, 49);
            this.normalIn.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.normalIn.Name = "normalIn";
            this.normalIn.Primary = false;
            this.normalIn.Size = new System.Drawing.Size(84, 46);
            this.normalIn.TabIndex = 0;
            this.normalIn.UseVisualStyleBackColor = false;
            // 
            // zoomSlider
            // 
            this.zoomSlider.AccessibleRole = System.Windows.Forms.AccessibleRole.Slider;
            this.zoomSlider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomSlider.Location = new System.Drawing.Point(5, 96);
            this.zoomSlider.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.zoomSlider.Name = "zoomSlider";
            this.zoomSlider.Size = new System.Drawing.Size(74, 206);
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
            this.normalOut.Location = new System.Drawing.Point(0, 303);
            this.normalOut.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.normalOut.Name = "normalOut";
            this.normalOut.Primary = false;
            this.normalOut.Size = new System.Drawing.Size(84, 46);
            this.normalOut.TabIndex = 2;
            this.normalOut.UseVisualStyleBackColor = false;
            // 
            // motionPosition
            // 
            this.motionPosition.AutoEllipsis = true;
            this.motionPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionPosition.Location = new System.Drawing.Point(3, 351);
            this.motionPosition.Name = "motionPosition";
            this.motionPosition.Size = new System.Drawing.Size(285, 48);
            this.motionPosition.TabIndex = 2;
            this.motionPosition.Text = "Zoom 50, Pan 0, Tilt 0";
            this.motionPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // presetPanel
            // 
            this.presetPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
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
            this.presetPanel.Size = new System.Drawing.Size(375, 411);
            this.presetPanel.TabIndex = 0;
            // 
            // presetHeader
            // 
            this.presetHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.presetHeader.ColumnCount = 3;
            this.presetHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.presetHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
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
            this.presetHeader.Size = new System.Drawing.Size(351, 49);
            this.presetHeader.TabIndex = 0;
            // 
            // presetTitle
            // 
            this.presetTitle.AutoEllipsis = true;
            this.presetTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.presetTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.presetTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.presetTitle.Location = new System.Drawing.Point(0, 0);
            this.presetTitle.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.presetTitle.Name = "presetTitle";
            this.presetTitle.Size = new System.Drawing.Size(263, 49);
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
            this.deleteTemporaryButton.Location = new System.Drawing.Point(267, 4);
            this.deleteTemporaryButton.Margin = new System.Windows.Forms.Padding(0, 4, 4, 6);
            this.deleteTemporaryButton.Name = "deleteTemporaryButton";
            this.deleteTemporaryButton.Primary = false;
            this.deleteTemporaryButton.Size = new System.Drawing.Size(36, 39);
            this.deleteTemporaryButton.TabIndex = 1;
            this.deleteTemporaryButton.Text = "×";
            this.deleteTemporaryButton.UseVisualStyleBackColor = false;
            // 
            // temporaryButton
            // 
            this.temporaryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.temporaryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.temporaryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temporaryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.temporaryButton.ForeColor = System.Drawing.Color.White;
            this.temporaryButton.Location = new System.Drawing.Point(307, 4);
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
            this.presetGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iconColumn,
            this.positionColumn,
            this.recallColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.presetGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.presetGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.presetGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(144)))), ((int)(((byte)(214)))));
            this.presetGrid.Location = new System.Drawing.Point(24, 49);
            this.presetGrid.Margin = new System.Windows.Forms.Padding(0);
            this.presetGrid.MultiSelect = false;
            this.presetGrid.Name = "presetGrid";
            this.presetGrid.ReadOnly = true;
            this.presetGrid.RowHeadersVisible = false;
            this.presetGrid.RowHeadersWidth = 51;
            this.presetGrid.RowTemplate.Height = 44;
            this.presetGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.presetGrid.Size = new System.Drawing.Size(351, 345);
            this.presetGrid.TabIndex = 1;
            // 
            // settingsTabs
            // 
            this.settingsTabs.Controls.Add(this.presetsTab);
            this.settingsTabs.Controls.Add(this.generalTab);
            this.settingsTabs.Controls.Add(this.imageTab);
            this.settingsTabs.Controls.Add(this.profilesTab);
            this.settingsTabs.Controls.Add(this.issuesTab);
            this.settingsTabs.Controls.Add(this.aboutTab);
            this.settingsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTabs.Location = new System.Drawing.Point(794, 0);
            this.settingsTabs.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.settingsTabs.Multiline = true;
            this.settingsTabs.Name = "settingsTabs";
            this.settingsTabs.SelectedIndex = 0;
            this.settingsTabs.Size = new System.Drawing.Size(330, 411);
            this.settingsTabs.TabIndex = 1;
            // 
            // presetsTab
            // 
            this.presetsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.presetsTab.Controls.Add(this.permanentPanel);
            this.presetsTab.Location = new System.Drawing.Point(4, 54);
            this.presetsTab.Margin = new System.Windows.Forms.Padding(4);
            this.presetsTab.Name = "presetsTab";
            this.presetsTab.Padding = new System.Windows.Forms.Padding(10);
            this.presetsTab.Size = new System.Drawing.Size(322, 353);
            this.presetsTab.TabIndex = 2;
            this.presetsTab.Text = "Presets";
            // 
            // permanentPanel
            // 
            this.permanentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.permanentPanel.ColumnCount = 1;
            this.permanentPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.permanentPanel.Controls.Add(this.permanentTitle, 0, 0);
            this.permanentPanel.Controls.Add(this.permanentList, 0, 1);
            this.permanentPanel.Controls.Add(this.permanentActions, 0, 2);
            this.permanentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permanentPanel.Location = new System.Drawing.Point(10, 10);
            this.permanentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.permanentPanel.Name = "permanentPanel";
            this.permanentPanel.RowCount = 3;
            this.permanentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.permanentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.permanentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.permanentPanel.Size = new System.Drawing.Size(302, 333);
            this.permanentPanel.TabIndex = 0;
            // 
            // permanentTitle
            // 
            this.permanentTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.permanentTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permanentTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.permanentTitle.Location = new System.Drawing.Point(4, 0);
            this.permanentTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.permanentTitle.Name = "permanentTitle";
            this.permanentTitle.Size = new System.Drawing.Size(294, 25);
            this.permanentTitle.TabIndex = 0;
            this.permanentTitle.Text = "Permanent presets";
            this.permanentTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // permanentList
            // 
            this.permanentList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.permanentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permanentList.ForeColor = System.Drawing.Color.White;
            this.permanentList.IntegralHeight = false;
            this.permanentList.ItemHeight = 20;
            this.permanentList.Location = new System.Drawing.Point(4, 29);
            this.permanentList.Margin = new System.Windows.Forms.Padding(4);
            this.permanentList.Name = "permanentList";
            this.permanentList.Size = new System.Drawing.Size(294, 262);
            this.permanentList.TabIndex = 1;
            // 
            // permanentActions
            // 
            this.permanentActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.permanentActions.Controls.Add(this.addPermanent);
            this.permanentActions.Controls.Add(this.editPermanent);
            this.permanentActions.Controls.Add(this.updatePermanent);
            this.permanentActions.Controls.Add(this.deletePermanent);
            this.permanentActions.Controls.Add(this.movePermanentUp);
            this.permanentActions.Controls.Add(this.movePermanentDown);
            this.permanentActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permanentActions.Location = new System.Drawing.Point(4, 295);
            this.permanentActions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.permanentActions.Name = "permanentActions";
            this.permanentActions.Size = new System.Drawing.Size(294, 38);
            this.permanentActions.TabIndex = 2;
            this.permanentActions.WrapContents = false;
            // 
            // addPermanent
            // 
            this.addPermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.addPermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addPermanent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addPermanent.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.addPermanent.ForeColor = System.Drawing.Color.White;
            this.addPermanent.Location = new System.Drawing.Point(2, 3);
            this.addPermanent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.addPermanent.Name = "addPermanent";
            this.addPermanent.Primary = false;
            this.addPermanent.Size = new System.Drawing.Size(32, 30);
            this.addPermanent.TabIndex = 0;
            this.addPermanent.Text = "+";
            this.addPermanent.UseVisualStyleBackColor = false;
            // 
            // editPermanent
            // 
            this.editPermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.editPermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editPermanent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editPermanent.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.editPermanent.ForeColor = System.Drawing.Color.White;
            this.editPermanent.Location = new System.Drawing.Point(38, 3);
            this.editPermanent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.editPermanent.Name = "editPermanent";
            this.editPermanent.Primary = false;
            this.editPermanent.Size = new System.Drawing.Size(32, 30);
            this.editPermanent.TabIndex = 1;
            this.editPermanent.Text = "✎";
            this.editPermanent.UseVisualStyleBackColor = false;
            // 
            // updatePermanent
            // 
            this.updatePermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.updatePermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updatePermanent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updatePermanent.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.updatePermanent.ForeColor = System.Drawing.Color.White;
            this.updatePermanent.Location = new System.Drawing.Point(74, 3);
            this.updatePermanent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.updatePermanent.Name = "updatePermanent";
            this.updatePermanent.Primary = false;
            this.updatePermanent.Size = new System.Drawing.Size(32, 30);
            this.updatePermanent.TabIndex = 2;
            this.updatePermanent.Text = "↻";
            this.updatePermanent.UseVisualStyleBackColor = false;
            // 
            // deletePermanent
            // 
            this.deletePermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.deletePermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deletePermanent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deletePermanent.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.deletePermanent.ForeColor = System.Drawing.Color.White;
            this.deletePermanent.Location = new System.Drawing.Point(110, 3);
            this.deletePermanent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.deletePermanent.Name = "deletePermanent";
            this.deletePermanent.Primary = false;
            this.deletePermanent.Size = new System.Drawing.Size(32, 30);
            this.deletePermanent.TabIndex = 3;
            this.deletePermanent.Text = "−";
            this.deletePermanent.UseVisualStyleBackColor = false;
            // 
            // movePermanentUp
            // 
            this.movePermanentUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.movePermanentUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.movePermanentUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.movePermanentUp.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.movePermanentUp.ForeColor = System.Drawing.Color.White;
            this.movePermanentUp.Location = new System.Drawing.Point(146, 3);
            this.movePermanentUp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.movePermanentUp.Name = "movePermanentUp";
            this.movePermanentUp.Primary = false;
            this.movePermanentUp.Size = new System.Drawing.Size(32, 30);
            this.movePermanentUp.TabIndex = 4;
            this.movePermanentUp.Text = "↑";
            this.movePermanentUp.UseVisualStyleBackColor = false;
            // 
            // movePermanentDown
            // 
            this.movePermanentDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.movePermanentDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.movePermanentDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.movePermanentDown.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.movePermanentDown.ForeColor = System.Drawing.Color.White;
            this.movePermanentDown.Location = new System.Drawing.Point(182, 3);
            this.movePermanentDown.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.movePermanentDown.Name = "movePermanentDown";
            this.movePermanentDown.Primary = false;
            this.movePermanentDown.Size = new System.Drawing.Size(32, 30);
            this.movePermanentDown.TabIndex = 5;
            this.movePermanentDown.Text = "↓";
            this.movePermanentDown.UseVisualStyleBackColor = false;
            // 
            // generalTab
            // 
            this.generalTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.generalTab.Controls.Add(this.settingsCategories);
            this.generalTab.Location = new System.Drawing.Point(4, 54);
            this.generalTab.Margin = new System.Windows.Forms.Padding(4);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(10);
            this.generalTab.Size = new System.Drawing.Size(322, 353);
            this.generalTab.TabIndex = 0;
            this.generalTab.Text = "Settings";
            // 
            // imageTab
            // 
            this.imageTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.imageTab.Controls.Add(this.videoLayout);
            this.imageTab.Location = new System.Drawing.Point(4, 54);
            this.imageTab.Margin = new System.Windows.Forms.Padding(4);
            this.imageTab.Name = "imageTab";
            this.imageTab.Padding = new System.Windows.Forms.Padding(10);
            this.imageTab.Size = new System.Drawing.Size(322, 353);
            this.imageTab.TabIndex = 1;
            this.imageTab.Text = "Video";
            // 
            // profilesTab
            // 
            this.profilesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.profilesTab.Controls.Add(this.profilesPanel);
            this.profilesTab.Location = new System.Drawing.Point(4, 54);
            this.profilesTab.Margin = new System.Windows.Forms.Padding(4);
            this.profilesTab.Name = "profilesTab";
            this.profilesTab.Padding = new System.Windows.Forms.Padding(10);
            this.profilesTab.Size = new System.Drawing.Size(322, 353);
            this.profilesTab.TabIndex = 3;
            this.profilesTab.Text = "Profiles";
            // 
            // profilesPanel
            // 
            this.profilesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.profilesPanel.ColumnCount = 1;
            this.profilesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.profilesPanel.Controls.Add(this.profilesList, 0, 0);
            this.profilesPanel.Controls.Add(this.profileActions, 0, 1);
            this.profilesPanel.Controls.Add(this.profileNote, 0, 2);
            this.profilesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profilesPanel.Location = new System.Drawing.Point(10, 10);
            this.profilesPanel.Margin = new System.Windows.Forms.Padding(0);
            this.profilesPanel.Name = "profilesPanel";
            this.profilesPanel.RowCount = 3;
            this.profilesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.profilesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.profilesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.profilesPanel.Size = new System.Drawing.Size(302, 333);
            this.profilesPanel.TabIndex = 0;
            // 
            // profilesList
            // 
            this.profilesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.profilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profilesList.ForeColor = System.Drawing.Color.White;
            this.profilesList.IntegralHeight = false;
            this.profilesList.ItemHeight = 20;
            this.profilesList.Location = new System.Drawing.Point(4, 4);
            this.profilesList.Margin = new System.Windows.Forms.Padding(4);
            this.profilesList.Name = "profilesList";
            this.profilesList.Size = new System.Drawing.Size(294, 237);
            this.profilesList.TabIndex = 0;
            // 
            // profileActions
            // 
            this.profileActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.profileActions.Controls.Add(this.newProfile);
            this.profileActions.Controls.Add(this.copyProfile);
            this.profileActions.Controls.Add(this.renameProfile);
            this.profileActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileActions.Location = new System.Drawing.Point(4, 245);
            this.profileActions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.profileActions.Name = "profileActions";
            this.profileActions.Size = new System.Drawing.Size(294, 38);
            this.profileActions.TabIndex = 1;
            this.profileActions.WrapContents = false;
            // 
            // newProfile
            // 
            this.newProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.newProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.newProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newProfile.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.newProfile.ForeColor = System.Drawing.Color.White;
            this.newProfile.Location = new System.Drawing.Point(2, 3);
            this.newProfile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.newProfile.Name = "newProfile";
            this.newProfile.Primary = false;
            this.newProfile.Size = new System.Drawing.Size(32, 30);
            this.newProfile.TabIndex = 0;
            this.newProfile.Text = "+";
            this.newProfile.UseVisualStyleBackColor = false;
            // 
            // copyProfile
            // 
            this.copyProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.copyProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.copyProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyProfile.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.copyProfile.ForeColor = System.Drawing.Color.White;
            this.copyProfile.Location = new System.Drawing.Point(38, 3);
            this.copyProfile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.copyProfile.Name = "copyProfile";
            this.copyProfile.Primary = false;
            this.copyProfile.Size = new System.Drawing.Size(32, 30);
            this.copyProfile.TabIndex = 1;
            this.copyProfile.Text = "⧉";
            this.copyProfile.UseVisualStyleBackColor = false;
            // 
            // renameProfile
            // 
            this.renameProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.renameProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.renameProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renameProfile.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.renameProfile.ForeColor = System.Drawing.Color.White;
            this.renameProfile.Location = new System.Drawing.Point(74, 3);
            this.renameProfile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.renameProfile.Name = "renameProfile";
            this.renameProfile.Primary = false;
            this.renameProfile.Size = new System.Drawing.Size(32, 30);
            this.renameProfile.TabIndex = 2;
            this.renameProfile.Text = "✎";
            this.renameProfile.UseVisualStyleBackColor = false;
            // 
            // profileNote
            // 
            this.profileNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.profileNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileNote.Location = new System.Drawing.Point(4, 283);
            this.profileNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.profileNote.Name = "profileNote";
            this.profileNote.Size = new System.Drawing.Size(294, 50);
            this.profileNote.TabIndex = 2;
            this.profileNote.Text = "Edit the active profile in Settings.";
            // 
            // aboutTab
            // 
            this.aboutTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.aboutTab.Controls.Add(this.aboutPanel);
            this.aboutTab.Location = new System.Drawing.Point(4, 54);
            this.aboutTab.Margin = new System.Windows.Forms.Padding(4);
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Padding = new System.Windows.Forms.Padding(10);
            this.aboutTab.Size = new System.Drawing.Size(322, 353);
            this.aboutTab.TabIndex = 4;
            this.aboutTab.Text = "About";
            // 
            // aboutPanel
            // 
            this.aboutPanel.AutoScroll = true;
            this.aboutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.aboutPanel.Controls.Add(this.credit);
            this.aboutPanel.Controls.Add(this.repoLink);
            this.aboutPanel.Controls.Add(this.issuesLink);
            this.aboutPanel.Controls.Add(this.discussionLink);
            this.aboutPanel.Controls.Add(this.coffeeLink);
            this.aboutPanel.Controls.Add(this.versionLabel);
            this.aboutPanel.Controls.Add(this.releasesLink);
            this.aboutPanel.Controls.Add(this.guideIndexLink);
            this.aboutPanel.Controls.Add(this.openGuideButton);
            this.aboutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.aboutPanel.Location = new System.Drawing.Point(10, 10);
            this.aboutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.aboutPanel.Name = "aboutPanel";
            this.aboutPanel.Size = new System.Drawing.Size(302, 333);
            this.aboutPanel.TabIndex = 0;
            this.aboutPanel.WrapContents = false;
            // 
            // credit
            // 
            this.credit.AutoSize = true;
            this.credit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.credit.Location = new System.Drawing.Point(5, 3);
            this.credit.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.credit.MaximumSize = new System.Drawing.Size(350, 0);
            this.credit.Name = "credit";
            this.credit.Size = new System.Drawing.Size(247, 40);
            this.credit.TabIndex = 0;
            this.credit.Text = "Developed as open source by Brian Kristensen, Vejle Uhrhøj, DK.";
            // 
            // repoLink
            // 
            this.repoLink.ActiveLinkColor = System.Drawing.Color.White;
            this.repoLink.AutoSize = true;
            this.repoLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.repoLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.repoLink.Location = new System.Drawing.Point(5, 49);
            this.repoLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.repoLink.Name = "repoLink";
            this.repoLink.Size = new System.Drawing.Size(56, 20);
            this.repoLink.TabIndex = 1;
            this.repoLink.TabStop = true;
            this.repoLink.Text = "GitHub";
            this.repoLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // issuesLink
            // 
            this.issuesLink.ActiveLinkColor = System.Drawing.Color.White;
            this.issuesLink.AutoSize = true;
            this.issuesLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.issuesLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.issuesLink.Location = new System.Drawing.Point(5, 75);
            this.issuesLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.issuesLink.Name = "issuesLink";
            this.issuesLink.Size = new System.Drawing.Size(219, 20);
            this.issuesLink.TabIndex = 2;
            this.issuesLink.TabStop = true;
            this.issuesLink.Text = "Report bugs or missing features";
            this.issuesLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // discussionLink
            // 
            this.discussionLink.ActiveLinkColor = System.Drawing.Color.White;
            this.discussionLink.AutoSize = true;
            this.discussionLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.discussionLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.discussionLink.Location = new System.Drawing.Point(5, 101);
            this.discussionLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.discussionLink.Name = "discussionLink";
            this.discussionLink.Size = new System.Drawing.Size(182, 20);
            this.discussionLink.TabIndex = 3;
            this.discussionLink.TabStop = true;
            this.discussionLink.Text = "Join the project discussion";
            this.discussionLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // coffeeLink
            // 
            this.coffeeLink.ActiveLinkColor = System.Drawing.Color.White;
            this.coffeeLink.AutoSize = true;
            this.coffeeLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.coffeeLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.coffeeLink.Location = new System.Drawing.Point(5, 127);
            this.coffeeLink.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.coffeeLink.Name = "coffeeLink";
            this.coffeeLink.Size = new System.Drawing.Size(116, 20);
            this.coffeeLink.TabIndex = 4;
            this.coffeeLink.TabStop = true;
            this.coffeeLink.Text = "Buy me a coffee";
            this.coffeeLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.versionLabel.Location = new System.Drawing.Point(5, 153);
            this.versionLabel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
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
            this.footerSeparator.Size = new System.Drawing.Size(1124, 13);
            this.footerSeparator.TabIndex = 2;
            this.footerSeparator.TabStop = false;
            // 
            // footer
            // 
            this.footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
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
            this.footer.Size = new System.Drawing.Size(1124, 62);
            this.footer.TabIndex = 1;
            // 
            // footerLabels
            // 
            this.footerLabels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.footerLabels.ColumnCount = 2;
            this.footerLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.footerLabels.Controls.Add(this.cameraStatus, 0, 0);
            this.footerLabels.Controls.Add(this.releaseNotice, 1, 0);
            this.footerLabels.Controls.Add(this.status, 0, 1);
            this.footerLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerLabels.Location = new System.Drawing.Point(0, 3);
            this.footerLabels.Margin = new System.Windows.Forms.Padding(0, 3, 7, 0);
            this.footerLabels.Name = "footerLabels";
            this.footerLabels.RowCount = 2;
            this.footerLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.footerLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerLabels.Size = new System.Drawing.Size(860, 59);
            this.footerLabels.TabIndex = 0;
            // 
            // cameraStatus
            // 
            this.cameraStatus.AutoEllipsis = true;
            this.cameraStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.cameraStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraStatus.Location = new System.Drawing.Point(4, 0);
            this.cameraStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cameraStatus.Name = "cameraStatus";
            this.cameraStatus.Size = new System.Drawing.Size(803, 25);
            this.cameraStatus.TabIndex = 0;
            this.cameraStatus.Text = "Camera: not connected";
            this.cameraStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // releaseNotice
            // 
            this.releaseNotice.ActiveLinkColor = System.Drawing.Color.White;
            this.releaseNotice.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.releaseNotice.AutoSize = true;
            this.releaseNotice.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.releaseNotice.Location = new System.Drawing.Point(817, 2);
            this.releaseNotice.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            this.releaseNotice.Name = "releaseNotice";
            this.releaseNotice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.releaseNotice.Size = new System.Drawing.Size(39, 20);
            this.releaseNotice.TabIndex = 2;
            this.releaseNotice.TabStop = true;
            this.releaseNotice.Text = "0.0.0";
            this.releaseNotice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.releaseNotice.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // status
            // 
            this.status.AutoEllipsis = true;
            this.status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.footerLabels.SetColumnSpan(this.status, 2);
            this.status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.status.Location = new System.Drawing.Point(4, 25);
            this.status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(852, 34);
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
            this.profileButton.Location = new System.Drawing.Point(870, 9);
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
            this.helpButton.Location = new System.Drawing.Point(1023, 9);
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
            this.settingsButton.Location = new System.Drawing.Point(1071, 9);
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
            this.ClientSize = new System.Drawing.Size(1160, 510);
            this.Controls.Add(this.root);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(670, 450);
            this.Name = "MainForm";
            this.Text = "Camera Control Studio";
            this.settingsCategories.ResumeLayout(false);
            this.motionTab.ResumeLayout(false);
            this.motionGroup.ResumeLayout(false);
            this.movementScroll.ResumeLayout(false);
            this.movementScroll.PerformLayout();
            this.generalFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.finePanPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPanSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transitionSpeedPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).EndInit();
            this.applicationTab.ResumeLayout(false);
            this.applicationScroll.ResumeLayout(false);
            this.applicationScroll.PerformLayout();
            this.applicationFields.ResumeLayout(false);
            this.windowGroup.ResumeLayout(false);
            this.windowFields.ResumeLayout(false);
            this.languageGroup.ResumeLayout(false);
            this.videoLayout.ResumeLayout(false);
            this.videoLayout.PerformLayout();
            this.videoStatus.ResumeLayout(false);
            this.videoStatus.PerformLayout();
            this.imagePages.ResumeLayout(false);
            this.imageCategories.ResumeLayout(false);
            this.focusTab.ResumeLayout(false);
            this.focusPanel.ResumeLayout(false);
            this.focusPanel.PerformLayout();
            this.gainControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gainSlider)).EndInit();
            this.exposureControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exposureSlider)).EndInit();
            this.focusControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.focusSlider)).EndInit();
            this.colourTab.ResumeLayout(false);
            this.colourPanel.ResumeLayout(false);
            this.colourPanel.PerformLayout();
            this.whiteBalanceControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.whiteBalanceSlider)).EndInit();
            this.saturationControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.saturationSlider)).EndInit();
            this.gammaControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gammaSlider)).EndInit();
            this.hueControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hueSlider)).EndInit();
            this.lightTab.ResumeLayout(false);
            this.lightPanel.ResumeLayout(false);
            this.lightPanel.PerformLayout();
            this.contrastControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contrastSlider)).EndInit();
            this.brightnessControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.brightnessSlider)).EndInit();
            this.backlightControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.backlightSlider)).EndInit();
            this.sharpnessControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sharpnessSlider)).EndInit();
            this.issuesTab.ResumeLayout(false);
            this.issueGroups.ResumeLayout(false);
            this.logGroup.ResumeLayout(false);
            this.logActions.ResumeLayout(false);
            this.logActions.PerformLayout();
            this.reportGroup.ResumeLayout(false);
            this.reportActions.ResumeLayout(false);
            this.reportActions.PerformLayout();
            this.root.ResumeLayout(false);
            this.body.ResumeLayout(false);
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            this.motionPanel.ResumeLayout(false);
            this.fovDisplay.ResumeLayout(false);
            this.zoomPanel.ResumeLayout(false);
            this.presetPanel.ResumeLayout(false);
            this.presetHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.presetGrid)).EndInit();
            this.settingsTabs.ResumeLayout(false);
            this.presetsTab.ResumeLayout(false);
            this.permanentPanel.ResumeLayout(false);
            this.permanentActions.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.imageTab.ResumeLayout(false);
            this.profilesTab.ResumeLayout(false);
            this.profilesPanel.ResumeLayout(false);
            this.profileActions.ResumeLayout(false);
            this.aboutTab.ResumeLayout(false);
            this.aboutPanel.ResumeLayout(false);
            this.aboutPanel.PerformLayout();
            this.footer.ResumeLayout(false);
            this.footerLabels.ResumeLayout(false);
            this.footerLabels.PerformLayout();
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
        private Panel movementScroll;
        private TableLayoutPanel generalFields;
        private Label startupFovLabel;
        private ListBox startupFov;
        private CheckBox allowFov;
        private CheckBox adjustFovAtLimits;
        private Panel fovDisplay;
        private Label fieldOfViewLabel;
        private ComboBox fieldOfViewPicker;
        private BlueButton fullFrame;
        private Label motionPosition;
        private Label panStepsLabel;
        private NumericUpDown panSteps;
        private Label fastPanStepsLabel;
        private NumericUpDown fastPanSteps;
        private Label zoomStepsLabel;
        private NumericUpDown zoomSteps;
        private Label secondsLabel;
        private NumericUpDown seconds;
        private Label finePanLabel;
        private NumericUpDown finePanPercent;
        private CheckBox useTransitionSpeed;
        private Label transitionSpeedLabel;
        private NumericUpDown transitionSpeedPercent;
        private CheckBox reverseX;
        private CheckBox reverseY;
        private CheckBox streamingCheck;
        private CheckBox alwaysTop;
        private ComboBox language;
        private TableLayoutPanel permanentPanel;
        private Label permanentTitle;
        private ListBox permanentList;
        private FlowLayoutPanel permanentActions;
        private BlueButton addPermanent;
        private BlueButton editPermanent;
        private BlueButton updatePermanent;
        private BlueButton deletePermanent;
        private BlueButton movePermanentUp;
        private BlueButton movePermanentDown;
        private TableLayoutPanel profilesPanel;
        private ListBox profilesList;
        private FlowLayoutPanel profileActions;
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
        private Timer movement;
        private Timer held;
        private Timer saveTimer;
        private Timer motionLimits;
        private Timer documentationTimer;
        private TabControl settingsCategories;
        private TabPage motionTab;
        private TabPage applicationTab;
        private GroupBox motionGroup;
        private Panel applicationScroll;
        private TableLayoutPanel applicationFields;
        private GroupBox windowGroup;
        private GroupBox languageGroup;
        private TableLayoutPanel windowFields;
        private TableLayoutPanel videoLayout;
        private FlowLayoutPanel videoStatus;
        private BlueButton loadVideoControls;
        private Label noVideoControls;
        private TableLayoutPanel imagePages;
        private TabControl imageCategories;
        private BlueButton resetVideoControls;
        private TabPage focusTab;
        private Panel focusPanel;
        private GroupBox focusControl;
        private TrackBar focusSlider;
        private CheckBox focusAutomatic;
        private GroupBox exposureControl;
        private TrackBar exposureSlider;
        private CheckBox exposureAutomatic;
        private GroupBox gainControl;
        private TrackBar gainSlider;
        private CheckBox gainAutomatic;
        private TabPage colourTab;
        private Panel colourPanel;
        private GroupBox hueControl;
        private TrackBar hueSlider;
        private CheckBox hueAutomatic;
        private GroupBox gammaControl;
        private TrackBar gammaSlider;
        private CheckBox gammaAutomatic;
        private GroupBox saturationControl;
        private TrackBar saturationSlider;
        private CheckBox saturationAutomatic;
        private GroupBox whiteBalanceControl;
        private TrackBar whiteBalanceSlider;
        private CheckBox whiteBalanceAutomatic;
        private TabPage lightTab;
        private Panel lightPanel;
        private GroupBox sharpnessControl;
        private TrackBar sharpnessSlider;
        private CheckBox sharpnessAutomatic;
        private GroupBox backlightControl;
        private TrackBar backlightSlider;
        private CheckBox backlightAutomatic;
        private GroupBox brightnessControl;
        private TrackBar brightnessSlider;
        private CheckBox brightnessAutomatic;
        private GroupBox contrastControl;
        private TrackBar contrastSlider;
        private CheckBox contrastAutomatic;
        private TabPage issuesTab;
        private FlowLayoutPanel issueGroups;
        private GroupBox logGroup;
        private GroupBox reportGroup;
        private FlowLayoutPanel logActions;
        private FlowLayoutPanel reportActions;
        private LinkLabel logSettingsLink;
        private LinkLabel showLogLink;
        private Label reportNote;
        private LinkLabel reportLink;
        private LinkLabel releasesLink;
        private LinkLabel guideIndexLink;
        private BlueButton openGuideButton;
        private DataGridViewImageColumn iconColumn;
        private DataGridViewTextBoxColumn positionColumn;
        private DataGridViewButtonColumn recallColumn;
        private Label cameraStatus;
        private LinkLabel releaseNotice;
        private Label status;
        private BlueButton settingsButton;
        private BlueButton helpButton;
        private DataGridView presetGrid;
    }
}
