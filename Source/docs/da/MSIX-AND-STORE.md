# MSIX og butiksdistribution

Brug `scripts/build-msix.ps1` og manifestskabelonen til at bygge en MSIX-pakke. Microsoft gennemgår pakken, før den kan vises i butikken. Den bærbare eksekverbare er usigneret.

## Vælg en rute

| Rute | Underskriver |
| --- | --- |
| Microsoft Store med MSIX | Microsoft underskriver den accepterede Store-pakke. Du køber ikke et signeringscertifikat til denne rute. |
| Microsoft Store med et EXE/MSI-installationsprogram | Forlagets underskrift er påkrævet; butikken signerer ikke det installationsprogram igen. |
| Direkte MSIX download | Pakken skal have en signatur, der er tillid til på brugerens enhed. |
| Direkte bærbar EXE | En usigneret build kan give advarsler. Signering identificerer udgiveren, men lover ikke øjeblikkeligt omdømme. |

## Tjekliste

- [ ] Registrer produktet og reserver dets navn i Partner Center. Kopiér det nøjagtige pakkenavn, Publisher og PublisherDisplayName.
- [ ] Installer Windows SDK-værktøjerne, og kør kommandoen nedenfor ved hjælp af disse identitetsværdier.
- [ ] Valider manifestet og test kameraadgang, installer, opgrader, afinstaller, profillagring og sprog på Windows. Skabelonen erklærer desktop/full-trust drift og webcam-adgang. Bekræft, at disse stemmer overens med de aktuelle certificeringskrav.
- [ ] Forbered fortegnelsen, privatlivspolitikken, supportlink, licensoplysninger, aldersvurdering og rigtige skærmbilleder. Forklar de nødvendige kamera-driverkontroller. Kør de aktuelle Windows-app-certificeringstjek.
- [ ] Upload kandidaten til Partner Center, ret valideringsproblemer, indsend og vent på certificering. Beskriv ikke appen som butiksgodkendt før godkendelse.

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.PRODUCT.IDENTITY' -Publisher 'CN=YOUR-PARTNER-CENTER-ID' -PublisherDisplayName 'YOUR DISPLAY NAME'
```

Outputtet er en usigneret butiksindsendelseskandidat, ikke en klar til at installere offentlig sideload-pakke. Scriptet validerer påkrævede identitetsfelter og bruger en eksplicit x64-build. Vælg en ny firedelt pakkeversion for hver udgivelse, med den sidste del nul til butiksindsendelse.

Officielle referencer, kontrolleret 18. november 2026:

- [Microsoft kodesigneringsmuligheder](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options)
- [ Butiksberedskabstjekliste](https://learn.microsoft.com/en-us/windows/apps/publish/store-readiness-checklist)
- [MSIX signeringsvejledning](https://learn.microsoft.com/en-us/windows/msix/package/sign-msix-package-guide)

Se også [SignPath](SIGNPATH.md). De nuværende regler for berettigelse og indsendelse bør kontrolleres igen før offentliggørelse.
