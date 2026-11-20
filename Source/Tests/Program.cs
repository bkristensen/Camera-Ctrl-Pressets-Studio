using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using CameraControlStudio;

class Program
{
    static void Check(bool value, string message)
    {
        if (!value)
            throw new Exception(message);
    }

    static void Main()
    {
        TestManualMotion();
        TestPresetActivity();
        TestCameraAccess();
        TestReleaseNotice();
        var previousLayout = TestProfileLayout();

        string directory = Path.Combine(Path.GetTempPath(), "camera-tests-" + Guid.NewGuid());
        Directory.CreateDirectory(directory);
        try
        {
            TestDiagnostics(directory);
            TestProfileStorage(directory, previousLayout);
            TestTranslations(directory);
        }
        finally
        {
            Directory.Delete(directory, true);
        }

        Console.WriteLine("PASS: motion, activity colours, camera consent, profile/layout migration, image persistence, movement defaults, session presets, release notices and concurrency.");
    }

    static void TestReleaseNotice()
    {
        const string tagUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases/tag/";
        const string releases = """
            [
              {"tag_name":"v0.7.0-beta.10","html_url":"URLv0.7.0-beta.10","prerelease":true},
              {"tag_name":"Beta","name":"Camera Control Studio 0.7.0-beta.2","html_url":"URLBeta"},
              {"tag_name":"v0.7.0-beta.3","html_url":"URLv0.7.0-beta.3"},
              {"tag_name":"v8.0.0","html_url":"URLv8.0.0","draft":true},
              {"tag_name":"v9.0.0","html_url":"https://example.com/releases/tag/v9.0.0"}
            ]
            """;
        Uri CheckReleases(string installedVersion, string json)
        {
            using (var payload = new MemoryStream(Encoding.UTF8.GetBytes(json.Replace("URL", tagUrl))))
                return GitHubReleases.FindNewer(installedVersion, payload);
        }

        Check(CheckReleases("0.7.0-beta.3+91abc", releases)?.AbsoluteUri == tagUrl + "v0.7.0-beta.10",
            "release check orders numeric prereleases and skips drafts and foreign URLs");
        Check(CheckReleases("0.7.0-beta.10+91abc", releases) == null, "build metadata does not change release precedence");
        Check(CheckReleases("0.7.0", releases) == null, "a prerelease of the installed stable version is older");

        const string legacy = """
            [{"tag_name":"Beta","name":"Camera Control Studio 0.7.0-beta.2","html_url":"URLBeta"}]
            """;
        Check(CheckReleases("0.7.0-beta.1", legacy)?.AbsoluteUri == tagUrl + "Beta", "legacy Beta tag reads its release title");
        Check(CheckReleases("0.7.0-beta.3", legacy) == null, "legacy Beta cannot downgrade the installed app");

        const string stable = """
            [{"tag_name":"v0.7.0","html_url":"URLv0.7.0"}]
            """;
        Check(CheckReleases("0.7.0-rc.1", stable)?.AbsoluteUri == tagUrl + "v0.7.0",
            "stable release wins over its release candidate");
    }

    static void TestManualMotion()
    {
        var setting = new Setting
        {
            Min = 0,
            Max = 200,
            Step = 2
        };
        Check(ManualMotion.Target(setting, 50, 1, 5, false) == 60, "zoom uses driver units");
        Check(ManualMotion.Target(setting, 50, -1, 32, false) == 0, "movement clamps to range");
        Check(ManualMotion.Target(setting, 50, 1, 5, true) == 40, "reverse direction");
        Check(!ManualMotion.CanMove(setting, 0, -1, false) && ManualMotion.CanMove(setting, 0, 1, false), "minimum blocks outward movement only");
        Check(!ManualMotion.CanMove(setting, 200, 1, false) && ManualMotion.CanMove(setting, 200, -1, false), "maximum blocks outward movement only");
        Check(!ManualMotion.CanMove(setting, 200, -1, true) && ManualMotion.CanMove(setting, 200, 1, true), "reversed direction swaps blocked side");
        var unevenRange = new Setting
        {
            Min = -5,
            Max = 8,
            Step = 3
        };
        Check(!ManualMotion.CanMove(unevenRange, 7, 1, false) && ManualMotion.CanMove(unevenRange, 7, -1, false),
            "reachable maximum honors driver step");
        Check(ManualMotion.ZoomAtFraction(unevenRange, 0) == -5 && ManualMotion.ZoomAtFraction(unevenRange, 1) == 7,
            "slider spans reachable zoom range");
        Check(ManualMotion.ZoomAtFraction(unevenRange, .5) == 1 && ManualMotion.ZoomFraction(unevenRange, 1) == .5,
            "slider maps zoom in both directions");
        Check(!ManualMotion.CanMove(new Setting { Min = 0, Max = 1, Step = 2 }, 0, 1, false), "an axis without reachable steps cannot move");
    }

