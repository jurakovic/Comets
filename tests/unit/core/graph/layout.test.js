import { test } from 'node:test';
import assert from 'node:assert/strict';
import { graphLayout } from '../../../../app/js/core/graph/layout.js';
import { projection } from '../../../../app/js/core/graph/hover.js';
import { graphModel } from '../../../../app/js/core/graph/model.js';
import { defaultGraphSettings } from '../../../../app/js/core/graph/settings.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { partsToInstant } from '../../../../app/js/core/time/calendar.js';
import { instantToJd } from '../../../../app/js/core/time/julian.js';

const c = createComet({ full: 'C/2024 A1 (Test)', id: 'C/2024 A1', Ty: 2026, Tm: 1, Td: 1, q: 1, e: 0.5 });
assert.ok(c);

/** a label font of 7 px a character, lines of 15 px, titles of 19 px */
const METRICS = Object.freeze({ width: (/** @type {string} */ text) => text.length * 7, lineHeight: 15, titleHeight: 19 });

/**
 * @param {number} year
 * @param {number} month
 * @param {number} day
 * @param {number} [hour]
 * @param {number} [minute]
 * @param {number} [second]
 */
const utc = (year, month, day, hour = 0, minute = 0, second = 0) => partsToInstant({ year, month, day, hour, minute, second });

/**
 * a model from Start to End with the value axis fixed from min to max
 * @param {number} start
 * @param {number} end
 * @param {{ min?: number, max?: number, multiple?: boolean }} [options]
 */
function model(start, end, { min = 2, max = 12, multiple = false } = {}) {
	const settings = {
		...defaultGraphSettings(start), start, end,
		minimum: { checked: true, value: min }, maximum: { checked: true, value: max },
		perihelionLine: { checked: false, color: '#000000' }, nowLine: { checked: false, color: '#000000' }
	};
	const startJd = instantToJd(start);
	return graphModel({ comets: [c], nearest: [startJd], settings, multiple, observer: { latitude: 0, longitude: 0 }, nowJd: 0 }, { jds: [startJd], values: [[min]] });
}

/**
 * the time labels of a model on a chart width wide
 * @param {ReturnType<typeof model>} m
 * @param {number} width
 */
const timeLabels = (m, width) => graphLayout(m, { width, height: 400 }, METRICS).timeTicks.map(tick => tick.label);

const JUNE = utc(2024, 6, 1);
const JANUARY = utc(2025, 1, 1);

test('the plot area: the title and the time axis above it, the value axis left of it', () => {
	const single = graphLayout(model(JUNE, JANUARY), { width: 800, height: 400 }, METRICS);
	const multiple = graphLayout(model(JUNE, JANUARY, { multiple: true }), { width: 800, height: 400 }, METRICS);
	// padding 8; the Date line, the labels' line, a gap of 4 and the ticks of 4 above the plot; half
	// a time label (77 px) and the padding right of it; half a value label and the padding below
	assert.deepEqual({ ...multiple.area }, { left: 70, top: 46, width: 683.5, height: 338.5 });
	assert.equal(multiple.title, null);
	assert.deepEqual([multiple.timeLabelY, multiple.timeTitle.y], [23, 8]);
	// the title's line and a gap more
	assert.equal(single.area.top, 46 + 19 + 4);
	assert.deepEqual({ ...single.title }, { text: 'C/2024 A1 (Test)', x: 400, y: 8 });
	assert.deepEqual([single.timeTitle.y, single.timeLabelY], [31, 46]);
	// Date at the right end of the axis, the value title upwards from the top of the axis
	assert.deepEqual({ ...multiple.timeTitle }, { text: 'Date', x: 753.5, y: 8 });
	assert.deepEqual({ ...multiple.valueTitle }, { text: 'Magnitude', x: 15.5, y: 46 });
	// the value title's line, a gap, the widest label (5 characters), a gap and the ticks
	assert.deepEqual([multiple.valueLabelX, multiple.tickLength], [62, 4]);
	const negative = graphLayout(model(JUNE, JANUARY, { min: -1.5 }), { width: 800, height: 400 }, METRICS);
	assert.equal(negative.area.left, 77, '-01.50 is a character wider');
});

test('a chart too small for a plot still has a plot of a pixel', () => {
	const { area } = graphLayout(model(JUNE, JANUARY), { width: 50, height: 40 }, METRICS);
	assert.deepEqual([area.width, area.height], [1, 1]);
});

