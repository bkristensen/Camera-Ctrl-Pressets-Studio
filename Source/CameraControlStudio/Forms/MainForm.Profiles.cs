using System;
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
            config.FinePanPercent = (int)finePanPercent.Value;
            config.PanSteps = (int)panSteps.Value;
            config.FastPanSteps = (int)fastPanSteps.Value;
            config.ZoomSteps = (int)zoomSteps.Value;
            config.Seconds = seconds.Value;
            config.UseTransitionSpeed = useTransitionSpeed.Checked;
            config.TransitionSpeedPercent = (int)transitionSpeedPercent.Value;
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
                LoadMovementSettings();
                LoadFramingSettings();
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
