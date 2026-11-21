// The comet picker mounted alone on the page, over the sample collection at the vectors' clock: its
// entries and choice, the tooltip, quick select, the sort, Filter over the whole database, and the
// Perihelion Date preset of a date picker beside it following the chosen comet.
import { readFileSync } from 'node:fs';
import { expect, test } from './fixtures.js';

const SAMPLE = readFileSync(new URL('../vectors/data/import/sample.txt', import.meta.url), 'utf8');
/** the vectors' clock, at which selection.json's quick selections hold */
const VECTOR_NOW = new Date('2026-10-18T12:00:00Z');
/** the sample's brightest comet then, and the closest to Earth */
const BRIGHTEST = 'C/2026 T99 (Parabola Test)';
const CLOSEST_TO_EARTH = '2P/Encke';

/**
 * mounts a picker over the Help view with the sample as the database and its list, and a date
 * picker whose Perihelion Date preset follows it; records what onChange reports
 * @param {import('@playwright/test').Page} page
 */
async function mountPicker(page) {
	await page.clock.setFixedTime(VECTOR_NOW);
	await page.goto('./#/help');
	await page.locator('[data-view="help"]').waitFor();
	await page.evaluate(async sample => {
		/** @param {string} path */
		const load = path => import(new URL(`js/${path}`, location.href).href);
		const [{ createCometPicker }, { createDatePicker }, { importElements }, { defaultFilters }, { DEFAULT_SORT }, { createObservationsService }] = await Promise.all([
			load('ui/components/comet-picker.js'), load('ui/components/date-picker.js'), load('core/formats/import.js'),
			load('core/comets/filter.js'), load('core/comets/sort.js'), load('services/observations.js')
		]);
		const clock = { now: () => Date.now(), timeZone: () => 'UTC' };
		const database = importElements([], 'MPC', sample).comets;
		const observations = createObservationsService({ clock, observer: () => ({ latitude: 0, longitude: 0 }) });
		/** @type {any} */
		const probe = window;
		probe.changes = [];
		probe.database = database;
		const start = createDatePicker({ value: Date.UTC(2026, 9, 1), now: clock.now, timeZone: clock.timeZone, label: 'Start', onChange: () => {} });
		probe.picker = createCometPicker({
			list: { comets: database, filters: defaultFilters(clock.now()), sort: DEFAULT_SORT },
			database: () => database,
			value: observations.value,
			now: clock.now,
			timeZone: clock.timeZone,
			datePickers: [start],
			onChange: (/** @type {any} */ choice) => probe.changes.push(choice.all ? 'all' : choice.comet?.full ?? null)
		});
		document.body.prepend(probe.picker.element, start.element);
	}, SAMPLE);
	return page.getByRole('group', { name: 'Comet' });
}

/** @param {import('@playwright/test').Page} page */
const changes = page => page.evaluate(() => /** @type {any} */ (window).changes);
/** @param {import('@playwright/test').Locator} picker */
const comboOf = picker => picker.getByRole('combobox', { name: 'Comet' });
/** @param {import('@playwright/test').Locator} combo the text of the entry shown */
const shownOf = combo => combo.evaluate(select => /** @type {HTMLSelectElement} */ (select).selectedOptions[0]?.text ?? null);

/**
 * the presets a date picker offers, as its menu lists them once it shows
 * @param {import('@playwright/test').Page} page
 */
async function presets(page) {
	const button = page.getByRole('group', { name: 'Start' }).getByRole('button', { name: 'Date presets' });
	const menu = page.getByRole('menu', { name: 'Date presets' });
	await button.click();
	// the menu stays unseen until placed, a task after it opens; Today is always there
	await expect(menu.getByRole('menuitem', { name: 'Today' })).toBeVisible();
	const items = await menu.getByRole('menuitem').allTextContents();
	await page.keyboard.press('Escape');
	await expect(menu).toBeHidden();
	return items;
}

test('[ALL COMETS (n)] first, then the list in its order; the brightest chosen, with its tooltip', async ({ page }) => {
	const picker = await mountPicker(page);
	const combo = comboOf(picker);
	const options = combo.getByRole('option');
	await expect(options).toHaveCount(35);
	await expect(options.first()).toHaveText('[ALL COMETS (34)]');
	await expect(options.nth(1)).toHaveText('1P/Halley');
	expect(await shownOf(combo)).toBe(BRIGHTEST);
	await combo.hover();
	await expect(combo).toHaveAttribute('title', `${BRIGHTEST}\nT: 03 Nov 2026 06:00:00\nP: -\nq: 0.812345 AU\nr: 0.865609 AU\nd: 1.474289 AU\nm: 9.72`);
	expect(await presets(page)).toContain('Perihelion Date');
});

