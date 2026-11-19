// The JobRunner port: a module Worker running core/jobs.js, and the same protocol on this thread
// in slices, for tests and where Workers fail. INTERNALS.md, Long computations and the Worker.
import { createRpcClient, createRpcServer } from '../lib/rpc.js';

/** @typedef {import('../lib/rpc.js').Job} Job */

/** @typedef {Pick<import('../lib/rpc.js').RpcClient, 'run'>} JobRunner the JobRunner port of services/ports.js */

/** the name of the error a failed Worker rejects its jobs with */
export const WORKER_FAILURE = 'WorkerFailure';

/**
 * the compute Worker (workers/compute.js). A Worker that fails, which in practice means one that did
 * not load, is ended: the jobs it was running and every later one reject with a WorkerFailure, so
 * that the local runner can take over (createFallbackJobRunner)
 * @param {URL | string} url of workers/compute.js
 * @returns {JobRunner}
 */
export function createWorkerJobRunner(url) {
	const worker = new Worker(url, { type: 'module' });
	const client = createRpcClient({ post: request => worker.postMessage(request) });
	/** @type {Error | null} */
	let failure = null;
	const fail = (/** @type {string} */ message) => {
		if (failure) return;
		failure = Object.assign(new Error(message), { name: WORKER_FAILURE });
		worker.terminate();
		client.failAll(failure);
	};
	worker.addEventListener('message', event => client.receive(event.data));
	worker.addEventListener('error', event => {
		event.preventDefault();
		fail(`the compute worker failed: ${event.message || 'it did not load'}`);
	});
	worker.addEventListener('messageerror', () => fail('the compute worker sent a message that could not be read'));
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

/**
 * the Worker's runner until it fails, then the local one: the jobs the failure rejected run again
 * on this thread, and so does every later one. Each runner is made on its first job
 * @param {object} runners
 * @param {() => JobRunner} runners.worker may throw, where module Workers are not supported
 * @param {() => Promise<JobRunner>} runners.local
 * @returns {JobRunner}
 */
export function createFallbackJobRunner({ worker, local }) {
	/** @type {JobRunner | null} */
	let primary = null;
	/** @type {Promise<JobRunner> | null} */
	let fallback = null;
	return Object.freeze({
		/** @type {JobRunner['run']} */
		async run(name, input, options) {
			if (!fallback) {
				try {
					primary ??= worker();
					return await primary.run(name, input, options);
				} catch (error) {
					// a job's own error, or its cancel, is the caller's; a Worker that was made and failed
					// is not
					if (primary && !(error instanceof Error && error.name === WORKER_FAILURE)) throw error;
					if (!fallback) {
						console.warn('the compute worker failed; running jobs on the main thread', error);
						fallback = local();
					}
				}
			}
			return (await fallback).run(name, input, options);
		}
	});
}
