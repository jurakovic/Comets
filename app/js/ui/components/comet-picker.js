// The comet picker (`SelectCometControl`): a combo box of its own list, `[ALL COMETS (n)]` first,
// with the chosen comet's tooltip; the quick-select menu; Filter, a dialog with the filters panel
// over the whole database; and the sort menu. Its rules are core/input/comet-picker.js.
// INTERNALS.md, How the UI is written.
import { databaseTitle } from '../../core/comets/counts.js';
import { QUICK_SELECTIONS, quickSelect } from '../../core/comets/selection.js';
import {
	choiceAt, cometTooltip, entryIndex, filterPickerList, NO_CHOICE, perihelionDate, pickerEntries, rebindChoice, sortPickerList
} from '../../core/input/comet-picker.js';
import { instantToJd } from '../../core/time/julian.js';
import { valid } from '../../core/validation.js';
import { el } from '../dom.js';
import { createFiltersPanel } from './filters-panel.js';
import { createMenu } from './menu.js';
import { openModal } from './modal.js';
import { createSortMenu } from './sort-menu.js';

/** @typedef {import('../../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../../core/input/comet-picker.js').PickerChoice} PickerChoice */
/** @typedef {import('../../core/input/comet-picker.js').PickerList} PickerList */

/**
 * @typedef {object} CometPicker
 * @property {HTMLElement} element
 * @property {() => Readonly<PickerChoice>} choice
 * @property {() => Readonly<PickerList>} list the comets offered, with the filters and sort that made them
 * @property {(list: Readonly<PickerList>, choice?: Readonly<PickerChoice>) => void} setList a list
 *   as the owner sets it, with the choice it keeps as a new list keeps one (the current choice
 *   when none is given); no onChange
 */

let ids = 0;

/**
 * @param {object} options
 * @param {Readonly<PickerList>} options.list the list to start with
 * @param {Readonly<PickerChoice>} [options.choice] kept as a new list keeps it; otherwise the brightest
 * @param {() => ReadonlyArray<Comet>} options.database the main collection, which Filter
 *   filters, read at each apply
 * @param {import('../../core/comets/properties.js').ValueAccessor} options.value
 * @param {() => number} options.now for Closest to perihelion, and the filters' dates
 * @param {() => string} options.timeZone the filters' date pickers' local time
 * @param {ReadonlyArray<Pick<import('./date-picker.js').DatePicker, 'setPerihelionDate'>>} [options.datePickers]
 *   the owner's, whose Perihelion Date preset follows the chosen comet
 * @param {(choice: Readonly<PickerChoice>) => void} [options.onChange] after the user changes the
 *   choice: by the combo box, quick select, or a filter edit that changes it (no comet and no
 *   multiple mode while the list is empty)
 * @param {string} [options.label] names the picker and its combo box
 * @returns {CometPicker}
 */
export function createCometPicker({ list, choice = NO_CHOICE, database, value, now, timeZone, datePickers = [], onChange = () => {}, label = 'Comet' }) {
	const id = `comet-picker-${++ids}`;
	const select = el('select', { class: 'comet-picker__comet', 'aria-label': label });
	const quick = createMenu({
		label: 'Quick select',
		name: 'Quick select',
		className: 'comet-picker__quick',
		entries: () => QUICK_SELECTIONS.map(({ key, label: text }) => ({ label: text, run: () => quickSelected(key) }))
	});
	const filterButton = el('button', { type: 'button', class: 'button', 'aria-haspopup': 'dialog' }, 'Filter');
	const sortMenu = createSortMenu({ sort: () => current.sort, onSort: sort => relist(sortPickerList(current, sort, value), chosen) });
	const element = el('div', { class: 'comet-picker', role: 'group', 'aria-label': label },
		select, el('div', { class: 'comet-picker__tools' }, quick.element, filterButton, sortMenu.element));

	/** @type {Readonly<PickerList>} */
	let current = list;
	/** @type {Readonly<PickerChoice>} */
	let chosen = NO_CHOICE;
	/** @type {ReadonlyArray<Comet> | null} the comets the entries show */
	let shown = null;

	const nowJd = () => instantToJd(now());

	// the tooltip, built as it is about to be seen too, since the current values age
	function describe() {
		if (!chosen.comet) {
			select.removeAttribute('title');
			return;
		}
		const { title, body } = cometTooltip(chosen.comet, value);
		select.title = `${title}\n${body}`;
	}

	/** @param {Readonly<PickerChoice>} next */
	function choose(next) {
		chosen = next;
		select.selectedIndex = entryIndex(current.comets, next);
		describe();
		const date = perihelionDate(next, value);
		for (const picker of datePickers) picker.setPerihelionDate(date);
	}

	/**
	 * @param {Readonly<PickerList>} next
	 * @param {Readonly<PickerChoice>} previous the choice the list keeps
	 */
	function show(next, previous) {
		current = next;
		if (next.comets !== shown) {
			shown = next.comets;
			select.replaceChildren(...pickerEntries(next.comets).map((text, index) => el('option', { value: String(index) }, text)));
		}
		// nothing to choose from an empty list, nor to select quickly
		select.disabled = next.comets.length === 0;
		quick.button.disabled = next.comets.length === 0;
		choose(rebindChoice(next.comets, previous, value, nowJd()));
	}

	/** @param {Readonly<PickerChoice>} next the user's */
	function changed(next) {
		const before = chosen;
		choose(next);
		if (next.all !== before.all || next.comet !== before.comet) onChange(next);
	}

	/**
	 * a new list from the user's filter or sort, keeping the choice as a new list keeps it
	 * @param {Readonly<PickerList>} next
	 * @param {Readonly<PickerChoice>} previous
	 */
	function relist(next, previous) {
		const before = chosen;
		show(next, previous);
		if (chosen.all !== before.all || chosen.comet !== before.comet) onChange(chosen);
	}

	/** @param {string} key a quick selection */
	function quickSelected(key) {
		const comet = quickSelect(current.comets, key, value, nowJd());
		if (comet) changed(Object.freeze({ all: false, comet }));
	}

	// Filter: the filters panel in a dialog over the whole database, from the picker's own filters,
	// applied to its list after every edit, in its sort. The choice made before it opened is the one
	// each list keeps, as the desktop kept it for the list its Database dialog gave on OK
	function filter() {
		const opened = chosen;
		const title = el('h2', { id: `${id}-filter-title`, class: 'comet-picker__filter-title' }, databaseTitle(current.comets.length, database().length));
		/** @type {() => void} */
		let close = () => {};
		const panel = createFiltersPanel({
			filters: current.filters,
			now,
			timeZone,
			apply(rows) {
				const comets = database();
				const result = filterPickerList(comets, rows, current.sort, value);
				if (!result.ok) return result;
				relist(result.value, opened);
				title.textContent = databaseTitle(result.value.comets.length, comets.length);
				return valid(result.value.filters);
			},
			onClose: () => close()
		});
		const dialog = el('dialog', { class: 'comet-picker__filter', 'aria-labelledby': title.id }, title, panel.element);
		// Esc closes it natively; the focus goes back to Filter
		close = openModal(dialog);
	}

	select.addEventListener('change', () => changed(choiceAt(current.comets, select.selectedIndex)));
	select.addEventListener('pointerenter', describe);
	select.addEventListener('focus', describe);
	filterButton.addEventListener('click', filter);

	show(list, choice);
	return Object.freeze({
		element,
		choice: () => chosen,
		list: () => current,
		setList: (next, keep = chosen) => show(next, keep)
	});
}
