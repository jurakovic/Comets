// Reading a file in a known format, and importing it into the database (`ImportMain`).
// docs/spec/03-element-formats.md, Import rules and Merge.
import { countResults, mergeComets } from '../comets/merge.js';
import { detectFormat, EMPTY_FILE, NO_FILE_SELECTED, UNKNOWN } from './detection.js';
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

/** what the Import format label says when detection finds no format */
const NO_FORMAT_LABELS = Object.freeze({
	[NO_FILE_SELECTED]: '(no file selected)',
	[EMPTY_FILE]: '(empty file)',
	[UNKNOWN]: '(unknown)'
});

/** the Total comets tooltip */
export const COUNTS_TOOLTIP = 'New, updates, no changes; total';

/**
 * what the Update dialog shows for a source, and what Import applies
 * @typedef {object} ImportPreview
 * @property {string} format the detected format key, or NoFileSelected, EmptyFile or Unknown
 * @property {string} formatLabel the format's name, or `(no file selected)`, `(empty file)`, `(unknown)`
 * @property {import('../comets/merge.js').Merge | null} merge the file merged into the database; null without a format
 * @property {string} counts `<new>, <updates>, <no changes>; <total>`, or `-` without a format
 */

/**
 * a source merged into the database without applying it (`FormUpdate.SetImportStatus`)
 * @param {ReadonlyArray<Comet>} database
 * @param {string | null} text the source's text; null while there is none
 * @returns {ImportPreview}
 */
export function previewImport(database, text) {
	const format = detectFormat(text);
	if (text === null || Object.hasOwn(NO_FORMAT_LABELS, format)) {
		return Object.freeze({ format, formatLabel: NO_FORMAT_LABELS[/** @type {keyof typeof NO_FORMAT_LABELS} */ (format)], merge: null, counts: '-' });
	}
	const merge = importElements(database, format, text);
	const { New, Update, NoChanges, total } = countResults(merge);
	return Object.freeze({ format, formatLabel: formatByKey(format).name, merge, counts: `${New}, ${Update}, ${NoChanges}; ${total}` });
}
