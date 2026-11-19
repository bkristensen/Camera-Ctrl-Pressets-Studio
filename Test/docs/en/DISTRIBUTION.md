# Distribution, Store and SignPath

Choose portable ZIPs for early testing, Inno Setup for conventional Windows
installation, or Store MSIX for Store-hosted installation and updates. Unsigned
portable/installer files can trigger Windows warnings. Store MSI/EXE submissions
require publisher signing; Store MSIX signing is handled by Microsoft after acceptance.

Store choices include public discovery, public direct-link discovery restrictions,
a private audience and package flights for update test groups. Direct-link public
visibility is not access control. A public product cannot later become private;
start with private testers if needed. Private testers use matching personal Microsoft accounts.

## MSIX checklist

- [ ] Register at https://storedeveloper.microsoft.com/ and complete identity verification.
- [ ] Reserve a name; copy Identity Name, Publisher CN and Publisher Display Name.
- [ ] Install .NET SDK 8+ and Windows SDK MakeAppx.exe on Windows.
- [ ] Run the packaging script with actual identity values.
- [ ] Test installation, camera control, the startup gate, external program launch and data migration.

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.IDENTITY' -Publisher 'CN=YOUR-PUBLISHER-ID' -PublisherDisplayName 'Your publisher name' -MakeAppxPath 'C:\path\MakeAppx.exe'
```

The script builds/tests, creates explicit x64 binaries and icons, and produces an
unsigned MSIX candidate. It is not an end-user signed sideload installer. Packaging
has not been executed here. The four-part version ends in zero; minimum manifest
OS is Windows build 19041. MSIX may redirect profile data; test migration/update/uninstall.

## Before Store submission

- [ ] Provide real screenshots without private meeting information.
- [ ] Supply an actual support URL and reviewed, publicly hosted privacy policy.
- [ ] Choose audience, markets, languages, category and age ratings accurately.
- [ ] Explain full trust: driver control and chosen program launching, not administrator elevation.
- [ ] Explain Zoom readiness and hardware requirements to reviewers; pass certification before publication.

## SignPath

SignPath Foundation may provide free signing to approved open-source projects.
Approval is not automatic. Prepare a maintained public project, releases and
traceable builds; review license, MFA, review, approval, privacy and uninstall
requirements. Signing establishes provenance, not bug-free software or an immediate
SmartScreen bypass. Each signed release needs approval under the provider's rules.

This project has not applied, been approved or configured SignPath signing. Its
local certificate-store signing script is not a SignPath integration. Do not claim
provider endorsement before approval. See https://signpath.org/terms and
https://signpath.org/apply. MSIX preparation also does not mean the app is in the Store.
