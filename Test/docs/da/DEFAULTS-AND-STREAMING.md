# Standardværdier, streaming og kamerakilder (0.6.4)

## Gem dine normale indstillinger
Åbn tandhjulet, vælg en profil, ret værdierne og tryk **Gem indstillinger**. De indlæses sammen med profilen. Opret en ny profil med **Ny**, og behold **Kopiér den aktuelle gemte konfiguration**, hvis den skal have samme værdier. En tom profil får fabriksstandarderne. Ændringer af standarder i kildekoden overskriver ikke eksisterende profiler.

## Ret fabriksstandarder i koden
Åbn `SharedProfiles/Models.cs`, klassen `SavedData`, metoden `InitializeDefaults`. Både nye profiler og manglende felter i gamle profiler får værdier herfra. Ret `Seconds`, `PanSteps`, `ZoomSteps`, `FinePanSteps`, `FineZoomSteps`, `FastPanSteps` og `WaitForStartup` her. Nu er værdierne: 2 sekunder, normale trin 5, hurtige trin 32, zoomtrin 5, fine trin 1 og streamingbekræftelse false. Øvrige booleske valg er som udgangspunkt false. Kompilér, og prøv med en ny tom profil. Slet ikke din eksisterende profilfil for at teste standarderne.

## Valgfri streamingbekræftelse
**Spørg om streaming er startet før kameraet bruges** er slået fra i nye profiler. Gamle profiler beholder deres gemte valg, også hvis det tidligere var slået til. Feltet hedder fortsat `WaitForStartup` i filen for kompatibilitet, men betyder nu streamingbekræftelse.

Med flueben søger programmet ikke efter kameraer og forbinder ikke ved opstart. Første panorering, zoom, hentning/gemning af position, kamerasøgning, indlæsning af billedindstillinger eller kildevalg spørger, om streaming er startet. **Nej** afbryder uden kameratilgang. Næste forsøg spørger igen. **Ja** tillader kameraet resten af sessionen og fortsætter handlingen. Genstart, profilskift og ændring af fluebenet nulstiller bekræftelsen. Redigering af opstartsprogrammer nulstiller den også. Der kræves ikke et bestemt streamingprogram. Programmer i opstartslisten startes fortsat separat i den valgte rækkefølge.

Uden flueben kan programmet forbinde ved opstart uden spørgsmål. Hvis kameraet er frakoblet, prøver en kamerahandling at forbinde til senest gemte kamera, ellers et tilgængeligt kamera. Du skal ikke først ind i indstillingerne. Mangler kameraet, eller fejler driveren, vises en fejl. En position anvendes ikke på et andet kamera, som programmet automatisk måtte have valgt.

## Kilde og billede
Tryk på kameraikonet i midten af hjulet, eller giv hjulet fokus og tryk Space/Enter. Vælg kamera og bekræft. Valget gemmes i profilen. Det vælger kameraet, som dette program styrer; det ændrer ikke kameravalget i dit streamingprogram og starter ikke skærmdeling.

Under tandhjulet > Billede kan du trykke **Indlæs billedindstillinger**, hvis kameraet ikke er tilsluttet. Der er lysstyrke, kontrast, farveintensitet, farvetone, skarphed, gamma, forstærkning, modlyskompensation, fokus, hvidbalance og eksponering, hvor driveren understøtter dem. Automatik vises kun ved understøttelse. Øvrige felter markeres utilgængelige. **Gendan driverens billedstandarder** nulstiller billedkontroller, ikke panorering eller zoom. HDR, synsfelt og flimmerreduktion kræver fortsat producentens funktioner. Billedværdier ændres direkte i kameraet og gemmes ikke i positionspresets.

## Afprøv på Windows
1. Ny tom profil: flueben fra og ingen spørgsmål ved opstart eller bevægelse.
2. Sæt flueben, gem og genstart. Første kamerahandling spørger. Nej må ikke forbinde eller bevæge. Prøv igen med Ja; senere handlinger må ikke spørge igen.
3. Genstart eller skift profil: en profil med flueben skal spørge igen. Prøv uden et bestemt streamingprogram åbent.
4. Vælg kilde i midten, gem og genstart. Kontrollér husket kamera. Afprøv et frakoblet kamera uden at anvende dets positioner på et andet kamera.
5. Afprøv billedkontroller, automatik, nulstilling, alle sprog og små vinduer.

Windows-grafik og kamera skal stadig testes på din PC. Logik- og lagringstest bruger ikke hardware.
