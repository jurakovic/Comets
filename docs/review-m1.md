# Review of M1 — Core engine

A review of `app/js/core/` and `app/js/lib/math.js` and their tests, by a reviewer with a clean context. It checked the code against spec 02, 03 (number formatting), 04, 10, 11 and 12 and against the desktop sources, and ran its own fuzzing and transcription checks. It found **nothing that gives wrong results with real data**. The findings below are weak tests, contracts that later milestones depend on, spec text, and architecture. All were fixed in the close-out commit unless the outcome says otherwise.

| # | Severity | Finding | Outcome |
|---|---|---|---|
| 1 | Medium | `HMS` and `Angle` ties to even were untested: `formatting.json` has no exact tie, and half-away-from-zero rounding passed every vector | `tests/unit/core/format/angles.test.js`: `hms(0.03125)` → `00h01m52`, `angle(±0.375)` → `±00°22'`, `angle(0.125)` → `000:08`. Fails with half-away-from-zero rounding |
| 2 | Medium | The unit test of `cometOrbitLine` compared only the first point, an aphelion with y ≈ 0, so a wrong matrix element for y passed | Every point is now checked against the rotation. Fails with that wrong element |
| 3 | Low | `jdToParts` claimed to return proper date parts, but gives Julian parts before 1582 (29 February 1500 included) and `hour: 24` in the last 0.4 ms of a day (`jdToParts(2461331.4999999995)`). The desktop throws for both, and the spec covered only 29 February | `jdToParts` returns `JdParts`, documented as the desktop's raw parts, with `jdToInstant` the way to a date. The hour-24 carry to the next day is tested. Spec 04 and spec 10 D5 name both cases. M2 importers that take date parts from `JDToDateTime` use `instantToParts(jdToInstant(jd))` |
| 4 | Low | `partsToInstant` said overflowing fields carry, but months above 14 came out wrong (month 15 of 2026 → 2027-03-03) | `daysFromCivil` carries months outside 1–12 into the year. Tested, and the test fails without the fix |
| 5 | Low | Spec 02's sort-key text was looser than the desktop: the lenient year, `code` as the second space-separated field, "always 15 characters" when padding never truncates, and no list of the inputs that fail | Spec 02 spells all four out |
| 6 | Low | `Number(P.toPrecision(15))` is not exactly .NET's `Convert.ToDecimal(double)`: the 15th digit differs at times, moving `Tn` by about 2e-9 day | Kept. The code and spec 02 now say so, with the effect bounded under the 1e-8 tolerance |
| 7 | Low | Spec 12 lists an ephemeris row among the frozen records, but `ephemeris()`, `derivedElements`, `perihelionValues` and `currentValues` returned mutable objects | All four are frozen. Positions from `cometXyz`, `sunXyz` and `planetXyz` stay plain temporaries: they are not records, and the orbit viewer makes them per frame |
| 8 | Low | `planets.js` branched on the planet's name (perturbations, the Earth's position) where spec 12 asks for a registry | One model per planet: elements, an optional perturbation, an optional position of its own |
| 9 | Low | Past D7's 100-iteration cap the hyperbolic solution is not accurate; that happens for e within about 1e-13 of 1 (the desktop hangs there) | Spec 10 D7 says so. The 963 MPC comets need at most 9 iterations over 1700–2300 |
| 10 | Low | `sameComet` was tested with only two of its nine fields changed, and `precession.test.js` did not check the case count | A test changes each of the nine fields in turn, and the case count is checked (6) |
| 11 | Low | `createComet({ ...comet, Th })` kept the old `T` and sort key, because both came in as fields | `T` and `sortkey` are a separate `fixed` argument, so a spread comet gets both afresh. Tested |
| 12 | Low | Lint did not ban the other .NET traps of CLAUDE.md in `core/`; `designation.js` has its own copy of spec 03's `dbl` grammar; the degree helpers lacked `@returns` | Lint bans `Date`, `parseFloat`, `parseInt`, `Math.round` and `localeCompare` in `core/`. M2 S1 in the plan now says to read the sort key's year with the shared `dbl`. `@returns` added |

## Checked and found correct

- **Transcriptions against the desktop:** `JD0`, `JDToDateTime`, `SunXyz`, the three `CometXyz` branches, `GetEphemeris` (including the 17.2 small-angle formula, position angle, Alt/Az, magnitude) and `PrecessElementsToJ2000`; the planets against `tools/spec-vectors/Canonical.cs`; the `CometOrbit` sampler (all four branches, the 1e-16 band, the closed loop's order); the 1582 skip in `ATime.ChangeDate`; `GetSortkey`, `IsPeriodic`, `GetFullFromIdName` and pattern 00.
- **Number formatting:** 2.7 million random value and pattern pairs against an independent BigInt reference agreed, except at exact 16-digit ties, where the code rounds to even on purpose (D9 in the plan's log). In the M1 session the formatter also agreed with .NET 8 itself on 114 000 cases.
- **Round trips:** 2 million random whole-second instants, and 23:59:59 on every day of the range, convert to a JD and back exactly.
- **`nearestPerihelion`:** equal to a transcription of the desktop loop on 20 000 cases (T from 1.5e6 to 3.5e6, P from 0.05 to 3200 years).
- **Robustness:** no hangs on NaN, 0 or negative q or e in the solvers or the samplers.
- **Architecture:** the layer and determinism rules hold, the comet record is frozen, and the steps and elements use registries.
- **Performance:** about 1.8 µs per formatted number, 1.3 µs per ephemeris and 0.36 µs per `cometXyz`, enough for 1000 comets per frame.
