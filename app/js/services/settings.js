// The app's settings: their defaults, the rules each value follows, the versioned JSON they are
// stored as, and the service that owns them. docs/spec/09-settings-and-persistence.md.
import { createStore, readonly } from '../lib/store.js';
import { invalid, valid } from '../core/validation.js';

/** where the settings are stored (spec 09, Storage layout) */
export const SETTINGS_KEY = 'comets.settings';

/** the MPC's elements file, which Restore default puts back */
export const DEFAULT_DOWNLOAD_URL = 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt';

/** @typedef {'light' | 'dark' | 'system'} Theme */

/** the theme menu, in its order */
export const THEMES = /** @type {ReadonlyArray<Theme>} */ (Object.freeze(['light', 'dark', 'system']));

/** the update interval's field: whole days */
export const UPDATE_INTERVAL = Object.freeze({ min: 1, max: 99, decimals: 0 });

/**
 * @typedef {object} Location
 * @property {string} name
 * @property {number} latitude degrees, −90 to 90, south negative
 * @property {number} longitude degrees, −180 to 180, west negative
 */

/**
 * @typedef {object} Settings
 * @property {Theme} theme
 * @property {string} downloadUrl
 * @property {boolean} automaticUpdate
 * @property {number | null} updateInterval whole days, 1–99; null while the field is empty
 * @property {number | null} lastUpdateDate UTC milliseconds of the last imported download; null before one
 * @property {boolean} showLongCalculationConfirmation
 * @property {boolean} showDeleteCometConfirmation
 * @property {Readonly<Location>} location the observer
 */

/** @typedef {Partial<Omit<Settings, 'location'>> & { location?: Partial<Location> }} SettingsPatch */

/** @type {Readonly<Settings>} */
export const DEFAULT_SETTINGS = Object.freeze({
	theme: 'system',
	downloadUrl: DEFAULT_DOWNLOAD_URL,
	automaticUpdate: true,
	updateInterval: 7,
	lastUpdateDate: null,
	showLongCalculationConfirmation: true,
	showDeleteCometConfirmation: true,
	location: Object.freeze({ name: 'Home', latitude: 0, longitude: 0 })
});

/** @param {unknown} value */
const isBoolean = value => typeof value === 'boolean';
/** @param {unknown} value */
const isString = value => typeof value === 'string';
/**
 * @param {number} min
 * @param {number} max
 * @returns {(value: unknown) => boolean}
 */
const isNumberIn = (min, max) => value => typeof value === 'number' && value >= min && value <= max;

/**
 * what each value may be; anything else is a programmer's error in an update, and the default
 * when read from storage
 * @type {Readonly<Record<Exclude<keyof Settings, 'location'>, (value: unknown) => boolean>>}
 */
const RULES = Object.freeze({
	theme: value => THEMES.includes(/** @type {Theme} */ (value)),
	downloadUrl: isString,
	automaticUpdate: isBoolean,
	updateInterval: value => value === null || (Number.isInteger(value) && isNumberIn(UPDATE_INTERVAL.min, UPDATE_INTERVAL.max)(value)),
	lastUpdateDate: value => value === null || Number.isFinite(value),
	showLongCalculationConfirmation: isBoolean,
	showDeleteCometConfirmation: isBoolean
});

/** @type {Readonly<Record<keyof Location, (value: unknown) => boolean>>} */
const LOCATION_RULES = Object.freeze({
	name: isString,
	latitude: isNumberIn(-90, 90),
	longitude: isNumberIn(-180, 180)
});

/**
 * the checks of the settings as a whole, in the order the Settings view shows them; each names
 * the setting it belongs to
 * @param {Readonly<Settings>} settings
 * @returns {import('../core/validation.js').Validation<Readonly<Settings>>}
 */
export function validateSettings(settings) {
	if (settings.automaticUpdate && settings.updateInterval === null) {
		return invalid([{ field: 'updateInterval', message: 'Please enter days interval for automatic update' }]);
	}
	return valid(settings);
}

/**
 * the settings with a change applied
 * @param {Readonly<Settings>} settings
 * @param {SettingsPatch} patch
 * @returns {Readonly<Settings>}
 * @throws {Error} for an unknown setting or a value its rule refuses: the UI's inputs keep to them
 */
export function patchSettings(settings, patch) {
	const { location = {}, ...rest } = patch;
	for (const [key, value] of Object.entries(rest)) {
		if (!Object.hasOwn(RULES, key)) throw new Error(`unknown setting ${key}`);
		if (!RULES[/** @type {keyof typeof RULES} */ (key)](value)) throw new Error(`${key} cannot be ${String(value)}`);
	}
	for (const [key, value] of Object.entries(location)) {
		if (!Object.hasOwn(LOCATION_RULES, key)) throw new Error(`unknown setting location.${key}`);
		if (!LOCATION_RULES[/** @type {keyof Location} */ (key)](value)) throw new Error(`location.${key} cannot be ${String(value)}`);
	}
	return Object.freeze({ ...settings, ...rest, location: Object.freeze({ ...settings.location, ...location }) });
}

