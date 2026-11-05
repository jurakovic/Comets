// How strings compare wherever the app sorts them: invariant-culture (ICU root) collation, as the
// desktop's default comparer. docs/spec/02-data-model.md, Sorting.

// 'und' is not an available locale, so Intl falls back to the host's own, whose tailoring moves
// "ch" in Czech and "aa" in Danish; CLDR's "en" has no tailoring, which is the root order
const collator = new Intl.Collator('en');

/**
 * negative, zero or positive as a sorts before, with or after b
 * @type {(a: string, b: string) => number}
 */
export const compareText = collator.compare;
