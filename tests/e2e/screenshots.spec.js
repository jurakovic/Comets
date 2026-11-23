// Every view in both themes, at desktop and phone size, over the sample collection with the clock
// held at the vectors' instant; and the empty database, the Orbital elements tab, the filters panel,
// the Update preview, the import result browser, and the Ephemeris and Graph views' errors and a
// result. Written to test-results/screenshots/.
import { readFileSync } from 'node:fs';
import { expect, seed, test, VIEWS } from './fixtures.js';

/** @param {string} path a file under tests/vectors/data */
const vector = path => readFileSync(new URL(`../vectors/data/${path}`, import.meta.url), 'utf8');
const MERGE = JSON.parse(vector('import-merge.json'));
const SAMPLE = vector(MERGE.oldFile);
const MPC_URL = 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt';
/** the vectors' clock; no timer runs, so the live block stays at this second */
const NOW = new Date('2026-10-18T12:00:00Z');
/** two days before NOW, within the update interval, so start-up asks nothing */
const LAST_UPDATE = '2026-10-16T09:30:00.000Z';
const SIZES = { desktop: { width: 1280, height: 800 }, phone: { width: 390, height: 844 } };

/** @param {import('@playwright/test').Page} page */
const databaseOf = page => page.locator('[data-view="database"]');

