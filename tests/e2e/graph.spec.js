// The Graph view over the sample collection at the vectors' clock: the settings form and its
// errors, the long-calculation question, Cancel, each case of graph.json drawn as its model,
// result tabs with Edit settings, Save as PNG or JPEG and Close, a hidden chart drawn again when
// shown, and the comet picker following the database.
import { readFileSync } from 'node:fs';
import { drain } from '../helpers/generators.js';
import { GRAPH, graphCase } from '../helpers/graph.js';
import { readVectorText, VECTOR_NOW } from '../helpers/vectors.js';
import { calculateGraph } from '../../app/js/core/graph/model.js';
import { expect, holdDatabase, landDatabase, seed, test } from './fixtures.js';

const SAMPLE = readVectorText('import/sample.txt');
const FULL = readVectorText('import/full/Soft00Cmt.txt');
const MERGE_NEW = readVectorText('import/merge-new.txt');
const MPC_URL = 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt';
const NOW = new Date(VECTOR_NOW);
/** the sample's brightest comet at the vectors' clock, which a new form chooses */
const BRIGHTEST = 'C/2026 T99 (Parabola Test)';
/** the chart types' labels, by the vectors' keys */
const CHART_TYPE_LABELS = Object.freeze({ Magnitude: 'Magnitude', SunDistance: 'Sun distance', EarthDistance: 'Earth distance' });

/** @param {import('@playwright/test').Page} page */
const viewOf = page => page.locator('[data-view="graph"]');
/** @param {import('@playwright/test').Locator} view */
const comboOf = view => view.getByRole('combobox', { name: 'Comet' });
/** @param {import('@playwright/test').Locator} combo the text of the entry shown */
const shownOf = combo => combo.evaluate(select => /** @type {HTMLSelectElement} */ (select).selectedOptions[0]?.text ?? null);
/** @param {import('@playwright/test').Locator} view */
const tabsOf = view => view.getByRole('tablist', { name: 'Results' }).getByRole('tab');
/** @param {import('@playwright/test').Locator} view the canvas of the selected result */
const chartOf = view => view.getByRole('tabpanel').locator('canvas');

/**
 * a database stored, at the vectors' clock, and the Graph view open over it
 * @param {import('@playwright/test').Page} page
 * @param {{ settings?: object, database?: string, count?: number }} [options] the database's comets
 */
async function open(page, { settings, database = SAMPLE, count = 34 } = {}) {
	await page.clock.setFixedTime(NOW);
	await seed(page, { database, settings });
	await page.goto('./#/graph');
	const view = viewOf(page);
	await expect(comboOf(view).getByRole('option')).toHaveCount(count + 1);
	return view;
}

/**
 * a date picker's date set through its editor's JD
 * @param {import('@playwright/test').Page} page
 * @param {string} name Start or End
 * @param {string} iso a UTC instant
 */
async function setDate(page, name, iso) {
	await viewOf(page).getByRole('group', { name, exact: true }).getByRole('button').first().click();
	const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
	await editor.getByRole('textbox', { name: 'JD' }).fill(String(Date.parse(iso) / 86_400_000 + 2440587.5));
	await editor.getByRole('button', { name: 'OK' }).click();
	await expect(editor).toHaveCount(0);
}

/**
 * a preset of a date picker's menu
 * @param {import('@playwright/test').Page} page
 * @param {string} name Start or End
 * @param {string} preset
 */
async function pickPreset(page, name, preset) {
	await viewOf(page).getByRole('group', { name, exact: true }).getByRole('button', { name: 'Date presets' }).click();
	await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: preset }).click();
}

/**
 * an end of the value range typed, which checks its box while it holds text
 * @param {import('@playwright/test').Locator} view
 * @param {'Minimum' | 'Maximum'} name
 * @param {number | null} value
 */
async function setLimit(view, name, value) {
	await view.getByRole('textbox', { name }).fill(value === null ? '' : String(value));
	await expect(view.getByRole('checkbox', { name })).toBeChecked({ checked: value !== null });
}

