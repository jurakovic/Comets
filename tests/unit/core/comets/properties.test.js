import { test } from 'node:test';
import assert from 'node:assert/strict';
import { fieldValue, PROPERTIES, property, SESSION_KEYS } from '../../../../app/js/core/comets/properties.js';
import { COMPARES_BY_TYPE, numberCompare, textCompare } from '../../../../app/js/core/comets/compares.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';

test('the 17 properties in the filter menu order, with the labels of spec 02', () => {
	assert.deepEqual(PROPERTIES.map(p => [p.key, p.label]), [
		['full', 'Full name'], ['name', 'Discoverer'], ['id', 'Designation'], ['Tn', 'Perihelion date'],
		['q', 'Perihelion distance'], ['PerihEarthDist', 'Perihelion distance from Earth'],
		['PerihMag', 'Perihelion magnitude'], ['CurrentSunDist', 'Current distance from Sun'],
		['CurrentEarthDist', 'Current distance from Earth'], ['CurrentMag', 'Current magnitude'],
		['P', 'Period'], ['Q', 'Aphelion distance'], ['a', 'Semi-major axis'], ['e', 'Eccentricity'],
		['i', 'Inclination'], ['N', 'Longitude of Ascending Node'], ['w', 'Argument of Pericenter']
	]);
	assert.ok(Object.isFrozen(PROPERTIES) && PROPERTIES.every(p => Object.isFrozen(p)));
});

test('types, units, tolerances, input ranges and default compares as spec 02 lists them', () => {
	const row = (/** @type {string} */ key) => {
		const p = property(key);
		return [p.type, p.unit, p.tolerance, p.input && [p.input.min, p.input.max, p.input.decimals], p.compare];
	};
	assert.deepEqual(row('full'), ['string', '', 0, null, 'contains']);
	assert.deepEqual(row('Tn'), ['date', '', 1, null, 'greaterThan']);
	assert.deepEqual(row('q'), ['number', 'AU', 0.1, [0, 15, 6], 'lessThan']);
	assert.deepEqual(row('PerihEarthDist'), ['number', 'AU', 0.1, [0, 15, 6], 'lessThan']);
	assert.deepEqual(row('PerihMag'), ['number', '', 0.2, [-20, 40, 2], 'lessThan']);
	assert.deepEqual(row('CurrentSunDist'), ['number', 'AU', 0.1, [0, 150, 6], 'lessThan']);
	assert.deepEqual(row('CurrentEarthDist'), ['number', 'AU', 0.1, [0, 150, 6], 'lessThan']);
	assert.deepEqual(row('CurrentMag'), ['number', '', 0.5, [-20, 40, 2], 'lessThan']);
	assert.deepEqual(row('P'), ['number', 'years', 0.1, [0, 10000, 6], 'lessThan']);
	assert.deepEqual(row('Q'), ['number', 'AU', 0.1, [0, 1000, 6], 'lessThan']);
	assert.deepEqual(row('a'), ['number', 'AU', 0.1, [0, 1000, 6], 'lessThan']);
	assert.deepEqual(row('e'), ['number', '', 0.01, [0, 1.2, 6], 'lessThan']);
	assert.deepEqual(row('i'), ['number', '°', 1, [0, 179.9999, 4], 'lessThan']);
	assert.deepEqual(row('N'), ['number', '°', 1, [0, 359.9999, 4], 'lessThan']);
	assert.deepEqual(row('w'), ['number', '°', 1, [0, 359.9999, 4], 'lessThan']);
});

test('every default compare is in its type\'s menu', () => {
	for (const p of PROPERTIES) assert.ok(COMPARES_BY_TYPE[p.type].some(c => c.key === p.compare), p.key);
});

test('the session values are Tn, Perih* and Current*', () => {
	assert.deepEqual(SESSION_KEYS, ['Tn', 'PerihEarthDist', 'PerihMag', 'CurrentSunDist', 'CurrentEarthDist', 'CurrentMag']);
});

test('an unknown property is a programmer error', () => {
	assert.throws(() => property('sortkey'), /unknown comet property/);
});

test('fieldValue reads the comet\'s fields, and only those', () => {
	const comet = createComet({ full: '1P/Halley', id: '1P', name: 'Halley', q: 0.5, e: 0.9 });
	assert.ok(comet);
	assert.equal(fieldValue(comet, 'full'), '1P/Halley');
	assert.equal(fieldValue(comet, 'sortkey'), '000010000000000');
	assert.equal(fieldValue(comet, 'q'), 0.5);
	assert.throws(() => fieldValue(comet, 'Tn'), /not a comet field/);
});

test('the compare menus, with "Greater" spelt right (spec 10, D15)', () => {
	assert.deepEqual(COMPARES_BY_TYPE.string.map(c => c.label), ['Contains', 'Does not contain', 'Starts with', 'Ends with']);
	assert.deepEqual(COMPARES_BY_TYPE.number.map(c => c.label), ['Greater than (>)', 'Less than (<)', 'Equals (=)']);
	assert.equal(COMPARES_BY_TYPE.date, COMPARES_BY_TYPE.number);
});

test('text compares: any term, and an empty term matches everything (spec 10, Q16)', () => {
	const terms = ['halley', 'encke'];
	assert.equal(textCompare('contains').test('1p/halley', terms), true);
	assert.equal(textCompare('contains').test('3d/biela', terms), false);
	assert.equal(textCompare('doesNotContain').test('3d/biela', terms), true);
	assert.equal(textCompare('doesNotContain').test('2p/encke', terms), false);
	assert.equal(textCompare('startsWith').test('c/2025 a1', ['c/2025']), true);
	assert.equal(textCompare('startsWith').test('c/2024 a1', ['c/2025']), false);
	assert.equal(textCompare('endsWith').test('73p-b', ['-b', 'x']), true);
	assert.equal(textCompare('endsWith').test('73p-bt', ['-b']), false);
	for (const key of ['contains', 'startsWith', 'endsWith']) assert.equal(textCompare(key).test('anything', ['halley', '']), true, key);
	assert.equal(textCompare('doesNotContain').test('anything', ['']), false);
});

test('number compares: strict above and below, equal within the tolerance inclusive', () => {
	assert.equal(numberCompare('greaterThan').test(2, 1, 0.1), true);
	assert.equal(numberCompare('greaterThan').test(1, 1, 0.1), false);
	assert.equal(numberCompare('lessThan').test(0.5, 1, 0.1), true);
	assert.equal(numberCompare('lessThan').test(1, 1, 0.1), false);
	assert.equal(numberCompare('equals').test(1.5, 1, 0.5), true);
	assert.equal(numberCompare('equals').test(1.51, 1, 0.5), false);
	assert.equal(numberCompare('equals').test(0.49, 1, 0.5), false);
});

test('an unknown compare is a programmer error', () => {
	assert.throws(() => textCompare('equals'), /unknown compare/);
	assert.throws(() => numberCompare('contains'), /unknown compare/);
});
