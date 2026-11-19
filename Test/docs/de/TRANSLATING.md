# Übersetzungen pflegen oder Sprachen hinzufügen

## Dateien und Priorität

Die Apptexte liegen in SharedUi/Locales/en.xml, da.xml und de.xml. Jedes text-Element
enthält einen exakten englischen Schlüssel und einen übersetzten Wert. Die Dateien
werden in die EXE eingebettet; XML neben einer installierten EXE zu ändern genügt
nicht. Nach Änderungen neu bauen.

Priorität: erkannte --lang-Option, gespeicherte Auswahl in language.txt, danach
Windows-Anzeigesprache bei Automatisch. Nicht unterstützte Sprachen verwenden Englisch.
Ein Neustart verhindert gemischte Übersetzungen in Kamera-Eigenschaftsschlüsseln.

## Bestehende Übersetzung ändern

1. Den englischen Schlüssel in allen Sprachdateien finden. Nur den übersetzten Wert ändern.
2. Führende/abschließende Leerzeichen, Zeilenumbrüche, Platzhalter und Dateifilter erhalten.
   XML-Zeichen maskieren, etwa & als &amp;. Geräte- und benutzerdefinierte Namen nicht übersetzen.
3. scripts/check-translations.ps1 und anschließend scripts/build.ps1 ausführen.
4. Mit --lang=de, da oder en starten. Einstellungen und Dialoge bei mehreren DPI-Stufen prüfen.
5. Die Anleitungen in docs/en, docs/da und docs/de aktualisieren und einen Pull Request erstellen.

## Weitere Sprache hinzufügen

en.xml etwa nach fr.xml kopieren und alle Werte übersetzen. Das language-Attribut
anpassen. Code und Eigenname der Sprache in LanguageCodes und LanguageNames in
SharedUi/UiText.cs in gleicher Reihenfolge ergänzen. App- und Testprojekte betten
XML-Dateien per Platzhalter ein. ResolveLanguage verwendet die Basissprache;
regionale Varianten benötigen gezielte Änderungen.

Tests für manuelle Auswahl, Automatisch, unbekannte Sprache und englischen Fallback
hinzufügen. Die Sprache unter Resources im MSIX-Manifest ergänzen. Entsprechende
Inno Setup-Sprache und .NET-Voraussetzungsmeldung ergänzen, sofern verfügbar.
Anleitungen und Dokumentationsindex erstellen. Vor Freigabe unter Windows testen.

## Neue Apptexte

UiText.T("English", "Dansk") verwenden und den exakten englischen Schlüssel in
allen Sprachdateien ergänzen. Bei Schlüsseländerungen Code und Ressourcen gemeinsam
ändern. Der Prüfer vergleicht die Sprachdateien; neue Quelltextschlüssel müssen
zusätzlich geprüft werden. Fehlende deutsche/andere Texte fallen auf Englisch zurück;
Dänisch kann den Inline-Text nutzen. Das ist eine Absicherung, kein Ersatz für eine
vollständige Übersetzung. Betriebssystem- und Treibermeldungen sind getrennt zu betrachten.

Die verbindliche MIT LICENSE bleibt Englisch. Übersetzte Erläuterungen ändern ihre Bedingungen nicht.
