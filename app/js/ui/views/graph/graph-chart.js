// The graph chart: a graph model drawn on a Canvas 2D in the screen's own pixels, drawn again when
// its size, the device pixel ratio or the theme changes, with a tooltip for what the pointer is
// over. INTERNALS.md, How the UI is written.
import { traceCurve } from '../../../core/graph/curve.js';
import { hoverAt, projection } from '../../../core/graph/hover.js';
import { graphLayout } from '../../../core/graph/layout.js';
import { el } from '../../dom.js';
import { watchTheme } from '../../shell/theme.js';

/** @typedef {import('../../../core/graph/model.js').GraphModel} GraphModel */
/** @typedef {import('../../../core/graph/model.js').GraphSeries} GraphSeries */
/** @typedef {import('../../../core/graph/layout.js').GraphLayout} GraphLayout */

/**
 * @typedef {object} GraphChart
 * @property {HTMLElement} element fills the box its owner gives it
 * @property {() => void} destroy stops following the size, the screen and the theme
 */

/**
 * @typedef {object} Theme the chart's colours and font, from the theme's custom properties
 * @property {string} background
 * @property {string} text
 * @property {string} font
 */

/** the fonts in CSS pixels, and their line height */
const LABEL_SIZE = 12;
const TITLE_SIZE = 15;
const LINE_HEIGHT = 1.25;
/** the tooltip's distance from the pointer */
const OFFSET = 12;

/** @param {Element} element */
function themeOf(element) {
	const style = getComputedStyle(element);
	const value = (/** @type {string} */ name) => style.getPropertyValue(name).trim();
	return Object.freeze({ background: value('--color-graph-bg'), text: value('--color-graph-text'), font: value('--font-ui') || 'sans-serif' });
}

/**
 * lines a whole number of device pixels wide on whole device pixels, so that they stay sharp
 * @param {number} ratio device pixels per CSS pixel
 */
function pixelGrid(ratio) {
	const device = Math.max(1, Math.round(ratio));
	const offset = device % 2 === 1 ? 0.5 : 0;
	return { width: device / ratio, at: (/** @type {number} */ v) => (Math.round(v * ratio - offset) + offset) / ratio };
}

/**
 * the titles, the labels, the axes along the top and the left of the plot, and their ticks
 * @param {CanvasRenderingContext2D} context
 * @param {GraphLayout} layout
 * @param {Theme} theme
 * @param {ReturnType<typeof pixelGrid>} grid
 */
function paintFrame(context, layout, theme, grid) {
	const { area, title, timeTitle, valueTitle, tickLength } = layout;
	context.fillStyle = theme.text;
	context.textBaseline = 'top';
	if (title) {
		context.font = `${TITLE_SIZE}px ${theme.font}`;
		context.textAlign = 'center';
		context.fillText(title.text, title.x, title.y);
	}
	context.font = `${LABEL_SIZE}px ${theme.font}`;
	context.textAlign = 'right';
	context.fillText(timeTitle.text, timeTitle.x, timeTitle.y);
	context.textAlign = 'center';
	for (const tick of layout.timeTicks) context.fillText(tick.label, tick.labelX, layout.timeLabelY);
	context.textAlign = 'right';
	context.textBaseline = 'middle';
	for (const tick of layout.valueTicks) {
		if (tick.label !== null) context.fillText(tick.label, layout.valueLabelX, tick.y);
	}
	// read upwards, ending at the top of the axis
	context.save();
	context.translate(valueTitle.x, valueTitle.y);
	context.rotate(-Math.PI / 2);
	context.fillText(valueTitle.text, 0, 0);
	context.restore();

	const left = grid.at(area.left);
	const top = grid.at(area.top);
	context.beginPath();
	context.moveTo(grid.at(area.left + area.width), top);
	context.lineTo(left, top);
	context.lineTo(left, grid.at(area.top + area.height));
	for (const tick of layout.timeTicks) {
		context.moveTo(grid.at(tick.x), top - tickLength);
		context.lineTo(grid.at(tick.x), top);
	}
	for (const tick of layout.valueTicks) {
		context.moveTo(left - tickLength, grid.at(tick.y));
		context.lineTo(left, grid.at(tick.y));
	}
	context.strokeStyle = theme.text;
	context.lineWidth = grid.width;
	context.stroke();
}

/**
 * what a series traces on the plot, by its kind
 * @typedef {object} Painting
 * @property {CanvasRenderingContext2D} context
 * @property {ReturnType<typeof projection>} at
 * @property {ReturnType<typeof pixelGrid>} grid
 * @property {Map<ReadonlyArray<number>, Float64Array>} xs the value series' x on the chart, which
 *   they share
 */

/**
 * @param {GraphSeries} series
 * @param {Painting} painting
 */
function verticalLine({ x, y }, { context, at, grid }) {
	const left = grid.at(at.x(x[0]));
	context.moveTo(left, at.y(y[0]));
	context.lineTo(left, at.y(y[1]));
	context.lineWidth = grid.width;
}

/** @type {Readonly<Record<GraphSeries['kind'], (series: GraphSeries, painting: Painting) => void>>} */
const PAINTERS = Object.freeze({
	value(series, { context, at, xs }) {
		let x = xs.get(series.x);
		if (!x) {
			x = Float64Array.from(series.x, at.x);
			xs.set(series.x, x);
		}
		traceCurve(context, x, Float64Array.from(series.y, at.y));
		context.lineWidth = 1;
	},
	perihelion: verticalLine,
	now: verticalLine
});

