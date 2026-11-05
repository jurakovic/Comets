// The compare operators of a filter row, by property type, in the compare menu's order.
// docs/spec/02-data-model.md, Filters.

/**
 * `test` gets the lower-cased value and the filter's trimmed, lower-cased terms
 * @typedef {object} TextCompare
 * @property {string} key
 * @property {string} label
 * @property {(value: string, terms: ReadonlyArray<string>) => boolean} test
 */

/**
 * `test` gets the value, the filter's number and the property's tolerance
 * @typedef {object} NumberCompare
 * @property {string} key
 * @property {string} label
 * @property {(value: number, filter: number, tolerance: number) => boolean} test
 */

/**
 * @param {string} key
 * @param {string} label
 * @param {TextCompare['test']} test
 * @returns {TextCompare}
 */
const text = (key, label, test) => Object.freeze({ key, label, test });

/**
 * @param {string} key
 * @param {string} label
 * @param {NumberCompare['test']} test
 * @returns {NumberCompare}
 */
const number = (key, label, test) => Object.freeze({ key, label, test });

export const TEXT_COMPARES = Object.freeze([
	text('contains', 'Contains', (value, terms) => terms.some(t => value.includes(t))),
	text('doesNotContain', 'Does not contain', (value, terms) => !terms.some(t => value.includes(t))),
	text('startsWith', 'Starts with', (value, terms) => terms.some(t => value.startsWith(t))),
	text('endsWith', 'Ends with', (value, terms) => terms.some(t => value.endsWith(t)))
]);

// "Greater" where the desktop spells "Greather" (spec 10, D15)
export const NUMBER_COMPARES = Object.freeze([
	number('greaterThan', 'Greater than (>)', (value, filter) => value > filter),
	number('lessThan', 'Less than (<)', (value, filter) => value < filter),
	number('equals', 'Equals (=)', (value, filter, tolerance) => Math.abs(value - filter) <= tolerance)
]);

/** the compare menu of each property type */
export const COMPARES_BY_TYPE = Object.freeze({
	string: TEXT_COMPARES,
	number: NUMBER_COMPARES,
	date: NUMBER_COMPARES
});

/**
 * @param {ReadonlyArray<{ key: string }>} list
 * @param {string} key
 */
function find(list, key) {
	const found = list.find(c => c.key === key);
	if (!found) throw new Error(`unknown compare: ${key}`);
	return found;
}

/**
 * @param {string} key
 * @returns {TextCompare}
 */
export function textCompare(key) {
	return /** @type {TextCompare} */ (find(TEXT_COMPARES, key));
}

/**
 * @param {string} key
 * @returns {NumberCompare}
 */
export function numberCompare(key) {
	return /** @type {NumberCompare} */ (find(NUMBER_COMPARES, key));
}
