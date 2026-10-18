# 08 — Orbit viewer

Desktop: File › Orbit Viewer (`OrbitViewerControl`, `OrbitPanel`, toolbox controls). The viewer is an interactive 3D view of the Sun, the eight planets and the comets, with a movable date and time simulation. Geometry and positions come from the [canonical engine](04-astronomy.md#3d-scene-geometry).

## Layout

- A **canvas** fills the view; its background is black.
- A **toolbox** sits beside it, a column of collapsible panels: Comet, Mode, Display, Date and time, Simulation, Filter on date, Info labels, Misc. **Display** starts collapsed and the others expanded.
  - `T` or a toolbar button shows or hides the toolbox (desktop: Orbit › Show Toolbox, `Ctrl+T`).
  - On narrow screens the toolbox becomes an overlay drawer.
- **View title**: `Orbit Viewer`, plus ` - <selected full>` when a comet is selected, plus ` (<n> comets)` in multiple mode with more than one comet.

## Opening state

1. The comet list is a copy of the app-wide selection, in its order.
2. The selected comet is the **brightest** of the list, by `CurrentMag` now ([02](02-data-model.md#quick-selection)).
3. **Multiple** mode, with every comet of the list loaded.
4. The date is today at 00:00 UTC.
5. The camera is at horizontal angle `h = 15°`, vertical angle `v = 70°`, zoom `z = 100`, centred on the Sun.
6. Display:
   - orbits: Mercury, Venus, Earth, Mars and Jupiter on; Saturn, Uranus, Neptune and Comets off;
   - labels: all eight planets on; Comets off;
   - the selected comet's Orbit, Label and Marker all on.
7. Info labels on. Show in weak colour on. Axes, axis labels and grid off; grid extent 150. Simulation step `1 Day`, direction forward, stopped.

## Camera and projection

All scene geometry is heliocentric ecliptic of date, in AU. The camera has a horizontal angle `h` (rotation about the ecliptic pole), a vertical angle `v` (`0` looks straight down from the north ecliptic pole), a zoom `z`, and a **target**, the position of the centred object. Both angles wrap into [0, 360). `z` is clamped to [1.5, 5000].

For a scene point `p`, take `(x, y, z) = p − target`. The camera coordinates are:

```
Xc = x·cos h + y·sin h
Yc = cos v·(−x·sin h + y·cos h) + z·sin v
Zc = sin v·(x·sin h − y·cos h) + z·cos v       # larger = nearer the viewer
```

The projection is **orthographic**:

- `halfH = (1800 / z)·tan(22.5°)` AU, and `halfW = halfH × width/height`;
- the screen position is `px = (Xc/halfW + 1)/2 · width`, `py = (1 − Yc/halfH)/2 · height`, in CSS pixels;
- nothing is clipped in depth within `±(1800/z + 500)` AU;
- depth testing is on, so nearer geometry hides farther geometry.

At the defaults the view spans about ±7.5 AU vertically, with the ecliptic tilted 70° from face-on.

**Centring.** The target is the Sun (origin), the centred planet, or the centred comet, recomputed every frame so the camera follows a moving body. Centring on a comet locks onto the comet that was selected **when centring began**:

- selecting another comet later does not move the camera;
- double-clicking a comet re-centres on it;
- choosing another centre and then the comet again picks up the current selection;
- with no selection, the comet centre is the origin.

## What is drawn

Draw back to front: grid, planet orbits, comet orbits, axes, bodies, markers, labels, info overlay.

### Planet visibility

A planet's orbit, body and label are drawn only when `z × a ≥ 15`, so they disappear when zoomed out too far for them to be seen. The reference distances `a` are:

| Mercury | Venus | Earth | Mars | Jupiter | Saturn | Uranus | Neptune |
|---|---|---|---|---|---|---|---|
| 0.387 | 0.723 | 1.0 | 1.524 | 5.2 | 9.58 | 19.2 | 30.1 |

### Orbit lines

Orbits are drawn as line strips about 1.5 px wide. WebGL usually draws only 1 px lines, so wider lines SHOULD be drawn as screen-space quads; 1 px is acceptable.

**Above/below split.** Each fragment of an orbit line takes the *upper* colour where its ecliptic Z ≥ 0 and the *lower* colour where Z < 0, a depth cue. This is decided per fragment, so it MUST be done in the shader, using the Z after the date rotation.

| Line | Upper | Lower |
|---|---|---|
| Planet orbit | SteelBlue `#4682B4` | DarkSlateBlue `#483D8B` |
| Earth orbit | SteelBlue | SteelBlue (no split) |
| Comet orbit | Tomato `#FF6347` | Firebrick `#B22222` |
| Highlighted comet orbit | Gold `#FFD700` | DarkOrange `#FF8C00` |

Which comet orbits are drawn:

- **All comets** (Display › Orbit › C): the orbit of every comet that [passes the date filter](#filter-on-date), in comet colours.
- **The selected comet** (Comet › Orbit checked) and **every marked comet**: always drawn, whatever the date filter says. Such a line is **highlighted** when it belongs to the selected comet, the mode is multiple, and at least one of these holds:
  - all-comet orbits are on;
  - some comet is marked and the selected comet is not;
  - more than one comet is marked and the selected comet is one of them.

  Otherwise it uses the comet colours.

### Bodies

Bodies are drawn as round points with antialiased edges.

- **Sun**: 7 px, Orange `#FFA500`.
- **Planets**: 6 px, Lime `#00FF00`.
- **Comets**: diameter `d × 1.25 + 1` px. `d` and the colour come from the comet's magnitude at the current date:

  | Magnitude | d | Colour |
  |---|---|---|
  | < 0 | 6 | White |
  | 0 – < 2 | 5 | White |
  | 2 – < 3 | 4 | White |
  | 3 – < 5 | 4 | Silver `#C0C0C0` |
  | 5 – < 8 | 3 | Silver |
  | 8 – < 12 | 2 | DarkGray `#A9A9A9` |
  | ≥ 12 | 2 | `rgb(70,70,70)` |

  A comet that fails the date filter is drawn in weak colour `rgb(25,25,70)` instead. Comet dots are drawn when the comet **passes the date filter**, or **Show in weak colour** is on, or it is the selected comet and the selected-comet **Label** is checked, or it is **marked**.
- **Markers**: a crosshair of four arms around a comet dot. Each arm runs from `d + 4` to `d + 8` px from the centre, up, down, left and right on screen, 3 px wide.
  - The selected comet, when Marker is checked, gets a Red `#FF0000` marker.
  - Marked comets get a DeepSkyBlue `#00BFFF` marker.

### Labels and overlay

Fonts: names in a sans-serif 10 pt font (Helvetica on the desktop), the overlay in a monospaced **bold** 10 pt font (Consolas), axis names at 8.5 pt.

- **Planet labels**: the planet's English name in LimeGreen `#32CD32`, with its top-left corner at the projected point + (5, 0) px. Drawn for visible planets whose label is on.
- **Comet labels**: the comet's `full`, placed the same way. A label is drawn when one of these holds:
  - all comet labels are on and the comet passes the date filter;
  - it is the selected comet and its Label is checked;
  - the comet is marked.

  A comet that fails the date filter, with weak colour on, gets a label only in the last two cases. The colour is White when the mode is multiple, more than one comet is loaded, all comet labels are on, and this is the selected comet with Label checked; otherwise Peru `#CD853F`.
- **Info overlay** (White), with an 8 px margin:
  - top-left: the selected comet's `full`;
  - bottom-left, when **Magnitude and distances** is on and a comet is selected, three lines whose tops are 50, 35 and 20 px above the bottom margin:
    ```
    Magnitude:      {mag,5:0.00}
    Earth Distance: {Δ,9:0.000000} AU
    Sun Distance:   {r,9:0.000000} AU
    ```
    The values are the [ephemeris](04-astronomy.md#ephemeris) at the current date, for an observer at latitude 0, longitude 0;
  - bottom-right, when **Date and Time** is on: `dd Mon yyyy HH:mm:ss UT` (e.g. `18 Oct 2026 00:00:00 UT`), right-aligned, with its top 20 px above the bottom margin. During a simulation this shows the [rounded display date](#simulation).

### Axes and grid

Both are part of Misc. The extent `E = max(0.01, gridExtent)` AU, and `D = E × 800/150` is the pseudo-perspective distance.

**Pseudo-perspective.** In an orthographic view a flat grid seen at an angle has no depth cue. Each point `(x, y, 0)` in the ecliptic plane is therefore scaled:

```
vd = sin v·(x·sin h − y·cos h)
f  = D / max(1, D − vd)
(x, y, 0) → (f·x, f·y, 0)          # points with z ≠ 0 are not changed
```

**Axes.** Six segments from the origin to the transformed points `±E` on X and Y, and `±E` on Z, which is not transformed:

- the + ends are Yellow `#FFFF00`, the − ends DarkOliveGreen `#556B2F`;
- **axis labels**, drawn only when the axes are on and labels are enabled, are Gray `#808080` text at the projected tips: `Vernal equinox` (+X), `Autumnal equinox` (−X), `Summer solstice` (+Y), `Winter solstice` (−Y), `North ecliptic pole` (+Z), `South ecliptic pole` (−Z).

**Grid.** Lines in the ecliptic plane, colour `rgb(30,40,100)`:

```
pxPerAU  = viewHeightPx / (2·halfH)
elev     = |cos v|
ideal    = 50 / (pxPerAU · max(0.15, elev))
steps    = [0.1, 0.2, 0.5, 1, 2, 5, 10, 25]
i        = first index with steps[i] ≥ ideal (last index if none)
while i > 0 and E/steps[i] < 9 and steps[i−1]·pxPerAU ≥ 15: i −= 1
step     = steps[i]
raw      = min(1, elev·3.86)^1.5
zoomNorm = (halfH ≥ E) ? 0 : clamp((pxPerAU·E/150 − 5) / 65, 0, 1)
alpha    = raw + (1 − raw)·(1 − zoomNorm);  if alpha < 0.01: draw nothing
for k = 0 … round(2E/step):
    u = −E + k·step
    if axes are shown and |u| < 0.001·step: skip      # avoids z-fighting with the axes
    line (−E, u) → (E, u) and line (u, −E) → (u, E), endpoints transformed
```

The grid is drawn with that alpha. Depth writes are off when `alpha < 0.99`.

## Toolbox

| Panel | Controls |
|---|---|
| **Comet** | A comet combo box (the viewer's list, by `full`); **Filter** (the [filters panel](05-database-and-update.md#filters-panel) over the whole database, starting from the viewer's own filters and sort; applying it replaces the viewer's list); **Mark**; checkboxes **Orbit**, **Label**, **Marker** for the selected comet |
| **Mode** | **Single** (only the selected comet is shown) / **Multiple** (the whole list is shown) |
| **Display** | A grid with columns `S M V E M J S U N C` (Sun, eight planets, Comets) and rows **Orbit** (no Sun), **Label** (no Sun) and **Center** (radio buttons, Sun and C included). Plus all-on and all-off buttons for the Orbit row and the Label row; **Default** (the opening display state, centred on the Sun); and **All** (every orbit and label on) |
| **Date and time** | A [date picker](09-settings-and-persistence.md#date-picker) button showing UTC `dd.MM.yyyy. HH:mm:ss`, with local time in its tooltip. Presets include **Perihelion Date** = the selected comet's `T` (not `Tn`, as on the desktop) |
| **Simulation** | `<<` play backward, `|<` step back, `||` stop, `>|` step forward, `>>` play forward, and a step menu: `1 Hour`, `6 Hours`, `12 Hours`, `1 Day`, `3 Days`, `10 Days`, `1 Month`, `3 Months`, `6 Months`, `1 Year`, `3 Years`, `10 Years` |
| **Filter on date** | `Distance from Sun ≤` (0–99.99, 2 decimals), `Distance from Earth ≤` (0–99.99, 2 decimals), `Magnitude ≤` (−20–40, 2 decimals), each with a checkbox; **Show in weak color** (on) |
| **Info labels** | **Magnitude and distances** (on), **Date and Time** (on) |
| **Misc.** | **Show axes** (off), **Show axes labels** (off, enabled only while the axes are shown), **Show grid** (off), **Extent** (integer 1–150, default 150), **Save image** |

- **Extent**:
  - it applies on Enter or when the field loses focus;
  - values over 150 are clamped to 150, and values ≤ 0 or non-numbers are ignored;
  - applying a *changed* value also switches the grid on.
- **Mode**: switching to Single keeps only the selected comet. Switching to Multiple reloads the whole list. Marks exist only in multiple mode.
- **Filter on date**:
  - typing a value checks its box when the field is non-empty and unchecks it when emptied;
  - unchecking the box disables the filter but keeps the text.

## Filter on date

At the current date, each loaded comet **passes** when all enabled conditions hold: `SunDist ≤ r`, `EarthDist ≤ d`, `Magnitude ≤ m`. The values come from the ephemeris at the current date (observer 0, 0). With no condition enabled every comet passes. This is recomputed whenever the date or the filter changes.

## Selection, marking and centring

- **Selecting** a comet, from the combo box or by clicking it, makes it the selected comet. The perihelion preset of the date picker follows it.
- **Clicking** the canvas, in multiple mode only, picks the **first** comet in list order whose projected dot lies within ±5 px of the pointer horizontally and vertically. Only comets that pass the date filter, or are marked, or any comet when weak colour is on, can be picked. Clicking where no comet qualifies **clears** the selection.
- **Double-clicking** a comet centres on it. If it was already the centred comet, the click instead toggles the selected comet's Orbit and Label:
  - Orbit on, Label off → turn Label on;
  - Label on, Orbit off → turn Orbit on;
  - otherwise invert both.
- **Mark** (multiple mode) toggles a mark on the selected comet. **Clear marks** removes all marks. Marks are lost when the comet list is replaced by Filter.
- **Esc** clears the selection.

## Simulation

The simulation moves the date continuously, about 60 frames per second (desktop: `docs/06a-simulation-and-navigation-implementation.md`).

- **Step in days**: `stepDays = years·365.25 + months·365.25/12 + days + hours/24`.
- **Each frame**:
  ```
  elapsed = min(ms since previous frame, 100)          # a stall costs movement, never a jump
  clock  += stepDays · (elapsed / 50) · (forward ? 1 : −1)
  date    = clock, clamped to the date range
  ```
  The speed is therefore one step per 50 ms, i.e. 20 steps per second, whatever the frame rate. If the date reaches a range limit, the simulation stops.
- **Display rounding**: the date field and the overlay show a rounded clock, while the scene uses the exact clock:
  - steps under one day round to the nearest multiple of the step within the day, half away from zero;
  - steps of a day or more truncate to 00:00 of the day.
- **Resume** continues from the internal clock, not from the rounded display. Any date change that does not come from the simulation itself (picker, presets, the single-step buttons, `N`, `B`) re-seeds the clock from that date. A date change made by the user while playing stops playback.
- **Single steps** (`|<`, `>|`) stop the simulation and move by the step using calendar arithmetic: add years, then months (clamping the day to the month's length), then days, then hours. The result is clamped to the date range. Vector: [`simulation-steps.json`](vectors/simulation-steps.json).
- **Faster** and **slower** move one entry along the step menu. Slower at the smallest step stops the simulation. Either key, pressed while stopped, starts it.
- The simulation **stops** when the view is hidden (another view, a hidden browser tab) and while the Filter dialog is open. It resumes after the Filter dialog closes if it was running before.

## Keyboard

Keys apply while the canvas has focus or the pointer is over it, and no toolbox text field has focus. Matching uses `KeyboardEvent.code`, so `Shift`+digit works on every keyboard layout; numpad digits behave like digits.

| Keys | Action | Desktop |
|---|---|---|
| `←` `→` | Rotate horizontally (`+` / `−` h), 60°/s while held | same |
| `↑` `↓` | Rotate vertically (`+` / `−` v), 60°/s while held | same |
| `+` or `Q` / `−` or `A` | Zoom in / out, ×2.5 per second while held | same |
| `S` or `0` | Centre on the Sun | same |
| `1`–`8` | Centre on Mercury … Neptune | same |
| `C` or `9` | Centre on the selected comet | same |
| `Shift`+`1`–`8`, `Shift`+`C`/`9` | Toggle that planet's label / all comet labels | same |
| `O` then `1`–`8` or `C`/`9` (within 1 s) | Toggle that planet's orbit / all comet orbits | `Ctrl`+digit, which browsers reserve |
| `Space` or `P` | Start / stop the simulation | same |
| `J` | Reverse the simulation direction | same |
| `K` | Slower (starts the simulation if stopped) | same |
| `L` | Faster (starts the simulation if stopped) | same |
| `G` / `H` / `M` | Toggle the selected comet's Orbit / Label / Marker | same |
| `Enter` | Mark or unmark the selected comet (multiple mode) | same |
| `Delete` | Clear all marks (multiple mode) | same |
| `Esc` | Clear the selection | same |
| `F` or `/` | Find comet | `Ctrl+F` |
| `Shift`+`F` | Filter comets | `Ctrl+Shift+F` |
| `I` | JPL info for the selected comet ([05](05-database-and-update.md#details-orbital-elements-tab)) | `Ctrl+I` |
| `D` | Open the date picker | `Ctrl+D` |
| `N` | Date = today 00:00 UTC | `Ctrl+N` |
| `B` | Date = the selected comet's perihelion `T` | `Ctrl+B` |
| `T` | Show / hide the toolbox | `Ctrl+T` |
| `Ctrl/⌘+S` | Save image | same |

**Held keys** are tracked with `keydown`/`keyup`. Movement is driven by the animation frame and scaled by elapsed time, clamped to 100 ms per frame, so auto-repeat does not matter. All movement stops on `blur`, on `visibilitychange`, and when the pointer leaves the canvas.

## Pointer and touch

- **Drag** with the left or right button rotates: `h −= 0.5° × dx`, `v −= 0.4° × dy`, per CSS pixel. A press that moves less than 4 px is a click. The context menu is suppressed on the canvas.
- **Wheel** zooms by a factor of `1.15` per notch. For pixel-precise deltas use `1.15^(−deltaY/100)`.
- **Touch**:
  - one-finger drag rotates, with the same factors;
  - pinch zooms by the ratio of finger distances;
  - tap selects, like a click;
  - double-tap acts like a double-click.
- The desktop had no panning, only centring, and the web app adds none.

## Find comet

`F` opens a search box with a list of the viewer's comets, sorted by `sortkey`.

- Typing filters the list: lower-case the query and every `full`, keep only letters and digits in both, and match by *contains*.
- Moving through the list (`↑`/`↓` or clicking) **selects** that comet in the viewer live.
- Enter or a double-click closes the box.

## Save image

Saves the frame shown at the moment of the request, captured before any dialog opens. The image includes all labels and the overlay, at the canvas's device-pixel size. It downloads as PNG (default) or JPEG, named `Comets_OrbitViewer_<yyyy-MM-dd_HHmmss>.png` in local time. If the frame cannot be captured, show `The orbit panel could not be captured.`

## Performance

- The viewer MUST stay interactive, at 30 fps or better, with the whole MPC database (~1000 comets) loaded in multiple mode on a mid-range laptop.
- Comet orbit vertices are built once per comet list or display change. A date change only updates:
  - the date rotation uniform `Rz(3.82394E-5·d)`;
  - the planet orbits (8 × 301 points);
  - the body positions;
  - the per-comet magnitude and date filter (one ephemeris per comet).
- Antialiasing SHOULD be on (WebGL `antialias: true`). The desktop's Orbit › Antialiasing toggle MAY be offered, in which case it recreates the context.
