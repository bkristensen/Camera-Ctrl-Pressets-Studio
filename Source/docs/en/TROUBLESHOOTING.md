# Troubleshooting

*Written for Camera Control Studio 1.0.0.*

## Contents

- [1. Video apps and field of view](#1-video-apps-and-field-of-view)
  - [Limited field of view in Zoom Workplace](#limited-field-of-view-in-zoom-workplace)
- [2. Camera connection and compatibility](#2-camera-connection-and-compatibility)
  - [Camera missing or unavailable](#camera-missing-or-unavailable)
  - [Controls missing or different between apps](#controls-missing-or-different-between-apps)
- [3. Movement and saved presets](#3-movement-and-saved-presets)
  - [Movement stops, looks stepped or goes the wrong way](#movement-stops-looks-stepped-or-goes-the-wrong-way)
  - [Field of view or a saved preset looks wrong](#field-of-view-or-a-saved-preset-looks-wrong)

## 1. Video apps and field of view

### Limited field of view in Zoom Workplace

**Symptoms**\
The edges of the scene are missing. Zooming out and panning sideways stop too soon. Saved presets frame the scene differently. Zoom’s **Optimize for video sharing** option is unavailable. The same restriction appears in the original Logitech BRIO Control app.

**Cause**\
**Confirmed in the reported case.** Zoom Workplace had **Efficiency Mode** enabled. Turning it off and restarting Zoom restored the original view without changes to Camera Control Studio or its presets. The mechanism behind the narrower view is unknown.

**Solution**

1. Open Zoom Workplace and go to your profile picture → **Settings → Meeting & webinar → Advanced**.
2. Under **Performance**, turn off **Efficiency Mode**. If the option is locked, ask your Zoom administrator.
3. Quit Zoom Workplace completely and reopen it. Keep your existing presets while testing.

**Verification**\
Check that the edges of the original scene are visible again. Recall your existing presets and compare the live framing.

**Affected software and hardware**\
Windows, Zoom Workplace, Logitech BRIO, Logitech BRIO Control and Camera Control Studio 0.7.0-beta.8. The Zoom version, driver version and firmware version were not recorded.

**References**\
[Zoom Support](https://support.zoom.com/hc/en/article?id=zm_kb&sysparm_article=KB0080877) · [GitHub #5](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/5)

## 2. Camera connection and compatibility

### Camera missing or unavailable

**Symptoms**\
The camera list is empty, the saved camera is unavailable or the controls stay disconnected after sleep.

**Cause**\
**Possible causes.** The USB connection is loose, Windows blocks access, another app is using the camera, or Camera Control Studio is waiting for streaming confirmation.

**Solution**

1. Check the USB cable. If your setup uses streaming confirmation, start the stream before choosing **Yes**.
2. Check Windows camera privacy settings. Test the preview in Windows Camera and then close it.
3. Use a camera control to reconnect after sleep. If the saved camera is missing, open the source list from the centre button and choose the camera again. By default, double-click the centre button to open the camera list.

![Direction wheel with the camera source button in the centre](../images/screenshots/control%20wheel%20only.png)

**Verification**\
Make sure the footer shows the correct camera and a supported control changes the intended live picture.

**Affected software and hardware**\
Windows and built-in or USB cameras. Concurrent access depends on the driver.

**References**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [User guides](SETUP.md)

### Controls missing or different between apps

**Symptoms**\
The camera has a preview, but some controls are missing. The picture changes in one video app and looks unchanged in another.

**Cause**\
**Confirmed behaviour.** Camera Control Studio uses controls exposed by the Windows driver. **Possible causes in your setup.** A driver, firmware or another app may change which controls work or what the preview shows.

**Solution**

1. Select the same physical camera in both apps. Compare its preview in each app, one at a time.
2. Close other camera tools and reopen the video app you will use. Test one supported control.
3. If the problem persists, check Windows Update and the manufacturer’s support page for your exact model. Follow its instructions for any driver or firmware update.

**Verification**\
Check which control changes the live picture in the app you will use. Record the camera model, driver, firmware and app versions if you report a difference.

**Affected software and hardware**\
Windows, the camera’s driver and firmware, and any video or camera control apps in use.

**References**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K)

## 3. Movement and saved presets

### Movement stops, looks stepped or goes the wrong way

**Symptoms**\
Pan, tilt or zoom stops before you expect it to. A transition moves in steps, or the arrows move the shot the wrong way.

**Cause**\
**Confirmed behaviour.** The driver sets the range and step size. Camera Control Studio can reverse the direction of the arrows.

If the direction changes after you open Logitech Camera Settings or Logitech BRIO Control, another app may have changed how the camera responds. This has been reported with the video still upright in Zoom. The affected axis and cause have not been confirmed.

**Solution**

1. Use the square frame button above the zoom buttons to restore the widest available view and minimum zoom. Test the arrows and zoom again.
2. If the arrows move the wrong way, open **Settings → Movement**. Reverse only the affected axis.
3. For smaller manual movements, try holding **Ctrl** with an arrow key. A longer transition cannot make the driver’s steps smaller.
4. When you are off air, close the Logitech tool and test both axes in the same video app at the same zoom level. Open one Logitech tool at a time to check whether the change follows it.

**Verification**\
Check that the arrows move the shot in the expected direction. Compare the limits in the live picture.

**Affected software and hardware**\
Cameras whose Windows drivers expose pan, tilt or zoom. Exact limits vary by model and video mode.

**References**\
[GitHub #3](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/3) · [GitHub #4](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/4) · [User guides](DAILY-USE.md)

### Field of view or a saved preset looks wrong

**Symptoms**\
The picture is too tight, a preset no longer frames the expected scene, or field-of-view choices are unavailable.

**Cause**\
**Confirmed behaviour.** Presets belong to a camera and profile. Older presets without a saved viewing angle keep the current angle. The driver may expose no selectable angles.

**Solution**

1. Check the active profile and camera. Restore the full frame and check the live picture.
2. Check **Settings → Movement** for the startup field of view. If the driver offers angles, enable manual changes to compare them.
3. Recall the preset. If the whole view is still narrow only with Zoom running, follow the Zoom section above. Keep the preset while checking the cause.

**Verification**\
Compare the live shot with the expected framing. Reselect the preset after restoring the original view.

**Affected software and hardware**\
Camera Control Studio profiles and presets. Selectable viewing angles depend on the camera and driver.

**References**\
[Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K) · [User guides](SETUP.md)

---

Still need help? Review the logs before sharing them. Include the camera model, app versions, the steps you tried, and the result.

[Report a problem](ISSUES.md) · [User guides](../USER-GUIDES.md)
