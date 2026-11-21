// The start-up sequence: the settings and the database loaded, and whether to offer an update.
import { DAY_MS } from '../core/time/calendar.js';

/** @typedef {import('./settings.js').Settings} Settings */

/**
 * @typedef {object} UpdatePrompt
 * @property {number} days whole days since the last update
 * @property {string} message the question to ask
 */

/**
 * the update question at start-up (`FormMain_Shown`): asked when automatic update is on, a
 * previous update is stored, and the whole days since it reach the update interval
 * @param {Readonly<Settings>} settings
 * @param {number} now UTC milliseconds
 * @returns {Readonly<UpdatePrompt> | null}
 */
export function updatePrompt({ automaticUpdate, lastUpdateDate, updateInterval }, now) {
	if (!automaticUpdate || lastUpdateDate === null || updateInterval === null) return null;
	const days = Math.floor((now - lastUpdateDate) / DAY_MS);
	if (days < updateInterval) return null;
	return Object.freeze({ days, message: `Last update was ${days} days ago. Update now?` });
}

/**
 * what start-up found; the shell reports the failures, asks the question, and on yes opens Update
 * in automatic mode. The Database view offers the download once the database has loaded empty
 * @typedef {object} Startup
 * @property {Readonly<UpdatePrompt> | null} prompt
 * @property {ReadonlyArray<unknown>} failures why a load failed, the settings' or the database's:
 *   the question is asked all the same, from the settings as they are then
 */

/**
 * @typedef {object} StartupService
 * @property {() => Promise<Readonly<Startup>>} start
 */

/**
 * @param {object} deps
 * @param {Pick<import('./settings.js').SettingsService, 'load' | 'settings'>} deps.settings
 * @param {Pick<import('./database.js').DatabaseService, 'load'>} deps.database
 * @param {import('./ports.js').Clock} deps.clock
 * @returns {StartupService}
 */
export function createStartupService({ settings, database, clock }) {
	return Object.freeze({
		async start() {
			// a database that could not be read is when an update is needed most
			const loads = await Promise.allSettled([settings.load(), database.load()]);
			const failures = loads.flatMap(load => (load.status === 'rejected' ? [load.reason] : []));
			return Object.freeze({ prompt: updatePrompt(settings.settings.get(), clock.now()), failures: Object.freeze(failures) });
		}
	});
}
