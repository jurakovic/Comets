import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock, createRecordingStore } from '../../helpers/fakes.js';
import { readVector, readVectorText, VECTOR_NOW } from '../../helpers/vectors.js';
import { importElements, previewImport } from '../../../app/js/core/formats/import.js';
import { createDatabaseRepository } from '../../../app/js/services/database-repository.js';
import { createDatabaseService } from '../../../app/js/services/database.js';
import { createSettingsService, DEFAULT_DOWNLOAD_URL } from '../../../app/js/services/settings.js';
import { createUpdateService, CROSS_ORIGIN_HINT, downloadError, EMPTY_DOWNLOAD, UNREADABLE_FILE } from '../../../app/js/services/update.js';

const merge = readVector('import-merge.json');
const SAMPLE = readVectorText(merge.oldFile);
const NEW = readVectorText(merge.newFile);
const FULL = readVectorText('import/full/Soft00Cmt.txt');

/**
 * a TextFetcher whose downloads a test settles by hand
 * @returns {import('../../../app/js/services/ports.js').TextFetcher & { calls: Array<{ url: string, signal?: AbortSignal, onProgress?: Function, resolve: (text: string) => void, reject: (error: unknown) => void }> }}
 */
function createManualFetcher() {
	/** @type {any[]} */
	const calls = [];
	return {
		calls,
		fetchText: (url, { signal, onProgress } = {}) => new Promise((resolve, reject) => {
			calls.push({ url, signal, onProgress, resolve, reject });
			signal?.addEventListener('abort', () => reject(signal.reason));
		})
	};
}

/** a file as the browser hands it over, read by the reader below */
const file = (/** @type {string} */ name, /** @type {string} */ text) => Object.assign(new Blob([text]), { name });
const reader = { readText: async (/** @type {Blob} */ blob) => blob.text() };

async function setup({ stored = SAMPLE, url = DEFAULT_DOWNLOAD_URL } = {}) {
	const clock = createFakeClock(VECTOR_NOW);
	const store = createRecordingStore();
	const repository = createDatabaseRepository({ store, clock });
	if (stored !== null) await repository.save(importElements([], 'MPC', stored).comets);
	const settings = createSettingsService({ store: createRecordingStore() });
	await settings.load();
	if (url !== DEFAULT_DOWNLOAD_URL) await settings.update({ downloadUrl: url });
	const database = createDatabaseService({ repository, clock, settings });
	await database.load();
	const fetcher = createManualFetcher();
	const update = createUpdateService({ fetcher, reader, database, settings });
	return { clock, settings, database, fetcher, update };
}

/** the preview of a text into the sample collection, for services over a stand-in database */
const sampleComets = importElements([], 'MPC', SAMPLE).comets;
const importPreview = (/** @type {string | null} */ text) => previewImport(sampleComets, text);
const fixedSettings = { settings: { get: () => /** @type {any} */ ({ downloadUrl: DEFAULT_DOWNLOAD_URL }), subscribe: () => () => {} } };

/** lets awaited promises settle */
const settle = () => new Promise(resolve => setTimeout(resolve, 0));

test('a fresh dialog: no source, nothing to import', async () => {
	const { update } = await setup();
	update.open();
	const state = update.state.get();
	assert.equal(state.url, DEFAULT_DOWNLOAD_URL);
	assert.equal(state.automatic, false);
	assert.deepEqual(state.download, { status: 'idle' });
	assert.equal(state.file, null);
	assert.equal(state.candidate, null);
	assert.equal(state.preview.formatLabel, '(no file selected)');
	assert.equal(state.preview.counts, '-');
});

test('a download with progress becomes the candidate, previewed against the database', async () => {
	const { update, fetcher } = await setup();
	update.open();
	update.download();
	update.download();
	assert.equal(fetcher.calls.length, 1, 'one download at a time');
	assert.equal(fetcher.calls[0].url, DEFAULT_DOWNLOAD_URL);
	assert.deepEqual(update.state.get().download, { status: 'running', progress: null });
	fetcher.calls[0].onProgress?.({ received: 10, total: 100 });
	assert.deepEqual(update.state.get().download, { status: 'running', progress: { received: 10, total: 100 } });
	fetcher.calls[0].resolve(NEW);
	await settle();
	const state = update.state.get();
	assert.deepEqual(state.download, { status: 'done' });
	assert.equal(state.candidate, 'download');
	assert.equal(state.preview.formatLabel, 'MPC (Soft00Cmt)');
	assert.equal(state.preview.counts, '1, 1, 33; 35');
});

test('Import of the download replaces the database and sets the last update; the counts follow', async () => {
	const { update, fetcher, database, settings, clock } = await setup({ stored: null });
	update.open({ automatic: true });
	assert.equal(fetcher.calls.length, 1, 'automatic mode downloads at once');
	fetcher.calls[0].resolve(FULL);
	await settle();
	assert.equal(update.state.get().preview.counts, '963, 0, 0; 963');
	const outcome = await update.importCandidate();
	assert.deepEqual(outcome, { status: 'imported', message: 'Import complete', count: 963 });
	assert.equal(database.state.get().comets.length, 963);
	assert.equal(settings.settings.get().lastUpdateDate, clock.now());
	assert.equal(update.state.get().preview.counts, '0, 0, 963; 963');
});

