# Application screenshots

*Camera Control Studio 1.1.1*

The screenshots show the Standard profile, a demo camera and sample presets. The camera driver determines which video controls are available. See the [user guides](USER-GUIDES.md) for setup and daily use.

| View | What it shows |
| --- | --- |
| [Main window](images/screenshots/1.1.1/en/main.png) | Direction wheel, camera selector, zoom, presets and status with Settings closed |
| [Main window with Settings open](images/screenshots/1.1.1/en/main-settings.png) | The separate Settings window alongside the main window |
| [Settings → Panning](images/screenshots/1.1.1/en/settings.png) | Pan and tilt steps, direction reversal and camera button options |
| [Settings → Zoom](images/screenshots/1.1.1/en/zoom.png) | Step size for the zoom buttons |
| [Settings → Application](images/screenshots/1.1.1/en/application.png) | Streaming confirmation, always-on-top behaviour and profile language |
| [Video before loading controls](images/screenshots/1.1.1/en/video.png) | Where to load the controls available from the camera driver |
| [Video → Focus and exposure](images/screenshots/1.1.1/en/image.png) | Focus, exposure and gain |
| [Video → Colour](images/screenshots/1.1.1/en/video-colour.png) | Hue, gamma, saturation and white balance |
| [Video → Light and detail](images/screenshots/1.1.1/en/video-light-detail.png) | Sharpness, backlight compensation, brightness and contrast |
| [Video → Field of view](images/screenshots/1.1.1/en/field-of-view.png) | Startup angle and when the app may change the field of view |
| [Presets → Permanent presets](images/screenshots/1.1.1/en/presets.png) | Permanent presets and editing actions |
| [Presets → Transitions](images/screenshots/1.1.1/en/transitions.png) | Fixed duration or speed for moves between saved positions |
| [Profiles](images/screenshots/1.1.1/en/profiles.png) | Profile list and actions to create, copy or rename a profile |
| [Contact & About → Contact](images/screenshots/1.1.1/en/issues.png) | GitHub issue buttons, support ZIP and diagnostic logs |
| [Contact & About → About](images/screenshots/1.1.1/en/about.png) | Version, developer credit and project links |

## Other windows

- [Choose camera source](images/screenshots/1.1.1/en/camera-source.png)
- [Choose profile](images/screenshots/1.1.1/en/profile-picker.png)
- [Edit name and symbol](images/screenshots/1.1.1/en/preset-editor.png)
- [New profile](images/screenshots/1.1.1/en/text-entry.png)
- [Log settings](images/screenshots/1.1.1/en/log-settings.png)
- [The action failed. Open the log for details.](images/screenshots/1.1.1/en/error-dialog.png)
- [Open guides](images/screenshots/1.1.1/en/guide-viewer.png)

## Refresh the guide images

From the solution folder, run `./scripts/capture-docs.ps1` on Windows. It uses sample data and writes the PNGs to `docs/images/screenshots/<version>/<language>/`. The script captures every view on this page and the annotated quick guide in all 12 languages.

Use 100% display scaling and a working area of at least 1440 × 800 for the full windows. Review the captures before updating the README and guides. Keep earlier screenshot folders and files at their original paths so older release links still work.
