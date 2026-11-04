import { test } from 'node:test';
import assert from 'node:assert/strict';
import { compileFormat, format } from '../../../../app/js/core/format/composite.js';

test('items with alignment and a number format', () => {
	assert.equal(format('{0,4} {1:00} {2,2}.{3:0000} {4,9:0.000000}', [-146, 8, 10, 5000, 0.391312]), '-146 08 10.5000  0.391312');
	assert.equal(format('[{0,-6}|{1,6}]', ['ab', 'cd']), '[ab    |    cd]');
	assert.equal(format('{0,8:0.0°} {1}', [21.46, 'W']), '   21.5° W');
});

test('alignment never truncates', () => {
	assert.equal(format('{0,3}|{1,-2}|', ['abcdef', 'xyz']), 'abcdef|xyz|');
});

test('items without a format: strings as they are, integers plainly, other numbers as .NET does', () => {
	assert.equal(format('{0}/{1}/{2}/{3}', ['1P/Halley', 2026, -0, 0.00001]), '1P/Halley/2026/0/1E-05');
});

test('an argument used twice, out of order, and literal braces', () => {
	assert.equal(format('{1}{0}{1} {{x}}', ['a', 'b']), 'bab {x}');
});

test('the MPC line of spec 03', () => {
	const mpc = compileFormat('              {0,4} {1:00} {2,2}.{3:0000} {4,9:0.000000}  {5:0.000000}  {6,8:0.0000}  {7,8:0.0000}  {8,8:0.0000}  {9}{10:00}{11:00}  {12,4:0.0} {13,4:0.0}  {14,-56} MPC 00000');
	const line = mpc([1986, 2, 9, 4570, 0.585978, 0.967143, 111.8657, 59.3966, 162.1897, 2026, 10, 18, 4, 6, '1P/Halley']);
	assert.equal(line, '              1986 02  9.4570  0.585978  0.967143  111.8657   59.3966  162.1897  20261018   4.0  6.0  1P/Halley                                                MPC 00000');
});

test('malformed templates and missing arguments are programmer errors', () => {
	assert.throws(() => compileFormat('{0'));
	assert.throws(() => compileFormat('a}b'));
	assert.throws(() => compileFormat('{x}'));
	assert.throws(() => compileFormat('{0:#.#}'));
	assert.throws(() => format('{1}', ['only one']));
});