/**
 * until a canvas is drawn: its background is opaque
 * @param {import('@playwright/test').Locator} canvas
 */
const drawn = canvas => expect.poll(() => canvas.evaluate(c => /** @type {HTMLCanvasElement} */ (c).getContext('2d')?.getImageData(0, 0, 1, 1).data[3])).toBe(255);

/**
 * the colour of a canvas' corner pixel
 * @param {import('@playwright/test').Locator} canvas
 */
const corner = canvas => canvas.evaluate(c => [...(/** @type {HTMLCanvasElement} */ (c).getContext('2d')?.getImageData(1, 1, 1, 1).data.slice(0, 3) ?? [])]);

/**
 * a download: its name and its bytes
 * @param {import('@playwright/test').Page} page
 * @param {() => Promise<unknown>} save
 */
async function saved(page, save) {
	const [download] = await Promise.all([page.waitForEvent('download'), save()]);
	return { name: download.suggestedFilename(), bytes: readFileSync(await download.path()) };
}

/**
 * a PNG's width and height, from its header
 * @param {Buffer} bytes
 */
function pngSize(bytes) {
	expect([...bytes.subarray(0, 8)]).toEqual([137, 80, 78, 71, 13, 10, 26, 10]);
	expect(bytes.subarray(12, 16).toString('latin1')).toBe('IHDR');
	return [bytes.readUInt32BE(16), bytes.readUInt32BE(20)];
}

test('a new form: a copy of the app-wide selection, its brightest chosen, and the default settings', async ({ page }) => {
	const view = await open(page);
	expect(await shownOf(comboOf(view))).toBe(BRIGHTEST);
	await expect(view.getByRole('group', { name: 'Start', exact: true }).getByRole('button').first()).toHaveText('01.09.2026. 00:00:00');
	await expect(view.getByRole('group', { name: 'End', exact: true }).getByRole('button').first()).toHaveText('01.03.2027. 00:00:00');
	await expect(view.getByRole('radio', { name: 'Magnitude' })).toBeChecked();
	await expect(view.getByRole('radio', { name: 'Sun distance' })).not.toBeChecked();
	await expect(view.getByLabel('Value colour')).toHaveValue('#ff0000');
	await expect(view.getByRole('checkbox', { name: 'Perihelion line' })).toBeChecked();
	await expect(view.getByLabel('Perihelion line colour')).toHaveValue('#4169e1');
	await expect(view.getByRole('checkbox', { name: 'Now line' })).toBeChecked();
	await expect(view.getByLabel('Now line colour')).toHaveValue('#32cd32');
	await expect(view.getByRole('checkbox', { name: 'Minimum' })).not.toBeChecked();
	await expect(view.getByRole('textbox', { name: 'Minimum' })).toHaveValue('');
	await expect(view.getByRole('checkbox', { name: 'Maximum' })).toBeChecked();
	await expect(view.getByRole('textbox', { name: 'Maximum' })).toHaveValue('12');
	await expect(tabsOf(view)).toHaveText(['Settings']);
	// typing checks a box while its field holds text
	await setLimit(view, 'Minimum', -1.5);
	await setLimit(view, 'Maximum', null);
	// the Default preset gives the default dates back
	await pickPreset(page, 'End', 'Add 1 year');
	await pickPreset(page, 'End', 'Default');
	await expect(view.getByRole('group', { name: 'End', exact: true }).getByRole('button').first()).toHaveText('01.03.2027. 00:00:00');
});

