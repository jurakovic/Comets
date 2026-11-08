// The Database view's live clock: an instant shown in whole seconds that a tick moves by exactly
// 1 s, 20 ms past each second boundary; it follows the system clock until a date is picked.
// docs/spec/05-database-and-update.md, Details: Ephemeris tab.

/**
 * @typedef {object} LiveClock
 * @property {number} shownAt the instant shown, UTC milliseconds, whole seconds
 * @property {boolean} following whether it follows the system clock: no date was picked
 */

/** how long after a second boundary a tick runs, as the desktop's sync window (15–35 ms) has it */
export const TICK_AFTER_BOUNDARY = 20;

/**
 * the second a clock following now shows: the one whose tick has run, so a tick due within the
 * 20 ms after a boundary still moves it on to that boundary's second
 * @param {number} now UTC milliseconds
 * @returns {number}
 */
export function followedSecond(now) {
	return Math.floor((now - TICK_AFTER_BOUNDARY) / 1000) * 1000;
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
 * milliseconds from now to the next tick, 20 ms past the next second boundary
 * @param {number} now UTC milliseconds
 * @returns {number} 1–1000
 */
export function tickDelay(now) {
	const delay = (((TICK_AFTER_BOUNDARY - now) % 1000) + 1000) % 1000;
	return delay === 0 ? 1000 : delay;
}
