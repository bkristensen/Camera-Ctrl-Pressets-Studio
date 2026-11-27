# Validierung für 0.7.0-beta.3

*Geschrieben für Camera Control Studio 1.1.0.*

Der Windows-Release-Build verwendet gesperrte Abhängigkeiten. Übersetzungsparität und die automatisierten Tests bestanden. Die Tests umfassen Bewegung, Voreinstellungen, Profilsprache, Persistenz, Protokollierung und das nach dem Ruhezustand gemeldete HRESULT „Kamera nicht bereit“.

Das extrahierte tragbare ZIP wurde in x86- und x64-Windows-Prozessen getestet. Beide öffneten und öffneten die Anleitungen für den täglichen Gebrauch und die Einrichtung erneut, zeigten ihre Screenshots an und druckten sie auf PDF aus. Beim x64 PDF waren auf allen fünf Seiten Titel, Version, Datum und korrekte Seitenzahl angegeben. Die Schaltfläche **?** war während der Entwurfszeitinitialisierung von WinForms vorhanden. Der ARM64-Loader wurde auf die korrekte Binärarchitektur überprüft.

Das English Standard-Profil wurde mit einer Demokamera aufgenommen. Die 12 Vollfenster-Screenshots decken beide Hauptfensterzustände, jede Einstellungsregisterkarte und jede Unterregisterkarte ab. Die lokalen Links in den Handbüchern und READMEs wurden mit den gepackten Dokumentpfaden verglichen.

## Brio-Sichtfeld und Zoom

Verwenden Sie einen physischen Brio, der 65°, 78° und 90° bietet. Beachten Sie das Modell, die Firmware, den Treiber, die App-Versionen und den von Zoom verwendeten Videomodus.

1. Starten Sie Zoom und verwenden Sie eine feste Szene mit sichtbaren Referenzpunkten in der Nähe der Ränder. Bestätigen Sie mit Logitech Brio Control den breitesten Bildausschnitt bei 90° und minimalem Zoom.
2. Starten Sie Camera Control Studio mit **Nichts ändern** und stellen Sie sicher, dass das Gerät seinen FOV behält. Starten Sie mit **Weitest verfügbar** neu und wiederholen Sie den Vorgang dann mit einem angegebenen FOV und mit aktivierter Streaming-Bestätigung.
3. Wenn alle drei Apps ausgeführt werden, können Sie Schwenken, Neigen, Zoomen und FOV in Logitech Brio Control ändern. Bestätigen Sie die aktualisierten Messwerte in Camera Control Studio mit deaktiviertem und aktiviertem manuellem FOV.
4. Ändern Sie FOV in Camera Control Studio. Beobachten Sie, wenn Logitech Brio Control seine eigene Anzeige aktualisiert. Speichern und rufen Sie Voreinstellungen für jeden FOV ab, einschließlich einer älteren Voreinstellung ohne FOV.
5. Wählen Sie aus einer schmalen, vergrößerten Aufnahme **Vollbild** aus und vergleichen Sie das Bild mit der Referenz aus Schritt 1. Wiederholen Sie den Vorgang während eines voreingestellten Übergangs und bei bereits geöffneter Logitech Brio Control.
6. Schließen Sie Camera Control Studio und prüfen Sie, ob Logitech Brio Control die Kamera noch wechseln kann. Wiederholen Sie den Vorgang in umgekehrter Reihenfolge beim Starten der Anwendung. Fügen Sie das Diagnoseprotokoll bei und notieren Sie etwaige Abweichungen zwischen dem Livebild und den gemeldeten Werten.
7. Drücken Sie bei 65° und minimalem Zoom einmal auf „Zoom out“. Überprüfen Sie, ob sich FOV auf 78° ändert und der nächste Schritt herauszoomt. Bei 78° wiederholen. Stellen Sie sicher, dass das Verkleinern nur bei 90° und minimalem Zoom stoppt. Testen Sie den umgekehrten Pfad zum Vergrößern.
8. Drücken Sie an einem horizontalen Pfannenrand erneut denselben Pfeil. Prüfen Sie, ob ein breiterer FOV und eine Zoomeinstellung Bewegung ermöglichen, ohne dass die Aufnahme verloren geht. Wenn keine Ansicht hilft, überprüfen Sie, ob der ursprüngliche Rahmen wiederhergestellt ist. Schalten Sie die Funktion aus und vergewissern Sie sich, dass der FOV an Ort und Stelle bleibt.

Inno Setup 7 hat das Installationsprogramm mit den Anleitungen für alle 12 Sprachen und den x86-, x64- und ARM64-Loadern kompiliert. Beide Pakete sind unsigniert und enthalten SHA-256-Hashes. Eine saubere Kontoinstallation und -deinstallation, ein physischer Kamera-Ruhezustandszyklus und ein ARM64-Laufzeittest stehen noch aus. Siehe die Release-Checkliste [](RELEASE-CHECKLIST.md) und den Build- und Release-Leitfaden [](BUILD-AND-RELEASE.md).