/**
 * the series in their order, the last on top, within the plot
 * @param {CanvasRenderingContext2D} context
 * @param {GraphModel} model
 * @param {GraphLayout} layout
 * @param {ReturnType<typeof pixelGrid>} grid
 */
function paintSeries(context, model, { area }, grid) {
	const painting = { context, at: projection(model, area), grid, xs: new Map() };
	context.save();
	// a line on the plot's edge is drawn whole
	const half = grid.width / 2;
	context.beginPath();
	context.rect(area.left - half, area.top - half, area.width + 2 * half, area.height + 2 * half);
	context.clip();
	context.lineJoin = 'round';
	for (const series of model.series) {
		context.beginPath();
		PAINTERS[series.kind](series, painting);
		context.strokeStyle = series.color;
		context.stroke();
	}
	context.restore();
}

/**
 * a chart of the model, drawn once its element is laid out
 * @param {GraphModel} model
 * @returns {GraphChart}
 */
export function createGraphChart(model) {
	const canvas = el('canvas', { class: 'graph-chart__canvas', role: 'img', 'aria-label': [model.valueAxis.title, model.title ?? ''].filter(Boolean).join(', ') });
	const tooltip = el('div', { class: 'graph-chart__tooltip', role: 'tooltip', hidden: true });
	const element = el('div', { class: 'graph-chart' }, canvas, tooltip);
	/** the size drawn, in CSS pixels */
	let box = { width: 0, height: 0 };
	/** @type {GraphLayout | null} */
	let layout = null;
	let drawnTheme = '';
	/** what the tooltip shows, kept while the pointer stays over the same */
	let shown = '';

	function draw() {
		hideTooltip();
		const context = canvas.getContext('2d');
		// a pixel of its own per device pixel
		const width = Math.round(box.width * devicePixelRatio);
		const height = Math.round(box.height * devicePixelRatio);
		// hidden, or not laid out yet
		if (!context || width === 0 || height === 0) {
			layout = null;
			return;
		}
		const theme = themeOf(element);
		drawnTheme = JSON.stringify(theme);
		canvas.width = width;
		canvas.height = height;
		const ratio = canvas.width / box.width;
		context.setTransform(ratio, 0, 0, canvas.height / box.height, 0, 0);
		context.fillStyle = theme.background;
		context.fillRect(0, 0, box.width, box.height);
		context.font = `${LABEL_SIZE}px ${theme.font}`;
		layout = graphLayout(model, box, {
			width: text => context.measureText(text).width,
			lineHeight: Math.ceil(LABEL_SIZE * LINE_HEIGHT),
			titleHeight: Math.ceil(TITLE_SIZE * LINE_HEIGHT)
		});
		const grid = pixelGrid(ratio);
		paintFrame(context, layout, theme, grid);
		paintSeries(context, model, layout, grid);
	}

	function hideTooltip() {
		tooltip.hidden = true;
		shown = '';
	}

	/** @param {PointerEvent} event */
	function hover({ offsetX, offsetY }) {
		const found = layout && hoverAt(model, layout.area, offsetX, offsetY);
		if (!found) {
			hideTooltip();
			return;
		}
		const text = [found.title, ...found.lines].join('\n');
		if (text !== shown) {
			shown = text;
			tooltip.replaceChildren(el('div', { class: 'graph-chart__tooltip-title' }, found.title), ...found.lines.map(line => el('div', {}, line)));
		}
		tooltip.hidden = false;
		// above and right of the pointer, or where there is room
		const { offsetWidth: width, offsetHeight: height } = tooltip;
		const x = offsetX + OFFSET + width <= box.width ? offsetX + OFFSET : offsetX - OFFSET - width;
		const y = offsetY - OFFSET - height >= 0 ? offsetY - OFFSET - height : offsetY + OFFSET;
		tooltip.style.left = `${Math.max(0, x)}px`;
		tooltip.style.top = `${Math.max(0, y)}px`;
	}

	canvas.addEventListener('pointermove', hover);
	canvas.addEventListener('pointerdown', hover);
	// a touch leaves as it lifts: its tooltip stays until the next touch
	canvas.addEventListener('pointerleave', event => {
		if (event.pointerType !== 'touch') hideTooltip();
	});

	const resizes = new ResizeObserver(([entry]) => {
		box = { width: entry.contentRect.width, height: entry.contentRect.height };
		draw();
	});
	resizes.observe(canvas);
	/** @type {() => void} */
	let stopResolution = () => {};
	// the device pixel ratio changes with the zoom, and from one screen to another
	const watchResolution = () => {
		const media = matchMedia(`(resolution: ${devicePixelRatio}dppx)`);
		const changed = () => {
			stopResolution();
			watchResolution();
			draw();
		};
		media.addEventListener('change', changed);
		stopResolution = () => media.removeEventListener('change', changed);
	};
	watchResolution();
	const stopTheme = watchTheme(() => {
		if (JSON.stringify(themeOf(element)) !== drawnTheme) draw();
	});

	return {
		element,
		destroy() {
			resizes.disconnect();
			stopResolution();
			stopTheme();
		}
	};
}
