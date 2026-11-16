[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$File,
    [Parameter(Mandatory)][ValidatePattern('^[A-Fa-f0-9]{40}$')][string]$CertificateThumbprint,
    [string]$SignToolPath = 'signtool.exe',
    [string]$TimestampUrl = 'http://timestamp.digicert.com'
)
$ErrorActionPreference = 'Stop'
$cert = Get-Item -LiteralPath "Cert:\CurrentUser\My\$CertificateThumbprint" -ErrorAction Stop
if (-not $cert.HasPrivateKey) { throw 'Certificate has no accessible private key.' }
if ($cert.NotAfter -le (Get-Date)) { throw 'Certificate has expired.' }
if (-not ($cert.EnhancedKeyUsageList | Where-Object { $_.ObjectId.Value -eq '1.3.6.1.5.5.7.3.3' })) {
    throw 'Certificate does not permit code signing.'
}
& $SignToolPath sign /sha1 $CertificateThumbprint /s My /fd SHA256 /tr $TimestampUrl /td SHA256 $File
if ($LASTEXITCODE -ne 0) { throw "Signing failed: $File" }
& $SignToolPath verify /pa /all /v $File
if ($LASTEXITCODE -ne 0) { throw "Signature verification failed: $File" }
