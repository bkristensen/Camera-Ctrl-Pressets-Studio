# Capture the guide images

The checked-in SVG files are labelled layout illustrations. They are not screenshots or evidence that the UI has been tested. The setup and daily-use guides say this explicitly.

To replace them with real program screenshots:

1. On Windows, use 100% display scaling and a desktop with at least 1280 × 720 pixels. Close any foreground windows that could cover the app.
2. Run `./scripts/capture-docs.ps1` from the project folder. The script builds the app, then opens it once in each language with sample data. Do not interact while it captures.
3. Review the nine PNG files in `docs/images`. Check text, labels and control boundaries. The app must remain fully visible while capture runs.
4. In both guides for each language, replace image link endings `.svg` with `.png`. Replace the illustration captions with “Application screenshot with sample data” in that language.
5. Commit the PNG files and updated guides together. GitHub will display the relative image and language links.

Capture mode uses actual Forms, rendered by Windows, with synthetic positions. It never sets normal runtime mode, never connects to a camera and never loads or saves profiles. The green sample row illustrates the state; it is not a hardware test. Capture itself still needs Windows verification.

| Label | Control |
| --- | --- |
| A / B | Profile selection / settings gear |
| C / D | Direction controller and central source selector / zoom buttons and slider |
| E / F | Draggable divider / position list |
| G / H | Add temporary position / status area |
| I / J | Settings tabs / general settings fields |
| K / L | Permanent position list / add |
| M / N / O | Edit name and symbol / replace coordinates / delete |
