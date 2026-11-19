import { test } from 'node:test';
import assert from 'node:assert/strict';
import { coordinateFields, coordinateValue, LATITUDE, LONGITUDE } from '../../../../app/js/core/input/location.js';
import { acceptsText } from '../../../../app/js/core/input/number.js';

test('the fields show the size with six decimals and the hemisphere; 0 is north and east', () => {
	assert.deepEqual({ ...coordinateFields(45.815) }, { text: '45.815000', negative: false });
	assert.deepEqual({ ...coordinateFields(-15.9819) }, { text: '15.981900', negative: true });
	assert.deepEqual({ ...coordinateFields(0) }, { text: '0.000000', negative: false });
	assert.deepEqual({ ...coordinateFields(-0) }, { text: '0.000000', negative: false });
	// rounded half away from zero, as .NET's 0.000000 does
	assert.equal(coordinateFields(45.8123455).text, '45.812346');
	assert.equal(coordinateFields(-90).text, '90.000000');
});

test('the value: south and west negative, an empty field 0, never −0', () => {
	assert.equal(coordinateValue('45.815', false), 45.815);
	assert.equal(coordinateValue('15.9819', true), -15.9819);
	assert.equal(coordinateValue('', false), 0);
	assert.ok(Object.is(coordinateValue('', true), 0));
	assert.ok(Object.is(coordinateValue('0.000000', true), 0));
	assert.equal(coordinateValue('.', true), 0);
});

test('the fields read back as the value they show', () => {
	for (const value of [0, 12.5, -33.868820, 151.209296, -179.999999, 90]) {
		const { text, negative } = coordinateFields(value);
		assert.equal(coordinateValue(text, negative), value);
	}
});

test('the ranges and hemispheres of the desktop', () => {
	assert.deepEqual({ ...LATITUDE.range }, { min: 0, max: 90, decimals: 6 });
	assert.deepEqual({ ...LONGITUDE.range }, { min: 0, max: 180, decimals: 6 });
	assert.deepEqual(LATITUDE.hemispheres, ['North', 'South']);
	assert.deepEqual(LONGITUDE.hemispheres, ['East', 'West']);
	assert.ok(acceptsText(coordinateFields(-180).text, LONGITUDE.range));
	assert.ok(!acceptsText('90.0000001', LATITUDE.range));
});
