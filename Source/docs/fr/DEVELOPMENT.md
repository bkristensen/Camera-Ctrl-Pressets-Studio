[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Modifier l'interface et les traductions

*Écrit pour Camera Control Studio 1.1.1.*

## Ouvrez le bon fichier

Ouvrez `Camera-Control-Pressets-Studio.sln`. Dans l’Explorateur de solutions, générez la solution, puis ouvrez `CameraControlStudio → Forms → MainForm.cs`. Appuyez sur **Shift+F7** pour le Windows Forms Designer. Ne sélectionnez pas `MainForm.Designer.cs` comme aire de conception. Utilisez F7 pour revenir au code.

Installez la charge de travail de bureau .NET Visual Studio à partir de `.vsconfig`. L'application est WinForms sur .NET Framework 4.8. Le concepteur WPF n'est pas utilisé.

## Changer la mise en page

Utilisez le panneau Propriétés pour modifier `Dock`, `Margin`, `Padding`, les tailles de lignes et les tailles de colonnes. Utilisez le plan du document pour sélectionner des panneaux imbriqués. La mise en page principale utilise des tableaux et un `SplitContainer` ; évitez les coordonnées d’écran codées en dur.

Chaque fenêtre hérite de `AppForm` et possède un constructeur public sans paramètres. Placez la mise en page dans `.Designer.cs` et les événements dans le fichier `.cs` correspondant. Définissez la propriété `Icon` du formulaire dans le Concepteur. Vérifiez la disposition dans Visual Studio et dans l'application en cours d'exécution.

La molette de la caméra et les boutons bleus sont dessinés en `Controls/BlueControls.cs`. Le séparateur vertical et les commandes de zoom se trouvent dans `Controls/MainViewControls.cs`. Le concepteur peut déplacer et dimensionner ces contrôles. Les images d'icônes prédéfinies et leurs titres neutres se trouvent dans `Assets/PressetIcons.resx` et `Controls/PresetIcons.cs` les charge. L'onglet Vidéo contient des curseurs créés par des concepteurs. L'application en cours d'exécution affiche les curseurs pris en charge par le pilote de l'appareil photo.

## Définir les valeurs par défaut

Modifiez `Shared/Models.cs`, dans `SavedData.Defaults`, pour les paramètres du nouveau profil. Modifiez `ProfileLayout` pour la mise en page initiale. Les profils existants conservent leurs valeurs enregistrées. Créez un nouveau profil pour tester de nouvelles valeurs par défaut ; ne supprimez pas les profils utilisateur pour les forcer.

## Mettre à jour une traduction

Modifiez la valeur correspondante dans le fichier de langue concerné sous `Shared/Locales`. Gardez le `key` inchangé. XML nécessite `&amp;` pour une esperluette. Tous les fichiers doivent avoir les mêmes clés. Conservez les identifiants de caméra et d’icône stockés inchangés.

Exécutez `scripts/check-translations.ps1`, puis compilez. Testez avec `"Camera Control Studio.exe" --lang=de` ou le sélecteur de langue dans les paramètres. Redémarrez après avoir modifié le choix de langue enregistré.

## Ajouter une langue

Copiez `en.xml` dans un nouveau fichier de code de langue. Traduisez chaque valeur et enregistrez le code et le nom dans `Shared/UiText.cs`. Le vérificateur de traduction et les tests de paramètres régionaux récupèrent automatiquement le nouveau fichier et l'enregistrement. Déclarez la langue de l'application dans le manifeste MSIX.

La sélection automatique utilise le préfixe de langue Windows. Les langues non prises en charge reviennent à l'anglais. Testez les fenêtres étroites et les étiquettes longues ; la traduction ne doit pas cacher les contrôles.
