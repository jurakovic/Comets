// Where the graph model lies on the chart, and what the pointer is over: the tooltip of a sample,
// a perihelion line or the now line. docs/spec/07-graph.md, Interaction.
import { compileDateFormat, DATE_FULL } from '../format/dates.js';
import { instantToParts } from '../time/calendar.js';
import { jdToInstant } from '../time/julian.js';
import { chartTypeOf } from './settings.js';

/** @typedef {import('./model.js').GraphModel} GraphModel */

/**
 * the plot area, in the renderer's pixels
 * @typedef {object} PlotArea
 * @property {number} left
 * @property {number} top
 * @property {number} width
 * @property {number} height
 */

/**
 * @typedef {object} Tooltip
 * @property {string} title
 * @property {ReadonlyArray<string>} lines
 */

const TIME = compileDateFormat(DATE_FULL);

/**
 * the mapping of JDs and values to pixels; smaller values are higher (the axis is reversed)
 * @param {GraphModel} model
 * @param {PlotArea} area
 * @returns {{ x: (jd: number) => number, y: (value: number) => number, jd: (x: number) => number }}
 */
export function projection({ timeAxis, valueAxis }, { left, top, width, height }) {
	const time = timeAxis.max - timeAxis.min;
	const values = valueAxis.max - valueAxis.min;
	return {
		x: jd => left + ((jd - timeAxis.min) / time) * width,
		y: value => top + ((value - valueAxis.min) / values) * height,
		jd: x => timeAxis.min + ((x - left) / width) * time
	};
}

/** a time as the tooltips show it: UTC, `dd.MM.yyyy. HH:mm:ss`, seconds truncated */
const timeText = (/** @type {number} */ jd) => TIME(instantToParts(jdToInstant(jd)));

/**
 * the index of the first x not below the value, in an ascending array
 * @param {ReadonlyArray<number>} xs
 * @param {number} value
 */
function lowerBound(xs, value) {
	let low = 0;
	let high = xs.length;
	while (low < high) {
		const middle = (low + high) >>> 1;
		if (xs[middle] < value) low = middle + 1;
		else high = middle;
	}
	return low;
}

/**
 * what a hit-test needs to know about the pointer
 * @typedef {object} Pointer
 * @property {number} px
 * @property {number} py
 * @property {number} radius
 * @property {PlotArea} area
 * @property {ReturnType<typeof projection>} at
 * @property {GraphModel} model
 */

/**
 * a vertical line is hit within radius of its x, when it lies on the time axis and the pointer is
 * over the plot
 * @param {import('./model.js').GraphSeries} series
 * @param {Pointer} pointer
 */
function lineHit({ x: [t] }, { px, py, radius, area, at, model: { timeAxis } }) {
	if (t < timeAxis.min || t > timeAxis.max || Math.abs(at.x(t) - px) > radius) return null;
	return py >= area.top && py <= area.top + area.height ? t : null;
}

/**
 * the nearest sample within radius, the first of equally near ones; only samples inside the axes
 * @param {import('./model.js').GraphSeries} series
 * @param {Pointer} pointer
 */
function nearestSample({ x, y }, { px, py, radius, at, model: { timeAxis, valueAxis } }) {
	let best = -1;
	let nearest = Infinity;
	for (let k = lowerBound(x, at.jd(px - radius)); k < x.length && x[k] <= at.jd(px + radius); k++) {
		if (x[k] < timeAxis.min || x[k] > timeAxis.max || y[k] < valueAxis.min || y[k] > valueAxis.max) continue;
		const dx = at.x(x[k]) - px;
		const dy = at.y(y[k]) - py;
		const distance = dx * dx + dy * dy;
		if (distance <= radius * radius && distance < nearest) {
			best = k;
			nearest = distance;
		}
	}
	return best;
}

/** @type {Readonly<Record<import('./model.js').GraphSeries['kind'], (series: import('./model.js').GraphSeries, pointer: Pointer) => Tooltip | null>>} */
const HIT_TESTS = Object.freeze({
	value(series, pointer) {
		const k = nearestSample(series, pointer);
		if (k < 0) return null;
		const lines = [`Time: ${timeText(series.x[k])}`, chartTypeOf(pointer.model.chartType).tooltip(series.y[k])];
		return Object.freeze({ title: series.comet ?? '', lines: Object.freeze(lines) });
	},
	perihelion(series, pointer) {
		const t = lineHit(series, pointer);
		return t === null ? null : Object.freeze({ title: series.comet ?? '', lines: Object.freeze([`Perihelion time: ${timeText(t)}`]) });
	},
	now(series, pointer) {
		const t = lineHit(series, pointer);
		return t === null ? null : Object.freeze({ title: 'Today', lines: Object.freeze([`Time: ${timeText(t)}`]) });
	}
});

/**
 * what the pointer at (px, py) is over, within radius pixels: the topmost series wins, which is
 * the last drawn, and in a value series the nearest sample; only what lies inside the plot area
 * can be hit
 * @param {GraphModel} model
 * @param {PlotArea} area
 * @param {number} px
 * @param {number} py
 * @param {number} [radius]
 * @returns {Tooltip | null}
 */
export function hoverAt(model, area, px, py, radius = 6) {
	if (px < area.left - radius || px > area.left + area.width + radius) return null;
	if (py < area.top - radius || py > area.top + area.height + radius) return null;
	/** @type {Pointer} */
	const pointer = { px, py, radius, area, at: projection(model, area), model };
	for (let s = model.series.length - 1; s >= 0; s--) {
		const tooltip = HIT_TESTS[model.series[s].kind](model.series[s], pointer);
		if (tooltip) return tooltip;
	}
	return null;
}
