// orbit-sampling.json: the adaptive orbit sampler in the orbital plane (spec 04, Comet orbit sampling).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertClose, readVector } from '../helpers/vectors.js';
import { sampleOrbit } from '../../app/js/core/astro/orbit-sampling.js';

test('point counts and points of 7 orbits: ellipses closed and open, hyperbolas, a parabola', () => {
	const { cases } = readVector('orbit-sampling.json');
	assert.equal(cases.length, 7);
	for (const { comet, q, e, pointCount, points } of cases) {
		const plane = sampleOrbit(q, e);
		assert.equal(plane.length / 2, pointCount, `${comet} point count`);
		for (const [k, x, y] of points) {
			assertClose(plane[2 * k], x, 1e-9, `${comet} x[${k}]`);
			assertClose(plane[2 * k + 1], y, 1e-9, `${comet} y[${k}]`);
		}
	}
});
