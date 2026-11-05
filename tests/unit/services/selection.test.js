import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock } from '../../helpers/fakes.js';
import { readVector, readVectorText, VECTOR_NOW } from '../../helpers/vectors.js';
import { createStore } from '../../../app/js/lib/store.js';
import { COMPARES_BY_TYPE } from '../../../app/js/core/comets/compares.js';
import { createFilter, defaultFilters } from '../../../app/js/core/comets/filter.js';
import { property } from '../../../app/js/core/comets/properties.js';
import { DEFAULT_SORT, sortComets } from '../../../app/js/core/comets/sort.js';
import { importElements } from '../../../app/js/core/formats/import.js';
import { createObservationsService } from '../../../app/js/services/observations.js';
import { createSelectionService } from '../../../app/js/services/selection.js';

/** @typedef {import('../../../app/js/services/database.js').DatabaseState} DatabaseState */

const filtersVector = readVector('filters.json');
const full = importElements([], 'MPC', readVectorText(filtersVector.input)).comets;
const sample = importElements([], 'MPC', readVectorText('import/sample.txt')).comets;
const observations = createObservationsService({ clock: createFakeClock(VECTOR_NOW), observer: () => ({ latitude: 0, longitude: 0 }) });
const names = (/** @type {ReadonlyArray<{ full: string }>} */ comets) => comets.map(c => c.full);

/** a selection over a database store the test changes */
function setup(/** @type {ReadonlyArray<any>} */ comets = []) {
	const clock = createFakeClock(VECTOR_NOW);
	const database = createStore(/** @type {Readonly<DatabaseState>} */ ({ comets, change: 'load' }));
	const selection = createSelectionService({ database, value: observations.value, clock });
	return { clock, database, selection };
}

/** a vector's filter row, its compare given by its index in the desktop's menu */
function fromVector(/** @type {any} */ row) {
	return createFilter(row.property, { checked: row.checked, text: row.text, compare: COMPARES_BY_TYPE[property(row.property).type][row.compare].key });
}

test('it starts with the default rows and sort over the database', () => {
	const { selection } = setup(sample);
	const state = selection.state.get();
	assert.deepEqual(state.filters, defaultFilters(VECTOR_NOW));
	assert.deepEqual(state.sort, DEFAULT_SORT);
	assert.deepEqual(names(state.comets), names(sample));
	assert.notEqual(state.comets, sample, 'its own list');
	assert.equal(state.total, 34);
});

test('every filters.json case through the service, in the default sort', () => {
	const { database, selection } = setup();
	database.set({ comets: full, change: 'load' });
	assert.equal(selection.state.get().total, 963);
	for (const { title, filters, result } of filtersVector.cases) {
		const rows = filters.map(fromVector);
		assert.equal(selection.filter(rows).ok, true, title);
		assert.deepEqual(names(selection.state.get().comets), result, title);
		assert.deepEqual(selection.state.get().filters, rows);
	}
});

test('filters apply to the whole database and keep the sort', () => {
	const { selection } = setup(sample);
	selection.sort({ property: 'q', ascending: true });
	selection.filter([createFilter('q', { checked: true, text: '1' })]);
	const expected = sortComets(sample.filter(c => c.q < 1), { property: 'q', ascending: true }, observations.value);
	assert.deepEqual(names(selection.state.get().comets), names(expected));
	selection.filter([createFilter('q', { checked: true, text: '2' })]);
	assert.equal(selection.state.get().comets.length, sample.filter(c => c.q < 2).length, 'not only the comets shown before');
});

test('rows that do not validate leave the selection as it was', () => {
	const { selection } = setup(sample);
	selection.filter([createFilter('name', { checked: true, text: 'Halley' })]);
	const before = selection.state.get();
	const result = selection.filter([createFilter('full'), createFilter('q', { checked: true, text: '  ' })]);
	assert.deepEqual(result, { ok: false, errors: [{ field: 'text', index: 1, message: 'Please enter value for "Perihelion distance"' }] });
	assert.equal(selection.state.get(), before);
});

