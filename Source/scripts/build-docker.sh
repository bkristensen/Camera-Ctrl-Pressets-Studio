#!/usr/bin/env bash
set -euo pipefail
project_root="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$project_root"
image="${1:-cam-pressets-studio-build:local}"
output="${2:-artifacts/docker}"
platform="${3:-linux/amd64}"
container=''
cleanup() { if [[ -n "$container" ]]; then docker rm "$container" >/dev/null; fi; }
trap cleanup EXIT
docker info --format '{{.OSType}}'
docker build --platform "$platform" --tag "$image" --file Dockerfile .
mkdir -p -- "$output"
output="$(cd -- "$output" && pwd)"
container="$(docker create "$image")"
docker cp "$container:/artifacts/." "$output"
printf 'Image: %s\nWindows ZIP: %s/Cam-Pressets-Studio-portable.zip\n' "$image" "$output"
