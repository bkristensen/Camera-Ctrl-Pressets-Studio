# Check prerequisites, optionally install the SDK, restore pinned packages and build.
[CmdletBinding()]
param([switch]$InstallSdk, [switch]$CheckOnly)
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
Push-Location $root
try {
    if ($InstallSdk -and $CheckOnly) { throw 'Choose either -InstallSdk or -CheckOnly.' }
    if ($InstallSdk) {
        if (-not (Get-Command winget -ErrorAction SilentlyContinue)) { throw 'WinGet is missing. Install .NET SDK 8 from https://dotnet.microsoft.com/download/dotnet/8.0 and reopen the terminal.' }
        & winget install --id Microsoft.DotNet.SDK.8 --exact --source winget
        if ($LASTEXITCODE -ne 0) { throw 'WinGet did not complete successfully. If the SDK is already installed, rerun without -InstallSdk.' }
        Write-Host 'SDK installation completed. Reopen the terminal and run ./scripts/setup-dev.ps1.'
        return
    }
    if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) { throw 'Missing .NET SDK. Run ./scripts/setup-dev.ps1 -InstallSdk or install .NET SDK 8 manually.' }
    $sdk = & dotnet --version
    if ($LASTEXITCODE -ne 0) { throw 'No SDK matching global.json. Install .NET SDK 8.0.4xx or newer 8.0 feature band, then reopen the terminal.' }
    Write-Host "Selected SDK: $sdk"
    if ($env:OS -eq 'Windows_NT') {
        $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio/Installer/vswhere.exe'
        if (Test-Path -LiteralPath $vswhere) {
            $desktop = & $vswhere -latest -products '*' -requires Microsoft.VisualStudio.Workload.ManagedDesktop -property installationPath
            if (-not $desktop) { Write-Warning 'For Visual Studio, import .vsconfig using Visual Studio Installer > More > Import configuration.' }
            else { Write-Host "Desktop workload: $desktop" }
        } else { Write-Warning 'Visual Studio not found. CLI builds can still work; import .vsconfig if using Visual Studio.' }
    } else { Write-Warning 'The application UI can only run on Windows. This host can build and run logic tests.' }
    if ($CheckOnly) { Write-Host 'Prerequisite check only; no restore or build performed.'; return }
    & dotnet restore Camera-Control-Pressets-Studio.sln --locked-mode --disable-parallel -m:1
    if ($LASTEXITCODE -ne 0) { throw 'NuGet restore failed. Check the first error, network/proxy access and NuGet.Config. Do not delete lock files as a workaround.' }
    & "$PSScriptRoot/build.ps1"
    Write-Host 'Ready: open Camera-Control-Pressets-Studio.sln, set CameraControlStudio as Startup Project, then F5.'
} finally { Pop-Location }
