// The Graph view's settings form: what its fields show for settings and the settings they give,
// when Calculate asks first, what it calculates, and a saved image's formats and name.
import { fieldText } from '../ephemeris/form.js';
import { LONG_CALCULATION_STEPS } from '../ephemeris/settings.js';
import { stampedFileName } from '../format/dates.js';
import { numberOf } from '../input/number.js';
import { instantToJd } from '../time/julian.js';
import { graphCalculationSteps } from './settings.js';

// Calculate waits for a choice, and runs over the comets it chooses, as for an ephemeris
export { canCalculate, cometsOf } from '../ephemeris/form.js';

/** @typedef {import('./settings.js').GraphSettings} GraphSettings */
/** @typedef {import('./settings.js').LineOption} LineOption */
/** @typedef {import('./settings.js').RangeLimit} RangeLimit */

/**
 * what the form's fields hold: the dates, the chart type, the colours as a colour input holds
 * them (`#rrggbb`), the lines' checkboxes, and each end of the value range's checkbox and text
 * @typedef {object} GraphFields
 * @property {number} start UTC milliseconds
 * @property {number} end UTC milliseconds
 * @property {string} chartType a ChartType key
 * @property {string} valueColor
 * @property {Readonly<LineOption>} perihelionLine
 * @property {Readonly<LineOption>} nowLine
 * @property {Readonly<{ checked: boolean, text: string }>} minimum
 * @property {Readonly<{ checked: boolean, text: string }>} maximum
 */

/**
 * an image a graph saves as
 * @typedef {object} ImageFormat
 * @property {string} key
 * @property {string} type the media type a canvas encodes
 * @property {string} extension of the file, without its dot
 */

/** the formats a graph saves as: PNG, which Save and `Ctrl/⌘+S` write, or JPEG */
export const IMAGE_FORMATS = Object.freeze(/** @type {ImageFormat[]} */ ([
	{ key: 'png', type: 'image/png', extension: 'png' },
	{ key: 'jpeg', type: 'image/jpeg', extension: 'jpg' }
]).map(format => Object.freeze(format)));

/** a saved graph's name, before its time stamp (`FormGraph.Save`) */
const FILE_PREFIX = 'Comets_Graph';


/**
 * an end of the value range as its checkbox and field give it: no value while the field is empty
 * (`ValueRangeControl.MinValue`)
 * @param {boolean} checked
 * @param {string} text
 * @returns {Readonly<RangeLimit>}
 */
export function limitOf(checked, text) {
	return Object.freeze({ checked, value: numberOf(text) });
}

/**
 * an end of the value range once its field is typed into: checked while the field holds text,
 * unchecked when it is emptied (`ValueRangeControl.txtMinMag_TextChanged`)
 * @param {string} text
 * @returns {Readonly<RangeLimit>}
 */
export function typedLimit(text) {
	return limitOf(text.length > 0, text);
}

/**
 * a line's option with its colour converted
 * @param {Readonly<LineOption>} line
 * @param {(color: string) => string} convert
 * @returns {Readonly<LineOption>}
 */
const lineOf = ({ checked, color }, convert) => Object.freeze({ checked, color: convert(color) });

/**
 * the fields that show settings
 * @param {GraphSettings} settings
 * @returns {Readonly<GraphFields>}
 */
export function fieldsOf({ start, end, chartType, valueColor, perihelionLine, nowLine, minimum, maximum }) {
	const lower = (/** @type {string} */ color) => color.toLowerCase();
	return Object.freeze({
		start,
		end,
		chartType,
		valueColor: lower(valueColor),
		perihelionLine: lineOf(perihelionLine, lower),
		nowLine: lineOf(nowLine, lower),
		minimum: Object.freeze({ checked: minimum.checked, text: fieldText(minimum.value) }),
		maximum: Object.freeze({ checked: maximum.checked, text: fieldText(maximum.value) })
	});
}

/**
 * the settings the fields give, to validate; colours as `#RRGGBB`
 * @param {Readonly<GraphFields>} fields
 * @returns {GraphSettings}
 */
export function settingsOf({ start, end, chartType, valueColor, perihelionLine, nowLine, minimum, maximum }) {
	const upper = (/** @type {string} */ color) => color.toUpperCase();
	return Object.freeze({
		start,
		end,
		chartType,
		valueColor: upper(valueColor),
		perihelionLine: lineOf(perihelionLine, upper),
		nowLine: lineOf(nowLine, upper),
		minimum: limitOf(minimum.checked, minimum.text),
		maximum: limitOf(maximum.checked, maximum.text)
	});
}

/**
 * whether Calculate asks the long-calculation question first: while its setting is on, for more
 * than LONG_CALCULATION_STEPS steps over all the comets, as for an ephemeris
 * @param {GraphSettings} settings validated
 * @param {number} comets the comets it runs over
 * @param {boolean} confirmation the setting `showLongCalculationConfirmation`
 * @returns {boolean}
 */
export function asksLongCalculation(settings, comets, confirmation) {
	return confirmation && graphCalculationSteps(settings, comets) > LONG_CALCULATION_STEPS;
}

/**
 * the calculation's input, all of it plain data
 * @param {object} options
 * @param {ReadonlyArray<import('../comets/comet.js').Comet>} options.comets cometsOf the choice
 * @param {GraphSettings} options.settings validated
 * @param {boolean} options.multiple whether the comet picker chose all comets
 * @param {import('../astro/ephemeris.js').Observer} options.observer the location when Calculate was pressed
 * @param {number} options.now UTC milliseconds, when Calculate was pressed
 * @param {(comet: import('../comets/comet.js').Comet) => number} options.nearest each comet's Tn
 * @returns {import('./sampling.js').GraphInput}
 */
export function calculationInput({ comets, settings, multiple, observer, now, nearest }) {
	const { latitude, longitude } = observer;
	return Object.freeze({
		comets,
		nearest: Object.freeze(comets.map(nearest)),
		settings,
		multiple,
		observer: Object.freeze({ latitude, longitude }),
		nowJd: instantToJd(now)
	});
}

/**
 * a saved graph's file name, in the local time of the save (`Comets_Graph_2026-10-18_140501.png`)
 * @param {import('../time/calendar.js').DateParts} local
 * @param {Readonly<ImageFormat>} format
 * @returns {string}
 */
export function graphFileName(local, format) {
	return stampedFileName(FILE_PREFIX, local, format.extension);
}
