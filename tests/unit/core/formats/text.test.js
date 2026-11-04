import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	attempt, dayFraction, dbl, dec, fail, from, int, InvalidRecord, item, jdDate, pad4, padZeros,
	round6, splitLines, sub
} from '../../../../app/js/core/formats/text.js';
import { jd0 } from '../../../../app/js/core/time/julian.js';

const BOM = String.fromCharCode(0xfeff);

test('lines split on CRLF, LF and CR, as File.ReadAllLines', () => {
	assert.deepEqual(splitLines('a\r\nb\nc\rd'), ['a', 'b', 'c', 'd']);
	assert.deepEqual(splitLines('a\r\r\nb'), ['a', '', 'b']);
	assert.deepEqual(splitLines('a\n'), ['a']);
	assert.deepEqual(splitLines('a\n\n'), ['a', '']);
	assert.deepEqual(splitLines('\n'), ['']);
	assert.deepEqual(splitLines(''), []);
	assert.deepEqual(splitLines(`${BOM}a\nb`), ['a', 'b']);
	assert.deepEqual(splitLines(BOM), []);
	// only a leading BOM goes
	assert.deepEqual(splitLines(`a${BOM}`), [`a${BOM}`]);
});

test('sub and from fail on a short line, unlike slice', () => {
	assert.equal(sub('abcdef', 2, 3), 'cde');
	assert.equal(sub('abcdef', 3, 3), 'def');
	assert.throws(() => sub('abcdef', 4, 3), InvalidRecord);
	assert.equal(sub('abc', 3, 0), '');
	assert.equal(from('abcdef', 4), 'ef');
	assert.equal(from('abcdef', 6), '');
	assert.throws(() => from('abcdef', 7), InvalidRecord);
});

test('split fields exist or fail', () => {
	const parts = 'a,,b'.split(',');
	assert.equal(item(parts, 1), '');
	assert.equal(item(parts, 2), 'b');
	assert.throws(() => item(parts, 3), InvalidRecord);
});

test('day fractions pad to 4 with zeros and never truncate', () => {
	assert.equal(pad4(' 912 '), '9120');
	assert.equal(pad4('976 '), '9760');
	assert.equal(pad4(''), '0000');
	assert.equal(pad4('12345'), '12345');
	assert.equal(padZeros('2026101', 13), '2026101000000');
});

test('int, dbl and dec fail the record where the parsers give null', () => {
	assert.equal(int('12'), 12);
	assert.throws(() => int('12.5'), InvalidRecord);
	assert.equal(dbl('2.5'), 2.5);
	assert.throws(() => dbl('2,5x'), InvalidRecord);
	assert.equal(dec('0.0'), 0);
	assert.throws(() => dec('1e5'), InvalidRecord);
});

test('round6 rounds ties to even, as Math.Round(x, 6)', () => {
	assert.equal(2.5e-6 * 1e6, 2.5);
	assert.equal(round6(2.5e-6), 0.000002);
	assert.equal(round6(3.5e-6), 0.000004);
	assert.equal(round6(1.2775434), 1.277543);
	assert.equal(round6(-1.2775436), -1.277544);
	// .NET leaves values of 1e16 and more as they are
	assert.equal(round6(1.002e16), 1.002e16);
});

test('importer dates fail where the desktop DateTime throws', () => {
	assert.deepEqual(jdDate(jd0(2026, 10, 18, 5000)), { year: 2026, month: 10, day: 18, hour: 12, minute: 0, second: 0 });
	// year -146 (C/-146 P1), year 10000, a Julian 29 February 1500, hour 24 in a day's last 0.4 ms
	for (const jd of [jd0(-146, 8, 13, 5000), jd0(10000, 1, 1, 0), 2268991.5, 2461331.4999999995, Infinity, NaN]) {
		assert.throws(() => jdDate(jd), InvalidRecord, String(jd));
	}
	// the Julian calendar before 1582 is kept, as JDToDateTime reads it
	assert.deepEqual(jdDate(2268990.5), { year: 1500, month: 2, day: 28, hour: 0, minute: 0, second: 0 });
});

test('a day fraction from the time of day, ties to even', () => {
	assert.equal(dayFraction({ year: 2026, month: 1, day: 1, hour: 12, minute: 0, second: 0 }), 5000);
	assert.equal(dayFraction({ year: 2026, month: 1, day: 1, hour: 23, minute: 59, second: 59 }), 10000);
	// 00:01:48 is 12.5e-4 day, a tie in float64 too: to even, 12, where Math.round gives 13
	assert.equal(dayFraction({ year: 2026, month: 1, day: 1, hour: 0, minute: 1, second: 48 }), 12);
});

test('a record parser gives null where a field fails, and bugs still throw', () => {
	assert.equal(attempt(() => fail('bad')), null);
	assert.equal(attempt(() => int(sub('12', 0, 2))), 12);
	assert.throws(() => attempt(() => { throw new TypeError('bug'); }), TypeError);
});
