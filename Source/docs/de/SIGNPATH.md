# Code-Signing-Richtlinie

*Geschrieben für Camera Control Studio 1.1.0.*

Die SignPath Foundation bietet kostenlose Signaturen für berechtigte Open-Source-Projekte. Eine Abnahme ist erforderlich. Das Zertifikat identifiziert die SignPath Foundation als Herausgeber; Der Dienst verknüpft verifizierte Builds mit dem Quell-Repository. Das Projekt hat die Unterzeichnung durch die SignPath Foundation beantragt. Der Antrag steht noch aus und GitHub-Downloads bleiben unsigniert.

Bevor Sie sich bewerben, veröffentlichen Sie den Quellcode unter ihrer Lizenz. Halten Sie das Projekt aktuell. Veröffentlichen Sie ein Release, dokumentieren Sie die App und machen Sie den Build überprüfbar. Signieren Sie nur Ihre eigenen Projektdateien. Signieren Sie weiterhin Anmeldeinformationen aus dem Repository.

Die [-Datenschutzerklärung](../../PRIVACY.md) deckt die App, ihre GitHub-Update-Prüfung und die WebView2-Laufzeit von GuideViewer ab.

## Während der Antrag noch aussteht

- Überprüfen Sie, ob das öffentliche Repository über einen erfolgreichen Windows-Build unter **Actions** verfügt. Verwenden Sie diesen Workflow als Grundlage für die SignPath-Integration.
- Bestätigen Sie die Personen, die für Commits, Überprüfungen und das Unterzeichnen von Genehmigungen verantwortlich sind. Aktivieren Sie die Multi-Faktor-Authentifizierung für alle Teammitglieder auf GitHub und SignPath.
- Bitten Sie SignPath um eine Bestätigung, wie mit den WebView2 SDK-Binärdateien und dem Inno Setup-Installationsprogramm umgegangen werden soll. Die Pakete umfassen die SDK-Binärdateien und Loader von Microsoft. Die App nutzt die auf dem PC des Benutzers installierte WebView2 Runtime. Listen Sie diese Abhängigkeiten in der Anwendung auf und bewahren Sie ihre Hinweise auf.
- Fügen Sie einen **Code-Signatur-Richtlinie**-Link zu den GitHub-Versionshinweisen für die in der Anwendung bereitgestellte Download-Seite hinzu. Geben Sie den tatsächlichen Signaturstatus dieser Downloads an.

## Nachdem SignPath das Projekt akzeptiert hat

- Fügen Sie dieser Seite die genehmigte SignPath-Zuordnung und die bestätigten Namen oder Berechtigungsgruppen für Committer, Prüfer und Genehmiger hinzu.
- Konfigurieren Sie SignPath so, dass Artefakte aus dem vertrauenswürdigen GitHub Actions-Build akzeptiert werden. Beschränken Sie das Signieren auf Projektbinärdateien. Legen Sie den Produktnamen und die entsprechende Produktversion für alle gemeinsam übermittelten Dateien fest und erzwingen Sie diese.
- Signieren Sie für ZIP die ausführbare App-Datei vor dem Packen. Für das Installationsprogramm fügen Sie die signierte ausführbare App-Datei in das Installationsprogramm ein, bevor Sie das fertige Installationsprogramm signieren. Holen Sie für jede Release-Signierungsanfrage eine manuelle Genehmigung ein.
- Überprüfen Sie die Authenticode-Signaturen und Zeitstempel und generieren Sie dann SHA-256-Prüfsummen. Fügen Sie einen Link **Code-Signaturrichtlinie** und den Signaturstatus in die Versionshinweise ein. Überprüfen Sie die hochgeladenen Dateien und Hashes vor der Veröffentlichung.

Bei direkten GitHub-Downloads identifiziert eine gültige Signatur den Herausgeber und schützt die Integrität der Binärdatei. Ein neuer Download kann SmartScreen weiterhin auslösen, während sich die Reputation aufbaut. Für die Erstverteilung über den Store signiert Microsoft akzeptierte MSIX-Pakete.

Für ein öffentlich vertrauenswürdiges Codesignaturzertifikat, das über den Windows-Zertifikatspeicher des aktuellen Benutzers verfügbar ist, verwenden Sie `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` für das tragbare ZIP. Verwenden Sie dieselben Parameter mit `scripts/build-installer.ps1`, um sowohl die ausführbare App-Datei als auch das generierte Installationsprogramm zu signieren. Für die SignPath-Genehmigung ist ein eigener Signatur-Workflow erforderlich.

Lesen Sie die offiziellen [-Bedingungen](https://signpath.org/terms.html), die [Foundation-Website](https://signpath.org/) und die [SmartScreen-Anleitung von Microsoft](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation). Geprüft am 24. November 2026. Informationen zur Store-Route finden Sie unter [MSIX und Store](MSIX-AND-STORE.md).
