// The Database view: the empty state, the list and its keys, the sort menu, Reset, Delete and the
// details tabs (docs/spec/05-database-and-update.md, Database view).
import { readFileSync } from 'node:fs';
import { expect, seed, test } from './fixtures.js';

/** @param {string} path a file under docs/spec/vectors */
const vector = path => readFileSync(new URL(`../../docs/spec/vectors/${path}`, import.meta.url), 'utf8');
const SAMPLE = vector('import/sample.txt');
/** @type {Array<{ full: string, id: string, q: number, sortkey: string }>} the sample collection */
const COMETS = JSON.parse(vector('comets.json')).comets;
const BY_SORTKEY = COMETS.toSorted((a, b) => (a.sortkey < b.sortkey ? -1 : a.sortkey > b.sortkey ? 1 : 0)).map(c => c.full);
const MPC_URL = 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt';

/** @param {import('@playwright/test').Page} page */
const viewOf = page => page.locator('[data-view="database"]');
/** @param {import('@playwright/test').Page} page */
const listOf = page => viewOf(page).getByRole('listbox', { name: 'Comets' });
/** @param {import('@playwright/test').Page} page */
const selectedOf = page => listOf(page).getByRole('option', { selected: true });
/** @param {import('@playwright/test').Page} page */
const detailName = page => viewOf(page).getByRole('tabpanel').locator('dd[data-key="full"]');

