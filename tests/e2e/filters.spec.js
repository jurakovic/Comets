// The Database view's filters panel: its rows and editors, live apply, Apply and Enter, the
// validation by its row, + and remove, Reset, closing it; and filters.json cases through the
// panel, over the MPC snapshot at the vectors' clock.
import { readFileSync } from 'node:fs';
import { expect, holdDatabase, landDatabase, seed, test } from './fixtures.js';

/** @param {string} path a file under tests/vectors/data */
const vector = path => readFileSync(new URL(`../vectors/data/${path}`, import.meta.url), 'utf8');
const SAMPLE = vector('import/sample.txt');
/** @type {Array<{ full: string, q: number, e: number, Tn: number, sortkey: string }>} the sample collection */
const COMETS = JSON.parse(vector('comets.json')).comets
	.toSorted((/** @type {any} */ a, /** @type {any} */ b) => (a.sortkey < b.sortkey ? -1 : a.sortkey > b.sortkey ? 1 : 0));
/** the sample's full names that pass, in the list's order */
const names = (/** @type {(comet: typeof COMETS[number]) => boolean} */ pass) => COMETS.filter(pass).map(c => c.full);
const FILTERS = JSON.parse(vector('filters.json'));
/** the vectors' clock, at which the default Perihelion date is 1 September 2026 */
const VECTOR_NOW = new Date('2026-10-18T12:00:00Z');

/** @param {import('@playwright/test').Page} page */
const viewOf = page => page.locator('[data-view="database"]');
/** @param {import('@playwright/test').Page} page */
const panelOf = page => viewOf(page).getByRole('form', { name: 'Filters' });
/** @param {import('@playwright/test').Page} page */
const filtersButton = page => viewOf(page).getByRole('button', { name: 'Filters' });
/**
 * @param {import('@playwright/test').Page} page
 * @param {number} n from 1
 */
const rowOf = (page, n) => panelOf(page).getByRole('group', { name: `Filter ${n}`, exact: true });
/** @param {import('@playwright/test').Page} page */
const titleOf = page => viewOf(page).getByRole('heading', { level: 1 });
/** @param {import('@playwright/test').Page} page */
const optionsOf = page => viewOf(page).getByRole('listbox', { name: 'Comets' }).getByRole('option');
/** @param {import('@playwright/test').Locator} row */
const valueOf = row => row.getByRole('textbox', { name: 'Value' });

/**
 * types into a field that refuses what it cannot hold, from empty
 * @param {import('@playwright/test').Locator} field
 * @param {string} text
 */
async function typeInto(field, text) {
	await field.fill('');
	await field.pressSequentially(text);
}

/**
 * sets a date picker through its editor's JD field
 * @param {import('@playwright/test').Page} page
 * @param {import('@playwright/test').Locator} row
 * @param {string} jd
 */
async function pickJd(page, row, jd) {
	await row.locator('.date-picker__button').click();
	const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
	await typeInto(editor.getByRole('textbox', { name: 'JD' }), jd);
	await editor.getByRole('button', { name: 'OK' }).click();
	await expect(editor).toBeHidden();
}

