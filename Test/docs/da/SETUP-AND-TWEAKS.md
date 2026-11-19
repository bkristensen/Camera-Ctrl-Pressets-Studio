> 0.6.4: [Updated defaults and camera startup / Opdateret kamerastart / Aktualisierter Kamerastart](DEFAULTS-AND-STREAMING.md). This supersedes earlier Zoom-specific startup instructions.

# Opsætning, indstillinger og justeringer

## Installation og sprog

Udpak hele Windows-pakken, og start Cam Pressets Studio.exe. Appen kræver Windows
og .NET Framework 4.8. Betaversionerne er usignerede. Slå ikke Windows-sikkerheden
fra for at installere dem. Docker bygger appen, men kører ikke brugerfladen.

Vælg Automatisk, English, Dansk eller Deutsch under Indstillinger > Sprog. Tryk
**Gem sprog**, luk Studio, og åbn det igen. Gem også andre ændrede indstillinger før
lukning. Automatisk følger Windows' visningssprog: dansk/tysk, ellers engelsk.
En genvej med --lang=en/da/de/auto har forrang. Windows' egne dialogknapper og fejl
fra driveren kan fortsat være på Windows-sproget.

## Profiler og opstartsprogrammer

Opret profiler til forskellige kameraer eller arbejdsgange. En kopieret profil
medtager opstartsindstillinger og eventuel kode. En tom profil har automatisk
programstart slået fra. Sproget gælder Windows-brugeren, ikke den enkelte profil.

Under Redigér programmer / kode vælges .exe-, .lnk- eller .appref-ms-filer. Angiv
det faktiske procesnavn uden .exe; se Jobliste > Detaljer. Genveje kan åbne et program
med et andet procesnavn. Flyt op/ned ændrer rækkefølgen. Det fælles flueben aktiverer
automatisk start. Gem og start nu afprøver listen. Allerede kørende programmer
springes over. Hvis en proces ikke vises inden 20 sekunder, stopper resten af listen.

Den valgfrie kode låser redigering af opstartsprogrammer, ikke positioner eller
andre indstillinger. Fjern koden ved at låse op og angive en tom ny kode. Det er
ikke kryptering; en person med adgang til konfigurationsfilen kan omgå låsen.

## Kamera og styring

Behold kameraets opstartsspærring ved brug af referenceopsætningen med BRIO og Zoom.
Den blokerer søgning og forbindelse, indtil programmerne kører, og du bekræfter mødet.
Kameravalg og Find kameraer omgår ikke spærringen. Fjernes fluebenet, tillades tidlig
adgang til kameraet.

| Indstilling | Virkning / forslag |
| --- | --- |
| Normale trin | Prøv 1 til præcis indstilling; øg hvis bevægelsen er for langsom |
| Hurtige trin | Brug et større tal til at flytte rundt; kameraets grænser gælder |
| Zoomtrin | Selvstændigt antal drivertrin pr. handling |
| Skiftetid | Længere tid giver blødere skift; 0 giver øjeblikkeligt skift |
| Omvend pile | Vender bevægelsesretning, aldrig selve billedet |
| Altid øverst | Holder kontrolvinduet over andre vinduer |
| Billedindstillinger | Kun egenskaber, som driveren stiller til rådighed |

Tryk Gem indstillinger for at gemme profilens valg. Antal trin er ikke motorhastighed.
Positioner gemmer ikke HDR eller synsfelt. Indstil manglende muligheder i producentens
program, og brug samme opsætning mellem de gemte positioner.

## Fejlfinding og backup

Intet kamera: kontrollér spærringen, USB og Windows' kameraadgang. Optaget kamera:
undersøg andre kontrolprogrammer, men bevar den nødvendige opstartsrækkefølge.
Hakkende bevægelse: prøv færre trin eller længere skiftetid; driveren kan have grove
trin. Omvendt billede: få korrekt billede i Zoom før Studio får adgang.

Den almindelige app gemmer profiler i %LOCALAPPDATA%\CamPressets\profiles.json og
sprog i language.txt ved siden af. Tag backup af begge. MSIX kan omdirigere data;
deling af data med den portable udgave og adfærd ved afinstallation er ikke verificeret.
