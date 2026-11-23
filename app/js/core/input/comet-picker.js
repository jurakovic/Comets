// The comet picker's rules (`SelectCometControl`): the combo box's entries, `[ALL COMETS (n)]`
// first, and the choice each makes; the picker's own list, filtered over the whole database and
// sorted, and read again when the database changes; the choice kept across a new list; the chosen
// comet's tooltip and its perihelion date.
import { applyFilters, validateFilters } from '../comets/filter.js';
import { rebindSelection } from '../comets/selection.js';
import { sortComets } from '../comets/sort.js';
import { compileDateFormat, DATE_PERIHELION } from '../format/dates.js';
import { formatNumber } from '../format/number.js';
import { instantToParts } from '../time/calendar.js';
import { jdToInstant } from '../time/julian.js';
import { valid } from '../validation.js';

/** @typedef {import('../comets/comet.js').Comet} Comet */
/** @typedef {import('../comets/filter.js').Filter} Filter */
/** @typedef {import('../comets/sort.js').Sort} Sort */
/** @typedef {import('../comets/properties.js').ValueAccessor} ValueAccessor */

/**
 * the comets a picker offers, with the filters and sort that made them; each picker keeps its own,
 * apart from the app-wide selection
 * @typedef {object} PickerList
 * @property {ReadonlyArray<Comet>} comets in the picker's order
 * @property {ReadonlyArray<Filter>} filters the rows last applied, over the whole database
 * @property {Readonly<Sort>} sort
 */

/**
 * what the picker has chosen: every comet of its list (multiple mode), one comet, or nothing
 * while its list is empty
 * @typedef {object} PickerChoice
 * @property {boolean} all multiple mode
 * @property {Comet | null} comet the comet chosen; null in multiple mode and while nothing is chosen
 */

/** @type {Readonly<PickerChoice>} nothing chosen, as while the list is empty */
export const NO_CHOICE = Object.freeze({ all: false, comet: null });

/** @type {Readonly<PickerChoice>} every comet of the list */
export const ALL_COMETS = Object.freeze({ all: true, comet: null });

/** a period from this many years on shows as `-` in the tooltip */
const LONG_PERIOD = 10_000;

const PERIHELION_DATE = compileDateFormat(DATE_PERIHELION);

/**
 * @param {Comet} comet
 * @returns {Readonly<PickerChoice>}
 */
const single = comet => Object.freeze({ all: false, comet });

/**
 * the combo box's first entry, which chooses every comet of the list
 * @param {number} count the list's comets
 * @returns {string}
 */
export function allCometsLabel(count) {
	return `[ALL COMETS (${count})]`;
}

/**
 * the combo box's entries: `[ALL COMETS (n)]`, then the list's full names in its order; none for
 * an empty list, which offers no multiple mode either
 * @param {ReadonlyArray<Comet>} comets
 * @returns {ReadonlyArray<string>}
 */
export function pickerEntries(comets) {
	return Object.freeze(comets.length === 0 ? [] : [allCometsLabel(comets.length), ...comets.map(comet => comet.full)]);
}

/**
 * the entry that shows a choice
 * @param {ReadonlyArray<Comet>} comets
 * @param {Readonly<PickerChoice>} choice
 * @returns {number} an index into pickerEntries; -1 for none
 */
export function entryIndex(comets, { all, comet }) {
	if (comets.length === 0) return -1;
	if (all) return 0;
	const index = comet ? comets.indexOf(comet) : -1;
	return index < 0 ? -1 : index + 1;
}

/**
 * the choice an entry makes
 * @param {ReadonlyArray<Comet>} comets
 * @param {number} index an index into pickerEntries
 * @returns {Readonly<PickerChoice>}
 */
export function choiceAt(comets, index) {
	if (comets.length === 0 || index < 0 || index > comets.length) return NO_CHOICE;
	return index === 0 ? ALL_COMETS : single(comets[index - 1]);
}

/**
 * the choice once the picker shows a new list (`SelectCometControl.DataBind`): multiple mode
 * stays, and so does the comet chosen while an equal one is in the list; otherwise the brightest
 * @param {ReadonlyArray<Comet>} comets the new list
 * @param {Readonly<PickerChoice>} previous
 * @param {ValueAccessor} value
 * @param {number} nowJd the JD of now
 * @returns {Readonly<PickerChoice>} the list's own record, when a comet is chosen
 */
