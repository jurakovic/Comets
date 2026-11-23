// The Ephemeris view's settings form: what its fields show for settings and the settings they
// give, when Calculate can run and asks first, what it calculates, and a result's file name.
import { stampedFileName } from '../format/dates.js';
import { numberOf } from '../input/number.js';
import { cometsToCalculate } from './calculation.js';
import { COLUMNS } from './columns.js';
import { calculationSteps, LONG_CALCULATION_STEPS, REQUIREMENTS } from './settings.js';

/** @typedef {import('./settings.js').EphemerisSettings} EphemerisSettings */
/** @typedef {import('./settings.js').Interval} Interval */
/** @typedef {import('./settings.js').Requirement} Requirement */
/** @typedef {import('../input/comet-picker.js').PickerChoice} PickerChoice */
/** @typedef {import('../input/comet-picker.js').PickerList} PickerList */

/**
 * @typedef {object} IntervalField
 * @property {keyof Interval} key
 * @property {string} letter as the form shows it
 * @property {string} name the field's accessible name
 */

/** the interval's fields, in the form's order */
export const INTERVAL_FIELDS = Object.freeze(/** @type {IntervalField[]} */ ([
	{ key: 'days', letter: 'D', name: 'Days' },
	{ key: 'hours', letter: 'H', name: 'Hours' },
	{ key: 'minutes', letter: 'M', name: 'Minutes' }
]).map(field => Object.freeze(field)));

/** a saved ephemeris's name, before its time stamp (`FormEphemeris.Save`) */
const FILE_PREFIX = 'Comets_Ephemeris';

/**
 * the text a number field shows for a value: empty for none, as the desktop's `ToString()` for
 * one
 * @param {number | null} value
 * @returns {string}
 */
export function fieldText(value) {
	return value === null ? '' : String(value);
}

/**
 * the interval the D, H and M fields give: whole numbers, an empty field 0 (`Int()`)
 * @param {Readonly<Record<keyof Interval, string>>} texts
 * @returns {Readonly<Interval>}
 */
export function intervalOf(texts) {
	/** @param {keyof Interval} key */
	const part = key => Math.trunc(numberOf(texts[key]) ?? 0);
	return Object.freeze({ days: part('days'), hours: part('hours'), minutes: part('minutes') });
}

/**
 * a requirement as its checkbox and field give it: no value while the field is empty
 * @param {boolean} checked
 * @param {string} text
 * @returns {Readonly<Requirement>}
 */
export function requirementOf(checked, text) {
	return Object.freeze({ checked, value: numberOf(text) });
}

/**
 * a requirement once its field is typed into: checked while the field holds text, unchecked when
 * it is emptied (`RequirementsControl.txtMaxSunDist_TextChanged`)
 * @param {string} text
 * @returns {Readonly<Requirement>}
 */
export function typedRequirement(text) {
	return requirementOf(text.length > 0, text);
}

/**
 * what the form's fields hold: the dates, the interval's texts, the time column, the columns, and
 * each requirement's checkbox and text
 * @typedef {object} EphemerisFields
 * @property {number} start UTC milliseconds
 * @property {number} end UTC milliseconds
 * @property {Readonly<Record<keyof Interval, string>>} interval
 * @property {boolean} localTime
 * @property {Readonly<Record<string, boolean>>} columns by column key
 * @property {Readonly<Record<string, Readonly<{ checked: boolean, text: string }>>>} requirements by requirement key
 */

/**
 * the fields that show settings
 * @param {EphemerisSettings} settings
 * @returns {Readonly<EphemerisFields>}
 */
export function fieldsOf({ start, end, interval, localTime, columns, requirements }) {
	return Object.freeze({
		start,
		end,
		interval: Object.freeze({ days: fieldText(interval.days), hours: fieldText(interval.hours), minutes: fieldText(interval.minutes) }),
		localTime,
		columns: Object.freeze({ ...columns }),
		requirements: Object.freeze(Object.fromEntries(REQUIREMENTS.map(({ key }) => {
			const { checked, value } = requirements[key];
			return [key, Object.freeze({ checked, text: fieldText(value) })];
		})))
	});
}

/**
 * the settings the fields give, to validate
 * @param {Readonly<EphemerisFields>} fields
 * @returns {EphemerisSettings}
 */
export function settingsOf({ start, end, interval, localTime, columns, requirements }) {
	return Object.freeze({
		start,
		end,
		interval: intervalOf(interval),
		localTime,
		columns: Object.freeze(Object.fromEntries(COLUMNS.map(({ key }) => [key, columns[key]]))),
		requirements: Object.freeze(Object.fromEntries(REQUIREMENTS.map(({ key }) => [key, requirementOf(requirements[key].checked, requirements[key].text)])))
	});
}

/**
 * whether Calculate can run: a comet is chosen, or multiple mode
 * @param {Readonly<PickerChoice>} choice
 * @returns {boolean}
 */
export function canCalculate({ all, comet }) {
	return all || comet !== null;
}

/**
 * the comets a calculation over the picker's choice runs over
 * @param {Readonly<PickerChoice>} choice
 * @param {Readonly<PickerList>} list
 * @returns {ReadonlyArray<import('../comets/comet.js').Comet>}
 */
export function cometsOf({ all, comet }, list) {
	return cometsToCalculate({ multiple: all, comets: list.comets, selected: comet });
}

/**
 * whether Calculate asks the long-calculation question first: while its setting is on, for more
 * than LONG_CALCULATION_STEPS steps over all the comets
 * @param {EphemerisSettings} settings validated
 * @param {number} comets the comets it runs over
 * @param {boolean} confirmation the setting `showLongCalculationConfirmation`
 * @returns {boolean}
 */
export function asksLongCalculation(settings, comets, confirmation) {
	return confirmation && calculationSteps(settings, comets) > LONG_CALCULATION_STEPS;
}

/**
 * the calculation's input, all of it plain data
 * @param {object} options
 * @param {ReadonlyArray<import('../comets/comet.js').Comet>} options.comets cometsOf the choice
 * @param {EphemerisSettings} options.settings validated
 * @param {import('../astro/ephemeris.js').Observer} options.observer the location when Calculate was pressed
 * @param {string} options.timeZone
 * @param {(comet: import('../comets/comet.js').Comet) => number} options.nearest each comet's Tn
 * @returns {import('./calculation.js').EphemerisInput}
 */
export function calculationInput({ comets, settings, observer, timeZone, nearest }) {
	const { latitude, longitude } = observer;
	return Object.freeze({
		comets,
		nearest: Object.freeze(comets.map(nearest)),
		settings,
		observer: Object.freeze({ latitude, longitude }),
		timeZone
	});
}

/**
 * a saved ephemeris's file name, in the local time of the save (`Comets_Ephemeris_2026-10-18_140501.txt`)
 * @param {import('../time/calendar.js').DateParts} local
 * @returns {string}
 */
export function ephemerisFileName(local) {
	return stampedFileName(FILE_PREFIX, local, 'txt');
}
