using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Resources;

namespace CameraControlStudio {
    public static class PresetIcons {
        public const string DefaultId = "presset_15_camera";

        // Keep the IDs already stored in profiles and resolve them to resource names.
        static readonly Dictionary<string, string> legacyIds = new Dictionary<string, string>(StringComparer.Ordinal) {
            ["person"] = "presset_01_person",
            ["lectern"] = "presset_02_lectern",
            ["wide-table"] = "presset_03_table1",
            ["table"] = "presset_04_table2",
            ["cafe-table"] = "presset_05_cafe_table",
            ["stage"] = "presset_06_stage",
            ["conversation"] = "presset_08_conversation2",
            ["armchairs"] = "presset_09_armchairs",
            ["round-table"] = "presset_10_group",
            ["laptop"] = "presset_11_laptop",
            ["screen"] = "presset_12_screen",
            ["split-screen"] = "presset_13_split_screen",
            ["film"] = "presset_14_film",
            ["camera"] = "presset_15_camera",
            ["microphone"] = "presset_16_microphone",
            ["sound"] = "presset_17_sound",
            ["mute"] = "presset_18_mute",
            ["zoom-in"] = "presset_19_zoom_in",
            ["zoom-out"] = "presset_20_zoom_out",
            ["zoom-reset"] = "presset_21_zoom_reset",
            ["target"] = "presset_22_center",
            ["move"] = "presset_23_move",
            ["focus"] = "presset_24_focus",
            ["home"] = "presset_25_home",
            ["star"] = "presset_26_favorite",
            ["flag"] = "presset_27_flag",
            ["location"] = "presset_28_location",
            ["grid"] = "presset_29_grid",
            ["combo:lectern+wide-table"] = "presset_30_lectern_table",
            ["combo:wide-table+lectern"] = "presset_31_table_lectern",
            ["combo:wide-table+lectern+cafe-table"] = "presset_32_schene1",
            ["combo:cafe-table+lectern+wide-table"] = "presset_33_schene2",
            ["combo:lectern+cafe-table"] = "presset_34_lectern_cafe",
            ["combo:cafe-table+lectern"] = "presset_35_cafe_lectern",
            ["more"] = "presset_36_more",
            ["group"] = "presset_37_group",
            ["combo:lectern+table"] = "presset_38_lectern_table2",
            ["combo:table+lectern"] = "presset_39_table2_lectern",
            ["combo:table+lectern+cafe-table"] = "presset_40_table2_lectern_cafe",
            ["combo:cafe-table+lectern+table"] = "presset_41_cafe_lectern_table2",
            ["combo:cafe-table+lectern+cafe-table"] = "presset_42_cafe_lectern_cafe",
            ["combo:lectern+round-table"] = "presset_43_lectern_round_table",
            ["combo:round-table+lectern"] = "presset_44_round_table_lectern",
            ["combo:round-table+lectern+cafe-table"] = "presset_45_round_table_lectern_cafe",
            ["combo:cafe-table+lectern+round-table"] = "presset_46_cafe_lectern_round_table",
        };

        static readonly Dictionary<string, IconEntry> catalog = LoadCatalog();
        public static IReadOnlyList<string> Ids { get; } =
            Array.AsReadOnly(catalog.Keys.OrderBy(id => id, StringComparer.Ordinal).ToArray());

        public static string ResolveId(string id) {
            if (string.IsNullOrEmpty(id))
                return DefaultId;
            if (catalog.ContainsKey(id))
                return id;
            return legacyIds.TryGetValue(id, out var resourceId) && catalog.ContainsKey(resourceId)
                ? resourceId : DefaultId;
        }

        public static string GetTitle(string id) {
            var title = catalog[ResolveId(id)].Title;
            return UiText.Translate("Preset icon: " + title, title);
        }

        public static void Draw(Graphics graphics, string id, Rectangle bounds) {
            var image = catalog[ResolveId(id)].Image;
            var scale = Math.Min((double)bounds.Width / image.Width, (double)bounds.Height / image.Height);
            var width = (int)Math.Round(image.Width * scale);
            var height = (int)Math.Round(image.Height * scale);
            if (width <= 0 || height <= 0)
                return;

            var destination = new Rectangle(bounds.X + (bounds.Width - width) / 2,
                bounds.Y + (bounds.Height - height) / 2, width, height);
            var state = graphics.Save();
            try {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, destination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            } finally {
                graphics.Restore(state);
            }
        }

        static Dictionary<string, IconEntry> LoadCatalog() {
            using (var stream = typeof(PresetIcons).Assembly.GetManifestResourceStream("CameraControlStudio.Assets.PressetIcons.xml"))
            using (var reader = new ResXResourceReader(stream) { UseResXDataNodes = true }) {
                var catalog = new Dictionary<string, IconEntry>(StringComparer.Ordinal);
                foreach (DictionaryEntry entry in reader) {
                    var node = (ResXDataNode)entry.Value;
                    var image = LoadImage(node);
                    if (image == null)
                        continue;

                    var title = node.Comment?.Trim();
                    catalog.Add(node.Name, new IconEntry(image, string.IsNullOrEmpty(title) ? node.Name : title));
                }
                return catalog;
            }
        }

        static Image LoadImage(ResXDataNode node) {
            var file = node.FileRef;
            var typeName = (file?.TypeName ?? node.GetValueTypeName((ITypeResolutionService)null)).Split(',')[0].Trim();
            bool isIcon = typeName == "System.Drawing.Icon";
            if (!isIcon && typeName != "System.Drawing.Bitmap" && typeName != "System.Drawing.Image"
                && typeName != "System.Drawing.Imaging.Metafile")
                return null;

            if (file == null) {
                var resource = node.GetValue((ITypeResolutionService)null);
                if (!isIcon)
                    return (Image)resource;
                using (var icon = (Icon)resource)
                    return icon.ToBitmap();
            }

            var name = "CameraControlStudio.PresetIconFiles/" + file.FileName;
            using (var stream = typeof(PresetIcons).Assembly.GetManifestResourceStream(name)
                ?? throw new MissingManifestResourceException(name)) {
                if (isIcon) {
                    using (var icon = new Icon(stream))
                        return icon.ToBitmap();
                }
                using (var image = Image.FromStream(stream))
                    return new Bitmap(image);
            }
        }

        sealed class IconEntry {
            public Image Image { get; }
            public string Title { get; }

            public IconEntry(Image image, string title) {
                Image = image;
                Title = title;
            }
        }
    }
}