for (const { id, settings: vector } of GRAPH.cases) {
	test(`${id}: the chart draws the case's model`, async ({ page }) => {
		const view = await open(page);
		await comboOf(view).selectOption({ label: vector.multiple ? '[ALL COMETS (34)]' : vector.comets });
		await setDate(page, 'Start', vector.startUtc);
		await setDate(page, 'End', vector.stopUtc);
		await view.getByRole('radio', { name: CHART_TYPE_LABELS[/** @type {keyof typeof CHART_TYPE_LABELS} */ (vector.chartType)] }).check();
		await view.getByRole('checkbox', { name: 'Perihelion line' }).setChecked(vector.perihelionLine);
		await view.getByRole('checkbox', { name: 'Now line' }).setChecked(vector.nowLine);
		await setLimit(view, 'Minimum', vector.minValue);
		await setLimit(view, 'Maximum', vector.maxValue);
		await view.getByRole('button', { name: 'Calculate' }).click();
		await expect(tabsOf(view).nth(1)).toHaveText(vector.title);
		await expect(tabsOf(view).nth(1)).toHaveAttribute('aria-selected', 'true');

		// the vectors' model, which the vector tests check, drawn alone at the same size
		const model = drain(calculateGraph(graphCase(vector).input)).result;
		const name = model.title === null ? model.valueAxis.title : `${model.valueAxis.title}, ${model.title}`;
		const canvas = view.getByRole('img', { name, exact: true });
		await drawn(canvas);
		const same = await canvas.evaluate(async (shown, model) => {
			const { createGraphChart } = await import(new URL('js/ui/views/graph/graph-chart.js', location.href).href);
			const { width, height } = shown.getBoundingClientRect();
			const box = document.createElement('div');
			box.style.cssText = `position: fixed; left: 0; top: 0; width: ${width}px; height: ${height}px`;
			const chart = createGraphChart(model);
			box.append(chart.element);
			document.body.append(box);
			const probe = /** @type {HTMLCanvasElement} */ (box.querySelector('canvas'));
			const view = /** @type {HTMLCanvasElement} */ (shown);
			const pixels = (/** @type {HTMLCanvasElement} */ c) => /** @type {CanvasRenderingContext2D} */ (c.getContext('2d')).getImageData(0, 0, c.width, c.height).data;
			// drawn once its size has been observed
			while (probe.width !== view.width || probe.height !== view.height) await new Promise(resolve => requestAnimationFrame(resolve));
			const [a, b] = [pixels(view), pixels(probe)];
			chart.destroy();
			box.remove();
			return a.length > 0 && a.every((value, k) => value === b[k]);
		}, model);
		expect(same).toBe(true);
	});
}

test('errors show by their fields, the first focused, and follow the fields once shown; nothing is calculated', async ({ page }) => {
	const view = await open(page);
	await setDate(page, 'End', '2026-08-01T00:00:00Z');
	await view.getByRole('checkbox', { name: 'Minimum' }).check();
	await view.getByRole('button', { name: 'Calculate' }).click();
	const minimum = view.getByRole('textbox', { name: 'Minimum' });
	const maximum = view.getByRole('textbox', { name: 'Maximum' });
	const start = view.getByRole('group', { name: 'Start', exact: true }).getByRole('button').first();
	await expect(view.getByRole('alert')).toHaveText(['End date must be greater than start date', 'Enter Minimum value']);
	await expect(minimum).toBeFocused();
	await expect(minimum).toHaveAttribute('aria-invalid', 'true');
	await expect(minimum).toHaveAccessibleDescription('Enter Minimum value');
	await expect(start).toHaveAccessibleDescription('End date must be greater than start date');
	// Minimum not below Maximum: the error is Maximum's
	await minimum.pressSequentially('12');
	await expect(view.getByRole('alert')).toHaveText(['End date must be greater than start date', 'Minimum value must be lower than Maximum value']);
	await expect(minimum).not.toHaveAttribute('aria-invalid');
	await expect(maximum).toHaveAccessibleDescription('Minimum value must be lower than Maximum value');
	await view.getByRole('checkbox', { name: 'Maximum' }).check();
	await maximum.fill('');
	await view.getByRole('checkbox', { name: 'Maximum' }).check();
	await expect(view.getByRole('alert')).toHaveText(['End date must be greater than start date', 'Enter Maximum value']);
	await maximum.pressSequentially('20');
	await pickPreset(page, 'End', 'Default');
	await expect(view.getByRole('alert')).toHaveCount(0);
	await expect(start).not.toHaveAttribute('aria-invalid');
	await expect(tabsOf(view)).toHaveCount(1);
});

