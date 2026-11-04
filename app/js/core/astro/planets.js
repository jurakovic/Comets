// The planets for the orbit viewer: Schlyter's mean elements, the perturbations of Jupiter,
// Saturn and Uranus, positions and orbit lines. docs/spec/04-astronomy.md, Planets, and
// 3D scene geometry.
import { eccentricAnomaly } from './kepler.js';
import { atan2d, cosd, D0, sind } from './math.js';
import { earthXyz } from './position.js';

/**
 * @typedef {'Mercury' | 'Venus' | 'Earth' | 'Mars' | 'Jupiter' | 'Saturn' | 'Uranus' | 'Neptune'} Planet
 */

/**
 * mean elements of date: N, i, w and M in degrees, a in AU
 * @typedef {{ N: number, i: number, w: number, a: number, e: number, M: number }} PlanetElements
 */

/** the planets from the Sun outwards */
export const PLANETS = /** @type {readonly Planet[]} */ (Object.freeze(['Mercury', 'Venus', 'Earth', 'Mars', 'Jupiter', 'Saturn', 'Uranus', 'Neptune']));

/**
 * how a planet is computed: its mean elements, and, where it has them, its perturbations from the
 * mean anomalies of Jupiter, Saturn and Uranus, or a position of its own
 * @typedef {object} PlanetModel
 * @property {(d: number) => PlanetElements} elements
 * @property {(Mj: number, Ms: number, Mu: number) => { dLon: number, dLat: number }} [perturbation]
 * @property {(jd: number) => import('./position.js').Xyz} [position]
 */

/** @type {Readonly<Record<Planet, PlanetModel>>} */
const MODELS = Object.freeze({
	Mercury: { elements: d => ({ N: 48.3313 + 3.24587e-5 * d, i: 7.0047 + 5.00e-8 * d, w: 29.1241 + 1.01444e-5 * d, a: 0.387098, e: 0.205635 + 5.59e-10 * d, M: 168.6562 + 4.0923344368 * d }) },
	Venus: { elements: d => ({ N: 76.6799 + 2.46590e-5 * d, i: 3.3946 + 2.75e-8 * d, w: 54.8910 + 1.38374e-5 * d, a: 0.723330, e: 0.006773 - 1.302e-9 * d, M: 48.0052 + 1.6021302244 * d }) },
	Earth: {
		// the Sun's elements with w + 180, only for drawing Earth's orbit; the position is the Sun
		// turned round, so the view and the ephemeris share one Earth
		elements: d => ({ N: 0, i: 0, w: 282.9404 + 4.70935e-5 * d + 180, a: 1.000000, e: 0.016709 - 1.151e-9 * d, M: 356.0470 + 0.9856002585 * d }),
		position: earthXyz
	},
	Mars: { elements: d => ({ N: 49.5574 + 2.11081e-5 * d, i: 1.8497 - 1.78e-8 * d, w: 286.5016 + 2.92961e-5 * d, a: 1.523688, e: 0.093405 + 2.516e-9 * d, M: 18.6021 + 0.5240207766 * d }) },
	Jupiter: {
		elements: d => ({ N: 100.4542 + 2.76854e-5 * d, i: 1.3030 - 1.557e-7 * d, w: 273.8777 + 1.64505e-5 * d, a: 5.20256, e: 0.048498 + 4.469e-9 * d, M: 19.8950 + 0.0830853001 * d }),
		perturbation: (Mj, Ms) => ({
			dLon: -0.332 * sind(2 * Mj - 5 * Ms - 67.6) - 0.056 * sind(2 * Mj - 2 * Ms + 21) + 0.042 * sind(3 * Mj - 5 * Ms + 21)
				- 0.036 * sind(Mj - 2 * Ms) + 0.022 * cosd(Mj - Ms) + 0.023 * sind(2 * Mj - 3 * Ms + 52) - 0.016 * sind(Mj - 5 * Ms - 69),
			dLat: 0
		})
	},
	Saturn: {
		elements: d => ({ N: 113.6634 + 2.38980e-5 * d, i: 2.4886 - 1.081e-7 * d, w: 339.3939 + 2.97661e-5 * d, a: 9.55475, e: 0.055546 - 9.499e-9 * d, M: 316.9670 + 0.0334442282 * d }),
		perturbation: (Mj, Ms) => ({
			dLon: +0.812 * sind(2 * Mj - 5 * Ms - 67.6) - 0.229 * cosd(2 * Mj - 4 * Ms - 2) + 0.119 * sind(Mj - 2 * Ms - 3)
				+ 0.046 * sind(2 * Mj - 6 * Ms - 69) + 0.014 * sind(Mj - 3 * Ms + 32),
			dLat: -0.020 * cosd(2 * Mj - 4 * Ms - 2) + 0.018 * sind(2 * Mj - 6 * Ms - 49)
		})
	},
	Uranus: {
		elements: d => ({ N: 74.0005 + 1.3978e-5 * d, i: 0.7733 + 1.9e-8 * d, w: 96.6612 + 3.0565e-5 * d, a: 19.18171 - 1.55e-8 * d, e: 0.047318 + 7.45e-9 * d, M: 142.5905 + 0.011725806 * d }),
		perturbation: (Mj, Ms, Mu) => ({
			dLon: +0.040 * sind(Ms - 2 * Mu + 6) + 0.035 * sind(Ms - 3 * Mu + 33) - 0.015 * sind(Mj - Mu + 20),
			dLat: 0
		})
	},
	Neptune: { elements: d => ({ N: 131.7806 + 3.0173e-5 * d, i: 1.7700 - 2.55e-7 * d, w: 272.8461 - 6.027e-6 * d, a: 30.05826 + 3.313e-8 * d, e: 0.008606 + 2.15e-9 * d, M: 260.2471 + 0.005995147 * d }) }
});

