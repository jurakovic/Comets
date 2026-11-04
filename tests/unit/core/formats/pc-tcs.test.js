import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readElements } from '../../../../app/js/core/formats/import.js';
import { spacedId } from '../../../../app/js/core/formats/pc-tcs.js';
import { attempt } from '../../../../app/js/core/formats/text.js';

const VAISALA = 'C/1942EA 1.277543 0.934878 37.8727 335.5629 172.3201 2028 09 13.9762 13.5 10.0 Vaisala   ';

test('the id gets its space back before the first letter from index 2', () => {
	assert.equal(spacedId('C/1942EA'), 'C/1942 EA');
	assert.equal(spacedId('C/-146P1'), 'C/-146 P1');
	assert.equal(spacedId('C/1947X1-B'), 'C/1947 X1-B');
	assert.equal(spacedId('1P'), '1P');
	for (const id of ['C/1942', 'C/', 'C/12']) assert.equal(attempt(() => spacedId(id)), null, id);
});

test('names of several words, and none', () => {
	const [comet] = readElements('PCTCS', VAISALA.replace('Vaisala', 'Southern  comet'));
	assert.equal(comet.full, 'C/1942 EA (Southern  comet)');
	const [unnamed] = readElements('PCTCS', VAISALA.replace('Vaisala', ''));
	assert.equal(unnamed.full, 'C/1942 EA');
	assert.equal(readElements('PCTCS', VAISALA.replace('C/1942EA', 'C/1942')).length, 0);
});

test('two-line records fail without their second line', () => {
	const ecu = 'C/1942 EA (Vaisala)\nE C 2000 2028 09 13.9762 1.277543 0.934878 335.5629 172.3201 37.8727 13.5 10.0\n';
	assert.equal(readElements('EarthCenteredUniverse', ecu).length, 1);
	assert.equal(readElements('EarthCenteredUniverse', ecu.split('\n')[0]).length, 0);
	const deepSpace = `Type C\nType A\nVaisala (C/1942 EA)\nC J2000 2028 09 13.9762 1.277543 0.934878 335.5629 172.3201 37.8727 13.5 10.0\nHalley (1P)`;
	assert.deepEqual(readElements('DeepSpace', deepSpace).map(c => c.full), ['C/1942 EA (Vaisala)']);
});
