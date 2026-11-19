using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace CameraControlStudio {
    [ComImport, Guid("29840822-5B84-11D0-BD3B-00A0C911CE86"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface ICreateDevEnum {
        [PreserveSig] int CreateClassEnumerator(ref Guid category, out IEnumMoniker enumerator, int flags);
    }
    [ComImport, Guid("55272A00-42CB-11CE-8135-00AA004BB851"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IPropertyBag {
        [PreserveSig] int Read([MarshalAs(UnmanagedType.LPWStr)] string name, [MarshalAs(UnmanagedType.Struct)] out object value, IntPtr log);
        [PreserveSig] int Write([MarshalAs(UnmanagedType.LPWStr)] string name, [MarshalAs(UnmanagedType.Struct)] ref object value);
    }
    [ComImport, Guid("C6E13370-30AC-11D0-A18C-00A0C9118956"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAMCameraControl {
        [PreserveSig] int GetRange(int property, out int min, out int max, out int step, out int def, out int flags);
        [PreserveSig] int Set(int property, int value, int flags);
        [PreserveSig] int Get(int property, out int value, out int flags);
    }
    [ComImport, Guid("C6E13360-30AC-11D0-A18C-00A0C9118956"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAMVideoProcAmp {
        [PreserveSig] int GetRange(int property, out int min, out int max, out int step, out int def, out int flags);
        [PreserveSig] int Set(int property, int value, int flags);
        [PreserveSig] int Get(int property, out int value, out int flags);
    }
    public sealed class Device : IDisposable {
        public string Name, Id; internal IMoniker Moniker;
        public override string ToString() {
            return Name;
        }
        public void Dispose() {
            if (Moniker != null)
                Marshal.ReleaseComObject(Moniker);
            Moniker = null;
        }
    }
    public sealed class Setting {
        public string Name; public int Property, Min, Max, Step, Default, Caps; public bool Video;
        public int Snap(double value) {
            double v = Min + Math.Round((value - Min) / Math.Max(1, Step)) * Math.Max(1, Step);
            return (int)Math.Max(Min, Math.Min(Min + Math.Floor(((double)Max - Min) / Math.Max(1, Step)) * Math.Max(1, Step), v));
        }
    }
    public sealed class Camera : IDisposable {
        object filter; IAMCameraControl control; IAMVideoProcAmp video;
        public Dictionary<string, Setting> Settings = new Dictionary<string, Setting>();
        public static List<Device> Enumerate() {
            var result = new List<Device>();
            object dev = null;
            IEnumMoniker en = null;
            try {
                dev = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("62BE5D10-60EB-11D0-BD3B-00A0C911CE86")));
                Guid cat = new Guid("860BB310-5D01-11D0-BD3B-00A0C911CE86");
                if (((ICreateDevEnum)dev).CreateClassEnumerator(ref cat, out en, 0) != 0)
                    return result;
                var one = new IMoniker[1];
                while (en.Next(1, one, IntPtr.Zero) == 0) {
                    object bag = null;
                    bool keep = false;
                    try {
                        Guid iid = typeof(IPropertyBag).GUID;
                        one[0].BindToStorage(null, null, ref iid, out bag);
                        object name;
                        Marshal.ThrowExceptionForHR(((IPropertyBag)bag).Read("FriendlyName", out name, IntPtr.Zero));
                        string id;
                        one[0].GetDisplayName(null, null, out id);
                        result.Add(new Device { Name = Convert.ToString(name), Id = id, Moniker = one[0] });
                        keep = true;
                    } catch (COMException) { } finally { if (bag != null) Marshal.ReleaseComObject(bag); if (!keep) Marshal.ReleaseComObject(one[0]); }
                }
                return result;
            } catch { foreach (var d in result) d.Dispose(); throw; } finally { if (en != null) Marshal.ReleaseComObject(en); if (dev != null) Marshal.ReleaseComObject(dev); }
        }
        public Camera(Device device) {
            try {
                Guid iid = new Guid("56A86895-0AD4-11CE-B03A-0020AF0BA770");
                device.Moniker.BindToObject(null, null, ref iid, out filter);
                control = filter as IAMCameraControl;
                video = filter as IAMVideoProcAmp;
                Add("Pan", 0, false);
                Add("Tilt", 1, false);
                Add("Zoom", 3, false);
                Add("Focus", 6, false);
                Add("Exposure", 4, false);
                Add("Hue", 2, true);
                Add("Sharpness", 4, true);
                Add("Gamma", 5, true);
                Add("Backlight compensation", 8, true);
                Add("Gain", 9, true);
                Add("Brightness", 0, true);
                Add("Contrast", 1, true);
                Add("Saturation", 3, true);
                Add("White balance", 7, true);
            } catch { Dispose(); throw; }
        }
        void Add(string name, int property, bool isVideo) {
            int lo, hi, step, def, caps, hr;
            if (isVideo) {
                if (video == null)
                    return;
                hr = video.GetRange(property, out lo, out hi, out step, out def, out caps);
            } else {
                if (control == null)
                    return;
                hr = control.GetRange(property, out lo, out hi, out step, out def, out caps);
            }
            if (hr >= 0 && hi > lo)
                Settings[name] = new Setting { Name = name, Property = property, Video = isVideo, Min = lo, Max = hi, Step = Math.Max(1, step), Default = def, Caps = caps };
        }
        public int Get(string name, out int flags) {
            var s = Settings[name];
            int value;
            int hr = s.Video ? video.Get(s.Property, out value, out flags) : control.Get(s.Property, out value, out flags);
            Marshal.ThrowExceptionForHR(hr);
            return value;
        }
        public int Get(string name) {
            int f;
            return Get(name, out f);
        }
        public void Set(string name, double value, bool auto = false) {
            var s = Settings[name];
            int flag = auto ? 1 : 2;
            if ((s.Caps & flag) == 0)
                throw new InvalidOperationException(name + UiText.T(": this mode is not supported.", ": denne tilstand understøttes ikke."));
            int hr = s.Video ? video.Set(s.Property, s.Snap(value), flag) : control.Set(s.Property, s.Snap(value), flag);
            Marshal.ThrowExceptionForHR(hr);
        }
        public void Dispose() {
            control = null;
            video = null;
            if (filter != null)
                Marshal.ReleaseComObject(filter);
            filter = null;
        }
    }
}
