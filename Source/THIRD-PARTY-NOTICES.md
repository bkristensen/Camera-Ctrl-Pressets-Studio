# Third-party notices

The application uses the Windows and .NET Framework platform APIs. Platform components are not redistributed in the portable ZIP.

The build restores Microsoft.NETFramework.ReferenceAssemblies 1.0.3 and its net48 reference package. These are build-only dependencies. Preserve their upstream licence files when redistributing those packages themselves. See the package metadata at https://www.nuget.org/packages/Microsoft.NETFramework.ReferenceAssemblies/1.0.3 and `packages.lock.json` for resolved versions.

The icon drawing code and documentation diagrams are included with this project's source. No third-party icon font or runtime UI toolkit is bundled. Inno Setup and the Windows SDK are external build tools; their installers are not bundled.

The MIT licence in `LICENSE` covers this project's code. It does not replace licences for separately installed tools or platform components.
