$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$reference=$null
foreach ($lang in @('en','da','de')) {
    [xml]$xml=Get-Content (Join-Path $root "Shared/Locales/$lang.xml") -Raw -Encoding UTF8
    $keys=@($xml.DocumentElement.text | ForEach-Object { $_.key })
    if (@($keys | Select-Object -Unique).Count -ne $keys.Count) { throw "Duplicate keys: $lang" }
    if ($reference -and (Compare-Object $reference $keys)) { throw "Translation keys differ: $lang" }
    $reference=$keys
}
Write-Host 'Translation keys match.'
