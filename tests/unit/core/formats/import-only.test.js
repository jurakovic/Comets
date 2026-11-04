import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVectorText, VECTOR_NOW } from '../../../helpers/vectors.js';
import { detectFormat } from '../../../../app/js/core/formats/detection.js';
import { exportElements } from '../../../../app/js/core/formats/export.js';
import { readElements } from '../../../../app/js/core/formats/import.js';

const dat = readVectorText('import/CometForWindows.dat');

test('Comet for Windows: a value is the text between the first = and the next', () => {
	const lines = dat.split('\n');
	lines[6] = 'Name=C/1995 O1 (Hale-Bopp)=ignored';
	assert.equal(readElements('CometForWindows', lines.join('\n'))[0].full, 'C/1995 O1 (Hale-Bopp)');
	lines[10] = 'q 0.925995';
	assert.notEqual(readElements('CometForWindows', lines.join('\n'))[0].full, 'C/1995 O1 (Hale-Bopp)');
});

test('Comet for Windows: a record missing its last lines fails', () => {
	const lines = dat.split('\n').slice(0, -1);
	assert.equal(readElements('CometForWindows', lines.join('\n')).length, 3);
	// the third record's magnitude line, its twelfth, is gone
	assert.equal(readElements('CometForWindows', lines.slice(0, 6 + 2 * 13 + 11).join('\n')).length, 2);
	// a last record needs its twelve lines, not the blank thirteenth
	assert.equal(readElements('CometForWindows', lines.slice(0, 6 + 2 * 13 + 12).join('\n')).length, 3);
	assert.equal(detectFormat(lines.slice(0, 2).join('\n')), 'CometForWindows');
	assert.equal(detectFormat('[File]'), 'Unknown');
});

const HALLEY = readVectorText('import/ELEMENTS.COMET').split('\n')[2];

test('NASA: the day fraction is rounded to four digits, ties to even', () => {
	/** @param {string} fraction */
	const th = fraction => readElements('NASA', `\n\n${HALLEY.replace('.47362', `.${fraction}`)}`)[0]?.Th;
	assert.equal(th('47362'), 4736);
	assert.equal(th('47365'), 4736);
	assert.equal(th('47375'), 4738);
	assert.equal(th('4736 '), 4736);
	assert.equal(th('5    '), 5000);
	// Convert.ToInt32 gives 0, not −0, which the MPC format would write as -0000
	assert.ok(Object.is(th('-0004'), 0));
	const [comet] = readElements('NASA', `\n\n${HALLEY.replace('.47362', '.-0004')}`);
	assert.match(exportElements('MPC', [comet], VECTOR_NOW), /^ {14}1986 02 {2}8\.0000 /);
});

test('NASA: the angles are precessed from the epoch to J2000 (spec 10, Q7)', () => {
	assert.equal(HALLEY.slice(44, 51), '  39875');
	const [comet] = readElements('NASA', `\n\n${HALLEY}`);
	assert.ok(Math.abs(comet.i - 162.19053) > 1e-3);
	const [atJ2000] = readElements('NASA', `\n\n${HALLEY.slice(0, 44)}51544.5${HALLEY.slice(51)}`);
	assert.deepEqual([atJ2000.i, atJ2000.N, atJ2000.w], [162.19053, 59.09895, 112.24143]);
});
