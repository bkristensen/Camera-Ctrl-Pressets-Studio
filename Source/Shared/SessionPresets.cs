using System.Collections.Generic;

namespace CameraControlStudio
{
    // This store deliberately lives outside SavedData and is never serialized.
    public sealed class SessionPresets
    {
        readonly Dictionary<string, Dictionary<string, List<Preset>>> profiles = new Dictionary<string, Dictionary<string, List<Preset>>>();
        public void RenameProfile(string before, string after)
        {
            if (before == after)
                return;
            if (profiles.TryGetValue(before, out var cameras))
            {
                profiles.Remove(before);
                profiles[after] = cameras;
            }
        }

        public List<Preset> For(string profile, string camera)
        {
            if (!profiles.TryGetValue(profile, out var cameras))
                profiles[profile] = cameras = new Dictionary<string, List<Preset>>();
            if (!cameras.TryGetValue(camera, out var items))
                cameras[camera] = items = new List<Preset>();
            return items;
        }
    }
}
