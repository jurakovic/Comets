import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFallbackJobRunner, createLocalJobRunner, WORKER_FAILURE } from '../../../app/js/adapters/job-runners.js';

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

/**
 * a runner that answers each job as the test says
 * @param {(name: string) => Promise<unknown>} answer
 */
const fakeRunner = answer => ({ calls: /** @type {string[]} */ ([]), run(/** @type {string} */ name) { this.calls.push(name); return answer(name); } });
const workerFailure = () => Object.assign(new Error('the compute worker failed: it did not load'), { name: WORKER_FAILURE });

test('the fallback runner: the Worker\'s runner while it works; a job\'s error and a cancel are the caller\'s', async () => {
	const worker = fakeRunner(async name => {
		if (name === 'broken') throw new RangeError('bad input');
		if (name === 'cancelled') throw Object.assign(new Error('The job was cancelled'), { name: 'AbortError' });
		return `${name} done`;
	});
	let locals = 0;
	const runner = createFallbackJobRunner({ worker: () => worker, local: async () => { locals += 1; return fakeRunner(async () => 'local'); } });
	assert.equal(await runner.run('graph', null, {}), 'graph done');
	await assert.rejects(runner.run('broken', null, {}), RangeError);
	await assert.rejects(runner.run('cancelled', null, {}), { name: 'AbortError' });
	assert.equal(await runner.run('graph', null, {}), 'graph done');
	assert.deepEqual(worker.calls, ['graph', 'broken', 'cancelled', 'graph']);
	assert.equal(locals, 0);
});

test('the fallback runner: the jobs a Worker failure rejects run again locally, and so does every later one', async () => {
	let workers = 0;
	const worker = fakeRunner(() => Promise.reject(workerFailure()));
	const local = fakeRunner(async name => `${name} on this thread`);
	let locals = 0;
	const runner = createFallbackJobRunner({ worker: () => { workers += 1; return worker; }, local: async () => { locals += 1; return local; } });
	const warn = console.warn;
	/** @type {unknown[]} */
	const warnings = [];
	console.warn = (...args) => { warnings.push(args); };
	try {
		assert.deepEqual(await Promise.all([runner.run('ephemeris', null, {}), runner.run('graph', null, {})]), ['ephemeris on this thread', 'graph on this thread']);
		assert.equal(await runner.run('graph', null, {}), 'graph on this thread');
	} finally {
		console.warn = warn;
	}
	assert.deepEqual(worker.calls, ['ephemeris', 'graph'], 'not asked again');
	assert.deepEqual(local.calls, ['ephemeris', 'graph', 'graph']);
	assert.equal(workers, 1);
	assert.equal(locals, 1);
	assert.equal(warnings.length, 1);
});

test('the fallback runner: a Worker that cannot be made is a failure too', async () => {
	const runner = createFallbackJobRunner({
		worker: () => { throw new TypeError('module workers are not supported'); },
		local: async () => fakeRunner(async () => 'local')
	});
	const warn = console.warn;
	console.warn = () => {};
	try {
		assert.equal(await runner.run('graph', null, {}), 'local');
	} finally {
		console.warn = warn;
	}
});
