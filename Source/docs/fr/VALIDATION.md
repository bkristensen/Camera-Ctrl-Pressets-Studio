# Validation pour 0.7.0-beta.3

*Écrit pour Camera Control Studio 1.1.0.*

La version Windows Release utilise des dépendances verrouillées. Parité de traduction et tests automatisés réussis. Les tests couvrent le mouvement, les préréglages, la langue du profil, la persistance, la journalisation et le HRESULT de caméra non prête signalé après la mise en veille prolongée.

Le ZIP portable extrait a été testé dans les processus Windows x86 et x64. Tous deux ont ouvert et rouvert les guides d'utilisation quotidienne et de configuration, affiché leurs captures d'écran et imprimées sur PDF. Le x64 PDF avait le titre, la version, la date et le nombre de pages correct sur les cinq pages. Le bouton **?** était présent lors de l'initialisation au moment du design de WinForms. Le chargeur ARM64 a été vérifié pour l'architecture binaire correcte.

Le profil English Standard a été capturé avec une caméra de démonstration. Les 12 captures d'écran en pleine fenêtre couvrent les deux états de la fenêtre principale, chaque onglet de paramètres et chaque sous-onglet. Les liens locaux dans les guides et les README ont été vérifiés par rapport aux chemins des documents packagés.

## Champ de vision Brio et Zoom

Utilisez un Brio physique qui propose 65°, 78° et 90°. Notez le modèle, le micrologiciel, le pilote, les versions de l'application et le mode vidéo utilisé par Zoom.

1. Démarrez Zoom et utilisez une scène fixe avec des points de référence visibles près des bords. Confirmez le cadrage le plus large à 90° et le zoom minimum à l'aide de Logitech Brio Control.
2. Démarrez Camera Control Studio avec **Ne rien changer** et confirmez que l'appareil conserve son FOV. Redémarrez avec **Le plus large disponible**, puis répétez avec un FOV spécifié et avec la confirmation de diffusion activée.
3. Une fois les trois applications en cours d'exécution, modifiez le panoramique, l'inclinaison, le zoom et FOV dans Logitech Brio Control. Confirmez les lectures mises à jour dans Camera Control Studio avec le manuel FOV désactivé et activé.
4. Remplacez FOV par Camera Control Studio. Observez quand Logitech Brio Control met à jour son propre affichage. Enregistrez et rappelez les préréglages pour chaque FOV, y compris un ancien préréglage sans FOV.
5. À partir d'une photo étroite et agrandie, choisissez **Cadre complet** et comparez l'image avec la référence de l'étape 1. Répétez l'opération pendant une transition prédéfinie et avec Logitech Brio Control déjà ouvert.
6. Fermez Camera Control Studio et vérifiez si Logitech Brio Control peut toujours changer de caméra. Répétez dans l’ordre inverse de lancement de l’application. Joignez le journal de diagnostic et notez toute différence entre l'image en direct et les valeurs signalées.
7. À 65° et avec un zoom minimum, appuyez une fois sur Zoom arrière. Vérifiez que FOV passe à 78° et l'étape suivante effectue un zoom arrière. Répéter à 78°. Vérifiez que le zoom arrière s'arrête uniquement à 90° et au zoom minimum. Testez le chemin inverse pour le zoom avant.
8. Sur un bord panoramique horizontal, appuyez à nouveau sur la même flèche. Vérifiez si un FOV plus large et un réglage du zoom permettent le mouvement sans perdre la photo. Si aucune vue ne vous aide, vérifiez que le cadrage d'origine revient. Désactivez la fonction et confirmez que le FOV reste en place.

Inno Setup 7 a compilé le programme d'installation avec les guides pour les 12 langues et les chargeurs x86, x64 et ARM64. Les deux packages ne sont pas signés et les hachages SHA-256 sont fournis. Une installation et une désinstallation de compte propre, un cycle d'hibernation de la caméra physique et un test d'exécution ARM64 sont en attente. Consultez la liste de contrôle de version [](RELEASE-CHECKLIST.md) et le guide de construction et de publication [](BUILD-AND-RELEASE.md).
