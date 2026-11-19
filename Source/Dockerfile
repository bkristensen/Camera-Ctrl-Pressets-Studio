# Builds Windows binaries and runs logic tests. Does not run the Windows UI.
ARG SDK_IMAGE=mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim
FROM ${SDK_IMAGE} AS build
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_NOLOGO=1
WORKDIR /src
COPY . .
RUN pwsh -NoLogo -NoProfile -File ./scripts/build.ps1
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-bookworm-slim AS package
WORKDIR /artifacts
COPY --from=build /src/artifacts/ /artifacts/
CMD ["sh", "-c", "test -d /output || { echo 'Mount a directory at /output.' >&2; exit 1; }; cp -R /artifacts/. /output/"]
