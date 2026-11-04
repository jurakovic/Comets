# Review of M2 — Element formats

A review of `app/js/core/formats/`, `core/comets/merge.js` and `collation.js`, the sort key's year in `designation.js`, and their tests, by a reviewer with a clean context. It checked the code against spec 03, 10, 11 and 12 and against the desktop's `ImportManager`, `ExportManager`, `CometCollection` and `ExtensionMethods`, and ran its own fuzzing and mutation checks. It found **nothing that changes what a real MPC or JPL file imports or exports**. The findings below are integers that could print as `-0`, weak tests, spec text, and parser differences that only absurd input reaches. All were fixed in the close-out commit unless the outcome says otherwise.

| # | Severity | Finding | Outcome |
|---|---|---|---|
| 1 | Low | NASA's `Th` was `-0` for a day fraction in (−0.05, 0), because `roundEven` keeps the sign; the MPC writer, which is also the database's storage format, then wrote `8.-0000` where the desktop writes `8.0000` | `+ 0` after the rounding, as `Convert.ToInt32` gives an integer. Tested; the test fails without the fix |
| 2 | Low | SkyTools wrote `Math.trunc(Th / 10)`, which is `-0` for a `Th` of −1 to −9 (`01.-000`) | `+ 0`. Tested; fails without the fix |
| 3 | Low | The day-fraction test named "ties to even" had no tie, so `Math.round` passed the suite; 125 of the day's whole seconds give a float64 tie that rounds differently (00:01:48 → 12.5) | 00:01:48 must give 12. Fails with `Math.round` |
| 4 | Low | Mutations the suite let through: any change to the detection order (no line of any vector file is read by two parsers), `jdDate` without its year-9999 limit, TheSky padding to 12, a Home Planet header test cut to `Name`, Comet for Windows records of 13 lines | The detection order is now compared with spec 03's own list, read from the spec; tests for a JD past 9999, a TheSky date without a fraction, the `Name,Perihelion` prefix, and a last record of exactly 12 lines. Each fails on its mutation, except the 13-line one: `size` matters only to detection, which finds Comet for Windows by its header, so that mutation changes nothing |
| 5 | Low | `File.ReadAllLines` also honours UTF-16 and UTF-32 byte-order marks; spec 03 said UTF-8 only, so a file Notepad saved as "Unicode" would be `Unknown` | Spec 03's Decoding covers them. Decoding is adapter work: M4 S6 in the plan says so |
| 6 | Low | Spec 03 said the app reads 21 formats; it reads 19 (Autostar files as TheSky) | Fixed |
| 7 | Low | Spec 12 said a record parser returns `null`; the parsers throw `InvalidRecord`, which `attempt` turns into `null` (D11) | Spec 12 describes the parsers as they are |
| 8 | Low | `InvalidRecord` extended `Error`, so every skipped record captured a stack: about 5 µs a record, 5 s for a million junk lines | `InvalidRecord` is a plain class: 1.3 µs a record |
| 9 | Low | Differences that only absurd input reaches: .NET takes NUL characters after a number; it rejects decimals of 2⁹⁶ and more; `Math.Round(x, 6)` leaves \|x\| ≥ 10¹⁶ alone; a sort-key year beyond 32 bits casts to `int.MinValue` on .NET 8 x64 | All four mirrored, tested (each test fails without its change), and written into spec 02 and 03 |
| 10 | Low | `core/comets/designation.js` imported `core/formats/text.js`, while `core/formats` imports `core/comets`: no module cycle, but the two folders depended on each other | .NET number reading and trimming moved to `core/format/parse.js`, which both use; `text.js` keeps the failing wrappers of spec 03 |
| 11 | Low | `nameWithSlash` had no `@returns` | Added |

## Checked and found correct

- **Readers against `ImportManager.cs`:** the same lines, first lines and steps, the same columns, trimmed for fixed-width columns and untrimmed for split fields, and records failing exactly where the desktop throws: string bounds, split indices, Voyager II's `t[1]`, PC-TCS's `tempId[p]`, Dance of the Planets' `Substring(70)`, xephem's `from(2)` and `n = 0`, `DateTime` construction, NASA's `Convert.ToInt32` overflow.
- **Writers against `ExportManager.cs`:** format strings, argument order, headers, PC-TCS's padding to 126, `today` in UTC, CRLF after every line including the empty ones, and an empty collection.
- **Number parsing:** 400 000 random strings against a port of .NET's `TryParseNumber` for the Integer, Float | AllowThousands and Number styles; the only difference was the trailing NUL (finding 9). `trim` has the set of `Char.IsWhiteSpace`.
- **Merge:** 400 000 random cases against a literal port of `ImportMain`, `CometCollection.Add` and `List.Remove`, with duplicates in the database and in Voyager II files: no mismatch. The stable sort and a file with no comets match too.
- **Robustness:** random texts, every input file mutated 40 times, and adversarial ones (million-character lines of spaces, separators, `P/` prefixes, parentheses, digits, lone surrogates; a million empty lines): `detectFormat` and every reader threw nothing but `InvalidRecord`, nothing hung, and pattern 02 stayed linear.
- **Detection:** the header strings and the order are the desktop's.
- **Vectors:** every file of `import-expected/` (20 inputs, 19 round trips), `export/*` byte for byte with CRLF, `import-detection.json`, `import-merge.json`; the computed `q` of xephem and `w` of MyStars! match exactly, NASA's precessed angles within 1·10⁻⁹.
- **Performance:** the full MPC file (963 comets) is detected and imported in about 17 ms on a first run under Node (the test allows 100 ms); merging it into a full database takes about 7 ms, and the largest export (Celestia) about 25 ms.
- **Conventions:** module headers cite their spec section, comments carry no plan ids or history, records are frozen, and the formats are registry entries; detection's order is data.
