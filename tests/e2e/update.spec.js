// The Update dialog: the first-run download, the preview counts, a local file, errors, Cancel and
// automatic mode, with the MPC URL answered from the vectors (docs/spec/05-database-and-update.md,
// Update; docs/spec/11-test-vectors.md, Conformance plan level 3).
import { readFileSync } from 'node:fs';
import { expect, seed, test } from './fixtures.js';

const MPC_URL = 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt';
/** @param {string} path a file under docs/spec/vectors */
const vector = path => readFileSync(new URL(`../../docs/spec/vectors/${path}`, import.meta.url), 'utf8');
const FULL = vector('import/full/Soft00Cmt.txt');
const MERGE = JSON.parse(vector('import-merge.json'));
const SAMPLE = vector(MERGE.oldFile);
const NEW = vector(MERGE.newFile);
const CORS = { 'access-control-allow-origin': '*', 'content-type': 'text/plain' };

/** the counts import-merge.json's results make, as the preview writes them */
function mergeCounts() {
	/** @param {string} result */
	const count = result => MERGE.result.filter((/** @type {{ importResult: string }} */ c) => c.importResult === result).length;
	return `${count('New')}, ${count('Update')}, ${count('NoChanges')}; ${MERGE.result.length}`;
}

/**
 * answers the MPC URL with text
 * @param {import('@playwright/test').Page} page
 * @param {string} body
 */
const answerMpc = (page, body) => page.route(MPC_URL, route => route.fulfill({ status: 200, headers: CORS, body }));

/** @param {import('@playwright/test').Page} page */
const dialogOf = page => page.getByRole('dialog', { name: 'Update' });

