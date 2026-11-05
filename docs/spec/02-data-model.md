# 02 — Data model

## Comet record

A comet is a record with the fields below. They are set at import time and never change afterwards, except through re-import or deletion. Field names match the desktop `Comets.Core.Comet` and the [test vectors](vectors/comets.json).

| Field | Type | Meaning |
|---|---|---|
| `full` | string | Full name as displayed and as the identity key, e.g. `C/1995 O1 (Hale-Bopp)`, `1P/Halley` |
| `id` | string | Designation without fragment, e.g. `1P`, `C/1995 O1`, `73P`, `A/2018 W3` (may be empty, see Voyager II) |
| `name` | string | Discoverer or common name, e.g. `Hale-Bopp` (may be empty) |
| `fragment` | string | Fragment suffix without the hyphen, e.g. `B`, `BT`, `A1` (usually empty) |
| `Ty`, `Tm`, `Td` | integer | Perihelion date: year, month, day |
| `Th` | integer 0–9999 | Fraction of the perihelion day in units of 10⁻⁴ day (`13.9762` → `Td = 13`, `Th = 9762`) |
| `T` | JD | Perihelion time: `JD0(Ty, Tm, Td, Th)` ([04](04-astronomy.md#calendar-to-jd-jd0)) |
| `q` | AU | Perihelion distance |
| `e` | — | Eccentricity (`< 1` ellipse, `= 1` parabola, `> 1` hyperbola) |
| `w` | ° | Argument of perihelion ω (J2000) |
| `N` | ° | Longitude of the ascending node Ω (J2000) |
| `i` | ° | Inclination (J2000) |
| `g` | mag | Absolute magnitude |
| `k` | — | Slope parameter, with magnitude `m = g + 5 log₁₀ Δ + 2.5 k log₁₀ r` |
| `P`, `a`, `Q`, `n` | | Derived elements, see below |
| `sortkey` | string | Database ordering key, see below |

Elements are referred to the J2000 ecliptic and equinox. Every import format is assumed to be J2000. The exception is NASA, which is converted ([03](03-element-formats.md#nasa-elementscomet-import-only)).

`T` MUST be stored as a float64 JD in the web app. The desktop uses a 128-bit decimal; the difference is far below every tolerance in [11](11-test-vectors.md#tolerances).

### Derived elements

Every importer computes these once, right after reading q, e and T (`CometManager`). The constant `0.999999` stands in for e = 1 so that parabolic comets still get finite, sortable values. `1 − 0.999999` MUST be computed in float64 arithmetic, as written.

| Element | e < 1 | e > 1 | e = 1 |
|---|---|---|---|
| Period `P` (years) | `(q/(1−e))^1.5` | `(q/(e−1))^1.5` | `(q/(1−0.999999))^1.5` |
| Semi-major axis `a` (AU) | `q/(1−e)` | `−(q/(1−e))` (i.e. `q/(e−1)`, positive) | `q/(1−0.999999)` |
| Aphelion distance `Q` (AU) | `a·(1+e)` | `a·(1+(2−e))` | `a·(1+0.999999)` |
| Mean motion `n` (°/day) | `0.9856076686 / P` | `0` | `0` |

Vectors: [`elements.json`](vectors/elements.json); every comet in [`import-expected/`](vectors/import-expected/).

### Is periodic

```
isPeriodic = (id ≠ "" and id[0] is a digit and (id ends with "P" or id contains "P-"))
             or id starts with "P/"
```

So `1P` and `P/2004 V5` are periodic. `3D`, `1I`, `C/1995 O1` and `A/2018 W3` are not.

### Nearest perihelion `Tn`

```
if isPeriodic:
    periodDays = P × 365.25          (P to 15 significant digits, as the desktop's decimal holds it;
                                      toPrecision(15) may differ from .NET's conversion in that
                                      digit, which moves Tn by under 1e-8 day)
    t = T
    while t − periodDays > JD(1700-01-01 00:00): t −= periodDays
    candidates = []
    while t < JD(2300-01-01 00:00): candidates.add(t); t += periodDays
    Tn = the candidate with the smallest |candidate − JD(now)|   (the earliest one on a tie)
    if candidates is empty: Tn = T           (desktop throws here; see 10-quirks)
else:
    Tn = T
```

The desktop sums the steps in exact decimals. In float64, compute each candidate afresh as `T + m × periodDays` for whole `m`, since summing hundreds of steps adds up their rounding errors. A period that is not positive and finite gives `Tn = T` (the desktop never ends its loop there, see 10-quirks D6).

`Tn` is computed on first use and then kept for the session, against the clock at that moment.

### Observer-dependent values

Each comet exposes these values, which depend on the time of use and on the observer location from settings ([09](09-settings-and-persistence.md)). Each is taken from an ephemeris ([04](04-astronomy.md#ephemeris)):

| Value | Definition | Caching |
|---|---|---|
| `PerihEarthDist`, `PerihMag` | Earth distance and magnitude of the ephemeris at `Tn` | Computed on first use, kept for the session |
| `CurrentSunDist`, `CurrentEarthDist`, `CurrentMag` | Sun distance, Earth distance and magnitude of the ephemeris now | Recomputed when the cached ephemeris is more than 5 minutes old |

## Designations and full names

The importers parse designations with one of two patterns. **Pattern 00** is designation first, as in `1P/Halley`, `C/1995 O1 (Hale-Bopp)` or `3D-A/Biela`. **Pattern 02** is name first, as in `Vaisala (C/1942 EA)` or `P/PANSTARRS 2 (470P-A)`. Both return `id`, `name` and `fragment`, or fail. On failure the importer skips the record.

The two character classes below are used throughout:

- **name character**: `A–Z a–z`, apostrophe `'`, backtick `` ` ``, hyphen `-`, space.
- **type letter**: one of `P C X D I A`.

### Pattern 00 (designation first)

Pattern 00 is a regular expression, safe in any backtracking engine. It is matched at the start of the string with no end anchor:

```
^(?<id>[0-9]+[PCXDIA]|[PCXDIA]/-*[0-9]+ *[a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)[ /]*\(*(?<name>['`a-zA-Z0-9\- ]+)?
```

A group that did not take part in the match is the empty string. This is the desktop `CometManager._regFull00` with `(?<name>…+)*` written as `(?<name>…+)?`; the two are equivalent.

Examples, from the desktop source comments:

| Input | id | fragment | name |
|---|---|---|---|
| `3D-B/Biela` | `3D` | `B` | `Biela` |
| `C/-146 P1` | `C/-146 P1` | | |
| `C/1860 D1-A (Liais)` | `C/1860 D1` | `A` | `Liais` |
| `D/1993 F2-P1 Shoemaker-Levy 9` | `D/1993 F2` | `P1` | `Shoemaker-Levy 9` |
| `1I/`` `Oumuamua `` | `1I` | | `` `Oumuamua `` |

### Pattern 02 (name first)

The desktop regex for this pattern is
``^([PCXDIA]\/)*((?<name>['`a-zA-Z- ]+ *[0-9]*)* )*\(*(?<id>…)-*(?<fragment>…)\)*``. Its nested quantifiers backtrack exponentially on a long line that contains no designation: the desktop app hangs when asked to import such a file. A web implementation MUST NOT use it. It MUST use this linear procedure instead, which returns the same result for every input the desktop regex can finish on. The harness checks this on all MPC names and 20 000 fuzzed strings.

```
REST = ^\(*(?<id>[0-9]+[PCXDIA]|[PCXDIA]/-*[0-9]+ [a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)

parse02(s):
    kmax = number of leading "X/" pairs in s (X a type letter), counted from the start
    for k = kmax down to 0:
        t = s without its first 2k characters
        run = length of the longest prefix of t made only of name characters and digits
        lengths = []
        if t is non-empty and t[0] is a name character:
            for j = run−1 down to 0: if t[j] = ' ': lengths.add(j+1)     # longest first
        lengths.add(0)
        for m in lengths:
            if REST matches t[m:]:
                name = (m = 0) ? "" : lastToken(t[0 : m−1])
                return id, fragment from REST, name
    fail

lastToken(x):    # x consists of name characters and digits and starts with a name character
    split x into tokens, each = a maximal run of name characters followed by a maximal run of digits
    return the last token ("" if x is empty)
```

Results to note: `P/PANSTARRS 2 (470P-A)` gives name `PANSTARRS 2`, id `470P`, fragment `A`. `C/2023 R1-B` gives id `C/2023 R1`, fragment `B` and an empty name. `Vaisala (C/1942 EA)` gives name `Vaisala`. Vectors: [`names.json`](vectors/names.json) (`pattern00`, `pattern02`, and every comet of the MPC snapshot).

### Full name from parts

`GetFullFromIdName(id, name, fragment)`:

```
full = id
if fragment ≠ "": full += "-" + fragment
if id contains "/" and name ≠ "": full += " (" + name + ")"      # C/1995 O1 (Hale-Bopp)
else if name ≠ "":               full += "/" + name               # 1P/Halley
```

### Sort key

`GetSortkey(id, fragment)` builds a fixed-width string whose ordinal order is the database order. Periodic numbers come first, then negative years, then positive years, then interstellar objects:

```
if id[0] is a digit:
    group  = id ends with "I" ? "3" : "0"
    number = id without its last character, left-padded with "0" to 4
    codeL = "000"; codeD = "000"
else:
    group  = id[2] = '-' ? "1" : "2"
    parts  = id split on " "
    year   = parts[0] after its first "/", read as a number and truncated (e.g. -146, 1995);
             0 when it does not read as one (like dbl in 03, so "--146" gives 0 and "1,995" 1995);
             beyond 32 bits, −2147483648, which is what .NET 8's (int) cast gives on x64
    if year < 0: year += 10000
    number = year left-padded with "0" to 4
    code   = parts[1] (e.g. "O1", "UN271", "EA"; empty after two spaces)
    codeL  = leading letters of code, left-padded with "0" to 3
    codeD  = following digits of code, left-padded with "0" to 3
fragL = leading letters of fragment, left-padded with "0" to 2
fragD = following digits of fragment, left-padded with "0" to 2
sortkey = group + number + codeL + codeD + fragL + fragD
```

The key is 15 characters, 1 + 4 + 3 + 3 + 2 + 2, for every real designation. Padding never truncates, so a longer part (a 5-digit year or number, `UN2711`, a 3-letter fragment) makes the key longer, as on the desktop.

The key **fails**, and the record is skipped ([03](03-element-formats.md#field-access)), where the desktop throws: an empty id; an id that does not start with a digit and is shorter than 3 characters; no `/` in `parts[0]`; no space in the id.

| id | fragment | sortkey (group · number · codeL · codeD · fragL · fragD) |
|---|---|---|
| `1P` | | `0` `0001` `000` `000` `00` `00` |
| `3D` | `A` | `0` `0003` `000` `000` `0A` `00` |
| `73P` | `BT` | `0` `0073` `000` `000` `BT` `00` |
| `C/-146 P1` | | `1` `9854` `00P` `001` `00` `00` |
| `C/1942 EA` | | `2` `1942` `0EA` `000` `00` `00` |
| `C/2000 A1` | `B2` | `2` `2000` `00A` `001` `0B` `02` |
| `C/2014 UN271` | | `2` `2014` `0UN` `271` `00` `00` |
| `1I` | | `3` `0001` `000` `000` `00` `00` |

The authoritative values are in [`names.json`](vectors/names.json) (`sortkeys`, `mpcSoft00`). Voyager II imports have no id, and their sort key is the empty string.

## Collections

- The **database** (main collection) is a list of comets with unique `full` values. When a comet is added and another with the same `full` exists, the new one is **ignored**: the first one wins. The one exception is the Voyager II importer, which allows duplicates.
- After every import the database is ordered by `sortkey`, ascending, with a **stable** sort.
- Two comets are **equal** (unchanged on re-import) when `full`, `T`, `q`, `e`, `i`, `N`, `w`, `g` and `k` are all exactly equal.
- The **selection** (user collection) is the database after the user's filters and sort ([05](05-database-and-update.md#database-view)). The Ephemeris, Graph and Orbit viewer tools start from a copy of the selection, and each can then be filtered and sorted on its own.

## Comet properties

Filters and sorting both use this list. The order is the order shown in the filter's property menu.

| Key | Label | Type | Unit | Equals tolerance | Filter input range (decimals) | Default compare |
|---|---|---|---|---|---|---|
| `full` | Full name | string | | | | Contains |
| `name` | Discoverer | string | | | | Contains |
| `id` | Designation | string | | | | Contains |
| `Tn` | Perihelion date | date | | 1.0 day | date picker | Greater than |
| `q` | Perihelion distance | number | AU | 0.10 | 0–15 (6) | Less than |
| `PerihEarthDist` | Perihelion distance from Earth | number | AU | 0.10 | 0–15 (6) | Less than |
| `PerihMag` | Perihelion magnitude | number | | 0.20 | −20–40 (2) | Less than |
| `CurrentSunDist` | Current distance from Sun | number | AU | 0.10 | 0–150 (6) | Less than |
| `CurrentEarthDist` | Current distance from Earth | number | AU | 0.10 | 0–150 (6) | Less than |
| `CurrentMag` | Current magnitude | number | | 0.50 | −20–40 (2) | Less than |
| `P` | Period | number | years | 0.10 | 0–10000 (6) | Less than |
| `Q` | Aphelion distance | number | AU | 0.10 | 0–1000 (6) | Less than |
| `a` | Semi-major axis | number | AU | 0.10 | 0–1000 (6) | Less than |
| `e` | Eccentricity | number | | 0.01 | 0–1.2 (6) | Less than |
| `i` | Inclination | number | ° | 1.00 | 0–179.9999 (4) | Less than |
| `N` | Longitude of Ascending Node | number | ° | 1.00 | 0–359.9999 (4) | Less than |
| `w` | Argument of Pericenter | number | ° | 1.00 | 0–359.9999 (4) | Less than |

## Filters

A filter is `{property, checked, text, compare}`. A **filter set** is the list of filter rows shown to the user, up to 10. `FilterManager.ApplyFilters(collection, filters)`:

1. Only **checked** filters count. If none is checked, the result is a copy of the collection.
2. A comet passes when it passes **every** checked filter (AND). The result keeps the collection order.
3. **String** properties: lower-case the property value; split the filter text on `,`; trim and lower-case each term. Compare:
   - `Contains`: some term is a substring;
   - `Does not contain`: no term is a substring;
   - `Starts with`: some term is a prefix;
   - `Ends with`: some term is a suffix.

   An empty term (e.g. a trailing comma) is a substring, prefix and suffix of everything.
4. **Number** properties: the filter value is the text, trimmed, parsed as `dbl` reads it ([03 § Field access](03-element-formats.md#field-access)), or `0` if it does not parse (`Double.TryParse`). `Tn` filters store the chosen date as its JD. Compare:
   - `Greater than`: `value > filter`;
   - `Less than`: `value < filter`;
   - `Equals`: `|value − filter| ≤ tolerance` from the table above.
5. Before applying, every checked filter MUST have text that is not empty or white space only (a date always has one). Otherwise reject with `Please enter value for "<Label>"`, naming the first such filter.

Vectors: [`filters.json`](vectors/filters.json) (all 963 comets of the snapshot, at the vector clock).

## Sorting

Sort properties, in menu order: Designation (`sortkey`), Discoverer (`name`), Perihelion date (`Tn`), Perihelion distance (`q`), Perihelion distance from Earth, Perihelion magnitude, Current distance from Sun, Current distance from Earth, Current magnitude, Period (`P`), Aphelion distance (`Q`), Semi-major axis (`a`), Eccentricity (`e`), Inclination (`i`), Long. of the Asc. Node (`N`), Arg. of Pericenter (`w`). Each sort is either **Ascending** or **Descending**. The default is Designation, ascending.

- Sorting MUST be **stable** in both directions: a descending sort keeps equal items in their original order and does not reverse them.
- Numbers compare numerically.
- Strings (`name`, `sortkey`) compare with **invariant-culture (ICU root) collation**: in JavaScript, `Intl.Collator('und')`. Sort keys contain only digits and capital letters, so ordinal comparison gives the same order for them.
- Choosing a different property keeps the current direction. Choosing a direction re-sorts.

Vectors: [`sorting.json`](vectors/sorting.json).

## Quick selection

The comet pickers in Ephemeris and Graph offer a quick-select menu (`CometManager.GetCometBySelectionType`):

| Menu item | Picks the comet with the smallest |
|---|---|
| Brightest | `CurrentMag` |
| Closest to perihelion | `|Tn − JD(now)|` |
| Closest to Earth | `CurrentEarthDist` |
| Closest to Sun | `CurrentSunDist` |

The first comet in list order wins a tie. When a picker is rebound to a new list and the previously selected comet is still in it (by [equality](#collections)), that comet stays selected; otherwise the Brightest is selected. Vectors: [`selection.json`](vectors/selection.json).
