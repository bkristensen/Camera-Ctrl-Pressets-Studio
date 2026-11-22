using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CameraControlStudio {
    public sealed class MainViewSplitter : SplitContainer {
        internal static readonly Color LineColor = Color.FromArgb(50, 110, 168);

        public MainViewSplitter() {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SplitterMoved += (sender, e) => Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                SplitterWidth = Math.Max(8, (int)Math.Round(12 * DeviceDpi / 96.0));
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            var gap = SplitterRectangle;
            var line = new RectangleF(gap.Left + (gap.Width - 4) / 2f, gap.Top + 3, 4, gap.Height - 6);
            if (line.Height >= 4)
                DrawLine(e.Graphics, line);
        }

        internal static void DrawLine(Graphics graphics, RectangleF bounds) {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = BlueTheme.Round(bounds, 2))
            using (var brush = new SolidBrush(LineColor))
                graphics.FillPath(brush, path);
        }
    }

    public sealed class MainViewSeparator : Control {
        public MainViewSeparator() {
            SetStyle(ControlStyles.ResizeRedraw, true);
            TabStop = false;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (Width >= 4 && Height >= 4)
                MainViewSplitter.DrawLine(e.Graphics, new RectangleF(0, (Height - 4) / 2f, Width, 4));
        }
    }

    public enum ZoomDirection { In, Out }

    public sealed class ZoomButton : BlueButton {
        ZoomDirection direction;

        [DefaultValue(ZoomDirection.In)]
        public ZoomDirection Direction {
            get => direction;
            set {
                if (direction == value)
                    return;
                direction = value;
                Invalidate();
            }
        }

        protected override Image DisplayImage => Image ?? (Direction == ZoomDirection.In
            ? Properties.ZoomButtonIcons.In : Properties.ZoomButtonIcons.Out);
    }

    public sealed class ZoomSlider : Control {
        const int ThumbHeight = 22;
        double position = .5, preview;
        bool dragging;

        internal event Action<double> PositionRequested;
        internal event Action<int> StepRequested;

        public ZoomSlider() {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Cursor = Cursors.Hand;
            AccessibleRole = AccessibleRole.Slider;
            TabStop = true;
        }

        internal double Position {
            get => position;
            set {
                if (position == value)
                    return;
                position = value;
                if (!dragging)
                    Invalidate();
            }
        }

        int TrackLength => Math.Max(1, Height - ThumbHeight - 4);

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;
            Focus();
            preview = position;
            dragging = Capture = true;
            RequestPointerPosition(e.Y);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (dragging)
                RequestPointerPosition(e.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            if (!dragging || e.Button != MouseButtons.Left)
                return;
            RequestPointerPosition(e.Y);
            dragging = Capture = false;
            Invalidate();
        }

        protected override void OnMouseCaptureChanged(EventArgs e) {
            base.OnMouseCaptureChanged(e);
            if (!Capture && dragging) {
                dragging = false;
                Invalidate();
            }
        }

        void RequestPointerPosition(int y) {
            double fraction = Math.Max(0, Math.Min(1, 1 - (y - ThumbHeight / 2.0 - 2) / TrackLength));
            if (fraction == preview)
                return;
            preview = fraction;
            Invalidate();
            PositionRequested?.Invoke(fraction);
        }

        protected override bool IsInputKey(Keys key) {
            switch (key & Keys.KeyCode) {
                case Keys.Up:
                case Keys.Down:
                case Keys.Home:
                case Keys.End:
                    return true;
                default:
                    return base.IsInputKey(key);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            switch (e.KeyCode) {
                case Keys.Up:
                case Keys.Down:
                    StepRequested?.Invoke(e.KeyCode == Keys.Up ? 1 : -1);
                    break;
                case Keys.Home:
                case Keys.End:
                    PositionRequested?.Invoke(e.KeyCode == Keys.Home ? 1 : 0);
                    break;
                case Keys.Escape:
                    Capture = false;
                    break;
                default:
                    return;
            }
            e.Handled = e.SuppressKeyPress = true;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (Width < 12 || Height < ThumbHeight + 8)
                return;

            var g = e.Graphics;
            var track = new RectangleF((Width - 4) / 2f, ThumbHeight / 2f + 2, 4, TrackLength);
            MainViewSplitter.DrawLine(g, track);

            int width = Math.Min(40, Width - 6);
            float y = 2 + (float)((1 - (dragging ? preview : position)) * TrackLength);
            var thumb = new RectangleF((Width - width) / 2f, y, width, ThumbHeight);
            using (var path = BlueTheme.Round(thumb, 7))
            using (var fill = new LinearGradientBrush(thumb,
                Enabled ? Color.FromArgb(46, 120, 185) : Color.FromArgb(43, 73, 104), BlueTheme.Background, 90))
            using (var edge = new Pen(Enabled ? BlueTheme.Line : Color.FromArgb(61, 93, 124))) {
                g.FillPath(fill, path);
                g.DrawPath(edge, path);
            }
            if (Focused)
                ControlPaint.DrawFocusRectangle(g, Rectangle.Round(thumb));
        }

        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e) {
            base.OnEnabledChanged(e);
            Invalidate();
        }
    }
}
