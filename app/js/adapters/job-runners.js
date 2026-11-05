// The JobRunner port: a module Worker running core/jobs.js, and the same protocol on this thread
// in slices, for tests and where Workers fail. docs/spec/12-architecture.md, Long computations.
import { createRpcClient, createRpcServer } from '../lib/rpc.js';

/** @typedef {import('../lib/rpc.js').Job} Job */

/** @typedef {Pick<import('../lib/rpc.js').RpcClient, 'run'>} JobRunner the JobRunner port of services/ports.js */

/**
 * the compute Worker (workers/compute.js). A Worker that fails, which in practice means one that did
 * not load, is ended: the jobs it was running and every later one reject with its error, so the
 * composition root can turn to the local runner
 * @param {URL | string} url of workers/compute.js
 * @returns {JobRunner}
 */
export function createWorkerJobRunner(url) {
	const worker = new Worker(url, { type: 'module' });
	const client = createRpcClient({ post: request => worker.postMessage(request) });
	/** @type {Error | null} */
	let failure = null;
	const fail = (/** @type {Error} */ error) => {
		if (failure) return;
		failure = error;
		worker.terminate();
		client.failAll(error);
	};
	worker.addEventListener('message', event => client.receive(event.data));
	worker.addEventListener('error', event => {
		event.preventDefault();
		fail(new Error(`the compute worker failed: ${event.message || 'it did not load'}`));
	});
	worker.addEventListener('messageerror', () => fail(new Error('the compute worker sent a message that could not be read')));
	return Object.freeze({
		run: (name, input, options) => failure ? Promise.reject(failure) : client.run(name, input, options)
	});
}

/**
 * the jobs run on this thread, in slices between other tasks; messages are cloned as a Worker
 * would clone them, so a result is the same plain data either way
 * @param {Readonly<Record<string, Job>>} jobs
 * @param {{ slice?: number }} [options] milliseconds a slice may run
 * @returns {JobRunner}
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
