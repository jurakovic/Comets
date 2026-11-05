// sorting.json: the sample collection stably sorted by each of the 16 sort properties, both
// directions, at the vector clock (spec 02, Sorting).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock } from '../helpers/fakes.js';
import { readVector, readVectorText, VECTOR_NOW } from '../helpers/vectors.js';
import { SORT_PROPERTIES, sortComets } from '../../app/js/core/comets/sort.js';
import { importElements } from '../../app/js/core/formats/import.js';
import { createObservationsService } from '../../app/js/services/observations.js';

const vector = readVector('sorting.json');
const sample = importElements([], 'MPC', readVectorText('import/sample.txt')).comets;
const observations = createObservationsService({ clock: createFakeClock(VECTOR_NOW), observer: () => ({ latitude: 0, longitude: 0 }) });

test('the input is the sample in database order', () => {
	assert.deepEqual(sample.map(c => c.full), vector.input);
});

test('every sort property, both directions', () => {
	assert.deepEqual(vector.cases.map((/** @type {any} */ c) => `${c.property} ${c.ascending}`),
		SORT_PROPERTIES.flatMap(p => [`${p.key} true`, `${p.key} false`]));
});

for (const { property, ascending, result } of vector.cases) {
	test(`${property} ${ascending ? 'ascending' : 'descending'}`, () => {
		assert.deepEqual(sortComets(sample, { property, ascending }, observations.value).map(c => c.full), result);
	});
}
