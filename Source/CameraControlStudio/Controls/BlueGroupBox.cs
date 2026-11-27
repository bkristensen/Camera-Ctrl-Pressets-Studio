using System;
using System.Drawing;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public sealed class BlueGroupBox : GroupBox
    {
        public BlueGroupBox()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (SystemInformation.HighContrast)
            {
                base.OnPaint(e);
                return;
            }

            var bounds = ClientRectangle;
            if (bounds.Width < 2 || bounds.Height < 2)
                return;

            var blue = MainViewSplitter.LineColor;
            int captionHeight = Math.Min(bounds.Height, Font.Height);
            using (var fill = new SolidBrush(blue))
            using (var border = new Pen(blue))
            {
                e.Graphics.FillRectangle(fill, 0, 0, bounds.Width, captionHeight);
                e.Graphics.DrawRectangle(border, 0, 0, bounds.Width - 1, bounds.Height - 1);
            }

            int inset = Math.Min(8 * DeviceDpi / 96, (bounds.Width - 1) / 2);
            var caption = new Rectangle(inset, 0, bounds.Width - 2 * inset, captionHeight);
            var flags = TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            if (RightToLeft == RightToLeft.Yes)
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            if (!ShowKeyboardCues)
                flags |= TextFormatFlags.HidePrefix;
            TextRenderer.DrawText(e.Graphics, Text, Font, caption, Color.White, flags);
        }
    }
}
