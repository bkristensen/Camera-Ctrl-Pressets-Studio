# Release checklist

Use the [build and release guide](en/BUILD-AND-RELEASE.md) for commands and output paths.

## Automated checks

- [ ] Run `scripts/build.ps1` on a clean checkout with locked dependencies.
- [ ] Confirm every locale has the same unique, non-empty keys. The build runs the translation check and the locale tests.
- [ ] Review the source and binary version, package identity and release notes.

## Windows and camera checks

- [ ] Launch at 900 × 500 and verify the 670 × 450 minimum. Resize, maximize and restore at 100%, 125%, 150% and 200% display scaling. Check the footer, inputs, combined icons and zoom glyphs.
- [ ] Open settings on the right. Drag the divider. Close and reopen. Test a saved layout after disconnecting a monitor.
- [ ] Recall a position: yellow during movement, green only after matching readback. Pan/tilt/zoom clears the active colour. Disconnect during a transition: no false green. Icons never disappear after confirmation.
- [ ] Test each combined icon, including every reversed order. Test zoom buttons, the zoom slider and both direction reversals.
- [ ] On a supported Brio, test automatic FOV in both zoom directions and at the left and right pan edges. Follow the [Brio test steps](VALIDATION.md#brio-field-of-view-and-zoom).
- [ ] Enable camera confirmation. Restart. Verify no camera enumeration or connection before Yes. Test No, then retry from a pan/zoom button. Test profile change and source selection.

## Data and editing

- [ ] Back up the 0.6 profile file before migration. Confirm presets and icon IDs survive. Confirm obsolete external-program settings are absent after save.
- [ ] Test new/copy/rename profiles, restore layout and keep-current-layout behaviour. Verify temporary positions remain session-only.
- [ ] Test image automatic/manual settings and defaults with the actual camera.
- [ ] Open every Form with Shift+F7 in Visual Studio. Adjust a layout property, save and rebuild. Custom drawing stays in the control classes.
- [ ] Open the guide viewer from the extracted ZIP in a 64-bit Windows process. Open it again and print a guide to PDF. Confirm the installer and ZIP include the matching WebView2 native loader under `runtimes` for each supported architecture.
- [ ] Check the [user-guide index](USER-GUIDES.md) and confirm every language guide is present in both packages. Check the [guide screenshots](SCREENSHOTS.md) for complete tab and subtab coverage, cropping, and overlap.

## Distribution

- [ ] For a portable beta, use `scripts/build.ps1`. For an Inno Setup installer, use `scripts/build-installer.ps1`, which also runs the portable build and tests. Verify the version shown by the installer and the app.
- [ ] For a Store release, follow the [Store checklist](MSIX-AND-STORE.md). Test the package types you plan to publish on Windows.
- [ ] If signing, verify the Authenticode signatures on the app executable and installer where applicable. Label unsigned downloads clearly. Generate SHA-256 hashes after final signing and packaging.
- [ ] Publish source, licence, release notes, hashes and binaries together. Do not claim unperformed hardware tests or Store approval.
