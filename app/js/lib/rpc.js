// The message protocol between a thread that asks for long computations and the thread that runs
// them: run → progress* → result | error, and cancel. The server runs generators in time slices,
// so a cancel can arrive between them. docs/spec/12-architecture.md, Long computations.

/**
 * @typedef {{ done: number, total: number }} Progress
 */

/**
 * a long computation: yields progress, returns its result
 * @typedef {(input: any) => Generator<Progress, unknown, void>} Job
 */

/**
 * @typedef {{ type: 'run', id: number, name: string, input: unknown }
 *   | { type: 'cancel', id: number }} Request
 */

/**
 * @typedef {{ type: 'progress', id: number, progress: Progress }
 *   | { type: 'result', id: number, value: unknown }
 *   | { type: 'error', id: number, error: { name: string, message: string } }} Reply
 */

/**
 * the part of AbortSignal the client needs
 * @typedef {object} Signal
 * @property {boolean} aborted
 * @property {unknown} [reason]
 * @property {(type: 'abort', listener: () => void, options?: { once?: boolean }) => void} addEventListener
 * @property {(type: 'abort', listener: () => void) => void} removeEventListener
 */

/**
 * @typedef {object} RunOptions
 * @property {Signal} [signal] aborting it cancels the job and rejects with its reason
 * @property {(progress: Progress) => void} [onProgress]
 */

/**
 * @param {unknown} error
 * @returns {{ name: string, message: string }}
 */
function describe(error) {
	return error instanceof Error ? { name: error.name, message: error.message } : { name: 'Error', message: String(error) };
}

/**
 * the side that runs jobs: give it every request, and it posts the replies
 * @param {object} options
 * @param {Readonly<Record<string, Job>>} options.jobs by name
 * @param {(reply: Reply) => void} options.post
 * @param {(task: () => void) => void} options.schedule runs the next slice later, after pending messages
 * @param {() => number} options.now milliseconds, for the length of a slice
 * @param {number} [options.slice] milliseconds a slice may run
 * @returns {{ receive: (request: Request) => void }}
 */
export function createRpcServer({ jobs, post, schedule, now, slice = 50 }) {
	/** @type {Map<number, Generator<Progress, unknown, void>>} */
	const running = new Map();

	/**
	 * posts a reply; one that cannot be sent (a result that does not clone) becomes an error reply,
	 * so that the run still settles
	 * @param {Reply} reply
	 */
	function send(reply) {
		try {
			post(reply);
		} catch (error) {
			if (reply.type !== 'error') post({ type: 'error', id: reply.id, error: describe(error) });
		}
	}

	/** @param {number} id */
	function step(id) {
		const generator = running.get(id);
		// cancelled since the last slice
		if (!generator) return;
		const until = now() + slice;
		for (;;) {
			let next;
			try {
				next = generator.next();
			} catch (error) {
				running.delete(id);
				send({ type: 'error', id, error: describe(error) });
				return;
			}
			if (next.done) {
				running.delete(id);
				send({ type: 'result', id, value: next.value });
				return;
			}
			if (now() >= until) {
				send({ type: 'progress', id, progress: next.value });
				schedule(() => step(id));
				return;
			}
		}
	}

	/** @type {{ [K in Request['type']]: (request: Extract<Request, { type: K }>) => void }} */
	const handlers = {
		run({ id, name, input }) {
			const job = Object.hasOwn(jobs, name) ? jobs[name] : undefined;
			if (!job) {
				send({ type: 'error', id, error: { name: 'Error', message: `unknown job: ${name}` } });
				return;
			}
			try {
				running.set(id, job(input));
			} catch (error) {
				send({ type: 'error', id, error: describe(error) });
				return;
			}
			schedule(() => step(id));
		},
		cancel({ id }) {
			running.get(id)?.return(undefined);
			running.delete(id);
		}
	};

	return Object.freeze({
		receive(/** @type {Request} */ request) {
			/** @type {(request: Request) => void} */ (handlers[request.type])(request);
		}
	});
}

/**
 * @typedef {object} RpcClient
 * @property {(name: string, input: unknown, options?: RunOptions) => Promise<unknown>} run the job's
 *   result; rejects with the job's error, or with the signal's reason when it is aborted
 * @property {(reply: Reply) => void} receive
 * @property {(error: Error) => void} failAll rejects every job still running, as when the thread
 *   that runs them fails
 */

/**
 * an error named AbortError, for a signal aborted without a reason
 * @returns {Error}
 */
function abortError() {
	return Object.assign(new Error('The job was cancelled'), { name: 'AbortError' });
}

/**
 * the side that asks: run returns the result, give it every reply
 * @param {object} options
 * @param {(request: Request) => void} options.post
 * @returns {RpcClient}
 */
export function createRpcClient({ post }) {
	let lastId = 0;
	/** @type {Map<number, { resolve: (value: unknown) => void, reject: (error: unknown) => void, onProgress?: (progress: Progress) => void, done: () => void }>} */
	const pending = new Map();

	/** @param {number} id */
	function settle(id) {
		const entry = pending.get(id);
		pending.delete(id);
		entry?.done();
		return entry;
	}

	/** @type {{ [K in Reply['type']]: (reply: Extract<Reply, { type: K }>) => void }} */
	const handlers = {
		progress({ id, progress }) { pending.get(id)?.onProgress?.(progress); },
		result({ id, value }) { settle(id)?.resolve(value); },
		error({ id, error }) { settle(id)?.reject(Object.assign(new Error(error.message), { name: error.name })); }
	};

	return Object.freeze({
		/**
		 * @param {string} name
		 * @param {unknown} input plain data
		 * @param {RunOptions} [options]
		 * @returns {Promise<unknown>}
		 */
		run(name, input, { signal, onProgress } = {}) {
			if (signal?.aborted) return Promise.reject(signal.reason ?? abortError());
			const id = ++lastId;
			return new Promise((resolve, reject) => {
				const abort = () => {
					settle(id);
					post({ type: 'cancel', id });
					reject(signal?.reason ?? abortError());
				};
				signal?.addEventListener('abort', abort, { once: true });
				pending.set(id, { resolve, reject, onProgress, done: () => signal?.removeEventListener('abort', abort) });
				try {
					post({ type: 'run', id, name, input });
				} catch (error) {
					// an input that does not clone: nothing was sent, so there is nothing to cancel
					settle(id);
					reject(error);
				}
			});
		},
		/** @param {Reply} reply */
		receive(reply) {
			/** @type {(reply: Reply) => void} */ (handlers[reply.type])(reply);
		},
		/**
		 * rejects every job still running, as when the thread that runs them fails
		 * @param {Error} error
		 */
		failAll(error) {
			for (const id of [...pending.keys()]) settle(id)?.reject(error);
		}
	});
}
