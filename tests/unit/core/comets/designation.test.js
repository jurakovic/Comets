import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	fullName, isPeriodic, lastToken, parseDesignationFirst, parseNameFirst, sortKey
} from '../../../../app/js/core/comets/designation.js';

test('text with no designation fails both patterns', () => {
	for (const text of ['', 'Halley', '(1995)', 'Q/2020 A1', 'C/ A1']) {
		assert.equal(parseDesignationFirst(text), null, text);
		assert.equal(parseNameFirst(text), null, text);
	}
});

test('pattern 02 is linear on long lines with no designation (spec 10, D3)', () => {
	const started = performance.now();
	for (const line of ['ab '.repeat(20_000), 'P/'.repeat(20_000), `${'a 1'.repeat(10_000)}(`]) {
		assert.equal(parseNameFirst(line), null);
	}
	assert.ok(performance.now() - started < 2000, 'took too long');
});

test('the last token keeps stray spaces and digits (spec 10, Q18)', () => {
	assert.equal(lastToken('PANSTARRS 2'), 'PANSTARRS 2');
	assert.equal(lastToken('abc 1 def 2'), ' def 2');
	assert.equal(lastToken(''), '');
	assert.deepEqual(parseNameFirst('Some 1 Name 2 (470P)'), { id: '470P', name: ' Name 2', fragment: '' });
});

test('full names', () => {
	assert.equal(fullName('C/1995 O1', 'Hale-Bopp', ''), 'C/1995 O1 (Hale-Bopp)');
	assert.equal(fullName('73P', 'Schwassmann-Wachmann', 'BT'), '73P-BT/Schwassmann-Wachmann');
	assert.equal(fullName('C/2023 R1', '', 'B'), 'C/2023 R1-B');
	assert.equal(fullName('', 'Voyager only', ''), '/Voyager only');
});

test('a sort key fails where the desktop throws, which skips the record', () => {
	for (const id of ['', 'C/', 'Cx', 'C/1995O1', 'Name']) assert.equal(sortKey(id, ''), null, id);
});

test('sort key years parse leniently, as the desktop does', () => {
	assert.equal(sortKey('C/--146 P1', ''), '1000000P0010000');
	assert.equal(sortKey('C/1,995 O1', ''), '2199500O0010000');
	assert.equal(sortKey('C/1995  O1', ''), '219950000000000');
	assert.equal(sortKey('C/12345 A1', 'B'), '21234500A0010B00');
});

test('isPeriodic needs a leading digit with P, or a P/ designation', () => {
	assert.deepEqual(['1P', '73P-BT', '3P-A', 'P/2004 V5', '3D', '1I', 'C/1995 O1', ''].map(isPeriodic), [true, true, true, true, false, false, false, false]);
});
