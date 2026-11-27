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
            config.FinePanPercent = (int)settings.finePanPercent.Value;
            config.PanSteps = (int)settings.panSteps.Value;
            config.FastPanSteps = (int)settings.fastPanSteps.Value;
            config.ZoomSteps = (int)settings.zoomSteps.Value;
            config.Seconds = settings.seconds.Value;
            config.UseTransitionSpeed = settings.useTransitionSpeed.Checked;
            config.TransitionSpeedPercent = (int)settings.transitionSpeedPercent.Value;
            config.ReverseHorizontal = settings.reverseX.Checked;
            config.ReverseVertical = settings.reverseY.Checked;
            config.PanWithCameraButton = settings.panWithCameraButton.Checked;
            config.DoubleClickForCameraSource = settings.doubleClickForCameraSource.Checked;
            config.RequireStreamingConfirmation = settings.streamingCheck.Checked;
            config.AlwaysOnTop = settings.alwaysTop.Checked;
            config.Language = UiText.LanguageCodes[settings.language.SelectedIndex];
            config.Layout.SettingsOpen = false;
            ProfileWindow.Capture(this, config.Layout, wasMaximized);
            config.SettingsLayout = settings.CapturePlacement() ?? config.SettingsLayout;
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
                settings.reverseX.Checked = config.ReverseHorizontal;
                settings.reverseY.Checked = config.ReverseVertical;
                settings.panWithCameraButton.Checked = config.PanWithCameraButton;
                settings.doubleClickForCameraSource.Checked = config.DoubleClickForCameraSource;
                settings.streamingCheck.Checked = config.RequireStreamingConfirmation;
                settings.alwaysTop.Checked = config.AlwaysOnTop;
                TopMost = config.AlwaysOnTop;
                // Older profiles saved the window with the original 272-DIP settings column.
                if (config.Layout.SettingsOpen && config.Layout.HasBounds)
                    config.Layout.Width = ProfileWindow.WidthWithoutSettings(config.Layout.Width, LogicalToDeviceUnits(272), MinimumSize.Width);
                config.Layout.SettingsOpen = false;
                settings.ApplyPlacement(config.SettingsLayout);
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
        void RefreshProfiles(string selection = null)
        {
            settings.profilesList.Items.Clear();
            settings.profilesList.Items.AddRange(store.Read().Profiles.Keys.OrderBy(x => x == ProfileStore.Standard ? 0 : 1).ThenBy(x => x).ToArray());
            settings.profilesList.SelectedItem = selection ?? profile;
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
                if (dialog.ShowDialog(CommandOwner) != DialogResult.OK)
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
                destination.Config.SettingsLayout = config.SettingsLayout?.Copy();
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
                if (dialog.ShowDialog(CommandOwner) != DialogResult.OK)
                    return;
                store.Create(dialog.Value, copy ? profile : null);
                SwitchProfile(dialog.Value, false);
            }
        }
        void RenameProfile()
        {
            var selected = settings.profilesList.SelectedItem as string;
            if (!loaded || selected == null || selected == ProfileStore.Standard)
                return;
            bool renamingActive = selected == profile;
            if (renamingActive)
                SaveCurrent();
            long revision = renamingActive ? entry.Revision : store.Read().Profiles[selected].Revision;
            using (var dialog = new TextEntryForm(UiText.T("Rename profile", "Omdøb profil"), selected))
            {
                if (dialog.ShowDialog(CommandOwner) != DialogResult.OK || dialog.Value == selected)
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
            settings.finePanPercent.Value = config.FinePanPercent;
            settings.panSteps.Value = config.PanSteps;
            settings.fastPanSteps.Value = config.FastPanSteps;
            settings.zoomSteps.Value = config.ZoomSteps;
            settings.seconds.Value = config.Seconds;
            settings.transitionSpeedPercent.Value = config.TransitionSpeedPercent;
            settings.useTransitionSpeed.Checked = config.UseTransitionSpeed;
            UpdateTransitionMode();
        }
        void UpdateTransitionMode()
        {
            settings.seconds.Enabled = !settings.useTransitionSpeed.Checked;
            settings.transitionSpeedPercent.Enabled = settings.useTransitionSpeed.Checked;
        }
    }
}
