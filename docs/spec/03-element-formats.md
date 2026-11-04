# 03 — Orbital element formats

The app reads 21 formats and writes 19. Most are the formats the MPC publishes as `Soft00Cmt.txt` … `Soft16Cmt.txt` (<https://www.minorplanetcenter.net/iau/Ephemerides/Comets/SoftwareComets.html>). Behaviour MUST match the desktop byte for byte. The one exception is the Dance of the Planets header, see [10](10-quirks.md). Vectors: [`import/`](vectors/import/), [`import-expected/`](vectors/import-expected/), [`export/`](vectors/export/).

| # | Key | Name shown | Software | Import | Export | Extension |
|---|---|---|---|---|---|---|
| 0 | `MPC` | MPC (Soft00Cmt) | MPC | ✓ | ✓ | .txt |
| 1 | `SkyMap` | SkyMap (Soft01Cmt) | SkyMap | ✓ | ✓ | .txt |
| 2 | `Guide` | Guide (Soft02Cmt) | Guide | ✓ | ✓ | .txt |
| 3 | `xephem` | xephem (Soft03Cmt) | xephem | ✓ | ✓ | .txt |
| 4 | `HomePlanet` | Home Planet (Soft04Cmt) | Home Planet | ✓ | ✓ | .csv |
| 5 | `MyStars` | MyStars! (Soft05Cmt) | MyStars! | ✓ | ✓ | .txt |
| 6 | `TheSky` | TheSky (Soft06Cmt) | TheSky | ✓ | ✓ | .txt |
| 7 | `StarryNight` | Starry Night (Soft07Cmt) | Starry Night | ✓ | ✓ | .txt |
| 8 | `DeepSpace` | Deep Space (Soft08Cmt) | Deep Space | ✓ | ✓ | .txt |
| 9 | `PCTCS` | PC-TCS (Soft09Cmt) | PC-TCS | ✓ | ✓ | .txt |
| 10 | `EarthCenteredUniverse` | Earth Centered Universe (Soft10Cmt) | Earth Centered Universe | ✓ | ✓ | .txt |
| 11 | `DanceOfThePlanets` | Dance of the Planets (Soft11Cmt) | Dance of the Planets | ✓ | ✓ | .txt |
| 12 | `MegaStarV4` | MegaStar V4.x (Soft12Cmt) | MegaStar V4.x | ✓ | ✓ | .txt |
| 13 | `SkyChartIII` | SkyChart III (Soft13Cmt) | SkyChart III | ✓ | ✓ | .txt |
| 14 | `VoyagerII` | Voyager II (Soft14Cmt) | Voyager II | ✓ | ✓ | .txt |
| 15 | `SkyTools` | SkyTools (Soft15Cmt) | SkyTools | ✓ | ✓ | .txt |
| 16 | `Autostar` | Autostar (Soft16Cmt) | Autostar | as TheSky | ✓ (same as TheSky) | .txt |
| 17 | `SpaceEngine` | SpaceEngine | SpaceEngine | — | ✓ | .sc |
| 18 | `Celestia` | Celestia | Celestia | — | ✓ | .ssc |
| 19 | `CometForWindows` | Comet for Windows | Comet for Windows | ✓ | — | .dat |
| 20 | `NASA` | NASA (ELEMENTS.COMET) | NASA | ✓ | — | .comet |

The two detection results that are not formats, `NoFileSelected` and `EmptyFile`, plus `Unknown`, are covered in [Detection](#detection).

## Text conventions

- **Decoding**: input files are decoded as UTF-8, ignoring a leading BOM. Invalid bytes become U+FFFD; JavaScript's `TextDecoder('utf-8')` does this by default.
- **Lines**: split on `\r\n`, `\n` or `\r`. A final line terminator does not produce an empty last line. This matches .NET `File.ReadAllLines`.
- **Output**: exported text uses **CRLF** line terminators, including after the last line. The vectors are stored with LF, so compare after normalizing line endings. Files are written as UTF-8 without BOM.

### Field access

The import rules below use these operations. Any operation that fails makes the **record** invalid, and an invalid record is skipped silently. Where a rule reads a fixed-width column (`sub`) as a number, the desktop trims it first, so `int(sub(14,4))` means `int(trim(sub(14,4)))`; fields from `split` are read as they are.

| Operation | Meaning |
|---|---|
| `sub(a, n)` | Characters `[a, a+n)` of the line. **Fails if the line is shorter than `a+n`.** JavaScript's `substr` does not fail on its own, so the check must be explicit. |
| `from(a)` | Characters from `a` to the end. Fails if the line is shorter than `a`. |
| `split(c)` | Split on the single character `c`, keeping empty fields. Reading a field index that does not exist fails. |
| `trim` | Remove leading and trailing white space, as .NET `String.Trim` does: `Char.IsWhiteSpace`, which includes U+0085 and excludes U+FEFF, unlike JavaScript's `trim`. |
| `pad4(s)` | `trim(s)` right-padded with `0` to 4 characters (`"912"` → `"9120"`). |
| `int(s)` | Integer: optional white space, optional sign, digits, optional white space, within 32 bits. Anything else fails (no decimal point). The white space numbers allow is space and U+0009–U+000D, here and in `dbl` and `dec`. |
| `dbl(s)` | Number: optional white space, optional sign, digits with an optional `.` fraction (`.5` and `5.` are valid), optional exponent `e±n`; commas inside the integer digits are ignored (thousands separators). Anything else fails. The desktop also accepts `NaN` and `Infinity`; the web app MAY reject them. |
| `dec(s)` | Like `dbl` but with no exponent, and the sign may trail instead of lead (`5-` is −5). On the desktop this is an exact decimal; the web app uses float64. |
| `roundEven(x)` | Round to the nearest integer, ties to even (.NET `Math.Round`). |
| `round6(x)` | `roundEven(x × 10⁶) / 10⁶` |
| `date(jd)` | The date parts of `JDToDateTime(jd)` ([04](04-astronomy.md#jd-to-instant-jdtodatetime)). Fails where the desktop's `DateTime` throws: a year before 1 or after 9999, a Julian 29 February the Gregorian calendar lacks, hour 24. So an importer still skips such a record, although the rest of the web app keeps those dates ([10](10-quirks.md#deviations), D5); the round trips of `C/-146 P1` through MyStars! and Starry Night rely on it. |

Every importer finishes a valid record the same way (**finalize**):

```
T = JD0(Ty, Tm, Td, Th)        (unless the format says otherwise)
P, a, n, Q from q and e        (02-data-model § Derived elements)
sortkey = GetSortkey(id, fragment)   (fails, so the record is skipped, if id is malformed)
```

`full` is either **kept** as read, or **rebuilt** as `GetFullFromIdName(id, name, fragment)`. Each format below says which. Fields a format does not carry stay at their defaults: numbers `0`, strings empty.

## Detection

`GetImportType(text)`. The order of the checks matters: the first match wins.

```
if no file was chosen:        NoFileSelected
if the file has 0 bytes:      EmptyFile   (also when it has no lines, only a BOM: spec 10, D4)
lines  = split lines
line0  = lines[0];  lineX = lines[len−2] (none if fewer than 2 lines);  lineN = lines[len−1]

if line0 starts with "Name,Perihelion"            → HomePlanet
if line0 starts with "RDPC"                       → MyStars
if line0 starts with "NOTE: If"                   → StarryNight
if line0 starts with "Type C:"                    → DeepSpace
if line0 starts with "Comet      peri(au)"        → DanceOfThePlanets
if line0 starts with "NOTE TO VOYAGER II USERS"   → VoyagerII
if lines[1] = "[File]"                            → CometForWindows   (only when there are ≥ 2 lines)

then try these record parsers, in this order, on lineN, or on (lineX, lineN) for two-line formats.
The first that does not fail gives the type:
    MPC, SkyMap, xephem, HomePlanet, MyStars, TheSky, StarryNight, PCTCS,
    EarthCenteredUniverse (lineX, lineN), DanceOfThePlanets, MegaStarV4, SkyChartIII,
    VoyagerII, SkyTools, NASA, DeepSpace (lineX, lineN), Guide
otherwise → Unknown
```

Detection runs a record parser on just one record, without importing. Autostar files are detected as `TheSky`, because the formats are identical. Vectors: `detectedType` in every [`import-expected/*.json`](vectors/import-expected/), and [`import-detection.json`](vectors/import-detection.json).

## Import rules

`lines[k]` is the k-th line, counting from 0. "Every line" means every line, with no header to skip.

### MPC (Soft00Cmt)

Every line. `full` is **kept**: `full = trim(sub(102, 55))`, and id, name and fragment come from it via pattern 00.

| Field | Source |
|---|---|
| Ty, Tm, Td | `int(sub(14,4))`, `int(sub(19,2))`, `int(sub(22,2))` |
| Th | `int(pad4(sub(25,4)))` |
| q, e | `dbl(sub(30,9))`, `dbl(sub(41,8))` |
| w, N, i | `dbl(sub(51,8))`, `dbl(sub(61,8))`, `dbl(sub(71,8))` |
| g, k | `dbl(sub(91,4))`, `dbl(sub(96,4))` |

### SkyMap (Soft01Cmt)

Every line. `full` is **rebuilt**: id, name and fragment come from `trim(sub(0,44))` via pattern 00.

Ty `sub(47,4)`, Tm `sub(52,2)`, Td `sub(55,2)`, Th `pad4(sub(58,4))`, q `sub(63,10)`, e `sub(78,10)`, w `sub(88,9)`, N `sub(97,9)`, i `sub(106,9)`, g `sub(115,5)`, k `sub(121,5)`.

### Guide (Soft02Cmt)

Every line. `full` is **rebuilt**: id, name and fragment come from `trim(sub(0,42))` via **pattern 02**.

Td `sub(43,2)`, Th `pad4(sub(46,4))`, Tm `sub(52,2)`, Ty `sub(55,5)`, q `sub(73,10)`, e `sub(85,10)`, i `sub(95,10)`, w `sub(107,10)`, N `sub(119,10)`, g `sub(140,5)`, k `sub(145,5)`.

### xephem (Soft03Cmt)

Lines 1, 3, 5, …; the even lines are `# From …` comments. `p = split(',')`. `full` is **kept**: `full = trim(p[0])`, then pattern 00. Dates are `month/day.fraction/year`. Splitting `day.fraction` on `.` MUST give two parts, so `17` alone fails while `17.0` is valid. Th = `int(pad4(fraction))`.

- **`p[1] = "e"` (ellipse)**:
  - i `p[2]`, N `p[3]`, w `p[4]`, a `dbl(p[5])`, n `dec(p[6])`, e `p[7]`, M `dec(p[8])`;
  - the epoch date comes from `p[9]`; g `dbl(from(2) of p[11])` (drop the leading `g `); k `p[12]`;
  - `q = round6(a × (1 − e))`;
  - if `M = 0`: the perihelion date is the epoch date, and `T = JD0(…)`.
  - otherwise (`n = 0` fails, as the desktop's decimal division does):
    ```
    E = JD0(epoch y, m, d, h)
    t = E − M / n
    if M ≥ 180: t += P(q, e) × 365.25
    dt = date(t)
    Ty, Tm, Td = dt.year, dt.month, dt.day
    Th = roundEven(((dt.hour + dt.minute/60 + dt.second/3600) / 24) × 10000)
    T  = JD0(Ty, Tm, Td, Th)
    ```
- **`p[1] = "p"` (parabola)**: date `p[2]`, i `p[3]`, w `p[4]`, q `p[5]`, N `p[6]`, g `p[8]`, k `p[9]`, e = 1.
- **`p[1] = "h"` (hyperbola)**: date `p[2]`, i `p[3]`, N `p[4]`, w `p[5]`, e `p[6]`, q `p[7]`, g `p[9]`, k `p[10]`.
- Any other value fails.

### Home Planet (Soft04Cmt)

Lines from 1; line 0 is the header. `p = split(',')`. `full` is **kept**: `full = trim(p[0])`, then pattern 00. Date `p[1]` is `year-month-day.fraction`: split it on `-` to get Ty, Tm and `day.fraction`, then on `.` to get Td and Th. q `p[2]`, e `p[3]`, w `p[4]`, N `p[5]`, i `p[6]`. The format has no g or k. Negative years cannot be read, because of the split on `-`.

### MyStars! (Soft05Cmt)

Lines from 1. `p = split('\t')`. `full` is **kept**: `full = trim(p[0])`, including the trailing `;` the format puts there. Pattern 00 still extracts the name correctly.

- Time: split `p[1]` on `.`. `T0 = dec(part0) + 2400000.5` (an MJD) and `h = int(pad4(part1))`. Ty, Tm and Td are those of `date(T0)`; `Th = h`.
- `lonPeri = dbl(p[2])`, e `p[3]`, q `p[4]`, i `p[5]`, N `p[6]`, g `p[7]`, k `p[8]`.
- `w = (lonPeri − N + 360) mod 360`. This is a floating remainder that takes the sign of the dividend, like `%` in JavaScript.

### TheSky (Soft06Cmt) and Autostar (Soft16Cmt)

Every line. `p = split('|')`. `full` is **kept**: `full = trim(p[0])`, then pattern 00.

- `d = trim(p[2])` right-padded with `0` to 13 characters. Ty `int(d[0..4))`, Tm `int(d[4..6))`, Td `int(d[6..8))`, Th `int(pad4(trim(d[9..13))))`.
- q `p[3]`, e `p[4]`, w `p[5]`, N `p[6]`, i `p[7]`, g `p[8]`, `k = dbl(p[9]) / 2.5`.

### Starry Night (Soft07Cmt)

Lines from 15. `full` is **rebuilt**: `trim(sub(136,14)) + " " + trim(sub(5,29))`, that is id then name, goes through pattern 00.

- g `sub(34,6)`, e `sub(48,10)`, q `sub(59,11)`, N `sub(72,10)`, w `sub(82,10)`, i `sub(92,10)`, `T0 = dec(trim(sub(102,14)))` (a JD), `k = dbl(sub(129,6)) / 2.5`.
- Date: `dt = date(T0)`, Ty, Tm, Td from dt, `Th = roundEven(((h + min/60 + s/3600) / 24) × 10000)`, then `T = JD0(…)`.

### Deep Space (Soft08Cmt)

Records are pairs of lines starting at line 2: `(lines[k], lines[k+1])` for k = 2, 4, …. A missing second line fails. `full` is **rebuilt**: id, name and fragment come from the first line, **untrimmed**, via **pattern 02**. `p = split(' ')` of the second line: Ty `p[2]`, Tm `p[3]`, `day.fraction` `p[4]`, q `p[5]`, e `p[6]`, w `p[7]`, N `p[8]`, i `p[9]`, g `p[10]`, `k = dbl(p[11]) / 2.5`.

### PC-TCS (Soft09Cmt)

Every line. `p = split(' ')` of the line with trailing white space removed.

- `id0 = p[0]`. If it contains `/`, insert a space before the first letter at index ≥ 2: `C/1942EA` → `C/1942 EA`, `C/-146P1` → `C/-146 P1`. Having no such letter fails.
- q `p[1]`, e `p[2]`, i `p[3]`, w `p[4]`, N `p[5]`, Ty `p[6]`, Tm `p[7]`, `day.fraction` `p[8]`, g `p[9]`, `k = dbl(p[10]) / 2.5`.
- `name0 = trim(p[11] + " " + p[12] + " " + … )`.
- `full` is **rebuilt** from `id0 + " " + name0` via pattern 00.

### Earth Centered Universe (Soft10Cmt)

Records are pairs of lines starting at line 0. `full` is **kept**: `full = trim(first line)`, then pattern 00. `p = split(' ')` of the second line: Ty `p[3]`, Tm `p[4]`, `day.fraction` `p[5]`, q `p[6]`, e `p[7]`, w `p[8]`, N `p[9]`, i `p[10]`, g `p[11]`, `k = dbl(p[12]) / 2.5`.

### Dance of the Planets (Soft11Cmt)

Lines from 5.

- `id0 = trim(sub(0,11))`, with the PC-TCS space insertion when it contains `/`.
- q `sub(11,9)`, e `sub(20,9)`, i `sub(29,9)`, N `sub(38,9)`, w `sub(47,9)`, Ty `sub(56,4)`, Tm `sub(61,2)`, Td `sub(63,2)`, Th `pad4(sub(65,4))`.
- The format writes q ≥ 10 AU as `********`, which fails to parse, so such comets are lost.
- `name0` = the empty string if the line is exactly 69 characters long, otherwise `trim(from(70))`.
- `full` is **rebuilt** from `id0 + " " + name0` via pattern 00. The format has no g or k.

### MegaStar V4.x (Soft12Cmt)

Every line. `full` is **rebuilt** from `trim(sub(30,12)) + " " + trim(sub(0,30))` (id, then name) via pattern 00.

Ty `sub(42,4)`, Tm `sub(47,2)`, Td `sub(51,2)`, Th `pad4(sub(54,4))`, q `sub(59,12)`, e `sub(73,8)`, w `sub(85,8)`, N `sub(97,8)`, i `sub(109,8)`, g `sub(119,6)`, `k = dbl(sub(126,6)) / 2.5`.

### SkyChart III (Soft13Cmt)

Every line. `p = split('\t')`.

- `q = |dbl(p[2])|`; the format stores q negated. e `p[3]`, i `p[4]`, w `p[5]`, N `p[6]`.
- Date `p[8]` is `year/month/day.fraction`.
- Split `p[9]` on a space to get g and k.
- `full` is **kept**: `full = trim(split(p[12], ';')[0])`, then pattern 00.

### Voyager II (Soft14Cmt)

Lines from 23. **Duplicates are allowed**: a record is added even if its `full` already exists.

- `t = trim(sub(0,27))`. If `t[0]` is a type letter and `t[1] = '/'`, or `t[0]` is a digit: `full = id = t`, `name = ""`. Otherwise: `full = name = t`, `id = ""`. `fragment` is empty (the desktop leaves it null). An empty `t` fails, and so does a `t` that is one type letter alone, since the desktop reads `t[1]`; any other one-character `t` is an id (a digit) or a name.
- q `sub(27,9)`, e `sub(39,8)`, i `sub(49,8)`, N `sub(60,8)`, w `sub(71,8)`, Ty `sub(87,4)`.
- `Tm` = 1 + the index of `sub(91,3)` in `Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec`, matched case-sensitively. A missing month fails.
- `day.fraction` = `trim(sub(94,7))`.
- `sortkey = ""`; the finalize step does not compute it. The format has no g or k.

### SkyTools (Soft15Cmt)

Every line. `full` is **rebuilt** from `trim(sub(2,41))` via pattern 00.

Ty `sub(54,4)`, Tm `sub(59,2)`, Td `sub(62,2)`, Th `pad4(sub(65,4))`, q `sub(70,9)`, e `sub(82,8)`, w `sub(91,8)`, N `sub(99,8)`, i `sub(107,7)`, g `sub(115,5)`, k `sub(122,4)`.

The format writes three decimals of the day fraction, so Th comes from `"976 "` → `"9760"`.

### Comet for Windows (.dat, import only)

Records start at line 6 and are 13 lines long. The record's lines are `L0 = lines[k]`, `L3 = lines[k+3]`, `L4 … L8 = lines[k+4 … k+8]` and `L11 = lines[k+11]`. A missing line fails. `value(L)` is the text after the first `=`.

- `full` is **kept**: `full = trim(value(L0))`, then pattern 00.
- `value(L3)` split on a space gives Ty, Tm and `day.fraction`.
- q `value(L4)`, e `value(L5)`, w `value(L6)`, N `value(L7)`, i `value(L8)`.
- `value(L11)` split on a space gives g and `k = dbl(·) / 2.5`.

No real file was available, so the vector file [`import/CometForWindows.dat`](vectors/import/CometForWindows.dat) is synthetic. It follows the layout this parser reads.

### NASA ELEMENTS.COMET (import only)

Lines from 2. `full` is **kept**: `full = trim(sub(0,43))`, then pattern 00.

- `epoch = dbl(sub(44,7))` (an MJD).
- q `sub(52,11)`, e `sub(64,10)`, i `sub(75,9)`, w `sub(85,9)`, N `sub(95,9)`.
- i, N and w are passed through `PrecessElementsToJ2000(epoch, i, N, w)` ([04](04-astronomy.md#element-precession-nasa-import)).
- Ty `sub(105,4)`, Tm `sub(109,2)`, Td `sub(111,2)`.
- `Th = roundEven(dbl(trim(sub(114,5)) right-padded with 0 to 5) / 10)`.
- The format has no g or k.

## Merge (import into the database)

`ImportMain(database, type, file)`, which the app runs whenever a file or download is read:

```
old = copy of database;  new = parse(file)        # parse skips invalid records
if new is not empty:
    if old is not empty:
        mark every comet in old and new as NoChanges
        for n in new (file order):
            o = first comet in old with o.full = n.full
            if o exists: remove o from old; if not o.equals(n): n.result = Update
            else: n.result = New
            append n to old (duplicate full names skipped, except for VoyagerII)
    else:
        mark every comet in new as New;  old = new
else:
    mark every comet in old as NoChanges    # the desktop keeps each comet's result from before
result = old, stably sorted by sortkey   (strings compare as in 02 § Sorting)
```

Comets that are in the database but not in the file **stay**, marked `NoChanges`. The import summary shows the counts `New`, `Update`, `NoChanges` and their total ([05](05-database-and-update.md#update)). Vector: [`import-merge.json`](vectors/import-merge.json).

## Export rules

`ExportMain(type, collection)` writes the header, if any, then one block per comet in collection order. The format items `{index[,alignment][:format]}` follow .NET composite formatting, defined in [Number formatting](#number-formatting). Below, `today` is the current **UTC** date at export time, and `full`, `id`, `name` and so on are the comet's fields. Every listed line ends with CRLF. Expected outputs for the sample collection, at the vector clock: [`export/`](vectors/export/).

### MPC

```
"              {0,4} {1:00} {2,2}.{3:0000} {4,9:0.000000}  {5:0.000000}  {6,8:0.0000}  {7,8:0.0000}  {8,8:0.0000}  {9}{10:00}{11:00}  {12,4:0.0} {13,4:0.0}  {14,-56} MPC 00000"
args: Ty, Tm, Td, Th, q, e, w, N, i, today.year, today.month, today.day, g, k, full
```

The database is stored in this format ([05](05-database-and-update.md#storage)).

### SkyMap

```
s = id = ""                ? name
  : id[0] is a type letter ? full without "(" and ")"
  :                          full without "(" and ")", with "/" replaced by " "
"{0,-47}{1,4} {2:00} {3:00}.{4:0000} {5,9:0.000000}       {6:0.000000} {7,8:0.0000} {8,8:0.0000} {9,8:0.0000}  {10,4:0.0}  {11,4:0.0}"
args: s, Ty, Tm, Td, Th, q, e, w, N, i, g, k
```

### Guide

```
s = name = ""          ? id
  : id = ""            ? name
  : id[0] is a digit   ? "P/" + name + " (" + id + ")"
  :                      name + " (" + id + ")"
"{0,-43}{1,2}.{2:0000}  {3,2}  {4,4}  0.0        {5,9:0.000000}    {6:0.000000}  {7,8:0.0000}    {8,8:0.0000}    {9,8:0.0000}    2000.0   {10,4:0.0} {11,4:0.0}    MPC 00000"
args: s, Td, Th, Tm, Ty, q, e, i, w, N, g, k
```

### xephem

Each comet writes two lines: first `# From MPC 00000`, then the record. `s = full`, with `/` appended when `name = ""` (e.g. `282P/`).

```
e < 1:  "{0},e,{1:0.0000},{2:0.0000},{3:0.0000##},{4:0.0000##},{5:0.0000000},{6:0.00000000},0.0000,{7:00}/{8:00}.{9:0000}/{10},2000,g {11,4:0.0},{12:0.0}"
        args: s, i, N, w, a, n, e, Tm, Td, Th, Ty, g, k
e = 1:  "{0},p,{1:00}/{2:00.000#}/{3},{4:0.000#},{5:0.000#},{6:0.00000#},{7:0.000#},2000,{8:0.0},{9:0.0}"
        args: s, Tm, (Td + Th/10000), Ty, i, w, q, N, g, k
e > 1:  "{0},h,{1:00}/{2:00}.{3:0000}/{4},{5:0.0000},{6:0.0000},{7:0.0000},{8:0.000000},{9:0.000000},2000,{10:0.0},{11:0.0}"
        args: s, Tm, Td, Th, Ty, i, N, w, e, q, g, k
```

### Home Planet

Header line: `Name,Perihelion time,Perihelion AU,Eccentricity,Long. perihelion,Long. node,Inclination,Semimajor axis,Period`. `s` is as for xephem.

```
P < 30000:  "{0},{1}-{2}-{3}.{4:0000},{5:0.000000},{6:0.000000},{7:0.0000},{8:0.0000},{9:0.0000},{10:0.00000},{11:0.00} years, MPC 00000"
            args: s, Ty, Tm, Td, Th, q, e, w, N, i, a, P
otherwise:  "{0},{1}-{2}-{3}.{4:0000},{5:0.000000},{6:0.000000},{7:0.0000},{8:0.0000},{9:0.0000},,, MPC 00000"
            args: s, Ty, Tm, Td, Th, q, e, w, N, i
```

### MyStars!

Header line: `RDPC` + TAB + the number of comets. `mjdToday = JD(today 00:00 UTC) − 2400000.5`.

```
"{0};\t{1:0.0000}\t{2:0.0000}\t{3:0.000000}\t{4:0.000000}\t{5:0.0000}\t{6:0.0000}\t{7:0.0}\t{8:0.0}\tMPC00000\t{9:0.0}"
args: full, T − 2400000.5, (w + N) mod 360, e, q, i, N, g, k, mjdToday
```

### TheSky and Autostar

```
"{0,-39}|2000|{1,4}{2:00}{3:00}.{4:0000} |{5,9:0.000000} |{6:0.000000} |{7,8:0.0000} |{8,8:0.0000} |{9,8:0.0000} |{10,4:0.0} |{11,4:0.0} | MPC 00000"
args: full, Ty, Tm, Td, Th, q, e, w, N, i, g, k × 2.5
```

### Starry Night

Header, 15 lines verbatim. Lines 2, 7, 11, 13 and 15 are empty:

```
NOTE: If viewing this file and it appears confused, make the window very wide!

   The numbers are all in the proper format for easy use in Starry Night's
orbit editor. Just click on the word Sun in the planet floater and then
click on add. In the first window that appears select the comet as the type
of object you want to add. Please see the manual for more information.

   The orbital information should have the reference plane set at Ecliptic
 2000 and the Style should be pericentric. Don't forget to use copy and
 paste to ease the input of the orbital data into Starry Night.

This file kindly prepared by the IAU Minor Planet Center & Central Bureau for Astronomical Telegrams.

Num  Name                          Mag.   Diam      e            q        Node         w         i         Tp           Epoch       k   Desig         Reference

```

`jdToday = JD(today 00:00 UTC)`.

```
"     {0,-29} {1,4:0.0}    0.0   {2:0.000000}   {3,9:0.000000}    {4,8:0.0000}  {5,8:0.0000}  {6,8:0.0000}  {7,12:0.0000}    {8,9:0.0}  {9,4:0.0}  {10,-13} MPC 00000"
args: name, g, e, q, N, w, i, T, jdToday, k × 2.5, id
```

### Deep Space

Two header lines: `Type C: Equinox Year Month Day q e Peri Node i Mag k` and `Type A: Equinox Year Month Day a M e Peri Node i H G`. Each comet writes two lines: first `name + " (" + id + ")"`, then:

```
"C J2000 {0} {1:00} {2:00}.{3:0000} {4:0.000000} {5:0.000000} {6:0.0000} {7:0.0000} {8:0.0000} {9:0.0} {10:0.0}"
args: Ty, Tm, Td, Th, q, e, w, N, i, g, k × 2.5
```

### PC-TCS

`s` = `id` with every space removed. The line is right-padded with spaces to 126 characters (`{0,-126}`).

```
"{0} {1:0.000000} {2:0.000000} {3:0.0000} {4:0.0000} {5:0.0000} {6,4} {7:00} {8:00}.{9:0000} {10:0.0} {11:0.0} {12}"
args: s, q, e, i, w, N, Ty, Tm, Td, Th, g, k × 2.5, name
```

### Earth Centered Universe

Each comet writes two lines: first `full`, then:

```
"E C 2000 {0} {1:00} {2:00}.{3:0000} {4:0.000000} {5:0.000000} {6:0.0000} {7:0.0000} {8:0.0000} {9:0.0} {10:0.0}"
args: Ty, Tm, Td, Th, q, e, w, N, i, g, k × 2.5
```

### Dance of the Planets

Header, 5 lines verbatim. The first line is the MPC file's own header, see [10](10-quirks.md#deviations):

```
Comet      peri(au)   e         ix       jx       wx     peridate     name
(In order to be recognised by Dance of the Planets, this file)
(must have a .cmt extension.)
(File prepared by IAU Minor Planet Center/Central Bureau)
(for Astronomical Telegrams.)
```

`s` = `id` with spaces removed; `qs = q < 10 ? format(q, "0.000000") : "********"`.

```
"{0,-11}{1} {2:0.000000} {3,8:0.0000} {4,8:0.0000} {5,8:0.0000} {6,4}.{7:00}{8:00}{9:0000} {10}"
args: s, qs, e, i, N, w, Ty, Tm, Td, Th, name
```

### MegaStar V4.x

```
"{0,-30}{1,-12}{2,4} {3:00}  {4:00}.{5:0000}   {6,9:0.000000}   {7:0.000000}    {8,8:0.0000}    {9,8:0.0000}    {10,8:0.0000}   {11,4:0.0}   {12,4:0.0}    2000 MPC 00000"
args: name, id, Ty, Tm, Td, Th, q, e, w, N, i, g, k × 2.5
```

### SkyChart III

```
"P11\t2000.0\t-{0:0.000000}\t{1:0.000000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t0\t{5}/{6:00}/{7:00}.{8:0000}\t{9:0.0} {10:0.0}\t0\t0\t{11}; MPC 00000"
args: q, e, i, w, N, Ty, Tm, Td, Th, g, k, full
```

### Voyager II

Header, 23 lines verbatim. Lines 2, 5, 14, 19 and 22 are empty. The HTML anchor in line 18 is part of the text:

```
NOTE TO VOYAGER II USERS:

   The following table will link the symbols below with the names used in
the Voyager II "Define New Orbit..." dialog for comets.

     q        perihelion distance (astronomical units)
     e        eccentricity (no units)
     i        inclination of orbit to ecliptic (degrees)
     Node     longitude of ascending node (degrees)
     w        argument of perihelion (degrees)
     L        mean anomaly (this is 0 at perihelion) (degrees)
     Date     epoch of orbit
     Equinox  reference equinox (usually 2000.0)

Save this page as plain text from your browser and use the table to input
the orbital elements for the comets that you would like to plot and
follow.  If you have any question, consult your software manual or the
Carina web site: <a href="http://www.carinasoft.com">http://www.carinasoft.com</a>

Thanks to the IAU Minor Planet Center & Central Bureau for Astronomical
Telegrams for providing this information.

Name                            q          e         i        Node         w       L      T(Date)    Equinox
```

`date = format("{0,4}{1}{2}.{3:0000}", Ty, MonthAbbr[Tm], Td, Th)`, where MonthAbbr is `Jan` … `Dec`; for example `2019Dec9.0572`. A month outside 1–12 (an MPC file can carry one) makes the desktop throw; the web app writes the month's number instead, here and in Celestia.

```
"{0,-26} {1,9:0.000000}   {2:0.000000}  {3,8:0.0000}   {4,8:0.0000}   {5,8:0.0000}   0.0  {6,-14} 2000.0"
args: (name ≠ "" ? name : id), q, e, i, N, w, date
```

### SkyTools

`s` = `full` with `(` and `)` removed. `{7:000}` is the integer quotient `Th div 10`, which drops the last digit and does not round.

```
"C {0,-40} {1} {2:00} {3:00} {4,4} {5:00} {6:00}.{7:000}  {8,9:0.000000}   {9:0.000000} {10,7:0.000} {11,7:0.000} {12,7:0.000}  {13,4:0.0}  {14,4:0.0} 0.002000 MPC 00000"
args: s, today.year, today.month, today.day, Ty, Tm, Td, Th div 10, q, e, w, N, i, g, k
```

### SpaceEngine

```
s    = id = ""             ? name
     : id[0] is a digit    ? full with "/" replaced by " "
     :                       full with "/" removed
type = (id = "" or id[0] ≠ 'C') ? "P" : "C"
```

Each comet writes this block. `→` is a TAB, and `⏎` marks an empty line:

```
Comet→"<s>"
{
→ParentBody "Sol"
→CometType  "<type>"
→AbsMagn     <g as 0.##>
→SlopeParam  <k as 0.##>
⏎
→Orbit
→{
→→Epoch            <T as 0.0000>
→→PericenterDist   <q as 0.000000>
→→Eccentricity     <e as 0.000000>
→→Inclination      <i as 0.0000>
→→AscendingNode    <N as 0.0000>
→→ArgOfPericenter  <w as 0.0000>
→→MeanAnomaly      0
→}
}
⏎
```

### Celestia

Each comet writes this block. `→` is a TAB, `⏎` marks an empty line, and the `Period` line is written only when `e < 1`:

```
"<full with "/" replaced by " ">" "Sol"
{
→Class "comet"
→Mesh "asteroid.cms"
→Texture "asteroid.jpg"
→Radius 5
→Albedo 0.1
→#Magnitude g <g as 0.0>, k <k as 0.0>
→EllipticalOrbit
→{
→→Period         {P,20:0.000000}
→→PericenterDistance        {q,9:0.000000}
→→Eccentricity               {e,8:0.000000}
→→Inclination              {i,8:0.0000}
→→AscendingNode            {N,8:0.0000}
→→ArgOfPericenter          {w,8:0.0000}
→→MeanAnomaly                0.0
→→Epoch                {T,12:0.0000}     # {Ty,4} {MonthAbbr[Tm]} {Td:00}.{Th:0000}
→}
}
⏎
```

Line formats are given exactly as `ExportManager.ExportCelestia` writes them. The vector [`export/18-Celestia.txt`](vectors/export/18-Celestia.txt) is authoritative for spacing.

## Number formatting

The desktop formats every number with .NET custom format strings. They do **not** round the way JavaScript's `toFixed` does. A web implementation MUST implement this algorithm:

```
formatNumber(x, pattern):          # pattern like "0.0000", "00.000#", "0.##", "0.0°"
    literal suffix = any trailing characters that are not 0 # or .   (e.g. "°")
    intZeros  = number of "0" before the "."                           (at least 1 digit is always printed)
    minDec    = number of "0" after the "."
    maxDec    = minDec + number of "#" after the "."
    1. s = x rounded to 15 significant digits, as an exact decimal string. Round the exact
       binary value of x, ties to even. JavaScript's x.toPrecision(15) rounds ties up, so it
       differs exactly when the decimal expansion of x stops at the 16th significant digit with
       a 5 (100000000000000.5 → 100000000000000, 1000000000000.125 → 1000000000000.12); take
       the exact digits from the float's bits instead.
    2. Round s to maxDec decimals, half AWAY from zero, on the decimal digits of s.
    3. Remove trailing zeros beyond minDec decimals; drop the "." if no decimals remain.
    4. Left-pad the integer part with "0" to intZeros digits.
    5. If x is negative (including −0), prefix "-", even when the rounded value is zero: -0.04 → "-0.0".
    6. Append the literal suffix.
```

| x | `0.00` .NET (required) | `toFixed(2)` (wrong) |
|---|---|---|
| 2.675 | `2.68` | `2.67` |
| 1.005 | `1.01` | `1.00` |
| 0.045 | `0.05` | `0.04` |
| 59.2615 at `0.000` | `59.262` | `59.261` |

- `NaN` prints `NaN`, and infinities print `Infinity` and `-Infinity`, without the literal suffix.
- **Integers** (Ty, Td, Th, counts): `{n:00}` and `{n:0000}` zero-pad to that many digits, with any sign in front (`-146` with `{0:0000}` → `-0146`, with `{0,4}` → `-146`). A format item with no format string prints the plain integer. Integers must not be `−0`, which the steps above print as `-00`.
- A non-integer with no format string prints as .NET prints a double: the shortest digits that read back as the same value, in E notation when the decimal exponent is ≥ 17 or ≤ −5 (`1E+17`, `1.2345E-05`). No export or text output uses this.
- **Alignment** `{n,w}` pads the formatted text with spaces on the left to `w` characters. `{n,-w}` pads on the right. Text longer than `w` is never truncated.
- **Exact decimals**: the desktop formats `T` and JDs as exact decimals. Rounding a float64 to 15 significant digits first reproduces them.

The text output of [ephemerides](06-ephemeris.md#text-output) and the UI number displays use the same algorithm.

## Elements view

Desktop: File › Orbital Elements. The view converts the current selection into any export format.

- A format menu lists the 19 export formats, by the name in the table above.
- The text area shows `ExportMain(format, selection)`. It is read-only, monospaced, and does not wrap.
- The view title is `Orbital Elements (<Software>)`.
- **Save** (`Ctrl/⌘+S`) downloads the text with CRLF line endings, named `<Key>_<yyyy-MM-dd_HHmmss>.<extension>` using local time (e.g. `MPC_2026-10-18_140501.txt`). If the selection is empty, Save does nothing.
- **Copy** puts the text on the clipboard.
- The text is regenerated when the format or the selection changes.

## Import file picker

Accepts `.txt`, `.dat`, `.csv`, `.comet` and any other file, through a file chooser or drag and drop onto the Update dialog ([05](05-database-and-update.md#update)).