test('the value ticks where the projection puts them, every label while they fit', () => {
	const m = model(JUNE, JANUARY);
	const layout = graphLayout(m, { width: 800, height: 400 }, METRICS);
	const at = projection(m, layout.area);
	assert.deepEqual(layout.valueTicks.map(tick => tick.y), m.valueAxis.ticks.map(tick => at.y(tick.value)));
	assert.deepEqual(layout.valueTicks.map(tick => tick.label), ['02.00', '03.00', '04.00', '05.00', '06.00', '07.00', '08.00', '09.00', '10.00', '11.00', '12.00']);
	assert.equal(layout.valueTicks[0].y, layout.area.top, 'the axis is reversed');
});

test('value labels thin out to every second or fourth tick from the first when they would touch', () => {
	const wide = model(JUNE, JANUARY, { min: -20, max: 40, multiple: true });
	assert.equal(wide.valueAxis.ticks.length, 31);
	const labels = (/** @type {number} */ height) => graphLayout(wide, { width: 800, height }, METRICS).valueTicks.map(tick => tick.label);
	// 338.5 px for 30 steps: 11.3 px apart, a line of 15 and 2 between needs every second
	assert.deepEqual(labels(400).slice(0, 5), ['-20.00', null, '-16.00', null, '-12.00']);
	assert.equal(labels(400).filter(label => label !== null).length, 16);
	assert.deepEqual(labels(200).slice(0, 5), ['-20.00', null, null, null, '-12.00']);
	assert.equal(labels(800).filter(label => label !== null).length, 31);
});

test('time ticks: the finest calendar step whose labels fit, months over half a year', () => {
	const m = model(JUNE, JANUARY);
	assert.deepEqual(timeLabels(m, 800), ['01 Jun 2024', '01 Jul 2024', '01 Aug 2024', '01 Sep 2024', '01 Oct 2024', '01 Nov 2024', '01 Dec 2024', '01 Jan 2025']);
	assert.deepEqual(timeLabels(m, 400), ['01 Jul 2024', '01 Oct 2024', '01 Jan 2025']);
	assert.deepEqual(timeLabels(m, 1600), [
		'01 Jun 2024', '16 Jun 2024', '01 Jul 2024', '16 Jul 2024', '01 Aug 2024', '16 Aug 2024', '01 Sep 2024', '16 Sep 2024',
		'01 Oct 2024', '16 Oct 2024', '01 Nov 2024', '16 Nov 2024', '01 Dec 2024', '16 Dec 2024', '01 Jan 2025'
	]);
	// the ticks lie where the projection puts their dates
	const layout = graphLayout(m, { width: 800, height: 400 }, METRICS);
	const at = projection(m, layout.area);
	assert.equal(layout.timeTicks[1].x, at.x(instantToJd(utc(2024, 7, 1))));
});

test('the labels at the ends of the axis lie over their ticks, or are moved in to stay on the chart', () => {
	const layout = graphLayout(model(JUNE, JANUARY), { width: 800, height: 400 }, METRICS);
	const first = layout.timeTicks[0];
	const last = layout.timeTicks[layout.timeTicks.length - 1];
	assert.deepEqual([first.x, first.labelX], [70, 70]);
	assert.deepEqual([last.x, last.labelX], [753.5, 753.5]);
	// a label at End wider than Start's
	const wider = { ...METRICS, width: (/** @type {string} */ text) => text === '01 Jan 2025' ? 200 : text.length * 7 };
	const moved = graphLayout(model(JUNE, JANUARY), { width: 800, height: 400 }, wider).timeTicks;
	assert.deepEqual([moved[moved.length - 1].x, moved[moved.length - 1].labelX], [753.5, 700]);
});

test('up to 3 days, hours with the time of day; past 3 days, no step finer than a day', () => {
	const short = model(utc(2026, 10, 17), utc(2026, 10, 19));
	assert.equal(short.timeAxis.labelFormat, 'dd MMM yyyy HH:mm');
	assert.deepEqual(timeLabels(short, 1200), [
		'17 Oct 2026 00:00', '17 Oct 2026 06:00', '17 Oct 2026 12:00', '17 Oct 2026 18:00', '18 Oct 2026 00:00',
		'18 Oct 2026 06:00', '18 Oct 2026 12:00', '18 Oct 2026 18:00', '19 Oct 2026 00:00'
	]);
	assert.deepEqual(timeLabels(model(utc(2026, 10, 17, 10), utc(2026, 10, 17, 11)), 1200).slice(0, 3), ['17 Oct 2026 10:00', '17 Oct 2026 10:10', '17 Oct 2026 10:20']);
	const four = model(utc(2026, 10, 17), utc(2026, 10, 21));
	assert.deepEqual(timeLabels(four, 2560), ['17 Oct 2026', '18 Oct 2026', '19 Oct 2026', '20 Oct 2026', '21 Oct 2026']);
});

