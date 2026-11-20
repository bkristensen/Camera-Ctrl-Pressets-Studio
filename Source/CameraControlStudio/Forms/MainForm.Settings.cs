using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        TabPage[] savedTabOrder;
        TabPage[] videoTabs;
        bool refreshingVideo;

        void WireSettings()
        {
            // Profile files use the original tab order as their stable numeric IDs.
            savedTabOrder = new[] { generalTab, imageTab, presetsTab, profilesTab, aboutTab, issuesTab };

            logSettingsLink.LinkClicked += (sender, args) => Run(() => LoggingDialog.ShowSettings(this));
            showLogLink.LinkClicked += (sender, args) => Run(() => ErrorDialog.OpenLog(this, DiagnosticLog.Current.FindLatest()));
            reportLink.LinkClicked += (sender, args) => Run(() => ErrorDialog.Report(this, DiagnosticLog.Current.FindLatest()));
            Link(releasesLink, GuideViewer.ReleasesUrl);
            Link(guideIndexLink, GuideViewer.OnlineGuidesUrl);
            openGuideButton.Click += (sender, args) => Run(OpenGuide);
            helpButton.Click += (sender, args) => Run(OpenGuide);
        }

        void OpenGuide()
        {
            using (var viewer = new GuideViewer(settingsTabs.Visible))
                viewer.ShowDialog(this);
        }

        void ConfigureSettingsToolTips()
        {
            SetMainToolTip(addPermanent, UiText.T("Add permanent preset", "Tilføj fast position"));
            SetMainToolTip(editPermanent, UiText.T("Edit name and symbol", "Ret navn og symbol"));
            SetMainToolTip(updatePermanent, UiText.T("Replace with current position", "Erstat med aktuel position"));
            SetMainToolTip(deletePermanent, UiText.T("Delete permanent preset", "Slet fast position"));
            SetMainToolTip(movePermanentUp, UiText.T("Move selected preset up", "Flyt valgt position op"));
            SetMainToolTip(movePermanentDown, UiText.T("Move selected preset down", "Flyt valgt position ned"));
            SetMainToolTip(newProfile, UiText.T("New profile", "Ny profil"));
            SetMainToolTip(copyProfile, UiText.T("Copy active profile", "Kopiér aktiv profil"));
            SetMainToolTip(renameProfile, UiText.T("Rename selected profile", "Omdøb valgt profil"));
            foreach (var button in new ButtonBase[] { reverseX, reverseY, streamingCheck, alwaysTop })
                SetMainToolTip(button, button.Text);
            foreach (var action in new Control[] { logSettingsLink, showLogLink, reportLink, openGuideButton,
                loadVideoControls, resetVideoControls })
                SetMainToolTip(action, action.Text);
            SetMainToolTip(helpButton, UiText.T("User guides", "Brugervejledninger"));
        }

        void WireVideoControls()
        {
            videoTabs = imageCategories.TabPages.Cast<TabPage>().ToArray();
            foreach (var editor in videoTabs.SelectMany(VideoEditors))
            {
                SetMainToolTip(editor.Automatic, string.Format(UiText.T("Automatic {0}", "Automatisk {0}"), editor.Group.Text));
                editor.Slider.Scroll += (sender, args) => Run(() =>
                {
                    if (TryGetVideoSetting(editor.Group, out var setting))
                    {
                        var slider = editor.Slider;
                        double fraction = (slider.Value - (double)slider.Minimum) / (slider.Maximum - (double)slider.Minimum);
                        camera.Set(setting.Name, setting.Min + fraction * (setting.Max - (double)setting.Min));
                        SaveImageValue(setting.Name);
                    }
                });
                editor.Automatic.CheckedChanged += (sender, args) =>
                {
                    if (refreshingVideo)
                        return;
                    Run(() =>
                    {
                        if (!TryGetVideoSetting(editor.Group, out var setting))
                            return;
                        camera.Set(setting.Name, camera.Get(setting.Name), editor.Automatic.Checked);
                        SaveImageValue(setting.Name);
                        UpdateVideoState(editor.Slider, editor.Automatic, setting);
                    });
                };
            }
            loadVideoControls.Click += (sender, args) => Run(NeedCamera);
            resetVideoControls.Click += (sender, args) => Run(ResetVideoControls);
            ShowVideoControls(false);
        }

        static IEnumerable<(GroupBox Group, TrackBar Slider, CheckBox Automatic)> VideoEditors(Control container)
        {
            foreach (Control child in container.Controls)
            {
                if (child is GroupBox group && group.Tag is string)
                    yield return (group, group.Controls.OfType<TrackBar>().Single(), group.Controls.OfType<CheckBox>().Single());
                else
                    foreach (var nested in VideoEditors(child))
                        yield return nested;
            }
        }

        bool TryGetVideoSetting(GroupBox editor, out Setting setting)
        {
            setting = null;
            return camera != null && editor.Tag is string name
                && camera.Settings.TryGetValue(name, out setting) && (setting.Caps & 3) != 0;
        }

        void BuildImageControls()
        {
            var selected = imageCategories.SelectedTab;
            var supportedTabs = new List<TabPage>();
            refreshingVideo = true;
            try
            {
                foreach (var tab in videoTabs)
                {
                    bool available = false;
                    foreach (var editor in VideoEditors(tab))
                    {
                        bool supported = TryGetVideoSetting(editor.Group, out var setting);
                        editor.Group.Visible = supported;
                        if (!supported)
                            continue;
                        available = true;
                        int value = camera.Get(setting.Name, out int flags);
                        double fraction = (value - (double)setting.Min) / (setting.Max - (double)setting.Min);
                        var slider = editor.Slider;
                        slider.Value = slider.Minimum + (int)(Math.Max(0, Math.Min(1, fraction)) * (slider.Maximum - slider.Minimum));
                        editor.Automatic.Visible = (setting.Caps & 1) != 0;
                        editor.Automatic.Checked = (flags & 1) != 0;
                        UpdateVideoState(editor.Slider, editor.Automatic, setting);
                    }
                    if (available)
                        supportedTabs.Add(tab);
                }
                if (!imageCategories.TabPages.Cast<TabPage>().SequenceEqual(supportedTabs))
                {
                    imageCategories.TabPages.Clear();
                    imageCategories.TabPages.AddRange(supportedTabs.ToArray());
                    if (supportedTabs.Contains(selected))
                        imageCategories.SelectedTab = selected;
                }
                ShowVideoControls(supportedTabs.Count != 0);
            }
            finally
            {
                refreshingVideo = false;
            }
        }

        static void UpdateVideoState(TrackBar slider, CheckBox automatic, Setting setting)
        {
            bool allowsManual = (setting.Caps & 2) != 0;
            slider.Enabled = allowsManual && !automatic.Checked;
            automatic.Enabled = allowsManual || !automatic.Checked;
        }

        void ShowVideoControls(bool available)
        {
            imagePages.Visible = available;
            videoStatus.Visible = !available;
            loadVideoControls.Visible = camera == null;
            noVideoControls.Visible = camera != null && !available;
        }

        void ResetVideoControls()
        {
            NeedCamera();
            foreach (var editor in videoTabs.SelectMany(VideoEditors))
            {
                if (!TryGetVideoSetting(editor.Group, out var setting))
                    continue;
                camera.Set(setting.Name, setting.Default, (setting.Caps & 2) == 0);
                SaveImageValue(setting.Name);
            }
            BuildImageControls();
        }
    }
}
