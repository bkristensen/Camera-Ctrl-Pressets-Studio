# Validation: 0.6.1-beta.1

- Studio net48 Release build: passed with zero errors and zero warnings.
- CoreTests: passed (movement snapping, easing, reversed directions, persistence,
  English/Danish selection). Cross-platform test compilation emits CA1416 warnings
  for Windows COM methods; hardware calls were not executed.
- ProfileTests: passed (legacy migration, profile isolation, stale-save protection,
  concurrent revision conflicts, startup ordering/failure/cancellation, code hashes,
  and new fast-step, always-on-top and camera-ID fields).
- ShareTests: passed for retained legacy workflow logic; no Zoom button is exposed
  in this UI and no live Zoom sharing has been verified.

Not run: native Windows UI or DPI rendering, camera hardware, launching JW Library
or Zoom, PowerShell scripts, Inno Setup installation, signing, SmartScreen testing.
No claim of pixel-identical rendering or hardware validation is made.

The unsigned portable archive contains a compiled Studio EXE and its configuration.
The source archive includes build, installation and optional signing scripts. It
contains no compiled installer, no publisher certificate, no private key and no
simulated camera as the production backend.

0.6.1: camera gate default/migration, persisted opt-out, empty-list Zoom requirement,
missing required processes and Zoom exit were tested with a simulated runtime.
Code review: enumeration and connection are both gated before accessing a device.
Actual Zoom meeting state is confirmed by the user, not automatically detected.

Docker additions: Bash syntax checked and launcher tested with a simulated Docker
CLI for build/export and failure cleanup. Docker Engine and PowerShell are not
available in this environment; the actual image build and PowerShell launcher
have not been executed. The Dockerfile reuses scripts/build.ps1.

MSIX/documentation additions: manifest XML and local Markdown links checked.
Windows SDK MakeAppx, PowerShell packaging, Store certification and installed MSIX
behaviour have not been executed here. No signed or compiled MSIX is included.
