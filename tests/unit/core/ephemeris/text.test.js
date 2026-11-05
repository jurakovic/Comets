import { test } from 'node:test';
import assert from 'node:assert/strict';
import { drain } from '../../../helpers/generators.js';
import { calculateEphemerides } from '../../../../app/js/core/ephemeris/calculation.js';
import { defaultEphemerisSettings } from '../../../../app/js/core/ephemeris/settings.js';
import { ephemerisText } from '../../../../app/js/core/ephemeris/text.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';

const start = Date.parse('2026-10-24T23:00:00Z');
const observer = Object.freeze({ latitude: 45.815, longitude: 15.9819 });
const none = { maxSunDist: { checked: false, value: null }, maxEarthDist: { checked: false, value: null }, minMagnitude: { checked: false, value: null } };
const columns = { RA: false, Dec: false, Alt: false, Az: false, EcLon: false, EcLat: false, Elongation: false, SunDist: false, EarthDist: false, Magnitude: true };

/** @param {any} comet @param {object} changes @param {number} [Tn] */
function text(comet, changes, Tn = comet.T) {
	const settings = { ...defaultEphemerisSettings(start), start, end: start + 2 * 3_600_000, interval: { days: 0, hours: 1, minutes: 0 }, columns, requirements: none, ...changes };
	const { result } = drain(calculateEphemerides({ comets: [comet], nearest: [Tn], settings, observer, timeZone: 'Europe/Zagreb' }));
	return ephemerisText(result, settings, 'Europe/Zagreb').split('\r\n');
}

test('local time follows the time zone\'s rules at each row', () => {
	const comet = createComet({ full: '2P/Encke', id: '2P', Ty: 2026, Tm: 10, Td: 22, q: 0.33, e: 0.847, g: 11.5, k: 4 });
	const lines = text(comet, { localTime: true });
	assert.equal(lines[5], '     Local Time   Mag.');
	// 00:00 and 01:00 UTC are both 02:00 local: the clocks went back at 03:00 CEST
	assert.deepEqual(lines.slice(6, 9).map(l => l.slice(0, 16)), ['25.10.2026 01:00', '25.10.2026 02:00', '25.10.2026 02:00']);
	const universal = text(comet, { localTime: false });
	assert.equal(universal[5], ' Universal Time   Mag.');
	assert.deepEqual(universal.slice(6, 9).map(l => l.slice(0, 16)), ['24.10.2026 23:00', '25.10.2026 00:00', '25.10.2026 01:00']);
});

test('the heading: Tn, q, and the period or "-" from 10000 years', () => {
	const short = createComet({ full: '2P/Encke', id: '2P', Ty: 2026, Tm: 10, Td: 22, Th: 5000, q: 0.33, e: 0.847, g: 11.5, k: 4 });
	assert.ok(short);
	const lines = text(short, {});
	assert.deepEqual(lines.slice(0, 5), [
		'Comet:               \t2P/Encke',
		'Perihelion date:     \t22 Oct 2026 12:00:00',
		'Perihelion distance: \t0.330000 AU',
		'Period:              \t3.167625 years',
		''
	]);
	// each comet ends with three empty lines; the text ends with a line break
	assert.deepEqual(lines.slice(-4), ['', '', '', '']);
	const long = createComet({ full: 'C/1999 F1 (Catalina)', id: 'C/1999 F1', Ty: 2026, Tm: 10, Td: 22, q: 5.79, e: 0.99923, g: 6, k: 4 });
	assert.ok(long && long.P >= 10000);
	assert.equal(text(long, {})[3], 'Period:              \t-');
});

test('writing yields per comet and within a long one, and gives the same text', async () => {
	const { writeEphemeris, ephemerisText } = await import('../../../../app/js/core/ephemeris/text.js');
	const comet = createComet({ full: '2P/Encke', id: '2P', Ty: 2026, Tm: 10, Td: 22, q: 0.33, e: 0.847, g: 11.5, k: 4 });
	assert.ok(comet);
	const settings = { ...defaultEphemerisSettings(start), start, end: start + 3 * 86_400_000, interval: { days: 0, hours: 0, minutes: 1 }, columns, requirements: none };
	const { result } = drain(calculateEphemerides({ comets: [comet, comet], nearest: [comet.T, comet.T], settings, observer, timeZone: 'UTC' }));
	const { yielded, result: text } = drain(writeEphemeris(result, settings, 'UTC'));
	// 4321 rows each: two points inside a comet, then its completion
	assert.deepEqual(yielded, [
		{ done: 0, total: 2 }, { done: 0, total: 2 }, { done: 1, total: 2 },
		{ done: 1, total: 2 }, { done: 1, total: 2 }, { done: 2, total: 2 }
	]);
	assert.equal(text, ephemerisText(result, settings, 'UTC'));
});

test('the heading shows the session\'s Tn, not T', () => {
	const comet = createComet({ full: '2P/Encke', id: '2P', Ty: 2026, Tm: 10, Td: 22, q: 0.33, e: 0.847, g: 11.5, k: 4 });
	assert.ok(comet);
	assert.equal(text(comet, {}, comet.T + 1204.25)[1], 'Perihelion date:     \t07 Feb 2030 06:00:00');
});

test('the period is written below 10000 years and "-" from 10000 on', () => {
	const settings = { ...defaultEphemerisSettings(start), start, end: start, columns, requirements: none };
	const ep = /** @type {any} */ ({ JD: 2461338.5, Magnitude: 9 });
	const period = (/** @type {number} */ P) => ephemerisText([{ comet: /** @type {any} */ ({ full: 'X', q: 1, P }), Tn: 2461338.5, rows: [ep] }], settings, 'UTC').split('\r\n')[3];
	assert.equal(period(10000), 'Period:              \t-');
	assert.equal(period(9999.5), 'Period:              \t9999.500000 years');
});

