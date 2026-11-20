// The Database view (`FormDatabase`): the app-wide selection as a list, its sort, filters, Reset
// and Delete, the selected comet's details, and an empty state that offers the download.
import { databaseTitle } from '../../../core/comets/counts.js';
import { indexAfterChange } from '../../../core/comets/selection.js';
import { createFiltersPanel } from '../../components/filters-panel.js';
import { createSortMenu } from '../../components/sort-menu.js';
import { el } from '../../dom.js';
import { createCometDetails } from './comet-details.js';
import { createCometList } from './comet-list.js';

/** @typedef {import('../../../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../../../services/selection.js').Selection} Selection */
/** @typedef {import('../../../core/comets/filter.js').Filter} Filter */

/** the empty database's action, and the Update route it opens */
const DOWNLOAD_ACTION = 'Download comet elements';
const DOWNLOAD_ROUTE = '#/update?download';

/** the title until the stored database has been read */
const LOADING_TITLE = 'Database';

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
export function createDatabaseView({ selection, database, settings, observations, dialogs, clock }) {
	const title = el('h1', { id: 'view-title-database' });
	const details = createCometDetails({ value: observations.value, clock, observer: () => settings.settings.get().location });
	// a double click closes the filters, as on the desktop, and otherwise switches the tab
	const list = createCometList({ label: 'Comets', onSelect: choose, onActivate: () => (filtersOpen ? showFilters(false, true) : details.switchTab()) });
	const sortMenu = createSortMenu({ sort: () => selection.state.get().sort, onSort: sort => selection.sort(sort) });
	const filters = createFiltersPanel({
		filters: selection.state.get().filters,
		now: clock.now,
		timeZone: clock.timeZone,
		apply: applyFilters,
		onClose: () => showFilters(false, true)
	});
	filters.element.id = 'database-filters';
	filters.element.hidden = true;
	const filtersArrow = el('span', { 'aria-hidden': 'true' }, '▼');
	const filtersButton = el('button', { type: 'button', class: 'button', 'aria-expanded': 'false', 'aria-controls': filters.element.id }, 'Filters ', filtersArrow);
	const resetButton = el('button', { type: 'button', class: 'button' }, 'Reset');
	const deleteButton = el('button', { type: 'button', class: 'button' }, 'Delete');
	const downloadButton = el('button', { type: 'button', class: 'button button--primary' }, DOWNLOAD_ACTION);
	const empty = el('div', { class: 'database-view__empty', hidden: true },
		el('p', {}, 'The database is empty.'), downloadButton);
	const body = el('div', { class: 'database-view__body' }, list.element, details.element, filters.element);
	// focusable, so that a click on an empty part of the view keeps the focus in it, where Esc closes
	// the filters panel, rather than on the body
	const element = el('section', { class: 'database-view', 'data-view': 'database', 'aria-labelledby': 'view-title-database', tabindex: '-1' },
		el('header', { class: 'database-view__header' },
			title,
			el('div', { class: 'database-view__tools' }, sortMenu.element, filtersButton, resetButton, deleteButton)),
		empty, body);

	/** @type {Comet | null} the comet selected, kept while it is in the list */
	let selected = null;
	/** @type {number | null} the index Delete keeps, for the list that follows it */
	let deletedAt = null;
	/** whether the filters panel is shown in the details' place */
	let filtersOpen = false;
	/** @type {ReadonlyArray<Filter>} the selection's filters the panel last followed */
	let shownFilters = selection.state.get().filters;
	/** whether the panel is applying its own rows, which it already shows */
	let applying = false;

	/**
	 * the panel's rows applied to the selection; rows that do not validate leave it as it was
	 * @param {ReadonlyArray<Filter>} rows
	 */
	function applyFilters(rows) {
		applying = true;
		try {
			return selection.filter(rows);
		} finally {
			applying = false;
		}
	}

	/**
	 * the filters panel in the details' place, or the details back
	 * @param {boolean} open
	 * @param {boolean} [focus] gives the focus to the Filters button, as a close from the panel or the list needs
	 */
	function showFilters(open, focus = false) {
		filtersOpen = open;
		filters.element.hidden = !open;
		details.element.hidden = open;
		filtersButton.setAttribute('aria-expanded', String(open));
		filtersArrow.textContent = open ? '▲' : '▼';
		if (focus) filtersButton.focus();
	}

	/** @param {Comet | null} comet */
	function choose(comet) {
		selected = comet;
		details.show(comet);
		deleteButton.disabled = comet === null;
	}

	/** @param {Readonly<Selection>} state */
	function render({ comets, total, filters: current }) {
		// filters set elsewhere (Reset, an import, the load) replace the panel's rows
		if (current !== shownFilters) {
			shownFilters = current;
			if (!applying) filters.setFilters(current);
		}
		// before the load the list is empty for want of the stored comets, which may be there
		const { loaded } = database.state.get();
		title.textContent = loaded ? databaseTitle(comets.length, total) : LOADING_TITLE;
		element.setAttribute('aria-busy', String(!loaded));
		const emptyDatabase = loaded && total === 0;
		empty.hidden = !emptyDatabase;
		body.hidden = emptyDatabase;
		// the load replaces the filters, so they wait for it, as an empty database has none to set
		filtersButton.disabled = !loaded || total === 0;
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

	filtersButton.addEventListener('click', () => showFilters(!filtersOpen));
	// Esc closes the filters panel from anywhere in the view, as on the desktop
	element.addEventListener('keydown', event => {
		if (event.key !== 'Escape' || !filtersOpen || event.defaultPrevented) return;
		event.preventDefault();
		showFilters(false, true);
	});
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
			details.start();
		},
		hide() {
			details.stop();
			unsubscribe?.();
			unsubscribe = null;
		}
	};
}