for (const theme of /** @type {const} */ (['light', 'dark'])) {
	for (const [size, viewport] of Object.entries(SIZES)) {
		/**
		 * the page in this theme and size, with the clock held at NOW
		 * @param {import('@playwright/test').Page} page
		 */
		async function prepare(page) {
			await page.emulateMedia({ colorScheme: theme });
			await page.setViewportSize(viewport);
			// installed a minute before: the fake clock runs in real time until paused, so pausing at
			// the instant it was installed at may be a step back, which fails
			await page.clock.install({ time: new Date(NOW.getTime() - 60_000) });
			await page.clock.pauseAt(NOW);
		}

		/**
		 * the sample collection stored, then loaded into the Database view
		 * @param {import('@playwright/test').Page} page
		 */
		async function seedSample(page) {
			await seed(page, { database: SAMPLE, settings: { lastUpdateDate: LAST_UPDATE } });
			await expect(databaseOf(page).getByRole('heading', { level: 1 })).toHaveText('Database (34 comets)');
			await expect(databaseOf(page).locator('.live-ephemeris dd')).toHaveCount(8);
		}

		/**
		 * @param {import('@playwright/test').Page} page
		 * @param {string} name
		 */
		const shot = (page, name) => page.screenshot({ path: `test-results/screenshots/${name}-${theme}-${size}.png` });

		test(`views, ${theme}, ${size}`, async ({ page }) => {
			await prepare(page);
			await page.goto('./');
			await expect(databaseOf(page).getByText('The database is empty.')).toBeVisible();
			await shot(page, 'database-empty');
			await seedSample(page);
			for (const [key] of VIEWS) {
				await page.goto(`./#/${key}`);
				await page.locator(`[data-view="${key}"]`).waitFor();
				await shot(page, key);
			}
		});

		test(`the Database view's elements and filters, ${theme}, ${size}`, async ({ page }) => {
			await prepare(page);
			await seedSample(page);
			const view = databaseOf(page);
			await view.getByRole('tab', { name: 'Orbital elements' }).click();
			await expect(view.getByRole('tabpanel').locator('dd[data-key="T"]')).toHaveText('2061-08-01.5218');
			await shot(page, 'database-elements');
			// a row that filters, and a row checked without a value, which shows its error
			await view.getByRole('button', { name: 'Filters' }).click();
			const panel = view.getByRole('form', { name: 'Filters' });
			await panel.getByRole('group', { name: 'Filter 3', exact: true }).getByRole('textbox', { name: 'Value' }).pressSequentially('1');
			await panel.getByRole('group', { name: 'Filter 4', exact: true }).getByRole('checkbox', { name: 'Active' }).check();
			await expect(panel.getByRole('alert')).toHaveText('Please enter value for "Current magnitude"');
			await expect(view.getByRole('heading', { level: 1 })).toHaveText(/^Database \(\d+\/34 comets\)$/);
			await shot(page, 'database-filters');
		});

		test(`the Ephemeris view's errors and a result, ${theme}, ${size}`, async ({ page }) => {
			await prepare(page);
			await seedSample(page);
			await page.goto('./#/ephemeris');
			const view = page.locator('[data-view="ephemeris"]');
			await expect(view.getByRole('combobox', { name: 'Comet' }).getByRole('option')).toHaveCount(35);
			// a requirement checked without a value, and End before Start
			await view.getByRole('checkbox', { name: 'Max Sun distance' }).check();
			await view.getByRole('group', { name: 'End', exact: true }).getByRole('button', { name: 'Date presets' }).click();
			await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: 'Subtract 1 year' }).click();
			await view.getByRole('button', { name: 'Calculate' }).click();
			await expect(view.getByRole('alert')).toHaveCount(2);
			await shot(page, 'ephemeris-errors');
			await view.getByRole('checkbox', { name: 'Max Sun distance' }).uncheck();
			await view.getByRole('group', { name: 'End', exact: true }).getByRole('button', { name: 'Date presets' }).click();
			await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: 'Default' }).click();
			await view.getByRole('button', { name: 'Calculate' }).click();
			await expect(view.getByRole('tab', { name: 'Ephemeris - C/2026 T99 (Parabola Test)' })).toHaveAttribute('aria-selected', 'true');
			await shot(page, 'ephemeris-result');
		});

		test(`the Graph view's errors and a result, ${theme}, ${size}`, async ({ page }) => {
			await prepare(page);
			await seedSample(page);
			await page.goto('./#/graph');
			const view = page.locator('[data-view="graph"]');
			await expect(view.getByRole('combobox', { name: 'Comet' }).getByRole('option')).toHaveCount(35);
			// Minimum not below Maximum, and End before Start
			await view.getByRole('textbox', { name: 'Minimum' }).fill('12');
			await view.getByRole('group', { name: 'End', exact: true }).getByRole('button', { name: 'Date presets' }).click();
			await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: 'Subtract 1 year' }).click();
			await view.getByRole('button', { name: 'Calculate' }).click();
			await expect(view.getByRole('alert')).toHaveCount(2);
			await shot(page, 'graph-errors');
			await view.getByRole('textbox', { name: 'Minimum' }).fill('');
			await view.getByRole('group', { name: 'End', exact: true }).getByRole('button', { name: 'Date presets' }).click();
			await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: 'Default' }).click();
			// 12P's Sun distance from a year before its perihelion, with its line and the now line
			await view.getByRole('combobox', { name: 'Comet' }).selectOption({ label: '12P/Pons-Brooks' });
			await view.getByRole('radio', { name: 'Sun distance' }).check();
			for (const preset of ['Perihelion Date', 'Subtract 1 year']) {
				await view.getByRole('group', { name: 'Start', exact: true }).getByRole('button', { name: 'Date presets' }).click();
				await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: preset }).click();
			}
			await view.getByRole('button', { name: 'Calculate' }).click();
			await expect(view.getByRole('img', { name: 'Sun distance (AU), 12P/Pons-Brooks' })).toBeVisible();
			await shot(page, 'graph-result');
		});

		test(`the Update preview and the import result browser, ${theme}, ${size}`, async ({ page }) => {
			await page.route(MPC_URL, route => route.fulfill({ status: 200, headers: { 'access-control-allow-origin': '*' }, body: vector(MERGE.newFile) }));
			await prepare(page);
			await seedSample(page);
			await page.locator('.shell-nav a[href="#/update"]').click();
			const dialog = page.getByRole('dialog', { name: 'Update' });
			await dialog.getByRole('button', { name: 'Download' }).click();
			await expect(dialog.locator('.update-view__counts')).toHaveText('1, 1, 33; 35');
			await shot(page, 'update-preview');
			await dialog.getByRole('button', { name: 'View' }).click();
			const browser = page.getByRole('dialog', { name: /^Database/ });
			await expect(browser.getByRole('listbox', { name: 'Comets' }).getByRole('option')).toHaveCount(1);
			await expect(browser.locator('.live-ephemeris dd')).toHaveCount(8);
			await shot(page, 'update-result-browser');
		});
	}
}
