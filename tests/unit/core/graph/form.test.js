import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { LONG_CALCULATION_STEPS } from '../../../../app/js/core/ephemeris/settings.js';
import {
	asksLongCalculation, calculationInput, canCalculate, cometsOf, fieldsOf, graphFileName, IMAGE_FORMATS, limitOf, settingsOf, typedLimit
} from '../../../../app/js/core/graph/form.js';
import { defaultGraphSettings, validateGraphSettings } from '../../../../app/js/core/graph/settings.js';
import { ALL_COMETS, NO_CHOICE } from '../../../../app/js/core/input/comet-picker.js';

/** @param {string} full */
const comet = full => {
	const c = createComet({ full, id: '1P', q: 1, e: 0.5 });
	assert.ok(c);
	return c;
};
const [a, b] = [comet('a'), comet('b')];
const list = Object.freeze({ comets: Object.freeze([a, b]), filters: Object.freeze([]), sort: Object.freeze({ property: 'sortkey', ascending: true }) });
const DAY = 86_400_000;
const NOW = Date.UTC(2026, 9, 18, 12);

test('an end of the value range: its box and its field, no value while empty; typing checks it while the field holds text', () => {
	assert.deepEqual({ ...limitOf(true, '') }, { checked: true, value: null });
	assert.deepEqual({ ...limitOf(false, '-1.5') }, { checked: false, value: -1.5 });
	// text that is not a number reads as 0, as Double() gives
	assert.deepEqual({ ...limitOf(true, '-') }, { checked: true, value: 0 });
	assert.deepEqual({ ...typedLimit('12') }, { checked: true, value: 12 });
	assert.deepEqual({ ...typedLimit('') }, { checked: false, value: null });
	assert.ok(Object.isFrozen(typedLimit('1')));
});

test('the fields of settings, and the settings of fields, both ways', () => {
	const settings = defaultGraphSettings(NOW);
	const fields = fieldsOf(settings);
	assert.deepEqual(JSON.parse(JSON.stringify(fields)), {
		start: settings.start, end: settings.end, chartType: 'Magnitude', valueColor: '#ff0000',
		perihelionLine: { checked: true, color: '#4169e1' }, nowLine: { checked: true, color: '#32cd32' },
		minimum: { checked: false, text: '' }, maximum: { checked: true, text: '12' }
	});
	assert.ok(Object.isFrozen(fields) && Object.isFrozen(fields.perihelionLine) && Object.isFrozen(fields.maximum));
	assert.deepEqual(settingsOf(fields), settings);

	// as typed: the colours as #RRGGBB, a checked end without text has no value
	const typed = settingsOf({
		...fields, chartType: 'SunDistance', valueColor: '#00ff7f', nowLine: { checked: false, color: '#123abc' },
		minimum: { checked: true, text: '' }, maximum: { checked: false, text: '20.5' }
	});
	assert.equal(typed.chartType, 'SunDistance');
	assert.equal(typed.valueColor, '#00FF7F');
	assert.deepEqual({ ...typed.nowLine }, { checked: false, color: '#123ABC' });
	assert.deepEqual({ ...typed.minimum }, { checked: true, value: null });
	assert.deepEqual({ ...typed.maximum }, { checked: false, value: 20.5 });
	assert.ok(Object.isFrozen(typed) && Object.isFrozen(typed.nowLine) && Object.isFrozen(typed.minimum));
	const validation = validateGraphSettings(typed);
	assert.ok(!validation.ok);
	assert.deepEqual(validation.errors, [{ field: 'minimum', message: 'Enter Minimum value' }]);
	assert.deepEqual(fieldsOf(typed).maximum, { checked: false, text: '20.5' });
});

test('Calculate runs over the chosen comet, or the list in multiple mode; not while nothing is chosen', () => {
	assert.equal(canCalculate(NO_CHOICE), false);
	assert.equal(canCalculate(ALL_COMETS), true);
	assert.deepEqual(cometsOf({ all: false, comet: b }, list), [b]);
	assert.equal(cometsOf(ALL_COMETS, list), list.comets);
});

test('the long-calculation question: more than 100000 steps over all the comets, while its setting is on', () => {
	const settings = defaultGraphSettings(NOW);
	// up to 100 days, 100 steps a comet
	const days = { ...settings, end: settings.start + 100 * DAY };
	assert.equal(asksLongCalculation(days, LONG_CALCULATION_STEPS / 100, true), false);
	assert.equal(asksLongCalculation(days, LONG_CALCULATION_STEPS / 100 + 1, true), true);
	assert.equal(asksLongCalculation(days, LONG_CALCULATION_STEPS / 100 + 1, false), false);
	// 299 years at 40 days: 2730 steps a comet
	const centuries = { ...settings, end: settings.start + 2730 * 40 * DAY };
	assert.equal(asksLongCalculation(centuries, 36, true), false);
	assert.equal(asksLongCalculation(centuries, 37, true), true);
});

test('the input: the comets, their Tn, the settings, the mode, the observer\'s coordinates and the JD of now', () => {
	const settings = defaultGraphSettings(NOW);
	const nearest = new Map([[a, 2461000.5], [b, 2460000]]);
	const input = calculationInput({
		comets: [a, b], settings, multiple: true, observer: { name: 'Zagreb', latitude: 45.815, longitude: 15.9819 }, now: NOW + 999,
		nearest: x => /** @type {number} */ (nearest.get(x))
	});
	// the milliseconds of now dropped
	assert.deepEqual(input, {
		comets: [a, b], nearest: [2461000.5, 2460000], settings, multiple: true, observer: { latitude: 45.815, longitude: 15.9819 }, nowJd: 2461332
	});
	assert.ok(Object.isFrozen(input) && Object.isFrozen(input.nearest) && Object.isFrozen(input.observer));
	// plain data, for the Worker
	assert.deepEqual(structuredClone(input), input);
});

test('a saved graph\'s name: Comets_Graph_, the local time and the format\'s extension', () => {
	const local = { year: 2026, month: 10, day: 18, hour: 14, minute: 5, second: 1 };
	assert.deepEqual(IMAGE_FORMATS.map(f => [f.key, f.label, f.type]), [['png', 'Save', 'image/png'], ['jpeg', 'Save as JPEG', 'image/jpeg']]);
	assert.deepEqual(IMAGE_FORMATS.map(f => graphFileName(local, f)), ['Comets_Graph_2026-10-18_140501.png', 'Comets_Graph_2026-10-18_140501.jpg']);
});
