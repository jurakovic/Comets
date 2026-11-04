// time.json: JD0, the JD of an instant, JDToDateTime and the date range (spec 04, Time).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertClose, isoUtc, readVector, utcInstant } from '../helpers/vectors.js';
import { instantToJd, jd0, jdToInstant } from '../../app/js/core/time/julian.js';
import { MAXIMUM_DATE, MINIMUM_DATE } from '../../app/js/core/time/range.js';

const vectors = readVector('time.json');
const JD_TOLERANCE = 1e-8;

test('JD0 of a calendar date and day fraction', () => {
	for (const { year, month, day, hour, jd } of vectors.jd0) {
		assertClose(jd0(year, month, day, hour), jd, JD_TOLERANCE, `JD0(${year}, ${month}, ${day}, ${hour})`);
	}
});

test('the JD of an instant, milliseconds dropped', () => {
	for (const { utc, jd } of vectors.jd) {
		assertClose(instantToJd(utcInstant(utc)), jd, JD_TOLERANCE, `JD(${utc})`);
	}
});

test('a JD back to an instant, whole seconds', () => {
	for (const { jd, utc } of vectors.jdToDate) {
		assert.equal(isoUtc(jdToInstant(jd)), utc, `JDToDateTime(${jd})`);
	}
});

test('the date range', () => {
	assert.equal(isoUtc(MINIMUM_DATE), vectors.minimumDate);
	assert.equal(isoUtc(MAXIMUM_DATE), vectors.maximumDate);
});
