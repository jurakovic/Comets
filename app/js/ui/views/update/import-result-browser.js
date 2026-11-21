// The import result browser: the merged list opened from the Update dialog's View, read-only, by
// import result, with the Database view's details. INTERNALS.md, Views.
import { databaseTitle } from '../../../core/comets/counts.js';
import { cometsWithResult, IMPORT_RESULT_CHOICES } from '../../../core/comets/merge.js';
import { openModal } from '../../components/modal.js';
import { el } from '../../dom.js';
import { createCometDetails } from '../database/comet-details.js';
import { createCometList } from '../database/comet-list.js';

/**
 * @typedef {object} ImportResultBrowser
 * @property {() => void} close as its own Close does
 * @property {Promise<void>} closed settles once it is closed
 */

/**
 * opens the browser over the page
 * @param {object} options
 * @param {import('../../../core/comets/merge.js').Merge} options.merge
 * @param {import('../../../core/comets/properties.js').ValueAccessor} options.value
 * @param {import('../../../services/ports.js').Clock} options.clock
 * @param {() => import('../../../core/astro/ephemeris.js').Observer} options.observer
 * @returns {ImportResultBrowser}
 */
export function browseImportResults({ merge, value, clock, observer }) {
	/** @type {() => void} */
	let close = () => {};
	const closed = /** @type {Promise<void>} */ (new Promise(resolve => {
		const title = el('h2', { id: 'import-results-title', class: 'import-results__title' });
		const details = createCometDetails({ value, clock, observer });
		const list = createCometList({ label: 'Comets', onSelect: comet => details.show(comet), onActivate: () => details.switchTab() });
		const choice = el('select', { class: 'import-results__choice', id: 'import-results-choice' },
			...IMPORT_RESULT_CHOICES.map(({ key, label }) => el('option', { value: key }, label)));
		const closeButton = el('button', { type: 'button', class: 'button' }, 'Close');
		const dialog = el('dialog', { class: 'import-results', 'aria-labelledby': 'import-results-title' },
			el('div', { class: 'import-results__header' },
				title,
				el('label', { class: 'import-results__label', for: 'import-results-choice' }, 'Import result'), choice),
			el('div', { class: 'database-view__body import-results__body' }, list.element, details.element),
			el('div', { class: 'import-results__actions' }, closeButton));

		function show() {
			const key = /** @type {import('../../../core/comets/merge.js').ImportResult | 'All'} */ (choice.value);
			const comets = cometsWithResult(merge, key);
			title.textContent = databaseTitle(comets.length, merge.comets.length);
			list.show(comets, comets.length > 0 ? 0 : -1);
			details.show(list.selected());
		}

		choice.addEventListener('change', show);
		closeButton.addEventListener('click', () => close());

		show();
		// Esc closes the dialog natively
		close = openModal(dialog, {
			onClose: () => {
				details.stop();
				resolve();
			}
		});
		details.start();
	}));
	return Object.freeze({ close: () => close(), closed });
}
