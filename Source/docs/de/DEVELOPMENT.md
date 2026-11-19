[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Oberfläche und Übersetzungen bearbeiten

## Richtige Datei öffnen

Öffnen Sie `Camera-Control-Pressets-Studio.sln`. Erstellen Sie die Projektmappe. Öffnen Sie dann `CameraControlStudio → Forms → MainForm.cs` im Solution Explorer. **Shift+F7** öffnet den Windows Forms Designer. Wählen Sie nicht `MainForm.Designer.cs` als Designfläche. F7 zeigt wieder den Code.

Installieren Sie die .NET-Desktop-Workload von Visual Studio über `.vsconfig`. Die Anwendung nutzt WinForms und .NET Framework 4.8. Der WPF-Designer wird nicht verwendet.

## Layout ändern

Ändern Sie `Dock`, `Margin`, `Padding`, Zeilenhöhen und Spaltenbreiten im Eigenschaftenfenster. Die Dokumentgliederung hilft bei verschachtelten Panels. Das Hauptfenster nutzt Tabellen und einen `SplitContainer`. Vermeiden Sie feste Bildschirmkoordinaten.

Jedes Fenster besitzt einen öffentlichen Konstruktor ohne Parameter und erbt von `AppForm`. Das Layout liegt in `.Designer.cs`, die Ereignisbehandlung in der zugehörigen `.cs`-Datei. Die Struktur wurde kompiliert. Das Öffnen und Speichern jeder Form im Visual-Studio-Designer muss noch unter Windows geprüft werden.

Steuerrad und blaue Tasten werden in `Controls/BlueControls.cs` gezeichnet. Der Designer kann diese Elemente verschieben und skalieren. Verläufe, Ringe und Symbole werden in C# gezeichnet. Positionssymbole stehen in `Controls/PresetIcons.cs`. Bildregler entstehen zur Laufzeit anhand der Treiberfunktionen.

## Standardwerte ändern

Ändern Sie `SavedData.Defaults` in `Shared/Models.cs` für neue Profile. `ProfileLayout` enthält das anfängliche Fensterlayout. Bestehende Profile behalten ihre gespeicherten Werte. Legen Sie zum Testen ein neues Profil an. Löschen Sie keine Benutzerprofile, um neue Standardwerte zu erzwingen.

## Übersetzung ändern

Ändern Sie den Textwert in `Shared/Locales/en.xml`, `da.xml` oder `de.xml`. Lassen Sie `key` unverändert. Schreiben Sie ein kaufmännisches Und als `&amp;`. Alle Dateien müssen dieselben Schlüssel haben. Gespeicherte Kamera- und Symbol-IDs dürfen nicht übersetzt werden.

Führen Sie `scripts/check-translations.ps1` aus und erstellen Sie die Anwendung. Testen Sie mit `"Camera Control Studio.exe" --lang=de` oder über die Sprachauswahl. Starten Sie nach einer gespeicherten Sprachänderung neu.

## Sprache hinzufügen

Kopieren Sie `en.xml` in eine Datei mit dem neuen Sprachcode. Übersetzen Sie alle Werte. Ergänzen Sie Code und Namen in `Shared/UiText.cs`. Ergänzen Sie den Code im Übersetzungsprüfskript, in der Sprachschleife des Testprogramms und im Screenshot-Skript. Erstellen Sie den passenden Ordner für Anleitungen und ergänzen Sie die Sprachlinks oben in allen Anleitungen. Ergänzen Sie die Sprache im MSIX-Manifest und Installer, soweit dort unterstützt.

Die automatische Auswahl nutzt das Windows-Sprachpräfix. Nicht unterstützte Sprachen fallen auf Englisch zurück. Prüfen Sie schmale Fenster und lange Texte, damit keine Bedienelemente verdeckt werden.
