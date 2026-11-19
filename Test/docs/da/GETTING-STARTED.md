# Kom i gang fra Git eller kildekode-ZIP

## Visual Studio

1. Hent Git-projektet eller udpak hele kildekodepakken. Åbn ikke det gamle
   CamPressetsControlDemo; det var en separat WPF-designprototype.
2. Opdatér Visual Studio 2022 til mindst 17.11 eller brug en nyere understøttet
   udgave. Åbn Visual Studio Installer > More/Mere > Import configuration, og vælg
   `.vsconfig` i projektets rod. Den vælger .NET-desktopudvikling og .NET Framework 4.8 SDK/targeting pack.
3. Installér .NET SDK 8.0.4xx eller et nyere 8.0-featureband. Den almindelige .NET
   Framework-runtime er ikke nok. `.vsconfig` sikrer ikke alene netop dette moderne SDK.
4. Åbn **CamPressetsStudio.sln**. Tillad NuGet-gendannelse. Højreklik på **BrioPresets**,
   vælg det som startprojekt, byg løsningen, og start med F5 på Windows.

Appen bruger **Windows Forms og net48, ikke WPF**. Referencerne UIAutomationClient
og WindowsBase gør den ikke til et WPF-projekt. Du skal ikke køre en kommando som
`dotnet workload install wpf`. Den præcise årsag til din fejl kræver fejlkode og hele fejlteksten.

## Opsætningsscript

Kør fra projektmappen:

```powershell
./scripts/setup-dev.ps1
```

Scriptet kontrollerer SDK, oplyser om manglende Visual Studio-desktopværktøjer,
henter låste NuGet-afhængigheder samt bygger og kører logiktests. Det installerer
ikke Visual Studio automatisk og ændrer ikke Windows-sikkerhed eller scriptpolitik.

Vil du specifikt installere .NET 8 SDK via WinGet:

```powershell
./scripts/setup-dev.ps1 -InstallSdk
```

Åbn terminalen igen efter installation, og kør den almindelige kommando. `-CheckOnly`
kontrollerer kun. Hvis Windows blokerer usignerede scripts, kan disse manuelle
kommandoer bruges; slå ikke generelt scriptbeskyttelsen fra:

```powershell
dotnet --version
dotnet restore CamPressetsStudio.sln --locked-mode
dotnet build CamPressetsStudio.sln -c Release --no-restore
```

## Hvilke filer styrer afhængighederne?

| Fil | Ansvar |
| --- | --- |
| .vsconfig | Visual Studio-desktopværktøjer og Framework-komponenter |
| global.json | Stabilt .NET 8 SDK, mindst 8.0.400; tillader nyere 8.0-featurebands |
| NuGet.Config | Offentlig NuGet-kilde uden adgangskoder |
| Projektets .csproj | Framework, ressourcer og pakkeafhængigheder |
| packages.lock.json i hvert projekt | Fastlagte pakkeversioner og kontrolsummer |
| CamPressetsStudio.sln | App og tre testprojekter samlet i én løsning |

Kopiér ikke bin/obj mellem pc'er. Python kræves ikke. Inno Setup er kun nødvendigt
for klassisk installationspakning, MakeAppx fra Windows SDK for MSIX og Docker for
containerbygning. De er ikke krav for almindelig udvikling af appen.

## Hvis det stadig fejler

- Manglende SDK / NETSDK1045: se `dotnet --list-sdks`, installér SDK og opdatér Visual Studio.
  .NET 9/10 alene opfylder ikke det valgte SDK-krav.
- MSB3644: importér .vsconfig, og gendan NuGet-pakkerne. Framework-referencefiler
  hentes også af projektets NuGet-afhængighed.
- NU1301: kontrollér adgang til https://api.nuget.org/v3/index.json og eventuel proxy.
- Manglende WindowsDesktop SDK: kontrollér komplet Microsoft SDK og desktop-workload.
  Kontrollér også, at den rigtige løsning er åbnet, ikke WPF-demoen.
- Send den første fejlkode og fulde fejltekst, Visual Studio-version samt `dotnet --info`.

Ved bevidst pakkeopdatering ændres versionsnumre, derefter køres `dotnet restore
CamPressetsStudio.sln --force-evaluate`. Gennemgå og commit ændrede låsefiler, og kør
tests. Slet ikke låsefiler blot for at skjule en fejl.

Windows-scriptet og Visual Studio-forløbet er endnu ikke afprøvet på en Windows-pc.
