using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        int? currentFieldOfView;
        bool startupFramingPending = true;
        bool loggedFieldOfViewReadFailure;
        int[] displayedFieldOfViewAngles;
        int[] FieldOfViewAngles => camera?.FieldOfView?.AvailableAngles ?? Array.Empty<int>();
        readonly Dictionary<string, int> currentAxes = new Dictionary<string, int>();
        readonly HashSet<(string Axis, int Direction)> blockedFieldOfViewSteps = new HashSet<(string, int)>();

        void WireFraming()
        {
            string driverTooltip = UiText.T("The camera driver determines which field-of-view angles you can select.",
                "Kameradriveren afgør, hvilke vinkler du kan vælge.");
            settings.startupFovLabel.AccessibleDescription = fieldOfViewLabel.AccessibleDescription = driverTooltip;
            mainToolTips.SetToolTip(settings.startupFovLabel, driverTooltip);
            mainToolTips.SetToolTip(fieldOfViewLabel, driverTooltip);
            settings.startupFov.SelectedIndexChanged += (sender, args) => RunSettings(() =>
            {
                if (applying || config == null || settings.startupFov.SelectedIndex < 0)
                    return;
                int selection = settings.startupFov.SelectedIndex;
                config.StartupFieldOfView = selection < 2 ? -selection : displayedFieldOfViewAngles[selection - 2];
                MarkDirty();
            });
            settings.allowFov.CheckedChanged += (sender, args) => RunSettings(() =>
            {
                if (applying || config == null)
                    return;
                config.AllowFieldOfViewChange = settings.allowFov.Checked;
                UpdateFieldOfViewControls();
                MarkDirty();
            });
            settings.adjustFovAtLimits.CheckedChanged += (sender, args) => RunSettings(() =>
            {
                if (applying || config == null)
                    return;
                config.AdjustFieldOfViewAtLimits = settings.adjustFovAtLimits.Checked;
                blockedFieldOfViewSteps.Clear();
                RefreshMotionControls();
                MarkDirty();
            });
            fieldOfViewPicker.SelectionChangeCommitted += (sender, args) => Run(() =>
            {
                if (fieldOfViewPicker.SelectedIndex < 0 || camera?.FieldOfView == null)
                    return;
                StopMotion();
                try
                {
                    int degrees = displayedFieldOfViewAngles[fieldOfViewPicker.SelectedIndex];
                    camera.FieldOfView.Write(degrees);
                    if (camera.FieldOfView.Read() != degrees)
                        throw new InvalidOperationException(UiText.T("The camera did not apply the selected FOV.",
                            "Kameraet anvendte ikke det valgte synsfelt."));
                }
                finally { RefreshMotionControls(); }
            });
            fullFrame.Click += (sender, args) => Run(RestoreFullFrame);
        }

        void LoadFramingSettings()
        {
            settings.allowFov.Checked = config.AllowFieldOfViewChange;
            settings.adjustFovAtLimits.Checked = config.AdjustFieldOfViewAtLimits;
            UpdateFieldOfViewControls();
            SelectStartupFieldOfView();
        }

        void SelectStartupFieldOfView()
        {
            int selection = config?.StartupFieldOfView ?? 0;
            int index = Array.IndexOf(displayedFieldOfViewAngles, selection);
            settings.startupFov.SelectedIndex = index >= 0 ? index + 2 : selection == 0 ? 0 : selection == -1 ? 1 : -1;
        }

        void UpdateFieldOfViewOptions(int[] angles)
        {
            if (displayedFieldOfViewAngles == angles)
                return;
            displayedFieldOfViewAngles = angles;
            bool wasApplying = applying;
            applying = true;
            try
            {
                settings.startupFov.Items.Clear();
                settings.startupFov.Items.Add(UiText.T("Widest available", "Bredest muligt"));
                settings.startupFov.Items.Add(UiText.T("Do nothing", "Gør ingenting"));
                fieldOfViewPicker.Items.Clear();
                foreach (int angle in angles)
                {
                    string text = angle + "°";
                    settings.startupFov.Items.Add(text);
                    fieldOfViewPicker.Items.Add(text);
                }
                SelectStartupFieldOfView();
            }
            finally { applying = wasApplying; }
        }

        int? AdjacentFieldOfView(string axis, int direction, int? current)
        {
            var angles = FieldOfViewAngles;
            if (config?.AdjustFieldOfViewAtLimits != true || !current.HasValue ||
                !angles.Contains(current.Value) || blockedFieldOfViewSteps.Contains((axis, direction)))
                return null;
            if (axis == "Zoom")
                return ManualMotion.NextFieldOfView(angles, current.Value, direction);
            if (axis == "Pan" && HasManualAxis("Zoom", out var zoom) && currentAxes.TryGetValue("Zoom", out int position))
                return ManualMotion.PanFieldOfViewOptions(angles, current.Value, zoom, position)
                    .Select(value => (int?)value).FirstOrDefault();
            return null;
        }

        bool CrossFieldOfViewLimit(string axis, int direction, int steps, bool reverse)
        {
            if (config?.AdjustFieldOfViewAtLimits != true || (axis != "Zoom" && axis != "Pan"))
                return false;
            int? originalFov = ReadFieldOfView();
            int? next = AdjacentFieldOfView(axis, direction, originalFov);
            if (!next.HasValue || !HasManualAxis("Zoom", out var originalZoom))
                return false;

            var pose = ReadPose();
            BeginManualMovement();
            try
            {
                if (axis == "Zoom")
                {
                    if (SetFieldOfViewForMovement(next.Value, out var zoom))
                    {
                        double? equivalent = ManualMotion.EquivalentZoom(originalZoom, pose["Zoom"],
                            originalFov.Value, zoom, next.Value);
                        SetAxisIfDifferent("Zoom", ManualMotion.ZoomAfterFieldOfViewStep(zoom,
                            camera.Get("Zoom"), equivalent, direction));
                        camera.RefreshMotionRanges();
                        RestorePanAndTilt(pose);
                        return true;
                    }
                }
                else
                {
                    foreach (int candidate in ManualMotion.PanFieldOfViewOptions(FieldOfViewAngles,
                        originalFov.Value, originalZoom, pose["Zoom"]))
                    {
                        if (!SetFieldOfViewForMovement(candidate, out var zoom))
                            continue;
                        double? equivalent = ManualMotion.EquivalentZoom(originalZoom, pose["Zoom"],
                            originalFov.Value, zoom, candidate);
                        if (!equivalent.HasValue || Math.Abs(equivalent.Value - zoom.Snap(equivalent.Value)) > zoom.Step)
                            continue;
                        SetAxisIfDifferent("Zoom", zoom.Snap(equivalent.Value));
                        camera.RefreshMotionRanges();
                        if (!HasManualAxis("Pan", out var pan) ||
                            !ManualMotion.CanMove(pan, pose["Pan"], direction, reverse))
                            continue;
                        RestorePanAndTilt(pose);
                        int previous = camera.Get("Pan");
                        if (previous != pan.Snap(pose["Pan"]))
                            continue;
                        SetAxisIfDifferent("Pan", ManualMotion.Target(pan, previous, direction, steps, reverse));
                        int actual = camera.Get("Pan");
                        if ((actual - (long)pose["Pan"]) * (reverse ? -direction : direction) > 0)
                            return true;
                    }
                }
            }
            catch (COMException ex) when (!(ex is CameraUnavailableException))
            {
                DiagnosticLog.Current.Write("WARNING", "Change field of view at a movement limit", ex);
            }

            camera.FieldOfView.Write(originalFov.Value);
            camera.RefreshMotionRanges();
            if (HasManualAxis("Zoom", out var restoredZoom))
            {
                SetAxisIfDifferent("Zoom", restoredZoom.Snap(pose["Zoom"]));
                camera.RefreshMotionRanges();
            }
            RestorePanAndTilt(pose);
            if (camera.FieldOfView.Read() != originalFov.Value ||
                pose.Any(value => !HasManualAxis(value.Key, out _) || camera.Get(value.Key) != value.Value))
                throw new InvalidOperationException(UiText.T("The camera could not restore its previous framing.",
                    "Kameraet kunne ikke gendanne det forrige udsnit."));
            blockedFieldOfViewSteps.Add((axis, direction));
            return false;
        }

        bool SetFieldOfViewForMovement(int degrees, out Setting zoom)
        {
            camera.FieldOfView.Write(degrees);
            camera.RefreshMotionRanges();
            return HasManualAxis("Zoom", out zoom) && camera.FieldOfView.Read() == degrees;
        }

        void RestorePanAndTilt(Dictionary<string, int> pose)
        {
            foreach (string axis in new[] { "Pan", "Tilt" })
                if (pose.TryGetValue(axis, out int value) && HasManualAxis(axis, out var setting))
                    SetAxisIfDifferent(axis, setting.Snap(value));
        }

        void SetAxisIfDifferent(string axis, int value)
        {
            if (camera.Get(axis) != value)
                camera.Set(axis, value);
        }

        string ApplyStartupFieldOfView()
        {
            if (!startupFramingPending)
                return null;
            startupFramingPending = false;
            var angles = FieldOfViewAngles;
            if (config.StartupFieldOfView == -1 || angles.Length == 0)
                return null;
            int selection = config.StartupFieldOfView == 0 ? angles[0] : config.StartupFieldOfView;
            if (!angles.Contains(selection))
                return UiText.T("Could not apply the startup FOV.", "Kunne ikke anvende synsfeltet ved opstart.");
            try
            {
                camera.FieldOfView.Write(selection);
                if (camera.FieldOfView.Read() == selection)
                    return null;
            }
            catch (COMException ex) when (!(ex is CameraUnavailableException))
            {
                DiagnosticLog.Current.Write("WARNING", "Set startup field of view", ex);
            }
            return UiText.T("Could not apply the startup FOV.", "Kunne ikke anvende synsfeltet ved opstart.");
        }

        int? ReadFieldOfView()
        {
            try
            {
                int? value = camera?.FieldOfView?.Read();
                loggedFieldOfViewReadFailure = false;
                return value;
            }
            catch (COMException ex) when (!(ex is CameraUnavailableException))
            {
                if (!loggedFieldOfViewReadFailure)
                    DiagnosticLog.Current.Write("WARNING", "Read field of view", ex);
                loggedFieldOfViewReadFailure = true;
                return null;
            }
        }

        void UpdateFieldOfViewControls()
        {
            var angles = FieldOfViewAngles;
            UpdateFieldOfViewOptions(angles);
            settings.startupFov.Enabled = angles.Length > 0;
            settings.allowFov.Enabled = settings.adjustFovAtLimits.Enabled = angles.Length > 1;
            bool editing = config?.AllowFieldOfViewChange == true && angles.Length > 1;
            fieldOfViewPicker.Visible = editing;
            fieldOfViewLabel.Visible = !editing;
            fieldOfViewLabel.Text = currentFieldOfView.HasValue ? "FOV: " + currentFieldOfView + "°" :
                UiText.T("FOV unavailable", "Synsfelt utilgængeligt");
            if (!fieldOfViewPicker.DroppedDown)
                fieldOfViewPicker.SelectedIndex = Array.IndexOf(angles, currentFieldOfView ?? 0);
        }

        void RefreshPositionText()
        {
            motionPosition.Text = string.Join(" · ", axes.Where(currentAxes.ContainsKey).Select(axis =>
                UiText.Translate(axis, axis) + " " + currentAxes[axis]));
        }

        void UpdateFullFrameAvailability()
        {
            if (camera == null)
            {
                fullFrame.Enabled = loaded;
                return;
            }

            int? minimumZoom = HasManualAxis("Zoom", out var zoom) ? zoom.Min : (int?)null;
            int? currentZoom = currentAxes.TryGetValue("Zoom", out int position) ? position : (int?)null;
            var angles = FieldOfViewAngles;
            int? widestFov = angles.Length > 0 ? angles[0] : (int?)null;
            fullFrame.Enabled = ManualMotion.CanRestoreFullFrame(minimumZoom, currentZoom, widestFov, currentFieldOfView);
        }

        void RestoreFullFrame()
        {
            StopMotion();
            NeedCamera();
            var angles = FieldOfViewAngles;
            bool fovReached = false;
            bool zoomReached = false;
            try
            {
                if (angles.Length > 0)
                {
                    int widest = angles[0];
                    camera.FieldOfView.Write(widest);
                    fovReached = camera.FieldOfView.Read() == widest;
                }
            }
            catch (COMException ex) when (!(ex is CameraUnavailableException))
            {
                DiagnosticLog.Current.Write("WARNING", "Restore widest field of view", ex);
            }
            try
            {
                camera.RefreshMotionRanges();
                if (HasManualAxis("Zoom", out var zoom))
                {
                    if (camera.Get("Zoom") != zoom.Min)
                        camera.Set("Zoom", zoom.Min);
                    zoomReached = camera.Get("Zoom") == zoom.Min;
                }
            }
            finally { RefreshMotionControls(); }
            status.Text = fovReached && zoomReached
                ? UiText.T("Full frame restored.", "Fuldt billede gendannet.")
                : zoomReached
                    ? UiText.T("Minimum zoom reached. Widest FOV could not be verified.", "Mindste zoom er nået. Bredeste synsfelt kunne ikke bekræftes.")
                    : UiText.T("Minimum zoom could not be verified.", "Mindste zoom kunne ikke bekræftes.");
        }
    }
}
