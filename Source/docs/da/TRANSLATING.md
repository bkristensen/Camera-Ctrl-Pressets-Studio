# Opdatér eller tilføj et sprog

## Filer og prioritet

Apptekster findes i SharedUi/Locales/en.xml, da.xml og de.xml. Hvert text-element
har en præcis engelsk nøgle og en oversat værdi. Filerne indbygges i EXE-filen;
ændringer i løse XML-filer ved siden af programmet virker ikke. Byg appen igen.

Prioriteten er: genkendt --lang-parameter, gemt valg i language.txt og derefter
Windows' visningssprog ved Automatisk. Ukendte sprog giver engelsk. Sproget skiftes
først efter genstart, så kameraets oversatte opslag ikke blandes under kørsel.

## Ret en oversættelse

1. Find den engelske nøgle i sprogfilerne. Ret kun den oversatte værdi.
2. Bevar mellemrum i begyndelsen/slutningen, linjeskift, pladsholdere og filfiltersyntaks.
   Skriv XML-tegn korrekt, fx & som &amp;. Oversæt ikke enhedsnavne eller brugerens positioner.
3. Kør scripts/check-translations.ps1 og derefter scripts/build.ps1.
4. Start med --lang=de, da eller en. Kontrollér indstillinger og dialoger ved flere skaleringer.
5. Opdatér guiderne i docs/en, docs/da og docs/de, og indsend en pull request.

## Tilføj et nyt sprog

Kopiér en.xml til sprogkoden, fx fr.xml, og oversæt alle værdier. Ret attributten
language i roden. Tilføj koden og sprogets eget navn i LanguageCodes og LanguageNames
i SharedUi/UiText.cs i samme rækkefølge. Projekt- og testfiler medtager XML-filerne
automatisk. ResolveLanguage bruger basissproget; regionale varianter kræver særskilt ændring.

Tilføj tests af manuelt valg, Automatisk, ukendte sprog og engelsk fallback. Tilføj
sproget under Resources i MSIX-manifestet. Tilføj tilsvarende Inno Setup-sprog og
besked om .NET-krav, hvis sproget findes. Opret guiderne og opdatér dokumentoversigten.
Byg og afprøv på Windows før udgivelse.

## Nye apptekster

Brug UiText.T("English", "Dansk"), og tilføj den præcise engelske nøgle i alle
sprogfiler. Ændres en nøgle, skal kode og filer ændres sammen. Kontrolscriptet
sammenligner sprogfilerne; nye tekster i kildekoden skal også gennemgås. Manglende
tyske og andre oversættelser giver engelsk fallback. Dansk kan bruge teksten i
kodekaldet. Fallback er en sikkerhedsventil, ikke en erstatning for færdig oversættelse.
Windows- og driverbeskeder er ikke nødvendigvis en del af appens oversættelser.

Den gældende MIT LICENSE forbliver på engelsk. Oversatte forklaringer ændrer ikke licensen.
