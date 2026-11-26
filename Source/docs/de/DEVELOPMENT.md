[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Oberfläche und Übersetzungen bearbeiten

*Geschrieben für Camera Control Studio 1.0.1.*

## Richtige Datei öffnen

Öffnen Sie `Camera-Control-Pressets-Studio.sln`. Erstellen Sie die Projektmappe. Öffnen Sie dann `CameraControlStudio → Forms → MainForm.cs` im Solution Explorer. **Shift+F7** öffnet den Windows Forms Designer. Wählen Sie nicht `MainForm.Designer.cs` als Designfläche. F7 zeigt wieder den Code.

Installieren Sie die .NET-Desktop-Workload von Visual Studio über `.vsconfig`. Die Anwendung nutzt WinForms und .NET Framework 4.8. Der WPF-Designer wird nicht verwendet.

## Layout ändern

Ändern Sie `Dock`, `Margin`, `Padding`, Zeilenhöhen und Spaltenbreiten im Eigenschaftenfenster. Die Dokumentgliederung hilft bei verschachtelten Panels. Das Hauptfenster nutzt Tabellen und einen `SplitContainer`. Vermeiden Sie feste Bildschirmkoordinaten.

Jedes Fenster erbt von `AppForm` und hat einen öffentlichen Konstruktor ohne Parameter. Legen Sie das Layout in `.Designer.cs` und Ereignisse in die zugehörige `.cs`-Datei. Setzen Sie `Icon` im Designer. Prüfen Sie das Layout in Visual Studio und in der laufenden App.

Steuerrad und blaue Tasten werden in `Controls/BlueControls.cs` gezeichnet. Die vertikale Trennlinie und die Zoomsteuerung stehen in `Controls/MainViewControls.cs`. Der Designer kann diese Elemente verschieben und skalieren. Positionssymbole und ihre neutralen Titel stehen in `Assets/PressetIcons.resx` und werden von `Controls/PresetIcons.cs` geladen. Die Regler im Reiter Video sind im Designer bearbeitbar. Das Programm zeigt die vom Kameratreiber unterstützten Regler an.

## Standardwerte ändern

Ändern Sie `SavedData.Defaults` in `Shared/Models.cs` für neue Profile. `ProfileLayout` enthält das anfängliche Fensterlayout. Bestehende Profile behalten ihre gespeicherten Werte. Legen Sie zum Testen ein neues Profil an. Löschen Sie keine Benutzerprofile, um neue Standardwerte zu erzwingen.

## Übersetzung ändern

Ändern Sie den Textwert in der entsprechenden Sprachdatei unter `Shared/Locales`. Lassen Sie `key` unverändert. Schreiben Sie ein kaufmännisches Und als `&amp;`. Alle Dateien müssen dieselben Schlüssel haben. Lassen Sie gespeicherte Kamera- und Symbol-IDs unverändert.

Führen Sie `scripts/check-translations.ps1` aus und erstellen Sie die Anwendung. Testen Sie mit `"Camera Control Studio.exe" --lang=de` oder über die Sprachauswahl. Starten Sie nach einer gespeicherten Sprachänderung neu.

## Sprache hinzufügen

Kopieren Sie `en.xml` in eine Datei mit dem neuen Sprachcode. Übersetzen Sie alle Werte und registrieren Sie Code und Namen in `Shared/UiText.cs`. Die Übersetzungsprüfung und die Sprachtests erkennen die neue Datei und Registrierung automatisch. Tragen Sie die Anwendungssprache im MSIX-Manifest ein.

Die automatische Auswahl nutzt das Windows-Sprachpräfix. Nicht unterstützte Sprachen fallen auf Englisch zurück. Prüfen Sie schmale Fenster und lange Texte, damit keine Bedienelemente verdeckt werden.
