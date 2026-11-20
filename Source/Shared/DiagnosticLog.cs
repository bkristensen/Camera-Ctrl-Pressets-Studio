using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace CameraControlStudio
{
    internal sealed class DiagnosticLog
    {
        const string FileName = "CameraControlStudio.log";
        const string LogStem = "CameraControlStudio";
        static readonly Encoding LogEncoding = new UTF8Encoding(false);
        readonly object sync = new object();
        readonly string programDirectory, userDirectory;
        internal static string AppVersion { get; } = typeof(DiagnosticLog).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "unknown";

        readonly string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        internal static DiagnosticLog Current { get; } = new DiagnosticLog(AppDomain.CurrentDomain.BaseDirectory,
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CamPressets", "Logs"));

        internal volatile string CameraName;
        internal DiagnosticLogSettings Settings { get; private set; } = DiagnosticLogSettings.Default;

        internal DiagnosticLog(string programDirectory, string userDirectory)
        {
            this.programDirectory = programDirectory;
            this.userDirectory = userDirectory;
        }

        internal void Configure(DiagnosticLogSettings settings)
        {
            lock (sync)
                Settings = settings;
        }

        internal string Write(string level, string operation, Exception error = null,
            [CallerFilePath] string source = "", [CallerLineNumber] int line = 0)
        {
            try
            {
                var entry = new StringBuilder()
                    .AppendLine($"{DateTimeOffset.Now:O} [{level}] {operation}")
                    .AppendLine($"Version: {AppVersion} | Windows: {Environment.OSVersion.Version} | {(Environment.Is64BitProcess ? "64-bit" : "32-bit")}")
                    .AppendLine($"Camera: {CameraName ?? "(not connected)"} | Language: {UiText.Language}")
                    .AppendLine($"Location: {Path.GetFileName(source)}:{line}");
                for (var cause = error; cause != null; cause = cause.InnerException)
                {
                    entry.AppendLine($"{cause.GetType().FullName} (0x{cause.HResult:X8}): {cause.Message}");
                    foreach (var frame in new StackTrace(cause, true).GetFrames() ?? Array.Empty<StackFrame>())
                    {
                        var method = frame.GetMethod();
                        entry.Append($"  at {method?.DeclaringType?.FullName}.{method?.Name}");
                        if (frame.GetFileLineNumber() > 0)
                            entry.Append($" in {Path.GetFileName(frame.GetFileName())}:{frame.GetFileLineNumber()}");
                        entry.AppendLine();
                    }
                }

                var content = entry.AppendLine().ToString();
                if (!string.IsNullOrEmpty(userProfile))
                    content = Regex.Replace(content, Regex.Escape(userProfile), "%USERPROFILE%", RegexOptions.IgnoreCase);
                lock (sync)
                    return Append(Settings.DirectoryPath ?? programDirectory, content, Settings)
                        ?? Append(userDirectory, content, Settings);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is SecurityException)
            {
                return null;
            }
        }

        internal string FindLatest()
        {
            lock (sync)
            {
                try
                {
                    return FindLatestCore();
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is SecurityException)
                {
                    return null;
                }
            }
        }

        string FindLatestCore()
        {
            var programLog = Path.Combine(Settings.DirectoryPath ?? programDirectory, FileName);
            var userLog = Path.Combine(userDirectory, FileName);
            if (!File.Exists(userLog))
                return File.Exists(programLog) ? programLog : null;
            return File.GetLastWriteTimeUtc(userLog) >= File.GetLastWriteTimeUtc(programLog) ? userLog : programLog;
        }

        static string ArchivePath(string directory, int index) => Path.Combine(directory, $"{LogStem}.{index}.log");
        static string Append(string directory, string content, DiagnosticLogSettings settings)
        {
            try
            {
                Directory.CreateDirectory(directory);
                var path = Path.Combine(directory, FileName);
                var file = new FileInfo(path);
                int maxBytes = settings.SizeMiB * 1024 * 1024;
                if (LogEncoding.GetByteCount(content) > maxBytes)
                    content = content.Substring(0, maxBytes / 4) + Environment.NewLine + "[Entry truncated]" + Environment.NewLine;
                foreach (var archive in Directory.EnumerateFiles(directory, LogStem + ".*.log"))
                {
                    var suffix = Path.GetFileNameWithoutExtension(archive).Substring(LogStem.Length + 1);
                    if (int.TryParse(suffix, out int index) && index >= settings.FileCount)
                        File.Delete(archive);
                }

                if (file.Exists && file.Length + LogEncoding.GetByteCount(content) > maxBytes)
                {
                    if (settings.FileCount == 1)
                        File.Delete(path);
                    else
                    {
                        File.Delete(ArchivePath(directory, settings.FileCount - 1));
                        // Move old archives first so each file can take the next free suffix.
                        for (int index = settings.FileCount - 2; index > 0; index--)
                        {
                            var archive = ArchivePath(directory, index);
                            if (File.Exists(archive))
                                File.Move(archive, ArchivePath(directory, index + 1));
                        }

                        File.Move(path, ArchivePath(directory, 1));
                    }
                }

                File.AppendAllText(path, content, LogEncoding);
                return path;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is SecurityException)
            {
                return null;
            }
        }
    }
}
