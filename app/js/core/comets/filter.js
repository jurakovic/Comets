// Filters over a list of comets (`FilterManager.ApplyFilters`) and their validation.
// docs/spec/02-data-model.md, Filters.
import { defaultTimespan } from '../ephemeris/timespan.js';
import { parseDouble, trim } from '../format/parse.js';
import { instantToJd } from '../time/julian.js';
import { invalid, valid } from '../validation.js';
import { numberCompare, textCompare } from './compares.js';
import { property } from './properties.js';

/** @typedef {import('./comet.js').Comet} Comet */
/** @typedef {import('./properties.js').ValueAccessor} ValueAccessor */

/**
 * one filter row; a Perihelion date row holds its date's JD as text
 * @typedef {object} Filter
 * @property {string} property the property key
 * @property {boolean} checked only checked rows count
 * @property {string} text the terms (`halley, encke`) or the number
 * @property {string} compare the compare key (compares.js)
 */

/**
 * a filter row with defaults: the property's default compare, unchecked, empty
 * @param {string} key the property key
 * @param {Partial<Filter>} [fields]
 * @returns {Filter}
 */
export function createFilter(key, fields = {}) {
	return Object.freeze({ property: key, checked: false, text: '', compare: property(key).compare, ...fields });
}

/** a filter set has at most this many rows (spec 05, Filters panel) */
export const MAX_FILTERS = 10;

/** the properties of the rows a filter set starts with, and goes back to on Reset */
export const DEFAULT_FILTER_PROPERTIES = Object.freeze(['full', 'Tn', 'q', 'CurrentMag']);

/**
 * the default rows, all unchecked; the Perihelion date row holds the default start date
 * (`CommonManager.DefaultDateStart`)
 * @param {number} now UTC milliseconds
 * @returns {ReadonlyArray<Filter>}
 */
export function defaultFilters(now) {
	const start = String(instantToJd(defaultTimespan(now).start));
	return Object.freeze(DEFAULT_FILTER_PROPERTIES.map(key => createFilter(key, property(key).type === 'date' ? { text: start } : {})));
}

/**
 * the filter's number: the text read as .NET's Double.TryParse reads it, or 0
 * @param {string} text
 * @returns {number}
 */
export function filterNumber(text) {
	return parseDouble(trim(text)) ?? 0;
}

/**
 * every checked row must have text that is not only white space; the error names the first row
 * that has none (`FilterPanelManager.ValidateFilters`)
 * @param {ReadonlyArray<Filter>} filters
 * @returns {import('../validation.js').Validation<ReadonlyArray<Filter>>}
 */
export function validateFilters(filters) {
	const index = filters.findIndex(f => f.checked && trim(f.text) === '');
	if (index < 0) return valid(filters);
	const { label } = property(filters[index].property);
	return invalid([{ field: 'text', index, message: `Please enter value for "${label}"` }]);
}

/** @type {Readonly<Record<import('./properties.js').PropertyType, (filter: Filter, value: ValueAccessor) => (comet: Comet) => boolean>>} */
const PREDICATES = Object.freeze({
	string(filter, value) {
		// lower-cased, split on ",", each term trimmed: an empty term matches everything (spec 10, Q16)
		const terms = filter.text.toLowerCase().split(',').map(trim);
		const { test } = textCompare(filter.compare);
		return comet => test(String(value(comet, filter.property)).toLowerCase(), terms);
	},
	number: numberPredicate,
	date: numberPredicate
});

/**
 * @param {Filter} filter
 * @param {ValueAccessor} value
 * @returns {(comet: Comet) => boolean}
 */
function numberPredicate(filter, value) {
	const target = filterNumber(filter.text);
	const { tolerance } = property(filter.property);
	const { test } = numberCompare(filter.compare);
	return comet => test(Number(value(comet, filter.property)), target, tolerance);
}

/**
 * the comets that pass every checked filter, in list order; all of them when none is checked
 * @param {ReadonlyArray<Comet>} comets
 * @param {ReadonlyArray<Filter>} filters
 * @param {ValueAccessor} value
 * @returns {ReadonlyArray<Comet>} a new list
 */
export function applyFilters(comets, filters, value) {
	const tests = filters.filter(f => f.checked).map(f => PREDICATES[property(f.property).type](f, value));
	return Object.freeze(tests.length === 0 ? [...comets] : comets.filter(comet => tests.every(t => t(comet))));
}
