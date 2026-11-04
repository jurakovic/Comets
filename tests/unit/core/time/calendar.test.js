import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	addDays, addHours, addMonths, addYears, civilFromDays, DAY_MS, daysFromCivil, daysInMonth,
	instantToParts, isLeapYear, partsToInstant, startOfDay
} from '../../../../app/js/core/time/calendar.js';
import { isoUtc } from '../../../helpers/vectors.js';

const at = (/** @type {string} */ text) => Date.parse(text);

test('day counts agree with Date over a wide range of years, before 1 included', () => {
	for (let days = -1_200_000; days <= 1_200_000; days += 997) {
		const date = new Date(days * DAY_MS);
		const civil = civilFromDays(days);
		assert.deepEqual(civil, { year: date.getUTCFullYear(), month: date.getUTCMonth() + 1, day: date.getUTCDate() });
		assert.equal(daysFromCivil(civil.year, civil.month, civil.day), days);
	}
});

test('date parts of any year round-trip, and years 0–99 stay themselves', () => {
	for (const parts of [
		{ year: -146, month: 8, day: 13, hour: 12, minute: 0, second: 0 },
		{ year: 0, month: 2, day: 29, hour: 23, minute: 59, second: 59 },
		{ year: 99, month: 12, day: 31, hour: 0, minute: 0, second: 1 },
		{ year: 2026, month: 10, day: 18, hour: 12, minute: 0, second: 0 }
	]) {
		assert.deepEqual(instantToParts(partsToInstant(parts)), parts);
	}
	assert.equal(isoUtc(partsToInstant({ year: 50, month: 1, day: 1, hour: 0, minute: 0, second: 0 })), '0050-01-01T00:00:00Z');
});

test('months outside 1–12 carry into the year', () => {
	const first = (/** @type {number} */ month) => isoUtc(partsToInstant({ year: 2026, month, day: 1, hour: 0, minute: 0, second: 0 }));
	assert.deepEqual([13, 15, 24, 0, -1, -12].map(first), [
		'2027-01-01T00:00:00Z', '2027-03-01T00:00:00Z', '2027-12-01T00:00:00Z',
		'2025-12-01T00:00:00Z', '2025-11-01T00:00:00Z', '2024-12-01T00:00:00Z'
	]);
	assert.equal(daysFromCivil(2026, 14, 31), daysFromCivil(2027, 3, 3));
});

test('milliseconds are dropped towards the earlier second, also before 1970', () => {
	assert.deepEqual(instantToParts(at('1950-01-01T00:00:00Z') - 1),
		{ year: 1949, month: 12, day: 31, hour: 23, minute: 59, second: 59 });
	assert.deepEqual(instantToParts(at('2026-10-18T12:00:00Z') + 999),
		{ year: 2026, month: 10, day: 18, hour: 12, minute: 0, second: 0 });
});

test('leap years and month lengths are Gregorian', () => {
	assert.deepEqual([1600, 1700, 2000, 2024, 2026, 0, -4, -100].map(isLeapYear), [true, false, true, true, false, true, true, false]);
	assert.deepEqual([1, 2, 4, 12].map(month => daysInMonth(2026, month)), [31, 28, 30, 31]);
	assert.equal(daysInMonth(2024, 2), 29);
});

test('adding months clamps the day to the target month and keeps the time of day', () => {
	assert.equal(isoUtc(addMonths(at('2026-01-31T12:00:00Z'), 1)), '2026-02-28T12:00:00Z');
	assert.equal(isoUtc(addMonths(at('2024-01-31T12:00:00Z'), 1)), '2024-02-29T12:00:00Z');
	assert.equal(isoUtc(addMonths(at('2026-03-31T00:00:00Z'), -1)), '2026-02-28T00:00:00Z');
	assert.equal(isoUtc(addMonths(at('2026-11-30T06:00:00Z'), 3)), '2027-02-28T06:00:00Z');
	assert.equal(isoUtc(addMonths(at('2026-01-15T00:00:00Z'), -13)), '2024-12-15T00:00:00Z');
	assert.equal(addMonths(at('2026-01-31T12:00:00.250Z'), 1), at('2026-02-28T12:00:00.250Z'));
	assert.equal(isoUtc(addMonths(partsToInstant({ year: 1, month: 1, day: 31, hour: 0, minute: 0, second: 0 }), -2)), '0000-11-30T00:00:00Z');
});

test('adding years turns 29 February into the 28th in a common year', () => {
	assert.equal(isoUtc(addYears(at('2024-02-29T00:00:00Z'), 1)), '2025-02-28T00:00:00Z');
	assert.equal(isoUtc(addYears(at('2024-02-29T00:00:00Z'), 4)), '2028-02-29T00:00:00Z');
	assert.equal(isoUtc(addYears(at('2024-02-29T00:00:00Z'), -1)), '2023-02-28T00:00:00Z');
});

test('days and hours are fixed lengths', () => {
	assert.equal(isoUtc(addDays(at('2026-12-31T23:30:00Z'), 1)), '2027-01-01T23:30:00Z');
	assert.equal(isoUtc(addHours(at('2026-01-01T00:30:00Z'), -1)), '2025-12-31T23:30:00Z');
	assert.equal(isoUtc(startOfDay(at('1950-06-01T13:14:15Z'))), '1950-06-01T00:00:00Z');
});
