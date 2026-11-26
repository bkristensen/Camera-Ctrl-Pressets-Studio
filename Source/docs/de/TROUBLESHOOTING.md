# Fehlerbehebung

*Geschrieben für Camera Control Studio 1.0.1.*

## Inhaltsverzeichnis

- [1. Video-Apps und Sichtfeld](#1-video-apps-und-sichtfeld)
  - [Eingeschränktes Sichtfeld in Zoom Workplace](#eingeschränktes-sichtfeld-in-zoom-workplace)
- [2. Kameraverbindung und Kompatibilität](#2-kameraverbindung-und-kompatibilität)
  - [Kamera fehlt oder ist nicht verfügbar](#kamera-fehlt-oder-ist-nicht-verfügbar)
  - [Steuerung fehlt oder wirkt je nach App anders](#steuerung-fehlt-oder-wirkt-je-nach-app-anders)
- [3. Bewegung und gespeicherte Positionen](#3-bewegung-und-gespeicherte-positionen)
  - [Bewegung stoppt, ruckelt oder läuft in die falsche Richtung](#bewegung-stoppt-ruckelt-oder-läuft-in-die-falsche-richtung)
  - [Sichtfeld oder gespeicherte Position stimmt nicht](#sichtfeld-oder-gespeicherte-position-stimmt-nicht)

## 1. Video-Apps und Sichtfeld

### Eingeschränktes Sichtfeld in Zoom Workplace

**Symptome**\
Die Ränder der Szene fehlen. Herauszoomen und seitliches Schwenken enden zu früh. Gespeicherte Positionen zeigen andere Ausschnitte. **Optimize for video sharing** ist in Zoom deaktiviert. Das Problem tritt auch in der ursprünglichen App Logitech BRIO Control auf.

**Ursache**\
**Im gemeldeten Fall bestätigt.** In Zoom Workplace war **Efficiency Mode** aktiv. Nach dem Ausschalten und einem Neustart von Zoom war das ursprüngliche Sichtfeld wieder da. Camera Control Studio und die Positionen blieben unverändert. Der technische Grund für das engere Sichtfeld ist unbekannt.

**Lösung**

1. Öffnen Sie Zoom Workplace. Gehen Sie zu Ihrem Profilbild → **Settings → Meeting & webinar → Advanced**.
2. Schalten Sie **Efficiency Mode** unter **Performance** aus. Ist der Schalter gesperrt, fragen Sie Ihren Zoom-Administrator.
3. Beenden Sie Zoom Workplace vollständig und öffnen Sie es erneut. Behalten Sie Ihre gespeicherten Positionen beim Testen bei.

**Überprüfung**\
Prüfen Sie, ob die Ränder der Szene wieder sichtbar sind. Rufen Sie die gespeicherten Positionen auf und vergleichen Sie das Livebild mit dem erwarteten Ausschnitt.

**Betroffene Software und Geräte**\
Windows, Zoom Workplace, Logitech BRIO, Logitech BRIO Control und Camera Control Studio 0.7.0-beta.8. Die Versionen von Zoom, Treiber und Firmware wurden nicht erfasst.

**Quellen**\
[Zoom Support](https://support.zoom.com/hc/en/article?id=zm_kb&sysparm_article=KB0080877) · [GitHub #5](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/5)

## 2. Kameraverbindung und Kompatibilität

### Kamera fehlt oder ist nicht verfügbar

**Symptome**\
Die Kameraliste ist leer, die gespeicherte Kamera fehlt oder die Steuerung reagiert nach dem Ruhezustand nicht.

**Ursache**\
**Mögliche Ursachen.** Die USB-Verbindung ist locker, Windows blockiert den Zugriff, eine andere App nutzt die Kamera oder die Streaming-Bestätigung steht noch aus.

**Lösung**

1. Prüfen Sie das USB-Kabel. Falls Ihr Profil eine Streaming-Bestätigung verlangt, starten Sie den Stream, bevor Sie **Ja** wählen.
2. Prüfen Sie die Kamera-Berechtigungen in Windows. Testen Sie die Vorschau in der Windows-Kamera-App und schließen Sie sie danach.
3. Nutzen Sie nach dem Ruhezustand eine Kamerataste, um erneut zu verbinden. Fehlt die gespeicherte Kamera, öffnen Sie die Quellenauswahl über die mittlere Taste und wählen Sie sie erneut. Standardmäßig öffnet ein Doppelklick auf die mittlere Taste die Kameraliste.

![Kamerarad mit der Taste für die Kameraauswahl in der Mitte](../images/screenshots/control%20wheel%20only.png)

**Überprüfung**\
Prüfen Sie den Kameranamen unten im Fenster. Testen Sie, ob ein unterstütztes Steuerelement das Livebild ändert.

**Betroffene Software und Geräte**\
Windows und eingebaute oder USB-Kameras. Gleichzeitiger Zugriff hängt vom Treiber ab.

**Quellen**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Benutzerhandbücher](SETUP.md)

### Steuerung fehlt oder wirkt je nach App anders

**Symptome**\
Ein Kamerabild ist vorhanden, aber manche Regler fehlen. Das Bild ändert sich in einer Video-App und bleibt in einer anderen gleich.

**Ursache**\
**Bestätigt.** Camera Control Studio nutzt Funktionen, die der Windows-Treiber bereitstellt. **Mögliche Ursachen.** Treiber, Firmware oder eine weitere App können die Steuerung oder das angezeigte Bild beeinflussen.

**Lösung**

1. Wählen Sie in beiden Apps dieselbe physische Kamera. Vergleichen Sie die Vorschau in jeweils nur einer App.
2. Schließen Sie andere Kameraprogramme und öffnen Sie die gewünschte Video-App erneut. Testen Sie eine unterstützte Funktion.
3. Besteht das Problem weiter, prüfen Sie Windows Update und die Herstellerseite für Ihr genaues Modell. Folgen Sie bei Treiber- oder Firmware-Updates den Herstelleranweisungen.

**Überprüfung**\
Prüfen Sie, welche Funktion das Livebild in der gewünschten App ändert. Notieren Sie bei einer Meldung Modell, Treiber-, Firmware- und App-Versionen.

**Betroffene Software und Geräte**\
Windows, Kameratreiber und Firmware sowie die verwendeten Video- und Steuerungsprogramme.

**Quellen**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K)

## 3. Bewegung und gespeicherte Positionen

### Bewegung stoppt, ruckelt oder läuft in die falsche Richtung

**Symptome**\
Schwenken, Neigen oder Zoomen stoppt früh. Ein Wechsel läuft in Stufen oder die Pfeile bewegen das Bild in die falsche Richtung.

**Ursache**\
**Bestätigt.** Der Treiber legt Bereich und Schrittgröße fest. Camera Control Studio kann die Pfeilrichtungen umkehren.

Ändert sich die Richtung beim Öffnen von Logitech Camera Settings oder Logitech BRIO Control, könnte das andere Programm die Kamerasteuerung beeinflussen. Laut Bericht blieb das Bild in Zoom aufrecht. Die betroffene Achse und die Ursache sind unklar.

**Lösung**

1. Drücken Sie die quadratische Rahmentaste über den Lupen. Sie stellt das weiteste verfügbare Sichtfeld und den kleinsten Zoom wieder her. Testen Sie Pfeile und Zoom erneut.
2. Öffnen Sie bei falscher Pfeilrichtung **Einstellungen → Bewegung**. Kehren Sie nur die betroffene Achse um.
3. Halten Sie für eine feinere Bewegung **Strg** und eine Pfeiltaste gedrückt. Eine längere Übergangszeit verkleinert die Treiberschritte nicht.
4. Schließen Sie das Logitech-Programm außerhalb einer Übertragung. Testen Sie beide Achsen in derselben Video-App bei gleichem Zoom. Öffnen Sie jeweils nur ein Logitech-Programm und prüfen Sie, ob die Änderung erneut auftritt.

**Überprüfung**\
Prüfen Sie die Richtung der Pfeile und vergleichen Sie die Grenzen im Livebild.

**Betroffene Software und Geräte**\
Kameras mit Windows-Treibern für Schwenken, Neigen oder Zoomen. Die Grenzen hängen von Modell und Videomodus ab.

**Quellen**\
[GitHub #3](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/3) · [GitHub #4](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/4) · [Benutzerhandbücher](DAILY-USE.md)

### Sichtfeld oder gespeicherte Position stimmt nicht

**Symptome**\
Das Bild ist zu eng, eine gespeicherte Position zeigt einen anderen Ausschnitt oder auswählbare Sichtwinkel fehlen.

**Ursache**\
**Bestätigt.** Positionen gehören zu einer Kamera und einem Profil. Ältere Positionen ohne gespeicherten Sichtwinkel behalten den aktuellen Winkel. Manche Treiber melden keine auswählbaren Winkel.

**Lösung**

1. Prüfen Sie das aktive Profil und die Kamera. Stellen Sie das volle Bild wieder her und prüfen Sie das Livebild.
2. Prüfen Sie unter **Einstellungen → Bewegung** das Sichtfeld beim Start. Wenn der Treiber Winkel anbietet, erlauben Sie manuelle Änderungen und vergleichen Sie die Winkel.
3. Rufen Sie die gespeicherte Position auf. Ist das Bild nur bei geöffnetem Zoom enger, folgen Sie dem Abschnitt zu Zoom. Behalten Sie die Position bei der Fehlersuche bei.

**Überprüfung**\
Vergleichen Sie das Livebild mit dem erwarteten Ausschnitt. Rufen Sie die Position erneut auf, sobald das ursprüngliche Sichtfeld wieder da ist.

**Betroffene Software und Geräte**\
Profile und gespeicherte Positionen in Camera Control Studio. Auswählbare Winkel hängen von Kamera und Treiber ab.

**Quellen**\
[Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K) · [Benutzerhandbücher](SETUP.md)

---

Brauchen Sie noch Hilfe? Prüfen Sie Protokolle vor dem Teilen. Nennen Sie Kameramodell, App-Versionen, Schritte und Ergebnisse.

[Problem melden](../en/ISSUES.md) · [Benutzerhandbücher](../USER-GUIDES.md)
