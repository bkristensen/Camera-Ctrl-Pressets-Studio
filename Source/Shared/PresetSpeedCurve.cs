using System;

namespace CameraControlStudio {
    internal sealed class PresetSpeedCurve {
        const double FullSpeedRampSeconds = .4;
        readonly double distance, acceleration, rampTime, peakSpeed;

        public PresetSpeedCurve(double distance, double speed) {
            this.distance = distance;
            acceleration = speed / FullSpeedRampSeconds;
            rampTime = Math.Min(FullSpeedRampSeconds, Math.Sqrt(distance / acceleration));
            peakSpeed = acceleration * rampTime;
            Duration = distance == 0 ? 0 : distance / peakSpeed + rampTime;
        }

        public double Duration { get; }

        public static double AxisDistance(Setting setting, int start, int target) {
            double range = setting.Snap(setting.Max) - (double)setting.Min;
            return range > 0 ? Math.Abs(target - (double)start) / range : 0;
        }

        public double Progress(double elapsed) {
            if (elapsed >= Duration)
                return 1;
            if (elapsed <= 0)
                return 0;
            if (elapsed < rampTime)
                return RampDistance(elapsed) / distance;
            double remaining = Duration - elapsed;
            if (remaining < rampTime)
                return 1 - RampDistance(remaining) / distance;
            return peakSpeed * (elapsed - rampTime / 2) / distance;
        }

        double RampDistance(double seconds) => acceleration * seconds * seconds / 2;
    }
}
