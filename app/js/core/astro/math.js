// The engine's constants and degree helpers. docs/spec/04-astronomy.md, Constants and helpers.

/** the JD of 1999 Dec 31.0 (2000 Jan 0.0); the day count is d = JD − D0 */
export const D0 = 2451543.5;
/** the JD of J2000.0 */
export const J2000 = 2451545.0;
/** the Gaussian gravitational constant, radians per day */
export const GAUSS_K = 0.01720209895;
/** the convergence threshold of every solver */
export const EPS = 1e-10;

const DEG2RAD = Math.PI / 180;
const RAD2DEG = 180 / Math.PI;

/** @param {number} x degrees @returns {number} x in [0, 360) */
export function rev(x) {
	return x - Math.floor(x / 360) * 360;
}

/** @param {number} x degrees @returns {number} x in [−180, 180) */
export function rev2(x) {
	const a = rev(x);
	return a >= 180 ? a - 360 : a;
}

/** @param {number} x degrees @returns {number} */
export function sind(x) {
	return Math.sin(x * DEG2RAD);
}

/** @param {number} x degrees @returns {number} */
export function cosd(x) {
	return Math.cos(x * DEG2RAD);
}

/** @param {number} x degrees @returns {number} */
export function tand(x) {
	return Math.tan(x * DEG2RAD);
}

/** @param {number} x @returns {number} degrees */
export function asind(x) {
	return RAD2DEG * Math.asin(x);
}

/** @param {number} x @returns {number} degrees */
export function acosd(x) {
	return RAD2DEG * Math.acos(x);
}

/** @param {number} y @param {number} x @returns {number} degrees */
export function atan2d(y, x) {
	return RAD2DEG * Math.atan2(y, x);
}

/**
 * the desktop's log10, ln(x) times its 0.43429448190325182765… (Math.LOG10E as a float64);
 * Math.log10 may differ in the last bit
 * @param {number} x
 * @returns {number}
 */
export function log10(x) {
	return Math.LOG10E * Math.log(x);
}

/** the obliquity of the ecliptic at day count d @param {number} d @returns {number} degrees */
export function obliquity(d) {
	return 23.439291111 - 3.563e-7 * d;
}

/** radians to degrees @param {number} x @returns {number} */
export function degrees(x) {
	return x * RAD2DEG;
}
