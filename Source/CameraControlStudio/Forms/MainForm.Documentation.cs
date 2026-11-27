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
            settings.startupFov.Enabled = settings.allowFov.Enabled = settings.adjustFovAtLimits.Enabled = true;
            UpdateMotionAxis("Zoom", new Setting { Min = 100, Max = 300, Step = 1 }, 160);
            currentAxes["Pan"] = currentAxes["Tilt"] = 0;
            SetMotionAvailability("Pan", true, true);
            SetMotionAvailability("Tilt", true, true);
            RefreshPositionText();
            fieldOfViewLabel.Text = UiText.T("Field of view", "Synsfelt") + " 78°";
            fullFrame.Enabled = true;

            settings.profilesList.Items.Add(ProfileStore.Standard);
            settings.profilesList.SelectedIndex = 0;
            profileButton.Text = ProfileStore.Standard + " ▾";
            settings.language.SelectedIndex = Array.IndexOf(UiText.LanguageCodes, UiText.Language);
            foreach (var editor in videoTabs.SelectMany(VideoEditors))
            {
                editor.Slider.Maximum = 100;
                editor.Slider.Value = 45;
            }
            cameraStatus.Text = UiText.T("Camera", "Kamera") + ": Logitech BRIO";
            status.Text = UiText.T("Ready", "Klar");
            settings.versionLabel.Text = "MIT · " + DiagnosticLog.DisplayAppVersion;
            RefreshPresets();
            mainToolTips.Active = false;

            var area = Screen.FromControl(this).WorkingArea;
            StartPosition = FormStartPosition.Manual;
            SetBounds(area.Left + 12, area.Top + 12,
                Math.Min(1040, area.Width - 24), Math.Min(660, area.Height - 24));
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
                    settings.ShowFor(this);
                    settings.settingsTabs.SelectedTab = settings.presetsTab;
                    settings.permanentList.SelectedIndex = 0;
                }),
                ("settings", () => { settings.settingsTabs.SelectedTab = settings.generalTab; settings.settingsCategories.SelectedTab = settings.motionTab; }),
                ("zoom", () => settings.settingsCategories.SelectedTab = settings.zoomTab),
                ("transitions", () => { settings.settingsTabs.SelectedTab = settings.presetsTab; settings.presetCategories.SelectedTab = settings.transitionsTab; }),
                ("application", () => { settings.settingsTabs.SelectedTab = settings.generalTab; settings.settingsCategories.SelectedTab = settings.applicationTab; })
            };
            if (UiText.Language == "da")
                stages.Insert(1, ("quick-guide", () => { }));
            if (UiText.Language == "en" || UiText.Language == "da" || UiText.Language == "de")
            {
                stages.AddRange(new (string, Action)[]
                {
                    ("main-settings", () => { settings.settingsTabs.SelectedTab = settings.presetsTab; settings.presetCategories.SelectedTab = settings.permanentPresetsTab; settings.permanentList.ClearSelected(); }),
                    ("video", () => settings.settingsTabs.SelectedTab = settings.imageTab),
                    ("image", () => { SetVideoTabs(videoTabs); settings.imageCategories.SelectedTab = settings.focusTab; }),
                    ("video-colour", () => settings.imageCategories.SelectedTab = settings.colourTab),
                    ("video-light-detail", () => settings.imageCategories.SelectedTab = settings.lightTab),
                    ("field-of-view", () => settings.imageCategories.SelectedTab = settings.fieldOfViewTab),
                    ("profiles", () => settings.settingsTabs.SelectedTab = settings.profilesTab),
                    ("issues", () => { settings.settingsTabs.SelectedTab = settings.aboutTab; settings.contactCategories.SelectedTab = settings.contactTab; }),
                    ("about", () => settings.contactCategories.SelectedTab = settings.aboutInfoTab)
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
            bool includeSettings = name != "main" && name != "quick-guide" && settings.Visible;
            using (var bitmap = CaptureWindows(includeSettings))
            using (var graphics = Graphics.FromImage(bitmap))
            {
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

        int SettingsCaptureOffset => Width + 8;

        Bitmap CaptureWindows(bool includeSettings)
        {
            int width = includeSettings ? SettingsCaptureOffset + settings.Width : Width;
            int height = includeSettings ? Math.Max(Height, settings.Height) : Height;
            var result = new Bitmap(width, height);
            try
            {
                DrawToBitmap(result, new Rectangle(Point.Empty, Size));
                if (includeSettings)
                {
                    using (var settingsImage = new Bitmap(settings.Width, settings.Height))
                    using (var graphics = Graphics.FromImage(result))
                    {
                        settings.DrawToBitmap(settingsImage, new Rectangle(Point.Empty, settings.Size));
                        graphics.DrawImageUnscaled(settingsImage, SettingsCaptureOffset, 0);
                    }
                }
                return result;
            }
            catch
            {
                result.Dispose();
                throw;
            }
        }

        void DrawDocumentationLanguage(Graphics graphics)
        {
            // WinForms omits the owner-drawn combo selection when it renders off screen.
            var origin = settings.language.PointToScreen(Point.Empty) - new Size(settings.Left, settings.Top);
            origin.X += SettingsCaptureOffset;
            var bounds = new Rectangle(origin, settings.language.ClientSize);
            bounds.Inflate(-2, -2);
            int arrowWidth = SystemInformation.VerticalScrollBarWidth;
            if (settings.language.RightToLeft == RightToLeft.Yes)
                bounds.X += arrowWidth;
            bounds.Width -= arrowWidth;
            DrawComboItem(settings.language, new DrawItemEventArgs(graphics, settings.language.Font, bounds, settings.language.SelectedIndex,
                DrawItemState.ComboBoxEdit, settings.language.ForeColor, settings.language.BackColor));
        }
    }
}
