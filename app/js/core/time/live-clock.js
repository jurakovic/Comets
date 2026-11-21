// The Database view's live clock: an instant shown in whole seconds that a tick moves by exactly
// 1 s, 20 ms past each second boundary; it follows the system clock until a date is picked.
// INTERNALS.md, How the UI is written.

/**
 * @typedef {object} LiveClock
 * @property {number} shownAt the instant shown, UTC milliseconds, whole seconds
 * @property {boolean} following whether it follows the system clock: no date was picked
 */

/** how long after a second boundary a tick runs, as the desktop's sync window (15–35 ms) has it */
export const TICK_AFTER_BOUNDARY = 20;

/**
 * the second a clock following now shows: the current one. Within the 20 ms after a boundary that
 * boundary's tick counts as run, as tickDelay has it
 * @param {number} now UTC milliseconds
 * @returns {number}
 */
export function followedSecond(now) {
	return Math.floor(now / 1000) * 1000;
}

/**
 * a clock showing now, following it
 * @param {number} now UTC milliseconds
 * @returns {Readonly<LiveClock>}
 */
export function createLiveClock(now) {
	return Object.freeze({ shownAt: followedSecond(now), following: true });
}

/**
 * the clock coming back after a pause, or to a newly selected comet: one that follows the system
 * clock shows now again; a picked date stays where it stopped
 * @param {Readonly<LiveClock>} clock
 * @param {number} now UTC milliseconds
 * @returns {Readonly<LiveClock>}
 */
export function resumeLiveClock(clock, now) {
	return clock.following ? createLiveClock(now) : clock;
}

/**
 * a picked date: the clock goes on from it and no longer follows the system clock
 * @param {number} instant UTC milliseconds, whole seconds
 * @returns {Readonly<LiveClock>}
 */
export function pickLiveClock(instant) {
	return Object.freeze({ shownAt: instant, following: false });
}

/**
 * one tick: exactly a second on, however late it ran
 * @param {Readonly<LiveClock>} clock
 * @returns {Readonly<LiveClock>}
 */
export function tickLiveClock(clock) {
	return Object.freeze({ shownAt: clock.shownAt + 1000, following: clock.following });
}

/**
 * milliseconds from now to the next tick, 20 ms past the next second boundary. Within the 20 ms
 * after a boundary its tick counts as run and the next one waits for the boundary after: a coarse
 * clock (Firefox's resistFingerprinting, Tor Browser: 16.7–100 ms) reads a tick's own moment as up
 * to 20 ms early, which would otherwise bring the next tick within milliseconds
 * @param {number} now UTC milliseconds
 * @returns {number} 21–1020
 */
export function tickDelay(now) {
	const delay = (((TICK_AFTER_BOUNDARY - now) % 1000) + 1000) % 1000;
	return delay <= TICK_AFTER_BOUNDARY ? delay + 1000 : delay;
}
