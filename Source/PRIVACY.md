# Privacy

The application stores profiles, camera identifiers, coordinates, image settings and layout locally in your Windows user account. It does not include analytics, telemetry, video recording or automatic upload functionality.

At startup, the application requests the public release list from GitHub to check for a newer version. The request does not include profile, camera, or diagnostic data. The footer link opens the release page when selected.

About links open an external website only when you select them. Those websites apply their own privacy policies. Build tools may contact Microsoft, NuGet, GitHub or signing services when the developer runs them.

Temporary positions are stored only in memory. Documentation capture mode uses example data and does not connect to a camera or load/save profiles. Screenshots capture the visible application area; keep other windows away during capture.

Bug reports are public. Remove personal paths and session details before posting. The author cannot access local profiles through the app.

Warnings and errors are written to `CameraControlStudio.log` in the selected log folder. The default is the application folder, with `%LOCALAPPDATA%\CamPressets\Logs` as a fallback. The log includes the selected camera's display name, app and Windows versions, language, diagnostic messages and stack traces. **Settings → Issues** lets you choose where and how much to log and locate the current file.

**Report on GitHub** prepares a local copy of the latest log (up to the last 4 MiB) and a separate system log. The system log contains Windows build and architecture, app version and architecture, CPU model, logical processor count and installed RAM. It excludes account names, computer names, serial numbers, hardware IDs, and files or app lists. Review the files before attaching them to a public issue.
