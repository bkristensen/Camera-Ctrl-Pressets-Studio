# Kodesigneringspolitik

*Skrevet til Camera Control Studio 1.1.1.*

SignPath Foundation tilbyder gratis signering for kvalificerede open source-projekter. Accept er påkrævet. Certifikatet identificerer SignPath Foundation som udgiver; tjenesten forbinder verificerede builds til kildelageret. Projektet har ansøgt om SignPath Foundation-signering. Ansøgningen afventer, og GitHub-downloads forbliver usignerede.

Inden du ansøger, skal du offentliggøre kilden under dens licens. Hold projektet opdateret. Udgiv en udgivelse, dokumenter appen og gør buildet verificerbart. Underskriv kun dine egne projektfiler. Fortsæt med at signere legitimationsoplysninger ud af lageret.

[privatlivspolitikken](../../PRIVACY.md) dækker appen, dens GitHub opdateringstjek og GuideViewers WebView2 runtime.

## Mens ansøgningen afventer

- Kontroller, at det offentlige lager har en vellykket Windows-build under **Actions**. Brug den arbejdsgang som grundlag for SignPath-integrationen.
- Bekræft de personer, der er ansvarlige for tilsagn, anmeldelser og underskriftsgodkendelser. Slå multifaktorgodkendelse til for alle teammedlemmer på GitHub og SignPath.
- Bed SignPath om at bekræfte, hvordan WebView2 SDK-binære filer og Inno Setup-installationsprogrammet skal håndteres. Pakkerne inkluderer Microsofts SDK-binære filer og indlæsere. Appen bruger WebView2 Runtime installeret på brugerens pc. Angiv disse afhængigheder i applikationen og bevar deres meddelelser.
- Tilføj et **Code-signeringspolitik**-link til GitHub-udgivelsesbemærkningerne til downloadsiden, der leveres i applikationen. Angiv den faktiske signeringsstatus for disse downloads.

## Efter SignPath accepterer projektet

- Tilføj den godkendte SignPath-tilskrivning og de bekræftede navne eller tilladelsesgrupper for formidlere, anmeldere og godkendere til denne side.
- Konfigurer SignPath til at acceptere artefakter fra den betroede GitHub Actions build. Begræns signering til projektbinære filer. Indstil og håndhæv produktnavnet og den matchende produktversion på tværs af de indsendte filer.
- For ZIP skal du underskrive programfilen app, før den pakkes. For installationsprogrammet skal du sætte den signerede app eksekverbare i installationsprogrammet, før du signerer det færdige installationsprogram. Få manuel godkendelse for hver anmodning om udgivelsessignering.
- Bekræft Authenticode-signaturerne og tidsstempler, og generer derefter SHA-256 kontrolsummer. Inkluder et **Code-signeringspolitik**-link og signeringsstatus i udgivelsesbemærkningerne. Bekræft de uploadede filer og hashes før udgivelse.

For direkte GitHub-downloads identificerer en gyldig signatur udgiveren og beskytter binærens integritet. En ny download kan stadig udløse SmartScreen, mens omdømmet opbygges. Til førstegangsdistribution gennem butikken underskriver Microsoft accepterede MSIX-pakker.

For et offentligt betroet kodesigneringscertifikat, der er tilgængeligt via den aktuelle brugers Windows-certifikatlager, skal du bruge `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` til den bærbare ZIP. Brug de samme parametre med `scripts/build-installer.ps1` til at signere både programfilen app og det genererede installationsprogram. SignPath-godkendelse kræver sin egen signeringsarbejdsgang.

Læs [ officielle vilkår](https://signpath.org/terms.html), [Foundation-webstedet ](https://signpath.org/) og Microsofts [SmartScreen-vejledning](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation). Tjekket 24. november 2026. For butiksruten, se [MSIX og Store](MSIX-AND-STORE.md).
