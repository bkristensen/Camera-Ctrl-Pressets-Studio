using System;
using System.Drawing;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public sealed class BlueTabControl : TabControl
    {
        public BlueTabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Parent?.BackColor ?? BlueTheme.Background))
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (TabCount == 0)
                return;

            var tabColor = SystemInformation.HighContrast ? SystemColors.Control : MainViewSplitter.LineColor;
            var textColor = SystemInformation.HighContrast ? SystemColors.ControlText : Color.White;
            var frame = DisplayRectangle;
            frame.Inflate(1, 1);
            var flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix;
            if (RightToLeft == RightToLeft.Yes)
                flags |= TextFormatFlags.RightToLeft;

            using (var border = new Pen(tabColor))
            using (var fill = new SolidBrush(tabColor))
            using (var selection = new SolidBrush(textColor))
            {
                e.Graphics.DrawRectangle(border, frame.X, frame.Y, frame.Width - 1, frame.Height - 1);
                for (int index = 0; index < TabCount; index++)
                {
                    var tab = GetTabRect(index);
                    tab.Inflate(-1, 0);
                    if (tab.Width <= 0 || !tab.IntersectsWith(e.ClipRectangle))
                        continue;

                    e.Graphics.FillRectangle(fill, tab);
                    TextRenderer.DrawText(e.Graphics, TabPages[index].Text, Font, tab, textColor, flags);
                    if (index != SelectedIndex)
                        continue;

                    int indicator = Math.Max(2, 2 * DeviceDpi / 96);
                    e.Graphics.FillRectangle(selection, tab.Left, tab.Bottom - indicator, tab.Width, indicator);
                    if (Focused && ShowFocusCues)
                    {
                        tab.Inflate(-3, -3);
                        ControlPaint.DrawFocusRectangle(e.Graphics, tab, textColor, tabColor);
                    }
                }
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }
    }
}
