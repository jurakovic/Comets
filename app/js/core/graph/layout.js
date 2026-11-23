// The graph's layout on a chart of a given size: the plot area, where the titles go, and the ticks
// of both axes with the labels that fit, for a renderer to draw and the hit test to share.
import { roundEven } from '../../lib/math.js';
import { compileDateFormat } from '../format/dates.js';
import { addMonths, addYears, DAY_MS, daysInMonth, instantToParts, partsToInstant } from '../time/calendar.js';
import { instantToJd } from '../time/julian.js';
import { projection } from './hover.js';

/** @typedef {import('./model.js').GraphModel} GraphModel */
/** @typedef {import('./hover.js').PlotArea} PlotArea */

/**
 * the chart's fonts as the renderer measures them, in its pixels
 * @typedef {object} TextMetrics
 * @property {(text: string) => number} width a label's width in the label font
 * @property {number} lineHeight of the label font
 * @property {number} titleHeight of the title font
 */

/**
 * a text and where it goes
 * @typedef {object} Placed
 * @property {string} text
 * @property {number} x
 * @property {number} y
 */

/**
 * a tick of the time axis; its label is centred on labelX, which is x unless the label would
 * leave the chart
 * @typedef {object} TimeTick
 * @property {number} x
 * @property {string} label
 * @property {number} labelX
 */

/**
 * a tick of the value axis; label is null where the labels are thinned out
 * @typedef {object} ValueTick
 * @property {number} y
 * @property {string | null} label
 */

/**
 * @typedef {object} GraphLayout
 * @property {PlotArea} area the plot: the time axis along its top, the value axis down its left
 * @property {Placed | null} title centred on x, its top at y; none in multiple mode
 * @property {Placed} timeTitle its right end at x, at the end of the axis; its top at y
 * @property {Placed} valueTitle read upwards, the middle of its line at x and its end at y, the
 *   top of the axis
 * @property {number} timeLabelY the top of the time labels
 * @property {ReadonlyArray<TimeTick>} timeTicks
 * @property {number} valueLabelX the right end of the value labels
 * @property {ReadonlyArray<ValueTick>} valueTicks
 * @property {number} tickLength outwards from the axis
 */

/** the space around the chart and between its parts */
const PAD = 8;
const GAP = 4;
const TICK = 4;
/** beyond the padding, right of the plot, at least */
const RIGHT = 16;
/** the least space between two time labels */
const LABEL_GAP = 12;
/** the least space between two value labels, beyond the line height */
const VALUE_LABEL_GAP = 2;

const EPOCH_JD = instantToJd(0);

/**
 * a calendar unit of the time axis: the start of a step of count units at or before an instant,
 * and the next start after one; days is the unit's length, roughly
 * @typedef {object} TimeUnit
 * @property {number} days
 * @property {(instant: number, count: number) => number} floor
 * @property {(instant: number, count: number) => number} next
 */

/**
 * minutes and hours, whose counts divide a day: steps from midnight, as the epoch is one
 * @param {number} ms
 * @returns {TimeUnit}
 */
const fixedUnit = ms => ({
	days: ms / DAY_MS,
	floor: (instant, count) => Math.floor(instant / (ms * count)) * ms * count,
	next: (instant, count) => instant + ms * count
});

/**
 * whether a day of the month starts a step of count days: the 1st, 1 + count, 1 + 2·count…,
 * but none within three quarters of a step of the next month's 1st
 * @param {number} year
 * @param {number} month
 * @param {number} day
 * @param {number} count
 */
const startsDayStep = (year, month, day, count) =>
	day === 1 || ((day - 1) % count === 0 && daysInMonth(year, month) - day + 1 >= 0.75 * count);

/**
 * @param {number} year
 * @param {number} month may be 13, the next year's January
 * @param {number} day
 */
const midnight = (year, month, day) => partsToInstant({ year, month, day, hour: 0, minute: 0, second: 0 });

