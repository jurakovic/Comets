// ephemeris-text/ and graph.json through the job protocol, with every message cloned as a
// Worker clones it (spec 11, Conformance plan, level 2).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { EPHEMERIS_CASES, ephemerisCase } from '../helpers/ephemeris.js';
import { assertChart, GRAPH, graphCase } from '../helpers/graph.js';
import { readVectorText, toLf } from '../helpers/vectors.js';
import { createLocalJobRunner } from '../../app/js/adapters/job-runners.js';
import { JOBS } from '../../app/js/core/jobs.js';

const runner = createLocalJobRunner(JOBS, { slice: 5 });

test('the jobs are ephemeris and graph', () => {
	assert.deepEqual(Object.keys(JOBS), ['ephemeris', 'graph']);
});

for (const { id, file, settings } of EPHEMERIS_CASES) {
	test(`ephemeris ${id}`, async () => {
		const { input } = ephemerisCase(settings);
		/** @type {Array<{ done: number, total: number }>} */
		const progress = [];
		const text = await runner.run('ephemeris', input, { onProgress: p => progress.push(p) });
		assert.equal(typeof text, 'string');
		assert.equal(toLf(/** @type {string} */ (text)), readVectorText(file));
		assertEphemerisProgress(progress, input.comets.length, /** @type {string} */ (text));
	});
}

/**
 * progress in comets: the calculation's over the input, then the text's over the comets with rows
 * @param {ReadonlyArray<{ done: number, total: number }>} progress
 * @param {number} comets
 * @param {string} text
 */
function assertEphemerisProgress(progress, comets, text) {
	const written = (text.match(/^Comet:/gm) ?? []).length;
	assert.ok(progress.every(p => (p.total === comets || p.total === written) && p.done <= p.total), JSON.stringify(progress));
}

test('the ephemeris job reports both phases, the text over the comets with rows', async () => {
	// a slice of 0 ms posts the progress after every step
	const everyStep = createLocalJobRunner(JOBS, { slice: 0 });
	const { input } = ephemerisCase(EPHEMERIS_CASES[1].settings);
	/** @type {Array<{ done: number, total: number }>} */
	const progress = [];
	const text = /** @type {string} */ (await everyStep.run('ephemeris', input, { onProgress: p => progress.push(p) }));
	assertEphemerisProgress(progress, input.comets.length, text);
	assert.deepEqual([...new Set(progress.map(p => p.total))], [34, 1]);
	assert.deepEqual(progress.at(-1), { done: 1, total: 1 });
});

for (const { id, settings, chart } of GRAPH.cases) {
	test(`graph ${id}`, async () => {
		const { input } = graphCase(settings);
		const model = await runner.run('graph', input);
		assertChart(/** @type {any} */ (model), chart, id);
	});
}

test('a cancelled job rejects with an AbortError', async () => {
	const { input } = graphCase(GRAPH.cases[1].settings);
	const controller = new AbortController();
	const result = runner.run('graph', input, { signal: controller.signal });
	controller.abort();
	await assert.rejects(result, { name: 'AbortError' });
});
