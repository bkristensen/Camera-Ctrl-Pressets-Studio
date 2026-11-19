$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$locales=Join-Path $root 'Shared/Locales'
[xml]$english=Get-Content (Join-Path $locales 'en.xml') -Raw -Encoding UTF8
[string[]]$reference=@($english.DocumentElement.text | ForEach-Object { $_.key })
foreach ($file in Get-ChildItem -LiteralPath $locales -Filter '*.xml' -File) {
    [xml]$xml=Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8
    if ($xml.DocumentElement.language -cne $file.BaseName) { throw "Incorrect language code: $($file.Name)" }
    $keys=[System.Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
    foreach ($entry in $xml.DocumentElement.text) {
        if (!$keys.Add($entry.key)) { throw "Duplicate key '$($entry.key)' in $($file.Name)" }
        if ([string]::IsNullOrWhiteSpace($entry.InnerText)) { throw "Empty translation '$($entry.key)' in $($file.Name)" }
    }
    if (!$keys.SetEquals($reference)) { throw "Translation keys differ: $($file.Name)" }
}
Write-Host 'Translation keys match.'
