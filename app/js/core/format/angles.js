// Angles and hours as the ephemeris text and the database view print them (HMS, Angle).
// docs/spec/04-astronomy.md, Angle and time text.
import { roundEven } from '../../lib/math.js';

/**
 * an integer with at least two digits
 * @param {number} n
 */
function pad2(n) {
	return String(n).padStart(2, '0');
}

/**
 * hours as `05h07m24`, seconds rounded half to even; the sign is dropped (spec 10, Q19)
 * @param {number} t hours (RA / 15)
 * @returns {string}
 */
export function hms(t) {
	let hours = Math.abs(t);
	let minutes = 60 * (hours - Math.floor(hours));
	hours = Math.floor(hours);
	let seconds = roundEven(60 * (minutes - Math.floor(minutes)));
	minutes = Math.floor(minutes);
	if (seconds >= 60) { minutes += 1; seconds -= 60; }
	if (minutes >= 60) { hours += 1; minutes -= 60; }
	if (hours >= 24) hours -= 24;
	return `${pad2(hours)}h${pad2(minutes)}m${pad2(seconds)}`;
}

/**
 * degrees as `+45°30'` or `+45:30`, or `045°30'` for a full circle, arcminutes rounded half to even
 * @param {number} a degrees
 * @param {boolean} circle 0–360 with three digits and no sign, rather than ±90 with a sign
 * @param {boolean} arcmin `°` and `'` rather than `:`
 * @returns {string}
 */
export function angle(a, circle, arcmin) {
	const rounded = roundEven(a * 60) / 60;
	let degrees = Math.abs(rounded);
	let minutes = roundEven(60 * (degrees - Math.floor(degrees)));
	if (minutes >= 60) { degrees += 1; minutes = 0; }
	const whole = Math.floor(degrees);
	const lead = circle ? (whole < 100 ? '0' : '') : (rounded < 0 ? '-' : '+');
	return lead + pad2(whole) + (arcmin ? `°${pad2(minutes)}'` : `:${pad2(minutes)}`);
}
