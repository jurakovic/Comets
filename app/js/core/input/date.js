// The date picker's rules (`SelectDateControl`, `FormDateTime`, `DateTimeMenuControl`): the
// button's texts, the editor's fields and their steps, the JD field, OK's clamp, the presets.
// docs/spec/09-settings-and-persistence.md, Date picker.
import { DATE_FULL, DATE_PERIHELION, formatDate } from '../format/dates.js';
import { formatNumber } from '../format/number.js';
import {
	addDays, addHours, addMinutes, addMonths, addSeconds, addYears, daysInMonth, instantToParts, partsToInstant, startOfDay
} from '../time/calendar.js';
import { instantToJd, jdToInstant } from '../time/julian.js';
import { clampToRange, MAXIMUM_JD, MINIMUM_JD } from '../time/range.js';
import { numberOf } from './number.js';

/** @typedef {import('../time/calendar.js').DateParts} DateParts */
/** @typedef {import('./number.js').NumberRange} NumberRange */

/** the editor's title */
export const EDITOR_TITLE = 'Universal Time (UTC)';

/** the button tooltip's title */
export const LOCAL_TIME = 'Local Time';

/**
 * one of the editor's integer fields
 * @typedef {object} DateField
 * @property {keyof DateParts} key
 * @property {string} label as the editor shows it
 * @property {string} name its accessible name
 * @property {(parts: Readonly<DateParts>) => Readonly<NumberRange>} range the day's follows the month and year
 * @property {(instant: number, units: number) => number} step one unit of the field, with calendar arithmetic
 */

/**
 * @param {keyof DateParts} key
 * @param {string} label
 * @param {string} name
 * @param {number} min
 * @param {number} max
 * @param {DateField['step']} step
 * @returns {DateField}
 */
function field(key, label, name, min, max, step) {
	const range = Object.freeze({ min, max, decimals: 0 });
	return Object.freeze({ key, label, name, range: () => range, step });
}

/** D, M, Y, then H, M, S, as the editor lays them out (`ValNum.VDay` …) */
export const DATE_FIELDS = Object.freeze([
	Object.freeze({
		key: /** @type {const} */ ('day'), label: 'D', name: 'Day',
		// the month and year as typed; a month or year out of their ranges allows 31
		range: (/** @type {Readonly<DateParts>} */ { year, month }) => Object.freeze({
			min: 1, max: month >= 1 && month <= 12 && year >= 1 ? daysInMonth(year, month) : 31, decimals: 0
		}),
		step: addDays
	}),
	field('month', 'M', 'Month', 1, 12, addMonths),
	field('year', 'Y', 'Year', 1, 9999, addYears),
	field('hour', 'H', 'Hour', 0, 23, addHours),
	field('minute', 'M', 'Minute', 0, 59, addMinutes),
	field('second', 'S', 'Second', 0, 59, addSeconds)
]);

/** the JD field: the date range's JDs with 5 decimals */
export const JD_RANGE = Object.freeze({ min: MINIMUM_JD, max: MAXIMUM_JD, decimals: 5 });

/**
 * the button's text: the UTC instant as `dd.MM.yyyy. HH:mm:ss`
 * @param {number} instant UTC milliseconds
 * @returns {string}
 */
export function buttonText(instant) {
	return formatDate(instantToParts(instant), DATE_FULL);
}

/**
 * the same instant in local time, as the tooltip and the editor's Local Time show it
 * @param {number} instant UTC milliseconds
 * @param {(instant: number) => DateParts} local localPartsIn(timeZone)
 * @returns {string}
 */
export function localText(instant, local) {
	return formatDate(local(instant), DATE_FULL);
}

/**
 * the editor's field texts for an instant: integers without padding, and the JD as `0.0####`
 * (`FormDateTime.PopulateData`)
 * @param {number} instant UTC milliseconds
 * @returns {Readonly<Record<keyof DateParts, string> & { jd: string }>}
 */
export function fieldTexts(instant) {
	const parts = instantToParts(instant);
	return Object.freeze({
		day: String(parts.day), month: String(parts.month), year: String(parts.year),
		hour: String(parts.hour), minute: String(parts.minute), second: String(parts.second),
		jd: formatNumber(instantToJd(instant), '0.0####')
	});
}

/**
 * the parts the field texts hold, as numbers; a field without a number reads as 0, as `Int()`
 * @param {Readonly<Record<keyof DateParts, string>>} texts
 * @returns {DateParts}
 */
