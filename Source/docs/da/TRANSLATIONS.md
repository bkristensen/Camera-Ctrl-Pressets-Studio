# Oversæt Camera Control Studio

*Skrevet til Camera Control Studio 1.0.1.*

## Ret en oversættelse

Åbn sprogets fil i `Source/Shared/Locales/<kode>.xml`. Brug `en.xml` som reference. Ret teksten mellem `<text>`-mærkerne, og behold nøgler, pladsholdere som `{0}` og gemte id'er. Gem filen som UTF-8. Skriv `&amp;` for et ampersand i XML.

Hvis vejledningen også skal rettes, findes den i den tilsvarende sprogmappe under `Source/docs`. Afprøv links og billeder i vejledningsviseren.

## Tilføj et sprog

1. Kopiér `en.xml` til en ny fil med sprogkoden som navn. Oversæt alle værdier.
2. Tilføj kode og visningsnavn på samme placering i `LanguageCodes` og `LanguageNames` i `Source/Shared/UiText.cs`. Opdater håndteringen af tekstretning, hvis sproget skrives fra højre mod venstre.
3. Registrér sproget i MSIX-manifestet. Hvis du tilføjer en vejledning, følg strukturen i en eksisterende sprogmappe under `Source/docs`, registrér den i `USER-GUIDES.md`, og kontrollér den i vejledningsviseren.

## Afprøv og send til gennemgang

Kør `powershell -File scripts/check-translations.ps1` fra `Source`, og byg løsningen. Start programmet med `--lang=<kode>`. Prøv dialogbokse i det mindste vindue. Brug tastaturet. Tjek lange etiketter, tekstretning og beskeder med udfyldte pladsholdere.

Følg [GitHub-vejledningen](GITHUB.md) for at oprette en pull request. Angiv sprog, kode, ændringer og hvad du har afprøvet. Vedhæft gerne et billede, hvis det hjælper med at vurdere layoutet. Send eventuelle rettelser til samme branch under gennemgangen.
