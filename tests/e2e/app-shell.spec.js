// The shell around the views: the status area, the theme from settings, the start-up question,
// dialogs, notifications and Help.
import { readFileSync } from 'node:fs';
import { expect, holdDatabase, landDatabase, seed, test } from './fixtures.js';

const SAMPLE = readFileSync(new URL('../vectors/data/import/sample.txt', import.meta.url), 'utf8');
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

test('the stored theme applies before the first paint, and stays while the app starts', async ({ page }) => {
	await page.emulateMedia({ colorScheme: 'dark' });
	await seed(page, { settings: { theme: 'light' } });
	// every change of data-theme from the start of the next load
	await page.addInitScript(() => {
		const themes = /** @type {Array<string | null>} */ ([]);
		Object.assign(window, { themes });
		new MutationObserver(() => {
			const theme = document.documentElement.getAttribute('data-theme');
			if (themes.at(-1) !== theme) themes.push(theme);
		}).observe(document, { subtree: true, attributeFilter: ['data-theme'] });
	});
	/** @type {() => void} */
	let release = () => {};
	const held = new Promise(resolve => { release = () => resolve(undefined); });
	await page.route('**/js/main.js', async route => {
		await held;
		await route.continue();
	});
	await page.reload({ waitUntil: 'commit' });
	// the app's modules have not run yet
	await expect(page.locator('html')).toHaveAttribute('data-theme', 'light');
	release();
	await expect(page.locator('.shell-status__count')).toHaveText('Comets: 0');
	expect(await page.evaluate(() => /** @type {{ themes: unknown }} */ (/** @type {unknown} */ (window)).themes)).toEqual(['light']);
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

	test('after a reload on the Update dialog, the question is above it and answered first', async ({ page }) => {
		await page.goto('./#/update');
		await page.reload();
		const dialog = page.getByRole('alertdialog', { name: 'Last update was 10 days ago. Update now?' });
		await expect(dialog.getByRole('button', { name: 'Yes' })).toBeFocused();
		await dialog.getByRole('button', { name: 'No' }).click();
		await expect(dialog).toHaveCount(0);
		const update = page.getByRole('dialog', { name: 'Update' });
		await expect(update).toBeVisible();
		await page.keyboard.press('Escape');
		await expect(update).toBeHidden();
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

	test('a notice shown before a dialog opens shows above it', async ({ page }) => {
		await page.goto('./');
		await expect(page.locator('.shell-status__count')).toHaveText('Comets: 0');
		await page.evaluate(() => { Promise.reject(new Error('rejected')); });
		const notice = page.locator('.shell-notice--error');
		await expect(notice).toBeVisible();
		const background = await notice.evaluate(element => getComputedStyle(element).backgroundColor);
		await page.locator('.shell-nav a[href="#/update"]').click();
		await expect(page.getByRole('dialog', { name: 'Update' })).toBeVisible();
		// under the dialog's backdrop, the notice would show dimmed
		await expect.poll(() => shownColour(page, notice)).toBe(background);
	});
});

test.describe('a view that fails to load at start-up', () => {
	test.use({ expectedErrors: /Failed to load resource|dynamically imported module/ });

	test('is reported once, and the question is still asked', async ({ page }) => {
		await page.clock.setFixedTime(NOW);
		await seed(page, { settings: { lastUpdateDate: '2026-10-08T12:00:00.000Z' } });
		await page.route('**/js/ui/views/database/database-view.js', route => route.abort());
		// the view fails while start-up still waits for the stored database
		await holdDatabase(page);
		await page.reload();
		await expect(page.getByRole('alert')).toContainText(/Something went wrong: .*database-view\.js/);
		await landDatabase(page);
		await expect(page.getByRole('alertdialog')).toBeVisible();
		await expect(page.getByRole('alert')).toHaveCount(1);
	});
});

test('a fifth notice pushes out the oldest information notice, or the oldest error when all four are errors', async ({ page }) => {
	await page.goto('./');
	const shown = await page.evaluate(async () => {
		const { createNotifications } = await import(new URL('js/ui/shell/notifications.js', location.href).href);
		/** @param {(notify: import('../../app/js/ui/shell/notifications.js').Notify) => void} send */
		const after = send => {
			const region = document.createElement('div');
			region.popover = 'manual';
			document.body.append(region);
			send(createNotifications(region));
			const texts = [...region.querySelectorAll('.shell-notice__message')].map(message => message.textContent);
			region.remove();
			return texts;
		};
		return [
			after(notify => {
				notify('error 1', { kind: 'error' });
				for (const n of [1, 2, 3, 4]) notify(`info ${n}`);
			}),
			after(notify => {
				for (const n of [1, 2, 3, 4, 5]) notify(`error ${n}`, { kind: 'error' });
			}),
			after(notify => {
				for (const n of [1, 2, 3, 4]) notify(`error ${n}`, { kind: 'error' });
				notify('info 1');
			})
		];
	});
	expect(shown).toEqual([
		['error 1', 'info 2', 'info 3', 'info 4'],
		['error 2', 'error 3', 'error 4', 'error 5'],
		['error 2', 'error 3', 'error 4', 'info 1']
	]);
});

/**
 * the colour a point of the notice's padding shows on screen, as `rgb(r, g, b)`
 * @param {import('@playwright/test').Page} page
 * @param {import('@playwright/test').Locator} notice
 */
async function shownColour(page, notice) {
	const box = await notice.boundingBox();
	if (!box) return null;
	const png = await page.screenshot({ clip: { x: box.x + 6, y: box.y + box.height / 2, width: 1, height: 1 } });
	return page.evaluate(async data => {
		const image = new Image();
		image.src = `data:image/png;base64,${data}`;
		await image.decode();
		const canvas = new OffscreenCanvas(1, 1);
		const context = /** @type {OffscreenCanvasRenderingContext2D} */ (canvas.getContext('2d'));
		context.drawImage(image, 0, 0);
		const [r, g, b] = context.getImageData(0, 0, 1, 1).data;
		return `rgb(${r}, ${g}, ${b})`;
	}, png.toString('base64'));
}

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
