using System;

namespace CameraControlStudio
{
    internal static class PanMotion
    {
        internal const int Interval = 50;
        internal const double DiagonalShare = .45;

        static readonly (int Pan, int Tilt)[] Directions =
        {
            (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1), (0, -1), (1, -1)
        };

        internal static int Sector(double x, double y) =>
            (int)Math.Floor(Math.Atan2(y, x) / (Math.PI / 4) + 8.5) % Directions.Length;

        internal static (int Pan, int Tilt) Direction(int sector) => Directions[sector];

        internal static (double Pan, double Tilt) DragVelocity(double x, double y, double radius)
        {
            double distance = Math.Sqrt(x * x + y * y);
            double scale = Math.Max(1, Math.Max(radius, distance));
            return (x / scale, y / scale);
        }

        internal static int TakeSteps(ref double pending, double perTick)
        {
            pending += perTick;
            int whole = (int)pending;
            pending -= whole;
            return whole;
        }
    }
}
