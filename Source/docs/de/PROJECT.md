# Projektbeschreibung

*Geschrieben für Camera Control Studio 1.1.1.*

Camera Control Studio speichert und ruft Kameraaufnahmen unter Windows ab. Verwenden Sie es in einem Studio, einem Klassenzimmer oder einem Live-Meeting. Ihre Aufnahme-App verarbeitet das Video.

## Kernverhalten

Verwenden Sie den Innenring für normale Bewegungen und den Außenring für schnelle Bewegungen. Jeder Ring hat diagonale Pfeile. Doppelklicken Sie auf die mittlere Schaltfläche, um eine Kamera auszuwählen, oder ziehen Sie sie zum Steuern. Beide Gesten können Sie unter Einstellungen → Schwenken ändern. Die Zoomtasten bewegen sich im eingestellten Schritt. Der Schieberegler stellt den Zoom innerhalb des aktuellen FOV ein. Die App öffnet keine Videovorschau. [Bewegung und FOV](SETUP.md#sichtfeld-einstellen) einstellen

Permanente Positionen speichern einen Namen, eine stabile Symbol-ID sowie die Schwenk-, Neige-, Zoom- und FOV-Meldungen des Treibers. Kombinierte Symbole zeigen Themen von links nach rechts. Festanstellungen erscheinen zuerst in Fettdruck. Temporäre Positionen haben kein Symbol und bleiben bestehen, bis Sie die App schließen. Verwenden Sie „Play“ oder doppelklicken Sie, um einen der beiden Typen abzurufen.

Eine glatte Interpolation erfordert Zwischenkoordinaten. Während des Übergangs ist eine Zeile gelb. Es ist nur grün, wenn der Readback mit dem Ziel übereinstimmt. Durch eine manuelle Aktion, einen Abbruch oder einen fehlgeschlagenen Übergang wird dieser Status gelöscht. Der Treiber bestimmt die tatsächliche Bewegungsgranularität und Latenz.

Profile umfassen Kameraauswahl, permanente Positionen, Bildwerte, manuelle Bewegungsraten, voreingestellte Übergangsdauer oder -geschwindigkeit, Richtungsumkehr, Kamerabestätigung, Fensterlayout und Sprache. Beim Ändern von Profilen wird die App mit der Sprache des ausgewählten Profils neu gestartet. Die optionale Kamerabestätigung blockiert die Aufzählung und Verbindung, bis der Benutzer bestätigt, dass das Streaming gestartet wurde.

## Architektur

`CameraControlStudio` ist eine .NET Framework 4.8 WinForms-Anwendung. `Forms/*.Designer.cs` Haltelayout. `Forms/*.cs` Ereignisbehandlung anhalten. `AppForm` ist die gemeinsame Basis. `Controls` enthält Vektorrendering. `Camera.cs` verwendet Windows DirectShow-Kamera- und Videoverarbeitungsschnittstellen ohne Aufnahmediagramm.

`Shared` enthält Modelle, Persistenz, Übersetzung, Bewegungsarithmetik, Sitzungspositionen und Zustandslogik. `Tests` ist eine .NET 8-Konsolen-Regressionssuite. Profile verwenden atomare Dateiersetzung, Sicherungen und Revisionsprüfungen. Der vorhandene 0,6-Profilspeicher bleibt erhalten; Veraltete Programmstarteinstellungen werden beim Speichern ignoriert und verworfen.

## Umfang und Kompatibilität

Der Eigentümer hat berichtet, dass die frühere Anwendung bei der Einrichtung seines Laptops und seiner Kamera funktioniert hat. Diese 0.7-Neufassung wurde kompiliert und hat die Logiktests bestanden, wurde jedoch in dieser Build-Umgebung nicht auf physische Hardware angewendet. Es wird behauptet, dass keine zusätzliche Kamera oder Aufnahmesoftware getestet wurde.

Andere Windows-Kameras funktionieren möglicherweise, wenn ihre Treiber die erforderlichen Standardsteuerelemente implementieren. Der gleichzeitige Zugriff mit Aufnahme-, Besprechungs- oder Streaming-Software ist treiberabhängig. Virtuelle Kameras dürfen keine physischen Kontrollen offenlegen. Dies ist kein generischer Netzwerk-PTZ-Controller. Linux und macOS können über den Docker-Workflow Build-Hosts sein, diese Windows-Benutzeroberfläche jedoch nicht nativ ausführen.

Bitte melden Sie erfolgreiche Kombinationen, einschließlich Windows-Version, Kameramodell, Treiberversion, Aufnahmeanwendung und welche Steuerelemente funktionierten. Melden Sie auch Einschränkungen, damit die Kompatibilitätsliste evidenzbasiert sein kann.

## Mögliche zukünftige Arbeit

Automatisierung der Windows-UI-Regression; bessere Zugänglichkeit und Tastaturerkennung; optionaler Import/Export einzelner Profile; verifizierte Hardwarematrix; unterzeichnete Veröffentlichungen nach Genehmigung; zusätzliche Kamera-Backends, wenn sie den einfachen Kernworkflow bewahren. Dabei handelt es sich um Ideen, nicht um gelieferte Features.
