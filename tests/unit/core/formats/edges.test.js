// Paths of the column formats that the sample collection does not reach.
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVectorText, VECTOR_NOW } from '../../../helpers/vectors.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { detectFormat } from '../../../../app/js/core/formats/detection.js';
import { exportElements } from '../../../../app/js/core/formats/export.js';
import { readElements } from '../../../../app/js/core/formats/import.js';

const VAISALA = 'C/1942 EA (Vaisala)                    |2000|20280913.9762 | 1.277543 |0.934878 |335.5629 |172.3201 | 37.8727 |13.5 |10.0 | MPEC 2026-K60';

test('TheSky: a date with no fraction is padded to 13 characters', () => {
	const [comet] = readElements('TheSky', VAISALA.replace('20280913.9762', '20280913'));
	assert.deepEqual([comet.Ty, comet.Tm, comet.Td, comet.Th], [2028, 9, 13, 0]);
	const [short] = readElements('TheSky', VAISALA.replace('20280913.9762', '20280913.97'));
	assert.equal(short.Th, 9700);
});

test('a header must start the way the format writes it', () => {
	const mpc = readVectorText('import/sample.txt');
	assert.equal(detectFormat(`Name,Perihelion\n${mpc}`), 'HomePlanet');
	assert.equal(detectFormat(`Name,\n${mpc}`), 'MPC');
	assert.equal(detectFormat(`RDP\n${mpc}`), 'MPC');
});

test('SkyTools writes Th div 10 as an integer: never -000', () => {
	const comet = createComet({ full: '1P/Halley', id: '1P', name: 'Halley', Ty: 2061, Tm: 8, Td: 1, Th: -1, q: 0.5, e: 0.9 });
	assert.ok(comet);
	assert.match(exportElements('SkyTools', [comet], VECTOR_NOW), / 2061 08 01\.000 {2}/);
});
