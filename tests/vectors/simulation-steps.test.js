// simulation-steps.json: the orbit viewer's single steps (spec 08, Simulation).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { isoUtc, readVector, utcInstant } from '../helpers/vectors.js';
import { simulationStep, stepDate } from '../../app/js/core/orbit/steps.js';

const { cases } = readVector('simulation-steps.json');

test('single steps forward and back with calendar arithmetic', () => {
	assert.equal(cases.length, 48);
	for (const { startUtc, step, forward, resultUtc } of cases) {
		const result = stepDate(utcInstant(startUtc), simulationStep(step), forward);
		assert.equal(isoUtc(result), resultUtc, `${startUtc} ${forward ? '+' : '−'} ${step}`);
	}
});
