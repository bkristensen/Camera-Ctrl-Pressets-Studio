# Cam Pressets Studio — WPF design prototype

Scalable, blue WPF (.NET 8) three-column control panel with two-ring pan/tilt wheel, zoom buttons, profiles, presets, settings, and shadows.

## Run
Open `CamPressetsControlDemo.csproj` using Visual Studio 2022 with the .NET desktop development workload (Windows 10/11, .NET 8 SDK). Build and run.

## What's implemented
- Inner wheel ring = normal movement, outer ring = fast. Mouse hold to move, release to stop.
- Zoom +/- controls; step speed sliders for normal, fast and zoom speeds (1–50). Speed values affect simulated movement.
- Profile dropdown and create profile; add, rename, select presets; delete the most recently saved preset.
- Default startup-program selections: **JW Library** and **Zoom**; optional additional executable paths.
- A **single shared** `Start programmer ved opstart` checkbox below the program list.
- Camera selection field intentionally shows **Demo (intet kamera tilsluttet)** instead of claiming a real webcam connection.
- `Altid øverst` toggles WPF window Topmost immediately.
- Settings stored in `%APPDATA%\CamPressetsControlDemo\settings.json`; profiles stored in the same folder as `profiles.json`.

## Limitations
**Prototype only.** A real Logitech Brio PTZ driver is **not** connected. Camera positions, preset recalls, and movement are simulated. The startup-program preferences are saved but executables are **not automatically launched**; that behavior must be wired to the final startup policy. JW Library and Zoom are included as named choices only, not assumed installed at particular paths.

The project was not compiled here because the Windows WPF SDK is unavailable in the environment.
