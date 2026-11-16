[CmdletBinding()]
param(
    [string]$Image = 'cam-pressets-studio-build:local',
    [string]$OutputDirectory = 'artifacts/docker',
    [string]$Platform = 'linux/amd64'
)
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
$container = $null
Push-Location $root
try {
    & docker info --format '{{.OSType}}'
    if ($LASTEXITCODE -ne 0) { throw 'Start Docker Desktop / Docker Engine with Linux containers.' }
    & docker build --platform $Platform --tag $Image --file Dockerfile .
    if ($LASTEXITCODE -ne 0) { throw 'Docker build or tests failed.' }
    New-Item -ItemType Directory -Force -Path $OutputDirectory | Out-Null
    $output = (Resolve-Path -LiteralPath $OutputDirectory).Path
    $container = & docker create $Image
    if ($LASTEXITCODE -ne 0) { throw 'Could not create the artifact container.' }
    $container = "$container".Trim()
    & docker cp "${container}:/artifacts/." $output
    if ($LASTEXITCODE -ne 0) { throw 'Could not export build artifacts.' }
    Write-Host "Image: $Image"
    Write-Host "Windows ZIP: $(Join-Path $output 'Cam-Pressets-Studio-portable.zip')"
} finally {
    if ($container) { & docker rm $container | Out-Null }
    Pop-Location
}
