[CmdletBinding()]
param(
    [Parameter(Mandatory)][ValidatePattern('^[A-Za-z0-9.-]{3,50}$')][string]$IdentityName,
    [Parameter(Mandatory)][string]$Publisher,
    [Parameter(Mandatory)][string]$PublisherDisplayName,
    [string]$DisplayName = 'Cam Pressets Studio',
    [string]$PackageVersion = '0.6.2.0',
    [string]$MakeAppxPath = 'MakeAppx.exe'
)
$ErrorActionPreference = 'Stop'
if ($IdentityName -match 'REPLACE' -or $Publisher -match 'REPLACE' -or $PublisherDisplayName -match 'REPLACE') { throw 'Supply the real Partner Center product identity.' }
if ($Publisher -notmatch '^CN=' -or [string]::IsNullOrWhiteSpace($PublisherDisplayName)) { throw 'Publisher must be the exact Partner Center CN; display name is required.' }
if ($PackageVersion -notmatch '^\d+\.\d+\.\d+\.0$') { throw 'Use a four-part Store version with final component 0.' }
$parts = $PackageVersion.Split('.') | ForEach-Object { [int]$_ }
if (($parts | Where-Object { $_ -gt 65535 }).Count -gt 0 -or ($parts | Measure-Object -Sum).Sum -eq 0) { throw 'Invalid MSIX version.' }
$root = Split-Path $PSScriptRoot -Parent
Push-Location $root
try {
    & "$PSScriptRoot/build.ps1"
    # Package an explicit x64 build; portable builds retain their original target.
    & dotnet build BrioPresets/BrioPresets.csproj -c Release -p:PlatformTarget=x64 -p:Prefer32Bit=false -o artifacts/msix-bin --nologo
    if ($LASTEXITCODE -ne 0) { throw 'x64 build failed.' }
    $stage = Join-Path $root 'artifacts/msix-stage'
    if (Test-Path $stage) { Remove-Item $stage -Recurse -Force }
    New-Item -ItemType Directory -Path "$stage/Assets" -Force | Out-Null
    Copy-Item 'artifacts/msix-bin/Cam Pressets Studio.exe', 'artifacts/msix-bin/Cam Pressets Studio.exe.config', 'LICENSE', 'README.md' $stage
    Copy-Item '*.md' $stage
    Copy-Item 'docs' (Join-Path $stage 'docs') -Recurse
    [xml]$manifest = Get-Content 'packaging/msix/AppxManifest.xml' -Raw
    $manifest.Package.Identity.SetAttribute('Name', $IdentityName)
    $manifest.Package.Identity.SetAttribute('Publisher', $Publisher)
    $manifest.Package.Identity.SetAttribute('Version', $PackageVersion)
    $manifest.Package.Properties.DisplayName = $DisplayName
    $manifest.Package.Properties.PublisherDisplayName = $PublisherDisplayName
    $ns = New-Object System.Xml.XmlNamespaceManager($manifest.NameTable)
    $ns.AddNamespace('uap','http://schemas.microsoft.com/appx/manifest/uap/windows10')
    $manifest.SelectSingleNode('//uap:VisualElements',$ns).SetAttribute('DisplayName',$DisplayName)
    $manifest.Save((Join-Path $stage 'AppxManifest.xml'))
    Add-Type -AssemblyName System.Drawing
    $icon = [System.Drawing.Icon]::new((Join-Path $root 'BrioPresets/Assets/CamPressetsStudio.ico'),256,256)
    $source = $icon.ToBitmap()
    try {
        foreach ($item in @(@('StoreLogo',50),@('Square44x44Logo',44),@('Square150x150Logo',150))) {
            $size = [int]$item[1]
            $bitmap = [System.Drawing.Bitmap]::new($size,$size)
            $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
            try {
                $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
                $graphics.DrawImage($source,0,0,$size,$size)
                $bitmap.Save((Join-Path $stage "Assets/$($item[0]).png"),[System.Drawing.Imaging.ImageFormat]::Png)
            } finally { $graphics.Dispose(); $bitmap.Dispose() }
        }
    } finally { $source.Dispose(); $icon.Dispose() }
    $output = Join-Path $root "artifacts/Cam-Pressets-Studio-$PackageVersion-x64.msix"
    & $MakeAppxPath pack /d $stage /p $output /o
    if ($LASTEXITCODE -ne 0) { throw 'MakeAppx packaging or manifest validation failed.' }
    Write-Host "Unsigned Store submission candidate: $output"
    Write-Host 'This is not a signed sideload installer. Follow docs/MSIX-AND-STORE.md before distribution.'
} finally { Pop-Location }
