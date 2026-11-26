[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Edit the interface and translations

*Written for Camera Control Studio 1.0.0.*

## Open the right file

Open `Camera-Control-Pressets-Studio.sln`. In Solution Explorer, build the solution, then open `CameraControlStudio → Forms → MainForm.cs`. Press **Shift+F7** for the Windows Forms Designer. Do not select `MainForm.Designer.cs` as the design surface. Use F7 to return to code.

Install the Visual Studio .NET desktop workload from `.vsconfig`. The application is WinForms on .NET Framework 4.8. The WPF designer is not used.

## Change layout

Use the Properties panel to change `Dock`, `Margin`, `Padding`, row sizes and column sizes. Use the Document Outline to select nested panels. The main layout uses tables and a `SplitContainer`; avoid hard-coded screen coordinates.

Each window inherits from `AppForm` and has a public constructor without parameters. Put the layout in `.Designer.cs` and events in the matching `.cs` file. Set the form’s `Icon` property in the Designer. Check the layout in Visual Studio and in the running app.

The camera wheel and blue buttons are drawn in `Controls/BlueControls.cs`. The vertical divider and zoom controls are in `Controls/MainViewControls.cs`. The designer can move and size these controls. Preset icon images and their neutral titles are in `Assets/PressetIcons.resx`, and `Controls/PresetIcons.cs` loads them. The Video tab contains designer-authored sliders. The running app shows the sliders supported by the camera driver.

## Set defaults

Edit `Shared/Models.cs`, in `SavedData.Defaults`, for new-profile settings. Edit `ProfileLayout` for the initial layout. Existing profiles retain their saved values. Create a new profile to test new defaults; do not delete user profiles to force them.

## Update a translation

Edit the matching value in the relevant language file under `Shared/Locales`. Keep the `key` unchanged. XML requires `&amp;` for an ampersand. All files must have the same keys. Keep stored camera and icon IDs unchanged.

Run `scripts/check-translations.ps1`, then build. Test with `"Camera Control Studio.exe" --lang=de` or the language selector in settings. Restart after changing the saved language choice.

## Add a language

Copy `en.xml` to a new language-code file. Translate every value and register the code and name in `Shared/UiText.cs`. The translation checker and locale tests pick up the new file and registration automatically. Declare the app language in the MSIX manifest.

Automatic selection uses the Windows language prefix. Unsupported languages fall back to English. Test narrow windows and long labels; translation must not hide controls.
