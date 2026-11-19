> 0.6.4: [Updated defaults and camera startup / Opdateret kamerastart / Aktualisierter Kamerastart](DEFAULTS-AND-STREAMING.md). This supersedes earlier Zoom-specific startup instructions.

# Oberfläche und Grafik bearbeiten

## Einstieg
1. `CamPressetsStudio.sln` in Visual Studio unter Windows öffnen. **BrioPresets** als Startprojekt auswählen.
2. In `BrioPresets/Program.cs` nach `MainForm()` suchen. Dieser Konstruktor erstellt das Hauptfenster.
3. Einen Layoutwert ändern und mit **F5** starten. Vor der nächsten Änderung das Debugging beenden.
4. Kleine und maximierte Fenster mit geöffneten und geschlossenen Einstellungen prüfen.

## Dateien
| Datei | Aufgabe |
| --- | --- |
| `BrioPresets/Program.cs` | Hauptlayout, Zeilen, Spalten, Preset-Liste und Ereignisse |
| `BrioPresets/BlueControls.cs` | Farben in `BlueTheme`, Schaltflächen, Rahmen und `PanWheel` |
| `BrioPresets/PresetIcons.cs` | 30 Vektorsymbole und Dialog `PresetEditor` |
| `SharedUi/ProfileControls.cs` | Profilauswahl und Schaltflächen |
| `SharedUi/Locales/en.xml`, `da.xml`, `de.xml` | Übersetzungen |
| `SharedProfiles/Models.cs` | Gespeicherte Daten und das stabile Feld `IconId` |

## Visueller Designer
Visual Studio bietet weiterhin den **Windows Forms Designer**: **View Designer** (Shift+F7), Toolbox und Eigenschaftenfenster (F4). Normalerweise schreibt er das Layout in eine `.Designer.cs`-Datei.

Dieses Hauptfenster wird jedoch direkt in C# aufgebaut, einschließlich Schleifen und Ereignissen. Es enthält keine vom Designer verwaltete `MainForm.Designer.cs` und kein XAML. Das vorhandene Layout lässt sich deshalb nicht zuverlässig per Drag-and-drop bearbeiten. Es fehlt kein WPF SDK: Die App nutzt **Windows Forms und .NET Framework 4.8**.

Der einfachste Weg ist derzeit, die genannten Dateien zu bearbeiten und die App zu starten. Für visuelle Versuche eine separate Form über **Project > Add > Windows Form** hinzufügen. Eine spätere Umstellung erfordert eine Designer-Partial-Class für das Layout und getrennten Kamera- und Ereigniscode. Das Umbenennen von Program.cs reicht nicht. Ein Designer-Konstruktor darf niemals die Kamera öffnen.

## Layout und Skalierung
- `DockStyle.Fill` füllt eine Zelle. `Padding` ist Innenabstand, `Margin` Außenabstand.
- `ColumnStyles` und `RowStyles` bestimmen feste Größen und prozentuale Anteile des verbleibenden Platzes.
- Das Rad verwendet `Math.Min(Width, Height)`. Zeichnung und Trefferprüfung müssen denselben Mittelpunkt und Maßstab nutzen.
- Benutzerdefinierte Zeichnungen benötigen `ResizeRedraw`, Double Buffering und einen vollständig gezeichneten Hintergrund.
- Alle drei Sprachen sowie Windows-Skalierung mit 100 %, 125 %, 150 % und 200 % prüfen.

## Preset-Symbole
Beim Speichern einer Position werden Name und Symbol gewählt. Der Stift bearbeitet beide, ohne die gespeicherte Kameraposition zu verändern. Doppelklick auf Name/Symbol, Enter oder Play ruft das Preset auf. Doppelklick auf den Stiftbereich ruft keine Position auf. Alte Presets ohne Symbol zeigen eine Kamera.

Die Symbole sind saubere, im Code gezeichnete Interpretationen der Vorlage, keine ausgeschnittenen PNGs. Bildschirm- und Tonsymbole sind nur Beschriftungen: Sie starten keine Freigabe und ändern keinen Ton.

Neue Symbole brauchen eine stabile Kennung in `PresetIcons.Ids`, einen Zeichenfall in `Draw` und eine Übersetzung `Icon: ID` in jeder Sprachdatei. Die Zeichenfläche ist 64×64 mit einheitlicher Strichstärke. Bestehende Kennungen nur zusammen mit einer Migration ändern.

## Windows-Prüfliste
- Wiederholt vergrößern, verkleinern, maximieren und wiederherstellen: keine alten Radsegmente, schwarzen Ecken oder abgeschnittenen Rahmen.
- Einstellungen bei kleinster Fenstergröße öffnen und vollständig durchscrollen. Rad vollständig, Zoom einzeilig.
- Verschiedene Symbole speichern, neu starten und Profile wechseln. Symbole müssen erhalten bleiben.
- Nur das Symbol ändern; Name und Kameraposition müssen unverändert bleiben. Abbrechen darf nichts speichern.
- Doppelklick, Play, Enter, Stift, leeren Listenbereich und Überschreiben eines vorhandenen Namens prüfen.
- Kamerasperre eingeschaltet lassen. Programme und das tatsächliche Zoom-Meeting starten, bevor die Kamerafreigabe bestätigt wird.

Build und Logiktests wurden ausgeführt. Native Windows-Grafik, DPI und Kamera müssen noch unter Windows geprüft werden.

## Microsoft-Dokumentation
- [Windows Forms Designer](https://learn.microsoft.com/en-us/visualstudio/designers/windows-forms-designer-overview)
- [Designer-Tutorial](https://learn.microsoft.com/en-us/visualstudio/designers/walkthrough-windows-forms-designer)
