// The last resort for errors nothing else caught: the browser logs them, and the user is told, so
// that one failing part does not leave the page silently broken. docs/spec/12-architecture.md, Errors.

/**
 * @param {unknown} reason
 * @returns {boolean} whether it is a cancellation, which is not an error to show
 */
export function isAbort(reason) {
	return typeof reason === 'object' && reason !== null && /** @type {{ name?: unknown }} */ (reason).name === 'AbortError';
}

/**
 * @param {unknown} reason
 * @returns {string}
 */
function messageOf(reason) {
	if (reason instanceof Error) return reason.message || reason.name;
	return String(reason);
}

/**
 * @param {import('./notifications.js').Notify} notify
 * @param {Window} [target]
 */
export function installErrorHandler(notify, target = window) {
	target.addEventListener('error', event => {
		notify(`Something went wrong: ${messageOf(event.error ?? event.message)}`, { kind: 'error' });
	});
	target.addEventListener('unhandledrejection', event => {
		if (isAbort(event.reason)) {
			event.preventDefault();
			return;
		}
		notify(`Something went wrong: ${messageOf(event.reason)}`, { kind: 'error' });
	});
}
