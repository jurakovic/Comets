// import-expected/*.json: each input file's detected type and its import into an empty database,
// including the round trips of every export file (spec 03, Import rules).
import { test } from 'node:test';
import { assertImportExpected } from '../helpers/formats.js';

// the vector files, with the elements their format computes rather than reads
/** @type {Array<[string, ReadonlyArray<string>]>} */
const CASES = [
	['Soft00Cmt.txt.json', []],
	['roundtrip-00-MPC.txt.json', []]
];

for (const [name, computed] of CASES) {
	test(name, () => assertImportExpected(name, { computed }));
}
