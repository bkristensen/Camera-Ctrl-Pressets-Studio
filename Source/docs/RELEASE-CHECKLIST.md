# Release checklist

## Automated checks

- [ ] Run `scripts/build.ps1` on a clean checkout with locked dependencies.
- [ ] Confirm all three language files have matching, unique keys.
- [ ] Review the source and binary version, package identity and release notes.

## Windows and camera checks

- [ ] Launch at 900 × 500. Resize, maximize and restore at 100%, 125%, 150% and 200% display scaling. Check footer, all inputs, combined icons and centred zoom glyphs.
- [ ] Open settings on the right. Drag the divider. Close and reopen. Test a saved layout after disconnecting a monitor.
- [ ] Recall a position: yellow during movement, green only after matching readback. Pan/tilt/zoom clears the active colour. Disconnect during a transition: no false green. Icons never disappear after confirmation.
- [ ] Test each combined icon, including every reversed order. Test both zoom speeds and both direction reversals.
- [ ] Enable camera confirmation. Restart. Verify no camera enumeration or connection before Yes. Test No, then retry from a pan/zoom button. Test profile change and source selection.

## Data and editing

- [ ] Back up the 0.6 profile file before migration. Confirm presets and icon IDs survive. Confirm obsolete external-program settings are absent after save.
- [ ] Test new/copy/rename profiles, restore layout and keep-current-layout behaviour. Verify temporary positions remain session-only.
- [ ] Test image automatic/manual settings and defaults with the actual camera.
- [ ] Open every Form with Shift+F7 in Visual Studio. Adjust a layout property, save and rebuild. Custom drawing stays in the control classes.
- [ ] Run the screenshot script, review all nine PNGs and update the guide links/captions.

## Distribution

- [ ] Build and test the installer and MSIX on Windows. Neither is verified by the Linux compile.
- [ ] Complete [Store checklist](MSIX-AND-STORE.md) if publishing to the Store.
- [ ] Verify signing if used. If unsigned, state it clearly. Generate hashes after signing.
- [ ] Publish source, licence, release notes, hashes and binaries together. Do not claim unperformed hardware tests or Store approval.
