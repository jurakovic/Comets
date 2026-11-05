// Notifications: messages that do not block, in place of the desktop's message boxes for
// information and errors. docs/spec/01-product.md, Messages.
import { el } from '../dom.js';

/** @typedef {'info' | 'error'} NoticeKind */

/**
 * shows a message: information goes after a while, an error stays until dismissed
 * @typedef {(message: string, options?: { kind?: NoticeKind }) => void} Notify
 */

/** at most this many notices at once; the oldest goes first */
const MAX_NOTICES = 4;

/**
 * @param {HTMLElement} region the live region the notices go in, a manual popover: shown again
 *   for each notice, it shows above an open modal dialog, though like the rest of the page it can
 *   be used only once the dialog closes
 * @param {{ timeout?: number }} [options] milliseconds an information notice stays
 * @returns {Notify}
 */
export function createNotifications(region, { timeout = 5000 } = {}) {
	const raise = () => {
		if (region.matches(':popover-open')) region.hidePopover();
		if (region.children.length > 0) region.showPopover();
	};
	return (message, { kind = 'info' } = {}) => {
		const dismiss = el('button', { type: 'button', class: 'shell-notice__dismiss', 'aria-label': 'Dismiss' }, '×');
		const notice = el('div', { class: `shell-notice shell-notice--${kind}`, role: kind === 'error' ? 'alert' : 'status' },
			el('span', { class: 'shell-notice__message' }, message), dismiss);
		const remove = () => {
			notice.remove();
			if (region.children.length === 0 && region.matches(':popover-open')) region.hidePopover();
		};
		dismiss.addEventListener('click', remove);
		region.append(notice);
		while (region.children.length > MAX_NOTICES) region.firstElementChild?.remove();
		raise();
		if (kind === 'info') setTimeout(remove, timeout);
	};
}