test.describe('over the full MPC file', () => {
	// every comet over three centuries: the long-calculation question, and time to cancel
	test.slow();

	test('a long calculation asks first; No calculates nothing, Yes runs it with its progress, and Cancel discards it', async ({ page }) => {
		const view = await open(page, { database: FULL, count: 963 });
		const calculate = view.getByRole('button', { name: 'Calculate' });
		const job = page.locator('.shell-status__job');
		await comboOf(view).selectOption({ label: '[ALL COMETS (963)]' });
		await setDate(page, 'Start', '1850-01-01T00:00:00Z');
		await setDate(page, 'End', '2149-01-01T00:00:00Z');
		await calculate.click();
		const question = page.getByRole('alertdialog');
		await expect(question).toHaveText(/^Calculation using selected settings could take a while, depending on your PC performances\. Are you sure you want to continue\?/);
		await question.getByRole('button', { name: 'No' }).click();
		await expect(question).toHaveCount(0);
		await expect(job).toBeHidden();
		await expect(calculate).toBeEnabled();
		// Yes: the job runs, shown in the status area, and the form's Cancel stops it
		await calculate.click();
		await question.getByRole('button', { name: 'Yes' }).click();
		await expect(job.locator('progress')).toHaveJSProperty('max', 963);
		await expect(calculate).toBeDisabled();
		await view.getByRole('button', { name: 'Cancel' }).click();
		await expect(job).toBeHidden();
		await expect(calculate).toBeEnabled();
		await expect(view.getByRole('button', { name: 'Cancel' })).toBeHidden();
		// the status area's Cancel stops it too
		await calculate.click();
		await question.getByRole('button', { name: 'Yes' }).click();
		await expect(job).toBeVisible();
		await job.getByRole('button', { name: 'Cancel' }).click();
		await expect(job).toBeHidden();
		await expect(calculate).toBeEnabled();
		await expect(tabsOf(view)).toHaveCount(1);
	});

	test('without the question when its setting is off', async ({ page }) => {
		const view = await open(page, { database: FULL, count: 963, settings: { showLongCalculationConfirmation: false } });
		await comboOf(view).selectOption({ label: '[ALL COMETS (963)]' });
		await setDate(page, 'Start', '1850-01-01T00:00:00Z');
		await setDate(page, 'End', '2149-01-01T00:00:00Z');
		await view.getByRole('button', { name: 'Calculate' }).click();
		await expect(page.locator('.shell-status__job')).toBeVisible();
		await expect(page.getByRole('alertdialog')).toHaveCount(0);
		await view.getByRole('button', { name: 'Cancel' }).click();
		await expect(page.locator('.shell-status__job')).toBeHidden();
		await expect(tabsOf(view)).toHaveCount(1);
	});
});

