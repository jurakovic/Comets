// The navigation bar's links, from the route table, marking the route shown
// (docs/spec/01-product.md, Shell).
import { el } from '../dom.js';

/**
 * @param {readonly import('./routes.js').Route[]} routes
 * @param {import('../../lib/store.js').ReadableStore<string | null>} current the key of the route shown
 * @param {Readonly<Record<string, import('../../lib/store.js').ReadableStore<string>>>} [notes] a line
 *   shown under a route's title, by route key: how long ago the last update was
 */
export function createNav(routes, current, notes = {}) {
	/** @type {Map<string, HTMLAnchorElement>} */
	const links = new Map();
	/** @param {'views' | 'app'} group */
	const list = group => el('ul', { class: `shell-nav__group shell-nav__group--${group}` },
		...routes.filter(route => route.group === group).map(route => {
			const link = el('a', { class: 'shell-nav__link', href: `#/${route.key}` }, route.title);
			const note = notes[route.key];
			if (note) {
				const text = el('span', { class: 'shell-nav__note' });
				const show = (/** @type {string} */ value) => { text.textContent = value; };
				show(note.get());
				note.subscribe(show);
				link.append(text);
			}
			links.set(route.key, link);
			return el('li', {}, link);
		}));
	const element = el('div', { class: 'shell-nav__groups' }, list('views'), list('app'));
	element.style.display = 'contents';

	/** @param {string | null} key */
	function mark(key) {
		for (const [routeKey, link] of links) {
			if (routeKey === key) link.setAttribute('aria-current', 'page');
			else link.removeAttribute('aria-current');
		}
	}
	mark(current.get());
	current.subscribe(mark);
	return { element };
}
