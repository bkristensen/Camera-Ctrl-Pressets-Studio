using System.Drawing;
using System.IO;

namespace CameraControlStudio.Properties {
    internal static class ZoomButtonIcons {
        public static Image In { get; } = Load(Resources.Zoom_In);
        public static Image FastIn { get; } = Load(Resources.Zoom_In_Fast);
        public static Image Out { get; } = Load(Resources.Zoom_Out);
        public static Image FastOut { get; } = Load(Resources.Zoom_Out_Fast);

        static Bitmap Load(byte[] data) {
            using (var stream = new MemoryStream(data, writable: false))
            using (var image = Image.FromStream(stream))
                return new Bitmap(image);
        }
    }
}
