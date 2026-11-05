// Graph sampling: the interval by span, the sample times, and the calculation of the plotted values
// as a generator. docs/spec/07-graph.md, Sampling.
import { ephemeris } from '../astro/ephemeris.js';
import { wholeSteps } from '../ephemeris/timespan.js';
import { DAY_MS } from '../time/calendar.js';
import { instantToJd } from '../time/julian.js';

/** @typedef {import('../comets/comet.js').Comet} Comet */

const YEAR = 365.25;

/** the interval for spans up to the bound (exclusive, or inclusive for the first), in days */
const INTERVALS = Object.freeze([
	[10 * YEAR, 2], [50 * YEAR, 5], [100 * YEAR, 15], [200 * YEAR, 30], [300 * YEAR, 40]
]);

/**
 * the sampling interval in days for a span `S = JD(End) − JD(Start)`: S / 100 up to 100 days,
 * then whole days by the table; 0 from 300 years on, which validation rejects
 * @param {number} start UTC milliseconds
 * @param {number} end UTC milliseconds
 * @returns {number}
 */
export function samplingInterval(start, end) {
	const span = (end - start) / DAY_MS;
	if (span <= 100) return span / 100;
	if (span < 365) return 1;
	return INTERVALS.find(([bound]) => span < bound)?.[1] ?? 0;
}

/**
 * how many samples: 101 up to 100 days, otherwise every whole interval from Start to End, End
 * included when it falls on one (spec 10, D8)
 * @param {number} start UTC milliseconds, whole seconds
 * @param {number} end UTC milliseconds, whole seconds
 * @returns {number}
 */
export function sampleCount(start, end) {
	const interval = samplingInterval(start, end);
	if ((end - start) / DAY_MS <= 100) return 101;
	return interval > 0 ? wholeSteps(start, end, interval * DAY_MS) + 1 : 0;
}

/**
 * the JDs of the samples: JD(Start) + k·interval
 * @param {number} start UTC milliseconds, whole seconds
 * @param {number} end UTC milliseconds, whole seconds
 * @returns {ReadonlyArray<number>}
 */
export function sampleJds(start, end) {
	const startJd = instantToJd(start);
	const count = sampleCount(start, end);
	const span = (end - start) / DAY_MS;
	// k·S / 100 rather than k·(S / 100), the nearer to the exact value
	const at = span <= 100 ? (/** @type {number} */ k) => startJd + (k * span) / 100 : (/** @type {number} */ k) => startJd + k * samplingInterval(start, end);
	return Object.freeze(Array.from({ length: count }, (_, k) => at(k)));
}

/**
 * what a graph calculation needs, as plain data
 * @typedef {object} GraphInput
 * @property {ReadonlyArray<Comet>} comets in order: the selected comet, or the list in multiple mode
 * @property {ReadonlyArray<number>} nearest nearest[k] is the Tn of comets[k]
 * @property {import('./settings.js').GraphSettings} settings validated
 * @property {boolean} multiple whether the comet picker chose all comets
 * @property {import('../astro/ephemeris.js').Observer} observer the location when Calculate was pressed
 * @property {number} nowJd the JD of now, its milliseconds dropped
 */

/**
 * the plotted values: `values[c][k]` for comets[c] at `jds[k]`
 * @typedef {object} GraphSamples
 * @property {ReadonlyArray<number>} jds
 * @property {ReadonlyArray<ReadonlyArray<number>>} values
 */

/** samples between two yields, so that a runner can report and cancel within a long comet */
const SAMPLES_PER_YIELD = 2000;

/**
 * the samples, with no requirements; yields `{ done, total }` in comets
 * @param {GraphInput} input
 * @param {(ep: import('../astro/ephemeris.js').Ephemeris) => number} value the chart type's value
 * @returns {Generator<{ done: number, total: number }, GraphSamples, void>}
 */
export function* calculateSamples({ comets, settings, observer }, value) {
	const jds = sampleJds(settings.start, settings.end);
	const total = comets.length;
	/** @type {ReadonlyArray<number>[]} */
	const values = [];
	for (let index = 0; index < total; index++) {
		const row = new Array(jds.length);
		for (let k = 0; k < jds.length; k++) {
			if (k > 0 && k % SAMPLES_PER_YIELD === 0) yield { done: index, total };
			row[k] = value(ephemeris(comets[index], jds[k], observer));
		}
		values.push(Object.freeze(row));
		yield { done: index + 1, total };
	}
	return Object.freeze({ jds, values: Object.freeze(values) });
}
