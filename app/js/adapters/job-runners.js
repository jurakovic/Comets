// The JobRunner port: a module Worker running core/jobs.js, and the same protocol on this thread
// in slices, for tests and where Workers fail. docs/spec/12-architecture.md, Long computations.
import { createRpcClient, createRpcServer } from '../lib/rpc.js';

/** @typedef {import('../lib/rpc.js').Job} Job */

/**
 * the compute Worker (workers/compute.js); a Worker that fails rejects every job it was running
 * @param {URL | string} url of workers/compute.js
 */
export function createWorkerJobRunner(url) {
	const worker = new Worker(url, { type: 'module' });
	const client = createRpcClient({ post: request => worker.postMessage(request) });
	worker.addEventListener('message', event => client.receive(event.data));
	worker.addEventListener('error', event => {
		event.preventDefault();
		client.failAll(new Error(`the compute worker failed: ${event.message || 'it did not load'}`));
	});
	worker.addEventListener('messageerror', () => client.failAll(new Error('the compute worker sent a message that could not be read')));
	return Object.freeze({ run: client.run });
}

/**
 * the jobs run on this thread, in slices between other tasks; messages are cloned as a Worker
 * would clone them, so a result is the same plain data either way
 * @param {Readonly<Record<string, Job>>} jobs
 * @param {{ slice?: number }} [options] milliseconds a slice may run
 */
export function createLocalJobRunner(jobs, { slice = 20 } = {}) {
	// each side gets the other's messages as a later task, as across threads
	const later = (/** @type {() => void} */ task) => { setTimeout(task, 0); };
	const client = createRpcClient({ post: request => { const copy = structuredClone(request); later(() => server.receive(copy)); } });
	const server = createRpcServer({
		jobs,
		post: reply => { const copy = structuredClone(reply); later(() => client.receive(copy)); },
		schedule: later,
		now: () => performance.now(),
		slice
	});
	return Object.freeze({ run: client.run });
}
