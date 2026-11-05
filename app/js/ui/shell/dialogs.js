// Confirmation dialogs: modal, answered by a button, closed by Esc as a No. Open dialogs form a
// stack, so the Esc command closes the top one. docs/spec/01-product.md, Messages.
import { el } from '../dom.js';

/**
 * @typedef {object} ConfirmOptions
 * @property {string} message the question
 * @property {string} [yes] the button that answers true
 * @property {string} [no] the button that answers false, as Esc does
 */

/**
 * @typedef {object} Dialogs
 * @property {(options: ConfirmOptions) => Promise<boolean>} confirm
 * @property {() => boolean} isOpen whether a dialog is open
 * @property {() => void} closeTop closes the top dialog as Esc does
 */

/**
 * @param {HTMLElement} host where the dialogs go
 * @returns {Dialogs}
 */
export function createDialogs(host) {
	/** @type {Array<() => void>} the open dialogs' cancels, the top last */
	const open = [];
	let ids = 0;

	return Object.freeze({
		confirm({ message, yes = 'Yes', no = 'No' }) {
			return new Promise(resolve => {
				const previous = document.activeElement;
				const id = `shell-dialog-${++ids}`;
				const yesButton = el('button', { type: 'button', class: 'shell-dialog__button shell-dialog__button--primary' }, yes);
				const noButton = el('button', { type: 'button', class: 'shell-dialog__button' }, no);
				const dialog = el('dialog', { class: 'shell-dialog', role: 'alertdialog', 'aria-modal': 'true', 'aria-labelledby': id },
					el('p', { id, class: 'shell-dialog__message' }, message),
					el('div', { class: 'shell-dialog__actions' }, yesButton, noButton));
				let answered = false;
				/** @param {boolean} value */
				const answer = value => {
					if (answered) return;
					answered = true;
					open.splice(open.indexOf(cancel), 1);
					dialog.close();
					dialog.remove();
					if (previous instanceof HTMLElement && previous.isConnected) previous.focus();
					resolve(value);
				};
				const cancel = () => answer(false);
				yesButton.addEventListener('click', () => answer(true));
				noButton.addEventListener('click', cancel);
				// the browser's own Esc, for when the key never reaches the command
				dialog.addEventListener('cancel', event => {
					event.preventDefault();
					cancel();
				});
				open.push(cancel);
				host.append(dialog);
				dialog.showModal();
				yesButton.focus();
			});
		},
		isOpen: () => open.length > 0,
		closeTop: () => { open.at(-1)?.(); }
	});
}
