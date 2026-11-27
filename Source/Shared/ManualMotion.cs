using System;
using System.Collections.Generic;
using System.Linq;

namespace CameraControlStudio {
    public static class ManualMotion {
        internal const int DefaultRepeatInterval = 150;

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

        internal static bool CanRestoreFullFrame(int? minimumZoom, int? currentZoom, int? widestFov, int? currentFov) =>
            (minimumZoom.HasValue && currentZoom != minimumZoom) ||
            (widestFov.HasValue && currentFov != widestFov);

        internal static int? NextFieldOfView(int[] widestFirst, int current, int zoomDirection) {
            int index = Array.IndexOf(widestFirst, current);
            if (index < 0)
                return null;
            int next = index + Math.Sign(zoomDirection);
            if (next < 0 || next >= widestFirst.Length)
                return null;
            return widestFirst[next];
        }

        internal static double? EquivalentZoom(Setting before, int zoom, int oldFov, Setting after, int newFov) {
            if (before.Min <= 0 || after.Min <= 0)
                return null;
            double widthRatio = Math.Tan(newFov * Math.PI / 360) / Math.Tan(oldFov * Math.PI / 360);
            return zoom / (double)before.Min * after.Min * widthRatio;
        }

        internal static IEnumerable<int> PanFieldOfViewOptions(int[] widestFirst, int current, Setting zoom, int position) {
            return widestFirst.Where(fov => fov > current).Reverse()
                .Concat(widestFirst.Where(fov => fov < current))
                .Where(fov => EquivalentZoom(zoom, position, current, zoom, fov) >= zoom.Min);
        }

        internal static int ZoomAfterFieldOfViewStep(Setting setting, int zoom, double? equivalent, int direction) {
            int desired = setting.Snap(equivalent ?? zoom);
            // Leave a step in the requested direction after crossing into the adjacent FOV.
            return CanMove(setting, desired, direction, false) ? desired :
                Target(setting, desired, -direction, 1, false);
        }
    }
}