test('a tab per result; Edit settings, then Calculate replaces the tab; New graph; Save, Save as JPEG, Ctrl+S, Close and Delete', async ({ page }) => {
	const view = await open(page);
	const combo = comboOf(view);
	const calculate = view.getByRole('button', { name: 'Calculate' });
	const editing = view.locator('.graph-form__editing');
	await calculate.click();
	await expect(tabsOf(view)).toHaveText(['Settings', `Magnitude graph - ${BRIGHTEST}`]);
	await expect(view.getByRole('img', { name: `Magnitude, ${BRIGHTEST}`, exact: true })).toBeVisible();
	await drawn(chartOf(view));
	const size = await chartOf(view).evaluate(c => [/** @type {HTMLCanvasElement} */ (c).width, /** @type {HTMLCanvasElement} */ (c).height]);

	// Save, a PNG of the chart as it shows; Save as JPEG; Ctrl+S, the PNG
	const png = await saved(page, () => view.getByRole('button', { name: 'Save', exact: true }).click());
	expect(png.name).toBe('Comets_Graph_2026-10-18_120000.png');
	expect(pngSize(png.bytes)).toEqual(size);
	const jpeg = await saved(page, () => view.getByRole('button', { name: 'Save as JPEG' }).click());
	expect(jpeg.name).toBe('Comets_Graph_2026-10-18_120000.jpg');
	expect([...jpeg.bytes.subarray(0, 3)]).toEqual([0xff, 0xd8, 0xff]);
	const keyed = await saved(page, () => page.keyboard.press('Control+s'));
	expect(keyed.name).toBe('Comets_Graph_2026-10-18_120000.png');
	expect(pngSize(keyed.bytes)).toEqual(size);

	await tabsOf(view).first().click();
	await combo.selectOption({ label: '[ALL COMETS (34)]' });
	await view.getByRole('radio', { name: 'Earth distance' }).check();
	await calculate.click();
	await expect(tabsOf(view)).toHaveText(['Settings', `Magnitude graph - ${BRIGHTEST}`, 'Earth distance graph - 34 comets']);
	await expect(tabsOf(view).nth(2)).toHaveAttribute('aria-selected', 'true');
	await expect(view.getByRole('img', { name: 'Earth distance (AU)', exact: true })).toBeVisible();

	// Edit settings shows the tab's own settings, and Calculate replaces it
	await tabsOf(view).nth(1).click();
	await view.getByRole('button', { name: 'Edit settings' }).click();
	await expect(tabsOf(view).first()).toHaveAttribute('aria-selected', 'true');
	await expect(tabsOf(view).first()).toBeFocused();
	await expect(editing).toContainText(`Editing Magnitude graph - ${BRIGHTEST}`);
	expect(await shownOf(combo)).toBe(BRIGHTEST);
	await expect(view.getByRole('radio', { name: 'Magnitude' })).toBeChecked();
	await combo.selectOption({ label: '1P/Halley' });
	await view.getByRole('radio', { name: 'Sun distance' }).check();
	await setLimit(view, 'Maximum', null);
	await calculate.click();
	await expect(tabsOf(view)).toHaveText(['Settings', 'Sun distance graph - 1P/Halley', 'Earth distance graph - 34 comets']);
	await expect(tabsOf(view).nth(1)).toHaveAttribute('aria-selected', 'true');
	await expect(view.getByRole('img', { name: 'Sun distance (AU), 1P/Halley', exact: true })).toBeVisible();
	await expect(view.locator('canvas')).toHaveCount(2);
	// the new form as it was before the edit
	await expect(editing).toBeHidden();
	await tabsOf(view).first().click();
	expect(await shownOf(combo)).toBe('[ALL COMETS (34)]');
	await expect(view.getByRole('radio', { name: 'Earth distance' })).toBeChecked();
	await expect(view.getByRole('textbox', { name: 'Maximum' })).toHaveValue('12');

	// the edited tab's settings are those it was calculated with; New graph leaves them
	await tabsOf(view).nth(1).click();
	await view.getByRole('button', { name: 'Edit settings' }).click();
	expect(await shownOf(combo)).toBe('1P/Halley');
	await expect(view.getByRole('radio', { name: 'Sun distance' })).toBeChecked();
	await expect(view.getByRole('textbox', { name: 'Maximum' })).toHaveValue('');
	await view.getByRole('button', { name: 'New graph' }).click();
	await expect(editing).toBeHidden();
	expect(await shownOf(combo)).toBe('[ALL COMETS (34)]');

	// Close, and Delete on a tab; the tab before is selected
	await tabsOf(view).nth(2).click();
	await view.getByRole('button', { name: 'Close' }).click();
	await expect(tabsOf(view)).toHaveText(['Settings', 'Sun distance graph - 1P/Halley']);
	await expect(tabsOf(view).nth(1)).toHaveAttribute('aria-selected', 'true');
	await expect(tabsOf(view).nth(1)).toBeFocused();
	await page.keyboard.press('Delete');
	await expect(tabsOf(view)).toHaveText(['Settings']);
	await expect(tabsOf(view).first()).toBeFocused();
	await expect(view.locator('canvas')).toHaveCount(0);
	// Ctrl+S on the Settings tab saves nothing
	let downloads = 0;
	page.on('download', () => downloads++);
	await page.keyboard.press('Control+s');
	await expect(tabsOf(view).first()).toBeFocused();
	expect(downloads).toBe(0);
});

