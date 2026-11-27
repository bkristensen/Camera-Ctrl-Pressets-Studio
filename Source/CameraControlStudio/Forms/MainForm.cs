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
        readonly int temporaryDeleteWidth;
        readonly SettingsForm settings;
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
            settings = new SettingsForm();
            temporaryDeleteWidth = (int)presetHeader.ColumnStyles[1].Width;
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
        void Run(Action action) => RunAction(action, false);

        void RunSettings(Action action) => RunAction(action, true);

        Form CommandOwner => settings.Visible && ReferenceEquals(Form.ActiveForm, settings) ? (Form)settings : this;

        void RunAction(Action action, bool fromSettings) {
            try {
                action();
            } catch (OperationCanceledException) {
                RecoverAction(StopMotion);
            } catch (CameraUnavailableException ex) {
                DiagnosticLog.Current.Write("WARNING", "Camera connection lost", ex);
                RecoverAction(Disconnect);
                SetActionStatus(UiText.T("Camera unavailable. Use a camera control to reconnect.",
                    "Kameraet er ikke tilgængeligt. Brug en kameraknap for at forbinde igen."), fromSettings);
            } catch (InvalidOperationException ex) {
                RecoverAction(StopMotion);
                DiagnosticLog.Current.Write("WARNING", "Handle application action", ex);
                SetActionStatus(ex.Message, fromSettings);
                if (!fromSettings)
                    ShowMessage(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (Exception ex) {
                RecoverAction(StopMotion);
                var log = DiagnosticLog.Current.Write("ERROR", "Handle application action", ex);
                SetActionStatus(ActionFailedMessage(), fromSettings);
                if (!fromSettings)
                    ErrorDialog.Show(this, log ?? DiagnosticLog.Current.FindLatest());
            }
        }

        static string ActionFailedMessage() =>
            UiText.T("An unexpected error prevented this action.", "En uventet fejl forhindrede handlingen.");

        void SetActionStatus(string message, bool fromSettings)
        {
            status.Text = message;
            if (fromSettings && !settings.IsDisposed)
                settings.SetStatus(message);
        }

        static void RecoverAction(Action reset)
        {
            try { reset(); }
            catch (Exception error) { DiagnosticLog.Current.Write("ERROR", "Recover from action failure", error); }
        }

        internal void RecoverUiError(Exception error)
        {
            DiagnosticLog.Current.Write("ERROR", "Unhandled UI action", error);
            RecoverAction(StopMotion);
            SetActionStatus(ActionFailedMessage(), ReferenceEquals(CommandOwner, settings));
        }

        void WireEvents() {
            Shown += (o, e) => {
                if (documentationMode)
                    InitializeDocumentation();
                else
                    Run(InitializeProfile);
            };
            settingsButton.Click += (o, e) => Run(() => settings.ShowFor(this));
            profileButton.Click += (o, e) => Run(ChooseProfile);
            wheel.MovementRequested += (pan, tilt, speed) => Run(() => StartPanHeld(pan, tilt, speed));
            wheel.DragRequested += (pan, tilt) => Run(() => UpdatePanDrag(pan, tilt));
            wheel.Released += StopHeld;
            wheel.SourceRequested += () => Run(ChooseCamera);
            WireZoom(normalIn, 1);
            WireZoom(normalOut, -1);
            zoomSlider.PositionRequested += fraction => Run(() => SetZoomPosition(fraction));
            zoomSlider.StepRequested += direction => Run(() => Nudge("Zoom", direction, (int)settings.zoomSteps.Value));
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
                if (loaded && dirty && (ContainsFocus || settings.ContainsFocus) && !movement.Enabled && !held.Enabled)
                    RunAction(SaveCurrent, settings.ContainsFocus);
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
            settings.permanentList.SelectedIndexChanged += (o, e) => RunSettings(() => {
                if (!refreshingPresets)
                    RefreshActionButtons();
            });
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
            settings.addPermanent.Click += (o, e) => RunSettings(AddPermanent);
            settings.editPermanent.Click += (o, e) => RunSettings(EditPermanent);
            settings.updatePermanent.Click += (o, e) => RunSettings(UpdatePermanent);
            settings.deletePermanent.Click += (o, e) => RunSettings(DeletePermanent);
            settings.movePermanentUp.Click += (o, e) => RunSettings(() => MovePermanent(-1));
            settings.movePermanentDown.Click += (o, e) => RunSettings(() => MovePermanent(1));
            settings.newProfile.Click += (o, e) => RunSettings(() => CreateProfile(false));
            settings.copyProfile.Click += (o, e) => RunSettings(() => CreateProfile(true));
            settings.renameProfile.Click += (o, e) => RunSettings(RenameProfile);
            settings.profilesList.SelectedIndexChanged += (o, e) => RunSettings(RefreshActionButtons);
            settings.profilesList.DoubleClick += (o, e) => RunSettings(() => { if (settings.profilesList.SelectedItem != null) ChooseProfile((string)settings.profilesList.SelectedItem); });
            foreach (var control in new[] { settings.finePanPercent, settings.panSteps, settings.fastPanSteps, settings.zoomSteps, settings.seconds, settings.transitionSpeedPercent })
                control.ValueChanged += (o, e) => MarkDirty();
            settings.useTransitionSpeed.CheckedChanged += (o, e) => RunSettings(() => {
                UpdateTransitionMode();
                MarkDirty();
            });
            foreach (var control in new[] { settings.reverseX, settings.reverseY, settings.alwaysTop })
                control.CheckedChanged += (o, e) => { if (!applying) { TopMost = settings.alwaysTop.Checked; MarkDirty(); } };
            settings.reverseX.CheckedChanged += (o, e) => RunSettings(() => {
                blockedFieldOfViewSteps.Clear();
                RefreshMotionAxis("Pan");
            });
            settings.reverseY.CheckedChanged += (o, e) => RunSettings(() => RefreshMotionAxis("Tilt"));
            settings.streamingCheck.CheckedChanged += (o, e) => RunSettings(() => { if (applying) return; permission.Reset(); Disconnect(); MarkDirty(); });
            settings.language.DrawItem += DrawComboItem;
            settings.language.Items.AddRange(UiText.LanguageNames);
            settings.language.Items[0] = UiText.T("Automatic", "Automatisk");
            settings.language.SelectedIndex = Array.IndexOf(UiText.LanguageCodes, UiText.Preference);
            settings.language.SelectedIndexChanged += (o, e) => {
                if (applying || !loaded || settings.language.SelectedIndex < 0)
                    return;
                RunSettings(() => {
                    MarkDirty();
                    SaveCurrent();
                    MessageBox.Show(settings, UiText.T("Restart the application to change language.",
                        "Genstart programmet for at skifte sprog."), settings.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageDirection);
                });
            };
            mainSplit.SplitterMoved += (o, e) => { if (!applying && loaded) { config.Layout.ControllerRatio = LayoutPolicy.Ratio(mainSplit.SplitterDistance / (double)Math.Max(1, mainSplit.ClientSize.Width)); MarkDirty(); } };
            mainSplit.SizeChanged += (o, e) => { if (loaded && !applying) ApplySplitter(); };
            settings.Move += (o, e) => MarkDirty();
            settings.Resize += (o, e) => MarkDirty();
            settings.VisibleChanged += (o, e) => MarkDirty();
            Move += (o, e) => MarkDirty();
            Resize += (o, e) => {
                if (WindowState != FormWindowState.Minimized)
                    wasMaximized = WindowState == FormWindowState.Maximized;
                MarkDirty();
            };
            settings.settingsTabs.SelectedIndexChanged += (o, e) => RunSettings(() => {
                if (settings.settingsTabs.SelectedTab == settings.presetsTab)
                    SelectPresetInSettings(SelectedPresetRow);
            });
            KeyPreview = true;
            KeyDown += (o, e) => {
                if (e.KeyCode != Keys.Escape)
                    return;
                wheel.CancelMovement();
                RecoverAction(StopMotion);
            };
            Deactivate += (o, e) => { wheel.CancelMovement(); StopHeld(); };
            Link(settings.coffeeLink, "https://buymeacoffee.com/brislingveo");
            settings.versionLabel.Text = "MIT · " + DiagnosticLog.DisplayAppVersion;
            settings.versionLabel.AccessibleDescription = DiagnosticLog.AppVersion;
            mainToolTips.SetToolTip(settings.versionLabel, DiagnosticLog.AppVersion);
            FormClosing += (o, e) => {
                RecoverAction(StopMotion);
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
                RecoverAction(() => camera?.Dispose());
                foreach (var d in devices)
                    RecoverAction(d.Dispose);
                foreach (var image in iconImages.Values)
                    image.Dispose();
                foreach (var tab in videoTabs)
                    if (tab.Parent == null)
                        tab.Dispose();
                permanentFont?.Dispose();
            };
            Disposed += (o, e) => settings.Dispose();
        }
        void Link(LinkLabel label, string url, string tooltipLabel = null, string tooltipDetail = null) {
            SetMainToolTip(label, tooltipLabel ?? label.Text, tooltipDetail);
            label.LinkClicked += (o, e) => RunAction(() => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }),
                ReferenceEquals(label.FindForm(), settings));
        }
        void ConfigureMainToolTips() {
            SetMainToolTip(temporaryButton, UiText.T("Add temporary preset", "Tilføj midlertidig position"));
            SetMainToolTip(deleteTemporaryButton, UiText.T("Delete temporary preset", "Slet midlertidig position"));
            SetMainToolTip(settingsButton, UiText.T("Settings", "Indstillinger"));
            SetMainToolTip(profileButton, UiText.T("Choose profile", "Vælg profil"));
            UpdateWheelToolTip();
            wheel.SourceHoverChanged += overSource => UpdateWheelToolTip(overSource);
            SetMainToolTip(fullFrame, UiText.T("Restore full frame", "Gendan fuldt billede"),
                UiText.T("Use the widest view and minimum zoom.", "Brug det bredeste synsfelt og mindste zoom."));
            SetMainToolTip(zoomSlider, UiText.T("Zoom", "Zoom"),
                UiText.T("Drag to adjust zoom.", "Træk for at justere zoom."));
        }
        void UpdateWheelToolTip(bool? overSource = null) {
            var wheelDescription = UiText.T("Inner ring for normal moves. Outer ring for fast moves.", "Inderste ring flytter normalt. Yderste ring flytter hurtigt.");
            var sourceDescription = wheel.DoubleClickForCameraSource
                ? UiText.T("Double-click to choose a camera.", "Dobbeltklik for at vælge kamera.")
                : UiText.T("Click to choose a camera.", "Klik for at vælge kamera.");
            if (wheel.PanWithCameraButton)
                sourceDescription += " " + UiText.T("Drag to pan and tilt.", "Træk for at panorere og vippe.");
            if (overSource.HasValue)
                mainToolTips.SetToolTip(wheel, overSource.Value ? sourceDescription : wheelDescription);
            else
                SetMainToolTip(wheel, wheel.AccessibleName, wheelDescription + " " + sourceDescription);
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
                    Run(() => Nudge("Zoom", direction, (int)settings.zoomSteps.Value));
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
