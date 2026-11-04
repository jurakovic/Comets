// Julian Dates: from calendar dates and instants, and back to instants.
// docs/spec/04-astronomy.md, Time.
import { instantToParts, partsToInstant } from './calendar.js';

/**
 * the JD of a calendar date (`JD0`), always with the Gregorian formula, also before 1582
 * @param {number} year astronomical
 * @param {number} month 1–12
 * @param {number} day
 * @param {number} h4 the fraction of the day in units of 1e-4 day (the `Th` field), 0–9999
 * @returns {number}
 */
export function jd0(year, month, day, h4) {
	let y = year;
	let m = month;
	const d = day + h4 / 10_000;
	if (m < 3) { m += 12; y -= 1; }
	const a = Math.floor(y / 100);
	const b = 2 - a + Math.floor(a / 4);
	return Math.floor(365.25 * (y + 4716)) + Math.floor(30.6001 * (m + 1)) + d + b - 1524.5;
}

/**
 * the JD of a UTC instant, its milliseconds dropped (`JD`)
 * @param {number} instant UTC milliseconds
 * @returns {number}
 */
export function instantToJd(instant) {
	const { year, month, day, hour, minute, second } = instantToParts(instant);
	return jd0(year, month, day, 0) + (hour + minute / 60 + second / 3600) / 24;
}

/**
 * the desktop's raw date parts of a JD, before they become a date: the Julian calendar before
 * 1582-10-15 (so 29 February 1500 can occur), astronomical years ≤ 0, and hour 24 in the last
 * 0.4 ms of a day, where the nudge below carries the hour over; the desktop throws for the last
 * three of these (spec 10, D5)
 * @typedef {import('./calendar.js').DateParts} JdParts
 */

/**
 * the raw date parts of a JD (`JDToDateTime` up to building its DateTime); jdToInstant makes a
 * date of them, and instantToParts of that gives proper date parts
 * @param {number} jd
 * @returns {JdParts} whole seconds
 */
export function jdToParts(jd) {
	const jdHalf = jd + 0.5;
	let a = Math.floor(jdHalf);
	if (a >= 2299160.5) {
		const alpha = Math.floor((a - 1867216.25) / 36524.25);
		a += alpha - Math.floor(alpha / 4) + 1;
	}
	const b = Math.floor(a) + 1524;
	const c = Math.floor((b - 122.1) / 365.25);
	const k = Math.floor(365.25 * c);
	const e = Math.floor((b - k) / 30.6001);
	const day0 = b - k - Math.floor(30.6001 * e) + (jdHalf - Math.floor(jdHalf));
	const month = Math.floor(e - (e >= 13.5 ? 13 : 1) + 0.5);
	const year = Math.floor(c - (month > 2 ? 4716 : 4715) + 0.5);
	const day = Math.floor(day0);
	// the nudge keeps 23:59:59.9999 from losing a second, and absorbs float64 error
	const hour0 = (day0 - day) * 24 + 0.0000001;
	const hour = Math.floor(hour0);
	const minute0 = (hour0 - hour) * 60;
	const minute = Math.floor(minute0);
	const second = Math.trunc((minute0 - minute) * 60);
	return { year, month, day, hour, minute, second };
}

/**
 * the UTC instant of a JD (`JDToDateTime`). Before 1582-10-15 the Julian date parts are taken as
 * Gregorian ones, as the desktop does (spec 10, Q8). Where the desktop throws (spec 10, D5), a
 * Julian 29 February that the Gregorian calendar lacks becomes 1 March, and hour 24 the next
 * day's 00:00:00.
 * @param {number} jd
 * @returns {number} UTC milliseconds, whole seconds
 */
export function jdToInstant(jd) {
	return partsToInstant(jdToParts(jd));
}
