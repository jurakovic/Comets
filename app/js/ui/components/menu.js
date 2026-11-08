// A menu under a button: a popover whose entries are built each time it opens, with the arrow
// keys, Home and End among them, Esc to close, and focus back on the button. The sort menu and
// the date presets are menus. docs/spec/12-architecture.md, UI (Accessibility).
import { el } from '../dom.js';

/**
 * an entry: an action, a choice marked when it is the current one (`checked`), or a separator
 * @typedef {{ separator: true } | { label: string, checked?: boolean, run: () => void }} MenuEntry
 */

/**
 * @typedef {object} Menu
 * @property {HTMLElement} element the button and its menu
 * @property {HTMLButtonElement} button
 * @property {() => void} close
 */

/** room kept between the menu and the window's edges, in px */
const EDGE = 8;

let ids = 0;

/**
 * @param {object} options
 * @param {string | Node} options.label the button's content
 * @param {string} options.name the menu's accessible name, and the button's when its content
 *   is not text
 * @param {() => ReadonlyArray<MenuEntry>} options.entries what the menu shows, read as it opens
 * @param {string} [options.className] added to the element
 * @returns {Menu}
 */
export function createMenu({ label, name, entries, className = '' }) {
	const id = `menu-${++ids}`;
	const button = el('button', {
		type: 'button', class: 'menu__button', 'aria-haspopup': 'menu', 'aria-expanded': 'false', 'aria-controls': id,
		'aria-label': typeof label === 'string' ? undefined : name
	}, label);
	const list = el('div', { id, class: 'menu__list', role: 'menu', 'aria-label': name, popover: 'auto' });
	button.popoverTargetElement = list;
	const element = el('div', { class: `menu ${className}`.trim() }, button, list);

	/** @returns {HTMLButtonElement[]} */
	const items = () => [...list.querySelectorAll('button')];

	function close() {
		if (list.matches(':popover-open')) list.hidePopover();
	}

	function build() {
		list.replaceChildren(...entries().map(entry => {
			if ('separator' in entry) return el('div', { class: 'menu__separator', role: 'separator' });
			const checkable = entry.checked !== undefined;
			const item = el('button', {
				type: 'button', class: 'menu__item', tabindex: '-1',
				role: checkable ? 'menuitemradio' : 'menuitem',
				'aria-checked': checkable ? String(entry.checked) : undefined
			}, entry.label);
			item.addEventListener('click', () => {
				close();
				button.focus();
				entry.run();
			});
			return item;
		}));
	}

	// below the button, or above it when there is no room below; within the window
	function place() {
		const anchor = button.getBoundingClientRect();
		const room = innerHeight - 2 * EDGE;
		list.style.maxHeight = `${room}px`;
		const { width, height } = list.getBoundingClientRect();
		const below = anchor.bottom + height <= innerHeight - EDGE;
		const top = below ? anchor.bottom : Math.max(EDGE, anchor.top - height);
		list.style.top = `${top}px`;
		list.style.left = `${Math.max(EDGE, Math.min(anchor.left, innerWidth - EDGE - width))}px`;
	}

	list.addEventListener('beforetoggle', event => {
		if (/** @type {ToggleEvent} */ (event).newState === 'open') build();
	});
	list.addEventListener('toggle', event => {
		const open = /** @type {ToggleEvent} */ (event).newState === 'open';
		button.setAttribute('aria-expanded', String(open));
		if (!open) return;
		place();
		const all = items();
		(all.find(item => item.getAttribute('aria-checked') === 'true') ?? all[0])?.focus();
	});
	list.addEventListener('keydown', event => {
		const all = items();
		const at = all.indexOf(/** @type {HTMLButtonElement} */ (document.activeElement));
		/** @type {Record<string, () => number>} */
		const moves = {
			ArrowDown: () => (at + 1) % all.length,
			ArrowUp: () => (at - 1 + all.length) % all.length,
			Home: () => 0,
			End: () => all.length - 1
		};
		if (event.key in moves && all.length > 0) {
			event.preventDefault();
			all[moves[event.key]()].focus();
		} else if (event.key === 'Escape') {
			// the menu's own: the app's Esc would close a dialog the menu is in
			event.preventDefault();
			event.stopPropagation();
			close();
			button.focus();
		} else if (event.key === 'Tab') {
			close();
		}
	});

	return Object.freeze({ element, button, close });
}
