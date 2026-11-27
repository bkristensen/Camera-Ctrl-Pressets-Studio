[CmdletBinding()]
param([switch]$SkipBuild)

$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
[xml]$properties = Get-Content -LiteralPath (Join-Path $root 'Directory.Build.props') -Raw
$version = [string]$properties.Project.PropertyGroup.Version
if ($version -notmatch '^\d+\.\d+\.\d+(?:-[A-Za-z0-9.-]+)?$') { throw 'Invalid screenshot version.' }

$images = Join-Path $root 'docs/images/screenshots'
$target = Join-Path $images $version
if (Test-Path -LiteralPath $target) {
    throw "Screenshots for $version already exist. Earlier screenshot folders stay in place."
}
if (-not $SkipBuild) {
    & dotnet build (Join-Path $root 'CameraControlStudio/CameraControlStudio.csproj') -c Release --nologo -p:RestoreLockedMode=true
    if ($LASTEXITCODE -ne 0) { throw 'Screenshot build failed.' }
}

$exe = Get-Item -LiteralPath (Join-Path $root 'CameraControlStudio/bin/Release/net48/Camera Control Studio.exe')
if ($exe.VersionInfo.ProductVersion.Split('+')[0] -ne $version) { throw 'The executable version does not match the screenshot folder.' }

$staging = Join-Path $root ('artifacts/screenshots-' + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $staging | Out-Null
foreach ($language in 'en','da','de','ar','bn','es','fr','hi','id','pt','ur','zh') {
    $output = Join-Path $staging $language
    $arguments = @("--lang=$language", ('"--capture-docs=' + $output + '"'))
    $process = Start-Process -FilePath $exe.FullName -WindowStyle Hidden -PassThru -ArgumentList $arguments
    try {
        if (-not $process.WaitForExit(30000)) {
            $process.Kill()
            throw "Screenshot capture timed out for $language."
        }
        if ($process.ExitCode -ne 0) { throw "Screenshot capture failed for $language." }
    } finally {
        $process.Dispose()
    }

    $names = @('main','presets','settings','zoom','transitions','application')
    if ($language -in @('en','da','de')) {
        $names += @('main-settings','video','image','video-colour','video-light-detail','field-of-view','profiles','issues','about')
    }
    if ($language -eq 'da') { $names += 'quick-guide' }
    foreach ($name in $names) {
        if (-not (Test-Path -LiteralPath (Join-Path $output "$name.png") -PathType Leaf)) {
            throw "Missing screenshot: $language/$name.png"
        }
    }
    Write-Host "Captured $($names.Count) screenshots for $language."
}

# Move the verified set only after all languages have finished.
[System.IO.Directory]::Move($staging, $target)
Write-Host "Review the screenshots in $target before committing."
