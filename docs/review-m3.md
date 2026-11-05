# Review of M3 — Collections and long computations

A clean-context reviewer read the whole of M3: the properties, filters, sorting and quick selection of `core/comets/`, `core/validation.js`, `core/ephemeris/`, `core/graph/`, `lib/rpc.js`, `core/jobs.js`, `workers/compute.js`, `adapters/job-runners.js`, `services/observations.js` and `services/jobs.js`, with their tests and spec edits. It checked the code against spec 02, 06, 07, 10, 11 and 12 and against the desktop's `FilterManager`, `CometManager`, `Comet`, `EphemerisManager`, the settings classes, and the ephemeris and graph forms and controls. It also ran 71 mutations, benchmarks over the 963 comets, and probes of the Worker in Chromium.

One finding changed real results: **sorting followed the host's locale**. The other findings were robustness of the Worker protocol, a text phase with no point to cancel, a desktop crash that was not listed, spec text, and weak tests. All were fixed in the commit "Core and jobs: fixes from the M3 review" unless the outcome says otherwise.

| # | Severity | Finding | Outcome |
|---|---|---|---|
| 1 | High | `Intl.Collator('und')` is not the root collation. `und` is not an available locale, so Intl falls back to the host's default locale. Names, sort keys and the database order after an import then follow Czech, Danish or Lithuanian tailoring: 11 vectors fail under `lt_LT`, and 152 of the 963 Discoverer positions move under `cs_CZ`. The e2e suite runs with `en-GB`, so it could not see this | `Intl.Collator('en')`, whose CLDR collation has no tailoring. A test sorts under `cs_CZ`, `da_DK` and `lt_LT` in a child process and fails with `'und'`. The whole suite passes under `lt_LT`, `cs_CZ` and `da_DK`. Spec 02, CLAUDE.md and the lint message said `'und'` |
| 2 | Medium | A Worker that did not load rejected its first job and left every later one pending forever. A Worker error also left the failed jobs' generators running | The runner ends the Worker on its first error and rejects every later run at once, so the composition root can turn to the local runner. Tested in Chromium |
| 3 | Medium | The ephemeris text had no yield point. Writing 963 comets (175 266 rows) takes 2–3 s, and a cancel could not be read until it finished. The desktop checks for a cancel per comet there (spec 06, step 5) | `writeEphemeris` is a generator that yields per comet and every 2000 rows. The job reports progress again for this phase, as the desktop does. Spec 06 says so |
| 4 | Medium | Multiple mode over a one-comet list: the desktop calculates the picker's selected comet, which is null while all comets are chosen, and crashes. Spec 06 said "the selected comet" for this case, and nothing tested it | Spec 06 and spec 10 D18: the list's comet, in multiple mode. Tests for the ephemeris and the graph |
| 5 | Medium | Mutations the suite let through: Starts with as Contains, `T` for the session `Tn` in the heading, no margin on the minimum, ticks from multiples of the step, no slack for the tick on `yMax`, and hover without its axis, nearest-sample and line checks | A test for each; every mutation now fails the suite. Spec 07's "a tick at yMin" is the chart's default behaviour, which the vectors do not record (the harness keeps only the range and the step). It is now tested as the web app's rule |
| 6 | Low | Boundary mutations survived: `≤` in the requirements, `P < 10000`, `yMin ≥ yMax`, `v ≤ yMax`, and the perihelion loops (equality, the fix-up loop, `toPrecision(15)`, accumulated steps) | Equality cases for each. The perihelion loop's fix-up is pinned by a case found by a random search, where `(Tn − Start) / p` rounds up. Loosening the guard before the loops (`>` to `≥`) is an equivalent mutant: the loops correct it |
| 7 | Low | `validateGraphSettings` took an empty Maximum as 0 and added "Minimum value must be lower than Maximum value" to "Enter Maximum value". The desktop never reaches that comparison with an empty field | The comparison runs only when both values are there. Both validators say that `errors[0]` is the desktop's message |
| 8 | Low | A result that did not clone left a local run pending forever; in the Worker it failed every job. An input that did not clone left its run registered, and a later abort sent a cancel for it | The server turns a reply it cannot send into an error reply. The client rejects a run whose request cannot be posted, and keeps nothing of it. Tested |
| 9 | Low | The protocol's cancel test aborted from the first progress, which a fast machine might never post | It aborts right after the run, which the protocol handles deterministically |
| 10 | Low | `Perih*` and `Current*` are geocentric, so the observer never changes them. Spec 02, spec 09 and Q11 described a location effect that does not exist, and the tests of it were vacuous. Q5 did not mention the graph's perihelion line every hyperbolic "period" | Spec 02, 09 and 10 corrected. Q11's recommendation is now "keep", and the plan's deferred decision no longer lists it. The tests now check the caching by counting observer reads |
| 11 | Low | Conventions: hover branched on the series kind; factories lacked `@returns`; the interval's input ranges were not in core; no decision-log entry for M3. The shared timespan lives in `core/ephemeris/` | A registry of hit tests by kind, the `@returns`, `INTERVAL_INPUT`, and D13 in the plan. The timespan stays where it is: spec 07 defines the graph's timespan by reference to spec 06 |
| 12 | Low | Edges reachable only without validation or with absurd elements: End before Start still gave a row or 101 samples. A period of 10⁻⁶ years would draw 3·10⁸ perihelion lines. Nested `setTimeout(0)` is clamped to 4 ms | End before Start gives no rows and no samples. The Worker schedules slices with a `MessageChannel`. The line count is not capped: the desktop is no better, and only absurd elements reach it |

