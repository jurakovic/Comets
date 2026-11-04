// The end every record parser shares: the name kept or rebuilt, and the finalize step.
// docs/spec/03-element-formats.md, Field access.
import { createComet } from '../comets/comet.js';
import { fullName, parseDesignationFirst } from '../comets/designation.js';
import { fail } from './text.js';

/**
 * @typedef {import('../comets/comet.js').Comet} Comet
 * @typedef {import('../comets/comet.js').CometFields} CometFields
 * @typedef {import('../comets/comet.js').FixedFields} FixedFields
 * @typedef {{ full: string, id: string, name: string, fragment: string }} NameFields
 */

/**
 * a full name **kept** as read, with id, name and fragment from pattern 00
 * @param {string} full
 * @returns {NameFields}
 */
export function keptName(full) {
	const parts = parseDesignationFirst(full) ?? fail(`no designation in ${full}`);
	return { full, ...parts };
}

/**
 * a full name **rebuilt** from what a pattern finds in the text
 * @param {string} text
 * @param {(text: string) => import('../comets/designation.js').Designation | null} [pattern]
 *   pattern 00 unless the format says 02
 * @returns {NameFields}
 */
export function rebuiltName(text, pattern = parseDesignationFirst) {
	const { id, name, fragment } = pattern(text) ?? fail(`no designation in ${text}`);
	return { full: fullName(id, name, fragment), id, name, fragment };
}

/**
 * the finalize step: T, the derived elements and the sort key; fails when the id gives no sort key
 * @param {CometFields} fields
 * @param {FixedFields} [fixed]
 * @returns {Comet}
 */
export function finish(fields, fixed) {
	return createComet(fields, fixed) ?? fail(`no sort key for ${fields.id}`);
}
