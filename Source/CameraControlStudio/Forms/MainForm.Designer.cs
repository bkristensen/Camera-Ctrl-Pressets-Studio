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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.finePanPercent = new CameraControlStudio.BlueNumericUpDown();
            this.panStepsLabel = new System.Windows.Forms.Label();
            this.panSteps = new CameraControlStudio.BlueNumericUpDown();
            this.fastPanStepsLabel = new System.Windows.Forms.Label();
            this.fastPanSteps = new CameraControlStudio.BlueNumericUpDown();
            this.zoomStepsLabel = new System.Windows.Forms.Label();
            this.zoomSteps = new CameraControlStudio.BlueNumericUpDown();
            this.useTransitionSpeed = new CameraControlStudio.BlueCheckBox();
            this.transitionSpeedLabel = new System.Windows.Forms.Label();
            this.transitionSpeedPercent = new CameraControlStudio.BlueNumericUpDown();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.seconds = new CameraControlStudio.BlueNumericUpDown();
            this.reverseX = new CameraControlStudio.BlueCheckBox();
            this.reverseY = new CameraControlStudio.BlueCheckBox();
            this.startupFovLabel = new System.Windows.Forms.Label();
            this.startupFov = new System.Windows.Forms.ListBox();
            this.allowFov = new CameraControlStudio.BlueCheckBox();
            this.adjustFovAtLimits = new CameraControlStudio.BlueCheckBox();
            this.panWithCameraButton = new CameraControlStudio.BlueCheckBox();
            this.doubleClickForCameraSource = new CameraControlStudio.BlueCheckBox();
            this.applicationTab = new System.Windows.Forms.TabPage();
            this.applicationScroll = new System.Windows.Forms.Panel();
            this.applicationFields = new System.Windows.Forms.TableLayoutPanel();
            this.windowGroup = new System.Windows.Forms.GroupBox();
            this.windowFields = new System.Windows.Forms.TableLayoutPanel();
            this.streamingCheck = new CameraControlStudio.BlueCheckBox();
            this.alwaysTop = new CameraControlStudio.BlueCheckBox();
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
            this.gainAutomatic = new CameraControlStudio.BlueCheckBox();
            this.gainSlider = new System.Windows.Forms.TrackBar();
            this.exposureControl = new System.Windows.Forms.GroupBox();
            this.exposureAutomatic = new CameraControlStudio.BlueCheckBox();
            this.exposureSlider = new System.Windows.Forms.TrackBar();
            this.focusControl = new System.Windows.Forms.GroupBox();
            this.focusAutomatic = new CameraControlStudio.BlueCheckBox();
            this.focusSlider = new System.Windows.Forms.TrackBar();
            this.colourTab = new System.Windows.Forms.TabPage();
            this.colourPanel = new System.Windows.Forms.Panel();
            this.whiteBalanceControl = new System.Windows.Forms.GroupBox();
            this.whiteBalanceAutomatic = new CameraControlStudio.BlueCheckBox();
            this.whiteBalanceSlider = new System.Windows.Forms.TrackBar();
            this.saturationControl = new System.Windows.Forms.GroupBox();
            this.saturationAutomatic = new CameraControlStudio.BlueCheckBox();
            this.saturationSlider = new System.Windows.Forms.TrackBar();
            this.gammaControl = new System.Windows.Forms.GroupBox();
            this.gammaAutomatic = new CameraControlStudio.BlueCheckBox();
            this.gammaSlider = new System.Windows.Forms.TrackBar();
            this.hueControl = new System.Windows.Forms.GroupBox();
            this.hueAutomatic = new CameraControlStudio.BlueCheckBox();
            this.hueSlider = new System.Windows.Forms.TrackBar();
            this.lightTab = new System.Windows.Forms.TabPage();
            this.lightPanel = new System.Windows.Forms.Panel();
            this.contrastControl = new System.Windows.Forms.GroupBox();
            this.contrastAutomatic = new CameraControlStudio.BlueCheckBox();
            this.contrastSlider = new System.Windows.Forms.TrackBar();
            this.brightnessControl = new System.Windows.Forms.GroupBox();
            this.brightnessAutomatic = new CameraControlStudio.BlueCheckBox();
            this.brightnessSlider = new System.Windows.Forms.TrackBar();
            this.backlightControl = new System.Windows.Forms.GroupBox();
            this.backlightAutomatic = new CameraControlStudio.BlueCheckBox();
            this.backlightSlider = new System.Windows.Forms.TrackBar();
            this.sharpnessControl = new System.Windows.Forms.GroupBox();
            this.sharpnessAutomatic = new CameraControlStudio.BlueCheckBox();
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
            resources.ApplyResources(this.fullFrame, "fullFrame");
            this.fullFrame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.fullFrame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fullFrame.ForeColor = System.Drawing.Color.White;
            this.fullFrame.Name = "fullFrame";
            this.fullFrame.Primary = false;
            this.mainToolTips.SetToolTip(this.fullFrame, resources.GetString("fullFrame.ToolTip"));
            this.fullFrame.UseVisualStyleBackColor = false;
            // 
            // wheel
            // 
            resources.ApplyResources(this.wheel, "wheel");
            this.wheel.Name = "wheel";
            this.mainToolTips.SetToolTip(this.wheel, resources.GetString("wheel.ToolTip"));
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
            resources.ApplyResources(this.settingsCategories, "settingsCategories");
            this.settingsCategories.Name = "settingsCategories";
            this.settingsCategories.SelectedIndex = 0;
            // 
            // motionTab
            // 
            this.motionTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.motionTab.Controls.Add(this.motionGroup);
            resources.ApplyResources(this.motionTab, "motionTab");
            this.motionTab.Name = "motionTab";
            // 
            // motionGroup
            // 
            this.motionGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.motionGroup.Controls.Add(this.movementScroll);
            resources.ApplyResources(this.motionGroup, "motionGroup");
            this.motionGroup.ForeColor = System.Drawing.Color.White;
            this.motionGroup.Name = "motionGroup";
            this.motionGroup.TabStop = false;
            // 
            // movementScroll
            // 
            resources.ApplyResources(this.movementScroll, "movementScroll");
            this.movementScroll.Controls.Add(this.generalFields);
            this.movementScroll.Name = "movementScroll";
            // 
            // generalFields
            // 
            resources.ApplyResources(this.generalFields, "generalFields");
            this.generalFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
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
            this.generalFields.Controls.Add(this.panWithCameraButton, 0, 12);
            this.generalFields.Controls.Add(this.doubleClickForCameraSource, 0, 13);
            this.generalFields.Name = "generalFields";
            // 
            // finePanLabel
            // 
            this.finePanLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.finePanLabel, "finePanLabel");
            this.finePanLabel.Name = "finePanLabel";
            // 
            // finePanPercent
            // 
            this.finePanPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            resources.ApplyResources(this.finePanPercent, "finePanPercent");
            this.finePanPercent.ForeColor = System.Drawing.Color.White;
            this.finePanPercent.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.finePanPercent.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.finePanPercent.Name = "finePanPercent";
            this.finePanPercent.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // panStepsLabel
            // 
            this.panStepsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.panStepsLabel, "panStepsLabel");
            this.panStepsLabel.Name = "panStepsLabel";
            // 
            // panSteps
            // 
            this.panSteps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            resources.ApplyResources(this.panSteps, "panSteps");
            this.panSteps.ForeColor = System.Drawing.Color.White;
            this.panSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.panSteps.Name = "panSteps";
            this.panSteps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fastPanStepsLabel
            // 
            this.fastPanStepsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.fastPanStepsLabel, "fastPanStepsLabel");
            this.fastPanStepsLabel.Name = "fastPanStepsLabel";
            // 
            // fastPanSteps
            // 
            this.fastPanSteps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            resources.ApplyResources(this.fastPanSteps, "fastPanSteps");
            this.fastPanSteps.ForeColor = System.Drawing.Color.White;
            this.fastPanSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fastPanSteps.Name = "fastPanSteps";
            this.fastPanSteps.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // zoomStepsLabel
            // 
            this.zoomStepsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.zoomStepsLabel, "zoomStepsLabel");
            this.zoomStepsLabel.Name = "zoomStepsLabel";
            // 
            // zoomSteps
            // 
            this.zoomSteps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            resources.ApplyResources(this.zoomSteps, "zoomSteps");
            this.zoomSteps.ForeColor = System.Drawing.Color.White;
            this.zoomSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zoomSteps.Name = "zoomSteps";
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
            resources.ApplyResources(this.useTransitionSpeed, "useTransitionSpeed");
            this.useTransitionSpeed.Name = "useTransitionSpeed";
            this.useTransitionSpeed.UseVisualStyleBackColor = false;
            // 
            // transitionSpeedLabel
            // 
            this.transitionSpeedLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.transitionSpeedLabel, "transitionSpeedLabel");
            this.transitionSpeedLabel.Name = "transitionSpeedLabel";
            // 
            // transitionSpeedPercent
            // 
            this.transitionSpeedPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            resources.ApplyResources(this.transitionSpeedPercent, "transitionSpeedPercent");
            this.transitionSpeedPercent.ForeColor = System.Drawing.Color.White;
            this.transitionSpeedPercent.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.transitionSpeedPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.transitionSpeedPercent.Name = "transitionSpeedPercent";
            this.transitionSpeedPercent.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // secondsLabel
            // 
            this.secondsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.secondsLabel, "secondsLabel");
            this.secondsLabel.Name = "secondsLabel";
            // 
            // seconds
            // 
            this.seconds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            this.seconds.DecimalPlaces = 1;
            resources.ApplyResources(this.seconds, "seconds");
            this.seconds.ForeColor = System.Drawing.Color.White;
            this.seconds.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.seconds.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.seconds.Name = "seconds";
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
            resources.ApplyResources(this.reverseX, "reverseX");
            this.reverseX.Name = "reverseX";
            this.reverseX.UseVisualStyleBackColor = false;
            // 
            // reverseY
            // 
            this.reverseY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.generalFields.SetColumnSpan(this.reverseY, 2);
            resources.ApplyResources(this.reverseY, "reverseY");
            this.reverseY.Name = "reverseY";
            this.reverseY.UseVisualStyleBackColor = false;
            // 
            // startupFovLabel
            // 
            resources.ApplyResources(this.startupFovLabel, "startupFovLabel");
            this.startupFovLabel.Name = "startupFovLabel";
            // 
            // startupFov
            // 
            resources.ApplyResources(this.startupFov, "startupFov");
            this.startupFov.Name = "startupFov";
            // 
            // allowFov
            // 
            this.generalFields.SetColumnSpan(this.allowFov, 2);
            resources.ApplyResources(this.allowFov, "allowFov");
            this.allowFov.Name = "allowFov";
            // 
            // adjustFovAtLimits
            // 
            this.adjustFovAtLimits.Checked = true;
            this.adjustFovAtLimits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generalFields.SetColumnSpan(this.adjustFovAtLimits, 2);
            resources.ApplyResources(this.adjustFovAtLimits, "adjustFovAtLimits");
            this.adjustFovAtLimits.Name = "adjustFovAtLimits";
            // 
            // panWithCameraButton
            // 
            this.panWithCameraButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.panWithCameraButton.Checked = true;
            this.panWithCameraButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generalFields.SetColumnSpan(this.panWithCameraButton, 2);
            resources.ApplyResources(this.panWithCameraButton, "panWithCameraButton");
            this.panWithCameraButton.ForeColor = System.Drawing.Color.White;
            this.panWithCameraButton.Name = "panWithCameraButton";
            this.panWithCameraButton.UseVisualStyleBackColor = false;
            // 
            // doubleClickForCameraSource
            // 
            this.doubleClickForCameraSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.doubleClickForCameraSource.Checked = true;
            this.doubleClickForCameraSource.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generalFields.SetColumnSpan(this.doubleClickForCameraSource, 2);
            resources.ApplyResources(this.doubleClickForCameraSource, "doubleClickForCameraSource");
            this.doubleClickForCameraSource.ForeColor = System.Drawing.Color.White;
            this.doubleClickForCameraSource.Name = "doubleClickForCameraSource";
            this.doubleClickForCameraSource.UseVisualStyleBackColor = false;
            // 
            // applicationTab
            // 
            this.applicationTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.applicationTab.Controls.Add(this.applicationScroll);
            resources.ApplyResources(this.applicationTab, "applicationTab");
            this.applicationTab.Name = "applicationTab";
            // 
            // applicationScroll
            // 
            resources.ApplyResources(this.applicationScroll, "applicationScroll");
            this.applicationScroll.Controls.Add(this.applicationFields);
            this.applicationScroll.Name = "applicationScroll";
            // 
            // applicationFields
            // 
            resources.ApplyResources(this.applicationFields, "applicationFields");
            this.applicationFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.applicationFields.Controls.Add(this.windowGroup, 0, 0);
            this.applicationFields.Controls.Add(this.languageGroup, 0, 1);
            this.applicationFields.Name = "applicationFields";
            // 
            // windowGroup
            // 
            this.windowGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.windowGroup.Controls.Add(this.windowFields);
            resources.ApplyResources(this.windowGroup, "windowGroup");
            this.windowGroup.ForeColor = System.Drawing.Color.White;
            this.windowGroup.Name = "windowGroup";
            this.windowGroup.TabStop = false;
            // 
            // windowFields
            // 
            this.windowFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.windowFields, "windowFields");
            this.windowFields.Controls.Add(this.streamingCheck, 0, 0);
            this.windowFields.Controls.Add(this.alwaysTop, 0, 1);
            this.windowFields.Name = "windowFields";
            // 
            // streamingCheck
            // 
            this.streamingCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.streamingCheck, "streamingCheck");
            this.streamingCheck.Name = "streamingCheck";
            this.streamingCheck.UseVisualStyleBackColor = false;
            // 
            // alwaysTop
            // 
            this.alwaysTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.alwaysTop, "alwaysTop");
            this.alwaysTop.Name = "alwaysTop";
            this.alwaysTop.UseVisualStyleBackColor = false;
            // 
            // languageGroup
            // 
            this.languageGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.languageGroup.Controls.Add(this.language);
            resources.ApplyResources(this.languageGroup, "languageGroup");
            this.languageGroup.ForeColor = System.Drawing.Color.White;
            this.languageGroup.Name = "languageGroup";
            this.languageGroup.TabStop = false;
            // 
            // language
            // 
            this.language.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            resources.ApplyResources(this.language, "language");
            this.language.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.language.ForeColor = System.Drawing.Color.White;
            this.language.Name = "language";
            // 
            // videoLayout
            // 
            this.videoLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.videoLayout, "videoLayout");
            this.videoLayout.Controls.Add(this.videoStatus, 0, 0);
            this.videoLayout.Controls.Add(this.imagePages, 0, 1);
            this.videoLayout.Name = "videoLayout";
            // 
            // videoStatus
            // 
            resources.ApplyResources(this.videoStatus, "videoStatus");
            this.videoStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.videoStatus.Controls.Add(this.loadVideoControls);
            this.videoStatus.Controls.Add(this.noVideoControls);
            this.videoStatus.Name = "videoStatus";
            // 
            // loadVideoControls
            // 
            resources.ApplyResources(this.loadVideoControls, "loadVideoControls");
            this.loadVideoControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.loadVideoControls.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadVideoControls.ForeColor = System.Drawing.Color.White;
            this.loadVideoControls.Name = "loadVideoControls";
            this.loadVideoControls.Primary = false;
            this.loadVideoControls.UseVisualStyleBackColor = false;
            // 
            // noVideoControls
            // 
            resources.ApplyResources(this.noVideoControls, "noVideoControls");
            this.noVideoControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.noVideoControls.Name = "noVideoControls";
            // 
            // imagePages
            // 
            this.imagePages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.imagePages, "imagePages");
            this.imagePages.Controls.Add(this.imageCategories, 0, 0);
            this.imagePages.Controls.Add(this.resetVideoControls, 0, 1);
            this.imagePages.Name = "imagePages";
            // 
            // imageCategories
            // 
            this.imageCategories.Controls.Add(this.focusTab);
            this.imageCategories.Controls.Add(this.colourTab);
            this.imageCategories.Controls.Add(this.lightTab);
            resources.ApplyResources(this.imageCategories, "imageCategories");
            this.imageCategories.Name = "imageCategories";
            this.imageCategories.SelectedIndex = 0;
            // 
            // focusTab
            // 
            this.focusTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.focusTab.Controls.Add(this.focusPanel);
            resources.ApplyResources(this.focusTab, "focusTab");
            this.focusTab.Name = "focusTab";
            // 
            // focusPanel
            // 
            resources.ApplyResources(this.focusPanel, "focusPanel");
            this.focusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.focusPanel.Controls.Add(this.gainControl);
            this.focusPanel.Controls.Add(this.exposureControl);
            this.focusPanel.Controls.Add(this.focusControl);
            this.focusPanel.Name = "focusPanel";
            // 
            // gainControl
            // 
            resources.ApplyResources(this.gainControl, "gainControl");
            this.gainControl.Controls.Add(this.gainAutomatic);
            this.gainControl.Controls.Add(this.gainSlider);
            this.gainControl.ForeColor = System.Drawing.Color.White;
            this.gainControl.Name = "gainControl";
            this.gainControl.TabStop = false;
            this.gainControl.Tag = "Gain";
            // 
            // gainAutomatic
            // 
            resources.ApplyResources(this.gainAutomatic, "gainAutomatic");
            this.gainAutomatic.Name = "gainAutomatic";
            this.gainAutomatic.UseVisualStyleBackColor = true;
            // 
            // gainSlider
            // 
            resources.ApplyResources(this.gainSlider, "gainSlider");
            this.gainSlider.Maximum = 1000;
            this.gainSlider.Name = "gainSlider";
            this.gainSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // exposureControl
            // 
            resources.ApplyResources(this.exposureControl, "exposureControl");
            this.exposureControl.Controls.Add(this.exposureAutomatic);
            this.exposureControl.Controls.Add(this.exposureSlider);
            this.exposureControl.ForeColor = System.Drawing.Color.White;
            this.exposureControl.Name = "exposureControl";
            this.exposureControl.TabStop = false;
            this.exposureControl.Tag = "Exposure";
            // 
            // exposureAutomatic
            // 
            resources.ApplyResources(this.exposureAutomatic, "exposureAutomatic");
            this.exposureAutomatic.Name = "exposureAutomatic";
            this.exposureAutomatic.UseVisualStyleBackColor = true;
            // 
            // exposureSlider
            // 
            resources.ApplyResources(this.exposureSlider, "exposureSlider");
            this.exposureSlider.Maximum = 1000;
            this.exposureSlider.Name = "exposureSlider";
            this.exposureSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // focusControl
            // 
            resources.ApplyResources(this.focusControl, "focusControl");
            this.focusControl.Controls.Add(this.focusAutomatic);
            this.focusControl.Controls.Add(this.focusSlider);
            this.focusControl.ForeColor = System.Drawing.Color.White;
            this.focusControl.Name = "focusControl";
            this.focusControl.TabStop = false;
            this.focusControl.Tag = "Focus";
            // 
            // focusAutomatic
            // 
            resources.ApplyResources(this.focusAutomatic, "focusAutomatic");
            this.focusAutomatic.Name = "focusAutomatic";
            this.focusAutomatic.UseVisualStyleBackColor = true;
            // 
            // focusSlider
            // 
            resources.ApplyResources(this.focusSlider, "focusSlider");
            this.focusSlider.Maximum = 1000;
            this.focusSlider.Name = "focusSlider";
            this.focusSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // colourTab
            // 
            this.colourTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.colourTab.Controls.Add(this.colourPanel);
            resources.ApplyResources(this.colourTab, "colourTab");
            this.colourTab.Name = "colourTab";
            // 
            // colourPanel
            // 
            resources.ApplyResources(this.colourPanel, "colourPanel");
            this.colourPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.colourPanel.Controls.Add(this.whiteBalanceControl);
            this.colourPanel.Controls.Add(this.saturationControl);
            this.colourPanel.Controls.Add(this.gammaControl);
            this.colourPanel.Controls.Add(this.hueControl);
            this.colourPanel.Name = "colourPanel";
            // 
            // whiteBalanceControl
            // 
            resources.ApplyResources(this.whiteBalanceControl, "whiteBalanceControl");
            this.whiteBalanceControl.Controls.Add(this.whiteBalanceAutomatic);
            this.whiteBalanceControl.Controls.Add(this.whiteBalanceSlider);
            this.whiteBalanceControl.ForeColor = System.Drawing.Color.White;
            this.whiteBalanceControl.Name = "whiteBalanceControl";
            this.whiteBalanceControl.TabStop = false;
            this.whiteBalanceControl.Tag = "White balance";
            // 
            // whiteBalanceAutomatic
            // 
            resources.ApplyResources(this.whiteBalanceAutomatic, "whiteBalanceAutomatic");
            this.whiteBalanceAutomatic.Name = "whiteBalanceAutomatic";
            this.whiteBalanceAutomatic.UseVisualStyleBackColor = true;
            // 
            // whiteBalanceSlider
            // 
            resources.ApplyResources(this.whiteBalanceSlider, "whiteBalanceSlider");
            this.whiteBalanceSlider.Maximum = 1000;
            this.whiteBalanceSlider.Name = "whiteBalanceSlider";
            this.whiteBalanceSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // saturationControl
            // 
            resources.ApplyResources(this.saturationControl, "saturationControl");
            this.saturationControl.Controls.Add(this.saturationAutomatic);
            this.saturationControl.Controls.Add(this.saturationSlider);
            this.saturationControl.ForeColor = System.Drawing.Color.White;
            this.saturationControl.Name = "saturationControl";
            this.saturationControl.TabStop = false;
            this.saturationControl.Tag = "Saturation";
            // 
            // saturationAutomatic
            // 
            resources.ApplyResources(this.saturationAutomatic, "saturationAutomatic");
            this.saturationAutomatic.Name = "saturationAutomatic";
            this.saturationAutomatic.UseVisualStyleBackColor = true;
            // 
            // saturationSlider
            // 
            resources.ApplyResources(this.saturationSlider, "saturationSlider");
            this.saturationSlider.Maximum = 1000;
            this.saturationSlider.Name = "saturationSlider";
            this.saturationSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // gammaControl
            // 
            resources.ApplyResources(this.gammaControl, "gammaControl");
            this.gammaControl.Controls.Add(this.gammaAutomatic);
            this.gammaControl.Controls.Add(this.gammaSlider);
            this.gammaControl.ForeColor = System.Drawing.Color.White;
            this.gammaControl.Name = "gammaControl";
            this.gammaControl.TabStop = false;
            this.gammaControl.Tag = "Gamma";
            // 
            // gammaAutomatic
            // 
            resources.ApplyResources(this.gammaAutomatic, "gammaAutomatic");
            this.gammaAutomatic.Name = "gammaAutomatic";
            this.gammaAutomatic.UseVisualStyleBackColor = true;
            // 
            // gammaSlider
            // 
            resources.ApplyResources(this.gammaSlider, "gammaSlider");
            this.gammaSlider.Maximum = 1000;
            this.gammaSlider.Name = "gammaSlider";
            this.gammaSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // hueControl
            // 
            resources.ApplyResources(this.hueControl, "hueControl");
            this.hueControl.Controls.Add(this.hueAutomatic);
            this.hueControl.Controls.Add(this.hueSlider);
            this.hueControl.ForeColor = System.Drawing.Color.White;
            this.hueControl.Name = "hueControl";
            this.hueControl.TabStop = false;
            this.hueControl.Tag = "Hue";
            // 
            // hueAutomatic
            // 
            resources.ApplyResources(this.hueAutomatic, "hueAutomatic");
            this.hueAutomatic.Name = "hueAutomatic";
            this.hueAutomatic.UseVisualStyleBackColor = true;
            // 
            // hueSlider
            // 
            resources.ApplyResources(this.hueSlider, "hueSlider");
            this.hueSlider.Maximum = 1000;
            this.hueSlider.Name = "hueSlider";
            this.hueSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // lightTab
            // 
            this.lightTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.lightTab.Controls.Add(this.lightPanel);
            resources.ApplyResources(this.lightTab, "lightTab");
            this.lightTab.Name = "lightTab";
            // 
            // lightPanel
            // 
            resources.ApplyResources(this.lightPanel, "lightPanel");
            this.lightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.lightPanel.Controls.Add(this.contrastControl);
            this.lightPanel.Controls.Add(this.brightnessControl);
            this.lightPanel.Controls.Add(this.backlightControl);
            this.lightPanel.Controls.Add(this.sharpnessControl);
            this.lightPanel.Name = "lightPanel";
            // 
            // contrastControl
            // 
            resources.ApplyResources(this.contrastControl, "contrastControl");
            this.contrastControl.Controls.Add(this.contrastAutomatic);
            this.contrastControl.Controls.Add(this.contrastSlider);
            this.contrastControl.ForeColor = System.Drawing.Color.White;
            this.contrastControl.Name = "contrastControl";
            this.contrastControl.TabStop = false;
            this.contrastControl.Tag = "Contrast";
            // 
            // contrastAutomatic
            // 
            resources.ApplyResources(this.contrastAutomatic, "contrastAutomatic");
            this.contrastAutomatic.Name = "contrastAutomatic";
            this.contrastAutomatic.UseVisualStyleBackColor = true;
            // 
            // contrastSlider
            // 
            resources.ApplyResources(this.contrastSlider, "contrastSlider");
            this.contrastSlider.Maximum = 1000;
            this.contrastSlider.Name = "contrastSlider";
            this.contrastSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // brightnessControl
            // 
            resources.ApplyResources(this.brightnessControl, "brightnessControl");
            this.brightnessControl.Controls.Add(this.brightnessAutomatic);
            this.brightnessControl.Controls.Add(this.brightnessSlider);
            this.brightnessControl.ForeColor = System.Drawing.Color.White;
            this.brightnessControl.Name = "brightnessControl";
            this.brightnessControl.TabStop = false;
            this.brightnessControl.Tag = "Brightness";
            // 
            // brightnessAutomatic
            // 
            resources.ApplyResources(this.brightnessAutomatic, "brightnessAutomatic");
            this.brightnessAutomatic.Name = "brightnessAutomatic";
            this.brightnessAutomatic.UseVisualStyleBackColor = true;
            // 
            // brightnessSlider
            // 
            resources.ApplyResources(this.brightnessSlider, "brightnessSlider");
            this.brightnessSlider.Maximum = 1000;
            this.brightnessSlider.Name = "brightnessSlider";
            this.brightnessSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // backlightControl
            // 
            resources.ApplyResources(this.backlightControl, "backlightControl");
            this.backlightControl.Controls.Add(this.backlightAutomatic);
            this.backlightControl.Controls.Add(this.backlightSlider);
            this.backlightControl.ForeColor = System.Drawing.Color.White;
            this.backlightControl.Name = "backlightControl";
            this.backlightControl.TabStop = false;
            this.backlightControl.Tag = "Backlight compensation";
            // 
            // backlightAutomatic
            // 
            resources.ApplyResources(this.backlightAutomatic, "backlightAutomatic");
            this.backlightAutomatic.Name = "backlightAutomatic";
            this.backlightAutomatic.UseVisualStyleBackColor = true;
            // 
            // backlightSlider
            // 
            resources.ApplyResources(this.backlightSlider, "backlightSlider");
            this.backlightSlider.Maximum = 1000;
            this.backlightSlider.Name = "backlightSlider";
            this.backlightSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // sharpnessControl
            // 
            resources.ApplyResources(this.sharpnessControl, "sharpnessControl");
            this.sharpnessControl.Controls.Add(this.sharpnessAutomatic);
            this.sharpnessControl.Controls.Add(this.sharpnessSlider);
            this.sharpnessControl.ForeColor = System.Drawing.Color.White;
            this.sharpnessControl.Name = "sharpnessControl";
            this.sharpnessControl.TabStop = false;
            this.sharpnessControl.Tag = "Sharpness";
            // 
            // sharpnessAutomatic
            // 
            resources.ApplyResources(this.sharpnessAutomatic, "sharpnessAutomatic");
            this.sharpnessAutomatic.Name = "sharpnessAutomatic";
            this.sharpnessAutomatic.UseVisualStyleBackColor = true;
            // 
            // sharpnessSlider
            // 
            resources.ApplyResources(this.sharpnessSlider, "sharpnessSlider");
            this.sharpnessSlider.Maximum = 1000;
            this.sharpnessSlider.Name = "sharpnessSlider";
            this.sharpnessSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // resetVideoControls
            // 
            this.resetVideoControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.resetVideoControls.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.resetVideoControls, "resetVideoControls");
            this.resetVideoControls.ForeColor = System.Drawing.Color.White;
            this.resetVideoControls.Name = "resetVideoControls";
            this.resetVideoControls.Primary = false;
            this.resetVideoControls.UseVisualStyleBackColor = false;
            // 
            // issuesTab
            // 
            this.issuesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.issuesTab.Controls.Add(this.issueGroups);
            resources.ApplyResources(this.issuesTab, "issuesTab");
            this.issuesTab.Name = "issuesTab";
            // 
            // issueGroups
            // 
            resources.ApplyResources(this.issueGroups, "issueGroups");
            this.issueGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.issueGroups.Controls.Add(this.logGroup);
            this.issueGroups.Controls.Add(this.reportGroup);
            this.issueGroups.Name = "issueGroups";
            // 
            // logGroup
            // 
            this.logGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.logGroup.Controls.Add(this.logActions);
            this.logGroup.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.logGroup, "logGroup");
            this.logGroup.Name = "logGroup";
            this.logGroup.TabStop = false;
            // 
            // logActions
            // 
            this.logActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.logActions.Controls.Add(this.logSettingsLink);
            this.logActions.Controls.Add(this.showLogLink);
            resources.ApplyResources(this.logActions, "logActions");
            this.logActions.Name = "logActions";
            // 
            // logSettingsLink
            // 
            this.logSettingsLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.logSettingsLink, "logSettingsLink");
            this.logSettingsLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.logSettingsLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.logSettingsLink.Name = "logSettingsLink";
            this.logSettingsLink.TabStop = true;
            this.logSettingsLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // showLogLink
            // 
            this.showLogLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.showLogLink, "showLogLink");
            this.showLogLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.showLogLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.showLogLink.Name = "showLogLink";
            this.showLogLink.TabStop = true;
            this.showLogLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // reportGroup
            // 
            this.reportGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.reportGroup.Controls.Add(this.reportActions);
            this.reportGroup.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.reportGroup, "reportGroup");
            this.reportGroup.Name = "reportGroup";
            this.reportGroup.TabStop = false;
            // 
            // reportActions
            // 
            this.reportActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.reportActions.Controls.Add(this.reportNote);
            this.reportActions.Controls.Add(this.reportLink);
            resources.ApplyResources(this.reportActions, "reportActions");
            this.reportActions.Name = "reportActions";
            // 
            // reportNote
            // 
            resources.ApplyResources(this.reportNote, "reportNote");
            this.reportNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.reportNote.Name = "reportNote";
            // 
            // reportLink
            // 
            this.reportLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.reportLink, "reportLink");
            this.reportLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.reportLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.reportLink.Name = "reportLink";
            this.reportLink.TabStop = true;
            this.reportLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // releasesLink
            // 
            this.releasesLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.releasesLink, "releasesLink");
            this.releasesLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.releasesLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.releasesLink.Name = "releasesLink";
            this.releasesLink.TabStop = true;
            this.releasesLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // guideIndexLink
            // 
            this.guideIndexLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.guideIndexLink, "guideIndexLink");
            this.guideIndexLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.guideIndexLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.guideIndexLink.Name = "guideIndexLink";
            this.guideIndexLink.TabStop = true;
            this.guideIndexLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // openGuideButton
            // 
            this.openGuideButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.openGuideButton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.openGuideButton, "openGuideButton");
            this.openGuideButton.ForeColor = System.Drawing.Color.White;
            this.openGuideButton.Name = "openGuideButton";
            this.openGuideButton.Primary = false;
            this.openGuideButton.UseVisualStyleBackColor = false;
            // 
            // iconColumn
            // 
            this.iconColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.NullValue = null;
            this.iconColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.iconColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            resources.ApplyResources(this.iconColumn, "iconColumn");
            this.iconColumn.Name = "iconColumn";
            this.iconColumn.ReadOnly = true;
            // 
            // positionColumn
            // 
            resources.ApplyResources(this.positionColumn, "positionColumn");
            this.positionColumn.Name = "positionColumn";
            this.positionColumn.ReadOnly = true;
            // 
            // recallColumn
            // 
            this.recallColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.recallColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.recallColumn, "recallColumn");
            this.recallColumn.Name = "recallColumn";
            this.recallColumn.ReadOnly = true;
            this.recallColumn.Text = "▶";
            this.recallColumn.UseColumnTextForButtonValue = true;
            // 
            // root
            // 
            this.root.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.root, "root");
            this.root.Controls.Add(this.body, 0, 0);
            this.root.Controls.Add(this.footerSeparator, 0, 1);
            this.root.Controls.Add(this.footer, 0, 2);
            this.root.Name = "root";
            // 
            // body
            // 
            this.body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.body, "body");
            this.body.Controls.Add(this.mainSplit, 0, 0);
            this.body.Controls.Add(this.settingsTabs, 1, 0);
            this.body.Name = "body";
            // 
            // mainSplit
            // 
            this.mainSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.mainSplit, "mainSplit");
            this.mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            this.mainSplit.Panel1.Controls.Add(this.motionPanel);
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.Controls.Add(this.presetPanel);
            // 
            // motionPanel
            // 
            this.motionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.motionPanel, "motionPanel");
            this.motionPanel.Controls.Add(this.title, 0, 0);
            this.motionPanel.Controls.Add(this.fovDisplay, 0, 1);
            this.motionPanel.Controls.Add(this.zoomPanel, 1, 1);
            this.motionPanel.Controls.Add(this.wheel, 0, 2);
            this.motionPanel.Controls.Add(this.motionPosition, 0, 3);
            this.motionPanel.Name = "motionPanel";
            // 
            // title
            // 
            this.title.AutoEllipsis = true;
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.motionPanel.SetColumnSpan(this.title, 2);
            resources.ApplyResources(this.title, "title");
            this.title.Name = "title";
            this.title.UseMnemonic = false;
            // 
            // fovDisplay
            // 
            this.fovDisplay.Controls.Add(this.fieldOfViewLabel);
            this.fovDisplay.Controls.Add(this.fieldOfViewPicker);
            resources.ApplyResources(this.fovDisplay, "fovDisplay");
            this.fovDisplay.Name = "fovDisplay";
            // 
            // fieldOfViewLabel
            // 
            this.fieldOfViewLabel.AutoEllipsis = true;
            resources.ApplyResources(this.fieldOfViewLabel, "fieldOfViewLabel");
            this.fieldOfViewLabel.Name = "fieldOfViewLabel";
            // 
            // fieldOfViewPicker
            // 
            resources.ApplyResources(this.fieldOfViewPicker, "fieldOfViewPicker");
            this.fieldOfViewPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldOfViewPicker.Name = "fieldOfViewPicker";
            // 
            // zoomPanel
            // 
            this.zoomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.zoomPanel, "zoomPanel");
            this.zoomPanel.Controls.Add(this.fullFrame, 0, 0);
            this.zoomPanel.Controls.Add(this.normalIn, 0, 1);
            this.zoomPanel.Controls.Add(this.zoomSlider, 0, 2);
            this.zoomPanel.Controls.Add(this.normalOut, 0, 3);
            this.zoomPanel.Name = "zoomPanel";
            this.motionPanel.SetRowSpan(this.zoomPanel, 3);
            // 
            // normalIn
            // 
            this.normalIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.normalIn.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.normalIn, "normalIn");
            this.normalIn.ForeColor = System.Drawing.Color.White;
            this.normalIn.Name = "normalIn";
            this.normalIn.Primary = false;
            this.normalIn.UseVisualStyleBackColor = false;
            // 
            // zoomSlider
            // 
            this.zoomSlider.AccessibleRole = System.Windows.Forms.AccessibleRole.Slider;
            this.zoomSlider.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.zoomSlider, "zoomSlider");
            this.zoomSlider.Name = "zoomSlider";
            // 
            // normalOut
            // 
            this.normalOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.normalOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.normalOut.Direction = CameraControlStudio.ZoomDirection.Out;
            resources.ApplyResources(this.normalOut, "normalOut");
            this.normalOut.ForeColor = System.Drawing.Color.White;
            this.normalOut.Name = "normalOut";
            this.normalOut.Primary = false;
            this.normalOut.UseVisualStyleBackColor = false;
            // 
            // motionPosition
            // 
            this.motionPosition.AutoEllipsis = true;
            resources.ApplyResources(this.motionPosition, "motionPosition");
            this.motionPosition.Name = "motionPosition";
            // 
            // presetPanel
            // 
            this.presetPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.presetPanel, "presetPanel");
            this.presetPanel.Controls.Add(this.presetHeader, 0, 0);
            this.presetPanel.Controls.Add(this.presetGrid, 0, 1);
            this.presetPanel.Name = "presetPanel";
            // 
            // presetHeader
            // 
            this.presetHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.presetHeader, "presetHeader");
            this.presetHeader.Controls.Add(this.presetTitle, 0, 0);
            this.presetHeader.Controls.Add(this.deleteTemporaryButton, 1, 0);
            this.presetHeader.Controls.Add(this.temporaryButton, 2, 0);
            this.presetHeader.Name = "presetHeader";
            // 
            // presetTitle
            // 
            this.presetTitle.AutoEllipsis = true;
            this.presetTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.presetTitle, "presetTitle");
            this.presetTitle.Name = "presetTitle";
            this.presetTitle.UseMnemonic = false;
            // 
            // deleteTemporaryButton
            // 
            this.deleteTemporaryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.deleteTemporaryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.deleteTemporaryButton, "deleteTemporaryButton");
            this.deleteTemporaryButton.ForeColor = System.Drawing.Color.White;
            this.deleteTemporaryButton.Name = "deleteTemporaryButton";
            this.deleteTemporaryButton.Primary = false;
            this.deleteTemporaryButton.UseVisualStyleBackColor = false;
            // 
            // temporaryButton
            // 
            this.temporaryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.temporaryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.temporaryButton, "temporaryButton");
            this.temporaryButton.ForeColor = System.Drawing.Color.White;
            this.temporaryButton.Name = "temporaryButton";
            this.temporaryButton.Primary = false;
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
            resources.ApplyResources(this.presetGrid, "presetGrid");
            this.presetGrid.ColumnHeadersVisible = false;
            this.presetGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iconColumn,
            this.positionColumn,
            this.recallColumn});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.presetGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.presetGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(144)))), ((int)(((byte)(214)))));
            this.presetGrid.MultiSelect = false;
            this.presetGrid.Name = "presetGrid";
            this.presetGrid.ReadOnly = true;
            this.presetGrid.RowHeadersVisible = false;
            this.presetGrid.RowTemplate.Height = 44;
            this.presetGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // settingsTabs
            // 
            this.settingsTabs.Controls.Add(this.presetsTab);
            this.settingsTabs.Controls.Add(this.generalTab);
            this.settingsTabs.Controls.Add(this.imageTab);
            this.settingsTabs.Controls.Add(this.profilesTab);
            this.settingsTabs.Controls.Add(this.issuesTab);
            this.settingsTabs.Controls.Add(this.aboutTab);
            resources.ApplyResources(this.settingsTabs, "settingsTabs");
            this.settingsTabs.ForeColor = System.Drawing.Color.White;
            this.settingsTabs.Name = "settingsTabs";
            this.settingsTabs.SelectedIndex = 0;
            // 
            // presetsTab
            // 
            this.presetsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.presetsTab.Controls.Add(this.permanentPanel);
            resources.ApplyResources(this.presetsTab, "presetsTab");
            this.presetsTab.Name = "presetsTab";
            // 
            // permanentPanel
            // 
            this.permanentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.permanentPanel, "permanentPanel");
            this.permanentPanel.Controls.Add(this.permanentTitle, 0, 0);
            this.permanentPanel.Controls.Add(this.permanentList, 0, 1);
            this.permanentPanel.Controls.Add(this.permanentActions, 0, 2);
            this.permanentPanel.Name = "permanentPanel";
            // 
            // permanentTitle
            // 
            this.permanentTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.permanentTitle, "permanentTitle");
            this.permanentTitle.Name = "permanentTitle";
            // 
            // permanentList
            // 
            this.permanentList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            resources.ApplyResources(this.permanentList, "permanentList");
            this.permanentList.ForeColor = System.Drawing.Color.White;
            this.permanentList.Name = "permanentList";
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
            resources.ApplyResources(this.permanentActions, "permanentActions");
            this.permanentActions.Name = "permanentActions";
            // 
            // addPermanent
            // 
            this.addPermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.addPermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.addPermanent, "addPermanent");
            this.addPermanent.ForeColor = System.Drawing.Color.White;
            this.addPermanent.Name = "addPermanent";
            this.addPermanent.Primary = false;
            this.addPermanent.UseVisualStyleBackColor = false;
            // 
            // editPermanent
            // 
            this.editPermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.editPermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.editPermanent, "editPermanent");
            this.editPermanent.ForeColor = System.Drawing.Color.White;
            this.editPermanent.Name = "editPermanent";
            this.editPermanent.Primary = false;
            this.editPermanent.UseVisualStyleBackColor = false;
            // 
            // updatePermanent
            // 
            this.updatePermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.updatePermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.updatePermanent, "updatePermanent");
            this.updatePermanent.ForeColor = System.Drawing.Color.White;
            this.updatePermanent.Name = "updatePermanent";
            this.updatePermanent.Primary = false;
            this.updatePermanent.UseVisualStyleBackColor = false;
            // 
            // deletePermanent
            // 
            this.deletePermanent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.deletePermanent.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.deletePermanent, "deletePermanent");
            this.deletePermanent.ForeColor = System.Drawing.Color.White;
            this.deletePermanent.Name = "deletePermanent";
            this.deletePermanent.Primary = false;
            this.deletePermanent.UseVisualStyleBackColor = false;
            // 
            // movePermanentUp
            // 
            this.movePermanentUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.movePermanentUp.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.movePermanentUp, "movePermanentUp");
            this.movePermanentUp.ForeColor = System.Drawing.Color.White;
            this.movePermanentUp.Name = "movePermanentUp";
            this.movePermanentUp.Primary = false;
            this.movePermanentUp.UseVisualStyleBackColor = false;
            // 
            // movePermanentDown
            // 
            this.movePermanentDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.movePermanentDown.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.movePermanentDown, "movePermanentDown");
            this.movePermanentDown.ForeColor = System.Drawing.Color.White;
            this.movePermanentDown.Name = "movePermanentDown";
            this.movePermanentDown.Primary = false;
            this.movePermanentDown.UseVisualStyleBackColor = false;
            // 
            // generalTab
            // 
            this.generalTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.generalTab.Controls.Add(this.settingsCategories);
            resources.ApplyResources(this.generalTab, "generalTab");
            this.generalTab.Name = "generalTab";
            // 
            // imageTab
            // 
            this.imageTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.imageTab.Controls.Add(this.videoLayout);
            resources.ApplyResources(this.imageTab, "imageTab");
            this.imageTab.Name = "imageTab";
            // 
            // profilesTab
            // 
            this.profilesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.profilesTab.Controls.Add(this.profilesPanel);
            resources.ApplyResources(this.profilesTab, "profilesTab");
            this.profilesTab.Name = "profilesTab";
            // 
            // profilesPanel
            // 
            this.profilesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.profilesPanel, "profilesPanel");
            this.profilesPanel.Controls.Add(this.profilesList, 0, 0);
            this.profilesPanel.Controls.Add(this.profileActions, 0, 1);
            this.profilesPanel.Controls.Add(this.profileNote, 0, 2);
            this.profilesPanel.Name = "profilesPanel";
            // 
            // profilesList
            // 
            this.profilesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(79)))));
            resources.ApplyResources(this.profilesList, "profilesList");
            this.profilesList.ForeColor = System.Drawing.Color.White;
            this.profilesList.Name = "profilesList";
            // 
            // profileActions
            // 
            this.profileActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.profileActions.Controls.Add(this.newProfile);
            this.profileActions.Controls.Add(this.copyProfile);
            this.profileActions.Controls.Add(this.renameProfile);
            resources.ApplyResources(this.profileActions, "profileActions");
            this.profileActions.Name = "profileActions";
            // 
            // newProfile
            // 
            this.newProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.newProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.newProfile, "newProfile");
            this.newProfile.ForeColor = System.Drawing.Color.White;
            this.newProfile.Name = "newProfile";
            this.newProfile.Primary = false;
            this.newProfile.UseVisualStyleBackColor = false;
            // 
            // copyProfile
            // 
            this.copyProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.copyProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.copyProfile, "copyProfile");
            this.copyProfile.ForeColor = System.Drawing.Color.White;
            this.copyProfile.Name = "copyProfile";
            this.copyProfile.Primary = false;
            this.copyProfile.UseVisualStyleBackColor = false;
            // 
            // renameProfile
            // 
            this.renameProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.renameProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.renameProfile, "renameProfile");
            this.renameProfile.ForeColor = System.Drawing.Color.White;
            this.renameProfile.Name = "renameProfile";
            this.renameProfile.Primary = false;
            this.renameProfile.UseVisualStyleBackColor = false;
            // 
            // profileNote
            // 
            this.profileNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.profileNote, "profileNote");
            this.profileNote.Name = "profileNote";
            // 
            // aboutTab
            // 
            this.aboutTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.aboutTab.Controls.Add(this.aboutPanel);
            resources.ApplyResources(this.aboutTab, "aboutTab");
            this.aboutTab.Name = "aboutTab";
            // 
            // aboutPanel
            // 
            resources.ApplyResources(this.aboutPanel, "aboutPanel");
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
            this.aboutPanel.Name = "aboutPanel";
            // 
            // credit
            // 
            resources.ApplyResources(this.credit, "credit");
            this.credit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.credit.Name = "credit";
            // 
            // repoLink
            // 
            this.repoLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.repoLink, "repoLink");
            this.repoLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.repoLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.repoLink.Name = "repoLink";
            this.repoLink.TabStop = true;
            this.repoLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // issuesLink
            // 
            this.issuesLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.issuesLink, "issuesLink");
            this.issuesLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.issuesLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.issuesLink.Name = "issuesLink";
            this.issuesLink.TabStop = true;
            this.issuesLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // discussionLink
            // 
            this.discussionLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.discussionLink, "discussionLink");
            this.discussionLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.discussionLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.discussionLink.Name = "discussionLink";
            this.discussionLink.TabStop = true;
            this.discussionLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // coffeeLink
            // 
            this.coffeeLink.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.coffeeLink, "coffeeLink");
            this.coffeeLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.coffeeLink.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.coffeeLink.Name = "coffeeLink";
            this.coffeeLink.TabStop = true;
            this.coffeeLink.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // versionLabel
            // 
            resources.ApplyResources(this.versionLabel, "versionLabel");
            this.versionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.versionLabel.Name = "versionLabel";
            // 
            // footerSeparator
            // 
            resources.ApplyResources(this.footerSeparator, "footerSeparator");
            this.footerSeparator.Name = "footerSeparator";
            this.footerSeparator.TabStop = false;
            // 
            // footer
            // 
            this.footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.footer, "footer");
            this.footer.Controls.Add(this.footerLabels, 0, 0);
            this.footer.Controls.Add(this.profileButton, 1, 0);
            this.footer.Controls.Add(this.helpButton, 2, 0);
            this.footer.Controls.Add(this.settingsButton, 3, 0);
            this.footer.Name = "footer";
            // 
            // footerLabels
            // 
            this.footerLabels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.footerLabels, "footerLabels");
            this.footerLabels.Controls.Add(this.cameraStatus, 0, 0);
            this.footerLabels.Controls.Add(this.releaseNotice, 1, 0);
            this.footerLabels.Controls.Add(this.status, 0, 1);
            this.footerLabels.Name = "footerLabels";
            // 
            // cameraStatus
            // 
            this.cameraStatus.AutoEllipsis = true;
            this.cameraStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.cameraStatus, "cameraStatus");
            this.cameraStatus.Name = "cameraStatus";
            // 
            // releaseNotice
            // 
            this.releaseNotice.ActiveLinkColor = System.Drawing.Color.White;
            resources.ApplyResources(this.releaseNotice, "releaseNotice");
            this.releaseNotice.LinkColor = System.Drawing.Color.LightSkyBlue;
            this.releaseNotice.Name = "releaseNotice";
            this.releaseNotice.TabStop = true;
            this.releaseNotice.VisitedLinkColor = System.Drawing.Color.LightSkyBlue;
            // 
            // status
            // 
            this.status.AutoEllipsis = true;
            this.status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.footerLabels.SetColumnSpan(this.status, 2);
            resources.ApplyResources(this.status, "status");
            this.status.Name = "status";
            // 
            // profileButton
            // 
            this.profileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.profileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.profileButton, "profileButton");
            this.profileButton.ForeColor = System.Drawing.Color.White;
            this.profileButton.Name = "profileButton";
            this.profileButton.Primary = false;
            this.profileButton.UseVisualStyleBackColor = false;
            // 
            // helpButton
            // 
            this.helpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.helpButton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.helpButton, "helpButton");
            this.helpButton.ForeColor = System.Drawing.Color.White;
            this.helpButton.Name = "helpButton";
            this.helpButton.Primary = false;
            this.helpButton.UseVisualStyleBackColor = false;
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.settingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.settingsButton, "settingsButton");
            this.settingsButton.ForeColor = System.Drawing.Color.White;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Primary = false;
            this.settingsButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.root);
            this.Name = "MainForm";
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
        private BlueCheckBox allowFov;
        private BlueCheckBox adjustFovAtLimits;
        private Panel fovDisplay;
        private Label fieldOfViewLabel;
        private ComboBox fieldOfViewPicker;
        private BlueButton fullFrame;
        private Label motionPosition;
        private Label panStepsLabel;
        private BlueNumericUpDown panSteps;
        private Label fastPanStepsLabel;
        private BlueNumericUpDown fastPanSteps;
        private Label zoomStepsLabel;
        private BlueNumericUpDown zoomSteps;
        private Label secondsLabel;
        private BlueNumericUpDown seconds;
        private Label finePanLabel;
        private BlueNumericUpDown finePanPercent;
        private BlueCheckBox useTransitionSpeed;
        private Label transitionSpeedLabel;
        private BlueNumericUpDown transitionSpeedPercent;
        private BlueCheckBox reverseX;
        private BlueCheckBox panWithCameraButton;
        private BlueCheckBox doubleClickForCameraSource;
        private BlueCheckBox reverseY;
        private BlueCheckBox streamingCheck;
        private BlueCheckBox alwaysTop;
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
        private BlueCheckBox focusAutomatic;
        private GroupBox exposureControl;
        private TrackBar exposureSlider;
        private BlueCheckBox exposureAutomatic;
        private GroupBox gainControl;
        private TrackBar gainSlider;
        private BlueCheckBox gainAutomatic;
        private TabPage colourTab;
        private Panel colourPanel;
        private GroupBox hueControl;
        private TrackBar hueSlider;
        private BlueCheckBox hueAutomatic;
        private GroupBox gammaControl;
        private TrackBar gammaSlider;
        private BlueCheckBox gammaAutomatic;
        private GroupBox saturationControl;
        private TrackBar saturationSlider;
        private BlueCheckBox saturationAutomatic;
        private GroupBox whiteBalanceControl;
        private TrackBar whiteBalanceSlider;
        private BlueCheckBox whiteBalanceAutomatic;
        private TabPage lightTab;
        private Panel lightPanel;
        private GroupBox sharpnessControl;
        private TrackBar sharpnessSlider;
        private BlueCheckBox sharpnessAutomatic;
        private GroupBox backlightControl;
        private TrackBar backlightSlider;
        private BlueCheckBox backlightAutomatic;
        private GroupBox brightnessControl;
        private TrackBar brightnessSlider;
        private BlueCheckBox brightnessAutomatic;
        private GroupBox contrastControl;
        private TrackBar contrastSlider;
        private BlueCheckBox contrastAutomatic;
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
