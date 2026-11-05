import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVector, readVectorText } from '../../../helpers/vectors.js';
import { detectFormat } from '../../../../app/js/core/formats/detection.js';
import { COUNTS_TOOLTIP, importElements, previewImport, readElements } from '../../../../app/js/core/formats/import.js';

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

test('the full MPC file is detected and imported in under 100 ms, on the first run', () => {
	const text = readVectorText('import/full/Soft00Cmt.txt');
	const started = performance.now();
	const { comets } = importElements([], detectFormat(text), text);
	const elapsed = performance.now() - started;
	assert.equal(comets.length, 963);
	assert.ok(elapsed < 100, `took ${elapsed} ms`);
});

test('the preview without a format: no merge, and "-" for the counts', () => {
	const preview = (/** @type {string | null} */ text) => ({ ...previewImport([], text) });
	assert.deepEqual(preview(null), { format: 'NoFileSelected', formatLabel: '(no file selected)', merge: null, counts: '-' });
	assert.deepEqual(preview(''), { format: 'EmptyFile', formatLabel: '(empty file)', merge: null, counts: '-' });
	assert.deepEqual(preview('\uFEFF'), { format: 'EmptyFile', formatLabel: '(empty file)', merge: null, counts: '-' });
	assert.deepEqual(preview(readVectorText('import/edge-unknown.txt')), { format: 'Unknown', formatLabel: '(unknown)', merge: null, counts: '-' });
	assert.equal(COUNTS_TOOLTIP, 'New, updates, no changes; total');
});

test('the preview of a file: its format\'s name, the merge into the database and its counts (import-merge.json)', () => {
	const vector = readVector('import-merge.json');
	const database = importElements([], 'MPC', readVectorText(vector.oldFile)).comets;
	const preview = previewImport(database, readVectorText(vector.newFile));
	assert.equal(preview.format, 'MPC');
	assert.equal(preview.formatLabel, 'MPC (Soft00Cmt)');
	assert.equal(preview.counts, '1, 1, 33; 35');
	assert.deepEqual(preview.merge?.comets.map(c => c.full), vector.result.map((/** @type {any} */ r) => r.full));
	assert.equal(previewImport([], readVectorText(vector.oldFile)).counts, '34, 0, 0; 34');
	assert.ok(Object.isFrozen(preview));
});
