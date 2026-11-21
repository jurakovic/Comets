// The Settings view: every setting saved as it changes, the update interval's rule, the last update
// as the days pass, the location with its hemispheres and Use my location, Export database and
// Clear all data.
import { readFileSync } from 'node:fs';
import { expect, seed, test } from './fixtures.js';

const SAMPLE = readFileSync(new URL('../vectors/data/import/sample.txt', import.meta.url), 'utf8');
const MPC_URL = 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt';
const NOW = new Date('2026-10-18T12:00:00Z');

/** @param {import('@playwright/test').Page} page */
const viewOf = page => page.locator('[data-view="settings"]');

/**
 * the settings as stored
 * @param {import('@playwright/test').Page} page
 * @returns {Promise<any>}
 */
const stored = page => page.evaluate(() => JSON.parse(localStorage.getItem('comets.settings') ?? 'null'));

/**
 * opens the Settings view
 * @param {import('@playwright/test').Page} page
 */
async function openSettings(page) {
	await page.goto('./#/settings');
	await expect(viewOf(page).getByRole('heading', { level: 1, name: 'Settings' })).toBeVisible();
}

/**
 * types into a field that refuses what it cannot hold, from empty
 * @param {import('@playwright/test').Locator} field
 * @param {string} text
 */
async function typeInto(field, text) {
	await field.fill('');
	await field.pressSequentially(text);
}

test('every setting shows its default; a change is saved as it is made and kept after a reload', async ({ page }) => {
	await openSettings(page);
	const view = viewOf(page);
	await expect(view.getByLabel('Theme')).toHaveValue('system');
	await expect(view.getByLabel('Theme').getByRole('option')).toHaveText(['Light', 'Dark', 'System']);
	await expect(view.getByLabel('Elements download URL')).toHaveValue(MPC_URL);
	await expect(view.getByRole('checkbox', { name: 'Automatically update elements after:' })).toBeChecked();
	await expect(view.getByLabel('Update interval in days')).toHaveValue('7');
	await expect(view.locator('.settings-view__last-update')).toHaveText('never');
	await expect(view.getByRole('checkbox', { name: 'Show long calculation confirmation' })).toBeChecked();
	await expect(view.getByRole('checkbox', { name: 'Show delete comet confirmation' })).toBeChecked();
	await expect(view.getByLabel('Name', { exact: true })).toHaveValue('Home');
	await expect(view.getByLabel('Latitude', { exact: true })).toHaveValue('0.000000');
	await expect(view.getByLabel('Latitude hemisphere')).toHaveValue('0');
	await expect(view.getByLabel('Longitude', { exact: true })).toHaveValue('0.000000');
	await expect(view.getByLabel('Longitude hemisphere')).toHaveValue('0');
	expect(await stored(page)).toBeNull();

	await view.getByLabel('Theme').selectOption('Dark');
	await expect(page.locator('html')).toHaveAttribute('data-theme', 'dark');
	await view.getByLabel('Elements download URL').fill('  https://example.org/comets.txt ');
	await view.getByRole('checkbox', { name: 'Show long calculation confirmation' }).uncheck();
	await view.getByRole('checkbox', { name: 'Show delete comet confirmation' }).uncheck();
	await typeInto(view.getByLabel('Update interval in days'), '14');
	await view.getByLabel('Name', { exact: true }).fill('Zagreb ');
	await expect.poll(() => stored(page)).toMatchObject({
		version: 1, theme: 'dark', downloadUrl: 'https://example.org/comets.txt', updateInterval: 14,
		showLongCalculationConfirmation: false, showDeleteCometConfirmation: false, location: { name: 'Zagreb' }
	});

	await page.reload();
	await expect(view.getByLabel('Theme')).toHaveValue('dark');
	await expect(view.getByLabel('Elements download URL')).toHaveValue('https://example.org/comets.txt');
	await expect(view.getByLabel('Update interval in days')).toHaveValue('14');
	await expect(view.getByRole('checkbox', { name: 'Show long calculation confirmation' })).not.toBeChecked();
	await expect(view.getByRole('checkbox', { name: 'Show delete comet confirmation' })).not.toBeChecked();
	await expect(view.getByLabel('Name', { exact: true })).toHaveValue('Zagreb');
	await view.getByLabel('Theme').selectOption('System');
	await expect(page.locator('html')).not.toHaveAttribute('data-theme');
});

