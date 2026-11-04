import { test } from 'node:test';
import assert from 'node:assert/strict';
import { SIMULATION_STEPS, simulationStep, stepDate } from '../../../../app/js/core/orbit/steps.js';
import { isoUtc } from '../../../helpers/vectors.js';

test('the step menu, smallest first', () => {
	assert.deepEqual(SIMULATION_STEPS.map(step => step.label), [
		'1 Hour', '6 Hours', '12 Hours', '1 Day', '3 Days', '10 Days',
		'1 Month', '3 Months', '6 Months', '1 Year', '3 Years', '10 Years'
	]);
	assert.throws(() => simulationStep('2 Days'));
});

test('a step back into the days of the Gregorian reform lands on 1582-10-04', () => {
	assert.equal(isoUtc(stepDate(Date.parse('1582-10-20T06:00:00Z'), simulationStep('10 Days'), false)), '1582-10-04T06:00:00Z');
});

test('a step starts from the whole second', () => {
	assert.equal(isoUtc(stepDate(Date.parse('2026-10-18T12:00:00.750Z'), simulationStep('1 Hour'), true)), '2026-10-18T13:00:00Z');
});
