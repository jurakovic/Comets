import { test } from 'node:test';
import assert from 'node:assert/strict';
import { angle, hms } from '../../../../app/js/core/format/angles.js';

test('seconds and arcminutes exactly halfway round to even, as .NET Math.Round does', () => {
	// 0.03125 h is 1 min 52.5 s; 0.375° is 22.5′
	assert.equal(hms(0.03125), '00h01m52');
	assert.equal(hms(0.03125 + 1 / 3600), '00h01m54');
	assert.equal(angle(0.375, false, true), "+00°22'");
	assert.equal(angle(-0.375, false, true), "-00°22'");
	// 0.125° is 7.5′, which goes up to the even 8
	assert.equal(angle(0.125, true, false), '000:08');
});

test('carries from seconds into minutes and hours, and past 24 h', () => {
	assert.equal(hms(1.9999999), '02h00m00');
	assert.equal(hms(23.99999), '00h00m00');
	assert.equal(angle(359.999, true, true), "360°00'");
});
