import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock, createRecordingStore } from '../../helpers/fakes.js';
import { readVector, readVectorText, VECTOR_NOW } from '../../helpers/vectors.js';
import { importElements } from '../../../app/js/core/formats/import.js';
import { createDatabaseRepository, DATABASE_KEY } from '../../../app/js/services/database-repository.js';
import { createDatabaseService, IMPORT_COMPLETE, ZERO_IMPORTED } from '../../../app/js/services/database.js';
import { createSettingsService, SETTINGS_KEY } from '../../../app/js/services/settings.js';

const merge = readVector('import-merge.json');
const SAMPLE = readVectorText(merge.oldFile);
const NEW = readVectorText(merge.newFile);
const HOME_PLANET_HEADER = `${readVectorText('import/Soft04Cmt.txt').split('\n')[0]}\n`;

/** the services over recording stores, with the sample collection stored */
async function setup({ stored = SAMPLE } = {}) {
	const clock = createFakeClock(VECTOR_NOW);
	const store = createRecordingStore();
	const repository = createDatabaseRepository({ store, clock });
	if (stored !== null) await repository.save(importElements([], 'MPC', stored).comets);
	store.writes.length = 0;
	const settingsStore = createRecordingStore();
	const settings = createSettingsService({ store: settingsStore });
	await settings.load();
	const database = createDatabaseService({ repository, clock, settings });
	await database.load();
	return { clock, store, repository, settings, settingsStore, database };
}

const names = (/** @type {ReadonlyArray<{ full: string }>} */ comets) => comets.map(c => c.full);

test('empty until loaded, then the stored database', async () => {
	const clock = createFakeClock(VECTOR_NOW);
	const store = createRecordingStore();
	const repository = createDatabaseRepository({ store, clock });
	await repository.save(importElements([], 'MPC', SAMPLE).comets);
	const database = createDatabaseService({ repository, clock, settings: { update: async () => { throw new Error('not expected'); } } });
	assert.deepEqual(database.state.get(), { comets: [], change: 'load' });
	await database.load();
	assert.equal(database.state.get().comets.length, 34);
	assert.equal(database.state.get().change, 'load');
	assert.deepEqual(store.writes, ['put database'], 'loading writes nothing');
});

test('the preview merges into the database as it is, and changes nothing', async () => {
	const { database, store } = await setup();
	const before = database.state.get();
	const preview = database.preview(NEW);
	assert.equal(preview.counts, '1, 1, 33; 35');
	assert.equal(database.preview(null).formatLabel, '(no file selected)');
	assert.equal(database.state.get(), before);
	assert.deepEqual(store.writes, []);
});

test('Import of a download: the merged list replaces the database, which is stored, and the last update is now', async () => {
	const { database, store, settings, clock } = await setup();
	clock.advance(1234);
	/** @type {string[]} */
	const heard = [];
	database.state.subscribe(state => heard.push(state.change));
	const outcome = await database.apply(NEW, 'download');
	assert.deepEqual(outcome, { status: 'imported', message: IMPORT_COMPLETE, count: 35 });
	assert.deepEqual(heard, ['import']);
	assert.deepEqual(names(database.state.get().comets), merge.result.map((/** @type {any} */ r) => r.full));
	assert.deepEqual(store.writes, [`put ${DATABASE_KEY}`]);
	assert.equal(settings.settings.get().lastUpdateDate, VECTOR_NOW + 1234);
	assert.equal(IMPORT_COMPLETE, 'Import complete');
});

test('the stored text is the new database', async () => {
	const { database, repository } = await setup();
	await database.apply(NEW, 'file');
	assert.deepEqual(names(await repository.load()), names(database.state.get().comets));
});

test('Import of a local file leaves the last update as it was', async () => {
	const { database, settings, settingsStore } = await setup();
	await database.apply(NEW, 'file');
	assert.equal(settings.settings.get().lastUpdateDate, null);
	assert.deepEqual(settingsStore.writes, []);
	assert.equal(settingsStore.values.has(SETTINGS_KEY), false);
});

