# Validation for 0.7.0-beta.2

The Windows Release build uses locked dependencies. The automated suite checks motion arithmetic, preset state, profile and settings persistence, translation coverage, and zoom range mapping.

Release preparation checked that the portable ZIP contains the Release executable and guides for all 12 languages. The Inno Setup 7 compiler packaged the same language guides and the user-guide shortcut into the Windows installer. SHA-256 hashes identify the final local packages.

Use the [release checklist](RELEASE-CHECKLIST.md) for clean Windows install, upgrade and uninstall testing, physical camera checks, and per-monitor layout checks. See the [build and release guide](en/BUILD-AND-RELEASE.md) for the packaging and signing commands. The staged beta downloads are unsigned.
