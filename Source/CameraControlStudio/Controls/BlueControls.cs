using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
namespace CameraControlStudio {
    public static class BlueTheme {
        public static readonly Color Background = Color.FromArgb(5, 40, 79), Panel = Color.FromArgb(9, 52, 94), Line = Color.FromArgb(73, 144, 214);
        public static readonly Color DisabledText = Color.FromArgb(176, 193, 211);
        public static GraphicsPath Round(RectangleF r, float radius) {
            var p = new GraphicsPath();
            float d = radius * 2;
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }
        public static void Apply(Control c) {
            c.ForeColor = Color.White;
            c.BackColor = Panel;
            if (c is Button b) {
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
    public class BluePanel : Panel {
        public BluePanel() {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = BlueTheme.Panel;
            Padding = new Padding(14);
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = BlueTheme.Round(new RectangleF(1, 1, Width - 3, Height - 3), 14))
            using (var pen = new Pen(BlueTheme.Line, 1.5f))
                e.Graphics.DrawPath(pen, path);
        }
    }
    public class BlueCheckBox : CheckBox {
        protected override void OnPaint(PaintEventArgs e) {
            if (Enabled) {
                base.OnPaint(e);
                return;
            }

            // The standard button is opaque. This paint path must also erase its background.
            using (var background = new SolidBrush(BackColor))
                e.Graphics.FillRectangle(background, e.ClipRectangle);
            var state = CheckState == CheckState.Indeterminate ? CheckBoxState.MixedDisabled
                : Checked ? CheckBoxState.CheckedDisabled : CheckBoxState.UncheckedDisabled;
            var glyph = CheckBoxRenderer.GetGlyphSize(e.Graphics, state);
            bool rightToLeft = RightToLeft == RightToLeft.Yes;
            var textBounds = new Rectangle(Padding.Left, Padding.Top,
                ClientSize.Width - Padding.Horizontal, ClientSize.Height - Padding.Vertical);
            int glyphX = rightToLeft ? textBounds.Right - glyph.Width : textBounds.Left;
            int glyphY = textBounds.Top + (textBounds.Height - glyph.Height) / 2;
            CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(glyphX, glyphY), state);

            int space = glyph.Width + glyph.Width / 3;
            textBounds.Width -= space;
            if (textBounds.Width <= 0)
                return;
            var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak | TextFormatFlags.PreserveGraphicsClipping;
            if (rightToLeft)
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            else
                textBounds.X += space;
            if (!UseMnemonic)
                flags |= TextFormatFlags.NoPrefix;
            else if (!ShowKeyboardCues)
                flags |= TextFormatFlags.HidePrefix;
            TextRenderer.DrawText(e.Graphics, Text, Font, textBounds, BlueTheme.DisabledText, flags);
        }
    }
    public class BlueNumericUpDown : NumericUpDown {
        public BlueNumericUpDown() {
            BackColor = BlueTheme.Background;
            ForeColor = Color.White;
        }

