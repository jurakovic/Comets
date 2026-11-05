import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createLocalJobRunner } from '../../../app/js/adapters/job-runners.js';

/** @type {Record<string, import('../../../app/js/lib/rpc.js').Job>} */
const jobs = {
	*count(/** @type {number} */ n) {
		for (let i = 0; i < n; i++) yield { done: i, total: n };
		return { counted: n, frozen: Object.freeze([1, 2]) };
	},
	*clone() { yield { done: 0, total: 1 }; return { f() {} }; }
};

test('the local runner returns cloned plain data, as a Worker would', async () => {
	const runner = createLocalJobRunner(jobs);
	const result = /** @type {any} */ (await runner.run('count', 1000, {}));
	assert.deepEqual(result, { counted: 1000, frozen: [1, 2] });
	assert.equal(Object.isFrozen(result.frozen), false, 'a clone, not the job\'s own object');
});

test('a result that does not clone rejects rather than hanging', async () => {
	await assert.rejects(createLocalJobRunner(jobs).run('clone', null, {}), { name: 'DataCloneError' });
});

test('an abort right after the run cancels it before its first slice', async () => {
	const controller = new AbortController();
	/** @type {unknown[]} */
	const progress = [];
	const result = createLocalJobRunner(jobs, { slice: 0 }).run('count', 1e9, { signal: controller.signal, onProgress: p => progress.push(p) });
	controller.abort();
	await assert.rejects(result, { name: 'AbortError' });
	await new Promise(resolve => setTimeout(resolve, 20));
	assert.deepEqual(progress, []);
});
