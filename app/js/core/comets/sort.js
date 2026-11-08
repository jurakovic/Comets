// Sorting a list of comets by one property, stably in both directions. docs/spec/02-data-model.md,
// Sorting.
import { compareText } from './collation.js';

/** @typedef {import('./comet.js').Comet} Comet */

/**
 * @typedef {object} SortProperty
 * @property {string} key the comet field or session value
 * @property {string} label as the sort menu shows it
 * @property {boolean} text compared as text (collation) rather than as a number
 */

/**
 * @typedef {object} Sort
 * @property {string} property a SortProperty key
 * @property {boolean} ascending
 */

/**
 * @param {string} key
 * @param {string} label
 * @param {boolean} [text]
 * @returns {SortProperty}
 */
const entry = (key, label, text = false) => Object.freeze({ key, label, text });

/** the sort menu, in its order */
export const SORT_PROPERTIES = Object.freeze([
	entry('sortkey', 'Designation', true),
	entry('name', 'Discoverer', true),
	entry('Tn', 'Perihelion date'),
	entry('q', 'Perihelion distance'),
	entry('PerihEarthDist', 'Perihelion distance from Earth'),
	entry('PerihMag', 'Perihelion magnitude'),
	entry('CurrentSunDist', 'Current distance from Sun'),
	entry('CurrentEarthDist', 'Current distance from Earth'),
	entry('CurrentMag', 'Current magnitude'),
	entry('P', 'Period'),
	entry('Q', 'Aphelion distance'),
	entry('a', 'Semi-major axis'),
	entry('e', 'Eccentricity'),
	entry('i', 'Inclination'),
	entry('N', 'Long. of the Asc. Node'),
	entry('w', 'Arg. of Pericenter')
]);

/** Designation, ascending */
export const DEFAULT_SORT = Object.freeze({ property: 'sortkey', ascending: true });

/**
 * the sort after a choice in the sort menu (`SortMenuControl`): another property keeps the
 * direction, the property already chosen changes nothing (null), and a direction sorts in it
 * @param {Readonly<Sort>} current
 * @param {{ property: string } | { ascending: boolean }} choice
 * @returns {Readonly<Sort> | null} null when nothing is to be sorted
 */
export function chooseSort(current, choice) {
	if ('property' in choice) {
		sortProperty(choice.property);
		return choice.property === current.property ? null : Object.freeze({ property: choice.property, ascending: current.ascending });
	}
	return Object.freeze({ property: current.property, ascending: choice.ascending });
}

/**
 * @param {string} key
 * @returns {SortProperty}
 */
export function sortProperty(key) {
	const found = SORT_PROPERTIES.find(p => p.key === key);
	if (!found) throw new Error(`unknown sort property: ${key}`);
	return found;
}

/**
 * numbers as .NET's Double.CompareTo orders them: NaN before every number
 * @param {number} a
 * @param {number} b
 * @returns {number}
 */
export function compareNumbers(a, b) {
	if (a < b) return -1;
	if (a > b) return 1;
	if (a === b) return 0;
	return Number.isNaN(a) ? (Number.isNaN(b) ? 0 : -1) : 1;
}

/**
 * the comets sorted by a property; equal ones keep their order in either direction, as LINQ's
 * OrderBy and OrderByDescending do
 * @param {ReadonlyArray<Comet>} comets
 * @param {Sort} sort
 * @param {import('./properties.js').ValueAccessor} value
 * @returns {ReadonlyArray<Comet>} a new list
 */
export function sortComets(comets, { property, ascending }, value) {
	const { key, text } = sortProperty(property);
	const keyed = comets.map(comet => ({ comet, key: value(comet, key) }));
	const compare = text
		? (/** @type {typeof keyed[number]} */ a, /** @type {typeof keyed[number]} */ b) => compareText(String(a.key), String(b.key))
		: (/** @type {typeof keyed[number]} */ a, /** @type {typeof keyed[number]} */ b) => compareNumbers(Number(a.key), Number(b.key));
	const sign = ascending ? 1 : -1;
	// Array.prototype.sort is stable, and negating the order keeps equal items as they were
	keyed.sort((a, b) => sign * compare(a, b));
	return Object.freeze(keyed.map(k => k.comet));
}
