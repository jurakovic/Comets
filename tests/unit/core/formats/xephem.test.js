import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readElements } from '../../../../app/js/core/formats/import.js';

const VAISALA = 'C/1942 EA (Vaisala),e,37.8727,172.3201,335.5629,19.61782,0.0113430,0.93487842,352.0829,10/17.0/2026,2000,g 13.5,4.0';

/** @param {string} line */
const read = line => readElements('xephem', `# comment\n${line}\n`);

test('an ellipse with M < 180 has its perihelion M / n days before the epoch', () => {
	const [comet] = read(VAISALA.replace('0.0113430', '1').replace('352.0829', '10'));
	assert.deepEqual([comet.Ty, comet.Tm, comet.Td, comet.Th], [2026, 10, 7, 0]);
	assert.equal(comet.q, 1.277543);
});

test('an ellipse with M = 0 is at perihelion at the epoch', () => {
	const [comet] = read(VAISALA.replace('352.0829', '0.0000'));
	assert.deepEqual([comet.Ty, comet.Tm, comet.Td, comet.Th], [2026, 10, 17, 0]);
});

test('records that fail', () => {
	for (const line of [
		VAISALA.replace('0.0113430', '0'),
		VAISALA.replace(',e,', ',x,'),
		VAISALA.replace('10/17.0/2026', '10/17/2026'),
		VAISALA.replace('10/17.0/2026', '10/17.0'),
		VAISALA.replace(',g 13.5,', ',g,'),
		VAISALA.replace('0.0113430', '1.1e-2'),
		VAISALA.split(',').slice(0, 12).join(','),
		'C/1942 EA (Vaisala)'
	]) {
		assert.deepEqual(read(line), [], line);
	}
});

test('only the odd lines are records', () => {
	assert.deepEqual(readElements('xephem', `${VAISALA}\n# comment`), []);
});
