# Validation for 0.7.0-beta.3

The Windows Release build uses locked dependencies. Translation parity and the automated tests passed. The tests cover motion, presets, profile language, persistence, logging and the camera-not-ready HRESULT reported after hibernation.

The extracted portable ZIP was tested in x86 and x64 Windows processes. Both opened and reopened the daily-use and setup guides, displayed their screenshots, and printed to PDF. The x64 PDF had the title, version, date and correct page count on all five pages. The **?** button was present during WinForms design-time initialization. The ARM64 loader was checked for the correct binary architecture.

The English Standard profile was captured with a demo camera. The 12 full-window screenshots cover both main-window states, each settings tab and every subtab. The local links in the guides and READMEs were checked against the packaged document paths.

## Brio field of view and Zoom

Use a physical Brio that offers 65°, 78° and 90°. Note the model, firmware, driver, app versions and which video mode Zoom uses.

1. Start Zoom and use a fixed scene with visible reference points near the edges. Confirm the widest framing at 90° and minimum zoom using Logitech Brio Control.
2. Start Camera Control Studio with **Do nothing** and confirm that the device retains its FOV. Restart with **Widest available**, then repeat with a specified FOV and with streaming confirmation enabled.
3. With all three apps running, change pan, tilt, zoom and FOV in Logitech Brio Control. Confirm the updated readings in Camera Control Studio with manual FOV disabled and enabled.
4. Change FOV in Camera Control Studio. Observe when Logitech Brio Control updates its own display. Save and recall presets for each FOV, including an older preset without FOV.
5. From a narrow, zoomed-in shot, choose **Full frame** and compare the image with the reference from step 1. Repeat during a preset transition and with Logitech Brio Control already open.
6. Close Camera Control Studio and check whether Logitech Brio Control can still change the camera. Repeat in the reverse application launch order. Attach the diagnostic log and note any discrepancy between the live image and the reported values.
7. At 65° and minimum zoom, press Zoom out once. Check that FOV changes to 78° and the next step zooms out. Repeat at 78°. Check that Zoom out stops only at 90° and minimum zoom. Test the reverse path for Zoom in.
8. At a horizontal pan edge, press the same arrow again. Check whether a wider FOV and a zoom adjustment allow motion without losing the shot. If no view helps, check that the original framing returns. Turn the feature off and confirm that the FOV stays put.

Inno Setup 7 compiled the installer with the guides for all 12 languages and the x86, x64 and ARM64 loaders. Both packages are unsigned, with SHA-256 hashes supplied. A clean-account installation and uninstall, a physical camera hibernation cycle, and an ARM64 runtime test are pending. See the [release checklist](RELEASE-CHECKLIST.md) and [build and release guide](en/BUILD-AND-RELEASE.md).
