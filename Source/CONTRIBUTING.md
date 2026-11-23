# Contributing

Fork the repository and create a branch for one change. If the work is large, open an issue first. State the problem and what should happen.

Keep pan, tilt and zoom easy to reach. Keep temporary and permanent positions apart, and keep profiles separate. Open settings beside the position list. When confirmation is on, wait for Yes before touching the camera. Keep icons visible as the camera connects and moves.

Keep camera ranges in driver units. Keep stored icon IDs stable. Keep the text keys in every locale in sync with the English catalog. Do not add vendor-specific assumptions to the normal workflow.

Keep fixed controls, components, and layout in each form's `*.Designer.cs` so they can be edited in Visual Studio. Keep runtime logic in the corresponding code-behind and populate designer-authored controls with runtime data.

Build the solution, run the logic tests and complete relevant Windows checks in `docs/RELEASE-CHECKLIST.md`. Include screenshots for UI changes. Never include real user profiles, credentials, private meeting content or signing keys.

In your pull request, say what changed, why, and how you tested it. State what still needs a check. Contributions use the MIT licence. You can change the design in your fork.
