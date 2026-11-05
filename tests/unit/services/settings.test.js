import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createRecordingStore } from '../../helpers/fakes.js';
import {
	createSettingsService, DEFAULT_DOWNLOAD_URL, DEFAULT_SETTINGS, patchSettings, readSettings, SETTINGS_KEY,
	SETTINGS_VERSION, validateSettings, writeSettings
} from '../../../app/js/services/settings.js';

const STORED = {
	version: 1,
	theme: 'dark',
	downloadUrl: 'https://example.org/elements.txt',
	automaticUpdate: false,
	updateInterval: 30,
	lastUpdateDate: '2026-10-18T12:00:00.000Z',
	showLongCalculationConfirmation: false,
	showDeleteCometConfirmation: false,
	location: { name: 'Zagreb', latitude: 45.815, longitude: -15.9819 }
};

test('the defaults of spec 09', () => {
	assert.deepEqual(DEFAULT_SETTINGS, {
		theme: 'system',
		downloadUrl: 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt',
		automaticUpdate: true,
		updateInterval: 7,
		lastUpdateDate: null,
		showLongCalculationConfirmation: true,
		showDeleteCometConfirmation: true,
		location: { name: 'Home', latitude: 0, longitude: 0 }
	});
	assert.equal(DEFAULT_DOWNLOAD_URL, DEFAULT_SETTINGS.downloadUrl);
	assert.ok(Object.isFrozen(DEFAULT_SETTINGS) && Object.isFrozen(DEFAULT_SETTINGS.location));
});

test('stored settings are versioned JSON, the last update an ISO 8601 UTC text', () => {
	const settings = readSettings(JSON.stringify(STORED));
	assert.equal(settings.lastUpdateDate, Date.UTC(2026, 9, 18, 12));
	assert.deepEqual(JSON.parse(writeSettings(settings)), STORED);
	assert.equal(SETTINGS_VERSION, 1);
	assert.deepEqual(JSON.parse(writeSettings(DEFAULT_SETTINGS)), { version: 1, ...DEFAULT_SETTINGS, location: { ...DEFAULT_SETTINGS.location } });
	assert.ok(writeSettings(DEFAULT_SETTINGS).startsWith('{"version":1,"theme":"system",'));
});

test('nothing stored, or anything but settings JSON, gives the defaults', () => {
	for (const text of [undefined, '', 'not json', 'null', '42', '[]', '"text"', '{}', '{"version":0}', '{"version":"1"}', '{"version":1.5}']) {
		assert.equal(readSettings(text), DEFAULT_SETTINGS, String(text));
	}
});

test('a stored value its rule refuses is replaced by its default, and the rest are kept', () => {
	const settings = readSettings(JSON.stringify({
		...STORED,
		theme: 'blue',
		updateInterval: 100,
		lastUpdateDate: 'yesterday',
		showDeleteCometConfirmation: 'no',
		location: { name: 7, latitude: 91, longitude: -15.9819 }
	}));
	assert.deepEqual(settings, {
		...readSettings(JSON.stringify(STORED)),
		theme: 'system',
		updateInterval: 7,
		lastUpdateDate: null,
		showDeleteCometConfirmation: true,
		location: { name: 'Home', latitude: 0, longitude: -15.9819 }
	});
	assert.ok(Object.isFrozen(settings) && Object.isFrozen(settings.location));
});

