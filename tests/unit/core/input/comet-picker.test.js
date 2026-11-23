import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { createFilter } from '../../../../app/js/core/comets/filter.js';
import { fieldValue } from '../../../../app/js/core/comets/properties.js';
import { importElements } from '../../../../app/js/core/formats/import.js';
import {
	ALL_COMETS, allCometsLabel, choiceAt, cometTooltip, entryIndex, filterPickerList, NO_CHOICE, perihelionDate,
	pickerEntries, pickerListOver, rebindChoice, sortPickerList
} from '../../../../app/js/core/input/comet-picker.js';
import { jdToInstant } from '../../../../app/js/core/time/julian.js';
import { readVector, readVectorText } from '../../../helpers/vectors.js';

/** @typedef {import('../../../../app/js/core/comets/comet.js').Comet} Comet */

/** @param {string} full @param {number} q */
const comet = (full, q) => {
	const c = createComet({ full, id: '1P', q, e: 0.5 });
	assert.ok(c);
	return c;
};
const [a, b, c, d] = [comet('a', 3), comet('b', 1), comet('c', 2), comet('d', 1)];
/** the session values from a fixed table, for the sake of the test: b is the brightest */
const values = new Map([[a, { CurrentMag: 9, Tn: 2460581.3337 }], [b, { CurrentMag: 5, Tn: 2451545 }], [c, { CurrentMag: 7, Tn: 2461000.5 }], [d, { CurrentMag: 6, Tn: 2460000 }]]);
const value = (/** @type {Comet} */ comet, /** @type {string} */ key) => /** @type {any} */ (values.get(comet))?.[key] ?? fieldValue(comet, key);
const byDesignation = Object.freeze({ property: 'sortkey', ascending: true });
const fullNames = (/** @type {ReadonlyArray<Comet>} */ comets) => comets.map(x => x.full).join('');

test('the entries: [ALL COMETS (n)] first, then the list in its order; none for an empty list', () => {
	assert.equal(allCometsLabel(963), '[ALL COMETS (963)]');
	assert.deepEqual(pickerEntries([c, a, b]), ['[ALL COMETS (3)]', 'c', 'a', 'b']);
	assert.deepEqual(pickerEntries([]), []);
});

test('the first entry is multiple mode, the others their comet', () => {
	const list = [c, a, b];
	assert.equal(choiceAt(list, 0), ALL_COMETS);
	assert.deepEqual({ ...choiceAt(list, 2) }, { all: false, comet: a });
	assert.equal(choiceAt(list, 3).comet, b);
	for (const index of [-1, 4]) assert.equal(choiceAt(list, index), NO_CHOICE);
	assert.equal(choiceAt([], 0), NO_CHOICE);
	for (let index = 0; index <= list.length; index++) assert.equal(entryIndex(list, choiceAt(list, index)), index);
	assert.equal(entryIndex(list, NO_CHOICE), -1);
	assert.equal(entryIndex(list, { all: false, comet: d }), -1);
	assert.equal(entryIndex([], ALL_COMETS), -1);
});

test('a new list keeps multiple mode and the comet chosen while it is in the list, else the brightest', () => {
	assert.equal(rebindChoice([a, b], ALL_COMETS, value, 0), ALL_COMETS);
	assert.equal(rebindChoice([a, c], { all: false, comet: c }, value, 0).comet, c);
	// an equal comet, as after an import replaced the record: the list's own record
	const twin = comet('c', 2);
	assert.equal(rebindChoice([twin, a], { all: false, comet: c }, value, 0).comet, twin);
	assert.equal(rebindChoice([a, c, d], { all: false, comet: b }, value, 0).comet, d);
	assert.equal(rebindChoice([a, c, d], NO_CHOICE, value, 0).comet, d);
	// an empty list chooses nothing, multiple mode included
	assert.equal(rebindChoice([], ALL_COMETS, value, 0), NO_CHOICE);
	assert.equal(rebindChoice([], { all: false, comet: a }, value, 0), NO_CHOICE);
});

test('the sort re-sorts the list as it is: equal comets keep their order', () => {
	const filters = Object.freeze([createFilter('q', { checked: true, text: '0', compare: 'greaterThan' })]);
	const list = Object.freeze({ comets: Object.freeze([d, a, b, c]), filters, sort: byDesignation });
	const sorted = sortPickerList(list, { property: 'q', ascending: true }, value);
	assert.equal(fullNames(sorted.comets), 'dbca');
	assert.deepEqual({ ...sorted.sort }, { property: 'q', ascending: true });
	assert.equal(sorted.filters, filters);
	assert.ok(Object.isFrozen(sorted) && Object.isFrozen(sorted.sort));
	assert.equal(fullNames(sortPickerList(sorted, { property: 'q', ascending: false }, value).comets), 'acdb');
	assert.equal(fullNames(list.comets), 'dabc');
});

