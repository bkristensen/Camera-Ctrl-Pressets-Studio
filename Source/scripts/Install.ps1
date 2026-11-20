# Optional per-user installation of the portable distribution; no elevation required.
[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$exeName = 'Camera Control Studio.exe'
$destination = Join-Path $env:LOCALAPPDATA 'Programs\Camera Control Studio'
$source = Join-Path $PSScriptRoot $exeName
if (-not (Test-Path -LiteralPath $source)) { throw 'Extract the complete portable ZIP before running this script.' }
if (Get-Process -Name 'Camera Control Studio' -ErrorAction SilentlyContinue) { throw 'Close Camera Control Studio before installing.' }
New-Item -ItemType Directory -Force -Path $destination | Out-Null
$runtime = Get-ChildItem -LiteralPath $PSScriptRoot -Filter '*.dll' -File | ForEach-Object Name
foreach ($name in @($exeName, "$exeName.config", 'README.md', 'LICENSE', 'THIRD-PARTY-NOTICES.md') + @($runtime)) {
    $from = Join-Path $PSScriptRoot $name
    $to = Join-Path $destination $name
    if ([IO.Path]::GetFullPath($from) -ne [IO.Path]::GetFullPath($to)) { Copy-Item -LiteralPath $from -Destination $to -Force }
}
foreach ($folder in 'docs', 'runtimes') {
    $from = Join-Path $PSScriptRoot $folder
    $to = Join-Path $destination $folder
    if ((Test-Path -LiteralPath $from) -and [IO.Path]::GetFullPath($from) -ne [IO.Path]::GetFullPath($to)) {
        Copy-Item -LiteralPath $from -Destination $destination -Recurse -Force
    }
}
$shell = New-Object -ComObject WScript.Shell
$shortcutPath = Join-Path ([Environment]::GetFolderPath('Programs')) 'Camera Control Studio.lnk'
$shortcut = $shell.CreateShortcut($shortcutPath)
$shortcut.TargetPath = Join-Path $destination $exeName
$shortcut.WorkingDirectory = $destination
$shortcut.IconLocation = $shortcut.TargetPath
$shortcut.Save()
Write-Host "Installed: $destination"
Write-Host 'Launch Camera Control Studio from the Start menu. Profiles have been preserved.'
