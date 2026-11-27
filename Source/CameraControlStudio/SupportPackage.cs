using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraControlStudio
{
    internal static class SupportPackage
    {
        internal static string ChooseDestination(SaveFileDialog dialog, Form owner, string title)
        {
            dialog.Title = title;
            dialog.FileName = "CameraControlStudio-support-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            return dialog.ShowDialog(owner) == DialogResult.OK ? dialog.FileName : null;
        }

        internal static DialogResult AskToSave(Form owner, string title) =>
            MessageBox.Show(owner, UiText.T("Save a support ZIP to attach to the GitHub issue?",
                "Vil du gemme en ZIP-pakke til at vedhæfte på GitHub?"), title, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, AppForm.MessageDirection);

        internal static async Task SaveAsync(Form owner, string destination, string title, Func<Bitmap> capture,
            string profilePath, ProfileLayout layout, string diagnosticLog = null, bool includeSystemReport = false)
        {
            try
            {
                if (destination == null)
                    return;

                var logs = DiagnosticLog.Current.GetLogDirectories();
                var alternateDataDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CameraPressets");
                using (var screenshot = new MemoryStream())
                {
                    using (var bitmap = capture())
                        bitmap.Save(screenshot, ImageFormat.Png);
                    screenshot.Position = 0;
                    await Task.Run(() => Export(destination, screenshot, profilePath, layout, logs,
                        alternateDataDirectory, diagnosticLog, includeSystemReport));
                }

                if (!owner.IsDisposed && owner.Visible)
                    MessageBox.Show(owner, UiText.T("Support ZIP saved. Review it before sharing.",
                        "ZIP-pakken er gemt. Gennemgå den, før du deler den.") + Environment.NewLine + destination,
                        title, MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, AppForm.MessageDirection);
            }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Save support ZIP", ex);
                if (!owner.IsDisposed && owner.Visible)
                    MessageBox.Show(owner, UiText.T("Could not save the support ZIP. Try a different location.",
                        "Kunne ikke gemme ZIP-pakken. Prøv at gemme den et andet sted."), title,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, AppForm.MessageDirection);
            }
        }

        internal static void Export(string destination, Stream screenshot, string profilePath, ProfileLayout layout,
            IEnumerable<string> logDirectories, string alternateDataDirectory, string diagnosticLog = null, bool includeSystemReport = false)
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

                    if (includeSystemReport)
                    {
                        using (var writer = new StreamWriter(archive.CreateEntry("system-report.log").Open()))
                            writer.Write(SystemReport.Describe());
                    }

                    bool includedDiagnosticLog = false;
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
                            {
                                bool copied = CopyIfPresent(archive, logPath, prefix + Path.GetFileName(logPath));
                                includedDiagnosticLog |= copied && string.Equals(logPath, diagnosticLog, StringComparison.OrdinalIgnoreCase);
                            }
                    }

                    if (diagnosticLog != null && !includedDiagnosticLog)
                        CopyIfPresent(archive, diagnosticLog, "Logs/Reported-error/" + Path.GetFileName(diagnosticLog));
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
