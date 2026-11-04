// formatting.json: HMS and Angle text (spec 04, Angle and time text).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVector } from '../helpers/vectors.js';
import { angle, hms } from '../../app/js/core/format/angles.js';

const vectors = readVector('formatting.json');

test('hours as HMS', () => {
	assert.equal(vectors.hms.length, 10);
	for (const { hours, text } of vectors.hms) assert.equal(hms(hours), text, `HMS(${hours})`);
});

test('angles as degrees and minutes', () => {
	assert.equal(vectors.angle.length, 36);
	for (const { value, circle, arcmin, text } of vectors.angle) {
		assert.equal(angle(value, circle, arcmin), text, `Angle(${value}, ${circle}, ${arcmin})`);
	}
});