test('first run: the download is previewed, imported, stored and found again after a reload', async ({ page }) => {
	await answerMpc(page, FULL);
	await page.goto('./');
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 0');
	await page.locator('.shell-nav a[href="#/update"]').click();
	const dialog = dialogOf(page);
	await expect(dialog).toBeVisible();
	await expect(dialog.locator('.update-view__format')).toHaveText('(no file selected)');
	await expect(dialog.locator('.update-view__counts')).toHaveText('-');
	await expect(dialog.getByRole('link', { name: 'Open in browser' })).toHaveAttribute('href', MPC_URL);
	await dialog.getByRole('button', { name: 'Download' }).click();
	await expect(dialog.locator('.update-view__format')).toHaveText('MPC (Soft00Cmt)');
	const counts = dialog.locator('.update-view__counts');
	await expect(counts).toHaveText('963, 0, 0; 963');
	await expect(counts).toHaveAttribute('title', 'New, updates, no changes; total');
	await dialog.getByRole('button', { name: 'Import' }).click();
	await expect(page.getByRole('status').filter({ hasText: 'Import complete' })).toBeVisible();
	await expect(dialog).toBeHidden();
	await expect(page).toHaveURL(/#\/database$/);
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 963');
	await expect(page.locator('.shell-nav a[href="#/update"]')).toContainText('today');
	await page.reload();
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 963');
});

test('the preview counts a download merged into the database, as import-merge.json says', async ({ page }) => {
	await answerMpc(page, NEW);
	await seed(page, { database: SAMPLE });
	await page.goto('./#/update');
	const dialog = dialogOf(page);
	await dialog.getByRole('button', { name: 'Download' }).click();
	await expect(dialog.locator('.update-view__counts')).toHaveText(mergeCounts());
	expect(mergeCounts()).toBe('1, 1, 33; 35');
});

test('a local file, chosen or dropped and decoded by its byte-order mark, is the candidate', async ({ page }) => {
	await seed(page, { database: SAMPLE });
	await page.goto('./#/update');
	const dialog = dialogOf(page);
	await dialog.getByLabel('Choose file').setInputFiles({ name: 'merge-new.txt', mimeType: 'text/plain', buffer: Buffer.from(NEW) });
	await expect(dialog.locator('.update-view__file-name')).toHaveText('merge-new.txt');
	await expect(dialog.locator('.update-view__counts')).toHaveText('1, 1, 33; 35');

	// UTF-16 with its byte-order mark, dropped on the zone
	const utf16 = [0xff, 0xfe, ...Buffer.from(SAMPLE, 'utf16le')];
	await dialog.locator('.update-view__drop').evaluate((zone, bytes) => {
		const transfer = new DataTransfer();
		transfer.items.add(new File([new Uint8Array(bytes)], 'sample-utf16.txt'));
		zone.dispatchEvent(new DragEvent('dragover', { dataTransfer: transfer, bubbles: true, cancelable: true }));
		zone.dispatchEvent(new DragEvent('drop', { dataTransfer: transfer, bubbles: true, cancelable: true }));
	}, utf16);
	await expect(dialog.locator('.update-view__file-name')).toHaveText('sample-utf16.txt');
	await expect(dialog.locator('.update-view__counts')).toHaveText('0, 0, 34; 34');

	// a file has no last update; without Close when finished the dialog stays, its counts afresh
	await dialog.getByLabel('Choose file').setInputFiles({ name: 'merge-new.txt', mimeType: 'text/plain', buffer: Buffer.from(NEW) });
	await dialog.getByRole('checkbox', { name: 'Close when finished' }).uncheck();
	await dialog.getByRole('button', { name: 'Import' }).click();
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 35');
	await expect(dialog).toBeVisible();
	await expect(dialog.locator('.update-view__counts')).toHaveText('0, 0, 35; 35');
	await expect(page.locator('.shell-nav a[href="#/update"]')).toContainText('never');
});

test.describe('a failed download', () => {
	// the browser logs the failed requests the test makes
	test.use({ expectedErrors: /Failed to load resource|net::ERR_FAILED/ });

	test('errors: the HTTP status, an empty body, and the cross-origin hint for another URL', async ({ page }) => {
		await page.route(MPC_URL, route => route.fulfill({ status: 404, headers: CORS, body: 'Not here' }));
		await page.goto('./#/update');
		const dialog = dialogOf(page);
		await dialog.getByRole('button', { name: 'Download' }).click();
		await expect(dialog.getByRole('alert')).toHaveText('Download failed: HTTP 404 Not Found.');
		await expect(dialog.locator('.update-view__counts')).toHaveText('-');

		await page.unroute(MPC_URL);
		await answerMpc(page, '');
		await dialog.getByRole('button', { name: 'Download' }).click();
		await expect(dialog.getByRole('alert')).toHaveText('Download failed: the response is empty.');

		const other = 'https://example.org/elements.txt';
		await page.route(other, route => route.abort('failed'));
		await seed(page, { settings: { downloadUrl: other } });
		await page.goto('./#/update');
		await expect(dialogOf(page).locator('.update-view__url')).toHaveText(other);
		await dialogOf(page).getByRole('button', { name: 'Download' }).click();
		await expect(dialogOf(page).getByRole('alert')).toHaveText(
			'Download failed: network error. The server must allow cross-origin requests (CORS) from this page, or the browser refuses the download.');
	});
});

test('Cancel stops a download that shows its progress', async ({ page }) => {
	await page.route(MPC_URL, () => {});
	await page.goto('./#/update');
	const dialog = dialogOf(page);
	await dialog.getByRole('button', { name: 'Download' }).click();
	await expect(dialog.getByRole('progressbar', { name: 'Download progress' })).toBeVisible();
	await expect(dialog.getByRole('button', { name: 'Download' })).toBeDisabled();
	await dialog.getByRole('button', { name: 'Cancel' }).click();
	await expect(dialog.getByRole('progressbar')).toBeHidden();
	await expect(dialog.getByRole('button', { name: 'Download' })).toBeEnabled();
	await expect(dialog.getByRole('alert')).toBeHidden();
});

test('automatic mode from the start-up question: the download at once, no local file, closed by Import', async ({ page }) => {
	await page.clock.setFixedTime(new Date('2026-10-18T12:00:00Z'));
	await answerMpc(page, NEW);
	await seed(page, { settings: { lastUpdateDate: '2026-10-08T12:00:00.000Z' }, database: SAMPLE });
	await page.getByRole('alertdialog').getByRole('button', { name: 'Yes' }).click();
	const dialog = dialogOf(page);
	await expect(dialog).toBeVisible();
	await expect(dialog.locator('.update-view__counts')).toHaveText('1, 1, 33; 35');
	await expect(dialog.getByLabel('Choose file')).toBeDisabled();
	await expect(dialog.getByRole('checkbox', { name: 'Close when finished' })).toBeHidden();
	await dialog.getByRole('button', { name: 'Import' }).click();
	await expect(dialog).toBeHidden();
	await expect(page).toHaveURL(/#\/database$/);
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 35');
	await expect(page.locator('.shell-nav a[href="#/update"]')).toContainText('today');
});

test('the dialog stays over the view it was opened from; Esc and Close go back to it', async ({ page }) => {
	await page.goto('./#/graph');
	await page.locator('.shell-nav a[href="#/update"]').click();
	const dialog = dialogOf(page);
	await expect(dialog).toBeVisible();
	await expect(page.locator('[data-view="graph"]')).toBeVisible();
	await expect(page.locator('.shell-nav a[href="#/update"]')).toHaveAttribute('aria-current', 'page');
	await expect(page).toHaveTitle('Update - Comets');
	await page.keyboard.press('Escape');
	await expect(dialog).toBeHidden();
	await expect(page).toHaveURL(/#\/graph$/);
	await expect(page).toHaveTitle('Graph - Comets');

	// opened by a reload: the default view goes under it, and Close goes there
	await page.goto('./#/update');
	await page.reload();
	await expect(dialogOf(page)).toBeVisible();
	await expect(page.locator('[data-view="database"]')).toBeVisible();
	await dialogOf(page).getByRole('button', { name: 'Close' }).click();
	await expect(dialogOf(page)).toBeHidden();
	await expect(page).toHaveURL(/#\/database$/);
});

test('View browses the merged list by import result, read-only, over the dialog', async ({ page }) => {
	await answerMpc(page, NEW);
	await seed(page, { database: SAMPLE });
	await page.goto('./#/update');
	const dialog = dialogOf(page);
	await expect(dialog.getByRole('button', { name: 'View' })).toBeDisabled();
	await dialog.getByRole('button', { name: 'Download' }).click();
	await dialog.getByRole('button', { name: 'View' }).click();
	const browser = page.getByRole('dialog', { name: /^Database/ });
	await expect(browser).toBeVisible();
	const choice = browser.getByLabel('Import result');
	await expect(choice).toHaveValue('New');
	await expect(browser.getByRole('option', { name: 'New comets' })).toBeAttached();
	const list = browser.getByRole('listbox', { name: 'Comets' });
	await expect(browser.getByRole('heading')).toHaveText('Database (1/35 comets)');
	await expect(list.getByRole('option')).toHaveText(['21P/Giacobini-Zinner']);
	await expect(browser.getByRole('tabpanel').locator('dd[data-key="full"]')).toHaveText('21P/Giacobini-Zinner');
	await choice.selectOption({ label: 'Updates' });
	await expect(list.getByRole('option')).toHaveText(['2P/Encke']);
	await choice.selectOption({ label: 'No changes' });
	await expect(list.getByRole('option')).toHaveCount(33);
	await choice.selectOption({ label: 'All comets' });
	await expect(browser.getByRole('heading')).toHaveText('Database (35 comets)');
	await expect(list.getByRole('option')).toHaveText(MERGE.result.map((/** @type {{ full: string }} */ c) => c.full));
	await expect(browser.getByRole('button', { name: 'Delete' })).toHaveCount(0);
	await page.keyboard.press('Escape');
	await expect(browser).toHaveCount(0);
	await expect(dialog).toBeVisible();
	await expect(page).toHaveURL(/#\/update$/);
});
