[CmdletBinding()]
param([string]$Configuration = 'Release', [string]$CertificateThumbprint = '', [string]$SignToolPath = 'signtool.exe')
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
Push-Location $root
try {
    & "$PSScriptRoot/check-translations.ps1"
    & dotnet build 'BrioPresets/BrioPresets.csproj' -c $Configuration --nologo
    if ($LASTEXITCODE -ne 0) { throw 'Studio build failed.' }
    foreach ($test in @('CoreTests', 'ShareTests', 'ProfileTests')) {
        & dotnet run --project "$test/$test.csproj" -c $Configuration
        if ($LASTEXITCODE -ne 0) { throw "Tests failed: $test" }
    }
    $exe = "BrioPresets/bin/$Configuration/net48/Cam Pressets Studio.exe"
    if ($CertificateThumbprint) {
        & "$PSScriptRoot/sign-file.ps1" -File $exe -CertificateThumbprint $CertificateThumbprint -SignToolPath $SignToolPath
    }
    $stage = Join-Path $root 'artifacts/portable/Cam-Pressets-Studio'
    if (Test-Path $stage) { Remove-Item $stage -Recurse -Force }
    New-Item -ItemType Directory -Force -Path $stage | Out-Null
    Copy-Item $exe, "$exe.config", 'README.md', 'LICENSE' $stage
    Copy-Item '*.md' $stage
    Copy-Item 'docs' (Join-Path $stage 'docs') -Recurse
    Copy-Item 'scripts/Install.ps1' $stage
    Compress-Archive -Path "$stage/*" -DestinationPath 'artifacts/Cam-Pressets-Studio-portable.zip' -Force
} finally { Pop-Location }
