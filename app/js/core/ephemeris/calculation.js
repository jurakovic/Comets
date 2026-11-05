// The ephemeris calculation (`CalculateEphemerisAsync`) as a generator: rows at exact steps from
// Start to End that meet the requirements, comet by comet. docs/spec/06-ephemeris.md, Calculation.
import { ephemeris } from '../astro/ephemeris.js';
import { instantToJd } from '../time/julian.js';
import { intervalMs, REQUIREMENTS } from './settings.js';
import { wholeSteps } from './timespan.js';

/** @typedef {import('../comets/comet.js').Comet} Comet */
/** @typedef {import('../astro/ephemeris.js').Ephemeris} Ephemeris */

/**
 * what a calculation needs, all of it plain data: the Tn of each comet comes from the session
 * cache on the main thread, so the result does not depend on the thread that computes it
 * @typedef {object} EphemerisInput
 * @property {ReadonlyArray<Comet>} comets in order: the selected comet, or the list in multiple mode
 * @property {ReadonlyArray<number>} nearest nearest[k] is the Tn of comets[k]
 * @property {import('./settings.js').EphemerisSettings} settings validated
 * @property {import('../astro/ephemeris.js').Observer} observer the location when Calculate was pressed
 * @property {string} timeZone for Local Time
 */

/**
 * a comet with rows; comets without any are left out
 * @typedef {object} EphemerisResult
 * @property {Comet} comet
 * @property {number} Tn
 * @property {ReadonlyArray<Ephemeris>} rows
 */

/**
 * how far a calculation is, in comets
 * @typedef {{ done: number, total: number }} Progress
 */

/** rows between two yields, so that a runner can report and cancel within a long comet */
const ROWS_PER_YIELD = 2000;

/**
 * the comets a calculation runs over: the list in multiple mode, otherwise the selected comet
 * @param {{ multiple: boolean, comets: ReadonlyArray<Comet>, selected: Comet | null }} selection
 * @returns {ReadonlyArray<Comet>}
 */
export function cometsToCalculate({ multiple, comets, selected }) {
	return multiple ? comets : selected ? [selected] : [];
}

/**
 * the rows of one comet; yields nothing but a point to stop at every ROWS_PER_YIELD rows
 * @param {Comet} comet
 * @param {EphemerisInput} input
 * @returns {Generator<void, Ephemeris[], void>}
 */
function* rowsOf(comet, { settings, observer }) {
	const step = intervalMs(settings.interval);
	const steps = wholeSteps(settings.start, settings.end, step);
	const checks = REQUIREMENTS
		.filter(r => settings.requirements[r.key].checked)
		.map(r => ({ keeps: r.keeps, value: /** @type {number} */ (settings.requirements[r.key].value) }));
	/** @type {Ephemeris[]} */
	const rows = [];
	for (let k = 0; k <= steps; k++) {
		if (k > 0 && k % ROWS_PER_YIELD === 0) yield;
		const ep = ephemeris(comet, instantToJd(settings.start + k * step), observer);
		if (checks.every(c => c.keeps(ep, c.value))) rows.push(ep);
	}
	return rows;
}

/**
 * the calculation; yields `{ done, total }` in comets, also between rows of a long comet
 * @param {EphemerisInput} input
 * @returns {Generator<Progress, ReadonlyArray<EphemerisResult>, void>}
 */
export function* calculateEphemerides(input) {
	const total = input.comets.length;
	/** @type {EphemerisResult[]} */
	const results = [];
	for (let index = 0; index < total; index++) {
		const comet = input.comets[index];
		const rows = rowsOf(comet, input);
		let next = rows.next();
		while (!next.done) {
			yield { done: index, total };
			next = rows.next();
		}
		if (next.value.length > 0) results.push(Object.freeze({ comet, Tn: input.nearest[index], rows: Object.freeze(next.value) }));
		yield { done: index + 1, total };
	}
	return Object.freeze(results);
}
