import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	attempt, dayFraction, dbl, dec, fail, from, int, InvalidRecord, item, jdDate, pad4, padZeros,
	parseDecimal, parseDouble, parseInteger, round6, splitLines, sub, trim
} from '../../../../app/js/core/formats/text.js';
import { jd0 } from '../../../../app/js/core/time/julian.js';

const BOM = String.fromCharCode(0xfeff);
const NEL = String.fromCharCode(0x85);
const NBSP = String.fromCharCode(0xa0);

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

test('trim removes .NET white space: U+0085 goes, U+FEFF stays', () => {
	assert.equal(trim(` \t${NEL}${NBSP}a b\r\n `), 'a b');
	assert.equal(trim(`${BOM}a`), `${BOM}a`);
	assert.equal(trim(''), '');
	assert.equal(trim('   '), '');
});

test('day fractions pad to 4 with zeros and never truncate', () => {
	assert.equal(pad4(' 912 '), '9120');
	assert.equal(pad4('976 '), '9760');
	assert.equal(pad4(''), '0000');
	assert.equal(pad4('12345'), '12345');
	assert.equal(padZeros('2026101', 13), '2026101000000');
});

test('int reads what Convert.ToInt32 reads, and never gives -0', () => {
	assert.equal(parseInteger(' 2026 '), 2026);
	assert.equal(parseInteger('\t-146'), -146);
	assert.equal(parseInteger('+07'), 7);
	assert.ok(Object.is(parseInteger('-0'), 0));
	assert.equal(parseInteger('2147483647'), 2147483647);
	assert.equal(parseInteger('-2147483648'), -2147483648);
	for (const text of ['', ' ', '1.0', '1e3', '1,000', '- 5', '5-', '0x10', '2147483648', '1 2', `${NBSP}1`, '٣']) {
		assert.equal(parseInteger(text), null, JSON.stringify(text));
	}
	assert.equal(int('12'), 12);
	assert.throws(() => int('12.5'), InvalidRecord);
});

test('dbl reads what Convert.ToDouble reads: fractions, exponents, thousands commas', () => {
	assert.equal(parseDouble(' 0.570945 '), 0.570945);
	assert.equal(parseDouble('.5'), 0.5);
	assert.equal(parseDouble('5.'), 5);
	assert.equal(parseDouble('-.5e3'), -500);
	assert.equal(parseDouble('5.e3'), 5000);
	assert.equal(parseDouble('1E-05'), 0.00001);
	assert.equal(parseDouble('+1,234.5'), 1234.5);
	assert.equal(parseDouble('1,,2'), 12);
	assert.equal(parseDouble('1,.5'), 1.5);
	assert.ok(Object.is(parseDouble('-0'), -0));
	assert.equal(parseDouble('1e400'), Infinity);
	for (const text of ['', '.', '-', 'e5', '.e5', '5e', '5e+', ',5', '1.5,3', '1.2.3', '- 5', '5-', '********', 'NaN', 'Infinity', '-Infinity', '0x10']) {
		assert.equal(parseDouble(text), null, JSON.stringify(text));
	}
	assert.equal(dbl('2.5'), 2.5);
	assert.throws(() => dbl('2,5x'), InvalidRecord);
});

test('dec reads what Convert.ToDecimal reads: no exponent, and a sign may trail', () => {
	assert.equal(parseDecimal(' 62027.9762 '), 62027.9762);
	assert.equal(parseDecimal('1,000.5'), 1000.5);
	assert.equal(parseDecimal('5-'), -5);
	assert.equal(parseDecimal(' 5 - '), -5);
	assert.equal(parseDecimal('+5'), 5);
	for (const text of ['', '1e5', '-5-', '+5-', '5+-', '- 5', 'NaN']) {
		assert.equal(parseDecimal(text), null, JSON.stringify(text));
	}
	assert.equal(dec('0.0'), 0);
	assert.throws(() => dec('1e5'), InvalidRecord);
});

test('round6 rounds ties to even, as Math.Round(x, 6)', () => {
	assert.equal(2.5e-6 * 1e6, 2.5);
	assert.equal(round6(2.5e-6), 0.000002);
	assert.equal(round6(3.5e-6), 0.000004);
	assert.equal(round6(1.2775434), 1.277543);
	assert.equal(round6(-1.2775436), -1.277544);
});

test('importer dates fail where the desktop DateTime throws', () => {
	assert.deepEqual(jdDate(jd0(2026, 10, 18, 5000)), { year: 2026, month: 10, day: 18, hour: 12, minute: 0, second: 0 });
	// year -146 (C/-146 P1), a Julian 29 February 1500, and hour 24 in a day's last 0.4 ms
	for (const jd of [jd0(-146, 8, 13, 5000), 2268991.5, 2461331.4999999995]) {
		assert.throws(() => jdDate(jd), InvalidRecord, String(jd));
	}
	// the Julian calendar before 1582 is kept, as JDToDateTime reads it
	assert.deepEqual(jdDate(2268990.5), { year: 1500, month: 2, day: 28, hour: 0, minute: 0, second: 0 });
});

test('a day fraction from the time of day, ties to even', () => {
	assert.equal(dayFraction({ year: 2026, month: 1, day: 1, hour: 12, minute: 0, second: 0 }), 5000);
	assert.equal(dayFraction({ year: 2026, month: 1, day: 1, hour: 23, minute: 59, second: 59 }), 10000);
	// 00:00:54 is 6.25e-4 day: 6.25 → 6
	assert.equal(dayFraction({ year: 2026, month: 1, day: 1, hour: 0, minute: 0, second: 54 }), 6);
});

test('a record parser gives null where a field fails, and bugs still throw', () => {
	assert.equal(attempt(() => fail('bad')), null);
	assert.equal(attempt(() => int(sub('12', 0, 2))), 12);
	assert.throws(() => attempt(() => { throw new TypeError('bug'); }), TypeError);
});
