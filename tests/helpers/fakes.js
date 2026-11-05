// Fakes of the ports (docs/spec/12-architecture.md, Services and ports) for service tests.

/**
 * a Clock that stands still until a test moves it
 * @param {number} start UTC milliseconds
 * @param {string} [zone] the IANA time zone
 */
export function createFakeClock(start, zone = 'UTC') {
	let instant = start;
	return {
		now: () => instant,
		timeZone: () => zone,
		/** moves the clock by ms, which may be negative */
		advance(/** @type {number} */ ms) { instant += ms; },
		/** puts the clock at an instant */
		set(/** @type {number} */ to) { instant = to; }
	};
}

/**
 * a KeyValueStore in memory that records every write, as `put key` and `remove key`
 * @param {Record<string, string>} [initial]
 */
export function createRecordingStore(initial = {}) {
	const values = new Map(Object.entries(initial));
	/** @type {string[]} */
	const writes = [];
	return {
		values,
		writes,
		get: async (/** @type {string} */ key) => values.get(key),
		async put(/** @type {string} */ key, /** @type {string} */ value) { writes.push(`put ${key}`); values.set(key, value); },
		async remove(/** @type {string} */ key) { writes.push(`remove ${key}`); values.delete(key); }
	};
}

/**
 * Web Storage in memory. `failing` makes every call throw as a blocked or full storage does;
 * `quota` makes setItem throw once the stored text would be longer.
 * @param {{ failing?: boolean, quota?: number }} [options]
 */
export function createFakeWebStorage({ failing = false, quota = Infinity } = {}) {
	const values = new Map();
	const state = { failing };
	const check = () => {
		if (state.failing) throw new DOMException('The operation is insecure.', 'SecurityError');
	};
	const storage = {
		getItem(/** @type {string} */ key) { check(); return values.has(key) ? values.get(key) : null; },
		setItem(/** @type {string} */ key, /** @type {string} */ value) {
			check();
			const size = [...values].reduce((sum, [k, v]) => sum + (k === key ? 0 : k.length + v.length), key.length + value.length);
			if (size > quota) throw new DOMException('The quota has been exceeded.', 'QuotaExceededError');
			values.set(key, String(value));
		},
		removeItem(/** @type {string} */ key) { check(); values.delete(key); }
	};
	return { storage: /** @type {Storage} */ (/** @type {unknown} */ (storage)), values, state };
}

/**
 * IndexedDB in memory, with as much of the API as adapters/key-value-stores.js uses: open with an
 * upgrade, one object store, get, put and delete in transactions that complete or fail as a task.
 * @param {{ open?: 'ok' | 'throw' | 'error' | 'blocked' | 'hang', failing?: boolean }} [options]
 *   how open behaves; failing makes every transaction fail
 */
export function createFakeIndexedDb({ open = 'ok', failing = false } = {}) {
	/** @type {Map<string, Map<string, unknown>>} */
	const stores = new Map();
	const state = { failing, opens: 0, upgrades: 0, closed: 0, transactions: 0 };
	/** @type {any[]} the connections opened */
	const connections = [];
	const later = (/** @type {() => void} */ task) => { setTimeout(task, 0); };

	function createDatabase() {
		return {
			objectStoreNames: { contains: (/** @type {string} */ name) => stores.has(name) },
			createObjectStore(/** @type {string} */ name) { state.upgrades += 1; stores.set(name, new Map()); },
			close() { state.closed += 1; },
			onversionchange: null,
			transaction(/** @type {string} */ name, /** @type {string} */ mode) {
				const objects = stores.get(name);
				if (!objects) throw new DOMException(`no object store ${name}`, 'NotFoundError');
				state.transactions += 1;
				/** @type {any} */
				const transaction = { error: null, oncomplete: null, onerror: null, onabort: null };
				/** @param {(objects: Map<string, unknown>) => unknown} act */
				const request = act => {
					/** @type {any} */
					const req = { result: undefined };
					later(() => {
						if (state.failing) {
							transaction.error = new DOMException('The transaction failed.', 'UnknownError');
							transaction.onerror?.();
							transaction.onabort?.();
							return;
						}
						req.result = act(objects);
						transaction.oncomplete?.();
					});
					return req;
				};
				const readonly = () => {
					if (mode !== 'readwrite') throw new DOMException('read only', 'ReadOnlyError');
				};
				transaction.objectStore = () => ({
					get: (/** @type {string} */ key) => request(o => o.get(key)),
					put: (/** @type {unknown} */ value, /** @type {string} */ key) => { readonly(); return request(o => { o.set(key, value); return key; }); },
					delete: (/** @type {string} */ key) => { readonly(); return request(o => { o.delete(key); }); }
				});
				return transaction;
			}
		};
	}

	const factory = {
		open() {
			state.opens += 1;
			if (open === 'throw') throw new DOMException('The operation is insecure.', 'SecurityError');
			/** @type {any} */
			const request = { result: undefined, error: null, onupgradeneeded: null, onsuccess: null, onerror: null, onblocked: null };
			later(() => {
				if (open === 'hang') return;
				if (open === 'error') {
					request.error = new DOMException('The database could not be opened.', 'UnknownError');
					request.onerror?.();
					return;
				}
				if (open === 'blocked') {
					request.onblocked?.();
					return;
				}
				request.result = createDatabase();
				connections.push(request.result);
				if (stores.size === 0) request.onupgradeneeded?.();
				request.onsuccess?.();
			});
			return request;
		}
	};
	return { factory: /** @type {IDBFactory} */ (/** @type {unknown} */ (factory)), stores, state, connections };
}
