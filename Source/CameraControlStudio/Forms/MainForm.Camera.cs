using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        int? recallFieldOfView;
        void AllowCamera()
        {
            if (!loaded)
                throw new InvalidOperationException(UiText.T("Load a profile first.", "Indlæs først en profil."));
            if (!permission.Allow(streamingCheck.Checked, () => ShowMessage(UiText.T("Has streaming started? Choose Yes to enable camera controls.", "Er streaming startet? Vælg Ja for at aktivere kamerastyringen."), UiText.T("Confirm streaming", "Bekræft streaming"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
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
            string framingWarning = ApplyStartupFieldOfView();
            RefreshMotionControls();
            cameraStatus.Text = UiText.T("Camera", "Kamera") + ": " + device.Name;
            ApplyImageValues();
            BuildImageControls();
            RefreshPresets();
            MarkDirty();
            status.Text = framingWarning ?? UiText.T("Ready", "Klar");
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
            try { disconnectedCamera?.Dispose(); }
            catch (Exception ex)
            {
                DiagnosticLog.Current.Write("WARNING", "Release camera connection", ex);
            }
            RefreshMotionControls();
            RefreshActionButtons();
            SetVideoTabs(Array.Empty<TabPage>());
            cameraStatus.Text = UiText.T("Camera: not connected", "Kamera: ikke tilsluttet");
        }
        void StopMotion()
        {
            movement.Stop();
            StopHeld();
            clock.Stop();
            activity.Clear();
            presetGrid.Invalidate();
        }
        Func<bool> repeatHeld;
        double dragPan, dragTilt;
        double pendingPan, pendingTilt;

        void StopHeld()
        {
            held.Stop();
            repeatHeld = null;
            pendingPan = pendingTilt = 0;
            dragPan = dragTilt = 0;
        }

        void StartZoomHeld(int direction)
        {
            StopHeld();
            int steps = (int)zoomSteps.Value;
            if (!Nudge("Zoom", direction, steps) || (Control.MouseButtons & MouseButtons.Left) == 0)
                return;
            repeatHeld = () => Nudge("Zoom", direction, steps);
            held.Interval = ManualMotion.DefaultRepeatInterval;
            held.Start();
        }

        void StartPanHeld(int pan, int tilt, PanSpeed speed)
        {
            StopHeld();
            NeedCamera();
            int steps = (int)(speed == PanSpeed.Fast ? fastPanSteps.Value : panSteps.Value);
            double share = pan != 0 && tilt != 0 ? PanMotion.DiagonalShare : 1;
            int clickSteps = speed == PanSpeed.Fine ? 1 : Math.Max(1,
                (int)Math.Round(steps * share, MidpointRounding.AwayFromZero));
            if (!MovePan(pan, tilt, clickSteps, clickSteps) || !wheel.IsHoldingDirection)
                return;
            double rate = speed == PanSpeed.Fine ? (double)(panSteps.Value * finePanPercent.Value / 100) : steps * share;
            repeatHeld = () => RepeatPan(pan * rate, tilt * rate);
            held.Interval = PanMotion.Interval;
            held.Start();
        }

        void UpdatePanDrag(double pan, double tilt)
        {
            if (pan == 0 && tilt == 0)
            {
                StopHeld();
                return;
            }
            if (!held.Enabled)
            {
                NeedCamera();
                if (!wheel.Capture)
                    return;
                repeatHeld = () => RepeatPan(dragPan * (double)fastPanSteps.Value * 2,
                    dragTilt * (double)fastPanSteps.Value * 2);
                held.Interval = PanMotion.Interval;
                held.Start();
            }
            if (Math.Sign(pan) != Math.Sign(dragPan))
                pendingPan = 0;
            if (Math.Sign(tilt) != Math.Sign(dragTilt))
                pendingTilt = 0;
            dragPan = pan;
            dragTilt = tilt;
        }

        bool RepeatPan(double panRate, double tiltRate)
        {
            const double intervalShare = PanMotion.Interval / (double)ManualMotion.DefaultRepeatInterval;
            int pan = PanMotion.TakeSteps(ref pendingPan, panRate * intervalShare);
            int tilt = PanMotion.TakeSteps(ref pendingTilt, tiltRate * intervalShare);
            return MovePan(Math.Sign(panRate), Math.Sign(tiltRate), Math.Abs(pan), Math.Abs(tilt));
        }

        bool MovePan(int pan, int tilt, int panSteps, int tiltSteps)
        {
            bool canPan = MovePanAxis("Pan", pan, panSteps);
            bool canTilt = MovePanAxis("Tilt", tilt, tiltSteps);
            return canPan || canTilt;
        }

        bool MovePanAxis(string axis, int direction, int steps) =>
            wheel.CanMoveAxis(axis, direction) && HasManualAxis(axis, out _) &&
            (steps == 0 || Nudge(axis, direction, steps));

        bool HasManualAxis(string axis, out Setting setting)
        {
            setting = null;
            return camera != null && camera.Settings.TryGetValue(axis, out setting) && (setting.Caps & 2) != 0;
        }
        bool AxisReversed(string axis) => axis == "Pan" ? reverseX.Checked : axis == "Tilt" && reverseY.Checked;
        void RefreshMotionControls()
        {
            int? fieldOfView = ReadFieldOfView();
            if (fieldOfView != currentFieldOfView)
            {
                blockedFieldOfViewSteps.Clear();
                if (fieldOfView.HasValue)
                    camera.RefreshMotionRanges();
            }
            currentFieldOfView = fieldOfView;
            UpdateFieldOfViewControls();
            foreach (var axis in axes)
                RefreshMotionAxis(axis);
            RefreshPositionText();
            if (activity.Phase == PresetPhase.Reached &&
                (target.Any(p => !currentAxes.TryGetValue(p.Key, out int value) || value != p.Value) ||
                (recallFieldOfView.HasValue && currentFieldOfView != recallFieldOfView)))
            {
                activity.Clear();
                status.Text = UiText.T("Camera position changed.", "Kameraets position er ændret.");
                presetGrid.Invalidate();
            }
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
                catch (COMException ex) when (!(ex is CameraUnavailableException))
                {
                    if (failedMotionReads.Add(axis))
                        DiagnosticLog.Current.Write("WARNING", "Read camera position: " + axis, ex);
                }
            }
            bool canConnect = loaded && camera == null;
            currentAxes.Remove(axis);
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
            if (currentAxes.TryGetValue(axis, out int before) && before != position)
            {
                blockedFieldOfViewSteps.Clear();
                if (axis == "Zoom")
                {
                    camera.RefreshMotionRanges();
                    HasManualAxis(axis, out setting);
                }
            }
            currentAxes[axis] = position;
            bool negative = CanNudge(axis, setting, position, -1);
            bool positive = CanNudge(axis, setting, position, 1);
            if (axis == "Zoom")
            {
                zoomSlider.Position = ManualMotion.ZoomFraction(setting, position);
                zoomSlider.Enabled = setting.Snap(setting.Max) > setting.Min;
            }
            SetMotionAvailability(axis, negative, positive);
        }
        bool CanNudge(string axis, Setting setting, int position, int direction)
        {
            return !blockedFieldOfViewSteps.Contains((axis, direction)) &&
                (ManualMotion.CanMove(setting, position, direction, AxisReversed(axis)) ||
                AdjacentFieldOfView(axis, direction, currentFieldOfView).HasValue);
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
        bool Nudge(string axis, int direction, int steps)
        {
            NeedCamera();
            if (!HasManualAxis(axis, out var setting))
                throw new InvalidOperationException(UiText.T("This camera control is not supported.", "Denne kamerafunktion understøttes ikke."));
            int position = camera.Get(axis);
            bool reverse = AxisReversed(axis);
            bool panStalled = false;
            if (ManualMotion.CanMove(setting, position, direction, reverse) &&
                !blockedFieldOfViewSteps.Contains((axis, direction)))
            {
                SetManualPosition(axis, ManualMotion.Target(setting, position, direction, steps, reverse));
                int updated = camera.Get(axis);
                if (updated != position || axis != "Pan")
                {
                    UpdateMotionAxis(axis, setting, updated);
                    if (axis == "Zoom" && updated != position)
                    {
                        RefreshMotionAxis("Pan");
                        RefreshMotionAxis("Tilt");
                    }
                    RefreshPositionText();
                    return CanNudge(axis, camera.Settings[axis], updated, direction);
                }
                panStalled = true;
            }
            bool crossed = CrossFieldOfViewLimit(axis, direction, steps, reverse);
            if (!crossed && panStalled)
                blockedFieldOfViewSteps.Add((axis, direction));
            RefreshMotionControls();
            return crossed && currentAxes.TryGetValue(axis, out int next) &&
                HasManualAxis(axis, out setting) && CanNudge(axis, setting, next, direction);
        }
        void SetZoomPosition(double fraction)
        {
            if (!HasManualAxis("Zoom", out var setting))
                return;

            int position = camera.Get("Zoom");
            int desired = ManualMotion.ZoomAtFraction(setting, fraction);
            if (position != desired)
            {
                StopHeld();
                SetManualPosition("Zoom", desired);
                position = camera.Get("Zoom");
                UpdateMotionAxis("Zoom", setting, position);
                RefreshMotionAxis("Pan");
                RefreshMotionAxis("Tilt");
                RefreshPositionText();
            }
        }
        void SetManualPosition(string axis, int position)
        {
            BeginManualMovement();
            camera.Set(axis, position);
        }

        void BeginManualMovement()
        {
            movement.Stop();
            clock.Stop();
            activity.Clear();
            presetGrid.Invalidate();
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
            if (row.Preset.FieldOfView is int degrees)
            {
                if (camera.FieldOfView == null ||
                    (!FieldOfViewAngles.Contains(degrees) && ReadFieldOfView() != degrees))
                    throw new InvalidOperationException(UiText.T("The preset FOV is unavailable on this camera.", "Positionens synsfelt er ikke tilgængeligt på dette kamera."));
                camera.FieldOfView.Write(degrees);
                if (camera.FieldOfView.Read() != degrees)
                    throw new InvalidOperationException(UiText.T("The camera did not reach the preset FOV.", "Kameraet nåede ikke positionens synsfelt."));
                RefreshMotionControls();
            }
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
            recallFieldOfView = row.Preset.FieldOfView;
            activity.Begin(row.Preset.Name);
            status.Text = UiText.T("Moving to position…", "Flytter til position…");
            presetGrid.Invalidate();
            speedCurve = useTransitionSpeed.Checked ? new PresetSpeedCurve(
                target.Max(p => PresetSpeedCurve.AxisDistance(camera.Settings[p.Key], start[p.Key], p.Value)),
                (double)transitionSpeedPercent.Value / 100) : null;
            duration = speedCurve?.Duration ?? (double)seconds.Value;
            clock.Restart();
            movement.Start();
            TickMotion();
        }
        void TickMotion()
        {
            double elapsed = clock.Elapsed.TotalSeconds;
            bool finished = elapsed >= duration;
            double f = finished ? 1 : speedCurve?.Progress(elapsed) ?? Motion.Ease(elapsed / duration);
            foreach (var p in target)
            {
                int value = camera.Settings[p.Key].Snap(start[p.Key] + (p.Value - (double)start[p.Key]) * f);
                if (value != last[p.Key] || finished)
                {
                    camera.Set(p.Key, value);
                    last[p.Key] = value;
                }
            }
            if (finished)
            {
                bool reached = target.All(p => camera.Get(p.Key) == p.Value)
                    && (!recallFieldOfView.HasValue || ReadFieldOfView() == recallFieldOfView);
                if (!reached && elapsed < duration + 2)
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
