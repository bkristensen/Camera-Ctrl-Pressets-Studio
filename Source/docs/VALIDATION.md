# Validation for 0.7.0-beta.1

The application and test project compile with .NET SDK 8.0.408. The logic test suite passes for movement arithmetic, preset state, streaming confirmation, profile serialization and migration, layout selection, image value persistence, independent zoom steps, temporary positions, profile renaming and revision conflicts.

The test build emits Windows-platform warnings for the linked DirectShow COM implementation. The tests do not invoke COM on Linux. UI rendering, physical camera behaviour, Visual Studio Designer round trips, screenshot capture, PowerShell scripts, Docker, Inno Setup and MSIX have not been executed in this environment. They remain acceptance checks, not claimed test results.

The checked-in documentation visuals are explicitly labelled illustrations. No screenshots of this new build have been represented as real Windows captures.
