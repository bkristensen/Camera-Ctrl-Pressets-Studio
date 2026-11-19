[CmdletBinding()]
param(
    [string]$Version = '',
    [string]$IsccPath = '',
    [string]$CertificateThumbprint = '',
    [string]$SignToolPath = 'signtool.exe'
)
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
if (-not $Version) {
    [xml]$buildProperties = Get-Content -LiteralPath (Join-Path $root 'Directory.Build.props') -Raw
    $Version = [string]$buildProperties.Project.PropertyGroup.Version
}
if ($Version -notmatch '^\d+\.\d+\.\d+(?:-[A-Za-z0-9.-]+)?$') { throw 'Invalid version.' }
if (-not $IsccPath) {
    $compiler = Get-Command ISCC.exe -CommandType Application -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($compiler) { $IsccPath = $compiler.Source }
}
if (-not $IsccPath) {
    $programFolders = @($env:ProgramFiles, ${env:ProgramFiles(x86)}) | Where-Object { $_ } | Select-Object -Unique
    foreach ($major in 7, 6) {
        foreach ($folder in $programFolders) {
            $candidate = Join-Path $folder "Inno Setup $major\ISCC.exe"
            if (Test-Path -LiteralPath $candidate -PathType Leaf) {
                $IsccPath = $candidate
                break
            }
        }
        if ($IsccPath) { break }
    }
}
if (-not $IsccPath -or -not (Test-Path -LiteralPath $IsccPath -PathType Leaf)) {
    throw 'Install Inno Setup 6 or 7 or supply -IsccPath.'
}
& "$PSScriptRoot/build.ps1" -CertificateThumbprint $CertificateThumbprint -SignToolPath $SignToolPath
$baseName = "CamCtrlStudio-Setup-$Version"
$installer = Join-Path $root "artifacts/installers/$baseName.exe"
& $IsccPath "/DAppVersion=$Version" "/F$baseName" (Join-Path $root 'installer/CameraControlStudio.iss')
if ($LASTEXITCODE -ne 0) { throw 'Installer compilation failed.' }
if (-not (Test-Path -LiteralPath $installer -PathType Leaf)) { throw "Installer output missing: $installer" }
if ($CertificateThumbprint) {
    & "$PSScriptRoot/sign-file.ps1" -File $installer -CertificateThumbprint $CertificateThumbprint -SignToolPath $SignToolPath
}
Write-Host "Installer: $installer"
