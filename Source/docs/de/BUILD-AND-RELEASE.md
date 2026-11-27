# Erstellen und veröffentlichen Sie Camera Control Studio

*Geschrieben für Camera Control Studio 1.0.1.*

Führen Sie die Befehle aus dem Verzeichnis aus, das `Camera-Control-Pressets-Studio.sln` enthält. Verwenden Sie Windows und PowerShell für das Installationsprogramm und Windows-Tests.

## 1. Bereiten Sie die Veröffentlichung vor

1. Beginnen Sie mit dem für die Veröffentlichung ausgewählten Commit. Schließen Sie alle laufenden Camera Control Studio-Instanzen, bevor Sie ein Upgrade kompilieren oder testen.
2. Installieren Sie das von `global.json` ausgewählte .NET 8 SDK. Installieren Sie Inno Setup 6 oder 7, wenn Sie planen, das Installationsprogramm zu veröffentlichen. Installieren Sie das Windows SDK, wenn Sie Dateien mit SignTool signieren oder ein MSIX mit MakeAppx erstellen möchten.
3. Überprüfen Sie die Voraussetzungen mit `./scripts/setup-dev.ps1 -CheckOnly`. Für eine neue Workstation stellt `./scripts/setup-dev.ps1` die angehefteten Pakete wieder her und führt den tragbaren Build aus.
4. Legen Sie die Release-Version in `Directory.Build.props` fest und aktualisieren Sie `CHANGELOG.md`. Das Installationsskript liest die Standardversion aus den Build-Eigenschaften. Halten Sie die vierteilige Store-Version getrennt, wenn Sie einen MSIX vorbereiten.

## 2. Erstellen Sie die GitHub-Pakete

Wählen Sie einen Befehl. Der Installer-Build umfasst den portablen Build.

| Was soll gebaut werden? | Befehl | Ausgabe |
| --- | --- | --- |
| Tragbares ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| Tragbares ZIP- und Inno-Installationsprogramm | `./scripts/build-installer.ps1` | Tragbares ZIP plus `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` prüft Übersetzungen, erstellt Releases mit gesperrten Abhängigkeiten, führt Tests durch und erstellt ZIP. Suchen Sie die ausführbare Datei in `CameraControlStudio/bin/Release/net48` und die bereitgestellten Dateien in `artifacts/portable/Camera-Control-Studio`. Veröffentlichen Sie den gesamten ZIP. Die App benötigt ihre Konfiguration, Bibliotheken und Anleitungen.

Um ein Codesignaturzertifikat einer Einzelperson oder einer Organisation zu verwenden, das im Windows-Zertifikatspeicher des aktuellen Benutzers verfügbar ist, übergeben Sie beim Erstellen dessen Fingerabdruck und den Pfad an SignTool. Der Installationsbefehl signiert die Anwendung vor dem Packen und signiert das abgeschlossene Installationsprogramm mit einem Zeitstempel.

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

Geben Sie für ZIP allein die gleichen Parameter wie `build.ps1` ein. Befolgen Sie für gesponserte Signaturen den genehmigten [SignPath-Workflow](SIGNPATH.md). Markieren Sie nicht signierte Downloads deutlich.

## 3. Überprüfen Sie die Pakete unter Windows

1. Extrahieren Sie ZIP in ein neues Verzeichnis. Starten Sie die App von dort aus und öffnen Sie **?** mit geschlossenen und geöffneten Einstellungen. Wenn [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) installiert ist, überprüfen Sie die Anleitungsnavigation und drucken Sie nach PDF, um Titel, Druckdatum und -uhrzeit, installierte Version, Projektadresse und Seitenzahlen zu überprüfen. Überprüfen Sie beim Profilwechsel die App-Version, das Startverhalten, die Voreinstellungen, die Kamerasteuerung und die Sprachauswahl.
2. Wenn Sie planen, das Installationsprogramm zu veröffentlichen, installieren Sie es auf einem sauberen Windows-Testkonto. Überprüfen Sie die Verknüpfungen der Startmenü-App und des Benutzerhandbuchs. Überprüfen Sie, ob alle 12 Anleitungen vorhanden sind, ob bei einem Upgrade vorhandene Profile erhalten bleiben und ob die Deinstallation abgeschlossen ist.
3. Arbeiten Sie die Release-Checkliste [](RELEASE-CHECKLIST.md) durch. Führen Sie für signierte Builds die SignTool-Überprüfung für die ausführbare Datei und das Installationsprogramm der App durch. Generieren Sie Hashes erst, nachdem das Signieren und Neupacken abgeschlossen ist.

