# Windows Forms

- Define every fixed WinForms control and component in its form's `*.Designer.cs` file. Keep the control hierarchy, layout, captions, columns and tab order editable in the Visual Studio Designer.
- Keep event handling, localization, data binding and state changes in the corresponding code-behind. Initialize hardware, WebView2 and other runtime services after the designer guard. Populate data-driven items at runtime using designer-authored controls.
- Include a public parameterless constructor that calls `InitializeComponent()` for each form. Verify new forms and layout changes in design-time and runtime paths.

# Writing

- Use a direct Hemingway style for UI text and guides in every supported language. Use short sentences and clear labels.
- Answer likely questions where they arise. Link to a guide for details that would make the current text too long.
- Write image alt text in the language of the guide. Describe what the reader needs to find or understand.
- Do not use Caveman to write or edit project text and documentation.
