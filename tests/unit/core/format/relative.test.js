import { test } from 'node:test';
import assert from 'node:assert/strict';
import { daysAgo } from '../../../../app/js/core/format/relative.js';

const NOW = Date.UTC(2026, 9, 18, 12);
const DAY = 86_400_000;

test('whole days since the last update', () => {
	assert.equal(daysAgo(null, NOW), 'never');
	assert.equal(daysAgo(NOW, NOW), 'today');
	assert.equal(daysAgo(NOW - DAY + 1, NOW), 'today');
	assert.equal(daysAgo(NOW - DAY, NOW), 'yesterday');
	assert.equal(daysAgo(NOW - 3 * DAY - 5, NOW), '3 days ago');
	assert.equal(daysAgo(NOW - 400 * DAY, NOW), '400 days ago');
	assert.equal(daysAgo(NOW + 2 * DAY, NOW), 'today', 'a clock set back');
});
