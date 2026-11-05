// The Clock port over the system clock and the browser's time zone.

/** @returns {import('../services/ports.js').Clock} */
export function createSystemClock() {
	return Object.freeze({
		now: () => Date.now(),
		timeZone: () => Intl.DateTimeFormat().resolvedOptions().timeZone
	});
}
