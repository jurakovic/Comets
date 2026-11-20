// Notifications: messages that do not block, in place of the desktop's message boxes for
// information and errors.
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
 *   for each notice and whenever a modal dialog opens, it shows above the dialog, though like the
 *   rest of the page it can be used only once the dialog closes
 * @param {{ timeout?: number }} [options] milliseconds an information notice stays
 * @returns {Notify}
 */
export function createNotifications(region, { timeout = 5000 } = {}) {
	const raise = () => {
		if (region.matches(':popover-open')) region.hidePopover();
		if (region.children.length > 0) region.showPopover();
	};
	// the top layer stacks in the order of showing, so a dialog opened after the notices covers them
	new MutationObserver(records => {
		if (records.some(({ target }) => target instanceof HTMLDialogElement && target.matches(':modal'))) raise();
	}).observe(region.ownerDocument.body, { subtree: true, attributeFilter: ['open'] });
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
