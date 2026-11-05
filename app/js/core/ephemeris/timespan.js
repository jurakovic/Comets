// The timespan of an ephemeris or a graph: the default dates, the validation, and the steps from
// Start to End. docs/spec/06-ephemeris.md, Settings and Calculation.
import { addMonths, civilFromDays, DAY_MS, daysFromCivil } from '../time/calendar.js';

/** a timespan must be shorter than this many days */
export const MAXIMUM_SPAN_DAYS = 300 * 365.25;

/** 00:00 UTC on the first day of the instant's month */
const FIRST_OF_MONTH = (/** @type {number} */ instant) => {
	const { year, month } = civilFromDays(Math.floor(instant / DAY_MS));
	return daysFromCivil(year, month, 1) * DAY_MS;
};

/**
 * the default Start and End, at 00:00 UTC: the first day of last month and the first day of the
 * month five months ahead (`CommonManager.DefaultDateStart`, `DefaultDateEnd`)
 * @param {number} now UTC milliseconds
 * @returns {Readonly<{ start: number, end: number }>}
 */
export function defaultTimespan(now) {
	return Object.freeze({ start: FIRST_OF_MONTH(addMonths(now, -1)), end: FIRST_OF_MONTH(addMonths(now, 5)) });
}

/**
 * End after Start, and less than 300 years after it; both errors point at Start, as on the desktop
 * (`TimespanControl.ValidateData`)
 * @param {number} start UTC milliseconds
 * @param {number} end UTC milliseconds
 * @returns {ReadonlyArray<import('../validation.js').ValidationError>} empty when valid
 */
export function timespanErrors(start, end) {
	// "greater" where the desktop spells "greather" (spec 10, D15)
	if (end <= start) return [{ field: 'start', message: 'End date must be greater than start date' }];
	if ((end - start) / DAY_MS >= MAXIMUM_SPAN_DAYS) return [{ field: 'start', message: 'Timespan must be less than 300 years' }];
	return [];
}

/**
 * the number of whole steps from Start to End, in exact integer arithmetic, so that a step lying
 * on End is always counted (spec 10, D8)
 * @param {number} start UTC milliseconds, whole seconds
 * @param {number} end UTC milliseconds, whole seconds
 * @param {number} step milliseconds, a whole number of seconds and more than 0
 * @returns {number} 0 when End is before Start
 */
export function wholeSteps(start, end, step) {
	const span = end - start;
	return span < 0 ? 0 : (span - (span % step)) / step;
}
