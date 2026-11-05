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
	const at = projection(model, area);
	const { timeAxis, valueAxis } = model;
	const inside = (/** @type {number} */ jd, /** @type {number} */ value) =>
		jd >= timeAxis.min && jd <= timeAxis.max && value >= valueAxis.min && value <= valueAxis.max;
	const from = at.jd(px - radius);
	const to = at.jd(px + radius);

	for (let s = model.series.length - 1; s >= 0; s--) {
		const series = model.series[s];
		if (series.kind !== 'value') {
			const t = series.x[0];
			if (Math.abs(at.x(t) - px) > radius || t < timeAxis.min || t > timeAxis.max) continue;
			if (py < area.top || py > area.top + area.height) continue;
			return series.kind === 'now'
				? Object.freeze({ title: 'Today', lines: Object.freeze([`Time: ${timeText(t)}`]) })
				: Object.freeze({ title: series.comet ?? '', lines: Object.freeze([`Perihelion time: ${timeText(t)}`]) });
		}
		let best = -1;
		let nearest = Infinity;
		for (let k = lowerBound(series.x, from); k < series.x.length && series.x[k] <= to; k++) {
			if (!inside(series.x[k], series.y[k])) continue;
			const dx = at.x(series.x[k]) - px;
			const dy = at.y(series.y[k]) - py;
			const distance = dx * dx + dy * dy;
			// the first of equally near samples
			if (distance <= radius * radius && distance < nearest) {
				best = k;
				nearest = distance;
			}
		}
		if (best >= 0) {
			const lines = [`Time: ${timeText(series.x[best])}`, chartTypeOf(model.chartType).tooltip(series.y[best])];
			return Object.freeze({ title: series.comet ?? '', lines: Object.freeze(lines) });
		}
	}
	return null;
}
