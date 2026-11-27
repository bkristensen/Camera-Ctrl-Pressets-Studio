# Fejlfinding

*Skrevet til Camera Control Studio 1.0.1.*

## Indholdsfortegnelse

- [1. Videoprogrammer og synsfelt](#1-videoprogrammer-og-synsfelt)
  - [Begrænset synsfelt i Zoom Workplace](#begr%C3%A6nset-synsfelt-i-zoom-workplace)
- [2. Forbindelse og kompatibilitet](#2-forbindelse-og-kompatibilitet)
  - [Kameraet mangler eller er utilgængeligt](#kameraet-mangler-eller-er-utilg%C3%A6ngeligt)
  - [Knapper mangler eller virker forskelligt i programmer](#knapper-mangler-eller-virker-forskelligt-i-programmer)
- [3. Bevægelse og gemte positioner](#3-bev%C3%A6gelse-og-gemte-positioner)
  - [Bevægelsen stopper, hakker eller vender forkert](#bev%C3%A6gelsen-stopper-hakker-eller-vender-forkert)
  - [Synsfeltet eller en gemt position ser forkert ud](#synsfeltet-eller-en-gemt-position-ser-forkert-ud)

## 1. Videoprogrammer og synsfelt

### Begrænset synsfelt i Zoom Workplace

**Symptomer**\
Kanterne af scenen mangler. Zoom ud og panorering til siden stopper for tidligt. Gemte positioner viser et andet udsnit. **Optimize for video sharing** er slået fra i Zoom. Begrænsningen ses også i det originale Logitech BRIO Control.

**Årsag**\
**Bekræftet i den indberettede sag.** **Efficiency Mode** var slået til i Zoom Workplace. Det oprindelige synsfelt kom tilbage, da tilstanden blev slået fra, og Zoom blev genstartet. Camera Control Studio og positionerne blev ikke ændret. Den tekniske årsag til det smallere synsfelt er ukendt.

**Løsning**

1. Åbn Zoom Workplace. Gå til dit profilbillede → **Settings → Meeting & webinar → Advanced**.
2. Slå **Efficiency Mode** fra under **Performance**. Hvis valget er låst, skal du spørge din Zoom-administrator.
3. Luk Zoom Workplace helt, og åbn det igen. Behold dine gemte positioner, mens du tester.

**Kontrol**\
Kontrollér, at kanterne af scenen er synlige igen. Åbn dine gemte positioner, og sammenlign det levende billede med det udsnit, du forventer.

**Berørte programmer og enheder**\
Windows, Zoom Workplace, Logitech BRIO, Logitech BRIO Control og Camera Control Studio 0.7.0-beta.8. Versioner af Zoom, driver og firmware blev ikke registreret.

**Referencer**\
[Zoom Support](https://support.zoom.com/hc/en/article?id=zm_kb&sysparm_article=KB0080877) · [GitHub #5](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/5)

## 2. Forbindelse og kompatibilitet

### Kameraet mangler eller er utilgængeligt

**Symptomer**\
Kameralisten er tom, det gemte kamera mangler, eller styringen virker ikke efter dvale.

**Årsag**\
**Mulige årsager.** USB-forbindelsen er løs, Windows blokerer adgangen, et andet program bruger kameraet, eller appen afventer bekræftelse af streaming.

**Løsning**

1. Kontrollér USB-kablet. Hvis din opsætning kræver streamingbekræftelse, skal du starte streamingen, før du vælger **Ja**.
2. Kontrollér kameraadgangen i Windows. Test billedet i appen Kamera, og luk den bagefter.
3. Brug en kameraknap til at oprette forbindelsen igen efter dvale. Åbn kildelisten med midterknappen, og vælg kameraet igen, hvis det gemte kamera mangler. Som standard skal du dobbeltklikke på midterknappen for at åbne kameralisten.

![Kamerahjulet med knappen til valg af kamera i midten](../images/screenshots/control%20wheel%20only.png)

**Kontrol**\
Tjek, at det rigtige kamera står nederst, og at en understøttet knap ændrer det levende billede.

**Berørte programmer og enheder**\
Windows og indbyggede kameraer eller USB-kameraer. Samtidig adgang afhænger af driveren.

**Referencer**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Brugervejledninger](SETUP.md)

### Knapper mangler eller virker forskelligt i programmer

**Symptomer**\
Kameraet viser et billede, men nogle knapper mangler. Billedet ændrer sig i ét videoprogram og ser uændret ud i et andet.

**Årsag**\
**Bekræftet adfærd.** Camera Control Studio bruger de funktioner, som Windows-driveren stiller til rådighed. **Mulige årsager.** Driveren, firmwaren eller et andet program kan påvirke styringen eller det billede, du ser.

**Løsning**

1. Vælg det samme fysiske kamera i begge programmer. Sammenlign billedet i ét program ad gangen.
2. Luk andre kameraværktøjer, og åbn det videoprogram, du skal bruge, igen. Test én understøttet funktion.
3. Hvis problemet fortsætter, så tjek Windows Update og producentens supportside for din præcise model. Følg producentens vejledning ved opdatering af driver eller firmware.

**Kontrol**\
Se, hvilke funktioner der ændrer det levende billede i det program, du skal bruge. Notér model samt driver-, firmware- og programversion ved indberetning.

**Berørte programmer og enheder**\
Windows, kameraets driver og firmware samt de programmer, der viser eller styrer billedet.

**Referencer**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K)

## 3. Bevægelse og gemte positioner

### Bevægelsen stopper, hakker eller vender forkert

**Symptomer**\
Panorering, vipning eller zoom stopper tidligt. Et skift går i trin, eller pilene flytter billedet i forkert retning.

**Årsag**\
**Bekræftet adfærd.** Driveren bestemmer rækkevidden og trinstørrelsen. Camera Control Studio kan vende pilenes retning.

Hvis retningen ændrer sig, når du åbner Logitech Camera Settings eller Logitech BRIO Control, kan det andet program have ændret, hvordan kameraet reagerer. Det er rapporteret, at billedet i Zoom stadig vender rigtigt. Det er endnu uklart, hvilken akse der vender, og hvad der udløser ændringen.

**Løsning**

1. Brug den firkantede rammeknap over lupknapperne for at få bredeste tilgængelige synsfelt og mindste zoom. Test pilene og zoom igen.
2. Åbn **Indstillinger → Panorering**, hvis pilene vender forkert. Vend kun den berørte retning.
3. Prøv at holde **Ctrl** nede sammen med en piletast for at flytte langsommere. En længere skiftetid giver ikke mindre drivertrin.
4. Luk Logitech-værktøjet uden for en udsendelse, og test begge akser i det samme videoprogram med samme zoom. Åbn kun ét Logitech-værktøj ad gangen for at se, om ændringen følger med.

**Kontrol**\
Tjek, at pilene flytter billedet i den forventede retning. Sammenlign grænserne i det levende billede.

**Berørte programmer og enheder**\
Kameraer, hvis Windows-drivere understøtter panorering, vipning eller zoom. Grænserne afhænger af model og videotilstand.

**Referencer**\
[GitHub #3](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/3) · [GitHub #4](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/4) · [Brugervejledninger](DAILY-USE.md)

### Synsfeltet eller en gemt position ser forkert ud

**Symptomer**\
Billedet er for tæt. En gemt position viser et uventet udsnit, eller valgene for synsfelt mangler.

**Årsag**\
**Bekræftet adfærd.** Positioner hører til et kamera og en profil. Ældre positioner uden en gemt vinkel beholder den aktuelle vinkel. Nogle drivere tilbyder ingen valgbare vinkler.

**Løsning**

1. Tjek den aktive profil og det valgte kamera. Gendan fuldt billede, og tjek det levende billede.
2. Tjek synsfelt ved opstart under **Video → Synsfelt**. Slå manuel ændring til for at sammenligne vinkler, hvis driveren tilbyder dem.
3. Åbn den gemte position. Hvis synsfeltet kun er smallere med Zoom åbent, så følg afsnittet om Zoom ovenfor. Behold positionen, mens du finder årsagen.

**Kontrol**\
Sammenlign det levende billede med det forventede udsnit. Åbn positionen igen, når det oprindelige synsfelt er tilbage.

**Berørte programmer og enheder**\
Profiler og gemte positioner i Camera Control Studio. Valgbare vinkler afhænger af kamera og driver.

**Referencer**\
[Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K) · [Brugervejledninger](SETUP.md)

---

Har du stadig brug for hjælp? Gennemgå logfilerne, før du deler dem. Skriv kameramodel, programversioner, forsøg og resultater.

[Indberet et problem](ISSUES.md) · [Brugervejledninger](USER-GUIDES.md)
