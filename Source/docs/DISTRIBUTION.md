# Distribution

Publish a tested Studio build only. The project contains one user-facing app.
Build the portable ZIP and optional Inno Setup installer using the root README.

Unsigned beta downloads may produce Windows security prompts. No script disables
SmartScreen or changes PowerShell execution policy. Do not describe the unsigned
artifacts as signed or guarantee that a signature eliminates all warnings.

Use a trusted code-signing certificate and scripts/sign-file.ps1, or configure a
verified cloud signing identity separately. The provided scripts do not create a
publisher identity. Protect all credentials and verify final signatures after
packaging. The Inno embedded uninstaller is not separately signed here.

Microsoft Store publication is a separate distribution project, not included in
this export. See Microsoft's current guidance:
https://learn.microsoft.com/windows/apps/package-and-deploy/smartscreen-reputation
