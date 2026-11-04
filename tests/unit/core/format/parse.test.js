import { test } from 'node:test';
import assert from 'node:assert/strict';
import { parseDecimal, parseDouble, parseInteger, trim, trimEnd } from '../../../../app/js/core/format/parse.js';

const BOM = String.fromCharCode(0xfeff);
const NEL = String.fromCharCode(0x85);
const NBSP = String.fromCharCode(0xa0);

test('trim removes .NET white space: U+0085 goes, U+FEFF stays', () => {
	assert.equal(trim(` \t${NEL}${NBSP}a b\r\n `), 'a b');
	assert.equal(trim(`${BOM}a`), `${BOM}a`);
	assert.equal(trim(''), '');
	assert.equal(trim('   '), '');
	assert.equal(trimEnd(` a${NEL} `), ' a');
});


test('integers as Convert.ToInt32 reads them, never -0', () => {
	assert.equal(parseInteger(' 2026 '), 2026);
	assert.equal(parseInteger('\t-146'), -146);
	assert.equal(parseInteger('+07'), 7);
	assert.ok(Object.is(parseInteger('-0'), 0));
	assert.equal(parseInteger('2147483647'), 2147483647);
	assert.equal(parseInteger('-2147483648'), -2147483648);
	for (const text of ['', ' ', '1.0', '1e3', '1,000', '- 5', '5-', '0x10', '2147483648', '1 2', `${NBSP}1`, '٣']) {
		assert.equal(parseInteger(text), null, JSON.stringify(text));
	}
});


test('doubles as Convert.ToDouble reads them: fractions, exponents, thousands commas', () => {
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
});


test('decimals as Convert.ToDecimal reads them: no exponent, a sign that may trail, below 2^96', () => {
	assert.equal(parseDecimal(' 62027.9762 '), 62027.9762);
	assert.equal(parseDecimal('1,000.5'), 1000.5);
	assert.equal(parseDecimal('5-'), -5);
	assert.equal(parseDecimal(' 5 - '), -5);
	assert.equal(parseDecimal('+5'), 5);
	assert.equal(parseDecimal('79228162514264337593543950335'), 2 ** 96);
	assert.equal(parseDecimal('100000000000000000000000000000'), null);
	for (const text of ['', '1e5', '-5-', '+5-', '5+-', '- 5', 'NaN']) {
		assert.equal(parseDecimal(text), null, JSON.stringify(text));
	}
});


test('NUL characters may end a number, as .NET allows, and nothing after them', () => {
	const NUL = String.fromCharCode(0);
	assert.equal(parseInteger(`5${NUL}`), 5);
	assert.equal(parseDouble(`5.5 ${NUL}${NUL}`), 5.5);
	assert.equal(parseDecimal(`5-${NUL}`), -5);
	assert.equal(parseDouble(`5${NUL} `), null);
	assert.equal(parseInteger(`${NUL}5`), null);
});
