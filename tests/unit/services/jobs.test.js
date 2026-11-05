import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createJobsService } from '../../../app/js/services/jobs.js';

/**
 * a JobRunner a test drives: each run waits until the test settles it
 */
function fakeRunner() {
	/** @type {Array<{ name: string, input: unknown, signal?: AbortSignal, onProgress?: (p: { done: number, total: number }) => void, resolve: (v: unknown) => void, reject: (e: unknown) => void }>} */
	const runs = [];
	return {
		runs,
		run(/** @type {string} */ name, /** @type {unknown} */ input, /** @type {{ signal?: AbortSignal, onProgress?: (p: { done: number, total: number }) => void }} */ { signal, onProgress }) {
			return new Promise((resolve, reject) => {
				runs.push({ name, input, signal, onProgress, resolve, reject });
				signal?.addEventListener('abort', () => reject(signal.reason), { once: true });
			});
		}
	};
}

test('a running job is listed with its progress until it ends', async () => {
	const runner = fakeRunner();
	const jobs = createJobsService({ runner });
	/** @type {any[]} */
	const seen = [];
	jobs.running.subscribe(list => seen.push(list.map(j => ({ ...j }))));
	const result = jobs.run('graph', { x: 1 });
	assert.deepEqual(jobs.running.get().map(j => ({ ...j })), [{ id: 1, name: 'graph', progress: null }]);
	assert.deepEqual(runner.runs[0].input, { x: 1 });
	runner.runs[0].onProgress?.({ done: 1, total: 3 });
	assert.deepEqual(jobs.running.get()[0].progress, { done: 1, total: 3 });
	runner.runs[0].resolve('model');
	assert.equal(await result, 'model');
	assert.deepEqual(jobs.running.get(), []);
	assert.deepEqual(seen.map(list => list.map((/** @type {any} */ j) => j.progress)), [[null], [{ done: 1, total: 3 }], []]);
});

test('cancel aborts the run with an AbortError, and the job leaves the list', async () => {
	const runner = fakeRunner();
	const jobs = createJobsService({ runner });
	const first = jobs.run('ephemeris', 1);
	const second = jobs.run('ephemeris', 2);
	assert.deepEqual(jobs.running.get().map(j => j.id), [1, 2]);
	jobs.cancel(1);
	await assert.rejects(first, { name: 'AbortError' });
	assert.deepEqual(jobs.running.get().map(j => j.id), [2]);
	jobs.cancel(42);
	runner.runs[1].resolve('text');
	assert.equal(await second, 'text');
});

test('a failing job leaves the list and passes its error on', async () => {
	const runner = fakeRunner();
	const jobs = createJobsService({ runner });
	const result = jobs.run('graph', null);
	runner.runs[0].reject(new Error('broken'));
	await assert.rejects(result, { message: 'broken' });
	assert.deepEqual(jobs.running.get(), []);
});

test('the caller\'s signal cancels too, also when it is aborted already', async () => {
	const runner = fakeRunner();
	const jobs = createJobsService({ runner });
	const controller = new AbortController();
	const result = jobs.run('graph', null, { signal: controller.signal });
	controller.abort();
	await assert.rejects(result, { name: 'AbortError' });
	await assert.rejects(jobs.run('graph', null, { signal: controller.signal }), { name: 'AbortError' });
	assert.deepEqual(jobs.running.get(), []);
});
