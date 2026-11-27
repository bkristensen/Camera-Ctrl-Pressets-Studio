# Byg og frigiv Camera Control Studio

*Skrevet til Camera Control Studio 1.0.1.*

Kør kommandoerne fra den mappe, der indeholder `Camera-Control-Pressets-Studio.sln`. Brug Windows og PowerShell til installationsprogrammet og Windows-testning.

## 1. Forbered udgivelsen

1. Start fra den commit, der er valgt til udgivelsen. Luk enhver kørende Camera Control Studio-instans, før du kompilerer eller tester en opgradering.
2. Installer .NET 8 SDK valgt af `global.json`. Installer Inno Setup 6 eller 7, hvis du planlægger at udgive installationsprogrammet. Installer Windows SDK, hvis du planlægger at signere filer med SignTool eller bygge en MSIX med MakeAppx.
3. Tjek forudsætningerne med `./scripts/setup-dev.ps1 -CheckOnly`. For en ny arbejdsstation gendanner `./scripts/setup-dev.ps1` de fastlåste pakker og kører den portable build.
4. Indstil udgivelsesversionen i `Directory.Build.props` og opdater `CHANGELOG.md`. Installationsscriptet læser standardversionen fra build-egenskaberne. Hold den firedelte Store-version adskilt, når du forbereder en MSIX.

## 2. Byg GitHub-pakkerne

Vælg én kommando. Installationsversionen inkluderer den portable build.

| Hvad skal bygges | Kommando | Output |
| --- | --- | --- |
| Portabel ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| Portabel ZIP og Inno installationsprogram | `./scripts/build-installer.ps1` | Portabel ZIP plus `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` kontrollerer oversættelser, bygger Release med låste afhængigheder, kører test og opretter ZIP. Find programfilen i `CameraControlStudio/bin/Release/net48` og de klargjorte filer i `artifacts/portable/Camera-Control-Studio`. Udgiv hele ZIP. Appen har brug for dens konfiguration, biblioteker og vejledninger.

For at bruge et enkeltperson- eller organisationskodesigneringscertifikat, der er tilgængeligt i den aktuelle brugers Windows-certifikatlager, skal du videregive dets certifikataftryk og stien til SignTool, når du bygger. Installationskommandoen underskriver applikationen, inden den pakkes, og underskriver det færdige installationsprogram med et tidsstempel.

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

For ZIP alene, giv de samme parametre til `build.ps1`. For sponsoreret signering skal du følge den godkendte [SignPath-workflow](SIGNPATH.md). Marker usignerede downloads tydeligt.

## 3. Tjek pakkerne på Windows

1. Udpak ZIP til en ny mappe. Start appen derfra, og åbn **?** med Indstillinger lukket og åben. Med [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) installeret, bekræft guidenavigation og udskriv til PDF for at kontrollere titlen, udskriftsdato og -klokkeslæt, installeret version, projektadresse og sidetal. Bekræft appversionen, startadfærd, forudindstillinger, kamerakontroller og sprogvalg, når du skifter profil.
2. Hvis du planlægger at udgive installationsprogrammet, skal du installere det på en ren Windows-testkonto. Bekræft appen Startmenu og genveje til brugervejledningen. Tjek, at alle 12 guider er til stede, at en opgradering bevarer eksisterende profiler, og at afinstallationen er fuldført.
3. Gennemfør [-udgivelsestjeklisten](RELEASE-CHECKLIST.md). For signerede builds skal du køre SignTool-bekræftelse på appens eksekverbare og installationsprogram. Generer kun hashes, efter enhver signering og ompakning er afsluttet.

## 4. Iscenesætter og hash GitHub-downloads

Erstat versionen i dette eksempel for efterfølgende udgivelser. Udelad installationslinjerne for en ZIP-only release.

```powershell
$version = '1.0.0'
$release = "artifacts/releases/$version"
New-Item -ItemType Directory -Path $release -Force | Out-Null

$zip = "Camera-Control-Studio-$version-portable.zip"
$installer = "CamCtrlStudio-Setup-$version.exe"
Copy-Item 'artifacts/Camera-Control-Studio-portable.zip' (Join-Path $release $zip)
Copy-Item "artifacts/installers/$installer" $release
@($zip, $installer) | ForEach-Object {
    $hash = Get-FileHash -LiteralPath (Join-Path $release $_) -Algorithm SHA256
    '{0}  {1}' -f $hash.Hash.ToLowerInvariant(), $_
} | Set-Content -LiteralPath (Join-Path $release 'SHA256SUMS.txt') -Encoding ascii
```

Den versionerede mappe indeholder det endelige ZIP, installationsprogram og SHA-256 manifest. `bin` og bærbare iscenesættelsesmapper er build-input til dette trin.

## 5. Udgiv på GitHub

1. Opdater versionens undertekst i hver guide, du gennemgår for denne udgivelse. Hold dens oversættelser synkroniseret. Overfør udgivelsesversionen, ændringslog, guider og kildeændringer. I [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases) skal du oprette et udkast og bruge et tag såsom `v1.0.0` på den testede commit.
2. Kopier den matchende ændringslogpost til udgivelsesbemærkningerne, angiv, om downloads er signerede, og vælg kun forhåndsudgivelsesindstillingen for forhåndsvisningsbuilds.
3. Vedhæft den versionerede ZIP, installationsprogram, hvis den er bygget, og `SHA256SUMS.txt` fra `artifacts/releases/<version>/`. Gennemgå filnavnene, download og bekræft de færdige filer, og udgiv derefter.

## Valgfri byggestier

| Formål | Script | Output |
| --- | --- | --- |
| Dokumentationsskærmbilleder med eksempeldata | `./scripts/capture-docs.ps1` | PNG'er i `docs/images/screenshots/<version>/<language>/` for alle 12 brugervejledninger |
| Docker build og logik test | `./scripts/build-docker.ps1` | Portabel ZIP i `artifacts/docker/` |
| Microsoft Store indsendelseskandidat | `./scripts/build-msix.ps1` med din Partner Center identitet, udgiver og pakkeversion | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

Butiksbygningen kræver den rigtige Partner Center-identitet og følger den separate [MSIX og Store-tjekliste](MSIX-AND-STORE.md). Outputtet er en usigneret indsendelseskandidat. `Install.ps1` er den valgfri per-bruger-installationsprogram inde i en ekstraheret bærbar ZIP.
