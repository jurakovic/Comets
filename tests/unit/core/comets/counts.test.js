import { test } from 'node:test';
import assert from 'node:assert/strict';
import { databaseTitle, statusCount } from '../../../../app/js/core/comets/counts.js';

test('the status count names the database only when filters hide some', () => {
	assert.equal(statusCount(963, 963), 'Comets: 963');
	assert.equal(statusCount(12, 963), 'Comets: 12 (963)');
	assert.equal(statusCount(0, 0), 'Comets: 0');
	assert.equal(statusCount(0, 5), 'Comets: 0 (5)');
});

test('the Database view\'s title', () => {
	assert.equal(databaseTitle(963, 963), 'Database (963 comets)');
	assert.equal(databaseTitle(12, 963), 'Database (12/963 comets)');
	assert.equal(databaseTitle(0, 0), 'Database (0 comets)');
});