test.describe('with the sample collection', () => {
	test.beforeEach(async ({ page }) => {
		await page.clock.setFixedTime(VECTOR_NOW);
		await seed(page, { database: SAMPLE });
		await expect(optionsOf(page)).toHaveCount(34);
		await filtersButton(page).click();
	});

	test('Filters opens the panel in the details\' place with the default rows, all inactive', async ({ page }) => {
		await expect(filtersButton(page)).toHaveAttribute('aria-expanded', 'true');
		await expect(filtersButton(page)).toHaveText('Filters ▲');
		await expect(panelOf(page)).toBeVisible();
		await expect(viewOf(page).getByRole('tablist')).toBeHidden();
		await expect(panelOf(page).getByRole('group', { name: /^Filter \d+$/ })).toHaveCount(4);
		const expected = [
			['full', 'contains', ''],
			['Tn', 'greaterThan', ''],
			['q', 'lessThan', 'AU'],
			['CurrentMag', 'lessThan', '']
		];
		for (const [i, [key, compare, unit]] of expected.entries()) {
			const row = rowOf(page, i + 1);
			await expect(row.getByRole('checkbox', { name: 'Active' })).not.toBeChecked();
			await expect(row.getByRole('combobox', { name: 'Property' })).toHaveValue(key);
			await expect(row.getByRole('combobox', { name: 'Compare' })).toHaveValue(compare);
			await expect(row.locator('.filters-panel__unit')).toHaveText(unit);
		}
		await expect(rowOf(page, 1).getByRole('combobox', { name: 'Compare' }).getByRole('option'))
			.toHaveText(['Contains', 'Does not contain', 'Starts with', 'Ends with']);
		await expect(rowOf(page, 3).getByRole('combobox', { name: 'Compare' }).getByRole('option'))
			.toHaveText(['Greater than (>)', 'Less than (<)', 'Equals (=)']);
		await expect(rowOf(page, 1).getByRole('combobox', { name: 'Property' }).getByRole('option')).toHaveCount(17);
		await expect(rowOf(page, 2).locator('.date-picker__button')).toHaveText('01.09.2026. 00:00:00');
		await expect(titleOf(page)).toHaveText('Database (34 comets)');
	});

	test('Close, Esc and a double click on the list close the panel, and the focus goes to Filters', async ({ page }) => {
		await panelOf(page).getByRole('button', { name: 'Close' }).click();
		await expect(panelOf(page)).toBeHidden();
		await expect(viewOf(page).getByRole('tablist')).toBeVisible();
		await expect(filtersButton(page)).toBeFocused();
		await expect(filtersButton(page)).toHaveAttribute('aria-expanded', 'false');
		await expect(filtersButton(page)).toHaveText('Filters ▼');

		await filtersButton(page).click();
		await valueOf(rowOf(page, 1)).focus();
		await page.keyboard.press('Escape');
		await expect(panelOf(page)).toBeHidden();
		await expect(filtersButton(page)).toBeFocused();

		await filtersButton(page).click();
		await optionsOf(page).nth(2).dblclick();
		await expect(panelOf(page)).toBeHidden();
		await expect(filtersButton(page)).toBeFocused();
		// the double click closed the panel instead of switching the tab
		await expect(viewOf(page).getByRole('tab', { name: 'Ephemeris' })).toHaveAttribute('aria-selected', 'true');
	});

	test('a value filters as it is typed and checks its row; emptying it unchecks the row', async ({ page }) => {
		const row = rowOf(page, 1);
		await valueOf(row).pressSequentially('halley');
		await expect(row.getByRole('checkbox', { name: 'Active' })).toBeChecked();
		await expect(optionsOf(page)).toHaveText(['1P/Halley']);
		await expect(titleOf(page)).toHaveText('Database (1/34 comets)');
		await expect(page.locator('.shell-status__count')).toHaveText('Comets: 1 (34)');
		await valueOf(row).pressSequentially(', encke');
		await expect(optionsOf(page)).toHaveText(names(c => /halley|encke/i.test(c.full)));
		await valueOf(row).fill('');
		await expect(row.getByRole('checkbox', { name: 'Active' })).not.toBeChecked();
		await expect(optionsOf(page)).toHaveCount(34);
		// the checkbox alone applies too
		await valueOf(row).fill('atlas');
		await row.getByRole('checkbox', { name: 'Active' }).uncheck();
		await expect(optionsOf(page)).toHaveCount(34);
		await row.getByRole('checkbox', { name: 'Active' }).check();
		await expect(optionsOf(page)).toHaveText(names(c => c.full.toLowerCase().includes('atlas')));
	});

	test('a number row takes what its property allows; the compare and the property apply as they change', async ({ page }) => {
		const row = rowOf(page, 3);
		await valueOf(row).pressSequentially('0.5x');
		await expect(valueOf(row)).toHaveValue('0.5');
		await expect(optionsOf(page)).toHaveText(names(c => c.q < 0.5));
		await valueOf(row).pressSequentially('1234567');
		await expect(valueOf(row)).toHaveValue('0.512345');
		await typeInto(valueOf(row), '16');
		await expect(valueOf(row)).toHaveValue('1');
		await typeInto(valueOf(row), '0.5');
		await row.getByRole('combobox', { name: 'Compare' }).selectOption('Greater than (>)');
		await expect(optionsOf(page)).toHaveText(names(c => c.q > 0.5));

		// a new property: its default compare, its unit, the value kept
		await row.getByRole('combobox', { name: 'Property' }).selectOption('Eccentricity');
		await expect(row.getByRole('combobox', { name: 'Compare' })).toHaveValue('lessThan');
		await expect(row.locator('.filters-panel__unit')).toHaveText('');
		await expect(valueOf(row)).toHaveValue('0.5');
		await expect(optionsOf(page)).toHaveText(names(c => c.e < 0.5));
		await row.getByRole('combobox', { name: 'Property' }).selectOption('Inclination');
		await expect(row.locator('.filters-panel__unit')).toHaveText('°');
		await row.getByRole('combobox', { name: 'Property' }).selectOption('Eccentricity');

		// a text property shows its own field, empty, and the checked row asks for a value
		await row.getByRole('combobox', { name: 'Property' }).selectOption('Discoverer');
		await expect(row.getByRole('combobox', { name: 'Compare' })).toHaveValue('contains');
		await expect(valueOf(row)).toHaveValue('');
		await expect(row.getByRole('alert')).toHaveText('Please enter value for "Discoverer"');
		await expect(valueOf(row)).toHaveAttribute('aria-invalid', 'true');
		await expect(optionsOf(page)).toHaveText(names(c => c.e < 0.5));
		await valueOf(row).pressSequentially('halley');
		await expect(row.getByRole('alert')).toBeHidden();
		await expect(valueOf(row)).not.toHaveAttribute('aria-invalid');
		await expect(optionsOf(page)).toHaveText(['1P/Halley']);
	});

	test('an error stays by its row and keeps the result; Apply and Enter focus its field', async ({ page }) => {
		await valueOf(rowOf(page, 1)).pressSequentially('atlas');
		const atlas = names(c => c.full.toLowerCase().includes('atlas'));
		await expect(optionsOf(page)).toHaveText(atlas);
		await rowOf(page, 3).getByRole('checkbox', { name: 'Active' }).check();
		await expect(rowOf(page, 3).getByRole('alert')).toHaveText('Please enter value for "Perihelion distance"');
		await expect(rowOf(page, 1).getByRole('alert')).toBeHidden();
		await expect(optionsOf(page)).toHaveText(atlas);
		await panelOf(page).getByRole('button', { name: 'Apply' }).click();
		await expect(valueOf(rowOf(page, 3))).toBeFocused();
		await valueOf(rowOf(page, 1)).press('Enter');
		await expect(valueOf(rowOf(page, 3))).toBeFocused();
		await valueOf(rowOf(page, 3)).pressSequentially('1');
		await expect(rowOf(page, 3).getByRole('alert')).toBeHidden();
		await expect(optionsOf(page)).toHaveText(names(c => c.full.toLowerCase().includes('atlas') && c.q < 1));
	});

	test('+ adds Full name rows up to ten and then hides; remove takes a row out and applies', async ({ page }) => {
		const add = panelOf(page).getByRole('button', { name: 'Add filter' });
		for (let n = 5; n <= 10; n++) {
			await add.click();
			await expect(rowOf(page, n).getByRole('combobox', { name: 'Property' })).toBeFocused();
			await expect(rowOf(page, n).getByRole('combobox', { name: 'Property' })).toHaveValue('full');
		}
		await expect(add).toBeHidden();
		await valueOf(rowOf(page, 10)).pressSequentially('halley');
		await expect(optionsOf(page)).toHaveText(['1P/Halley']);
		await rowOf(page, 10).getByRole('button', { name: 'Remove filter' }).click();
		await expect(panelOf(page).getByRole('group', { name: /^Filter \d+$/ })).toHaveCount(9);
		await expect(add).toBeVisible();
		await expect(optionsOf(page)).toHaveCount(34);
		await expect(rowOf(page, 9).getByRole('button', { name: 'Remove filter' })).toBeFocused();
		// the rows after a removed one move up and are named by their new place
		await rowOf(page, 1).getByRole('button', { name: 'Remove filter' }).click();
		await expect(rowOf(page, 1).getByRole('combobox', { name: 'Property' })).toHaveValue('Tn');
		await expect(rowOf(page, 1).getByRole('button', { name: 'Remove filter' })).toBeFocused();
	});

	test('a date checks its row; Reset puts back the default rows and the whole database', async ({ page }) => {
		const row = rowOf(page, 2);
		await row.getByRole('button', { name: 'Date presets' }).click();
		await page.getByRole('menu').getByRole('menuitem', { name: 'Today' }).click();
		await expect(row.getByRole('checkbox', { name: 'Active' })).toBeChecked();
		await expect(row.locator('.date-picker__button')).toHaveText('18.10.2026. 00:00:00');
		await expect(optionsOf(page)).toHaveText(names(c => c.Tn > 2461331.5));
		await pickJd(page, row, '2461331.5');
		await row.getByRole('combobox', { name: 'Compare' }).selectOption('Less than (<)');
		await expect(optionsOf(page)).toHaveText(names(c => c.Tn < 2461331.5));
		await viewOf(page).getByRole('button', { name: 'Reset', exact: true }).click();
		await expect(optionsOf(page)).toHaveCount(34);
		await expect(panelOf(page)).toBeVisible();
		await expect(row.getByRole('checkbox', { name: 'Active' })).not.toBeChecked();
		await expect(row.getByRole('combobox', { name: 'Compare' })).toHaveValue('greaterThan');
		await expect(row.locator('.date-picker__button')).toHaveText('01.09.2026. 00:00:00');
	});

	test('an import puts back the default rows', async ({ page }) => {
		await valueOf(rowOf(page, 1)).pressSequentially('halley');
		await expect(optionsOf(page)).toHaveCount(1);
		await page.locator('.shell-nav a[href="#/update"]').click();
		const dialog = page.getByRole('dialog', { name: 'Update' });
		await dialog.getByLabel('Choose file').setInputFiles({ name: 'sample.txt', mimeType: 'text/plain', buffer: Buffer.from(SAMPLE) });
		await dialog.getByRole('button', { name: 'Import' }).click();
		await expect(dialog).toBeHidden();
		await expect(optionsOf(page)).toHaveCount(34);
		await expect(valueOf(rowOf(page, 1))).toHaveValue('');
		await expect(rowOf(page, 1).getByRole('checkbox', { name: 'Active' })).not.toBeChecked();
	});
});

