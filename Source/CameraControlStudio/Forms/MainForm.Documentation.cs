using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        // Documentation mode uses sample values without opening a camera or changing saved profiles.
        bool documentationMode;
        string documentationDirectory;

        internal MainForm(string outputDirectory) : this()
        {
            documentationMode = true;
            documentationDirectory = Path.GetFullPath(outputDirectory);
        }

        void InitializeDocumentation()
        {
            config = new SavedData { CameraId = "documentation-only", UseTransitionSpeed = true };
            var examples = new (string Icon, string Name)[]
            {
                ("combo:table+lectern+cafe-table", "Whole stage"),
                ("lectern", "Lectern"),
                ("person", "Speaker close-up"),
                ("table", "Left table"),
                ("cafe-table", "Right café table"),
                ("combo:lectern+cafe-table", "Lectern + café table"),
                ("combo:table+lectern", "Table + lectern")
            };
            config.Cameras[config.CameraId] = examples.Select(example => new Preset
            {
                IconId = example.Icon,
                Name = UiText.Translate("Preset example: " + example.Name, example.Name)
            }).ToList();
            LoadMovementSettings();
            LoadFramingSettings();
            UpdateFieldOfViewOptions(new[] { 90, 78, 65 });
            startupFov.Enabled = allowFov.Enabled = adjustFovAtLimits.Enabled = true;
            UpdateMotionAxis("Zoom", new Setting { Min = 100, Max = 300, Step = 1 }, 160);
            currentAxes["Pan"] = currentAxes["Tilt"] = 0;
            SetMotionAvailability("Pan", true, true);
            SetMotionAvailability("Tilt", true, true);
            RefreshPositionText();
            fieldOfViewLabel.Text = UiText.T("Field of view", "Synsfelt") + " 78°";
            fullFrame.Enabled = true;

            profilesList.Items.Add(ProfileStore.Standard);
            profilesList.SelectedIndex = 0;
            profileButton.Text = ProfileStore.Standard + " ▾";
            language.SelectedIndex = Array.IndexOf(UiText.LanguageCodes, UiText.Language);
            foreach (var editor in videoTabs.SelectMany(VideoEditors))
            {
                editor.Slider.Maximum = 100;
                editor.Slider.Value = 45;
            }
            cameraStatus.Text = UiText.T("Camera", "Kamera") + ": Logitech BRIO";
            status.Text = UiText.T("Ready", "Klar");
            versionLabel.Text = "MIT · " + DiagnosticLog.DisplayAppVersion;
            RefreshPresets();
            mainToolTips.Active = false;

            var area = Screen.FromControl(this).WorkingArea;
            StartPosition = FormStartPosition.Manual;
            SetBounds(area.Left + 12, area.Top + 12,
                Math.Min(1040, area.Width - settingsPanelWidth - 24), Math.Min(660, area.Height - 24));
            config.Layout.ControllerRatio = LayoutPolicy.DefaultControllerRatio;
            ApplySplitter();
            Directory.CreateDirectory(documentationDirectory);
            RunDocumentationStages();
        }

        void RunDocumentationStages()
        {
            var stages = new List<(string Name, Action Show)>
            {
                ("main", () => { }),
                ("presets", () =>
                {
                    Height = Math.Min(750, Screen.FromControl(this).WorkingArea.Height - 24);
                    SetSettings(true, true);
                    settingsTabs.SelectedTab = presetsTab;
                    permanentList.SelectedIndex = 0;
                }),
                ("settings", () => { settingsTabs.SelectedTab = generalTab; settingsCategories.SelectedTab = motionTab; }),
                ("zoom", () => settingsCategories.SelectedTab = zoomTab),
                ("transitions", () => { settingsTabs.SelectedTab = presetsTab; presetCategories.SelectedTab = transitionsTab; }),
                ("application", () => { settingsTabs.SelectedTab = generalTab; settingsCategories.SelectedTab = applicationTab; })
            };
            if (UiText.Language == "da")
                stages.Insert(1, ("quick-guide", () => { }));
            if (UiText.Language == "en" || UiText.Language == "da" || UiText.Language == "de")
            {
                stages.AddRange(new (string, Action)[]
                {
                    ("main-settings", () => { settingsTabs.SelectedTab = presetsTab; presetCategories.SelectedTab = permanentPresetsTab; permanentList.ClearSelected(); }),
                    ("video", () => settingsTabs.SelectedTab = imageTab),
                    ("image", () => { SetVideoTabs(videoTabs); imageCategories.SelectedTab = focusTab; }),
                    ("video-colour", () => imageCategories.SelectedTab = colourTab),
                    ("video-light-detail", () => imageCategories.SelectedTab = lightTab),
                    ("field-of-view", () => imageCategories.SelectedTab = fieldOfViewTab),
                    ("profiles", () => settingsTabs.SelectedTab = profilesTab),
                    ("issues", () => { settingsTabs.SelectedTab = aboutTab; contactCategories.SelectedTab = contactTab; }),
                    ("about", () => contactCategories.SelectedTab = aboutInfoTab)
                });
            }

            int stage = 0;
            stages[stage].Show();
            documentationTimer.Tick += (sender, args) =>
            {
                try
                {
                    CaptureDocumentation(stages[stage].Name);
                    stage++;
                    if (stage < stages.Count)
                    {
                        stages[stage].Show();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    DiagnosticLog.Current.Write("ERROR", "Capture documentation", ex);
                    Environment.ExitCode = 1;
                }
                documentationTimer.Stop();
                Close();
            };
            documentationTimer.Start();
        }

        void CaptureDocumentation(string name)
        {
            using (var bitmap = new Bitmap(Width, Height))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                DrawToBitmap(bitmap, new Rectangle(Point.Empty, Size));
                if (name == "application")
                    DrawDocumentationLanguage(graphics);

                if (name == "quick-guide")
                {
                    using (var pen = new Pen(Color.Gold, 3))
                    {
                        foreach (Control control in new Control[] { wheel, normalIn, normalOut, presetGrid, helpButton })
                        {
                            var position = control.PointToScreen(Point.Empty) - new Size(Left, Top);
                            graphics.DrawRectangle(pen, new Rectangle(position, control.ClientSize));
                        }
                    }
                }
                bitmap.Save(Path.Combine(documentationDirectory, name + ".png"), ImageFormat.Png);
            }
        }

        void DrawDocumentationLanguage(Graphics graphics)
        {
            // WinForms omits the owner-drawn combo selection when it renders off screen.
            var bounds = new Rectangle(language.PointToScreen(Point.Empty) - new Size(Left, Top), language.ClientSize);
            bounds.Inflate(-2, -2);
            int arrowWidth = SystemInformation.VerticalScrollBarWidth;
            if (language.RightToLeft == RightToLeft.Yes)
                bounds.X += arrowWidth;
            bounds.Width -= arrowWidth;
            DrawComboItem(language, new DrawItemEventArgs(graphics, language.Font, bounds, language.SelectedIndex,
                DrawItemState.ComboBoxEdit, language.ForeColor, language.BackColor));
        }
    }
}