test.describe('in Europe/Zagreb', () => {
	test.use({ timezoneId: 'Europe/Zagreb' });

	test('a saved graph is named in local time', async ({ page }) => {
		const view = await open(page);
		await view.getByRole('button', { name: 'Calculate' }).click();
		await drawn(chartOf(view));
		const { name } = await saved(page, () => view.getByRole('button', { name: 'Save', exact: true }).click());
		expect(name).toBe('Comets_Graph_2026-10-18_140000.png');
	});
});

test('a chart hidden in its tab or with the view draws again when shown, in the theme shown then', async ({ page }) => {
	const view = await open(page);
	const calculate = view.getByRole('button', { name: 'Calculate' });
	await calculate.click();
	await expect(tabsOf(view)).toHaveCount(2);
	await tabsOf(view).first().click();
	await comboOf(view).selectOption({ label: '1P/Halley' });
	await calculate.click();
	await expect(tabsOf(view)).toHaveCount(3);
	await drawn(chartOf(view));
	expect(await corner(chartOf(view))).toEqual([255, 255, 255]);

	// the first result's tab, hidden while the theme changes
	const first = view.locator('canvas').first();
	expect(await corner(first)).toEqual([255, 255, 255]);
	await page.evaluate(() => { document.documentElement.dataset.theme = 'dark'; });
	await expect.poll(() => corner(chartOf(view))).toEqual([55, 55, 55]);
	expect(await corner(first)).toEqual([255, 255, 255]);
	await tabsOf(view).nth(1).click();
	await expect.poll(() => corner(chartOf(view))).toEqual([55, 55, 55]);

	// the view hidden behind another while the theme changes back: it draws nothing then
	await page.locator('.shell-nav a[href="#/database"]').click();
	await expect(view).toBeHidden();
	await page.evaluate(() => new Promise(resolve => requestAnimationFrame(() => requestAnimationFrame(resolve))));
	await page.evaluate(() => { document.documentElement.dataset.theme = 'light'; });
	expect(await corner(first)).toEqual([55, 55, 55]);
	await page.locator('.shell-nav a[href="#/graph"]').click();
	await expect.poll(() => corner(chartOf(view))).toEqual([255, 255, 255]);
});

test('closing the tab being edited goes back to the new form', async ({ page }) => {
	const view = await open(page);
	await view.getByRole('button', { name: 'Calculate' }).click();
	await expect(tabsOf(view)).toHaveCount(2);
	await view.getByRole('button', { name: 'Edit settings' }).click();
	await comboOf(view).selectOption({ label: '2P/Encke' });
	await tabsOf(view).nth(1).click();
	await view.getByRole('button', { name: 'Close' }).click();
	await expect(view.locator('.graph-form__editing')).toBeHidden();
	expect(await shownOf(comboOf(view))).toBe(BRIGHTEST);
});

