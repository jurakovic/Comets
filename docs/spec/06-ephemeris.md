# 06 — Ephemeris

Desktop: File › Ephemeris (`FormEphemerisSettings`, `FormEphemeris`, `EphemerisManager.CalculateEphemerisAsync`, `GenerateEphemerisAsync`). Vectors: [`ephemeris-text/`](vectors/ephemeris-text/), [`ephemeris.json`](vectors/ephemeris.json).

## Settings

The settings form starts from a copy of the app-wide selection. When editing an existing result tab, it starts from that tab's settings instead.

| Group | Control | Default | Rules |
|---|---|---|---|
| Comet | [Comet picker](05-database-and-update.md#comet-picker) | Brightest of the list | Single comet, or `[ALL COMETS (n)]` |
| Timespan (Universal Time) | Start, End: [date pickers](09-settings-and-persistence.md#date-picker) | Start: the first day of last month; End: the first day of the month five months ahead (00:00 UTC) | End > start (`End date must be greather than start date`); `End − Start < 300 × 365.25` days (`Timespan must be less than 300 years`) |
| Interval | D, H, M number fields | 1 d 0 h 0 min | D 0–3652, H 0–23, M 0–59, integers. **Default** resets to 1 d 0 h 0 min. A total of 0 means 1 day. |
| Output data | Local Time / Universal Time | Local Time | |
| | Right ascension (RA) | on | |
| | Declination (Dec) | on | |
| | Altitude (Alt) | on | |
| | Azimuth (Az) | on | |
| | Ecliptic Longitude | off | |
| | Ecliptic Latitude | off | |
| | Elongation | on | |
| | Sun distance | on | |
| | Earth distance | on | |
| | Magnitude | on | |
| Requirements | Max Sun distance | off | 0–9.99, 2 decimals |
| | Max Earth distance | off | 0–9.99, 2 decimals |
| | Min magnitude | **on, 12** | −20–40, 2 decimals |

- **Default dates**: Start is the 1st of the month containing `now − 1 month`; End is the 1st of the month containing `now + 5 months`; both at 00:00 UTC. They are computed once per session.
- Typing into a requirement field checks its box when the field is non-empty and unchecks it when emptied. A checked requirement with an empty field fails validation: `Please enter Maximum Sun distance value`, `Please enter Maximum Earth distance value`, `Please enter Minimum magnitude value`.
- The interval in days is `D + (H + M/60)/24`.
- The observer is the location in settings ([09](09-settings-and-persistence.md)), read when **Calculate** is pressed.

## Calculation

1. Validate as above.
2. **Long calculation check** (setting `showLongCalculationConfirmation`):
   - `count = trunc((JD(End) − JD(Start)) / interval)`, multiplied by the number of comets in multiple mode;
   - if `count > 100000`, ask `Calculation using selected settings could take a while, depending on your PC performances. Are you sure you want to continue?`. No cancels.
3. The comets are the selected comet (single mode, or multiple mode with a one-comet list), or every comet of the list in order (multiple mode). For each:
   ```
   rows = []
   steps = floor(spanSeconds / intervalSeconds)     # exact integer arithmetic, see below
   for k = 0 … steps:
       jd = JD(Start) + k·interval
       ep = GetEphemeris(comet, jd, observer)
       keep = (!minMagChecked or ep.Magnitude ≤ minMag)
          and (!maxSunChecked or ep.SunDist ≤ maxSun)
          and (!maxEarthChecked or ep.EarthDist ≤ maxEarth)
       if keep: rows.add(ep)
   if rows not empty: result[comet] = rows       # comets without rows are left out
   ```
   Start and End are whole-second instants and the interval is a whole number of minutes, so `spanSeconds = End − Start` and `intervalSeconds` are integers. The row count is therefore exact, and a row falls on End whenever End is a whole number of steps after Start. The desktop instead adds the interval repeatedly in decimal arithmetic. For intervals that decimals cannot hold exactly, such as 1 h = 0.041666…7 day, that sometimes drops a row lying exactly on End ([10 § Deviations](10-quirks.md#deviations)). No vector depends on the difference.
4. Run the calculation in a Worker. In multiple mode, report progress as comets completed out of the total. **Cancel** stops it and discards everything.
5. Write the [text output](#text-output). A cancel at this stage also discards everything.
6. Open the result in a new tab, or replace the edited tab. The tab title is `Ephemeris - <full>` (single) or `Ephemeris - <n> comets` (multiple, where n is the size of the list, not the number of comets with rows).

## Text output

`GenerateEphemerisAsync`. It uses monospaced, invariant formatting ([03 § Number formatting](03-element-formats.md#number-formatting)). Each comet in `result`, in order, writes:

```
Comet:               <TAB><full>
Perihelion date:     <TAB><JDToDateTime(Tn) as "dd MMM yyyy HH:mm:ss">
Perihelion distance: <TAB><q as 0.000000> AU
Period:              <TAB><P as 0.000000> years      (or "-" when P ≥ 10000)
<empty line>
<header line>
<one line per row>
<empty line>
<empty line>
<empty line>
```

The labels are padded with spaces exactly as shown, then a TAB character, then the value. `MMM` is the English month abbreviation (`Jan` … `Dec`).

**Header line**: concatenate the parts for the enabled columns, in this order:

| Column | Header part (exact, spaces significant) |
|---|---|
| (time) | `"     Local Time  "` or `" Universal Time  "` |
| RA | `"   R.A.   "` |
| Dec | `"   Dec   "` |
| Alt | `"   Alt  "` |
| Az | `"   Az   "` |
| Ecl. lon | `" Ecl.Lon "` |
| Ecl. lat | `" Ecl.Lat "` |
| Elongation | `"   Elong. "` |
| Sun distance | `"    r    "` |
| Earth distance | `"    d    "` |
| Magnitude | `" Mag."` |

**Row line**: concatenate, for the enabled columns in the same order:

| Column | Row part |
|---|---|
| (time) | instant `JDToDateTime(ep.JD)` (converted to local time when Local Time is chosen) as `dd.MM.yyyy HH:mm` |
| RA | `"  " + HMS(RA/15)` |
| Dec | `"  " + Angle(Dec, false, true)` |
| Alt | `{0,8:0.0°}` of Alt |
| Az | `{0,8:0.0°}` of Az |
| Ecl. lon | `"  " + Angle(EcLon, true, true)` |
| Ecl. lat | `"  " + Angle(EcLat, false, true)` |
| Elongation | `{0,8:0.0°} {1}` of Elongation and `W` or `E` |
| Sun distance | `{0,9:0.0000}` of SunDist |
| Earth distance | `{0,9:0.0000}` of EarthDist |
| Magnitude | `{0,6:0.0}` of Magnitude |

`HMS` and `Angle` are defined in [04](04-astronomy.md#angle-and-time-text). Seconds are truncated by `JDToDateTime` before formatting `HH:mm`, so no rounding happens.

Example (vector `01-single-ut-all-columns`):

```
Comet:               	C/2023 A3 (Tsuchinshan-ATLAS)
Perihelion date:     	27 Sep 2024 20:00:31
Perihelion distance: 	0.391312 AU
Period:              	-

 Universal Time     R.A.      Dec      Alt     Az    Ecl.Lon  Ecl.Lat    Elong.     r        d     Mag.
01.10.2024 00:00  11h04m51  -05°55'  -65.3°  104.3°  169°39'  -10°53'   21.5° W   0.4008   0.7706   2.8
```

## Result tab

- Read-only monospaced text that does not wrap, with horizontal scrolling. The text keeps its TAB characters.
- **Save** (`Ctrl/⌘+S`) downloads `Comets_Ephemeris_<yyyy-MM-dd_HHmmss>.txt` (local time) with CRLF line endings.
- **Copy** copies the whole text.
- **Edit settings** reopens the form with this tab's settings ([01 § Multiple results](01-product.md#multiple-results)).
