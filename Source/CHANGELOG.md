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
