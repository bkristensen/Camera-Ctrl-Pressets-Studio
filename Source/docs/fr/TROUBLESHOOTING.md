# Dépannage

*Rédigé pour Camera Control Studio 1.1.0.*

## Sommaire

- [1. Applications vidéo et champ de vision](#1-applications-vid%C3%A9o-et-champ-de-vision)
  - [Champ de vision réduit dans Zoom Workplace](#champ-de-vision-r%C3%A9duit-dans-zoom-workplace)
- [2. Connexion et compatibilité](#2-connexion-et-compatibilit%C3%A9)
  - [Caméra introuvable ou indisponible](#cam%C3%A9ra-introuvable-ou-indisponible)
  - [Commandes absentes ou différentes selon l’application](#commandes-absentes-ou-diff%C3%A9rentes-selon-lapplication)
- [3. Mouvement et positions enregistrées](#3-mouvement-et-positions-enregistr%C3%A9es)
  - [Mouvement bloqué, saccadé ou inversé](#mouvement-bloqu%C3%A9-saccad%C3%A9-ou-invers%C3%A9)
  - [Champ de vision ou position enregistrée incorrects](#champ-de-vision-ou-position-enregistr%C3%A9e-incorrects)

## 1. Applications vidéo et champ de vision

### Champ de vision réduit dans Zoom Workplace

**Symptômes**\
Les bords de la scène manquent. Le zoom arrière et le déplacement latéral s’arrêtent trop tôt. Les positions enregistrées changent de cadrage. **Optimize for video sharing** est désactivé. Le problème apparaît aussi dans Logitech BRIO Control.

**Cause**\
**Confirmé dans le cas signalé.** Le **Efficiency Mode** était activé dans Zoom Workplace. Sa désactivation suivie d’un redémarrage de Zoom a rétabli le champ initial. Camera Control Studio et les positions sont restés intacts. Le mécanisme technique reste inconnu.

**Solution**

1. Dans Zoom Workplace, ouvrez votre photo de profil → **Settings → Meeting & webinar → Advanced**.
2. Dans **Performance**, désactivez **Efficiency Mode**. Si le réglage est verrouillé, contactez votre administrateur Zoom.
3. Quittez Zoom Workplace complètement. Rouvrez-le et conservez vos positions pendant le test.

**Vérification**\
Vérifiez que les bords sont de nouveau visibles. Rappelez vos positions et comparez le cadrage en direct.

**Logiciels et appareils concernés**\
Windows, Zoom Workplace, Logitech BRIO, Logitech BRIO Control et Camera Control Studio 0.7.0-beta.8. Les versions de Zoom, du pilote et du micrologiciel n’ont pas été relevées.

**Sources**\
[Zoom Support](https://support.zoom.com/hc/en/article?id=zm_kb&sysparm_article=KB0080877) · [GitHub #5](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/5)

## 2. Connexion et compatibilité

### Caméra introuvable ou indisponible

**Symptômes**\
La caméra manque dans la liste ou ne répond plus après la veille.

**Cause**\
**Causes possibles.** Câble USB, autorisations Windows, autre application ou confirmation de diffusion en attente.

**Solution**

1. Vérifiez le câble USB. Si une confirmation est demandée, démarrez la diffusion avant de choisir **Oui**.
2. Vérifiez les autorisations de la caméra dans Windows. Testez l’aperçu dans Caméra Windows, puis fermez cette application.
3. Essayez une commande après la veille. Si la caméra enregistrée manque, choisissez-la de nouveau avec le bouton central. Par défaut, double-cliquez sur le bouton central pour ouvrir la liste des caméras.

![Molette avec le bouton de sélection de la caméra au centre](../images/screenshots/control%20wheel%20only.png)

**Vérification**\
Vérifiez le nom en bas de la fenêtre et testez une commande sur l’image en direct.

**Logiciels et appareils concernés**\
Windows et caméras intégrées ou USB. L’accès simultané dépend du pilote.

**Sources**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Guides utilisateur](USER-GUIDE.md)

### Commandes absentes ou différentes selon l’application

**Symptômes**\
La caméra affiche une image, mais des commandes manquent ou leurs effets ne se voient que dans une application.

**Cause**\
**Confirmé.** Camera Control Studio utilise les commandes du pilote Windows. **Causes possibles.** Le pilote, le micrologiciel ou une autre application peut influer sur les commandes ou l’image.

**Solution**

1. Sélectionnez la même caméra physique. Comparez les aperçus, une application à la fois.
2. Fermez les autres outils de caméra. Rouvrez votre application vidéo et essayez une commande disponible.
3. Consultez Windows Update et l’assistance du modèle exact. Suivez les consignes du fabricant pour toute mise à jour du pilote ou du micrologiciel.

**Vérification**\
Vérifiez quelle commande agit dans l’application utilisée. Notez les versions si vous signalez le problème.

**Logiciels et appareils concernés**\
Windows, pilote, micrologiciel et applications vidéo ou de caméra.

**Sources**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K)

## 3. Mouvement et positions enregistrées

### Mouvement bloqué, saccadé ou inversé

**Symptômes**\
Le mouvement ou le zoom s’arrête trop tôt. Les transitions avancent par à-coups ou les flèches vont dans le mauvais sens.

**Cause**\
**Confirmé.** Le pilote fixe la plage et le pas. L’application peut inverser les flèches.

Si le sens change à l’ouverture de Logitech Camera Settings ou de Logitech BRIO Control, une autre application a peut-être affecté les commandes. D’après le signalement, l’image restait à l’endroit dans Zoom. L’axe et la cause restent à confirmer.

**Solution**

1. Utilisez le bouton carré au-dessus des loupes pour rétablir le champ le plus large et le zoom minimal. Refaites le test.
2. Dans **Paramètres → Panoramique**, inversez uniquement l’axe concerné.
3. Pour affiner le mouvement, essayez **Ctrl** et une flèche. Une transition plus longue ne réduit pas les pas du pilote.
4. Hors diffusion, fermez l’outil Logitech. Testez les deux axes dans la même application vidéo, au même niveau de zoom. Ouvrez les outils Logitech un à un pour voir si le changement revient.

**Vérification**\
Vérifiez le sens des flèches et les limites sur l’image en direct.

**Logiciels et appareils concernés**\
Caméras avec commandes de mouvement ou de zoom sous Windows. Les limites dépendent du modèle et du mode vidéo.

**Sources**\
[GitHub #3](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/3) · [GitHub #4](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/4) · [Guides utilisateur](USER-GUIDE.md)

### Champ de vision ou position enregistrée incorrects

**Symptômes**\
Le cadrage est trop serré, une position a changé ou aucun angle n’est proposé.

**Cause**\
**Confirmé.** Les positions appartiennent à une caméra et un profil. Une ancienne position sans angle conserve l’angle actuel. Certains pilotes ne proposent aucun angle.

**Solution**

1. Vérifiez le profil et la caméra. Rétablissez le cadre complet et observez l’image.
2. Dans **Vidéo → Champ de vision**, vérifiez le champ au démarrage. Autorisez le réglage manuel si le pilote propose des angles.
3. Rappelez la position. Si le champ se réduit seulement avec Zoom ouvert, suivez le premier article. Conservez la position pendant le test.

**Vérification**\
Comparez l’image au cadrage attendu. Rappelez la position après avoir retrouvé le champ initial.

**Logiciels et appareils concernés**\
Profils et positions de Camera Control Studio. Les angles dépendent de la caméra et du pilote.

**Sources**\
[Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K) · [Guides utilisateur](USER-GUIDE.md)

---

Besoin d’aide ? Relisez les journaux avant de les partager. Indiquez le modèle, les versions et les résultats de vos essais.

[Signaler un problème](../en/ISSUES.md) · [Guides utilisateur](USER-GUIDES.md)
