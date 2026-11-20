# Application screenshots

The user guides share these screenshots of the English interface. They show the Standard profile with a demo camera and sample presets. The Video examples illustrate controls that a camera driver may expose.

| View | What it shows |
| --- | --- |
| [Main window](images/screenshots/main.png) | Direction wheel, camera selector, zoom, presets and status with Settings closed |
| [Main window with Settings open](images/screenshots/main-settings.png) | The settings pane alongside the camera controls and preset list |
| [Settings → Movement](images/screenshots/settings.png) | Pan and zoom steps, transition time and direction reversal |
| [Settings → Application](images/screenshots/application.png) | Streaming confirmation, always-on-top behaviour and profile language |
| [Video before loading controls](images/screenshots/video.png) | Where to load the controls available from the camera driver |
| [Video → Focus and exposure](images/screenshots/image.png) | Focus, exposure and gain |
| [Video → Colour](images/screenshots/video-colour.png) | Hue, gamma, saturation and white balance |
| [Video → Light and detail](images/screenshots/video-light-detail.png) | Sharpness, backlight compensation, brightness and contrast |
| [Presets](images/screenshots/presets.png) | Permanent presets and editing actions |
| [Profiles](images/screenshots/profiles.png) | Profile list and actions to create, copy or rename a profile |
| [Issues](images/screenshots/issues.png) | Diagnostic logs and GitHub reporting |
| [About](images/screenshots/about.png) | Version, developer credit and project links |

## Refresh the guide images

Open the Windows app with the English Standard profile and a selected camera or a demo camera. Capture the full window. Open the settings pane and capture each tab and subtab in the table. Save the PNGs under the existing names in `docs/images/screenshots` to preserve links from the guides and README. Check that the window fits the screen and that the images contain only sample data.

The separate `./scripts/capture-docs.ps1` script builds the app and generates nine **annotated** examples in `docs/images` using the built-in demo mode. Use 100% display scaling and at least 1280 × 720 available desktop space for that script. Demo mode leaves real cameras and saved profiles untouched.
