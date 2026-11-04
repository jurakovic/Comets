// import-detection.json: the detection results that are not formats (spec 03, Detection).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVector, readVectorText } from '../helpers/vectors.js';
import { detectFormat } from '../../app/js/core/formats/detection.js';

test('no file, an empty file and an unknown one', () => {
	const { cases } = readVector('import-detection.json');
	assert.equal(cases.length, 3);
	for (const { file, type } of cases) {
		const text = file === '(none)' ? null : readVectorText(file);
		assert.equal(detectFormat(text), type, file);
	}
});
