![Camera Control Studio main window with the direction wheel, zoom slider and presets. The settings pane is closed.](Source/docs/images/screenshots/main.png)

# Camera Control Studio

**[Download Camera Control Studio 0.7.0-beta.2](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases/tag/Beta)** · [User guides in 12 languages](Source/docs/USER-GUIDES.md) · [FAQ](Source/FAQ.md)

Camera Control Studio is a Windows app for controlling a supported camera during video meetings, recordings and live streams. Move and zoom the camera manually, then save useful framings as named presets so you can switch between them during a session. For example, save a wide shot of a stage, a close-up of the speaker and a shot of the demonstration table.

Use it alongside Zoom Meetings or your preferred capture or streaming app. That app displays the picture, shares it with viewers and handles recording. Camera Control Studio sends pan, tilt, zoom and image commands through the Windows camera driver.

## Download and install

Download the installer or portable ZIP under **Assets** on the [Beta release page](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases/tag/Beta). Browse [all releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases) for other versions.

| Download | How to use it |
| --- | --- |
| Windows installer (`CamCtrlStudio-Setup-*.exe`) | Run Setup to install the app and add Start menu shortcuts for the app and its guides. |
| Portable ZIP (`Camera-Control-Studio-*-portable.zip`) | Extract the entire archive and run `Camera Control Studio.exe`. The guides are in the extracted `docs` folder. |
| `SHA256SUMS.txt` | When provided, compare the published SHA-256 hash with your downloaded file. |

You need Windows with .NET Framework 4.8 and a camera whose Windows driver exposes the controls you want to use. The current beta packages are unsigned, so Windows may show a publisher warning. See the [FAQ](Source/FAQ.md) for details.

## Get started

1. Open your meeting or streaming app and choose the camera you want to use. Start capture first if your camera setup requires it.
2. Open Camera Control Studio. Use the profile picker in the footer and click the camera icon in the centre of the direction wheel to choose the same camera.
3. Use the inner arrows for small pan or tilt moves and the outer arrows for larger moves. Use the magnifier buttons to zoom in and out, or drag and release the vertical slider to set zoom directly.
4. Frame the shot in your meeting or streaming app. Click **+** to save it temporarily, or open the gear and choose **Presets → Add permanent preset** to save it for future sessions.
5. Click **▶** on a preset row to recall the shot. Yellow shows a move in progress. Green means the driver has reported the saved coordinates. Check the framing in your meeting or streaming app.

## Presets and transitions

Store permanent presets by camera and profile. Give each preset a name and optional symbol that makes it easy to recognise at a glance. In the **Presets** tab you can edit the name and symbol, replace the coordinates with the current position, or delete the preset. Temporary presets last for the current session and appear below permanent presets.

Preset transitions use the duration set in **Settings**. The camera driver determines the available positions and how finely the camera can move. Manual movement cancels a transition, and unavailable directions are disabled at the limits of the camera's range.

![Presets tab with the permanent preset list and buttons to add, edit, replace or delete a preset](Source/docs/images/screenshots/presets.png)

## Settings, image controls and profiles

| Tab | What you can do |
| --- | --- |
| [Settings](Source/docs/images/screenshots/settings.png) | Set normal and fast movement steps, transition time, direction reversal, always-on-top behaviour and the interface language. You can also ask the app to wait for your confirmation before controlling the camera. |
| [Image](Source/docs/images/screenshots/image.png) | Adjust the controls exposed by the camera driver, such as focus, exposure, sharpness and backlight compensation. Image settings are saved per camera and profile. |
| [Profiles](Source/docs/images/screenshots/profiles.png) | Create or copy profiles for different operators, rooms or camera setups. Use the footer picker to switch profiles. |
| [About](Source/docs/images/screenshots/about.png) | Find the installed version, developer credit and links to the project, issue tracker and discussions. |

Drag the vertical divider to resize the camera controls and preset list. Hover over compact buttons for tooltips. The settings pane starts closed each time the app opens.

<details>
<summary>See the Settings, Image, Profiles and About tabs</summary>

![Settings tab with movement steps, transition time and language options](Source/docs/images/screenshots/settings.png)

![Image tab with camera controls for focus, exposure and sharpness](Source/docs/images/screenshots/image.png)

![Profiles tab with a profile list and buttons to create, copy or rename profiles](Source/docs/images/screenshots/profiles.png)

![About tab with developer information, project links and the installed version](Source/docs/images/screenshots/about.png)

</details>

## Camera compatibility

Each camera driver determines which controls are available and how they behave when another app is using the camera. A control can work during a Zoom meeting and show no visible change in the Windows Camera preview. Check your framing in the app you will use to broadcast or take the meeting. See [camera compatibility and project scope](Source/docs/PROJECT.md).

## Documentation

- [Setup and settings](Source/docs/en/SETUP.md) covers camera selection, movement steps, image controls, permanent presets and profiles.
- [Daily use](Source/docs/en/DAILY-USE.md) covers recalling shots, manual adjustments and temporary presets.
- [All user guides](Source/docs/USER-GUIDES.md) covers English, Danish, German, Mandarin Chinese (Simplified Chinese), Hindi, Spanish, Arabic, Bengali, French, Indonesian, Portuguese and Urdu.
- [Build and release guide](Source/docs/en/BUILD-AND-RELEASE.md) explains how to build, verify and package the Windows downloads.

## Source and contributions

Open `Source/Camera-Control-Pressets-Studio.sln` in Visual Studio for the WinForms application. The [development guide](Source/docs/en/DEVELOPMENT.md) covers editing the interface and translations. See [Contributing](Source/CONTRIBUTING.md) for guidance on pull requests.

Camera Control Studio is open source under the [MIT licence](LICENSE), developed by **Brian Kristensen, Vejle Uhrhøj, Denmark**. [Report a bug or request a feature](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues) · [Discuss an idea](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/discussions) · [Privacy](Source/PRIVACY.md) · [Third-party notices](Source/THIRD-PARTY-NOTICES.md)
