# Descriptif du projet

*Écrit pour Camera Control Studio 1.1.1.*

Camera Control Studio enregistre et rappelle les prises de vue de la caméra sous Windows. Utilisez-le dans un studio, une salle de classe ou une réunion en direct. Votre application de capture gère la vidéo.

## Comportement de base

Utilisez l'anneau intérieur pour les mouvements normaux et l'anneau extérieur pour les mouvements rapides. Chaque anneau a des flèches diagonales. Double-cliquez sur le bouton central pour choisir une caméra ou faites-la glisser pour la diriger. Vous pouvez modifier les deux gestes sous Paramètres → Panoramique. Les boutons de zoom se déplacent selon le pas défini. Le curseur définit le zoom dans le FOV actuel. L'application n'ouvre aucun aperçu vidéo. Mouvement [Set et FOV](SETUP.md#d%C3%A9finir-le-champ-de-vision)

Les positions permanentes stockent un nom, un ID de symbole stable et les valeurs de panoramique, d'inclinaison, de zoom et FOV signalées par le conducteur. Les symboles combinés montrent les sujets de gauche à droite. Les postes permanents apparaissent en premier en gras. Les positions temporaires n'ont pas d'icône et durent jusqu'à ce que vous fermiez l'application. Utilisez Play ou double-cliquez pour rappeler l’un ou l’autre type.

Une interpolation douce demande des coordonnées intermédiaires. Une ligne est jaune pendant la transition. Il est vert uniquement lorsque la relecture correspond à la cible. Une action manuelle, une annulation ou un échec de transition efface cet état. Le pilote détermine la granularité et la latence réelles du mouvement.

Les profils contiennent la sélection de la caméra, les positions permanentes, les valeurs d'image, les taux de mouvement manuels, la durée ou la vitesse de transition prédéfinie, l'inversion de direction, la confirmation de la caméra, la disposition des fenêtres et la langue. La modification des profils redémarre l'application avec la langue du profil sélectionné. La confirmation facultative de la caméra bloque l’énumération et la connexion jusqu’à ce que l’utilisateur confirme que la diffusion en continu a commencé.

## Architecture

`CameraControlStudio` est une application .NET Framework 4.8 WinForms. Mise en attente `Forms/*.Designer.cs`. `Forms/*.cs` maintient la gestion des événements. `AppForm` est la base commune. `Controls` contient un rendu vectoriel. `Camera.cs` utilise les interfaces de caméra et de processus vidéo Windows DirectShow sans graphique de capture.

`Shared` contient des modèles, la persistance, la traduction, l'arithmétique des mouvements, les positions de session et la logique d'état. `Tests` est une suite de régression de console .NET 8. Les profils utilisent le remplacement de fichiers atomiques, les sauvegardes et les vérifications de révision. Le stockage de profil 0.6 existant est conservé ; les paramètres de démarrage du programme obsolètes sont ignorés et supprimés lors de la sauvegarde.

## Portée et compatibilité

Le propriétaire a signalé que l'application précédente fonctionnait sur la configuration de son ordinateur portable et de sa caméra. Cette réécriture 0.7 a compilé et réussi les tests logiques mais n'a pas été testée sur le matériel physique dans cet environnement de construction. Aucun appareil photo ou logiciel de capture supplémentaire n'est prétendument testé.

D'autres caméras Windows peuvent fonctionner lorsque leurs pilotes implémentent les contrôles standard nécessaires. L'accès simultané avec un logiciel de capture, de réunion ou de streaming dépend du pilote. Les caméras virtuelles ne peuvent exposer aucun contrôle physique. Il ne s'agit pas d'un contrôleur réseau-PTZ générique. Linux et macOS peuvent créer des hôtes via le flux de travail Docker, mais ne peuvent pas exécuter cette interface utilisateur Windows de manière native.

Veuillez signaler les combinaisons réussies, notamment la version de Windows, le modèle d'appareil photo, la version du pilote, l'application de capture et les commandes qui ont fonctionné. Signalez également les limitations afin que la liste de compatibilité puisse être fondée sur des preuves.

## Travaux futurs possibles

Automatisation de la régression de l'interface utilisateur Windows ; une meilleure accessibilité et découverte du clavier ; importation/exportation facultative de profils individuels ; matrice matérielle vérifiée ; autorisations signées après approbation ; backends de caméra supplémentaires s’ils préservent le flux de travail de base simple. Ce sont des idées, pas des fonctionnalités livrées.
