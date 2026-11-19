using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace CameraControlStudio {
    [DataContract]
    public sealed class ProfileEntry {
        [DataMember] public long Revision = 1;
        [DataMember] public SavedData Config = new SavedData();
    }
    [DataContract]
    public sealed class ProfileCatalog {
        [DataMember] public int Schema = 1;
        [DataMember] public Dictionary<string, ProfileEntry> Profiles = new Dictionary<string, ProfileEntry>();
    }
    public sealed class ProfileStore {
        public const string Standard = "Standard";
        public readonly string FilePath;
        readonly string mutexName;
        public ProfileStore() : this(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CamPressets", "profiles.json")) { }
        public ProfileStore(string path) {
            FilePath = Path.GetFullPath(path);
            using (var sha = SHA256.Create())
                mutexName = "CamProfiles_" + BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(FilePath.ToUpperInvariant()))).Replace("-", "");
        }
        T Locked<T>(Func<T> action) {
            using (var mutex = new Mutex(false, mutexName)) {
                bool held = false;
                try {
                    try {
                        held = mutex.WaitOne(5000);
                    } catch (AbandonedMutexException) { held = true; }
                    if (!held)
                        throw new IOException(UiText.T("The profile file is busy. Try again.", "Profilfilen er optaget. Prøv igen."));
                    return action();
                } finally { if (held) mutex.ReleaseMutex(); }
            }
        }
        public ProfileCatalog Read() {
            return Locked(() => Load());
        }
        ProfileCatalog Load() {
            if (!File.Exists(FilePath)) {
                var c = new ProfileCatalog();
                c.Profiles.Add(Standard, new ProfileEntry());
                Write(c);
                return c;
            }
            ProfileCatalog result;
            using (var f = File.OpenRead(FilePath))
                result = (ProfileCatalog)new DataContractJsonSerializer(typeof(ProfileCatalog)).ReadObject(f);
            if (result == null || result.Schema != 1 || result.Profiles == null || !result.Profiles.ContainsKey(Standard))
                throw Invalid();
            foreach (var pair in result.Profiles) {
                CheckName(pair.Key);
                if (pair.Value == null || pair.Value.Revision < 1)
                    throw Invalid();
                Validate(pair.Value.Config);
            }
            if (result.Profiles.Keys.Distinct(StringComparer.OrdinalIgnoreCase).Count() != result.Profiles.Count)
                throw Invalid();
            return result;
        }
        static Exception Invalid() {
            return new InvalidDataException(UiText.T("The shared profile file is invalid or unsupported. It has not been overwritten.", "Den fælles profilfil er ugyldig eller ikke understøttet. Den er ikke overskrevet."));
        }
        static void Validate(SavedData data) {
            if (data == null || data.Cameras == null || data.Seconds < 0 || data.Seconds > 15)
                throw Invalid();
            if (data.FastPanSteps < 1 || data.FastPanSteps > 100)
                throw Invalid();
            if (data.PanSteps < 1 || data.PanSteps > 100 || data.ZoomSteps < 1 || data.ZoomSteps > 100 || data.FastZoomSteps < 1 || data.FastZoomSteps > 100)
                throw Invalid();
            if (data.Layout == null)
                data.Layout = new ProfileLayout();
            data.Layout.ControllerRatio = LayoutPolicy.Ratio(data.Layout.ControllerRatio);
            foreach (var kv in data.Cameras)
            if (kv.Key == null || kv.Value == null || kv.Value.Any(p => p == null || string.IsNullOrWhiteSpace(p.Name) || p.Position == null))
                throw Invalid();
        }
        static void CheckName(string name) {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 60 || name.Any(char.IsControl))
                throw new InvalidDataException(UiText.T("Use a profile name of 1–60 characters without line breaks.", "Brug et profilnavn på 1–60 tegn uden linjeskift."));
        }
        public ProfileEntry Save(string name, SavedData data, long expectedRevision) {
            return Locked(() => {
                var c = Load();
                ProfileEntry old;
                if (!c.Profiles.TryGetValue(name, out old) || old.Revision != expectedRevision)
                    throw new IOException(UiText.T("This profile was changed elsewhere. Restart the application before saving again.", "Profilen er ændret et andet sted. Genstart programmet før du gemmer igen."));
                Validate(data);
                var updated = new ProfileEntry { Revision = checked(old.Revision + 1), Config = Copy(data) };
                c.Profiles[name] = updated;
                Write(c);
                return updated;
            });
        }
        public ProfileCatalog Create(string name, string copyFrom = null) {
            name = (name ?? "").Trim();
            CheckName(name);
            return Locked(() => {
                var c = Load();
                if (c.Profiles.Keys.Any(k => string.Equals(k, name, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException(UiText.T("A profile with that name already exists.", "Der findes allerede en profil med det navn."));
                c.Profiles[name] = new ProfileEntry { Config = copyFrom == null ? new SavedData() : Copy(c.Profiles[copyFrom].Config) };
                Write(c);
                return c;
            });
        }
        public static SavedData Copy(SavedData data) {
            var s = new DataContractJsonSerializer(typeof(SavedData));
            using (var m = new MemoryStream()) {
                s.WriteObject(m, data);
                m.Position = 0;
                return (SavedData)s.ReadObject(m);
            }
        }
        public ProfileCatalog Rename(string oldName, string newName, long revision) {
            newName = (newName ?? "").Trim();
            CheckName(newName);
            return Locked(() => { var c = Load(); if (oldName == Standard) throw new InvalidOperationException(UiText.T("The Standard profile cannot be renamed.", "Standardprofilen kan ikke omdøbes.")); if (!c.Profiles.ContainsKey(oldName) || c.Profiles[oldName].Revision != revision) throw new IOException("Profile changed; reload first."); if (c.Profiles.Keys.Any(k => k != oldName && string.Equals(k, newName, StringComparison.OrdinalIgnoreCase))) throw new InvalidOperationException(UiText.T("Name already exists.", "Navnet findes allerede.")); var entry = c.Profiles[oldName]; entry.Revision++; c.Profiles.Remove(oldName); c.Profiles[newName] = entry; Write(c); return c; });
        }
        void Write(ProfileCatalog data) {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            string temp = FilePath + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try {
                using (var f = File.Create(temp))
                    new DataContractJsonSerializer(typeof(ProfileCatalog)).WriteObject(f, data);
                if (File.Exists(FilePath))
                    File.Replace(temp, FilePath, FilePath + ".bak");
                else
                    File.Move(temp, FilePath);
            } finally { if (File.Exists(temp)) File.Delete(temp); }
        }
    }
}
