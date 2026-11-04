// What every format of the registry honours, whatever its layout.
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVectorText, VECTOR_NOW } from '../../../helpers/vectors.js';
import { exportElements } from '../../../../app/js/core/formats/export.js';
import { importElements } from '../../../../app/js/core/formats/import.js';
import { FORMATS } from '../../../../app/js/core/formats/registry.js';
import { attempt } from '../../../../app/js/core/formats/text.js';

const sample = importElements([], 'MPC', readVectorText('import/sample.txt')).comets;

test('every reader rejects empty and missing records without throwing', () => {
	for (const { key, reader } of FORMATS) {
		if (!reader) continue;
		for (const record of [[], [''], Array(reader.size).fill(''), Array(reader.size).fill(' '.repeat(200))]) {
			assert.equal(attempt(() => reader.parse(record)), null, `${key} ${JSON.stringify(record).slice(0, 20)}`);
		}
	}
});

test('every writer ends each line with CRLF and writes only its header for no comets', () => {
	for (const { key, writer } of FORMATS) {
		if (!writer) continue;
		const text = exportElements(key, sample, VECTOR_NOW);
		assert.ok(text.endsWith('\r\n'), key);
		assert.doesNotMatch(text, /[^\r]\n|\r[^\n]/, key);
		const empty = exportElements(key, [], VECTOR_NOW);
		assert.equal(empty, writer.header ? `${writer.header([]).join('\r\n')}\r\n` : '', key);
	}
});
