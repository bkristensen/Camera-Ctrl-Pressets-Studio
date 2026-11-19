using System;
using System.Drawing;
using System.Windows.Forms;
namespace CameraControlStudio {
    internal static class ProfileWindow {
        public static readonly Size MinimumSize = new Size(670, 450);

        public static void Apply(Form form, ProfileLayout layout) {
            form.WindowState = FormWindowState.Normal;
            var desired = layout.HasBounds
                ? new Rectangle(layout.X, layout.Y, Math.Max(MinimumSize.Width, layout.Width), Math.Max(MinimumSize.Height, layout.Height))
                : new Rectangle(form.Location, new Size(900, 500));
            var work = Screen.FromRectangle(desired).WorkingArea;
            desired.Width = Math.Min(desired.Width, work.Width);
            desired.Height = Math.Min(desired.Height, work.Height);
            desired.X = Math.Max(work.Left, Math.Min(desired.X, work.Right - desired.Width));
            desired.Y = Math.Max(work.Top, Math.Min(desired.Y, work.Bottom - desired.Height));
            form.MinimumSize = new Size(Math.Min(MinimumSize.Width, work.Width), Math.Min(MinimumSize.Height, work.Height));
            form.Bounds = desired;
            if (layout.Maximized)
                form.WindowState = FormWindowState.Maximized;
        }
        public static void Capture(Form form, ProfileLayout layout, bool maximized) {
            var bounds = form.WindowState == FormWindowState.Normal ? form.Bounds : form.RestoreBounds;
            layout.X = bounds.X;
            layout.Y = bounds.Y;
            layout.Width = bounds.Width;
            layout.Height = bounds.Height;
            layout.Maximized = maximized;
            layout.HasBounds = true;
        }
    }
}
