# Windows Forms

- Define every fixed WinForms control and component in its form's `*.Designer.cs` file. Keep the control hierarchy, layout, captions, columns and tab order editable in the Visual Studio Designer.
- Keep event handling, localization, data binding and state changes in the corresponding code-behind. Initialize hardware, WebView2 and other runtime services after the designer guard. Populate data-driven items at runtime using designer-authored controls.
- Include a public parameterless constructor that calls `InitializeComponent()` for each form. Verify new forms and layout changes in design-time and runtime paths.