## 4. Bereitstellen und Hashen der GitHub-Downloads

Ersetzen Sie die Version in diesem Beispiel für nachfolgende Versionen. Lassen Sie die Installationszeilen für eine reine ZIP-Version weg.

```powershell
$version = '1.0.0'
$release = "artifacts/releases/$version"
New-Item -ItemType Directory -Path $release -Force | Out-Null

$zip = "Camera-Control-Studio-$version-portable.zip"
$installer = "CamCtrlStudio-Setup-$version.exe"
Copy-Item 'artifacts/Camera-Control-Studio-portable.zip' (Join-Path $release $zip)
Copy-Item "artifacts/installers/$installer" $release
@($zip, $installer) | ForEach-Object {
    $hash = Get-FileHash -LiteralPath (Join-Path $release $_) -Algorithm SHA256
    '{0}  {1}' -f $hash.Hash.ToLowerInvariant(), $_
} | Set-Content -LiteralPath (Join-Path $release 'SHA256SUMS.txt') -Encoding ascii
```

Der versionierte Ordner enthält das endgültige ZIP-, Installationsprogramm- und SHA-256-Manifest. Die Verzeichnisse `bin` und portable Staging sind Build-Eingaben für diesen Schritt.

## 5. Veröffentlichen Sie auf GitHub

1. Aktualisieren Sie den Untertitel der Version in jedem Handbuch, das Sie für diese Version überprüfen. Halten Sie die Übersetzungen synchron. Übertragen Sie die Release-Version, das Änderungsprotokoll, die Anleitungen und Quelländerungen. Erstellen Sie in [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases) einen Entwurf und verwenden Sie ein Tag wie `v1.0.0` für den getesteten Commit.
2. Kopieren Sie den passenden Changelog-Eintrag in die Versionshinweise, geben Sie an, ob die Downloads signiert sind, und wählen Sie die Vorabversionsoption nur für Vorschau-Builds aus.
3. Hängen Sie den versionierten ZIP, das Installationsprogramm, falls erstellt, und `SHA256SUMS.txt` von `artifacts/releases/<version>/` an. Überprüfen Sie die Dateinamen, laden Sie die fertigen Dateien herunter, überprüfen Sie sie und veröffentlichen Sie sie dann.

## Optionale Build-Pfade

| Zweck | Skript | Ausgabe |
| --- | --- | --- |
| Dokumentations-Screenshots mit Beispieldaten | `./scripts/capture-docs.ps1` | PNGs in `docs/images/screenshots/<version>/<language>/` für alle 12 Benutzerhandbücher |
| Docker-Build- und Logiktests | `./scripts/build-docker.ps1` | Tragbares ZIP in `artifacts/docker/` |
| Kandidat für die Einreichung im Microsoft Store | `./scripts/build-msix.ps1` mit Ihrer Partner Center-Identität, Ihrem Herausgeber und Ihrer Paketversion | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

Der Store-Build erfordert die echte Partner Center-Identität und folgt der separaten [MSIX- und Store-Checkliste](MSIX-AND-STORE.md). Die Ausgabe ist ein nicht signierter Einreichungskandidat. `Install.ps1` ist das optionale benutzerspezifische Installationsprogramm in einem extrahierten tragbaren ZIP.
