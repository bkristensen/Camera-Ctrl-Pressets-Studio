# Build a Windows desktop application with Microsoft's Linux SDK image.
# This container does NOT run the Windows GUI, camera control or Zoom.
ARG SDK_IMAGE=mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim
FROM ${SDK_IMAGE} AS build
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_NOLOGO=1
WORKDIR /src
COPY Directory.Build.props LICENSE ./
COPY *.md ./
COPY docs/ docs/
COPY BrioPresets/ BrioPresets/
COPY SharedProfiles/ SharedProfiles/
COPY SharedUi/ SharedUi/
COPY SharedZoom/ SharedZoom/
COPY CoreTests/ CoreTests/
COPY ShareTests/ ShareTests/
COPY ProfileTests/ ProfileTests/
COPY scripts/ scripts/
# build.ps1 fails if compilation or any regression test fails.
# The official SDK image includes PowerShell.
RUN pwsh -NoLogo -NoProfile -File ./scripts/build.ps1

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-bookworm-slim AS package
WORKDIR /artifacts
COPY --from=build /src/artifacts/ /artifacts/
# Optional: docker run --rm --mount type=bind,src=/absolute/output,dst=/output IMAGE
# No ports, camera devices or privileged access are needed.
CMD ["sh", "-c", "test -d /output || { echo 'Mount an output directory at /output to export the Windows files.' >&2; exit 1; }; cp -R /artifacts/. /output/"]
