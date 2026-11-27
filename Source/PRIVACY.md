# Privacy

Profiles, camera IDs, positions, image settings and layout stay in your Windows user account. The app does not track usage, record video or upload data on its own.

The app checks GitHub’s public release list at startup and when you report a bug or request a feature. The check sends no profile, camera or diagnostic data. Select the footer link to open the release.

GuideViewer checks GitHub for the latest release. When you select **Use online guides**, it requests the selected guide and its images from that release on GitHub. These requests include the guide path and language. They do not contain profile, camera or diagnostic data. GuideViewer uses Microsoft Edge WebView2 to display the guides. WebView2 includes Microsoft Defender SmartScreen and may send diagnostic and security information to Microsoft. See how Microsoft handles this data. [WebView2 data and privacy](https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/data-privacy) · [Microsoft Privacy Statement](https://privacy.microsoft.com/en-us/privacystatement)

About links open an external website only when you select them. Those websites apply their own privacy policies. Build tools may contact Microsoft, NuGet, GitHub or signing services when the developer runs them.

Temporary positions vanish when you close the app. Documentation capture uses sample data. It leaves cameras and profiles alone. Move other windows aside before taking screenshots.

Bug reports are public. Remove personal paths and session details before posting. The author cannot access local profiles through the app.

Warnings and errors are written to `CameraControlStudio.log` in the selected log folder. The default is the application folder, with `%LOCALAPPDATA%\CamPressets\Logs` as a fallback. The log includes the selected camera's display name, app and Windows versions, language, diagnostic messages and stack traces. Open **Contact & About → Contact** to choose where and how much to log and to find the current file.

**Save support ZIP** captures the app window and saves a ZIP at the location you choose. The archive includes available logs, saved profiles, and the window layout. Profiles may include camera IDs and preset names. Review the contents before you attach files to a public issue.

The report button in the unexpected-error dialog prepares a separate local copy of the latest log and a system log. The system log contains Windows build and architecture, app version and architecture, CPU model, logical processor count, and installed RAM.
