import { test } from 'node:test';
import assert from 'node:assert/strict';
import { traceCurve } from '../../../../app/js/core/graph/curve.js';

/**
 * what a curve traces, as text
 * @param {number[]} xs
 * @param {number[]} ys
 * @param {number} [tension]
 */
function trace(xs, ys, tension) {
	/** @type {Array<[string, ...number[]]>} */
	const calls = [];
	traceCurve({
		moveTo: (...args) => calls.push(['M', ...args]),
		bezierCurveTo: (...args) => calls.push(['C', ...args])
	}, xs, ys, tension);
	return calls;
}

test('points on a line: control points on it too, so the curve is straight', () => {
	assert.deepEqual(trace([0, 3, 6, 9], [0, 3, 6, 9]), [
		['M', 0, 0],
		['C', 0.5, 0.5, 2, 2, 3, 3],
		['C', 4, 4, 5, 5, 6, 6],
		['C', 7, 7, 8.5, 8.5, 9, 9]
	]);
});

test('a sixth of the neighbours\' difference at tension 0.5, the one-sided difference at the ends', () => {
	const [, first, second] = trace([0, 6, 12], [0, 12, 0]);
	// tangents: (6, 12) / 6 at the start, (12, 0) / 6 in the middle, (6, −12) / 6 at the end
	assert.deepEqual(first, ['C', 1, 2, 4, 12, 6, 12]);
	assert.deepEqual(second, ['C', 8, 12, 11, 2, 12, 0]);
});

test('tension 0 traces straight segments', () => {
	assert.deepEqual(trace([0, 6, 12], [0, 12, 0], 0), [['M', 0, 0], ['C', 0, 0, 6, 12, 6, 12], ['C', 6, 12, 12, 0, 12, 0]]);
});

test('a point that is not finite breaks the curve; a lone point traces nothing to stroke', () => {
	assert.deepEqual(trace([0, 3, 4, 6, 9, 10], [0, 3, NaN, 6, 9, Infinity]), [
		['M', 0, 0], ['C', 0.5, 0.5, 2.5, 2.5, 3, 3],
		['M', 6, 6], ['C', 6.5, 6.5, 8.5, 8.5, 9, 9]
	]);
	assert.deepEqual(trace([5], [5]), [['M', 5, 5]]);
	assert.deepEqual(trace([], []), []);
});
