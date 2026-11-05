import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeIndexedDb, createFakeWebStorage } from '../../helpers/fakes.js';
import { createIndexedDbStore, createMemoryStore, createWebStorageStore, tolerant } from '../../../app/js/adapters/key-value-stores.js';

/** console.warn silenced for a test, with what it was given */
function silenceWarnings(/** @type {import('node:test').TestContext} */ t) {
	return t.mock.method(console, 'warn', () => {});
}

/**
 * the contract every store keeps while its storage works
 * @param {import('../../../app/js/services/ports.js').KeyValueStore} store
 */
async function assertWorkingStore(store) {
	assert.equal(await store.get('a'), undefined);
	await store.put('a', 'one');
	await store.put('b', '');
	assert.equal(await store.get('a'), 'one');
	assert.equal(await store.get('b'), '');
	await store.put('a', 'two');
	assert.equal(await store.get('a'), 'two');
	await store.remove('a');
	await store.remove('missing');
	assert.equal(await store.get('a'), undefined);
	assert.equal(await store.get('b'), '');
}

test('the memory store', async () => {
	await assertWorkingStore(createMemoryStore());
	assert.equal(await createMemoryStore([['k', 'v']]).get('k'), 'v');
});

test('Web Storage: values under their own keys', async () => {
	const { storage, values } = createFakeWebStorage();
	const store = createWebStorageStore(() => storage);
	await assertWorkingStore(store);
	assert.deepEqual([...values], [['b', '']]);
});

test('Web Storage that throws on every call: nothing fails, the session keeps its writes', async t => {
	const warn = silenceWarnings(t);
	const { storage } = createFakeWebStorage({ failing: true });
	const store = createWebStorageStore(() => storage);
	await assertWorkingStore(store);
	assert.equal(warn.mock.callCount(), 1, 'one warning per store');
});

test('Web Storage that cannot be reached at all', async t => {
	silenceWarnings(t);
	const store = createWebStorageStore(() => { throw new DOMException('The operation is insecure.', 'SecurityError'); });
	await assertWorkingStore(store);
});

test('Web Storage that is full keeps the refused value in memory until a write gets through', async t => {
	silenceWarnings(t);
	const { storage, values } = createFakeWebStorage({ quota: 10 });
	const store = createWebStorageStore(() => storage);
	await store.put('k', 'x'.repeat(20));
	assert.equal(await store.get('k'), 'x'.repeat(20), 'the session sees its own write');
	assert.equal(values.has('k'), false);
	await store.put('k', 'small');
	assert.equal(values.get('k'), 'small');
	assert.equal(await store.get('k'), 'small');
});

test('a removal storage refuses still removes the value for the session', async t => {
	silenceWarnings(t);
	const fake = createFakeWebStorage();
	const store = createWebStorageStore(() => fake.storage);
	await store.put('k', 'stored');
	fake.state.failing = true;
	await store.remove('k');
	assert.equal(await store.get('k'), undefined);
	fake.state.failing = false;
	assert.equal(fake.values.get('k'), 'stored', 'storage still has it');
	assert.equal(await store.get('k'), undefined, 'the session does not');
	await store.put('k', 'again');
	assert.equal(await store.get('k'), 'again');
});

test('a read that fails gives undefined', async t => {
	silenceWarnings(t);
	const store = tolerant({
		get: () => Promise.reject(new Error('broken')),
		put: async () => {},
		remove: async () => {}
	}, 'test');
	assert.equal(await store.get('k'), undefined);
});

test('IndexedDB: creates its object store once, then reads and writes in transactions', async () => {
	const fake = createFakeIndexedDb();
	const store = createIndexedDbStore({ factory: () => fake.factory, database: 'comets', store: 'kv' });
	await assertWorkingStore(store);
	assert.deepEqual([...fake.stores.keys()], ['kv']);
	assert.equal(fake.state.opens, 1, 'one connection for the session');
	assert.equal(fake.state.upgrades, 1);
	assert.deepEqual([...(fake.stores.get('kv') ?? [])], [['b', '']]);
});

test('IndexedDB: a value that is not text reads as nothing', async () => {
	const fake = createFakeIndexedDb();
	const store = createIndexedDbStore({ factory: () => fake.factory, database: 'comets', store: 'kv' });
	await store.put('k', 'v');
	fake.stores.get('kv')?.set('k', 42);
	assert.equal(await store.get('k'), undefined);
});

for (const open of /** @type {const} */ (['throw', 'error', 'blocked'])) {
	test(`IndexedDB that does not open (${open}): the session keeps working in memory, without trying again`, async t => {
		const warn = silenceWarnings(t);
		const fake = createFakeIndexedDb({ open });
		const store = createIndexedDbStore({ factory: () => fake.factory, database: 'comets', store: 'kv' });
		await assertWorkingStore(store);
		assert.equal(fake.state.opens, 1);
		assert.equal(warn.mock.callCount(), 1);
	});
}

test('IndexedDB that cannot be reached at all', async t => {
	silenceWarnings(t);
	const store = createIndexedDbStore({ factory: () => { throw new DOMException('denied', 'SecurityError'); }, database: 'comets', store: 'kv' });
	await assertWorkingStore(store);
});

test('IndexedDB that never answers gives up after the timeout', async t => {
	silenceWarnings(t);
	const fake = createFakeIndexedDb({ open: 'hang' });
	const store = createIndexedDbStore({ factory: () => fake.factory, database: 'comets', store: 'kv', timeout: 20 });
	const started = performance.now();
	assert.equal(await store.get('k'), undefined);
	assert.ok(performance.now() - started >= 15);
	await store.put('k', 'v');
	assert.equal(await store.get('k'), 'v');
});

test('IndexedDB whose transactions fail: writes are kept in memory, and storage takes over again once it works', async t => {
	silenceWarnings(t);
	const fake = createFakeIndexedDb({ failing: true });
	const store = createIndexedDbStore({ factory: () => fake.factory, database: 'comets', store: 'kv' });
	await store.put('k', 'v');
	assert.equal(await store.get('k'), 'v');
	assert.equal(fake.stores.get('kv')?.has('k'), false);
	fake.state.failing = false;
	await store.put('k', 'w');
	assert.equal(fake.stores.get('kv')?.get('k'), 'w');
	assert.equal(await store.get('k'), 'w');
});

test('IndexedDB closes its connection when another tab upgrades the database', async () => {
	const fake = createFakeIndexedDb();
	const store = createIndexedDbStore({ factory: () => fake.factory, database: 'comets', store: 'kv' });
	await store.get('k');
	assert.equal(fake.connections.length, 1);
	fake.connections[0].onversionchange();
	assert.equal(fake.state.closed, 1);
});
