import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createRpcClient, createRpcServer } from '../../../app/js/lib/rpc.js';

/** @typedef {import('../../../app/js/lib/rpc.js').Progress} Progress */

/**
 * a client and a server joined by queues that a test runs by hand; the clock moves 10 ms at
 * every reading, so a 25 ms slice takes three steps of a generator
 * @param {Record<string, import('../../../app/js/lib/rpc.js').Job>} jobs
 */
function setup(jobs) {
	/** @type {Array<() => void>} */
	const tasks = [];
	/** @type {any[]} */
	const requests = [];
	/** @type {any[]} */
	const replies = [];
	let time = 0;
	const server = createRpcServer({
		jobs,
		post: reply => { replies.push(reply); tasks.push(() => client.receive(reply)); },
		schedule: task => tasks.push(task),
		now: () => (time += 10),
		slice: 25
	});
	const client = createRpcClient({ post: request => { requests.push(request); tasks.push(() => server.receive(request)); } });
	const flush = () => { while (tasks.length > 0) /** @type {() => void} */ (tasks.shift())(); };
	return { client, requests, replies, flush, tasks };
}

/** counts to n, yielding each step; records whether it was closed early */
function counter(/** @type {{ closed: boolean, steps: number }} */ seen) {
	return function* (/** @type {number} */ n) {
		try {
			for (let i = 0; i < n; i++) {
				seen.steps += 1;
				yield { done: i, total: n };
			}
			return `counted ${n}`;
		} finally {
			seen.closed = true;
		}
	};
}

test('run, progress once per slice, then the result', async () => {
	const seen = { closed: false, steps: 0 };
	const { client, replies, flush } = setup({ count: counter(seen) });
	/** @type {Progress[]} */
	const progress = [];
	const result = client.run('count', 7, { onProgress: p => progress.push(p) });
	flush();
	assert.equal(await result, 'counted 7');
	// slices of three steps: after steps 3 and 6, then the end
	assert.deepEqual(progress, [{ done: 2, total: 7 }, { done: 5, total: 7 }]);
	assert.deepEqual(replies.map(r => r.type), ['progress', 'progress', 'result']);
});

test('cancel: the generator is closed, nothing more is sent, and the run rejects with the signal\'s reason', async () => {
	const seen = { closed: false, steps: 0 };
	const { client, replies, flush, tasks, requests } = setup({ count: counter(seen) });
	const controller = new AbortController();
	const result = client.run('count', 1000, { signal: controller.signal });
	// the run request, then the first slice
	tasks.shift()?.();
	tasks.shift()?.();
	assert.equal(seen.steps, 3);
	controller.abort();
	await assert.rejects(result, { name: 'AbortError' });
	flush();
	// the slice queued before the cancel arrived still runs, as in a Worker; then it stops
	assert.equal(seen.closed, true);
	assert.equal(seen.steps, 6);
	assert.deepEqual(requests.map(r => r.type), ['run', 'cancel']);
	assert.deepEqual(replies.map(r => r.type), ['progress', 'progress']);
});

test('an aborted signal rejects at once and runs nothing', async () => {
	const seen = { closed: false, steps: 0 };
	const { client, requests } = setup({ count: counter(seen) });
	const controller = new AbortController();
	controller.abort(new Error('not now'));
	await assert.rejects(client.run('count', 3, { signal: controller.signal }), { message: 'not now' });
	assert.deepEqual(requests, []);
});

test('errors: an unknown job, and a job that throws, with its name and message', async () => {
	const { client, flush } = setup({
		*broken() { yield { done: 0, total: 1 }; throw new RangeError('out of range'); },
		eager() { throw new TypeError('bad input'); }
	});
	const unknown = client.run('nothing', null);
	const broken = client.run('broken', null);
	const eager = client.run('eager', null);
	flush();
	await assert.rejects(unknown, { name: 'Error', message: 'unknown job: nothing' });
	await assert.rejects(broken, { name: 'RangeError', message: 'out of range' });
	await assert.rejects(eager, { name: 'TypeError', message: 'bad input' });
});

test('jobs run side by side, each with its own id', async () => {
	const seen = { closed: false, steps: 0 };
	const { client, flush } = setup({ count: counter(seen) });
	const results = Promise.all([client.run('count', 4), client.run('count', 2)]);
	flush();
	assert.deepEqual(await results, ['counted 4', 'counted 2']);
});

test('failAll rejects every job still running', async () => {
	const seen = { closed: false, steps: 0 };
	const { client } = setup({ count: counter(seen) });
	const result = client.run('count', 3);
	client.failAll(new Error('the worker failed'));
	await assert.rejects(result, { message: 'the worker failed' });
});

test('a reply for a job no longer waited for is dropped', () => {
	const client = createRpcClient({ post: () => {} });
	client.receive({ type: 'result', id: 99, value: 1 });
	client.receive({ type: 'progress', id: 99, progress: { done: 0, total: 1 } });
	client.receive({ type: 'error', id: 99, error: { name: 'Error', message: 'x' } });
});

test('a result that does not clone becomes an error, so the run still settles', async () => {
	/** @type {Array<() => void>} */
	const tasks = [];
	const server = createRpcServer({
		jobs: { *bad() { yield { done: 0, total: 1 }; return { f() {} }; } },
		post: reply => { const copy = structuredClone(reply); tasks.push(() => client.receive(copy)); },
		schedule: task => tasks.push(task),
		now: () => 0
	});
	const client = createRpcClient({ post: request => tasks.push(() => server.receive(request)) });
	const result = client.run('bad', null);
	while (tasks.length > 0) /** @type {() => void} */ (tasks.shift())();
	await assert.rejects(result, { name: 'DataCloneError' });
});

test('an input that does not clone rejects, and leaves nothing to cancel', async () => {
	/** @type {any[]} */
	const posted = [];
	const client = createRpcClient({ post: request => { posted.push(structuredClone(request)); } });
	const controller = new AbortController();
	await assert.rejects(client.run('count', { f() {} }, { signal: controller.signal }), { name: 'DataCloneError' });
	controller.abort();
	assert.deepEqual(posted, []);
});
