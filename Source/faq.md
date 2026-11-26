# FAQ

[Troubleshooting guide](docs/en/TROUBLESHOOTING.md)

**Why is the row yellow?** The camera is moving. The row turns green when the driver reports the saved coordinates and FOV, if saved. Check the shot in the live video. Manual movement clears the colour.

**Why can I still press Zoom out at minimum zoom?** Automatic FOV can switch to a wider view. Zoom continues on the next press. The reverse works at maximum zoom. [Set the field of view](docs/en/SETUP.md#set-the-field-of-view)

**Why does movement still look stepped?** The camera driver accepts a limited set of positions. Longer transition time cannot create finer hardware steps.

**Where are my positions?** Permanent positions belong to the selected camera and profile. Temporary positions last until the app closes. Permanent positions are managed under Settings → Presets.

**Why did the camera not connect?** It may be disconnected, busy, unsupported, or waiting for your streaming confirmation. Double-click the central camera button to select a source. This app cannot detect whether your stream has begun.

**What is Camera Control Studio designed for?** Use it alongside video meeting and streaming apps such as Zoom Meetings to adjust your camera and recall saved positions. Your meeting or streaming app displays and shares the video.

**Why might zoom work in Zoom Meetings but show no visible change in Windows Camera?** This has been observed with the same camera. Whether live zoom changes appear can depend on the camera driver and the app displaying the video. Test camera controls in the app you intend to use for your meeting or stream.

**Where are profiles stored?** `%LOCALAPPDATA%\CamPressets\profiles.json`. A `.bak` copy is written when the file is replaced. Close the app before copying or restoring these files. Keep the original backup when upgrading from 0.6.x: removed startup-program options are discarded when the updated profile is saved.

**Does language follow a profile?** Yes. Set it under **Settings → Application → Profile language**. Changing profiles restarts the program. Temporary presets end when the program restarts. Existing profiles without a language initially use your previous language preference.

**Will this work with another camera?** It may work if its Windows driver exposes the standard controls used by the app. Please report the model, driver version and controls that work. Do not assume that every USB camera supports pan or zoom.

**Why does Windows warn about the download?** The Windows downloads are unsigned. Get it from the project’s release page and compare the published hash if there is one. Keep Windows protection on. [Check the download and read the SmartScreen warning](docs/en/SETUP.md#install-and-verify-the-download)

**Where can I request a feature?** Open an issue and describe the problem, expected behaviour and one example. Use Discussions when you want to explore an idea first.

**What if the camera stops responding after sleep or hibernation?** Use a camera control to reconnect, or select the camera again using the centre button. If the problem repeats, use **Settings → Issues → Report on GitHub**. Review the exported app and system logs, drag the available files into the issue, and describe what happened before the error.