test('the first entry is multiple mode, without a tooltip or a Perihelion Date', async ({ page }) => {
	const picker = await mountPicker(page);
	const combo = comboOf(picker);
	await combo.selectOption({ index: 0 });
	await expect(combo).not.toHaveAttribute('title');
	expect(await presets(page)).not.toContain('Perihelion Date');
	await combo.selectOption({ label: '1P/Halley' });
	await expect(combo).toHaveAttribute('title', /^1P\/Halley\nT: 01 Aug 2061 12:31:23\nP: 75\.4705 years\n/);
	const start = page.getByRole('group', { name: 'Start' });
	await start.getByRole('button', { name: 'Date presets' }).click();
	await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: 'Perihelion Date' }).click();
	await expect(start.getByRole('button').first()).toHaveText('01.08.2061. 12:31:23');
	expect(await changes(page)).toEqual(['all', '1P/Halley']);
	expect(await page.evaluate(() => /** @type {any} */ (window).picker.choice().comet.full)).toBe('1P/Halley');
});

test('quick select picks by its rule, also from multiple mode', async ({ page }) => {
	const picker = await mountPicker(page);
	const combo = comboOf(picker);
	const button = picker.getByRole('button', { name: 'Quick select' });
	const menu = page.getByRole('menu', { name: 'Quick select' });
	await button.click();
	await expect(menu.getByRole('menuitem')).toHaveText(['Brightest', 'Closest to perihelion', 'Closest to Earth', 'Closest to Sun']);
	await menu.getByRole('menuitem', { name: 'Closest to Earth' }).click();
	expect(await shownOf(combo)).toBe(CLOSEST_TO_EARTH);
	await expect(button).toBeFocused();
	await combo.selectOption({ index: 0 });
	await button.click();
	await menu.getByRole('menuitem', { name: 'Brightest' }).click();
	expect(await shownOf(combo)).toBe(BRIGHTEST);
	expect(await changes(page)).toEqual([CLOSEST_TO_EARTH, 'all', BRIGHTEST]);
});

test('the sort re-sorts the list and keeps the choice', async ({ page }) => {
	const picker = await mountPicker(page);
	const combo = comboOf(picker);
	await picker.getByRole('button', { name: 'Sort by' }).click();
	await page.getByRole('menu', { name: 'Sort by' }).getByRole('menuitemradio', { name: 'Perihelion distance', exact: true }).click();
	const options = combo.getByRole('option');
	await expect(options.nth(1)).toHaveText('C/2024 G3 (ATLAS)');
	await expect(options.nth(2)).toHaveText('96P-B/Machholz');
	expect(await shownOf(combo)).toBe(BRIGHTEST);
	await combo.selectOption({ index: 0 });
	await picker.getByRole('button', { name: 'Sort by' }).click();
	await page.getByRole('menu', { name: 'Sort by' }).getByRole('menuitemradio', { name: 'Descending' }).click();
	await expect(options.nth(1)).toHaveText('C/2014 UN271 (Bernardinelli-Bernstein)');
	expect(await shownOf(combo)).toBe('[ALL COMETS (34)]');
	expect(await page.evaluate(() => /** @type {any} */ (window).picker.list().sort)).toEqual({ property: 'q', ascending: false });
	expect(await changes(page)).toEqual(['all']);
});

