// The long computations a Worker can run, by name, each a generator over plain input.
// docs/spec/12-architecture.md, Long computations and the Worker.
import { calculateEphemerides } from './ephemeris/calculation.js';
import { ephemerisText } from './ephemeris/text.js';
import { calculateGraph } from './graph/model.js';

/** @typedef {import('../lib/rpc.js').Progress} Progress */

/**
 * @param {import('./ephemeris/calculation.js').EphemerisInput} input
 * @returns {Generator<Progress, string, void>} the ephemeris text
 */
function* ephemerisJob(input) {
	const results = yield* calculateEphemerides(input);
	return ephemerisText(results, input.settings, input.timeZone);
}

/**
 * the jobs by name: `ephemeris` gives the text of spec 06, `graph` the graph model of spec 07
 * @type {Readonly<Record<string, import('../lib/rpc.js').Job>>}
 */
export const JOBS = Object.freeze({
	ephemeris: ephemerisJob,
	graph: calculateGraph
});