test('a sort re-sorts the selection as it is: equal comets keep the order of the sort before', () => {
	const { selection } = setup(full);
	selection.sort({ property: 'e', ascending: false });
	selection.sort({ property: 'name', ascending: true });
	const byEThenName = sortComets(sortComets(full, { property: 'e', ascending: false }, observations.value), { property: 'name', ascending: true }, observations.value);
	const byName = sortComets(full, { property: 'name', ascending: true }, observations.value);
	assert.notDeepEqual(names(byEThenName), names(byName), 'the case tells the two apart');
	assert.deepEqual(names(selection.state.get().comets), names(byEThenName));
	assert.deepEqual(selection.state.get().sort, { property: 'name', ascending: true });
});

test('an import clears the filters and applies the current sort to the new database (spec 10, D9)', () => {
	const { database, selection } = setup(sample);
	selection.sort({ property: 'q', ascending: false });
	selection.filter([createFilter('name', { checked: true, text: 'Halley' })]);
	const merged = importElements(sample, 'MPC', readVectorText('import/merge-new.txt')).comets;
	database.set({ comets: merged, change: 'import' });
	const state = selection.state.get();
	assert.deepEqual(state.filters, defaultFilters(VECTOR_NOW));
	assert.deepEqual(state.sort, { property: 'q', ascending: false });
	assert.deepEqual(names(state.comets), names(sortComets(merged, { property: 'q', ascending: false }, observations.value)));
	assert.equal(state.total, 35);
});

test('the load and Clear all data start the filters afresh too', () => {
	const { database, selection } = setup(sample);
	selection.filter([createFilter('name', { checked: true, text: 'Halley' })]);
	database.set({ comets: [], change: 'clear' });
	assert.deepEqual(selection.state.get().comets, []);
	assert.deepEqual(selection.state.get().filters, defaultFilters(VECTOR_NOW));
	selection.filter([createFilter('name', { checked: true, text: 'Halley' })]);
	database.set({ comets: sample, change: 'load' });
	assert.equal(selection.state.get().comets.length, 34);
});

test('a deletion takes the comet out of the selection and keeps the filters', () => {
	const { database, selection } = setup(sample);
	const rows = [createFilter('q', { checked: true, text: '1' })];
	selection.filter(rows);
	selection.sort({ property: 'q', ascending: true });
	const before = selection.state.get().comets;
	const gone = before[2];
	database.set({ comets: sample.filter(c => c !== gone), change: 'delete' });
	const state = selection.state.get();
	assert.deepEqual(state.comets, before.filter(c => c !== gone));
	assert.deepEqual(state.filters, rows);
	assert.equal(state.total, 33);
});

test('a deletion of a comet the filters hide changes only the total', () => {
	const { database, selection } = setup(sample);
	selection.filter([createFilter('q', { checked: true, text: '1' })]);
	const before = selection.state.get().comets;
	const hidden = sample.find(c => c.q >= 1);
	database.set({ comets: sample.filter(c => c !== hidden), change: 'delete' });
	assert.deepEqual(selection.state.get().comets, before);
	assert.equal(selection.state.get().total, 33);
});

test('Reset: the default rows and sort over the whole database', () => {
	const { selection, clock } = setup(sample);
	selection.filter([createFilter('name', { checked: true, text: 'Halley' })]);
	selection.sort({ property: 'q', ascending: false });
	clock.advance(60 * 86_400_000);
	selection.reset();
	const state = selection.state.get();
	assert.deepEqual(state.filters, defaultFilters(VECTOR_NOW + 60 * 86_400_000), 'the default date of now');
	assert.deepEqual(state.sort, DEFAULT_SORT);
	assert.deepEqual(names(state.comets), names(sample));
});

test('the state is frozen and changes only by its owner', () => {
	const { selection } = setup(sample);
	const state = selection.state.get();
	assert.ok(Object.isFrozen(state) && Object.isFrozen(state.comets) && Object.isFrozen(state.filters));
	assert.equal('set' in selection.state, false);
});
