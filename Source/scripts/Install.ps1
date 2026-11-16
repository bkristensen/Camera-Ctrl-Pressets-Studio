# Optional per-user installation of the portable distribution; no elevation required.
[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$exeName = 'Cam Pressets Studio.exe'
$destination = Join-Path $env:LOCALAPPDATA 'Programs\Cam Pressets Studio'
$source = Join-Path $PSScriptRoot $exeName
if (-not (Test-Path -LiteralPath $source)) { throw 'Extract the complete portable ZIP before running this script.' }
if (Get-Process -Name 'Cam Pressets Studio' -ErrorAction SilentlyContinue) { throw 'Close Cam Pressets Studio before installing.' }
New-Item -ItemType Directory -Force -Path $destination | Out-Null
foreach ($name in @($exeName, "$exeName.config", 'README.md', 'LICENSE')) {
    $from = Join-Path $PSScriptRoot $name
    $to = Join-Path $destination $name
    if ([IO.Path]::GetFullPath($from) -ne [IO.Path]::GetFullPath($to)) { Copy-Item -LiteralPath $from -Destination $to -Force }
}
$shell = New-Object -ComObject WScript.Shell
$shortcutPath = Join-Path ([Environment]::GetFolderPath('Programs')) 'Cam Pressets Studio.lnk'
$shortcut = $shell.CreateShortcut($shortcutPath)
$shortcut.TargetPath = Join-Path $destination $exeName
$shortcut.WorkingDirectory = $destination
$shortcut.IconLocation = $shortcut.TargetPath
$shortcut.Save()
Write-Host "Installed: $destination"
Write-Host 'Launch Cam Pressets Studio from the Start menu. Profiles have been preserved.'
