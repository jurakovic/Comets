// The values that depend on the time and the observer: at the nearest perihelion and now. The
// Observations service decides when to compute them. docs/spec/02-data-model.md,
// Observer-dependent values.
import { ephemeris } from '../astro/ephemeris.js';

/**
 * Earth distance and magnitude at the nearest perihelion
 * @param {import('./comet.js').Comet} comet
 * @param {number} Tn from nearestPerihelion
 * @param {import('../astro/ephemeris.js').Observer} observer
 * @returns {{ PerihEarthDist: number, PerihMag: number }}
 */
export function perihelionValues(comet, Tn, observer) {
	const record = ephemeris(comet, Tn, observer);
	return { PerihEarthDist: record.EarthDist, PerihMag: record.Magnitude };
}

/**
 * Sun distance, Earth distance and magnitude now
 * @param {import('./comet.js').Comet} comet
 * @param {number} nowJd the JD of now, its milliseconds dropped
 * @param {import('../astro/ephemeris.js').Observer} observer
 * @returns {{ CurrentSunDist: number, CurrentEarthDist: number, CurrentMag: number }}
 */
export function currentValues(comet, nowJd, observer) {
	const record = ephemeris(comet, nowJd, observer);
	return { CurrentSunDist: record.SunDist, CurrentEarthDist: record.EarthDist, CurrentMag: record.Magnitude };
}
