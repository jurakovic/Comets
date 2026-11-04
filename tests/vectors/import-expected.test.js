// import-expected/*.json: each input file's detected type and its import into an empty database,
// including the round trips of every export file (spec 03, Import rules).
import { test } from 'node:test';
import { assertImportExpected } from '../helpers/formats.js';

// the vector files, with the elements their format computes rather than reads
/** @type {Array<[string, ReadonlyArray<string>]>} */
const CASES = [
	['Soft00Cmt.txt.json', []],
	['roundtrip-00-MPC.txt.json', []],
	['Soft01Cmt.txt.json', []],
	['roundtrip-01-SkyMap.txt.json', []],
	['Soft02Cmt.txt.json', []],
	['roundtrip-02-Guide.txt.json', []],
	['Soft03Cmt.txt.json', []],
	['roundtrip-03-xephem.txt.json', []],
	['Soft04Cmt.txt.json', []],
	['roundtrip-04-HomePlanet.txt.json', []],
	['Soft05Cmt.txt.json', []],
	['roundtrip-05-MyStars.txt.json', []]
];

for (const [name, computed] of CASES) {
	test(name, () => assertImportExpected(name, { computed }));
}
