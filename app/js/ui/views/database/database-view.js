// The Database view (`FormDatabase`): the app-wide selection as a list, its sort, Reset and Delete,
// the selected comet's details, and an empty state that offers the download.
// docs/spec/05-database-and-update.md, Database view.
import { databaseTitle } from '../../../core/comets/counts.js';
import { indexAfterChange } from '../../../core/comets/selection.js';
import { createSortMenu } from '../../components/sort-menu.js';
import { el } from '../../dom.js';
import { createCometDetails } from './comet-details.js';
import { createCometList } from './comet-list.js';

/** @typedef {import('../../../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../../../services/selection.js').Selection} Selection */

/** the empty database's action, and the Update route it opens */
const DOWNLOAD_ACTION = 'Download comet elements';
const DOWNLOAD_ROUTE = '#/update?download';

/**
 * the question Delete asks when its setting is on (`SettingsBase.ValidateCometDelete`)
 * @param {Comet} comet
 * @returns {string}
 */
export function deleteQuestion(comet) {
	return `Do you really want to delete ${comet.full}`;
}

/**
 * @param {import('../../shell/context.js').AppContext} context
 * @returns {import('../../shell/router.js').View}
 */
export function createDatabaseView({ selection, database, settings, observations, dialogs }) {
	const title = el('h1', { id: 'view-title-database' });
	const details = createCometDetails({ value: observations.value });
	const list = createCometList({ label: 'Comets', onSelect: choose, onActivate: () => details.switchTab() });
	const sortMenu = createSortMenu({ sort: () => selection.state.get().sort, onSort: sort => selection.sort(sort) });
	const resetButton = el('button', { type: 'button', class: 'button' }, 'Reset');
	const deleteButton = el('button', { type: 'button', class: 'button' }, 'Delete');
	const downloadButton = el('button', { type: 'button', class: 'button button--primary' }, DOWNLOAD_ACTION);
	const empty = el('div', { class: 'database-view__empty', hidden: true },
		el('p', {}, 'The database is empty.'), downloadButton);
	const body = el('div', { class: 'database-view__body' }, list.element, details.element);
	const element = el('section', { class: 'database-view', 'data-view': 'database', 'aria-labelledby': 'view-title-database' },
		el('header', { class: 'database-view__header' },
			title,
			el('div', { class: 'database-view__tools' }, sortMenu.element, resetButton, deleteButton)),
		empty, body);

	/** @type {Comet | null} the comet selected, kept while it is in the list */
	let selected = null;
	/** @type {number | null} the index Delete keeps, for the list that follows it */
	let deletedAt = null;

	/** @param {Comet | null} comet */
	function choose(comet) {
		selected = comet;
		details.show(comet);
		deleteButton.disabled = comet === null;
	}

	/** @param {Readonly<Selection>} state */
	function render({ comets, total }) {
		title.textContent = databaseTitle(comets.length, total);
		const emptyDatabase = total === 0;
		empty.hidden = !emptyDatabase;
		body.hidden = emptyDatabase;
		const index = indexAfterChange(comets, deletedAt === null ? selected : null, deletedAt ?? 0);
		deletedAt = null;
		list.show(comets, index);
		choose(list.selected());
	}

	async function remove() {
		const comet = list.selected();
		if (!comet) return;
		if (settings.settings.get().showDeleteCometConfirmation && !await dialogs.confirm({ message: deleteQuestion(comet) })) return;
		// the database changes at once, and the list with it, before the write
		deletedAt = list.index();
		const done = database.remove(comet);
		deletedAt = null;
		list.element.focus();
		await done;
	}

	resetButton.addEventListener('click', () => selection.reset());
	deleteButton.addEventListener('click', () => { remove(); });
	downloadButton.addEventListener('click', () => { location.hash = DOWNLOAD_ROUTE; });

	/** @type {(() => void) | null} */
	let unsubscribe = null;
	return {
		element,
		show() {
			unsubscribe ??= selection.state.subscribe(render);
			render(selection.state.get());
		},
		hide() {
			unsubscribe?.();
			unsubscribe = null;
		}
	};
}
