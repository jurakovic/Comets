// Reading a file in a known format, and importing it into the database (`ImportMain`).
// docs/spec/03-element-formats.md, Import rules and Merge.
import { mergeComets } from '../comets/merge.js';
import { formatByKey } from './registry.js';
import { attempt, splitLines } from './text.js';

/**
 * @typedef {import('../comets/comet.js').Comet} Comet
 */

/**
 * the valid records of a file in file order; invalid ones are skipped, and so is a comet whose
 * full name came before, unless the format allows duplicates. A format with no reader gives none.
 * @param {string} key a format key
 * @param {string} text
 * @returns {Comet[]}
 */
export function readElements(key, text) {
	const { reader } = formatByKey(key);
	if (!reader) return [];
	const lines = splitLines(text);
	/** @type {Comet[]} */
	const comets = [];
	const seen = new Set();
	for (let start = reader.first; start < lines.length; start += reader.step) {
		const record = lines.slice(start, start + reader.size);
		const comet = attempt(() => reader.parse(record));
		if (comet && (reader.duplicates || !seen.has(comet.full))) {
			comets.push(comet);
			seen.add(comet.full);
		}
	}
	return comets;
}

/**
 * a file merged into the database (`ImportMain`), for the preview and for Import
 * @param {ReadonlyArray<Comet>} database
 * @param {string} key the detected format
 * @param {string} text
 * @returns {import('../comets/merge.js').Merge}
 */
export function importElements(database, key, text) {
	return mergeComets(database, readElements(key, text), formatByKey(key).reader?.duplicates ?? false);
}
