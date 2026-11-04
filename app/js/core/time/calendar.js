// Instants and calendar date parts for any year, and the calendar arithmetic of .NET's DateTime.
// docs/spec/04-astronomy.md, Time; docs/spec/09-settings-and-persistence.md, Date picker.

/** milliseconds in a day */
export const DAY_MS = 86_400_000;

/**
 * a date and time in the proleptic Gregorian calendar, UTC unless a caller says otherwise;
 * years ≤ 0 are astronomical (0 is 1 BC)
 * @typedef {object} DateParts
 * @property {number} year
 * @property {number} month 1–12
 * @property {number} day 1–31
 * @property {number} hour 0–23
 * @property {number} minute 0–59
 * @property {number} second 0–59
 */

/**
 * the days from 1970-01-01 to a calendar date; days past the month's end count on into the next
 * (H. Hinnant, "chrono-compatible low-level date algorithms", days_from_civil)
 * @param {number} year
 * @param {number} month 1–12
 * @param {number} day
 * @returns {number}
 */
export function daysFromCivil(year, month, day) {
	const y = month <= 2 ? year - 1 : year;
	const era = Math.floor(y / 400);
	const yearOfEra = y - era * 400;
	const monthFromMarch = month > 2 ? month - 3 : month + 9;
	const dayOfYear = Math.floor((153 * monthFromMarch + 2) / 5) + day - 1;
	const dayOfEra = yearOfEra * 365 + Math.floor(yearOfEra / 4) - Math.floor(yearOfEra / 100) + dayOfYear;
	return era * 146_097 + dayOfEra - 719_468;
}

/**
 * the calendar date of a day count from 1970-01-01 (civil_from_days, as above)
 * @param {number} days a whole number
 * @returns {{ year: number, month: number, day: number }}
 */
export function civilFromDays(days) {
	const z = days + 719_468;
	const era = Math.floor(z / 146_097);
	const dayOfEra = z - era * 146_097;
	const yearOfEra = Math.floor((dayOfEra - Math.floor(dayOfEra / 1460) + Math.floor(dayOfEra / 36_524)
		- Math.floor(dayOfEra / 146_096)) / 365);
	const dayOfYear = dayOfEra - (365 * yearOfEra + Math.floor(yearOfEra / 4) - Math.floor(yearOfEra / 100));
	const monthFromMarch = Math.floor((5 * dayOfYear + 2) / 153);
	const day = dayOfYear - Math.floor((153 * monthFromMarch + 2) / 5) + 1;
	const month = monthFromMarch < 10 ? monthFromMarch + 3 : monthFromMarch - 9;
	return { year: yearOfEra + era * 400 + (month <= 2 ? 1 : 0), month, day };
}

/**
 * @param {number} year
 * @returns {boolean}
 */
export function isLeapYear(year) {
	return (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;
}

/**
 * @param {number} year
 * @param {number} month 1–12
 * @returns {number}
 */
export function daysInMonth(year, month) {
	if (month === 2) return isLeapYear(year) ? 29 : 28;
	return month === 4 || month === 6 || month === 9 || month === 11 ? 30 : 31;
}

/**
 * the UTC instant of date parts; values past their field's end carry into the next field
 * @param {DateParts} parts
 * @returns {number} UTC milliseconds
 */
export function partsToInstant({ year, month, day, hour, minute, second }) {
	return daysFromCivil(year, month, day) * DAY_MS + ((hour * 60 + minute) * 60 + second) * 1000;
}

/**
 * the date parts of a UTC instant; milliseconds are dropped (towards the earlier second)
 * @param {number} instant UTC milliseconds
 * @returns {DateParts}
 */
export function instantToParts(instant) {
	const days = Math.floor(instant / DAY_MS);
	const seconds = Math.floor((instant - days * DAY_MS) / 1000);
	return {
		...civilFromDays(days),
		hour: Math.floor(seconds / 3600),
		minute: Math.floor(seconds / 60) % 60,
		second: seconds % 60
	};
}

/**
 * the instant whole months later or earlier, keeping the time of day and clamping the day to the
 * target month's length (.NET AddMonths: Jan 31 + 1 month → Feb 28 or 29)
 * @param {number} instant UTC milliseconds
 * @param {number} months a whole number, negative to go back
 * @returns {number}
 */
export function addMonths(instant, months) {
	const days = Math.floor(instant / DAY_MS);
	const timeOfDay = instant - days * DAY_MS;
	const { year, month, day } = civilFromDays(days);
	const index = year * 12 + (month - 1) + months;
	const targetYear = Math.floor(index / 12);
	const targetMonth = index - targetYear * 12 + 1;
	const targetDay = Math.min(day, daysInMonth(targetYear, targetMonth));
	return daysFromCivil(targetYear, targetMonth, targetDay) * DAY_MS + timeOfDay;
}

/**
 * whole years later or earlier; 29 February becomes the 28th in a common year (.NET AddYears)
 * @param {number} instant UTC milliseconds
 * @param {number} years a whole number
 * @returns {number}
 */
export function addYears(instant, years) {
	return addMonths(instant, years * 12);
}

/**
 * @param {number} instant UTC milliseconds
 * @param {number} days whole days
 * @returns {number}
 */
export function addDays(instant, days) {
	return instant + days * DAY_MS;
}

/**
 * @param {number} instant UTC milliseconds
 * @param {number} hours whole hours
 * @returns {number}
 */
export function addHours(instant, hours) {
	return instant + hours * 3_600_000;
}

/**
 * @param {number} instant UTC milliseconds
 * @param {number} minutes whole minutes
 * @returns {number}
 */
export function addMinutes(instant, minutes) {
	return instant + minutes * 60_000;
}

/**
 * @param {number} instant UTC milliseconds
 * @param {number} seconds whole seconds
 * @returns {number}
 */
export function addSeconds(instant, seconds) {
	return instant + seconds * 1000;
}

/**
 * 00:00 UTC of the instant's day
 * @param {number} instant UTC milliseconds
 * @returns {number}
 */
export function startOfDay(instant) {
	return Math.floor(instant / DAY_MS) * DAY_MS;
}
