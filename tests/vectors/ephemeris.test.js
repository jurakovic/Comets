// ephemeris.json: GetEphemeris for every sample comet at 7 dates and 4 observers (spec 04, Ephemeris).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertAngleClose, assertClose, readVector, utcInstant } from '../helpers/vectors.js';
import { ephemeris } from '../../app/js/core/astro/ephemeris.js';
import { instantToJd } from '../../app/js/core/time/julian.js';

const { cases } = readVector('ephemeris.json');
const sample = new Map(readVector('comets.json').comets.map((/** @type {any} */ comet) => [comet.full, comet]));

const ANGLES = /** @type {const} */ (['RA', 'Dec', 'Alt', 'Az', 'EcLon', 'EcLat', 'Elongation', 'PositionAngle']);
const DISTANCES = /** @type {const} */ (['SunDist', 'EarthDist']);

test('every case within the tolerances of spec 11', () => {
	assert.equal(cases.length, 340);
	for (const { comet, utc, observer, ephemeris: expected } of cases) {
		const elements = sample.get(comet);
		assert.ok(elements, comet);
		const jd = instantToJd(utcInstant(utc));
		assertClose(jd, expected.JD, 1e-8, `${comet} ${utc} JD`);
		const record = ephemeris(elements, jd, observer);
		const what = `${comet} at ${utc} from ${observer.name}`;
		for (const key of ANGLES) assertAngleClose(record[key], expected[key], 1e-6, `${what} ${key}`);
		for (const key of DISTANCES) assertClose(record[key], expected[key], 1e-8, `${what} ${key}`);
		assertClose(record.Magnitude, expected.Magnitude, 1e-8, `${what} Magnitude`);
	}
});
