// Merging an import into the database (`ImportMain`): what is new, updated or unchanged, and the
// database order. docs/spec/03-element-formats.md, Merge.
import { compareText } from './collation.js';
import { sameComet } from './comet.js';

/**
 * @typedef {import('./comet.js').Comet} Comet
 * @typedef {'New' | 'Update' | 'NoChanges'} ImportResult
 */

/**
 * the merged list and each comet's import result, both in database order
 * @typedef {object} Merge
 * @property {ReadonlyArray<Comet>} comets
 * @property {ReadonlyArray<ImportResult>} results results[k] belongs to comets[k]
 */

/**
 * the import results in the order the Update dialog shows them, and the result browser's choices
 * @type {ReadonlyArray<ImportResult>}
 */
export const IMPORT_RESULTS = Object.freeze(['New', 'Update', 'NoChanges']);

/**
 * the comets read from a file merged into the database: each replaces the first database comet
 * with its full name (Update if they differ), or is New; database comets the file lacks stay as
 * NoChanges; the result is stably sorted by sort key. A file with no comets leaves the database as
 * it is, every comet NoChanges (the desktop keeps each comet's result of the import before).
 * @param {ReadonlyArray<Comet>} database
 * @param {ReadonlyArray<Comet>} imported in file order, as the format's reader gives them
 * @param {boolean} [duplicates] keep a comet whose full name is already there (Voyager II)
 * @returns {Merge}
 */
export function mergeComets(database, imported, duplicates = false) {
	/** @type {Array<{ comet: Comet, result: ImportResult, live: boolean }>} */
	let entries;
	if (imported.length === 0) {
		entries = database.map(comet => ({ comet, result: 'NoChanges', live: true }));
	} else if (database.length === 0) {
		entries = imported.map(comet => ({ comet, result: 'New', live: true }));
	} else {
		entries = database.map(comet => ({ comet, result: 'NoChanges', live: true }));
		// the live entries with each full name, in list order, so that "the first with this name"
		// and "is the name there" need no scan
		/** @type {Map<string, number[]>} */
		const live = new Map();
		const track = (/** @type {number} */ index) => {
			const { full } = entries[index].comet;
			const indices = live.get(full);
			if (indices) indices.push(index);
			else live.set(full, [index]);
		};
		entries.forEach((_entry, index) => track(index));
		for (const comet of imported) {
			const indices = live.get(comet.full);
			/** @type {ImportResult} */
			let result = 'New';
			if (indices && indices.length > 0) {
				const old = entries[/** @type {number} */ (indices.shift())];
				old.live = false;
				result = sameComet(old.comet, comet) ? 'NoChanges' : 'Update';
			}
			if (duplicates || !indices || indices.length === 0) {
				entries.push({ comet, result, live: true });
				track(entries.length - 1);
			}
		}
		entries = entries.filter(entry => entry.live);
	}
	// Array.prototype.sort is stable
	entries.sort((a, b) => compareText(a.comet.sortkey, b.comet.sortkey));
	return Object.freeze({
		comets: Object.freeze(entries.map(entry => entry.comet)),
		results: Object.freeze(entries.map(entry => entry.result))
	});
}

/**
 * how many comets of the merge have each result, and the total, as the Update dialog shows them
 * @param {Merge} merge
 * @returns {Readonly<Record<ImportResult, number> & { total: number }>}
 */
export function countResults({ results }) {
	const counts = { New: 0, Update: 0, NoChanges: 0, total: results.length };
	for (const result of results) counts[result] += 1;
	return Object.freeze(counts);
}

/**
 * a choice of the import result browser: a result, or all of them
 * @typedef {object} ImportResultChoice
 * @property {ImportResult | 'All'} key
 * @property {string} label
 */

/** the import result browser's choices, in order (`CometManager.ImportResults`) */
export const IMPORT_RESULT_CHOICES = Object.freeze(/** @type {ImportResultChoice[]} */ ([
	{ key: 'New', label: 'New comets' },
	{ key: 'Update', label: 'Updates' },
	{ key: 'NoChanges', label: 'No changes' },
	{ key: 'All', label: 'All comets' }
]).map(choice => Object.freeze(choice)));

/**
 * the merged comets with a result, in merged order
 * @param {Merge} merge
 * @param {ImportResult | 'All'} key
 * @returns {ReadonlyArray<Comet>}
 */
export function cometsWithResult({ comets, results }, key) {
	return Object.freeze(key === 'All' ? [...comets] : comets.filter((_comet, index) => results[index] === key));
}
