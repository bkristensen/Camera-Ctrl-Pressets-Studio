> **0.6.4 camera startup:** confirmation is optional and off for new profiles. Checked profiles ask at first camera use; no Zoom-process requirement or Settings activation button. See [current guide](docs/en/DEFAULTS-AND-STREAMING.md). Earlier gate descriptions below are superseded.

# Cam Pressets Studio

[English guides](docs/en/INDEX.md) · [Danske guider](docs/da/INDEX.md) · [Deutsche Anleitungen](docs/de/INDEX.md) · [Documentation](docs/INDEX.md) · [FAQ](faq.md) · [Project description](docs/PROJECT.md) · [Compatibility](docs/COMPATIBILITY.md) · [MSIX / Store](docs/MSIX-AND-STORE.md) · [Code signing policy](CODE-SIGNING-POLICY.md)

A single C# Windows camera controller with a blue, resizable interface inspired
by the supplied design. Version **0.6.4-beta.1** replaces the separate Studio and
compact downloads. It uses the actual camera driver, not the simulated coordinates
in the original design demo.

## Run

Extract the complete portable ZIP and run `Cam Pressets Studio.exe`.
Windows 10/11 and .NET Framework 4.8 are required. No administrator rights are
normally needed. No preview, recording or audio capture is opened.

An optional `Install.ps1` in the portable folder copies the app into your local
Programs directory and creates a Start-menu shortcut. It does not modify Windows
security or PowerShell execution policy. If scripts are restricted, run the
portable EXE or build the Inno Setup installer on Windows instead.

Use Settings > Language to choose Automatic, English, Dansk or Deutsch. Click
Save language and restart Studio. Automatic follows the Windows display language
for Danish/German and otherwise uses English. A `--lang=en`, `--lang=da`,
`--lang=de` or `--lang=auto` command-line option takes priority for that launch.
Language is saved per Windows user in `%LOCALAPPDATA%\CamPressets\language.txt`,
independently of camera profiles. Windows-owned dialogs may use the OS language.

## Camera startup gate (enabled by default)

Studio does not enumerate or connect to a camera while the gate is locked.
The Settings checkbox **Enable camera only after startup programs and Zoom meeting**
is enabled for new profiles and older profiles that do not contain the setting.

1. Start the configured programs (automatically if enabled, otherwise manually).
2. Start or join the Zoom meeting and verify its camera image.
3. In Settings, click **Zoom meeting is ready — enable camera** and confirm.

Studio verifies that every program in the saved startup list and the Zoom process
are running in the current Windows session. It then asks you to confirm the actual
meeting: process detection cannot prove meeting readiness. No automatic meeting
recognition is claimed. Cancel or missing processes leave the camera disconnected.

The approval lasts only for this session/profile; it is never saved. Restarting,
loading a profile or editing the startup list resets approval. Find cameras and
camera selection cannot bypass the gate. An empty program list still requires Zoom.
Unchecking the option explicitly allows immediate camera connection. Save settings
persists that choice; leave it checked for installations that need this ordering.

## Controls

- Inner wheel: normal steps. Outer wheel: fast steps. Hold a mouse button to repeat.
  Zoom uses its own step setting. With the wheel focused, arrows move and Shift
  selects fast steps. Escape or deactivating the window stops movement.
- Preset rows: select a row, click **▶** to recall or **✎** to rename. Enter recalls
  the selected preset; F2 renames it. Delete removes only the selected preset after
  confirmation. Save current position reads the hardware's actual PTZ values.
- The gear shows/hides the settings panel. Minimum window size is 720×540 with
  settings closed, 1040×540 with settings open. The settings panel scrolls.
- Normal, fast and zoom sliders specify **hardware steps**, not motor speed.
  A value of one is the finest control available from the driver. Transition time
  uses gradual acceleration/deceleration between presets.
- Settings include reversed horizontal/vertical arrows, camera selection, always
  on top, transition time and camera image controls exposed by the driver. Save
  settings persists them per profile. Closing asks about unsaved settings.

There are no demo presets. Screenshot labels such as “Screen 2” or “Speaker +
Screen 2” do **not** imply implemented screen-sharing or video-compositing features.
This release does not expose the unconfirmed Zoom sharing button. Starting Zoom
as an application remains supported.

## Profiles and previous versions

Select **Standard** or create a profile using **New**. A new profile can copy the
current saved configuration. **Refresh** reloads data from disk.

The shared file remains:

```text
%LOCALAPPDATA%\CamPressets\profiles.json
```

Existing v0.5 profiles are retained. New fields receive safe defaults. The first
run can import legacy `BrioPresets/presets.json` and `BrioPresetTool/presets.json`
from LocalApplicationData. Conflicting Mini names receive a suffix and original
files are untouched. Atomic writes keep a `.bak` backup; revision checks prevent
silently overwriting newer data. Close older versions before using this release.

Copying the executable folder does not copy profiles to another Windows account
or PC. Back up the configuration separately. Unpackaged profiles remain after uninstall. MSIX data behaviour still requires testing.
New empty profiles start with automatic program launching **disabled**. Copied
profiles retain their saved startup settings.

## Startup programs and optional code

Under Settings, choose **Edit programs / settings code**. Add the real executable
or shortcut for JW Library, Zoom or another program. Enter its actual process name
without `.exe` (Task Manager > Details). Store apps require a suitable shortcut
and their actual process name; locations are not guessed.

Use Move up / Move down to order the list. One common checkbox enables automatic
startup for the profile. Programs already running in the current Windows session
are skipped. The runner waits up to 20 seconds for each process to appear before
continuing; this does not mean its window has finished loading. Failure stops the
remaining sequence. Save and start now tests the configured list explicitly.

