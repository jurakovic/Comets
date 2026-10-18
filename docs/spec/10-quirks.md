# 10 — Desktop quirks and deviations

The web app reproduces desktop behaviour unless this document lists a **deviation**. Some desktop behaviour is odd but harmless, or is a bug that changes data. The **quirks** section lists those cases: the web app keeps them for now, so that the [test vectors](11-test-vectors.md) stay authoritative. Each one can be fixed later as a separate decision. A fix means: change this spec, change the harness ([tools/spec-vectors](../../tools/spec-vectors/)) so the vectors describe the fixed behaviour, then regenerate.

## Deviations

These changes are deliberate and already reflected in this spec.

| # | Desktop | Web | Why |
|---|---|---|---|
| D1 | The orbit viewer has its own engine: comets through a precession matrix, planets from a two-regime series, orbit lines from a third model | One canonical engine ([04](04-astronomy.md#decision-record-one-canonical-engine)) | Agreed decision. Comet differences ≤ 0.006°; planet differences up to ~1° only for Uranus and Neptune at the range ends |
| D2 | Dance of the Planets header line reads `iř ęř wř`, a text-encoding accident | `ix jx wx`, as in MPC's own `Soft11Cmt.txt` | Mojibake. The vectors already contain the fix (`CanonicalizeExport` in the harness) |
| D3 | The name-first designation regex backtracks exponentially, and the app hangs on importing a file whose second-to-last line is long text with no designation | A linear procedure with identical results ([02](02-data-model.md#pattern-02-name-first)) | A hang. Equivalence is proven by the harness on 41–43 k inputs |
| D4 | Detection reads `lines[1]` unconditionally, and a one-line file that matches no header crashes | `[File]` is checked only when there are ≥ 2 lines | A crash |
| D5 | `JDToDateTime` fails for years < 1 (e.g. `C/-146 P1`'s perihelion), which crashes the database view | Years ≤ 0 are returned and shown as astronomical years | A crash |
| D6 | `Tn` throws when no candidate perihelion falls in 1700–2300 | `Tn = T` | A crash |
| D7 | Hyperbolic and parabolic Kepler loops have no iteration limit | At most 100 iterations | Guards against hangs; no real data needs it |
| D8 | Ephemeris and graph steps accumulate decimal sums, and for steps like 1 h the row exactly at End is sometimes dropped | Exact step count; the row at End is always included ([06](06-ephemeris.md#calculation)) | Emulating .NET decimal rounding is unreasonable in JavaScript, and including End is clearly the intent |
| D9 | After an import, the selection stays in sort-key order until the Database dialog is opened | The selection is re-sorted by the current sort right away | Consistency |
| D10 | The database is written on exit | Written after every change | The web has no exit |
| D11 | Orbit viewer shortcuts `Ctrl+digit`, `Ctrl+F/I/D/N/B/T` | `O`+digit chord and plain letter keys ([08](08-orbit-viewer.md#keyboard)) | Browser-reserved keys |
| D12 | Help text says K = faster, L = slower; the code does the opposite | The web follows the code (K slower, L faster) and its help text says so | The help text was wrong |
| D13 | The Elements format list includes Comet for Windows and NASA, which export nothing | Only the 19 exportable formats are listed | Natural UI |
| D14 | Theme default Light; changing it needs a restart | Default System; applies immediately; the graph follows live | Natural web behaviour |
| D15 | Label `Greather than (>)` | `Greater than (>)` (SHOULD) | Typo |
| D16 | The JPL lookup URL is built from the raw id | The id is URL-encoded | Correct URLs |
| D17 | Desktop-only features ([01 § Dropped](01-product.md#dropped-desktop-features)) | Removed | Agreed decision |

## Quirks kept

| # | Behaviour | Where | Effect | Recommendation |
|---|---|---|---|---|
| Q1 | Guide, Starry Night, Deep Space, PC-TCS, Dance of the Planets and MegaStar exports write `id` and `name` but **not the fragment** | `ExportManager` | Fragments collapse on round trip (`73P-BT` → `73P`, `C/2023 R1-B` → `C/2023 R1`). See [`import-expected/roundtrip-*`](vectors/import-expected/) | Fix: append `-fragment` to the id, as the MPC files do |
| Q2 | xephem and Home Planet exports append `/` to every comet without a name, meant for `282P/` | `ExportXephem03`, `ExportHomePlanet04` | `A/2018 W3/` and `C/2023 R1-B/` round-trip with a trailing slash | Fix: only for numbered periodic ids |
| Q3 | The MyStars importer keeps the `;` that ends the name column | `ParseMyStars05` | Every comet imported from MyStars has a different `full` (`1P/Halley;`), so a re-import adds duplicates | Fix: strip a trailing `;` |
| Q4 | Home Planet export writes a and P for hyperbolic orbits (`P < 30000` is true for them) | `ExportHomePlanet04` | Meaningless numbers; MPC leaves them empty | Fix: empty when e ≥ 1 |
| Q5 | Hyperbolic `Q = a(1 + (2 − e))`, and `P` from `(q/(e−1))^1.5` | `CometManager` | The database shows a negative aphelion and a small "period" for hyperbolic comets (`P < 1000` passes) | Fix: treat P, Q and n as not applicable when e ≥ 1 |
| Q6 | `e = 1` is replaced by `0.999999` for P, a and Q | `CometManager` | Huge P, a and Q for parabolic comets, used only for sorting | Keep |
| Q7 | The NASA importer precesses i, Ω and ω from the **osculation epoch** to J2000, but JPL's file is already J2000 | `ParseNasaComet` | Angles shift by about 0.014° × (years from 2000), e.g. ~0.45° for 1P (epoch 1968) | Fix: drop the precession |
| Q8 | `JDToDateTime` uses the Julian calendar before 1582-10-15, while `JD0` is always Gregorian | `EphemerisManager` | Dates before 1582 move on conversion (X/1106 C1: Feb 1 → Jan 25 through Starry Night) | Keep (outside the app's date range) |
| Q9 | The orbit viewer's **Perihelion Date** preset and `B` use `T`; everything else uses `Tn` | `OrbitViewerControl` | For periodic comets, a different perihelion from the one shown elsewhere | Fix: use `Tn` |
| Q10 | Orbit viewer: the selected comet's **Label** checkbox, not Orbit or Marker, keeps its dot visible when the date filter hides it | `OrbitPanel.RenderBodies` | Surprising toggle coupling | Keep or decouple |
| Q11 | `Tn` and the `Perih*` values are cached per session, with the clock and location at first use | `Comet` | Changing the location has no effect on them until reload | Fix: recompute when the location changes |
| Q12 | MPC export writes **today's date** in the epoch-of-osculation column | `ExportMpc00` | Claims the elements osculate today | Keep (matches desktop files) or write the original epoch |
| Q13 | Formats without g/k (Home Planet, Dance, Voyager, NASA) import g = k = 0 | importers | Magnitudes become `5·log Δ`, which means nothing | Keep (no data available) |
| Q14 | Voyager II imports have no id, an empty sort key, and allow duplicates | `ParseVoyager14` | They sort first and cannot be told apart | Keep (format limitation) |
| Q15 | Dance of the Planets cannot carry q ≥ 10 AU (`********`) | format | Such comets are lost on round trip | Keep (format limitation) |
| Q16 | A string filter with an empty term (`halley,`) matches every comet | `FilterManager` | Surprising | Fix: ignore empty terms |
| Q17 | Detection tries record parsers on the **last line**, so a trailing garbage line gives `Unknown` | `GetImportType` | Some edited files are not recognised | Keep |
| Q18 | Unmatched names in pattern 02 can carry stray spaces or digits (`lastToken`) | name parsing | No real data is affected | Keep |
| Q19 | `HMS` drops the sign of negative values | `HMSString` | Only RA is formatted, and RA is never negative | Keep |
| Q20 | Graph Maximum defaults to 12 for distance charts as well | `GraphSettings` | Distance graphs are capped at 12 AU by default | Keep |
