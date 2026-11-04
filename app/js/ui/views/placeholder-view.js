// A view still to be built: its title, so the shell can be used end to end.
import { el } from '../dom.js';

/**
 * @param {string} title
 * @param {string} key
 * @returns {import('../shell/router.js').View}
 */
export function createPlaceholderView(title, key) {
	const element = el('section', { class: 'placeholder-view', 'data-view': key, 'aria-labelledby': `view-title-${key}` },
		el('h1', { id: `view-title-${key}` }, title),
		el('p', {}, 'Not built yet.'));
	return { element, show() {}, hide() {} };
}
