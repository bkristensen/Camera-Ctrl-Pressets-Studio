# Privacy

Profiles, camera IDs, positions, image settings and layout stay in your Windows user account. The app does not track usage, record video or upload data on its own.

At startup, the app checks GitHub’s public release list. It sends no profile, camera or diagnostic data. Select the footer link to open the release.

GuideViewer uses Microsoft Edge WebView2 to display the bundled guides. WebView2 includes Microsoft Defender SmartScreen and may send diagnostic and security information to Microsoft. See how Microsoft handles this data. [WebView2 data and privacy](https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/data-privacy) · [Microsoft Privacy Statement](https://privacy.microsoft.com/en-us/privacystatement)

About links open an external website only when you select them. Those websites apply their own privacy policies. Build tools may contact Microsoft, NuGet, GitHub or signing services when the developer runs them.

Temporary positions vanish when you close the app. Documentation capture uses sample data. It leaves cameras and profiles alone. Move other windows aside before taking screenshots.

Bug reports are public. Remove personal paths and session details before posting. The author cannot access local profiles through the app.

Warnings and errors are written to `CameraControlStudio.log` in the selected log folder. The default is the application folder, with `%LOCALAPPDATA%\CamPressets\Logs` as a fallback. The log includes the selected camera's display name, app and Windows versions, language, diagnostic messages and stack traces. **Settings → Issues** lets you choose where and how much to log and locate the current file.

**Report on GitHub** prepares a local copy of the latest log (up to the last 4 MiB) and a separate system log. The system log contains Windows build and architecture, app version and architecture, CPU model, logical processor count and installed RAM. It excludes account names, computer names, serial numbers, hardware IDs, and files or app lists. Review the files before attaching them to a public issue.
