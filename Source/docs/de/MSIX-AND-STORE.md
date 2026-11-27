# MSIX und Store-Verteilung

Verwenden Sie `scripts/build-msix.ps1` und die Manifestvorlage, um ein MSIX-Paket zu erstellen. Microsoft überprüft das Paket, bevor es im Store erscheinen kann. Die portable ausführbare Datei ist nicht signiert.

## Wählen Sie eine Route

| Route | Unterzeichnung |
| --- | --- |
| Microsoft Store mit MSIX | Microsoft signiert das akzeptierte Store-Paket. Für diesen Weg erwerben Sie kein Signaturzertifikat. |
| Microsoft Store mit einem EXE/MSI-Installationsprogramm | Eine Signatur des Herausgebers ist erforderlich. Der Store signiert dieses Installationsprogramm nicht erneut. |
| Direkter MSIX-Download | Das Paket muss über eine vertrauenswürdige Signatur auf dem Gerät des Benutzers verfügen. |
| Direkt portable EXE | Ein nicht signierter Build kann Warnungen erzeugen. Die Signatur identifiziert den Herausgeber, verspricht jedoch keinen sofortigen Ruf. |

## Checkliste

- [ ] Registrieren Sie das Produkt und reservieren Sie seinen Namen in Partner Center. Kopieren Sie den genauen Paketnamen, den Herausgeber und den PublisherDisplayName.
- [ ] Installieren Sie die Windows SDK-Tools und führen Sie den folgenden Befehl mit diesen Identitätswerten aus.
- [ ] Validieren Sie das Manifest und testen Sie den Kamerazugriff, die Installation, das Upgrade, die Deinstallation, die Profilspeicherung und die Sprache unter Windows. Die Vorlage deklariert den Desktop-/Full-Trust-Betrieb und den Webcam-Zugriff. Stellen Sie sicher, dass diese den aktuellen Zertifizierungsanforderungen entsprechen.
- [ ] Bereiten Sie den Eintrag, die Datenschutzrichtlinie, den Support-Link, die Lizenzinformationen, die Altersfreigabe und echte Screenshots vor. Erklären Sie die erforderlichen Kameratreibersteuerungen. Führen Sie die aktuellen Windows-App-Zertifizierungsprüfungen durch.
- [ ] Laden Sie den Kandidaten auf Partner Center hoch, beheben Sie Validierungsprobleme, reichen Sie ihn ein und warten Sie auf die Zertifizierung. Beschreiben Sie die App vor der Genehmigung nicht als vom Store genehmigt.

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.PRODUCT.IDENTITY' -Publisher 'CN=YOUR-PARTNER-CENTER-ID' -PublisherDisplayName 'YOUR DISPLAY NAME'
```

Die Ausgabe ist ein unsignierter Store-Submission-Kandidat und kein bereit zur Installation befindliches öffentliches Sideload-Paket. Das Skript validiert erforderliche Identitätsfelder und verwendet einen expliziten x64-Build. Wählen Sie für jede Veröffentlichung eine neue vierteilige Paketversion, wobei der letzte Teil Null für die Store-Einreichung ist.

Offizielle Referenzen, überprüft am 18. November 2026:

- [Microsoft-Codesignaturoptionen](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options)
- [Checkliste für die Store-Bereitschaft](https://learn.microsoft.com/en-us/windows/apps/publish/store-readiness-checklist)
- [MSIX Signieranleitung](https://learn.microsoft.com/en-us/windows/msix/package/sign-msix-package-guide)

Siehe auch [SignPath](SIGNPATH.md). Die aktuellen Zulassungs- und Einreichungsregeln sollten vor der Veröffentlichung noch einmal überprüft werden.
