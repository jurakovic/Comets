import { test } from 'node:test';
import assert from 'node:assert/strict';
import { graphModel, perihelionTimes } from '../../../../app/js/core/graph/model.js';
import { hoverAt, projection } from '../../../../app/js/core/graph/hover.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { defaultGraphSettings } from '../../../../app/js/core/graph/settings.js';

const DAY = 86_400_000;
const start = Date.parse('2026-01-01T00:00:00Z');
const startJd = 2461041.5;
/** @param {string} full @param {number} q @param {number} e */
const comet = (full, q, e) => {
	const c = createComet({ full, id: '2P', Ty: 2026, Tm: 1, Td: 1, q, e });
	assert.ok(c);
	return c;
};
const a = comet('A', 1, 0.5);
const b = comet('B', 2, 0.5);
const jds = Object.freeze([startJd, startJd + 5, startJd + 10]);

/**
 * a model of hand-made samples over 10 days
 * @param {ReadonlyArray<ReadonlyArray<number>>} values
 * @param {object} [changes] to the settings
 * @param {{ multiple?: boolean, nearest?: number[], nowJd?: number }} [options]
 */
function model(values, changes = {}, { multiple = values.length > 1, nearest = values.map(() => startJd + 4), nowJd = startJd + 7 } = {}) {
	const settings = { ...defaultGraphSettings(start), start, end: start + 10 * DAY, maximum: { checked: false, value: null }, ...changes };
	const comets = [a, b].slice(0, values.length);
	return graphModel({ comets, nearest, settings, multiple, observer: { latitude: 0, longitude: 0 }, nowJd }, { jds, values });
}

test('the value axis: the range with the margin, floored and ceiled, steps and 00.00 labels', () => {
	const m = model([[9.3, 10.1, 11.7]]);
	assert.deepEqual([m.valueAxis.title, m.valueAxis.min, m.valueAxis.max, m.valueAxis.step], ['Magnitude', 9, 12, 0.5]);
	assert.deepEqual(m.valueAxis.ticks.map(t => t.label), ['09.00', '09.50', '10.00', '10.50', '11.00', '11.50', '12.00']);
	const distance = model([[0.31, 0.5, 0.79]], { chartType: 'SunDistance' });
	assert.deepEqual([distance.valueAxis.min, distance.valueAxis.max, distance.valueAxis.step], [0, 1, 0.1]);
	assert.equal(distance.valueAxis.ticks.length, 11);
	assert.equal(distance.valueAxis.ticks[3].label, '00.30');
	const negative = model([[-1.5, 0, 0.4]], { minimum: { checked: true, value: -1.5 } });
	assert.deepEqual(negative.valueAxis.ticks.map(t => t.label).slice(0, 3), ['-01.50', '-01.00', '-00.50']);
});

test('steps by span: 0.1 to 1, 0.2 to 2, 0.5 to 5, 1 to 10, then 2', () => {
	const step = (/** @type {number} */ max) => model([[0, 0, 0]], { chartType: 'SunDistance', minimum: { checked: true, value: 0 }, maximum: { checked: true, value: max } }).valueAxis.step;
	assert.deepEqual([1, 1.01, 2, 2.5, 5, 6, 10, 10.5].map(step), [0.1, 0.2, 0.2, 0.5, 0.5, 1, 1, 2]);
});

test('Minimum and Maximum fix the ends; a range they leave empty falls back to the samples', () => {
	const fixed = model([[9.3, 10.1, 11.7]], { minimum: { checked: true, value: 8.5 }, maximum: { checked: true, value: 13.25 } });
	assert.deepEqual([fixed.valueAxis.min, fixed.valueAxis.max], [8.5, 13.25]);
	const crossed = model([[9.3, 10.1, 11.7]], { minimum: { checked: true, value: 14 } });
	assert.deepEqual([crossed.valueAxis.min, crossed.valueAxis.max], [9, 12]);
});

test('several comets default the maximum to 15 for magnitudes and 2 AU for distances', () => {
	assert.equal(model([[9, 9, 9], [20, 20, 20]]).valueAxis.max, 15);
	assert.equal(model([[0.5, 0.5, 0.5], [3, 3, 3]], { chartType: 'EarthDistance' }).valueAxis.max, 2);
	// one comet in multiple mode, or single mode: the samples' maximum
	assert.equal(model([[9, 10, 11]], {}, { multiple: true }).valueAxis.max, 12);
});

test('a value series is drawn when one of its values is not past the maximum', () => {
	const m = model([[9, 16, 16], [15.01, 16, 20]], { perihelionLine: { checked: false, color: '#000000' }, nowLine: { checked: false, color: '#000000' } });
	assert.deepEqual(m.series.map(s => `${s.kind} ${s.comet}`), ['value A']);
	assert.equal(m.series[0].x, jds);
	assert.equal(m.series[0].color, '#FF0000');
});

test('perihelion lines: after each drawn series in multiple mode, after all in single mode', () => {
	const multiple = model([[9, 9, 9], [20, 20, 20], ]);
	assert.deepEqual(multiple.series.map(s => `${s.kind} ${s.comet}`), ['value A', 'perihelion A', 'now null']);
	const single = model([[20, 20, 20]], { minimum: { checked: true, value: 5 }, maximum: { checked: true, value: 12 } });
	assert.deepEqual(single.series.map(s => `${s.kind} ${s.comet}`), ['perihelion A', 'now null']);
	assert.deepEqual([single.series[0].x, single.series[0].y], [[startJd + 4, startJd + 4], [5, 12]]);
	assert.equal(single.series[0].color, '#4169E1');
	assert.equal(single.title, 'A');
	assert.equal(multiple.title, null);
});

