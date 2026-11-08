import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	acceptsText, deleteFrom, inputModeOf, insertInto, matchesPattern, normalizeTyped, numberOf, stepText
} from '../../../../app/js/core/input/number.js';

const MAGNITUDE = { min: -20, max: 40, decimals: 2 };
const INTERVAL = { min: 1, max: 99, decimals: 0 };
const DAYS = { min: 0, max: 3652, decimals: 0 };
const LATITUDE = { min: 0, max: 90, decimals: 6 };
const DISTANCE = { min: 0, max: 9.99, decimals: 2 };

test('empty text is always accepted: it means no value', () => {
	for (const range of [MAGNITUDE, INTERVAL, DAYS, LATITUDE]) assert.ok(acceptsText('', range));
	assert.equal(numberOf(''), null);
	assert.equal(numberOf('  '), null);
});

test('digits, and a leading - only when the minimum is below 0', () => {
	assert.ok(acceptsText('-', MAGNITUDE));
	assert.ok(acceptsText('-20', MAGNITUDE));
	assert.ok(!acceptsText('-21', MAGNITUDE));
	assert.ok(!acceptsText('-', DAYS));
	assert.ok(!acceptsText('-1', DAYS));
	assert.ok(!acceptsText('1-', MAGNITUDE));
	assert.ok(!acceptsText('--1', MAGNITUDE));
	for (const text of ['a', '1a', '+1', ' 1', '1 ', '1e3', '0x1', '∞']) assert.ok(!acceptsText(text, MAGNITUDE), text);
});

test('a . with up to decimals digits after it, only in a field with decimals', () => {
	assert.ok(acceptsText('.', LATITUDE));
	assert.ok(acceptsText('.5', LATITUDE));
	assert.ok(acceptsText('5.', LATITUDE));
	assert.ok(acceptsText('5.123456', LATITUDE));
	assert.ok(!acceptsText('5.1234567', LATITUDE));
	assert.ok(!acceptsText('5..1', LATITUDE));
	assert.ok(!acceptsText('5.1.', LATITUDE));
	assert.ok(!acceptsText('5.', DAYS));
	assert.ok(acceptsText('-.', MAGNITUDE));
	assert.ok(acceptsText('-.25', MAGNITUDE));
	assert.ok(!acceptsText('-.255', MAGNITUDE));
});

test('the number stays within the range, its ends included', () => {
	assert.ok(acceptsText('9.99', DISTANCE));
	assert.ok(!acceptsText('10', DISTANCE));
	assert.ok(acceptsText('40', MAGNITUDE));
	assert.ok(!acceptsText('40.01', MAGNITUDE));
	assert.ok(acceptsText('3652', DAYS));
	assert.ok(!acceptsText('3653', DAYS));
	assert.ok(acceptsText('90.000000', LATITUDE));
	assert.ok(!acceptsText('90.000001', LATITUDE));
	// a text without a number reads as 0, which a minimum above 0 refuses, as on the desktop
	assert.ok(!acceptsText('0', INTERVAL));
	assert.ok(acceptsText('1', INTERVAL));
	assert.ok(acceptsText('007', DAYS));
	assert.equal(numberOf('-'), 0);
	assert.equal(numberOf('.'), 0);
	assert.equal(numberOf('-.5'), -0.5);
	assert.equal(numberOf('5.'), 5);
});

test('the pattern alone, for a field that marks a number out of range instead', () => {
	const JD = { min: 2341972.5, max: 2560755.5, decimals: 5 };
	assert.ok(matchesPattern('2', JD));
	assert.ok(!acceptsText('2', JD));
	assert.ok(matchesPattern('2451545.12345', JD));
	assert.ok(!matchesPattern('2451545.123456', JD));
	assert.ok(!matchesPattern('-2', JD));
});

test('a , typed is a .', () => {
	assert.equal(normalizeTyped('1,5'), '1.5');
	assert.equal(insertInto('12', 1, 1, ','), '1.2');
	assert.equal(insertInto('12.5', 0, 4, '3,25'), '3.25');
	assert.equal(insertInto('', 0, 0, '7'), '7');
});

test('deleting the selection, or the character before or after the caret', () => {
	assert.equal(deleteFrom('12.5', 1, 3, true), '15');
	assert.equal(deleteFrom('12.5', 1, 3, false), '15');
	assert.equal(deleteFrom('12.5', 2, 2, true), '1.5');
	assert.equal(deleteFrom('12.5', 2, 2, false), '125');
	assert.equal(deleteFrom('12', 0, 0, true), '12');
	assert.equal(deleteFrom('12', 2, 2, false), '12');
});

test('↑ and ↓ move an integer by one within the range, and write it again', () => {
	assert.equal(stepText('5', DAYS, 1), '6');
	assert.equal(stepText('5', DAYS, -1), '4');
	assert.equal(stepText('3652', DAYS, 1), '3652');
	assert.equal(stepText('0', DAYS, -1), '0');
	assert.equal(stepText('007', DAYS, 1), '8');
	assert.equal(stepText('1', INTERVAL, -1), '1');
	assert.equal(stepText('99', INTERVAL, 1), '99');
	// the range truncated to integers, as IMin and IMax are
	assert.equal(stepText('9', DISTANCE, 1), '9');
	assert.equal(stepText('8.5', DISTANCE, 1), '9.5');
	assert.equal(stepText('-1', { min: -5, max: 5, decimals: 0 }, 1), '0');
	assert.equal(stepText('', DAYS, 1), null);
	assert.equal(stepText('-', MAGNITUDE, 1), null);
});

test('the keyboard a phone shows', () => {
	assert.equal(inputModeOf(DAYS), 'numeric');
	assert.equal(inputModeOf(LATITUDE), 'decimal');
	assert.equal(inputModeOf(MAGNITUDE), 'text');
});