    static void TestPresetActivity()
    {
        var activity = new PresetActivity();
        activity.Begin("A");
        Check(activity.Phase == PresetPhase.Moving, "yellow while moving");
        activity.Complete(true);
        Check(activity.Phase == PresetPhase.Reached, "green only when reached");
        activity.Clear();
        Check(activity.Phase == PresetPhase.None && activity.Name == null, "manual override clears state");
        activity.Begin("B");
        activity.Complete(false);
        Check(activity.Phase == PresetPhase.None, "failed target never green");
        activity.Begin("A");
        activity.Begin("B");
        Check(activity.Name == "B", "new preset replaces old state");
    }

    static void TestCameraAccess()
    {
        var gate = new CameraAccessGate();
        int asks = 0;
        Check(gate.Allow(false, () =>
        {
            asks++;
            return false;
        }) && asks == 0, "default no confirmation");
        Check(!gate.Allow(true, () => false), "No blocks camera");
        Check(gate.Allow(true, () => true), "Yes allows camera");
        Check(gate.Allow(true, () => throw new Exception("repeat prompt")), "confirmation remembered");
        gate.Reset();
        Check(!gate.Allow(true, () => false), "profile reset blocks camera again");
    }

    static ProfileLayout TestProfileLayout()
    {
        var from = new ProfileLayout
        {
            X = 20,
            Width = 1000,
            ControllerRatio = .4,
            SettingsOpen = true
        };
        var to = new ProfileLayout
        {
            X = 800,
            Width = 900,
            ControllerRatio = .6
        };
        var carried = LayoutPolicy.OnSwitch(from, to, true);
        Check(carried.X == 20 && carried.ControllerRatio == .4 && carried.SettingsOpen, "keep layout applies current geometry");
        Check(!ReferenceEquals(carried, from), "layout copy isolated");
        Check(LayoutPolicy.OnSwitch(from, to, false).X == 800, "default switch restores target geometry");
        Check(LayoutPolicy.Ratio(double.NaN) == LayoutPolicy.DefaultControllerRatio, "bad ratio corrected");
        var layoutSerializer = new DataContractJsonSerializer(typeof(ProfileLayout));
        using (var oldLayout = new MemoryStream(Encoding.UTF8.GetBytes("{\"ControllerRatio\":0.36}")))
        {
            var migrated = (ProfileLayout)layoutSerializer.ReadObject(oldLayout);
            Check(migrated.ControllerRatio == LayoutPolicy.DefaultControllerRatio, "old default gets the compact layout split");
        }

        Check(ProfileStore.Copy(new SavedData { Layout = new ProfileLayout { ControllerRatio = .36 } }).Layout.ControllerRatio == .36,
            "newly chosen split is preserved");
        return from;
    }

