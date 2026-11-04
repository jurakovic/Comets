// Shared setup for the browser tests: requests that leave the local server are
// refused unless a test answers them, and any script error fails the test.
import { test as base, expect } from '@playwright/test';

export const test = base.extend({
	page: async ({ page }, use) => {
		const errors = [];
		page.on('pageerror', error => errors.push(error.message));
		page.on('console', message => {
			if (message.type() === 'error') errors.push(message.text());
		});
		await page.route(url => url.hostname !== '127.0.0.1', route => route.fulfill({ status: 404, body: '' }));
		await use(page);
		expect(errors, 'errors on the page').toEqual([]);
	}
});

export { expect };

/** the views in the navigation, in order: [route key, heading] */
export const VIEWS = [
	['database', 'Database'],
	['ephemeris', 'Ephemeris'],
	['graph', 'Graph'],
	['orbit', 'Orbit Viewer'],
	['elements', 'Orbital Elements'],
	['update', 'Update'],
	['settings', 'Settings'],
	['help', 'Help']
];
