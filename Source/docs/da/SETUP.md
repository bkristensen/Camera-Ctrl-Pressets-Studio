[English](../en/SETUP.md) · [Dansk](SETUP.md) · [Deutsch](../de/SETUP.md) · [Alle guidesprog](USER-GUIDES.md)

# Sæt programmet op

*Skrevet til Camera Control Studio 1.0.1.*

[Fejlfinding](TROUBLESHOOTING.md)

Tryk på tandhjulet i bunden. Indstillinger åbner til højre. Positionslisten bliver ved med at være synlig.

![Hovedvinduet med indstillingerne åbne til højre](../images/screenshots/1.0.1/da/main-settings.png)

## Installér og kontrollér downloadet

Hent installationsfilen eller den portable ZIP fra projektets [GitHub-udgivelser](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases). Kør installationsfilen, eller pak hele ZIP-filen ud før start. Hvis der er udgivet en SHA-256-kontrolsum, kan du sammenligne den med resultatet fra `Get-FileHash -Algorithm SHA256`.

**Officiel download**\
<https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases>\
![QR-kode til download af Camera Control Studio](../images/qr/downloads.png)

Microsoft Defender SmartScreen kan vise en blå advarsel, når en fil eller udgiver har et begrænset omdømme. Tag advarslen alvorligt. Hvis du har hentet den forventede fil fra den officielle udgivelse og stoler på kilden, kan du vælge **Flere oplysninger** og kontrollere programnavn og udgiver. Vælg kun **Kør alligevel**, hvis du vil stole på netop den fil, og Windows tilbyder valget. Luk ellers advarslen. Følg eventuelle regler for din pc. [Microsofts SmartScreen-vejledning](https://learn.microsoft.com/windows/apps/package-and-deploy/smartscreen-reputation)

<https://learn.microsoft.com/windows/apps/package-and-deploy/smartscreen-reputation>

Hent WebView2 Runtime fra Microsoft, hvis guideviseren mangler den.\
<https://developer.microsoft.com/microsoft-edge/webview2/>

![SmartScreen med linket Flere oplysninger](../images/reporting/smartscreen-details.png)

![Udvidet SmartScreen-advarsel med knappen Kør alligevel](../images/reporting/smartscreen-run.png)

## 1. Vælg profil og kamera

Brug profilvælgeren i bunden til at vælge profil. Opret en profil under **Indstillinger → Profiler**. Vælg **Ny** for standardindstillinger eller **Kopiér** for at kopiere den aktive profil. Brug et navn for personen eller opstillingen. Den nye profil bliver aktiv. Markér en profil under **Profiler**, og klik på **✎** for at omdøbe den. Standardprofilen har et fast navn.

Dobbeltklik på kamerasymbolet midt i kamerahjulet for at vælge kamera. Programmet husker valget. Hvis kameraet mangler, skal du vælge en anden kilde. Positioner hører til et bestemt kamera i en profil.

![Fanen Profiler med profillisten og knapperne](../images/screenshots/1.0.1/da/profiles.png)

## 2. Vælg, hvornår kamerastyringen må starte

Sæt flueben ved **Spørg om streaming er startet før kameraet bruges** under **Indstillinger → Program**, hvis dit optageprogram skal starte først. Fluebenet er slået fra som standard.

Med fluebenet sat spørger den første kamerafunktion, om streaming er startet. Vælg først **Ja**, når den er startet. **Nej** holder kamerastyringen frakoblet. Efter Ja spørger programmet ikke igen, før du skifter profil, ændrer fluebenet eller genstarter. Programmet registrerer eller starter ikke selv en stream.

Har du brug for denne rækkefølge, skal du sætte fluebenet før næste session. Standardindstillingen tillader forbindelse ved opstart.

![Fanen Program med streamingbekræftelse, vinduesindstillinger og profilsprog](../images/screenshots/1.0.1/da/application.png)

## 3. Indstil bevægelse og zoom

Åbn **Indstillinger → Panorering** for at indstille trin, vende retninger og vælge, hvordan kameraknappen virker. Start med små trin. Se resultatet i dit optageprogram.

![Fanen Panorering med trinstørrelser, retninger og valg for kameraknappen](../images/screenshots/settings-tabs/da/settings.png)

| Indstilling | Hvad den ændrer | Standard |
| --- | --- | --- |
| Fin hastighed (% af normal) | Hastighed, når Ctrl og en piletast holdes nede | 50 % |
| Normale panoreringstrin | Den midterste ring med pile | 1 |
| Hurtige panoreringstrin | Den yderste ring med pile | 4 |

Et trin er ét trin, som kameradriveren understøtter. Det er ikke en fast afstand eller procent. Kameraets driver sætter grænsen for bevægelse og bløde skift.

Hænger kameraet på hovedet, så prøv **Vend kamerapanorering op / ned**. Test pil op. Slå kun **Vend kamerapanorering venstre / højre** til, hvis venstre bevæger sig forkert. Disse valg vender styringen. De drejer ikke videobilledet.

### Indstil zoomtrin

Åbn **Indstillinger → Zoom** for at vælge, hvor meget plus- og minusknapperne ændrer zoom ved ét tryk. Standardværdien er ét drivertrin. [Se fanen Zoom](../images/screenshots/settings-tabs/da/zoom.png).

### Indstil overgange mellem positioner

Åbn **Positioner → Overgange** for at vælge, hvordan kameraet skifter mellem gemte positioner. [Se fanen Overgange](../images/screenshots/settings-tabs/da/transitions.png).

| Indstilling | Hvad den ændrer | Standard |
| --- | --- | --- |
| Brug hastighed ved skift af position | Afstanden bestemmer varigheden | Fra |
| Overgangshastighed (%/s) | Hastighed i procent af den styrende akses område pr. sekund | 30 % |
| Skiftetid i sekunder | Tid til skift ved fast varighed | 1,0 |

Ved hastighedsbaserede skift tager en kort bevægelse mindre tid end en lang. Appen accelererer og bremser og styrer alle akser sammen, så de kan nå målet samtidigt.

### Indstil synsfeltet

Åbn **Video → Synsfelt**. Under **Synsfelt ved opstart** kan du vælge **Bredest muligt**, en vinkel som kameradriveren melder, eller **Gør ingenting** for at beholde det aktuelle synsfelt. Valget gælder, første gang programmet forbinder. Slå **Tillad manuel ændring af synsfelt** til for at vælge mellem vinklerne under kamerahjulet. Hvis driveren ikke melder nogen valgbare vinkler, er synsfeltets indstillinger slået fra. Du kan stadig zoome, hvis kameraet understøtter det. [Se fanen Synsfelt](../images/screenshots/settings-tabs/da/field-of-view.png).

**Skift synsfelt ved grænsen for zoom og panorering** er slået til som standard. Det virker også, når valglisten er skjult, hvis driveren understøtter FOV.

- **Zoom ud:** Ved mindste zoom på en Brio med 65° skifter næste tryk til 78°. Trykket skifter synsfelt og justerer zoom, så udsnittet ændrer sig mindst muligt. Tryk igen for at zoome videre. Det samme sker fra 78° til 90°. Knappen stopper først ved 90° og mindste zoom.
- **Zoom ind:** Ved højeste zoom skifter næste tryk til et smallere synsfelt. Hvis driveren melder 90°, 78° og 65°, bruger programmet dem i den rækkefølge. Knappen stopper ved det smalleste tilgængelige synsfelt og højeste zoom.
- **Drej til venstre eller højre:** Ved kanten prøver programmet først et bredere synsfelt. Det justerer zoom, så udsnittet ændrer sig mindst muligt. Det kan også prøve et smallere synsfelt, hvis zoom tillader det. Hvis ingen af delene giver mere plads, gendanner programmet det forrige udsnit. Driveren afgør, hvor præcist udsnittet kan bevares.

Brug den firkantede knap med rammesymbolet for at gå direkte til bredeste synsfelt og mindste zoom. Skyderen ændrer zoom inden for det aktuelle synsfelt. Slå automatisk skift fra, hvis zoom og panorering skal holde sig inden for det valgte synsfelt.

Positioner gemmer pan, tilt, zoom og det synsfelt, driveren melder tilbage. Ældre positioner beholder kameraets aktuelle synsfelt. Tjek livebilledet, hvis Zoom, Logitech Brio Control eller et andet program også styrer udsnittet. [Testtrin for Brio og Zoom på engelsk](VALIDATION.md#brio-synsfelt-og-zoom)

## 4. Tilpas billedet og gem faste positioner

![Fokus og eksponering på fanen Video](../images/screenshots/settings-tabs/da/image.png)

[Før videoindstillingerne indlæses](../images/screenshots/settings-tabs/da/video.png) · [Farve](../images/screenshots/1.0.1/da/video-colour.png) · [Lys og detaljer](../images/screenshots/1.0.1/da/video-light-detail.png)

Vælg **Video** i indstillingspanelet. Indlæs billedindstillinger, hvis det er nødvendigt. Vælg **Fokus og eksponering**, **Farve** eller **Lys og detaljer**. Kun funktioner fra kameraets driver bliver vist. Du kan ændre lysstyrke, kontrast, farvemætning, fokus eller hvidbalance, hvis de findes. Slå automatik fra før manuel justering. Billedværdier gemmes for kameraet i profilen. Knappen til nulstilling gendanner driverens standarder.

Indstil først kameraet. Vælg **Positioner** i indstillingspanelet, og klik på **+** for at tilføje en fast position. Vælg ikon øverst. Kontrollér navnet, og gem. Er navnefeltet tomt, og er navnet ledigt, foreslår ikonet et navn. Kombinerede symboler viser motiverne fra venstre mod højre.

Under **Faste positioner** kan du holde musen over symbolerne for at se deres funktion. Vælg en position, og brug **✎** til navn og symbol, **↻** til at erstatte med kameraets aktuelle position og **−** til at slette. Flyt positionen med **↑** eller **↓**. Rækkefølgen gemmes og vises også i hovedlisten.

En fast position, du markerer i hovedlisten, markeres også i indstillingerne. Når du gemmer en ny fast position, markeres den i indstillingerne.

![Fanen Positioner med listen over faste positioner og knapperne](../images/screenshots/settings-tabs/da/presets.png)

## 5. Tilpas udseende og sprog

Træk skillelinjen mellem kamerastyringen og positionslisten. Profilen gemmer skillelinjen og vinduets placering og størrelse. Startstørrelsen er 900 × 500, og minimum er 670 × 450. Indstillingerne starter lukket. Første gang du åbner dem efter start, vises den første fane. Rul ned på **Panorering** eller **Program**, hvis nogle felter ligger uden for vinduet. Appen gemmer vinduets bredde med indstillingerne lukket.

Når du vælger profil i bunden, skal **Behold nuværende placering og layout** være uden flueben for at hente profilens gemte udseende. Sæt flueben for at tage det nuværende udseende med. Valget huskes i profilen, du forlader. Det overførte udseende gemmes i den nye profil.

Vælg sprog under **Indstillinger → Program → Profilsprog**, og genstart for at anvende det. **Automatisk** følger Windows' visningssprog, hvis programmet understøtter det, og bruger ellers engelsk. Ved profilskift genstarter programmet med den valgte profils sprog. Midlertidige positioner forsvinder ved genstart.

## 6. Version og projektoplysninger

Åbn **Kontakt & Om → Om** for at se versionen og projektets links. Hold musen over versionen for at se det fulde build-id. Skriv det i en fejlrapport, hvis fejlen gælder en bestemt build.

![Fanen Om med version, udvikler og projektlinks](../images/screenshots/contact/da/about.png)

## 7. Diagnoselogfiler og fejlrapporter

![Fanen Kontakt med GitHub-knapper, supportpakke og diagnoselog](../images/screenshots/contact/da/issues.png)

Åbn **Kontakt & Om → Kontakt** for at vælge logmappe, maksimal filstørrelse og antal logfiler inklusive den aktive. Som standard skrives loggen i programmappen eller i `%LOCALAPPDATA%\CamPressets\Logs`, hvis programmappen ikke kan bruges.

Vælg **Rapportér en fejl** eller **Foreslå en ny funktion** for at åbne det relevante udkast på GitHub. Ved en fejl tilbyder appen at gemme en supportpakke. Gennemgå pakken, og vedhæft den i udkastet. [Indberet eller følg en sag](ISSUES.md)

**Meld en fejl eller foreslå en funktion**\
<https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues?q=is%3Aissue>\
![QR-kode til projektets sager på GitHub](../images/qr/issues.png)

Fortsæt med [Daglig brug](DAILY-USE.md). Tag backup ved at lukke programmet og kopiere `%LOCALAPPDATA%\CamPressets`. Midlertidige positioner er ikke med i backup.