test('the interval is required while automatic update is on', async ({ page }) => {
	await openSettings(page);
	const view = viewOf(page);
	const interval = view.getByLabel('Update interval in days');
	const automatic = view.getByRole('checkbox', { name: 'Automatically update elements after:' });
	await interval.fill('');
	await expect(view.getByRole('alert')).toHaveText('Please enter days interval for automatic update');
	await expect(interval).toHaveAttribute('aria-invalid', 'true');
	// nothing is saved while the error stands
	expect(await stored(page)).toBeNull();
	await automatic.uncheck();
	await expect(view.getByRole('alert')).toBeHidden();
	await expect(interval).not.toHaveAttribute('aria-invalid');
	await expect.poll(() => stored(page)).toMatchObject({ automaticUpdate: false, updateInterval: null });
	await automatic.check();
	await expect(view.getByRole('alert')).toBeVisible();
	await expect.poll(() => stored(page)).toMatchObject({ automaticUpdate: false });
	await interval.pressSequentially('0');
	await expect(interval).toHaveValue('');
	await interval.pressSequentially('3');
	await expect(view.getByRole('alert')).toBeHidden();
	await expect.poll(() => stored(page)).toMatchObject({ automaticUpdate: true, updateInterval: 3 });
	await interval.press('ArrowUp');
	await expect(interval).toHaveValue('4');
	await expect.poll(() => stored(page)).toMatchObject({ updateInterval: 4 });
});

test('Restore default puts back the MPC URL', async ({ page }) => {
	await seed(page, { settings: { downloadUrl: 'https://example.org/comets.txt' } });
	await openSettings(page);
	const url = viewOf(page).getByLabel('Elements download URL');
	await expect(url).toHaveValue('https://example.org/comets.txt');
	await viewOf(page).getByRole('button', { name: 'Restore default' }).click();
	await expect(url).toHaveValue(MPC_URL);
	await expect.poll(() => stored(page)).toMatchObject({ downloadUrl: MPC_URL });
});

test('the last update, as how long ago it was', async ({ page }) => {
	await page.clock.setFixedTime(NOW);
	await seed(page, { settings: { automaticUpdate: false, lastUpdateDate: '2026-10-15T08:00:00.000Z' } });
	await openSettings(page);
	await expect(viewOf(page).locator('.settings-view__last-update')).toHaveText('3 days ago');
});

test('the last update moves on while the view stays open, as the navigation\'s does', async ({ page }) => {
	await page.clock.install({ time: NOW });
	// 23 h 30 min before now
	await seed(page, { settings: { automaticUpdate: false, lastUpdateDate: '2026-10-17T12:30:00.000Z' } });
	await openSettings(page);
	// from here only the test moves the clock
	await page.clock.pauseAt(new Date('2026-10-18T12:10:00Z'));
	const lastUpdate = viewOf(page).locator('.settings-view__last-update');
	const nav = page.locator('.shell-nav a[href="#/update"]');
	await expect(lastUpdate).toHaveText('today');
	await expect(nav).toContainText('today');
	// counted again every hour
	await page.clock.runFor(60 * 60_000);
	await expect(lastUpdate).toHaveText('yesterday');
	await expect(nav).toContainText('yesterday');
	// and when the page shows again, though no timer ran
	await page.clock.setSystemTime(new Date('2026-10-19T13:00:00Z'));
	await expect(lastUpdate).toHaveText('yesterday');
	await page.evaluate(() => document.dispatchEvent(new Event('visibilitychange')));
	await expect(lastUpdate).toHaveText('2 days ago');
	await expect(nav).toContainText('2 days ago');
});

test('latitude and longitude with their hemispheres; south and west are stored negative', async ({ page }) => {
	await openSettings(page);
	const view = viewOf(page);
	const latitude = view.getByLabel('Latitude', { exact: true });
	await typeInto(latitude, '45.8150001');
	await expect(latitude).toHaveValue('45.815000');
	await typeInto(latitude, '91');
	await expect(latitude).toHaveValue('9');
	await typeInto(latitude, '45.815');
	await view.getByLabel('Latitude hemisphere').selectOption('South');
	await typeInto(view.getByLabel('Longitude', { exact: true }), '15.9819');
	await view.getByLabel('Longitude hemisphere').selectOption('West');
	await expect.poll(() => stored(page)).toMatchObject({ location: { name: 'Home', latitude: -45.815, longitude: -15.9819 } });
	await page.reload();
	await expect(latitude).toHaveValue('45.815000');
	await expect(view.getByLabel('Latitude hemisphere')).toHaveValue('1');
	await expect(view.getByLabel('Longitude', { exact: true })).toHaveValue('15.981900');
	await expect(view.getByLabel('Longitude hemisphere')).toHaveValue('1');
	// an empty field is 0, as on the desktop
	await latitude.fill('');
	await expect.poll(() => stored(page)).toMatchObject({ location: { latitude: 0 } });
});

test.describe('Use my location', () => {
	test.use({ permissions: ['geolocation'], geolocation: { latitude: -33.8688197, longitude: 151.2092955 } });

	test('fills the coordinates from the browser\'s position, to six decimals', async ({ page }) => {
		await openSettings(page);
		const view = viewOf(page);
		await view.getByRole('button', { name: 'Use my location' }).click();
		await expect(view.getByLabel('Latitude', { exact: true })).toHaveValue('33.868820');
		await expect(view.getByLabel('Latitude hemisphere')).toHaveValue('1');
		await expect(view.getByLabel('Longitude', { exact: true })).toHaveValue('151.209296');
		await expect(view.getByLabel('Longitude hemisphere')).toHaveValue('0');
		await expect.poll(() => stored(page)).toMatchObject({ location: { latitude: -33.86882, longitude: 151.209296 } });
	});
});

