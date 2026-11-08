# 05 — Database and update

## Storage

The database is stored in the browser as the **MPC-format export** of the main collection ([03](03-element-formats.md#mpc)): one text value, exactly as the desktop writes `Comets.db`. On startup it is parsed with the MPC importer into an empty database ([03 § Merge](03-element-formats.md#merge-import-into-the-database)).

- Storing MPC text keeps the desktop's behaviour exactly, including the precision the stored data has after a restart (6 decimals for q and e, 4 for the angles, a 4-digit day fraction).
- The stored text MUST be rewritten after every change: after an import and after a deletion. The desktop wrote it on exit; the web app has no exit.
- The storage layout is in [09](09-settings-and-persistence.md#storage-layout).

## Update

Desktop: Edit › Update (F6). The Update dialog opens over the current view (route `#/update`) and offers two sources. The one loaded **most recently** becomes the import candidate.

- The view it was opened over stays under it; after a reload on `#/update`, the Database view is under it. **Close** and `Esc` go back to that view (Back, when a navigation opened the dialog). Closing stops a running download.
- Each opening starts afresh, as the desktop's new form did: no source, no candidate.
- `#/update?automatic` opens it in [automatic mode](#automatic-update); `#/update?download` starts the download at once in the normal mode (the empty database's action).

### 1. Download

- Label: `Download the latest orbital elements from Internet`.
- Shows the configured URL, with an **Open in browser** link.
- **Download** fetches the URL with `fetch`, revalidating any cached copy. It shows a determinate progress bar when the response has `Content-Length` and no `Content-Encoding` (a compressed response's length counts compressed bytes, not the bytes the stream delivers), and an indeterminate one otherwise. A cross-origin response hides its `Content-Encoding` unless the server exposes it, so a length the body outgrows is dropped and the bar becomes indeterminate. **Cancel** aborts the download, which is not an error. A download can be made again; the desktop downloaded once per dialog.
- **Errors** are shown in the dialog, under the download:
  - On a network or HTTP error: `Download failed: <reason>.`, where the reason is `network error`, or `HTTP <status> <status text>` (`HTTP 404 Not Found`), or `HTTP <status>` for a response without a status text, as over HTTP/2. If the URL is not the default, add `The server must allow cross-origin requests (CORS) from this page, or the browser refuses the download.`
  - An empty body is an error: `Download failed: the response is empty.` The desktop shows the transport error text for this case.
- The downloaded text is parsed in memory, decoded as a file is ([03 § Text conventions](03-element-formats.md#text-conventions)). The desktop kept timestamped copies in a `Downloads` folder; the web app does not.

### 2. Local file

- Label: `Import from local file`.
- A file chooser (`.txt`, `.dat`, `.csv`, `.comet`, any file) and a drop zone; the chosen file's name is shown. A file dropped elsewhere in the dialog, or in automatic mode, is refused rather than opened by the browser. A file that cannot be read shows `The file could not be read.`, and is no source.

### Import preview

As soon as a source is loaded, the dialog shows:

- **Import format**: the detected format's name ([03 § Detection](03-element-formats.md#detection)), or `(no file selected)`, `(empty file)` or `(unknown)`.
- **Total comets**: the result of merging the file into the current database, without applying it yet. It shows the counts as `<new>, <updates>, <no changes>; <total>`, with the tooltip `New, updates, no changes; total`, and `-` without a tooltip while there is no format. The counts count the merged list by import result ([03 § Merge](03-element-formats.md#merge-import-into-the-database)), so `<total>` includes comets that are only in the database.
- A **View** action opens the merged list in an [import result browser](#import-result-browser).

### Import

Import applies the preview:

1. If the merged list is not empty:
   - it replaces the database, and the selection becomes the whole database;
   - **filters are cleared**; the current sort is applied ([10](10-quirks.md#deviations));
   - if the candidate came from the **download**, `lastUpdateDate` is set to the current UTC time and saved;
   - the database is saved, the user is told `Import complete` (an information notice), and the dialog closes when **Close when finished** is checked, as it is by default; otherwise the counts show the candidate against the new database.
2. If the merged list is empty, show the error `Something wrong happened. Zero comets imported.` (an error notice) and keep the dialog open.
3. Without a format (no source, an empty file, an unknown format), Import does nothing.

### Automatic update

The startup prompt is described in [01](01-product.md#startup). If the user accepts it, the Update dialog opens in **automatic mode**: the download starts at once, the local-file source is disabled, **Close when finished** is hidden, and the dialog closes after **Import** whether or not anything was imported.

## Database view

Desktop: Edit › Database (F5), a modal dialog with OK and Cancel. On the web it is a normal view whose changes **apply immediately** to the app-wide selection. The selection persists across views for the session.

### Layout

- **Title**: `Database (<n> comets)`, or `Database (<n>/<total> comets)` when filters hide some.
- **Comet list**: the `full` names of the selection in its current order. Selecting an entry shows its details. The arrow keys, Home, End, Page Up and Page Down move the selection; a double click switches the details tab, as on the desktop.
  - After a sort, a filter or an import, the comet selected stays selected while it is in the list; otherwise the first is. The desktop selected the first after every change.
- **Sort menu** ([02 § Sorting](02-data-model.md#sorting)): the property list plus Ascending and Descending, with the current choice marked.
- **Filters panel**, toggled by `Filters ▼/▲` (described below).
- **Reset**: clears the filters back to the default rows, restores the default sort (Designation, ascending), and shows the whole database.
- **Delete**:
  - Removes the selected comet from the database and the selection, after the confirmation `Do you really want to delete <full>` when that setting is on ([09](09-settings-and-persistence.md)).
  - The list selection moves to the same index, clamped to the list.
  - The database is saved.
  - Without a selected comet, Delete is disabled.
- **Empty database**: an empty state, `The database is empty.`, with a **Download comet elements** action, which opens Update and starts the download (`#/update?download`). A database whose filters hide every comet shows the empty list instead.
- **Details**: two tabs, **Ephemeris** (first) and **Orbital elements**. A value shown only below a period of 1000 years is left empty beyond it, as on the desktop.

### Filters panel

([02 § Filters](02-data-model.md#filters))

- Up to **10** rows. The **default rows** are Full name, Perihelion date, Perihelion distance and Current magnitude, all inactive.
- **+** adds a row (property Full name) and is hidden at 10 rows.
- Each row has:
  - an **active** checkbox;
  - a **property** menu (all 17 properties). Changing the property resets the compare to its default and shows the right value editor;
  - a **compare** menu: `Contains`, `Does not contain`, `Starts with`, `Ends with` for strings; `Greather than (>)`, `Less than (<)`, `Equals (=)` for numbers. The desktop spells "Greather"; the web app SHOULD spell it "Greater";
  - a **value editor**: a text field for strings; a [number field](09-settings-and-persistence.md#number-fields) with the property's range and decimals; or a [date picker](09-settings-and-persistence.md#date-picker) for Perihelion date, defaulting to the default start date;
  - a **unit** label (AU, years, °);
  - a **remove** button.
- Editing a value makes its row active when the value is non-empty, and inactive when it is emptied. Choosing a date makes the row active.
- Filters apply **live** on every edit, and also on **Apply** (or Enter). A validation error (`Please enter value for "<Label>"`) is shown next to the row and leaves the previous result in place. `Esc` closes the panel.

### Details: Ephemeris tab

This is the first tab. The values are for the observer location in settings.

| Field | Value and format |
|---|---|
| Name | `full` |
| Perihelion date | `JDToDateTime(Tn)` as `dd.MM.yyyy. HH:mm:ss` |
| Period | `P` as `0.000000` years, shown only if `P < 1000` |
| Aphelion distance | `Q` as `0.000000` AU, shown only if `P < 1000` |
| Perihelion distance | `q` as `0.000000` AU |
| Perihelion distance from Earth | `PerihEarthDist` as `0.000000` AU |
| Perihelion magnitude | `PerihMag` as `0.00` |

There is also a **live ephemeris** block:

- A [date picker](09-settings-and-persistence.md#date-picker) shows the UTC instant `dd.MM.yyyy. HH:mm:ss`, with local time in its tooltip. Its presets include **Perihelion date** (`Tn`). It starts at the current time when the view opens.
- The clock **ticks once per second**:
  - ticks are aligned to the system clock's second boundary;
  - each tick adds exactly 1 s to the shown instant and recomputes;
  - picking a date restarts the clock from that date;
  - ticking stops while the view is hidden (another view shown, or `visibilitychange`) or no comet is selected, and resumes on return. Until a date is picked the clock follows the system clock, so it resumes at the current second, as it does when a comet is selected; a picked date resumes where it stopped. The desktop went back to the current time whenever its timer restarted;
  - a tick runs 20 ms after the boundary, as the desktop's sync window (15–35 ms) has it; a tick that comes late still adds 1 s.
- Values at the shown instant:

  | Field | Format |
  |---|---|
  | Current distance from Sun | `0.000000` AU |
  | Current distance from Earth | `0.000000` AU |
  | Current magnitude | `0.00` |
  | Right Ascension | `HMS(RA/15)` |
  | Declination | `Angle(Dec, false, true)` |
  | Altitude | `0.00°` |
  | Azimuth | `0.00°` |
  | Elongation | `0.00° W` or `0.00° E` |
- **Trend arrows** compare each value with the previous tick, or with the instant 1 s earlier on the first computation. `▲` means the value is ≥ the previous one, `▼` that it is smaller. Colours (desktop `EphemerisControl`):

  | Value | ▲ | ▼ |
  |---|---|---|
  | Sun distance, Earth distance, magnitude | red (worse) | green |
  | Altitude, elongation | green | red |
  | Declination, observer latitude ≥ 0 | green | red |
  | Declination, observer latitude < 0 | red | green |
  | RA, azimuth | neutral | neutral |

  Red and green are theme colours with text contrast in both themes; neutral is the text colour (the desktop's black).

### Details: Orbital elements tab

| Field | Value and format |
|---|---|
| Name | `full` |
| Elements perihelion date (T) | `<Ty>-<Tm:00>-<Td:00>.<Th:0000>` |
| Period (P) | `0.000000` years, if `P < 1000` |
| Mean motion (n) | `0.000000` °/day, if `P < 1000` |
| Perihelion distance (q) | `0.000000` AU |
| Aphelion distance (Q) | `0.000000` AU, if `P < 1000` |
| Semi-major axis (a) | `0.000000` AU, if `P < 1000` |
| Eccentricity (e) | `0.000000` |
| Ascending node (Ω) | `0.0000` ° |
| Inclination (i) | `0.0000` ° |
| Argument of pericenter (ω) | `0.0000` ° |
| Magnitude parameters (g, k) | `0.0`, `0.0` |
| Equinox | `2000.0` |

**JPL Info** opens `https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=<id>` in a new tab. The `id` has no fragment and is URL-encoded.

## Import result browser

This is the database list opened from the Update dialog's **View** action, read-only, as a dialog over it (Close and `Esc` go back to the Update dialog, and it closes with the Update dialog). View is disabled while there is no format.

- the title as the Database view's, `Database (<n>/<total> comets)`, where the total is the merged list;
- an **Import result** selector at the top: `New comets`, `Updates`, `No changes`, `All comets`, starting at `New comets` as on the desktop;
- the list shows only comets with the chosen result, in merged order;
- the details tabs work as in the Database view; Delete and the filters are not available.

## Comet picker

The Ephemeris and Graph settings share this component. Desktop: `SelectCometControl`.

- A combo box whose first entry is `[ALL COMETS (<n>)]`, followed by the tool's comet list in its order. Choosing the first entry is **multiple mode**.
- A **quick-select** menu: Brightest, Closest to perihelion, Closest to Earth, Closest to Sun ([02](02-data-model.md#quick-selection)).
- A **Filter** action opens the filters panel over the **whole database**, starting from the tool's own filters and sort. Applying it replaces the tool's comet list and remembers the filters and sort for that tool only. The app-wide selection is not changed. The previously selected comet is kept if it is still in the list; otherwise the Brightest is picked. If "all comets" was chosen, it stays chosen.
- A **Sort** menu re-sorts the tool's list.
- A **tooltip** on the selected comet: the title is `full`, and the body is
  ```
  T: <JDToDateTime(Tn) as dd MMM yyyy HH:mm:ss>
  P: <P as 0.0000> years    (or "-" when P ≥ 10000)
  q: <q as 0.000000> AU
  r: <CurrentSunDist as 0.000000> AU
  d: <CurrentEarthDist as 0.000000> AU
  m: <CurrentMag as 0.00>
  ```
- When the comet changes, the tool's date pickers update their **Perihelion date** preset to the new `Tn`.
