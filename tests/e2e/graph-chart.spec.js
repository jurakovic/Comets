// The graph chart in a real browser, mounted alone over the Help view: its canvas in the screen's
// own pixels, the theme's colours followed live, and the tooltips of what lies under the pointer.
import { expect, test } from './fixtures.js';
import { drain } from '../helpers/generators.js';
import { GRAPH, graphCase } from '../helpers/graph.js';
import { DATE_FULL, formatDate } from '../../app/js/core/format/dates.js';
import { calculateGraph } from '../../app/js/core/graph/model.js';
import { instantToParts } from '../../app/js/core/time/calendar.js';
import { jdToInstant } from '../../app/js/core/time/julian.js';

/**
 * the model of a case of graph.json at the vector clock
 * @param {string} id
 * @returns {import('../../app/js/core/graph/model.js').GraphModel}
 */
function modelOf(id) {
	const vector = GRAPH.cases.find((/** @type {any} */ c) => c.id === id);
	return drain(calculateGraph(graphCase(vector.settings).input)).result;
}

/** 12P's Sun distance over 2010–2030: its value series, a perihelion line and the now line */
const SUN_DISTANCE = modelOf('03-sundistance-single-12P');

/**
 * mounts a chart of the model in a box over the top four fifths of the page, and waits until it
 * is drawn
 * @param {import('@playwright/test').Page} page
 * @param {object} model
 */
async function mountChart(page, model) {
	await page.goto('./#/help');
	await page.locator('[data-view="help"]').waitFor();
	await page.evaluate(async model => {
		const { createGraphChart } = await import(new URL('js/ui/views/graph/graph-chart.js', location.href).href);
		const box = document.createElement('div');
		box.id = 'probe';
		box.style.cssText = 'position: fixed; left: 0; top: 0; width: 100%; height: 80%; z-index: 10';
		const chart = createGraphChart(model);
		box.append(chart.element);
		document.body.append(box);
		Object.assign(window, { chart });
	}, model);
	const canvas = page.locator('#probe canvas');
	// drawn: the background is opaque
	await expect.poll(() => canvas.evaluate(c => /** @type {HTMLCanvasElement} */ (c).getContext('2d')?.getImageData(0, 0, 1, 1).data[3])).toBe(255);
	return canvas;
}

/**
 * the colour of the canvas' pixel at a point, in CSS pixels
 * @param {import('@playwright/test').Locator} canvas
 * @param {number} x
 * @param {number} y
 */
const pixel = (canvas, x, y) => canvas.evaluate((element, [x, y]) => {
	const c = /** @type {HTMLCanvasElement} */ (element);
	const ratio = c.width / c.clientWidth;
	return [...(c.getContext('2d')?.getImageData(Math.floor(x * ratio), Math.floor(y * ratio), 1, 1).data.slice(0, 3) ?? [])];
}, [x, y]);

/**
 * whether a pixel of the colour is on the canvas
 * @param {import('@playwright/test').Locator} canvas
 * @param {number[]} rgb
 */
const shows = (canvas, rgb) => canvas.evaluate((element, rgb) => {
	const c = /** @type {HTMLCanvasElement} */ (element);
	const { data } = /** @type {CanvasRenderingContext2D} */ (c.getContext('2d')).getImageData(0, 0, c.width, c.height);
	for (let i = 0; i < data.length; i += 4) {
		if (data[i] === rgb[0] && data[i + 1] === rgb[1] && data[i + 2] === rgb[2]) return true;
	}
	return false;
}, rgb);

/**
 * the first point, in CSS pixels, along a row (y) or a column (x) of the canvas whose colour is
 * the series': a line's exactly, the curve's red where it is antialiased
 * @param {import('@playwright/test').Locator} canvas
 * @param {'perihelion' | 'now' | 'value'} kind
 * @param {{ x?: number, y?: number }} line
 * @returns {Promise<{ x: number, y: number } | null>}
 */
const find = (canvas, kind, line) => canvas.evaluate((element, { kind, line }) => {
	const c = /** @type {HTMLCanvasElement} */ (element);
	const ratio = c.width / c.clientWidth;
	const context = /** @type {CanvasRenderingContext2D} */ (c.getContext('2d'));
	/** @type {Record<string, (d: Uint8ClampedArray, i: number) => boolean>} */
	const colours = {
		perihelion: (d, i) => d[i] === 65 && d[i + 1] === 105 && d[i + 2] === 225,
		now: (d, i) => d[i] === 50 && d[i + 1] === 205 && d[i + 2] === 50,
		value: (d, i) => d[i] - Math.max(d[i + 1], d[i + 2]) > 100
	};
	const along = line.y !== undefined;
	const at = Math.floor(/** @type {number} */ (along ? line.y : line.x) * ratio);
	const { data } = along ? context.getImageData(0, at, c.width, 1) : context.getImageData(at, 0, 1, c.height);
	for (let k = 0; k < data.length / 4; k++) {
		if (!colours[kind](data, k * 4)) continue;
		const point = (k + 0.5) / ratio;
		return along ? { x: point, y: /** @type {number} */ (line.y) } : { x: /** @type {number} */ (line.x), y: point };
	}
	return null;
}, { kind, line });

