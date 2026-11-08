// The Update dialog over the current view (`FormUpdate`): the download and a local file, the
// preview of the source loaded last, and Import. Its state is the update service's.
// docs/spec/05-database-and-update.md, Update.
import { COUNTS_TOOLTIP } from '../../../core/formats/import.js';
import { closesAfterImport } from '../../../services/update.js';
import { el } from '../../dom.js';
import { browseImportResults } from './import-result-browser.js';

/** @typedef {import('../../../services/update.js').UpdateState} UpdateState */

/** what the file chooser offers first; any file can be chosen */
const FILE_TYPES = '.txt,.dat,.csv,.comet';

/**
 * @param {import('../../shell/context.js').AppContext} context
 * @returns {import('../../shell/router.js').View}
 */
export function createUpdateView({ update, notify, leaveOverlay, observations, settings, clock }) {
	const url = el('code', { class: 'update-view__url' });
	const openLink = el('a', { href: '', target: '_blank', rel: 'noopener' }, 'Open in browser');
	const downloadButton = el('button', { type: 'button', class: 'button' }, 'Download');
	const cancelButton = el('button', { type: 'button', class: 'button', hidden: true }, 'Cancel');
	const progress = el('progress', { class: 'update-view__progress', 'aria-label': 'Download progress', hidden: true });
	const downloadError = el('p', { class: 'update-view__error', role: 'alert', hidden: true });

	const fileInput = el('input', { type: 'file', id: 'update-file', class: 'update-view__file-input', accept: FILE_TYPES });
	const fileName = el('span', { class: 'update-view__file-name' });
	const fileError = el('p', { class: 'update-view__error', role: 'alert', hidden: true });
	const dropZone = el('div', { class: 'update-view__drop' },
		el('label', { for: 'update-file', class: 'button update-view__choose' }, 'Choose file'),
		el('span', { class: 'update-view__drop-hint' }, 'or drop a file here'),
		fileName);
	const fileSection = el('section', { class: 'update-view__source', 'aria-labelledby': 'update-file-title' },
		el('h2', { id: 'update-file-title' }, 'Import from local file'),
		fileInput, dropZone, fileError);

	const format = el('dd', { class: 'update-view__format' });
	const counts = el('span', { class: 'update-view__counts' });
	const viewButton = el('button', { type: 'button', class: 'button update-view__view' }, 'View');
	const closeWhenFinished = el('input', { type: 'checkbox', checked: true });
	const closeLabel = el('label', { class: 'update-view__close-when' }, closeWhenFinished, ' Close when finished');
	const importButton = el('button', { type: 'button', class: 'button button--primary' }, 'Import');
	const closeButton = el('button', { type: 'button', class: 'button' }, 'Close');

	const element = el('dialog', { class: 'update-view', 'data-view': 'update', 'aria-labelledby': 'view-title-update' },
		el('h1', { id: 'view-title-update' }, 'Update'),
		el('section', { class: 'update-view__source', 'aria-labelledby': 'update-download-title' },
			el('h2', { id: 'update-download-title' }, 'Download the latest orbital elements from Internet'),
			el('p', { class: 'update-view__url-row' }, url, ' ', openLink),
			el('div', { class: 'update-view__row' }, downloadButton, cancelButton, progress),
			downloadError),
		fileSection,
		el('dl', { class: 'update-view__status' },
			el('dt', {}, 'Import format'), format,
			el('dt', {}, 'Total comets'), el('dd', { class: 'update-view__total' }, counts, viewButton)),
		el('div', { class: 'update-view__actions' }, closeLabel, importButton, closeButton));

	/** @param {Readonly<UpdateState>} state */
	function render(state) {
		url.textContent = state.url;
		openLink.href = state.url;

		const { download } = state;
		const running = download.status === 'running';
		downloadButton.disabled = running;
		cancelButton.hidden = !running;
		progress.hidden = !running;
		if (download.status === 'running') {
			const total = download.progress?.total;
			if (total !== undefined) {
				progress.max = total;
				progress.value = download.progress?.received ?? 0;
			} else {
				progress.removeAttribute('value');
			}
		}
		downloadError.hidden = download.status !== 'failed';
		downloadError.textContent = download.status === 'failed' ? download.error : '';

		fileSection.classList.toggle('update-view__source--disabled', state.automatic);
		fileInput.disabled = state.automatic;
		fileName.textContent = state.file?.name ?? '';
		fileError.hidden = !state.file?.error;
		fileError.textContent = state.file?.error ?? '';

		format.textContent = state.preview.formatLabel;
		counts.textContent = state.preview.counts;
		if (state.preview.merge) counts.title = COUNTS_TOOLTIP;
		else counts.removeAttribute('title');
		viewButton.disabled = state.preview.merge === null;
		closeLabel.hidden = state.automatic;
	}

	/** @param {Blob & { name: string } | undefined} file */
	const load = file => { if (file) update.loadFile(file); };

	downloadButton.addEventListener('click', () => update.download());
	cancelButton.addEventListener('click', () => update.cancelDownload());
	fileInput.addEventListener('change', () => {
		load(fileInput.files?.[0]);
		// the same file chosen again is a change too
		fileInput.value = '';
	});
	// a drop anywhere in the dialog is the dialog's: the browser would open the file instead,
	// leaving the app; only the zone takes it, and not in automatic mode
	/** @param {DragEvent} event */
	const takes = event => !update.state.get().automatic && event.target instanceof Node && dropZone.contains(event.target);
	element.addEventListener('dragover', event => {
		event.preventDefault();
		const taking = takes(event);
		if (event.dataTransfer) event.dataTransfer.dropEffect = taking ? 'copy' : 'none';
		dropZone.classList.toggle('update-view__drop--over', taking);
	});
	element.addEventListener('dragleave', event => {
		if (!(event.relatedTarget instanceof Node && dropZone.contains(event.relatedTarget))) dropZone.classList.remove('update-view__drop--over');
	});
	element.addEventListener('drop', event => {
		event.preventDefault();
		dropZone.classList.remove('update-view__drop--over');
		if (takes(event)) load(event.dataTransfer?.files[0]);
	});

	importButton.addEventListener('click', async () => {
		importButton.disabled = true;
		try {
			const outcome = await update.importCandidate();
			if (outcome.status === 'imported') notify(outcome.message);
			else if (outcome.status === 'empty') notify(outcome.message, { kind: 'error' });
			if (closesAfterImport({ outcome, automatic: update.state.get().automatic, closeWhenFinished: closeWhenFinished.checked })) leaveOverlay();
		} finally {
			importButton.disabled = false;
		}
	});
	closeButton.addEventListener('click', leaveOverlay);
	/** @type {import('./import-result-browser.js').ImportResultBrowser | null} the one open, closed with the dialog */
	let browser = null;
	viewButton.addEventListener('click', () => {
		const { merge } = update.state.get().preview;
		if (!merge) return;
		const opened = browseImportResults({ merge, value: observations.value, clock, observer: () => settings.settings.get().location });
		browser = opened;
		opened.closed.then(() => { if (browser === opened) browser = null; });
	});

	let shown = false;
	/** @type {(() => void) | null} */
	let unsubscribe = null;
	// Esc closes the dialog natively; the route follows
	element.addEventListener('close', () => {
		if (shown) leaveOverlay();
	});

	return {
		element,
		show(params) {
			unsubscribe ??= update.state.subscribe(render);
			update.open({ automatic: params.has('automatic'), download: params.has('download') });
			render(update.state.get());
			closeWhenFinished.checked = true;
			shown = true;
			if (!element.open) element.showModal();
		},
		hide() {
			shown = false;
			browser?.close();
			update.close();
			unsubscribe?.();
			unsubscribe = null;
			if (element.open) element.close();
		}
	};
}
