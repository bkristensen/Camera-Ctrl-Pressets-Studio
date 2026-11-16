# Distribution, Store og SignPath

## Vælg distributionsform

| Mulighed | Brug | Vigtigt |
| --- | --- | --- |
| Portable ZIP på GitHub | Tidlige testudgaver | Usignerede filer kan give Windows-advarsler |
| Inno Setup-installation | Almindelig Windows-installation | Byg på Windows; signering er et særskilt trin |
| Microsoft Store med MSIX | Offentlig udgivelse og opdateringer | Microsoft signerer efter godkendelse |
| Store med privat målgruppe | Afprøvning hos udvalgte personer | Brugernes personlige Microsoft-konti skal være tilføjet |
| Offentlig Store-side via direkte link | Deling uden normal synlighed | Er ikke adgangskontrol eller en privat målgruppe |
| Store package flights | Test af forskellige opdateringspakker | Kræver opsætning af grupper i Partner Center |
| Direkte MSIX uden Store | Sideload eller administreret installation | Kræver passende signering og tillid på modtagerens pc |

Et offentligt Store-produkt kan ikke senere ændres til privat målgruppe. Vælg derfor
privat test først, hvis det er nødvendigt. Store via MSI/EXE kræver din egen betroede
signering; det er ikke det samme som gratis signering af MSIX via Store.

## MSIX-checkliste

- [ ] Opret en udviklerkonto via https://storedeveloper.microsoft.com/ og gennemfør identitetskontrol.
- [ ] Reservér et tilgængeligt navn. Kopiér Identity Name, Publisher CN og Publisher Display Name.
- [ ] Installér .NET SDK 8+ og Windows SDK med MakeAppx.exe på Windows.
- [ ] Kør nedenstående med de faktiske værdier, ikke eksempelteksterne.
- [ ] Test pakken, dataflytning, kamera, opstartsspærring og eksterne programmer på Windows.

```powershell
./scripts/build-msix.ps1 -IdentityName 'DIN.IDENTITY' -Publisher 'CN=DIN-PUBLISHER-ID' -PublisherDisplayName 'Dit udgivernavn' -MakeAppxPath 'C:\sti\MakeAppx.exe'
```

Scriptet bygger og tester, laver x64-udgaven, genererer ikoner og opretter en usigneret
MSIX-kandidat. Den er ikke en færdig, betroet sideload-installation. Pakningen kræver
Windows; den er ikke afprøvet i udviklingsmiljøet her. Versionsnummer har fire dele
og slutter med 0. Minimum i manifestet er Windows build 19041.

## Før indsendelse til Store

- [ ] Brug rigtige skærmbilleder af appen uden private mødedata.
- [ ] Angiv faktisk supportadresse og en gennemgået privatlivspolitik på en offentlig URL.
- [ ] Udfyld målgruppe, markeder, sprog, kategori og aldersmærkning korrekt.
- [ ] Forklar fuld tillid: kameradriverstyring og brugerens valgte programstart, ikke administratoradgang.
- [ ] Beskriv Zoom-spærringen og hardwarekrav til testeren; gennemfør certificering før offentlig udgivelse.

MSIX kan omdirigere brugerdata. Test profilimport, opdatering og afinstallation.
Antag ikke, at Store-udgaven automatisk deler filer med den portable udgave.

## SignPath

SignPath Foundation kan tilbyde gratis signering til godkendte open source-projekter.
Det kræver ansøgning; der er ingen automatisk ret til godkendelse. En signatur beviser
heller ikke, at programmet er fejlfrit, eller at SmartScreen aldrig viser en advarsel.

Forbered et offentligt, vedligeholdt projekt, dokumenterede udgivelser og sporbare builds.
Gennemgå krav om licens, MFA, review, godkendere, privatlivspolitik og afinstallation.
Hver signeret udgivelse kræver godkendelse efter udbyderens regler. Den eksisterende
signeringsfunktion til et lokalt certifikat er ikke en SignPath-integration.

Projektets aktuelle status: ingen SignPath-ansøgning, godkendelse eller signering.
Brug ikke deres mærker eller godkendelsestekst, før det er sandt. Se https://signpath.org/terms
og https://signpath.org/apply. MSIX-forberedelsen betyder heller ikke, at appen allerede findes i Store.
