# 07 — Graph

Desktop: File › Graph (`FormGraphSettings`, `FormGraph`, `EphemerisManager.GenerateGraph`). The graph plots one quantity against time for one comet or for many. Vector: [`graph.json`](vectors/graph.json). The harness records exactly what `GenerateGraph` asked the chart to draw.

## Settings

| Group | Control | Default | Rules |
|---|---|---|---|
| Comet | [Comet picker](05-database-and-update.md#comet-picker) | Brightest | Single comet, or all comets |
| Timespan (Universal Time) | Start, End | As in [Ephemeris](06-ephemeris.md#settings) | Same validation (End > Start, < 300 years) |
| Chart type | Magnitude / Sun distance / Earth distance | Magnitude | |
| Chart options | Value colour | `#FF0000` (Red) | Colour picker |
| | Perihelion line, with colour | on, `#4169E1` (RoyalBlue) | |
| | Now line, with colour | on, `#32CD32` (LimeGreen) | |
| Value range | Minimum | off | −20–40, 2 decimals |
| | Maximum | **on, 12** | −20–40, 2 decimals |

- Typing in a range field checks its box when the field is non-empty and unchecks it when emptied. Validation:
  - `Enter Minimum value`;
  - `Enter Maximum value`;
  - `Minimum value must be lower than Maximum value` when both are checked and Min ≥ Max.
- The Maximum default of 12 applies to every chart type, distances included, as on the desktop.
- The desktop also had an **Antialiasing** option. The web app always antialiases and drops the option.
- **Theme colours**: background `#FFFFFF` with black text in the light theme; background `rgb(55,55,55)` with white text in the dark theme. The desktop chooses them when the graph is created, and the web app SHOULD follow theme changes live.

## Sampling

The interval depends on the span `S = JD(End) − JD(Start)` in days:

| S | Interval (days) |
|---|---|
| ≤ 100 | S / 100 (always 101 samples) |
| < 365 | 1 |
| < 10 × 365.25 | 2 |
| < 50 × 365.25 | 5 |
| < 100 × 365.25 | 15 |
| < 200 × 365.25 | 30 |
| < 300 × 365.25 | 40 |

The samples are taken at `JD(Start) + k·interval` for `k = 0 … floor(S / interval)`, compared exactly ([06](06-ephemeris.md#calculation)). Each sample is an ephemeris for the configured observer, **without** the ephemeris requirements filter. The long-calculation check and the cancellation are as for ephemerides. Vector: `samplingIntervals` in `graph.json`.

## Value axis (y)

For each comet, the plotted value per sample is `Magnitude`, `SunDist` or `EarthDist`. Let `min` and `max` be the smallest and largest value over **all** comets with samples.

```
if chart = Magnitude: min −= 0.20; max += 0.20
minValue = floor(min);  maxValue = ceil(max)

yMin = MinimumChecked ? Minimum : minValue
yMax = MaximumChecked ? Maximum
     : (multiple mode and more than one comet has samples) ? (Magnitude ? 15 : 2)
     : maxValue
if yMin ≥ yMax: yMin = minValue; yMax = maxValue

span = yMax − yMin
step = span ≤ 1 ? 0.1 : span ≤ 2 ? 0.2 : span ≤ 5 ? 0.5 : span ≤ 10 ? 1 : 2
```

- The y axis is **reversed** for every chart type: smaller values are at the top, so brighter or closer is higher.
- Labels use the format `00.00`, for example `02.00` and `-01.50`, with a tick at `yMin` and then every `step` up to `yMax`. There are no grid lines.
- Axis titles: `Magnitude`, `Sun distance (AU)`, `Earth distance (AU)`.

## Time axis (x)

- The time axis is drawn at the **top** of the chart (the desktop's secondary axis) and is titled `Date`.
- Its range is exactly Start to End, with no margins.
- Labels use `dd MMM yyyy HH:mm` when `S ≤ 3` days, otherwise `dd MMM yyyy`. Tick spacing is automatic. There are no grid lines.

## Series

1. **Value series**: one smooth curve per comet, in list order and in the value colour. A comet's series is drawn only if **at least one** of its samples has a value `≤ yMax`. The desktop draws the curve as a cardinal spline (tension 0.5); straight segments or a monotone spline are acceptable.
2. **Perihelion lines**: vertical lines in the perihelion colour, from `yMin` to `yMax`:
   ```
   t = Tn;  p = P × 365.25
   while t − p > JD(Start): t −= p
   while t < JD(End): line at t; t += p
   ```
   `P` is held to 15 significant digits, as for `Tn` ([02](02-data-model.md#nearest-perihelion-tn)), and each `t` is computed afresh as `Tn + m·p`. A period that is not positive and finite gives the line at `Tn` alone, if `Tn < JD(End)` ([10](10-quirks.md#deviations) D6). Lines outside the range are not visible. In single mode they are drawn for the selected comet, whether or not its value series was drawn. In multiple mode they are drawn for every comet whose value series was drawn, right after it.
3. **Now line**: a vertical line at `JD(now)` in the now colour, drawn only if `JD(Start) < JD(now) < JD(End)`.
4. **Title**: the comet's `full` in single mode; no title in multiple mode.

## Interaction

- **Hover** over a sample, a perihelion line or the now line shows a tooltip. Times are UTC as `dd.MM.yyyy. HH:mm:ss`. Within a few pixels of the pointer, the topmost item wins (the series drawn last), and in a value series the nearest sample; only what lies inside the axes can be hovered.

  | Hovered | Title | Body |
  |---|---|---|
  | value series | the comet's `full` | `Time: <t>` and then `Magnitude: <0.00>` or `Distance: <0.0000> AU` |
  | perihelion line | the comet's `full` | `Perihelion time: <t>` |
  | now line | `Today` | `Time: <t>` |

- **Save** (`Ctrl/⌘+S`) downloads a PNG, or JPEG on request, of the chart at its displayed size times the device pixel ratio. The name is `Comets_Graph_<yyyy-MM-dd_HHmmss>.png`, in local time.
- **Edit settings** and result tabs work as in [Ephemeris](06-ephemeris.md#result-tab). The tab title follows `GraphSettings.ToString()`: `Magnitude graph - <full>`, `Sun distance graph - <n> comets`, `Earth distance graph - …`.

## Rendering

The chart MUST be drawn with Canvas 2D or SVG, not a heavy charting library. It MUST be readable at phone widths, and the time-axis labels thin out as needed.
