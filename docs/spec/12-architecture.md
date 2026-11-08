# 12 — Architecture

How the web app is built: its layers and what each may depend on, the patterns it uses, where state lives, how long work runs, and how it is tested. Documents 01–11 say **what** the app does; this one says **how the code is organised**, so that every part can be tested against the [vectors](11-test-vectors.md), replaced without touching the rest, and extended by adding to it rather than editing it.

The rules marked MUST are enforced by the tooling (ESLint, TypeScript over JSDoc, tests); the others are conventions that reviews hold the code to.

## Goals

1. **Correct by construction.** Everything the vectors cover lives in pure, DOM-free modules that run unchanged in the browser, in a Worker and under `node --test`.
2. **Deterministic.** Nothing in the computing code reads the clock, the time zone, the observer or storage on its own. They are passed in, so a test fixes them.
3. **Replaceable parts.** The UI depends on services, services depend on ports (interfaces), and adapters implement the ports. Storage, network, workers, the clock and the 3D renderer can each be swapped or faked.
4. **Open for extension.** Element formats, comet properties, chart types, routes and keyboard commands are entries in registries. Adding one means adding an entry and its module, not editing a `switch` elsewhere.
5. **No build step.** The files under `app/` are what GitHub Pages serves ([01](01-product.md#platform-and-hosting)). Tooling (lint, types, tests) is development-only and never shipped.

## Folders

```
app/                        the web app, served as is (GitHub Pages: /Comets/app/)
  index.html                the one page: shell markup, stylesheets, js/main.js
  css/                      tokens.css (theme custom properties), then one file per area
  img/                      icons
  js/
    main.js                 composition root: builds adapters, services and the UI, and wires them
    lib/                    generic helpers with no app knowledge (store, emitter, rpc, assert)
    core/                   the domain and the engine: pure functions over plain data
      time/  format/  comets/  astro/  formats/  ephemeris/  graph/  orbit/  input/
      jobs.js               the long computations a Worker can run, by name
    services/               use cases and app state; ports.js names the ports
    adapters/               ports implemented with browser APIs (storage, fetch, worker, clock)
    workers/                Worker entry points
    ui/                     everything that touches the DOM
      shell/                router, navigation, status area, dialogs, notifications, theme, shortcuts
      components/           number field, date picker, comet picker, filters panel, sort menu, result tabs
      views/                one folder per view: database, update, ephemeris, graph, orbit, elements, settings, help
tests/
  unit/                     node --test, mirroring app/js (core, lib, services with fakes)
  vectors/                  node --test, one file per vector file: conformance with docs/spec/vectors
  helpers/                  vector loading, tolerance assertions, fakes (clock, storage, fetch, job runner)
  e2e/                      Playwright, offline and with a fixed clock
  serve.js                  static server for e2e: the repository under /Comets/, as Pages serves it
```

Names: files and folders are kebab-case, and none starts with `_` or `.` (Jekyll, which builds the Pages site, skips them). Modules use named exports only.

## Layers and the dependency rule

Dependencies point inwards: `ui` → `services` → `core`. `adapters` implement what `services` need. `main.js` is the only module that knows every layer.

| Layer | May import | Must not use |
|---|---|---|
| `lib/` | `lib/` | anything app-specific; browser or Node globals |
| `core/` | `core/`, `lib/` | browser or Node globals (`window`, `document`, `self`, `localStorage`, `indexedDB`, `fetch`, `navigator`, timers…); `Date.now()`, `new Date()` without arguments, `Math.random()` |
| `services/` | `services/`, `core/`, `lib/` | the DOM, storage, network: those come in through ports. Allowed globals: `AbortController`, `AbortSignal`, `DOMException`, `setTimeout`, `clearTimeout`, `queueMicrotask`, `structuredClone`, `console` |
| `adapters/` | `adapters/`, `core/`, `lib/` | `services/` (they implement its ports by shape, not by import), `ui/` |
| `workers/` | `workers/`, `core/`, `lib/` | the DOM |
| `ui/` | `ui/`, `services/`, `core/`, `lib/` | `adapters/`: the UI gets adapter instances from `main.js` when it needs one |
| `main.js` | everything | |

These are MUST rules. ESLint enforces the imports (`import-x/no-restricted-paths`) and the globals (per-folder globals, `no-restricted-syntax` for the clock and randomness). TypeScript checks `core/` and `lib/` with no DOM types at all, and `services/` and `workers/` with Worker types only, so a stray `document` fails the type check as well as the lint. Import cycles are not allowed (`import-x/no-cycle`).

`core/` may use `Intl` (time zones, collation): it is part of the language, and its results depend only on its arguments.

## Core

The domain is **plain data and functions**:

- Records (a comet, an ephemeris row, a filter, settings for a calculation, a graph model, a scene) are plain objects, frozen when created. They have no methods and no class identity, so they cross the Worker boundary by structured clone unchanged, compare by value in tests, and serialise as they are.
- Each record has a **factory** in the module that owns its shape (`createComet(fields)` finalizes the derived elements and the sort key, [03](03-element-formats.md#field-access)), and its JSDoc `@typedef` lives there too.
- Validation returns a result instead of throwing: `{ ok: true, value }` or `{ ok: false, errors: [{ field, message }] }`, where `field` names the input the error belongs to, so the UI can point at it ([01 § Messages](01-product.md#messages)).
- Parsing never throws on bad input to its caller. An importer's record parser reads its fields with spec 03's operations, which throw `InvalidRecord` when one fails; `attempt` (`core/formats/text.js`) turns that into a `null` record at the record's edge, and anything else that throws is a bug ([03 § Field access](03-element-formats.md#field-access)).
- `throw` is for programmer errors only (an unknown registry key, a broken invariant).
- Time is passed as numbers: an instant is UTC milliseconds since the epoch (whole seconds where the spec says so), a JD is a float64, and calendar arithmetic works on `{ year, month, day, hour, minute, second }` parts, which allow years ≤ 0 ([04 § Time](04-astronomy.md#time)). `now`, the time zone and the observer are parameters.
- Values that depend on the session (`Tn`, `Perih*`, `Current*`, [02](02-data-model.md#observer-dependent-values)) are not fields of the comet. Core functions that need them take a **value accessor** `(comet, key) => number | string`, and the `Observations` service provides it with its cache. So filtering, sorting and quick selection are pure, and the caching rules live in one place.

Sub-areas:

| Folder | Holds | Spec |
|---|---|---|
| `core/time/` | `JD0`, `JD`, `JDToDateTime`, calendar arithmetic, the date range, local time through `Intl` with an explicit time zone | [04 § Time](04-astronomy.md#time) |
| `core/format/` | .NET custom number formats, composite format items (`{0,-47:0.000}`), date patterns, `HMS` and `Angle`; numbers and white space read as .NET reads them | [03 § Number formatting](03-element-formats.md#number-formatting), [§ Field access](03-element-formats.md#field-access) |
| `core/comets/` | the comet record and its derived elements, designations and sort keys, the property registry, filters, sorting, quick selection, merge, equality, the database view's detail rows | [02](02-data-model.md) |
| `core/astro/` | Kepler solvers, comet and Sun positions, the ephemeris, precession, planets, orbit sampling | [04](04-astronomy.md) |
| `core/formats/` | text conventions and field access, detection, the format registry, one module per format | [03](03-element-formats.md) |
| `core/ephemeris/` | ephemeris settings (defaults, validation), the calculation, the text output | [06](06-ephemeris.md) |
| `core/graph/` | sampling, the axes, the series and lines: a **graph model** the renderer draws and the vectors check; hover hit-testing | [07](07-graph.md) |
| `core/orbit/` | the camera and projection, the **scene model** (what to draw, where, in which colour and size), date filter, grid, picking, the simulation clock | [08](08-orbit-viewer.md) |
| `core/input/` | the rules of the shared input components: what a number field accepts and how `↑`/`↓` step it, the date picker's fields, steps and presets | [09 § Shared components](09-settings-and-persistence.md#shared-components) |

## Services and ports

A service is a use case together with the state it owns. Each is created by a factory that takes its dependencies (constructor injection):

```js
const database = createDatabaseService({ repository, clock });
```

| Service | Owns | Does |
|---|---|---|
| `settings` | the settings | loads, validates, migrates and saves them ([09](09-settings-and-persistence.md)) |
| `database` | the main collection | loads it at start-up, previews and applies an import, deletes, saves after every change ([05](05-database-and-update.md)) |
| `selection` | the app-wide filters, sort and selection | re-applies them when the database changes ([02](02-data-model.md#collections)) |
| `observations` | the session cache of `Tn`, `Perih*`, `Current*` | provides the value accessor, with the clock and the observer from settings |
| `update` | the Update dialog's state | downloads with progress and cancellation, reads files, asks `database` for the preview, applies it |
| `jobs` | the running long computations | runs them through the `JobRunner` port, exposes progress and cancellation to the status area |
| `startup` | | the start-up sequence ([01 § Startup](01-product.md#startup)): what to load, and whether to ask about an update |

**Ports** are the services' view of the outside world, named in `services/ports.js` as JSDoc typedefs:

| Port | Shape | Adapters |
|---|---|---|
| `Clock` | `now(): number`, `timeZone(): string` | system clock; a fake clock in tests |
| `KeyValueStore` | async `get`, `put`, `remove` of strings | IndexedDB, `localStorage`, memory; all tolerate failing storage ([09](09-settings-and-persistence.md#storage-layout)) |
| `TextFetcher` | `fetchText(url, { signal, onProgress })` | `fetch` with a stream reader |
| `JobRunner` | `run(name, input, { signal, onProgress })` | a module Worker; an in-thread runner for tests and as a fallback |

Services never talk to the user. When something needs asking (the start-up update prompt, a long-calculation warning, a delete confirmation), the service returns what to ask, and the view asks and calls back. Services never import the UI.

## State and change

- **One owner per piece of state.** Each piece belongs to one service (or, for view-local state such as a form's fields, one view) and is changed only through that owner's functions.
- **Observable stores.** A service exposes its state as a store from `lib/store.js`: `get()`, `subscribe(listener)` returning an unsubscribe function, and an update that notifies only on a real change. Views subscribe when shown and unsubscribe when disposed. There is no global store.
- **Events** (`lib/emitter.js`) are for things that happen rather than state that is: a notification to show, a job finished.
- Records are immutable; a change produces a new record or a new list, so a store can compare by reference.

## Long computations and the Worker

Ephemerides, graphs and other work over many comets run off the main thread ([01](01-product.md#platform-and-hosting)):

- A long computation is a **generator** in `core/` that yields progress (`{ done, total }`) and returns its result. It runs as fast as it can when iterated synchronously (Node tests), and gives a runner a point to report progress and check cancellation between steps.
- `core/jobs.js` maps job names to those generators: `ephemeris` returns the text of [06](06-ephemeris.md#text-output), `graph` the graph model of [07](07-graph.md). The Worker (`workers/compute.js`) serves exactly that registry over the message protocol in `lib/rpc.js`: `run` → `progress`* → `result` | `error`, and `cancel`. The server runs a generator in time slices, posting the latest progress after each, and schedules the next slice as a task, so that a `cancel` is read between slices (a slice already queued still runs). The in-thread runner joins the same server and client on one thread and clones every message as a Worker would. Both implement `JobRunner` (`adapters/job-runners.js`), so the services and tests do not care which one runs.
- Inputs and results are plain data. Session-cached values a job needs (`Tn`, the observer, `now`) are put into its input on the main thread, so a result never depends on which thread computed it.

## UI

- **Views.** The router (`ui/shell/router.js`) maps hash routes to views through a route table ([01 § Views](01-product.md#views)). A view module is imported on first visit (`import()`), so the orbit viewer's WebGL code loads only when it is opened. Views are kept after their first visit, which keeps their state for the session. A view implements `{ element, show(params), hide(), save?() }`: `hide()` stops its clocks and animation ([05](05-database-and-update.md#details-ephemeris-tab), [08](08-orbit-viewer.md#simulation)), and `save()` is what `Ctrl/⌘+S` runs.
- **Humble views.** A view turns state into DOM and DOM events into service calls; it decides nothing the spec defines. What to show (texts, numbers, colours, which rows) comes from pure functions in `core/` (or a service) that the unit tests and vectors check. The graph and the orbit viewer follow the same split: a model built in `core/`, a renderer in `ui/`.
- **Components** are ES modules that create and own a DOM subtree, expose `element` and a small API, and report changes through callbacks given to them. They do not read services themselves: their owner passes data in. No framework, no virtual DOM, no custom elements.
- **Renderers.** The graph draws its model on a Canvas 2D. The orbit viewer draws its scene through a renderer interface (`resize`, `setOrbits`, `draw`, `capture`) with a WebGL2 implementation, so a Canvas 2D fallback can be added as another implementation ([01](01-product.md#platform-and-hosting)).
- **Commands.** Every action a key or a control can run is a command registered by id with its key bindings and an `enabled` test (`ui/shell/shortcuts.js` for the app, the orbit viewer's own registry for its keys). Key handling, the text-field guard and the help's control list are written once, against the registry ([01 § Keyboard shortcuts](01-product.md#keyboard-shortcuts), [08 § Keyboard](08-orbit-viewer.md#keyboard)).
- **Styling.** Theme colours and sizes are custom properties in `css/tokens.css`, redefined for the dark theme ([09 § Theme](09-settings-and-persistence.md#theme)). Each area has its own stylesheet; class names are prefixed by their component or view.
- **Accessibility.** Controls are native elements (buttons, inputs, labels) wherever one fits; dialogs, menus and tabs carry their ARIA roles and manage focus; everything works from the keyboard; focus is visible; both themes keep text contrast at WCAG AA; non-essential motion respects `prefers-reduced-motion`.

## Patterns

| Pattern | Where | Why |
|---|---|---|
| Layers, ports and adapters | the dependency rule above | the core is testable without a browser; storage, network and workers can be replaced |
| Composition root, constructor injection | `main.js`, service factories | one place decides the concrete parts; tests pass fakes |
| Registry + strategy | element formats, comet properties, compare operators, sort properties, chart types, simulation steps, routes, commands, jobs | open for extension: a new entry, not a new `switch` |
| Factory, value object | records in `core/` | invariants established once; immutable plain data |
| Interpreter | `core/format/`: .NET composite and custom format strings | exporters and text output are written as the spec's format strings, not hand-built padding |
| Repository | the database (MPC text) and settings (versioned JSON) over `KeyValueStore` | the storage format is decided in one place ([09](09-settings-and-persistence.md#storage-layout)) |
| Observer | stores, the emitter | views follow state without services knowing views |
| Command | shortcuts and the orbit viewer's keys | bindings are data; one dispatcher |
| State machine | the Update dialog, the simulation | explicit states and transitions instead of flags |
| Model / renderer (bridge) | graph, orbit viewer | pure models checked by tests; replaceable drawing |
| Humble object | views, components | logic stays where Node can test it |

Applied SOLID: one reason to change per module (a format, a property, a view); extension through registries; every implementation of a port or registry entry honours the same contract and tests (a format module passes the same import and export checks as every other); ports are small and specific; services depend on ports, not on browser APIs.

## Errors

- Expected failures are values: validation results, `null` records, a fetch error the `update` service turns into the spec's message ([05 § Update](05-database-and-update.md#update)).
- Storage failures fall back to memory and never stop the app ([09](09-settings-and-persistence.md#storage-layout)).
- Cancellation uses `AbortSignal` everywhere and is not an error to show.
- The shell catches `error` and `unhandledrejection`, logs them and shows a notification, so one failing part does not leave the page blank.

## Testing

| Level | Runs | Checks |
|---|---|---|
| Unit | `node --test tests/unit` | `lib/`, `core/` and `services/` (with fakes from `tests/helpers/`) |
| Vectors | `node --test tests/vectors` | every file of [`docs/spec/vectors`](vectors/) within the [tolerances](11-test-vectors.md#tolerances); the jobs through the `rpc` protocol ([11 § Conformance plan](11-test-vectors.md#conformance-plan) levels 1 and 2) |
| End to end | Playwright | the scripted flows of the conformance plan, offline: the MPC download is answered from a vector file, the clock is fixed with Playwright's clock, the time zone set per test |
| Screenshots | Playwright | every view in both themes at desktop and phone size, written to `test-results/screenshots/`; orbit viewer references once accepted ([11](11-test-vectors.md#conformance-plan) level 4) |
| Manual | [`docs/manual-test.md`](../manual-test.md) | what automation cannot see: real browsers and devices, touch, performance on a mid-range laptop |

A bug fix comes with a test that fails without the fix.

## Types and conventions

- JavaScript ES2022 modules with JSDoc types, checked by TypeScript (`checkJs`, `strict`). Every exported function has typed parameters and return value. Shared shapes are `@typedef`s in the module that owns them, imported where used (`@typedef {import('../core/comets/comet.js').Comet} Comet`).
- No runtime dependencies. Development tools are `devDependencies` and never loaded by the app.
- Text the spec fixes (labels, messages, file names) is copied exactly, in the module that shows it.
- Functions JavaScript gets subtly wrong for this app are banned by lint in `app/js`: `toFixed` (rounds unlike .NET; use `core/format`) and `substr` (does not fail on short lines; use the field access of `core/formats`).

## Extending

| To add | Do |
|---|---|
| An element format | a module in `core/formats/` with its record parser and writer, its registry entry, vectors |
| A comet property | an entry in `core/comets/properties.js` (label, type, unit, tolerance, range, default compare); a value that depends on the session also gets its reader in `services/observations.js` |
| A view | a route-table entry and a folder under `ui/views/` |
| A key or toolbar action | a command in the registry with its bindings |
| A long computation | a generator in `core/`, an entry in `core/jobs.js` |

## Decisions

| Decision | Instead of | Because |
|---|---|---|
| Plain frozen data and functions in `core/` | classes with methods | structured clone to and from the Worker, value comparison in tests, no hidden state |
| One store per owning service | a global store | the app has few shared states; ownership stays obvious |
| Plain ES-module components | a framework, custom elements | no build step, no runtime dependency, explicit lifecycles |
| Views loaded with `import()` on first visit | one script for everything | the shell and the database appear without loading WebGL and charting code |
| Generators for long computations | callbacks or promises inside `core/` | the same code runs synchronously in tests and in slices in a Worker, with progress and cancellation |
| Session-dependent values through a value accessor | fields on the comet | the comet stays immutable; caching rules live in one service |
| Rules enforced by lint and types | conventions alone | many short sessions write this code; the tools hold the line |
