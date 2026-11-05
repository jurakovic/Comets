// How long ago the last update was, in whole days ("3 days ago"), for the navigation's Update
// action and the Settings view. docs/spec/09-settings-and-persistence.md, Settings.
import { DAY_MS } from '../time/calendar.js';

const DAYS = new Intl.RelativeTimeFormat('en', { numeric: 'auto' });

/**
 * `today`, `yesterday`, `3 days ago`, or `never` before the first update; a date ahead of now (a
 * clock set back) counts as today
 * @param {number | null} then UTC milliseconds
 * @param {number} now UTC milliseconds
 * @returns {string}
 */
export function daysAgo(then, now) {
	if (then === null) return 'never';
	// floor, as start-up counts the days (spec 01, Startup); + 0 turns −0 into 0
	return DAYS.format(-Math.max(0, Math.floor((now - then) / DAY_MS)) + 0, 'day');
}