Checks run at app startup or an explicit profile change, not continuously.
Switching profiles or closing the app cancels the pending sequence without closing
programs already started. Programs run with normal user permissions.

The optional code protects editing the startup list and its enable checkbox.
Camera and preset controls stay unlocked. Codes are salted PBKDF2-SHA256 hashes.
This is an accidental-edit lock, not encryption or Windows access control;
someone able to edit the JSON file can bypass it. No recovery code is built in.

## Development prerequisites

Open **CamPressetsStudio.sln**, import **.vsconfig** in Visual Studio Installer, and
run `./scripts/setup-dev.ps1` from the repository root. The app is Windows Forms,
not the earlier WPF demo. Dependencies are declared in global.json, NuGet.Config,
project files and committed packages.lock.json files.

[English setup](docs/en/GETTING-STARTED.md) · [Dansk opsætning](docs/da/GETTING-STARTED.md) · [Deutsche Einrichtung](docs/de/GETTING-STARTED.md)

## Build and installer

On Windows, install .NET SDK 8 or newer and run:

```powershell
./scripts/build.ps1
```

This builds only Studio, runs regression tests and creates
`artifacts/Cam-Pressets-Studio-portable.zip`. NuGet supplies Microsoft's .NET
Framework reference assemblies on first restore.

With Inno Setup 6 installed:

```powershell
./scripts/build-installer.ps1
```

The installer includes only Studio, with English/Danish/German language detection and
per-user installation. The GitHub Actions workflow builds unsigned test artifacts;
it does not publish a release. The exported project is MIT-licensed. The supplied
icon is embedded in the EXE and installer configuration.

## Docker build image

Docker builds and tests the Windows application; it does **not** run its GUI,
camera control or Zoom. Use Docker Desktop in Linux-container mode, or Docker
Engine on Linux. The first build needs internet access to Microsoft Container
Registry and NuGet. A local .NET SDK or PowerShell is not required when using the
Bash script; the Windows launcher requires PowerShell on the host.

From the project root on Windows:

```powershell
./scripts/build-docker.ps1
```

On Linux/macOS:

```bash
bash scripts/build-docker.sh
```

Both scripts build the local image `cam-pressets-studio-build:local`, run the
existing build and regression tests during the image build, and export:

```text
artifacts/docker/Cam-Pressets-Studio-portable.zip
artifacts/docker/portable/Cam-Pressets-Studio/
```

The temporary export container is removed after copying the files; the image stays
available locally. No image is pushed to a registry. Existing output files of the
same name are replaced. The default target is `linux/amd64`; other hosts may need
Docker's emulation support. Customize the image, output and platform:

```powershell
./scripts/build-docker.ps1 -Image cam-pressets:dev -OutputDirectory artifacts/docker-dev -Platform linux/amd64
```

```bash
bash scripts/build-docker.sh cam-pressets:dev artifacts/docker-dev linux/amd64
```

To build only the image:

```bash
docker build --platform linux/amd64 -t cam-pressets-studio-build:local .
```

The final image contains only runtime dependencies and the compiled artifacts.
Optionally run it with an existing absolute host output directory mounted at
`/output`; it copies the files there and exits. It exposes no service or port.
The default scripts export with `docker create`/`docker cp` without running it.

Docker output is **unsigned**. The Windows Inno Setup installer and certificate
signing still use the Windows scripts described above. `.dockerignore` restricts
the build context to source inputs and excludes local output, profiles and keys.
SDK/runtime tags float with .NET 8 servicing; pin approved image digests if exact
base-image reproducibility is required.

References: [Microsoft SDK images](https://mcr.microsoft.com/en-us/product/dotnet/sdk/about),
[Dockerfile reference](https://docs.docker.com/reference/dockerfile/).

## Signing

**This beta is unsigned.** No private signing key or verified publisher identity
was provided. A self-signed certificate does not remove public SmartScreen warnings.

The build accepts a trusted code-signing certificate installed in the current
user's Windows certificate store, with its private key accessible to SignTool:

```powershell
./scripts/build-installer.ps1 -CertificateThumbprint YOUR_40_HEX_THUMBPRINT -SignToolPath 'C:\path\to\signtool.exe'
```

The script signs and verifies the application before packaging, then signs and
verifies the installer. It requests a SHA-256 RFC3161 timestamp. The installer’s
embedded uninstaller is not separately signed by this script. Certificate/token
setup and publisher validation must be completed by the publisher. Never commit
private keys or passwords to the repository.

Signing does not guarantee zero SmartScreen prompts for a new download.
[Microsoft's SmartScreen guidance](https://learn.microsoft.com/windows/apps/package-and-deploy/smartscreen-reputation)
identifies Microsoft Store distribution as the reliable route to avoiding those
download prompts. Store publication requires its own account, packaging and review;
this export is not a Store submission. See [MSIX preparation](docs/MSIX-AND-STORE.md) for the new manifest and packaging script.

## Validation and limitations

See `docs/VALIDATION.md` and `docs/WINDOWS-TEST.md`. The project can be cross-compiled
here, and model/driver-math tests can run without a camera. The Windows interface,
DPI layout, hardware, startup shortcuts and installer still need on-device testing.
The design is implemented using drawn WinForms controls and native window chrome;
it is not a screenshot or a pixel-identical copy of the mockup.

Driver granularity limits smoothness. HDR, field of view, anti-flicker and firmware
controls are not implemented. Presets store PTZ only; keep field of view consistent.
The app does not send network requests. Started applications may do so.
Independent community project, not affiliated with Logitech or Zoom.

## Editing the interface

[English](docs/en/UI-DEVELOPMENT.md) · [Dansk](docs/da/UI-DEVELOPMENT.md) · [Deutsch](docs/de/UI-DEVELOPMENT.md). The existing UI is code-built WinForms, not XAML or designer-managed layout.
