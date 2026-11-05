// filters.json: 27 filter sets over the 963 comets of the MPC snapshot, at the vector clock for
// the observer at 0, 0 (spec 02, Filters).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock } from '../helpers/fakes.js';
import { readVector, readVectorText, VECTOR_NOW } from '../helpers/vectors.js';
import { applyFilters, validateFilters } from '../../app/js/core/comets/filter.js';
import { COMPARES_BY_TYPE } from '../../app/js/core/comets/compares.js';
import { property } from '../../app/js/core/comets/properties.js';
import { importElements } from '../../app/js/core/formats/import.js';
import { createObservationsService } from '../../app/js/services/observations.js';

const vector = readVector('filters.json');
const database = importElements([], 'MPC', readVectorText(vector.input)).comets;
const observations = createObservationsService({ clock: createFakeClock(VECTOR_NOW), observer: () => ({ latitude: 0, longitude: 0 }) });

/** a vector's filter row, its compare given by its index in the desktop's menu */
function fromVector(/** @type {any} */ row) {
	const compares = COMPARES_BY_TYPE[property(row.property).type];
	assert.equal(row.dataType, compares.length === 4 ? 'String' : 'Double', row.property);
	return { property: row.property, checked: row.checked, text: row.text, compare: compares[row.compare].key };
}

test('the snapshot has 963 comets', () => {
	assert.equal(database.length, 963);
});

for (const { title, filters, result } of vector.cases) {
	test(title, () => {
		const rows = filters.map(fromVector);
		assert.ok(validateFilters(rows).ok);
		assert.deepEqual(applyFilters(database, rows, observations.value).map(c => c.full), result);
	});
}

test('27 cases', () => {
	assert.equal(vector.cases.length, 27);
});
