// The cases of ephemeris-text/index.json in the web app's shapes (docs/spec/06-ephemeris.md).
import assert from 'node:assert/strict';
import { createFakeClock } from './fakes.js';
import { readVector, readVectorText, utcInstant, VECTOR_NOW } from './vectors.js';
import { cometsToCalculate } from '../../app/js/core/ephemeris/calculation.js';
import { importElements } from '../../app/js/core/formats/import.js';
import { createObservationsService } from '../../app/js/services/observations.js';

/** the sample collection in database order */
export const SAMPLE = importElements([], 'MPC', readVectorText('import/sample.txt')).comets;

/** @type {ReadonlyArray<any>} */
export const EPHEMERIS_CASES = readVector('ephemeris-text/index.json').cases;

/**
 * a case's settings, comet selection and observer, and the calculation's input with each
 * comet's Tn at the vector clock
 * @param {any} vector the case's settings
 */
export function ephemerisCase(vector) {
	const total = Math.round(vector.intervalDays * 1440);
	const requirement = (/** @type {number | null} */ value) => ({ checked: value !== null, value });
	const { HelioDist, GeoDist, ...columns } = vector.columns;
	const settings = {
		start: utcInstant(vector.startUtc),
		end: utcInstant(vector.stopUtc),
		interval: { days: Math.floor(total / 1440), hours: Math.floor(total / 60) % 24, minutes: total % 60 },
		localTime: vector.localTime,
		columns: { ...columns, SunDist: HelioDist, EarthDist: GeoDist },
		requirements: {
			maxSunDist: requirement(vector.requirements.maxSunDist),
			maxEarthDist: requirement(vector.requirements.maxEarthDist),
			minMagnitude: requirement(vector.requirements.minMagnitude)
		}
	};
	const selected = vector.multiple ? null : SAMPLE.find(c => c.full === vector.comets) ?? null;
	assert.ok(vector.multiple || selected, vector.comets);
	const selection = { multiple: vector.multiple, comets: SAMPLE, selected };
	const observer = { latitude: vector.location.Latitude, longitude: vector.location.Longitude };
	const observations = createObservationsService({ clock: createFakeClock(VECTOR_NOW), observer: () => observer });
	const comets = cometsToCalculate(selection);
	const input = { comets, nearest: comets.map(observations.nearestPerihelion), settings, observer, timeZone: vector.timeZone };
	return { settings, selection, observer, input };
}
