using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace CameraControlStudio {
    // Stable IDs are persisted, never translated display names. Coordinates use a 64-unit canvas.
    public static class PresetIcons {
        public static readonly string[] Ids = new[] { 
            
            
            
            
            
            
            
            
            
            
            
            
            
            "person", "lectern", "wide-table", "table", "cafe-table", "stage", "group", "conversation", "armchairs", "round-table", "laptop", "screen", "split-screen", "film", "camera", "microphone", "sound", "mute", "zoom-in", "zoom-out", "zoom-reset", "target", "move", "focus", "home", "star", "flag", "location", "grid", "more" }.Concat(new[] { "wide-table", "table", "cafe-table", "round-table" }.SelectMany(t => new[] { "combo:lectern+" + t, "combo:" + t + "+lectern", "combo:" + t + "+lectern+cafe-table", "combo:cafe-table+lectern+" + t })).Distinct().ToArray();
        public static void Draw(Graphics g, string id, Rectangle bounds, Color color) {
            if (id != null && id.StartsWith("combo:")) {
                var parts = id.Substring(6).Split('+');
                int gap = 2, width = (bounds.Width - gap * (parts.Length - 1)) / parts.Length;
                for (int i = 0; i < parts.Length; i++)
                    Draw(g, parts[i], new Rectangle(bounds.X + i * (width + gap), bounds.Y + (bounds.Height - width) / 2, width, width), color);
                return;
            }

            int side = Math.Min(bounds.Width, bounds.Height);
            bounds = new Rectangle(bounds.X + (bounds.Width - side) / 2, bounds.Y + (bounds.Height - side) / 2, side, side);
            var state = g.Save();
            try {
                g.TranslateTransform(bounds.X, bounds.Y);
                g.ScaleTransform(bounds.Width / 64f, bounds.Height / 64f);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var p = new Pen(color, 3.5f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
                using (var b = new SolidBrush(color)) {
                    Action<float, float, float, float> line = (x, y, u, v) => g.DrawLine(p, x, y, u, v);
                    Action<float, float, float, float> box = (x, y, w, h) => g.DrawRectangle(p, x, y, w, h);
                    Action<float, float, float> head = (x, y, r) => g.DrawEllipse(p, x - r, y - r, r * 2, r * 2);
                    Action<float, float, float> person = (x, y, k) => { head(x, y, 6 * k); g.DrawArc(p, x - 12 * k, y + 9 * k, 24 * k, 22 * k, 180, 180); };
                    switch (id) {
                        case "person":
                            person(32, 18, 1.3f);
                            break;
                        case "group":
                            person(32, 18, 1);
                            person(12, 28, .65f);
                            person(52, 28, .65f);
                            break;
                        case "lectern":
                            box(12, 26, 40, 7);
                            line(18, 33, 23, 54);
                            line(46, 33, 41, 54);
                            line(19, 54, 45, 54);
                            line(33, 25, 33, 15);
                            line(33, 15, 40, 9);
                            break;
                        case "wide-table":
                        case "table":
                            g.DrawPolygon(p, new[] { new Point(8, 25), new Point(15, 18), new Point(49, 18), new Point(56, 25) });
                            line(12, 30, 12, 49);
                            line(52, 30, 52, 49);
                            if (id == "table") {
                                line(19, 30, 19, 43);
                                line(45, 30, 45, 43);
                            }
                            break;
                        case "cafe-table":
                            g.DrawEllipse(p, 10, 10, 44, 13);
                            line(32, 24, 32, 53);
                            g.DrawEllipse(p, 21, 51, 22, 5);
                            break;
                        case "stage":
                            box(8, 23, 48, 31);
                            line(4, 56, 60, 56);
                            line(8, 12, 56, 12);
                            for (int i = 0; i < 3; i++) {
                                line(17 + i * 15, 12, 17 + i * 15, 17);
                                head(17 + i * 15, 19, 2);
                            }
                            line(20, 24, 20, 35);
                            line(20, 35, 10, 48);
                            line(44, 24, 44, 35);
                            line(44, 35, 54, 48);
                            break;
                        case "conversation":
                            head(12, 13, 5);
                            head(52, 13, 5);
                            line(11, 23, 11, 39);
                            line(11, 39, 22, 39);
                            line(22, 39, 22, 53);
                            line(53, 23, 53, 39);
                            line(53, 39, 42, 39);
                            line(42, 39, 42, 53);
                            line(5, 30, 5, 50);
                            line(59, 30, 59, 50);
                            line(24, 29, 40, 29);
                            line(32, 29, 32, 52);
                            break;
                        case "armchairs":
                            for (int i = 0; i < 2; i++) {
                                int x = 5 + i * 36;
                                box(x, 27, 18, 17);
                                box(x + 4, 16, 10, 14);
                                line(x + 3, 45, x + 3, 51);
                                line(x + 15, 45, x + 15, 51);
                            }
                            line(28, 39, 36, 39);
                            line(32, 39, 32, 52);
                            break;
                        case "round-table":
                            g.DrawEllipse(p, 9, 37, 46, 17);
                            person(32, 13, .65f);
                            person(12, 23, .5f);
                            person(52, 23, .5f);
                            break;
                        case "laptop":
                            box(15, 10, 34, 32);
                            g.DrawPolygon(p, new[] { new Point(15, 42), new Point(49, 42), new Point(58, 53), new Point(6, 53) });
                            break;
                        case "screen":
                            box(8, 10, 48, 34);
                            line(5, 8, 59, 8);
                            line(32, 44, 32, 55);
                            line(32, 51, 24, 56);
                            line(32, 51, 40, 56);
                            g.FillPolygon(b, new[] { new Point(27, 18), new Point(27, 35), new Point(41, 27) });
                            break;
                        case "split-screen":
                            box(6, 14, 52, 36);
                            line(32, 14, 32, 50);
                            person(19, 25, .65f);
                            g.DrawPolygon(p, new[] { new Point(37, 41), new Point(51, 25), new Point(51, 41) });
                            break;
                        case "film":
                            box(7, 13, 50, 38);
                            line(16, 13, 16, 51);
                            line(48, 13, 48, 51);
                            for (int y = 22; y < 50; y += 10) {
                                line(7, y, 16, y);
                                line(48, y, 57, y);
                            }
                            g.FillPolygon(b, new[] { new Point(26, 22), new Point(26, 43), new Point(42, 32) });
                            break;
                        case "microphone":
                            g.DrawRoundedRectangleCompat(p, new RectangleF(24, 6, 16, 33), 8);
                            g.DrawArc(p, 17, 24, 30, 25, 0, 180);
                            line(17, 25, 17, 35);
                            line(47, 25, 47, 35);
                            line(32, 49, 32, 58);
                            line(23, 58, 41, 58);
                            break;
                        case "sound":
                        case "mute":
                            g.DrawPolygon(p, new[] { new Point(8, 26), new Point(19, 26), new Point(32, 15), new Point(32, 49), new Point(19, 38), new Point(8, 38) });
                            if (id == "mute") {
                                line(43, 25, 55, 39);
                                line(55, 25, 43, 39);
                            } else {
                                g.DrawArc(p, 28, 20, 20, 24, -65, 130);
                                g.DrawArc(p, 24, 12, 34, 40, -60, 120);
                            }
                            break;
                        case "zoom-in":
                        case "zoom-out":
                        case "zoom-reset":
                            head(27, 27, 19);
                            line(41, 41, 57, 57);
                            if (id == "zoom-reset") {
                                g.DrawArc(p, 17, 17, 20, 20, 40, 280);
                                line(36, 17, 36, 25);
                                line(36, 25, 29, 25);
                            } else {
                                line(17, 27, 37, 27);
                                if (id == "zoom-in")
                                    line(27, 17, 27, 37);
                            }
                            break;
                        case "target":
                            head(32, 32, 21);
                            line(32, 5, 32, 18);
                            line(32, 46, 32, 59);
                            line(5, 32, 18, 32);
                            line(46, 32, 59, 32);
                            break;
                        case "move":
                            line(6, 32, 58, 32);
                            line(32, 6, 32, 58);
                            g.DrawLines(p, new[] { new Point(25, 13), new Point(32, 6), new Point(39, 13) });
                            g.DrawLines(p, new[] { new Point(25, 51), new Point(32, 58), new Point(39, 51) });
                            g.DrawLines(p, new[] { new Point(13, 25), new Point(6, 32), new Point(13, 39) });
                            g.DrawLines(p, new[] { new Point(51, 25), new Point(58, 32), new Point(51, 39) });
                            break;
                        case "focus":
                            foreach (int x in new[] { 9, 55 })
                                foreach (int y in new[] { 9, 55 }) {
                                    line(x, y, x + (x == 9 ? 10 : -10), y);
                                    line(x, y, x, y + (y == 9 ? 10 : -10));
                                }
                            g.FillEllipse(b, 27, 27, 10, 10);
                            break;
                        case "home":
                            g.DrawLines(p, new[] { new Point(7, 29), new Point(32, 8), new Point(57, 29) });
                            g.DrawLines(p, new[] { new Point(15, 25), new Point(15, 55), new Point(27, 55), new Point(27, 39), new Point(38, 39), new Point(38, 55), new Point(49, 55), new Point(49, 25) });
                            break;
                        case "star":
                            var pts = new PointF[10];
                            for (int i = 0; i < 10; i++) {
                                double a = (i * 36 - 90) * Math.PI / 180;
                                float r = i % 2 == 0 ? 26 : 12;
                                pts[i] = new PointF(32 + (float)Math.Cos(a) * r, 32 + (float)Math.Sin(a) * r);
                            }
                            g.DrawPolygon(p, pts);
                            break;
                        case "flag":
                            line(15, 8, 15, 57);
                            g.DrawLines(p, new[] { new Point(15, 11), new Point(30, 9), new Point(43, 17), new Point(55, 14), new Point(55, 38), new Point(43, 41), new Point(30, 33), new Point(15, 35) });
                            break;
                        case "location":
                            g.DrawArc(p, 17, 7, 30, 32, 180, 180);
                            line(17, 23, 32, 52);
                            line(47, 23, 32, 52);
                            head(32, 23, 6);
                            g.DrawArc(p, 11, 44, 42, 14, 0, 180);
                            break;
                        case "grid":
                            box(9, 9, 18, 18);
                            box(37, 9, 18, 18);
                            box(9, 37, 18, 18);
                            box(37, 37, 18, 18);
                            break;
                        case "more":
                            for (int x = 12; x <= 52; x += 20)
                                g.FillEllipse(b, x - 4, 28, 8, 8);
                            break;
                        default:
                            g.DrawRoundedRectangleCompat(p, new RectangleF(7, 18, 34, 29), 5);
                            g.DrawPolygon(p, new[] { new Point(42, 26), new Point(57, 18), new Point(57, 47), new Point(42, 39) });
                            break;
                    }
                }
            } finally { g.Restore(state); }
        }
        static void DrawRoundedRectangleCompat(this Graphics g, Pen pen, RectangleF r, float radius) {
            using (var path = BlueTheme.Round(r, radius))
                g.DrawPath(pen, path);
        }
    }
}
