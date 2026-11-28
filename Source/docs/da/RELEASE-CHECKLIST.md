# Frigiv tjekliste

*Skrevet til Camera Control Studio 1.1.1.*

Brug [build and release guide](BUILD-AND-RELEASE.md) til kommandoer og outputstier.

## Automatiserede kontroller

- [ ] Kør `scripts/build.ps1` på en ren kasse med låste afhængigheder.
- [ ] Bekræft, at hver lokalitet har de samme unikke, ikke-tomme nøgler. Bygningen kører oversættelseskontrollen og lokalitetstesten.
- [ ] Gennemgå kilden og den binære version, pakkens identitet og udgivelsesbemærkninger.

## Vinduer og kameratjek

- [ ] Start ved 900 × 500 og bekræft minimum 670 × 450. Tilpas størrelse, maksimer og gendan ved 100 %, 125 %, 150 % og 200 % skærmskalering. Tjek sidefoden, input, kombinerede ikoner og zoom-glyffer.
- [ ] Åbn indstillinger til højre. Træk skillelinjen. Luk og åbn igen. Test et gemt layout efter frakobling af en skærm.
- [ ] Genkald en position: gul under bevægelse, grøn kun efter matchende aflæsning. Pan/tilt/zoom rydder den aktive farve. Afbryd forbindelsen under en overgang: ingen falsk grøn. Ikoner forsvinder aldrig efter bekræftelse.
- [ ] Test hvert kombineret ikon, inklusive hver omvendt rækkefølge. Test zoomknapper, zoomskyderen og begge retningsvendinger.
- [ ] På en understøttet Brio, test automatisk FOV i begge zoomretninger og ved venstre og højre panoreringskant. Følg [Brio-testtrinene](VALIDATION.md#brio-synsfelt-og-zoom).
- [ ] Aktiver kamerabekræftelse. Genstart. Bekræft ingen kameraopregning eller forbindelse før Ja. Test Nej, og prøv derefter igen fra en panorerings-/zoomknap. Testprofilændring og kildevalg.

## Data og redigering

- [ ] Sikkerhedskopier 0.6-profilfilen før migrering. Bekræft, at forudindstillinger og ikon-id'er overlever. Bekræft, at forældede eksterne programindstillinger er fraværende efter lagring.
- [ ] Test nye/kopier/omdøb profiler, gendan layout og hold-aktuel-layout-adfærd. Bekræft, at midlertidige stillinger forbliver kun session.
- [ ] Test billedets automatiske/manuelle indstillinger og standardindstillinger med det faktiske kamera.
- [ ] Åbn hver formular med Shift+F7 i Visual Studio. Juster en layoutegenskab, gem og genopbyg. Brugerdefineret tegning forbliver i kontrolklasserne.
- [ ] Åbn guidefremviseren fra den udpakkede ZIP i en 64-bit Windows-proces. Åbn den igen og udskriv en guide til PDF. Bekræft installationsprogrammet, og ZIP inkluderer den matchende WebView2 native loader under `runtimes` for hver understøttet arkitektur.
- [ ] Tjek [user-guide index](USER-GUIDES.md) og bekræft, at hver sprogguide er til stede i begge pakker. Tjek [guide-skærmbillederne](SCREENSHOTS.md) for fuldstændig fane- og underfanedækning, beskæring og overlapning.

## Distribution

- [ ] For en bærbar udgivelse skal du bruge `scripts/build.ps1`. For et Inno Setup-installationsprogram skal du bruge `scripts/build-installer.ps1`, som også kører den portable build og tests. Bekræft den version, der vises af installationsprogrammet og appen.
- [ ] For en butiksudgivelse skal du følge [Store-tjeklisten](MSIX-AND-STORE.md). Test de pakketyper, du planlægger at udgive på Windows.
- [ ] Hvis du underskriver, skal du bekræfte Authenticode-signaturerne på appens eksekverbare og installationsprogram, hvor det er relevant. Mærk usignerede downloads tydeligt. Generer SHA-256-hashes efter endelig signering og pakning.
- [ ] Link til [Code-signeringspolitikken](SIGNPATH.md) i GitHub release notes. For SignPath-udgivelser skal du kontrollere build-herkomst og manuel godkendelse og kreditere SignPath for filer, den har underskrevet.
- [ ] Udgiv kilde, licens, release notes, hashes og binære filer sammen. Gør ikke krav på uudførte hardwaretests eller butiksgodkendelse.