export function partsOf(texts) {
	/** @param {string} text */
	const number = text => numberOf(text) ?? 0;
	return {
		year: number(texts.year), month: number(texts.month), day: number(texts.day),
		hour: number(texts.hour), minute: number(texts.minute), second: number(texts.second)
	};
}

/**
 * the instant the fields make, or null while one is empty or out of its range, where the desktop
 * cannot build a DateTime (`FormDateTime.CollectData`)
 * @param {Readonly<Record<keyof DateParts, string>>} texts
 * @returns {number | null}
 */
export function collectFields(texts) {
	const parts = partsOf(texts);
	for (const { key, range } of DATE_FIELDS) {
		const { min, max } = range(parts);
		if (numberOf(texts[key]) === null || parts[key] < min || parts[key] > max) return null;
	}
	return partsToInstant(parts);
}

/**
 * the date a JD field's text sets (`JDToDateTime`); null without a number in the range
 * @param {string} text
 * @returns {number | null}
 */
export function jdFieldDate(text) {
	const jd = numberOf(text);
	if (jd === null || jd < JD_RANGE.min || jd > JD_RANGE.max) return null;
	return jdToInstant(jd);
}

/**
 * ↑ (+1) or ↓ (−1) in a field: one unit of that field, a day for the JD; null where the result
 * leaves the years the fields hold, 1–9999
 * @param {number} instant UTC milliseconds
 * @param {keyof DateParts | 'jd'} key
 * @param {1 | -1} direction
 * @returns {number | null}
 */
export function stepDate(instant, key, direction) {
	const step = key === 'jd' ? addDays : DATE_FIELDS.find(f => f.key === key)?.step;
	if (!step) throw new Error(`no date field ${key}`);
	const next = step(instant, direction);
	const { year } = instantToParts(next);
	return year >= 1 && year <= 9999 ? next : null;
}

/**
 * what OK keeps: the date clamped to [1700-01-01, 2300-01-01] UTC (`FormDateTime.RangeDateTime`)
 * @param {number} instant UTC milliseconds
 * @returns {number}
 */
export function okDate(instant) {
	return clampToRange(instant);
}

/**
 * a preset: a label and the date it sets, or a separator
 * @typedef {{ separator: true } | { key: string, label: string, value: number }} DatePreset
 */

/**
 * @typedef {object} PresetContext
 * @property {number} now UTC milliseconds
 * @property {number} value the date shown, which the Add and Subtract presets move
 * @property {number | null} defaultDate the component's default; Default is shown only with one
 * @property {number | null} perihelionDate the comet's `Tn` as an instant; shown only with one
 */

const SEPARATOR = Object.freeze({ separator: /** @type {const} */ (true) });

/**
 * the presets menu, in its order with its separators (`DateTimeMenuControl`); presets are not
 * clamped
 * @param {Readonly<PresetContext>} context
 * @returns {ReadonlyArray<DatePreset>}
 */
export function datePresets({ now, value, defaultDate, perihelionDate }) {
	/** @param {string} key @param {string} label @param {number} date */
	const preset = (key, label, date) => Object.freeze({ key, label, value: date });
	const { year } = instantToParts(now);
	/** @param {number} y */
	const firstOf = y => partsToInstant({ year: y, month: 1, day: 1, hour: 0, minute: 0, second: 0 });
	const years = [year - 1, year, year + 1, year + 2].map((y, index) => {
		const date = firstOf(y);
		return preset(['lastYear', 'thisYear', 'nextYear', 'yearAfterNext'][index], formatDate(instantToParts(date), DATE_PERIHELION), date);
	});
	return Object.freeze([
		...(defaultDate === null ? [] : [preset('default', 'Default', defaultDate), SEPARATOR]),
		preset('today', 'Today', startOfDay(now)),
		SEPARATOR,
		...(perihelionDate === null ? [] : [preset('perihelion', 'Perihelion Date', perihelionDate), SEPARATOR]),
		...years,
		SEPARATOR,
		preset('add3Months', 'Add 3 months', addMonths(value, 3)),
		preset('add6Months', 'Add 6 months', addMonths(value, 6)),
		preset('add1Year', 'Add 1 year', addYears(value, 1)),
		SEPARATOR,
		preset('subtract3Months', 'Subtract 3 months', addMonths(value, -3)),
		preset('subtract6Months', 'Subtract 6 months', addMonths(value, -6)),
		preset('subtract1Year', 'Subtract 1 year', addYears(value, -1))
	]);
}
