[CmdletBinding()]
param([string]$Configuration='Release', [string]$CertificateThumbprint='', [string]$SignToolPath='signtool.exe')
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
Push-Location $root
try {
    & "$PSScriptRoot/check-translations.ps1"
    & dotnet build Camera-Control-Pressets-Studio.sln -c $Configuration --nologo -p:RestoreLockedMode=true
    if ($LASTEXITCODE -ne 0) { throw 'Build failed.' }
    & dotnet run --project Tests/Tests.csproj -c $Configuration --no-build --no-restore
    if ($LASTEXITCODE -ne 0) { throw 'Logic tests failed.' }
    $exe="CameraControlStudio/bin/$Configuration/net48/Camera Control Studio.exe"
    if ($CertificateThumbprint) { & "$PSScriptRoot/sign-file.ps1" -File $exe -CertificateThumbprint $CertificateThumbprint -SignToolPath $SignToolPath }
    $stage=Join-Path $root 'artifacts/portable/Camera-Control-Studio'
    if (Test-Path $stage) { Remove-Item $stage -Recurse -Force }
    New-Item -ItemType Directory -Force -Path $stage | Out-Null
    Copy-Item $exe, "$exe.config", 'LICENSE', 'README.md' $stage
    Copy-Item 'docs' (Join-Path $stage 'docs') -Recurse
    Copy-Item 'scripts/Install.ps1' $stage
    Compress-Archive -Path "$stage/*" -DestinationPath 'artifacts/Camera-Control-Studio-portable.zip' -Force
} finally { Pop-Location }
