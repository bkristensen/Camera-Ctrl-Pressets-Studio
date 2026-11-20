using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        TableLayoutPanel imagePages;
        TabControl imageCategories;

        void OrganizeSettings()
        {
            var categories = new TabControl { Dock = DockStyle.Fill };
            var motion = new TabPage(UiText.T("Movement", "Bevægelse")) { BackColor = BlueTheme.Background };
            var app = new TabPage(UiText.T("Application", "Program")) { BackColor = BlueTheme.Background };
            generalTab.Controls.Add(categories);
            categories.TabPages.AddRange(new[] { motion, app });
            var movementGroup = SettingsGroup(UiText.T("Pan, tilt and zoom", "Panorering, vipning og zoom"));
            movementGroup.Dock = DockStyle.Fill;
            motion.Controls.Add(movementGroup);
            movementGroup.Controls.Add(generalFields);
            foreach (RowStyle row in generalFields.RowStyles)
                if (row.SizeType == SizeType.Absolute)
                    row.Height = 30;

            var preferences = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, AutoScroll = true, Padding = new Padding(4)
            };
            var behaviour = SettingsGroup(UiText.T("Window and streaming", "Vindue og streaming"));
            var languageGroup = SettingsGroup(UiText.T("Profile language", "Profilsprog"));
            var behaviourRows = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            behaviourRows.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            behaviourRows.RowStyles.Add(new RowStyle(SizeType.Absolute, 29));
            behaviourRows.Controls.Add(streamingCheck, 0, 0);
            behaviourRows.Controls.Add(alwaysTop, 0, 1);
            behaviour.Controls.Add(behaviourRows);
            behaviour.Dock = DockStyle.Fill;
            languageGroup.Dock = DockStyle.Fill;
            languageGroup.Controls.Add(language);
            language.Dock = DockStyle.Top;
            generalFields.Controls.Remove(languageLabel);
            languageLabel.Dispose();
            preferences.RowStyles.Add(new RowStyle(SizeType.Absolute, 114));
            preferences.RowStyles.Add(new RowStyle(SizeType.Absolute, 61));
            preferences.Controls.Add(behaviour, 0, 0);
            preferences.Controls.Add(languageGroup, 0, 1);
            app.Controls.Add(preferences);
            generalFields.RowCount = 7;
            while (generalFields.RowStyles.Count > generalFields.RowCount)
                generalFields.RowStyles.RemoveAt(generalFields.RowStyles.Count - 1);

            var reporting = new TabPage(UiText.T("Issues", "Fejlrapport")) { BackColor = BlueTheme.Background };
            var reportActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true
            };
            var logGroup = SettingsGroup(UiText.T("Diagnostic logs", "Diagnoselogfiler"));
            logGroup.Controls.Add(SettingsActions(
                SettingsLink(UiText.T("Log settings", "Logindstillinger"), () => LoggingDialog.Show(this)),
                SettingsLink(UiText.T("Show log file", "Vis logfil"), () => ErrorDialog.OpenLog(this, DiagnosticLog.Current.FindLatest()))));
            var reportGroup = SettingsGroup(UiText.T("Report a problem", "Rapportér et problem"));
            reportGroup.Height = 124;
            reportGroup.Controls.Add(SettingsActions(
                new Label { AutoSize = true, MaximumSize = new Size(246, 0), Text = UiText.T(
                    "Review the exported logs and attach them to your GitHub issue.",
                    "Gennemgå de eksporterede logfiler, og vedhæft dem til din fejlrapport på GitHub.") },
                SettingsLink(UiText.T("Report on GitHub", "Rapportér på GitHub"), () => ErrorDialog.Report(this, DiagnosticLog.Current.FindLatest()))));
            reportActions.Controls.AddRange(new Control[] { logGroup, reportGroup });
            reporting.Controls.Add(reportActions);
            settingsTabs.TabPages.Add(reporting);

            foreach (Control item in aboutPanel.Controls)
                item.Margin = new Padding(5, 3, 5, 3);
            aboutPanel.Controls.Add(SettingsLink(UiText.T("GitHub releases", "Udgivelser på GitHub"),
                () => GuideViewer.OpenBrowser(GuideViewer.ReleasesUrl)));
            aboutPanel.Controls.Add(SettingsLink(UiText.T("User guides on GitHub", "Brugervejledninger på GitHub"),
                () => GuideViewer.OpenBrowser(GuideViewer.OnlineGuidesUrl)));
            var guide = new BlueButton
            {
                Text = UiText.T("Open guide viewer", "Åbn vejledningsviser"), Primary = false, Size = new Size(225, 30)
            };
            guide.Click += (sender, args) => Run(OpenGuide);
            aboutPanel.Controls.Add(guide);

            helpButton.Click += (sender, args) => Run(OpenGuide);
            SetMainToolTip(helpButton, UiText.T("User guides", "Brugervejledninger"));
        }

        void OpenGuide()
        {
            using (var viewer = new GuideViewer(settingsTabs.Visible))
                viewer.ShowDialog(this);
        }

        static GroupBox SettingsGroup(string title) => new GroupBox
        {
            Text = title, Padding = new Padding(5, 4, 5, 4), ForeColor = Color.White,
            Size = new Size(270, 95)
        };

        static FlowLayoutPanel SettingsActions(params Control[] controls)
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false
            };
            panel.Controls.AddRange(controls);
            return panel;
        }

        LinkLabel SettingsLink(string title, Action action)
        {
            var link = new LinkLabel
            {
                Text = title, AutoSize = true, MaximumSize = new Size(250, 0), LinkColor = Color.LightSkyBlue,
                VisitedLinkColor = Color.LightSkyBlue, Margin = new Padding(5, 3, 5, 3)
            };
            link.LinkClicked += (sender, args) => Run(action);
            return link;
        }

        static int ImageCategory(string name)
        {
            switch (name)
            {
                case "Focus":
                case "Exposure":
                case "Gain":
                    return 0;
                case "Hue":
                case "Saturation":
                case "White balance":
                case "Gamma":
                    return 1;
                default:
                    return 2;
            }
        }

        Dictionary<int, Panel> CreateImagePages(Setting[] settings)
        {
            string selectedPage = imageCategories?.SelectedTab?.Name;
            ResetImagePages();
            while (imageFields.Controls.Count != 0)
                imageFields.Controls[0].Dispose();
            var result = new Dictionary<int, Panel>();
            if (settings.Length == 0)
            {
                imageFields.Controls.Add(new Label
                {
                    Text = UiText.T("No image controls available through the driver.", "Driveren tilbyder ingen billedindstillinger."),
                    AutoSize = true, MaximumSize = new Size(260, 0)
                });
                return result;
            }
            imageFields.Hide();
            imagePages = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            imagePages.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            imagePages.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            imageCategories = new TabControl { Dock = DockStyle.Fill };
            var titles = new[] { UiText.T("Focus and exposure", "Fokus og eksponering"), UiText.T("Colour", "Farve"), UiText.T("Light and detail", "Lys og detaljer") };
            foreach (var category in settings.Select(setting => ImageCategory(setting.Name)).Distinct().OrderBy(index => index))
            {
                var tab = new TabPage(titles[category]) { Name = category.ToString(), BackColor = BlueTheme.Background };
                var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
                result.Add(category, panel);
                tab.Controls.Add(panel);
                imageCategories.TabPages.Add(tab);
            }
            if (selectedPage != null && imageCategories.TabPages.ContainsKey(selectedPage))
                imageCategories.SelectTab(selectedPage);
            var reset = new BlueButton { Text = UiText.T("Reset image to driver defaults", "Gendan driverens billedstandarder"), Dock = DockStyle.Fill };
            reset.Click += (sender, args) => Run(() =>
            {
                NeedCamera();
                foreach (var setting in settings)
                {
                    camera.Set(setting.Name, setting.Default, (setting.Caps & 2) == 0);
                    SaveImageValue(setting.Name);
                }
                BuildImageControls();
            });
            imagePages.Controls.Add(imageCategories, 0, 0);
            imagePages.Controls.Add(reset, 0, 1);
            imageTab.Controls.Add(imagePages);
            return result;
        }

        void ResetImagePages()
        {
            imagePages?.Dispose();
            imagePages = null;
            imageCategories = null;
            imageFields.Show();
        }
    }
}