test('the now line only strictly inside the time axis', () => {
	const kinds = (/** @type {number} */ nowJd) => model([[9, 9, 9]], {}, { nowJd }).series.map(s => s.kind);
	assert.deepEqual(kinds(startJd + 7), ['value', 'perihelion', 'now']);
	assert.deepEqual(kinds(startJd), ['value', 'perihelion']);
	assert.deepEqual(kinds(startJd + 10), ['value', 'perihelion']);
	const now = model([[9, 9, 9]], {}, { nowJd: startJd + 7 }).series[2];
	assert.deepEqual([now.comet, now.color, now.x], [null, '#32CD32', [startJd + 7, startJd + 7]]);
});

test('the time axis: Start to End, long labels up to 3 days', () => {
	const m = model([[9, 9, 9]]);
	assert.deepEqual({ ...m.timeAxis }, { title: 'Date', min: startJd, max: startJd + 10, labelFormat: 'dd MMM yyyy' });
	const short = model([[9, 9, 9]], { end: start + 3 * DAY });
	assert.equal(short.timeAxis.labelFormat, 'dd MMM yyyy HH:mm');
	assert.throws(() => graphModel({ comets: [], nearest: [], settings: defaultGraphSettings(start), multiple: true, observer: { latitude: 0, longitude: 0 }, nowJd: 0 }, { jds, values: [] }), /at least one comet/);
});

test('perihelion times: back to the first after Start less a period, then on before End', () => {
	const year = 365.25;
	assert.deepEqual(perihelionTimes(1000, 1, 0, 3000), [269.5, 634.75, 1000, 1365.25, 1730.5, 2095.75, 2461, 2826.25]);
	assert.deepEqual(perihelionTimes(100, 1, 0, 3000)[0], 100);
	// the first line may lie before Start: Start less a period is not passed
	assert.deepEqual(perihelionTimes(-200, 1, 0, 400), [-200, -200 + year]);
	// from a Tn after End, back into the range as well
	assert.deepEqual(perihelionTimes(5000, 1, 0, 3000).slice(0, 2), [251.75, 617]);
	assert.deepEqual(perihelionTimes(5000, 1e6, 0, 3000), []);
	assert.deepEqual(perihelionTimes(-5000, 1e6, 0, 3000), [-5000]);
	// a period that is not positive and finite: the line at Tn alone (spec 10, D6)
	assert.deepEqual(perihelionTimes(100, 0, 0, 3000), [100]);
	assert.deepEqual(perihelionTimes(100, NaN, 0, 3000), [100]);
	assert.deepEqual(perihelionTimes(5000, 0, 0, 3000), []);
});

test('hover: the topmost series, the nearest sample, and the tooltip texts', () => {
	const m = model([[9, 10, 11], [9.5, 10, 10.5]]);
	const area = { left: 50, top: 20, width: 500, height: 300 };
	const at = projection(m, area);
	assert.equal(at.x(startJd), 50);
	assert.equal(at.x(startJd + 10), 550);
	assert.equal(at.y(m.valueAxis.min), 20);
	assert.equal(at.y(m.valueAxis.max), 320);
	assert.ok(at.y(9) < at.y(11), 'smaller values are higher');
	// B's series is drawn after A's: at the shared sample B wins
	const shared = hoverAt(m, area, at.x(startJd + 5), at.y(10));
	assert.deepEqual(shared && { ...shared }, { title: 'B', lines: ['Time: 06.01.2026. 00:00:00', 'Magnitude: 10.00'] });
	const nearA = hoverAt(m, area, at.x(startJd + 10) - 2, at.y(11) + 3);
	assert.deepEqual(nearA?.lines, ['Time: 11.01.2026. 00:00:00', 'Magnitude: 11.00']);
	assert.equal(hoverAt(m, area, at.x(startJd + 10) - 2, at.y(11) + 7), null);
	const now = hoverAt(m, area, at.x(startJd + 7) + 3, 100);
	assert.deepEqual(now && { ...now }, { title: 'Today', lines: ['Time: 08.01.2026. 00:00:00'] });
	const perihelion = hoverAt(m, area, at.x(startJd + 4), 200);
	assert.deepEqual(perihelion && { ...perihelion }, { title: 'B', lines: ['Perihelion time: 05.01.2026. 00:00:00'] });
	assert.equal(hoverAt(m, area, 10, 100), null);
	assert.equal(hoverAt(m, area, 300, 330), null, 'below the plot');
});

test('hover: distances, and samples outside the axis cannot be hit', () => {
	const m = model([[0.25, 1.2, 0.5]], { chartType: 'EarthDistance', maximum: { checked: true, value: 1 } });
	const area = { left: 0, top: 0, width: 100, height: 100 };
	const at = projection(m, area);
	assert.deepEqual(hoverAt(m, area, at.x(startJd), at.y(0.25))?.lines, ['Time: 01.01.2026. 00:00:00', 'Distance: 0.2500 AU']);
	assert.equal(hoverAt(m, area, at.x(startJd + 5), 100), null, 'the sample at 1.2 AU lies below the axis');
});
