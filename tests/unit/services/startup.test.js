import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock, createRecordingStore } from '../../helpers/fakes.js';
import { readVectorText, VECTOR_NOW } from '../../helpers/vectors.js';
import { createDatabaseRepository, DATABASE_KEY } from '../../../app/js/services/database-repository.js';
import { createDatabaseService } from '../../../app/js/services/database.js';
import { createSettingsService, DEFAULT_SETTINGS, SETTINGS_KEY, writeSettings } from '../../../app/js/services/settings.js';
import { createStartupService, updatePrompt } from '../../../app/js/services/startup.js';

const DAY = 86_400_000;
const settingsWith = (/** @type {Partial<import('../../../app/js/services/settings.js').Settings>} */ changes) => ({ ...DEFAULT_SETTINGS, ...changes });

test('no question while automatic update is off, before the first update, or within the interval', () => {
	const last = VECTOR_NOW - 30 * DAY;
	assert.equal(updatePrompt(settingsWith({ automaticUpdate: false, lastUpdateDate: last }), VECTOR_NOW), null);
	assert.equal(updatePrompt(settingsWith({ lastUpdateDate: null }), VECTOR_NOW), null);
	assert.equal(updatePrompt(settingsWith({ lastUpdateDate: VECTOR_NOW - 7 * DAY + 1 }), VECTOR_NOW), null, '6.99… days');
	assert.equal(updatePrompt(settingsWith({ lastUpdateDate: VECTOR_NOW + DAY }), VECTOR_NOW), null, 'a clock set back');
});

test('the question once the whole days since the last update reach the interval', () => {
	assert.deepEqual(updatePrompt(settingsWith({ lastUpdateDate: VECTOR_NOW - 7 * DAY }), VECTOR_NOW), { days: 7, message: 'Last update was 7 days ago. Update now?' });
	assert.deepEqual(updatePrompt(settingsWith({ lastUpdateDate: VECTOR_NOW - 30 * DAY - 1 }), VECTOR_NOW), { days: 30, message: 'Last update was 30 days ago. Update now?' });
	assert.equal(updatePrompt(settingsWith({ updateInterval: 1, lastUpdateDate: VECTOR_NOW - DAY }), VECTOR_NOW)?.days, 1);
	assert.equal(updatePrompt(settingsWith({ updateInterval: 99, lastUpdateDate: VECTOR_NOW - 98 * DAY }), VECTOR_NOW), null);
});

/**
 * start-up over stores holding settings and a database
 * @param {{ settings?: string, database?: string }} stored
 */
async function start(stored) {
	const clock = createFakeClock(VECTOR_NOW);
	const settingsStore = createRecordingStore(stored.settings === undefined ? {} : { [SETTINGS_KEY]: stored.settings });
	const databaseStore = createRecordingStore(stored.database === undefined ? {} : { [DATABASE_KEY]: stored.database });
	const settings = createSettingsService({ store: settingsStore });
	const database = createDatabaseService({ repository: createDatabaseRepository({ store: databaseStore, clock }), clock, settings });
	const result = await createStartupService({ settings, database, clock }).start();
	return { result, settings, database, writes: [...settingsStore.writes, ...databaseStore.writes] };
}

test('the first run: the defaults, an empty database, no question', async () => {
	const { result, settings, writes } = await start({});
	assert.deepEqual(result, { prompt: null, empty: true });
	assert.equal(settings.settings.get(), DEFAULT_SETTINGS);
	assert.deepEqual(writes, []);
});

test('a later run: the stored settings and database, and the question when it is due', async () => {
	const { result, settings, database } = await start({
		settings: writeSettings(settingsWith({ theme: 'dark', lastUpdateDate: VECTOR_NOW - 10 * DAY })),
		database: readVectorText('import/sample.txt')
	});
	assert.deepEqual(result, { prompt: { days: 10, message: 'Last update was 10 days ago. Update now?' }, empty: false });
	assert.equal(settings.settings.get().theme, 'dark');
	assert.equal(database.state.get().comets.length, 34);
});
