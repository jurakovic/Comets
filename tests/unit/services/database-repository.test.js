import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock, createRecordingStore } from '../../helpers/fakes.js';
import { assertComets } from '../../helpers/formats.js';
import { readVector, readVectorText, toLf, VECTOR_NOW } from '../../helpers/vectors.js';
import { importElements } from '../../../app/js/core/formats/import.js';
import { createDatabaseRepository, DATABASE_KEY } from '../../../app/js/services/database-repository.js';

const sample = importElements([], 'MPC', readVectorText('import/sample.txt')).comets;

test('nothing stored is an empty database', async () => {
	const repository = createDatabaseRepository({ store: createRecordingStore(), clock: createFakeClock(VECTOR_NOW) });
	assert.deepEqual(await repository.load(), []);
	assert.equal(await repository.text(), undefined);
});

test('the database is stored as the MPC export, in one write', async () => {
	const store = createRecordingStore();
	const repository = createDatabaseRepository({ store, clock: createFakeClock(VECTOR_NOW) });
	await repository.save(sample);
	assert.deepEqual(store.writes, [`put ${DATABASE_KEY}`]);
	const text = /** @type {string} */ (await repository.text());
	assert.equal(toLf(text), readVectorText('export/00-MPC.txt'));
	assert.ok(text.endsWith('\r\n'));
});

test('it reads back as an MPC import into an empty database, with the precision MPC text keeps', async () => {
	const store = createRecordingStore();
	const repository = createDatabaseRepository({ store, clock: createFakeClock(VECTOR_NOW) });
	await repository.save(sample);
	assertComets(await repository.load(), readVector('import-expected/roundtrip-00-MPC.txt.json').comets, { label: 'reloaded' });
});

test('the full MPC file stored as it is loads as its import', async () => {
	const text = readVectorText('import/full/Soft00Cmt.txt');
	const repository = createDatabaseRepository({ store: createRecordingStore({ [DATABASE_KEY]: text }), clock: createFakeClock(VECTOR_NOW) });
	const comets = await repository.load();
	assert.equal(comets.length, 963);
	assert.deepEqual(comets.map(c => c.full), importElements([], 'MPC', text).comets.map(c => c.full));
});

test('stored text that is not MPC loads as no comets; an empty database is stored as empty text', async () => {
	const store = createRecordingStore({ [DATABASE_KEY]: 'garbage\r\nmore garbage\r\n' });
	const repository = createDatabaseRepository({ store, clock: createFakeClock(VECTOR_NOW) });
	assert.deepEqual(await repository.load(), []);
	await repository.save([]);
	assert.equal(store.values.get(DATABASE_KEY), '');
	assert.deepEqual(await repository.load(), []);
});

test('clear removes the stored text', async () => {
	const store = createRecordingStore();
	const repository = createDatabaseRepository({ store, clock: createFakeClock(VECTOR_NOW) });
	await repository.save(sample);
	await repository.clear();
	assert.equal(store.values.has(DATABASE_KEY), false);
	assert.deepEqual(await repository.load(), []);
});
