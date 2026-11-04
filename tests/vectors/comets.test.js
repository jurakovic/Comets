// comets.json: the sample collection's record fields and Tn, at the vector clock (spec 02).
// The Perih* and Current* values are checked with the ephemeris.
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertComets } from '../helpers/formats.js';
import { assertClose, assertRelativeClose, isoUtc, readVector, readVectorText, VECTOR_NOW } from '../helpers/vectors.js';
import { createComet, nearestPerihelion } from '../../app/js/core/comets/comet.js';
import { isPeriodic } from '../../app/js/core/comets/designation.js';
import { importElements } from '../../app/js/core/formats/import.js';
import { instantToJd, jdToInstant } from '../../app/js/core/time/julian.js';

const { comets } = readVector('comets.json');
const nowJd = instantToJd(VECTOR_NOW);

/** @param {any} vector */
function fromVector(vector) {
	const { full, id, name, fragment, Ty, Tm, Td, Th, q, e, w, N, i, g, k } = vector;
	const comet = createComet({ full, id, name, fragment, Ty, Tm, Td, Th, q, e, w, N, i, g, k });
	assert.ok(comet, full);
	return comet;
}

test('T, the derived elements, the sort key and isPeriodic of every sample comet', () => {
	assert.equal(comets.length, 34);
	for (const vector of comets) {
		const comet = fromVector(vector);
		assertClose(comet.T, vector.T, 1e-8, `${vector.full} T`);
		for (const key of /** @type {const} */ (['P', 'a', 'Q', 'n'])) {
			assertRelativeClose(comet[key], vector[key], 1e-12, `${vector.full} ${key}`);
		}
		assert.equal(comet.sortkey, vector.sortkey, vector.full);
		assert.equal(isPeriodic(comet.id), vector.isPeriodic, vector.full);
	}
});

test('sample.txt imports to the sample collection, in database order', () => {
	assertComets(importElements([], 'MPC', readVectorText('import/sample.txt')).comets, comets, { label: 'sample' });
});

test('the nearest perihelion and its date', () => {
	for (const vector of comets) {
		const Tn = nearestPerihelion(fromVector(vector), nowJd);
		assertClose(Tn, vector.Tn, 1e-8, `${vector.full} Tn`);
		// the desktop fails for years before 1 (spec 10, D5); the web gives the astronomical year
		const expected = vector.TnDate ?? (vector.full === 'C/-146 P1' ? '-000146-08-13T12:00:00Z' : null);
		assert.equal(isoUtc(jdToInstant(Tn)), expected, `${vector.full} TnDate`);
	}
});

test('the values at the nearest perihelion and now, for the observer at 0, 0', async () => {
	const { currentValues, perihelionValues } = await import('../../app/js/core/comets/observer-values.js');
	const observer = { latitude: 0, longitude: 0 };
	for (const vector of comets) {
		const comet = fromVector(vector);
		const perihelion = perihelionValues(comet, nearestPerihelion(comet, nowJd), observer);
		const current = currentValues(comet, nowJd, observer);
		assertClose(perihelion.PerihEarthDist, vector.PerihEarthDist, 1e-8, `${vector.full} PerihEarthDist`);
		assertClose(perihelion.PerihMag, vector.PerihMag, 1e-8, `${vector.full} PerihMag`);
		assertClose(current.CurrentSunDist, vector.CurrentSunDist, 1e-8, `${vector.full} CurrentSunDist`);
		assertClose(current.CurrentEarthDist, vector.CurrentEarthDist, 1e-8, `${vector.full} CurrentEarthDist`);
		assertClose(current.CurrentMag, vector.CurrentMag, 1e-8, `${vector.full} CurrentMag`);
	}
});
