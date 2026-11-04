// Kepler's equation for ellipses, hyperbolas and parabolas: true anomaly and distance.
// docs/spec/04-astronomy.md, Orbital plane.
import { EPS, GAUSS_K } from './math.js';

/** the desktop's hyperbolic and parabolic loops have no limit (spec 10, D7) */
const MAX_ITERATIONS = 100;

/**
 * @typedef {object} PlanePosition
 * @property {number} v true anomaly, radians
 * @property {number} r distance from the Sun, AU
 */

/**
 * the eccentric anomaly of an ellipse by Laguerre–Conway iteration, as in Stellarium; the comets
 * and the planets share it
 * @param {number} meanAnomaly radians, any value
 * @param {number} e eccentricity, below 1
 * @returns {number} radians
 */
export function eccentricAnomaly(meanAnomaly, e) {
	let M = meanAnomaly % (2 * Math.PI);
	if (M < 0) M += 2 * Math.PI;
	let E = M + 0.85 * e * Math.sign(Math.sin(M));
	for (let escape = 0; ;) {
		const previous = E;
		const f2 = e * Math.sin(E);
		const f = E - f2 - M;
		const f1 = 1 - e * Math.cos(E);
		E += (-5 * f) / (f1 + Math.sign(f1) * Math.sqrt(Math.abs(16 * f1 * f1 - 20 * f * f2)));
		if (Math.abs(E - previous) < EPS) break;
		if (++escape > 10) break;
	}
	return E;
}

/**
 * an ellipse (e < 1)
 * @param {number} q perihelion distance, AU
 * @param {number} e eccentricity
 * @param {number} dt days since perihelion
 * @returns {PlanePosition}
 */
export function ellipse(q, e, dt) {
	const a = q / (1 - e);
	const E = eccentricAnomaly(GAUSS_K * dt / (Math.sqrt(a) * a), e);
	const x = a * (Math.cos(E) - e);
	const y = q * Math.sqrt((1 + e) / (1 - e)) * Math.sin(E);
	return { v: Math.atan2(y, x), r: Math.sqrt(x * x + y * y) };
}

/**
 * a hyperbola (e > 1), as in Stellarium
 * @param {number} q perihelion distance, AU
 * @param {number} e eccentricity
 * @param {number} dt days since perihelion
 * @returns {PlanePosition}
 */
export function hyperbola(q, e, dt) {
	const a = q / (e - 1);
	const M = dt * (GAUSS_K / Math.pow(q / (e - 1), 1.5));
	let E = Math.sign(M) * Math.log(2 * Math.abs(M) / e + 1.85);
	for (let n = 0; n < MAX_ITERATIONS; n++) {
		const previous = E;
		const f2 = e * Math.sinh(E);
		const f = f2 - E - M;
		const f1 = e * Math.cosh(E) - 1;
		E += (-5 * f) / (f1 + Math.sign(f1) * Math.sqrt(Math.abs(16 * f1 * f1 - 20 * f * f2)));
		if (Math.abs(E - previous) < EPS) break;
	}
	const v = 2 * Math.atan(Math.sqrt((e + 1) / (e - 1)) * Math.tanh(E / 2));
	return { v, r: a * (e * e - 1) / (1 + e * Math.cos(v)) };
}

/**
 * a parabola (e = 1), as in Cartes du Ciel
 * @param {number} q perihelion distance, AU
 * @param {number} dt days since perihelion
 * @returns {PlanePosition}
 */
export function parabola(q, dt) {
	const W = 3.649116245e-2 * dt / (q * Math.sqrt(q));
	let s = 0;
	for (let n = 0; n < MAX_ITERATIONS; n++) {
		const previous = s;
		s = (2 * previous * previous * previous + W) / (3 * (previous * previous + 1));
		if (Math.abs(s - previous) < EPS) break;
	}
	return { v: 2 * Math.atan(s), r: q * (1 + s * s) };
}

/**
 * the position in the orbital plane, the branch chosen by an exact comparison of e with 1
 * @param {number} q perihelion distance, AU
 * @param {number} e eccentricity
 * @param {number} dt days since perihelion
 * @returns {PlanePosition}
 */
export function orbitalPlane(q, e, dt) {
	if (e < 1) return ellipse(q, e, dt);
	if (e > 1) return hyperbola(q, e, dt);
	return parabola(q, dt);
}
