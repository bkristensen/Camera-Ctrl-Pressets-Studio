# Projektbeskrivelse

*Skrevet til Camera Control Studio 1.1.1.*

Camera Control Studio gemmer og genkalder kamerabilleder på Windows. Brug den i et studie, et klasseværelse eller et livemøde. Din optagelsesapp håndterer videoen.

## Kerneadfærd

Brug den indre ring til normale træk og den ydre ring til hurtige træk. Hver ring har diagonale pile. Dobbeltklik på midterknappen for at vælge et kamera, eller træk det for at styre. Du kan ændre begge bevægelser under Indstillinger → Panorering. Zoomknapperne flyttes med det indstillede trin. Skyderen indstiller zoom inden for den aktuelle FOV. Appen åbner ingen videoforhåndsvisning. [Indstil bevægelse og synsfelt](SETUP.md#indstil-synsfeltet)

Permanente positioner gemmer et navn, et stabilt symbol-id og panorering, tilt, zoom og FOV, som føreren rapporterer. Kombinerede symboler viser motiver fra venstre mod højre. Faste stillinger vises først med fed skrift. Midlertidige positioner har intet ikon og varer, indtil du lukker appen. Brug Afspil eller dobbeltklik for at genkalde begge typer.

En jævn interpolation anmoder om mellemliggende koordinater. En række er gul under overgangen. Den er kun grøn, når tilbagelæsningen matcher målet. En manuel handling, annullering eller mislykket overgang rydder denne tilstand. Driveren bestemmer den faktiske bevægelsesgranularitet og latens.

Profiler indeholder kameravalg, permanente positioner, billedværdier, manuelle bevægelseshastigheder, forudindstillet overgangsvarighed eller hastighed, retningsvending, kamerabekræftelse, vindueslayout og sprog. Ændring af profiler genstarter appen med den valgte profils sprog. Valgfri kamerabekræftelse blokerer opregning og forbindelse, indtil brugeren bekræfter, at streaming er startet.

## Arkitektur

`CameraControlStudio` er en .NET Framework 4.8 WinForms-applikation. `Forms/*.Designer.cs` hold layout. `Forms/*.cs` hold begivenhedshåndtering. `AppForm` er den fælles base. `Controls` indeholder vektorgengivelse. `Camera.cs` bruger Windows DirectShow kamera og video-proc interfaces uden en optagelsesgraf.

`Shared` indeholder modeller, persistens, oversættelse, bevægelsesaritmetik, sessionspositioner og tilstandslogik. `Tests` er en .NET 8 konsol regression suite. Profiler bruger atomic filerstatning, sikkerhedskopier og revisionstjek. Eksisterende lager på 0,6 profil bibeholdes; forældede programstartindstillinger ignoreres og kasseres ved lagring.

## Omfang og kompatibilitet

Ejeren har rapporteret, at den tidligere applikation fungerede på deres bærbare computer og kameraopsætning. Denne 0.7-omskrivning har kompileret og bestået logiske test, men er ikke blevet udøvet mod fysisk hardware i dette byggemiljø. Ingen yderligere kamera- eller optagelsessoftware hævdes testet.

Andre Windows-kameraer fungerer muligvis, når deres drivere implementerer de nødvendige standardkontroller. Samtidig adgang med software til optagelse, møde eller streaming er driverafhængig. Virtuelle kameraer må ikke udsætte nogen fysisk kontrol. Dette er ikke en generisk netværks-PTZ-controller. Linux og macOS kan bygge værter via Docker-arbejdsgangen, men kan ikke køre denne Windows-brugergrænseflade indbygget.

Rapportér vellykkede kombinationer, herunder Windows-version, kameramodel, driverversion, optagelsesapplikation og hvilke kontroller, der fungerede. Rapportér også begrænsninger, så kompatibilitetslisten kan være evidensbaseret.

## Muligt fremtidigt arbejde

Windows UI regression automatisering; bedre tilgængelighed og tastaturgenkendelse; valgfri import/eksport af individuelle profiler; verificeret hardware matrix; underskrevne udgivelser efter godkendelse; ekstra kamerabackends, hvis de bevarer den enkle kernearbejdsgang. Disse er ideer, ikke leverede funktioner.
