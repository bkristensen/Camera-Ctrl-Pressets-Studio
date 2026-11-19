# Standardwerte, Streaming und Kameraquellen (0.6.4)

## Eigene Standardwerte speichern
Über das Zahnrad ein Profil wählen, Werte ändern und **Einstellungen speichern** drücken. Neue Profile können die gespeicherte Konfiguration kopieren. Ein leeres Profil nutzt die Werkseinstellungen. Geänderte Quellcode-Standardwerte überschreiben keine vorhandenen Profile.

## Werkseinstellungen im Code
In `SharedProfiles/Models.cs`, Klasse `SavedData`, Methode `InitializeDefaults` bearbeiten. Konstruktor und Deserialisierung verwenden dieselbe Methode. Werte: `Seconds` 2, `PanSteps` 5, `FastPanSteps` 32, `ZoomSteps` 5, feine Schritte 1 und `WaitForStartup` false. Andere boolesche Optionen sind standardmäßig false. Neu bauen und mit einem neuen leeren Profil testen; vorhandene Profile nicht löschen.

## Optionale Streaming-Bestätigung
**Vor der Kameranutzung fragen, ob das Streaming gestartet wurde** ist bei neuen Profilen ausgeschaltet. Bestehende Profile behalten ihren gespeicherten Wert. Der Dateischlüssel `WaitForStartup` bleibt kompatibel, bedeutet jetzt aber Streaming-Bestätigung.

Ist die Option eingeschaltet, werden beim Start keine Kameras gesucht oder verbunden. Die erste Kamerafunktion fragt nach: Schwenken, Zoom, Preset abrufen/speichern, Kamerasuche, Bildsteuerung laden oder Quelle wählen. **Nein** bricht ohne Kamerazugriff ab; beim nächsten Versuch wird erneut gefragt. **Ja** erlaubt die Nutzung für die Sitzung und setzt die Aktion fort. Neustart, Profilwechsel, Änderung der Option und Bearbeiten der Startprogramme setzen die Bestätigung zurück. Ein bestimmter Streaming-Prozess wird nicht vorausgesetzt. Startprogramme werden weiterhin separat in ihrer Reihenfolge gestartet.

Ohne Häkchen kann die Kamera beim Start ohne Rückfrage verbunden werden. Bei getrennter Kamera versucht eine Aktion die zuletzt gespeicherte Kamera, sonst eine verfügbare Kamera. Ein Besuch der Einstellungen ist nicht erforderlich. Treiberfehler oder fehlende Geräte werden gemeldet. Ein Preset wird nicht auf eine andere automatisch ausgewählte Kamera angewendet.

## Quelle und Bild
Das Kamerasymbol in der Radmitte öffnet die Quellenauswahl; alternativ bei fokussiertem Rad Space/Enter drücken. Die bestätigte Wahl wird im Profil gespeichert. Das wählt nur die von dieser App gesteuerte Kamera, nicht die Kamera der Streamingsoftware oder eine Bildschirmfreigabe.

Über Zahnrad > Bild bei Bedarf **Bildeinstellungen laden** drücken. Helligkeit, Kontrast, Sättigung, Farbton, Schärfe, Gamma, Verstärkung, Gegenlichtkompensation, Fokus, Weißabgleich und Belichtung werden je nach Treiber angeboten. Automatik erscheint nur bei Unterstützung; andere Funktionen werden als nicht verfügbar markiert. Das Zurücksetzen betrifft Bildwerte, nicht Schwenken/Zoom. HDR, Sichtfeld und Flimmerreduktion bleiben herstellerspezifisch. Bildwerte werden unmittelbar gesetzt und nicht in Positionspresets gespeichert.

## Windows-Prüfung
1. Neues leeres Profil: Option aus, keine Rückfrage.
2. Option einschalten, speichern, neu starten: erste Kameraaktion fragt. Nein darf nichts verbinden; danach Ja erlaubt die Aktion und weitere Aktionen ohne Rückfrage.
3. Neustart oder Profilwechsel setzt die Erlaubnis zurück. Ohne Zoom-Prozess testen.
4. Quelle wählen und neu starten: gespeichertes Gerät prüfen. Fehlendes Gerät darf nicht zum Anwenden seiner Presets auf eine andere Kamera führen.
5. Bildregler, Automatik, Zurücksetzen, drei Sprachen und kleine Fenster prüfen.

Native Windows- und Hardwaretests stehen noch aus; Logik- und Speichertests benötigen keine Hardware.
