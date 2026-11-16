# Roadmap — proposals, not promises

## Release readiness
- Validate the new UI and startup gate on Windows with the reference BRIO setup.
- Test scaling, keyboard access, packaged data migration and camera sharing.
- Establish a public repository, support channel and reproducible release records.
- Submit a tested MSIX to a Store test audience before wider distribution.

## Potential improvements
- General startup-readiness profiles independent of Zoom, keeping safe defaults.
- Profile export/import with validation, backups and camera remapping.
- More translations and accessibility improvements without crowding the main view.
- Optional hotkeys, repeat-rate settings and richer device diagnostics.
- Verified support for additional camera models and applications.

## Research only
Reliable meeting detection and screen-sharing automation need documented APIs or
verified interfaces. ONVIF/VISCA camera backends, network control and virtual-camera
features would require separate scope, privacy and design review. No implementation
or release date is promised. Preview is intentionally absent from the current plan
because it conflicted with the reference setup.

New features must not bypass the startup gate, silently replace presets, impose
network access on basic operation, or make the main control view harder to use.
