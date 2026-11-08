// The Update dialog's state (`FormUpdate`): a download with progress and Cancel, a local file,
// the source loaded last as the candidate, its preview against the database, and Import.
// docs/spec/05-database-and-update.md, Update.
import { createStore, readonly } from '../lib/store.js';
import { DEFAULT_DOWNLOAD_URL } from './settings.js';

/** @typedef {import('../core/formats/import.js').ImportPreview} ImportPreview */
/** @typedef {import('./ports.js').FetchProgress} FetchProgress */
/** @typedef {import('./database.js').ImportSource} ImportSource */

/**
 * the download: not started (or cancelled), running with its progress, done, or failed with the
 * message to show
 * @typedef {{ status: 'idle' } | { status: 'running', progress: FetchProgress | null } | { status: 'done' } | { status: 'failed', error: string }} DownloadState
 */

/**
 * @typedef {object} UpdateState
 * @property {string} url the download URL, from the settings when the dialog opened
 * @property {boolean} automatic automatic mode: the download starts at once, no local file, and
 *   Import closes the dialog whatever it imported
 * @property {DownloadState} download
 * @property {{ name: string, error: string | null } | null} file the file loaded last, or the one
 *   that could not be read
 * @property {ImportSource | null} candidate the source loaded most recently
 * @property {ImportPreview} preview the candidate merged into the database as it is
 */

/** what an empty download shows, where the desktop showed its transport's error text */
export const EMPTY_DOWNLOAD = 'Download failed: the response is empty.';

/** added to a failed download's message when the URL is not the default */
export const CROSS_ORIGIN_HINT = 'The server must allow cross-origin requests (CORS) from this page, or the browser refuses the download.';

/** what a file that cannot be read shows */
export const UNREADABLE_FILE = 'The file could not be read.';

/**
 * the message a failed download shows
 * @param {string} reason what failed: `network error`, `HTTP 404 Not Found`
 * @param {string} url the URL downloaded
 * @returns {string}
 */
export function downloadError(reason, url) {
	const message = `Download failed: ${reason}.`;
	return url === DEFAULT_DOWNLOAD_URL ? message : `${message} ${CROSS_ORIGIN_HINT}`;
}

/**
 * whether the dialog closes after Import: always in automatic mode, otherwise once something was
 * imported with Close when finished checked
 * @param {object} after
 * @param {import('./database.js').ImportOutcome} after.outcome
 * @param {boolean} after.automatic
 * @param {boolean} after.closeWhenFinished
 * @returns {boolean}
 */
export function closesAfterImport({ outcome, automatic, closeWhenFinished }) {
	return automatic || (outcome.status === 'imported' && closeWhenFinished);
}

/**
 * @typedef {object} UpdateService
 * @property {import('../lib/store.js').ReadableStore<Readonly<UpdateState>>} state
 * @property {(options?: { automatic?: boolean, download?: boolean }) => void} open a fresh dialog,
 *   downloading at once in automatic mode or when asked
 * @property {() => void} download starts the download, unless one runs
 * @property {() => void} cancelDownload
 * @property {(file: Blob & { name: string }) => Promise<void>} loadFile a chosen or dropped file;
 *   none in automatic mode
 * @property {() => Promise<import('./database.js').ImportOutcome>} importCandidate Import
 * @property {() => void} close stops a running download
 */

/**
 * @param {object} deps
 * @param {import('./ports.js').TextFetcher} deps.fetcher
 * @param {import('./ports.js').TextFileReader} deps.reader
 * @param {Pick<import('./database.js').DatabaseService, 'preview' | 'apply'>} deps.database
 * @param {Pick<import('./settings.js').SettingsService, 'settings'>} deps.settings
 * @returns {UpdateService}
 */
export function createUpdateService({ fetcher, reader, database, settings }) {
	/** @returns {Readonly<UpdateState>} */
	const fresh = (automatic = false) => Object.freeze({
		url: settings.settings.get().downloadUrl,
		automatic,
		download: Object.freeze({ status: /** @type {const} */ ('idle') }),
		file: null,
		candidate: null,
		preview: database.preview(null)
	});
	const state = createStore(fresh());
	/** @type {Record<ImportSource, string | null>} the sources' texts, kept apart from the state */
	const texts = { download: null, file: null };
	/** @type {AbortController | null} */
	let downloading = null;
	// a later file read wins over an earlier one still running
	let fileTicket = 0;

	/** @param {Partial<UpdateState>} change */
	const set = change => state.set(Object.freeze({ ...state.get(), ...change }));

	/** @param {ImportSource | null} candidate */
	const previewOf = candidate => database.preview(candidate === null ? null : texts[candidate]);

	/** @param {ImportSource} source @param {string} text */
	function loaded(source, text) {
		texts[source] = text;
		return { candidate: source, preview: previewOf(source) };
	}

	function stop() {
		downloading?.abort(new DOMException('The download was cancelled', 'AbortError'));
		downloading = null;
	}

	async function download() {
		if (downloading) return;
		const controller = new AbortController();
		downloading = controller;
		const { url } = state.get();
		set({ download: Object.freeze({ status: 'running', progress: null }) });
		try {
			const text = await fetcher.fetchText(url, {
				signal: controller.signal,
				onProgress: progress => {
					if (downloading === controller) set({ download: Object.freeze({ status: 'running', progress: Object.freeze({ ...progress }) }) });
				}
			});
			if (downloading !== controller) return;
			set(text === ''
				? { download: Object.freeze({ status: 'failed', error: EMPTY_DOWNLOAD }) }
				: { download: Object.freeze({ status: 'done' }), ...loaded('download', text) });
		} catch (error) {
			if (downloading !== controller) return;
			const reason = error instanceof Error ? error.message : String(error);
			set({ download: Object.freeze({ status: 'failed', error: downloadError(reason, url) }) });
		} finally {
			if (downloading === controller) downloading = null;
		}
	}

	return Object.freeze({
		state: readonly(state),
		open({ automatic = false, download: start = false } = {}) {
			stop();
			fileTicket += 1;
			texts.download = texts.file = null;
			state.set(fresh(automatic));
			if (automatic || start) download();
		},
		download() { download(); },
		cancelDownload() {
			if (!downloading) return;
			stop();
			set({ download: Object.freeze({ status: 'idle' }) });
		},
		async loadFile(/** @type {Blob & { name: string }} */ file) {
			if (state.get().automatic) return;
			const ticket = ++fileTicket;
			try {
				const text = await reader.readText(file);
				if (ticket === fileTicket) set({ file: Object.freeze({ name: file.name, error: null }), ...loaded('file', text) });
			} catch {
				if (ticket !== fileTicket) return;
				// the file source is gone: the download, if there is one, is the candidate again
				texts.file = null;
				const candidate = state.get().candidate === 'file' ? (texts.download === null ? null : 'download') : state.get().candidate;
				set({ file: Object.freeze({ name: file.name, error: UNREADABLE_FILE }), candidate, preview: previewOf(candidate) });
			}
		},
		async importCandidate() {
			const { candidate } = state.get();
			const outcome = await database.apply(candidate === null ? null : texts[candidate], candidate ?? 'file');
			// the counts against the database as it is now, for the candidate now: a download may
			// have finished while the import was stored
			set({ preview: previewOf(state.get().candidate) });
			return outcome;
		},
		close() {
			stop();
			if (state.get().download.status === 'running') set({ download: Object.freeze({ status: 'idle' }) });
		}
	});
}
