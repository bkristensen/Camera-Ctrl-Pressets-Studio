namespace CameraControlStudio {
    public static class ManualMotion {
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
    }
}
