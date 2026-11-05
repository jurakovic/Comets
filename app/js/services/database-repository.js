// The stored database: the MPC text of the main collection, written whole and read back as an MPC
// import into an empty database. docs/spec/05-database-and-update.md, Storage.
import { exportElements } from '../core/formats/export.js';
import { importElements } from '../core/formats/import.js';

/** @typedef {import('../core/comets/comet.js').Comet} Comet */

/** the database's key in its IndexedDB object store (spec 09, Storage layout) */
export const DATABASE_KEY = 'database';

const FORMAT = 'MPC';

/**
 * @typedef {object} DatabaseRepository
 * @property {() => Promise<ReadonlyArray<Comet>>} load the stored comets in database order; none
 *   when nothing is stored
 * @property {(comets: ReadonlyArray<Comet>) => Promise<void>} save replaces the stored text in one write
 * @property {() => Promise<string | undefined>} text the stored MPC text, for Export database
 * @property {() => Promise<void>} clear removes it (Clear all data)
 */

/**
 * @param {object} deps
 * @param {import('./ports.js').KeyValueStore} deps.store IndexedDB
 * @param {import('./ports.js').Clock} deps.clock the MPC text holds today's date (spec 10, Q12)
 * @returns {DatabaseRepository}
 */
export function createDatabaseRepository({ store, clock }) {
	return Object.freeze({
		async load() {
			const text = await store.get(DATABASE_KEY);
			return text === undefined ? Object.freeze([]) : importElements([], FORMAT, text).comets;
		},
		save: (/** @type {ReadonlyArray<Comet>} */ comets) => store.put(DATABASE_KEY, exportElements(FORMAT, comets, clock.now())),
		text: () => store.get(DATABASE_KEY),
		clear: () => store.remove(DATABASE_KEY)
	});
}
