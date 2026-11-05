// The graph model (`GenerateGraph`): the axes, which series are drawn and their points, the lines
// and the title, for a renderer to draw and the vectors to check. docs/spec/07-graph.md.
import { formatNumber } from '../format/number.js';
import { DATE_GRAPH_LONG, DATE_GRAPH_SHORT } from '../format/dates.js';
import { instantToJd } from '../time/julian.js';
import { calculateSamples } from './sampling.js';
import { chartTypeOf } from './settings.js';

/** @typedef {import('./sampling.js').GraphInput} GraphInput */

/**
 * one series; x values are JDs, and value series share one x array
 * @typedef {object} GraphSeries
 * @property {'value' | 'perihelion' | 'now'} kind
 * @property {string | null} comet the comet's full name; null for the now line
 * @property {string} color `#RRGGBB`
 * @property {ReadonlyArray<number>} x
 * @property {ReadonlyArray<number>} y
 */

/**
 * @typedef {object} ValueAxis
 * @property {string} title
 * @property {number} min the top of the chart: the axis is reversed
 * @property {number} max
 * @property {number} step between ticks
 * @property {ReadonlyArray<{ value: number, label: string }>} ticks from min, every step, labels as `00.00`
 */

/**
 * @typedef {object} GraphModel
 * @property {string} chartType
 * @property {string | null} title the comet's full name in single mode
 * @property {{ title: string, min: number, max: number, labelFormat: string }} timeAxis at the top,
 *   exactly Start to End
 * @property {ValueAxis} valueAxis
 * @property {ReadonlyArray<GraphSeries>} series in drawing order, the last on top
 */

/**
 * the value axis step for a span: 0.1, 0.2, 0.5, 1 or 2
 * @param {number} span yMax − yMin
 */
function stepFor(span) {
	if (span <= 1) return 0.1;
	if (span <= 2) return 0.2;
	if (span <= 5) return 0.5;
	if (span <= 10) return 1;
	return 2;
}

/**
 * @param {number} min
 * @param {number} max
 * @param {number} step
 */
function ticksOf(min, max, step) {
	// a hair of slack so that rounding in the division keeps the tick on max
	const count = Math.floor((max - min) / step + 1e-9) + 1;
	return Object.freeze(Array.from({ length: count }, (_, k) => {
		const value = min + k * step;
		return Object.freeze({ value, label: formatNumber(value, '00.00') });
	}));
}

/**
 * the axis range: the samples' range with the chart type's margin, floored and ceiled, unless the
 * settings fix an end; several comets default the maximum to 15 or 2
 * @param {GraphInput} input
 * @param {ReadonlyArray<ReadonlyArray<number>>} values
 */
function valueRange({ settings, multiple }, values) {
	const type = chartTypeOf(settings.chartType);
	let min = Infinity;
	let max = -Infinity;
	for (const row of values) {
		for (const v of row) {
			if (v < min) min = v;
			if (v > max) max = v;
		}
	}
	const minValue = Math.floor(min - type.margin);
	const maxValue = Math.ceil(max + type.margin);
	let yMin = settings.minimum.checked ? /** @type {number} */ (settings.minimum.value) : minValue;
	let yMax = settings.maximum.checked ? /** @type {number} */ (settings.maximum.value)
		: multiple && values.length > 1 ? type.multipleMaximum
			: maxValue;
	if (yMin >= yMax) {
		yMin = minValue;
		yMax = maxValue;
	}
	return { yMin, yMax };
}

/**
 * the perihelion lines of a comet: from Tn back to the earliest one after Start less a period,
 * then forward while before End; the first may lie before Start, where it is not visible
 * @param {number} Tn
 * @param {number} P years
 * @param {number} startJd
 * @param {number} endJd
 * @returns {number[]}
 */
export function perihelionTimes(Tn, P, startJd, endJd) {
	// the desktop's decimal holds P to 15 significant digits (spec 02, Tn)
	const period = Number(P.toPrecision(15)) * 365.25;
	// the desktop never ends its loops for a period of 0 (spec 10, D6)
	if (!(period > 0 && Number.isFinite(period))) return Tn < endJd ? [Tn] : [];
	// t = Tn + m·period, each computed afresh: back while t − period > Start, then on while t < End
	let m = 0;
	if (Tn - period > startJd) {
		m = -Math.max(1, Math.ceil((Tn - startJd) / period) - 1);
		while (Tn + (m - 1) * period > startJd) m -= 1;
		while (m < 0 && !(Tn + m * period > startJd)) m += 1;
	}
	const times = [];
	for (let t = Tn + m * period; t < endJd; t = Tn + ++m * period) times.push(t);
	return times;
}

/**
 * the model of a calculation's samples
 * @param {GraphInput} input
 * @param {import('./sampling.js').GraphSamples} samples
 * @returns {GraphModel}
 */
export function graphModel(input, { jds, values }) {
	const { settings, multiple, comets, nearest, nowJd } = input;
	if (comets.length === 0) throw new Error('a graph needs at least one comet');
	const type = chartTypeOf(settings.chartType);
	const { yMin, yMax } = valueRange(input, values);
	const startJd = instantToJd(settings.start);
	const endJd = instantToJd(settings.end);

	/** @type {GraphSeries[]} */
	const series = [];
	const line = (/** @type {'perihelion' | 'now'} */ kind, /** @type {string | null} */ comet, /** @type {string} */ color, /** @type {number} */ t) =>
		series.push(Object.freeze({ kind, comet, color, x: Object.freeze([t, t]), y: Object.freeze([yMin, yMax]) }));
	const perihelia = (/** @type {number} */ index) => {
		for (const t of perihelionTimes(nearest[index], comets[index].P, startJd, endJd)) {
			line('perihelion', comets[index].full, settings.perihelionLine.color, t);
		}
	};

	values.forEach((row, index) => {
		// a series of values all past the axis maximum is left out
		if (!row.some(v => v <= yMax)) return;
		series.push(Object.freeze({ kind: 'value', comet: comets[index].full, color: settings.valueColor, x: jds, y: row }));
		if (settings.perihelionLine.checked && multiple) perihelia(index);
	});
	// single mode: the selected comet's lines, drawn or not
	if (settings.perihelionLine.checked && !multiple) perihelia(0);
	if (settings.nowLine.checked && startJd < nowJd && endJd > nowJd) line('now', null, settings.nowLine.color, nowJd);

	const step = stepFor(yMax - yMin);
	return Object.freeze({
		chartType: type.key,
		title: multiple ? null : comets[0].full,
		timeAxis: Object.freeze({ title: 'Date', min: startJd, max: endJd, labelFormat: endJd - startJd <= 3 ? DATE_GRAPH_LONG : DATE_GRAPH_SHORT }),
		valueAxis: Object.freeze({ title: type.axisTitle, min: yMin, max: yMax, step, ticks: ticksOf(yMin, yMax, step) }),
		series: Object.freeze(series)
	});
}

/**
 * the graph as a job: the samples, then the model; yields `{ done, total }` in comets
 * @param {GraphInput} input
 * @returns {Generator<{ done: number, total: number }, GraphModel, void>}
 */
export function* calculateGraph(input) {
	const samples = yield* calculateSamples(input, chartTypeOf(input.settings.chartType).value);
	return graphModel(input, samples);
}
