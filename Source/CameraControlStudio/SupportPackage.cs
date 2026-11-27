using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace CameraControlStudio
{
    internal static class SupportPackage
    {
        internal static void Export(string destination, Stream screenshot, string profilePath, ProfileLayout layout,
            IEnumerable<string> logDirectories, string alternateDataDirectory)
        {
            var dataDirectory = Path.GetDirectoryName(profilePath);
            var windowPath = new[] { Path.Combine(dataDirectory, "window.json"), Path.Combine(alternateDataDirectory, "window.json") }
                .FirstOrDefault(File.Exists);
            var temporary = Path.Combine(Path.GetDirectoryName(destination), Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                using (var output = new FileStream(temporary, FileMode.CreateNew))
                using (var archive = new ZipArchive(output, ZipArchiveMode.Create))
                {
                    using (var entry = archive.CreateEntry("screenshot.png").Open())
                        screenshot.CopyTo(entry);
                    if (!CopyIfPresent(archive, profilePath, "profiles.json"))
                        CopyIfPresent(archive, Path.Combine(alternateDataDirectory, "profiles.json"), "profiles.json");
                    if (!CopyIfPresent(archive, windowPath, "window.json"))
                    {
                        using (var entry = archive.CreateEntry("window.json").Open())
                            new DataContractJsonSerializer(typeof(ProfileLayout)).WriteObject(entry, layout);
                    }

                    var userLogs = Path.Combine(dataDirectory, "Logs");
                    var alternateLogs = Path.Combine(alternateDataDirectory, "Logs");
                    int directoryIndex = 0;
                    foreach (var directory in new[] { userLogs, alternateLogs }.Concat(logDirectories)
                        .Select(Path.GetFullPath)
                        .Distinct(StringComparer.OrdinalIgnoreCase).Where(Directory.Exists))
                    {
                        bool isAppLogFolder = string.Equals(directory, userLogs, StringComparison.OrdinalIgnoreCase)
                            || string.Equals(directory, alternateLogs, StringComparison.OrdinalIgnoreCase);
                        string prefix = directoryIndex++ == 0 ? "Logs/" : $"Logs/Source-{directoryIndex}/";
                        foreach (var logPath in Directory.EnumerateFiles(directory, "*.log"))
                            if (isAppLogFolder && string.Equals(Path.GetExtension(logPath), ".log", StringComparison.OrdinalIgnoreCase)
                                || DiagnosticLog.IsLogFile(logPath))
                                CopyIfPresent(archive, logPath, prefix + Path.GetFileName(logPath));
                    }
                }

                if (File.Exists(destination))
                    File.Replace(temporary, destination, null);
                else
                    File.Move(temporary, destination);
            }
            finally
            {
                File.Delete(temporary);
            }
        }

        static bool CopyIfPresent(ZipArchive archive, string path, string entryName)
        {
            if (path == null)
                return false;

            FileStream source;
            try
            {
                source = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            }
            catch (FileNotFoundException) { return false; }
            catch (DirectoryNotFoundException) { return false; }

            using (source)
            using (var entry = archive.CreateEntry(entryName).Open())
                source.CopyTo(entry);
            return true;
        }
    }
}
