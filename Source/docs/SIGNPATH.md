# Code signing policy

*Written for Camera Control Studio 1.1.1.*

SignPath Foundation offers free signing for eligible open-source projects. Acceptance is required. The certificate identifies SignPath Foundation as publisher; the service links verified builds to the source repository. The project has applied for SignPath Foundation signing. The application is pending, and GitHub downloads remain unsigned.

Before you apply, publish the source under its licence. Keep the project current. Publish a release, document the app and make the build verifiable. Sign only your own project files. Keep signing credentials out of the repository.

The [privacy policy](../PRIVACY.md) covers the app, its GitHub update check and GuideViewer’s WebView2 runtime.

## While the application is pending

- Check that the public repository has a successful Windows build under **Actions**. Use that workflow as the basis of the SignPath integration.
- Confirm the people responsible for commits, reviews and signing approvals. Turn on multi-factor authentication for all team members on GitHub and SignPath.
- Ask SignPath to confirm how the WebView2 SDK binaries and the Inno Setup installer should be handled. The packages include Microsoft's SDK binaries and loaders. The app uses the WebView2 Runtime installed on the user’s PC. List these dependencies in the application and preserve their notices.
- Add a **Code signing policy** link to the GitHub release notes for the download page supplied in the application. State the actual signing status of those downloads.

## After SignPath accepts the project

- Add the approved SignPath attribution and the confirmed names or permission groups for committers, reviewers and approvers to this page.
- Configure SignPath to accept artifacts from the trusted GitHub Actions build. Restrict signing to project binaries. Set and enforce the product name and matching product version across the files submitted together.
- For the ZIP, sign the app executable before packaging. For the installer, put the signed app executable into the installer before signing the finished installer. Obtain manual approval for each release signing request.
- Verify the Authenticode signatures and timestamps, then generate SHA-256 checksums. Include a **Code signing policy** link and the signing status in the release notes. Verify the uploaded files and hashes before publishing.

For direct GitHub downloads, a valid signature identifies the publisher and protects the binary's integrity. A new download can still trigger SmartScreen while reputation builds. For first-run distribution through the Store, Microsoft signs accepted MSIX packages.

For a publicly trusted code-signing certificate available through the current user's Windows certificate store, use `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` for the portable ZIP. Use the same parameters with `scripts/build-installer.ps1` to sign both the app executable and the generated installer. SignPath approval requires its own signing workflow.

Read the [official terms](https://signpath.org/terms.html), the [Foundation website](https://signpath.org/), and Microsoft's [SmartScreen guidance](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation). Checked 24 November 2026. For the Store route, see [MSIX and Store](MSIX-AND-STORE.md).
