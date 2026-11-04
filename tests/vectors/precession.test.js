// precession.json: PrecessElementsToJ2000 (spec 04, Element precession).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertAngleClose, readVector } from '../helpers/vectors.js';
import { precessToJ2000 } from '../../app/js/core/astro/precession.js';

test('i, N and w precessed to J2000', () => {
	const { cases } = readVector('precession.json');
	assert.equal(cases.length, 6);
	for (const { epochMjd, i, N, w, iJ2000, NJ2000, wJ2000 } of cases) {
		const precessed = precessToJ2000(epochMjd, { i, N, w });
		// computed angles of the NASA import: 1e-9 (spec 11, Tolerances)
		assertAngleClose(precessed.i, iJ2000, 1e-9, `i at ${epochMjd}`);
		assertAngleClose(precessed.N, NJ2000, 1e-9, `N at ${epochMjd}`);
		assertAngleClose(precessed.w, wJ2000, 1e-9, `w at ${epochMjd}`);
	}
});
