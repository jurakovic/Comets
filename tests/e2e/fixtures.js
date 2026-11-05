// Shared setup for the browser tests: requests that leave the local server are
// refused unless a test answers them, and any script error fails the test unless
// the test expects it.
import { test as base, expect } from '@playwright/test';

export const test = base.extend({
	// page errors and console errors a test provokes on purpose (one RegExp: test.use reads an
	// array as a value with its options)
	expectedErrors: [/** @type {RegExp | null} */ (null), { option: true }],
	page: async ({ page, expectedErrors }, use) => {
		/** @type {string[]} */
		const errors = [];
		page.on('pageerror', error => errors.push(error.message));
		page.on('console', message => {
			if (message.type() === 'error') errors.push(message.text());
		});
		await page.route(url => url.hostname !== '127.0.0.1', route => route.fulfill({ status: 404, body: '' }));
		await use(page);
		expect(errors.filter(error => !expectedErrors?.test(error)), 'errors on the page').toEqual([]);
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

/**
 * stores settings and a database where the app keeps them (spec 09, Storage layout), then
 * reloads, so the app starts with them
 * @param {import('@playwright/test').Page} page
 * @param {{ settings?: object, database?: string }} stored settings without their version
 */
export async function seed(page, { settings, database }) {
	await page.goto('./');
	await page.evaluate(async ({ settings, database }) => {
		const stores = await import(new URL('js/adapters/key-value-stores.js', location.href).href);
		if (settings) await stores.createWebStorageStore(() => localStorage).put('comets.settings', JSON.stringify({ version: 1, ...settings }));
		if (database !== undefined) await stores.createIndexedDbStore({ factory: () => indexedDB, database: 'comets', store: 'kv' }).put('database', database);
	}, { settings, database });
	await page.reload();
}
