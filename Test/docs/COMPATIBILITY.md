> **0.6.4 camera startup:** confirmation is optional and off for new profiles. Checked profiles ask at first camera use; no Zoom-process requirement or Settings activation button. See [current guide](en/DEFAULTS-AND-STREAMING.md). Earlier gate descriptions below are superseded.

# Compatibility and test evidence

## Reported reference setup

The project owner has used and tested the developing application with a Logitech
BRIO on Windows alongside Zoom. Feedback from that setup led to removing the
preview and delaying camera access until after the Zoom meeting starts.
This is an owner-reported development setup, not certification of every release.
The latest UI, startup gate and MSIX package still require the Windows acceptance
checks in WINDOWS-TEST.md. Exact passing app/driver/Zoom versions should be recorded
before describing a release as tested.

## Expected compatibility — not verified

| System | Expectation and limit | Evidence |
| --- | --- | --- |
| Windows 10/11 with .NET Framework 4.8 | Target desktop platform; MSIX targets x64 build 19041+ | Development target; release acceptance pending |
| Logitech BRIO + Zoom | Reference setup; initialise Zoom meeting before controller access | Owner-reported testing; latest changes pending |
| Other Logitech webcams, such as C920/C922/C930e | May expose some standard camera controls; some have no usable pan/tilt/zoom | Not tested |
| Other USB/UVC webcams and conference cameras | Expected only where Windows DirectShow driver exposes supported controls | Not tested |
| Microsoft Teams, Google Meet, Webex | May display the same camera while Studio changes its properties; sharing/exclusive access depends on driver and app | Not tested |
| OBS Studio and Windows Camera | May be used to observe the image; simultaneous driver access may fail | Not tested |
| Windows on ARM / 32-bit Windows | No packaged release commitment; supplied MSIX script is x64 only | Not tested |
| macOS, Linux, browser-only environments | No native Studio GUI support | Unsupported |
| IP/ONVIF, VISCA or vendor-only PTZ protocols | No protocol backend implemented | Unsupported unless a compatible Windows camera driver exposes controls |

A webcam providing video does not necessarily expose writable pan/tilt/zoom.
Digital pan/tilt may require zooming in first. Virtual cameras may have no device
control interface. Driver behaviour and camera sharing differ across applications.

**Important for non-Zoom workflows:** the current default startup gate explicitly
requires a running Zoom process and user confirmation. It is not a Teams/Meet/OBS
meeting detector. A non-Zoom user must intentionally turn this option off after
checking their own startup sequence; a generic gate is planned.

## Please contribute reports

We welcome both successful and unsuccessful combinations. Use the compatibility
report issue template. Include app version, Windows build, camera model/firmware,
driver, video application/version, USB connection, and features tested. State
whether this was portable, installer or MSIX and whether the startup gate was used.
Do not include meeting links, participant images, passwords or private profiles.

A useful report says what worked: enumeration, reading position, saving/recalling,
all directions, zoom limits, transition smoothness and simultaneous video usage.
Maintainers should add dated results with report links, rather than upgrading
“expected” to “tested” merely because a similar model worked.
