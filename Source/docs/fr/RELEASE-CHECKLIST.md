# Liste de contrôle de sortie

*Écrit pour Camera Control Studio 1.1.0.*

Utilisez le guide de construction et de publication [](BUILD-AND-RELEASE.md) pour les commandes et les chemins de sortie.

## Contrôles automatisés

- [ ] Exécutez `scripts/build.ps1` sur une extraction propre avec des dépendances verrouillées.
- [ ] Vérifiez que chaque paramètre régional possède les mêmes clés uniques et non vides. La build exécute la vérification de la traduction et les tests de paramètres régionaux.
- [ ] Passez en revue la version source et binaire, l'identité du package et les notes de version.

## Vérifications des fenêtres et des caméras

- [ ] Lancez à 900 × 500 et vérifiez le minimum de 670 × 450. Redimensionnez, agrandissez et restaurez à une mise à l'échelle d'affichage de 100 %, 125 %, 150 % et 200 %. Vérifiez le pied de page, les entrées, les icônes combinées et les glyphes de zoom.
- [ ] Ouvrez les paramètres à droite. Faites glisser le séparateur. Fermez et rouvrez. Testez une mise en page enregistrée après avoir déconnecté un moniteur.
- [ ] Rappeler une position : jaune pendant le mouvement, vert seulement après relecture correspondante. Pan/inclinaison/zoom efface la couleur active. Déconnexion lors d'une transition : pas de faux vert. Les icônes ne disparaissent jamais après confirmation.
- [ ] Testez chaque icône combinée, y compris chaque ordre inversé. Testez les boutons de zoom, le curseur de zoom et les inversions des deux sens.
- [ ] Sur un Brio pris en charge, testez le FOV automatique dans les deux directions de zoom et sur les bords panoramiques gauche et droit. Suivez les étapes du test [Brio](VALIDATION.md#champ-de-vision-brio-et-zoom).
- [ ] Activez la confirmation de la caméra. Redémarrage. Vérifiez qu'il n'y a pas d'énumération ou de connexion de caméra avant de répondre Oui. Testez Non, puis réessayez à partir d'un bouton panoramique/zoom. Changement de profil de test et sélection de source.

## Données et édition

- [ ] Sauvegardez le fichier de profil 0.6 avant la migration. Confirmez que les préréglages et les identifiants d'icônes survivent. Confirmez que les paramètres de programme externe obsolètes sont absents après l'enregistrement.
- [ ] Testez les nouveaux profils/copiez/renommez, restaurez la mise en page et conservez le comportement de mise en page actuel. Vérifiez que les postes temporaires restent réservés aux sessions.
- [ ] Testez les paramètres automatiques/manuels et les valeurs par défaut de l'image avec l'appareil photo réel.
- [ ] Ouvrez chaque formulaire avec Shift+F7 dans Visual Studio. Ajustez une propriété de mise en page, enregistrez et reconstruisez. Le dessin personnalisé reste dans les classes de contrôle.
- [ ] Ouvrez la visionneuse de guide à partir du ZIP extrait dans un processus Windows 64 bits. Ouvrez-le à nouveau et imprimez un guide sur PDF. Confirmez que le programme d'installation et ZIP incluent le chargeur natif WebView2 correspondant sous `runtimes` pour chaque architecture prise en charge.
- [ ] Consultez l'index du guide de l'utilisateur [](USER-GUIDES.md) et confirmez que chaque guide linguistique est présent dans les deux packages. Consultez les captures d'écran du guide [](SCREENSHOTS.md) pour connaître la couverture complète des onglets et des sous-onglets, le recadrage et le chevauchement.

## Distribution

- [ ] Pour une version portable, utilisez `scripts/build.ps1`. Pour un programme d'installation Inno Setup, utilisez `scripts/build-installer.ps1`, qui exécute également la version portable et les tests. Vérifiez la version affichée par le programme d'installation et l'application.
- [ ] Pour une version Store, suivez la liste de contrôle [Store](MSIX-AND-STORE.md). Testez les types de packages que vous prévoyez de publier sur Windows.
- [ ] Si vous signez, vérifiez les signatures Authenticode sur l'exécutable de l'application et le programme d'installation, le cas échéant. Étiquetez clairement les téléchargements non signés. Générez des hachages SHA-256 après la signature finale et l'empaquetage.
- [ ] Lien vers la stratégie de signature de code [](SIGNPATH.md) dans les notes de version GitHub. Pour les versions SignPath, vérifiez la provenance de la construction et l’approbation manuelle, et créditez SignPath pour les fichiers signés.
- [ ] Publiez ensemble la source, la licence, les notes de version, les hachages et les binaires. Ne réclamez pas de tests matériels non effectués ou l’approbation du magasin.
