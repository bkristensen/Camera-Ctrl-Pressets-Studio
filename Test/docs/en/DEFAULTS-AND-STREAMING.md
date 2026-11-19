# Defaults, streaming and camera sources (0.6.4)

## Set your normal settings
Open the gear, choose a profile, adjust values and press **Save settings**. These values are loaded with that profile. To create another profile with the same values, use **New** and keep **Copy the current saved configuration** selected. A blank profile uses factory defaults instead. Changing source-code defaults does not overwrite saved profiles.

## Change factory defaults in code
Open `SharedProfiles/Models.cs`, class `SavedData`, method `InitializeDefaults`. The constructor and deserializer both use this method. Set `Seconds`, `PanSteps`, `ZoomSteps`, `FinePanSteps`, `FineZoomSteps`, `FastPanSteps` and `WaitForStartup` there. Currently: 2 seconds, normal 5, fast 32, zoom 5, fine 1, confirmation false. Boolean options otherwise default to false. Build and test with a new blank profile. Do not delete your existing profile file just to test defaults.

## Optional streaming confirmation
The checkbox **Ask whether streaming has started before using the camera** is off for new profiles. Old profiles keep an explicitly saved value, including true from earlier versions. The serialized key remains `WaitForStartup` for compatibility; its meaning is now streaming confirmation, not process checking.

When checked, startup does not enumerate or connect to cameras. The first pan, zoom, preset recall, save-position, camera search, image-controls load or source selection asks whether streaming has started. **No** cancels the action without camera access. A later action asks again. **Yes** permits camera access for the session; the original action continues. Restarting, changing profiles or changing the checkbox resets permission. Editing the startup-program configuration also resets permission. There is no Zoom-process requirement. Optional startup programs are still launched separately in their configured order.

When unchecked, the app may connect at startup without asking. If disconnected, a camera action attempts to reconnect to the last saved camera, otherwise an available camera. It does not require a settings visit. If no camera is available or a driver fails, an error is reported. A preset is not applied to a different automatically selected camera.

## Camera source and image
Click the camera symbol in the center of the wheel (or focus the wheel and press Space/Enter) to select a camera source. Select and confirm to save that choice in the current profile. This switches the device controlled by this app; it does not change the camera selected in streaming software and does not select screen sharing.

Under the gear > Image, use **Load image controls** if disconnected. Controls include brightness, contrast, saturation, hue, sharpness, gamma, gain, backlight compensation, focus, white balance and exposure, where the driver exposes them. Automatic focus/white balance/exposure is shown only when supported. Unsupported settings are labeled unavailable. **Reset image to driver defaults** changes supported image controls, not pan/tilt/zoom. HDR, field of view and anti-flicker still need vendor support; they are not added by this release. Image values are applied to the camera immediately and are not stored in position presets.

## Windows acceptance checks
1. New blank profile: confirmation unchecked; no prompt on startup or movement.
2. Check and save, restart: no camera enumeration until a camera action. Choose No: no connection or movement. Try again and choose Yes: action completes, later actions do not ask.
3. Restart or switch profile: checked profile asks again. Verify without Zoom running.
4. Use center source picker, save and restart; verify last source. Remove the camera and verify safe failure/fallback without applying another camera's preset.
5. Verify supported image controls, auto modes, image reset, all three languages and small windows.

Native Windows/hardware testing remains required. Core and persistence tests run without hardware.
