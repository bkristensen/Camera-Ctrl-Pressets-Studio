using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace CameraControlStudio {
    public partial class MainForm : AppForm {
        readonly ProfileStore store = new ProfileStore();
        readonly SessionPresets temporary = new SessionPresets();
        readonly CameraAccessGate permission = new CameraAccessGate();
        readonly PresetActivity activity = new PresetActivity();
        readonly HashSet<string> failedMotionReads = new HashSet<string>();
        readonly Stopwatch clock = new Stopwatch();
        Font permanentFont;
        readonly Dictionary<string, Bitmap> iconImages = new Dictionary<string, Bitmap>();
        ProfileEntry entry; SavedData config; string profile = ProfileStore.Standard;
        Camera camera; List<Device> devices = new List<Device>();
        bool loaded, applying, dirty, wasMaximized;
        bool refreshingPresets;
        Dictionary<string, int> start, target, last; double duration;
        PresetSpeedCurve speedCurve;
        static readonly string[] axes = { "Zoom", "Pan", "Tilt" };
        readonly Size preferredMinimumSize;
        readonly int settingsPanelWidth, temporaryDeleteWidth;
        int settingsWidthAdded;
        int iconColumnWidth, recallColumnWidth;
        internal bool RestartRequested {
            get; private set;
        }
        string CameraId {
            get {
                return config.CameraId ?? "";
            }
        }
        public MainForm() {
            InitializeComponent();
            if (InDesigner)
                return;
            preferredMinimumSize = MinimumSize;
            settingsPanelWidth = (int)body.ColumnStyles[1].Width;
            temporaryDeleteWidth = (int)presetHeader.ColumnStyles[1].Width;
            SetSettings(false, false);
            FormText.Translate(this);
            ConfigureGrid();
            WireSettings();
            WireVideoControls();
            ConfigureMainToolTips();
            ConfigureSettingsToolTips();
            WireEvents();
            WireFraming();
            RefreshActionButtons();
            RefreshMotionControls();
        }
        internal MainForm(ProfileEntry initialEntry, string initialProfile) : this() {
            entry = initialEntry;
            profile = initialProfile;
        }
        void Run(Action action) {
            try {
                action();
            } catch (OperationCanceledException) {
                StopMotion();
            } catch (CameraUnavailableException ex) {
                DiagnosticLog.Current.Write("WARNING", "Camera connection lost", ex);
                Disconnect();
                status.Text = UiText.T("Camera unavailable. Use a camera control to reconnect.",
                    "Kameraet er ikke tilgængeligt. Brug en kameraknap for at forbinde igen.");
            } catch (InvalidOperationException ex) {
                StopMotion();
                DiagnosticLog.Current.Write("WARNING", "Handle application action", ex);
                status.Text = ex.Message;
                ShowMessage(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (Exception ex) {
                StopMotion();
                var log = DiagnosticLog.Current.Write("ERROR", "Handle application action", ex);
                status.Text = UiText.T("An unexpected error prevented this action.", "En uventet fejl forhindrede handlingen.");
                ErrorDialog.Show(this, log ?? DiagnosticLog.Current.FindLatest());
            }
        }


        void WireEvents() {
            Shown += (o, e) => {
                if (documentationMode)
                    InitializeDocumentation();
                else
                    Run(InitializeProfile);
            };
            settingsButton.Click += (o, e) => Run(() => {
                SetSettings(!settingsTabs.Visible, true);
                MarkDirty();
            });
            profileButton.Click += (o, e) => Run(ChooseProfile);
            wheel.MovementRequested += (pan, tilt, speed) => Run(() => StartPanHeld(pan, tilt, speed));
            wheel.DragRequested += (pan, tilt) => Run(() => UpdatePanDrag(pan, tilt));
            wheel.Released += StopHeld;
            wheel.SourceRequested += () => Run(ChooseCamera);
            WireZoom(normalIn, 1);
            WireZoom(normalOut, -1);
            zoomSlider.PositionRequested += fraction => Run(() => SetZoomPosition(fraction));
            zoomSlider.StepRequested += direction => Run(() => Nudge("Zoom", direction, (int)zoomSteps.Value));
            movement.Tick += (o, e) => Run(TickMotion);
            held.Tick += (o, e) => Run(() => {
                if (repeatHeld?.Invoke() != true)
                    StopHeld();
            });
            motionLimits.Tick += (o, e) => Run(() => {
                if (movement.Enabled || held.Enabled || zoomSlider.Capture)
                    return;
                try {
                    RefreshMotionControls();
                } catch (Exception ex) when (!(ex is CameraUnavailableException)) { Disconnect(); throw; }
            });
            saveTimer.Tick += (o, e) => {
                saveTimer.Stop();
                if (loaded && dirty && ContainsFocus && !movement.Enabled && !held.Enabled)
                    Run(SaveCurrent);
                else if (dirty)
                    saveTimer.Start();
            };
            temporaryButton.Click += (o, e) => Run(AddTemporary);
            deleteTemporaryButton.Click += (o, e) => Run(DeleteTemporary);
            presetGrid.SelectionChanged += (o, e) => {
                if (refreshingPresets)
                    return;
                SelectPresetInSettings(SelectedPresetRow);
                RefreshActionButtons();
            };
            permanentList.SelectedIndexChanged += (o, e) => {
                if (!refreshingPresets)
                    RefreshActionButtons();
            };
            presetGrid.CellContentClick += (o, e) => {
                if (e.RowIndex >= 0 && e.ColumnIndex == recallColumn.Index)
                    Run(() => Recall((RowItem)presetGrid.Rows[e.RowIndex].Tag));
            };
            presetGrid.CellDoubleClick += (o, e) => {
                if (e.RowIndex >= 0 && e.ColumnIndex != recallColumn.Index)
                    Run(() => Recall((RowItem)presetGrid.Rows[e.RowIndex].Tag));
            };
            presetGrid.KeyDown += (o, e) => {
                if (e.KeyCode == Keys.Enter) {
                    e.Handled = e.SuppressKeyPress = true;
                    Run(() => Recall(SelectedPresetRow));
                }
            };
            presetGrid.CellFormatting += FormatPresetCell;
            addPermanent.Click += (o, e) => Run(AddPermanent);
            editPermanent.Click += (o, e) => Run(EditPermanent);
            updatePermanent.Click += (o, e) => Run(UpdatePermanent);
            deletePermanent.Click += (o, e) => Run(DeletePermanent);
            movePermanentUp.Click += (o, e) => Run(() => MovePermanent(-1));
            movePermanentDown.Click += (o, e) => Run(() => MovePermanent(1));
            newProfile.Click += (o, e) => Run(() => CreateProfile(false));
            copyProfile.Click += (o, e) => Run(() => CreateProfile(true));
            renameProfile.Click += (o, e) => Run(RenameProfile);
            profilesList.SelectedIndexChanged += (o, e) => RefreshActionButtons();
            profilesList.DoubleClick += (o, e) => Run(() => { if (profilesList.SelectedItem != null) ChooseProfile((string)profilesList.SelectedItem); });
            foreach (var control in new[] { finePanPercent, panSteps, fastPanSteps, zoomSteps, seconds, transitionSpeedPercent })
                control.ValueChanged += (o, e) => MarkDirty();
            useTransitionSpeed.CheckedChanged += (o, e) => {
                UpdateTransitionMode();
                MarkDirty();
            };
            foreach (var control in new[] { reverseX, reverseY, alwaysTop })
                control.CheckedChanged += (o, e) => { if (!applying) { TopMost = alwaysTop.Checked; MarkDirty(); } };
            reverseX.CheckedChanged += (o, e) => Run(() => {
                blockedFieldOfViewSteps.Clear();
                RefreshMotionAxis("Pan");
            });
            reverseY.CheckedChanged += (o, e) => Run(() => RefreshMotionAxis("Tilt"));
            streamingCheck.CheckedChanged += (o, e) => { if (applying) return; permission.Reset(); Disconnect(); MarkDirty(); };
            language.DrawItem += DrawComboItem;
            language.Items.AddRange(UiText.LanguageNames);
            language.Items[0] = UiText.T("Automatic", "Automatisk");
            language.SelectedIndex = Array.IndexOf(UiText.LanguageCodes, UiText.Preference);
            language.SelectedIndexChanged += (o, e) => {
                if (applying || !loaded || language.SelectedIndex < 0)
                    return;
                Run(() => {
                    MarkDirty();
                    SaveCurrent();
                    ShowMessage(UiText.T("Restart the application to change language.", "Genstart programmet for at skifte sprog."));
                });
            };
            mainSplit.SplitterMoved += (o, e) => { if (!applying && loaded) { config.Layout.ControllerRatio = LayoutPolicy.Ratio(mainSplit.SplitterDistance / (double)Math.Max(1, mainSplit.ClientSize.Width)); MarkDirty(); } };
            mainSplit.SizeChanged += (o, e) => { if (loaded && !applying) ApplySplitter(); };
            body.SizeChanged += (o, e) => SizeSettingsColumn(settingsTabs.Visible);
            Move += (o, e) => MarkDirty();
            Resize += (o, e) => {
                if (WindowState != FormWindowState.Minimized)
                    wasMaximized = WindowState == FormWindowState.Maximized;
                if (WindowState == FormWindowState.Normal && !settingsTabs.Visible)
                    RemoveSettingsWidth();
                MarkDirty();
            };
            settingsTabs.SelectedIndexChanged += (o, e) => {
                if (settingsTabs.SelectedTab == presetsTab)
                    SelectPresetInSettings(SelectedPresetRow);
            };
            KeyPreview = true;
            KeyDown += (o, e) => {
                if (e.KeyCode != Keys.Escape)
                    return;
                wheel.CancelMovement();
                StopMotion();
            };
            Deactivate += (o, e) => { wheel.CancelMovement(); StopHeld(); };
            Link(repoLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio");
            Link(issuesLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues");
            Link(discussionLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/discussions");
            Link(coffeeLink, "https://buymeacoffee.com/brislingveo");
            versionLabel.Text = "MIT · " + DiagnosticLog.DisplayAppVersion;
            versionLabel.AccessibleDescription = DiagnosticLog.AppVersion;
            mainToolTips.SetToolTip(versionLabel, DiagnosticLog.AppVersion);
            FormClosing += (o, e) => {
                StopMotion();
                if (!loaded || RestartRequested)
                    return;
                try {
                    SaveCurrent();
                } catch (Exception ex) {
                    DiagnosticLog.Current.Write("ERROR", "Save profile on close", ex);
                    e.Cancel = ShowMessage(ex.Message + "\n" + UiText.T("Close without saving?", "Luk uden at gemme?"), Text, MessageBoxButtons.YesNo) != DialogResult.Yes;
                }
            };
            FormClosed += (o, e) => {
                camera?.Dispose();
                foreach (var d in devices)
                    d.Dispose();
                foreach (var image in iconImages.Values)
                    image.Dispose();
                foreach (var tab in videoTabs)
                    if (tab.Parent == null)
                        tab.Dispose();
                permanentFont?.Dispose();
            };
        }
        void Link(LinkLabel label, string url, string tooltipLabel = null, string tooltipDetail = null) {
            SetMainToolTip(label, tooltipLabel ?? label.Text, tooltipDetail);
            label.LinkClicked += (o, e) => Run(() => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }));
        }
        void ConfigureMainToolTips() {
            SetMainToolTip(temporaryButton, UiText.T("Add temporary preset", "Tilføj midlertidig position"));
            SetMainToolTip(deleteTemporaryButton, UiText.T("Delete temporary preset", "Slet midlertidig position"));
            SetMainToolTip(settingsButton, UiText.T("Settings", "Indstillinger"));
            SetMainToolTip(profileButton, UiText.T("Choose profile", "Vælg profil"));
            var wheelDescription = UiText.T("Inner ring for normal moves. Outer ring for fast moves.", "Inderste ring flytter normalt. Yderste ring flytter hurtigt.");
            var sourceDescription = UiText.T("Click to choose a camera. Drag to pan in that direction.", "Klik for at vælge kamera. Træk for at panorere i den retning.");
            SetMainToolTip(wheel, wheel.AccessibleName, wheelDescription + " " + sourceDescription);
            wheel.SourceHoverChanged += overSource => mainToolTips.SetToolTip(wheel, overSource
                ? sourceDescription : wheelDescription);
            SetMainToolTip(fullFrame, UiText.T("Restore full frame", "Gendan fuldt billede"),
                UiText.T("Use the widest view and minimum zoom.", "Brug det bredeste synsfelt og mindste zoom."));
            SetMainToolTip(zoomSlider, UiText.T("Zoom", "Zoom"),
                UiText.T("Drag to adjust zoom.", "Træk for at justere zoom."));
        }
        void SetMainToolTip(Control control, string label, string detail = null) {
            control.AccessibleName = label;
            control.AccessibleDescription = detail;
            mainToolTips.SetToolTip(control, detail == null ? label : label + "\n" + detail);
        }
        void WireZoom(Button button, int direction) {
            var title = direction > 0
                ? UiText.Translate("Preset icon: Zoom in", "Zoom in")
                : UiText.Translate("Preset icon: Zoom out", "Zoom out");
            SetMainToolTip(button, title);
            button.MouseDown += (o, e) => {
                if (e.Button == MouseButtons.Left)
                    Run(() => StartZoomHeld(direction));
            };
            button.MouseUp += (o, e) => StopHeld();
            button.MouseCaptureChanged += (o, e) => { if (!button.Capture) StopHeld(); };
            button.KeyDown += (o, e) => {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) {
                    e.SuppressKeyPress = true;
                    Run(() => Nudge("Zoom", direction, (int)zoomSteps.Value));
                }
            };
        }
        void SaveImageValue(string key) {
            if (!config.ImageValues.TryGetValue(CameraId, out var values))
                config.ImageValues[CameraId] = values = new Dictionary<string, ImageValue>();
            int flags;
            int value = camera.Get(key, out flags);
            values[key] = new ImageValue { Value = value, Automatic = (flags & 1) != 0 };
            MarkDirty();
        }
        void ApplyImageValues() {
            if (config.ImageValues == null)
                config.ImageValues = new Dictionary<string, Dictionary<string, ImageValue>>();
            if (!config.ImageValues.TryGetValue(CameraId, out var values))
                return;
            foreach (var p in values) {
                if (camera.Settings.TryGetValue(p.Key, out var setting) && !axes.Contains(p.Key) && (setting.Caps & (p.Value.Automatic ? 1 : 2)) != 0)
                    camera.Set(p.Key, p.Value.Value, p.Value.Automatic);
            }
        }
    }
}
