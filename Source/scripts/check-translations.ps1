$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
[xml]$english = Get-Content (Join-Path $root 'SharedUi/Locales/en.xml') -Raw
$keys = @($english.translations.text | ForEach-Object { $_.GetAttribute('key') })
foreach ($file in Get-ChildItem (Join-Path $root 'SharedUi/Locales/*.xml')) {
    [xml]$xml = Get-Content $file.FullName -Raw
    $other = @($xml.translations.text | ForEach-Object { $_.GetAttribute('key') })
    if (@($other | Select-Object -Unique).Count -ne $other.Count) { throw "Duplicate keys: $($file.Name)" }
    $missing = @($keys | Where-Object { $_ -cnotin $other })
    $extra = @($other | Where-Object { $_ -cnotin $keys })
    if ($missing.Count -or $extra.Count) { throw "Translation keys differ in $($file.Name): missing $($missing.Count), extra $($extra.Count)" }
    if (@($xml.translations.text | Where-Object { [string]::IsNullOrWhiteSpace($_.InnerText) }).Count) { throw "Empty translation in $($file.Name)" }
    Write-Host "$($file.Name): $($other.Count) entries OK"
}
