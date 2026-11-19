// The filters panel's rows (`FilterControl`, `FilterPanelManager`): what a row holds while it is
// edited, how each edit changes it, and the filter rows it gives.
import { defaultTimespan } from '../ephemeris/timespan.js';
import { createFilter, filterNumber, MAX_FILTERS } from '../comets/filter.js';
import { property } from '../comets/properties.js';
import { trim } from '../format/parse.js';
import { instantToJd, jdToInstant } from '../time/julian.js';

/** @typedef {import('../comets/filter.js').Filter} Filter */
/** @typedef {import('../comets/properties.js').PropertyType} PropertyType */

/**
 * a row as the panel edits it: the text field, the number field and the date picker each keep
 * their own value, as on the desktop, and the property's type says which one is shown and counts
 * @typedef {object} PanelRow
 * @property {string} property the property key
 * @property {boolean} checked
 * @property {string} compare the compare key
 * @property {string} text the text field's, for a string property
 * @property {string} number the number field's, for a number property
 * @property {number} date the date picker's, UTC milliseconds, for the Perihelion date
 */

/**
 * an edit of one row: the property, the checkbox, the compare, or one editor's value
 * @typedef {{ property: string } | { checked: boolean } | { compare: string } | { text: string } | { number: string } | { date: number }} RowEdit
 */

/** the property a row added by + starts with */
export const NEW_ROW_PROPERTY = 'full';

/**
 * a row of the property, unchecked, with its default compare, empty fields, and the default
 * start date in its date picker (`CommonManager.DefaultDateStart`)
 * @param {string} key the property key
 * @param {number} now UTC milliseconds
 * @returns {Readonly<PanelRow>}
 */
export function newRow(key, now) {
	return Object.freeze({ property: key, checked: false, compare: property(key).compare, text: '', number: '', date: defaultTimespan(now).start });
}

/**
 * how a filter's text goes into a row's editor, and back
 * @type {Readonly<Record<PropertyType, { read: (row: Readonly<PanelRow>, text: string) => Readonly<PanelRow>, text: (row: Readonly<PanelRow>) => string }>>}
 */
const EDITORS = Object.freeze({
	string: { read: (row, text) => ({ ...row, text }), text: row => trim(row.text) },
	number: { read: (row, number) => ({ ...row, number }), text: row => trim(row.number) },
	// the date as its JD in text, as `FilterControl.CollectFilters` writes it
	date: { read: (row, text) => ({ ...row, date: jdToInstant(filterNumber(text)) }), text: row => String(instantToJd(row.date)) }
});

/**
 * the rows that show filters, each value in the editor of its property's type
 * @param {ReadonlyArray<Filter>} filters
 * @param {number} now for the date pickers of rows that are not dates
 * @returns {ReadonlyArray<Readonly<PanelRow>>}
 */
export function panelRows(filters, now) {
	return Object.freeze(filters.map(filter => {
		const row = { ...newRow(filter.property, now), checked: filter.checked, compare: filter.compare };
		return Object.freeze(EDITORS[property(filter.property).type].read(row, filter.text));
	}));
}

/**
 * the filters the rows give: each row's value from the editor its property shows, trimmed
 * @param {ReadonlyArray<Readonly<PanelRow>>} rows
 * @returns {ReadonlyArray<Filter>}
 */
export function rowFilters(rows) {
	return Object.freeze(rows.map(row => createFilter(row.property, {
		checked: row.checked, compare: row.compare, text: EDITORS[property(row.property).type].text(row)
	})));
}

/**
 * whether + may add a row
 * @param {ReadonlyArray<unknown>} rows
 * @returns {boolean}
 */
export function canAddRow(rows) {
	return rows.length < MAX_FILTERS;
}

/**
 * the rows with a Full name row added at the end; the same rows when they are full
 * @param {ReadonlyArray<Readonly<PanelRow>>} rows
 * @param {number} now
 * @returns {ReadonlyArray<Readonly<PanelRow>>}
 */
export function addRow(rows, now) {
	return canAddRow(rows) ? Object.freeze([...rows, newRow(NEW_ROW_PROPERTY, now)]) : rows;
}

/**
 * the rows without the one at index
 * @param {ReadonlyArray<Readonly<PanelRow>>} rows
 * @param {number} index
 * @returns {ReadonlyArray<Readonly<PanelRow>>}
 */
export function removeRow(rows, index) {
	return Object.freeze(rows.filter((_, i) => i !== index));
}

/**
 * what each edit does to its row: a new property starts from its default compare and keeps the
 * editors' values and the checkbox; a value makes the row checked when it is not empty and
 * unchecked when it is; a date always makes it checked
 * @type {Readonly<Record<string, (row: Readonly<PanelRow>, value: never) => PanelRow>>}
 */
const EDITS = Object.freeze({
	property: (row, /** @type {string} */ key) => ({ ...row, property: key, compare: property(key).compare }),
	checked: (row, /** @type {boolean} */ checked) => ({ ...row, checked }),
	compare: (row, /** @type {string} */ compare) => ({ ...row, compare }),
	text: (row, /** @type {string} */ text) => ({ ...row, text, checked: trim(text) !== '' }),
	number: (row, /** @type {string} */ number) => ({ ...row, number, checked: trim(number) !== '' }),
	date: (row, /** @type {number} */ date) => ({ ...row, date, checked: true })
});

/**
 * the rows with one edit made to the row at index
 * @param {ReadonlyArray<Readonly<PanelRow>>} rows
 * @param {number} index
 * @param {Readonly<RowEdit>} edit
 * @returns {ReadonlyArray<Readonly<PanelRow>>}
 */
export function editRow(rows, index, edit) {
	const [[name, value]] = Object.entries(edit);
	const change = EDITS[name];
	if (!change) throw new Error(`unknown filter row edit: ${name}`);
	return Object.freeze(rows.map((row, i) => (i === index ? Object.freeze(change(row, /** @type {never} */ (value))) : row)));
}