        protected override void OnEnabledChanged(EventArgs e) {
            base.OnEnabledChanged(e);
            // The native edit control uses the system's gray text when disabled.
            BackColor = Enabled ? BlueTheme.Background : SystemColors.Window;
        }
    }
    public class BlueButton : Button {
        protected virtual Image DisplayImage => Image;
        bool over, down; public bool Primary {
            get; set;
        }
        public BlueButton() {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            ForeColor = Color.White;
            BackColor = BlueTheme.Panel;
            Cursor = Cursors.Hand;
        }
        protected override void OnMouseEnter(EventArgs e) {
            over = true;
            Invalidate();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e) {
            over = false;
            down = false;
            Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(MouseEventArgs e) {
            down = true;
            Invalidate();
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            down = false;
            Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnEnabledChanged(EventArgs e) {
            over = down = false;
            base.OnEnabledChanged(e);
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e) {
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
            using (var pen = new Pen(border, 1.5f)) {
                g.FillPath(brush, p);
                g.DrawPath(pen, p);
            }
            var textColor = Enabled ? ForeColor : BlueTheme.DisabledText;
            var image = DisplayImage;
            if (image != null)
                DrawButtonImage(g, image, Rectangle.Round(r));
            else if (Text == "+" || Text == "−" || Text == "++" || Text == "−−") {
                int count = Text.Length;
                float arm = Math.Min(7, Math.Min(r.Height / 4, (r.Width - 8) / (count * 3))), gap = arm * 2.8f, cx = r.X + r.Width / 2, cy = r.Y + r.Height / 2;
                using (var glyph = new Pen(textColor, 2.5f)) {
                    glyph.StartCap = glyph.EndCap = LineCap.Round;
                    for (int n = 0; n < count; n++) {
                        float x = cx + (n - (count - 1) / 2f) * gap;
                        g.DrawLine(glyph, x - arm, cy, x + arm, cy);
                        if (Text[0] == '+')
                            g.DrawLine(glyph, x, cy - arm, x, cy + arm);
                    }
                }
            } else {
                var flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;
                if (RightToLeft == RightToLeft.Yes)
                    flags |= TextFormatFlags.RightToLeft;
                TextRenderer.DrawText(g, Text, Font, Rectangle.Round(r), textColor, flags);
            }
            if (Focused)
                ControlPaint.DrawFocusRectangle(g, new Rectangle(7, 7, Width - 15, Height - 15));
        }

        void DrawButtonImage(Graphics graphics, Image image, Rectangle bounds) {
            bounds.Inflate(-4, -4);
            if (bounds.Width <= 0 || bounds.Height <= 0)
                return;

            double scale = Math.Min((double)bounds.Width / image.Width, (double)bounds.Height / image.Height);
            int width = Math.Max(1, (int)Math.Round(image.Width * scale));
            int height = Math.Max(1, (int)Math.Round(image.Height * scale));
            var target = new Rectangle(bounds.X + (bounds.Width - width) / 2,
                bounds.Y + (bounds.Height - height) / 2, width, height);

            var state = graphics.Save();
            try {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                using (var attributes = new ImageAttributes()) {
                    attributes.SetWrapMode(WrapMode.TileFlipXY);
                    if (!Enabled)
                        attributes.SetColorMatrix(new ColorMatrix { Matrix33 = .45f });
                    graphics.DrawImage(image, target, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            } finally {
                graphics.Restore(state);
            }
        }
    }
    public enum PanSpeed { Fine, Normal, Fast }

    public sealed class PanWheel : Control {
        static readonly float[] RingEdges = { .175f, .3375f, .5f };
        readonly bool[] available = { true, true, true, true };
        int active = -1;
        Keys activeKey;
        Point sourceOrigin;
        bool sourcePressed, sourceDragged, sourceHovered;
        bool previousSourceClick, sourceDoubleClick;

        internal bool PanWithCameraButton { get; set; } = true;
        internal bool DoubleClickForCameraSource { get; set; } = true;

        public event Action<int, int, PanSpeed> MovementRequested;
        public event Action<double, double> DragRequested;
        public event Action<bool> SourceHoverChanged;
        public event Action Released;
        public event Action SourceRequested;

        public PanWheel() {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            TabStop = true;
        }

        internal bool IsHoldingDirection => active >= 0 || activeKey != Keys.None;
        float WheelSize => Math.Min(Width, Height) - 24;

        internal void SetDirectionAvailability(string axis, bool negative, bool positive) {
            int index = axis == "Pan" ? 0 : 2;
            if (available[index] == negative && available[index + 1] == positive)
                return;
            available[index] = negative;
            available[index + 1] = positive;
            Invalidate();
        }

        bool CanMove(int sector) {
            var (pan, tilt) = PanMotion.Direction(sector);
            return CanMoveAxis("Pan", pan) || CanMoveAxis("Tilt", tilt);
        }

        internal bool CanMoveAxis(string axis, int direction) =>
            direction != 0 && available[(axis == "Pan" ? 0 : 2) + (direction < 0 ? 0 : 1)];

        bool IsOverSource(Point location) {
            float radius = WheelSize * .16f;
            float x = location.X - Width / 2f, y = location.Y - Height / 2f;
            return radius > 0 && x * x + y * y <= radius * radius;
        }

        int Hit(Point location) {
            float size = WheelSize;
            float x = location.X - Width / 2f, y = location.Y - Height / 2f;
            double distance = Math.Sqrt(x * x + y * y);
            if (size <= 0 || distance < size * RingEdges[0] || distance > size * RingEdges[2])
                return -1;
            int sector = PanMotion.Sector(x, y);
            return distance < size * RingEdges[1] ? sector : sector + 8;
        }

        void Trigger(int sector, PanSpeed speed) {
            var (pan, tilt) = PanMotion.Direction(sector);
            MovementRequested?.Invoke(pan, tilt, speed);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || !Enabled)
                return;
            Focus();
            bool overSource = IsOverSource(e.Location);
            sourceDoubleClick = overSource && e.Clicks == 2 && previousSourceClick;
            previousSourceClick = false;
            if (overSource) {
                sourceOrigin = e.Location;
                sourcePressed = Capture = true;
                return;
            }
            int hit = Hit(e.Location);
            if (hit >= 0 && CanMove(hit % 8)) {
                active = hit;
                Capture = true;
                Trigger(hit % 8, hit < 8 ? PanSpeed.Normal : PanSpeed.Fast);
            }
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            SetSourceHover(IsOverSource(e.Location));
            if (!sourcePressed)
                return;
            int x = e.X - sourceOrigin.X, y = e.Y - sourceOrigin.Y;
            var threshold = SystemInformation.DragSize;
            if (!sourceDragged && Math.Abs(x) < threshold.Width / 2 && Math.Abs(y) < threshold.Height / 2)
                return;
            sourceDragged = true;
            if (!PanWithCameraButton)
                return;
            var (pan, tilt) = PanMotion.DragVelocity(x, y, WheelSize / 2);
            DragRequested?.Invoke(pan, tilt);
        }

        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            SetSourceHover(false);
        }

        void SetSourceHover(bool overSource) {
            if (sourceHovered == overSource)
                return;
            sourceHovered = overSource;
            SourceHoverChanged?.Invoke(overSource);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left)
                return;
            var threshold = SystemInformation.DragSize;
            bool sourceClick = sourcePressed && !sourceDragged && IsOverSource(e.Location) &&
                Math.Abs(e.X - sourceOrigin.X) < threshold.Width / 2 &&
                Math.Abs(e.Y - sourceOrigin.Y) < threshold.Height / 2;
            bool chooseSource = sourceClick && (!DoubleClickForCameraSource || sourceDoubleClick);
            CancelMovement();
            previousSourceClick = sourceClick && !chooseSource;
            if (chooseSource)
                SourceRequested?.Invoke();
        }

        protected override void OnMouseCaptureChanged(EventArgs e) {
            base.OnMouseCaptureChanged(e);
            if (!Capture && (sourcePressed || active >= 0))
                CancelMovement();
        }

        internal void CancelMovement() {
            previousSourceClick = sourceDoubleClick = false;
            if (!sourcePressed && active < 0 && activeKey == Keys.None)
                return;
            sourcePressed = sourceDragged = false;
            active = -1;
            activeKey = Keys.None;
            Capture = false;
            Released?.Invoke();
            Invalidate();
        }

        protected override bool IsInputKey(Keys key) {
            var code = key & Keys.KeyCode;
            return code == Keys.Up || code == Keys.Down || code == Keys.Left || code == Keys.Right || base.IsInputKey(key);
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) {
                CancelMovement();
                SourceRequested?.Invoke();
                e.SuppressKeyPress = true;
                return;
            }
            int sector = e.KeyCode == Keys.Right ? 0 : e.KeyCode == Keys.Down ? 2 : e.KeyCode == Keys.Left ? 4 : e.KeyCode == Keys.Up ? 6 : -1;
            if (sector < 0)
                return;
            e.Handled = true;
            if (sourcePressed || active >= 0 || activeKey == e.KeyCode || !CanMove(sector))
                return;
            activeKey = e.KeyCode;
            Trigger(sector, e.Shift ? PanSpeed.Fast : e.Control ? PanSpeed.Fine : PanSpeed.Normal);
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);
            if (e.KeyCode == activeKey)
                CancelMovement();
        }

        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);
            CancelMovement();
        }

        protected override void OnEnabledChanged(EventArgs e) {
            base.OnEnabledChanged(e);
            if (!Enabled)
                CancelMovement();
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float size = Math.Min(Width, Height) - 24, cx = Width / 2f, cy = Height / 2f;
            if (size < 60)
                return;
            for (int ring = 0; ring < 2; ring++)
            for (int dir = 0; dir < 8; dir++) {
                bool enabled = Enabled && CanMove(dir);
                float outer = size * RingEdges[ring + 1] - 1;
                float inner = size * RingEdges[ring] + 1;
                float angle = dir * 45 - 21.5f;
                using (var p = new GraphicsPath()) {
                    p.AddArc(cx - outer, cy - outer, outer * 2, outer * 2, angle, 43);
                    p.AddArc(cx - inner, cy - inner, inner * 2, inner * 2, angle + 43, -43);
                    p.CloseFigure();
                    var r = p.GetBounds();
                    bool selected = active == dir + ring * 8;
                    var top = !enabled ? Color.FromArgb(31, 58, 86) : selected ? Color.FromArgb(65, 210, 255)
                        : ring == 1 ? Color.FromArgb(0, 142, 238) : Color.FromArgb(38, 100, 160);
                    using (var b = new LinearGradientBrush(r, top, Color.FromArgb(3, 44, 85), 90))
                        g.FillPath(b, p);
                    if (enabled && ring == 1)
                    using (var glow = new Pen(Color.FromArgb(40, 0, 200, 255), Math.Max(2, size * .025f)))
                        g.DrawPath(glow, p);
                    using (var pen = new Pen(!enabled ? Color.FromArgb(52, 75, 101) : ring == 1 ? Color.FromArgb(92, 222, 255) : BlueTheme.Line, 2))
                        g.DrawPath(pen, p);
                }
                float radius = (outer + inner) / 2;
                var state = g.Save();
                g.TranslateTransform(cx + (float)Math.Cos(dir * Math.PI / 4) * radius, cy + (float)Math.Sin(dir * Math.PI / 4) * radius);
                g.RotateTransform(dir * 45);
                using (var pen = new Pen(enabled ? Color.White : Color.SlateGray, Math.Max(1.5f, size / 100))) {
                    pen.StartCap = pen.EndCap = LineCap.Round;
                    float a = size * .016f;
                    for (int n = 0; n <= ring; n++) {
                        float x = (n - ring / 2f) * a * 1.5f;
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
