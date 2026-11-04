// Comet orbit lines for the 3D view: the desktop's curvature-adaptive sampler in the orbital
// plane, and the rotations that place the samples in the scene.
// docs/spec/04-astronomy.md, 3D scene geometry and Comet orbit sampling.
import { cosd, D0, sind } from './math.js';

/** the base number of steps over a whole orbit or arc */
const DIV = 500;
/** how far out an open orbit is drawn, AU */
const RMAX = 500;
/** how close to 1 an eccentricity is taken as a parabola */
const PARABOLIC = 1e-16;

/**
 * the next step: the base step divided by the curvature, which is held within [1, kMax]
 * @param {number} base
 * @param {number} curvature
 * @param {number} kMax
 */
function step(base, curvature, kMax) {
	return base / Math.max(Math.min(curvature, kMax), 1);
}

/**
 * @param {number} q
 * @param {number} e
 * @param {number[]} points x, y in turn
 */
function ellipse(q, e, points) {
	const a = q / (1 - e);
	const w = 1 - e * e;
	const sw = Math.sqrt(w);
	const kMax = Math.min(50, Math.max(20, Math.pow(1 / w, 2 / 3)));
	const curvature = (/** @type {number} */ sinE, /** @type {number} */ cosE) => sw / Math.pow(sinE * sinE + w * cosE * cosE, 1.5);

	if (a * (1 + e) > RMAX) {
		// an open arc around perihelion
		const Emax = Math.acos(Math.max(-1, Math.min(1, (1 - RMAX / a) / e)));
		const dE = 2 * Emax / DIV;
		for (let E = -Emax; E < Emax;) {
			const sinE = Math.sin(E);
			const cosE = Math.cos(E);
			points.push(a * (cosE - e), a * sw * sinE);
			E += step(dE, curvature(sinE, cosE), kMax);
		}
		points.push(a * (Math.cos(Emax) - e), a * sw * Math.sin(Emax));
		return;
	}

	// a closed orbit as two legs out from perihelion, so each starts where the curvature is
	// highest and the steps are finest; the inbound leg is emitted reversed
	const dE = 2 * Math.PI / DIV;
	const inbound = [];
	for (let E = 0; E > -Math.PI;) {
		E -= step(dE, curvature(Math.sin(E), Math.cos(E)), kMax);
		if (E < -Math.PI) E = -Math.PI;
		inbound.push(E);
	}
	for (let n = inbound.length - 1; n >= 0; n--) {
		points.push(a * (Math.cos(inbound[n]) - e), a * sw * Math.sin(inbound[n]));
	}
	for (let E = 0; E < Math.PI;) {
		const sinE = Math.sin(E);
		const cosE = Math.cos(E);
		points.push(a * (cosE - e), a * sw * sinE);
		E += step(dE, curvature(sinE, cosE), kMax);
	}
	// aphelion closes the loop
	points.push(a * (-1 - e), 0);
}

/**
 * @param {number} q
 * @param {number} e
 * @param {number[]} points
 */
function hyperbola(q, e, points) {
	const a = q / (e - 1);
	const b = Math.sqrt(e * e - 1);
	const kMax = Math.min(50, Math.max(20, Math.pow(1 / b, 2 / 3)));
	const c = (RMAX / a + 1) / e;
	const m = Math.min(c, 1e6);
	const Fmax = c > 1 ? Math.log(m + Math.sqrt(m * m - 1)) : 0;
	const dF = 2 * Fmax / DIV;
	for (let F = -Fmax; F < Fmax;) {
		const sinhF = Math.sinh(F);
		const coshF = Math.cosh(F);
		points.push(a * (e - coshF), a * b * sinhF);
		F += step(dF, b / Math.pow(sinhF * sinhF + b * b * coshF * coshF, 1.5), kMax);
	}
	points.push(a * (e - Math.cosh(Fmax)), a * b * Math.sinh(Fmax));
}

/**
 * @param {number} q
 * @param {number[]} points
 */
function parabola(q, points) {
	const Dmax = Math.sqrt(Math.max(RMAX / q - 1, 0));
	const kMax = Math.min(50, Math.max(20, Math.pow(Dmax, 4 / 3)));
	const dD = 2 * Dmax / DIV;
	for (let D = -Dmax; D < Dmax;) {
		points.push(q * (1 - D * D), 2 * q * D);
		D += step(dD, kMax / Math.pow(1 + D * D, 0.75), kMax);
	}
	points.push(q * (1 - Dmax * Dmax), 2 * q * Dmax);
}

/**
 * the orbit sampled in its plane (`CometOrbit`): x towards perihelion, y along the motion there;
 * a closed loop for an ellipse within 500 AU, otherwise the arc within 500 AU of the Sun
 * @param {number} q perihelion distance, AU
 * @param {number} e eccentricity
 * @returns {Float64Array} x, y of each point in turn, AU
 */
export function sampleOrbit(q, e) {
	/** @type {number[]} */
	const points = [];
	if (e < 1 - PARABOLIC) ellipse(q, e, points);
	else if (e > 1 + PARABOLIC) hyperbola(q, e, points);
	else parabola(q, points);
	return Float64Array.from(points);
}

/**
 * a 3×3 rotation, row by row
 * @typedef {readonly [number, number, number, number, number, number, number, number, number]} Matrix3
 */

/**
 * the rotation from the orbital plane to the J2000 ecliptic, Rz(N)·Rx(i)·Rz(w)
 * @param {{ w: number, N: number, i: number }} elements degrees
 * @returns {Matrix3}
 */
export function orbitRotation({ w, N, i }) {
	const cN = cosd(N), sN = sind(N), ci = cosd(i), si = sind(i), cw = cosd(w), sw = sind(w);
	return [
		cN * cw - sN * ci * sw, -cN * sw - sN * ci * cw, sN * si,
		sN * cw + cN * ci * sw, -sN * sw + cN * ci * cw, -cN * si,
		si * sw, si * cw, ci
	];
}

/**
 * the turn about the ecliptic pole from J2000 to the equinox of date, Rz(3.82394E-5·d): the
 * node's precession, the only one the engine applies
 * @param {number} jd
 * @returns {Matrix3}
 */
export function dateRotation(jd) {
	const angle = 3.82394e-5 * (jd - D0);
	const c = cosd(angle), s = sind(angle);
	return [c, -s, 0, s, c, 0, 0, 0, 1];
}

/**
 * a comet's orbit line in the J2000 ecliptic, for a line buffer that a date change leaves alone
 * @param {{ q: number, e: number, w: number, N: number, i: number }} elements
 * @returns {Float64Array} x, y, z of each point in turn, AU
 */
export function cometOrbitLine(elements) {
	const plane = sampleOrbit(elements.q, elements.e);
	const [m0, m1, , m3, m4, , m6, m7] = orbitRotation(elements);
	const line = new Float64Array(plane.length / 2 * 3);
	for (let k = 0, j = 0; k < plane.length; k += 2, j += 3) {
		const x = plane[k], y = plane[k + 1];
		line[j] = m0 * x + m1 * y;
		line[j + 1] = m3 * x + m4 * y;
		line[j + 2] = m6 * x + m7 * y;
	}
	return line;
}
