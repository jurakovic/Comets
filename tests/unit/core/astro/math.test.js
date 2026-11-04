import { test } from 'node:test';
import assert from 'node:assert/strict';
import { atan2d, log10, obliquity, rev, rev2 } from '../../../../app/js/core/astro/math.js';

test('rev wraps into [0, 360) and rev2 into [−180, 180)', () => {
	assert.deepEqual([0, 360, 720.5, -0.5, -360, 359.999].map(rev), [0, 0, 0.5, 359.5, 0, 359.999]);
	assert.deepEqual([180, -180, 179.5, -179.5, 540, 0].map(rev2), [-180, -180, 179.5, -179.5, -180, 0]);
});

test('degree helpers and the desktop log10', () => {
	assert.equal(atan2d(1, 1), 45);
	assert.ok(Math.abs(log10(1000) - 3) < 1e-15);
	assert.equal(obliquity(0), 23.439291111);
});