test('an empty merged list is an error that changes and stores nothing', async () => {
	const { database, store, settings } = await setup({ stored: null });
	assert.equal(database.preview(HOME_PLANET_HEADER).format, 'HomePlanet');
	const outcome = await database.apply(HOME_PLANET_HEADER, 'download');
	assert.deepEqual(outcome, { status: 'empty', message: ZERO_IMPORTED });
	assert.equal(ZERO_IMPORTED, 'Something wrong happened. Zero comets imported.');
	assert.deepEqual(database.state.get().comets, []);
	assert.deepEqual(store.writes, []);
	assert.equal(settings.settings.get().lastUpdateDate, null);
});

test('a header with no records still imports into a database that has comets: all of them, unchanged', async () => {
	const { database } = await setup();
	const outcome = await database.apply(HOME_PLANET_HEADER, 'file');
	assert.equal(outcome.status, 'imported');
	assert.equal(database.state.get().comets.length, 34);
});

test('a source without a format imports nothing and says nothing', async () => {
	const { database, store } = await setup();
	for (const text of [null, '', readVectorText('import/edge-unknown.txt')]) {
		assert.deepEqual(await database.apply(text, 'download'), { status: 'none' });
	}
	assert.deepEqual(store.writes, []);
});

test('Import merges into the database as it is then, not as it was at the preview', async () => {
	const { database } = await setup();
	const preview = database.preview(NEW);
	assert.equal(preview.counts, '1, 1, 33; 35');
	const w3 = database.state.get().comets.find(c => c.full === 'A/2018 W3');
	assert.ok(w3);
	// A/2018 W3 is not in the new file, so after its deletion the import no longer brings it back
	await database.remove(w3);
	await database.apply(NEW, 'file');
	assert.equal(database.state.get().comets.length, 34);
	assert.equal(database.state.get().comets.some(c => c.full === 'A/2018 W3'), false);
});

test('Delete takes out that record and stores the rest', async () => {
	const { database, store, repository } = await setup();
	const before = database.state.get().comets;
	const comet = before[5];
	await database.remove(comet);
	assert.deepEqual(database.state.get().comets, before.filter(c => c !== comet));
	assert.equal(database.state.get().change, 'delete');
	assert.deepEqual(store.writes, [`put ${DATABASE_KEY}`]);
	assert.equal((await repository.load()).length, 33);
	assert.ok(Object.isFrozen(database.state.get().comets));
});

test('deleting a comet that is not in the database does nothing', async () => {
	const { database, store } = await setup();
	const before = database.state.get();
	const [stranger] = importElements([], 'MPC', SAMPLE).comets;
	await database.remove(stranger);
	assert.equal(database.state.get(), before);
	assert.deepEqual(store.writes, []);
});

test('changes are stored in the order they were made', async () => {
	/** @type {Array<() => void>} */
	const pending = [];
	/** @type {number[]} */
	const saved = [];
	const repository = {
		load: async () => importElements([], 'MPC', SAMPLE).comets,
		save: (/** @type {ReadonlyArray<unknown>} */ comets) => new Promise(resolve => pending.push(() => { saved.push(comets.length); resolve(undefined); })),
		text: async () => undefined,
		clear: async () => {}
	};
	const database = createDatabaseService({ repository, clock: createFakeClock(VECTOR_NOW), settings: { update: async () => /** @type {any} */ ({ ok: true }) } });
	await database.load();
	const [a, b] = database.state.get().comets;
	const first = database.remove(a);
	const second = database.remove(b);
	await new Promise(resolve => setTimeout(resolve, 0));
	assert.equal(pending.length, 1, 'the second waits for the first');
	pending.shift()?.();
	await first;
	await new Promise(resolve => setTimeout(resolve, 0));
	pending.shift()?.();
	await second;
	assert.deepEqual(saved, [33, 32]);
});

test('Clear all data empties the database and removes what is stored', async () => {
	const { database, store } = await setup();
	await database.clear();
	assert.deepEqual(database.state.get(), { comets: [], change: 'clear' });
	assert.deepEqual(store.writes, [`remove ${DATABASE_KEY}`]);
});
