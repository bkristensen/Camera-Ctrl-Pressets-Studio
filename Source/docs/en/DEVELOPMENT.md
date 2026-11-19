[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Edit the interface and translations

## Open the right file

Open `Camera-Control-Pressets-Studio.sln`. In Solution Explorer, build the solution, then open `CameraControlStudio → Forms → MainForm.cs`. Press **Shift+F7** for the Windows Forms Designer. Do not select `MainForm.Designer.cs` as the design surface. Use F7 to return to code.

Install the Visual Studio .NET desktop workload from `.vsconfig`. The application is WinForms on .NET Framework 4.8. The WPF designer is not used.

## Change layout

Use the Properties panel to change `Dock`, `Margin`, `Padding`, row sizes and column sizes. Use the Document Outline to select nested panels. The main layout uses tables and a `SplitContainer`; avoid hard-coded screen coordinates.

Each window has a public, parameterless constructor and inherits from `AppForm`. Layout is in its `.Designer.cs` file. Event handling is in the corresponding `.cs` file. The designer structure has compiled, but opening and saving each Form in Visual Studio still needs verification on Windows.

The camera wheel and blue buttons are drawn in `Controls/BlueControls.cs`. The designer can move and size these controls, but their gradients, rings and symbols are C# drawing code. Preset symbols are in `Controls/PresetIcons.cs`. Image sliders are created at runtime according to driver capabilities.

## Set defaults

Edit `Shared/Models.cs`, in `SavedData.Defaults`, for new-profile settings. Edit `ProfileLayout` for the initial layout. Existing profiles retain their saved values. Create a new profile to test new defaults; do not delete user profiles to force them.

## Update a translation

Edit the matching value in `Shared/Locales/en.xml`, `da.xml` or `de.xml`. Keep the `key` unchanged. XML requires `&amp;` for an ampersand. All files must have the same keys. Stored camera and icon IDs must not be translated.

Run `scripts/check-translations.ps1`, then build. Test with `"Camera Control Studio.exe" --lang=de` or the language selector in settings. Restart after changing the saved language choice.

## Add a language

Copy `en.xml` to a new language-code file. Translate every value and register the code and name in `Shared/UiText.cs`. The translation checker and locale tests pick up the new file and registration automatically. Declare the app language in the MSIX manifest.

Automatic selection uses the Windows language prefix. Unsupported languages fall back to English. Test narrow windows and long labels; translation must not hide controls.
