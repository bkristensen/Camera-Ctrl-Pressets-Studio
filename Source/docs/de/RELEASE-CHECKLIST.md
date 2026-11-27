# Release-Checkliste

Verwenden Sie für Befehle und Ausgabepfade den Build- und Release-Leitfaden [](BUILD-AND-RELEASE.md).

## Automatisierte Kontrollen

- [ ] Führen Sie `scripts/build.ps1` auf einem sauberen Checkout mit gesperrten Abhängigkeiten aus.
- [ ] Bestätigen Sie, dass jedes Gebietsschema über dieselben eindeutigen, nicht leeren Schlüssel verfügt. Der Build führt die Übersetzungsprüfung und die Gebietsschematests durch.
- [ ] Überprüfen Sie die Quell- und Binärversion, die Paketidentität und die Versionshinweise.

## Windows- und Kameraprüfungen

- [ ] Starten Sie mit 900 × 500 und überprüfen Sie das Minimum von 670 × 450. Größe ändern, maximieren und wiederherstellen bei 100 %, 125 %, 150 % und 200 % Anzeigeskalierung. Überprüfen Sie die Fußzeile, Eingaben, kombinierte Symbole und Zoom-Glyphen.
- [ ] Öffnen Sie rechts die Einstellungen. Ziehen Sie die Trennlinie. Schließen und wieder öffnen. Testen Sie ein gespeichertes Layout, nachdem Sie einen Monitor getrennt haben.
- [ ] Position abrufen: gelb während der Bewegung, grün nur nach übereinstimmender Rücklesung. Durch Schwenken/Neigen/Zoomen wird die aktive Farbe gelöscht. Verbindung während eines Übergangs trennen: kein falsches Grün. Symbole verschwinden nach der Bestätigung nie.
- [ ] Testen Sie jedes kombinierte Symbol, einschließlich jeder umgekehrten Reihenfolge. Testen Sie die Zoom-Tasten, den Zoom-Schieberegler und beide Richtungsumkehrungen.
- [ ] Testen Sie auf einem unterstützten Brio den automatischen FOV in beiden Zoomrichtungen sowie am linken und rechten Schwenkrand. Befolgen Sie die [Brio-Testschritte](VALIDATION.md#brio-sichtfeld-und-zoom).
- [ ] Aktivieren Sie die Kamerabestätigung. Neustart. Stellen Sie sicher, dass keine Kameranummer oder Verbindung vorhanden ist, bevor Sie „Ja“ wählen. Testen Sie Nein, dann versuchen Sie es erneut über eine Schwenk-/Zoom-Taste. Testprofiländerung und Quellenauswahl.

## Daten und Bearbeitung

- [ ] Sichern Sie die 0.6-Profildatei vor der Migration. Bestätigen Sie, dass Voreinstellungen und Symbol-IDs erhalten bleiben. Bestätigen Sie, dass veraltete externe Programmeinstellungen nach dem Speichern nicht mehr vorhanden sind.
- [ ] Testen Sie neue/kopierte/umbenennte Profile, stellen Sie das Layout wieder her und behalten Sie das aktuelle Layout bei. Stellen Sie sicher, dass temporäre Positionen nur für Sitzungen gelten.
- [ ] Testen Sie die automatischen/manuellen Bildeinstellungen und Standardeinstellungen mit der tatsächlichen Kamera.
- [ ] Öffnen Sie jedes Formular mit Shift+F7 in Visual Studio. Passen Sie eine Layouteigenschaft an, speichern Sie sie und erstellen Sie sie neu. Benutzerdefinierte Zeichnungen bleiben in den Kontrollklassen.
- [ ] Öffnen Sie den Guide Viewer aus dem extrahierten ZIP in einem 64-Bit-Windows-Prozess. Öffnen Sie es erneut und drucken Sie eine Anleitung zu PDF aus. Bestätigen Sie, dass das Installationsprogramm und ZIP für jede unterstützte Architektur den passenden nativen WebView2-Loader unter `runtimes` einschließen.
- [ ] Überprüfen Sie den [uruser-guide index](USER-GUIDES.md) und stellen Sie sicher, dass alle Sprachführer in beiden Paketen vorhanden sind. Überprüfen Sie die [guide-Screenshots](SCREENSHOTS.md) auf vollständige Tab- und Untertab-Abdeckung, Zuschnitt und Überlappung.

## Verteilung

- [ ] Für eine portable Version verwenden Sie `scripts/build.ps1`. Verwenden Sie für ein Inno Setup-Installationsprogramm `scripts/build-installer.ps1`, das auch den tragbaren Build und die Tests ausführt. Überprüfen Sie die vom Installationsprogramm und der App angezeigte Version.
- [ ] Befolgen Sie für eine Store-Veröffentlichung die [Store-Checkliste](MSIX-AND-STORE.md). Testen Sie die Pakettypen, die Sie unter Windows veröffentlichen möchten.
- [ ] Überprüfen Sie beim Signieren gegebenenfalls die Authenticode-Signaturen in der ausführbaren App-Datei und im Installationsprogramm. Kennzeichnen Sie nicht signierte Downloads deutlich. Generieren Sie SHA-256-Hashes nach der endgültigen Signatur und Verpackung.
- [ ] Link zur [Code-Signatur-Richtlinie](SIGNPATH.md) in den GitHub-Versionshinweisen. Überprüfen Sie bei SignPath-Releases die Build-Herkunft und die manuelle Genehmigung und schreiben Sie SignPath die von ihm signierten Dateien zu.
- [ ] Veröffentlichen Sie Quelle, Lizenz, Versionshinweise, Hashes und Binärdateien gemeinsam. Machen Sie keinen Anspruch auf nicht durchgeführte Hardwaretests oder eine Store-Genehmigung.
