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
		assert.ok(progress.every(p => p.total === input.comets.length && p.done <= p.total));
	});
}

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
