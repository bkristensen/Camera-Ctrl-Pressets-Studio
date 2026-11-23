using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace CameraControlStudio {
    [DataContract]
    public class Preset {
        [DataMember] public string Name;
        [DataMember(EmitDefaultValue = false)] public string IconId;
        [DataMember(EmitDefaultValue = false)] public int? FieldOfView;
        [DataMember] public Dictionary<string, int> Position = new Dictionary<string, int>();
        public override string ToString() {
            return Name;
        }
    }
    [DataContract]
    public sealed class ProfileLayout {
        [DataMember] public bool HasBounds, Maximized, SettingsOpen;
        [DataMember] public int X, Y, Width = 900, Height = 500, SettingsTab = 2;
        [DataMember] public double ControllerRatio = LayoutPolicy.DefaultControllerRatio;
        [DataMember(EmitDefaultValue = false)] bool CompactMainView = true;

        [OnDeserialized]
        void RestoreControllerRatio(StreamingContext context) {
            if (!CompactMainView && ControllerRatio == .36)
                ControllerRatio = LayoutPolicy.DefaultControllerRatio;
            CompactMainView = true;
        }

        public ProfileLayout Copy() {
            return (ProfileLayout)MemberwiseClone();
        }
    }
    [DataContract]
    public sealed class ImageValue {
        [DataMember] public int Value; [DataMember] public bool Automatic;
    }
    [DataContract]
    public class SavedData {
        [DataMember] public Dictionary<string, List<Preset>> Cameras;
        [DataMember] public decimal Seconds;
        [DataMember] public bool UseTransitionSpeed;
        [DataMember] public int TransitionSpeedPercent, FinePanPercent;
        [DataMember] public int PanSteps, FastPanSteps, ZoomSteps;
        [DataMember] public bool ReverseHorizontal, ReverseVertical, AlwaysOnTop;
        // Zero selects the widest supported FOV. Minus one leaves the device unchanged.
        [DataMember(EmitDefaultValue = false)] public int StartupFieldOfView;
        [DataMember(EmitDefaultValue = false)] public bool AllowFieldOfViewChange;
        [DataMember] public string CameraId;
        [DataMember(EmitDefaultValue = false)] public string Language;
        // Retain the serialized key so existing user choices survive the upgrade.
        [DataMember(Name = "WaitForStartup")] public bool RequireStreamingConfirmation;
        [DataMember] public bool KeepLayoutOnSwitch;
        [DataMember] public ProfileLayout Layout;
        [DataMember] public Dictionary<string, Dictionary<string, ImageValue>> ImageValues;
        public SavedData() {
            Defaults(default(StreamingContext));
        }
        [OnDeserializing]
        void Defaults(StreamingContext context) {
            ImageValues = new Dictionary<string, Dictionary<string, ImageValue>>();
            Cameras = new Dictionary<string, List<Preset>>();
            Seconds = 1.0m;
            TransitionSpeedPercent = 30;
            FinePanPercent = 50;
            PanSteps = 1;
            FastPanSteps = 4;
            ZoomSteps = 1;
            Layout = new ProfileLayout();
        }
    }
    public static class Motion {
        public static double Ease(double t) {
            t = Math.Max(0, Math.Min(1, t));
            return t * t * t * (t * (t * 6 - 15) + 10);
        }
    }
    public static class LayoutPolicy {
        public const double DefaultControllerRatio = .55;
        public static ProfileLayout OnSwitch(ProfileLayout current, ProfileLayout target, bool keep) {
            return (keep ? current : target).Copy();
        }
        public static double Ratio(double value) {
            return double.IsNaN(value) || double.IsInfinity(value) || value == 0
                ? DefaultControllerRatio : Math.Max(.2, Math.Min(.7, value));
        }
    }
}