test('an empty database offers the download, which opens Update and starts it', async ({ page }) => {
	await page.route(MPC_URL, route => route.fulfill({ status: 200, headers: { 'access-control-allow-origin': '*' }, body: SAMPLE }));
	await page.goto('./');
	await expect(viewOf(page).getByRole('heading', { level: 1 })).toHaveText('Database (0 comets)');
	await expect(viewOf(page).getByText('The database is empty.')).toBeVisible();
	await expect(listOf(page)).toBeHidden();
	await viewOf(page).getByRole('button', { name: 'Download comet elements' }).click();
	await expect(page).toHaveURL(/#\/update\?download$/);
	const dialog = page.getByRole('dialog', { name: 'Update' });
	await expect(dialog.locator('.update-view__counts')).toHaveText('34, 0, 0; 34');
	await expect(dialog.getByLabel('Choose file')).toBeEnabled();
	await dialog.getByRole('button', { name: 'Import' }).click();
	await expect(viewOf(page).getByRole('heading', { level: 1 })).toHaveText('Database (34 comets)');
	await expect(listOf(page).getByRole('option')).toHaveCount(34);
});

test.describe('with the sample collection', () => {
	test.beforeEach(async ({ page }) => {
		await seed(page, { database: SAMPLE });
		await expect(listOf(page).getByRole('option')).toHaveCount(34);
	});

	test('the list in database order, the first selected and its details shown', async ({ page }) => {
		await expect(viewOf(page).getByRole('heading', { level: 1 })).toHaveText('Database (34 comets)');
		await expect(listOf(page).getByRole('option')).toHaveText(BY_SORTKEY);
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[0]);
		await expect(viewOf(page).getByRole('tab', { name: 'Ephemeris' })).toHaveAttribute('aria-selected', 'true');
		await expect(detailName(page)).toHaveText(BY_SORTKEY[0]);
	});

	test('the arrow keys, Home and End move the selection, and the details follow', async ({ page }) => {
		await listOf(page).focus();
		await page.keyboard.press('ArrowDown');
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[1]);
		await expect(detailName(page)).toHaveText(BY_SORTKEY[1]);
		await page.keyboard.press('End');
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[33]);
		await page.keyboard.press('ArrowDown');
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[33]);
		await page.keyboard.press('Home');
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[0]);
		await page.keyboard.press('ArrowUp');
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[0]);
		await page.keyboard.press('PageDown');
		await expect(selectedOf(page)).not.toHaveText(BY_SORTKEY[0]);
		await listOf(page).getByRole('option', { name: BY_SORTKEY[5], exact: true }).click();
		await expect(detailName(page)).toHaveText(BY_SORTKEY[5]);
	});

	test('the sort menu sorts the list and keeps the selected comet; Reset restores the default', async ({ page }) => {
		await listOf(page).getByRole('option', { name: BY_SORTKEY[3], exact: true }).click();
		await viewOf(page).getByRole('button', { name: 'Sort by' }).click();
		await page.getByRole('menu', { name: 'Sort by' }).getByRole('menuitemradio', { name: 'Perihelion distance', exact: true }).click();
		const byQ = COMETS.toSorted((a, b) => a.q - b.q).map(c => c.full);
		await expect(listOf(page).getByRole('option').first()).toHaveText(byQ[0]);
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[3]);
		await viewOf(page).getByRole('button', { name: 'Sort by' }).click();
		await page.getByRole('menu', { name: 'Sort by' }).getByRole('menuitemradio', { name: 'Descending' }).click();
		await expect(listOf(page).getByRole('option').first()).toHaveText(byQ.at(-1) ?? '');
		await viewOf(page).getByRole('button', { name: 'Reset' }).click();
		await expect(listOf(page).getByRole('option')).toHaveText(BY_SORTKEY);
		await viewOf(page).getByRole('button', { name: 'Sort by' }).click();
		await expect(page.getByRole('menu', { name: 'Sort by' }).getByRole('menuitemradio', { checked: true })).toHaveText(['Designation', 'Ascending']);
	});

	test('Delete asks first, keeps the index, and the database stays smaller after a reload', async ({ page }) => {
		await listOf(page).getByRole('option', { name: BY_SORTKEY[2], exact: true }).click();
		await viewOf(page).getByRole('button', { name: 'Delete' }).click();
		const question = page.getByRole('alertdialog', { name: `Do you really want to delete ${BY_SORTKEY[2]}` });
		await expect(question).toBeVisible();
		await question.getByRole('button', { name: 'No' }).click();
		await expect(listOf(page).getByRole('option')).toHaveCount(34);
		await viewOf(page).getByRole('button', { name: 'Delete' }).click();
		await question.getByRole('button', { name: 'Yes' }).click();
		await expect(listOf(page).getByRole('option')).toHaveCount(33);
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[3]);
		await expect(viewOf(page).getByRole('heading', { level: 1 })).toHaveText('Database (33 comets)');
		await expect(page.locator('.shell-status__count')).toHaveText('Comets: 33');
		await expect(listOf(page)).toBeFocused();
		await page.reload();
		await expect(listOf(page).getByRole('option')).toHaveCount(33);
		await expect(listOf(page).getByRole('option', { name: BY_SORTKEY[2], exact: true })).toHaveCount(0);
	});

	test('the last comet deleted moves the selection up', async ({ page }) => {
		await listOf(page).focus();
		await page.keyboard.press('End');
		await viewOf(page).getByRole('button', { name: 'Delete' }).click();
		await page.getByRole('alertdialog').getByRole('button', { name: 'Yes' }).click();
		await expect(selectedOf(page)).toHaveText(BY_SORTKEY[32]);
	});

	test('the Orbital elements tab, JPL Info, and a double click switching tabs', async ({ page }) => {
		const halley = COMETS.find(c => c.full === '1P/Halley');
		await listOf(page).getByRole('option', { name: '1P/Halley', exact: true }).click();
		await viewOf(page).getByRole('tab', { name: 'Orbital elements' }).click();
		const panel = viewOf(page).getByRole('tabpanel');
		await expect(panel.locator('dd[data-key="T"]')).toHaveText('2061-08-01.5218');
		await expect(panel.locator('dd[data-key="q"]')).toHaveText('0.570945AU');
		await expect(panel.getByRole('link', { name: 'JPL Info' })).toHaveAttribute('href', `https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=${halley?.id}`);
		await listOf(page).getByRole('option', { name: '1P/Halley', exact: true }).dblclick();
		await expect(viewOf(page).getByRole('tab', { name: 'Ephemeris' })).toHaveAttribute('aria-selected', 'true');
		await expect(viewOf(page).getByRole('tabpanel').locator('dd[data-key="Tn"]')).toHaveText('01.08.2061. 12:31:23');
		await viewOf(page).getByRole('tab', { name: 'Ephemeris' }).press('ArrowRight');
		await expect(viewOf(page).getByRole('tab', { name: 'Orbital elements' })).toBeFocused();
	});
});

test('Delete without its question when the setting is off', async ({ page }) => {
	await seed(page, { database: SAMPLE, settings: { showDeleteCometConfirmation: false } });
	await expect(listOf(page).getByRole('option')).toHaveCount(34);
	await viewOf(page).getByRole('button', { name: 'Delete' }).click();
	await expect(listOf(page).getByRole('option')).toHaveCount(33);
	await expect(page.getByRole('alertdialog')).toHaveCount(0);
});
