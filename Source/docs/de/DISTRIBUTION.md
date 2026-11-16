# Verteilung, Store und SignPath

## Vertriebsweg wählen

| Möglichkeit | Verwendung | Wichtig |
| --- | --- | --- |
| Portables ZIP auf GitHub | Frühe Tests | Unsignierte Dateien können Windows-Warnungen auslösen |
| Inno Setup | Klassische Windows-Installation | Unter Windows bauen; Signierung ist ein eigener Schritt |
| Microsoft Store mit MSIX | Öffentliche Freigabe und Updates | Microsoft signiert nach Annahme |
| Store mit privater Zielgruppe | Ausgewählte Tester | Persönliche Microsoft-Konten müssen zugeordnet sein |
| Öffentlicher Store über Direktlink | Verteilung ohne normale Auffindbarkeit | Kein privater Zugriffsschutz |
| Store package flights | Unterschiedliche Updatepakete testen | Gruppen in Partner Center konfigurieren |
| MSIX direkt außerhalb des Store | Sideload/verwaltete Installation | Geeignete Signierung und Vertrauen beim Empfänger erforderlich |

Ein öffentliches Store-Produkt kann später nicht auf private Zielgruppe zurückgestellt
werden. Bei Bedarf zuerst privat testen. Beim Store-Vertrieb über MSI/EXE muss der
Herausgeber selbst vertrauenswürdig signieren; dies ist nicht die kostenlose MSIX-Signierung.

## MSIX-Checkliste

- [ ] Entwicklerkonto über https://storedeveloper.microsoft.com/ erstellen und Identität bestätigen.
- [ ] Verfügbaren Namen reservieren; Identity Name, Publisher CN und Publisher Display Name übernehmen.
- [ ] .NET SDK 8+ und Windows SDK mit MakeAppx.exe unter Windows installieren.
- [ ] Folgenden Befehl mit den echten Werten statt Platzhaltern ausführen.
- [ ] Paket, Datenmigration, Kamera, Startsperre und externe Programme unter Windows testen.

```powershell
./scripts/build-msix.ps1 -IdentityName 'IHRE.IDENTITY' -Publisher 'CN=IHRE-PUBLISHER-ID' -PublisherDisplayName 'Ihr Herausgebername' -MakeAppxPath 'C:\Pfad\MakeAppx.exe'
```

Das Skript baut und testet, erstellt x64-Binärdateien und Symbole und erzeugt einen
unsignierten MSIX-Kandidaten. Dies ist kein vertrauenswürdiges Sideload-Installationspaket.
Die Paketierung benötigt Windows und wurde hier noch nicht ausgeführt. Die Version
hat vier Teile und endet mit 0; das Manifest setzt mindestens Windows Build 19041 voraus.

## Vor der Store-Einreichung

- [ ] Echte App-Screenshots ohne private Meetinginformationen verwenden.
- [ ] Tatsächliche Supportadresse und geprüfte öffentliche Datenschutz-URL angeben.
- [ ] Zielgruppe, Märkte, Sprachen, Kategorie und Altersbewertung korrekt ausfüllen.
- [ ] Full Trust erklären: Kameratreibersteuerung und gewählte Programmstarts, keine Administratoranforderung.
- [ ] Zoom-Sperre und Hardwarevoraussetzungen für Prüfer erklären; Zertifizierung vor öffentlicher Freigabe abschließen.

MSIX kann Benutzerdaten umleiten. Profilimport, Updates und Deinstallation testen.
Nicht davon ausgehen, dass Store- und portable Edition automatisch dieselben Dateien verwenden.

## SignPath

SignPath Foundation kann genehmigten Open-Source-Projekten kostenlose Signierung
anbieten. Eine Bewerbung ist nötig; eine Zusage ist nicht garantiert. Eine Signatur
beweist weder Fehlerfreiheit noch das Ausbleiben aller SmartScreen-Warnungen.

Ein öffentliches, gepflegtes Projekt, dokumentierte Releases und nachvollziehbare
Builds vorbereiten. Anforderungen an Lizenz, MFA, Review, Freigabeverantwortliche,
Datenschutz und Deinstallation prüfen. Signierte Releases benötigen eine Genehmigung
nach den Regeln des Anbieters. Das vorhandene Skript für lokale Zertifikate ist
keine SignPath-Integration.

Aktueller Projektstatus: keine SignPath-Bewerbung, Genehmigung oder Signierung.
Keine Anbieterfreigabe oder entsprechende Logos vortäuschen. Siehe https://signpath.org/terms
und https://signpath.org/apply. MSIX-Vorbereitung bedeutet ebenfalls keine vorhandene Store-Veröffentlichung.
