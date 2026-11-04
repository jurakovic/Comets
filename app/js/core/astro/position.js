// Heliocentric comet positions and the geocentric Sun, ecliptic and equinox of date.
// docs/spec/04-astronomy.md, Comet position and Sun position.
import { orbitalPlane } from './kepler.js';
import { atan2d, cosd, D0, degrees, rev, sind } from './math.js';

/**
 * rectangular ecliptic-of-date coordinates, AU, and the distance from the origin
 * @typedef {{ x: number, y: number, z: number, r: number }} Xyz
 */

/**
 * the orbital elements a position needs (a Comet has them)
 * @typedef {object} Elements
 * @property {number} T perihelion JD
 * @property {number} q perihelion distance, AU
 * @property {number} e eccentricity
 * @property {number} w argument of perihelion, degrees (J2000)
 * @property {number} N longitude of the ascending node, degrees (J2000)
 * @property {number} i inclination, degrees (J2000)
 */

/** the J2000 node's precession to the equinox of date, degrees per day */
const NODE_PRECESSION = 3.82394e-5;

/**
 * the heliocentric comet (`CometXyz`); only the node is precessed, as on the desktop
 * @param {Elements} elements
 * @param {number} jd
 * @returns {Xyz}
 */
export function cometXyz({ T, q, e, w, N, i }, jd) {
	const { v, r } = orbitalPlane(q, e, jd - T);
	const u = degrees(v) + w;
	const node = N + NODE_PRECESSION * (jd - D0);
	return {
		x: r * (cosd(node) * cosd(u) - sind(node) * sind(u) * cosd(i)),
		y: r * (sind(node) * cosd(u) + cosd(node) * sind(u) * cosd(i)),
		z: r * (sind(u) * sind(i)),
		r
	};
}

/**
 * the geocentric Sun (`SunXyz`): Schlyter's Sun with a one-step eccentric anomaly
 * @param {number} jd
 * @returns {Xyz & { lon: number }} with the Sun's true longitude, degrees
 */
export function sunXyz(jd) {
	const d = jd - D0;
	const w = 282.9404 + 4.70935e-5 * d;
	const e = 0.016709 - 1.151e-9 * d;
	const M = rev(356.0470 + 0.9856002585 * d);
	const E = M + e * degrees(1) * sind(M) * (1 + e * cosd(M));
	const xv = cosd(E) - e;
	const yv = Math.sqrt(1 - e * e) * sind(E);
	const v = atan2d(yv, xv);
	const r = Math.sqrt(xv * xv + yv * yv);
	const lon = rev(v + w);
	return { x: r * cosd(lon), y: r * sind(lon), z: 0, r, lon };
}

/**
 * the heliocentric Earth: the Sun's position turned round
 * @param {number} jd
 * @returns {Xyz}
 */
export function earthXyz(jd) {
	const sun = sunXyz(jd);
	return { x: -sun.x, y: -sun.y, z: 0, r: sun.r };
}
