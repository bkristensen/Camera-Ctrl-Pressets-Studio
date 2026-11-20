# Contributing

Fork the repository and create a focused branch. Describe the problem and expected behaviour in an issue before large changes.

Preserve these primary behaviours: quick pan/tilt/zoom, permanent and temporary positions, profile separation, optional camera-access confirmation, and a compact readable interface. Settings open beside the position list. No camera access should occur while confirmation is pending. Icons must survive connection and status changes.

Keep camera ranges in driver units. Keep stored icon IDs stable. Keep the text keys in every locale in sync with the English catalog. Do not add vendor-specific assumptions to the normal workflow.

Keep fixed controls, components, and layout in each form's `*.Designer.cs` so they can be edited in Visual Studio. Keep runtime logic in the corresponding code-behind and populate designer-authored controls with runtime data.

Build the solution, run the logic tests and complete relevant Windows checks in `docs/RELEASE-CHECKLIST.md`. Include screenshots for UI changes. Never include real user profiles, credentials, private meeting content or signing keys.

Pull requests should state what changed, why, how it was tested, and any remaining limits. Contributions are offered under the MIT licence. Forks may change the design; the design preferences here govern contributions to this repository, not the licence.
