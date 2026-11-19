using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace CameraControlStudio {
    public partial class MainForm : AppForm {
        readonly ProfileStore store = new ProfileStore();
        readonly SessionPresets temporary = new SessionPresets();
        readonly CameraAccessGate permission = new CameraAccessGate();
        readonly PresetActivity activity = new PresetActivity();
        readonly Timer movement = new Timer { Interval = 33 }, held = new Timer { Interval = 150 }, saveTimer = new Timer { Interval = 600 };
        readonly Timer motionLimits = new Timer { Interval = 250 };
        readonly Stopwatch clock = new Stopwatch();
        Font permanentFont;
        readonly Dictionary<string, Bitmap> iconImages = new Dictionary<string, Bitmap>();
        ProfileEntry entry; SavedData config; string profile = ProfileStore.Standard;
        Camera camera; List<Device> devices = new List<Device>();
        bool loaded, applying, dirty, wasMaximized; string heldAxis; int heldDirection; bool heldFast;
        bool refreshingPresets;
        Dictionary<string, int> start, target, last; double duration;
        static readonly string[] axes = { "Zoom", "Pan", "Tilt" };
        const int SettingsPanelWidth = 340;
        static string LastProfileFile {
            get {
                return Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "CamPressets", "last-profile.txt");
            }
        }
        sealed class RowItem {
            public Preset Preset; public bool Temporary; public string DeviceId;
        }
        RowItem SelectedPresetRow => presetGrid.SelectedRows.Count == 0
            ? null : presetGrid.SelectedRows[0].Tag as RowItem;
        string CameraId {
            get {
                return config.CameraId ?? "";
            }
        }
        List<Preset> Permanent {
            get {
                if (!config.Cameras.TryGetValue(CameraId, out var list))
                    config.Cameras[CameraId] = list = new List<Preset>();
                return list;
            }
        }
        List<Preset> Temporary {
            get {
                return temporary.For(profile, CameraId);
            }
        }
        public MainForm() {
            InitializeComponent();
            MinimumSize = ProfileWindow.MinimumSize;
            if (InDesigner)
                return;
            Prepare(this);
            FormText.Translate(this);
            zoomPanel.RightToLeft = RightToLeft.No;
            wheel.RightToLeft = RightToLeft.No;
            Text = title.Text;
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            ConfigureGrid();
            ConfigureMainToolTips();
            WireEvents();
            RefreshActionButtons();
            RefreshMotionControls();
        }
        void Run(Action action) {
            try {
                action();
            } catch (OperationCanceledException) { 
                StopMotion(); 
            } catch (Exception ex) { 
                StopMotion(); 
                status.Text = ex.Message; 
                ShowMessage(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }
        void WireEvents() {
            Shown += (o, e) => { 
                if (documentationMode) 
                    InitializeDocumentation(); 
                else 
                    Run(InitializeProfile); };
            settingsButton.Click += (o, e) => Run(() => { 
                SetSettings(!settingsTabs.Visible, true); MarkDirty(); 
            });
            profileButton.Click += (o, e) => Run(ChooseProfile);
            wheel.MovementRequested += (axis, direction, fast) => Run(() => StartHeld(axis, direction, fast));
            wheel.Released += () => held.Stop();
            wheel.SourceRequested += () => Run(ChooseCamera);
            WireZoom(normalIn, 1);
            WireZoom(normalOut, -1);
            zoomSlider.PositionRequested += fraction => Run(() => SetZoomPosition(fraction));
            zoomSlider.StepRequested += (direction, fast) => Run(() => Nudge("Zoom", direction, fast));
            movement.Tick += (o, e) => Run(TickMotion);
            held.Tick += (o, e) => Run(() => {
                if (!Nudge(heldAxis, heldDirection, heldFast))
                    held.Stop();
            });
            motionLimits.Tick += (o, e) => RefreshMotionControls();
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
                if (e.RowIndex >= 0 && e.ColumnIndex == 2) 
                    Run(() => Recall((RowItem)presetGrid.Rows[e.RowIndex].Tag)); 
            };
            presetGrid.CellDoubleClick += (o, e) => { 
                if (e.RowIndex >= 0 && e.ColumnIndex != 2) 
                    Run(() => Recall((RowItem)presetGrid.Rows[e.RowIndex].Tag)); 
            };
            presetGrid.KeyDown += (o, e) => { 
                if (e.KeyCode == Keys.Enter) {
                    e.Handled = e.SuppressKeyPress = true; 
                    Run(() => Recall(SelectedPresetRow));
                } 
            };
            presetGrid.CellFormatting += (o, e) => { 
                if (e.RowIndex < 0) 
                    return; 
                var row = (RowItem)presetGrid.Rows[e.RowIndex].Tag; 
                if (row == null) return; 
                var c = row.Preset.Name == activity.Name ? (activity.Phase == PresetPhase.Moving ? Color.Gold : activity.Phase == PresetPhase.Reached ? Color.MediumSeaGreen : BlueTheme.Panel) : BlueTheme.Panel; e.CellStyle.BackColor = c; e.CellStyle.SelectionBackColor = c == BlueTheme.Panel ? Color.FromArgb(15, 90, 160) : c; e.CellStyle.ForeColor = e.CellStyle.SelectionForeColor = c == BlueTheme.Panel ? Color.White : Color.Black; };
            addPermanent.Click += (o, e) => Run(AddPermanent);
            editPermanent.Click += (o, e) => Run(EditPermanent);
            updatePermanent.Click += (o, e) => Run(UpdatePermanent);
            deletePermanent.Click += (o, e) => Run(DeletePermanent);
            newProfile.Click += (o, e) => Run(() => CreateProfile(false));
            copyProfile.Click += (o, e) => Run(() => CreateProfile(true));
            renameProfile.Click += (o, e) => Run(RenameProfile);
            profilesList.DoubleClick += (o, e) => Run(() => { if (profilesList.SelectedItem != null) ChooseProfile((string)profilesList.SelectedItem); });
            foreach (var control in new[] { panSteps, fastPanSteps, zoomSteps, fastZoomSteps, seconds })
                control.ValueChanged += (o, e) => MarkDirty();
            foreach (var control in new[] { reverseX, reverseY, alwaysTop })
                control.CheckedChanged += (o, e) => { if (!applying) { TopMost = alwaysTop.Checked; MarkDirty(); } };
            reverseX.CheckedChanged += (o, e) => RefreshMotionAxis("Pan");
            reverseY.CheckedChanged += (o, e) => RefreshMotionAxis("Tilt");
            streamingCheck.CheckedChanged += (o, e) => { if (applying) return; permission.Reset(); Disconnect(); MarkDirty(); };
            language.Items.AddRange(UiText.LanguageNames);
            language.Items[0] = UiText.T("Automatic", "Automatisk");
            language.SelectedIndex = Array.IndexOf(UiText.LanguageCodes, UiText.Preference);
            language.SelectedIndexChanged += (o, e) => { if (applying || !loaded) return; Run(() => { UiText.SavePreference(UiText.LanguageCodes[language.SelectedIndex]); ShowMessage(UiText.T("Restart the application to change language.", "Genstart programmet for at skifte sprog.")); }); };
            mainSplit.SplitterMoved += (o, e) => { if (!applying && loaded) { config.Layout.ControllerRatio = LayoutPolicy.Ratio(mainSplit.SplitterDistance / (double)Math.Max(1, mainSplit.ClientSize.Width)); MarkDirty(); } };
            mainSplit.SizeChanged += (o, e) => { if (loaded && !applying) ApplySplitter(); };
            body.SizeChanged += (o, e) => SizeSettingsColumn(settingsTabs.Visible);
            Move += (o, e) => MarkDirty();
            Resize += (o, e) => { if (WindowState != FormWindowState.Minimized) wasMaximized = WindowState == FormWindowState.Maximized; MarkDirty(); };
            settingsTabs.SelectedIndexChanged += (o, e) => {
                if (settingsTabs.SelectedTab == presetsTab)
                    SelectPresetInSettings(SelectedPresetRow);
                MarkDirty();
            };
            imageFields.SizeChanged += (o, e) => FitImageFields();
            KeyPreview = true;
            KeyDown += (o, e) => { if (e.KeyCode == Keys.Escape) StopMotion(); };
            Deactivate += (o, e) => { held.Stop(); };
            Link(repoLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio");
            Link(issuesLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues");
            Link(discussionLink, "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/discussions");
            Link(coffeeLink, "https://buymeacoffee.com/brislingveo");
            versionLabel.Text = "MIT · " + Application.ProductVersion;
            FormClosing += (o, e) => { StopMotion(); if (!loaded) return; try { SaveCurrent(); } catch (Exception ex) { e.Cancel = ShowMessage(ex.Message + "\n" + UiText.T("Close without saving?", "Luk uden at gemme?"), Text, MessageBoxButtons.YesNo) != DialogResult.Yes; } };
            FormClosed += (o, e) => { movement.Dispose(); held.Dispose(); saveTimer.Dispose(); motionLimits.Dispose(); camera?.Dispose(); foreach (var d in devices) d.Dispose(); foreach (var image in iconImages.Values) image.Dispose(); permanentFont?.Dispose(); };
        }
        void Link(LinkLabel label, string url) {
            label.LinkClicked += (o, e) => Run(() => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }));
        }
        void ConfigureMainToolTips() {
            SetMainToolTip(temporaryButton, UiText.T("Add temporary preset", "Tilføj midlertidig position"));
            SetMainToolTip(deleteTemporaryButton, UiText.T("Delete temporary preset", "Slet midlertidig position"));
            SetMainToolTip(settingsButton, UiText.T("Settings", "Indstillinger"));
            SetMainToolTip(profileButton, UiText.T("Choose profile", "Vælg profil"));
            SetMainToolTip(wheel, wheel.AccessibleName);
            SetMainToolTip(zoomSlider, UiText.T("Zoom", "Zoom"),
                UiText.T("Drag and release to set zoom.", "Træk og slip for at indstille zoom."));
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
            SetMainToolTip(button, title, UiText.T("Hold Shift for fast zoom.", "Hold Skift nede for hurtig zoom."));
            button.MouseDown += (o, e) => {
                if (e.Button == MouseButtons.Left)
                    Run(() => StartHeld("Zoom", direction, (ModifierKeys & Keys.Shift) != 0));
            };
            button.MouseUp += (o, e) => held.Stop();
            button.MouseCaptureChanged += (o, e) => { if (!button.Capture) held.Stop(); };
            button.KeyDown += (o, e) => {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) {
                    e.SuppressKeyPress = true;
                    Run(() => Nudge("Zoom", direction, e.Shift));
                }
            };
        }
        void InitializeProfile() {
            var catalog = store.Read();
            try {
                if (File.Exists(LastProfileFile)) {
                    var name = File.ReadAllText(LastProfileFile).Trim();
                    if (catalog.Profiles.ContainsKey(name))
                        profile = name;
                }
            } catch (IOException) { }
            entry = catalog.Profiles[profile];
            config = entry.Config;
            loaded = true;
            ApplyProfile(settingsOpen: false);
            RefreshProfiles();
            if (!config.RequireStreamingConfirmation)
                NeedCamera();
        }
        void MarkDirty() {
            if (!loaded || applying)
                return;
            dirty = true;
            saveTimer.Stop();
            saveTimer.Start();
        }
        void ReadSettings() {
            config.PanSteps = (int)panSteps.Value;
            config.FastPanSteps = (int)fastPanSteps.Value;
            config.ZoomSteps = (int)zoomSteps.Value;
            config.FastZoomSteps = (int)fastZoomSteps.Value;
            config.Seconds = seconds.Value;
            config.ReverseHorizontal = reverseX.Checked;
            config.ReverseVertical = reverseY.Checked;
            config.RequireStreamingConfirmation = streamingCheck.Checked;
            config.AlwaysOnTop = alwaysTop.Checked;
            config.Layout.SettingsOpen = settingsTabs.Visible;
            config.Layout.SettingsTab = settingsTabs.SelectedIndex;
            ProfileWindow.Capture(this, config.Layout, wasMaximized);
        }
        void SaveCurrent() {
            if (!loaded)
                return;
            saveTimer.Stop();
            ReadSettings();
            entry = store.Save(profile, config, entry.Revision);
            config = entry.Config;
            Directory.CreateDirectory(Path.GetDirectoryName(LastProfileFile));
            File.WriteAllText(LastProfileFile, profile);
            dirty = false;
            RefreshPresets();
        }
        void ApplyProfile(bool settingsOpen) {
            applying = true;
            try {
                Disconnect();
                permission.Reset();
                panSteps.Value = config.PanSteps;
                fastPanSteps.Value = config.FastPanSteps;
                zoomSteps.Value = config.ZoomSteps;
                fastZoomSteps.Value = config.FastZoomSteps;
                seconds.Value = config.Seconds;
                reverseX.Checked = config.ReverseHorizontal;
                reverseY.Checked = config.ReverseVertical;
                streamingCheck.Checked = config.RequireStreamingConfirmation;
                alwaysTop.Checked = config.AlwaysOnTop;
                TopMost = config.AlwaysOnTop;
                SetSettings(settingsOpen, false);
                settingsTabs.SelectedIndex = Math.Max(0, Math.Min(settingsTabs.TabCount - 1, config.Layout.SettingsTab));
                ProfileWindow.Apply(this, config.Layout);
                ApplySplitter();
                profileButton.Text = profile + " ▾";
                RefreshPresets();
            } finally { applying = false; }
        }
        void ApplySplitter() {
            if (config == null)
                return;
            bool before = applying;
            applying = true;
            try {
                int usable = mainSplit.ClientSize.Width - mainSplit.SplitterWidth;
                if (usable < mainSplit.Panel1MinSize + mainSplit.Panel2MinSize)
                    return;
                mainSplit.SplitterDistance = Math.Max(mainSplit.Panel1MinSize, Math.Min(usable - mainSplit.Panel2MinSize, (int)(mainSplit.ClientSize.Width * config.Layout.ControllerRatio)));
            } finally { applying = before; }
        }
        void SetSettings(bool visible, bool resize) {
            bool was = settingsTabs.Visible;
            double ratio = config?.Layout.ControllerRatio ?? LayoutPolicy.DefaultControllerRatio;
            body.SuspendLayout();
            try {
                settingsTabs.Visible = visible;
                if (visible && !was && settingsTabs.SelectedTab == presetsTab)
                    SelectPresetInSettings(SelectedPresetRow);
                if (resize && was != visible && WindowState == FormWindowState.Normal) {
                    var area = Screen.FromControl(this).WorkingArea;
                    Width = Math.Min(area.Width, Math.Max(MinimumSize.Width, Width + (visible ? SettingsPanelWidth : -SettingsPanelWidth)));
                    if (Right > area.Right)
                        Left = area.Right - Width;
                }
                SizeSettingsColumn(visible);
            } finally {
                body.ResumeLayout();
            }
            if (config != null) {
                config.Layout.ControllerRatio = ratio;
                config.Layout.SettingsOpen = visible;
            }
            ApplySplitter();
        }
        void SizeSettingsColumn(bool visible) {
            int mainMinimum = mainSplit.Panel1MinSize + mainSplit.Panel2MinSize + mainSplit.SplitterWidth;
            int width = visible ? Math.Min(SettingsPanelWidth, Math.Max(0, body.ClientSize.Width - mainMinimum)) : 0;
            if (body.ColumnStyles[1].Width != width)
                body.ColumnStyles[1].Width = width;
        }
        void RefreshProfiles() {
            profilesList.Items.Clear();
            profilesList.Items.AddRange(store.Read().Profiles.Keys.OrderBy(x => x == ProfileStore.Standard ? 0 : 1).ThenBy(x => x).ToArray());
            profilesList.SelectedItem = profile;
            RefreshActionButtons();
        }
        void ChooseProfile() {
            ChooseProfile(profile);
        }
        void ChooseProfile(string selected) {
            if (!loaded)
                return;
            StopMotion();
            using (var dialog = new ProfilePickerForm(store.Read().Profiles.Keys, selected, config.KeepLayoutOnSwitch)) {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                config.KeepLayoutOnSwitch = dialog.KeepLayout;
                SaveCurrent();
                if (dialog.Selected == profile)
                    return;
                SwitchProfile(dialog.Selected, dialog.KeepLayout);
            }
        }
        void SwitchProfile(string next, bool keep) {
            var previous = config.Layout.Copy();
            var catalog = store.Read();
            entry = catalog.Profiles[next];
            config = entry.Config;
            config.Layout = LayoutPolicy.OnSwitch(previous, config.Layout, keep);
            profile = next;
            ClearPresetSelection();
            ApplyProfile(config.Layout.SettingsOpen);
            RefreshProfiles();
            SaveCurrent();
        }
        void CreateProfile(bool copy) {
            SaveCurrent();
            using (var dialog = new TextEntryForm(UiText.T("New profile", "Ny profil"), "")) {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                store.Create(dialog.Value, copy ? profile : null);
                SwitchProfile(dialog.Value, false);
            }
        }
        void RenameProfile() {
            SaveCurrent();
            using (var dialog = new TextEntryForm(UiText.T("Rename profile", "Omdøb profil"), profile)) {
                if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Value == profile)
                    return;
                var catalog = store.Rename(profile, dialog.Value, entry.Revision);
                temporary.RenameProfile(profile, dialog.Value);
                profile = dialog.Value;
                entry = catalog.Profiles[profile];
                config = entry.Config;
                profileButton.Text = profile + " ▾";
                RefreshProfiles();
                SaveCurrent();
            }
        }
        void AllowCamera() {
            if (!loaded)
                throw new InvalidOperationException(UiText.T("Load a profile first.", "Indlæs først en profil."));
            if (!permission.Allow(streamingCheck.Checked, () => ShowMessage(UiText.T("Has streaming started? Choose Yes to enable camera controls.", "Er streaming startet? Vælg Ja for at aktivere kamerastyringen."), UiText.T("Confirm streaming", "Bekræft streaming"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                throw new OperationCanceledException();
        }
        void EnumerateDevices() {
            AllowCamera();
            Disconnect();
            foreach (var d in devices)
                d.Dispose();
            devices = Camera.Enumerate();
        }
        void NeedCamera() {
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
        void Connect(Device device) {
            AllowCamera();
            StopMotion();
            camera?.Dispose();
            camera = null;
            camera = new Camera(device);
            if (CameraId != device.Id)
                ClearPresetSelection();
            config.CameraId = device.Id;
            RefreshMotionControls();
            motionLimits.Start();
            cameraStatus.Text = UiText.T("Camera", "Kamera") + ": " + device.Name;
            ApplyImageValues();
            BuildImageControls();
            RefreshPresets();
            MarkDirty();
        }
        void ChooseCamera() {
            StopMotion();
            EnumerateDevices();
            using (var dialog = new CameraSourceForm(devices, CameraId)) {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    Connect(dialog.Selected);
            }
        }
        void Disconnect() {
            StopMotion();
            motionLimits.Stop();
            camera?.Dispose();
            camera = null;
            RefreshMotionControls();
            RefreshActionButtons();
            imageFields.Controls.Clear();
            var load = new BlueButton { Text = UiText.T("Load image controls", "Indlæs billedindstillinger"), Width = 280, Height = 32 };
            load.Click += (o, e) => Run(NeedCamera);
            imageFields.Controls.Add(load);
            cameraStatus.Text = UiText.T("Camera: not connected", "Kamera: ikke tilsluttet");
        }
        void ConfigureGrid() {
            permanentFont = new Font(presetGrid.Font, FontStyle.Bold);
            presetGrid.RowTemplate.Height = 44;
            presetGrid.DefaultCellStyle.BackColor = BlueTheme.Panel;
            presetGrid.DefaultCellStyle.ForeColor = Color.White;
            presetGrid.DefaultCellStyle.Padding = new Padding(4);
            presetGrid.GridColor = BlueTheme.Line;
            presetGrid.Columns.Add(new DataGridViewImageColumn { Name = "Icon", Width = 64, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, DefaultCellStyle = new DataGridViewCellStyle { NullValue = null }, ImageLayout = DataGridViewImageCellLayout.Zoom });
            presetGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Position", FillWeight = 100 });
            presetGrid.Columns.Add(new DataGridViewButtonColumn { Name = "Recall", Text = "▶", UseColumnTextForButtonValue = true, Width = 42, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, FlatStyle = FlatStyle.Flat });
            presetGrid.CellPainting += PaintRecallButton;
            presetGrid.SizeChanged += (sender, e) => SizePresetColumns();
            SizePresetColumns();
        }
        void SizePresetColumns() {
            bool compact = presetGrid.ClientSize.Width < 190;
            presetGrid.Columns["Icon"].Width = compact ? 34 : 64;
            presetGrid.Columns["Recall"].Width = compact ? 30 : 42;
        }
        void PaintRecallButton(object sender, DataGridViewCellPaintingEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex != 2)
                return;

            e.Paint(e.ClipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.SelectionBackground);
            bool selected = (e.State & DataGridViewElementStates.Selected) != 0;
            var color = selected ? e.CellStyle.SelectionForeColor : e.CellStyle.ForeColor;
            var button = Rectangle.Inflate(e.CellBounds, -5, -6);
            button.Width--;
            button.Height--;
            using (var border = new Pen(Color.FromArgb(170, color)))
                e.Graphics.DrawRectangle(border, button);
            TextRenderer.DrawText(e.Graphics, "▶", presetGrid.Font, button, color,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);
            e.Handled = true;
        }
        Bitmap IconImage(string id) {
            id = PresetIcons.ResolveId(id);
            if (!iconImages.TryGetValue(id, out var image)) {
                image = new Bitmap(64, 48);
                using (var g = Graphics.FromImage(image))
                    PresetIcons.Draw(g, id, new Rectangle(2, 2, 60, 44));
                iconImages[id] = image;
            }
            return image;
        }
        void RefreshPresets() {
            if (config == null)
                return;
            var selected = SelectedPresetRow;
            string edited = (permanentList.SelectedItem as Preset)?.Name;
            refreshingPresets = true;
            try {
                presetGrid.Rows.Clear();
                permanentList.Items.Clear();
                DataGridViewRow selectedRow = null;
                string recall = UiText.T("Recall preset", "Kør til position");
                foreach (var pair in Permanent.Select(x => new RowItem { Preset = x, Temporary = false, DeviceId = CameraId }).Concat(Temporary.Select(x => new RowItem { Preset = x, Temporary = true, DeviceId = CameraId }))) {
                    int n = presetGrid.Rows.Add(pair.Temporary ? null : IconImage(pair.Preset.IconId), pair.Preset.Name, "▶");
                    var row = presetGrid.Rows[n];
                    row.Tag = pair;
                    row.Cells["Recall"].ToolTipText = recall;
                    row.DefaultCellStyle.Font = pair.Temporary ? presetGrid.Font : permanentFont;
                    if (selected != null && pair.DeviceId == selected.DeviceId
                        && pair.Temporary == selected.Temporary && pair.Preset.Name == selected.Preset.Name)
                        selectedRow = row;
                }
                permanentList.Items.AddRange(Permanent.ToArray());
                SelectPermanentPreset(edited);
                presetGrid.CurrentCell = selectedRow?.Cells[1];
                presetGrid.ClearSelection();
                if (selectedRow != null)
                    selectedRow.Selected = true;
            } finally {
                refreshingPresets = false;
            }
            RefreshActionButtons();
        }
        void ClearPresetSelection() {
            presetGrid.CurrentCell = null;
            presetGrid.ClearSelection();
            permanentList.ClearSelected();
        }
        void SelectPresetInSettings(RowItem row) {
            if (row != null)
                SelectPermanentPreset(row.Temporary ? null : row.Preset.Name);
        }
        void SelectPermanentPreset(string name) {
            permanentList.SelectedItem = name == null ? null
                : permanentList.Items.Cast<Preset>().FirstOrDefault(p => p.Name == name);
        }
        void RefreshActionButtons() {
            bool presetSelected = permanentList.SelectedItem is Preset;
            // A disconnected camera can still connect on demand when a capture action is used.
            bool canCapture = loaded && (camera == null || axes.Any(axis => HasManualAxis(axis, out _)));
            addPermanent.Enabled = temporaryButton.Enabled = canCapture;
            editPermanent.Enabled = deletePermanent.Enabled = presetSelected;
            updatePermanent.Enabled = presetSelected && canCapture;
            bool temporarySelected = SelectedPresetRow?.Temporary == true;
            deleteTemporaryButton.Enabled = deleteTemporaryButton.Visible = temporarySelected;
            presetHeader.ColumnStyles[1].Width = temporarySelected ? 40 : 0;
            profileButton.Enabled = newProfile.Enabled = copyProfile.Enabled = loaded;
            renameProfile.Enabled = loaded && profile != ProfileStore.Standard;
        }
        bool NameExists(string name, Preset except = null) {
            return Permanent.Concat(Temporary).Any(p => p != except && string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }
        Dictionary<string, int> ReadPose() {
            NeedCamera();
            var pose = new Dictionary<string, int>();
            foreach (var key in axes)
                if (camera.Settings.TryGetValue(key, out var setting) && (setting.Caps & 2) != 0)
                    pose[key] = camera.Get(key);
            if (pose.Count == 0)
                throw new InvalidOperationException(UiText.T("The driver does not provide position or zoom controls.", "Driveren stiller ikke position eller zoom til rådighed."));
            return pose;
        }
        void AddTemporary() {
            NeedCamera();
            using (var dialog = new TextEntryForm(UiText.T("Temporary preset name", "Navn på midlertidig position"), "")) {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                if (NameExists(dialog.Value))
                    throw new InvalidOperationException(UiText.T("Name already exists.", "Navnet findes allerede."));
                Temporary.Add(new Preset { Name = dialog.Value, Position = ReadPose() });
                RefreshPresets();
            }
        }
        void DeleteTemporary() {
            var row = SelectedPresetRow;
            if (row == null || !row.Temporary)
                return;
            Temporary.Remove(row.Preset);
            if (activity.Name == row.Preset.Name)
                StopMotion();
            RefreshPresets();
        }
        void AddPermanent() {
            NeedCamera();
            using (var dialog = new PresetEditorForm(null, x => NameExists(x))) {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                var p = new Preset { Name = dialog.PresetName, IconId = dialog.IconId, Position = ReadPose() };
                Permanent.Add(p);
                try {
                    SaveCurrent();
                } catch { Permanent.Remove(p); throw; }
                SelectPermanentPreset(p.Name);
            }
        }
        void EditPermanent() {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            using (var dialog = new PresetEditorForm(p, x => NameExists(x, p))) {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                string old = p.Name, icon = p.IconId;
                p.Name = dialog.PresetName;
                p.IconId = dialog.IconId;
                try {
                    SaveCurrent();
                } catch { p.Name = old; p.IconId = icon; throw; }
                if (activity.Name == old)
                    StopMotion();
            }
        }
        void UpdatePermanent() {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            NeedCamera();
            if (ShowMessage(UiText.T("Replace with current position?", "Erstat med aktuel position?"), Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            var old = p.Position;
            p.Position = ReadPose();
            try {
                SaveCurrent();
            } catch { p.Position = old; throw; }
            StopMotion();
        }
        void DeletePermanent() {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            string confirmation = string.Format(UiText.T("Delete permanent preset: {0}?", "Slet den permanente position »{0}«?"), p.Name);
            if (ShowMessage(confirmation, Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            int i = Permanent.IndexOf(p);
            Permanent.RemoveAt(i);
            try {
                SaveCurrent();
            } catch { Permanent.Insert(i, p); throw; }
            StopMotion();
        }
        void StopMotion() {
            movement.Stop();
            held.Stop();
            clock.Stop();
            activity.Clear();
            presetGrid.Invalidate();
        }
        void StartHeld(string axis, int direction, bool fast) {
            held.Stop();
            if (!Nudge(axis, direction, fast))
                return;
            heldAxis = axis;
            heldDirection = direction;
            heldFast = fast;
            if ((Control.MouseButtons & MouseButtons.Left) != 0)
                held.Start();
        }
        bool HasManualAxis(string axis, out Setting setting) {
            setting = null;
            return camera != null && camera.Settings.TryGetValue(axis, out setting) && (setting.Caps & 2) != 0;
        }
        bool AxisReversed(string axis) => axis == "Pan" ? reverseX.Checked : axis == "Tilt" && reverseY.Checked;
        void RefreshMotionControls() {
            foreach (var axis in axes)
                RefreshMotionAxis(axis);
        }
        void RefreshMotionAxis(string axis) {
            if (HasManualAxis(axis, out var setting)) {
                try {
                    UpdateMotionAxis(axis, setting, camera.Get(axis));
                    return;
                } catch (COMException) {
                    // Drivers can temporarily fail to report a position. The next tick retries.
                }
            }
            bool canConnect = loaded && camera == null;
            if (axis == "Zoom") {
                zoomSlider.Enabled = false;
                if (camera == null)
                    zoomSlider.Position = .5;
            }
            SetMotionAvailability(axis, canConnect, canConnect);
        }
        void UpdateMotionAxis(string axis, Setting setting, int position) {
            bool reverse = AxisReversed(axis);
            bool negative = ManualMotion.CanMove(setting, position, -1, reverse);
            bool positive = ManualMotion.CanMove(setting, position, 1, reverse);
            if (axis == "Zoom") {
                zoomSlider.Position = ManualMotion.ZoomFraction(setting, position);
                zoomSlider.Enabled = negative || positive;
            }
            SetMotionAvailability(axis, negative, positive);
        }
        void SetMotionAvailability(string axis, bool negative, bool positive) {
            if (axis == "Zoom") {
                normalOut.Enabled = negative;
                normalIn.Enabled = positive;
            } else {
                wheel.SetDirectionAvailability(axis, negative, positive);
            }
        }
        bool Nudge(string axis, int direction, bool fast) {
            NeedCamera();
            if (!HasManualAxis(axis, out var setting))
                throw new InvalidOperationException(UiText.T("This camera control is not supported.", "Denne kamerafunktion understøttes ikke."));
            int position = camera.Get(axis);
            bool reverse = AxisReversed(axis);
            if (!ManualMotion.CanMove(setting, position, direction, reverse)) {
                UpdateMotionAxis(axis, setting, position);
                return false;
            }
            int steps = (int)(axis == "Zoom" ? (fast ? fastZoomSteps.Value : zoomSteps.Value) : (fast ? fastPanSteps.Value : panSteps.Value));
            SetManualPosition(axis, ManualMotion.Target(setting, position, direction, steps, reverse));
            position = camera.Get(axis);
            UpdateMotionAxis(axis, setting, position);
            return ManualMotion.CanMove(setting, position, direction, reverse);
        }
        void SetZoomPosition(double fraction) {
            if (!HasManualAxis("Zoom", out var setting))
                return;

            int position = camera.Get("Zoom");
            int desired = ManualMotion.ZoomAtFraction(setting, fraction);
            if (position != desired) {
                held.Stop();
                SetManualPosition("Zoom", desired);
                position = camera.Get("Zoom");
            }
            UpdateMotionAxis("Zoom", setting, position);
        }
        void SetManualPosition(string axis, int position) {
            movement.Stop();
            clock.Stop();
            activity.Clear();
            presetGrid.Invalidate();
            camera.Set(axis, position);
            status.Text = UiText.T("Manual movement", "Manuel bevægelse");
        }
        void Recall(RowItem row) {
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
            foreach (var pair in row.Preset.Position) {
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
        void TickMotion() {
            double t = duration <= 0 ? 1 : Math.Min(1, clock.Elapsed.TotalSeconds / duration), f = Motion.Ease(t);
            foreach (var p in target) {
                int value = camera.Settings[p.Key].Snap(start[p.Key] + (p.Value - (double)start[p.Key]) * f);
                if (value != last[p.Key] || t >= 1) {
                    camera.Set(p.Key, value);
                    last[p.Key] = value;
                }
            }
            if (t >= 1) {
                bool reached = target.All(p => camera.Get(p.Key) == p.Value);
                if (!reached && clock.Elapsed.TotalSeconds < duration + 2)
                    return;
                movement.Stop();
                clock.Stop();
                activity.Complete(reached);
                status.Text = UiText.T(reached ? "Position reached." : "The camera did not reach the position.", reached ? "Positionen er nået." : "Kameraet nåede ikke positionen.");
                RefreshMotionControls();
                presetGrid.Invalidate();
            }
        }
        void FitImageFields() {
            foreach (Control child in imageFields.Controls)
                child.Width = Math.Max(180, imageFields.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 12);
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
        void BuildImageControls() {
            imageFields.Controls.Clear();
            var imageSettings = camera.Settings.Values.Where(s => !axes.Contains(s.Name) && (s.Caps & 3) != 0).ToArray();
            foreach (var setting in imageSettings) {
                var label = new Label { Text = UiText.T(setting.Name, setting.Name), AutoSize = false, Height = 24 };
                imageFields.Controls.Add(label);
                var slider = new TrackBar { Minimum = 0, Maximum = 1000, TickStyle = TickStyle.None, Height = 30 };
                imageFields.Controls.Add(slider);
                int flags;
                int value = camera.Get(setting.Name, out flags);
                slider.Value = Math.Max(0, Math.Min(1000, (int)((value - (double)setting.Min) / (setting.Max - (double)setting.Min) * 1000)));
                slider.Enabled = (flags & 1) == 0 && (setting.Caps & 2) != 0;
                slider.Scroll += (o, e) => Run(() => { NeedCamera(); camera.Set(setting.Name, setting.Min + slider.Value / 1000.0 * (setting.Max - (double)setting.Min)); SaveImageValue(setting.Name); });
                if ((setting.Caps & 1) != 0) {
                    var auto = new CheckBox { Text = string.Format(UiText.T("Automatic {0}", "Automatisk {0}"), label.Text), Checked = (flags & 1) != 0, Height = 28 };
                    bool allowsManual = (setting.Caps & 2) != 0;
                    auto.Enabled = allowsManual || !auto.Checked;
                    auto.CheckedChanged += (o, e) => Run(() => {
                        NeedCamera();
                        camera.Set(setting.Name, camera.Get(setting.Name), auto.Checked);
                        SaveImageValue(setting.Name);
                        slider.Enabled = allowsManual && !auto.Checked;
                        auto.Enabled = allowsManual || !auto.Checked;
                    });
                    imageFields.Controls.Add(auto);
                }
            }
            if (imageFields.Controls.Count == 0)
                imageFields.Controls.Add(new Label { Text = UiText.T("No image controls available through the driver.", "Driveren tilbyder ingen billedindstillinger."), AutoSize = true });
            var reset = new BlueButton { Text = UiText.T("Reset image to driver defaults", "Gendan driverens billedstandarder"), Height = 34, Enabled = imageSettings.Length != 0 };
            reset.Click += (o, e) => Run(() => { NeedCamera(); foreach (var setting in imageSettings) { camera.Set(setting.Name, setting.Default, (setting.Caps & 2) == 0); SaveImageValue(setting.Name); } BuildImageControls(); });
            imageFields.Controls.Add(reset);
            Prepare(imageFields);
            FitImageFields();
        }
    }
}