    static void TestDiagnostics(string directory)
    {
        int[] unavailableErrors =
        {
            unchecked((int)0x80070015),
            unchecked((int)0x8007048F),
            unchecked((int)0x80070006)
        };
        foreach (int code in unavailableErrors)
        {
            try
            {
                Camera.CheckControlResult(code, "Get", "Zoom");
                throw new Exception("unavailable camera failure was ignored");
            }
            catch (CameraUnavailableException ex)
            {
                Check(ex.HResult == code && ex.Message.Contains("Get Zoom"), "driver HRESULT and operation preserved");
            }
        }

        Camera.CheckControlResult(0, "Get", "Zoom");
        var programDirectory = Path.Combine(directory, "program");
        var userLogs = Path.Combine(directory, "logs");
        var diagnostics = new DiagnosticLog(programDirectory, userLogs)
        {
            CameraName = "Test camera"
        };
        string logPath;
        try
        {
            Camera.CheckControlResult(unchecked((int)0x80070015), "Get", "Zoom");
            throw new Exception("expected camera failure");
        }
        catch (CameraUnavailableException ex)
        {
            logPath = diagnostics.Write("WARNING", "Test zoom read", ex);
        }

        Check(Path.GetDirectoryName(logPath) == programDirectory && diagnostics.FindLatest() == logPath, "log goes in the program folder");
        string log = File.ReadAllText(logPath);
        Check(log.Contains("Version:") && log.Contains("Test camera") && log.Contains("0x80070015") && log.Contains("Camera.CheckControlResult") && log.Contains("Program.cs:"),
            "camera log includes diagnostic context");
        var blockedPath = Path.Combine(directory, "not-a-directory");
        File.WriteAllText(blockedPath, "");
        var fallback = new DiagnosticLog(blockedPath, userLogs);
        Check(Path.GetDirectoryName(fallback.Write("WARNING", "Fallback test")) == userLogs, "unwritable program log uses the per-user log folder");
        Check(new DiagnosticLog(blockedPath, blockedPath).Write("WARNING", "No storage") == null, "logging failure does not interrupt the app");
        File.WriteAllText(logPath, new string('x', 1024 * 1024));
        Check(diagnostics.Write("ERROR", "After rotation") == logPath && File.Exists(Path.Combine(programDirectory, "CameraControlStudio.1.log")) && File.ReadAllText(logPath).Contains("After rotation"),
            "log rotates at the size limit");
        var loggingSettings = new DiagnosticLogSettings(Path.Combine(directory, "selected"), 3, 1);
        var loggingFile = Path.Combine(directory, "logging.xml");
        loggingSettings.Save(loggingFile);
        loggingSettings = DiagnosticLogSettings.Load(loggingFile);
        diagnostics.Configure(loggingSettings);
        Check(loggingSettings.SizeMiB == 3 && loggingSettings.FileCount == 1 && Path.GetDirectoryName(diagnostics.Write("WARNING", "Custom folder")) == loggingSettings.DirectoryPath,
            "custom log options persist and direct new entries");
        var singleLog = diagnostics.FindLatest();
        File.WriteAllText(singleLog, new string('x', 3 * 1024 * 1024));
        diagnostics.Write("WARNING", "Replace only file");
        Check(Directory.GetFiles(loggingSettings.DirectoryPath, "*.log").Length == 1 && File.ReadAllText(singleLog).Contains("Replace only file"),
            "retention counts the active log");
    }

