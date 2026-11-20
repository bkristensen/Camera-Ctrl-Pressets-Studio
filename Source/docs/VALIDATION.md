# Validation for 0.7.0-beta.3

The Windows Release build uses locked dependencies. Translation parity and the automated tests passed. The tests cover motion, presets, profile language, persistence, logging and the camera-not-ready HRESULT reported after hibernation.

The extracted portable ZIP was tested in x86 and x64 Windows processes. Both opened and reopened the daily-use and setup guides, displayed their screenshots, and printed to PDF. The x64 PDF had the title, version, date and correct page count on all five pages. The **?** button was present during WinForms design-time initialization. The ARM64 loader was checked for the correct binary architecture.

The English Standard profile was captured with a demo camera. The 12 full-window screenshots cover both main-window states, each settings tab and every subtab. The local links in the guides and READMEs were checked against the packaged document paths.

Inno Setup 7 compiled the installer with the guides for all 12 languages and the x86, x64 and ARM64 loaders. Both packages are unsigned, with SHA-256 hashes supplied. A clean-account installation and uninstall, a physical camera hibernation cycle, and an ARM64 runtime test are pending. See the [release checklist](RELEASE-CHECKLIST.md) and [build and release guide](en/BUILD-AND-RELEASE.md).
