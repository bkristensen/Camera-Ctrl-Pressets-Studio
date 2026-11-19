# Release checklist

## Code and evidence
- [ ] Select a version/tag and update CHANGELOG.md with actual changes.
- [ ] Run build scripts and regression tests; keep logs linked to the commit.
- [ ] Complete WINDOWS-TEST.md for the exact binaries to be distributed.
- [ ] Record camera/driver/Windows/video-app versions; separate simulations from hardware tests.
- [ ] Verify default startup gate, cancellation, profile persistence and migration.

## Package and trust
- [ ] Confirm icon/design redistribution rights and dependency notices.
- [ ] Include LICENSE, documentation and truthful compatibility limitations.
- [ ] Scan final release artifacts and publish SHA-256 checksums.
- [ ] If signing, verify final signatures; otherwise label the release unsigned.
- [ ] Follow MSIX-AND-STORE.md or SIGNPATH.md for the chosen channel.

## Publication
- [ ] Set real repository, support and privacy-policy links.
- [ ] Check installation/uninstallation on a clean Windows account.
- [ ] Publish artifacts from the reviewed build, not an unrelated local folder.
- [ ] Link FAQ, compatibility reporting and code-signing policy from release notes.
- [ ] Verify download/install and keep previous release/migration notes available.
