// An observable value with one owner: read it, change it, follow it.
// See docs/spec/12-architecture.md, State and change.

/**
 * @template T
 * @typedef {object} Store
 * @property {() => T} get the current value
 * @property {(next: T) => void} set replaces the value; listeners hear of it only when it differs (Object.is)
 * @property {(change: (current: T) => T) => void} update sets what change makes of the current value
 * @property {(listener: (value: T) => void) => () => void} subscribe calls listener after every change; returns the unsubscribe
 */

/**
 * @template T
 * @typedef {Pick<Store<T>, 'get' | 'subscribe'>} ReadableStore what an owner hands out: read and follow, not change
 */

/**
 * @template T
 * @param {T} initial
 * @returns {Store<T>}
 */
export function createStore(initial) {
	let value = initial;
	/** @type {Set<(value: T) => void>} */
	const listeners = new Set();

	/** @param {T} next */
	function set(next) {
		if (Object.is(next, value)) return;
		value = next;
		// a copy: a listener may unsubscribe itself or another one
		for (const listener of [...listeners]) listener(value);
	}

	return Object.freeze({
		get: () => value,
		set,
		update: (/** @type {(current: T) => T} */ change) => set(change(value)),
		subscribe(/** @type {(value: T) => void} */ listener) {
			listeners.add(listener);
			return () => { listeners.delete(listener); };
		}
	});
}

/**
 * the read side of a store, for handing out
 * @template T
 * @param {Store<T>} store
 * @returns {ReadableStore<T>}
 */
export function readonly(store) {
	return Object.freeze({ get: store.get, subscribe: store.subscribe });
}