    static void TestProfileStorage(string directory, ProfileLayout previousLayout)
    {
        var store = new ProfileStore(Path.Combine(directory, "profiles.json"));
        var entry = store.Read().Profiles[ProfileStore.Standard];
        Check(!entry.Config.RequireStreamingConfirmation && !entry.Config.KeepLayoutOnSwitch, "safe defaults");
        Check(entry.Config.PanSteps == 1 && entry.Config.FastPanSteps == 4 && entry.Config.ZoomSteps == 1 && entry.Config.Seconds == 1.0m,
            "new movement defaults");
        entry.Config.Layout = previousLayout;
        entry.Config.ZoomSteps = 19;
        entry.Config.Seconds = 2.5m;
        entry.Config.KeepLayoutOnSwitch = true;
        entry.Config.Cameras["cam"] = new List<Preset>
        {
            new Preset
            {
                Name = "Scene",
                IconId = "combo:lectern+cafe-table",
                Position = new Dictionary<string, int>
                {
                    {
                        "Zoom",
                        50
                    }
                }
            }
        };
        entry.Config.ImageValues["cam"] = new Dictionary<string, ImageValue>
        {
            {
                "Brightness",
                new ImageValue
                {
                    Value = 45,
                    Automatic = false
                }
            }
        };
        store.Save(ProfileStore.Standard, entry.Config, entry.Revision);
        var serialized = File.ReadAllText(store.FilePath).Replace("\"ZoomSteps\":19", "\"FastZoomSteps\":999,\"ZoomSteps\":19");
        File.WriteAllText(store.FilePath, serialized);
        var saved = store.Read().Profiles[ProfileStore.Standard];
        Check(saved.Config.ZoomSteps == 19 && saved.Config.Seconds == 2.5m && saved.Config.Layout.Width == 1000 && saved.Config.KeepLayoutOnSwitch,
            "layout, zoom and transition survive legacy fast zoom setting");
        Check(saved.Config.Cameras["cam"][0].IconId == "combo:lectern+cafe-table", "combined icon persisted");
        Check(saved.Config.ImageValues["cam"]["Brightness"].Value == 45, "image values persisted");
        bool conflict = false;
        try
        {
            store.Save(ProfileStore.Standard, entry.Config, entry.Revision);
        }
        catch (IOException)
        {
            conflict = true;
        }

        Check(conflict, "stale saves refused");
        saved.Config.Language = "de";
        store.Save(ProfileStore.Standard, saved.Config, saved.Revision);
        Check(!File.ReadAllText(store.FilePath).Contains("FastZoomSteps"), "retired zoom setting is discarded on save");
        var copied = store.Create("Person A", ProfileStore.Standard).Profiles["Person A"];
        store.Rename("Person A", "Person B", copied.Revision);
        Check(store.Read().Profiles["Person B"].Config.Layout.Width == 1000 && copied.Config.Language == "de",
            "profile copy and rename retain language");
        UiText.Initialize(Array.Empty<string>(), "da");
        Check(UiText.Language == "da" && UiText.Preference == "da", "selected profile language used before constructing forms");
        UiText.Initialize(new[] { "--lang=fr" }, "da");
        Check(UiText.Language == "fr" && UiText.Preference == "da", "command line override preserves the profile preference");
        var sessions = new SessionPresets();
        sessions.For("Person B", "cam").Add(new Preset { Name = "Temporary" });
        Check(sessions.For("Standard", "cam").Count == 0, "temporary profile isolation");
        sessions.RenameProfile("Person B", "Person C");
        Check(sessions.For("Person C", "cam").Count == 1, "session presets survive rename");
        Check(!File.ReadAllText(store.FilePath).Contains("Temporary"), "temporary not serialized");
        using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("{\"WaitForStartup\":true,\"AutoStartPrograms\":true,\"StartupPrograms\":[{\"Name\":\"Legacy\",\"FileName\":\"unused.exe\"}]}")))
        {
            var migrated = (SavedData)new DataContractJsonSerializer(typeof(SavedData)).ReadObject(stream);
            Check(migrated.RequireStreamingConfirmation && migrated.ZoomSteps == 1 && migrated.Seconds == 1.0m && migrated.Layout != null,
                "old profile loads and new defaults apply");
            Check(!typeof(SavedData).GetFields().Any(f => f.Name == "StartupPrograms"), "program launch data removed");
        }
    }

    static void TestTranslations(string directory)
    {
        Check(UiText.LanguageCodes.Length == UiText.LanguageNames.Length, "one menu name per language");
        Check(UiText.ResolveLanguage("auto", "ja-JP") == "en", "unsupported Windows language falls back to English");
        var languageFile = Path.Combine(directory, "language.txt");
        using (var english = typeof(UiText).Assembly.GetManifestResourceStream("CamPressets.Locales.en.xml"))
        {
            var keys = XDocument.Load(english).Root.Elements("text").Select(node => (string)node.Attribute("key")).ToArray();
            foreach (var lang in UiText.LanguageCodes.Skip(1))
            {
                Check(UiText.ResolveLanguage("auto", lang + "-XX") == lang, "Windows language resolved: " + lang);
                UiText.SavePreferenceAt(lang, languageFile);
                Check(File.ReadAllText(languageFile) == lang, "locale preference saved: " + lang);
                UiText.Initialize(new[] { "--lang=" + lang });
                Check(UiText.Language == lang && UiText.CommandLineOverride, "locale selected: " + lang);
                Check(UiText.IsRightToLeft == (lang == "ar" || lang == "ur"), "text direction: " + lang);
                foreach (var key in keys)
                    Check(!string.IsNullOrWhiteSpace(UiText.Translate(key, null)), "translation available: " + lang + " / " + key);
                Check(string.Format(UiText.T("Delete permanent preset: {0}?", ""), "FX").Contains("FX"), "localized confirmation: " + lang);
            }
        }
    }
}
