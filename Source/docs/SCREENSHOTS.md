# Application screenshots

The user guides share these screenshots of the English interface. The displayed camera, presets and profiles are examples.

| View | What it shows |
| --- | --- |
| [Main window](images/screenshots/main.png) | Direction wheel, camera selector, zoom, presets and status with settings closed |
| [Settings](images/screenshots/settings.png) | Movement steps, transition time, direction reversal, streaming confirmation and language |
| [Image](images/screenshots/image.png) | Focus, exposure and other driver-provided controls |
| [Presets](images/screenshots/presets.png) | Permanent preset list and editing actions |
| [Profiles](images/screenshots/profiles.png) | Profiles and actions to create, copy or rename them |
| [About](images/screenshots/about.png) | Version, developer credit and links to the project |

## Refresh the guide images

On Windows, capture the full application window with settings closed for `main.png`, then open each tab for the remaining screenshots. Keep the settings pane wide enough for its labels and review the screenshots for personal details before committing them. Save the PNGs in `docs/images/screenshots` under the same filenames so the links in the guides and GitHub README continue to work.

For separate captures with synthetic sample data, run `./scripts/capture-docs.ps1` from the source folder. Use 100% display scaling and a desktop of at least 1280 × 720 pixels. Keep the app visible during capture. The script writes nine language-specific PNGs directly to `docs/images`. Capture mode uses the actual Windows forms with sample positions and leaves your camera and saved profiles untouched.
