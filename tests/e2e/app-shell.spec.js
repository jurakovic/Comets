// The shell around the views: the status area, the theme from settings, the start-up question,
// dialogs, notifications and Help (docs/spec/01-product.md, Shell, Startup, Messages, Help).
import { readFileSync } from 'node:fs';
import { expect, seed, test } from './fixtures.js';

const SAMPLE = readFileSync(new URL('../../docs/spec/vectors/import/sample.txt', import.meta.url), 'utf8');
const NOW = new Date('2026-10-18T12:00:00Z');
const MPC_URL = 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt';

test('the status area counts the stored comets, after a reload too, and opens the Database view', async ({ page }) => {
	await page.goto('./#/help');
	const count = page.locator('.shell-status__count');
	await expect(count).toHaveText('Comets: 0');
	await seed(page, { database: SAMPLE });
	await expect(count).toHaveText('Comets: 34');
	await page.reload();
	await expect(count).toHaveText('Comets: 34');
	await page.goto('./#/help');
	await count.click();
	await expect(page).toHaveURL(/#\/database$/);
});

test('the stored theme applies at start-up', async ({ page }) => {
	await seed(page, { settings: { theme: 'dark' } });
	await expect(page.locator('html')).toHaveAttribute('data-theme', 'dark');
	await seed(page, { settings: { theme: 'light' } });
	await expect(page.locator('html')).toHaveAttribute('data-theme', 'light');
	await seed(page, { settings: { theme: 'system' } });
	await expect(page.locator('html')).not.toHaveAttribute('data-theme');
});

test('no question on the first run; Update says it never happened', async ({ page }) => {
	await page.goto('./');
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 0');
	await expect(page.getByRole('alertdialog')).toHaveCount(0);
	await expect(page.locator('.shell-nav a[href="#/update"]')).toContainText('never');
});

test.describe('the update question at start-up', () => {
	test.beforeEach(async ({ page }) => {
		await page.clock.setFixedTime(NOW);
		await seed(page, { settings: { lastUpdateDate: '2026-10-08T12:00:00.000Z' } });
	});

	test('Yes opens Update in automatic mode', async ({ page }) => {
		const dialog = page.getByRole('alertdialog', { name: 'Last update was 10 days ago. Update now?' });
		await expect(dialog).toBeVisible();
		await expect(page.locator('.shell-nav a[href="#/update"]')).toContainText('10 days ago');
		await expect(dialog.getByRole('button', { name: 'Yes' })).toBeFocused();
		await page.route(MPC_URL, route => route.fulfill({ status: 200, headers: { 'access-control-allow-origin': '*' }, body: SAMPLE }));
		await dialog.getByRole('button', { name: 'Yes' }).click();
		await expect(dialog).toHaveCount(0);
		await expect(page).toHaveURL(/#\/update\?automatic$/);
		const update = page.getByRole('dialog', { name: 'Update' });
		await expect(update.locator('.update-view__counts')).toHaveText('34, 0, 0; 34');
		await expect(update.getByLabel('Choose file')).toBeDisabled();
	});

	test('No leaves the view as it was', async ({ page }) => {
		const dialog = page.getByRole('alertdialog');
		await dialog.getByRole('button', { name: 'No' }).click();
		await expect(dialog).toHaveCount(0);
		await expect(page).toHaveURL(/#\/database$/);
	});

	test('Esc answers No', async ({ page }) => {
		const dialog = page.getByRole('alertdialog');
		await expect(dialog).toBeVisible();
		await page.keyboard.press('Escape');
		await expect(dialog).toHaveCount(0);
		await expect(page).toHaveURL(/#\/database$/);
	});
});

test('Help lists the app-wide keys and About', async ({ page }) => {
	await page.goto('./#/help');
	const view = page.locator('[data-view="help"]');
	const rows = view.locator('.help-view__controls tbody tr');
	await expect(rows).toHaveText([
		/Ctrl\/⌘\+S\s*Save the current result: ephemeris text, graph image, orbit viewer image, or element file/,
		/Esc\s*Close the open dialog/
	]);
	await expect(view.getByText(/^Comets \S+$/)).toBeVisible();
	await expect(view.getByText('Copyright © 2026 jurakovic')).toBeVisible();
	await expect(view.getByRole('link', { name: 'https://github.com/jurakovic/Comets' })).toHaveAttribute('href', 'https://github.com/jurakovic/Comets');
	await expect(view.getByRole('link', { name: 'Comet for Windows' })).toBeVisible();
	await expect(view.getByRole('link', { name: 'Astronomical Algorithms' })).toBeVisible();
	await expect(view.getByText('GPL-3.0')).toBeVisible();
});

test.describe('errors nothing else caught', () => {
	test.use({ expectedErrors: /boom|rejected/ });

	test('are shown as notifications that stay until dismissed; a cancellation is not', async ({ page }) => {
		await page.goto('./');
		await expect(page.locator('.shell-status__count')).toHaveText('Comets: 0');
		await page.evaluate(() => {
			setTimeout(() => { throw new Error('boom'); });
			Promise.reject(new DOMException('The job was cancelled', 'AbortError'));
			Promise.reject(new Error('rejected'));
		});
		const alerts = page.getByRole('alert');
		await expect(alerts).toHaveCount(2);
		await expect(alerts.filter({ hasText: 'Something went wrong: boom' })).toHaveCount(1);
		await expect(alerts.filter({ hasText: 'Something went wrong: rejected' })).toHaveCount(1);
		await alerts.first().getByRole('button', { name: 'Dismiss' }).click();
		await expect(alerts).toHaveCount(1);
	});

	test('a notice shows above an open dialog, and can be dismissed once the dialog is answered', async ({ page }) => {
		await page.clock.setFixedTime(NOW);
		await seed(page, { settings: { lastUpdateDate: '2026-10-08T12:00:00.000Z' } });
		const dialog = page.getByRole('alertdialog');
		await expect(dialog).toBeVisible();
		await page.evaluate(() => { Promise.reject(new Error('rejected')); });
		const notice = page.locator('.shell-notice--error');
		await expect(notice).toHaveText(/Something went wrong: rejected/);
		await expect(notice).toBeVisible();
		await dialog.getByRole('button', { name: 'No' }).click();
		await expect(dialog).toHaveCount(0);
		await notice.getByRole('button', { name: 'Dismiss' }).click();
		await expect(notice).toHaveCount(0);
	});
});

test('the status area shows a running job\'s progress, and Cancel cancels it', async ({ page }) => {
	await page.goto('./#/help');
	// the status component over a jobs service whose runner the test drives
	await page.evaluate(async () => {
		const base = new URL('js/', location.href).href;
		const { createJobsService } = await import(`${base}services/jobs.js`);
		const { createStatus } = await import(`${base}ui/shell/status.js`);
		const { createStore } = await import(`${base}lib/store.js`);
		/** @type {any} */
		const probe = window;
		const runner = {
			run: (/** @type {string} */ _name, /** @type {unknown} */ _input, /** @type {any} */ { signal, onProgress }) => new Promise((_resolve, reject) => {
				probe.report = onProgress;
				signal.addEventListener('abort', () => reject(signal.reason));
			})
		};
		const jobs = createJobsService({ runner });
		const { element } = createStatus({ selection: createStore({ comets: [], total: 0 }), jobs });
		element.id = 'probe-status';
		document.body.append(element);
		probe.outcome = jobs.run('ephemeris', {}).catch((/** @type {Error} */ error) => error.name);
	});
	const job = page.locator('#probe-status .shell-status__job');
	const progress = job.locator('progress');
	await expect(job).toBeVisible();
	await expect(progress).not.toHaveAttribute('value');
	await page.evaluate(() => /** @type {any} */ (window).report({ done: 3, total: 10 }));
	await expect(progress).toHaveJSProperty('max', 10);
	await expect(progress).toHaveJSProperty('value', 3);
	await job.getByRole('button', { name: 'Cancel' }).click();
	await expect(job).toBeHidden();
	expect(await page.evaluate(() => /** @type {any} */ (window).outcome)).toBe('AbortError');
});
