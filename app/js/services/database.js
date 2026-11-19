// The main collection: loaded at start-up, previewed and replaced by an import, shrunk by a
// deletion, and stored again after every change.
import { createQueue } from '../lib/queue.js';
import { createStore, readonly } from '../lib/store.js';
import { previewImport } from '../core/formats/import.js';

/** @typedef {import('../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../core/formats/import.js').ImportPreview} ImportPreview */

/**
 * what made the database what it is: the selection follows each in its own way
 * @typedef {'load' | 'import' | 'delete' | 'clear'} DatabaseChange
 */

/**
 * @typedef {object} DatabaseState
 * @property {ReadonlyArray<Comet>} comets in database order
 * @property {DatabaseChange} change the last change
 * @property {boolean} loaded whether the stored database has been read: until then the list is
 *   empty for want of it, not because nothing is stored
 */

/** what Import tells the user */
export const IMPORT_COMPLETE = 'Import complete';

/** what Import tells the user when the merged list is empty */
export const ZERO_IMPORTED = 'Something wrong happened. Zero comets imported.';

/**
 * the outcome of Import: `none` when the source has no format, so nothing was imported or said
 * @typedef {{ status: 'imported', message: string, count: number } | { status: 'empty', message: string } | { status: 'none' }} ImportOutcome
 */

/**
 * @typedef {'download' | 'file'} ImportSource where the text came from; only a download sets the last update
 */

/**
 * The changes wait for the load, so that they change the stored database and a load that lands
 * late cannot replace them; once it has landed they change the state at once, in the call.
 * @typedef {object} DatabaseService
 * @property {import('../lib/store.js').ReadableStore<Readonly<DatabaseState>>} state empty until load
 * @property {() => Promise<void>} load reads the stored database, once; a load that fails leaves it
 *   empty and loaded
 * @property {(text: string | null) => ImportPreview} preview a source merged into the database as it is now
 * @property {(text: string | null, source: ImportSource) => Promise<ImportOutcome>} apply Import; resolves once stored
 * @property {(comet: Comet) => Promise<void>} remove deletes the comet (this record); resolves once stored
 * @property {() => Promise<void>} clear empties the database and removes what is stored (Clear all data)
 */

/**
 * @param {object} deps
 * @param {import('./database-repository.js').DatabaseRepository} deps.repository
 * @param {import('./ports.js').Clock} deps.clock
 * @param {Pick<import('./settings.js').SettingsService, 'update'>} deps.settings for the last update
 * @returns {DatabaseService}
 */
export function createDatabaseService({ repository, clock, settings }) {
	const state = createStore(/** @type {Readonly<DatabaseState>} */ (Object.freeze({ comets: Object.freeze([]), change: 'load', loaded: false })));
	// writes in the order of the changes, so the last one is the one stored
	const queue = createQueue();
	/** @type {() => void} */
	let landed = () => {};
	/** settles once the load has landed, or failed */
	const whenLoaded = new Promise(resolve => { landed = () => resolve(undefined); });
	/** @type {Promise<void> | null} */
	let loading = null;

	/**
	 * every change but the load comes after it, so each state from here on is loaded
	 * @param {ReadonlyArray<Comet>} comets
	 * @param {DatabaseChange} change
	 */
	function set(comets, change) {
		state.set(Object.freeze({ comets, change, loaded: true }));
	}

	/**
	 * a change once the database has loaded: in this call when it has, which the Database view's
	 * Delete relies on, since it reads the list the change makes before the call returns
	 * @template T
	 * @param {() => Promise<T>} change
	 * @returns {Promise<T>}
	 */
	const afterLoad = change => (state.get().loaded ? change() : whenLoaded.then(change));

	/** the database as it is now, stored after what was changed before */
	const save = () => {
		const { comets } = state.get();
		return queue(() => repository.save(comets));
	};

	return Object.freeze({
		state: readonly(state),
		load() {
			loading ??= repository.load().then(
				comets => set(comets, 'load'),
				error => {
					set(Object.freeze([]), 'load');
					throw error;
				}
			).finally(landed);
			return loading;
		},
		preview: (/** @type {string | null} */ text) => previewImport(state.get().comets, text),
		apply: (/** @type {string | null} */ text, /** @type {ImportSource} */ source) => afterLoad(async () => {
			// merged now, against the database as it is now, not as it was when the preview was shown
			const { merge } = previewImport(state.get().comets, text);
			if (!merge) return Object.freeze({ status: 'none' });
			if (merge.comets.length === 0) return Object.freeze({ status: 'empty', message: ZERO_IMPORTED });
			set(merge.comets, 'import');
			await Promise.all([
				save(),
				source === 'download' ? settings.update({ lastUpdateDate: clock.now() }) : null
			]);
			return Object.freeze({ status: 'imported', message: IMPORT_COMPLETE, count: merge.comets.length });
		}),
		remove: (/** @type {Comet} */ comet) => afterLoad(async () => {
			const { comets } = state.get();
			const index = comets.indexOf(comet);
			if (index < 0) return;
			set(Object.freeze([...comets.slice(0, index), ...comets.slice(index + 1)]), 'delete');
			await save();
		}),
		clear: () => afterLoad(async () => {
			set(Object.freeze([]), 'clear');
			await queue(() => repository.clear());
		})
	});
}
