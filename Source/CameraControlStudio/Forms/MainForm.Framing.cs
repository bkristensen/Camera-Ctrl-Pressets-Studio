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
        readonly Dictionary<string, int> currentAxes = new Dictionary<string, int>();

        void WireFraming()
        {
            startupFov.Items.Add(UiText.T("Widest available", "Bredest muligt"));
            startupFov.Items.Add(UiText.T("Do nothing", "Gør ingenting"));
            foreach (int degrees in BrioFieldOfView.Degrees)
            {
                startupFov.Items.Add(degrees + "°");
                fieldOfViewPicker.Items.Add(degrees + "°");
            }
            startupFov.SelectedIndexChanged += (sender, args) =>
            {
                if (applying || config == null || startupFov.SelectedIndex < 0)
                    return;
                int selection = startupFov.SelectedIndex;
                config.StartupFieldOfView = selection < 2 ? -selection : BrioFieldOfView.Degrees[selection - 2];
                MarkDirty();
            };
            allowFov.CheckedChanged += (sender, args) =>
            {
                if (applying || config == null)
                    return;
                config.AllowFieldOfViewChange = allowFov.Checked;
                UpdateFieldOfViewControls();
                MarkDirty();
            };
            fieldOfViewPicker.SelectionChangeCommitted += (sender, args) => Run(() =>
            {
                if (fieldOfViewPicker.SelectedIndex < 0 || camera?.FieldOfView == null)
                    return;
                StopMotion();
                try
                {
                    int degrees = BrioFieldOfView.Degrees[fieldOfViewPicker.SelectedIndex];
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
            int index = Array.IndexOf(BrioFieldOfView.Degrees, config.StartupFieldOfView);
            startupFov.SelectedIndex = index >= 0 ? index + 2 : config.StartupFieldOfView == -1 ? 1 : 0;
            allowFov.Checked = config.AllowFieldOfViewChange;
            UpdateFieldOfViewControls();
        }

        string ApplyStartupFieldOfView()
        {
            if (!startupFramingPending)
                return null;
            startupFramingPending = false;
            if (config.StartupFieldOfView == -1 || camera.FieldOfView == null)
                return null;
            int selection = config.StartupFieldOfView;
            if (!BrioFieldOfView.Degrees.Contains(selection))
                selection = BrioFieldOfView.Degrees[0];
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
            bool editing = config?.AllowFieldOfViewChange == true && camera?.FieldOfView != null;
            fieldOfViewPicker.Visible = editing;
            fieldOfViewLabel.Visible = !editing;
            fieldOfViewLabel.Text = currentFieldOfView.HasValue ? "FOV: " + currentFieldOfView + "°" :
                UiText.T("FOV unavailable", "Synsfelt utilgængeligt");
            if (!fieldOfViewPicker.DroppedDown)
                fieldOfViewPicker.SelectedIndex = Array.IndexOf(BrioFieldOfView.Degrees, currentFieldOfView ?? 0);
        }

        void RefreshPositionText()
        {
            motionPosition.Text = string.Join(" · ", axes.Where(currentAxes.ContainsKey).Select(axis =>
                UiText.Translate(axis, axis) + " " + currentAxes[axis]));
        }

        void RestoreFullFrame()
        {
            StopMotion();
            NeedCamera();
            var widest = BrioFieldOfView.Degrees[0];
            bool fovReached = false;
            bool zoomReached = false;
            try
            {
                if (camera.FieldOfView != null)
                {
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
