// How strings compare wherever the app sorts them: invariant-culture (ICU root) collation, as the
// desktop's default comparer. docs/spec/02-data-model.md, Sorting.

const collator = new Intl.Collator('und');

/**
 * negative, zero or positive as a sorts before, with or after b
 * @type {(a: string, b: string) => number}
 */
export const compareText = collator.compare;
