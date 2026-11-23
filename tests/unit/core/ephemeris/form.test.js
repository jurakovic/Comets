import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import {
	asksLongCalculation, calculationInput, canCalculate, cometsOf, ephemerisFileName, fieldText, INTERVAL_FIELDS, intervalOf,
	requirementOf, typedRequirement
} from '../../../../app/js/core/ephemeris/form.js';
import { defaultEphemerisSettings, INTERVAL_INPUT, LONG_CALCULATION_STEPS } from '../../../../app/js/core/ephemeris/settings.js';
import { ALL_COMETS, NO_CHOICE } from '../../../../app/js/core/input/comet-picker.js';

/** @param {string} full */
const comet = full => {
	const c = createComet({ full, id: '1P', q: 1, e: 0.5 });
	assert.ok(c);
	return c;
};
const [a, b, c] = [comet('a'), comet('b'), comet('c')];
const list = Object.freeze({ comets: Object.freeze([a, b, c]), filters: Object.freeze([]), sort: Object.freeze({ property: 'sortkey', ascending: true }) });
const DAY = 86_400_000;

test('the interval\'s fields: D, H and M, each with its range', () => {
	assert.deepEqual(INTERVAL_FIELDS.map(f => `${f.letter} ${f.name}`), ['D Days', 'H Hours', 'M Minutes']);
	for (const field of INTERVAL_FIELDS) assert.ok(Object.hasOwn(INTERVAL_INPUT, field.key));
});

test('a value\'s text, and the interval the texts give: whole numbers, empty 0', () => {
	assert.equal(fieldText(null), '');
	assert.equal(fieldText(12), '12');
	assert.equal(fieldText(-1.25), '-1.25');
	assert.equal(fieldText(0), '0');
	assert.deepEqual({ ...intervalOf({ days: '1', hours: '0', minutes: '0' }) }, { days: 1, hours: 0, minutes: 0 });
	assert.deepEqual({ ...intervalOf({ days: '', hours: '2', minutes: '30' }) }, { days: 0, hours: 2, minutes: 30 });
	assert.deepEqual({ ...intervalOf({ days: '3652', hours: '23', minutes: '59' }) }, { days: 3652, hours: 23, minutes: 59 });
	assert.ok(Object.isFrozen(intervalOf({ days: '', hours: '', minutes: '' })));
});

test('a requirement: its box and its field, no value while empty; typing checks it while the field holds text', () => {
	assert.deepEqual({ ...requirementOf(true, '') }, { checked: true, value: null });
	assert.deepEqual({ ...requirementOf(false, '1.5') }, { checked: false, value: 1.5 });
	// text that is not a number reads as 0, as Double() gives
	assert.deepEqual({ ...requirementOf(true, '-') }, { checked: true, value: 0 });
	assert.deepEqual({ ...typedRequirement('9.9') }, { checked: true, value: 9.9 });
	assert.deepEqual({ ...typedRequirement('.') }, { checked: true, value: 0 });
	assert.deepEqual({ ...typedRequirement('') }, { checked: false, value: null });
});

test('Calculate runs over the chosen comet, or the list in multiple mode; not while nothing is chosen', () => {
	assert.equal(canCalculate(NO_CHOICE), false);
	assert.equal(canCalculate(ALL_COMETS), true);
	assert.equal(canCalculate({ all: false, comet: b }), true);
	assert.deepEqual(cometsOf({ all: false, comet: b }, list), [b]);
	assert.equal(cometsOf(ALL_COMETS, list), list.comets);
	assert.deepEqual(cometsOf(NO_CHOICE, list), []);
	// a one-comet list in multiple mode: its comet
	assert.deepEqual(cometsOf(ALL_COMETS, { ...list, comets: [c] }), [c]);
});

test('the long-calculation question: more than 100000 steps over all the comets, while its setting is on', () => {
	const start = Date.UTC(2026, 0, 1);
	const minutes = { days: 0, hours: 0, minutes: 1 };
	const settings = { ...defaultEphemerisSettings(start), start, interval: minutes };
	const at = (/** @type {number} */ steps) => ({ ...settings, end: start + steps * 60_000 });
	assert.equal(asksLongCalculation(at(LONG_CALCULATION_STEPS), 1, true), false);
	assert.equal(asksLongCalculation(at(LONG_CALCULATION_STEPS + 1), 1, true), true);
	assert.equal(asksLongCalculation(at(LONG_CALCULATION_STEPS + 1), 1, false), false);
	// 50001 steps over two comets
	assert.equal(asksLongCalculation(at(50_001), 2, true), true);
	assert.equal(asksLongCalculation(at(50_000), 2, true), false);
	// a year of days is 365 steps a comet
	const year = { ...settings, end: start + 365 * DAY, interval: { days: 1, hours: 0, minutes: 0 } };
	assert.equal(asksLongCalculation(year, 273, true), false);
	assert.equal(asksLongCalculation(year, 274, true), true);
});

test('the input: the comets, their Tn, the settings, the observer\'s coordinates and the time zone', () => {
	const settings = defaultEphemerisSettings(Date.UTC(2026, 9, 18, 12));
	const nearest = new Map([[a, 2461000.5], [c, 2460000]]);
	const input = calculationInput({
		comets: [a, c], settings, observer: { name: 'Zagreb', latitude: 45.815, longitude: 15.9819 }, timeZone: 'Europe/Zagreb',
		nearest: x => /** @type {number} */ (nearest.get(x))
	});
	assert.deepEqual(input, { comets: [a, c], nearest: [2461000.5, 2460000], settings, observer: { latitude: 45.815, longitude: 15.9819 }, timeZone: 'Europe/Zagreb' });
	assert.ok(Object.isFrozen(input) && Object.isFrozen(input.nearest) && Object.isFrozen(input.observer));
	// plain data, for the Worker
	assert.deepEqual(structuredClone(input), input);
});

test('a saved ephemeris\'s name: Comets_Ephemeris_ and the local time', () => {
	assert.equal(ephemerisFileName({ year: 2026, month: 10, day: 18, hour: 14, minute: 5, second: 1 }), 'Comets_Ephemeris_2026-10-18_140501.txt');
});
