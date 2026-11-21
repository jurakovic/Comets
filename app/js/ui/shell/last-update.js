// How long ago the last update was ("3 days ago"): one store, which the navigation shows under
// Update and the Settings view as Last update. INTERNALS.md, The shell.
import { daysAgo } from '../../core/format/relative.js';
import { createStore, readonly } from '../../lib/store.js';

/** how often the days are counted again while the page stays open */
const REFRESH_MS = 60 * 60_000;

/**
 * counted again when the settings change, every hour, and when the page shows again, since days
 * pass while the page stays open and timers may not run while it is hidden
 * @param {object} deps
 * @param {Pick<import('../../services/settings.js').SettingsService, 'settings'>} deps.settings
 * @param {import('../../services/ports.js').Clock} deps.clock
 * @returns {import('../../lib/store.js').ReadableStore<string>}
 */
export function createLastUpdate({ settings, clock }) {
	const count = () => daysAgo(settings.settings.get().lastUpdateDate, clock.now());
	const store = createStore(count());
	const refresh = () => store.set(count());
	settings.settings.subscribe(refresh);
	document.addEventListener('visibilitychange', refresh);
	setInterval(refresh, REFRESH_MS);
	return readonly(store);
}
