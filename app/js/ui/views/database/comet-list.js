// The list of comets by their full names: a listbox whose selection the arrow keys, Home, End and
// the page keys move. docs/spec/05-database-and-update.md, Layout.
import { el } from '../../dom.js';

/** @typedef {import('../../../core/comets/comet.js').Comet} Comet */

/**
 * @typedef {object} CometList
 * @property {HTMLElement} element
 * @property {(comets: ReadonlyArray<Comet>, index: number) => void} show a new list, with the
 *   index to select (-1 for none); onSelect does not run
 * @property {() => number} index the selected index, -1 for none
 * @property {() => Comet | null} selected
 */

let ids = 0;

/**
 * @param {object} options
 * @param {string} options.label the list's accessible name
 * @param {(comet: Comet | null, index: number) => void} options.onSelect after the user selects
 * @param {(comet: Comet) => void} [options.onActivate] a double click on an entry
 * @returns {CometList}
 */
export function createCometList({ label, onSelect, onActivate }) {
	const prefix = `comet-list-${++ids}`;
	const element = el('ul', { class: 'comet-list', role: 'listbox', tabindex: '0', 'aria-label': label });
	/** @type {ReadonlyArray<Comet>} */
	let comets = [];
	let selectedIndex = -1;

	/** @param {number} index */
	const optionAt = index => /** @type {HTMLElement | undefined} */ (element.children[index]);

	/** @param {number} index */
	function mark(index) {
		optionAt(selectedIndex)?.setAttribute('aria-selected', 'false');
		selectedIndex = index;
		const option = optionAt(index);
		if (option) {
			option.setAttribute('aria-selected', 'true');
			element.setAttribute('aria-activedescendant', option.id);
			option.scrollIntoView({ block: 'nearest' });
		} else {
			element.removeAttribute('aria-activedescendant');
		}
	}

	/** @param {number} index */
	function select(index) {
		if (comets.length === 0) return;
		const next = Math.min(Math.max(index, 0), comets.length - 1);
		if (next === selectedIndex) return;
		mark(next);
		onSelect(comets[next], next);
	}

	/** the entries one page of the list holds */
	function pageSize() {
		const first = optionAt(0);
		return first ? Math.max(1, Math.floor(element.clientHeight / first.offsetHeight) - 1) : 10;
	}

	element.addEventListener('click', event => {
		const option = /** @type {HTMLElement} */ (event.target).closest('[role="option"]');
		if (option) select(Number(/** @type {HTMLElement} */ (option).dataset.index));
	});
	element.addEventListener('dblclick', event => {
		const option = /** @type {HTMLElement} */ (event.target).closest('[role="option"]');
		if (option && onActivate) onActivate(comets[Number(/** @type {HTMLElement} */ (option).dataset.index)]);
	});
	element.addEventListener('keydown', event => {
		if (event.altKey || event.ctrlKey || event.metaKey) return;
		/** @type {Record<string, () => number>} */
		const moves = {
			ArrowDown: () => selectedIndex + 1,
			ArrowUp: () => Math.max(0, selectedIndex - 1),
			Home: () => 0,
			End: () => comets.length - 1,
			PageDown: () => selectedIndex + pageSize(),
			PageUp: () => selectedIndex - pageSize()
		};
		const move = moves[event.key];
		if (!move) return;
		event.preventDefault();
		select(move());
	});

	return Object.freeze({
		element,
		show(/** @type {ReadonlyArray<Comet>} */ list, /** @type {number} */ index) {
			comets = list;
			const fragment = document.createDocumentFragment();
			list.forEach((comet, i) => {
				fragment.append(el('li', { id: `${prefix}-${i}`, class: 'comet-list__option', role: 'option', 'aria-selected': 'false', 'data-index': i }, comet.full));
			});
			element.replaceChildren(fragment);
			selectedIndex = -1;
			mark(index);
		},
		index: () => selectedIndex,
		selected: () => comets[selectedIndex] ?? null
	});
}
