// planets.json: planet elements, perturbations and positions at 8 dates (spec 04, Planets).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertClose, readVector } from '../helpers/vectors.js';
import { perturbations, planetElements, planetXyz, PLANETS } from '../../app/js/core/astro/planets.js';

const { cases } = readVector('planets.json');

test('elements, perturbations and positions of every planet', () => {
	assert.equal(cases.length, 8);
	for (const { utc, jd, d, planets } of cases) {
		assert.deepEqual(planets.map((/** @type {any} */ p) => p.planet), PLANETS);
		for (const { planet, elements, perturbation, x, y, z, r } of planets) {
			const what = `${planet} at ${utc}`;
			const actual = planetElements(planet, d);
			for (const key of /** @type {const} */ (['N', 'i', 'w', 'a', 'e', 'M'])) {
				assertClose(actual[key], elements[key], 1e-9, `${what} ${key}`);
			}
			const { dLon, dLat } = perturbations(planet, d);
			assertClose(dLon, perturbation.dLon, 1e-9, `${what} dLon`);
			assertClose(dLat, perturbation.dLat, 1e-9, `${what} dLat`);
			const position = planetXyz(planet, jd);
			assertClose(position.x, x, 1e-8, `${what} x`);
			assertClose(position.y, y, 1e-8, `${what} y`);
			assertClose(position.z, z, 1e-8, `${what} z`);
			assertClose(position.r, r, 1e-8, `${what} r`);
		}
	}
});
