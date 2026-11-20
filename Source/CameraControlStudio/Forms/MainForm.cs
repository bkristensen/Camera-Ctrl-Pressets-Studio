using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        bool loaded, applying, dirty, wasMaximized;
        string heldAxis;
        int heldDirection;
        bool heldFast;
        bool refreshingPresets;
        Dictionary<string, int> start, target, last;
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

        sealed class RowItem
        {
            public Preset Preset;
            public bool Temporary;
            public string DeviceId;
        }

        RowItem SelectedPresetRow => presetGrid.SelectedRows.Count == 0
            ? null : presetGrid.SelectedRows[0].Tag as RowItem;

        string CameraId
        {
            get
            {
                return config.CameraId ?? "";
            }
        }

        List<Preset> Permanent
        {
            get
            {
                if (!config.Cameras.TryGetValue(CameraId, out var list))
                    config.Cameras[CameraId] = list = new List<Preset>();
                return list;
            }
        }

        List<Preset> Temporary
        {
            get
            {
                return temporary.For(profile, CameraId);
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
            presetGrid.CellFormatting += (o, e) =>
            {
                if (e.RowIndex < 0)
                    return;
                var row = (RowItem)presetGrid.Rows[e.RowIndex].Tag;
                if (row == null)
                    return;
                var c = row.Preset.Name == activity.Name ? (activity.Phase == PresetPhase.Moving ? Color.Gold : activity.Phase == PresetPhase.Reached ? Color.MediumSeaGreen : BlueTheme.Panel) : BlueTheme.Panel;
                e.CellStyle.BackColor = c;
                e.CellStyle.SelectionBackColor = c == BlueTheme.Panel ? Color.FromArgb(15, 90, 160) : c;
                e.CellStyle.ForeColor = e.CellStyle.SelectionForeColor = c == BlueTheme.Panel ? Color.White : Color.Black;
            };
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

        void InitializeProfile()
        {
            if (entry == null)
            {
                var catalog = store.Read();
                profile = ProfileStore.ReadSelectedProfile(catalog);
                entry = catalog.Profiles[profile];
            }

            config = entry.Config;
            loaded = true;
            ApplyProfile(settingsOpen: false);
            RefreshProfiles();
            if (!config.RequireStreamingConfirmation)
                NeedCamera();
        }

        void MarkDirty()
        {
            if (!loaded || applying)
                return;
            dirty = true;
            saveTimer.Stop();
            saveTimer.Start();
        }

        void ReadSettings()
        {
            config.PanSteps = (int)panSteps.Value;
            config.FastPanSteps = (int)fastPanSteps.Value;
            config.ZoomSteps = (int)zoomSteps.Value;
            config.Seconds = seconds.Value;
            config.ReverseHorizontal = reverseX.Checked;
            config.ReverseVertical = reverseY.Checked;
            config.RequireStreamingConfirmation = streamingCheck.Checked;
            config.AlwaysOnTop = alwaysTop.Checked;
            config.Language = UiText.LanguageCodes[language.SelectedIndex];
            config.Layout.SettingsOpen = false;
            config.Layout.SettingsTab = Array.IndexOf(savedTabOrder, settingsTabs.SelectedTab);
            ProfileWindow.Capture(this, config.Layout, wasMaximized, settingsWidthAdded);
        }

        void SaveCurrent()
        {
            if (!loaded)
                return;
            saveTimer.Stop();
            ReadSettings();
            entry = store.Save(profile, config, entry.Revision);
            config = entry.Config;
            dirty = false;
            RefreshPresets();
        }

        void ApplyProfile(bool settingsOpen)
        {
            applying = true;
            try
            {
                Disconnect();
                DiagnosticLog.Current.CameraName = null;
                permission.Reset();
                panSteps.Value = config.PanSteps;
                fastPanSteps.Value = config.FastPanSteps;
                zoomSteps.Value = config.ZoomSteps;
                seconds.Value = config.Seconds;
                reverseX.Checked = config.ReverseHorizontal;
                reverseY.Checked = config.ReverseVertical;
                streamingCheck.Checked = config.RequireStreamingConfirmation;
                alwaysTop.Checked = config.AlwaysOnTop;
                TopMost = config.AlwaysOnTop;
                if (config.Layout.SettingsOpen && config.Layout.HasBounds)
                    config.Layout.Width = ProfileWindow.WidthWithoutSettings(config.Layout.Width, settingsPanelWidth, MinimumSize.Width);
                config.Layout.SettingsOpen = false;
                SetSettings(settingsOpen, false);
                settingsTabs.SelectedTab = savedTabOrder[Math.Max(0, Math.Min(savedTabOrder.Length - 1, config.Layout.SettingsTab))];
                ProfileWindow.Apply(this, config.Layout, preferredMinimumSize);
                ApplySplitter();
                profileButton.Text = profile + " ▾";
                RefreshPresets();
            }
            finally
            {
                applying = false;
            }
        }

        void ApplySplitter()
        {
            if (config == null)
                return;
            bool before = applying;
            applying = true;
            try
            {
                int usable = mainSplit.ClientSize.Width - mainSplit.SplitterWidth;
                if (usable < mainSplit.Panel1MinSize + mainSplit.Panel2MinSize)
                    return;
                mainSplit.SplitterDistance = Math.Max(mainSplit.Panel1MinSize, Math.Min(usable - mainSplit.Panel2MinSize, (int)(mainSplit.ClientSize.Width * config.Layout.ControllerRatio)));
            }
            finally
            {
                applying = before;
            }
        }

        void SetSettings(bool visible, bool resize)
        {
            bool was = settingsTabs.Visible;
            double ratio = config?.Layout.ControllerRatio ?? LayoutPolicy.DefaultControllerRatio;
            body.SuspendLayout();
            try
            {
                settingsTabs.Visible = visible;
                if (visible && !was && settingsTabs.SelectedTab == presetsTab)
                    SelectPresetInSettings(SelectedPresetRow);
                if (resize && was != visible && WindowState == FormWindowState.Normal)
                {
                    if (visible)
                    {
                        var area = Screen.FromControl(this).WorkingArea;
                        int expandedWidth = Math.Min(area.Width, Width + settingsPanelWidth);
                        settingsWidthAdded = Math.Max(0, expandedWidth - Width);
                        if (settingsWidthAdded > 0)
                            SetBounds(Math.Min(Left, area.Right - expandedWidth), Top, expandedWidth, Height);
                    }
                    else
                        RemoveSettingsWidth();
                }

                SizeSettingsColumn(visible);
            }
            finally
            {
                body.ResumeLayout();
            }

            if (config != null)
                config.Layout.ControllerRatio = ratio;
            ApplySplitter();
        }

        void RemoveSettingsWidth()
        {
            if (settingsWidthAdded == 0)
                return;
            int closedWidth = ProfileWindow.WidthWithoutSettings(Width, settingsWidthAdded, MinimumSize.Width);
            settingsWidthAdded = 0;
            Width = closedWidth;
        }

        void SizeSettingsColumn(bool visible)
        {
            int mainMinimum = mainSplit.Panel1MinSize + mainSplit.Panel2MinSize + mainSplit.SplitterWidth;
            int width = visible ? Math.Min(settingsPanelWidth, Math.Max(0, body.ClientSize.Width - mainMinimum)) : 0;
            if (body.ColumnStyles[1].Width != width)
                body.ColumnStyles[1].Width = width;
        }

        void RefreshProfiles(string selection = null)
        {
            profilesList.Items.Clear();
            profilesList.Items.AddRange(store.Read().Profiles.Keys.OrderBy(x => x == ProfileStore.Standard ? 0 : 1).ThenBy(x => x).ToArray());
            profilesList.SelectedItem = selection ?? profile;
            RefreshActionButtons();
        }

        void ChooseProfile()
        {
            ChooseProfile(profile);
        }

        void ChooseProfile(string selected)
        {
            if (!loaded)
                return;
            StopMotion();
            using (var dialog = new ProfilePickerForm(store.Read().Profiles.Keys, selected, config.KeepLayoutOnSwitch))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                config.KeepLayoutOnSwitch = dialog.KeepLayout;
                SaveCurrent();
                if (dialog.Selected == profile)
                    return;
                SwitchProfile(dialog.Selected, dialog.KeepLayout);
            }
        }

        void SwitchProfile(string next, bool keep)
        {
            if (keep)
            {
                var destination = store.Read().Profiles[next];
                destination.Config.Layout = LayoutPolicy.OnSwitch(config.Layout, destination.Config.Layout, true);
                store.Save(next, destination.Config, destination.Revision);
            }

            ProfileStore.SelectProfile(next);
            RestartRequested = true;
            Close();
        }

        void CreateProfile(bool copy)
        {
            SaveCurrent();
            using (var dialog = new TextEntryForm(UiText.T("New profile", "Ny profil"), ""))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                store.Create(dialog.Value, copy ? profile : null);
                SwitchProfile(dialog.Value, false);
            }
        }

        void RenameProfile()
        {
            var selected = profilesList.SelectedItem as string;
            if (!loaded || selected == null || selected == ProfileStore.Standard)
                return;
            bool renamingActive = selected == profile;
            if (renamingActive)
                SaveCurrent();
            long revision = renamingActive ? entry.Revision : store.Read().Profiles[selected].Revision;
            using (var dialog = new TextEntryForm(UiText.T("Rename profile", "Omdøb profil"), selected))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Value == selected)
                    return;
                string name = dialog.Value;
                var catalog = store.Rename(selected, name, revision);
                temporary.RenameProfile(selected, name);
                if (renamingActive)
                {
                    profile = name;
                    entry = catalog.Profiles[name];
                    config = entry.Config;
                    ProfileStore.SelectProfile(name);
                    profileButton.Text = name + " ▾";
                }

                RefreshProfiles(name);
            }
        }

        void AllowCamera()
        {
            if (!loaded)
                throw new InvalidOperationException(UiText.T("Load a profile first.", "Indlæs først en profil."));
            if (!permission.Allow(streamingCheck.Checked, () => ShowMessage(UiText.T("Has streaming started? Choose Yes to enable camera controls.", "Er streaming startet? Vælg Ja for at aktivere kamerastyringen."), UiText.T("Confirm streaming", "Bekræft streaming"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                throw new OperationCanceledException();
        }

        void EnumerateDevices()
        {
            AllowCamera();
            Disconnect();
            foreach (var d in devices)
                d.Dispose();
            devices.Clear();
            devices = Camera.Enumerate();
        }

        void NeedCamera()
        {
            AllowCamera();
            if (camera != null)
                return;
            EnumerateDevices();
            var d = devices.FirstOrDefault(x => x.Id == CameraId);
            if (d == null && !string.IsNullOrEmpty(CameraId))
                throw new InvalidOperationException(UiText.T("The saved camera is unavailable. Select another source using the center button.", "Det gemte kamera er ikke tilgængeligt. Vælg en anden kilde med midterknappen."));
            d = d ?? devices.FirstOrDefault();
            if (d == null)
                throw new InvalidOperationException(UiText.T("No camera found.", "Intet kamera fundet."));
            Connect(d);
        }

        void Connect(Device device)
        {
            AllowCamera();
            StopMotion();
            camera?.Dispose();
            camera = null;
            DiagnosticLog.Current.CameraName = device.Name;
            camera = new Camera(device);
            if (CameraId != device.Id)
                ClearPresetSelection();
            config.CameraId = device.Id;
            RefreshMotionControls();
            cameraStatus.Text = UiText.T("Camera", "Kamera") + ": " + device.Name;
            ApplyImageValues();
            BuildImageControls();
            RefreshPresets();
            MarkDirty();
            status.Text = UiText.T("Ready", "Klar");
            motionLimits.Start();
        }

        void ChooseCamera()
        {
            StopMotion();
            EnumerateDevices();
            using (var dialog = new CameraSourceForm(devices, CameraId))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    Connect(dialog.Selected);
            }
        }

        void Disconnect()
        {
            StopMotion();
            motionLimits.Stop();
            failedMotionReads.Clear();
            var disconnectedCamera = camera;
            camera = null;
            try
            {
                disconnectedCamera?.Dispose();
            }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Release camera connection", ex);
            }

            RefreshMotionControls();
            RefreshActionButtons();
            ShowVideoControls(false);
            cameraStatus.Text = UiText.T("Camera: not connected", "Kamera: ikke tilsluttet");
        }

        void ConfigureGrid()
        {
            permanentFont = new Font(presetGrid.Font, FontStyle.Bold);
            iconColumnWidth = iconColumn.Width;
            recallColumnWidth = recallColumn.Width;
            presetGrid.CellPainting += PaintRecallButton;
            presetGrid.SizeChanged += (sender, e) => SizePresetColumns();
            SizePresetColumns();
        }

        void SizePresetColumns()
        {
            bool compact = presetGrid.ClientSize.Width < 190;
            iconColumn.Width = compact ? Math.Min(34, iconColumnWidth) : iconColumnWidth;
            recallColumn.Width = compact ? Math.Min(30, recallColumnWidth) : recallColumnWidth;
        }

        void PaintRecallButton(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != recallColumn.Index)
                return;
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.SelectionBackground);
            bool selected = (e.State & DataGridViewElementStates.Selected) != 0;
            var color = selected ? e.CellStyle.SelectionForeColor : e.CellStyle.ForeColor;
            var button = Rectangle.Inflate(e.CellBounds, -5, -6);
            button.Width--;
            button.Height--;
            using (var border = new Pen(Color.FromArgb(170, color)))
                e.Graphics.DrawRectangle(border, button);
            TextRenderer.DrawText(e.Graphics, recallColumn.Text, presetGrid.Font, button, color,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);
            e.Handled = true;
        }

        Bitmap IconImage(string id)
        {
            id = PresetIcons.ResolveId(id);
            if (!iconImages.TryGetValue(id, out var image))
            {
                image = new Bitmap(64, 48);
                using (var g = Graphics.FromImage(image))
                    PresetIcons.Draw(g, id, new Rectangle(2, 2, 60, 44));
                iconImages[id] = image;
            }

            return image;
        }

        void RefreshPresets()
        {
            if (config == null)
                return;
            var selected = SelectedPresetRow;
            string edited = (permanentList.SelectedItem as Preset)?.Name;
            refreshingPresets = true;
            try
            {
                presetGrid.Rows.Clear();
                permanentList.Items.Clear();
                DataGridViewRow selectedRow = null;
                string recall = UiText.T("Recall preset", "Kør til position");
                foreach (var pair in Permanent.Select(x => new RowItem { Preset = x, Temporary = false, DeviceId = CameraId }).Concat(Temporary.Select(x => new RowItem { Preset = x, Temporary = true, DeviceId = CameraId })))
                {
                    int n = presetGrid.Rows.Add();
                    var row = presetGrid.Rows[n];
                    row.Cells[iconColumn.Index].Value = pair.Temporary ? null : IconImage(pair.Preset.IconId);
                    row.Cells[positionColumn.Index].Value = pair.Preset.Name;
                    row.Tag = pair;
                    row.Cells[recallColumn.Index].ToolTipText = recall;
                    row.DefaultCellStyle.Font = pair.Temporary ? presetGrid.Font : permanentFont;
                    if (selected != null && pair.DeviceId == selected.DeviceId
                        && pair.Temporary == selected.Temporary && pair.Preset.Name == selected.Preset.Name)
                        selectedRow = row;
                }

                permanentList.Items.AddRange(Permanent.ToArray());
                SelectPermanentPreset(edited);
                presetGrid.CurrentCell = selectedRow?.Cells[positionColumn.Index];
                presetGrid.ClearSelection();
                if (selectedRow != null)
                    selectedRow.Selected = true;
            }
            finally
            {
                refreshingPresets = false;
            }

            RefreshActionButtons();
        }

        void ClearPresetSelection()
        {
            presetGrid.CurrentCell = null;
            presetGrid.ClearSelection();
            permanentList.ClearSelected();
        }

        void SelectPresetInSettings(RowItem row)
        {
            if (row != null)
                SelectPermanentPreset(row.Temporary ? null : row.Preset.Name);
        }

        void SelectPermanentPreset(string name)
        {
            permanentList.SelectedItem = name == null ? null
                : permanentList.Items.Cast<Preset>().FirstOrDefault(p => p.Name == name);
        }

        void RefreshActionButtons()
        {
            bool presetSelected = permanentList.SelectedItem is Preset;
            // A disconnected camera can still connect on demand when a capture action is used.
            bool canCapture = loaded && (camera == null || axes.Any(axis => HasManualAxis(axis, out _)));
            addPermanent.Enabled = temporaryButton.Enabled = canCapture;
            editPermanent.Enabled = deletePermanent.Enabled = presetSelected;
            updatePermanent.Enabled = presetSelected && canCapture;
            movePermanentUp.Enabled = loaded && permanentList.SelectedIndex > 0;
            movePermanentDown.Enabled = loaded && presetSelected && permanentList.SelectedIndex < permanentList.Items.Count - 1;
            bool temporarySelected = SelectedPresetRow?.Temporary == true;
            deleteTemporaryButton.Enabled = deleteTemporaryButton.Visible = temporarySelected;
            presetHeader.ColumnStyles[1].Width = temporarySelected ? temporaryDeleteWidth : 0;
            profileButton.Enabled = newProfile.Enabled = copyProfile.Enabled = loaded;
            renameProfile.Enabled = loaded && profilesList.SelectedItem is string selectedProfile
                && selectedProfile != ProfileStore.Standard;
        }

        bool NameExists(string name, Preset except = null)
        {
            return Permanent.Concat(Temporary).Any(p => p != except && string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        Dictionary<string, int> ReadPose()
        {
            NeedCamera();
            var pose = new Dictionary<string, int>();
            foreach (var key in axes)
                if (camera.Settings.TryGetValue(key, out var setting) && (setting.Caps & 2) != 0)
                    pose[key] = camera.Get(key);
            if (pose.Count == 0)
                throw new InvalidOperationException(UiText.T("The driver does not provide position or zoom controls.", "Driveren stiller ikke position eller zoom til rådighed."));
            return pose;
        }

        void AddTemporary()
        {
            NeedCamera();
            using (var dialog = new TextEntryForm(UiText.T("Temporary preset name", "Navn på midlertidig position"), ""))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                if (NameExists(dialog.Value))
                    throw new InvalidOperationException(UiText.T("Name already exists.", "Navnet findes allerede."));
                Temporary.Add(new Preset { Name = dialog.Value, Position = ReadPose() });
                RefreshPresets();
            }
        }

        void DeleteTemporary()
        {
            var row = SelectedPresetRow;
            if (row == null || !row.Temporary)
                return;
            Temporary.Remove(row.Preset);
            if (activity.Name == row.Preset.Name)
                StopMotion();
            RefreshPresets();
        }

        void AddPermanent()
        {
            NeedCamera();
            using (var dialog = new PresetEditorForm(null, x => NameExists(x)))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                var p = new Preset
                {
                    Name = dialog.PresetName,
                    IconId = dialog.IconId,
                    Position = ReadPose()
                };
                Permanent.Add(p);
                try
                {
                    SaveCurrent();
                }
                catch
                {
                    Permanent.Remove(p);
                    throw;
                }

                SelectPermanentPreset(p.Name);
            }
        }

        void EditPermanent()
        {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            using (var dialog = new PresetEditorForm(p, x => NameExists(x, p)))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                string old = p.Name, icon = p.IconId;
                p.Name = dialog.PresetName;
                p.IconId = dialog.IconId;
                try
                {
                    SaveCurrent();
                }
                catch
                {
                    p.Name = old;
                    p.IconId = icon;
                    throw;
                }

                if (activity.Name == old)
                    StopMotion();
            }
        }

        void UpdatePermanent()
        {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            NeedCamera();
            if (ShowMessage(UiText.T("Replace with current position?", "Erstat med aktuel position?"), Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            var old = p.Position;
            p.Position = ReadPose();
            try
            {
                SaveCurrent();
            }
            catch
            {
                p.Position = old;
                throw;
            }

            StopMotion();
        }

        void DeletePermanent()
        {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            string confirmation = string.Format(UiText.T("Delete permanent preset: {0}?", "Slet den permanente position »{0}«?"), p.Name);
            if (ShowMessage(confirmation, Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            int i = Permanent.IndexOf(p);
            Permanent.RemoveAt(i);
            try
            {
                SaveCurrent();
            }
            catch
            {
                Permanent.Insert(i, p);
                throw;
            }

            StopMotion();
        }

        void MovePermanent(int direction)
        {
            if (!loaded || !(permanentList.SelectedItem is Preset selected))
                return;
            var presets = Permanent;
            int from = presets.IndexOf(selected);
            int to = from + direction;
            if (from < 0 || to < 0 || to >= presets.Count)
                return;
            var neighbor = presets[to];
            presets[from] = neighbor;
            presets[to] = selected;
            try
            {
                SaveCurrent();
            }
            catch
            {
                presets[from] = selected;
                presets[to] = neighbor;
                throw;
            }
        }

        void StopMotion()
        {
            movement.Stop();
            held.Stop();
            clock.Stop();
            activity.Clear();
            presetGrid.Invalidate();
        }

        void StartHeld(string axis, int direction, bool fast)
        {
            held.Stop();
            if (!Nudge(axis, direction, fast))
                return;
            heldAxis = axis;
            heldDirection = direction;
            heldFast = fast;
            if ((Control.MouseButtons & MouseButtons.Left) != 0)
                held.Start();
        }

        bool HasManualAxis(string axis, out Setting setting)
        {
            setting = null;
            return camera != null && camera.Settings.TryGetValue(axis, out setting) && (setting.Caps & 2) != 0;
        }

        bool AxisReversed(string axis) => axis == "Pan" ? reverseX.Checked : axis == "Tilt" && reverseY.Checked;
        void RefreshMotionControls()
        {
            foreach (var axis in axes)
                RefreshMotionAxis(axis);
        }

        void RefreshMotionAxis(string axis)
        {
            if (HasManualAxis(axis, out var setting))
            {
                try
                {
                    UpdateMotionAxis(axis, setting, camera.Get(axis));
                    failedMotionReads.Remove(axis);
                    return;
                }
                catch (COMException ex)when (!(ex is CameraUnavailableException))
                {
                    if (failedMotionReads.Add(axis))
                        DiagnosticLog.Current.Write("WARNING", "Read camera position: " + axis, ex);
                }
            }

            bool canConnect = loaded && camera == null;
            if (axis == "Zoom")
            {
                zoomSlider.Enabled = false;
                if (camera == null)
                    zoomSlider.Position = .5;
            }

            SetMotionAvailability(axis, canConnect, canConnect);
        }

        void UpdateMotionAxis(string axis, Setting setting, int position)
        {
            bool reverse = AxisReversed(axis);
            bool negative = ManualMotion.CanMove(setting, position, -1, reverse);
            bool positive = ManualMotion.CanMove(setting, position, 1, reverse);
            if (axis == "Zoom")
            {
                zoomSlider.Position = ManualMotion.ZoomFraction(setting, position);
                zoomSlider.Enabled = negative || positive;
            }

            SetMotionAvailability(axis, negative, positive);
        }

        void SetMotionAvailability(string axis, bool negative, bool positive)
        {
            if (axis == "Zoom")
            {
                normalOut.Enabled = negative;
                normalIn.Enabled = positive;
            }
            else
            {
                wheel.SetDirectionAvailability(axis, negative, positive);
            }
        }

        bool Nudge(string axis, int direction, bool fast)
        {
            NeedCamera();
            if (!HasManualAxis(axis, out var setting))
                throw new InvalidOperationException(UiText.T("This camera control is not supported.", "Denne kamerafunktion understøttes ikke."));
            int position = camera.Get(axis);
            bool reverse = AxisReversed(axis);
            if (!ManualMotion.CanMove(setting, position, direction, reverse))
            {
                UpdateMotionAxis(axis, setting, position);
                return false;
            }

            int steps = (int)(axis == "Zoom" ? zoomSteps.Value : (fast ? fastPanSteps.Value : panSteps.Value));
            SetManualPosition(axis, ManualMotion.Target(setting, position, direction, steps, reverse));
            position = camera.Get(axis);
            UpdateMotionAxis(axis, setting, position);
            return ManualMotion.CanMove(setting, position, direction, reverse);
        }

        void SetZoomPosition(double fraction)
        {
            if (!HasManualAxis("Zoom", out var setting))
                return;
            int position = camera.Get("Zoom");
            int desired = ManualMotion.ZoomAtFraction(setting, fraction);
            if (position != desired)
            {
                held.Stop();
                SetManualPosition("Zoom", desired);
                position = camera.Get("Zoom");
            }

            UpdateMotionAxis("Zoom", setting, position);
        }

        void SetManualPosition(string axis, int position)
        {
            movement.Stop();
            clock.Stop();
            activity.Clear();
            presetGrid.Invalidate();
            camera.Set(axis, position);
            status.Text = UiText.T("Manual movement", "Manuel bevægelse");
        }

        void Recall(RowItem row)
        {
            if (row == null)
                return;
            SelectPresetInSettings(row);
            StopMotion();
            NeedCamera();
            if (row.DeviceId != CameraId)
                throw new InvalidOperationException(UiText.T("Select a preset for this camera.", "Vælg en position til dette kamera."));
            start = ReadPose();
            target = new Dictionary<string, int>();
            last = new Dictionary<string, int>(start);
            foreach (var pair in row.Preset.Position)
            {
                if (!start.ContainsKey(pair.Key) || camera.Settings[pair.Key].Snap(pair.Value) != pair.Value)
                    throw new InvalidOperationException(UiText.T("The camera range has changed. Save this preset again.", "Kameraets område er ændret. Gem positionen igen."));
                target[pair.Key] = pair.Value;
            }

            if (target.Count == 0)
                throw new InvalidOperationException(UiText.T("Empty preset.", "Positionen er tom."));
            activity.Begin(row.Preset.Name);
            status.Text = UiText.T("Moving to position…", "Flytter til position…");
            presetGrid.Invalidate();
            duration = (double)seconds.Value;
            clock.Restart();
            movement.Start();
            TickMotion();
        }

        void TickMotion()
        {
            double t = duration <= 0 ? 1 : Math.Min(1, clock.Elapsed.TotalSeconds / duration), f = Motion.Ease(t);
            foreach (var p in target)
            {
                int value = camera.Settings[p.Key].Snap(start[p.Key] + (p.Value - (double)start[p.Key]) * f);
                if (value != last[p.Key] || t >= 1)
                {
                    camera.Set(p.Key, value);
                    last[p.Key] = value;
                }
            }

            if (t >= 1)
            {
                bool reached = target.All(p => camera.Get(p.Key) == p.Value);
                if (!reached && clock.Elapsed.TotalSeconds < duration + 2)
                    return;
                movement.Stop();
                clock.Stop();
                activity.Complete(reached);
                if (!reached)
                    DiagnosticLog.Current.Write("WARNING", "Camera did not reach the preset position");
                status.Text = UiText.T(reached ? "Position reached." : "The camera did not reach the position.", reached ? "Positionen er nået." : "Kameraet nåede ikke positionen.");
                RefreshMotionControls();
                presetGrid.Invalidate();
            }
        }

        void SaveImageValue(string key)
        {
            if (!config.ImageValues.TryGetValue(CameraId, out var values))
                config.ImageValues[CameraId] = values = new Dictionary<string, ImageValue>();
            int flags;
            int value = camera.Get(key, out flags);
            values[key] = new ImageValue
            {
                Value = value,
                Automatic = (flags & 1) != 0
            };
            MarkDirty();
        }

        void ApplyImageValues()
        {
            if (config.ImageValues == null)
                config.ImageValues = new Dictionary<string, Dictionary<string, ImageValue>>();
            if (!config.ImageValues.TryGetValue(CameraId, out var values))
                return;
            foreach (var p in values)
            {
                if (camera.Settings.TryGetValue(p.Key, out var setting) && !axes.Contains(p.Key) && (setting.Caps & (p.Value.Automatic ? 1 : 2)) != 0)
                    camera.Set(p.Key, p.Value.Value, p.Value.Automatic);
            }
        }
    }
}
