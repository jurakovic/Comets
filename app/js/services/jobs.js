// The running long computations: run through the JobRunner port, with their progress and a way
// to cancel them for the status area. docs/spec/12-architecture.md, Services and ports;
// docs/spec/01-product.md, Status area.
import { createStore, readonly } from '../lib/store.js';

/**
 * @typedef {object} RunningJob
 * @property {number} id
 * @property {string} name the job's name in core/jobs.js
 * @property {import('./ports.js').JobProgress | null} progress the latest, null until the first
 */

/**
 * @typedef {object} JobsService
 * @property {import('../lib/store.js').ReadableStore<ReadonlyArray<RunningJob>>} running in start order
 * @property {(name: string, input: unknown, options?: { signal?: AbortSignal }) => Promise<unknown>} run
 *   the job's result; rejects with an AbortError when cancelled, which is not an error to show
 * @property {(id: number) => void} cancel
 */

/**
 * @param {object} deps
 * @param {import('./ports.js').JobRunner} deps.runner
 * @returns {JobsService}
 */
export function createJobsService({ runner }) {
	/** @type {import('../lib/store.js').Store<ReadonlyArray<RunningJob>>} */
	const store = createStore(/** @type {ReadonlyArray<RunningJob>} */ (Object.freeze([])));
	/** @type {Map<number, AbortController>} */
	const controllers = new Map();
	let lastId = 0;

	/**
	 * @param {number} id
	 * @param {(job: RunningJob) => RunningJob | null} change null removes the job
	 */
	function change(id, change) {
		store.update(jobs => Object.freeze(jobs.flatMap(job => {
			if (job.id !== id) return [job];
			const changed = change(job);
			return changed ? [changed] : [];
		})));
	}

	return Object.freeze({
		running: readonly(store),
		async run(/** @type {string} */ name, /** @type {unknown} */ input, { signal } = {}) {
			signal?.throwIfAborted();
			const id = ++lastId;
			const controller = new AbortController();
			const forward = () => controller.abort(signal?.reason);
			signal?.addEventListener('abort', forward, { once: true });
			controllers.set(id, controller);
			store.update(jobs => Object.freeze([...jobs, Object.freeze({ id, name, progress: null })]));
			try {
				return await runner.run(name, input, {
					signal: controller.signal,
					onProgress: progress => change(id, job => Object.freeze({ ...job, progress: Object.freeze({ ...progress }) }))
				});
			} finally {
				signal?.removeEventListener('abort', forward);
				controllers.delete(id);
				change(id, () => null);
			}
		},
		cancel(/** @type {number} */ id) {
			controllers.get(id)?.abort(new DOMException('The job was cancelled', 'AbortError'));
		}
	});
}
