// Result tabs: a tab list whose first tab is the owner's own (the settings of a calculation), then
// a tab per result, which the owner adds, replaces and closes. INTERNALS.md, How the UI is written.
import { el } from '../dom.js';

/**
 * @typedef {object} ResultTabs
 * @property {HTMLElement} element the tab list over the panels
 * @property {(title: string, content: HTMLElement) => number} add a result after the others,
 *   selected; its id
 * @property {(id: number, title: string, content: HTMLElement) => void} replace a result's title and
 *   content, selected
 * @property {(id: number) => void} close removes a result; the tab before it is selected if it was
 * @property {(id: number | null) => void} select a result, or the first tab for null
 * @property {() => number | null} selected the result selected; null while the first tab is
 * @property {(id: number) => boolean} has whether the result is open
 */

/**
 * @typedef {object} Tab
 * @property {number | null} id null for the first tab
 * @property {HTMLButtonElement} tab
 * @property {HTMLElement} panel
 */

let ids = 0;

/**
 * @param {object} options
 * @param {string} options.label names the tab list
 * @param {string} options.name the first tab's
 * @param {HTMLElement} options.content the first tab's panel's content
 * @param {(id: number) => void} [options.onClose] after a result is closed, by close or by Delete
 *   on its tab
 * @returns {ResultTabs}
 */
export function createResultTabs({ label, name, content, onClose = () => {} }) {
	const prefix = `result-tabs-${++ids}`;
	const list = el('div', { class: 'result-tabs__list', role: 'tablist', 'aria-label': label });
	const element = el('div', { class: 'result-tabs' }, list);
	let lastId = 0;
	/** @type {Tab[]} the first tab, then the results in the order they were added */
	const tabs = [];
	/** @type {Tab} */
	let active;

	/**
	 * @param {number | null} id
	 * @param {string} title
	 * @param {HTMLElement} inner
	 * @returns {Tab}
	 */
	function tabOf(id, title, inner) {
		const key = id ?? 'first';
		const tab = el('button', {
			type: 'button', role: 'tab', id: `${prefix}-tab-${key}`, class: 'result-tabs__tab',
			'aria-controls': `${prefix}-panel-${key}`, 'aria-selected': 'false', tabindex: '-1'
		}, title);
		const panel = el('div', { id: `${prefix}-panel-${key}`, class: 'result-tabs__panel', role: 'tabpanel', 'aria-labelledby': tab.id, hidden: true }, inner);
		const entry = { id, tab, panel };
		tab.addEventListener('click', () => choose(entry));
		return entry;
	}

	/** @param {Tab} entry */
	function choose(entry) {
		// a panel hidden with the focus in it, as by a button in it, hands the focus to the new tab
		const focused = active !== undefined && active !== entry && active.panel.contains(document.activeElement);
		active = entry;
		for (const { tab, panel } of tabs) {
			const selected = tab === entry.tab;
			tab.setAttribute('aria-selected', String(selected));
			tab.tabIndex = selected ? 0 : -1;
			panel.hidden = !selected;
		}
		if (focused) entry.tab.focus();
	}

	/** @param {number} id */
	const find = id => tabs.find(entry => entry.id === id);

	/**
	 * @param {Tab} entry
	 * @param {string} title
	 */
	function retitle(entry, title) {
		entry.tab.textContent = title;
		// a long title is cut short in the list
		entry.tab.title = title;
	}

	/** @param {number} id */
	function close(id) {
		const index = tabs.findIndex(entry => entry.id === id);
		if (index <= 0) return;
		const [entry] = tabs.splice(index, 1);
		const focused = entry.tab.matches(':focus') || entry.panel.contains(document.activeElement);
		entry.tab.remove();
		entry.panel.remove();
		if (active === entry) {
			choose(tabs[index - 1]);
			// the focus was in what is gone
			if (focused) active.tab.focus();
		}
		onClose(id);
	}

	list.addEventListener('keydown', event => {
		const index = tabs.indexOf(active);
		/** @type {Record<string, () => number>} */
		const moves = { ArrowRight: () => index + 1, ArrowLeft: () => index - 1, Home: () => 0, End: () => tabs.length - 1 };
		if (event.key === 'Delete' && active.id !== null) {
			event.preventDefault();
			close(active.id);
			return;
		}
		const move = moves[event.key];
		if (!move) return;
		event.preventDefault();
		choose(tabs[(move() + tabs.length) % tabs.length]);
		active.tab.focus();
	});

	const first = tabOf(null, name, content);
	tabs.push(first);
	list.append(first.tab);
	element.append(first.panel);
	choose(first);

	return Object.freeze({
		element,
		add(/** @type {string} */ title, /** @type {HTMLElement} */ inner) {
			const entry = tabOf(++lastId, title, inner);
			retitle(entry, title);
			tabs.push(entry);
			list.append(entry.tab);
			element.append(entry.panel);
			choose(entry);
			return /** @type {number} */ (entry.id);
		},
		replace(/** @type {number} */ id, /** @type {string} */ title, /** @type {HTMLElement} */ inner) {
			const entry = find(id);
			if (!entry) return;
			retitle(entry, title);
			entry.panel.replaceChildren(inner);
			choose(entry);
		},
		close,
		select(/** @type {number | null} */ id) {
			const entry = id === null ? first : find(id);
			if (entry) choose(entry);
		},
		selected: () => active.id,
		has: (/** @type {number} */ id) => find(id) !== undefined
	});
}
