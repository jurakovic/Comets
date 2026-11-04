import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVectorText } from '../../../helpers/vectors.js';
import { importElements, readElements } from '../../../../app/js/core/formats/import.js';

const lines = readVectorText('import/sample.txt').split('\n').filter(Boolean);
const halley = lines.find(line => line.includes('1P/Halley')) ?? '';

test('invalid records are skipped: short lines, bad numbers, no designation', () => {
	const text = [halley.slice(0, 120), halley.replace(' 0.570945', ' 0.57x945'), halley.replace('1P/Halley', '1Q/Halley'), '', halley].join('\r\n');
	assert.deepEqual(readElements('MPC', text).map(c => c.full), ['1P/Halley']);
});

test('the first record with a full name wins', () => {
	const changed = halley.replace(' 0.570945', ' 0.500000');
	const comets = readElements('MPC', [changed, halley].join('\n'));
	assert.deepEqual(comets.map(c => c.q), [0.5]);
});

test('formats with no reader read nothing', () => {
	assert.deepEqual(readElements('Celestia', halley), []);
	assert.throws(() => importElements([], 'Unknown', halley), /unknown element format/);
});
