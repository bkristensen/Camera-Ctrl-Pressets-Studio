[CmdletBinding()]
param(
    [string]$Version = '0.6.2-beta.1',
    [string]$IsccPath = "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
    [string]$CertificateThumbprint = '',
    [string]$SignToolPath = 'signtool.exe'
)
$ErrorActionPreference = 'Stop'
if ($Version -notmatch '^\d+\.\d+\.\d+(?:-[A-Za-z0-9.-]+)?$') { throw 'Invalid version.' }
& "$PSScriptRoot/build.ps1" -CertificateThumbprint $CertificateThumbprint -SignToolPath $SignToolPath
if (-not (Test-Path -LiteralPath $IsccPath)) { throw 'Install Inno Setup 6 or supply -IsccPath.' }
$root = Split-Path $PSScriptRoot -Parent
& $IsccPath "/DAppVersion=$Version" (Join-Path $root 'installer/BrioPresets.iss')
if ($LASTEXITCODE -ne 0) { throw 'Installer compilation failed.' }
if ($CertificateThumbprint) {
    & "$PSScriptRoot/sign-file.ps1" -File (Join-Path $root "artifacts/installers/Cam-Pressets-Studio-Setup-$Version.exe") -CertificateThumbprint $CertificateThumbprint -SignToolPath $SignToolPath
}
