// The storage adapters over the browser's real IndexedDB and localStorage
// (docs/spec/09-settings-and-persistence.md, Storage layout).
import { expect, test } from './fixtures.js';

test('IndexedDB and localStorage keep their values across a reload', async ({ page }) => {
	await page.goto('./');
	const big = 'x'.repeat(1_000_000);
	await page.evaluate(async big => {
		const stores = await import(new URL('js/adapters/key-value-stores.js', location.href).href);
		const db = stores.createIndexedDbStore({ factory: () => indexedDB, database: 'comets-test', store: 'kv' });
		const web = stores.createWebStorageStore(() => localStorage);
		await db.put('text', 'stored');
		await db.put('big', big);
		await db.put('gone', 'soon');
		await db.remove('gone');
		await web.put('comets.test', 'kept');
	}, big);
	await page.reload();
	const read = await page.evaluate(async () => {
		const stores = await import(new URL('js/adapters/key-value-stores.js', location.href).href);
		const db = stores.createIndexedDbStore({ factory: () => indexedDB, database: 'comets-test', store: 'kv' });
		const web = stores.createWebStorageStore(() => localStorage);
		return { text: await db.get('text'), big: (await db.get('big'))?.length, gone: await db.get('gone') ?? null, web: await web.get('comets.test') };
	});
	expect(read).toEqual({ text: 'stored', big: big.length, gone: null, web: 'kept' });
});
