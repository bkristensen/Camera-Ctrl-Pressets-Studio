# Übersetzen Sie Camera Control Studio

*Geschrieben für Camera Control Studio 1.1.1.*

## Bearbeiten Sie eine vorhandene Sprache

1. Öffnen Sie `Source/Shared/Locales/<code>.xml`. Verwenden Sie `en.xml` als Referenz für Kontext und Schlüssel.
2. Bearbeiten Sie den Text zwischen den `<text>`-Tags. Behalten Sie die Schlüssel, Platzhalter wie `{0}` und gespeicherten IDs bei. Als UTF-8 speichern. Schreiben Sie in XML ein wörtliches kaufmännisches Und als `&amp;`.
3. Wenn Sie auch die Hilfe ändern, bearbeiten Sie den entsprechenden Ordner in `Source/docs`. Testen Sie Links zu anderen Anleitungen und Bildern im Guide Viewer.

## Fügen Sie eine Sprache hinzu

1. Kopieren Sie `en.xml` in eine Datei, die nach dem neuen Sprachcode benannt ist, und übersetzen Sie alle Werte.
2. Registrieren Sie den Code und den Anzeigenamen im gleichen Index in `LanguageCodes` und `LanguageNames` in `Source/Shared/UiText.cs`. Aktualisieren Sie ggf. die Handhabung von rechts nach links.
3. Fügen Sie die App-Sprache zum MSIX-Manifest hinzu. Wenn Sie ein Benutzerhandbuch hinzufügen, folgen Sie der Struktur eines vorhandenen Ordners unter `Source/docs`, fügen Sie es zu `USER-GUIDES.md` hinzu und stellen Sie sicher, dass Guide Viewer es öffnet.

## Überprüfen und abschicken

Führen Sie von `Source` aus `powershell -File scripts/check-translations.ps1` aus und erstellen Sie es. Starten Sie die App mit `--lang=<code>`. Überprüfen Sie Dialoge bei kleinster Fenstergröße und navigieren Sie mit der Tastatur. Testen Sie lange Beschriftungen, Textrichtungen und Nachrichten, die Platzhalter ausfüllen.

Folgen Sie dem [GitHub-Projektleitfaden](GITHUB.md), um einen Pull Request zu öffnen. Benennen Sie die Sprache und den Code, fassen Sie die Änderungen zusammen und geben Sie an, was Sie getestet haben. Fügen Sie einen Screenshot bei, wenn er den Prüfern bei der Beurteilung des Layouts hilft. Fügen Sie während der Überprüfung Folgebearbeitungen zum selben Zweig hinzu.
