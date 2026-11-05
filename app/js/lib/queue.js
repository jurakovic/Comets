// Tasks run one after another in the order they were given, as writes to storage must be, so that
// the last change is the one that stays.

/**
 * @typedef {(task: () => Promise<void>) => Promise<void>} Queue runs task after every task given
 *   before it has settled; resolves or rejects as task does, and a task that fails does not stop
 *   the ones after it
 */

/** @returns {Queue} */
export function createQueue() {
	/** @type {Promise<void>} */
	let last = Promise.resolve();
	return task => {
		const run = last.then(task);
		last = run.catch(() => {});
		return run;
	};
}