test('the filters wait for the stored database, whose load replaces them', async ({ page }) => {
	await seed(page, { database: SAMPLE });
	await holdDatabase(page);
	await page.reload();
	await expect(titleOf(page)).toHaveText('Database');
	await expect(filtersButton(page)).toBeDisabled();
	await landDatabase(page);
	await expect(optionsOf(page)).toHaveCount(34);
	await expect(filtersButton(page)).toBeEnabled();
});

test('an empty database has no filters to set', async ({ page }) => {
	await page.goto('./');
	await expect(titleOf(page)).toHaveText('Database (0 comets)');
	await expect(filtersButton(page)).toBeDisabled();
});

test.describe('filters.json through the panel', () => {
	test.beforeEach(async ({ page }) => {
		await page.clock.setFixedTime(VECTOR_NOW);
		await seed(page, { database: vector(FILTERS.input) });
		await expect(optionsOf(page)).toHaveCount(963);
		await filtersButton(page).click();
	});

	// every editor and every kind of compare, a row left unchecked, and rows together
	const CASES = [1, 2, 4, 7, 9, 11, 16, 19, 26];
	for (const index of CASES) {
		const { title, filters, result } = FILTERS.cases[index];
		test(title, async ({ page }) => {
			for (const [i, filter] of filters.entries()) {
				const row = rowOf(page, i + 1);
				await row.getByRole('combobox', { name: 'Property' }).selectOption(filter.property);
				await row.getByRole('combobox', { name: 'Compare' }).selectOption({ index: filter.compare });
				if (filter.property === 'Tn') await pickJd(page, row, filter.text);
				else await typeInto(valueOf(row), filter.text);
				if (!filter.checked) await row.getByRole('checkbox', { name: 'Active' }).uncheck();
			}
			await expect(optionsOf(page)).toHaveText(result);
		});
	}
});