/** @param {number} jd */
const timeText = jd => formatDate(instantToParts(jdToInstant(jd)), DATE_FULL);

test('the canvas has a pixel per device pixel, also after a resize', async ({ page }) => {
	const canvas = await mountChart(page, SUN_DISTANCE);
	const size = () => canvas.evaluate(element => {
		const c = /** @type {HTMLCanvasElement} */ (element);
		const { width, height } = c.getBoundingClientRect();
		return { pixels: [c.width, c.height], css: [width, height], ratio: devicePixelRatio };
	});
	const before = await size();
	expect(Math.abs(before.pixels[0] - before.css[0] * before.ratio)).toBeLessThanOrEqual(1);
	expect(Math.abs(before.pixels[1] - before.css[1] * before.ratio)).toBeLessThanOrEqual(1);
	await page.evaluate(() => { /** @type {HTMLElement} */ (document.getElementById('probe')).style.width = '300px'; });
	await expect.poll(async () => Math.abs((await size()).pixels[0] - 300 * before.ratio)).toBeLessThanOrEqual(1);
});

test('the value axis and its ticks are whole device pixels, across and down', async ({ page }) => {
	// the box's height, four fifths of the phone's, is not a whole number of device pixels
	const canvas = await mountChart(page, SUN_DISTANCE);
	const { ratio, axis, ticks } = await canvas.evaluate(element => {
		const c = /** @type {HTMLCanvasElement} */ (element);
		const { data } = /** @type {CanvasRenderingContext2D} */ (c.getContext('2d')).getImageData(0, 0, c.width, c.height);
		const is = (/** @type {number} */ x, /** @type {number} */ y, /** @type {number} */ value) =>
			[0, 1, 2].every(k => data[(y * c.width + x) * 4 + k] === value);
		// the value axis: the black run left of the plot, from the right in the middle row
		const row = Math.floor(c.height / 2);
		let right = Math.floor(c.width * 0.9);
		while (right > 0 && !is(right, row, 0)) right--;
		let left = right;
		while (left > 0 && is(left - 1, row, 0)) left--;
		// from its top to its bottom, the plot's, below the time labels
		let top = row;
		while (top > 0 && is(left, top - 1, 0)) top--;
		let bottom = row;
		while (bottom < c.height - 1 && is(left, bottom + 1, 0)) bottom++;
		// its ticks, down a column between the axis and the labels: a run of black rows each
		/** @type {Array<{ rows: number, black: boolean }>} */
		const ticks = [];
		const column = left - Math.round(2 * devicePixelRatio);
		const end = Math.min(c.height, bottom + 4);
		for (let y = Math.max(0, top - 4); y < end; y++) {
			if (is(column, y, 255)) continue;
			const tick = { rows: 0, black: true };
			for (; y < end && !is(column, y, 255); y++) {
				tick.rows++;
				tick.black &&= is(column, y, 0);
			}
			ticks.push(tick);
		}
		return { ratio: devicePixelRatio, axis: right - left + 1, ticks };
	});
	const width = Math.max(1, Math.round(ratio));
	expect(axis).toBe(width);
	// 0 to 20 AU every 2
	expect(ticks).toEqual(Array.from({ length: 11 }, () => ({ rows: width, black: true })));
});

test('the background and the text follow the theme as it changes', async ({ page }) => {
	const canvas = await mountChart(page, SUN_DISTANCE);
	const theme = (/** @type {string | null} */ value) => page.evaluate(value => {
		if (value === null) delete document.documentElement.dataset.theme;
		else document.documentElement.dataset.theme = value;
	}, value);
	expect(await pixel(canvas, 2, 2)).toEqual([255, 255, 255]);
	expect(await shows(canvas, [0, 0, 0])).toBe(true);
	await theme('dark');
	await expect.poll(() => pixel(canvas, 2, 2)).toEqual([55, 55, 55]);
	expect(await shows(canvas, [255, 255, 255])).toBe(true);
	expect(await shows(canvas, [0, 0, 0])).toBe(false);
	// without a choice the OS's theme shows
	await theme(null);
	await expect.poll(() => pixel(canvas, 2, 2)).toEqual([255, 255, 255]);
	await page.emulateMedia({ colorScheme: 'dark' });
	await expect.poll(() => pixel(canvas, 2, 2)).toEqual([55, 55, 55]);
	await theme('light');
	await expect.poll(() => pixel(canvas, 2, 2)).toEqual([255, 255, 255]);
});