export function rebindChoice(comets, previous, value, nowJd) {
	if (comets.length === 0) return NO_CHOICE;
	if (previous.all) return ALL_COMETS;
	const comet = rebindSelection(comets, previous.comet, value, nowJd);
	return comet ? single(comet) : NO_CHOICE;
}

/**
 * the picker's list sorted again as it is, so equal comets keep their order (`SortMenuControl`)
 * @param {Readonly<PickerList>} list
 * @param {Readonly<Sort>} sort
 * @param {ValueAccessor} value
 * @returns {Readonly<PickerList>}
 */
export function sortPickerList(list, sort, value) {
	return Object.freeze({ ...list, comets: sortComets(list.comets, sort, value), sort: Object.freeze({ ...sort }) });
}

/**
 * the picker's list from filter rows over the whole database, in database order through the
 * picker's sort, as the Database dialog of the desktop's Filter gave it; rows that do not
 * validate give their errors instead
 * @param {ReadonlyArray<Comet>} database the main collection
 * @param {ReadonlyArray<Filter>} filters
 * @param {Readonly<Sort>} sort the picker's
 * @param {ValueAccessor} value
 * @returns {import('../validation.js').Validation<Readonly<PickerList>>}
 */
export function filterPickerList(database, filters, sort, value) {
	const validation = validateFilters(filters);
	if (!validation.ok) return validation;
	return valid(Object.freeze({
		comets: sortComets(applyFilters(database, filters, value), sort, value),
		filters: Object.freeze([...filters]),
		sort
	}));
}

/**
 * the picker's list read again over a database that has changed (the load, an import, a deletion,
 * Clear all data): its own filters, which validated when they were applied, over the new database
 * in database order through its sort, so that it offers the database's own records
 * @param {Readonly<PickerList>} list
 * @param {ReadonlyArray<Comet>} database the main collection as it is now
 * @param {ValueAccessor} value
 * @returns {Readonly<PickerList>}
 */
export function pickerListOver(list, database, value) {
	return Object.freeze({ ...list, comets: sortComets(applyFilters(database, list.filters, value), list.sort, value) });
}

/**
 * the instant of a comet's nearest perihelion (`JDToDateTime(Tn)`)
 * @param {Comet} comet
 * @param {ValueAccessor} value
 * @returns {number} UTC milliseconds
 */
function perihelionInstant(comet, value) {
	return jdToInstant(Number(value(comet, 'Tn')));
}

/**
 * the Perihelion Date preset of the date pickers beside the picker: the chosen comet's `Tn`; none
 * in multiple mode, as on the desktop, nor while nothing is chosen
 * @param {Readonly<PickerChoice>} choice
 * @param {ValueAccessor} value
 * @returns {number | null} UTC milliseconds
 */
export function perihelionDate({ comet }, value) {
	return comet ? perihelionInstant(comet, value) : null;
}

/**
 * the tooltip on the chosen comet (`SelectCometControl.OnSelectedCometChangedInternal`): its full
 * name as the title, then its perihelion date, period, perihelion distance and current distances
 * and magnitude
 * @param {Comet} comet
 * @param {ValueAccessor} value
 * @returns {Readonly<{ title: string, body: string }>}
 */
export function cometTooltip(comet, value) {
	/** @param {number} x */
	const six = x => formatNumber(x, '0.000000');
	/** @param {string} key */
	const number = key => Number(value(comet, key));
	return Object.freeze({
		title: comet.full,
		body: [
			`T: ${PERIHELION_DATE(instantToParts(perihelionInstant(comet, value)))}`,
			`P: ${comet.P < LONG_PERIOD ? `${formatNumber(comet.P, '0.0000')} years` : '-'}`,
			`q: ${six(comet.q)} AU`,
			`r: ${six(number('CurrentSunDist'))} AU`,
			`d: ${six(number('CurrentEarthDist'))} AU`,
			`m: ${formatNumber(number('CurrentMag'), '0.00')}`
		].join('\n')
	});
}
