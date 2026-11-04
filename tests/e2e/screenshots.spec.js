// Every view in both themes, at desktop and phone size, written to
// test-results/screenshots/ for looking at (npm run screenshots).
import { test, VIEWS } from './fixtures.js';

const SIZES = { desktop: { width: 1280, height: 800 }, phone: { width: 390, height: 844 } };

for (const theme of /** @type {const} */ (['light', 'dark'])) {
	for (const [size, viewport] of Object.entries(SIZES)) {
		test(`views, ${theme}, ${size}`, async ({ page }) => {
			await page.emulateMedia({ colorScheme: theme });
			await page.setViewportSize(viewport);
			for (const [key] of VIEWS) {
				await page.goto(`./#/${key}`);
				await page.locator(`[data-view="${key}"]`).waitFor();
				await page.screenshot({ path: `test-results/screenshots/${key}-${theme}-${size}.png` });
			}
		});
	}
}
