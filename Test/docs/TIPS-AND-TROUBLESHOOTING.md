> **0.6.4 camera startup:** confirmation is optional and off for new profiles. Checked profiles ask at first camera use; no Zoom-process requirement or Settings activation button. See [current guide](en/DEFAULTS-AND-STREAMING.md). Earlier gate descriptions below are superseded.

# Tips and troubleshooting

## Precise framing

Use a one-step normal setting for fine work and a larger fast-ring value for
navigation. Keep zoom, field of view and output resolution consistent when judging
saved framing. Large requested movement values cannot overcome driver limits.
Try a longer preset transition, but expect visible jumps if hardware steps are coarse.

## No camera or empty controls

Check the startup gate first. Until approved, Studio intentionally does not even
enumerate cameras. After the meeting is ready, approve, select the camera, and check
Windows camera privacy permissions. Unsupported controls depend on the driver.
If the device is busy, test without a competing vendor control app; preserve the
required order with the conferencing/video app. Do not disable Windows security.

## Unexpected orientation

The reference setup reported inverted video after early camera access. Keep the
startup gate checked, initialise the Zoom meeting first, and verify the image
before approving. Reverse arrows only changes movement direction, not image rotation.

## Startup program not detected

Use the actual executable process name without .exe, visible under Task Manager's
Details tab. Shortcuts and Store applications can launch a differently named
process. The configured path must be valid on this PC. A 20-second timeout stops
the rest of the sequence; it does not prove the program could never start later.

## Profiles and settings

Save settings before switching profiles. A stale-save error means the on-disk
revision changed; refresh and reapply edits. Do not overwrite a damaged file
without a backup. The code gate is an accidental-edit lock, not file encryption.

## Display and keyboard

Escape stops movement. Focus the wheel and use arrow keys; Shift selects fast
steps. Enter recalls the selected preset; F2 renames it. At high display scaling,
resize the window and use the settings scrollbar. Report clipped controls with
Windows scaling percentage and a screenshot without private meeting content.

## MSIX-specific problems

Check manifest identity, installed package version, architecture and signing trust.
An unsigned Store submission package is not an end-user sideload installer. Test
camera access, external program launch and profile migration in the packaged app;
portable success alone is insufficient.

## Uninstall

Close Studio first. For an Inno installer or Store edition, use Windows Settings >
Apps > Installed apps > Uninstall. For a portable copy, delete the extracted app
folder. If Install.ps1 was used, remove its folder under
%LOCALAPPDATA%\Programs\Cam Pressets Studio and its Cam Pressets Studio shortcut
from your Start-menu Programs folder. Unpackaged profiles are retained separately;
back them up before deliberately deleting %LOCALAPPDATA%\CamPressets. Do not assume
Store/MSIX uninstall retains data: verify that behaviour and back up beforehand.
