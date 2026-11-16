# MSIX and Microsoft Store checklist

Status: packaging preparation only. No Store identity, signed MSIX, certification
or published listing is included. The existing EXE remains a Windows desktop app.
This package is a full-trust desktop package, not a browser app or sandbox rewrite.

## Choose distribution

| Channel | Suitable use | Signing and access |
| --- | --- | --- |
| Store MSIX, public/discoverable | General release | Microsoft signs after acceptance |
| Store MSIX, public/direct link | Distribution by URL without normal discovery | Not private access control; review current discoverability options |
| Store MSIX, private audience | Selected testers before public launch | Configure known users; testers use matching personal Microsoft accounts |
| Store package flights | Different update packages for test groups | Configure groups/flights in Partner Center |
| Store MSI/EXE route | Keep a conventional installer | Publisher must supply trusted Authenticode signatures; not the free-signing MSIX route |
| Direct MSIX outside Store | Managed deployment/sideload | Requires appropriate signing and recipient trust; unsigned candidate is not install-ready |
| GitHub portable/Inno installer | Early testing or alternative distribution | Unsigned warnings remain unless an approved signing route is configured |

Choose private audience before first public publication if needed: Microsoft does
not allow reverting a product from public to private audience. Link-only public
visibility is different from a private test group.

## Account and identity

- [ ] Register through https://storedeveloper.microsoft.com/ using the current free
  individual-developer flow; complete Microsoft's identity checks yourself.
- [ ] Reserve an available product name; the requested project name is not guaranteed.
- [ ] Copy Identity Name, Publisher CN and Publisher Display Name from Product identity.
- [ ] Keep identity stable for updates; record it outside public secrets/config files
  as appropriate. Identity values are not signing private keys.
- [ ] Choose audience, supported markets, languages and free pricing deliberately.

## Build on Windows

Install .NET SDK 8+, Windows SDK including MakeAppx.exe, and use PowerShell.
Run from the project root, replacing the example values with Partner Center values:

```powershell
./scripts/build-msix.ps1 `
  -IdentityName 'YOUR.PARTNER.CENTER.IDENTITY' `
  -Publisher 'CN=YOUR-PARTNER-CENTER-PUBLISHER-ID' `
  -PublisherDisplayName 'Your publisher display name' `
  -MakeAppxPath 'C:\Program Files (x86)\Windows Kits\10\bin\YOUR-SDK-VERSION\x64\MakeAppx.exe'
```

The script runs the normal build/tests, builds explicit x64 binaries, generates
three PNG logo sizes from the existing icon, fills the manifest and calls MakeAppx
with validation enabled. Output: `artifacts/Cam-Pressets-Studio-0.6.2.0-x64.msix`.
Pass `-PackageVersion` for updates; this script requires four parts ending in zero.
Pass `-DisplayName` if the reserved name differs. It refuses template identity values.

The minimum package OS is Windows desktop build 19041, with .NET Framework 4.8
available. No x86/ARM64 package is generated. The template maximum-tested version
is a manifest compatibility value, not evidence of actual platform testing.

## Packaged acceptance — must happen before submission

- [ ] Build with Windows SDK tools and inspect the actual package contents and logos.
- [ ] Use a private Store test release or a dedicated test certificate trusted only
  in a disposable development environment for local install. Do not distribute a
  self-signed package as a trusted public release or change users' trust stores.
- [ ] Run Windows App Certification Kit where applicable; resolve reported issues.
- [ ] Test camera controls and default startup gate while packaged, including Zoom.
- [ ] Test configured external executable/shortcut launches and process detection.
- [ ] Test profile read/write, legacy import, updates, uninstall and reinstall under
  MSIX file-system redirection. Back up profiles; portable/Store data sharing is unverified.
- [ ] Test keyboard access, window sizes, DPI and English/Danish/German display language.

## Listing and review

- [ ] Review docs/STORE-LISTING.md; replace draft/contact placeholders.
- [ ] Host the reviewed privacy policy at a public URL and provide a support URL.
- [ ] Use screenshots of the real current Windows UI, without meeting participants.
- [ ] Complete age rating, category, capabilities and accessibility declarations accurately.
- [ ] Explain `runFullTrust`: desktop COM camera properties and user-configured program
  launch/process checks, without requested administrator elevation. Review webcam capability.
- [ ] Add certification notes explaining camera hardware requirements, no preview,
  default gate, manual Zoom readiness and how a reviewer can test with/without hardware.
- [ ] Upload the package format accepted by the current Packages page; plain .msix is
  documented as supported. Use Visual Studio .msixupload if the chosen submission flow requires it.
- [ ] Submit for certification; resolve feedback before enabling wider distribution.

## References (checked 2026-11-16)

- https://learn.microsoft.com/windows/apps/publish/whats-new-individual-developer
- https://learn.microsoft.com/windows/apps/package-and-deploy/code-signing-options
- https://learn.microsoft.com/windows/msix/desktop/desktop-to-uwp-manual-conversion
- https://learn.microsoft.com/windows/apps/publish/publish-your-app/msix/visibility-options
- https://learn.microsoft.com/windows/apps/publish/publish-your-app/msix/upload-app-packages
- https://learn.microsoft.com/windows/apps/publish/store-readiness-checklist
