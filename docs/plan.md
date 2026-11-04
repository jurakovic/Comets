# Implementation plan

The web app of [`docs/spec`](spec/README.md), built milestone by milestone. Each milestone is split into items; an item is one session's work: one or a few commits that pass the gate (see `CLAUDE.md`). A milestone ends with green CI, a clean-context review, and its section in [`manual-test.md`](manual-test.md).

Legend: `[x]` done · `[ ]` open · `[~]` partly done (see note)

Work goes top to bottom: the next step is the first `[ ]` item. Each open item fits one session; split an item here when it does not (see `CLAUDE.md`, Sessions).

The order follows the architecture ([spec 12](spec/12-architecture.md)): the pure core first, each part proven against its vectors, then the services and the UI on top of it. Every vector file has a home in some item; the item is not done until its vectors pass.

## M0 — Harness

Exit: `npm run check` is green locally and in CI; the shell routes to every view; a new session can start with "continue with implementation".

- [x] **S0 Harness**: spec review and fixes; architecture (spec 12); `package.json` tooling (ESLint with the layer rules, TypeScript strict per layer, `node --test`, Playwright 1.63.0); `tests/` (helpers, server, fixtures, shell and screenshot tests); the shell skeleton (route table, hash router keeping views, navigation, theme tokens, placeholder views); CI (Web, Spec vectors); `.claude` hook and settings; `CLAUDE.md`, this plan, `manual-test.md`

## M1 — Core engine

Exit: `time.json`, `simulation-steps.json`, `formatting.json`, `names.json` (all 963 MPC comets), `elements.json`, `comets.json`, `ephemeris.json`, `orbit-viewer-positions.json`, `planets.json`, `precession.json` and `orbit-sampling.json` pass within the tolerances of spec 11.

- [x] **S1 Time**: `core/time/` (spec 04, Time): `JD0` (always Gregorian), `JD` of an instant (milliseconds dropped), `JDToDateTime` (Julian before 1582, the 1e-7 h nudge, astronomical years ≤ 0, D5), instants ↔ date parts for any year, calendar arithmetic (add years, months with the day clamped, days, hours), the date range and clamping. Vectors `time.json`, `simulation-steps.json`; `tests/helpers` gains what the vector tests share
- [x] **S2 Number formatting**: `core/format/number.js` (.NET custom formats: 15 significant digits, half away from zero, `#` digits, integer zero padding, sign of −0, literal suffix such as `°`) and `core/format/composite.js` (`{index[,alignment][:format]}` items over numbers, integers and strings). Unit tests: the spec's table and every pattern spec 03, 05, 06, 07 and 08 use
- [x] **S3 Date and angle text**: `core/format/dates.js` (`dd.MM.yyyy. HH:mm:ss`, `dd MMM yyyy HH:mm:ss`, `dd MMM yyyy`, `dd.MM.yyyy HH:mm`, file stamps `yyyy-MM-dd_HHmmss`, local time through `Intl` with an explicit time zone) and `core/format/angles.js` (`HMS`, `Angle`, spec 04). Vectors `formatting.json`
- [x] **S4 Designations**: `core/comets/designation.js` (spec 02): pattern 00 (regex), pattern 02 (the linear procedure, D3), `lastToken`, full name from parts, sort key, `isPeriodic`. Vectors `names.json`, every section, including the 963 MPC comets
- [ ] **S5 Comet record**: `core/comets/comet.js`: `createComet` (T from `JD0`, derived P, a, Q, n with the `0.999999` rule, sort key; frozen), equality, `Tn` for a given now (D6). Vectors `elements.json`; `comets.json` fields that need no ephemeris
- [ ] **S6 Positions**: `core/astro/`: angle helpers (`rev`, `rev2`, degree trigonometry), the three Kepler solvers (iteration limits, D7), `CometXyz`, `SunXyz`, Earth = −Sun. Vectors `orbit-viewer-positions.json`
- [ ] **S7 Ephemeris**: `GetEphemeris` (spec 04, Ephemeris: geocentric and equatorial frames, sidereal time, Alt/Az, magnitude, elongation with its small-angle fallback, position angle, W/E), and the observer values at `Tn` and at now as pure functions. Vectors `ephemeris.json` (340 cases) and the `Perih*`/`Current*` fields of `comets.json`
- [ ] **S8 Planets and precession**: Schlyter's elements with the Jupiter, Saturn and Uranus perturbations, the planet orbit lines (301 points, `Rz(Δλ)`), `PrecessElementsToJ2000`. Vectors `planets.json`, `precession.json`
- [ ] **S9 Orbit sampling**: the adaptive sampler (open and closed ellipse, hyperbola, parabola) and the scene rotations `Rz(N)·Rx(i)·Rz(w)` and the date rotation (spec 04, 3D scene geometry). Vectors `orbit-sampling.json`
- [ ] **S10 Close-out**: clean-context review of `core/` against spec 02 and 04 and the architecture rules; findings in `docs/review-m1.md`, fixes (as items of their own if they do not fit)

