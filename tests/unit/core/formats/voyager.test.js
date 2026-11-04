import { test } from 'node:test';
import assert from 'node:assert/strict';
import { VECTOR_NOW } from '../../../helpers/vectors.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { exportElements } from '../../../../app/js/core/formats/export.js';
import { readElements } from '../../../../app/js/core/formats/import.js';

const HEADER = 'NOTE TO VOYAGER II USERS:\n'.padEnd(26, 'x') + '\n'.repeat(22);
const VAISALA = 'Vaisala                     1.277543   0.934878   37.8727   172.3201   335.5629   0.0  2028Sep13.9762 2000.0';

/** @param {string[]} lines */
const read = lines => readElements('VoyagerII', HEADER + lines.join('\n'));

/** @param {string} name */
const named = name => name.padEnd(27) + VAISALA.slice(27);

test('the name column is an id or a name, never both, and the sort key is empty', () => {
	const comets = read([named('Vaisala'), named('C/1942 EA'), named('1'), named('x'), named('Cx')]);
	assert.deepEqual(comets.map(c => [c.full, c.id, c.name, c.fragment, c.sortkey]), [
		['Vaisala', '', 'Vaisala', '', ''],
		['C/1942 EA', 'C/1942 EA', '', '', ''],
		['1', '1', '', '', ''],
		['x', '', 'x', '', ''],
		['Cx', '', 'Cx', '', '']
	]);
});

test('a type letter alone, an empty name and a lowercase month fail', () => {
	assert.deepEqual(read([named('C'), named(''), VAISALA.replace('Sep', 'sep'), VAISALA.replace('Sep13.9762', 'Sep13     ')]), []);
});

test('duplicates are kept, in file order', () => {
	assert.deepEqual(read([VAISALA, VAISALA.replace('1.277543', '1.300000')]).map(c => c.q), [1.277543, 1.3]);
});

test('a month outside 1–12 is written as its number, where the desktop throws', () => {
	const comet = createComet({ full: '1P/Halley', id: '1P', name: 'Halley', Ty: 2061, Tm: 13, Td: 1, q: 0.5, e: 0.9 });
	assert.ok(comet);
	assert.match(exportElements('VoyagerII', [comet], VECTOR_NOW), / 2061131\.0000 +2000\.0\r\n$/);
	assert.match(exportElements('Celestia', [comet], VECTOR_NOW), /# 2061 13 01\.0000/);
});
