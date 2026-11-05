import { test } from 'node:test';
import assert from 'node:assert/strict';
import { drain } from '../../../helpers/generators.js';
import { calculateSamples, sampleCount, sampleJds, samplingInterval } from '../../../../app/js/core/graph/sampling.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { ephemeris } from '../../../../app/js/core/astro/ephemeris.js';
import { defaultGraphSettings } from '../../../../app/js/core/graph/settings.js';

const DAY = 86_400_000;
const start = Date.parse('2026-01-01T00:00:00Z');
const startJd = 2461041.5;

test('the interval at the edges of the table', () => {
	const interval = (/** @type {number} */ days) => samplingInterval(start, start + days * DAY);
	assert.equal(interval(100), 1);
	assert.equal(interval(100 + 1 / 86400), 1);
	assert.equal(interval(2), 0.02);
	assert.equal(interval(364), 1);
	assert.equal(interval(365), 2);
	assert.equal(interval(3652.5), 5);
	assert.equal(interval(18262.5), 15);
	assert.equal(interval(36525), 30);
	assert.equal(interval(73050), 40);
	assert.equal(interval(109575), 0);
});

test('101 samples up to 100 days, at JD(Start) + k·S/100', () => {
	assert.equal(sampleCount(start, start + DAY), 101);
	assert.equal(sampleCount(start, start + 100 * DAY), 101);
	const jds = sampleJds(start, start + 2 * DAY);
	assert.equal(jds.length, 101);
	assert.equal(jds[0], startJd);
	assert.equal(jds[1], startJd + 0.02);
	assert.equal(jds[100], startJd + 2);
});

test('whole intervals from Start, End included when one falls on it (spec 10, D8)', () => {
	assert.equal(sampleCount(start, start + 365 * DAY), 183);
	assert.equal(sampleCount(start, start + 366 * DAY), 184);
	assert.equal(sampleCount(start, start + 366 * DAY + 1000), 184);
	const jds = sampleJds(start, start + 200 * DAY);
	assert.deepEqual([jds.length, jds[0], jds[1], jds[200]], [201, startJd, startJd + 1, startJd + 200]);
	assert.ok(Object.isFrozen(jds));
});

test('the samples are the ephemeris values at each JD, comet by comet, with no requirements', () => {
	const comets = [
		createComet({ full: '2P/Encke', id: '2P', Ty: 2026, Tm: 2, Td: 1, q: 0.33, e: 0.847, w: 186, N: 334, i: 11, g: 11.5, k: 4 }),
		createComet({ full: 'C/2099 Z9', id: 'C/2099 Z9', Ty: 2099, Tm: 1, Td: 1, q: 30, e: 0.5, g: 15, k: 4 })
	].map(c => { assert.ok(c); return c; });
	const settings = { ...defaultGraphSettings(start), start, end: start + 10 * DAY };
	const observer = { latitude: 10, longitude: 20 };
	const input = { comets, nearest: [0, 0], settings, multiple: true, observer, nowJd: startJd };
	const { yielded, result } = drain(calculateSamples(input, ep => ep.Magnitude));
	assert.deepEqual(yielded, [{ done: 1, total: 2 }, { done: 2, total: 2 }]);
	assert.equal(result.values.length, 2);
	assert.equal(result.values[1][50], ephemeris(comets[1], result.jds[50], observer).Magnitude);
	assert.ok(result.values[1].every(v => v > 15), 'no requirement drops the faint comet');
});

test('a long comet yields points to stop at', () => {
	const comet = createComet({ full: '2P/Encke', id: '2P', Ty: 2026, Tm: 2, Td: 1, q: 0.33, e: 0.847, g: 11.5, k: 4 });
	assert.ok(comet);
	const settings = { ...defaultGraphSettings(start), start, end: start + 299 * 365.25 * DAY };
	const { yielded } = drain(calculateSamples({ comets: [comet], nearest: [0], settings, multiple: false, observer: { latitude: 0, longitude: 0 }, nowJd: 0 }, ep => ep.SunDist));
	// 2731 samples of 40 days: one point inside, then the comet's completion
	assert.deepEqual(yielded, [{ done: 0, total: 1 }, { done: 1, total: 1 }]);
});
