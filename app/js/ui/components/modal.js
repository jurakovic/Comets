// A dialog shown as a modal for one use: added to the page, removed when it closes, and the focus
// given back to where it was.

/**
 * @param {HTMLDialogElement} dialog
 * @param {object} [options]
 * @param {HTMLElement} [options.host] where the dialog goes
 * @param {() => void} [options.onClose] once it has closed, by the close this returns or natively
 *   (Esc, a `method="dialog"` form)
 * @returns {() => void} closes it; the focus is back when it returns, as a caller that moves the
 *   focus next relies on, where the `close` event would come a task later
 */
export function openModal(dialog, { host = document.body, onClose } = {}) {
	const previous = document.activeElement;
	let closed = false;
	function close() {
		if (closed) return;
		closed = true;
		if (dialog.open) dialog.close();
		dialog.remove();
		if (previous instanceof HTMLElement && previous.isConnected) previous.focus();
		onClose?.();
	}
	dialog.addEventListener('close', close);
	host.append(dialog);
	dialog.showModal();
	return close;
}
