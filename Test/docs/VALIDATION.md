# Validation: 0.6.4-beta.1

- Release solution build: zero errors, seven CA1416 platform warnings in cross-platform tests.
- CoreTests, ProfileTests and ShareTests passed.
- Streaming gate tests cover unchecked/no prompt, No refusal, retry/Yes, one confirmation per session, reset, legacy explicit true preservation and missing-field false defaults.
- Profile icon persistence and startup-program order tests remain passing.
- Locale XML keys match; values are nonempty.
- Code review: enumeration and binding both require permission; initial load does not ask or enumerate when checked; camera actions request permission on demand. Source browsing also requests permission.

Not run: Windows GUI, real camera controls, live streaming, Windows DPI, PowerShell, installers, Docker or MSIX. Native tests are listed in docs/en/DEFAULTS-AND-STREAMING.md. The portable EXE is unsigned.

Camera startup no longer depends on a Zoom process. New profiles default to confirmation off; previously saved choices are preserved. Image controls are driver-dependent. HDR, FOV and anti-flicker remain outside this implementation.
