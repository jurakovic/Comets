// selection.json: quick selection over the sample collection at the vector clock (spec 02, Quick
// selection).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock } from '../helpers/fakes.js';
import { readVector, readVectorText, VECTOR_NOW } from '../helpers/vectors.js';
import { QUICK_SELECTIONS, quickSelect } from '../../app/js/core/comets/selection.js';
import { importElements } from '../../app/js/core/formats/import.js';
import { instantToJd } from '../../app/js/core/time/julian.js';
import { createObservationsService } from '../../app/js/services/observations.js';

const { cases } = readVector('selection.json');
const sample = importElements([], 'MPC', readVectorText('import/sample.txt')).comets;
const observations = createObservationsService({ clock: createFakeClock(VECTOR_NOW), observer: () => ({ latitude: 0, longitude: 0 }) });

test('a case for every menu item, in menu order', () => {
	assert.deepEqual(cases.map((/** @type {any} */ c) => c.type), QUICK_SELECTIONS.map(s => s.key));
});

for (const { type, comet } of cases) {
	test(type, () => {
		assert.equal(quickSelect(sample, type, observations.value, instantToJd(VECTOR_NOW))?.full, comet);
	});
}
