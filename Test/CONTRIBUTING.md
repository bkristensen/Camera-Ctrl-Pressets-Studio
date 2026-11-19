# Contributing

Forks, fixes, documentation, translations and new functionality are welcome.
Use an issue or discussion for major changes before opening a pull request.
English is the primary project language; Danish translations and reports are welcome.

## Upstream design and behaviour

Contributions to this maintained project should preserve:
1. Reliable camera-specific preset save/recall from real driver coordinates.
2. Precise normal/fast directional control, zoom and cancellable eased transitions.
3. A compact blue main view with optional complexity inside settings.
4. The default camera-startup gate and explicit readiness confirmation.
5. Backward-compatible profiles, safe writes, local operation and clear errors.

Do not silently add preview capture, telemetry, automatic screen sharing or new
network dependencies. Discuss those changes first. Include keyboard, resize and
DPI implications in UI changes. These are merge criteria, not extra restrictions
on MIT-licensed forks. Independent forks may change functionality and design.
Please distinguish modified distributions and do not imply maintainer endorsement.

## Pull request checklist

- [ ] Explain the user problem and before/after behaviour.
- [ ] Keep scope focused; include migration for saved-data changes.
- [ ] Run scripts/build.ps1 and relevant regression tests.
- [ ] Describe actual Windows/hardware tests and anything not tested.
- [ ] Add English/Danish/German UI text with UiText.T; update docs and changelog as needed.
- [ ] Review dependencies, license notices and privacy impact.
- [ ] Confirm no keys, certificates, credentials or personal profiles are included.

Contributions are submitted under the project's MIT license. Only contribute code
and assets you have permission to license. No separate CLA is currently required.
Source comments and issue text should be clear; do not translate device IDs or
user-defined preset names. A simulated test is not a hardware compatibility result.
