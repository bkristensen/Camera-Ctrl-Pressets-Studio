# MSIX and Store distribution

Use `scripts/build-msix.ps1` and the manifest template to build an MSIX package. Microsoft reviews the package before it can appear in the Store. The portable executable is unsigned.

## Choose a route

| Route | Signing |
| --- | --- |
| Microsoft Store with MSIX | Microsoft signs the accepted Store package. You do not buy a signing certificate for this route. |
| Microsoft Store with an EXE/MSI installer | Publisher signing is required; the Store does not re-sign that installer. |
| Direct MSIX download | The package must have a signature trusted on the user's device. |
| Direct portable EXE | An unsigned build can produce warnings. Signing identifies the publisher but does not promise immediate reputation. |

## Checklist

- [ ] Register the product and reserve its name in Partner Center. Copy the exact package Name, Publisher and PublisherDisplayName.
- [ ] Install the Windows SDK tools and run the command below using those identity values.
- [ ] Validate the manifest and test camera access, install, upgrade, uninstall, profile storage and language on Windows. The template declares desktop/full-trust operation and webcam access. Verify these match current certification requirements.
- [ ] Prepare the listing, privacy policy, support link, licence information, age rating and real screenshots. Explain required camera-driver controls. Run the current Windows app certification checks.
- [ ] Upload the candidate to Partner Center, fix validation issues, submit and wait for certification. Do not describe the app as Store-approved before approval.

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.PRODUCT.IDENTITY' -Publisher 'CN=YOUR-PARTNER-CENTER-ID' -PublisherDisplayName 'YOUR DISPLAY NAME'
```

The output is an unsigned Store-submission candidate, not a ready-to-install public sideload package. The script validates required identity fields and uses an explicit x64 build. Choose a new four-part package version for each release, with the final part zero for Store submission.

Official references, checked 18 November 2026:

- [Microsoft code-signing options](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options)
- [Store readiness checklist](https://learn.microsoft.com/en-us/windows/apps/publish/store-readiness-checklist)
- [MSIX signing guide](https://learn.microsoft.com/en-us/windows/msix/package/sign-msix-package-guide)

See also [SignPath](SIGNPATH.md). Current eligibility and submission rules should be checked again before publication.
