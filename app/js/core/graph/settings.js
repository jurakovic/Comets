// Graph settings: the chart types, the defaults of a new form, validation, the long-calculation
// count and the result title. docs/spec/07-graph.md, Settings.
import { compileFormat } from '../format/composite.js';
import { invalid, valid } from '../validation.js';
import { defaultTimespan, timespanErrors } from '../ephemeris/timespan.js';
import { sampleCount } from './sampling.js';

/** @typedef {import('../astro/ephemeris.js').Ephemeris} Ephemeris */

/**
 * @typedef {object} ChartType
 * @property {string} key
 * @property {string} label as the settings form shows it
 * @property {string} axisTitle the value axis title
 * @property {string} tabTitle what the result tab's title starts with
 * @property {(ep: Ephemeris) => number} value the plotted value of a sample
 * @property {number} margin added below the smallest value and above the largest
 * @property {number} multipleMaximum the axis maximum when several comets are drawn and no Maximum is set
 * @property {(value: number) => string} tooltip the value's line in a tooltip
 */

/**
 * a checkbox and its number field, null when the field is empty
 * @typedef {object} RangeLimit
 * @property {boolean} checked
 * @property {number | null} value
 */

/**
 * a line's checkbox and colour
 * @typedef {object} LineOption
 * @property {boolean} checked
 * @property {string} color `#RRGGBB`
 */

/**
 * @typedef {object} GraphSettings
 * @property {number} start UTC milliseconds, whole seconds
 * @property {number} end UTC milliseconds, whole seconds
 * @property {string} chartType a ChartType key
 * @property {string} valueColor `#RRGGBB`
 * @property {LineOption} perihelionLine
 * @property {LineOption} nowLine
 * @property {RangeLimit} minimum
 * @property {RangeLimit} maximum
 */

const MAGNITUDE = compileFormat('Magnitude: {0:0.00}');
const DISTANCE = compileFormat('Distance: {0:0.0000} AU');

/**
 * @param {Omit<ChartType, 'tooltip' | 'margin' | 'multipleMaximum'> & Partial<ChartType>} fields
 * @returns {ChartType}
 */
const chartType = fields => Object.freeze({ margin: 0, multipleMaximum: 2, tooltip: v => DISTANCE([v]), ...fields });

/** the chart types, in the form's order */
export const CHART_TYPES = Object.freeze([
	chartType({
		key: 'Magnitude', label: 'Magnitude', axisTitle: 'Magnitude', tabTitle: 'Magnitude graph',
		value: ep => ep.Magnitude, margin: 0.2, multipleMaximum: 15, tooltip: v => MAGNITUDE([v])
	}),
	chartType({ key: 'SunDistance', label: 'Sun distance', axisTitle: 'Sun distance (AU)', tabTitle: 'Sun distance graph', value: ep => ep.SunDist }),
	chartType({ key: 'EarthDistance', label: 'Earth distance', axisTitle: 'Earth distance (AU)', tabTitle: 'Earth distance graph', value: ep => ep.EarthDist })
]);

/**
 * @param {string} key
 * @returns {ChartType}
 */
export function chartTypeOf(key) {
	const found = CHART_TYPES.find(t => t.key === key);
	if (!found) throw new Error(`unknown chart type: ${key}`);
	return found;
}

/** the range fields accept −20–40 with 2 decimals */
export const RANGE_INPUT = Object.freeze({ min: -20, max: 40, decimals: 2 });

/**
 * a new form's settings: the default dates for now, Magnitude, red values, the perihelion and now
 * lines on, Maximum 12 for every chart type (spec 10, Q20)
 * @param {number} now UTC milliseconds
 * @returns {GraphSettings}
 */
export function defaultGraphSettings(now) {
	const { start, end } = defaultTimespan(now);
	return Object.freeze({
		start,
		end,
		chartType: 'Magnitude',
		valueColor: '#FF0000',
		perihelionLine: Object.freeze({ checked: true, color: '#4169E1' }),
		nowLine: Object.freeze({ checked: true, color: '#32CD32' }),
		minimum: Object.freeze({ checked: false, value: null }),
		maximum: Object.freeze({ checked: true, value: 12 })
	});
}

/**
 * the value range first, then the timespan, in the desktop's order; fields `minimum`, `maximum`
 * or `start`. The desktop stops at the first error, which is errors[0]
 * @param {GraphSettings} settings
 * @returns {import('../validation.js').Validation<GraphSettings>}
 */
export function validateGraphSettings(settings) {
	const { minimum, maximum } = settings;
	/** @type {import('../validation.js').ValidationError[]} */
	const errors = [];
	if (minimum.checked && minimum.value === null) errors.push({ field: 'minimum', message: 'Enter Minimum value' });
	if (maximum.checked && maximum.value === null) errors.push({ field: 'maximum', message: 'Enter Maximum value' });
	if (minimum.checked && maximum.checked && minimum.value !== null && maximum.value !== null && minimum.value >= maximum.value) {
		errors.push({ field: 'maximum', message: 'Minimum value must be lower than Maximum value' });
	}
	errors.push(...timespanErrors(settings.start, settings.end));
	return errors.length === 0 ? valid(settings) : invalid(errors);
}

/**
 * the steps a graph takes, for the long-calculation question, times the comets in multiple mode
 * @param {GraphSettings} settings
 * @param {number} comets 1 in single mode
 * @returns {number}
 */
export function graphCalculationSteps(settings, comets) {
	return Math.max(0, sampleCount(settings.start, settings.end) - 1) * comets;
}

/**
 * the result tab's title: `Magnitude graph - <full>`, `Sun distance graph - <n> comets`, …
 * @param {GraphSettings} settings
 * @param {{ multiple: boolean, comets: ReadonlyArray<{ full: string }>, selected: { full: string } | null }} selection
 * @returns {string}
 */
export function graphTitle(settings, { multiple, comets, selected }) {
	return `${chartTypeOf(settings.chartType).tabTitle} - ${multiple ? `${comets.length} comets` : selected?.full ?? ''}`;
}