## M2 — Element formats

Exit: every `import-expected/*.json` (round trips included), `export/*`, `import-detection.json` and `import-merge.json` pass; the full MPC file (963 comets) imports in under 100 ms under Node.

- [ ] **S1 Text and field access**: `core/formats/text.js` (spec 03, Text conventions and Field access): UTF-8 with BOM, line splitting, `sub`, `from`, `split`, `trim`, `pad4`, strict `int` (never `−0`, which formats as `-00`), `dbl`, `dec`, `round6` (`roundEven` is in `lib/math.js`); the format registry (all 21 descriptors: key, name, software, extension, import, export) and detection over it. Unit tests for every operation, failures included
- [ ] **S2 MPC and merge**: MPC import and export, `ImportMain` (New, Update, NoChanges, first wins, Voyager II duplicates, stable sort-key order). Vectors `import-expected/Soft00Cmt.txt.json`, `full/Soft00Cmt.txt`, `sample.txt`, `export/00-MPC.txt`, `roundtrip-00`, `import-merge.json`, `import-detection.json`
- [ ] **S3 Formats 1–5**: SkyMap, Guide, xephem (ellipse, parabola, hyperbola; perihelion from the epoch), Home Planet, MyStars!: import, export, round trips
- [ ] **S4 Formats 6–10**: TheSky and Autostar, Starry Night, Deep Space, PC-TCS, Earth Centered Universe: import, export, round trips
- [ ] **S5 Formats 11–15, 17, 18**: Dance of the Planets (the corrected header, D2), MegaStar V4.x, SkyChart III, Voyager II, SkyTools; SpaceEngine and Celestia (export only)
- [ ] **S6 Import-only formats**: Comet for Windows, NASA (with the precession, Q7); detection of every input file and edge case; the 100 ms import budget as a test
- [ ] **S7 Close-out**: review of `core/formats/` (byte-exact output, failure paths, registry use); `docs/review-m2.md`, fixes

## M3 — Collections and long computations

Exit: `filters.json`, `sorting.json`, `selection.json`, `ephemeris-text/*` and `graph.json` pass, the last two also through the Worker protocol (spec 11, conformance levels 1 and 2).

- [ ] **S1 Properties and observations**: `core/comets/properties.js` (the 17 properties of spec 02: label, type, unit, tolerance, input range and decimals, default compare, value through the accessor) and the compare operators; `services/observations.js`, the first service (`Tn` and `Perih*` on first use, `Current*` refreshed after 5 minutes; Clock and observer injected; Q11 kept). Unit tests with a fake clock
- [ ] **S2 Filters, sorting, quick selection**: `core/comets/filter.js` (validation message, string terms, number compares with tolerances), `sort.js` (stable in both directions, `Intl.Collator('und')`), `selection.js` (quick select, keeping the selected comet on rebind). Vectors `filters.json` (27 sets over 963 comets), `sorting.json`, `selection.json`
- [ ] **S3 Ephemeris calculation and text**: `core/ephemeris/` (spec 06): settings with defaults from now and validation results naming their fields, the long-calculation count, the calculation as a generator (exact integer steps, D8; requirements; progress per comet), the text output (UTC and local time). Vectors `ephemeris-text/*` (UTC and Europe/Zagreb)
- [ ] **S4 Graph model**: `core/graph/` (spec 07): sampling intervals and samples, the y axis (margins, defaults for several comets, reversed, steps, `00.00` labels), x axis label formats, which series are drawn, perihelion and now lines, the title, hover hit-testing. Vectors `graph.json`
- [ ] **S5 Jobs and the Worker**: `lib/rpc.js` (run, progress, result, error, cancel), `core/jobs.js`, `workers/compute.js`, the Worker and in-thread `JobRunner` adapters, `services/jobs.js` (progress and cancel for the status area). Vector tests of the ephemeris text and the graph through the protocol handler; an e2e check that a job runs in a real Worker and cancels
- [ ] **S6 Close-out**: review of M3; `docs/review-m3.md`, fixes

