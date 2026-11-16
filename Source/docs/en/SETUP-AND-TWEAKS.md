# Setup, settings and tweaks

## Install and language

Extract the complete Windows ZIP and run Cam Pressets Studio.exe. The app needs
Windows and .NET Framework 4.8. Current beta downloads are unsigned. Do not disable
Windows security to install them. Docker only builds the app; it does not run its GUI.

Under Settings > Language choose Automatic, English, Dansk or Deutsch. Click
**Save language**, close Studio and reopen it. Save other pending settings before
closing. Automatic follows the Windows display language: Danish/German when
supported, otherwise English. A shortcut's --lang=en/da/de/auto option has priority.
Windows-owned dialog buttons and driver error text may retain the OS language.

## Profiles and startup programs

Create profiles for different cameras or workflows. A copied profile includes startup
settings and any edit code; a blank profile disables automatic program launch.
Language is global to this Windows user, not a profile setting.

In Edit programs / settings code, select .exe, .lnk or .appref-ms paths. Enter the
actual process name without .exe, visible in Task Manager > Details. Shortcuts may
launch a process with a different name. Reorder with Move up/down. The common
checkbox enables automatic startup; Save and start now explicitly tests the list.
Already-running programs are skipped. A 20-second process timeout stops the sequence.

The optional code locks startup-program editing, not presets or other preferences.
To remove it, unlock settings and set a blank new code. It is not encryption;
anyone able to edit the configuration file can bypass it.

## Camera access and controls

Keep the default camera gate enabled for the reference BRIO/Zoom workflow. It
blocks enumeration and connection until the required processes run and you confirm
meeting readiness. Selecting a camera or clicking Find cameras cannot bypass it.
Disabling the gate explicitly permits early camera access.

| Setting | Effect / starting point |
| --- | --- |
| Normal steps | Try 1 for precise framing; increase if movement is too slow |
| Fast steps | Use a larger value for navigating; hardware limits still apply |
| Zoom steps | Independent number of driver steps per action |
| Transition time | Longer durations soften preset transitions; 0 is immediate |
| Reverse arrows | Inverts direction commands, never rotates the video |
| Always on top | Keeps the controller above other windows |
| Image controls | Only properties exposed by the selected driver are available |

Click Save settings for profile preferences. Step counts are not motor speed.
Presets do not preserve HDR or field of view. Configure unsupported properties in
the vendor application and keep them consistent between saved positions.

## Troubleshooting and backup

No camera: check the startup gate, USB connection and privacy settings first.
Busy device: check competing camera-control apps while preserving required startup
order. Jerky movement: reduce steps or increase transition time; the driver may be
coarse. Reversed video: establish the correct image in Zoom before enabling Studio.

Unpackaged profiles are in %LOCALAPPDATA%\CamPressets\profiles.json; language is
in language.txt alongside it. Back up both. MSIX data redirection and uninstall
behaviour need packaged testing; do not assume the Store copy shares portable data.
