> 0.6.4: [Updated defaults and camera startup / Opdateret kamerastart / Aktualisierter Kamerastart](DEFAULTS-AND-STREAMING.md). This supersedes earlier Zoom-specific startup instructions.

# Editing the UI and graphics

## Start here
1. Open `CamPressetsStudio.sln` in Visual Studio on Windows. Set **BrioPresets** as the startup project.
2. Open `BrioPresets/Program.cs` and find `MainForm()`. This constructor builds the main window.
3. Change one layout value, then press **F5**. Stop debugging before the next edit.
4. Test a small and maximized window with settings both open and closed.

## Which file does what?
| File | Edit here |
| --- | --- |
| `BrioPresets/Program.cs` | Main window layout, column/row sizes, preset list and event handlers |
| `BrioPresets/BlueControls.cs` | Colors in `BlueTheme`; rounded buttons/panels; `PanWheel` geometry and hit testing |
| `BrioPresets/PresetIcons.cs` | Thirty vector symbols and `PresetEditor`, the name/symbol dialog |
| `SharedUi/ProfileControls.cs` | Profile selector and its buttons |
| `SharedUi/Locales/en.xml`, `da.xml`, `de.xml` | Translated text; use the same keys in all languages |
| `SharedProfiles/Models.cs` | Saved data; `IconId` is a stable, untranslated identifier |

## Does Visual Studio still have a designer?
Yes: **Windows Forms Designer**. Normally open a Form using **View Designer** (Shift+F7), use Toolbox to add controls, and Properties (F4) to change them. Visual Studio writes the layout into a `.Designer.cs` file.

This project's existing main window is constructed manually in C#, including loops and event handlers. It does **not** have a designer-managed `MainForm.Designer.cs` or XAML. You cannot reliably drag its current layout around in the designer. This is a project-structure limitation, not a missing WPF SDK. This app uses **Windows Forms and .NET Framework 4.8**, not WPF.

For now, editing the files above and running the app is the simplest reliable workflow. For visual experiments, add a separate Windows Form through **Project > Add > Windows Form**, then use its designer. A future conversion of the production window should move layout into a designer-managed partial class while keeping camera operations and events in separate code. Simply renaming Program.cs to Designer.cs will not convert it. Never connect to a camera from a designer constructor.

## Useful layout adjustments
- `Dock = DockStyle.Fill` fills a layout cell. `Padding` is inside spacing; `Margin` is outside spacing.
- `TableLayoutPanel.ColumnStyles` and `RowStyles` control proportions. Percentage rows/columns share remaining space after absolute sizes.
- Keep the direction wheel square by deriving its geometry from `Math.Min(Width, Height)`. Drawing and hit testing must use the same center and scale.
- Custom-painted controls need `ControlStyles.ResizeRedraw` and double buffering. Paint the complete background to prevent old borders or wheel segments remaining after a resize.
- Avoid fixed widths for translated text. Check Danish, English and German, and Windows scaling at 100%, 125%, 150% and 200%.

## Symbols and presets
Save current position opens a name and symbol picker. The pencil edits both without changing the saved camera position. Double-click the name/icon area or press Enter to recall; the play button remains. Double-clicking the pencil area does not recall a preset. Old presets without `IconId` display the camera symbol. Icons are clean code-drawn interpretations of the supplied sheet, not extracted PNGs. They are visual labels only: choosing a screen, microphone or sound symbol does not start sharing or control audio.

To add a symbol, add a stable ID to `PresetIcons.Ids`, its drawing case to `Draw`, and an `Icon: ID` translation to all three locale files. Coordinates use a 64×64 canvas and a consistent stroke. Never rename existing IDs unless you also migrate stored presets.

## Windows verification checklist
- Resize repeatedly, maximize and restore: no old wheel segments, black button corners or clipped borders.
- Open settings in the smallest supported window; the wheel remains fully visible and zoom text stays on one line. Scroll through every setting.
- Save two different symbols; restart and switch profiles; verify symbols persist.
- Edit only a symbol; verify name and position stay unchanged. Cancel must not save.
- Test double-click, play, Enter, pencil, blank list area and duplicate-name replacement.
- Keep the camera gate enabled: start required programs and the actual Zoom meeting before confirming camera readiness.

The changes compile and logic tests run in the build environment. Native Windows rendering, DPI and camera interaction still require this checklist on a Windows PC.

## Microsoft documentation
- [Windows Forms Designer](https://learn.microsoft.com/en-us/visualstudio/designers/windows-forms-designer-overview)
- [Designer tutorial](https://learn.microsoft.com/en-us/visualstudio/designers/walkthrough-windows-forms-designer)
