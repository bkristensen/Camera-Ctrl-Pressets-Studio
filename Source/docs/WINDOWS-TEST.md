# Windows acceptance check

1. Back up `%LOCALAPPDATA%\CamPressets\profiles.json`, close old versions, and open
   Studio. Confirm Standard and existing presets appear. Test English and Danish.
2. Resize with settings closed/open at 100%, 150% and 200% display scaling. Confirm
   preset names, gear, zoom, settings scrolling and keyboard focus remain usable.
3. Select the camera. Test all directions on both wheel rings and zoom. Release
   the button, press Escape and switch to another window during a held movement;
   each must stop. Check reverse switches and hardware step settings.
4. Save, rename, recall and delete a test preset. Set a transition and watch in
   Zoom. Reopen Studio and verify camera, always-on-top and profile settings.
5. Configure known executable/shortcut paths for JW Library and Zoom. Check
   already-running skip, ordering, bad-path failure, code gate and code removal.
   Verify camera and preset controls remain usable with the settings code enabled.

Installer: test install/update/uninstall as a non-administrator in a disposable
Windows account. Profiles should remain. Signed releases additionally require
`signtool verify /pa /all /v` on the app and installer; no signing was performed
in this export. The optional portable Install.ps1 is also unverified on Windows.

Startup gate: close Zoom and launch Studio. No camera should connect or enumerate.
Find cameras, profile switching and preset actions must not bypass the gate. Start
programs and Zoom without joining a meeting: do not confirm readiness. Join a
meeting, check the image, click the enable-camera button and confirm. Then test
restart/profile changes, missing processes, Cancel, and saved checkbox states.
