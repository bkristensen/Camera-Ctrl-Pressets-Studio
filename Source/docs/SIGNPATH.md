# SignPath for open source

SignPath Foundation offers free signing for eligible open-source projects. Acceptance is required. The certificate identifies SignPath Foundation as publisher; the service links verified builds to the source repository. This project has not been approved or configured for that service.

Before applying, publish the source under its open-source licence, maintain the project, provide a release and document the application. Prepare a verifiable build pipeline. Sign only your own project artifacts, and keep signing credentials out of the repository.

After acceptance, follow the required artifact rules and release approval process. Publish a “Code signing policy” with the required attribution, named team roles and privacy statement. Do not claim sponsored signing before approval. Every signed release requires manual approval under the Foundation's current terms.

For direct GitHub downloads, a valid signature identifies the publisher and protects the binary's integrity. A new download can still trigger SmartScreen while reputation builds. For first-run distribution through the Store, Microsoft signs accepted MSIX packages.

For a publicly trusted code-signing certificate available through the current user's Windows certificate store, use `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` for the portable ZIP. Use the same parameters with `scripts/build-installer.ps1` to sign both the app executable and the generated installer. SignPath approval requires its own signing workflow.

Read the [official terms](https://signpath.org/terms.html), the [Foundation website](https://signpath.org/), and Microsoft's [SmartScreen guidance](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation). Checked 19 November 2026. For the Store route, see [MSIX and Store](MSIX-AND-STORE.md).
