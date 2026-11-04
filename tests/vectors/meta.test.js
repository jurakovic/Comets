// The frame of reference every vector is computed in (docs/spec/11-test-vectors.md).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertAngleClose, readVector, VECTOR_NOW } from '../helpers/vectors.js';

test('the vectors are for 2026-10-18 12:00 UTC at 0, 0', () => {
	const meta = readVector('meta.json');
	assert.equal(Date.parse(meta.fakeNowUtc), VECTOR_NOW);
	assert.equal(meta.fakeNowJD, 2461332.0);
	assert.equal(meta.timeZone, 'UTC');
	assert.deepEqual([meta.observer.latitude, meta.observer.longitude], [0, 0]);
});

test('angles compare modulo 360', () => {
	assertAngleClose(359.9999995, 0.0000005, 1e-6);
	assert.throws(() => assertAngleClose(10, 10.1, 1e-6));
});
