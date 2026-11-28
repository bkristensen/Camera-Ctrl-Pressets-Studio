# Politique de signature de code

*Écrit pour Camera Control Studio 1.1.1.*

La Fondation SignPath propose une signature gratuite pour les projets open source éligibles. L'acceptation est requise. Le certificat identifie la Fondation SignPath en tant qu'éditeur ; le service relie les versions vérifiées au référentiel source. Le projet a demandé la signature de la Fondation SignPath. L'application est en attente et les téléchargements GitHub ne sont pas signés.

Avant de postuler, publiez la source sous sa licence. Gardez le projet à jour. Publiez une version, documentez l'application et rendez la version vérifiable. Signez uniquement vos propres fichiers de projet. Continuez à signer les informations d’identification hors du référentiel.

La politique de confidentialité [](../../PRIVACY.md) couvre l'application, sa vérification de mise à jour GitHub et le runtime WebView2 de GuideViewer.

## Pendant que la demande est en attente

- Vérifiez que le référentiel public dispose d'une version Windows réussie sous **Actions**. Utilisez ce flux de travail comme base de l'intégration SignPath.
- Confirmez les personnes responsables des validations, des révisions et des signatures des approbations. Activez l'authentification multifacteur pour tous les membres de l'équipe sur GitHub et SignPath.
- Demandez à SignPath de confirmer comment les binaires du SDK WebView2 et le programme d'installation de Inno Setup doivent être gérés. Les packages incluent les binaires et les chargeurs du SDK de Microsoft. L'application utilise le runtime WebView2 installé sur le PC de l'utilisateur. Répertoriez ces dépendances dans l’application et conservez leurs mentions.
- Ajoutez un lien **Code signature Policy** vers les notes de version GitHub pour la page de téléchargement fournie dans l'application. Indiquez le statut de signature réel de ces téléchargements.

## Après que SignPath ait accepté le projet

- Ajoutez l'attribution SignPath approuvée et les noms ou groupes d'autorisations confirmés pour les validateurs, les réviseurs et les approbateurs à cette page.
- Configurez SignPath pour accepter les artefacts de la version GitHub Actions approuvée. Restreindre la signature aux binaires du projet. Définissez et appliquez le nom du produit et la version du produit correspondante dans les fichiers soumis ensemble.
- Pour le ZIP, signez l’exécutable de l’application avant l’emballage. Pour le programme d'installation, placez l'exécutable de l'application signée dans le programme d'installation avant de signer le programme d'installation terminé. Obtenez une approbation manuelle pour chaque demande de signature de version.
- Vérifiez les signatures et les horodatages Authenticode, puis générez les sommes de contrôle SHA-256. Incluez un lien **Code de signature de stratégie** et l'état de signature dans les notes de version. Vérifiez les fichiers téléchargés et les hachages avant de les publier.

Pour les téléchargements directs GitHub, une signature valide identifie l'éditeur et protège l'intégrité du binaire. Un nouveau téléchargement peut toujours déclencher SmartScreen pendant que la réputation se construit. Pour une première distribution via le Store, Microsoft signe les packages MSIX acceptés.

Pour un certificat de signature de code de confiance publique disponible via le magasin de certificats Windows de l'utilisateur actuel, utilisez `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` pour le ZIP portable. Utilisez les mêmes paramètres avec `scripts/build-installer.ps1` pour signer à la fois l'exécutable de l'application et le programme d'installation généré. L'approbation SignPath nécessite son propre flux de travail de signature.

Lisez les conditions officielles du [](https://signpath.org/terms.html), le site Web de la Fondation [](https://signpath.org/) et les instructions [SmartScreen de Microsoft](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation). Vérifié le 24 novembre 2026. Pour l'itinéraire Store, voir [MSIX et Store](MSIX-AND-STORE.md).
