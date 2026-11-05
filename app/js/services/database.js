// The main collection: loaded at start-up, previewed and replaced by an import, shrunk by a
// deletion, and stored again after every change. docs/spec/05-database-and-update.md.
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
 * @typedef {object} DatabaseService
 * @property {import('../lib/store.js').ReadableStore<Readonly<DatabaseState>>} state empty until load
 * @property {() => Promise<void>} load reads the stored database
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
	const state = createStore(/** @type {Readonly<DatabaseState>} */ (Object.freeze({ comets: Object.freeze([]), change: 'load' })));
	// writes in the order of the changes, so the last one is the one stored
	/** @type {Promise<void>} */
	let writing = Promise.resolve();

	/**
	 * @param {ReadonlyArray<Comet>} comets
	 * @param {DatabaseChange} change
	 */
	function set(comets, change) {
		state.set(Object.freeze({ comets, change }));
	}

	/** the database as it is now, stored after what was changed before (spec 10, D10) */
	const save = () => {
		const { comets } = state.get();
		writing = writing.then(() => repository.save(comets));
		return writing;
	};

	return Object.freeze({
		state: readonly(state),
		async load() {
			set(await repository.load(), 'load');
		},
		preview: (/** @type {string | null} */ text) => previewImport(state.get().comets, text),
		async apply(/** @type {string | null} */ text, /** @type {ImportSource} */ source) {
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
		},
		async remove(/** @type {Comet} */ comet) {
			const { comets } = state.get();
			const index = comets.indexOf(comet);
			if (index < 0) return;
			set(Object.freeze([...comets.slice(0, index), ...comets.slice(index + 1)]), 'delete');
			await save();
		},
		async clear() {
			set(Object.freeze([]), 'clear');
			writing = writing.then(() => repository.clear());
			await writing;
		}
	});
}
