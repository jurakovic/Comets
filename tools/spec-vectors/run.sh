#!/usr/bin/env bash
#
# Regenerates docs/spec/vectors from the desktop sources.
#
#   tools/spec-vectors/run.sh [input-dir]
#
# input-dir holds Soft00Cmt.txt .. Soft16Cmt.txt and ELEMENTS.COMET. It defaults to the
# copies already committed under docs/spec/vectors/import, so a plain run reproduces the
# committed vectors. Point it at fresh downloads to rebase the vectors on new MPC data.
#
# Needs the .NET 8 SDK and libfaketime (Linux or WSL): several desktop functions read the
# system clock directly, so the clock is frozen at the instant the vectors are defined for.

set -euo pipefail

here="$(cd "$(dirname "$0")" && pwd)"
out="$here/../../docs/spec/vectors"
input="${1:-$out/import}"

libfaketime="$(ls /usr/lib/x86_64-linux-gnu/faketime/libfaketime.so.1 /usr/lib/faketime/libfaketime.so.1 2>/dev/null | head -n 1 || true)"
if [[ -z "$libfaketime" ]]; then
	echo "libfaketime not found (apt-get install faketime)" >&2
	exit 1
fi

dotnet build "$here/SpecVectors.csproj" -c Release --nologo -v quiet

# Frozen clock (no leading @), UTC, and a scratch working directory so no settings.ini is read.
cd "$(mktemp -d)"
TZ=UTC \
FAKETIME="2026-10-18 12:00:00" \
FAKETIME_DONT_FAKE_MONOTONIC=1 \
LD_PRELOAD="$libfaketime" \
	dotnet "$here/bin/Release/net8.0/SpecVectors.dll" --input "$input" --out "$out"