test('the rules at their edges', () => {
	const read = (/** @type {object} */ changes) => readSettings(JSON.stringify({ ...STORED, ...changes }));
	assert.equal(read({ updateInterval: 1 }).updateInterval, 1);
	assert.equal(read({ updateInterval: 99 }).updateInterval, 99);
	assert.equal(read({ updateInterval: 0 }).updateInterval, 7);
	assert.equal(read({ updateInterval: 7.5 }).updateInterval, 7);
	assert.equal(read({ updateInterval: null }).updateInterval, null, 'empty while automatic update is off');
	assert.equal(read({ location: { latitude: -90, longitude: 180 } }).location.latitude, -90);
	assert.equal(read({ location: { latitude: -90, longitude: 180 } }).location.longitude, 180);
	assert.equal(read({ location: { latitude: -90.000001, longitude: 180.000001 } }).location.longitude, 0);
	assert.deepEqual(read({ location: 'Zagreb' }).location, DEFAULT_SETTINGS.location);
	assert.equal(read({ lastUpdateDate: '2026-10-18T12:00:00Z' }).lastUpdateDate, Date.UTC(2026, 9, 18, 12));
	assert.equal(read({ lastUpdateDate: '2026-10-18T12:00:00' }).lastUpdateDate, null, 'not UTC');
	assert.equal(read({ lastUpdateDate: '2026-13-45T12:00:00Z' }).lastUpdateDate, null);
	assert.equal(read({ lastUpdateDate: 1760788800000 }).lastUpdateDate, null, 'a number is not the stored form');
});

test('an empty update interval with automatic update on is invalid, stored or changed', () => {
	assert.equal(readSettings(JSON.stringify({ ...STORED, automaticUpdate: true, updateInterval: null })).updateInterval, 7);
	const result = validateSettings({ ...DEFAULT_SETTINGS, updateInterval: null });
	assert.deepEqual(result, { ok: false, errors: [{ field: 'updateInterval', message: 'Please enter days interval for automatic update' }] });
	assert.equal(validateSettings({ ...DEFAULT_SETTINGS, automaticUpdate: false, updateInterval: null }).ok, true);
});

test('older stored versions are migrated in turn; a newer one is read where it can be', () => {
	/** @type {Array<(stored: Record<string, unknown>) => Record<string, unknown>>} */
	const migrations = [
		stored => ({ ...stored, theme: stored.colours }),
		stored => ({ ...stored, downloadUrl: `${stored.downloadUrl}?v3` })
	];
	const v1 = JSON.stringify({ version: 1, colours: 'dark', downloadUrl: 'https://example.org/a.txt' });
	assert.deepEqual(readSettings(v1, migrations), { ...DEFAULT_SETTINGS, theme: 'dark', downloadUrl: 'https://example.org/a.txt?v3' });
	const v2 = JSON.stringify({ version: 2, theme: 'light', downloadUrl: 'https://example.org/a.txt' });
	assert.deepEqual(readSettings(v2, migrations), { ...DEFAULT_SETTINGS, theme: 'light', downloadUrl: 'https://example.org/a.txt?v3' });
	const v9 = JSON.stringify({ version: 9, theme: 'light', somethingNew: true });
	assert.deepEqual(readSettings(v9, migrations), { ...DEFAULT_SETTINGS, theme: 'light' });
	assert.equal(readSettings(v1, [() => /** @type {any} */ (null)]), DEFAULT_SETTINGS, 'a migration that gives no record');
});

test('a change to an unknown setting or to a value its rule refuses is a programmer error', () => {
	assert.throws(() => patchSettings(DEFAULT_SETTINGS, /** @type {any} */ ({ colour: 'red' })), /unknown setting colour/);
	assert.throws(() => patchSettings(DEFAULT_SETTINGS, /** @type {any} */ ({ theme: 'blue' })), /theme cannot be blue/);
	assert.throws(() => patchSettings(DEFAULT_SETTINGS, { updateInterval: 100 }), /updateInterval/);
	assert.throws(() => patchSettings(DEFAULT_SETTINGS, { location: { latitude: 90.5 } }), /location.latitude/);
	assert.throws(() => patchSettings(DEFAULT_SETTINGS, /** @type {any} */ ({ location: { altitude: 3 } })), /location.altitude/);
	const patched = patchSettings(DEFAULT_SETTINGS, { location: { latitude: -33.87 } });
	assert.deepEqual(patched.location, { name: 'Home', latitude: -33.87, longitude: 0 });
	assert.ok(Object.isFrozen(patched) && Object.isFrozen(patched.location));
});

