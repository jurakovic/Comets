import { test } from 'node:test';
import assert from 'node:assert/strict';
import { localPartsIn } from '../../../../app/js/core/time/local.js';

const at = (/** @type {string} */ text) => Date.parse(text);

test('local time follows the zone\'s daylight-saving rules for the instant', () => {
	const zagreb = localPartsIn('Europe/Zagreb');
	assert.deepEqual(zagreb(at('2024-10-27T00:59:59Z')), { year: 2024, month: 10, day: 27, hour: 2, minute: 59, second: 59 });
	assert.deepEqual(zagreb(at('2024-10-27T01:00:00Z')), { year: 2024, month: 10, day: 27, hour: 2, minute: 0, second: 0 });
	assert.deepEqual(zagreb(at('2026-01-15T23:30:00Z')), { year: 2026, month: 1, day: 16, hour: 0, minute: 30, second: 0 });
	const sydney = localPartsIn('Australia/Sydney');
	assert.deepEqual(sydney(at('2026-10-18T12:00:00Z')), { year: 2026, month: 10, day: 18, hour: 23, minute: 0, second: 0 });
});

test('UTC, midnight as hour 0, milliseconds dropped, and years before 1', () => {
	const utc = localPartsIn('UTC');
	assert.deepEqual(utc(at('2026-10-18T00:00:00.999Z')), { year: 2026, month: 10, day: 18, hour: 0, minute: 0, second: 0 });
	assert.deepEqual(utc(new Date(0).setUTCFullYear(-146, 7, 13)), { year: -146, month: 8, day: 13, hour: 0, minute: 0, second: 0 });
	assert.deepEqual(utc(new Date(0).setUTCFullYear(0, 0, 1)), { year: 0, month: 1, day: 1, hour: 0, minute: 0, second: 0 });
});

test('an unknown zone is refused', () => {
	assert.throws(() => localPartsIn('Mars/Olympus'), RangeError);
});
