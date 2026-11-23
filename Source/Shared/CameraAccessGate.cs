using System;

namespace CameraControlStudio
{
    // Session-only permission. No process name can establish that streaming is ready.
    public sealed class CameraAccessGate
    {
        public bool Approved { get; private set; }

        public void Reset()
        {
            Approved = false;
        }

        public bool Allow(bool confirmationRequired, Func<bool> confirm)
        {
            if (!confirmationRequired || Approved)
                return true;
            Approved = confirm();
            return Approved;
        }
    }
}