test('the source loaded last is the candidate; a file does not set the last update', async () => {
	const { update, fetcher, database, settings } = await setup();
	update.open({ download: true });
	assert.equal(update.state.get().automatic, false);
	await update.loadFile(file('new.txt', NEW));
	assert.equal(update.state.get().candidate, 'file');
	assert.deepEqual(update.state.get().file, { name: 'new.txt', error: null });
	assert.equal(update.state.get().preview.counts, '1, 1, 33; 35');
	fetcher.calls[0].resolve(FULL);
	await settle();
	assert.equal(update.state.get().candidate, 'download');
	assert.equal(update.state.get().preview.counts, '934, 0, 34; 968');
	await update.loadFile(file('new.txt', NEW));
	assert.equal(update.state.get().candidate, 'file');
	const outcome = await update.importCandidate();
	assert.equal(outcome.status, 'imported');
	assert.equal(database.state.get().comets.length, 35);
	assert.equal(settings.settings.get().lastUpdateDate, null);
});

test('a file that cannot be read says so, and the download is the candidate again', async () => {
	const fetcher = createManualFetcher();
	const broken = Object.assign(new Blob(['x']), { name: 'broken.txt' });
	const update = createUpdateService({
		fetcher,
		reader: { readText: async blob => { if (blob === broken) throw new DOMException('gone', 'NotReadableError'); return blob.text(); } },
		database: { preview: importPreview, apply: async () => ({ status: 'none' }) },
		settings: fixedSettings
	});
	update.open({ download: true });
	fetcher.calls[0].resolve(NEW);
	await settle();
	await update.loadFile(file('sample.txt', SAMPLE));
	assert.equal(update.state.get().candidate, 'file');
	await update.loadFile(broken);
	assert.deepEqual(update.state.get().file, { name: 'broken.txt', error: UNREADABLE_FILE });
	assert.equal(update.state.get().candidate, 'download');
	assert.equal(update.state.get().preview.counts, '1, 1, 33; 35');
});

test('a later file read wins over an earlier one still running', async () => {
	/** @type {Array<() => void>} */
	const pending = [];
	const update = createUpdateService({
		fetcher: createManualFetcher(),
		reader: { readText: blob => new Promise(resolve => pending.push(() => resolve(blob.text()))) },
		database: { preview: importPreview, apply: async () => ({ status: 'none' }) },
		settings: fixedSettings
	});
	update.open();
	const first = update.loadFile(file('first.txt', SAMPLE));
	const second = update.loadFile(file('second.txt', NEW));
	pending[1]();
	await second;
	pending[0]();
	await first;
	assert.equal(update.state.get().file?.name, 'second.txt');
	assert.equal(update.state.get().preview.counts, '1, 1, 33; 35');
});

test('a failed download shows its message, with the cross-origin hint for another URL', async () => {
	for (const url of [DEFAULT_DOWNLOAD_URL, 'https://example.org/elements.txt']) {
		const { update, fetcher } = await setup({ url });
		update.open();
		update.download();
		fetcher.calls[0].reject(new Error('HTTP 404 Not Found'));
		await settle();
		assert.deepEqual(update.state.get().download, { status: 'failed', error: downloadError('HTTP 404 Not Found', url) });
		assert.equal(update.state.get().candidate, null);
		update.download();
		assert.equal(fetcher.calls.length, 2, 'a failed download can be tried again');
	}
	assert.equal(downloadError('network error', DEFAULT_DOWNLOAD_URL), 'Download failed: network error.');
	assert.equal(downloadError('network error', 'https://example.org/x'), `Download failed: network error. ${CROSS_ORIGIN_HINT}`);
});

test('an empty download is an error', async () => {
	const { update, fetcher } = await setup();
	update.open();
	update.download();
	fetcher.calls[0].resolve('');
	await settle();
	assert.deepEqual(update.state.get().download, { status: 'failed', error: EMPTY_DOWNLOAD });
	assert.equal(update.state.get().candidate, null);
});

test('Cancel, closing and opening again stop the download; nothing it sends later counts', async () => {
	const { update, fetcher } = await setup();
	update.open();
	update.download();
	update.cancelDownload();
	assert.deepEqual(update.state.get().download, { status: 'idle' });
	assert.equal(fetcher.calls[0].signal?.aborted, true);
	assert.equal(/** @type {any} */ (fetcher.calls[0].signal).reason.name, 'AbortError');
	update.download();
	update.close();
	assert.equal(fetcher.calls[1].signal?.aborted, true);
	assert.deepEqual(update.state.get().download, { status: 'idle' });
	update.download();
	update.open();
	assert.equal(fetcher.calls[2].signal?.aborted, true);
	fetcher.calls[2].resolve(NEW);
	await settle();
	assert.equal(update.state.get().candidate, null);
});

test('automatic mode takes no file; Import without a source imports nothing', async () => {
	const { update, database } = await setup();
	update.open({ automatic: true });
	await update.loadFile(file('new.txt', NEW));
	assert.equal(update.state.get().file, null);
	const before = database.state.get();
	assert.deepEqual(await update.importCandidate(), { status: 'none' });
	assert.equal(database.state.get(), before);
});

test('a file whose merge is empty is the zero-comets error, and changes nothing', async () => {
	const { update, database } = await setup({ stored: null });
	update.open();
	const header = `${readVectorText('import/Soft04Cmt.txt').split('\n')[0]}\n`;
	await update.loadFile(file('header.txt', header));
	assert.equal(update.state.get().preview.counts, '0, 0, 0; 0');
	assert.deepEqual(await update.importCandidate(), { status: 'empty', message: 'Something wrong happened. Zero comets imported.' });
	assert.equal(database.state.get().comets.length, 0);
});
