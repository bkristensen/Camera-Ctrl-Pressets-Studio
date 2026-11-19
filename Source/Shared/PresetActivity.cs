namespace CameraControlStudio {
    public enum PresetPhase {
        None, Moving, Reached
    }
    public sealed class PresetActivity {
        public string Name {
            get; private set;
        }
        public PresetPhase Phase {
            get; private set;
        }
        public void Begin(string name) {
            Name = name;
            Phase = PresetPhase.Moving;
        }
        public void Complete(bool reached) {
            if (reached && Phase == PresetPhase.Moving)
                Phase = PresetPhase.Reached;
            else
                Clear();
        }
        public void Clear() {
            Name = null;
            Phase = PresetPhase.None;
        }
    }
}
