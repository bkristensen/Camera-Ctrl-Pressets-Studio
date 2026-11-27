# Traduire Camera Control Studio

*Écrit pour Camera Control Studio 1.0.1.*

## Modifier une langue existante

1. Ouvrez `Source/Shared/Locales/<code>.xml`. Utilisez `en.xml` comme référence pour le contexte et les clés.
2. Modifiez le texte entre les balises `<text>`. Conservez intacts les clés, les espaces réservés tels que `{0}` et les identifiants stockés. Enregistrez au format UTF-8. En XML, écrivez une esperluette littérale sous la forme `&amp;`.
3. Si vous modifiez également l'aide, modifiez le dossier correspondant dans `Source/docs`. Testez les liens vers d’autres guides et images dans Guide Viewer.

## Ajouter une langue

1. Copiez `en.xml` dans un fichier nommé pour le nouveau code de langue et traduisez toutes les valeurs.
2. Enregistrez le code et le nom d'affichage au même index dans `LanguageCodes` et `LanguageNames` dans `Source/Shared/UiText.cs`. Mettez à jour la gestion de droite à gauche, le cas échéant.
3. Ajoutez la langue de l'application au manifeste MSIX. Si vous ajoutez un guide de l'utilisateur, suivez la structure d'un dossier existant sous `Source/docs`, ajoutez-le à `USER-GUIDES.md` et vérifiez que Guide Viewer l'ouvre.

## Vérifier et soumettre

À partir de `Source`, exécutez `powershell -File scripts/check-translations.ps1` et créez. Démarrez l'application avec `--lang=<code>`. Vérifiez les boîtes de dialogue dans la plus petite taille de fenêtre et naviguez avec le clavier. Testez les longues étiquettes, l’orientation du texte et les messages qui remplissent des espaces réservés.

Suivez le guide du projet [GitHub](GITHUB.md) pour ouvrir une pull request. Nommez le langage et le code, résumez les modifications et indiquez ce que vous avez testé. Incluez une capture d'écran si cela aide les évaluateurs à évaluer la mise en page. Ajoutez des modifications de suivi à la même branche lors de la révision.
