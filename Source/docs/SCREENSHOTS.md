# Application screenshots

These images show Camera Control Studio 1.0.1 with the Standard profile, a demo camera and sample presets. Your camera driver decides which controls appear in Video. The table links to the English screens. The [user guides](USER-GUIDES.md) use screenshots in their own language.

| View | What it shows |
| --- | --- |
| [Main window](images/screenshots/1.0.1/en/main.png) | Direction wheel, camera selector, zoom, presets and status with Settings closed |
| [Main window with Settings open](images/screenshots/1.0.1/en/main-settings.png) | The settings pane alongside the camera controls and preset list |
| [Settings → Panning](images/screenshots/settings-tabs/en/settings.png) | Pan and tilt steps, direction reversal and camera button options |
| [Settings → Zoom](images/screenshots/settings-tabs/en/zoom.png) | Step size for the zoom buttons |
| [Settings → Application](images/screenshots/1.0.1/en/application.png) | Streaming confirmation, always-on-top behaviour and profile language |
| [Video before loading controls](images/screenshots/settings-tabs/en/video.png) | Where to load the controls available from the camera driver |
| [Video → Focus and exposure](images/screenshots/settings-tabs/en/image.png) | Focus, exposure and gain |
| [Video → Colour](images/screenshots/1.0.1/en/video-colour.png) | Hue, gamma, saturation and white balance |
| [Video → Light and detail](images/screenshots/1.0.1/en/video-light-detail.png) | Sharpness, backlight compensation, brightness and contrast |
| [Video → Field of view](images/screenshots/settings-tabs/en/field-of-view.png) | Startup angle and when the app may change the field of view |
| [Presets → Permanent presets](images/screenshots/settings-tabs/en/presets.png) | Permanent presets and editing actions |
| [Presets → Transitions](images/screenshots/settings-tabs/en/transitions.png) | Fixed duration or speed for moves between saved positions |
| [Profiles](images/screenshots/1.0.1/en/profiles.png) | Profile list and actions to create, copy or rename a profile |
| [Issues](images/screenshots/1.0.1/en/issues.png) | Diagnostic logs and GitHub reporting |
| [About](images/screenshots/1.0.1/en/about.png) | Version, developer credit and project links |

## Refresh the guide images

From the solution folder, run `./scripts/capture-docs.ps1` on Windows. It uses sample data and writes the PNGs to `docs/images/screenshots/<version>/<language>/`. It captures all the views in the table for English, Danish and German, and six views for each of the other nine languages. The script also captures the [annotated Danish quick guide](images/screenshots/1.0.1/da/quick-guide.png).

Use 100% display scaling and a working area of at least 1440 × 800 for the full windows. Review the captures before updating the README and guides. Keep earlier screenshot folders and files at their original paths so older release links still work.
