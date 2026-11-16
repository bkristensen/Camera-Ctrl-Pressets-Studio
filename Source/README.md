# Cam Pressets Studio — WPF control panel demo

A scalable WPF (.NET 8) interface with a two-speed pan/tilt wheel, zoom buttons, profiles, and a preset list. The palette is based on the user's supplied blue reference and includes soft shadows.

## Run
- Windows 10/11 with .NET 8 SDK and Visual Studio WPF workload, or run `dotnet run --project CamPressetsControlDemo.csproj`.

## Features
- Inner ring: normal pan/tilt; outer ring: fast pan/tilt. Hold mouse and release to stop.
- Hold + or − to zoom.
- Pick profiles from the drop-down. The + beside it creates a profile.
- Click **Gem aktuel position** to name and save the current **simulated** position.
- Click a preset to restore its simulated position.
- Profiles and presets are stored in `%APPDATA%\CamPressetsControlDemo\profiles.json` and shared across runs of this demo.
- The UI elements are scalable WPF vectors. There is no camera preview and no speed slider.

## Important limitation
This is a **UI and state simulation only**: **no physical camera is controlled** and saved coordinates are not actual Logitech Brio values. Before using presets with a webcam, replace simulated position updates in `MainWindow.xaml.cs` with a camera adapter that queries real absolute coordinates, moves to them, reports failures and respects the supported ranges.

The project has not been compiled in this environment because the Windows .NET SDK is unavailable.

## Application branding
- Window title: **Cam Pressets Studio**.
- Windows executable and titlebar icon: `Assets/CamPressetsStudio.ico`.
- App header icon: `Assets/CamPressetsStudio.png`.
- Editable vector source: `Assets/camera-arrows.svg` (simple camera with four directional arrows).
