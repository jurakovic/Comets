import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	compileDateFormat, DATE_EPHEMERIS, DATE_FILENAME, DATE_FULL, DATE_GRAPH_LONG, DATE_GRAPH_SHORT, DATE_PERIHELION, formatDate
} from '../../../../app/js/core/format/dates.js';
import { instantToParts } from '../../../../app/js/core/time/calendar.js';

const parts = instantToParts(Date.parse('2024-09-27T20:00:31Z'));

test('the desktop date formats', () => {
	assert.equal(formatDate(parts, DATE_FULL), '27.09.2024. 20:00:31');
	assert.equal(formatDate(parts, DATE_PERIHELION), '27 Sep 2024 20:00:31');
	assert.equal(formatDate(parts, DATE_EPHEMERIS), '27.09.2024 20:00');
	assert.equal(formatDate(parts, DATE_GRAPH_LONG), '27 Sep 2024 20:00');
	assert.equal(formatDate(parts, DATE_GRAPH_SHORT), '27 Sep 2024');
	assert.equal(formatDate(parts, DATE_FILENAME), '2024-09-27_200031');
});

test('every month abbreviation, and padded fields', () => {
	const months = Array.from({ length: 12 }, (_, i) => formatDate({ year: 2026, month: i + 1, day: 1, hour: 0, minute: 0, second: 0 }, 'MMM'));
	assert.deepEqual(months, ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']);
	assert.equal(formatDate({ year: 7, month: 2, day: 3, hour: 4, minute: 5, second: 6 }, DATE_FULL), '03.02.0007. 04:05:06');
});

test('astronomical years ≤ 0 keep four digits after their sign (spec 10, D5)', () => {
	assert.equal(formatDate({ year: -146, month: 8, day: 13, hour: 12, minute: 0, second: 0 }, DATE_PERIHELION), '13 Aug -0146 12:00:00');
	assert.equal(formatDate({ year: 0, month: 1, day: 1, hour: 0, minute: 0, second: 0 }, DATE_GRAPH_SHORT), '01 Jan 0000');
});

test('a compiled format can be reused, and unknown fields are programmer errors', () => {
	const row = compileDateFormat(DATE_EPHEMERIS);
	assert.equal(row(parts), '27.09.2024 20:00');
	assert.throws(() => compileDateFormat('dd/MM/yyyy'));
	assert.throws(() => compileDateFormat('d.M.yy'));
});
