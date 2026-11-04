// names.json: both designation patterns, full names, sort keys, isPeriodic, and every comet of
// the MPC snapshot (spec 02, Designations and full names).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVector } from '../helpers/vectors.js';
import {
	fullName, isPeriodic, parseDesignationFirst, parseNameFirst, sortKey
} from '../../app/js/core/comets/designation.js';

const vectors = readVector('names.json');

/**
 * @param {(text: string) => import('../../app/js/core/comets/designation.js').Designation | null} parse
 * @param {Array<{ input: string, id: string, name: string, fragment: string, full: string }>} cases
 */
function checkParses(parse, cases) {
	for (const { input, id, name, fragment, full } of cases) {
		const parsed = parse(input);
		assert.deepEqual(parsed, { id, name, fragment }, input);
		assert.equal(fullName(id, name, fragment), full, input);
	}
}

test('pattern 00, designation first', () => {
	assert.equal(vectors.pattern00.length, 25);
	checkParses(parseDesignationFirst, vectors.pattern00);
});

test('pattern 02, name first', () => {
	assert.equal(vectors.pattern02.length, 21);
	checkParses(parseNameFirst, vectors.pattern02);
});

test('sort keys', () => {
	for (const { id, fragment, sortkey } of vectors.sortkeys) assert.equal(sortKey(id, fragment), sortkey, `${id} ${fragment}`);
});

test('isPeriodic', () => {
	for (const { id, isPeriodic: expected } of vectors.isPeriodic) assert.equal(isPeriodic(id), expected, id);
});

test('every comet of the MPC snapshot: parts, full name, sort key, isPeriodic', () => {
	assert.equal(vectors.mpcSoft00.length, 963);
	for (const comet of vectors.mpcSoft00) {
		const parsed = parseDesignationFirst(comet.full);
		assert.deepEqual(parsed, { id: comet.id, name: comet.name, fragment: comet.fragment }, comet.full);
		assert.equal(sortKey(comet.id, comet.fragment), comet.sortkey, comet.full);
		assert.equal(isPeriodic(comet.id), comet.IsPeriodic, comet.full);
	}
});
