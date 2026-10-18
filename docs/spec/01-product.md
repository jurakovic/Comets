# 01 — Product and architecture

## Purpose

Comets is a tool for amateur astronomers who follow comets. It:

1. Keeps a **database of comet orbital elements**, normally downloaded from the IAU Minor Planet Center (MPC) and refreshed periodically.
2. Computes **ephemerides**: tables of a comet's position in the sky and its distances and brightness over a time span, for the user's location.
3. Draws **graphs** of magnitude, Sun distance or Earth distance over time, for one comet or many.
4. Shows an **interactive 3D orbit viewer** of the Sun, planets and comets, with time simulation.
5. **Converts orbital elements** between 21 formats used by planetarium and astronomy programs (19 for export).

## Views

The web app is a single-page application. Each desktop window family becomes a **view**, reached from a persistent navigation bar. Views keep their state while the user moves between them, for the whole browser session.

| View | Route | Replaces (desktop) | Spec |
|---|---|---|---|
| Database | `#/database` (default) | Edit › Database dialog | [05](05-database-and-update.md#database-view) |
| Ephemeris | `#/ephemeris` | File › Ephemeris, ephemeris windows | [06](06-ephemeris.md) |
| Graph | `#/graph` | File › Graph, graph windows | [07](07-graph.md) |
| Orbit viewer | `#/orbit` | File › Orbit Viewer windows | [08](08-orbit-viewer.md) |
| Elements | `#/elements` | File › Orbital Elements windows | [03](03-element-formats.md#elements-view) |
| Update | `#/update` (dialog over the current view) | Edit › Update dialog | [05](05-database-and-update.md#update) |
| Settings | `#/settings` | Edit › Settings dialog | [09](09-settings-and-persistence.md) |
| Help | `#/help` | Help › Controls, Help › About | [below](#help) |

Routes MUST be hash-based so that a static host (GitHub Pages) serves every route from one `index.html`. Reloading a route MUST reopen that view. Transient state such as computed results need not survive a reload.

### Multiple results

On the desktop, the user can keep several ephemeris, graph and orbit-viewer windows open at once. The web app keeps this as **result tabs** inside the Ephemeris and Graph views:

- Each **Calculate** run with "new result" adds a tab titled like the desktop window (e.g. `Ephemeris - C/2023 A3 (Tsuchinshan-ATLAS)`, `Magnitude graph - 34 comets`).
- **Edit settings** on the active tab pre-fills the settings form with that tab's settings. Recalculating replaces that tab's content (desktop: `AddNew = false`).
- Tabs can be closed. The Orbit viewer is a single view, not tabbed.

## Shell

- **Navigation bar**: links to the views; an **Update** action showing how long ago the last update was; Settings; Help.
- **Status area** (replaces the desktop status bar), always visible:
  - comet count `Comets: <selection>` or `Comets: <selection> (<database>)` when a filter hides some (desktop `FormMain.SetStatusCometsLabel`). Activating it opens the Database view;
  - a progress bar with **Cancel** while a long calculation runs (ephemeris or graph over many comets).
- **Theme**: Light, Dark or System ([09](09-settings-and-persistence.md)). It applies immediately; the desktop needed a restart.

### Startup

1. Load settings and the stored database ([09](09-settings-and-persistence.md#storage-layout)). Parse the database exactly as an MPC-format import ([05](05-database-and-update.md#storage)).
2. If the database is empty, the Database view shows an empty state whose primary action downloads elements from the configured URL ([05 § Update](05-database-and-update.md#update)).
3. If **automatic update** is on and a previous update date is stored, compute the whole days since the last update: `floor((nowUtc − lastUpdateDate) / 1 day)`. If that is ≥ the update interval, ask: `Last update was <n> days ago. Update now?` On yes, open Update and start the download (desktop `FormMain_Shown`).

## Platform and hosting

- **Client-only.** No server code, no accounts, no analytics requirement. All computation runs in the browser.
- **Hosting**: GitHub Pages of this repository. The existing Pages site (Jekyll, repository root) serves the README. The app SHOULD live in its own folder (for example `/app/`) so it is published at `https://<owner>.github.io/Comets/app/` without disturbing the existing site. The folder MUST NOT start with `_`, which Jekyll ignores.
- **Technology**: HTML, CSS and JavaScript ES modules with no build step. A build step MAY be introduced, but only if its output is plain static files committed or deployed to Pages. Third-party runtime libraries SHOULD be avoided. If one is used it MUST be vendored, not loaded from a CDN, so the app works offline after first load.
- **Network**: the only remote resources are the elements download URL (default `https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt`, which sends `Access-Control-Allow-Origin: *`) and outbound links (JPL, MPC, GitHub). A user-configured URL works only if its server allows cross-origin requests. The app MUST explain that when a fetch fails ([05](05-database-and-update.md#update)).
- **Offline**: the app SHOULD register a service worker that caches its own files, so it opens and works offline with the stored database.
- **Workers**: ephemeris, graph and bulk sort/filter calculations over many comets SHOULD run in a Web Worker so the UI stays responsive. They MUST report progress and support cancellation, as the desktop does with `IProgress` and `CancellationToken`.
- **3D**: the orbit viewer MUST use WebGL2. If WebGL2 is unavailable it SHOULD show a clear message. A Canvas 2D fallback MAY be offered.
- **Browsers**: current Chrome, Edge, Firefox and Safari on desktop. Layouts MUST also work on tablets and phones (touch input for the orbit viewer is specified in [08](08-orbit-viewer.md#pointer-and-touch)).
- **Code structure**: astronomy, parsing, formatting and filtering code MUST NOT depend on the DOM, so it can run in a Worker and under Node's test runner against the [test vectors](11-test-vectors.md).

## Keyboard shortcuts

Browsers reserve several of the desktop's shortcuts (`Ctrl+N`, `Ctrl+T`, `Ctrl+W`, `Ctrl+1…8`, `Alt+1…8` on Linux, `F5`, `F6`). The web app MUST NOT rely on those. App-wide shortcuts:

| Keys | Action | Desktop |
|---|---|---|
| `Ctrl/⌘+S` | Save the current result: ephemeris text, graph image, orbit viewer image, or element file | `Ctrl+S` |
| `Esc` | Close the open dialog | — |

Each view's shortcuts apply only while that view is active and no text field has focus. The orbit viewer's are in [08](08-orbit-viewer.md#keyboard).

## Messages

Desktop message boxes become non-blocking notifications for information, and confirmation dialogs for questions. The texts are fixed where they are given in this spec. Validation errors MUST point at the offending field: the desktop throws `ValidationException(message, control)` and focuses the control.

## Help

- **Controls**: a reference of mouse, touch and keyboard controls, generated from [08 § Keyboard](08-orbit-viewer.md#keyboard) and the table above. It replaces the desktop `Resources/controls.txt`.
- **About**: `Comets <version>`, copyright line, link to `https://github.com/jurakovic/Comets`, the credits and references from the repository README, licence GPL-3.0.

## Dropped desktop features

| Desktop feature | Web replacement |
|---|---|
| Child windows: tile, cascade, minimize/restore all, close window | Views and result tabs |
| View › Always on top | — |
| View › Show status bar | Status area always shown |
| Remember window position (`Maximized`, `Left`, `Top`, `Width`, `Height`) | — |
| Exit without confirm | — (closing a tab is the browser's business) |
| Network proxy (domain, username, password, host, port) | The browser's own proxy settings |
| Programs: export folder per program | The browser's download location. Default file names are kept |
| Last used import/export folder | — |
| Downloads folder with timestamped copies of every download | The downloaded text is parsed in memory and not kept |
| `settings.ini` and `Comets.db` next to the executable | Browser storage ([09](09-settings-and-persistence.md)) |
| "Check for new version on startup" (dead on desktop: never loaded or saved) | — |
| Image export as BMP and GIF | PNG (default) and JPEG |
| Orbit viewer keyboard navigation continuing while the window is unfocused | Navigation stops when the canvas loses hover or focus |
| Windows dark-mode theming (`DarkModeCS`) | CSS themes |
| DPI-unaware rendering | Device-pixel-ratio-aware canvases |
