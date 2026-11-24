using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
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
            ApplyProfile();
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
            config.FinePanPercent = (int)finePanPercent.Value;
            config.PanSteps = (int)panSteps.Value;
            config.FastPanSteps = (int)fastPanSteps.Value;
            config.ZoomSteps = (int)zoomSteps.Value;
            config.Seconds = seconds.Value;
            config.UseTransitionSpeed = useTransitionSpeed.Checked;
            config.TransitionSpeedPercent = (int)transitionSpeedPercent.Value;
            config.ReverseHorizontal = reverseX.Checked;
            config.ReverseVertical = reverseY.Checked;
            config.PanWithCameraButton = panWithCameraButton.Checked;
            config.DoubleClickForCameraSource = doubleClickForCameraSource.Checked;
            config.RequireStreamingConfirmation = streamingCheck.Checked;
            config.AlwaysOnTop = alwaysTop.Checked;
            config.Language = UiText.LanguageCodes[language.SelectedIndex];
            config.Layout.SettingsOpen = false;
            ProfileWindow.Capture(this, config.Layout, wasMaximized, settingsWidthAdded, settingsLeftOffset);
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
        void ApplyProfile()
        {
            applying = true;
            try
            {
                Disconnect();
                DiagnosticLog.Current.CameraName = null;
                permission.Reset();
                LoadMovementSettings();
                LoadFramingSettings();
                reverseX.Checked = config.ReverseHorizontal;
                reverseY.Checked = config.ReverseVertical;
                panWithCameraButton.Checked = config.PanWithCameraButton;
                doubleClickForCameraSource.Checked = config.DoubleClickForCameraSource;
                streamingCheck.Checked = config.RequireStreamingConfirmation;
                alwaysTop.Checked = config.AlwaysOnTop;
                TopMost = config.AlwaysOnTop;
                // Older profiles saved the window with the original 272-DIP settings column.
                if (config.Layout.SettingsOpen && config.Layout.HasBounds)
                    config.Layout.Width = ProfileWindow.WidthWithoutSettings(config.Layout.Width, LogicalToDeviceUnits(272), MinimumSize.Width);
                config.Layout.SettingsOpen = false;
                SetSettings(false, false);
                settingsTabs.SelectedIndex = 0;
                settingsCategories.SelectedIndex = 0;
                ProfileWindow.Apply(this, config.Layout, preferredMinimumSize);
                ApplySplitter();
                profileButton.Text = profile + " ▾";
                RefreshPresets();
            }
            finally { applying = false; }
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
            finally { applying = before; }
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
                if (visible && !was)
                {
                    var area = Screen.FromControl(this).WorkingArea;
                    int panelWidth = PreferredSettingsWidth();
                    bool openLeft = area.Right - Right < panelWidth && Left - area.Left > area.Right - Right;
                    PlaceSettingsColumn(openLeft);
                    if (resize && WindowState == FormWindowState.Normal)
                        ExpandSettingsWindow(area, panelWidth, openLeft);
                }
                else if (!visible && WindowState == FormWindowState.Normal)
                    RemoveSettingsWidth();
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
        void PlaceSettingsColumn(bool onLeft)
        {
            // A right-to-left table lays out its first column on the right.
            int column = onLeft != (body.RightToLeft == RightToLeft.Yes) ? 0 : 1;
            if (body.GetColumn(settingsTabs) == column)
                return;
            body.SetColumn(settingsTabs, column);
            body.SetColumn(mainSplit, 1 - column);
            body.ColumnStyles[column].SizeType = SizeType.Absolute;
            body.ColumnStyles[column].Width = 0;
            body.ColumnStyles[1 - column].SizeType = SizeType.Percent;
            body.ColumnStyles[1 - column].Width = 100;
        }
        void ExpandSettingsWindow(Rectangle area, int panelWidth, bool onLeft)
        {
            int originalLeft = Left;
            int expandedWidth = Math.Min(area.Width, Width + panelWidth);
            settingsWidthAdded = Math.Max(0, expandedWidth - Width);
            int left = onLeft ? Left - settingsWidthAdded : Left;
            left = Math.Max(area.Left, Math.Min(left, area.Right - expandedWidth));
            settingsLeftOffset = originalLeft - left;
            SetBounds(left, Top, expandedWidth, Height);
        }
        int PreferredSettingsWidth()
        {
            int tabsWidth = 0;
            for (int index = 0; index < settingsTabs.TabCount; index++)
                tabsWidth += settingsTabs.GetTabRect(index).Width;
            // Leave room for the native tab borders and the table cell margins.
            return Math.Max(settingsPanelWidth, tabsWidth + settingsTabs.Margin.Horizontal + LogicalToDeviceUnits(8));
        }
        void RemoveSettingsWidth()
        {
            if (settingsWidthAdded == 0 && settingsLeftOffset == 0)
                return;
            int closedWidth = ProfileWindow.WidthWithoutSettings(Width, settingsWidthAdded, MinimumSize.Width);
            var area = Screen.FromControl(this).WorkingArea;
            int left = Math.Max(area.Left, Math.Min(Left + settingsLeftOffset, area.Right - closedWidth));
            settingsWidthAdded = settingsLeftOffset = 0;
            SetBounds(left, Top, closedWidth, Height);
        }
        void SizeSettingsColumn(bool visible)
        {
            int mainMinimum = mainSplit.Panel1MinSize + mainSplit.Panel2MinSize + mainSplit.SplitterWidth;
            int width = visible ? Math.Min(PreferredSettingsWidth(), Math.Max(0, body.ClientSize.Width - mainMinimum)) : 0;
            var column = body.ColumnStyles[body.GetColumn(settingsTabs)];
            if (column.Width != width)
                column.Width = width;
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
        void LoadMovementSettings()
        {
            finePanPercent.Value = config.FinePanPercent;
            panSteps.Value = config.PanSteps;
            fastPanSteps.Value = config.FastPanSteps;
            zoomSteps.Value = config.ZoomSteps;
            seconds.Value = config.Seconds;
            transitionSpeedPercent.Value = config.TransitionSpeedPercent;
            useTransitionSpeed.Checked = config.UseTransitionSpeed;
            UpdateTransitionMode();
        }
        void UpdateTransitionMode()
        {
            seconds.Enabled = !useTransitionSpeed.Checked;
            transitionSpeedPercent.Enabled = useTransitionSpeed.Checked;
        }
    }
}
