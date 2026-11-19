# Einstieg mit Git oder Quellcode-ZIP

## Visual Studio

1. Repository klonen oder das vollständige Quellcode-ZIP entpacken. Nicht das alte
   CamPressetsControlDemo öffnen; es war ein separater WPF-Designprototyp.
2. Visual Studio 2022 mindestens auf 17.11 aktualisieren oder eine neuere unterstützte
   Version verwenden. Im Visual Studio Installer > More/Mehr > Import configuration
   die `.vsconfig` im Stammordner importieren. Sie wählt .NET-Desktopentwicklung und
   .NET Framework 4.8 SDK/Targeting Pack.
3. .NET SDK 8.0.4xx oder ein neueres 8.0-Featureband installieren. Die Framework-Runtime
   allein reicht nicht. `.vsconfig` garantiert nicht dieses konkrete moderne SDK.
4. **CamPressetsStudio.sln** öffnen und NuGet-Wiederherstellung erlauben.
   **BrioPresets** als Startprojekt festlegen, bauen und unter Windows mit F5 starten.

Die Anwendung verwendet **Windows Forms mit net48, nicht WPF**. UIAutomationClient
und WindowsBase machen sie nicht zu einem WPF-Projekt. Kein `dotnet workload install wpf`
ausführen. Die genaue Fehlerursache benötigt Fehlercode und vollständige Meldung.

## Einrichtungsskript

Im Projektordner ausführen:

```powershell
./scripts/setup-dev.ps1
```

Das Skript prüft das SDK, meldet fehlende Visual-Studio-Desktopwerkzeuge, stellt
festgelegte NuGet-Pakete wieder her, baut und führt Logiktests aus. Es installiert
Visual Studio nicht stillschweigend und ändert keine Sicherheits- oder Skriptrichtlinien.

Um die Installation des .NET 8 SDK per WinGet ausdrücklich anzufordern:

```powershell
./scripts/setup-dev.ps1 -InstallSdk
```

Danach Terminal neu öffnen und den normalen Befehl ausführen. `-CheckOnly` prüft nur.
Falls unsignierte Skripte blockiert werden, diese manuellen Befehle nutzen statt
Schutzfunktionen global auszuschalten:

```powershell
dotnet --version
dotnet restore CamPressetsStudio.sln --locked-mode
dotnet build CamPressetsStudio.sln -c Release --no-restore
```

## Abhängigkeitsdateien

| Datei | Zweck |
| --- | --- |
| .vsconfig | Desktop-Workload und Framework-Entwicklungskomponenten |
| global.json | Stabiles .NET 8 SDK, mindestens 8.0.400, neuere 8.0-Featurebands erlaubt |
| NuGet.Config | Öffentliche Paketquelle ohne Zugangsdaten |
| .csproj | Framework, Ressourcen und Paketverweise |
| packages.lock.json je Projekt | Aufgelöste Paketversionen und Prüfsummen |
| CamPressetsStudio.sln | App und drei Testprojekte in einer Lösung |

bin/obj nicht zwischen Rechnern kopieren. Python ist nicht erforderlich. Inno Setup
ist nur für klassische Installer, Windows SDK MakeAppx für MSIX und Docker für
Container-Builds nötig, nicht für normale Entwicklung.

## Fehlerbehebung

- Fehlendes SDK / NETSDK1045: `dotnet --list-sdks` prüfen, SDK installieren und Visual Studio
  aktualisieren. Ein allein installiertes .NET 9/10 erfüllt diese SDK-Festlegung nicht.
- MSB3644: .vsconfig importieren und NuGet wiederherstellen. Das Projekt bezieht
  Framework-Referenzassemblies zusätzlich als NuGet-Buildabhängigkeit.
- NU1301: Netzwerk/Proxy und https://api.nuget.org/v3/index.json prüfen.
- WindowsDesktop SDK fehlt: vollständige Microsoft-SDK-Installation und Desktop-Workload
  prüfen. Sicherstellen, dass diese Lösung statt des alten WPF-Demos geöffnet ist.
- Für Hilfe ersten Fehlercode, vollständige Meldung, Visual-Studio-Version und
  `dotnet --info` bereitstellen; unnötige private Daten entfernen.

Bei beabsichtigten Paketupdates Versionen ändern und `dotnet restore
CamPressetsStudio.sln --force-evaluate` ausführen. Geänderte Lockdateien prüfen,
committen und Tests ausführen. Lockdateien nicht bloß zum Verbergen eines Fehlers löschen.

Das Windows-Skript und der IDE-Ablauf wurden hier noch nicht auf Windows ausgeführt.
