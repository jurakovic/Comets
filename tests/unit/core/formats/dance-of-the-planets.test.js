import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readElements } from '../../../../app/js/core/formats/import.js';

const HEADER = 'Comet      peri(au)\n\n\n\n\n';
const BORISOV = '2I         1.997724 3.345952  44.2624 307.8024 209.2911 2019.12090572 Borisov';

test('a line of exactly 69 characters has no name; a shorter one fails', () => {
	const line = BORISOV.slice(0, 69);
	assert.deepEqual(readElements('DanceOfThePlanets', HEADER + line).map(c => c.full), ['2I']);
	assert.deepEqual(readElements('DanceOfThePlanets', HEADER + line.slice(0, 68)), []);
	assert.deepEqual(readElements('DanceOfThePlanets', `${HEADER}${line} `).map(c => c.full), ['2I']);
	assert.deepEqual(readElements('DanceOfThePlanets', HEADER + BORISOV).map(c => c.full), ['2I/Borisov']);
});

test('q of 10 AU or more, written as stars, fails (spec 10, Q15)', () => {
	assert.deepEqual(readElements('DanceOfThePlanets', HEADER + BORISOV.replace(' 1.997724', '********')), []);
});