## Checked and found correct

- **Filters** against `FilterManager`, `Filter.cs` and `FilterPanelManager`:
  - text rows: the terms are split on `,`, then trimmed and lower-cased, and an empty term matches (Q16);
  - rows are ANDed, the list order is kept, and a copy is returned when nothing is checked;
  - `>`, `<` and `|d − f| ≤ tolerance`, with all 14 tolerances;
  - validation by `IsNullOrWhiteSpace`, naming the first such row with the exact label;
  - the filter number as `Double()` reads it.
- **String operations:** `toLowerCase`, `includes`, `startsWith` and `endsWith` differ from .NET only for non-ASCII or ignorable characters, which comet names never contain.
- **Sorting and selection:**
  - sorting is stable in both directions, as LINQ is; NaN comes first, ±0 compare equal, and the menu labels match the designer;
  - quick selection takes the first minimum and puts NaN first;
  - rebinding keeps the first equal record.
- **Observations:**
  - `Tn` on first use, its milliseconds dropped;
  - `Current*` recomputed only when strictly more than 5 minutes old, per comet record;
  - the caches are keyed by record.
- **Ephemeris:**
  - the default dates (AddMonths with its clamp), the defaults, and the column order, headers and cells;
  - `-` from 10 000 years; requirements as `≤`, validated before the timespan, whose errors point at Start;
  - an interval of 0 means a day; the count and D8 (the cast cannot overflow, but the product with the comet count wraps); the titles.
- **Graph:**
  - the sampling table, and min and max over every comet with the margin, floor and ceil;
  - 15 or 2 only when more than one comet has samples; the `yMin ≥ yMax` fallback; the step table; the reversed axis;
  - the drawn-series rule; the perihelion loop and where its lines go (D6);
  - the now line strictly inside; the title in single mode only; the label format up to 3 days;
  - the tooltip texts and formats of `FormGraph`.
- **Worker protocol:**
  - cancel before the first slice, during queued slices and after the result;
  - replies for unknown ids are dropped, and ids are unique;
  - errors keep their name and message, and unknown jobs are rejected;
  - an aborted signal rejects before anything is posted;
  - inputs and results are plain, cloneable data; the shared x array survives the clone by reference;
  - the jobs service's store and its cleanup.
- **Architecture:** imports and globals per layer, frozen records, and registries for properties, compares, predicates, chart types, requirements, columns, quick selections, hit tests and jobs. Module headers cite their spec section, and comments carry no history.
- **Performance, 963 comets, Node:**
  - the first filter on `CurrentMag` takes 9 ms; a sort takes 1 ms once the values are cached;
  - a multiple ephemeris takes 0.2–0.3 s, with at most 5 ms between yields;
  - a 6-month graph takes 0.23 s; a 299-year graph 3.2 s (8533 series, 0.61 M points, cloned in 91 ms);
  - hover takes about 10 µs per call, even over 8533 series.