## M4 — App foundation and the Database

Exit: on first run the app downloads the elements (answered from the vectors in tests), imports and stores them, and finds them again after a reload; the Database view lists, sorts, filters and deletes as spec 05 says; settings persist; the e2e flows of spec 11 level 3 for the download, the preview counts and the filters pass; `manual-test.md` has its M4 section.

- [ ] **S1 Storage and settings**: `KeyValueStore` adapters (IndexedDB, `localStorage`, memory) that survive failing or blocked storage; the database repository (MPC text, one atomic put) and `services/settings.js` (the settings of spec 09: defaults, validation, versioned JSON, migration, a store). Unit tests with failing-storage fakes
- [ ] **S2 Database and selection services**: `services/database.js` (load at start-up as an MPC import, preview of a text, apply: filters cleared and the current sort applied (D9), `lastUpdateDate` from a download, saved after every change (D10); delete), `services/selection.js` (the app-wide filters, sort and selection), `services/startup.js` (spec 01, Startup). Unit tests with fakes
- [ ] **S3 Shell**: status area (comet count, a job's progress with Cancel), notifications, confirmation dialog, the global error handler, the app's command registry (`Ctrl/⌘+S` to the view's `save`, `Esc`), the theme from settings applied live, the Help view (controls from the registries, About)
- [ ] **S4 Number field and sort menu**: the number field of spec 09 (accepted input, `beforeinput`, `↑`/`↓`, ranges, `,` → `.`), its rules as pure functions with unit tests; the sort menu (spec 02, Sorting)
- [ ] **S5 Date picker**: button with the local-time tooltip, the editor (D M Y H M S, local time, JD with 5 decimals, `↑`/`↓` calendar steps, OK clamps), the presets menu (spec 09, Date picker)
- [ ] **S6 Update dialog**: `#/update` as a dialog over the current view (spec 05, Update): download through a `TextFetcher` adapter (progress, Cancel, errors with the cross-origin hint), local file (chooser and drop zone), the preview (format, counts, tooltip), Import, automatic mode and the start-up prompt; e2e with the MPC URL answered from `vectors/import/full/Soft00Cmt.txt` and the counts of `import-merge.json`
- [ ] **S7 Database view: list and elements**: title, the comet list with keyboard selection, sort menu, Reset, Delete with its confirmation setting, the empty state; the Orbital elements tab and JPL Info (D16)
- [ ] **S8 Database view: live ephemeris**: the Ephemeris tab with its live clock (ticks on the second, paused while hidden, restarted by the picker) and the trend arrows and colours; the import result browser
- [ ] **S9 Filters panel**: up to 10 rows, defaults, add and remove, the property, compare and value editors, units, rows switching on and off as values change, live apply, Apply and Enter, inline validation, `Esc` (spec 05, Filters panel); e2e against `filters.json` cases
- [ ] **S10 Settings view**: every setting of spec 09, saved as changed; Restore default; location with N/S and E/W; Use my location; Export database; Clear all data
- [ ] **S11 Close-out**: review of M4 (services, storage, UI architecture); `docs/review-m4.md`, fixes; `manual-test.md` M4 section

## M5 — Ephemeris, Graph, Elements

Exit: a saved ephemeris equals `ephemeris-text/*` with the clock and time zone fixed; the graph draws `graph.json`'s model; a saved element file equals `export/*` for the sample; `manual-test.md` M5 section.

- [ ] **S1 Comet picker**: `[ALL COMETS (n)]`, quick select, the tool's own Filter and Sort, the tooltip, the Perihelion date preset following the comet (spec 05, Comet picker)
- [ ] **S2 Ephemeris view**: the settings form (spec 06) with errors at their fields, the long-calculation question, the run through `jobs` with progress and Cancel, result tabs (new, or replacing the edited one), Save and Copy; e2e: each saved text equals its vector
- [ ] **S3 Graph renderer**: a Canvas 2D chart of the graph model, sharp at any device pixel ratio: reversed y, the time axis on top with labels thinned to fit, smooth series, perihelion and now lines, title, theme colours followed live; hover tooltips (spec 07)
- [ ] **S4 Graph view**: the settings form (chart type, colours, value range rules), the run through `jobs`, result tabs, Save as PNG or JPEG
- [ ] **S5 Elements view**: the 19 export formats, read-only monospaced text regenerated on every change, Save (name in local time, CRLF), Copy (spec 03, Elements view); e2e: Save equals `export/*`
- [ ] **S6 Close-out**: review of M5; `docs/review-m5.md`, fixes; `manual-test.md` M5 section

## M6 — Orbit viewer

Exit: the visual checklist of spec 11 level 4 passes and its reference screenshots are accepted by the user; at least 30 fps with the whole database in multiple mode on a mid-range laptop (manual); `manual-test.md` M6 section.

- [ ] **S1 Scene model: camera and bodies**: `core/orbit/` (spec 08): camera angles and zoom, the orthographic projection, centring rules, planet visibility by zoom, comet sizes and colours by magnitude, the date filter (observer 0, 0) and the weak colour
- [ ] **S2 Scene model: lines and text**: which orbits are drawn and highlighted, labels and their colours, markers, the info overlay text, axes, the grid (pseudo-perspective, step choice, fading), picking within ±5 px
- [ ] **S3 WebGL2 renderer: orbits**: context (antialiasing, device pixel ratio), the line shaders with the upper/lower colour split per fragment after the date rotation, comet line buffers built per list change, planet orbits per date, the message when WebGL2 is missing
- [ ] **S4 WebGL2 renderer: bodies, grid, overlay**: round antialiased points, markers, grid and axes with their alpha and depth rules, the 2D overlay for labels and the info text, capture for Save image
- [ ] **S5 Toolbox and view state**: the panels (Comet, Mode, Display, Date and time, Simulation, Filter on date, Info labels, Misc with the Extent rules), the opening state, the title, single and multiple mode, marks, Find comet
- [ ] **S6 Input**: drag, click and double-click rules, wheel, touch (drag, pinch, tap, double-tap), the key registry (held keys moving by elapsed time, the `O` chord, `Shift`+digit by `code`), stopping on blur, hidden and leave; the Help lists the keys
- [ ] **S7 Simulation and Save image**: the simulation clock (20 steps a second, elapsed time clamped), display rounding, resuming from the internal clock, single calendar steps (`simulation-steps.json`), faster and slower, the stop conditions; Save image (PNG or JPEG); a frame-rate measurement with the full database
- [ ] **S8 Close-out**: review of M6; the visual checklist and reference screenshots; `docs/review-m6.md`, fixes; `manual-test.md` M6 section

## M7 — Offline and release

Exit: the app opens and works offline after one visit; the e2e suite passes on Chromium, Firefox and WebKit in CI; the app is ready to publish from `master`; final review done.

- [ ] **S1 Service worker and manifest**: a precache list generated by a script and held current by a test, versioned caches and the update flow, the web app manifest and icons; an offline e2e test
- [ ] **S2 Cross-browser**: Firefox and WebKit projects in CI's browser tests; fixes
- [ ] **S3 Responsive and accessible**: phone and tablet layouts (the toolbox as a drawer), focus order, ARIA roles, contrast in both themes, reduced motion
- [ ] **S4 Ready for Pages**: the version in About, the repository README's section on the web app, Jekyll's handling of the tooling folders checked (`_config.yml` `exclude` if needed); merging `dev` into `master` stays the user's step
- [ ] **S5 Close-out**: whole-app review (architecture, conformance, performance, accessibility); `docs/review-m7.md`, fixes; a full pass of `manual-test.md`

## Deferred decisions

For the user; none blocks the plan.

1. **Desktop quirks** (spec 10, Quirks kept): the web app keeps Q1–Q20 so the vectors stay authoritative. Each one fixed later is its own change to the spec, the harness and the vectors. The spec recommends fixing Q1–Q5, Q7, Q9, Q11 and Q16.
2. **Publishing**: when `dev` is merged into `master`, Pages serves the app at `/Comets/app/`. Whether to link it from the repository README and the Pages home page before M7 is open.

## Decision log

| # | Date | Decision |
|---|---|---|
| D1 | 2026-11-04 | All web work happens on `dev`, created from `master`. The spec commit was moved there unchanged, authored by the repository owner with its original dates. |
| D2 | 2026-11-04 | Spec review: the vectors reproduce byte for byte from the desktop sources (.NET 8, libfaketime), the name parsers agree on 41 075 inputs, `Intl.Collator('und')` with a stable sort reproduces every case of `sorting.json`, all spec links resolve, and the MPC download sends `Access-Control-Allow-Origin: *`. Fixed: the "greather" message (D15), the orbit viewer's perihelion preset wording, the download progress with a compressed response, the ephemeris vector's observer count. |
| D3 | 2026-11-04 | **Architecture** as spec 12: layers `lib`, `core`, `services`, `adapters`, `workers`, `ui` and the composition root `main.js`; plain frozen data in `core`; four ports; stores with one owner; generators for long work behind one rpc protocol; humble views over pure models; registries instead of switches. Enforced by ESLint (`import-x/no-restricted-paths` zones, per-layer globals, no clock or randomness in core) and by TypeScript run three times with different `lib`s (core without DOM, services and workers with Worker types, the UI with the DOM). |
| D4 | 2026-11-04 | **Tooling**: Node 22; ESLint 10 with `eslint-plugin-import-x`; TypeScript 5.9 over JSDoc (`checkJs`, `strict`), the JavaScript-based compiler whose JSDoc checking meteo already relies on, rather than the native 7; `node --test`; Playwright pinned to 1.63.0, whose Chromium cloud sessions have pre-installed. No runtime dependencies, no build step, no formatter (as in meteo): `.editorconfig`, LF and tabs for the web files. |
| D5 | 2026-11-04 | **Serving and tests**: the app lives in `app/` and is served as is; `tests/serve.js` puts the repository under `/Comets/`, as Pages does, so wrong absolute paths fail in tests. Browser tests run offline (fixtures answer or refuse every outside request) on desktop Chrome and a Pixel 7 (Chromium); Firefox and WebKit join in M7 S2. Screenshots of every view in both themes are a CI artifact. |
| D6 | 2026-11-04 | **CI**: the Web workflow runs the gate on `dev`, `master` and pull requests when the app, the tests, the vectors or the tooling change; the Spec vectors workflow regenerates the vectors with .NET 8 and libfaketime and fails on any difference when the generator, the vectors or the desktop engine change. |
| D7 | 2026-11-04 | The shell grid's column is capped at the window width (`minmax(0, 1fr)`): the phone test showed the navigation widening the page to 774 px on a 412 px screen. A test checks that no view scrolls sideways. |
| D8 | 2026-11-04 | **Time in core**: instants are UTC milliseconds, and `core/time/calendar.js` converts them to and from date parts with Hinnant's day-count algorithms instead of `Date`, so years 0–99 and ≤ 0 need no special case. Calendar arithmetic follows .NET's `AddMonths` and `AddYears` (the day clamped, the time of day kept). The orbit viewer's step menu and single steps live in `core/orbit/steps.js`, with the desktop's skip over 1582-10-05…14 (spec 08 now says so). |
| D9 | 2026-11-04 | **Number formatting** takes the exact decimal digits of a float64 from its bits (BigInt) and rounds them to 15 significant digits with ties to even, as .NET does; spec 03's `toPrecision(15)` rounds those ties up. Checked once against .NET 8 on 114 000 random values over every pattern the spec uses (all equal; `toPrecision(15)` differed on 173); a sample is in the unit tests. Templates compile once (`compileFormat`) so exporters keep their line formats as constants. |
