# Project description

Camera Control Studio saves and recalls camera shots on Windows. Use it in a studio, a classroom or a live meeting. Your capture app handles the video.

## Core behaviour

Use the inner ring for normal moves and the outer ring for fast moves. Each ring has diagonal arrows. Click the centre button to choose a camera, or drag it to steer. The zoom buttons move by the set step. The slider sets zoom within the current FOV. The app opens no video preview. [Set movement and FOV](en/SETUP.md#set-the-field-of-view)

Permanent positions store a name, a stable symbol ID, and the pan, tilt, zoom and FOV the driver reports. Combined symbols show subjects from left to right. Permanent positions appear first in bold. Temporary positions have no icon and last until you close the app. Use Play or double-click to recall either type.

A smooth interpolation requests intermediate coordinates. A row is yellow during the transition. It is green only when readback matches the target. A manual action, cancellation or failed transition clears that state. The driver determines actual motion granularity and latency.

Profiles contain camera selection, permanent positions, image values, manual movement rates, preset transition duration or speed, direction reversal, camera confirmation, window layout and language. Changing profiles restarts the app with the selected profile’s language. Optional camera confirmation blocks enumeration and connection until the user confirms streaming has started.

## Architecture

`CameraControlStudio` is a .NET Framework 4.8 WinForms application. `Forms/*.Designer.cs` hold layout. `Forms/*.cs` hold event handling. `AppForm` is the common base. `Controls` contains vector rendering. `Camera.cs` uses Windows DirectShow camera and video-proc interfaces without a capture graph.

`Shared` holds models, persistence, translation, movement arithmetic, session positions and state logic. `Tests` is a .NET 8 console regression suite. Profiles use atomic file replacement, backups and revision checks. Existing 0.6 profile storage is retained; obsolete program-start settings are ignored and discarded on save.

## Scope and compatibility

The owner has reported that the earlier application worked on their laptop and camera setup. This 0.7 rewrite has compiled and passed logic tests but has not been exercised against physical hardware in this build environment. No additional camera or capture software is claimed tested.

Other Windows cameras may work when their drivers implement the needed standard controls. Concurrent access with capture, meeting or streaming software is driver-dependent. Virtual cameras may expose no physical controls. This is not a generic network-PTZ controller. Linux and macOS can be build hosts via the Docker workflow, but cannot run this Windows UI natively.

Please report successful combinations, including Windows version, camera model, driver version, capture application and which controls worked. Also report limitations so the compatibility list can be evidence-based.

## Possible future work

Windows UI regression automation; better accessibility and keyboard discovery; optional import/export of individual profiles; verified hardware matrix; signed releases after approval; additional camera backends if they preserve the simple core workflow. These are ideas, not delivered features.
