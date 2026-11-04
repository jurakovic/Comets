// import-merge.json: merge-new.txt imported into the sample collection (spec 03, Merge).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVector, readVectorText } from '../helpers/vectors.js';
import { countResults } from '../../app/js/core/comets/merge.js';
import { importElements } from '../../app/js/core/formats/import.js';

test('New, Update and NoChanges, in database order', () => {
	const vector = readVector('import-merge.json');
	const old = importElements([], 'MPC', readVectorText(vector.oldFile)).comets;
	const merge = importElements(old, 'MPC', readVectorText(vector.newFile));
	assert.deepEqual(
		merge.comets.map((comet, index) => ({ full: comet.full, importResult: merge.results[index], q: comet.q })),
		vector.result
	);
	assert.deepEqual({ ...countResults(merge) }, { New: 1, Update: 1, NoChanges: 33, total: 35 });
});