test('the comet picker reads the database again after a deletion or an import, keeping its sort and its comet', async ({ page }) => {
	await page.route(MPC_URL, route => route.fulfill({ status: 200, headers: { 'access-control-allow-origin': '*' }, body: MERGE_NEW }));
	const view = await open(page);
	const combo = comboOf(view);
	await view.getByRole('button', { name: 'Sort by' }).click();
	await page.getByRole('menu', { name: 'Sort by' }).getByRole('menuitemradio', { name: 'Perihelion distance', exact: true }).click();
	await combo.selectOption({ label: '12P/Pons-Brooks' });
	const byDistance = await combo.getByRole('option').allTextContents();

	// a deletion in the Database view, while the Graph view is hidden
	await page.locator('.shell-nav a[href="#/database"]').click();
	const database = page.locator('[data-view="database"]');
	await database.getByRole('listbox', { name: 'Comets' }).getByRole('option', { name: '1P/Halley', exact: true }).click();
	await database.getByRole('button', { name: 'Delete' }).click();
	await page.getByRole('alertdialog').getByRole('button', { name: 'Yes' }).click();
	await expect(database.getByRole('heading', { level: 1 })).toHaveText('Database (33 comets)');
	await page.locator('.shell-nav a[href="#/graph"]').click();
	await expect(combo.getByRole('option')).toHaveText(['[ALL COMETS (33)]', ...byDistance.slice(1).filter(full => full !== '1P/Halley')]);
	expect(await shownOf(combo)).toBe('12P/Pons-Brooks');

	// an import from the Update dialog over the view: 21P and 1P come in
	await page.locator('.shell-nav a[href="#/update"]').click();
	const dialog = page.getByRole('dialog', { name: 'Update' });
	await dialog.getByRole('button', { name: 'Download' }).click();
	await dialog.getByRole('button', { name: 'Import' }).click();
	await expect(combo.getByRole('option')).toHaveCount(36);
	await expect(combo.getByRole('option', { name: '21P/Giacobini-Zinner' })).toHaveCount(1);
	expect(await shownOf(combo)).toBe('12P/Pons-Brooks');
});

test('the kept new form and a result\'s settings are read again over the database when they come back', async ({ page }) => {
	const view = await open(page);
	const combo = comboOf(view);
	const halley = combo.getByRole('option', { name: '1P/Halley', exact: true });
	await combo.selectOption({ label: '1P/Halley' });
	await view.getByRole('button', { name: 'Calculate' }).click();
	await expect(tabsOf(view)).toHaveText(['Settings', 'Magnitude graph - 1P/Halley']);
	// the new form, which chose 1P too, is kept while the result is edited
	await view.getByRole('button', { name: 'Edit settings' }).click();
	await expect(view.locator('.graph-form__editing')).toBeVisible();

	await page.locator('.shell-nav a[href="#/database"]').click();
	const database = page.locator('[data-view="database"]');
	await database.getByRole('listbox', { name: 'Comets' }).getByRole('option', { name: '1P/Halley', exact: true }).click();
	await database.getByRole('button', { name: 'Delete' }).click();
	await page.getByRole('alertdialog').getByRole('button', { name: 'Yes' }).click();
	await expect(database.getByRole('heading', { level: 1 })).toHaveText('Database (33 comets)');
	await page.locator('.shell-nav a[href="#/graph"]').click();
	await expect(combo.getByRole('option')).toHaveCount(34);
	await expect(halley).toHaveCount(0);
	expect(await shownOf(combo)).toBe(BRIGHTEST);

	// New graph: the kept form, read again
	await view.getByRole('button', { name: 'New graph' }).click();
	await expect(view.locator('.graph-form__editing')).toBeHidden();
	await expect(combo.getByRole('option')).toHaveCount(34);
	await expect(halley).toHaveCount(0);
	expect(await shownOf(combo)).toBe(BRIGHTEST);

	// Edit settings: the result's settings, read again
	await tabsOf(view).nth(1).click();
	await view.getByRole('button', { name: 'Edit settings' }).click();
	await expect(combo.getByRole('option')).toHaveCount(34);
	await expect(halley).toHaveCount(0);
	expect(await shownOf(combo)).toBe(BRIGHTEST);
});

test('a view opened before the stored database has loaded offers its comets once it lands', async ({ page }) => {
	await page.clock.setFixedTime(NOW);
	await seed(page, { database: SAMPLE });
	await holdDatabase(page);
	await page.goto('./#/graph');
	await page.reload();
	const view = viewOf(page);
	const combo = comboOf(view);
	await expect(view.getByRole('form', { name: 'Graph settings' })).toBeVisible();
	await expect(combo.getByRole('option')).toHaveCount(0);
	await expect(view.getByRole('button', { name: 'Calculate' })).toBeDisabled();
	await landDatabase(page);
	await expect(combo.getByRole('option')).toHaveCount(35);
	expect(await shownOf(combo)).toBe(BRIGHTEST);
	await expect(view.getByRole('button', { name: 'Calculate' })).toBeEnabled();
});
