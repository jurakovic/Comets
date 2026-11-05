// The KeyValueStore port over IndexedDB, localStorage and memory. Storage that is missing, full,
// blocked or broken never stops the app: what it refused is kept in memory for the session.
// docs/spec/09-settings-and-persistence.md, Storage layout.

/** @typedef {import('../services/ports.js').KeyValueStore} KeyValueStore */

/**
 * storage that may throw or reject
 * @typedef {object} RawStore
 * @property {(key: string) => Promise<string | undefined>} get
 * @property {(key: string, value: string) => Promise<void>} put
 * @property {(key: string) => Promise<void>} remove
 */

/**
 * values in memory only, for tests and as the last resort
 * @param {Iterable<[string, string]>} [entries]
 * @returns {KeyValueStore}
 */
export function createMemoryStore(entries = []) {
	const values = new Map(entries);
	return Object.freeze({
		get: async (/** @type {string} */ key) => values.get(key),
		put: async (/** @type {string} */ key, /** @type {string} */ value) => { values.set(key, value); },
		remove: async (/** @type {string} */ key) => { values.delete(key); }
	});
}

/**
 * a store that never fails: a write or removal the storage refuses is kept in memory, and wins
 * over the storage until a later write of the same key gets through; a read that fails gives
 * undefined
 * @param {RawStore} raw
 * @param {string} what names the storage in the one warning logged
 * @returns {KeyValueStore}
 */
export function tolerant(raw, what) {
	// key → the session's value where storage refused it (undefined: removed)
	/** @type {Map<string, string | undefined>} */
	const kept = new Map();
	let warned = false;
	const warn = (/** @type {unknown} */ error) => {
		if (warned) return;
		warned = true;
		console.warn(`${what} failed; keeping data in memory for this session`, error);
	};
	return Object.freeze({
		async get(/** @type {string} */ key) {
			if (kept.has(key)) return kept.get(key);
			try {
				return await raw.get(key);
			} catch (error) {
				warn(error);
				return undefined;
			}
		},
		async put(/** @type {string} */ key, /** @type {string} */ value) {
			try {
				await raw.put(key, value);
				kept.delete(key);
			} catch (error) {
				warn(error);
				kept.set(key, value);
			}
		},
		async remove(/** @type {string} */ key) {
			try {
				await raw.remove(key);
				kept.delete(key);
			} catch (error) {
				warn(error);
				kept.set(key, undefined);
			}
		}
	});
}

/**
 * Web Storage (localStorage). Reaching it can throw where the browser blocks site data, so it is
 * asked for on each use.
 * @param {() => Storage} storage
 * @returns {KeyValueStore}
 */
export function createWebStorageStore(storage) {
	return tolerant({
		get: async key => storage().getItem(key) ?? undefined,
		put: async (key, value) => storage().setItem(key, value),
		remove: async key => storage().removeItem(key)
	}, 'localStorage');
}

/**
 * IndexedDB: one object store of text values in its own database, opened on first use. A database
 * that fails to open, is blocked by another tab or does not answer within timeout fails every
 * later call of the session, which the memory then serves.
 * @param {object} options
 * @param {() => IDBFactory} options.factory reaching it can throw, as for localStorage
 * @param {string} options.database
 * @param {string} options.store
 * @param {number} [options.timeout] milliseconds to wait for the database to open
 * @returns {KeyValueStore}
 */
export function createIndexedDbStore({ factory, database, store, timeout = 5000 }) {
	/** @type {Promise<IDBDatabase> | null} */
	let opening = null;

	function open() {
		opening ??= new Promise((resolve, reject) => {
			const request = factory().open(database, 1);
			let settled = false;
			/** @param {() => void} settle */
			const once = settle => {
				if (settled) return false;
				settled = true;
				clearTimeout(timer);
				settle();
				return true;
			};
			const timer = setTimeout(() => once(() => reject(new Error(`IndexedDB ${database} did not open`))), timeout);
			request.onupgradeneeded = () => { request.result.createObjectStore(store); };
			request.onsuccess = () => {
				const db = request.result;
				// opened after the timeout: nobody waits for it
				if (!once(() => resolve(db))) {
					db.close();
					return;
				}
				// a newer version in another tab waits for this connection to close
				db.onversionchange = () => db.close();
			};
			request.onerror = () => { once(() => reject(request.error)); };
			request.onblocked = () => { once(() => reject(new Error(`IndexedDB ${database} is blocked`))); };
		});
		return opening;
	}

	/**
	 * @template T
	 * @param {IDBTransactionMode} mode
	 * @param {(objects: IDBObjectStore) => IDBRequest<T>} act
	 * @returns {Promise<T>} once the transaction has committed
	 */
	async function transact(mode, act) {
		const db = await open();
		return new Promise((resolve, reject) => {
			const transaction = db.transaction(store, mode);
			const request = act(transaction.objectStore(store));
			transaction.oncomplete = () => resolve(request.result);
			transaction.onerror = () => reject(transaction.error);
			transaction.onabort = () => reject(transaction.error ?? new Error('the transaction was aborted'));
		});
	}

	return tolerant({
		async get(key) {
			const value = await transact('readonly', objects => objects.get(key));
			return typeof value === 'string' ? value : undefined;
		},
		// one put of the whole value: a reader sees the old text or the new one, never a mix
		put: async (key, value) => { await transact('readwrite', objects => objects.put(value, key)); },
		remove: async key => { await transact('readwrite', objects => objects.delete(key)); }
	}, 'IndexedDB');
}
