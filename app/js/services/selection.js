// The app-wide selection: the database through the user's filters, in the user's sort, followed
// as the database changes. docs/spec/02-data-model.md, Collections; docs/spec/05-database-and-update.md.
import { createStore, readonly } from '../lib/store.js';
import { applyFilters, defaultFilters, validateFilters } from '../core/comets/filter.js';
import { DEFAULT_SORT, sortComets } from '../core/comets/sort.js';

/** @typedef {import('../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../core/comets/filter.js').Filter} Filter */
/** @typedef {import('../core/comets/sort.js').Sort} Sort */
/** @typedef {import('./database.js').DatabaseState} DatabaseState */

/**
 * @typedef {object} Selection
 * @property {ReadonlyArray<Filter>} filters the rows last applied
 * @property {Readonly<Sort>} sort
 * @property {ReadonlyArray<Comet>} comets the selection, in its order
 * @property {number} total the database's size
 */

/**
 * @typedef {object} SelectionService
 * @property {import('../lib/store.js').ReadableStore<Readonly<Selection>>} state
 * @property {(filters: ReadonlyArray<Filter>) => import('../core/validation.js').Validation<ReadonlyArray<Filter>>} filter
 *   applies filter rows to the whole database; rows that do not validate leave the selection as it was
 * @property {(sort: Readonly<Sort>) => void} sort re-sorts the selection as it is, so equal comets keep their order
 * @property {() => void} reset the default rows and sort over the whole database
 */

/**
 * @param {object} deps
 * @param {import('../lib/store.js').ReadableStore<Readonly<DatabaseState>>} deps.database
 * @param {import('../core/comets/properties.js').ValueAccessor} deps.value the Observations service's
 * @param {import('./ports.js').Clock} deps.clock the default rows' Perihelion date is the default start date
 * @returns {SelectionService}
 */
export function createSelectionService({ database, value, clock }) {
	/**
	 * the database through filters, sorted
	 * @param {ReadonlyArray<Filter>} filters
	 * @param {Readonly<Sort>} sort
	 * @returns {Readonly<Selection>}
	 */
	function select(filters, sort) {
		const { comets } = database.get();
		return Object.freeze({ filters, sort, comets: sortComets(applyFilters(comets, filters, value), sort, value), total: comets.length });
	}

	const state = createStore(select(defaultFilters(clock.now()), DEFAULT_SORT));

	/**
	 * how the selection follows each change of the database: an import (and the load, and Clear
	 * all data) clears the filters and keeps the sort (spec 10, D9); a deletion takes the comet
	 * out of the selection, which is sorted again, as on the desktop
	 * @type {Readonly<Record<import('./database.js').DatabaseChange, (current: Readonly<Selection>) => Readonly<Selection>>>}
	 */
	const FOLLOW = Object.freeze({
		load: current => select(defaultFilters(clock.now()), current.sort),
		import: current => select(defaultFilters(clock.now()), current.sort),
		clear: current => select(defaultFilters(clock.now()), current.sort),
		delete(current) {
			const { comets } = database.get();
			const kept = new Set(comets);
			const remaining = current.comets.filter(comet => kept.has(comet));
			return Object.freeze({ ...current, comets: sortComets(remaining, current.sort, value), total: comets.length });
		}
	});

	database.subscribe(({ change }) => state.update(FOLLOW[change]));

	return Object.freeze({
		state: readonly(state),
		filter(/** @type {ReadonlyArray<Filter>} */ filters) {
			const validation = validateFilters(filters);
			if (validation.ok) state.set(select(Object.freeze([...filters]), state.get().sort));
			return validation;
		},
		sort(/** @type {Readonly<Sort>} */ sort) {
			state.update(current => Object.freeze({ ...current, sort: Object.freeze({ ...sort }), comets: sortComets(current.comets, sort, value) }));
		},
		reset() {
			state.set(select(defaultFilters(clock.now()), DEFAULT_SORT));
		}
	});
}
