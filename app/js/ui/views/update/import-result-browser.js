// The import result browser: the merged list opened from the Update dialog's View, read-only, by
// import result, with the Database view's details. docs/spec/05-database-and-update.md, Import
// result browser.
import { databaseTitle } from '../../../core/comets/counts.js';
import { cometsWithResult, IMPORT_RESULT_CHOICES } from '../../../core/comets/merge.js';
import { el } from '../../dom.js';
import { createCometDetails } from '../database/comet-details.js';
import { createCometList } from '../database/comet-list.js';

/**
 * opens the browser over the page
 * @param {object} options
 * @param {import('../../../core/comets/merge.js').Merge} options.merge
 * @param {import('../../../core/comets/properties.js').ValueAccessor} options.value
 * @param {import('../../../services/ports.js').Clock} options.clock
 * @param {() => import('../../../core/astro/ephemeris.js').Observer} options.observer
 * @returns {Promise<void>} once it is closed
 */
export function browseImportResults({ merge, value, clock, observer }) {
	return new Promise(resolve => {
		const previous = document.activeElement;
		const title = el('h2', { id: 'import-results-title', class: 'import-results__title' });
		const details = createCometDetails({ value, clock, observer });
		const list = createCometList({ label: 'Comets', onSelect: comet => details.show(comet), onActivate: () => details.switchTab() });
		const choice = el('select', { class: 'import-results__choice', id: 'import-results-choice' },
			...IMPORT_RESULT_CHOICES.map(({ key, label }) => el('option', { value: key }, label)));
		const close = el('button', { type: 'button', class: 'button' }, 'Close');
		const dialog = el('dialog', { class: 'import-results', 'aria-labelledby': 'import-results-title' },
			el('div', { class: 'import-results__header' },
				title,
				el('label', { class: 'import-results__label', for: 'import-results-choice' }, 'Import result'), choice),
			el('div', { class: 'database-view__body import-results__body' }, list.element, details.element),
			el('div', { class: 'import-results__actions' }, close));

		function show() {
			const key = /** @type {import('../../../core/comets/merge.js').ImportResult | 'All'} */ (choice.value);
			const comets = cometsWithResult(merge, key);
			title.textContent = databaseTitle(comets.length, merge.comets.length);
			list.show(comets, comets.length > 0 ? 0 : -1);
			details.show(list.selected());
		}

		choice.addEventListener('change', show);
		close.addEventListener('click', () => dialog.close());
		// Esc closes the dialog natively
		dialog.addEventListener('close', () => {
			details.stop();
			dialog.remove();
			if (previous instanceof HTMLElement && previous.isConnected) previous.focus();
			resolve();
		});

		show();
		document.body.append(dialog);
		dialog.showModal();
		details.start();
	});
}
