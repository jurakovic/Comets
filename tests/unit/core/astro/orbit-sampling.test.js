import { test } from 'node:test';
import assert from 'node:assert/strict';
import { cometOrbitLine, dateRotation, orbitRotation, sampleOrbit } from '../../../../app/js/core/astro/orbit-sampling.js';
import { orbitalPlane } from '../../../../app/js/core/astro/kepler.js';
import { cometXyz } from '../../../../app/js/core/astro/position.js';

const halley = { T: 2474038.0218, q: 0.570945, e: 0.96803, w: 112.1616, N: 59.261, i: 162.1917 };

/** @param {readonly number[]} m @param {number[]} v */
const apply = (m, v) => [0, 1, 2].map(row => m[3 * row] * v[0] + m[3 * row + 1] * v[1] + m[3 * row + 2] * v[2]);

test('the rotations are orthonormal', () => {
	for (const m of [orbitRotation(halley), dateRotation(2461332)]) {
		for (let a = 0; a < 3; a++) {
			for (let b = 0; b < 3; b++) {
				const dot = m[a] * m[b] + m[3 + a] * m[3 + b] + m[6 + a] * m[6 + b];
				assert.ok(Math.abs(dot - (a === b ? 1 : 0)) < 1e-15);
			}
		}
	}
});

test('the orbit and date rotations put the plane position where CometXyz puts the comet', () => {
	for (const jd of [2446470.5, 2461332, 2474038.0218]) {
		const { v, r } = orbitalPlane(halley.q, halley.e, jd - halley.T);
		const scene = apply(dateRotation(jd), apply(orbitRotation(halley), [r * Math.cos(v), r * Math.sin(v), 0]));
		const position = cometXyz(halley, jd);
		for (const [k, value] of [position.x, position.y, position.z].entries()) {
			assert.ok(Math.abs(scene[k] - value) < 1e-12, `${jd}: ${scene[k]} vs ${value}`);
		}
	}
});

test('a line is every plane sample rotated, from aphelion round to aphelion for a closed orbit', () => {
	const plane = sampleOrbit(halley.q, halley.e);
	const line = cometOrbitLine(halley);
	const rotation = orbitRotation(halley);
	assert.equal(line.length, plane.length / 2 * 3);
	for (let k = 0; k < plane.length / 2; k++) {
		const expected = apply(rotation, [plane[2 * k], plane[2 * k + 1], 0]);
		for (let j = 0; j < 3; j++) assert.ok(Math.abs(line[3 * k + j] - expected[j]) < 1e-12, `point ${k}`);
	}
	const a = halley.q / (1 - halley.e);
	assert.ok(Math.abs(plane[plane.length - 2] + a * (1 + halley.e)) < 1e-12);
});

test('open orbits are symmetric about perihelion and end 500 AU out', () => {
	for (const [q, e] of [[1.356507, 6.139884], [0.812345, 1], [5.824775, 0.999655]]) {
		const plane = sampleOrbit(q, e);
		const n = plane.length;
		assert.ok(Math.abs(plane[0] - plane[n - 2]) < 1e-9 && Math.abs(plane[1] + plane[n - 1]) < 1e-9, `${q} ${e}`);
		assert.ok(Math.abs(Math.hypot(plane[n - 2], plane[n - 1]) - 500) < 1e-6, `${q} ${e}`);
	}
});