test('steps of days skip a day too near the next month\'s first', () => {
	// every 2 days: 29 January, then 1 February, not 31 January
	assert.deepEqual(timeLabels(model(utc(2026, 1, 20), utc(2026, 2, 10)), 1280), [
		'21 Jan 2026', '23 Jan 2026', '25 Jan 2026', '27 Jan 2026', '29 Jan 2026', '01 Feb 2026', '03 Feb 2026', '05 Feb 2026', '07 Feb 2026', '09 Feb 2026'
	]);
});

test('years over decades, on multiples of the step; negative years', () => {
	assert.deepEqual(timeLabels(model(utc(2010, 1, 1), utc(2030, 1, 1)), 800), ['01 Jan 2010', '01 Jan 2015', '01 Jan 2020', '01 Jan 2025', '01 Jan 2030']);
	assert.deepEqual(timeLabels(model(utc(-146, 8, 1), utc(-146, 12, 1)), 800), ['01 Aug -0146', '01 Sep -0146', '01 Oct -0146', '01 Nov -0146', '01 Dec -0146']);
	assert.deepEqual(timeLabels(model(utc(-160, 3, 1), utc(-100, 3, 1)), 800), ['01 Jan -0150', '01 Jan -0140', '01 Jan -0130', '01 Jan -0120', '01 Jan -0110', '01 Jan -0100']);
});

test('a narrow chart: a coarse step with a single label, or the coarsest step\'s labels that clear each other', () => {
	assert.deepEqual(timeLabels(model(utc(1850, 1, 1), utc(2149, 1, 1)), 200), ['01 Jan 1900']);
	assert.deepEqual(timeLabels(model(utc(2010, 1, 1), utc(2030, 1, 1)), 120), ['01 Jan 2020']);
});

test('a step with no tick in the span is passed over: the coarsest step with ticks is thinned', () => {
	// every 2 days (17 and 19 October) is the coarsest step with a tick; every 5 days starts on
	// the 16th and the 21st
	assert.deepEqual(timeLabels(model(utc(2026, 10, 17), utc(2026, 10, 19)), 240), ['17 Oct 2026 00:00']);
	assert.deepEqual(timeLabels(model(utc(2026, 10, 17), utc(2026, 10, 20, 12)), 240), ['17 Oct 2026']);
});

test('a span with no tick on any step: Start alone', () => {
	const m = model(utc(2026, 10, 17, 10, 0, 10), utc(2026, 10, 17, 10, 0, 50));
	const { timeTicks, area } = graphLayout(m, { width: 800, height: 400 }, METRICS);
	assert.deepEqual(timeTicks.map(tick => [tick.x, tick.label]), [[area.left, '17 Oct 2026 10:00']]);
});

test('at any width the time labels are on the axis, apart, and each date once', () => {
	const spans = [
		[utc(2026, 1, 1), utc(2026, 1, 1, 1)], [utc(2026, 1, 1), utc(2026, 1, 3)], [utc(2026, 1, 1), utc(2026, 1, 4)],
		[utc(2026, 1, 1), utc(2026, 1, 11)], [utc(2026, 1, 1), utc(2026, 4, 11)], [utc(2026, 1, 1), utc(2027, 1, 1)],
		[utc(2026, 1, 1), utc(2033, 5, 1)], [utc(2000, 1, 1), utc(2040, 1, 1)], [utc(1800, 1, 1), utc(2098, 12, 31)],
		// spans that start between the ticks of the coarser steps
		[utc(2026, 10, 17), utc(2026, 10, 19)], [utc(2026, 10, 17), utc(2026, 10, 20, 12)], [utc(2026, 3, 7, 5, 23), utc(2026, 3, 7, 9, 41)],
		[utc(2026, 10, 17, 10, 0, 10), utc(2026, 10, 17, 10, 0, 50)], [utc(2026, 5, 13, 7), utc(2028, 2, 3)], [utc(1913, 6, 9), utc(2207, 2, 1)]
	];
	for (const [start, end] of spans) {
		for (const width of [240, 412, 800, 1280, 2560]) {
			const m = model(start, end);
			const { timeTicks, area } = graphLayout(m, { width, height: 400 }, METRICS);
			const label = `${new Date(start).toISOString()} – ${new Date(end).toISOString()} at ${width}`;
			assert.ok(timeTicks.length >= 1, label);
			assert.equal(new Set(timeTicks.map(tick => tick.label)).size, timeTicks.length, label);
			for (const [i, tick] of timeTicks.entries()) {
				assert.ok(tick.x >= area.left && tick.x <= area.left + area.width, label);
				const half = METRICS.width(tick.label) / 2;
				assert.ok(tick.labelX - half >= 0 && tick.labelX + half <= width, label);
				if (i > 0) {
					const before = timeTicks[i - 1];
					assert.ok(tick.labelX - half - (before.labelX + METRICS.width(before.label) / 2) >= 12, label);
				}
			}
		}
	}
});