/**
 * Upgrades of the stored JSON: entry v − 1 turns version v into version v + 1. A change to the
 * stored shape adds an entry here, which raises SETTINGS_VERSION.
 * @type {ReadonlyArray<(stored: Record<string, unknown>) => Record<string, unknown>>}
 */
export const MIGRATIONS = Object.freeze([]);

/** the version the settings are written in */
export const SETTINGS_VERSION = MIGRATIONS.length + 1;

const ISO_UTC = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d{1,3})?Z$/;

/**
 * the stored last update: null, or an ISO 8601 UTC text
 * @param {unknown} value
 * @returns {number | null} the instant; NaN, which the rule refuses, for anything else
 */
function readInstant(value) {
	if (value === null) return null;
	return typeof value === 'string' && ISO_UTC.test(value) ? Date.parse(value) : NaN;
}

/**
 * @param {unknown} value
 * @returns {value is Record<string, unknown>}
 */
const isRecord = value => typeof value === 'object' && value !== null && !Array.isArray(value);

/**
 * the settings stored as text: each value that its rule accepts, the default for the rest; the
 * defaults when there is no text or it is not settings JSON. Older versions are migrated first; a
 * newer version's values are read where they follow this version's rules.
 * @param {string | undefined} text
 * @param {ReadonlyArray<(stored: Record<string, unknown>) => Record<string, unknown>>} [migrations]
 * @returns {Readonly<Settings>}
 */
export function readSettings(text, migrations = MIGRATIONS) {
	if (text === undefined) return DEFAULT_SETTINGS;
	/** @type {unknown} */
	let stored;
	try {
		stored = JSON.parse(text);
	} catch {
		return DEFAULT_SETTINGS;
	}
	if (!isRecord(stored) || !Number.isInteger(stored.version) || Number(stored.version) < 1) return DEFAULT_SETTINGS;
	for (let version = Number(stored.version); version <= migrations.length; version++) {
		stored = migrations[version - 1](stored);
		if (!isRecord(stored)) return DEFAULT_SETTINGS;
	}
	/** @type {Record<string, unknown>} */
	const values = { ...stored, lastUpdateDate: readInstant(stored.lastUpdateDate) };
	const location = isRecord(stored.location) ? stored.location : {};
	/** @type {Settings} */
	const settings = {
		...DEFAULT_SETTINGS,
		...Object.fromEntries(Object.entries(RULES).filter(([key, rule]) => rule(values[key])).map(([key]) => [key, values[key]])),
		location: Object.freeze({
			...DEFAULT_SETTINGS.location,
			...Object.fromEntries(Object.entries(LOCATION_RULES).filter(([key, rule]) => rule(location[key])).map(([key]) => [key, location[key]]))
		})
	};
	// values that are each fine but not together: the defaults of the settings at fault
	const validation = validateSettings(settings);
	if (!validation.ok) {
		for (const { field } of validation.errors) Object.assign(settings, { [field]: DEFAULT_SETTINGS[/** @type {keyof Settings} */ (field)] });
	}
	return Object.freeze(settings);
}

/**
 * the settings as stored: JSON with the version, `lastUpdateDate` as an ISO 8601 UTC text
 * @param {Readonly<Settings>} settings
 * @returns {string}
 */
export function writeSettings(settings) {
	const { lastUpdateDate } = settings;
	return JSON.stringify({
		version: SETTINGS_VERSION,
		...settings,
		lastUpdateDate: lastUpdateDate === null ? null : new Date(lastUpdateDate).toISOString()
	});
}

/**
 * @typedef {object} SettingsService
 * @property {import('../lib/store.js').ReadableStore<Readonly<Settings>>} settings the defaults until load
 * @property {() => Promise<void>} load reads the stored settings
 * @property {(patch: SettingsPatch) => Promise<import('../core/validation.js').Validation<Readonly<Settings>>>}
 *   update applies a change that validates at once, and resolves once it is saved; one that does
 *   not changes nothing. Text comes as the user typed it: the view trims what the desktop trims.
 * @property {() => Promise<void>} reset back to the defaults, with nothing stored (Clear all data)
 */

/**
 * @param {object} deps
 * @param {import('./ports.js').KeyValueStore} deps.store localStorage
 * @returns {SettingsService}
 */
export function createSettingsService({ store }) {
	const state = createStore(DEFAULT_SETTINGS);
	// writes in the order they were made, so the last change is the one stored
	/** @type {Promise<void>} */
	let writing = Promise.resolve();
	/** @param {() => Promise<void>} write */
	const queue = write => (writing = writing.then(write));

	return Object.freeze({
		settings: readonly(state),
		async load() {
			state.set(readSettings(await store.get(SETTINGS_KEY)));
		},
		async update(/** @type {SettingsPatch} */ patch) {
			const current = state.get();
			const next = patchSettings(current, patch);
			const validation = validateSettings(next);
			if (!validation.ok) return validation;
			const text = writeSettings(next);
			if (text === writeSettings(current)) return valid(current);
			state.set(next);
			await queue(() => store.put(SETTINGS_KEY, text));
			return validation;
		},
		async reset() {
			state.set(DEFAULT_SETTINGS);
			await queue(() => store.remove(SETTINGS_KEY));
		}
	});
}
