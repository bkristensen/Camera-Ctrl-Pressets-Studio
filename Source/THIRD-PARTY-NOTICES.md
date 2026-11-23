# Third-party notices

The app uses Windows and .NET Framework APIs. Guide Viewer runs on the Microsoft Edge WebView2 Runtime installed on your PC.

- [Markdig](docs/licenses/Markdig.txt) renders Markdown and is distributed under the BSD 2-Clause licence.
- [Microsoft WebView2 SDK](docs/licenses/WebView2.txt) provides the WinForms viewer and print APIs. See its [notices](docs/licenses/WebView2-NOTICE.txt) and the SDK licence for redistribution terms.
- [Microsoft .NET libraries](docs/licenses/DotNet.txt) include the System.Buffers, System.Memory, System.Numerics.Vectors, System.Resources.Extensions and System.Runtime.CompilerServices.Unsafe packages. They are distributed under the MIT licence.

The build restores Microsoft.NETFramework.ReferenceAssemblies 1.0.3 and its net48 reference package. These are build-only dependencies. Preserve their upstream licence files when redistributing those packages themselves. See the package metadata at https://www.nuget.org/packages/Microsoft.NETFramework.ReferenceAssemblies/1.0.3 and `packages.lock.json` for resolved versions.

The icon drawing code and documentation diagrams are included with this project's source. No third-party icon font or runtime UI toolkit is bundled. Inno Setup and the Windows SDK are external build tools; their installers are not bundled.

The MIT licence in `LICENSE` covers this project's code. It does not replace licences for separately installed tools or platform components.
