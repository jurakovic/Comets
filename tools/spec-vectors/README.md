# spec-vectors

Generates the test vectors in [`docs/spec/vectors`](../../docs/spec/vectors) by running the **original desktop sources**. The web implementation described in [`docs/spec`](../../docs/spec) is correct when it reproduces these files.

## How it works

- `SpecVectors.csproj` compiles the desktop engine files in unchanged (linked from `src/`, not copied): `Comets.Core` and the orbit viewer's astronomy classes. `OrbitPanel.cs` and `ValNumManager.cs` are left out because they are UI.
- `Stubs/WinFormsStubs.cs` supplies inert stand-ins for the few WinForms/GDI+ types those files mention, so the project builds and runs on Linux. The charting stub records what `EphemerisManager.GenerateGraph` draws, and that record becomes the graph vectors.
- `Canonical.cs` holds the only new astronomy, which is the canonical planet model (Schlyter mean elements, [04-astronomy.md](../../docs/spec/04-astronomy.md)). Comets and the Sun come straight from the desktop `EphemerisManager`.
- `NameParsing.cs` holds the spec's two designation parsers. On every run they are checked against the desktop regexes (all MPC names plus 20 000 fuzzed strings); the run fails on any disagreement.
- Several desktop functions read the system clock, so `run.sh` freezes it at **2026-10-18 12:00:00 UTC** with libfaketime and sets `TZ=UTC`. The program refuses to run otherwise.

## Running

Requirements: .NET 8 SDK and `libfaketime` (`apt-get install faketime`), on Linux or WSL.

```bash
tools/spec-vectors/run.sh                 # regenerate from the committed inputs (reproduces the committed vectors)
tools/spec-vectors/run.sh ~/Downloads/mpc # rebase on fresh downloads
```

A fresh-download directory must contain `Soft00Cmt.txt` … `Soft16Cmt.txt` from `https://minorplanetcenter.net/iau/Ephemerides/Comets/` and `ELEMENTS.COMET` from `https://ssd.jpl.nasa.gov/dat/ELEMENTS.COMET`. Rebasing changes the vectors because the MPC data changes daily, so commit the inputs and outputs together.

## When to re-run

- The desktop engine sources change in a way the web app must follow.
- A desktop quirk listed in [`docs/spec/10-quirks.md`](../../docs/spec/10-quirks.md) is fixed on purpose. Fix it in the harness as well (as `CanonicalizeExport` does for the Dance of the Planets header), so the vectors describe the new behaviour.
