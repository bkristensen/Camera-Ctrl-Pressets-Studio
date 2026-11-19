[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Ret brugerflade og oversættelser

## Åbn den rigtige fil

Åbn `Camera-Control-Pressets-Studio.sln`. Byg løsningen. Find derefter `CameraControlStudio → Forms → MainForm.cs` i Solution Explorer. Tryk **Shift+F7** for at åbne Windows Forms Designer. Vælg ikke `MainForm.Designer.cs` som designflade. F7 viser koden igen.

Installér Visual Studios .NET desktop-pakke fra `.vsconfig`. Programmet bruger WinForms og .NET Framework 4.8. WPF-designeren bruges ikke.

## Ret udseendet

Brug Properties til at ændre `Dock`, `Margin`, `Padding`, rækkehøjder og kolonnebredder. Brug Document Outline til at vælge paneler inde i andre paneler. Hovedvinduet bruger tabeller og en `SplitContainer`. Undgå faste skærmkoordinater.

Hvert vindue har en offentlig konstruktør uden parametre og arver fra `AppForm`. Layout ligger i `.Designer.cs`. Hændelser ligger i den tilhørende `.cs`-fil. Strukturen er kompileret, men åbning og lagring af hver Form i Visual Studio skal stadig testes på Windows.

Kamerahjulet og de blå knapper tegnes i `Controls/BlueControls.cs`. Designeren kan flytte og ændre størrelsen på dem. Farveovergange, ringe og symboler tegnes i C#-kode. Positionssymboler ligger i `Controls/PresetIcons.cs`. Billedskydere oprettes under kørsel efter kameraets funktioner.

## Ret standardværdier

Ret `SavedData.Defaults` i `Shared/Models.cs` for nye profilers indstillinger. Ret `ProfileLayout` for vinduets startudseende. Eksisterende profiler beholder deres gemte værdier. Opret en ny profil for at afprøve nye standarder. Slet ikke brugerprofiler for at tvinge dem igennem.

## Ret en oversættelse

Ret værdien i `Shared/Locales/en.xml`, `da.xml` eller `de.xml`. Behold `key` uændret. Skriv et og-tegn som `&amp;` i XML. Alle filer skal have de samme nøgler. Gemte kamera- og ikon-ID'er må ikke oversættes.

Kør `scripts/check-translations.ps1`, og byg programmet. Test med `"Camera Control Studio.exe" --lang=da` eller sprogvalget under indstillinger. Genstart efter ændring af det gemte sprogvalg.

## Tilføj et sprog

Kopiér `en.xml` til en fil med den nye sprogkode. Oversæt alle værdier. Tilføj kode og navn i `Shared/UiText.cs`. Tilføj koden i oversættelsestesten, sprogløkken i testprogrammet og skærmbilled-scriptet. Opret en mappe til guiderne, og tilføj sproglinks øverst i alle guider. Tilføj sproget i MSIX-manifest og installer, hvis det understøttes der.

Automatisk valg bruger Windows-sprogets første del. Ukendte sprog falder tilbage til engelsk. Test smalle vinduer og lange tekster, så knapperne ikke skjules.
