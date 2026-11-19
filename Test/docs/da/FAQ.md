# Ofte stillede spørgsmål

**Er appen gratis?** Ja. MIT tillader brug, ændring, forks og distribution, også
kommercielt, med licensmeddelelsen. Den engelske LICENSE er gældende.

**Hvilke systemer er testet?** Ejeren har afprøvet udviklingsversioner med Logitech
BRIO og Zoom. Det godkender ikke automatisk den seneste version. Andre Logitech-
og USB/UVC-kameraer forventes kun at virke, hvis deres Windows-driver giver relevante
pan/tilt/zoom-kontroller. Teams, Meet, Webex, OBS og Windows Camera er ikke verificeret.
Feedback om både fungerende og fejlede kombinationer er velkommen.

**Kan den bruges uden Zoom?** Kamerastyringen er ikke bundet til Zoom-API'et, men
den nuværende opstartsspærring kræver Zoom og manuel mødebekræftelse. Slå kun denne
fra efter at have vurderet din egen rækkefølge. En generel spærring er fremtidigt arbejde.

**Hvorfor forbindes kameraet ikke?** Kontrollér opstartsspærringen. Start programmerne,
gå ind i Zoom-mødet, kontrollér billedet, og bekræft under Indstillinger.

**Hvordan skifter jeg sprog?** Vælg sprog under Indstillinger, tryk Gem sprog, og
genstart. Valget er fælles for profilerne. En --lang-parameter i genvejen har forrang.

**Hvorfor er enkelte knapper stadig på et andet sprog?** Windows' egne dialoger
og driverfejl kan følge systemets sprog. Brugerens egne navne oversættes ikke.

**Er der preview, optagelse eller skærmdeling?** Nej. Se billedet i et andet program.
Positioner gemmer kun kameraets koordinater; de laver ikke videoscener.

**Hvorfor hakker bevægelsen?** Kameraets driver kan have grove trin. Prøv færre
trin eller længere skiftetid. Omvendte pile drejer ikke billedet.

**Hvor gemmes data?** Den almindelige app bruger %LOCALAPPDATA%\CamPressets.
profiles.json indeholder profiler, language.txt sproget. MSIX kan omdirigere data.

**Beskytter koden mine data?** Nej. Den hindrer utilsigtet redigering af opstartslisten,
men er ikke kryptering eller Windows-adgangskontrol.

**Hvordan afinstallerer jeg?** Installer/Store: brug Windows' appindstillinger.
Portable: luk appen og slet programmappen. Install.ps1: fjern den installerede mappe
og Startmenu-genvejen. Tag backup af profiler, før du bevidst sletter data.

**Hvordan foreslår jeg funktioner?** Brug GitHub Issues' feature-skabelon. Brug
kompatibilitetsskabelonen til hardwarefeedback og Discussions til åbne spørgsmål.
Vedlæg versioner og konkrete testresultater, aldrig private mødedata.
