using System;
using System.Runtime.InteropServices;

namespace CameraControlStudio
{
    [ComImport, Guid("720D4AC0-7533-11D0-A5D6-28DB04C10000"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IKsTopologyInfo
    {
        [PreserveSig] int GetNumCategories(out int count);
        [PreserveSig] int GetCategory(int index, out Guid category);
        [PreserveSig] int GetNumConnections(out int count);
        [PreserveSig] int GetConnectionInfo(int index, IntPtr connection);
        [PreserveSig] int GetNodeName(int node, IntPtr name, int size, out int length);
        [PreserveSig] int GetNumNodes(out int count);
        [PreserveSig] int GetNodeType(int node, out Guid type);
        [PreserveSig] int CreateNodeInstance(int node, ref Guid iid, [MarshalAs(UnmanagedType.IUnknown)] out object instance);
    }

    [ComImport, Guid("28F54685-06FD-11D2-B27A-00A0C9223196"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IKsControl
    {
        [PreserveSig] int KsProperty(ref NodeProperty request, int requestSize, ref byte data, int dataSize, out int returned);
    }

    [StructLayout(LayoutKind.Sequential)]
    struct NodeProperty
    {
        public Guid Set;
        public int Id, Flags, Node, Reserved;
    }

    internal sealed class BrioFieldOfView : IDisposable
    {
        // Logitech UVC selector and value mapping documented by CameraCtrls.
        // https://github.com/soyersoyer/cameractrls/blob/master/cameractrls.py
        static readonly Guid PropertySet = new Guid("49e40215-f434-47fe-b158-0e885023e51b");
        static readonly Guid DeviceSpecificNode = new Guid("941c7ac0-c559-11d0-8a2b-00a0c9255ac1");
        internal static readonly int[] Degrees = { 90, 78, 65 };
        const int Get = 1, Set = 2, Topology = 0x10000000;
        static readonly int RequestSize = Marshal.SizeOf(typeof(NodeProperty));
        readonly int node;
        IKsControl control;

        BrioFieldOfView(IKsControl control, int node)
        {
            this.control = control;
            this.node = node;
        }

        internal static BrioFieldOfView Open(object filter, string deviceId)
        {
            if (deviceId.IndexOf("vid_046d&pid_", StringComparison.OrdinalIgnoreCase) < 0
                || !(filter is IKsTopologyInfo topology) || topology.GetNumNodes(out int count) < 0)
                return null;
            var iid = typeof(IKsControl).GUID;
            for (int index = 0; index < count; index++)
            {
                if (topology.GetNodeType(index, out Guid type) < 0 || type != DeviceSpecificNode)
                    continue;
                if (topology.CreateNodeInstance(index, ref iid, out object instance) < 0 || instance == null)
                    continue;
                if (instance is IKsControl ks)
                {
                    var candidate = new BrioFieldOfView(ks, index);
                    var value = (byte)0;
                    if (candidate.Request(Get, ref value, out int returned) >= 0
                        && returned == 1 && value < Degrees.Length)
                        return candidate;
                }
                Marshal.ReleaseComObject(instance);
            }
            return null;
        }

        int Request(int operation, ref byte data, out int returned)
        {
            var request = new NodeProperty { Set = PropertySet, Id = 5, Flags = Topology | operation, Node = node };
            return control.KsProperty(ref request, RequestSize, ref data, 1, out returned);
        }

        internal int Read()
        {
            byte value = 0;
            Camera.CheckControlResult(Request(Get, ref value, out int returned), "Get", "FOV");
            if (returned != 1 || value >= Degrees.Length)
                throw new COMException("The camera returned an unknown field of view.");
            return Degrees[value];
        }

        internal void Write(int degrees)
        {
            int index = Array.IndexOf(Degrees, degrees);
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(degrees));
            if (Read() == degrees)
                return;
            byte value = (byte)index;
            Camera.CheckControlResult(Request(Set, ref value, out _), "Set", "FOV");
        }

        public void Dispose()
        {
            if (control != null)
                Marshal.ReleaseComObject(control);
            control = null;
        }
    }
}
