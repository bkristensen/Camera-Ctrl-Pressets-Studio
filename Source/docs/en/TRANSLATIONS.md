# Translate Camera Control Studio

*Written for Camera Control Studio 1.0.0.*

## Edit an existing language

1. Open `Source/Shared/Locales/<code>.xml`. Use `en.xml` as the reference for context and keys.
2. Edit the text between the `<text>` tags. Keep the keys, placeholders such as `{0}`, and stored IDs intact. Save as UTF-8. In XML, write a literal ampersand as `&amp;`.
3. If you also change the help, edit the matching folder in `Source/docs`. Test links to other guides and images in Guide Viewer.

## Add a language

1. Copy `en.xml` to a file named for the new language code and translate all values.
2. Register the code and display name at the same index in `LanguageCodes` and `LanguageNames` in `Source/Shared/UiText.cs`. Update right-to-left handling where applicable.
3. Add the app language to the MSIX manifest. If you add a user guide, follow the structure of an existing folder under `Source/docs`, add it to `USER-GUIDES.md`, and verify that Guide Viewer opens it.

## Verify and submit

From `Source`, run `powershell -File scripts/check-translations.ps1` and build. Start the app with `--lang=<code>`. Check dialogs at the smallest window size and navigate with the keyboard. Test long labels, text direction and messages that fill in placeholders.

Follow the [GitHub project guide](GITHUB.md) to open a pull request. Name the language and code, summarize the changes, and state what you tested. Include a screenshot if it helps reviewers evaluate the layout. Add follow-up edits to the same branch during review.
