import { test } from 'node:test';
import assert from 'node:assert/strict';
import { roundEven } from '../../../app/js/lib/math.js';

test('halves round to the even integer, as .NET Math.Round does', () => {
	assert.deepEqual([0.5, 1.5, 2.5, -0.5, -1.5, -2.5, -3.5, 3.5].map(roundEven), [0, 2, 2, -0, -2, -2, -4, 4]);
	assert.deepEqual([2.4999, 2.5001, -2.4999, -2.5001, 7, -7].map(roundEven), [2, 3, -2, -3, 7, -7]);
});
