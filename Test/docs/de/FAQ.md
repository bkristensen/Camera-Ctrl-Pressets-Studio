# Häufige Fragen

**Ist die App kostenlos?** Ja. MIT erlaubt Nutzung, Änderungen, Forks und Weitergabe,
auch kommerziell, mit dem Lizenzhinweis. Die englische LICENSE ist verbindlich.

**Welche Systeme wurden getestet?** Der Eigentümer hat Entwicklungsversionen mit
Logitech BRIO und Zoom verwendet. Das bestätigt nicht automatisch die neueste Version.
Weitere Logitech- und USB/UVC-Kameras werden nur bei passenden Windows-Treiberkontrollen
für Pan/Tilt/Zoom erwartet. Teams, Meet, Webex, OBS und Windows Camera sind nicht
verifiziert. Erfolgreiche und fehlgeschlagene Kombinationen bitte melden.

**Funktioniert es ohne Zoom?** Die Kamerasteuerung verwendet keine Zoom-API, aber
die aktuelle Startsperre verlangt Zoom und eine manuelle Meetingbestätigung.
Nur nach Prüfung der eigenen Reihenfolge ausschalten. Eine allgemeine Sperre ist geplant.

**Warum verbindet sich die Kamera nicht?** Startsperre prüfen. Programme starten,
am Zoom-Meeting teilnehmen, Bild prüfen und in den Einstellungen bestätigen.

**Wie ändere ich die Sprache?** In Einstellungen wählen, Sprache speichern anklicken
und neu starten. Die Auswahl gilt für alle Profile. Eine --lang-Option hat Vorrang.

**Warum bleiben einzelne Schaltflächen anderssprachig?** Windows-eigene Dialoge
und Treiberfehler können die Systemsprache verwenden. Eigene Namen werden nicht übersetzt.

**Gibt es Vorschau, Aufnahme oder Bildschirmfreigabe?** Nein. Das Bild in einer
anderen Anwendung betrachten. Positionen speichern Kamerakoordinaten, keine Videoszenen.

**Warum ruckelt es?** Der Treiber kann grobe Schritte vorgeben. Kleinere Schritte
oder längere Übergänge versuchen. Umgekehrte Pfeile drehen nicht das Bild.

**Wo liegen die Daten?** Die ungepackte App verwendet %LOCALAPPDATA%\CamPressets:
profiles.json für Profile, language.txt für Sprache. MSIX kann Daten umleiten.

**Schützt der Code meine Daten?** Nein. Er verhindert versehentliche Änderungen an
Startprogrammen, ist aber keine Verschlüsselung oder Windows-Zugriffskontrolle.

**Wie deinstalliere ich?** Installer/Store: Windows-App-Einstellungen verwenden.
Portable: App schließen und Ordner löschen. Install.ps1: installierten Ordner und
Startmenü-Verknüpfung entfernen. Profile vor bewusstem Löschen sichern.

**Wie schlage ich Funktionen vor?** Die GitHub-Feature-Vorlage verwenden.
Kompatibilitätsberichte für Hardwarefeedback und Discussions für offene Fragen nutzen.
Versionen und konkrete Tests angeben, niemals private Meetingdaten.
