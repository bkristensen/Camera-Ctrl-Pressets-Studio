# Third-party material and build dependencies

| Component | Use | Distribution note |
| --- | --- | --- |
| .NET Framework 4.8 | Windows runtime prerequisite | Not bundled; provided by Microsoft/Windows |
| Microsoft.NETFramework.ReferenceAssemblies 1.0.3 and net48 reference package | Compile-time references, PrivateAssets=all | Not included in app payload; retain upstream terms if redistributing build packages |
| .NET SDK / official Microsoft container images | Build and tests | Governed by upstream image/component licenses; not part of portable EXE payload |
| Inno Setup | Optional Windows installer generation | Build tool not included; review its license for generated installer redistribution |
| GitHub Actions | Hosted build workflows | Upstream actions and service terms apply; no action implementation is bundled |
| Supplied project icon and design assets | Application/packaging branding | Provided with the project input; maintainer must confirm rights for public MIT distribution |

The application uses Windows COM interfaces directly and does not bundle Logitech
or Zoom SDK libraries. Logitech, BRIO, Zoom and other product names belong to their
respective owners. No affiliation or endorsement is claimed.

This inventory is not a claim of a completed legal audit. Review actual release
contents and dependency metadata when publishing. Do not copy proprietary vendor
assets, binaries or screenshots into the repository without permission.
