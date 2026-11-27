using System;
using System.Drawing;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class SettingsForm : AppForm
    {
        readonly Size preferredMinimumSize;
        readonly Size initialSize;
        bool hasPlacement;
        bool wasMaximized;

        public SettingsForm()
        {
            InitializeComponent();
            if (InDesigner)
                return;

            preferredMinimumSize = MinimumSize;
            initialSize = Size;
            FormText.Translate(this);
            closeSettings.Click += (sender, args) => Close();
        }

        internal void ShowFor(Form owner)
        {
            if (Visible)
            {
                Activate();
                return;
            }

            presetCategories.SelectedIndex = 0;
            settingsCategories.SelectedIndex = 0;
            imageCategories.SelectedIndex = 0;
            contactCategories.SelectedIndex = 0;
            settingsTabs.SelectedIndex = 0;
            SetStatus("");
            if (!hasPlacement)
            {
                var work = Screen.FromControl(owner).WorkingArea;
                var size = new Size(Math.Min(initialSize.Width, work.Width), Math.Min(initialSize.Height, work.Height));
                int left = work.Right - owner.Right >= size.Width ? owner.Right : owner.Left - size.Width;
                left = Math.Max(work.Left, Math.Min(left, work.Right - size.Width));
                int top = Math.Max(work.Top, Math.Min(owner.Top, work.Bottom - size.Height));
                Bounds = new Rectangle(new Point(left, top), size);
                hasPlacement = true;
            }
            Show(owner);
        }

        internal void ApplyPlacement(ProfileLayout layout)
        {
            hasPlacement = layout?.HasBounds == true;
            wasMaximized = false;
            WindowState = FormWindowState.Normal;
            if (hasPlacement)
            {
                ProfileWindow.Apply(this, layout, preferredMinimumSize);
                wasMaximized = layout.Maximized;
            }
        }

        internal ProfileLayout CapturePlacement()
        {
            if (!hasPlacement)
                return null;

            var placement = new ProfileLayout();
            ProfileWindow.Capture(this, placement, wasMaximized);
            return placement;
        }

        internal void SetStatus(string message)
        {
            settingsStatus.Text = message;
            settingsStatus.AccessibleDescription = message;
        }

        protected override void OnResize(EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
                wasMaximized = WindowState == FormWindowState.Maximized;
            base.OnResize(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing && !e.Cancel)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
