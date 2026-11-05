// Ephemeris settings: the defaults of a new form, the interval, the requirements, validation, the
// long-calculation count and the result title. docs/spec/06-ephemeris.md, Settings and Calculation.
import { invalid, valid } from '../validation.js';
import { COLUMNS } from './columns.js';
import { defaultTimespan, timespanErrors, wholeSteps } from './timespan.js';

/** @typedef {import('../astro/ephemeris.js').Ephemeris} Ephemeris */

/**
 * @typedef {object} Interval
 * @property {number} days 0–3652
 * @property {number} hours 0–23
 * @property {number} minutes 0–59
 */

/**
 * a requirement's checkbox and its field, null when the field is empty
 * @typedef {object} Requirement
 * @property {boolean} checked
 * @property {number | null} value
 */

/**
 * @typedef {object} EphemerisSettings
 * @property {number} start UTC milliseconds, whole seconds
 * @property {number} end UTC milliseconds, whole seconds
 * @property {Interval} interval
 * @property {boolean} localTime the time column in local time rather than UTC
 * @property {Readonly<Record<string, boolean>>} columns by column key (columns.js)
 * @property {Readonly<Record<string, Requirement>>} requirements by requirement key
 */

/**
 * @typedef {object} RequirementDefinition
 * @property {string} key
 * @property {string} label
 * @property {import('../comets/properties.js').InputRange} input
 * @property {Requirement} initial
 * @property {string} message when it is checked with an empty field
 * @property {(ep: Ephemeris, value: number) => boolean} keeps whether a row passes
 */

/**
 * @param {string} key
 * @param {string} label
 * @param {[number, number, number]} input
 * @param {Requirement} initial
 * @param {string} message
 * @param {RequirementDefinition['keeps']} keeps
 * @returns {RequirementDefinition}
 */
const requirement = (key, label, [min, max, decimals], initial, message, keeps) =>
	Object.freeze({ key, label, input: Object.freeze({ min, max, decimals }), initial: Object.freeze(initial), message, keeps });

/** the requirements, in the form's order, which is also the order they are validated in */
export const REQUIREMENTS = Object.freeze([
	requirement('maxSunDist', 'Max Sun distance', [0, 9.99, 2], { checked: false, value: null },
		'Please enter Maximum Sun distance value', (ep, value) => ep.SunDist <= value),
	requirement('maxEarthDist', 'Max Earth distance', [0, 9.99, 2], { checked: false, value: null },
		'Please enter Maximum Earth distance value', (ep, value) => ep.EarthDist <= value),
	requirement('minMagnitude', 'Min magnitude', [-20, 40, 2], { checked: true, value: 12 },
		'Please enter Minimum magnitude value', (ep, value) => ep.Magnitude <= value)
]);

/** more steps than this, over all comets, asks before calculating */
export const LONG_CALCULATION_STEPS = 100_000;

/** the question asked then (`SettingsBase.ValidateCalculationAmount`) */
export const LONG_CALCULATION_QUESTION =
	'Calculation using selected settings could take a while, depending on your PC performances. Are you sure you want to continue?';

/** what **Default** resets the interval to */
export const DEFAULT_INTERVAL = Object.freeze({ days: 1, hours: 0, minutes: 0 });

/**
 * a new form's settings: the default dates for now, a 1-day interval, Local Time, the default
 * columns, and Min magnitude 12
 * @param {number} now UTC milliseconds
 * @returns {EphemerisSettings}
 */
export function defaultEphemerisSettings(now) {
	const { start, end } = defaultTimespan(now);
	return Object.freeze({
		start,
		end,
		interval: DEFAULT_INTERVAL,
		localTime: true,
		columns: Object.freeze(Object.fromEntries(COLUMNS.map(c => [c.key, c.on]))),
		requirements: Object.freeze(Object.fromEntries(REQUIREMENTS.map(r => [r.key, r.initial])))
	});
}

/**
 * the interval in milliseconds; a total of 0 means 1 day
 * @param {Interval} interval
 * @returns {number}
 */
export function intervalMs({ days, hours, minutes }) {
	const total = (days * 24 + hours) * 60 + minutes;
	return (total === 0 ? 1440 : total) * 60_000;
}

/**
 * the requirements first, then the timespan, as the desktop checks them; each error names its
 * field: a requirement key, or `start`
 * @param {EphemerisSettings} settings
 * @returns {import('../validation.js').Validation<EphemerisSettings>}
 */
export function validateEphemerisSettings(settings) {
	const errors = [
		...REQUIREMENTS
			.filter(r => settings.requirements[r.key].checked && settings.requirements[r.key].value === null)
			.map(r => ({ field: r.key, message: r.message })),
		...timespanErrors(settings.start, settings.end)
	];
	return errors.length === 0 ? valid(settings) : invalid(errors);
}

/**
 * the steps a calculation takes, for the long-calculation question: whole intervals in the
 * timespan, times the number of comets in multiple mode
 * @param {EphemerisSettings} settings
 * @param {number} comets 1 in single mode
 * @returns {number}
 */
export function calculationSteps(settings, comets) {
	return wholeSteps(settings.start, settings.end, intervalMs(settings.interval)) * comets;
}

/**
 * the result tab's title: `Ephemeris - <full>`, or `Ephemeris - <n> comets` with n the size of the
 * list in multiple mode
 * @param {{ multiple: boolean, comets: ReadonlyArray<{ full: string }>, selected: { full: string } | null }} selection
 * @returns {string}
 */
export function ephemerisTitle({ multiple, comets, selected }) {
	return `Ephemeris - ${multiple ? `${comets.length} comets` : selected?.full ?? ''}`;
}