test('a filter runs over the whole database, in database order through the picker\'s sort', () => {
	const database = [a, b, c, d];
	const rows = [createFilter('q', { checked: true, text: '2.5', compare: 'lessThan' }), createFilter('full')];
	const descending = Object.freeze({ property: 'q', ascending: false });
	const result = filterPickerList(database, rows, descending, value);
	assert.ok(result.ok);
	assert.equal(fullNames(result.value.comets), 'cbd');
	assert.deepEqual(result.value.filters, rows);
	assert.notEqual(result.value.filters, rows);
	assert.equal(result.value.sort, descending);
	// no checked row: the whole database
	const all = filterPickerList(database, [createFilter('full')], byDesignation, value);
	assert.ok(all.ok);
	assert.equal(fullNames(all.value.comets), 'abcd');
});

test('a changed database: the list again from its own filters, in database order through its sort', () => {
	const filters = Object.freeze([createFilter('q', { checked: true, text: '2.5', compare: 'lessThan' })]);
	const descending = Object.freeze({ property: 'q', ascending: false });
	const list = Object.freeze({ comets: Object.freeze([c, b]), filters, sort: descending });
	// d is new, b's record was replaced by an equal one, and c was deleted
	const twin = comet('b', 1);
	const relisted = pickerListOver(list, [d, a, twin], value);
	assert.deepEqual(relisted.comets, [d, twin]);
	assert.equal(relisted.comets[1], twin);
	assert.equal(relisted.filters, filters);
	assert.equal(relisted.sort, descending);
	assert.ok(Object.isFrozen(relisted));
	assert.deepEqual(pickerListOver(list, [], value).comets, []);
	// the default rows check nothing: the whole database
	assert.equal(fullNames(pickerListOver({ ...list, filters: [createFilter('full')] }, [d, c, a], value).comets), 'acd');
});

test('rows that do not validate give their error, by its row', () => {
	const rows = [createFilter('full'), createFilter('q', { checked: true, text: ' ' })];
	const result = filterPickerList([a, b], rows, byDesignation, value);
	assert.ok(!result.ok);
	assert.deepEqual(result.errors.map(e => ({ ...e })), [{ field: 'text', index: 1, message: 'Please enter value for "Perihelion distance"' }]);
});

test('the Perihelion Date preset: the chosen comet\'s Tn; none in multiple mode or with nothing chosen', () => {
	assert.equal(perihelionDate({ all: false, comet: a }, value), jdToInstant(2460581.3337));
	assert.equal(new Date(perihelionDate({ all: false, comet: b }, value) ?? 0).toISOString(), '2000-01-01T12:00:00.000Z');
	assert.equal(perihelionDate(ALL_COMETS, value), null);
	assert.equal(perihelionDate(NO_CHOICE, value), null);
});

/** the sample collection, and its session values as the desktop gave them at the vectors' clock */
const sample = importElements([], 'MPC', readVectorText('import/sample.txt')).comets;
/** @type {Map<string, Record<string, number>>} */
const desktop = new Map(readVector('comets.json').comets.map((/** @type {any} */ x) => [x.full, x]));
const desktopValue = (/** @type {Comet} */ comet, /** @type {string} */ key) => desktop.get(comet.full)?.[key] ?? fieldValue(comet, key);
/** @param {string} full */
const sampleComet = full => {
	const found = sample.find(x => x.full === full);
	assert.ok(found, full);
	return found;
};

test('the tooltip: the full name over T, P, q, r, d and m', () => {
	assert.deepEqual({ ...cometTooltip(sampleComet('1P/Halley'), desktopValue) }, {
		title: '1P/Halley',
		body: 'T: 01 Aug 2061 12:31:23\nP: 75.4705 years\nq: 0.570945 AU\nr: 35.011933 AU\nd: 35.198224 AU\nm: 25.59'
	});
	assert.deepEqual({ ...cometTooltip(sampleComet('C/2023 A3 (Tsuchinshan-ATLAS)'), desktopValue) }, {
		title: 'C/2023 A3 (Tsuchinshan-ATLAS)',
		body: 'T: 27 Sep 2024 20:00:31\nP: -\nq: 0.391312 AU\nr: 8.716899 AU\nd: 9.049030 AU\nm: 18.81'
	});
});

test('the period shows below 10000 years, and "-" from there on', () => {
	const southern = sampleComet('C/1947 X1-B (Southern comet)');
	assert.match(cometTooltip(southern, desktopValue).body, /^P: 3300\.7960 years$/m);
	assert.match(cometTooltip(sampleComet('A/2018 W3'), desktopValue).body, /^P: -$/m);
	assert.match(cometTooltip({ ...southern, P: 9999.99999 }, desktopValue).body, /^P: 10000\.0000 years$/m);
	assert.match(cometTooltip({ ...southern, P: 10_000 }, desktopValue).body, /^P: -$/m);
});
