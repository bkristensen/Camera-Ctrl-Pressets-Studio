# Einrichtung, Einstellungen und Anpassungen

## Installation und Sprache

Das vollständige Windows-ZIP entpacken und Cam Pressets Studio.exe starten.
Windows und .NET Framework 4.8 werden benötigt. Die Betaversionen sind unsigniert.
Windows-Schutzfunktionen nicht deaktivieren. Docker baut die Anwendung, führt aber
nicht ihre Benutzeroberfläche aus.

Unter Einstellungen > Sprache Automatisch, English, Dansk oder Deutsch wählen.
**Sprache speichern** anklicken, Studio schließen und erneut öffnen. Andere offene
Änderungen vorher ebenfalls speichern. Automatisch verwendet die Windows-Anzeigesprache:
Dänisch/Deutsch, sonst Englisch. --lang=en/da/de/auto in der Verknüpfung hat Vorrang.
Windows-eigene Dialogschaltflächen und Treiberfehler können in der Systemsprache bleiben.

## Profile und Startprogramme

Profile für verschiedene Kameras oder Abläufe anlegen. Kopierte Profile übernehmen
Startprogramme und einen vorhandenen Einstellungscode. Leere Profile deaktivieren
den automatischen Programmstart. Die Sprache gilt für den Windows-Benutzer, nicht
für das einzelne Profil.

Unter Programme / Einstellungscode bearbeiten .exe-, .lnk- oder .appref-ms-Dateien
wählen. Den tatsächlichen Prozessnamen ohne .exe eingeben; siehe Task-Manager > Details.
Verknüpfungen können anders benannte Prozesse starten. Nach oben/unten ändert die
Reihenfolge. Ein gemeinsames Kontrollkästchen aktiviert den automatischen Start.
Speichern und jetzt starten testet die Liste. Laufende Programme werden übersprungen.
Erscheint ein Prozess nicht innerhalb von 20 Sekunden, endet die Startsequenz.

Der optionale Code sperrt die Bearbeitung der Startprogramme, nicht Positionen oder
andere Einstellungen. Zum Entfernen entsperren und einen leeren neuen Code speichern.
Das ist keine Verschlüsselung; Dateizugriff ermöglicht das Umgehen der Sperre.

## Kamera und Steuerung

Für den BRIO/Zoom-Referenzablauf die Kamera-Startsperre aktiviert lassen. Sie blockiert
Suche und Verbindung, bis Programme laufen und Sie das Meeting bestätigen. Kameraauswahl
und Kameras suchen umgehen sie nicht. Ausschalten erlaubt ausdrücklich frühen Zugriff.

| Einstellung | Wirkung / Empfehlung |
| --- | --- |
| Normale Schritte | Für präzise Einstellung 1 versuchen; bei Bedarf erhöhen |
| Schnelle Schritte | Größerer Wert zur Navigation; Kameragrenzen bleiben wirksam |
| Zoomschritte | Eigene Anzahl von Treiberschritten pro Aktion |
| Übergang | Längere Zeit mildert Übergänge; 0 ist unmittelbar |
| Richtungen umkehren | Kehrt Befehle um, dreht niemals das Videobild |
| Immer im Vordergrund | Hält das Fenster über anderen Fenstern |
| Bildregler | Nur vom Treiber bereitgestellte Eigenschaften sind verfügbar |

Profileinstellungen mit Einstellungen speichern sichern. Schrittanzahl ist keine
Motorgeschwindigkeit. Positionen speichern weder HDR noch Sichtfeld. Fehlende
Eigenschaften im Herstellerprogramm einstellen und zwischen Positionen konstant halten.

## Fehlerbehebung und Sicherung

Keine Kamera: Startsperre, USB und Windows-Kameraberechtigung prüfen. Belegtes Gerät:
andere Steuerprogramme prüfen, dabei die erforderliche Reihenfolge erhalten.
Ruckeln: kleinere Schritte oder längeren Übergang versuchen; Treiber können grobe
Schritte vorgeben. Gedrehtes Bild: zuerst Zoom korrekt einrichten, dann Studio aktivieren.

Die ungepackte App speichert Profile in %LOCALAPPDATA%\CamPressets\profiles.json und
die Sprache daneben in language.txt. Beide sichern. MSIX kann Daten umleiten;
gemeinsame Daten mit der portablen Version und Deinstallationsverhalten sind unbestätigt.
