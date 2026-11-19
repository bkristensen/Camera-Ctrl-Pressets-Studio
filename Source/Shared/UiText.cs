using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
namespace CameraControlStudio {
    public static class UiText {
        public static readonly string[] LanguageCodes = { "auto", "en", "da", "de", "zh", "hi", "es", "ar", "fr", "bn", "pt", "id", "ur" };
        public static readonly string[] LanguageNames = {
            "Automatic", "English", "Dansk", "Deutsch",
            "简体中文", "हिन्दी", "Español", "العربية الفصحى", "Français", "বাংলা",
            "Português (Brasil)", "Bahasa Indonesia", "اردو"
        };
        static readonly Dictionary<string, string> translations = new Dictionary<string, string>();
        public static string Language { get; private set; } = "en";
        public static string Preference { get; private set; } = "auto";
        public static bool IsRightToLeft => Language == "ar" || Language == "ur";
        public static bool IsDanish {
            get {
                return Language == "da";
            }
        }
        public static bool CommandLineOverride {
            get; private set;
        }
        public static string PreferencePath {
            get {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CamPressets", "language.txt");
            }
        }
        public static string ResolveLanguage(string preference, string windowsLanguage) {
            string code = preference == "auto" ? windowsLanguage.Split('-')[0].ToLowerInvariant() : preference;
            return Array.IndexOf(LanguageCodes, code) > 0 ? code : "en";
        }
        public static void Initialize(string[] args) {
            Preference = "auto";
            CommandLineOverride = false;
            try {
                if (File.Exists(PreferencePath)) {
                    string value = File.ReadAllText(PreferencePath).Trim();
                    if (Array.IndexOf(LanguageCodes, value) >= 0)
                        Preference = value;
                }
            } catch (IOException) { } catch (UnauthorizedAccessException) { }
            string selected = Preference;
            foreach (string arg in args)
            if (arg.StartsWith("--lang=", StringComparison.OrdinalIgnoreCase)) {
                string value = arg.Substring(7).ToLowerInvariant();
                if (Array.IndexOf(LanguageCodes, value) >= 0) {
                    selected = value;
                    CommandLineOverride = true;
                }
            }
            Language = ResolveLanguage(selected, CultureInfo.CurrentUICulture.Name);
            translations.Clear();
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CamPressets.Locales." + Language + ".xml")) {
                if (stream == null)
                    return;
                var xml = new XmlDocument();
                xml.XmlResolver = null;
                xml.Load(stream);
                foreach (XmlElement node in xml.DocumentElement.SelectNodes("text"))
                    translations.Add(node.GetAttribute("key"), node.InnerText);
            }
        }
        // Persist a preference without changing live strings: a restart avoids mixed
        // languages in existing controls and in localized camera-setting lookup keys.
        public static void SavePreference(string code) {
            SavePreferenceAt(code, PreferencePath);
        }
        internal static void SavePreferenceAt(string code, string path) {
            if (Array.IndexOf(LanguageCodes, code) < 0)
                throw new ArgumentException("Unsupported language code.");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string temp = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try {
                File.WriteAllText(temp, code);
                if (File.Exists(path))
                    File.Replace(temp, path, null);
                else
                    File.Move(temp, path);
                Preference = code;
            } finally { if (File.Exists(temp)) File.Delete(temp); }
        }
        public static string T(string english, string dansk) {
            return Translate(english, IsDanish ? dansk : english);
        }
        internal static string Translate(string key, string fallback) {
            return translations.TryGetValue(key, out var value) ? value : fallback;
        }
    }
}
