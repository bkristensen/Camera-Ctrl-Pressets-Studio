# MSIX et distribution en magasin

Utilisez `scripts/build-msix.ps1` et le modèle de manifeste pour créer un package MSIX. Microsoft examine le package avant qu'il puisse apparaître dans le Store. L'exécutable portable n'est pas signé.

## Choisissez un itinéraire

| Itinéraire | Signature |
| --- | --- |
| MicrosoftStore avec MSIX | Microsoft signe le package Store accepté. Vous n'achetez pas de certificat de signature pour cet itinéraire. |
| Microsoft Store avec un programme d'installation EXE/MSI | La signature de l'éditeur est requise ; le Store ne signe pas à nouveau ce programme d'installation. |
| Téléchargement direct du MSIX | Le package doit avoir une signature approuvée sur l'appareil de l'utilisateur. |
| EXE portable direct | Une build non signée peut produire des avertissements. La signature identifie l'éditeur mais ne garantit pas une réputation immédiate. |

## Liste de contrôle

- [ ] Enregistrez le produit et réservez son nom dans Partner Center. Copiez le nom exact du package, Publisher et PublisherDisplayName.
- [ ] Installez les outils du SDK Windows et exécutez la commande ci-dessous en utilisant ces valeurs d'identité.
- [ ] Validez le manifeste et testez l'accès à la caméra, l'installation, la mise à niveau, la désinstallation, le stockage du profil et la langue sous Windows. Le modèle déclare le fonctionnement du bureau/de confiance totale et l'accès à la webcam. Vérifiez qu'ils correspondent aux exigences de certification actuelles.
- [ ] Préparez la liste, la politique de confidentialité, le lien d'assistance, les informations de licence, la tranche d'âge et de vraies captures d'écran. Expliquer les commandes requises du pilote de caméra. Exécutez les vérifications actuelles de certification des applications Windows.
- [ ] Téléchargez le candidat sur Partner Center, corrigez les problèmes de validation, soumettez et attendez la certification. Ne décrivez pas l’application comme approuvée par le Store avant son approbation.

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.PRODUCT.IDENTITY' -Publisher 'CN=YOUR-PARTNER-CENTER-ID' -PublisherDisplayName 'YOUR DISPLAY NAME'
```

Le résultat est un candidat non signé à la soumission au Store, et non un package de chargement latéral public prêt à installer. Le script valide les champs d'identité requis et utilise une version x64 explicite. Choisissez une nouvelle version du package en quatre parties pour chaque version, avec la dernière partie zéro pour la soumission au Store.

Références officielles, vérifiées le 18 novembre 2026 :

- [Options de signature de code Microsoft](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options)
- [Liste de contrôle de préparation au magasin](https://learn.microsoft.com/en-us/windows/apps/publish/store-readiness-checklist)
- [MSIX guide de signature](https://learn.microsoft.com/en-us/windows/msix/package/sign-msix-package-guide)

Voir également [SignPath](SIGNPATH.md). Les règles actuelles d’éligibilité et de soumission doivent être vérifiées à nouveau avant la publication.
