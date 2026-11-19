# Maintain or add a language

## Files and precedence

App text lives in SharedUi/Locales/en.xml, da.xml and de.xml. Each text element has
an exact English key and a translated value. The resources are embedded in the EXE;
editing XML beside an installed EXE has no effect. Rebuild after changes.

Precedence: recognised --lang option, saved language.txt preference, then Windows
display language for Automatic. Unsupported display languages use English. The
selection applies at restart to avoid mixing translated camera lookup keys.

## Update an existing translation

1. Find the English key in all locale files. Change only the translated value.
2. Preserve leading/trailing spaces, line breaks, placeholders and file-filter syntax.
   Escape XML characters such as & as &amp;. Device names and user presets stay unchanged.
3. Run scripts/check-translations.ps1, then scripts/build.ps1.
4. Start with --lang=de (or da/en) and inspect all settings/dialogs at multiple DPI values.
5. Update the matching guides under docs/en, docs/da and docs/de; submit a pull request.

## Add another language

Copy en.xml to the language code, for example fr.xml, and translate every value.
Set the root language attribute. Add the code and its self-name to LanguageCodes
and LanguageNames in SharedUi/UiText.cs in the same order. Resources are included
by wildcard in the application and test projects. ResolveLanguage matches the
base language; regional variants need explicit design changes if required.

Add tests for explicit selection, Automatic, unsupported language and English
fallback. Add the language to the MSIX Resources manifest. Configure a corresponding
Inno Setup language and prerequisite message if available. Create matching guide
files and update the documentation index. Build and test on Windows before release.

## Add or change app text

Wrap new text in UiText.T("English", "Dansk") and add the exact English key to all
locale files. When changing an English key, change code and resources together.
The key checker compares resource files, so reviewers must also check new source
strings are represented. German/other missing keys fall back to English; Danish
can use the inline fallback. This is resilience, not permission to ship incomplete
translations. Keep application messages separate from OS/vendor error text.

The authoritative MIT LICENSE remains English. Translated license explanations
are guidance and do not replace or modify its terms.
