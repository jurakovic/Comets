// The session cache of the values that depend on the clock and the observer: Tn, Perih*,
// Current*. docs/spec/02-data-model.md, Observer-dependent values.
import { nearestPerihelion } from '../core/comets/comet.js';
import { currentValues, perihelionValues } from '../core/comets/observer-values.js';
import { fieldValue } from '../core/comets/properties.js';
import { instantToJd } from '../core/time/julian.js';

/** @typedef {import('../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../core/astro/ephemeris.js').Observer} Observer */

/** Current* are recomputed once their ephemeris is more than this old (`MinimumMinutesForRecalculate`) */
export const CURRENT_REFRESH_MS = 5 * 60_000;

/**
 * @typedef {object} ObservationsService
 * @property {import('../core/comets/properties.js').ValueAccessor} value a property's value: the
 *   session values from the cache, the rest from the comet
 * @property {(comet: Comet) => number} nearestPerihelion `Tn`, kept from its first use
 */

/**
 * @param {object} deps
 * @param {import('./ports.js').Clock} deps.clock
 * @param {() => Observer} deps.observer the location from settings, read whenever a value needs it
 * @returns {ObservationsService}
 */
export function createObservationsService({ clock, observer }) {
	// by comet record: a re-imported comet is a new record and starts afresh, as on the desktop
	/** @type {WeakMap<Comet, number>} */
	const nearest = new WeakMap();
	/** @type {WeakMap<Comet, ReturnType<typeof perihelionValues>>} */
	const perihelion = new WeakMap();
	/** @type {WeakMap<Comet, { at: number, values: ReturnType<typeof currentValues> }>} */
	const current = new WeakMap();

	// Tn and Perih* keep the clock and the location of their first use for the session (spec 10, Q11)
	/** @param {Comet} comet */
	function tn(comet) {
		let value = nearest.get(comet);
		if (value === undefined) {
			value = nearestPerihelion(comet, instantToJd(clock.now()));
			nearest.set(comet, value);
		}
		return value;
	}

	/** @param {Comet} comet */
	function atPerihelion(comet) {
		let values = perihelion.get(comet);
		if (!values) {
			values = perihelionValues(comet, tn(comet), observer());
			perihelion.set(comet, values);
		}
		return values;
	}

	/** @param {Comet} comet */
	function now(comet) {
		const at = clock.now();
		let entry = current.get(comet);
		if (!entry || at - entry.at > CURRENT_REFRESH_MS) {
			entry = { at, values: currentValues(comet, instantToJd(at), observer()) };
			current.set(comet, entry);
		}
		return entry.values;
	}

	/** @type {Readonly<Record<string, (comet: Comet) => number>>} */
	const SESSION = Object.freeze({
		Tn: tn,
		PerihEarthDist: c => atPerihelion(c).PerihEarthDist,
		PerihMag: c => atPerihelion(c).PerihMag,
		CurrentSunDist: c => now(c).CurrentSunDist,
		CurrentEarthDist: c => now(c).CurrentEarthDist,
		CurrentMag: c => now(c).CurrentMag
	});

	return Object.freeze({
		value(/** @type {Comet} */ comet, /** @type {string} */ key) {
			return Object.hasOwn(SESSION, key) ? SESSION[key](comet) : fieldValue(comet, key);
		},
		nearestPerihelion: tn
	});
}
