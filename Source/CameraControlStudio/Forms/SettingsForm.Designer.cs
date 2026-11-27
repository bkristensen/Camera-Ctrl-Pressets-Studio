using System.Drawing;
using System.Windows.Forms;

namespace CameraControlStudio
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.settingsLayout = new TableLayoutPanel();
            this.settingsFooter = new TableLayoutPanel();
            this.settingsStatus = new Label();
            this.closeSettings = new BlueButton();
            this.settingsCategories = new CameraControlStudio.BlueTabControl();
            this.motionTab = new System.Windows.Forms.TabPage();
            this.motionGroup = new CameraControlStudio.BlueGroupBox();
            this.movementScroll = new System.Windows.Forms.Panel();
            this.generalFields = new System.Windows.Forms.TableLayoutPanel();
            this.directionSeparator = new CameraControlStudio.MainViewSeparator();
            this.cameraButtonSeparator = new CameraControlStudio.MainViewSeparator();
            this.zoomTab = new System.Windows.Forms.TabPage();
            this.zoomFields = new System.Windows.Forms.TableLayoutPanel();
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
            this.fieldOfViewTab = new System.Windows.Forms.TabPage();
            this.fieldOfViewFields = new System.Windows.Forms.TableLayoutPanel();
            this.startupFovLabel = new System.Windows.Forms.Label();
            this.startupFov = new System.Windows.Forms.ListBox();
            this.allowFov = new CameraControlStudio.BlueCheckBox();
            this.adjustFovAtLimits = new CameraControlStudio.BlueCheckBox();
            this.panWithCameraButton = new CameraControlStudio.BlueCheckBox();
            this.doubleClickForCameraSource = new CameraControlStudio.BlueCheckBox();
            this.applicationTab = new System.Windows.Forms.TabPage();
            this.applicationScroll = new System.Windows.Forms.Panel();
            this.applicationFields = new System.Windows.Forms.TableLayoutPanel();
            this.windowGroup = new CameraControlStudio.BlueGroupBox();
            this.windowFields = new System.Windows.Forms.TableLayoutPanel();
            this.streamingCheck = new CameraControlStudio.BlueCheckBox();
            this.alwaysTop = new CameraControlStudio.BlueCheckBox();
            this.languageGroup = new CameraControlStudio.BlueGroupBox();
            this.language = new System.Windows.Forms.ComboBox();
            this.videoLayout = new System.Windows.Forms.TableLayoutPanel();
            this.videoStatus = new System.Windows.Forms.FlowLayoutPanel();
            this.loadVideoControls = new CameraControlStudio.BlueButton();
            this.noVideoControls = new System.Windows.Forms.Label();
            this.imagePages = new System.Windows.Forms.TableLayoutPanel();
            this.imageCategories = new CameraControlStudio.BlueTabControl();
            this.focusTab = new System.Windows.Forms.TabPage();
            this.focusPanel = new System.Windows.Forms.Panel();
            this.gainControl = new CameraControlStudio.BlueGroupBox();
            this.gainAutomatic = new CameraControlStudio.BlueCheckBox();
            this.gainSlider = new System.Windows.Forms.TrackBar();
            this.exposureControl = new CameraControlStudio.BlueGroupBox();
            this.exposureAutomatic = new CameraControlStudio.BlueCheckBox();
            this.exposureSlider = new System.Windows.Forms.TrackBar();
            this.focusControl = new CameraControlStudio.BlueGroupBox();
            this.focusAutomatic = new CameraControlStudio.BlueCheckBox();
            this.focusSlider = new System.Windows.Forms.TrackBar();
            this.colourTab = new System.Windows.Forms.TabPage();
            this.colourPanel = new System.Windows.Forms.Panel();
            this.whiteBalanceControl = new CameraControlStudio.BlueGroupBox();
            this.whiteBalanceAutomatic = new CameraControlStudio.BlueCheckBox();
            this.whiteBalanceSlider = new System.Windows.Forms.TrackBar();
            this.saturationControl = new CameraControlStudio.BlueGroupBox();
            this.saturationAutomatic = new CameraControlStudio.BlueCheckBox();
            this.saturationSlider = new System.Windows.Forms.TrackBar();
            this.gammaControl = new CameraControlStudio.BlueGroupBox();
            this.gammaAutomatic = new CameraControlStudio.BlueCheckBox();
            this.gammaSlider = new System.Windows.Forms.TrackBar();
            this.hueControl = new CameraControlStudio.BlueGroupBox();
            this.hueAutomatic = new CameraControlStudio.BlueCheckBox();
            this.hueSlider = new System.Windows.Forms.TrackBar();
            this.lightTab = new System.Windows.Forms.TabPage();
            this.lightPanel = new System.Windows.Forms.Panel();
            this.contrastControl = new CameraControlStudio.BlueGroupBox();
            this.contrastAutomatic = new CameraControlStudio.BlueCheckBox();
            this.contrastSlider = new System.Windows.Forms.TrackBar();
            this.brightnessControl = new CameraControlStudio.BlueGroupBox();
            this.brightnessAutomatic = new CameraControlStudio.BlueCheckBox();
            this.brightnessSlider = new System.Windows.Forms.TrackBar();
            this.backlightControl = new CameraControlStudio.BlueGroupBox();
            this.backlightAutomatic = new CameraControlStudio.BlueCheckBox();
            this.backlightSlider = new System.Windows.Forms.TrackBar();
            this.sharpnessControl = new CameraControlStudio.BlueGroupBox();
            this.sharpnessAutomatic = new CameraControlStudio.BlueCheckBox();
            this.sharpnessSlider = new System.Windows.Forms.TrackBar();
            this.resetVideoControls = new CameraControlStudio.BlueButton();
            this.contactTab = new System.Windows.Forms.TabPage();
            this.issueGroups = new System.Windows.Forms.FlowLayoutPanel();
            this.logGroup = new CameraControlStudio.BlueGroupBox();
            this.logActions = new System.Windows.Forms.FlowLayoutPanel();
            this.logSettingsLink = new System.Windows.Forms.LinkLabel();
            this.showLogLink = new System.Windows.Forms.LinkLabel();
            this.githubActions = new CameraControlStudio.GitHubActions();
            this.reportGroup = new CameraControlStudio.BlueGroupBox();
            this.openGuideButton = new CameraControlStudio.BlueButton();
            this.settingsTabs = new CameraControlStudio.BlueTabControl();
            this.presetsTab = new System.Windows.Forms.TabPage();
            this.presetCategories = new CameraControlStudio.BlueTabControl();
            this.permanentPresetsTab = new System.Windows.Forms.TabPage();
            this.transitionsTab = new System.Windows.Forms.TabPage();
            this.transitionFields = new System.Windows.Forms.TableLayoutPanel();
            this.permanentPanel = new System.Windows.Forms.TableLayoutPanel();
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
            this.contactCategories = new CameraControlStudio.BlueTabControl();
            this.aboutInfoTab = new System.Windows.Forms.TabPage();
            this.aboutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.credit = new System.Windows.Forms.Label();
            this.coffeeLink = new System.Windows.Forms.LinkLabel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.settingsCategories.SuspendLayout();
            this.motionTab.SuspendLayout();
            this.motionGroup.SuspendLayout();
            this.movementScroll.SuspendLayout();
            this.generalFields.SuspendLayout();
            this.zoomTab.SuspendLayout();
            this.zoomFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.finePanPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPanSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transitionSpeedPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).BeginInit();
            this.fieldOfViewTab.SuspendLayout();
            this.fieldOfViewFields.SuspendLayout();
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
            this.contactTab.SuspendLayout();
            this.issueGroups.SuspendLayout();
            this.logGroup.SuspendLayout();
            this.logActions.SuspendLayout();
            this.reportGroup.SuspendLayout();
            this.settingsTabs.SuspendLayout();
            this.presetsTab.SuspendLayout();
            this.presetCategories.SuspendLayout();
            this.permanentPresetsTab.SuspendLayout();
            this.transitionsTab.SuspendLayout();
            this.transitionFields.SuspendLayout();
            this.permanentPanel.SuspendLayout();
            this.permanentActions.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.imageTab.SuspendLayout();
            this.profilesTab.SuspendLayout();
            this.profilesPanel.SuspendLayout();
            this.profileActions.SuspendLayout();
            this.aboutTab.SuspendLayout();
            this.contactCategories.SuspendLayout();
            this.aboutInfoTab.SuspendLayout();
            this.aboutPanel.SuspendLayout();
            this.settingsLayout.SuspendLayout();
            this.settingsFooter.SuspendLayout();
            this.SuspendLayout();
            //
            // settingsCategories
            //
            this.settingsCategories.Controls.Add(this.motionTab);
            this.settingsCategories.Controls.Add(this.zoomTab);
            this.settingsCategories.Controls.Add(this.applicationTab);
            resources.ApplyResources(this.settingsCategories, "settingsCategories");
            this.settingsCategories.Multiline = true;
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
            this.generalFields.Controls.Add(this.directionSeparator, 0, 3);
            this.generalFields.Controls.Add(this.reverseX, 0, 4);
            this.generalFields.Controls.Add(this.reverseY, 0, 5);
            this.generalFields.Controls.Add(this.cameraButtonSeparator, 0, 6);
            this.generalFields.Controls.Add(this.panWithCameraButton, 0, 7);
            this.generalFields.Controls.Add(this.doubleClickForCameraSource, 0, 8);
            this.generalFields.Name = "generalFields";
            //
            // directionSeparator
            //
            this.generalFields.SetColumnSpan(this.directionSeparator, 2);
            resources.ApplyResources(this.directionSeparator, "directionSeparator");
            this.directionSeparator.Name = "directionSeparator";
            //
            // cameraButtonSeparator
            //
            this.generalFields.SetColumnSpan(this.cameraButtonSeparator, 2);
            resources.ApplyResources(this.cameraButtonSeparator, "cameraButtonSeparator");
            this.cameraButtonSeparator.Name = "cameraButtonSeparator";
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
            // zoomTab
            //
            this.zoomTab.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.zoomTab.Controls.Add(this.zoomFields);
            resources.ApplyResources(this.zoomTab, "zoomTab");
            this.zoomTab.Name = "zoomTab";
            //
            // zoomFields
            //
            resources.ApplyResources(this.zoomFields, "zoomFields");
            this.zoomFields.Controls.Add(this.zoomStepsLabel, 0, 0);
            this.zoomFields.Controls.Add(this.zoomSteps, 1, 0);
            this.zoomFields.Name = "zoomFields";
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
            this.transitionFields.SetColumnSpan(this.useTransitionSpeed, 2);
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
            // fieldOfViewTab
            //
            this.fieldOfViewTab.AutoScroll = true;
            this.fieldOfViewTab.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.fieldOfViewTab.Controls.Add(this.fieldOfViewFields);
            resources.ApplyResources(this.fieldOfViewTab, "fieldOfViewTab");
            this.fieldOfViewTab.Name = "fieldOfViewTab";
            //
            // fieldOfViewFields
            //
            resources.ApplyResources(this.fieldOfViewFields, "fieldOfViewFields");
            this.fieldOfViewFields.Controls.Add(this.startupFovLabel, 0, 0);
            this.fieldOfViewFields.Controls.Add(this.startupFov, 1, 0);
            this.fieldOfViewFields.Controls.Add(this.allowFov, 0, 1);
            this.fieldOfViewFields.Controls.Add(this.adjustFovAtLimits, 0, 2);
            this.fieldOfViewFields.ForeColor = System.Drawing.Color.White;
            this.fieldOfViewFields.Name = "fieldOfViewFields";
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
            this.fieldOfViewFields.SetColumnSpan(this.allowFov, 2);
            resources.ApplyResources(this.allowFov, "allowFov");
            this.allowFov.Name = "allowFov";
            //
            // adjustFovAtLimits
            //
            this.adjustFovAtLimits.Checked = true;
            this.adjustFovAtLimits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fieldOfViewFields.SetColumnSpan(this.adjustFovAtLimits, 2);
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
            this.imageCategories.Controls.Add(this.fieldOfViewTab);
            resources.ApplyResources(this.imageCategories, "imageCategories");
            this.imageCategories.Multiline = true;
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
            this.resetVideoControls.AutoSize = true;
            this.resetVideoControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.resetVideoControls.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.resetVideoControls, "resetVideoControls");
            this.resetVideoControls.ForeColor = System.Drawing.Color.White;
            this.resetVideoControls.MinimumSize = new System.Drawing.Size(0, 26);
            this.resetVideoControls.Name = "resetVideoControls";
            this.resetVideoControls.Primary = false;
            this.resetVideoControls.UseVisualStyleBackColor = false;
            //
            // contactTab
            //
            this.contactTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.contactTab.Controls.Add(this.issueGroups);
            resources.ApplyResources(this.contactTab, "contactTab");
            this.contactTab.Name = "contactTab";
            //
            // issueGroups
            //
            resources.ApplyResources(this.issueGroups, "issueGroups");
            this.issueGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.issueGroups.Controls.Add(this.reportGroup);
            this.issueGroups.Controls.Add(this.logGroup);
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
            this.reportGroup.Controls.Add(this.githubActions);
            this.githubActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.githubActions.Name = "githubActions";
            this.githubActions.TabIndex = 0;
            this.reportGroup.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.reportGroup, "reportGroup");
            this.reportGroup.Name = "reportGroup";
            this.reportGroup.TabStop = false;
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
            // settingsTabs
            //
            this.settingsTabs.Controls.Add(this.presetsTab);
            this.settingsTabs.Controls.Add(this.generalTab);
            this.settingsTabs.Controls.Add(this.imageTab);
            this.settingsTabs.Controls.Add(this.profilesTab);
            this.settingsTabs.Controls.Add(this.aboutTab);
            resources.ApplyResources(this.settingsTabs, "settingsTabs");
            this.settingsTabs.ForeColor = System.Drawing.Color.White;
            this.settingsTabs.Name = "settingsTabs";
            this.settingsTabs.SelectedIndex = 0;
            //
            // presetsTab
            //
            this.presetsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.presetsTab.Controls.Add(this.presetCategories);
            resources.ApplyResources(this.presetsTab, "presetsTab");
            this.presetsTab.Name = "presetsTab";
            //
            // presetCategories
            //
            this.presetCategories.Controls.Add(this.permanentPresetsTab);
            this.presetCategories.Controls.Add(this.transitionsTab);
            resources.ApplyResources(this.presetCategories, "presetCategories");
            this.presetCategories.Multiline = true;
            this.presetCategories.Name = "presetCategories";
            this.presetCategories.SelectedIndex = 0;
            //
            // permanentPresetsTab
            //
            this.permanentPresetsTab.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.permanentPresetsTab.Controls.Add(this.permanentPanel);
            resources.ApplyResources(this.permanentPresetsTab, "permanentPresetsTab");
            this.permanentPresetsTab.Name = "permanentPresetsTab";
            //
            // transitionsTab
            //
            this.transitionsTab.AutoScroll = true;
            this.transitionsTab.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.transitionsTab.Controls.Add(this.transitionFields);
            resources.ApplyResources(this.transitionsTab, "transitionsTab");
            this.transitionsTab.Name = "transitionsTab";
            //
            // transitionFields
            //
            resources.ApplyResources(this.transitionFields, "transitionFields");
            this.transitionFields.Controls.Add(this.useTransitionSpeed, 0, 0);
            this.transitionFields.Controls.Add(this.transitionSpeedLabel, 0, 1);
            this.transitionFields.Controls.Add(this.transitionSpeedPercent, 1, 1);
            this.transitionFields.Controls.Add(this.secondsLabel, 0, 2);
            this.transitionFields.Controls.Add(this.seconds, 1, 2);
            this.transitionFields.Name = "transitionFields";
            //
            // permanentPanel
            //
            this.permanentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            resources.ApplyResources(this.permanentPanel, "permanentPanel");
            this.permanentPanel.Controls.Add(this.permanentList, 0, 0);
            this.permanentPanel.Controls.Add(this.permanentActions, 0, 1);
            this.permanentPanel.Name = "permanentPanel";
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
            this.aboutTab.Controls.Add(this.contactCategories);
            resources.ApplyResources(this.aboutTab, "aboutTab");
            this.aboutTab.Name = "aboutTab";
            //
            // contactCategories
            //
            this.contactCategories.Controls.Add(this.contactTab);
            this.contactCategories.Controls.Add(this.aboutInfoTab);
            resources.ApplyResources(this.contactCategories, "contactCategories");
            this.contactCategories.Multiline = true;
            this.contactCategories.Name = "contactCategories";
            this.contactCategories.SelectedIndex = 0;
            //
            // aboutInfoTab
            //
            this.aboutInfoTab.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.aboutInfoTab.Controls.Add(this.aboutPanel);
            resources.ApplyResources(this.aboutInfoTab, "aboutInfoTab");
            this.aboutInfoTab.Name = "aboutInfoTab";
            //
            // aboutPanel
            //
            resources.ApplyResources(this.aboutPanel, "aboutPanel");
            this.aboutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.aboutPanel.Controls.Add(this.credit);
            this.aboutPanel.Controls.Add(this.coffeeLink);
            this.aboutPanel.Controls.Add(this.versionLabel);
            this.aboutPanel.Controls.Add(this.openGuideButton);
            this.aboutPanel.Name = "aboutPanel";
            //
            // credit
            //
            resources.ApplyResources(this.credit, "credit");
            this.credit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(94)))));
            this.credit.Name = "credit";
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
            // settingsLayout
            //
            this.settingsLayout.ColumnCount = 1;
            this.settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.settingsLayout.RowCount = 2;
            this.settingsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.settingsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            this.settingsLayout.Controls.Add(this.settingsTabs, 0, 0);
            this.settingsLayout.Controls.Add(this.settingsFooter, 0, 1);
            this.settingsLayout.Dock = DockStyle.Fill;
            this.settingsLayout.Name = "settingsLayout";
            this.settingsLayout.Padding = new Padding(8, 8, 8, 0);
            this.settingsLayout.TabIndex = 0;
            //
            // settingsFooter
            //
            this.settingsFooter.ColumnCount = 2;
            this.settingsFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.settingsFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            this.settingsFooter.RowCount = 1;
            this.settingsFooter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.settingsFooter.Controls.Add(this.settingsStatus, 0, 0);
            this.settingsFooter.Controls.Add(this.closeSettings, 1, 0);
            this.settingsFooter.Dock = DockStyle.Fill;
            this.settingsFooter.Margin = new Padding(5, 2, 5, 4);
            this.settingsFooter.Name = "settingsFooter";
            this.settingsFooter.TabIndex = 1;
            //
            // settingsStatus
            //
            this.settingsStatus.AutoEllipsis = true;
            this.settingsStatus.Dock = DockStyle.Fill;
            this.settingsStatus.ForeColor = Color.White;
            this.settingsStatus.Name = "settingsStatus";
            this.settingsStatus.TextAlign = ContentAlignment.MiddleLeft;
            //
            // closeSettings
            //
            this.closeSettings.AccessibleName = "Close";
            this.closeSettings.BackColor = Color.FromArgb(9, 52, 94);
            this.closeSettings.Cursor = Cursors.Hand;
            this.closeSettings.Dock = DockStyle.Fill;
            this.closeSettings.Font = new Font("Segoe UI", 15F);
            this.closeSettings.ForeColor = Color.White;
            this.closeSettings.Name = "closeSettings";
            this.closeSettings.TabIndex = 0;
            this.closeSettings.Text = "×";
            this.closeSettings.UseVisualStyleBackColor = false;
            //
            // SettingsForm
            //
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = Color.FromArgb(9, 52, 94);
            this.Controls.Add(this.settingsLayout);
            this.ForeColor = Color.White;
            this.Icon = (Icon)resources.GetObject("$this.Icon");
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.settingsCategories.ResumeLayout(false);
            this.motionTab.ResumeLayout(false);
            this.motionGroup.ResumeLayout(false);
            this.movementScroll.ResumeLayout(false);
            this.movementScroll.PerformLayout();
            this.generalFields.ResumeLayout(false);
            this.zoomTab.ResumeLayout(false);
            this.zoomTab.PerformLayout();
            this.zoomFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.finePanPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPanSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transitionSpeedPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).EndInit();
            this.fieldOfViewTab.ResumeLayout(false);
            this.fieldOfViewTab.PerformLayout();
            this.fieldOfViewFields.ResumeLayout(false);
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
            this.contactTab.ResumeLayout(false);
            this.issueGroups.ResumeLayout(false);
            this.logGroup.ResumeLayout(false);
            this.logActions.ResumeLayout(false);
            this.logActions.PerformLayout();
            this.reportGroup.ResumeLayout(false);
            this.settingsTabs.ResumeLayout(false);
            this.presetsTab.ResumeLayout(false);
            this.presetCategories.ResumeLayout(false);
            this.permanentPresetsTab.ResumeLayout(false);
            this.transitionsTab.ResumeLayout(false);
            this.transitionsTab.PerformLayout();
            this.transitionFields.ResumeLayout(false);
            this.permanentPanel.ResumeLayout(false);
            this.permanentActions.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.imageTab.ResumeLayout(false);
            this.profilesTab.ResumeLayout(false);
            this.profilesPanel.ResumeLayout(false);
            this.profileActions.ResumeLayout(false);
            this.aboutTab.ResumeLayout(false);
            this.contactCategories.ResumeLayout(false);
            this.aboutInfoTab.ResumeLayout(false);
            this.aboutPanel.ResumeLayout(false);
            this.aboutPanel.PerformLayout();
            this.settingsFooter.ResumeLayout(false);
            this.settingsLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private TableLayoutPanel settingsLayout;
        private TableLayoutPanel settingsFooter;
        private Label settingsStatus;
        private BlueButton closeSettings;
        internal BlueTabControl settingsTabs;
        internal TabPage generalTab;
        internal TabPage imageTab;
        internal TabPage presetsTab;
        internal BlueTabControl presetCategories;
        internal TabPage permanentPresetsTab;
        internal TabPage transitionsTab;
        internal TableLayoutPanel transitionFields;
        internal TabPage profilesTab;
        internal TabPage aboutTab;
        internal BlueTabControl contactCategories;
        internal TabPage aboutInfoTab;
        internal Panel movementScroll;
        internal TableLayoutPanel generalFields;
        internal MainViewSeparator directionSeparator;
        internal MainViewSeparator cameraButtonSeparator;
        internal TabPage zoomTab;
        internal TableLayoutPanel zoomFields;
        internal TabPage fieldOfViewTab;
        internal TableLayoutPanel fieldOfViewFields;
        internal Label startupFovLabel;
        internal ListBox startupFov;
        internal BlueCheckBox allowFov;
        internal BlueCheckBox adjustFovAtLimits;
        internal Label panStepsLabel;
        internal BlueNumericUpDown panSteps;
        internal Label fastPanStepsLabel;
        internal BlueNumericUpDown fastPanSteps;
        internal Label zoomStepsLabel;
        internal BlueNumericUpDown zoomSteps;
        internal Label secondsLabel;
        internal BlueNumericUpDown seconds;
        internal Label finePanLabel;
        internal BlueNumericUpDown finePanPercent;
        internal BlueCheckBox useTransitionSpeed;
        internal Label transitionSpeedLabel;
        internal BlueNumericUpDown transitionSpeedPercent;
        internal BlueCheckBox reverseX;
        internal BlueCheckBox panWithCameraButton;
        internal BlueCheckBox doubleClickForCameraSource;
        internal BlueCheckBox reverseY;
        internal BlueCheckBox streamingCheck;
        internal BlueCheckBox alwaysTop;
        internal ComboBox language;
        internal TableLayoutPanel permanentPanel;
        internal ListBox permanentList;
        internal FlowLayoutPanel permanentActions;
        internal BlueButton addPermanent;
        internal BlueButton editPermanent;
        internal BlueButton updatePermanent;
        internal BlueButton deletePermanent;
        internal BlueButton movePermanentUp;
        internal BlueButton movePermanentDown;
        internal TableLayoutPanel profilesPanel;
        internal ListBox profilesList;
        internal FlowLayoutPanel profileActions;
        internal BlueButton newProfile;
        internal BlueButton copyProfile;
        internal BlueButton renameProfile;
        internal Label profileNote;
        internal FlowLayoutPanel aboutPanel;
        internal Label credit;
        internal LinkLabel coffeeLink;
        internal Label versionLabel;
        internal BlueTabControl settingsCategories;
        internal TabPage motionTab;
        internal TabPage applicationTab;
        internal BlueGroupBox motionGroup;
        internal Panel applicationScroll;
        internal TableLayoutPanel applicationFields;
        internal BlueGroupBox windowGroup;
        internal BlueGroupBox languageGroup;
        internal TableLayoutPanel windowFields;
        internal TableLayoutPanel videoLayout;
        internal FlowLayoutPanel videoStatus;
        internal BlueButton loadVideoControls;
        internal Label noVideoControls;
        internal TableLayoutPanel imagePages;
        internal BlueTabControl imageCategories;
        internal BlueButton resetVideoControls;
        internal TabPage focusTab;
        internal Panel focusPanel;
        internal BlueGroupBox focusControl;
        internal TrackBar focusSlider;
        internal BlueCheckBox focusAutomatic;
        internal BlueGroupBox exposureControl;
        internal TrackBar exposureSlider;
        internal BlueCheckBox exposureAutomatic;
        internal BlueGroupBox gainControl;
        internal TrackBar gainSlider;
        internal BlueCheckBox gainAutomatic;
        internal TabPage colourTab;
        internal Panel colourPanel;
        internal BlueGroupBox hueControl;
        internal TrackBar hueSlider;
        internal BlueCheckBox hueAutomatic;
        internal BlueGroupBox gammaControl;
        internal TrackBar gammaSlider;
        internal BlueCheckBox gammaAutomatic;
        internal BlueGroupBox saturationControl;
        internal TrackBar saturationSlider;
        internal BlueCheckBox saturationAutomatic;
        internal BlueGroupBox whiteBalanceControl;
        internal TrackBar whiteBalanceSlider;
        internal BlueCheckBox whiteBalanceAutomatic;
        internal TabPage lightTab;
        internal Panel lightPanel;
        internal BlueGroupBox sharpnessControl;
        internal TrackBar sharpnessSlider;
        internal BlueCheckBox sharpnessAutomatic;
        internal BlueGroupBox backlightControl;
        internal TrackBar backlightSlider;
        internal BlueCheckBox backlightAutomatic;
        internal BlueGroupBox brightnessControl;
        internal TrackBar brightnessSlider;
        internal BlueCheckBox brightnessAutomatic;
        internal BlueGroupBox contrastControl;
        internal TrackBar contrastSlider;
        internal BlueCheckBox contrastAutomatic;
        internal TabPage contactTab;
        internal FlowLayoutPanel issueGroups;
        internal BlueGroupBox logGroup;
        internal BlueGroupBox reportGroup;
        internal GitHubActions githubActions;
        internal FlowLayoutPanel logActions;
        internal LinkLabel logSettingsLink;
        internal LinkLabel showLogLink;
        internal BlueButton openGuideButton;
    }
}
