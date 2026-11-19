# Build and release Camera Control Studio

Run the commands from the directory that contains `Camera-Control-Pressets-Studio.sln`. Use Windows and PowerShell for the installer and Windows testing.

## 1. Prepare the release

1. Start from the commit selected for the release. Close any running Camera Control Studio instance before compiling or testing an upgrade.
2. Install the .NET 8 SDK selected by `global.json`. Install Inno Setup 6 or 7 if you plan to publish the installer. Install the Windows SDK if you plan to sign files with SignTool or build an MSIX with MakeAppx.
3. Check the prerequisites with `./scripts/setup-dev.ps1 -CheckOnly`. For a new workstation, `./scripts/setup-dev.ps1` restores the pinned packages and runs the portable build.
4. Set the release version in `Directory.Build.props` and update `CHANGELOG.md`. The installer script reads the default version from the build properties. Keep the four-part Store version separate when preparing an MSIX.

## 2. Build the GitHub packages

Choose one command. The installer build includes the portable build.

| What to build | Command | Output |
| --- | --- | --- |
| Portable ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| Portable ZIP and Inno installer | `./scripts/build-installer.ps1` | Portable ZIP plus `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` runs `check-translations.ps1`, builds Release with locked dependencies, runs the automated tests, and creates the ZIP. The raw Windows executable is at `CameraControlStudio/bin/Release/net48/Camera Control Studio.exe`. The portable staging folder is `artifacts/portable/Camera-Control-Studio/`. Publish the complete ZIP so the executable, configuration, licence and guides travel together.

To use an individual or organization code-signing certificate available in the current user's Windows certificate store, pass its thumbprint and the path to SignTool when building. The installer command signs the application before packing it and signs the completed installer with a timestamp.

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

For the ZIP alone, give the same parameters to `build.ps1`. For sponsored signing, follow the approved [SignPath workflow](../SIGNPATH.md). Mark unsigned downloads clearly.

## 3. Check the packages on Windows

1. Extract the ZIP to a new directory. Start the app from there and open `docs/USER-GUIDES.md`. Verify the app version, launch behaviour, presets, camera controls, and language selection.
2. If you plan to publish the installer, install it on a clean Windows test account. Verify the Start menu app and user-guide shortcuts. Check that all 12 guides are present, that an upgrade retains existing profiles, and that uninstall completes.
3. Work through the [release checklist](../RELEASE-CHECKLIST.md). For signed builds, run SignTool verification on the app executable and installer. Generate hashes only after any signing and repackaging is finished.

## 4. Stage and hash the GitHub downloads

Replace the version in this example for subsequent releases. Omit the installer lines for a ZIP-only release.

```powershell
$version = '0.7.0-beta.2'
$release = "artifacts/releases/$version"
New-Item -ItemType Directory -Path $release -Force | Out-Null

$zip = "Camera-Control-Studio-$version-portable.zip"
$installer = "CamCtrlStudio-Setup-$version.exe"
Copy-Item 'artifacts/Camera-Control-Studio-portable.zip' (Join-Path $release $zip)
Copy-Item "artifacts/installers/$installer" $release
@($zip, $installer) | ForEach-Object {
    $hash = Get-FileHash -LiteralPath (Join-Path $release $_) -Algorithm SHA256
    '{0}  {1}' -f $hash.Hash.ToLowerInvariant(), $_
} | Set-Content -LiteralPath (Join-Path $release 'SHA256SUMS.txt') -Encoding ascii
```

The versioned folder holds the final ZIP, installer and SHA-256 manifest. The `bin` and portable staging directories are build inputs for this step.

## 5. Publish on GitHub

1. Commit the release version, changelog, guides and source changes. In [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases), create a draft and use a tag such as `v0.7.0-beta.2` on the tested commit.
2. Copy the matching changelog entry into the release notes, state whether the downloads are signed, and mark the release as a prerelease.
3. Attach the versioned ZIP, installer if built, and `SHA256SUMS.txt` from `artifacts/releases/<version>/`. Review the filenames, download and verify the finished files, then publish.

## Optional build paths

| Purpose | Script | Output |
| --- | --- | --- |
| Documentation screenshots with sample data | `./scripts/capture-docs.ps1` | Nine PNGs in `docs/images/` for the English, Danish and German screens |
| Docker build and logic tests | `./scripts/build-docker.ps1` | Portable ZIP in `artifacts/docker/` |
| Microsoft Store submission candidate | `./scripts/build-msix.ps1` with your Partner Center identity, publisher and package version | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

The Store build requires the real Partner Center identity and follows the separate [MSIX and Store checklist](../MSIX-AND-STORE.md). The output is an unsigned submission candidate. `Install.ps1` is the optional per-user installer inside an extracted portable ZIP.
