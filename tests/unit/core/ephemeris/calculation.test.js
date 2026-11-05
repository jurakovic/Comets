import { test } from 'node:test';
import assert from 'node:assert/strict';
import { drain } from '../../../helpers/generators.js';
import { calculateEphemerides, cometsToCalculate } from '../../../../app/js/core/ephemeris/calculation.js';
import { defaultEphemerisSettings } from '../../../../app/js/core/ephemeris/settings.js';
import { ephemerisText } from '../../../../app/js/core/ephemeris/text.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { ephemeris } from '../../../../app/js/core/astro/ephemeris.js';
import { instantToJd } from '../../../../app/js/core/time/julian.js';

const utc = (/** @type {string} */ text) => Date.parse(text);
const observer = Object.freeze({ latitude: 0, longitude: 0 });
const encke = createComet({ full: '2P/Encke', id: '2P', name: 'Encke', Ty: 2026, Tm: 10, Td: 22, Th: 5000, q: 0.33, e: 0.847, w: 186.8, N: 334.2, i: 11.4, g: 11.5, k: 4 });
const faint = createComet({ full: 'C/2099 Z9 (Far)', id: 'C/2099 Z9', name: 'Far', Ty: 2099, Tm: 1, Td: 1, q: 30, e: 0.5, g: 15, k: 4 });
assert.ok(encke && faint);
const start = utc('2026-10-01T00:00:00Z');
const none = { maxSunDist: { checked: false, value: null }, maxEarthDist: { checked: false, value: null }, minMagnitude: { checked: false, value: null } };

/** @param {object} changes */
function settingsWith(changes) {
	return { ...defaultEphemerisSettings(start), start, requirements: none, ...changes };
}

/** @param {ReadonlyArray<any>} comets @param {any} settings */
const input = (comets, settings) => ({ comets, nearest: comets.map(c => c.T), settings, observer, timeZone: 'UTC' });

test('the comets: the list in multiple mode, otherwise the selected one', () => {
	assert.deepEqual(cometsToCalculate({ multiple: true, comets: [encke, faint], selected: faint }), [encke, faint]);
	assert.deepEqual(cometsToCalculate({ multiple: false, comets: [encke, faint], selected: faint }), [faint]);
	assert.deepEqual(cometsToCalculate({ multiple: false, comets: [encke], selected: null }), []);
	// a one-comet list in multiple mode is that comet, where the desktop crashes (spec 10, D18)
	assert.deepEqual(cometsToCalculate({ multiple: true, comets: [faint], selected: null }), [faint]);
});

test('rows at every whole step from Start, End included, at the JD of each step (spec 10, D8)', () => {
	const settings = settingsWith({ end: start + 86_400_000, interval: { days: 0, hours: 1, minutes: 0 } });
	const { result } = drain(calculateEphemerides(input([encke], settings)));
	assert.equal(result.length, 1);
	assert.equal(result[0].rows.length, 25);
	assert.equal(result[0].rows[24].JD, instantToJd(start + 86_400_000));
	assert.deepEqual(result[0].rows[7], ephemeris(encke, instantToJd(start + 7 * 3_600_000), observer));
	assert.equal(result[0].Tn, encke.T);
	assert.ok(Object.isFrozen(result) && Object.isFrozen(result[0]) && Object.isFrozen(result[0].rows));
});

test('End between two steps: the last row is the step before it', () => {
	const settings = settingsWith({ end: start + 86_400_000 - 1000, interval: { days: 0, hours: 1, minutes: 0 } });
	const { result } = drain(calculateEphemerides(input([encke], settings)));
	assert.equal(result[0].rows.length, 24);
});

test('rows must meet every checked requirement, and comets without rows are left out', () => {
	const end = start + 30 * 86_400_000;
	const all = drain(calculateEphemerides(input([encke, faint], settingsWith({ end })))).result;
	assert.deepEqual(all.map(r => r.rows.length), [31, 31]);
	const limit = (/** @type {string} */ key, /** @type {number} */ value) => settingsWith({ end, requirements: { ...none, [key]: { checked: true, value } } });
	for (const [key, field, value] of /** @type {const} */ ([['minMagnitude', 'Magnitude', 8], ['maxSunDist', 'SunDist', 0.4], ['maxEarthDist', 'EarthDist', 1]])) {
		const { result } = drain(calculateEphemerides(input([encke, faint], limit(key, value))));
		const expected = all[0].rows.filter(ep => ep[field] <= value);
		assert.ok(expected.length > 0 && expected.length < 31, key);
		assert.deepEqual(result.map(r => r.comet.full), ['2P/Encke'], key);
		assert.deepEqual(result[0].rows, expected, key);
	}
});

test('a requirement keeps a row equal to its value', () => {
	const end = start + 30 * 86_400_000;
	const all = drain(calculateEphemerides(input([encke], settingsWith({ end })))).result[0].rows;
	for (const [key, field] of /** @type {const} */ ([['minMagnitude', 'Magnitude'], ['maxSunDist', 'SunDist'], ['maxEarthDist', 'EarthDist']])) {
		const value = all[10][field];
		const { result } = drain(calculateEphemerides(input([encke], settingsWith({ end, requirements: { ...none, [key]: { checked: true, value } } }))));
		assert.ok(result[0].rows.some(ep => ep.JD === all[10].JD), key);
	}
});

test('progress in comets, with points to stop at inside a long comet', () => {
	const settings = settingsWith({ end: start + 5 * 86_400_000, interval: { days: 0, hours: 0, minutes: 1 } });
	const { yielded } = drain(calculateEphemerides(input([encke, faint], settings)));
	// 7201 rows each: three points inside a comet, then its completion
	assert.deepEqual(yielded, [
		{ done: 0, total: 2 }, { done: 0, total: 2 }, { done: 0, total: 2 }, { done: 1, total: 2 },
		{ done: 1, total: 2 }, { done: 1, total: 2 }, { done: 1, total: 2 }, { done: 2, total: 2 }
	]);
});

test('End before Start: no rows, as the desktop\'s loop gives', () => {
	const { result } = drain(calculateEphemerides(input([encke], settingsWith({ end: start - 1000 }))));
	assert.deepEqual(result, []);
	const one = drain(calculateEphemerides(input([encke], settingsWith({ end: start })))).result;
	assert.equal(one[0].rows.length, 1);
});

test('no comets, no rows, no text', () => {
	const { yielded, result } = drain(calculateEphemerides(input([], settingsWith({ end: start + 86_400_000 }))));
	assert.deepEqual(yielded, []);
	assert.deepEqual(result, []);
	assert.equal(ephemerisText(result, settingsWith({}), 'UTC'), '');
});
