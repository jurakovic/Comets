import { test } from 'node:test';
import assert from 'node:assert/strict';
import { indexAfterChange, QUICK_SELECTIONS, quickSelect, rebindSelection } from '../../../../app/js/core/comets/selection.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { fieldValue } from '../../../../app/js/core/comets/properties.js';

/** @typedef {import('../../../../app/js/core/comets/comet.js').Comet} Comet */

/** @param {string} full @param {number} q */
const comet = (full, q) => {
	const c = createComet({ full, id: '1P', q, e: 0.5 });
	assert.ok(c);
	return c;
};
const [a, b, c] = [comet('a', 3), comet('b', 1), comet('c', 1)];
/** the session values from fixed tables, for the sake of the test */
const values = new Map([[a, { CurrentMag: 9, CurrentEarthDist: 0.5, CurrentSunDist: 2, Tn: 100 }], [b, { CurrentMag: 5, CurrentEarthDist: 1, CurrentSunDist: 1, Tn: 90 }], [c, { CurrentMag: 5, CurrentEarthDist: 2, CurrentSunDist: 1, Tn: 111 }]]);
const value = (/** @type {Comet} */ comet, /** @type {string} */ key) => /** @type {any} */ (values.get(comet))?.[key] ?? fieldValue(comet, key);

test('the quick-select menu', () => {
	assert.deepEqual(QUICK_SELECTIONS.map(s => s.label), ['Brightest', 'Closest to perihelion', 'Closest to Earth', 'Closest to Sun']);
});

test('each picks the smallest, the first in list order on a tie', () => {
	const list = [a, b, c];
	assert.equal(quickSelect(list, 'Brightest', value, 0), b);
	assert.equal(quickSelect([a, c, b], 'Brightest', value, 0), c);
	assert.equal(quickSelect(list, 'ClosestToEarth', value, 0), a);
	assert.equal(quickSelect(list, 'ClosestToSun', value, 0), b);
	// |100 − 105| = 5 against |111 − 105| = 6 and |90 − 105| = 15
	assert.equal(quickSelect(list, 'ClosestToPerihelion', value, 105), a);
	// a tie at 105.5: a first
	assert.equal(quickSelect(list, 'ClosestToPerihelion', value, 105.5), a);
	assert.equal(quickSelect([c, a], 'ClosestToPerihelion', value, 105.5), c);
});

test('an empty list selects nothing; an unknown selection is a programmer error', () => {
	assert.equal(quickSelect([], 'Brightest', value, 0), null);
	assert.throws(() => quickSelect([a], 'Faintest', value, 0), /unknown quick selection/);
});

test('rebinding keeps the selected comet when an equal one is in the new list', () => {
	const again = comet('a', 3);
	assert.notEqual(again, a);
	values.set(again, { CurrentMag: 9, CurrentEarthDist: 0.5, CurrentSunDist: 2, Tn: 100 });
	assert.equal(rebindSelection([b, again, c], a, value, 0), again);
});

test('otherwise it selects the brightest', () => {
	const changed = comet('a', 3.5);
	assert.equal(rebindSelection([a, b, c], changed, value, 0), b);
	assert.equal(rebindSelection([a, c], null, value, 0), c);
	assert.equal(rebindSelection([], a, value, 0), null);
});

test('the index kept after the list changes', () => {
	assert.equal(indexAfterChange([a, b, c], b, 0), 1, 'the selected comet, where it is now');
	assert.equal(indexAfterChange([c, a], b, 0), 0, 'gone: the index given');
	assert.equal(indexAfterChange([a, c], null, 1), 1, 'after Delete: the same index');
	assert.equal(indexAfterChange([a, c], null, 2), 1, 'clamped to the list');
	assert.equal(indexAfterChange([], a, 0), -1);
	// an import replaces the records: the comet is found again by its full name
	const [a2, b2, c2] = [comet('a', 3), comet('b', 1), comet('c', 1)];
	assert.notEqual(c2, c);
	assert.equal(indexAfterChange([a2, b2, c2], c, 0), 2);
	assert.equal(indexAfterChange([a], null, -1), 0);
});