test('Use my location without the browser\'s position says so', async ({ page, context }) => {
	await context.clearPermissions();
	await openSettings(page);
	await page.evaluate(() => {
		// the browser refuses, as when the user says no
		Object.defineProperty(navigator, 'geolocation', {
			value: { getCurrentPosition: (/** @type {any} */ _, /** @type {any} */ fail) => fail({ code: 1, message: 'User denied Geolocation' }) }
		});
	});
	await viewOf(page).getByRole('button', { name: 'Use my location' }).click();
	await expect(page.locator('#notices').getByRole('alert')).toHaveText(/Your location could not be found\./);
	await expect(viewOf(page).getByLabel('Latitude', { exact: true })).toHaveValue('0.000000');
	expect(await stored(page)).toBeNull();
});

test('Export database downloads the stored MPC text, named by the local time', async ({ page }) => {
	await page.clock.setFixedTime(NOW);
	await page.goto('./#/settings');
	await expect(viewOf(page).getByRole('button', { name: 'Export database' })).toBeDisabled();
	await seed(page, { database: SAMPLE });
	await openSettings(page);
	const exportButton = viewOf(page).getByRole('button', { name: 'Export database' });
	await expect(exportButton).toBeEnabled();
	const [download] = await Promise.all([page.waitForEvent('download'), exportButton.click()]);
	expect(download.suggestedFilename()).toBe('Comets_2026-10-18_120000.txt');
	expect(readFileSync(await download.path(), 'utf8')).toBe(SAMPLE);
});

test.describe('Export database in local time', () => {
	test.use({ timezoneId: 'Europe/Zagreb' });

	test('names the file by the local time', async ({ page }) => {
		await page.clock.setFixedTime(NOW);
		await seed(page, { database: SAMPLE });
		await openSettings(page);
		const [download] = await Promise.all([page.waitForEvent('download'), viewOf(page).getByRole('button', { name: 'Export database' }).click()]);
		expect(download.suggestedFilename()).toBe('Comets_2026-10-18_140000.txt');
	});
});

test('a dialog closed over the view leaves it following the settings only while it is shown', async ({ page }) => {
	await page.route(MPC_URL, route => route.fulfill({ status: 200, headers: { 'access-control-allow-origin': '*' }, body: SAMPLE }));
	await openSettings(page);
	const lastUpdate = viewOf(page).locator('.settings-view__last-update');
	await expect(lastUpdate).toHaveText('never');
	const dialog = page.getByRole('dialog', { name: 'Update' });
	await page.locator('.shell-nav a[href="#/update"]').click();
	await dialog.getByRole('button', { name: 'Close' }).click();
	await expect(dialog).toBeHidden();
	await page.locator('.shell-nav a[href="#/database"]').click();
	await expect(viewOf(page)).toBeHidden();
	// a download's import sets the last update while the view is hidden
	await page.locator('.shell-nav a[href="#/update"]').click();
	await dialog.getByRole('button', { name: 'Download' }).click();
	await dialog.getByRole('button', { name: 'Import' }).click();
	await expect(page.locator('.shell-nav a[href="#/update"]')).toContainText('today');
	await expect(lastUpdate).toHaveText('never');
	await page.locator('.shell-nav a[href="#/settings"]').click();
	await expect(lastUpdate).toHaveText('today');
});

test('Clear all data asks first, then empties the database and resets the settings', async ({ page }) => {
	await seed(page, { settings: { theme: 'dark', location: { name: 'Zagreb', latitude: 45.815, longitude: 15.9819 } }, database: SAMPLE });
	await openSettings(page);
	const view = viewOf(page);
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 34');
	const clear = view.getByRole('button', { name: 'Clear all data' });
	await clear.click();
	const question = page.getByRole('alertdialog');
	await expect(question).toHaveText(/Do you really want to delete the database and reset all settings\?/);
	await question.getByRole('button', { name: 'No' }).click();
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 34');
	await expect(view.getByLabel('Name', { exact: true })).toHaveValue('Zagreb');

	await clear.click();
	await page.getByRole('alertdialog').getByRole('button', { name: 'Yes' }).click();
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 0');
	await expect(view.getByLabel('Name', { exact: true })).toHaveValue('Home');
	await expect(view.getByLabel('Latitude', { exact: true })).toHaveValue('0.000000');
	await expect(view.getByLabel('Theme')).toHaveValue('system');
	await expect(page.locator('html')).not.toHaveAttribute('data-theme');
	await expect(view.getByRole('button', { name: 'Export database' })).toBeDisabled();
	expect(await stored(page)).toBeNull();
	await page.reload();
	await page.goto('./#/database');
	await expect(page.locator('[data-view="database"]').getByText('The database is empty.')).toBeVisible();
});
