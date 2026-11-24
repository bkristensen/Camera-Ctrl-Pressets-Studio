# 0.7.0-beta.7

- Settings opens on the side with room. The tabs stay on one row and scroll when space is tight.
- Disabled settings stay legible and repaint cleanly.
- GuideViewer can open the issues guide and GitHub together. Issue-list links show open and closed reports.
- Updated the guide screenshots in all 12 languages with seven translated sample positions.
- Updated the zoom panel layout.

The Windows installer and portable ZIP are unsigned. Use SHA256SUMS.txt to verify the downloads.

# 0.7.0-beta.6

- The footer shows the installed version without its build identifier. If an update is available, hover over the link to see the new version and its publication date, when GitHub provides one.
- Updated the program screenshots in all 12 user guide languages and in the Danish handouts. New screenshots use versioned paths so links from older releases still work.

The Windows installer and portable ZIP are unsigned.

# 0.7.0-beta.5

- The pan wheel has two speed rings with diagonal movement. Click the camera button to change source or drag it to pan.
- The app offers the field-of-view (FOV) angles reported by a compatible camera driver. On supported cameras, it can switch FOV at the limits of zoom and horizontal pan. Turn this off under **Settings → Movement**.
- Presets save the camera’s reported FOV along with pan, tilt and zoom. Use the square **Full Frame** button to restore the widest available view and minimum zoom.
- Settings start closed and open on the first tab after launch. **Movement** and **Application** scroll when their controls do not fit.
- Look under **About** for the short version. Hover over it to see the full build identifier. The app also checks GitHub for newer releases.
- Added window icons and updated the setup and daily-use guides.

The Windows installer and portable ZIP are unsigned.

# 0.7.0-beta.4

- The camera zooms as you drag the zoom slider.

# 0.7.0-beta.3

- Added an in-app Markdown guide viewer for the active profile’s language. The **?** button opens daily use or setup according to whether Settings is open. Printing adds the guide title, app version, date, and current and total page numbers.
- Fixed the guide viewer’s WebView2 loading error on 64-bit Windows. The Windows packages include the x86, x64, and ARM64 loaders, and the viewer selects the one matching the running process. The **?** button is also visible in the WinForms Designer.
- Grouped **Settings** into **Movement** and **Application**, and **Video** into **Focus and exposure**, **Colour**, and **Light and detail**. Added an **Issues** tab for local diagnostics and links to releases and online guides under **About**.
- Language is saved per profile. Switching profiles restarts the app with the selected language. Temporary presets end when it restarts.
- Recover when the camera driver reports that the device is no longer ready, including after hibernation. Use a camera control to reconnect.
- Added local diagnostics with configurable folder, size, and retention. Reporting on GitHub prepares the error log and a separate system summary for review and manual attachment.
- Updated the daily-use and setup guides in all 12 supported languages. Added English screenshots of the main window and every settings tab and subtab with a demo camera.

The Windows installer and portable ZIP are unsigned. Guide viewing and printing use Microsoft Edge WebView2 Runtime. See the [validation notes](docs/VALIDATION.md) and the [user guides](docs/USER-GUIDES.md).

# 0.7.0-beta.2

- Compact main window with a vertical zoom slider, localized tooltips, footer controls and a profile-specific divider that repaints during resize.
- Normal zoom through the two magnifier buttons and larger steps with Shift. Pan, tilt and zoom directions are disabled at their limits.
- Preset icons load from resources, and older saved icon IDs continue to work. Selecting a permanent preset in the main list selects it in settings.
- The settings pane starts closed. The main window is titled Camera Control Studio and has a 670 × 450 minimum.
- Added nine app languages alongside English, Danish and German. The installer and ZIP include user guides for all 12 languages.
- Fixed the startup error after saving zoom controls in the Visual Studio Designer. Added project provenance and installer support links.

See the [validation notes](docs/VALIDATION.md) and the [build and release guide](docs/en/BUILD-AND-RELEASE.md).

# 0.7.0-beta.1

- Generic CameraControlStudio namespaces, source filenames and solution. Removed external-program startup integration and its settings.
- Designer-separated WinForms, compact layout, right-hand settings and a draggable controller/preset divider.
- Per-profile window/layout persistence and optional carry-over during profile selection.
- Separate normal/fast zoom buttons and steps. Zoom glyphs are drawn around the button centre.
- Yellow preset rows during transitions; green after matching driver readback. Manual movement clears active state without removing symbols.
- Fifteen unique combined symbols cover all requested left/right orders; the symmetric café/lectern/café arrangement appears once. Forty-five symbols in total.
- English, Danish and German setup, daily-use and development guides. Labelled layout illustrations and an isolated Windows screenshot capture mode.
- Generic build, Docker, installer and MSIX paths. No signature is included.

Before upgrading, close the previous app and copy `%LOCALAPPDATA%\CamPressets` as a backup. Existing permanent positions and confirmation choices are retained. Removed program-start fields are discarded on save. Keep the backup for rollback to an older version. Temporary positions do not survive app closure.

This build compiles and passes logic tests. Windows UI, hardware, Designer round trips and packaging execution remain unverified. See `docs/VALIDATION.md`.
