import { test } from 'node:test';
import assert from 'node:assert/strict';
import { ORBIT_LINE_POINTS, PLANETS, planetOrbitLine, planetXyz } from '../../../../app/js/core/astro/planets.js';
import { precessToJ2000 } from '../../../../app/js/core/astro/precession.js';

/** the angle, seen from the Sun, between a planet and the nearest segment of its orbit line */
function offLine(/** @type {Float64Array} */ line, /** @type {{ x: number, y: number, z: number, r: number }} */ p) {
	let best = Infinity;
	for (let k = 0; k + 1 < line.length / 3; k++) {
		const a = [line[3 * k], line[3 * k + 1], line[3 * k + 2]];
		const ab = [line[3 * k + 3] - a[0], line[3 * k + 4] - a[1], line[3 * k + 5] - a[2]];
		const ap = [p.x - a[0], p.y - a[1], p.z - a[2]];
		const t = Math.max(0, Math.min(1, (ab[0] * ap[0] + ab[1] * ap[1] + ab[2] * ap[2]) / (ab[0] ** 2 + ab[1] ** 2 + ab[2] ** 2)));
		best = Math.min(best, Math.hypot(ap[0] - t * ab[0], ap[1] - t * ab[1], ap[2] - t * ab[2]));
	}
	return best / p.r * 180 / Math.PI;
}

test('an orbit line has 301 points, the last on the first', () => {
	for (const planet of PLANETS) {
		const line = planetOrbitLine(planet, 2461332);
		assert.equal(line.length, ORBIT_LINE_POINTS * 3);
		for (let j = 0; j < 3; j++) assert.ok(Math.abs(line[j] - line[line.length - 3 + j]) < 1e-12, planet);
	}
});

test('every planet sits on its orbit line, within Δβ (spec 04, 3D scene geometry)', () => {
	for (const jd of [2341972.5, 2400000.5, 2461332, 2561117]) {
		for (const planet of PLANETS) {
			const off = offLine(planetOrbitLine(planet, jd), planetXyz(planet, jd));
			// Saturn keeps its latitude perturbation of up to 0.04°; the rest is the chords' sag
			assert.ok(off < (planet === 'Saturn' ? 0.045 : 0.005), `${planet} at ${jd}: ${off}°`);
		}
	}
});

test('elements at the J2000 epoch stay as they are', () => {
	assert.deepEqual(precessToJ2000(51544.5, { i: 10, N: 20, w: 30 }), { i: 10, N: 20, w: 30 });
});
