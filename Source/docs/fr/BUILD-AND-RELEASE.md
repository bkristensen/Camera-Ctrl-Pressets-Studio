# Construire et publier Camera Control Studio

*Écrit pour Camera Control Studio 1.0.1.*

Exécutez les commandes à partir du répertoire contenant `Camera-Control-Pressets-Studio.sln`. Utilisez Windows et PowerShell pour le programme d'installation et les tests Windows.

## 1. Préparez la version

1. Commencez à partir du commit sélectionné pour la version. Fermez toute instance Camera Control Studio en cours d'exécution avant de compiler ou de tester une mise à niveau.
2. Installez le SDK .NET 8 sélectionné par `global.json`. Installez Inno Setup 6 ou 7 si vous prévoyez de publier le programme d'installation. Installez le SDK Windows si vous envisagez de signer des fichiers avec SignTool ou de créer un MSIX avec MakeAppx.
3. Vérifiez les prérequis avec `./scripts/setup-dev.ps1 -CheckOnly`. Pour un nouveau poste de travail, `./scripts/setup-dev.ps1` restaure les packages épinglés et exécute la version portable.
4. Définissez la version dans `Directory.Build.props` et mettez à jour `CHANGELOG.md`. Le script d'installation lit la version par défaut à partir des propriétés de build. Gardez la version Store en quatre parties séparée lors de la préparation d'un MSIX.

## 2. Créez les packages GitHub

Choisissez une commande. La version du programme d’installation inclut la version portable.

| Que construire | Commande | Sortie |
| --- | --- | --- |
| Portable ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| Installateur portable ZIP et Inno | `./scripts/build-installer.ps1` | Portable ZIP plus `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` vérifie les traductions, construit Release avec des dépendances verrouillées, exécute des tests et crée le ZIP. Recherchez l'exécutable dans `CameraControlStudio/bin/Release/net48` et les fichiers préparés dans `artifacts/portable/Camera-Control-Studio`. Publiez l'intégralité du ZIP. L'application a besoin de sa configuration, de ses bibliothèques et de ses guides.

Pour utiliser un certificat de signature de code individuel ou d'organisation disponible dans le magasin de certificats Windows de l'utilisateur actuel, transmettez son empreinte numérique et le chemin d'accès à SignTool lors de la création. La commande d'installation signe l'application avant de la compresser et signe le programme d'installation terminé avec un horodatage.

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

Pour le ZIP seul, donnez les mêmes paramètres au `build.ps1`. Pour la signature sponsorisée, suivez le flux de travail approuvé [SignPath](SIGNPATH.md). Marquez clairement les téléchargements non signés.

## 3. Vérifiez les packages sous Windows

1. Extrayez le ZIP dans un nouveau répertoire. Démarrez l'application à partir de là et ouvrez **?** avec les paramètres fermés et ouverts. Avec [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) installé, vérifiez la navigation du guide et imprimez sur PDF pour vérifier le titre, la date et l'heure d'impression, la version installée, l'adresse du projet et les numéros de page. Vérifiez la version de l'application, le comportement de lancement, les préréglages, les commandes de la caméra et la sélection de la langue lors du changement de profil.
2. Si vous envisagez de publier le programme d'installation, installez-le sur un compte de test Windows propre. Vérifiez l'application du menu Démarrer et les raccourcis du guide de l'utilisateur. Vérifiez que les 12 guides sont présents, qu'une mise à niveau conserve les profils existants et que la désinstallation est terminée.
3. Parcourez la liste de contrôle de version [](RELEASE-CHECKLIST.md). Pour les versions signées, exécutez la vérification SignTool sur l'exécutable et le programme d'installation de l'application. Générez des hachages uniquement une fois la signature et le reconditionnement terminés.

## 4. Organisez et hachez les téléchargements GitHub

Remplacez la version dans cet exemple pour les versions ultérieures. Omettez les lignes du programme d'installation pour une version ZIP uniquement.

```powershell
$version = '1.0.0'
$release = "artifacts/releases/$version"
New-Item -ItemType Directory -Path $release -Force | Out-Null

$zip = "Camera-Control-Studio-$version-portable.zip"
$installer = "CamCtrlStudio-Setup-$version.exe"
Copy-Item 'artifacts/Camera-Control-Studio-portable.zip' (Join-Path $release $zip)
Copy-Item "artifacts/installers/$installer" $release
@($zip, $installer) | ForEach-Object {
    $hash = Get-FileHash -LiteralPath (Join-Path $release $_) -Algorithm SHA256
    '{0}  {1}' -f $hash.Hash.ToLowerInvariant(), $_
} | Set-Content -LiteralPath (Join-Path $release 'SHA256SUMS.txt') -Encoding ascii
```

Le dossier versionné contient le ZIP final, le programme d'installation et le manifeste SHA-256. Le `bin` et les répertoires de transfert portables sont des entrées de construction pour cette étape.

## 5. Publiez sur GitHub

1. Mettez à jour le sous-titre de la version dans chaque guide que vous consultez pour cette version. Gardez ses traductions synchronisées. Validez la version finale, le journal des modifications, les guides et les modifications source. Dans [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases), créez un brouillon et utilisez une balise telle que `v1.0.0` sur la validation testée.
2. Copiez l'entrée du journal des modifications correspondante dans les notes de version, indiquez si les téléchargements sont signés et sélectionnez l'option préliminaire uniquement pour les versions préliminaires.
3. Joignez le ZIP versionné, le programme d'installation s'il est construit et le `SHA256SUMS.txt` de `artifacts/releases/<version>/`. Vérifiez les noms de fichiers, téléchargez et vérifiez les fichiers terminés, puis publiez.

## Chemins de construction facultatifs

| Objectif | Scénario | Sortie |
| --- | --- | --- |
| Captures d'écran de la documentation avec des exemples de données | `./scripts/capture-docs.ps1` | Fichiers PNG dans `docs/images/screenshots/<version>/<language>/` pour les 12 guides d'utilisation |
| Construction Docker et tests logiques | `./scripts/build-docker.ps1` | Portable ZIP dans `artifacts/docker/` |
| Candidat à la soumission au Microsoft Store | `./scripts/build-msix.ps1` avec votre identité Partner Center, éditeur et version du package | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

La version Store nécessite la véritable identité Partner Center et suit la liste de contrôle distincte [MSIX et Store](MSIX-AND-STORE.md). Le résultat est un candidat à la soumission non signé. `Install.ps1` est le programme d'installation facultatif par utilisateur dans un ZIP portable extrait.
