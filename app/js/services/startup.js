// The start-up sequence: the settings and the database loaded, and whether to offer an update.
// docs/spec/01-product.md, Startup.
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
 * what start-up found; the shell asks the question, and on yes opens Update in automatic mode
 * @typedef {object} Startup
 * @property {Readonly<UpdatePrompt> | null} prompt
 * @property {boolean} empty no comets are stored: the Database view offers the download
 */

/**
 * @typedef {object} StartupService
 * @property {() => Promise<Readonly<Startup>>} start
 */

/**
 * @param {object} deps
 * @param {Pick<import('./settings.js').SettingsService, 'load' | 'settings'>} deps.settings
 * @param {Pick<import('./database.js').DatabaseService, 'load' | 'state'>} deps.database
 * @param {import('./ports.js').Clock} deps.clock
 * @returns {StartupService}
 */
export function createStartupService({ settings, database, clock }) {
	return Object.freeze({
		async start() {
			await Promise.all([settings.load(), database.load()]);
			return Object.freeze({
				prompt: updatePrompt(settings.settings.get(), clock.now()),
				empty: database.state.get().comets.length === 0
			});
		}
	});
}
