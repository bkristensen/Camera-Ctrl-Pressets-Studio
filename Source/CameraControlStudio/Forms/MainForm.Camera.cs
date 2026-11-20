using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        void AllowCamera()
        {
            if (!loaded)
                throw new InvalidOperationException(UiText.T("Load a profile first.", "Indlæs først en profil."));
            if (!permission.Allow(streamingCheck.Checked, () => ShowMessage(
                UiText.T("Has streaming started? Choose Yes to enable camera controls.",
                    "Er streaming startet? Vælg Ja for at aktivere kamerastyringen."),
                UiText.T("Confirm streaming", "Bekræft streaming"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                throw new OperationCanceledException();
        }

        void EnumerateDevices()
        {
            AllowCamera();
            Disconnect();
            foreach (var d in devices)
                d.Dispose();
            devices.Clear();
            devices = Camera.Enumerate();
        }

        void NeedCamera()
        {
            AllowCamera();
            if (camera != null)
                return;
            EnumerateDevices();
            var d = devices.FirstOrDefault(x => x.Id == CameraId);
            if (d == null && !string.IsNullOrEmpty(CameraId))
                throw new InvalidOperationException(UiText.T("The saved camera is unavailable. Select another source using the center button.", "Det gemte kamera er ikke tilgængeligt. Vælg en anden kilde med midterknappen."));
            d = d ?? devices.FirstOrDefault();
            if (d == null)
                throw new InvalidOperationException(UiText.T("No camera found.", "Intet kamera fundet."));
            Connect(d);
        }

        void Connect(Device device)
        {
            AllowCamera();
            StopMotion();
            camera?.Dispose();
            camera = null;
            DiagnosticLog.Current.CameraName = device.Name;
            camera = new Camera(device);
            if (CameraId != device.Id)
                ClearPresetSelection();
            config.CameraId = device.Id;
            RefreshMotionControls();
            cameraStatus.Text = UiText.T("Camera", "Kamera") + ": " + device.Name;
            ApplyImageValues();
            BuildImageControls();
            RefreshPresets();
            MarkDirty();
            status.Text = UiText.T("Ready", "Klar");
            motionLimits.Start();
        }

        void ChooseCamera()
        {
            StopMotion();
            EnumerateDevices();
            using (var dialog = new CameraSourceForm(devices, CameraId))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    Connect(dialog.Selected);
            }
        }

        void Disconnect()
        {
            StopMotion();
            motionLimits.Stop();
            failedMotionReads.Clear();
            var disconnectedCamera = camera;
            camera = null;
            try
            {
                disconnectedCamera?.Dispose();
            }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Release camera connection", ex);
            }

            RefreshMotionControls();
            RefreshActionButtons();
            ShowVideoControls(false);
            cameraStatus.Text = UiText.T("Camera: not connected", "Kamera: ikke tilsluttet");
        }

        void StopMotion()
        {
            movement.Stop();
            held.Stop();
            clock.Stop();
            activity.Clear();
            presetGrid.Invalidate();
        }

        void StartHeld(string axis, int direction, bool fast)
        {
            held.Stop();
            if (!Nudge(axis, direction, fast))
                return;
            heldAxis = axis;
            heldDirection = direction;
            heldFast = fast;
            if ((Control.MouseButtons & MouseButtons.Left) != 0)
                held.Start();
        }

        bool HasManualAxis(string axis, out Setting setting)
        {
            setting = null;
            return camera != null && camera.Settings.TryGetValue(axis, out setting) && (setting.Caps & 2) != 0;
        }

        bool AxisReversed(string axis) => axis == "Pan" ? reverseX.Checked : axis == "Tilt" && reverseY.Checked;
        void RefreshMotionControls()
        {
            foreach (var axis in axes)
                RefreshMotionAxis(axis);
        }

        void RefreshMotionAxis(string axis)
        {
            if (HasManualAxis(axis, out var setting))
            {
                try
                {
                    UpdateMotionAxis(axis, setting, camera.Get(axis));
                    failedMotionReads.Remove(axis);
                    return;
                }
                catch (COMException ex)when (!(ex is CameraUnavailableException))
                {
                    if (failedMotionReads.Add(axis))
                        DiagnosticLog.Current.Write("WARNING", "Read camera position: " + axis, ex);
                }
            }

            bool canConnect = loaded && camera == null;
            if (axis == "Zoom")
            {
                zoomSlider.Enabled = false;
                if (camera == null)
                    zoomSlider.Position = .5;
            }

            SetMotionAvailability(axis, canConnect, canConnect);
        }

        void UpdateMotionAxis(string axis, Setting setting, int position)
        {
            bool reverse = AxisReversed(axis);
            bool negative = ManualMotion.CanMove(setting, position, -1, reverse);
            bool positive = ManualMotion.CanMove(setting, position, 1, reverse);
            if (axis == "Zoom")
            {
                zoomSlider.Position = ManualMotion.ZoomFraction(setting, position);
                zoomSlider.Enabled = negative || positive;
            }

            SetMotionAvailability(axis, negative, positive);
        }

        void SetMotionAvailability(string axis, bool negative, bool positive)
        {
            if (axis == "Zoom")
            {
                normalOut.Enabled = negative;
                normalIn.Enabled = positive;
            }
            else
            {
                wheel.SetDirectionAvailability(axis, negative, positive);
            }
        }

        bool Nudge(string axis, int direction, bool fast)
        {
            NeedCamera();
            if (!HasManualAxis(axis, out var setting))
                throw new InvalidOperationException(UiText.T("This camera control is not supported.", "Denne kamerafunktion understøttes ikke."));
            int position = camera.Get(axis);
            bool reverse = AxisReversed(axis);
            if (!ManualMotion.CanMove(setting, position, direction, reverse))
            {
                UpdateMotionAxis(axis, setting, position);
                return false;
            }

            decimal stepCount = axis == "Zoom" ? zoomSteps.Value : fast ? fastPanSteps.Value : panSteps.Value;
            int steps = (int)stepCount;
            SetManualPosition(axis, ManualMotion.Target(setting, position, direction, steps, reverse));
            position = camera.Get(axis);
            UpdateMotionAxis(axis, setting, position);
            return ManualMotion.CanMove(setting, position, direction, reverse);
        }

        void SetZoomPosition(double fraction)
        {
            if (!HasManualAxis("Zoom", out var setting))
                return;
            int position = camera.Get("Zoom");
            int desired = ManualMotion.ZoomAtFraction(setting, fraction);
            if (position != desired)
            {
                held.Stop();
                SetManualPosition("Zoom", desired);
                position = camera.Get("Zoom");
            }

            UpdateMotionAxis("Zoom", setting, position);
        }

        void SetManualPosition(string axis, int position)
        {
            movement.Stop();
            clock.Stop();
            activity.Clear();
            presetGrid.Invalidate();
            camera.Set(axis, position);
            status.Text = UiText.T("Manual movement", "Manuel bevægelse");
        }

        void Recall(RowItem row)
        {
            if (row == null)
                return;
            SelectPresetInSettings(row);
            StopMotion();
            NeedCamera();
            if (row.DeviceId != CameraId)
                throw new InvalidOperationException(UiText.T("Select a preset for this camera.", "Vælg en position til dette kamera."));
            start = ReadPose();
            target = new Dictionary<string, int>();
            last = new Dictionary<string, int>(start);
            foreach (var pair in row.Preset.Position)
            {
                if (!start.ContainsKey(pair.Key) || camera.Settings[pair.Key].Snap(pair.Value) != pair.Value)
                    throw new InvalidOperationException(UiText.T("The camera range has changed. Save this preset again.", "Kameraets område er ændret. Gem positionen igen."));
                target[pair.Key] = pair.Value;
            }

            if (target.Count == 0)
                throw new InvalidOperationException(UiText.T("Empty preset.", "Positionen er tom."));
            activity.Begin(row.Preset.Name);
            status.Text = UiText.T("Moving to position…", "Flytter til position…");
            presetGrid.Invalidate();
            duration = (double)seconds.Value;
            clock.Restart();
            movement.Start();
            TickMotion();
        }

        void TickMotion()
        {
            double progress = duration <= 0 ? 1 : Math.Min(1, clock.Elapsed.TotalSeconds / duration);
            double easedProgress = Motion.Ease(progress);
            foreach (var axisTarget in target)
            {
                string axis = axisTarget.Key;
                double position = start[axis] + (axisTarget.Value - (double)start[axis]) * easedProgress;
                int value = camera.Settings[axis].Snap(position);
                if (value != last[axis] || progress >= 1)
                {
                    camera.Set(axis, value);
                    last[axis] = value;
                }
            }

            if (progress >= 1)
            {
                bool reached = target.All(p => camera.Get(p.Key) == p.Value);
                // Some drivers report the old position briefly after accepting the final target.
                if (!reached && clock.Elapsed.TotalSeconds < duration + 2)
                    return;
                movement.Stop();
                clock.Stop();
                activity.Complete(reached);
                if (!reached)
                    DiagnosticLog.Current.Write("WARNING", "Camera did not reach the preset position");
                status.Text = UiText.T(reached ? "Position reached." : "The camera did not reach the position.", reached ? "Positionen er nået." : "Kameraet nåede ikke positionen.");
                RefreshMotionControls();
                presetGrid.Invalidate();
            }
        }
    }
}
