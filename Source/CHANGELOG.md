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
