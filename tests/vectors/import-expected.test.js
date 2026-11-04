// import-expected/*.json: each input file's detected type and its import into an empty database,
// including the round trips of every export file (spec 03, Detection and Import rules).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readdirSync } from 'node:fs';
import { assertImportExpected } from '../helpers/formats.js';
import { readVector, vectorPath } from '../helpers/vectors.js';

// the elements a format computes rather than reads, compared within 1e-9 (spec 11, Tolerances)
/** @type {Record<string, ReadonlyArray<string>>} */
const COMPUTED = { 'ELEMENTS.COMET.json': ['i', 'N', 'w'] };

const files = readdirSync(vectorPath('import-expected')).filter(name => name.endsWith('.json')).sort();

test('every input file and every export round trip has its vector', () => {
	// the edge files are in import-detection.json, merge-new.txt in import-merge.json
	const inputs = readdirSync(vectorPath('import'), { withFileTypes: true })
		.filter(entry => entry.isFile() && !entry.name.startsWith('edge-') && entry.name !== 'merge-new.txt')
		.map(entry => `${entry.name}.json`);
	const roundTrips = readVector('export/index.json').exports.map((/** @type {any} */ entry) => `roundtrip-${entry.file.slice('export/'.length)}.json`);
	assert.equal(inputs.length, 20);
	assert.deepEqual(files, [...inputs, ...roundTrips].sort());
});

for (const name of files) {
	test(name, () => assertImportExpected(name, { computed: COMPUTED[name] ?? [] }));
}
