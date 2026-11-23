using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace CameraControlStudio
{
    internal static class SystemReport
    {
        const int MaxAttachmentBytes = 4 * 1024 * 1024;
        internal static string Export(string logPath)
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CamPressets", "Reports");
            Directory.CreateDirectory(folder);
            string attachment = Path.Combine(folder, "CameraControlStudio-report.log");
            File.Delete(attachment);
            if (File.Exists(logPath))
            {
                using (var source = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                using (var output = File.Create(attachment))
                {
                    if (source.Length > MaxAttachmentBytes)
                    {
                        source.Position = source.Length - MaxAttachmentBytes;
                        for (int next; (next = source.ReadByte()) != -1 && next != '\n';)
                        {
                        }
                    }

                    source.CopyTo(output);
                }
            }

            var hardware = new StringBuilder("Camera Control Studio system report").AppendLine()
                .AppendLine("App version: " + DiagnosticLog.AppVersion)
                .AppendLine("Windows version: " + WindowsBuild())
                .AppendLine("OS architecture: " + RuntimeInformation.OSArchitecture)
                .AppendLine("App architecture: " + RuntimeInformation.ProcessArchitecture)
                .AppendLine("Logical processors: " + Environment.ProcessorCount)
                .AppendLine("CPU model: " + (Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0", "ProcessorNameString", null) ?? "unknown"));
            if (GetPhysicallyInstalledSystemMemory(out ulong memoryKiB))
                hardware.AppendLine("Installed RAM (MiB): " + memoryKiB / 1024);
            File.WriteAllText(Path.Combine(folder, "CameraControlStudio-system.log"), hardware.ToString());
            return folder;
        }

        static string WindowsBuild()
        {
            using (var registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var windows = registry.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                var build = windows?.GetValue("CurrentBuildNumber");
                var revision = windows?.GetValue("UBR");
                return build == null ? Environment.OSVersion.Version.ToString()
                    : revision == null ? build.ToString() : build + "." + revision;
            }
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out ulong totalMemoryInKilobytes);
    }
}