test.describe('Filter', () => {
	/** @param {import('@playwright/test').Page} page */
	const dialogOf = page => page.getByRole('dialog');
	/**
	 * @param {import('@playwright/test').Page} page
	 * @param {number} n from 1
	 */
	const rowOf = (page, n) => dialogOf(page).getByRole('group', { name: `Filter ${n}`, exact: true });
	/**
	 * sets a row's value in one edit, which applies the rows once
	 * @param {import('@playwright/test').Page} page
	 * @param {number} n from 1
	 * @param {string} text
	 */
	const typeInto = (page, n, text) => rowOf(page, n).getByRole('textbox', { name: 'Value' }).fill(text);

	test('opens the filters panel over the whole database, and applies it to the list as it is edited', async ({ page }) => {
		const picker = await mountPicker(page);
		const combo = comboOf(picker);
		const filter = picker.getByRole('button', { name: 'Filter' });
		await filter.click();
		const dialog = dialogOf(page);
		await expect(dialog.getByRole('heading', { name: 'Database (34 comets)' })).toBeVisible();
		await expect(dialog.getByRole('form', { name: 'Filters' })).toBeVisible();
		await typeInto(page, 4, '20');
		await expect(dialog.getByRole('heading')).toHaveText('Database (9/34 comets)');
		await expect(combo.getByRole('option').first()).toHaveText('[ALL COMETS (9)]');
		await expect(combo.getByRole('option').nth(1)).toHaveText('2P/Encke');
		expect(await shownOf(combo)).toBe(BRIGHTEST);
		// a list without the comet chosen before Filter opened gets the brightest, and one with it
		// gets it back
		await typeInto(page, 1, 'lemmon');
		await expect(combo.getByRole('option')).toHaveText(['[ALL COMETS (1)]', 'C/2025 A6 (Lemmon)']);
		expect(await shownOf(combo)).toBe('C/2025 A6 (Lemmon)');
		await typeInto(page, 1, '');
		await expect(combo.getByRole('option').first()).toHaveText('[ALL COMETS (9)]');
		expect(await shownOf(combo)).toBe(BRIGHTEST);
		expect(await changes(page)).toEqual(['C/2025 A6 (Lemmon)', BRIGHTEST]);
		await dialog.getByRole('button', { name: 'Close' }).click();
		await expect(dialog).toHaveCount(0);
		await expect(filter).toBeFocused();
		// the next opening starts from the filters the picker keeps
		await filter.click();
		await expect(dialog.getByRole('heading')).toHaveText('Database (9/34 comets)');
		await expect(rowOf(page, 4).getByRole('checkbox', { name: 'Active' })).toBeChecked();
		await expect(rowOf(page, 4).getByRole('textbox', { name: 'Value' })).toHaveValue('20');
		await page.keyboard.press('Escape');
		await expect(dialog).toHaveCount(0);
		expect(await page.evaluate(() => /** @type {any} */ (window).picker.list().comets.length)).toBe(9);
	});

	test('keeps multiple mode; a list it empties offers nothing to choose', async ({ page }) => {
		const picker = await mountPicker(page);
		const combo = comboOf(picker);
		await combo.selectOption({ index: 0 });
		await picker.getByRole('button', { name: 'Filter' }).click();
		await typeInto(page, 1, 'encke');
		await expect(combo.getByRole('option')).toHaveText(['[ALL COMETS (1)]', '2P/Encke']);
		expect(await shownOf(combo)).toBe('[ALL COMETS (1)]');
		await typeInto(page, 1, 'nothing like it');
		await expect(dialogOf(page).getByRole('heading')).toHaveText('Database (0/34 comets)');
		await expect(combo.getByRole('option')).toHaveCount(0);
		await expect(combo).toBeDisabled();
		await expect(picker.getByRole('button', { name: 'Quick select' })).toBeDisabled();
		expect(await page.evaluate(() => /** @type {any} */ (window).picker.choice())).toEqual({ all: false, comet: null });
		await typeInto(page, 1, 'halley');
		expect(await shownOf(combo)).toBe('[ALL COMETS (1)]');
		await expect(combo).toBeEnabled();
		expect(await changes(page)).toEqual(['all', null, 'all']);
		await page.keyboard.press('Escape');
		expect(await presets(page)).not.toContain('Perihelion Date');
	});

	test('a row in error is marked by its row and leaves the list as it was', async ({ page }) => {
		const picker = await mountPicker(page);
		await picker.getByRole('button', { name: 'Filter' }).click();
		await typeInto(page, 1, 'encke');
		await rowOf(page, 3).getByRole('checkbox', { name: 'Active' }).check();
		await expect(rowOf(page, 3).getByRole('alert')).toHaveText('Please enter value for "Perihelion distance"');
		await expect(comboOf(picker).getByRole('option')).toHaveText(['[ALL COMETS (1)]', '2P/Encke']);
		await expect(dialogOf(page).getByRole('heading')).toHaveText('Database (1/34 comets)');
	});
});

test('a list set by the owner keeps the choice as a new list keeps it', async ({ page }) => {
	const picker = await mountPicker(page);
	const combo = comboOf(picker);
	await page.evaluate(() => {
		/** @type {any} */
		const probe = window;
		const list = probe.picker.list();
		probe.picker.setList({ ...list, comets: probe.database.slice(0, 3) });
	});
	await expect(combo.getByRole('option')).toHaveText(['[ALL COMETS (3)]', '1P/Halley', '2P/Encke', '3D-A/Biela']);
	expect(await shownOf(combo)).toBe(CLOSEST_TO_EARTH);
	await page.evaluate(() => {
		/** @type {any} */
		const probe = window;
		probe.picker.setList({ ...probe.picker.list(), comets: [] });
	});
	await expect(combo).toBeDisabled();
	expect(await presets(page)).not.toContain('Perihelion Date');
	expect(await changes(page)).toEqual([]);
});
