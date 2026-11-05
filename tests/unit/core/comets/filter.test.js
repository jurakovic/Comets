import { test } from 'node:test';
import assert from 'node:assert/strict';
import { applyFilters, createFilter, filterNumber, validateFilters } from '../../../../app/js/core/comets/filter.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { fieldValue } from '../../../../app/js/core/comets/properties.js';

const comets = [
	createComet({ full: '1P/Halley', id: '1P', name: 'Halley', q: 0.586, e: 0.967, i: 162 }),
	createComet({ full: '2P/Encke', id: '2P', name: 'Encke', q: 0.336, e: 0.847, i: 11.5 }),
	createComet({ full: 'C/1995 O1 (Hale-Bopp)', id: 'C/1995 O1', name: 'Hale-Bopp', q: 0.914, e: 0.995, i: 89 })
].map(c => { assert.ok(c); return c; });

const fullNames = (/** @type {ReadonlyArray<import('../../../../app/js/core/comets/comet.js').Comet>} */ list) => list.map(c => c.full);

test('a new row has the property\'s default compare and is unchecked and empty', () => {
	assert.deepEqual({ ...createFilter('full') }, { property: 'full', checked: false, text: '', compare: 'contains' });
	assert.deepEqual({ ...createFilter('q') }, { property: 'q', checked: false, text: '', compare: 'lessThan' });
	assert.deepEqual({ ...createFilter('Tn', { checked: true, text: '2461332.5' }) }, { property: 'Tn', checked: true, text: '2461332.5', compare: 'greaterThan' });
	assert.ok(Object.isFrozen(createFilter('e')));
});

test('with no checked row the result is a copy of the list', () => {
	const result = applyFilters(comets, [createFilter('full', { text: 'halley' })], fieldValue);
	assert.deepEqual(result, comets);
	assert.notEqual(result, comets);
	assert.ok(Object.isFrozen(result));
});

test('every checked row must pass, and the list order is kept', () => {
	const rows = [
		createFilter('full', { checked: true, text: 'p/, hale', compare: 'contains' }),
		createFilter('q', { checked: true, text: '0.9' })
	];
	assert.deepEqual(fullNames(applyFilters(comets, rows, fieldValue)), ['1P/Halley', '2P/Encke']);
});

test('text compares lower-case the value and the terms, and trim the terms', () => {
	const row = createFilter('name', { checked: true, text: '  ENCKE ,HALE-', compare: 'startsWith' });
	assert.deepEqual(fullNames(applyFilters(comets, [row], fieldValue)), ['2P/Encke', 'C/1995 O1 (Hale-Bopp)']);
});

test('an empty term matches every comet (spec 10, Q16)', () => {
	const row = createFilter('full', { checked: true, text: 'nothing,', compare: 'contains' });
	assert.equal(applyFilters(comets, [row], fieldValue).length, 3);
});

test('a number row reads its text as .NET does, and as 0 when it does not read', () => {
	assert.equal(filterNumber(' 1,000.5 '), 1000.5);
	assert.equal(filterNumber(' 0.5 '), 0.5);
	assert.equal(filterNumber('1e2'), 100);
	assert.equal(filterNumber('abc'), 0);
	assert.equal(filterNumber('0,5'), 5);
	const row = createFilter('q', { checked: true, text: 'x', compare: 'greaterThan' });
	assert.equal(applyFilters(comets, [row], fieldValue).length, 3);
});

test('Equals takes the property\'s tolerance, inclusive', () => {
	const row = createFilter('e', { checked: true, text: '0.84', compare: 'equals' });
	assert.deepEqual(fullNames(applyFilters(comets, [row], fieldValue)), ['2P/Encke']);
	// i within 1°: 90 − 89 = 1 passes, 162 does not
	const edge = createFilter('i', { checked: true, text: '90', compare: 'equals' });
	assert.deepEqual(fullNames(applyFilters(comets, [edge], fieldValue)), ['C/1995 O1 (Hale-Bopp)']);
	const past = createFilter('i', { checked: true, text: '12.5001', compare: 'equals' });
	assert.deepEqual(fullNames(applyFilters(comets, [past], fieldValue)), []);
});

test('session values come through the accessor', () => {
	const accessor = (/** @type {any} */ comet, /** @type {string} */ key) => key === 'CurrentMag' ? comet.q * 10 : fieldValue(comet, key);
	const row = createFilter('CurrentMag', { checked: true, text: '5' });
	assert.deepEqual(fullNames(applyFilters(comets, [row], accessor)), ['2P/Encke']);
});

test('validation: the first checked row without text, by its label', () => {
	const rows = [
		createFilter('full', { checked: false }),
		createFilter('q', { checked: true, text: ' \t' }),
		createFilter('name', { checked: true })
	];
	const result = validateFilters(rows);
	assert.equal(result.ok, false);
	assert.deepEqual(!result.ok && result.errors, [{ field: 'text', index: 1, message: 'Please enter value for "Perihelion distance"' }]);
	assert.deepEqual(validateFilters(rows.slice(0, 1)), { ok: true, value: rows.slice(0, 1) });
	const ok = validateFilters([createFilter('name', { checked: true, text: 'x' })]);
	assert.equal(ok.ok, true);
});
