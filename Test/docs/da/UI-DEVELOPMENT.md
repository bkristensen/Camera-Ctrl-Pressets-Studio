> 0.6.4: [Updated defaults and camera startup / Opdateret kamerastart / Aktualisierter Kamerastart](DEFAULTS-AND-STREAMING.md). This supersedes earlier Zoom-specific startup instructions.

# Arbejd videre med UI og grafik

## Kom i gang
1. Åbn `CamPressetsStudio.sln` i Visual Studio på Windows. Vælg **BrioPresets** som startprojekt.
2. Åbn `BrioPresets/Program.cs`, og find `MainForm()`. Her opbygges hovedvinduet.
3. Ret én værdi, og tryk **F5** for at se resultatet. Stop fejlfinding før næste ændring.
4. Prøv et lille og et maksimeret vindue med indstillinger både åbne og lukkede.

## Her ligger designet
| Fil | Indhold |
| --- | --- |
| `BrioPresets/Program.cs` | Layout, kolonner, rækker, presetliste og hændelser |
| `BrioPresets/BlueControls.cs` | Farver i `BlueTheme`, knapper, paneler og navigeringshjulet `PanWheel` |
| `BrioPresets/PresetIcons.cs` | De 30 vektorsymboler og dialogen `PresetEditor` til navn og symbol |
| `SharedUi/ProfileControls.cs` | Profilvælger og profilknapper |
| `SharedUi/Locales/en.xml`, `da.xml`, `de.xml` | Tekster på de tre sprog |
| `SharedProfiles/Models.cs` | Gemte data, herunder presetfeltet `IconId` |

## Findes designeren stadig?
Ja. Den hedder **Windows Forms Designer**. På en almindelig Windows Form vælger man **View Designer** (Shift+F7), tilføjer kontroller fra Toolbox og ændrer egenskaber med **F4**. Visual Studio gemmer layoutet i en `.Designer.cs`-fil.

Dette projekts hovedvindue er dog opbygget direkte i C# med løkker og hændelser. Det har hverken en designerstyret `MainForm.Designer.cs` eller XAML. Det kan derfor ikke pålideligt redigeres med træk-og-slip i sin nuværende form. Det skyldes projektets opbygning, ikke et manglende WPF SDK. Programmet bruger **Windows Forms og .NET Framework 4.8**.

Den nemmeste vej nu er at rette filerne ovenfor og starte programmet for at se resultatet. Du kan eksperimentere med designeren i en separat formular via **Project > Add > Windows Form**. En senere omlægning af hovedvinduet kræver, at layoutet flyttes til en designerstyret partial class, mens kamerakode og hændelser holdes adskilt. Det hjælper ikke blot at omdøbe Program.cs til Designer.cs. En designer må aldrig åbne kameraet fra en constructor.

## Det er nyttigt at justere
- `Dock = DockStyle.Fill` fylder en celle. `Padding` er indvendig afstand, og `Margin` er udvendig afstand.
- `ColumnStyles` og `RowStyles` bestemmer fordelingen i `TableLayoutPanel`. Procenter fordeler den plads, som er tilbage efter faste størrelser.
- Hjulet beregnes ud fra `Math.Min(Width, Height)`, så det forbliver rundt. Tegning og registrering af klik skal bruge samme centrum og skala.
- Specialtegnede kontroller skal bruge `ResizeRedraw` og double buffering. Hele baggrunden skal males, så gamle streger ikke bliver stående.
- Undgå for smalle faste tekstfelter. Afprøv alle tre sprog og Windows-skalering på 100 %, 125 %, 150 % og 200 %.

## Symboler og betjening
**Gem aktuel position** åbner valg af navn og symbol. Blyanten ændrer begge dele uden at ændre den gemte kameraposition. Dobbeltklik på navn/symbol, brug Enter eller tryk på play for at hente en position. Dobbeltklik på blyantområdet aktiverer ikke kameraet. Gamle presets uden symbol får et kameraikon.

Symbolerne er rene, kodebaserede fortolkninger af ikonarket. De er ikke udklippede PNG-filer. Et skærm- eller lydsymbol er kun en etiket; det starter ikke skærmdeling eller ændrer lyden.

Et nyt symbol kræver et stabilt ID i `PresetIcons.Ids`, en tegning i `Draw` og en `Icon: ID`-tekst i alle tre sprogfiler. Tegningerne bruger et 64×64-koordinatsystem med ens stregtykkelse. Omdøb ikke eksisterende ID'er uden også at migrere gamle presets.

## Tjek på din Windows-PC
- Træk gentagne gange i vinduets kanter, maksimer og gendan. Se efter gamle hjulsegmenter, sorte knaphjørner og afklippede rammer.
- Åbn indstillinger ved mindste vinduesstørrelse. Hjulet skal være helt synligt, og Zoom skal stå på én linje. Rul gennem alle indstillinger.
- Gem to forskellige symboler, genstart og skift profil. Symbolerne skal bevares.
- Rediger kun symbolet. Navn og kameraposition skal være uændrede. Annuller må ikke gemme.
- Test dobbeltklik, play, Enter, blyant, tomt listeområde og erstatning af et eksisterende navn.
- Behold kameraspærren aktiveret. Start de nødvendige programmer og selve Zoom-mødet, før du bekræfter, at kameraet må tilsluttes.

Koden er kompileret, og logiktestene er kørt. Windows-grafik, DPI og kamera skal stadig afprøves på en Windows-PC.

## Microsofts vejledninger
- [Windows Forms Designer](https://learn.microsoft.com/en-us/visualstudio/designers/windows-forms-designer-overview)
- [Designerens funktioner](https://learn.microsoft.com/en-us/visualstudio/designers/walkthrough-windows-forms-designer)
