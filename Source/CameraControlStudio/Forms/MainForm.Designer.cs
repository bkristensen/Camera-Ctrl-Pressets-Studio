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
            this.footerSeparator = new CameraControlStudio.MainViewSeparator();
            this.footer = new System.Windows.Forms.TableLayoutPanel();
            this.footerLabels = new System.Windows.Forms.TableLayoutPanel();
            this.cameraStatus = new System.Windows.Forms.Label();
            this.releaseNotice = new System.Windows.Forms.LinkLabel();
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
            this.fovDisplay.SuspendLayout();
            this.zoomPanel.SuspendLayout();
            this.presetPanel.SuspendLayout();
            this.presetHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.presetGrid)).BeginInit();
            this.footer.SuspendLayout();
            this.footerLabels.SuspendLayout();
            this.SuspendLayout();
            //
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
        private Panel fovDisplay;
        private Label fieldOfViewLabel;
        private ComboBox fieldOfViewPicker;
        private BlueButton fullFrame;
        private Label motionPosition;
        private TableLayoutPanel footerLabels;
        private TableLayoutPanel footer;
        private MainViewSeparator footerSeparator;
        private ToolTip mainToolTips;
        private Timer movement;
        private Timer held;
        private Timer saveTimer;
        private Timer motionLimits;
        private Timer documentationTimer;
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