/** @type {Readonly<Record<'minute' | 'hour' | 'day' | 'month' | 'year', TimeUnit>>} */
const UNITS = Object.freeze({
	minute: fixedUnit(60_000),
	hour: fixedUnit(3_600_000),
	day: {
		days: 1,
		floor(instant, count) {
			const { year, month, day } = instantToParts(instant);
			let start = 1 + Math.floor((day - 1) / count) * count;
			while (!startsDayStep(year, month, start, count)) start -= count;
			return midnight(year, month, start);
		},
		next(instant, count) {
			const { year, month, day } = instantToParts(instant);
			const start = day + count;
			return start <= daysInMonth(year, month) && startsDayStep(year, month, start, count)
				? midnight(year, month, start) : midnight(year, month + 1, 1);
		}
	},
	month: {
		days: 30.436875,
		floor(instant, count) {
			const { year, month } = instantToParts(instant);
			return midnight(year, month - ((month - 1) % count), 1);
		},
		// from a 1st, so the day is never clamped
		next: addMonths
	},
	year: {
		days: 365.2425,
		floor(instant, count) {
			const { year } = instantToParts(instant);
			return midnight(year - (((year % count) + count) % count), 1, 1);
		},
		next: addYears
	}
});

/** @typedef {{ unit: keyof typeof UNITS, count: number }} TimeStep */

/** the steps the time axis takes, finest first */
const TIME_STEPS = Object.freeze(/** @type {TimeStep[]} */ ([
	...[1, 2, 5, 10, 15, 30].map(count => ({ unit: 'minute', count })),
	...[1, 2, 3, 6, 12].map(count => ({ unit: 'hour', count })),
	...[1, 2, 5, 10, 15].map(count => ({ unit: 'day', count })),
	...[1, 2, 3, 6].map(count => ({ unit: 'month', count })),
	...[1, 2, 5, 10, 20, 50, 100].map(count => ({ unit: 'year', count }))
]));

/** @typedef {TimeTick & { width: number }} MeasuredTick */

/**
 * where the labels go: the axis' x of a JD, the label format, the label font, the chart's width
 * @typedef {object} TickPlace
 * @property {(jd: number) => number} x
 * @property {(parts: import('../time/calendar.js').DateParts) => string} format
 * @property {TextMetrics} metrics
 * @property {number} chartWidth
 */

/**
 * Start, the whole second the axis begins at
 * @param {GraphModel['timeAxis']} axis
 */
const startOf = axis => roundEven(((axis.min - EPOCH_JD) * DAY_MS) / 1000) * 1000;

/**
 * a tick at an instant, its label kept on the chart
 * @param {number} instant
 * @param {TickPlace} place
 * @returns {MeasuredTick}
 */
function tickAt(instant, { x, format, metrics, chartWidth }) {
	const at = x(instantToJd(instant));
	const label = format(instantToParts(instant));
	const width = metrics.width(label);
	return { x: at, label, labelX: Math.min(Math.max(at, width / 2), chartWidth - width / 2), width };
}

/**
 * the ticks of one step from Start to End
 * @param {TimeStep} step
 * @param {GraphModel['timeAxis']} axis
 * @param {TickPlace} place
 * @returns {MeasuredTick[]}
 */
function stepTicks({ unit, count }, axis, place) {
	const { floor, next } = UNITS[unit];
	/** @type {MeasuredTick[]} */
	const ticks = [];
	// ticks and labels in the proleptic Gregorian calendar, as Start and End are given, where the
	// desktop's axis and the tooltips show JDToDateTime's Julian dates before 1582-10-15
	let t = floor(startOf(axis), count);
	while (instantToJd(t) < axis.min) t = next(t, count);
	for (; instantToJd(t) <= axis.max; t = next(t, count)) ticks.push(tickAt(t, place));
	return ticks;
}

/**
 * the width of a time label, Start's
 * @param {GraphModel['timeAxis']} axis
 * @param {TextMetrics} metrics
 */
const typicalLabelWidth = (axis, metrics) => metrics.width(compileDateFormat(axis.labelFormat)(instantToParts(startOf(axis))));

/**
 * @param {MeasuredTick} a
 * @param {MeasuredTick} b the next to the right
 */
const apart = (a, b) => b.labelX - b.width / 2 - (a.labelX + a.width / 2) >= LABEL_GAP;

