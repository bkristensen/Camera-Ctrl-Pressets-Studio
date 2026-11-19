# Run on a Windows desktop at 100% scaling with at least 1280x720 free screen space.
# Uses real UI with sample data. Does not connect to a camera or change profiles.
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
& "$PSScriptRoot/build.ps1"
$exe=Join-Path $root 'CameraControlStudio/bin/Release/net48/Camera Control Studio.exe'
$images=Join-Path $root 'docs/images'
foreach ($lang in @('en','da','de')) {
    $process=Start-Process -FilePath $exe -ArgumentList @("--lang=$lang", "`"--capture-docs=$images`"") -PassThru -Wait
    if ($process.ExitCode -ne 0) { throw "Capture failed for $lang" }
    foreach ($name in @('main','settings','presets')) {
        if (-not (Test-Path "$images/$lang-$name.png")) { throw "Missing screenshot: $lang-$name" }
    }
}
Write-Host 'Review all nine PNG files for cropping, labels and overlap before committing.'
Write-Host 'Replace SVG links with PNG links in the guides and update their image captions after review.'
