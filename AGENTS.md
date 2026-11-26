# Windows Forms

- Define every fixed WinForms control and component in its form's `*.Designer.cs` file. Keep the control hierarchy, layout, captions, columns and tab order editable in the Visual Studio Designer.
- Keep event handling, localization, data binding and state changes in the corresponding code-behind. Initialize hardware, WebView2 and other runtime services after the designer guard. Populate data-driven items at runtime using designer-authored controls.
- Include a public parameterless constructor that calls `InitializeComponent()` for each form. Verify new forms and layout changes in design-time and runtime paths.

# Writing

- Use a direct Hemingway style for UI text and guides in every supported language. Use short sentences and clear labels.
- Answer likely questions where they arise. Link to a guide for details that would make the current text too long.
- Write image alt text in the language of the guide. Describe what the reader needs to find or understand.
- Do not use Caveman to write or edit project text and documentation.

# Troubleshooting documentation

- Evaluate every reported and resolved issue for the Troubleshooting Guide. Add or update entries when verified findings can help other users.
- Describe symptoms, causes, numbered steps, verification and the affected environment. Link to the relevant GitHub issue and official references when available.
- Distinguish confirmed facts from suspected causes. Distinguish defects in Camera Control Studio from limits or compatibility issues in external software, drivers, firmware or hardware.
- Keep all supported language versions synchronized. Update instructions and screenshots when the interface changes.
- Keep troubleshooting in the dedicated guides. Link to setup, daily use or the FAQ where they already explain a procedure.
- Include these documentation checks when completing an applicable bug fix or feature.

# Python scripts

- Keep reusable Python tools in `Source/scripts/python`. Use English filenames and paths relative to the repository.
- Keep one-off edit scripts and test output in a temporary folder outside the tracked source.
