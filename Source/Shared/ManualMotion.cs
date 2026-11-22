using System;

namespace CameraControlStudio {
    public static class ManualMotion {
        internal const int DefaultRepeatInterval = 150;
        const int MinimumRepeatInterval = 33;

        internal static int PanRepeat(double stepsPerDefaultInterval, bool fine, out int steps) {
            // Spread small moves over more frequent updates when the driver can accept them.
            // Fine mode always sends a single step, even if that limits the requested rate.
            steps = fine ? 1 : (int)Math.Ceiling(stepsPerDefaultInterval * MinimumRepeatInterval / DefaultRepeatInterval);
            return Math.Max(MinimumRepeatInterval, (int)Math.Round(DefaultRepeatInterval * steps / stepsPerDefaultInterval));
        }

        public static bool CanMove(Setting setting, int current, int direction, bool reverse) {
            int maximum = setting.Snap(setting.Max);
            if (maximum <= setting.Min)
                return false;
            return (reverse ? -direction : direction) > 0 ? current < maximum : current > setting.Min;
        }

        public static int Target(Setting setting, int current, int direction, int steps, bool reverse) {
            int sign = reverse ? -direction : direction;
            return setting.Snap(current + (double)sign * steps * setting.Step);
        }

        public static double ZoomFraction(Setting setting, int current) {
            double range = setting.Snap(setting.Max) - (double)setting.Min;
            return range <= 0 ? 0 : Math.Max(0, Math.Min(1, (current - (double)setting.Min) / range));
        }

        public static int ZoomAtFraction(Setting setting, double fraction) {
            double range = setting.Snap(setting.Max) - (double)setting.Min;
            return setting.Snap(setting.Min + Math.Max(0, Math.Min(1, fraction)) * range);
        }
    }
}