test('a sample, a perihelion line and the now line have their tooltips; it goes with the pointer', async ({ page }) => {
	const canvas = await mountChart(page, SUN_DISTANCE);
	const box = /** @type {{ x: number, y: number, width: number, height: number }} */ (await canvas.boundingBox());
	const tooltip = page.locator('#probe').getByRole('tooltip');
	const hover = async (/** @type {{ x: number, y: number } | null} */ point) => {
		expect(point).not.toBeNull();
		await page.mouse.move(box.x + /** @type {{ x: number }} */ (point).x, box.y + /** @type {{ y: number }} */ (point).y);
	};
	await expect(tooltip).toBeHidden();

	const perihelion = /** @type {import('../../app/js/core/graph/model.js').GraphSeries} */ (SUN_DISTANCE.series.find(s => s.kind === 'perihelion'));
	await hover(await find(canvas, 'perihelion', { y: box.height / 2 }));
	await expect(tooltip).toBeVisible();
	await expect(tooltip.locator('div')).toHaveText(['12P/Pons-Brooks', `Perihelion time: ${timeText(perihelion.x[0])}`]);

	await hover(await find(canvas, 'now', { y: box.height / 2 }));
	await expect(tooltip.locator('div')).toHaveText(['Today', 'Time: 18.10.2026. 12:00:00']);

	await hover(await find(canvas, 'value', { x: box.width * 0.45 }));
	await expect(tooltip.locator('div')).toHaveText(['12P/Pons-Brooks', /^Time: \d\d\.\d\d\.20\d\d\. 00:00:00$/, /^Distance: \d+\.\d{4} AU$/]);
	// within the chart, above the pointer or below it
	const shown = /** @type {{ x: number, y: number, width: number, height: number }} */ (await tooltip.boundingBox());
	expect(shown.x).toBeGreaterThanOrEqual(box.x);
	expect(shown.x + shown.width).toBeLessThanOrEqual(box.x + box.width + 1);

	// over nothing, then off the chart
	await hover({ x: 3, y: 3 });
	await expect(tooltip).toBeHidden();
	await hover(await find(canvas, 'now', { y: box.height / 2 }));
	await expect(tooltip).toBeVisible();
	await page.mouse.move(box.x + box.width / 2, box.y + box.height + 20);
	await expect(tooltip).toBeHidden();
});

test('a touch shows the tooltip, which stays as the finger lifts', async ({ page, hasTouch }) => {
	test.skip(!hasTouch, 'a touch screen');
	const canvas = await mountChart(page, SUN_DISTANCE);
	const box = /** @type {{ x: number, y: number, width: number, height: number }} */ (await canvas.boundingBox());
	const now = /** @type {{ x: number, y: number }} */ (await find(canvas, 'now', { y: box.height / 2 }));
	const tooltip = page.locator('#probe').getByRole('tooltip');
	await page.touchscreen.tap(box.x + now.x, box.y + now.y);
	await expect(tooltip).toBeVisible();
	await expect(tooltip.locator('div').first()).toHaveText('Today');
	await page.touchscreen.tap(box.x + 3, box.y + 3);
	await expect(tooltip).toBeHidden();
});

test('destroyed, the chart follows neither the theme nor its size', async ({ page }) => {
	const canvas = await mountChart(page, SUN_DISTANCE);
	const width = await canvas.evaluate(c => /** @type {HTMLCanvasElement} */ (c).width);
	await page.evaluate(() => {
		/** @type {any} */ (window).chart.destroy();
		document.documentElement.dataset.theme = 'dark';
		/** @type {HTMLElement} */ (document.getElementById('probe')).style.width = '300px';
	});
	// two frames: time for a resize observation and a theme change to land
	await page.evaluate(() => new Promise(resolve => requestAnimationFrame(() => requestAnimationFrame(resolve))));
	expect(await pixel(canvas, 2, 2)).toEqual([255, 255, 255]);
	expect(await canvas.evaluate(c => /** @type {HTMLCanvasElement} */ (c).width)).toBe(width);
});
