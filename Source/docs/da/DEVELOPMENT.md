[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Ret brugerflade og oversættelser

*Skrevet til Camera Control Studio 1.0.1.*

## Åbn den rigtige fil

Åbn `Camera-Control-Pressets-Studio.sln`. Byg løsningen. Find derefter `CameraControlStudio → Forms → MainForm.cs` i Solution Explorer. Tryk **Shift+F7** for at åbne Windows Forms Designer. Vælg ikke `MainForm.Designer.cs` som designflade. F7 viser koden igen.

Installér Visual Studios .NET desktop-pakke fra `.vsconfig`. Programmet bruger WinForms og .NET Framework 4.8. WPF-designeren bruges ikke.

## Ret udseendet

Brug Properties til at ændre `Dock`, `Margin`, `Padding`, rækkehøjder og kolonnebredder. Brug Document Outline til at vælge paneler inde i andre paneler. Hovedvinduet bruger tabeller og en `SplitContainer`. Undgå faste skærmkoordinater.

Hvert vindue arver fra `AppForm` og har en offentlig konstruktør uden parametre. Sæt layoutet i `.Designer.cs` og hændelserne i den tilhørende `.cs`-fil. Sæt vinduets `Icon` i designeren. Tjek layoutet i Visual Studio og i det kørende program.

Kamerahjulet og de blå knapper tegnes i `Controls/BlueControls.cs`. Den lodrette skillelinje og zoomstyringen ligger i `Controls/MainViewControls.cs`. Designeren kan flytte og ændre størrelsen på dem. Positionsikoner og neutrale titler ligger i `Assets/PressetIcons.resx` og indlæses af `Controls/PresetIcons.cs`. Skyderne på fanen Video ligger i designeren. Programmet viser de skydere, som kameradriveren understøtter.

## Ret standardværdier

Ret `SavedData.Defaults` i `Shared/Models.cs` for nye profilers indstillinger. Ret `ProfileLayout` for vinduets startudseende. Eksisterende profiler beholder deres gemte værdier. Opret en ny profil for at afprøve nye standarder. Slet ikke brugerprofiler for at tvinge dem igennem.

## Ret en oversættelse

Ret værdien i den relevante sprogfil under `Shared/Locales`. Behold `key` uændret. Skriv et og-tegn som `&amp;` i XML. Alle filer skal have de samme nøgler. Behold gemte kamera- og ikon-id'er.

Kør `scripts/check-translations.ps1`, og byg programmet. Test med `"Camera Control Studio.exe" --lang=da` eller sprogvalget under indstillinger. Genstart efter ændring af det gemte sprogvalg.

## Tilføj et sprog

Kopiér `en.xml` til en fil med den nye sprogkode. Oversæt alle værdier, og registrér kode og navn i `Shared/UiText.cs`. Oversættelseskontrollen og sprogtestene finder automatisk den nye fil og registrering. Angiv appens sprog i MSIX-manifestet.

Automatisk valg bruger Windows-sprogets første del. Ukendte sprog falder tilbage til engelsk. Test smalle vinduer og lange tekster, så knapperne ikke skjules.
