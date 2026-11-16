# Cam Pressets Studio — project description

## Purpose

Cam Pressets Studio is a small, open-source Windows desktop tool for precise
camera positioning and repeatable framing. It lets an operator save and recall
camera pan, tilt and zoom settings without managing a video production system.
It is intended for any suitable presentation, recording, conferencing, teaching,
demonstration or streaming workflow. No particular venue, organisation, subject,
meeting platform or content library defines the project.

The application controls camera properties. It does not capture, display, record,
transcode or transmit video or audio. The image is viewed in a separate application.
A camera may implement pan/tilt/zoom mechanically or as digital cropping; controls
are available only when its Windows driver exposes the relevant properties.

## Primary functions

1. Read the actual camera position and store named presets per camera/profile.
2. Recall presets with configurable transition duration and eased interpolation.
3. Provide precise normal/fast directional steps and independent zoom steps through
   a compact dual-ring wheel, with a consistent blue visual design.
4. Preserve profiles and settings reliably, including startup order and direction
   reversal, while remaining useful with settings hidden.
5. Avoid early camera access in workflows requiring other applications to initialise
   first. The default startup gate also requires explicit Zoom-meeting confirmation.

The last point currently contains a Zoom-specific readiness check. Generalising
that check to other applications is future work, not an existing generic detector.

## Interface and workflow

The main window contains the camera wheel, zoom buttons, profile selector and
preset list. A collapsible settings panel contains step sizes, transition duration,
image adjustments, startup-program settings, camera selection and always-on-top.
The window can be resized within documented minimum dimensions. English is the
default; Danish is selected for a Danish Windows display language.

Presets store PTZ coordinates only. They do not store a video composition, a
screen-sharing scene, HDR mode, field of view or the complete camera configuration.
Direction reversal changes arrow commands; it does not rotate the actual image.

## Profiles and data

Standard is the initial profile. New profiles can be empty or copied. Per-camera
preset lists, movement preferences, startup order and optional startup-settings
code are stored locally. The unpackaged app uses
`%LOCALAPPDATA%\CamPressets\profiles.json`.

The persistence layer performs revision checks and atomic replacement with a
backup. Legacy Studio/Mini files can be imported without deleting the originals.
Store/MSIX data redirection and migration still need packaged Windows testing;
identical data behaviour between portable and Store editions is not promised.

## Technical structure

- C# / .NET Framework 4.8 Windows Forms desktop application.
- Custom drawn wheel/buttons; native Windows window controls and dialogs.
- DirectShow COM camera control and video-property interfaces through the driver.
- Shared profile models and storage under SharedProfiles; UI support under SharedUi.
- Timed PTZ interpolation with snapped driver-supported values, cancellation and
  directional step controls. Hardware resolution determines visible smoothness.
- Optional process detection and ordered program launch in the current Windows
  session. A process being present is not proof that its UI or meeting is ready.
- No application network client, telemetry service, web backend or virtual camera.

The Linux Docker image compiles/tests and exports Windows artifacts. It does not
provide a Linux camera-control edition. MSIX preparation targets Windows x64;
Store publication, hardware compatibility and certification are separate steps.

## Boundaries

This is not a replacement for OBS, a conferencing client or vendor firmware tools.
The current UI does not offer reliable automatic screen sharing, preview or scene
composition. No universal UVC-camera support or hardware step smoothness is claimed.
See the compatibility matrix before choosing hardware.

## Contributions and identity

Forks, translations and new functionality are welcome under MIT. For inclusion in
the maintained project, preserve the primary functions, safe startup behaviour,
data compatibility and uncluttered design. Put optional complexity in settings.
These are upstream review criteria, not additional restrictions on MIT forks.
