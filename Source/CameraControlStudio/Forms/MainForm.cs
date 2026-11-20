using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm : AppForm
    {
        readonly ProfileStore store = new ProfileStore();
        readonly SessionPresets temporary = new SessionPresets();
        readonly CameraAccessGate permission = new CameraAccessGate();
        readonly PresetActivity activity = new PresetActivity();
        readonly HashSet<string> failedMotionReads = new HashSet<string>();
        readonly Stopwatch clock = new Stopwatch();
        Font permanentFont;
        readonly Dictionary<string, Bitmap> iconImages = new Dictionary<string, Bitmap>();
        ProfileEntry entry;
        SavedData config;
        string profile = ProfileStore.Standard;
        Camera camera;
        List<Device> devices = new List<Device>();
        bool loaded;
        bool applying;
        bool dirty;
        bool wasMaximized;
        string heldAxis;
        int heldDirection;
        bool heldFast;
        bool refreshingPresets;
        Dictionary<string, int> start;
        Dictionary<string, int> target;
        Dictionary<string, int> last;
        double duration;
        static readonly string[] axes =
        {
            "Zoom",
            "Pan",
            "Tilt"
        };
        readonly Size preferredMinimumSize;
        readonly int settingsPanelWidth, temporaryDeleteWidth;
        int settingsWidthAdded;
        int iconColumnWidth, recallColumnWidth;
        internal bool RestartRequested { get; private set; }

        string CameraId
        {
            get
            {
                return config.CameraId ?? "";
            }
        }

        public MainForm()
        {
            InitializeComponent();
            if (InDesigner)
                return;
            preferredMinimumSize = MinimumSize;
            settingsPanelWidth = (int)body.ColumnStyles[1].Width;
            temporaryDeleteWidth = (int)presetHeader.ColumnStyles[1].Width;
            SetSettings(false, false);
            FormText.Translate(this);
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            ConfigureGrid();
            WireSettings();
            WireVideoControls();
            ConfigureMainToolTips();
            ConfigureSettingsToolTips();
            WireEvents();
            RefreshActionButtons();
            RefreshMotionControls();
        }

        internal MainForm(ProfileEntry initialEntry, string initialProfile) : this()
        {
            entry = initialEntry;
            profile = initialProfile;
        }

        void Run(Action action)
        {
            try
            {
                action();
            }
            catch (OperationCanceledException)
            {
                StopMotion();
            }
            catch (CameraUnavailableException ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Camera connection lost", ex);
                Disconnect();
                status.Text = UiText.T("Camera unavailable. Use a camera control to reconnect.",
                    "Kameraet er ikke tilgængeligt. Brug en kameraknap for at forbinde igen.");
            }
            catch (InvalidOperationException ex)
            {
                StopMotion();
                DiagnosticLog.Current.Write("WARNING", "Handle application action", ex);
                status.Text = ex.Message;
                ShowMessage(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                StopMotion();
                var log = DiagnosticLog.Current.Write("ERROR", "Handle application action", ex);
                status.Text = UiText.T("An unexpected error prevented this action.", "En uventet fejl forhindrede handlingen.");
                ErrorDialog.Show(this, log ?? DiagnosticLog.Current.FindLatest());
            }
        }

        void WireEvents()
        {
            Shown += (o, e) =>
            {
                if (documentationMode)
                    InitializeDocumentation();
                else
                    Run(InitializeProfile);
            };
            settingsButton.Click += (o, e) => Run(() =>
            {
                SetSettings(!settingsTabs.Visible, true);
                MarkDirty();
            });
            profileButton.Click += (o, e) => Run(ChooseProfile);
            wheel.MovementRequested += (axis, direction, fast) => Run(() => StartHeld(axis, direction, fast));
            wheel.Released += () => held.Stop();
            wheel.SourceRequested += () => Run(ChooseCamera);
            WireZoom(normalIn, 1);
            WireZoom(normalOut, -1);
            zoomSlider.PositionRequested += fraction => Run(() => SetZoomPosition(fraction));
            zoomSlider.StepRequested += direction => Run(() => Nudge("Zoom", direction, false));
            movement.Tick += (o, e) => Run(TickMotion);
            held.Tick += (o, e) => Run(() =>
            {
                if (!Nudge(heldAxis, heldDirection, heldFast))
                    held.Stop();
            });
            motionLimits.Tick += (o, e) => Run(() =>
            {
                try
                {
                    RefreshMotionControls();
                }
                catch (Exception ex)when (!(ex is CameraUnavailableException))
                {
                    Disconnect();
                    throw;
                }
            });
            saveTimer.Tick += (o, e) =>
            {
                saveTimer.Stop();
                if (loaded && dirty && ContainsFocus && !movement.Enabled && !held.Enabled)
                    Run(SaveCurrent);
                else if (dirty)
                    saveTimer.Start();
            };
            temporaryButton.Click += (o, e) => Run(AddTemporary);
            deleteTemporaryButton.Click += (o, e) => Run(DeleteTemporary);
            presetGrid.SelectionChanged += (o, e) =>
            {
                if (refreshingPresets)
                    return;
                SelectPresetInSettings(SelectedPresetRow);
                RefreshActionButtons();
            };
            permanentList.SelectedIndexChanged += (o, e) =>
            {
                if (!refreshingPresets)
                    RefreshActionButtons();
            };
            presetGrid.CellContentClick += (o, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == recallColumn.Index)
                    Run(() => Recall((RowItem)presetGrid.Rows[e.RowIndex].Tag));
            };
            presetGrid.CellDoubleClick += (o, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex != recallColumn.Index)
                    Run(() => Recall((RowItem)presetGrid.Rows[e.RowIndex].Tag));
            };
            presetGrid.KeyDown += (o, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
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
            profilesList.DoubleClick += (o, e) => Run(() =>
            {
                if (profilesList.SelectedItem != null)
                    ChooseProfile((string)profilesList.SelectedItem);
            });
            foreach (var control in new[]
            {
                panSteps,
                fastPanSteps,
                zoomSteps,
                seconds
            }

            )
                control.ValueChanged += (o, e) => MarkDirty();
            foreach (var control in new[]
            {
                reverseX,
                reverseY,
                alwaysTop
            }

            )
                control.CheckedChanged += (o, e) =>
                {
                    if (!applying)
                    {
                        TopMost = alwaysTop.Checked;
                        MarkDirty();
                    }
                };
            reverseX.CheckedChanged += (o, e) => Run(() => RefreshMotionAxis("Pan"));
            reverseY.CheckedChanged += (o, e) => Run(() => RefreshMotionAxis("Tilt"));
            streamingCheck.CheckedChanged += (o, e) =>
            {
                if (applying)
                    return;
                permission.Reset();
                Disconnect();
                MarkDirty();
            };
            language.DrawItem += DrawComboItem;
            language.Items.AddRange(UiText.LanguageNames);
            language.Items[0] = UiText.T("Automatic", "Automatisk");
            language.SelectedIndex = Array.IndexOf(UiText.LanguageCodes, UiText.Preference);
            language.SelectedIndexChanged += (o, e) =>
            {
                if (applying || !loaded || language.SelectedIndex < 0)
                    return;
                Run(() =>
                {
                    MarkDirty();
                    SaveCurrent();
                    ShowMessage(UiText.T("Restart the application to change language.", "Genstart programmet for at skifte sprog."));
                });
            };
            mainSplit.SplitterMoved += (o, e) =>
            {
                if (!applying && loaded)
                {
                    config.Layout.ControllerRatio = LayoutPolicy.Ratio(mainSplit.SplitterDistance / (double)Math.Max(1, mainSplit.ClientSize.Width));
                    MarkDirty();
                }
            };
            mainSplit.SizeChanged += (o, e) =>
            {
                if (loaded && !applying)
                    ApplySplitter();
            };
            body.SizeChanged += (o, e) => SizeSettingsColumn(settingsTabs.Visible);
            Move += (o, e) => MarkDirty();
            Resize += (o, e) =>
            {
                if (WindowState != FormWindowState.Minimized)
                    wasMaximized = WindowState == FormWindowState.Maximized;
                if (WindowState == FormWindowState.Normal && !settingsTabs.Visible)
                    RemoveSettingsWidth();
                MarkDirty();
            };
            settingsTabs.SelectedIndexChanged += (o, e) =>
            {
                if (settingsTabs.SelectedTab == presetsTab)
                    SelectPresetInSettings(SelectedPresetRow);
                MarkDirty();
            };
            KeyPreview = true;
            KeyDown += (o, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    StopMotion();
            };
            Deactivate += (o, e) =>
            {
                held.Stop();
            };
            Link(repoLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio");
            Link(issuesLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues");
            Link(discussionLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/discussions");
            Link(coffeeLink, "https://buymeacoffee.com/brislingveo");
            versionLabel.Text = "MIT · " + Application.ProductVersion;
            FormClosing += (o, e) =>
            {
                StopMotion();
                if (!loaded || RestartRequested)
                    return;
                try
                {
                    SaveCurrent();
                }
                catch (Exception ex)
                {
                    DiagnosticLog.Current.Write("ERROR", "Save profile on close", ex);
                    e.Cancel = ShowMessage(ex.Message + "\n" + UiText.T("Close without saving?", "Luk uden at gemme?"), Text, MessageBoxButtons.YesNo) != DialogResult.Yes;
                }
            };
            FormClosed += (o, e) =>
            {
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

        void Link(LinkLabel label, string url)
        {
            SetMainToolTip(label, label.Text);
            label.LinkClicked += (o, e) => Run(() => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }));
        }

        void ConfigureMainToolTips()
        {
            SetMainToolTip(temporaryButton, UiText.T("Add temporary preset", "Tilføj midlertidig position"));
            SetMainToolTip(deleteTemporaryButton, UiText.T("Delete temporary preset", "Slet midlertidig position"));
            SetMainToolTip(settingsButton, UiText.T("Settings", "Indstillinger"));
            SetMainToolTip(profileButton, UiText.T("Choose profile", "Vælg profil"));
            SetMainToolTip(wheel, wheel.AccessibleName);
            SetMainToolTip(zoomSlider, UiText.T("Zoom", "Zoom"),
                UiText.T("Drag and release to set zoom.", "Træk og slip for at indstille zoom."));
        }

        void SetMainToolTip(Control control, string label, string detail = null)
        {
            control.AccessibleName = label;
            control.AccessibleDescription = detail;
            mainToolTips.SetToolTip(control, detail == null ? label : label + "\n" + detail);
        }

        void WireZoom(Button button, int direction)
        {
            var title = direction > 0
                ? UiText.Translate("Preset icon: Zoom in", "Zoom in")
                : UiText.Translate("Preset icon: Zoom out", "Zoom out");
            SetMainToolTip(button, title);
            button.MouseDown += (o, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    Run(() => StartHeld("Zoom", direction, false));
            };
            button.MouseUp += (o, e) => held.Stop();
            button.MouseCaptureChanged += (o, e) =>
            {
                if (!button.Capture)
                    held.Stop();
            };
            button.KeyDown += (o, e) =>
            {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    Run(() => Nudge("Zoom", direction, false));
                }
            };
        }

    }
}
