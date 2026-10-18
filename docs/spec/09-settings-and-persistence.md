# 09 — Settings, persistence and shared components

## Settings

The Settings view replaces the desktop Edit › Settings dialog. Changes apply and are saved immediately; there is no OK or Cancel. The desktop's Network and Programs tabs are dropped ([01](01-product.md#dropped-desktop-features)).

| Setting | Key | Default | UI and rules |
|---|---|---|---|
| Theme | `theme` | `system` | Light / Dark / System (follows `prefers-color-scheme`). Applies immediately. The desktop defaulted to Light and needed a restart. |
| Elements download URL | `downloadUrl` | `https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt` | Text field. **Restore default** puts back the MPC URL. |
| Automatically update elements | `automaticUpdate` | `true` | Checkbox |
| Update interval | `updateInterval` | `7` | Integer 1–99 days. Required while automatic update is on: `Please enter days interval for automatic update`. |
| Last update | `lastUpdateDate` | none | Read-only, shown as a relative time ("3 days ago"). It is set only by importing a download ([05](05-database-and-update.md#import)). |
| Show long calculation confirmation | `showLongCalculationConfirmation` | `true` | Checkbox ([06](06-ephemeris.md#calculation)) |
| Show delete comet confirmation | `showDeleteCometConfirmation` | `true` | Checkbox ([05](05-database-and-update.md#layout)) |
| Location name | `location.name` | `Home` | Text |
| Latitude | `location.latitude` | `0` | 0–90 with 6 decimals, plus an N/S selector. South is stored negative. |
| Longitude | `location.longitude` | `0` | 0–180 with 6 decimals, plus an E/W selector. West is stored negative. |

- **Use my location** MAY fill latitude and longitude from the Geolocation API, after the user agrees.
- **Data management** (new on the web):
  - **Export database** downloads the stored MPC text as `Comets_<yyyy-MM-dd_HHmmss>.txt`, the same as exporting the whole database in the MPC format;
  - **Clear all data** removes the database and resets the settings, after confirmation.

The observer location is read whenever a value needs it. The per-comet `Perih*` values cached for the session keep the location they were first computed with, as on the desktop.

## Storage layout

Browser storage is per origin. Every key is namespaced `comets.`. Every read and write MUST tolerate missing, corrupt or blocked storage, as in private windows: fall back to the defaults and keep working in memory.

| Data | Store | Key | Format |
|---|---|---|---|
| Settings | `localStorage` | `comets.settings` | JSON `{"version": 1, "theme": "system", …}` using the keys above. `lastUpdateDate` is an ISO 8601 UTC string. |
| Database | IndexedDB `comets`, object store `kv` | `database` | The MPC-format text of the database ([05](05-database-and-update.md#storage)) |
| UI conveniences | `localStorage` | `comets.ui` | JSON. Optional: collapsed toolbox panels, last active view, and so on |

- Writes to the database MUST be atomic: one `put` of the whole text.
- On a schema change, bump `version` and migrate on load.

## Shared components

### Date picker

Desktop: `SelectDateControl`, `DateTimeMenuControl`, `FormDateTime`. Used for Start and End, the database live clock, filter dates and the orbit viewer date.

- **Button**: the UTC instant as `dd.MM.yyyy. HH:mm:ss`, with a tooltip `Local Time` showing the same instant in local time.
- **Editor**, opened from the button:
  - title `Universal Time (UTC)`;
  - integer fields D, M, Y, H, M, S (UTC); the day's maximum follows the month and year;
  - a read-only **Local Time** field;
  - a **JD** field with 5 decimals. Editing it sets the date through `JDToDateTime`;
  - in any field, `↑`/`↓` add or subtract one unit of that field with calendar arithmetic (`AddMonths` and similar);
  - **OK** clamps the result to [1700-01-01 00:00, 2300-01-01 00:00] UTC.
- **Presets menu**, next to the button:

  | Preset | Value |
  |---|---|
  | Default | The component's default date; shown only when one is set (Start and End have one) |
  | Today | Today 00:00 UTC |
  | Perihelion Date | Shown only when a comet is in context |
  | `<1 Jan last year>`, `<1 Jan this year>`, `<1 Jan next year>`, `<1 Jan year after next>` | 00:00 UTC; labels formatted `dd MMM yyyy HH:mm:ss` |
  | Add 3 months, Add 6 months, Add 1 year | Calendar arithmetic on the current value |
  | Subtract 3 months, Subtract 6 months, Subtract 1 year | Calendar arithmetic on the current value |

- Presets are not clamped, which matches the desktop. Consumers that require the range (the orbit viewer) clamp on their side.
- Calendar arithmetic clamps the day to the target month's length: Jan 31 + 1 month → Feb 28 or 29.

### Number fields

Desktop: `ValNum`, `ValNumManager`. Each numeric field has a minimum, a maximum and a number of decimals.

- Accepted input: digits; a leading `-` only if the minimum is below 0; a `.` with up to *decimals* digits after it. A `,` typed by the user becomes `.`.
- Input that would make the text invalid or out of range is rejected. On the web this is done in `beforeinput`; showing an inline error instead is also acceptable.
- For integer fields, `↑`/`↓` increment and decrement within the range.
- Empty means "no value". Text is parsed with the invariant culture (`.` is the decimal separator).

| Field | Range | Decimals |
|---|---|---|
| Interval D / H / M | 0–3652 / 0–23 / 0–59 | 0 |
| Requirement Sun / Earth distance | 0–9.99 | 2 |
| Requirement magnitude, graph Min/Max | −20–40 | 2 |
| Filter-on-date distances | 0–99.99 | 2 |
| Filter-on-date magnitude | −20–40 | 2 |
| Grid extent | 0–150 | 0 |
| Update interval | 1–99 | 0 |
| Latitude / Longitude | 0–90 / 0–180 | 6 |
| Database filter values | see [02 § Comet properties](02-data-model.md#comet-properties) | |

## Theme

- Light and dark themes are defined as CSS custom properties. The orbit viewer canvas is always black.
- The graph uses the theme colours from [07](07-graph.md#settings).
- The app SHOULD use system fonts. Monospaced output (ephemeris text, element files) uses a monospace stack such as `ui-monospace, Consolas, monospace`.
