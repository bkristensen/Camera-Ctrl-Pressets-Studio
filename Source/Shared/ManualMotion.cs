namespace CameraControlStudio {
    public static class ManualMotion {
        public static int Target(Setting setting, int current, int direction, int steps, bool reverse) {
            int sign = reverse ? -direction : direction;
            return setting.Snap(current + (double)sign * steps * setting.Step);
        }
    }
}
