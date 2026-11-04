import { test } from 'node:test';
import assert from 'node:assert/strict';
import { ellipse, hyperbola, orbitalPlane, parabola } from '../../../../app/js/core/astro/kepler.js';
import { GAUSS_K } from '../../../../app/js/core/astro/math.js';

const close = (/** @type {number} */ a, /** @type {number} */ b, tolerance = 1e-9) => assert.ok(Math.abs(a - b) <= tolerance, `${a} ≠ ${b}`);

test('every branch is at perihelion at T', () => {
	for (const e of [0, 0.5, 0.967, 1, 1.0001, 6.1]) {
		const { v, r } = orbitalPlane(0.6, e, 0);
		close(v, 0);
		close(r, 0.6);
	}
});

test('an ellipse reaches aphelion after half a period', () => {
	const q = 0.585978;
	const e = 0.967143;
	const a = q / (1 - e);
	const halfPeriod = Math.PI * a * Math.sqrt(a) / GAUSS_K;
	const { v, r } = ellipse(q, e, halfPeriod);
	close(Math.abs(v), Math.PI, 1e-6);
	close(r, a * (1 + e), 1e-6);
});

test('before and after perihelion mirror each other', () => {
	for (const [q, e] of [[0.39, 1.000102], [1.36, 6.14]]) {
		const after = hyperbola(q, e, 40);
		const before = hyperbola(q, e, -40);
		close(before.v, -after.v);
		close(before.r, after.r);
	}
	close(parabola(0.8, -55).v, -parabola(0.8, 55).v);
});

test('the solvers stop when they cannot converge (spec 10, D7)', () => {
	for (const position of [ellipse(1, 0.5, NaN), hyperbola(1, 1.5, NaN), parabola(1, NaN)]) {
		assert.ok(Number.isNaN(position.v));
	}
});
