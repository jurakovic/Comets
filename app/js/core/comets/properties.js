// The comet properties that filters and sorting use, in the filter menu's order, and the value
// accessor through which they are read. docs/spec/02-data-model.md, Comet properties.

/**
 * a string compares as text, a number by value, a date by its JD with a date picker as its editor
 * @typedef {'string' | 'number' | 'date'} PropertyType
 */

/**
 * what a filter's number field accepts
 * @typedef {object} InputRange
 * @property {number} min
 * @property {number} max
 * @property {number} decimals
 */

/**
 * @typedef {object} Property
 * @property {string} key the comet field or session value it reads
 * @property {string} label as the filter menu shows it, and as validation messages name it
 * @property {PropertyType} type
 * @property {string} unit shown after the value editor; "" for none
 * @property {number} tolerance how far apart two values may be and still be equal; 0 for strings
 * @property {InputRange | null} input the number field's range; null for text and dates
 * @property {string} compare the key of the compare a new filter row starts with (compares.js)
 * @property {boolean} session whether the value depends on the clock and the observer, so the
 *   Observations service provides it rather than the comet
 */

/**
 * a property's value for a comet: a comet field, or a session value the Observations service
 * caches (`Tn`, `Perih*`, `Current*`)
 * @typedef {(comet: import('./comet.js').Comet, key: string) => number | string} ValueAccessor
 */

/**
 * @param {string} key
 * @param {string} label
 * @param {string} unit
 * @param {number} tolerance
 * @param {[number, number, number]} input min, max, decimals
 * @param {boolean} [session]
 * @returns {Property}
 */
function number(key, label, unit, tolerance, [min, max, decimals], session = false) {
	return Object.freeze({
		key, label, type: /** @type {const} */ ('number'), unit, tolerance,
		input: Object.freeze({ min, max, decimals }), compare: 'lessThan', session
	});
}

/**
 * @param {string} key
 * @param {string} label
 * @returns {Property}
 */
function text(key, label) {
	return Object.freeze({ key, label, type: /** @type {const} */ ('string'), unit: '', tolerance: 0, input: null, compare: 'contains', session: false });
}

const MAGNITUDE = /** @type {[number, number, number]} */ ([-20, 40, 2]);

/** the 17 properties, in the filter menu's order */
export const PROPERTIES = Object.freeze([
	text('full', 'Full name'),
	text('name', 'Discoverer'),
	text('id', 'Designation'),
	Object.freeze({
		key: 'Tn', label: 'Perihelion date', type: /** @type {const} */ ('date'), unit: '', tolerance: 1,
		input: null, compare: 'greaterThan', session: true
	}),
	number('q', 'Perihelion distance', 'AU', 0.1, [0, 15, 6]),
	number('PerihEarthDist', 'Perihelion distance from Earth', 'AU', 0.1, [0, 15, 6], true),
	number('PerihMag', 'Perihelion magnitude', '', 0.2, MAGNITUDE, true),
	number('CurrentSunDist', 'Current distance from Sun', 'AU', 0.1, [0, 150, 6], true),
	number('CurrentEarthDist', 'Current distance from Earth', 'AU', 0.1, [0, 150, 6], true),
	number('CurrentMag', 'Current magnitude', '', 0.5, MAGNITUDE, true),
	number('P', 'Period', 'years', 0.1, [0, 10000, 6]),
	number('Q', 'Aphelion distance', 'AU', 0.1, [0, 1000, 6]),
	number('a', 'Semi-major axis', 'AU', 0.1, [0, 1000, 6]),
	number('e', 'Eccentricity', '', 0.01, [0, 1.2, 6]),
	number('i', 'Inclination', '°', 1, [0, 179.9999, 4]),
	number('N', 'Longitude of Ascending Node', '°', 1, [0, 359.9999, 4]),
	number('w', 'Argument of Pericenter', '°', 1, [0, 359.9999, 4])
]);

const BY_KEY = new Map(PROPERTIES.map(p => [p.key, p]));

/** the keys whose values the Observations service provides */
export const SESSION_KEYS = Object.freeze(PROPERTIES.filter(p => p.session).map(p => p.key));

/**
 * @param {string} key
 * @returns {Property}
 */
export function property(key) {
	const found = BY_KEY.get(key);
	if (!found) throw new Error(`unknown comet property: ${key}`);
	return found;
}

/**
 * a comet field by key, for the properties that are fields (and the sort key)
 * @param {import('./comet.js').Comet} comet
 * @param {string} key
 * @returns {number | string}
 */
export function fieldValue(comet, key) {
	const value = /** @type {Record<string, unknown>} */ (comet)[key];
	if (typeof value !== 'number' && typeof value !== 'string') throw new Error(`not a comet field: ${key}`);
	return value;
}
