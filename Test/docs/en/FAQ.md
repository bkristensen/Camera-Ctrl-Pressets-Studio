# Frequently asked questions

## Is Cam Pressets Studio free and open source?
Yes. The project uses the MIT license. You may use, modify, fork and redistribute
it, including commercially, with the required license notice. See LICENSE.

## Which camera has been tested?
The owner reports development use with Logitech BRIO and Zoom. This does not certify
the latest release. Other combinations are expected only where compatible driver
controls exist; see ../COMPATIBILITY.md and send a compatibility report.

## Does it work without Zoom?
The camera backend is not a Zoom API. However, the current default startup gate
requires Zoom plus manual meeting confirmation. For another video application,
explicitly disable that gate after assessing the startup order. Generic readiness
rules are planned, not implemented.

## Why does the app not connect immediately?
The default gate prevents camera access before required applications and the Zoom
meeting are ready. Start them first, then confirm in Settings. Detecting a running
Zoom process cannot prove a meeting is active.

## Can it preview or record my camera?
No. View video in your conferencing, recording or streaming application. Studio
controls properties and saves coordinates; it does not capture video or audio.

## Can presets share a screen or combine camera and slides?
No. Current presets contain camera PTZ only. The earlier experimental Zoom sharing
button is not exposed. Use your meeting or production software for screen sharing.

## Why is movement not perfectly smooth?
Interpolation is limited by the driver's discrete steps and camera processing.
A longer transition helps only when the hardware supports enough intermediate values.

## Do reverse switches rotate the picture?
No. They reverse arrow commands for a camera mounted in a different orientation.

## Where are settings stored?
The unpackaged app uses %LOCALAPPDATA%\CamPressets\profiles.json and a .bak backup.
Store/MSIX data isolation and migration still need validation; do not assume the
portable and packaged editions share the same effective file.

## Is the settings code a security feature?
It prevents accidental changes to startup-program settings. It does not encrypt
profiles or prevent somebody with file access from changing them.

## Why does Windows warn about the download?
Current beta binaries are unsigned. A normal installer or ZIP does not establish
publisher trust. Never disable SmartScreen globally. Store MSIX distribution and
approved signing are separate release steps; see DISTRIBUTION.md.

## Does Docker run the app?
No. It builds/tests and exports Windows artifacts. The camera GUI runs on Windows.

## Is there a Store version or SignPath approval already?
No. MSIX preparation and checklists are supplied. Publisher registration, packaged
testing, certification and any SignPath application remain pending.

## How do I request a feature or report a bug?
Use GitHub Issues with the matching template after the repository is published.
Search existing issues first. Discussion is useful for open-ended ideas. See
../../CONTRIBUTING.md and ../COMMUNITY.md.

## Can I change the design in my own fork?
MIT allows modifications. For changes to be merged upstream, preserve the compact
blue design and core camera-control workflow. This review policy does not restrict
what the license permits in independently maintained forks.

## How do I change the interface language?
Choose Automatic, English, Dansk or Deutsch under Settings > Language. Click Save
language and restart. A --lang=en/da/de/auto option overrides the saved preference.
The language is per Windows user, not per camera profile. Windows-owned dialogs
and vendor error messages may remain in the OS language.
