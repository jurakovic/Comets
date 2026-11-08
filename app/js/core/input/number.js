// What a number field accepts (`ValNumManager`): its text, an edit to it, and ↑/↓ on an integer
// field. docs/spec/09-settings-and-persistence.md, Number fields.
import { parseDouble, trim } from '../format/parse.js';

/**
 * a number field's minimum, maximum and decimals (`ValNum`)
 * @typedef {object} NumberRange
 * @property {number} min
 * @property {number} max
 * @property {number} decimals digits allowed after the `.`; 0 for an integer field
 */

/**
 * the text's pattern: digits, a leading `-` only when the minimum is below 0, and a `.` with up
 * to `decimals` digits after it
 * @param {Readonly<NumberRange>} range
 * @returns {RegExp}
 */
function patternOf({ min, decimals }) {
	const sign = min < 0 ? '-?' : '';
	return new RegExp(decimals > 0 ? `^${sign}[0-9]*(?:\\.[0-9]{0,${decimals}})?$` : `^${sign}[0-9]*$`);
}

/**
 * the text's number, read with the invariant culture: null when the text is empty ("no value"),
 * 0 when it does not read as a number (`-`, `.`), as the desktop's `Double()` gives
 * @param {string} text
 * @returns {number | null}
 */
export function numberOf(text) {
	const trimmed = trim(text);
	if (trimmed === '') return null;
	return parseDouble(trimmed) ?? 0;
}

/**
 * whether a field may hold the text: empty, or the pattern with its number within the range,
 * as `ValNumManager.HandleKeyPress` decides after each key
 * @param {string} text
 * @param {Readonly<NumberRange>} range
 * @returns {boolean}
 */
export function acceptsText(text, range) {
	if (text === '') return true;
	if (!patternOf(range).test(text)) return false;
	const value = numberOf(text) ?? 0;
	return value >= range.min && value <= range.max;
}

/**
 * whether the text is only the pattern, whatever its number: the field marks such text instead
 * of refusing it when a range cannot be typed into digit by digit (a JD)
 * @param {string} text
 * @param {Readonly<NumberRange>} range
 * @returns {boolean}
 */
export function matchesPattern(text, range) {
	return patternOf(range).test(text);
}

/**
 * what the user types, with a `,` read as the `.`
 * @param {string} typed
 * @returns {string}
 */
export function normalizeTyped(typed) {
	return typed.replaceAll(',', '.');
}

/**
 * the text after typing over the selection [start, end)
 * @param {string} text
 * @param {number} start
 * @param {number} end
 * @param {string} typed
 * @returns {string}
 */
export function insertInto(text, start, end, typed) {
	return text.slice(0, start) + normalizeTyped(typed) + text.slice(end);
}

/**
 * the text after deleting the selection, or one character before (backward) or after the caret
 * @param {string} text
 * @param {number} start
 * @param {number} end
 * @param {boolean} backward
 * @returns {string}
 */
export function deleteFrom(text, start, end, backward) {
	if (start !== end) return text.slice(0, start) + text.slice(end);
	return backward
		? text.slice(0, Math.max(0, start - 1)) + text.slice(start)
		: text.slice(0, start) + text.slice(start + 1);
}

/**
 * the text after ↑ (+1) or ↓ (−1) on an integer field (`ValNumManager.TextBoxValueUpDown`): the
 * number moves by one while it stays within the range, and is written again; null when the text
 * has no number, and the key does what it would anyway
 * @param {string} text
 * @param {Readonly<NumberRange>} range
 * @param {1 | -1} direction
 * @returns {string | null}
 */
export function stepText(text, range, direction) {
	const value = parseDouble(text);
	if (value === null) return null;
	// the desktop compares with the range truncated to integers (`IMin`, `IMax`)
	const next = direction > 0
		? (value < Math.trunc(range.max) ? value + 1 : value)
		: (value > Math.trunc(range.min) ? value - 1 : value);
	return String(next + 0);
}

/**
 * the keyboard a phone shows: digits, digits with a decimal point, or the full keyboard where a
 * `-` must be typed (iOS's decimal pad has none)
 * @param {Readonly<NumberRange>} range
 * @returns {'numeric' | 'decimal' | 'text'}
 */
export function inputModeOf({ min, decimals }) {
	if (min < 0) return 'text';
	return decimals > 0 ? 'decimal' : 'numeric';
}
