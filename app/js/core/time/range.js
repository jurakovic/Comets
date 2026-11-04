// The range of dates a user can pick, 1700-01-01 00:00 to 2300-01-01 00:00 UTC.
// docs/spec/04-astronomy.md, Constants and helpers.
import { partsToInstant } from './calendar.js';
import { instantToJd } from './julian.js';

/** 1700-01-01 00:00 UTC, in UTC milliseconds */
export const MINIMUM_DATE = partsToInstant({ year: 1700, month: 1, day: 1, hour: 0, minute: 0, second: 0 });

/** 2300-01-01 00:00 UTC, in UTC milliseconds */
export const MAXIMUM_DATE = partsToInstant({ year: 2300, month: 1, day: 1, hour: 0, minute: 0, second: 0 });

/** the JD of MINIMUM_DATE */
export const MINIMUM_JD = instantToJd(MINIMUM_DATE);

/** the JD of MAXIMUM_DATE */
export const MAXIMUM_JD = instantToJd(MAXIMUM_DATE);

/**
 * @param {number} instant UTC milliseconds
 * @returns {boolean}
 */
export function isInRange(instant) {
	return instant >= MINIMUM_DATE && instant <= MAXIMUM_DATE;
}

/**
 * the instant moved to the nearest end of the range when it lies outside
 * @param {number} instant UTC milliseconds
 * @returns {number}
 */
export function clampToRange(instant) {
	return Math.min(Math.max(instant, MINIMUM_DATE), MAXIMUM_DATE);
}