test('the service: the defaults until load, then the stored settings', async () => {
	const store = createRecordingStore({ [SETTINGS_KEY]: JSON.stringify(STORED) });
	const service = createSettingsService({ store });
	assert.equal(service.settings.get(), DEFAULT_SETTINGS);
	await service.load();
	assert.equal(service.settings.get().theme, 'dark');
	assert.deepEqual(store.writes, [], 'loading writes nothing');
});

test('the service with nothing stored', async () => {
	const service = createSettingsService({ store: createRecordingStore() });
	await service.load();
	assert.equal(service.settings.get(), DEFAULT_SETTINGS);
});

test('a valid change applies at once, notifies and is saved', async () => {
	const store = createRecordingStore();
	const service = createSettingsService({ store });
	await service.load();
	/** @type {string[]} */
	const heard = [];
	service.settings.subscribe(settings => heard.push(settings.theme));
	const saved = service.update({ theme: 'dark' });
	assert.equal(service.settings.get().theme, 'dark', 'before the write completes');
	assert.deepEqual(await saved, { ok: true, value: service.settings.get() });
	assert.deepEqual(heard, ['dark']);
	assert.deepEqual(store.writes, [`put ${SETTINGS_KEY}`]);
	assert.equal(readSettings(store.values.get(SETTINGS_KEY)).theme, 'dark');
});

test('an invalid change changes and saves nothing', async () => {
	const store = createRecordingStore();
	const service = createSettingsService({ store });
	await service.load();
	const result = await service.update({ updateInterval: null });
	assert.equal(result.ok, false);
	assert.equal(service.settings.get(), DEFAULT_SETTINGS);
	assert.deepEqual(store.writes, []);
	assert.equal((await service.update({ automaticUpdate: false, updateInterval: null })).ok, true);
	assert.equal(service.settings.get().updateInterval, null);
});

test('a change to the same values neither notifies nor writes', async () => {
	const store = createRecordingStore();
	const service = createSettingsService({ store });
	let calls = 0;
	service.settings.subscribe(() => calls++);
	const result = await service.update({ theme: 'system', location: { latitude: 0 } });
	assert.equal(result.ok, true);
	assert.equal(calls, 0);
	assert.deepEqual(store.writes, []);
});

test('changes are written in the order they were made', async () => {
	/** @type {Array<() => void>} */
	const pending = [];
	/** @type {string[]} */
	const written = [];
	const store = {
		get: async () => undefined,
		put: (/** @type {string} */ _key, /** @type {string} */ value) => new Promise(resolve => pending.push(() => { written.push(JSON.parse(value).theme); resolve(undefined); })),
		remove: async () => {}
	};
	const service = createSettingsService({ store });
	const first = service.update({ theme: 'dark' });
	const second = service.update({ theme: 'light' });
	await new Promise(resolve => setTimeout(resolve, 0));
	assert.equal(pending.length, 1, 'the second waits for the first');
	pending.shift()?.();
	await first;
	await new Promise(resolve => setTimeout(resolve, 0));
	pending.shift()?.();
	await second;
	assert.deepEqual(written, ['dark', 'light']);
});

test('reset: the defaults again, with nothing stored', async () => {
	const store = createRecordingStore({ [SETTINGS_KEY]: JSON.stringify(STORED) });
	const service = createSettingsService({ store });
	await service.load();
	await service.reset();
	assert.equal(service.settings.get(), DEFAULT_SETTINGS);
	assert.equal(store.values.has(SETTINGS_KEY), false);
	await service.load();
	assert.equal(service.settings.get(), DEFAULT_SETTINGS);
});

test('a write that fails does not stop the later ones', async () => {
	/** @type {string[]} */
	const written = [];
	let calls = 0;
	const store = {
		get: async () => undefined,
		async put(/** @type {string} */ _key, /** @type {string} */ value) {
			calls += 1;
			if (calls === 1) throw new Error('broken');
			written.push(JSON.parse(value).theme);
		},
		remove: async () => {}
	};
	const service = createSettingsService({ store });
	await assert.rejects(service.update({ theme: 'dark' }), /broken/);
	assert.equal((await service.update({ theme: 'light' })).ok, true);
	assert.deepEqual(written, ['light']);
});
