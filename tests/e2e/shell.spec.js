// The shell: routes, navigation, and views kept between visits (docs/spec/01-product.md, Views).
import { expect, test, VIEWS } from './fixtures.js';

test('an empty hash opens the Database view', async ({ page }) => {
	await page.goto('./');
	await expect(page).toHaveURL(/#\/database$/);
	await expect(page.getByRole('heading', { level: 1, name: 'Database' })).toBeVisible();
	await expect(page.getByRole('link', { name: 'Database' })).toHaveAttribute('aria-current', 'page');
	await expect(page).toHaveTitle('Database - Comets');
});

test('every navigation link opens its view', async ({ page }) => {
	await page.goto('./');
	for (const [key, heading] of VIEWS) {
		await page.locator(`.shell-nav a[href="#/${key}"]`).click();
		await expect(page).toHaveURL(new RegExp(`#/${key}$`));
		await expect(page.getByRole('heading', { level: 1, name: heading })).toBeVisible();
		await expect(page.locator('.shell-view > :not([hidden])')).toHaveCount(1);
	}
});

test('a reload reopens the route, and an unknown route falls back', async ({ page }) => {
	await page.goto('./#/graph');
	await page.reload();
	await expect(page.getByRole('heading', { level: 1, name: 'Graph' })).toBeVisible();
	await page.goto('./#/nowhere');
	await expect(page).toHaveURL(/#\/database$/);
});

test('a view is created once and kept', async ({ page }) => {
	await page.goto('./#/graph');
	await page.goto('./#/database');
	await page.goto('./#/graph');
	await expect(page.locator('[data-view="graph"]')).toHaveCount(1);
});

test('no view makes the page wider than the window', async ({ page }) => {
	for (const [key] of VIEWS) {
		await page.goto(`./#/${key}`);
		await page.locator(`[data-view="${key}"]`).waitFor();
		const { scrollWidth, clientWidth } = await page.evaluate(() => ({
			scrollWidth: document.documentElement.scrollWidth,
			clientWidth: document.documentElement.clientWidth
		}));
		expect(scrollWidth, key).toBeLessThanOrEqual(clientWidth);
	}
});