/**
 * the time axis' ticks: those of the finest step with ticks in the span whose labels all fit side
 * by side, no finer than the label format tells apart; when none fits, the coarsest step's with
 * ticks in the span, each kept when its label clears the one kept before it; Start alone when no
 * step tried has a tick in the span. A step with more than twice the ticks that could stand side
 * by side is not tried, so on a very narrow plot none may be
 * @param {GraphModel['timeAxis']} axis
 * @param {PlotArea} area
 * @param {(jd: number) => number} x
 * @param {TextMetrics} metrics
 * @param {number} chartWidth
 * @returns {MeasuredTick[]}
 */
function timeTicks(axis, area, x, metrics, chartWidth) {
	/** @type {TickPlace} */
	const place = { x, format: compileDateFormat(axis.labelFormat), metrics, chartWidth };
	// a label without the time of day would repeat within a day
	const steps = axis.labelFormat.includes('HH') ? TIME_STEPS : TIME_STEPS.filter(s => UNITS[s.unit].days >= 1);
	const typical = typicalLabelWidth(axis, metrics) + LABEL_GAP;
	const span = axis.max - axis.min;
	/** @type {MeasuredTick[]} the ticks of the coarsest step tried so far with any in the span */
	let coarsest = [];
	for (const step of steps) {
		// twice the ticks that could stand side by side cannot fit: not worth making
		if (span / (UNITS[step.unit].days * step.count) > (2 * area.width) / typical + 2) continue;
		const ticks = stepTicks(step, axis, place);
		// a step with no tick in the span leaves the axis bare: no fit
		if (ticks.length === 0) continue;
		if (ticks.every((tick, i) => i === 0 || apart(ticks[i - 1], tick))) return ticks;
		coarsest = ticks;
	}
	if (coarsest.length === 0) return [tickAt(startOf(axis), place)];
	/** @type {MeasuredTick[]} */
	const kept = [];
	for (const tick of coarsest) {
		if (kept.length === 0 || apart(kept[kept.length - 1], tick)) kept.push(tick);
	}
	return kept;
}

/**
 * the layout of a model on a chart of width × height: the title, the time axis' title and labels
 * above the plot, the value axis' title and labels left of it
 * @param {GraphModel} model
 * @param {{ width: number, height: number }} size
 * @param {TextMetrics} metrics
 * @returns {GraphLayout}
 */
export function graphLayout(model, { width, height }, metrics) {
	const { lineHeight } = metrics;
	let y = PAD;
	const titleY = y;
	if (model.title !== null) y += metrics.titleHeight + GAP;
	const timeTitleY = y;
	const timeLabelY = y + lineHeight;
	const top = timeLabelY + lineHeight + GAP + TICK;
	// half the bottom value label hangs below the plot
	const bottom = height - PAD - lineHeight / 2;

	const labelWidth = Math.max(0, ...model.valueAxis.ticks.map(tick => metrics.width(tick.label)));
	const valueLabelX = PAD + lineHeight + GAP + labelWidth;
	const left = valueLabelX + GAP + TICK;
	// room for half a time label, so that one at End stays over its tick
	const right = width - PAD - Math.max(RIGHT, typicalLabelWidth(model.timeAxis, metrics) / 2);

	/** @type {PlotArea} */
	const area = Object.freeze({ left, top, width: Math.max(1, right - left), height: Math.max(1, bottom - top) });
	const at = projection(model, area);

	const { ticks, step, min, max } = model.valueAxis;
	// labels every stride-th tick from the first, so that they do not touch
	const spacing = (area.height * step) / (max - min);
	const stride = Math.max(1, Math.ceil((lineHeight + VALUE_LABEL_GAP) / spacing));

	return Object.freeze({
		area,
		title: model.title === null ? null : Object.freeze({ text: model.title, x: width / 2, y: titleY }),
		timeTitle: Object.freeze({ text: model.timeAxis.title, x: left + area.width, y: timeTitleY }),
		valueTitle: Object.freeze({ text: model.valueAxis.title, x: PAD + lineHeight / 2, y: top }),
		timeLabelY,
		timeTicks: Object.freeze(timeTicks(model.timeAxis, area, at.x, metrics, width)
			.map(({ x, label, labelX }) => Object.freeze({ x, label, labelX }))),
		valueLabelX,
		valueTicks: Object.freeze(ticks.map((tick, k) => Object.freeze({ y: at.y(tick.value), label: k % stride === 0 ? tick.label : null }))),
		tickLength: TICK
	});
}
