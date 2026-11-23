using System;
using System.IO;
using System.Xml.Linq;

namespace CameraControlStudio
{
    internal sealed class DiagnosticLogSettings
    {
        internal const int MaxSizeMiB = 100;
        internal const int MaxFiles = 20;
        internal static DiagnosticLogSettings Default { get; } = new DiagnosticLogSettings(null, 1, 2);
        static string PreferenceFile => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CamPressets", "logging.xml");
        internal string DirectoryPath { get; }
        internal int SizeMiB { get; }
        internal int FileCount { get; }

        internal DiagnosticLogSettings(string directory, int sizeMiB, int fileCount)
        {
            DirectoryPath = string.IsNullOrWhiteSpace(directory) ? null : Path.GetFullPath(directory);
            SizeMiB = Math.Max(1, Math.Min(MaxSizeMiB, sizeMiB));
            FileCount = Math.Max(1, Math.Min(MaxFiles, fileCount));
        }

        internal static DiagnosticLogSettings Load(string path = null)
        {
            path = path ?? PreferenceFile;
            if (!File.Exists(path))
                return Default;
            var xml = XElement.Load(path);
            return new DiagnosticLogSettings((string)xml.Element("Directory"),
                (int? )xml.Element("SizeMiB") ?? Default.SizeMiB, (int? )xml.Element("FileCount") ?? Default.FileCount);
        }

        internal void Save(string path = null)
        {
            path = path ?? PreferenceFile;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var temp = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try
            {
                new XElement("Logging", new XElement("Directory", DirectoryPath ?? ""),
                    new XElement("SizeMiB", SizeMiB), new XElement("FileCount", FileCount)).Save(temp);
                if (File.Exists(path))
                    File.Replace(temp, path, null);
                else
                    File.Move(temp, path);
            }
            finally
            {
                if (File.Exists(temp))
                    File.Delete(temp);
            }
        }
    }
}
