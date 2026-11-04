import { test } from 'node:test';
import assert from 'node:assert/strict';
import { instantToJd, jd0, jdToInstant, jdToParts } from '../../../../app/js/core/time/julian.js';
import { clampToRange, isInRange, MAXIMUM_DATE, MAXIMUM_JD, MINIMUM_DATE, MINIMUM_JD } from '../../../../app/js/core/time/range.js';
import { isoUtc } from '../../../helpers/vectors.js';

test('years before 1 come back as astronomical years (spec 10, D5)', () => {
	// C/-146 P1: JD0 is Gregorian, JDToDateTime gives the Julian calendar date
	assert.deepEqual(jdToParts(jd0(-146, 8, 10, 5000)), { year: -146, month: 8, day: 13, hour: 12, minute: 0, second: 0 });
	assert.equal(isoUtc(jdToInstant(1721422.5)), '0000-12-31T00:00:00Z');
});

test('a Julian 29 February the Gregorian calendar lacks becomes 1 March', () => {
	// 1500-02-29 in the Julian calendar
	assert.deepEqual(jdToParts(2268991.5), { year: 1500, month: 2, day: 29, hour: 0, minute: 0, second: 0 });
	assert.equal(isoUtc(jdToInstant(2268991.5)), '1500-03-01T00:00:00Z');
});

test('hour 24 in the last 0.4 ms of a day becomes the next day (spec 10, D5)', () => {
	assert.deepEqual(jdToParts(2461331.4999999995), { year: 2026, month: 10, day: 17, hour: 24, minute: 0, second: 0 });
	assert.equal(isoUtc(jdToInstant(2461331.4999999995)), '2026-10-18T00:00:00Z');
	assert.equal(isoUtc(jdToInstant(2461331.4999999)), '2026-10-17T23:59:59Z');
});

test('an instant converts to a JD and back to its whole second', () => {
	for (const text of ['1700-01-01T00:00:00Z', '1986-02-09T15:30:45Z', '2026-10-18T23:59:59Z', '2299-12-31T23:59:59Z']) {
		assert.equal(isoUtc(jdToInstant(instantToJd(Date.parse(text)))), text);
	}
});

test('the date range and its JDs', () => {
	assert.equal(MINIMUM_JD, 2341972.5);
	assert.equal(MAXIMUM_JD, 2561117.5);
	assert.equal(clampToRange(MINIMUM_DATE - 1), MINIMUM_DATE);
	assert.equal(clampToRange(MAXIMUM_DATE + 1), MAXIMUM_DATE);
	const inside = Date.parse('2026-10-18T12:00:00Z');
	assert.equal(clampToRange(inside), inside);
	assert.deepEqual([MINIMUM_DATE, MAXIMUM_DATE, MINIMUM_DATE - 1000, MAXIMUM_DATE + 1000].map(isInRange), [true, true, false, false]);
});