/**
 * @param {Planet} planet
 * @param {number} d the day count, JD − D0
 * @returns {PlanetElements}
 */
export function planetElements(planet, d) {
	return MODELS[planet].elements(d);
}

/**
 * the perturbations in longitude and latitude, degrees (Schlyter, section 10); 0 for planets
 * without them
 * @param {Planet} planet
 * @param {number} d the day count
 * @returns {{ dLon: number, dLat: number }}
 */
export function perturbations(planet, d) {
	const { perturbation } = MODELS[planet];
	if (!perturbation) return { dLon: 0, dLat: 0 };
	return perturbation(MODELS.Jupiter.elements(d).M, MODELS.Saturn.elements(d).M, MODELS.Uranus.elements(d).M);
}

/**
 * a point of the orbital plane turned to the ecliptic of date by Rz(N)·Rx(i)·Rz(w)
 * @param {number} xv
 * @param {number} yv
 * @param {PlanetElements} elements
 * @returns {{ x: number, y: number, z: number }}
 */
function toEcliptic(xv, yv, { N, i, w }) {
	const u = atan2d(yv, xv) + w;
	const r = Math.sqrt(xv * xv + yv * yv);
	return {
		x: r * (cosd(N) * cosd(u) - sind(N) * sind(u) * cosd(i)),
		y: r * (sind(N) * cosd(u) + cosd(N) * sind(u) * cosd(i)),
		z: r * (sind(u) * sind(i))
	};
}

/**
 * the heliocentric planet, ecliptic of date
 * @param {Planet} planet
 * @param {number} jd
 * @returns {import('./position.js').Xyz}
 */
export function planetXyz(planet, jd) {
	const { elements: meanElements, position } = MODELS[planet];
	if (position) return position(jd);
	const d = jd - D0;
	const elements = meanElements(d);
	const { a, e } = elements;
	const E = eccentricAnomaly(elements.M * Math.PI / 180, e);
	let { x, y, z } = toEcliptic(a * (Math.cos(E) - e), a * Math.sqrt(1 - e * e) * Math.sin(E), elements);
	const { dLon, dLat } = perturbations(planet, d);
	if (dLon !== 0 || dLat !== 0) {
		const r = Math.sqrt(x * x + y * y + z * z);
		const lon = atan2d(y, x) + dLon;
		const lat = atan2d(z, Math.sqrt(x * x + y * y)) + dLat;
		x = r * cosd(lat) * cosd(lon);
		y = r * cosd(lat) * sind(lon);
		z = r * sind(lat);
	}
	return { x, y, z, r: Math.sqrt(x * x + y * y + z * z) };
}

/** points in a planet's orbit line; the first and the last coincide */
export const ORBIT_LINE_POINTS = 301;

/**
 * the planet's orbit for the date: E stepped by 360°/300 from 0 to 360, and for Jupiter, Saturn
 * and Uranus the whole line turned by Rz(Δλ) so the planet sits on it
 * @param {Planet} planet
 * @param {number} jd
 * @returns {Float64Array} x, y, z of each point in turn, AU
 */
export function planetOrbitLine(planet, jd) {
	const d = jd - D0;
	const elements = MODELS[planet].elements(d);
	const { a, e } = elements;
	const turn = perturbations(planet, d).dLon;
	const cosTurn = cosd(turn);
	const sinTurn = sind(turn);
	const line = new Float64Array(ORBIT_LINE_POINTS * 3);
	for (let k = 0; k < ORBIT_LINE_POINTS; k++) {
		const E = (k * 360 / (ORBIT_LINE_POINTS - 1)) * Math.PI / 180;
		const { x, y, z } = toEcliptic(a * (Math.cos(E) - e), a * Math.sqrt(1 - e * e) * Math.sin(E), elements);
		line[3 * k] = x * cosTurn - y * sinTurn;
		line[3 * k + 1] = x * sinTurn + y * cosTurn;
		line[3 * k + 2] = z;
	}
	return line;
}
