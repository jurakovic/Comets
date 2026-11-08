import { test } from 'node:test';
import assert from 'node:assert/strict';
import { chooseSort, compareNumbers, DEFAULT_SORT, SORT_PROPERTIES, sortComets, sortProperty } from '../../../../app/js/core/comets/sort.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { fieldValue } from '../../../../app/js/core/comets/properties.js';

/** @param {string} full @param {number} q @param {string} name */
const comet = (full, q, name) => {
	const c = createComet({ full, id: '1P', name, q, e: 0.5 });
	assert.ok(c);
	return c;
};
const list = [comet('a', 2, 'b'), comet('b', 1, 'a'), comet('c', 2, 'B'), comet('d', 1, 'á'), comet('e', 3, 'a')];
const fullNames = (/** @type {ReadonlyArray<{ full: string }>} */ comets) => comets.map(c => c.full).join('');

test('the sort menu of spec 02, and its default', () => {
	assert.deepEqual(SORT_PROPERTIES.map(p => p.label), [
		'Designation', 'Discoverer', 'Perihelion date', 'Perihelion distance', 'Perihelion distance from Earth',
		'Perihelion magnitude', 'Current distance from Sun', 'Current distance from Earth', 'Current magnitude',
		'Period', 'Aphelion distance', 'Semi-major axis', 'Eccentricity', 'Inclination', 'Long. of the Asc. Node',
		'Arg. of Pericenter'
	]);
	assert.deepEqual({ ...DEFAULT_SORT }, { property: 'sortkey', ascending: true });
	assert.throws(() => sortProperty('full'), /unknown sort property/);
});

test('equal values keep their order in both directions', () => {
	assert.equal(fullNames(sortComets(list, { property: 'q', ascending: true }, fieldValue)), 'bdace');
	assert.equal(fullNames(sortComets(list, { property: 'q', ascending: false }, fieldValue)), 'eacbd');
});

test('names compare with the root collation: case and accents after the letter', () => {
	assert.equal(fullNames(sortComets(list, { property: 'name', ascending: true }, fieldValue)), 'bedac');
	assert.equal(fullNames(sortComets(list, { property: 'name', ascending: false }, fieldValue)), 'cadbe');
});

test('the result is a new frozen list; the input is untouched', () => {
	const sorted = sortComets(list, DEFAULT_SORT, fieldValue);
	assert.ok(Object.isFrozen(sorted));
	assert.notEqual(sorted, list);
	assert.equal(fullNames(list), 'abcde');
});

test('numbers order as .NET\'s Double.CompareTo: NaN first', () => {
	const values = [3, NaN, -Infinity, 0, NaN, Infinity, -0];
	assert.deepEqual(values.toSorted(compareNumbers), [NaN, NaN, -Infinity, 0, -0, 3, Infinity]);
	const odd = [comet('x', 1, ''), createComet({ full: 'y', id: '1P', q: -1, e: 0.5 }), comet('z', 0, '')].map(c => { assert.ok(c); return c; });
	assert.ok(Number.isNaN(odd[1].P));
	assert.equal(fullNames(sortComets(odd, { property: 'P', ascending: true }, fieldValue)), 'yzx');
	assert.equal(fullNames(sortComets(odd, { property: 'P', ascending: false }, fieldValue)), 'xzy');
});

test('the sort menu\'s choices: another property keeps the direction, the same one does nothing', () => {
	const current = { property: 'q', ascending: false };
	assert.deepEqual({ ...chooseSort(current, { property: 'name' }) }, { property: 'name', ascending: false });
	assert.equal(chooseSort(current, { property: 'q' }), null);
	assert.deepEqual({ ...chooseSort(current, { ascending: true }) }, { property: 'q', ascending: true });
	// the direction already chosen sorts again, where the desktop's menu flipped it (spec 10, D19)
	assert.deepEqual({ ...chooseSort(current, { ascending: false }) }, { property: 'q', ascending: false });
	assert.ok(Object.isFrozen(chooseSort(current, { ascending: true })));
	assert.throws(() => chooseSort(current, { property: 'full' }), /unknown sort property/);
});
