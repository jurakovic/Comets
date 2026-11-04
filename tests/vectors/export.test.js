// export/*: ExportMain of the sample collection in every export format, at the vector clock
// (spec 03, Export rules). The app writes CRLF; the vectors are stored with LF.
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVector, readVectorText, VECTOR_NOW } from '../helpers/vectors.js';
import { exportElements } from '../../app/js/core/formats/export.js';
import { importElements } from '../../app/js/core/formats/import.js';
import { EXPORT_FORMATS } from '../../app/js/core/formats/registry.js';

const sample = importElements([], 'MPC', readVectorText('import/sample.txt')).comets;
const { exports } = readVector('export/index.json');

test('every export format has its vector, in the Elements view\'s order', () => {
	assert.deepEqual(exports.map((/** @type {any} */ entry) => [entry.type, entry.typeName]), EXPORT_FORMATS.map(f => [f.key, f.name]));
});

for (const { type, file } of exports) {
	test(`${file} byte for byte`, () => {
		const expected = readVectorText(file).replace(/\n/g, '\r\n');
		assert.equal(exportElements(type, sample, VECTOR_NOW), expected);
	});
}

test('an empty collection writes nothing but the header', () => {
	assert.equal(exportElements('MPC', [], VECTOR_NOW), '');
});
