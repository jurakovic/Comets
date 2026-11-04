// The comet record: its factory with the derived elements and the sort key, equality, and the
// nearest perihelion. docs/spec/02-data-model.md, Comet record.
import { jd0 } from '../time/julian.js';
import { MAXIMUM_JD, MINIMUM_JD } from '../time/range.js';
import { isPeriodic, sortKey } from './designation.js';

/**
 * a comet; frozen, and the same everywhere it goes (Worker, store, tests)
 * @typedef {object} Comet
 * @property {string} full the full name, the identity key: `C/1995 O1 (Hale-Bopp)`
 * @property {string} id the designation without fragment, possibly empty (Voyager II)
 * @property {string} name the discoverer or common name, possibly empty
 * @property {string} fragment the fragment without its hyphen, usually empty
 * @property {number} Ty perihelion year
 * @property {number} Tm perihelion month
 * @property {number} Td perihelion day
 * @property {number} Th the fraction of the perihelion day in units of 1e-4 day, 0–9999
 * @property {number} T perihelion time, a JD
 * @property {number} q perihelion distance, AU
 * @property {number} e eccentricity
 * @property {number} w argument of perihelion, degrees (J2000)
 * @property {number} N longitude of the ascending node, degrees (J2000)
 * @property {number} i inclination, degrees (J2000)
 * @property {number} g absolute magnitude
 * @property {number} k slope parameter
 * @property {number} P period, years
 * @property {number} a semi-major axis, AU
 * @property {number} Q aphelion distance, AU
 * @property {number} n mean motion, degrees per day
 * @property {string} sortkey the database ordering key
 */

/**
 * what an importer read; what it does not carry defaults to 0 or ""
 * @typedef {object} CometFields
 * @property {string} [full]
 * @property {string} [id]
 * @property {string} [name]
 * @property {string} [fragment]
 * @property {number} [Ty]
 * @property {number} [Tm]
 * @property {number} [Td]
 * @property {number} [Th]
 * @property {number} [T] the perihelion JD, where a format gives it; otherwise JD0(Ty, Tm, Td, Th)
 * @property {number} [q]
 * @property {number} [e]
 * @property {number} [w]
 * @property {number} [N]
 * @property {number} [i]
 * @property {number} [g]
 * @property {number} [k]
 * @property {string} [sortkey] a fixed key, where a format sets one (Voyager II: ""); otherwise computed
 */

// stands in for e = 1, so that parabolic comets still get finite, sortable P, a and Q
const NEAR_PARABOLIC = 0.999999;

/**
 * P, a, Q and n from q and e (`CometManager`), with its hyperbolic quirks (spec 10, Q5, Q6)
 * @param {number} q
 * @param {number} e
 * @returns {{ P: number, a: number, Q: number, n: number }}
 */
export function derivedElements(q, e) {
	if (e < 1) {
		const a = q / (1 - e);
		const P = Math.pow(a, 1.5);
		return { P, a, Q: a * (1 + e), n: 0.9856076686 / P };
	}
	if (e > 1) {
		const a = -(q / (1 - e));
		return { P: Math.pow(q / (e - 1), 1.5), a, Q: a * (1 + (2 - e)), n: 0 };
	}
	const a = q / (1 - NEAR_PARABOLIC);
	return { P: Math.pow(a, 1.5), a, Q: a * (1 + NEAR_PARABOLIC), n: 0 };
}

/**
 * a comet from what an importer read (the finalize step of spec 03): T, the derived elements and
 * the sort key
 * @param {CometFields} fields
 * @returns {Comet | null} null when the id gives no sort key, which makes the record invalid
 */
export function createComet(fields) {
	const {
		full = '', id = '', name = '', fragment = '', Ty = 0, Tm = 0, Td = 0, Th = 0,
		q = 0, e = 0, w = 0, N = 0, i = 0, g = 0, k = 0
	} = fields;
	const sortkey = fields.sortkey ?? sortKey(id, fragment);
	if (sortkey === null) return null;
	const T = fields.T ?? jd0(Ty, Tm, Td, Th);
	return Object.freeze({
		full, id, name, fragment, Ty, Tm, Td, Th, T, q, e, w, N, i, g, k, ...derivedElements(q, e), sortkey
	});
}

/**
 * unchanged on re-import: the same full name, T and elements, compared exactly
 * @param {Comet} a
 * @param {Comet} b
 * @returns {boolean}
 */
export function sameComet(a, b) {
	return a.full === b.full && a.T === b.T && a.q === b.q && a.e === b.e && a.i === b.i
		&& a.N === b.N && a.w === b.w && a.g === b.g && a.k === b.k;
}

/**
 * the perihelion nearest to now (`Tn`): for a periodic comet, the one among T ± whole periods in
 * the date range closest to now, the earliest on a tie; otherwise, or when there is none, T
 * (spec 10, D6)
 * @param {Comet} comet
 * @param {number} nowJd the JD of now, its milliseconds dropped
 * @returns {number}
 */
export function nearestPerihelion(comet, nowJd) {
	const { T } = comet;
	if (!isPeriodic(comet.id)) return T;
	// the desktop's decimal holds P to 15 significant digits
	const period = Number(comet.P.toPrecision(15)) * 365.25;
	// the desktop never ends its loop for a period of 0
	if (!(period > 0 && Number.isFinite(period))) return T;

	// the candidates are T + m·period for first ≤ m ≤ last: the desktop steps back while
	// t − period > 1700-01-01, then forward while t < 2300-01-01. Each candidate is computed
	// afresh, since summing the steps in float64 would add up their rounding errors.
	const at = (/** @type {number} */ m) => T + m * period;
	let first = 0;
	if (at(-1) > MINIMUM_JD) {
		first = -Math.max(1, Math.ceil((T - MINIMUM_JD) / period) - 1);
		while (at(first - 1) > MINIMUM_JD) first -= 1;
		while (first < -1 && !(at(first) > MINIMUM_JD)) first += 1;
	}
	if (!(at(first) < MAXIMUM_JD)) return T;
	let last = Math.max(first, Math.ceil((MAXIMUM_JD - T) / period) - 1);
	while (at(last + 1) < MAXIMUM_JD) last += 1;
	while (last > first && !(at(last) < MAXIMUM_JD)) last -= 1;

	// the distance to now falls and then rises along the candidates, so the nearest is next to
	// now's place among them; one more on each side absorbs rounding in that place
	const place = Math.floor((nowJd - T) / period);
	const clamp = (/** @type {number} */ m) => Math.min(Math.max(m, first), last);
	let best = at(clamp(place - 1));
	for (let m = clamp(place - 1) + 1; m <= clamp(place + 2); m++) {
		if (Math.abs(at(m) - nowJd) < Math.abs(best - nowJd)) best = at(m);
	}
	return best;
}
