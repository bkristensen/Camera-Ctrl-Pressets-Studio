using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public static class BlueTheme
    {
        public static readonly Color Background = Color.FromArgb(5, 40, 79), Panel = Color.FromArgb(9, 52, 94), Line = Color.FromArgb(73, 144, 214);
        public static GraphicsPath Round(RectangleF r, float radius)
        {
            var p = new GraphicsPath();
            float d = radius * 2;
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        public static void Apply(Control c)
        {
            c.ForeColor = Color.White;
            c.BackColor = Panel;
            if (c is Button b)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderColor = Line;
                b.BackColor = Color.FromArgb(20, 79, 136);
            }

            if (c is TextBoxBase || c is ListBox || c is ComboBox || c is NumericUpDown)
                c.BackColor = Background;
            foreach (Control child in c.Controls)
                Apply(child);
        }
    }

    public class BluePanel : Panel
    {
        public BluePanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = BlueTheme.Panel;
            Padding = new Padding(14);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = BlueTheme.Round(new RectangleF(1, 1, Width - 3, Height - 3), 14))
            using (var pen = new Pen(BlueTheme.Line, 1.5f))
                e.Graphics.DrawPath(pen, path);
        }
    }

    public class BlueButton : Button
    {
        protected virtual Image DisplayImage => Image;

        bool over, down;
        public bool Primary { get; set; }

        public BlueButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            ForeColor = Color.White;
            BackColor = BlueTheme.Panel;
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            over = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            over = false;
            down = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            down = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            down = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            over = down = false;
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Parent == null ? BackColor : Parent.BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new RectangleF(2, 2, Width - 5, Height - 5);
            if (r.Width <= 0 || r.Height <= 0)
                return;
            bool highlighted = Enabled && (Primary || over);
            var top = !Enabled ? Color.FromArgb(31, 58, 86) : down ? Color.FromArgb(0, 65, 125)
                : highlighted ? Color.FromArgb(12, 112, 190) : Color.FromArgb(39, 106, 175);
            var border = !Enabled ? Color.FromArgb(52, 75, 101) : highlighted ? Color.FromArgb(80, 219, 255) : BlueTheme.Line;
            using (var p = BlueTheme.Round(r, 10))
            using (var brush = new LinearGradientBrush(r, top, Color.FromArgb(6, 46, 88), 90))
            using (var pen = new Pen(border, 1.5f))
            {
                g.FillPath(brush, p);
                g.DrawPath(pen, p);
            }

            var textColor = Enabled ? ForeColor : Color.FromArgb(176, 193, 211);
            var image = DisplayImage;
            if (image != null)
                DrawButtonImage(g, image, Rectangle.Round(r));
            else if (Text == "+" || Text == "−" || Text == "++" || Text == "−−")
            {
                int count = Text.Length;
                float arm = Math.Min(7, Math.Min(r.Height / 4, (r.Width - 8) / (count * 3))), gap = arm * 2.8f, cx = r.X + r.Width / 2, cy = r.Y + r.Height / 2;
                using (var glyph = new Pen(textColor, 2.5f))
                {
                    glyph.StartCap = glyph.EndCap = LineCap.Round;
                    for (int n = 0; n < count; n++)
                    {
                        float x = cx + (n - (count - 1) / 2f) * gap;
                        g.DrawLine(glyph, x - arm, cy, x + arm, cy);
                        if (Text[0] == '+')
                            g.DrawLine(glyph, x, cy - arm, x, cy + arm);
                    }
                }
            }
            else
            {
                var flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;
                if (RightToLeft == RightToLeft.Yes)
                    flags |= TextFormatFlags.RightToLeft;
                TextRenderer.DrawText(g, Text, Font, Rectangle.Round(r), textColor, flags);
            }

            if (Focused)
                ControlPaint.DrawFocusRectangle(g, new Rectangle(7, 7, Width - 15, Height - 15));
        }

        void DrawButtonImage(Graphics graphics, Image image, Rectangle bounds)
        {
            bounds.Inflate(-4, -4);
            if (bounds.Width <= 0 || bounds.Height <= 0)
                return;
            double scale = Math.Min((double)bounds.Width / image.Width, (double)bounds.Height / image.Height);
            int width = Math.Max(1, (int)Math.Round(image.Width * scale));
            int height = Math.Max(1, (int)Math.Round(image.Height * scale));
            var target = new Rectangle(bounds.X + (bounds.Width - width) / 2,
                bounds.Y + (bounds.Height - height) / 2, width, height);
            var state = graphics.Save();
            try
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                using (var attributes = new ImageAttributes())
                {
                    attributes.SetWrapMode(WrapMode.TileFlipXY);
                    if (!Enabled)
                        attributes.SetColorMatrix(new ColorMatrix { Matrix33 = .45f });
                    graphics.DrawImage(image, target, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            finally
            {
                graphics.Restore(state);
            }
        }
    }

    public sealed class PanWheel : Control
    {
        public event Action<string, int, bool> MovementRequested;
        public event Action Released;
        public event Action SourceRequested;
        readonly bool[] available =
        {
            true,
            true,
            true,
            true
        };
        int active = -1;
        public PanWheel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = BlueTheme.Background;
            AccessibleName = UiText.T("Camera direction wheel. Inner ring normal, outer ring fast.", "Kamerahjul. Inderste ring normal, yderste ring hurtig.");
            TabStop = true;
        }

        internal void SetDirectionAvailability(string axis, bool negative, bool positive)
        {
            int minus = axis == "Pan" ? 2 : 1;
            int plus = axis == "Pan" ? 0 : 3;
            if (available[minus] == negative && available[plus] == positive)
                return;
            available[minus] = negative;
            available[plus] = positive;
            if (active >= 0 && !available[active % 4])
                Release();
            else
                Invalidate();
        }

        int Hit(Point p)
        {
            float size = Math.Min(Width, Height) - 24, cx = Width / 2f, cy = Height / 2f, dx = p.X - cx, dy = p.Y - cy, r = (float)Math.Sqrt(dx * dx + dy * dy);
            if (r < size * .18 || r > size * .5)
                return -1;
            int direction = ((int)Math.Floor((Math.Atan2(dy, dx) * 180 / Math.PI + 405) / 90)) % 4;
            return direction + (r > size * .315 ? 4 : 0);
        }

        void Trigger(int n)
        {
            if (n < 0 || !available[n % 4])
                return;
            MovementRequested?.Invoke(n % 4 == 0 || n % 4 == 2 ? "Pan" : "Tilt", n % 4 == 0 || n % 4 == 3 ? 1 : -1, n >= 4);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || !Enabled)
                return;
            Focus();
            float dx = e.X - Width / 2f, dy = e.Y - Height / 2f;
            if (Math.Sqrt(dx * dx + dy * dy) < (Math.Min(Width, Height) - 24) * .16)
            {
                SourceRequested?.Invoke();
                return;
            }

            int hit = Hit(e.Location);
            if (hit >= 0 && available[hit % 4])
            {
                active = hit;
                Capture = true;
                Trigger(active);
            }

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Release();
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);
            if (!Capture)
                Release();
        }

        void Release()
        {
            active = -1;
            Capture = false;
            Released?.Invoke();
            Invalidate();
        }

        protected override bool IsInputKey(Keys key)
        {
            var code = key & Keys.KeyCode;
            return code == Keys.Up || code == Keys.Down || code == Keys.Left || code == Keys.Right || base.IsInputKey(key);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                SourceRequested?.Invoke();
                e.Handled = true;
                return;
            }

            int n = e.KeyCode == Keys.Right ? 0 : e.KeyCode == Keys.Down ? 1 : e.KeyCode == Keys.Left ? 2 : e.KeyCode == Keys.Up ? 3 : -1;
            if (n >= 0)
            {
                Trigger(n + (e.Shift ? 4 : 0));
                e.Handled = true;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            Released?.Invoke();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float size = Math.Min(Width, Height) - 24, cx = Width / 2f, cy = Height / 2f;
            if (size < 60)
                return;
            for (int ring = 0; ring < 2; ring++)
                for (int dir = 0; dir < 4; dir++)
                {
                    bool enabled = Enabled && available[dir];
                    float outer = size * (ring == 0 ? .305f : .5f), inner = size * (ring == 0 ? .18f : .325f);
                    float angle = dir * 90 - 43;
                    using (var p = new GraphicsPath())
                    {
                        p.AddArc(cx - outer, cy - outer, outer * 2, outer * 2, angle, 86);
                        p.AddArc(cx - inner, cy - inner, inner * 2, inner * 2, angle + 86, -86);
                        p.CloseFigure();
                        var r = p.GetBounds();
                        bool selected = active == dir + ring * 4;
                        var top = !enabled ? Color.FromArgb(31, 58, 86) : selected ? Color.FromArgb(65, 210, 255)
                        : ring == 1 ? Color.FromArgb(0, 142, 238) : Color.FromArgb(38, 100, 160);
                        using (var b = new LinearGradientBrush(r, top, Color.FromArgb(3, 44, 85), 90))
                            g.FillPath(b, p);
                        if (enabled && ring == 1)
                            using (var glow = new Pen(Color.FromArgb(40, 0, 200, 255), 8))
                                g.DrawPath(glow, p);
                        using (var pen = new Pen(!enabled ? Color.FromArgb(52, 75, 101) : ring == 1 ? Color.FromArgb(92, 222, 255) : BlueTheme.Line, 2))
                            g.DrawPath(pen, p);
                    }

                    float radius = (outer + inner) / 2;
                    var state = g.Save();
                    g.TranslateTransform(cx + (float)Math.Cos(dir * Math.PI / 2) * radius, cy + (float)Math.Sin(dir * Math.PI / 2) * radius);
                    g.RotateTransform(dir * 90);
                    using (var pen = new Pen(enabled ? Color.White : Color.SlateGray, Math.Max(3, size / 75)))
                    {
                        pen.StartCap = pen.EndCap = LineCap.Round;
                        float a = size * .025f;
                        for (int n = 0; n < (ring == 1 ? 2 : 1); n++)
                        {
                            float x = (n - (ring == 1 ? .5f : 0)) * a * 1.5f;
                            g.DrawLines(pen, new[] { new PointF(x - a, -a * 1.5f), new PointF(x + a, 0), new PointF(x - a, a * 1.5f) });
                        }
                    }

                    g.Restore(state);
                }

            float center = size * .16f;
            using (var b = new LinearGradientBrush(new RectangleF(cx - center, cy - center, center * 2, center * 2), Color.FromArgb(23, 101, 173), Color.FromArgb(1, 28, 59), 90))
                g.FillEllipse(b, cx - center, cy - center, center * 2, center * 2);
            using (var p = new Pen(BlueTheme.Line, 2))
                g.DrawEllipse(p, cx - center, cy - center, center * 2, center * 2);
            using (var p = BlueTheme.Round(new RectangleF(cx - size * .07f, cy - size * .04f, size * .105f, size * .08f), 4))
                g.FillPath(Brushes.White, p);
            g.FillPolygon(Brushes.White, new[] { new PointF(cx + size * .045f, cy - size * .025f), new PointF(cx + size * .08f, cy - size * .04f), new PointF(cx + size * .08f, cy + size * .04f), new PointF(cx + size * .045f, cy + size * .025f) });
        }
    }
}
