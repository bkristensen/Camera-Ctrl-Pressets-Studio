using System;
using System.Linq;
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
        [PreserveSig] int KsProperty(ref NodeProperty request, int requestSize,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, int dataSize, out int returned);
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
        static readonly int[] DegreesByCode = { 90, 78, 65 };
        const int Get = 1, Set = 2, BasicSupport = 0x200, Topology = 0x10000000;
        static readonly int RequestSize = Marshal.SizeOf(typeof(NodeProperty));
        readonly int node;
        IKsControl control;
        readonly byte[] value = new byte[1];
        internal int[] AvailableAngles { get; private set; } = Array.Empty<int>();

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
                    if (candidate.Request(Get, candidate.value, out int returned) >= 0
                        && returned == 1 && candidate.value[0] < DegreesByCode.Length)
                    {
                        var support = new byte[4096];
                        if (candidate.Request(BasicSupport, support, out returned) >= 0)
                            candidate.AvailableAngles = ReadSupportedAngles(support, returned);
                        return candidate;
                    }
                }
                Marshal.ReleaseComObject(instance);
            }
            return null;
        }

        int Request(int operation, byte[] data, out int returned)
        {
            var request = new NodeProperty { Set = PropertySet, Id = 5, Flags = Topology | operation, Node = node };
            return control.KsProperty(ref request, RequestSize, data, data.Length, out returned);
        }

        internal static int[] ReadSupportedAngles(byte[] support, int returned)
        {
            // A UVC extension returns a KSPROPERTY_DESCRIPTION followed by one or more member lists.
            const int descriptionSize = 40, membersHeaderSize = 16, memberRanges = 1;
            if (returned < descriptionSize || returned > support.Length || (BitConverter.ToInt32(support, 0) & Set) == 0)
                return Array.Empty<int>();
            int size = BitConverter.ToInt32(support, 4);
            int lists = BitConverter.ToInt32(support, 32);
            if (size < descriptionSize || size > returned || lists <= 0)
                return Array.Empty<int>();

            var supported = new bool[DegreesByCode.Length];
            int offset = descriptionSize;
            for (int list = 0; list < lists; list++)
            {
                if (size - offset < membersHeaderSize)
                    return Array.Empty<int>();
                int kind = BitConverter.ToInt32(support, offset);
                int memberSize = BitConverter.ToInt32(support, offset + 4);
                int count = BitConverter.ToInt32(support, offset + 8);
                offset += membersHeaderSize;
                if (memberSize <= 0 || count < 0 || count > (size - offset) / memberSize)
                    return Array.Empty<int>();

                if (kind == memberRanges && (memberSize == 2 || memberSize == 8))
                {
                    for (int member = 0; member < count; member++)
                    {
                        int start = offset + member * memberSize;
                        int min = memberSize == 2 ? support[start] : BitConverter.ToInt32(support, start);
                        int max = memberSize == 2 ? support[start + 1] : BitConverter.ToInt32(support, start + 4);
                        for (int code = 0; code < supported.Length; code++)
                            supported[code] |= code >= min && code <= max;
                    }
                }
                offset += memberSize * count;
            }
            return DegreesByCode.Where((degrees, code) => supported[code]).ToArray();
        }

        internal int Read()
        {
            Camera.CheckControlResult(Request(Get, value, out int returned), "Get", "FOV");
            if (returned != 1 || value[0] >= DegreesByCode.Length)
                throw new COMException("The camera returned an unknown field of view.");
            return DegreesByCode[value[0]];
        }

        internal void Write(int degrees)
        {
            if (Read() == degrees)
                return;
            if (!AvailableAngles.Contains(degrees))
                throw new ArgumentOutOfRangeException(nameof(degrees));
            value[0] = (byte)Array.IndexOf(DegreesByCode, degrees);
            Camera.CheckControlResult(Request(Set, value, out _), "Set", "FOV");
        }

        public void Dispose()
        {
            if (control != null)
                Marshal.ReleaseComObject(control);
            control = null;
        }
    }
}
