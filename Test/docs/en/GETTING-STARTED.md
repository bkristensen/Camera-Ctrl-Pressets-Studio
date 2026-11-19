# Get started from Git or a source ZIP

## Visual Studio

1. Clone the repository or extract the complete source ZIP. Do not open an old
   CamPressetsControlDemo project; that was a separate WPF design prototype.
2. Update Visual Studio 2022 to 17.11 or newer (a current supported Visual Studio
   release is recommended). Import the root `.vsconfig` through Visual Studio
   Installer > More > Import configuration. It selects .NET desktop development
   and the .NET Framework 4.8 SDK/targeting pack.
3. Install .NET SDK 8.0.4xx or a later 8.0 feature band. The .NET Framework runtime
   alone is insufficient. `.vsconfig` does not guarantee this exact modern SDK.
4. Open **CamPressetsStudio.sln** and allow NuGet restore. Set **BrioPresets** as
   Startup Project. Build, then press F5 on Windows.

The application is **Windows Forms targeting net48, not WPF**. UIAutomationClient
and WindowsBase references support retained Windows automation code; they do not
make the app a WPF project. No `dotnet workload install wpf` command is needed.
The exact cause of a reported Visual Studio error cannot be determined without
its error code and full message.

## Guided setup from the repository root

```powershell
./scripts/setup-dev.ps1
```

This checks the selected SDK, reports missing Visual Studio desktop tooling,
restores locked NuGet dependencies, builds and runs the existing regression tests.
It does not silently install Visual Studio, change execution policy or disable security.

To explicitly request installation of the .NET 8 SDK with WinGet:

```powershell
./scripts/setup-dev.ps1 -InstallSdk
```

After installation, reopen the terminal and run the normal command. For checks only,
use `-CheckOnly`. A Windows PowerShell policy may block unsigned scripts; use the
manual commands below or follow your administrator's signing policy, not a global bypass.

```powershell
dotnet --version
dotnet restore CamPressetsStudio.sln --locked-mode
dotnet build CamPressetsStudio.sln -c Release --no-restore
```

## Dependency files

| File | Responsibility |
| --- | --- |
| .vsconfig | Visual Studio desktop workload and Framework development components |
| global.json | Selects stable .NET 8 SDK, minimum 8.0.400, allows newer 8.0 feature bands |
| NuGet.Config | Explicit public NuGet source; no credentials |
| Project .csproj files | Target framework, resource files and package references |
| Per-project packages.lock.json | Resolved direct/transitive package versions and content hashes |
| CamPressetsStudio.sln | App and three logic-test projects in one Visual Studio solution |

Do not copy bin/obj folders between PCs. No Python dependency file is needed for
the application. Inno Setup is optional for classic installers; Windows SDK MakeAppx
is optional for MSIX. Docker is optional for container builds, not normal development.

## If it still fails

- Missing SDK / NETSDK1045: run `dotnet --list-sdks`, check global.json, install the SDK
  and update Visual Studio. An installed .NET 9/10 SDK alone does not satisfy this pin.
- MSB3644 / Framework references: import .vsconfig and restore NuGet. The project also
  obtains Framework reference assemblies through its compile-time NuGet package.
- NU1301 / feed errors: check access to https://api.nuget.org/v3/index.json and proxy settings.
- WindowsDesktop SDK missing: confirm a complete Microsoft SDK installation and
  the desktop workload. Verify you opened this solution, not the old WPF demo.
- For help, copy the first error code/full text, Visual Studio version and `dotnet --info`.
  Do not include private paths or credentials unnecessarily.

To intentionally update dependencies, edit package versions, run `dotnet restore
CamPressetsStudio.sln --force-evaluate`, review/commit all changed lock files and
run tests. Do not remove locks merely to hide a restore mismatch.

The Windows setup script and IDE workflow still need testing on an actual Windows PC.
References: https://learn.microsoft.com/visualstudio/install/import-export-installation-configurations
and https://learn.microsoft.com/dotnet/core/tools/global-json
