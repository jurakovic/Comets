import { test } from 'node:test';
import assert from 'node:assert/strict';
import { exactDecimal, formatNumber, formatShortest, parseNumberPattern } from '../../../../app/js/core/format/number.js';

/** @param {Array<[number, string, string]>} cases x, pattern, expected */
function check(cases) {
	for (const [x, pattern, expected] of cases) {
		assert.equal(formatNumber(x, pattern), expected, `${x} as ${pattern}`);
	}
}

test('halves round away from zero on the decimal digits, unlike toFixed (spec 03 table)', () => {
	check([[2.675, '0.00', '2.68'], [1.005, '0.00', '1.01'], [0.045, '0.00', '0.05'], [59.2615, '0.000', '59.262']]);
});

test('optional digits, integer zeros and a literal suffix', () => {
	check([
		[0.5, '0.##', '0.5'], [2, '0.##', '2'], [2.675, '0.##', '2.68'],
		[7, '00', '07'], [7, '0000', '0007'], [2026, '00', '2026'], [0.5, '00', '01'], [2.675, '00', '03'],
		[1.5, '00.000#', '01.500'], [1.23456, '00.000#', '01.2346'], [59.2615, '00.000#', '59.2615'],
		[0.391312, '0.0000##', '0.391312'], [0.39, '0.0000##', '0.3900'], [0.00001, '0.0000##', '0.00001'],
		[-65.27, '0.0°', '-65.3°'], [104.25, '0.0°', '104.3°'], [21.5, '0.00°', '21.50°']
	]);
});

test('every negative value keeps its sign, −0 and values rounding to zero included', () => {
	check([
		[-0.04, '0.0', '-0.0'], [-0, '0.0', '-0.0'], [-0, '0.##', '-0'], [-0.00001, '0.000', '-0.000'],
		[0, '0.0', '0.0'], [-146, '0000', '-0146'], [-146, '00', '-146'], [-146, '0.0°', '-146.0°']
	]);
});

test('NaN and infinities print as .NET prints them, without the suffix', () => {
	check([[NaN, '0.0', 'NaN'], [Infinity, '0.0°', 'Infinity'], [-Infinity, '0000', '-Infinity']]);
});

test('15 significant digits first, ties to even on the exact value', () => {
	check([
		[0.30000000000000004, '0.##', '0.3'], [1e21, '0.0', '1000000000000000000000.0'], [1.23456789e-7, '0.0000##', '0.0000'],
		[100000000000000.5, '0.0', '100000000000000.0'], [100000000000001.5, '0.0', '100000000000002.0'],
		[1000000000000.125, '0.00', '1000000000000.12'], [1000000000000.375, '0.00', '1000000000000.38'],
		[0.125, '0.00', '0.13'], [99.99995, '0.0000', '100.0000'], [9.9999999, '0.000000', '10.000000']
	]);
});

test('agrees with .NET 8 (invariant culture) on a sample of random values', () => {
	// from a one-off run of x.ToString(pattern) over 114 000 random values and every pattern the
	// spec uses, all of which this module matches
	const sample = [
		['50419195.5099956', '0.0', '50419195.5'], ['11.4182', '0.0', '11.4'],
		['-0.2622628936906929', '0.00', '-0.26'], ['2541935.446645103', '0.00', '2541935.45'],
		['27.4215', '0.000', '27.422'], ['-37.7644', '0.000', '-37.764'],
		['0.10596990515216986', '0.000#', '0.106'], ['0.5307148683762986', '0.000#', '0.5307'],
		['-63578559.27', '0.0000', '-63578559.2700'], ['2494288.7378364285', '0.0000', '2494288.7378'],
		['-895190209.46', '0.0000##', '-895190209.4600'], ['2487533.723976246', '0.0000##', '2487533.723976'],
		['907721.8176613544', '0.00000', '907721.81766'], ['-37.009268898', '0.00000', '-37.00927'],
		['798.9998700000015', '0.00000#', '798.99987'], ['6.2139784321832164E-06', '0.00000#', '0.000006'],
		['35.6292', '0.000000', '35.629200'], ['6.806', '0.000000', '6.806000'],
		['23280900059.831696', '0.0000000', '23280900059.8317000'], ['-10.0894', '0.0000000', '-10.0894000'],
		['289.2819959999995', '0.00000000', '289.28199600'], ['360409.9239198752', '0.00000000', '360409.92391988'],
		['2593103.880711414', '0.0°', '2593103.9°'], ['-14.271187808542507', '0.0°', '-14.3°'],
		['0.0837815439888469', '0.00°', '0.08°'], ['2582111.0347202565', '0.00°', '2582111.03°'],
		['22.047184010235622', '00', '22'], ['12254809391.39392', '00', '12254809391'],
		['-37.7921', '000', '-038'], ['602.5303218999985', '000', '603'],
		['23.3299', '0000', '0023'], ['108.41671600000049', '0000', '0108'],
		['69.8483092391885', '00.000#', '69.8483'], ['2524599.4727707463', '00.000#', '2524599.4728'],
		['-45.5122', '00.00', '-45.51'], ['22839018449360.812', '00.00', '22839018449360.80'],
		['0.00023111455132461298', '0.##', '0'], ['2565255.674517367', '0.##', '2565255.67'],
		['765340447638576.5', '0.0', '765340447638576.0']
	];
	check(sample.map(([x, pattern, expected]) => [Number(x), pattern, expected]));
});

test('the exact decimal of a float64', () => {
	assert.deepEqual(exactDecimal(0.5), { digits: '5', scale: 1 });
	assert.deepEqual(exactDecimal(0), { digits: '0', scale: 0 });
	assert.deepEqual(exactDecimal(2 ** 60), { digits: '1152921504606846976', scale: 0 });
	// 0.1 is 0.1000000000000000055511151231257827021181583404541015625 exactly
	const tenth = exactDecimal(0.1);
	assert.equal(tenth.digits.replace(/0+$/, ''), '1000000000000000055511151231257827021181583404541015625');
	assert.equal(tenth.scale - (tenth.digits.length - tenth.digits.replace(/0+$/, '').length), 55);
	assert.equal(exactDecimal(5e-324).scale, 1074);
});

test('patterns outside the supported grammar are programmer errors', () => {
	for (const pattern of ['#.##', '0.00%', '0,000', "0.0'x'", '0.0;-0.0', '0.00E+0', '0.0#0', '']) {
		assert.throws(() => parseNumberPattern(pattern), undefined, pattern);
	}
	assert.deepEqual(parseNumberPattern('0.0000##'), { intZeros: 1, minDecimals: 4, maxDecimals: 6, suffix: '' });
	assert.deepEqual(parseNumberPattern('0.0°'), { intZeros: 1, minDecimals: 1, maxDecimals: 1, suffix: '°' });
});

test('without a format: the shortest round-trip digits, E notation from 1E+17 and at 1E-05', () => {
	assert.deepEqual(
		[1e16, 1e17, 1.5e16, 1e21, 0.0001, 0.00001, 1.2345e-5, 123.456, 586.8000000000014, -66.993100058, 123456789012345.6, -0, NaN, -Infinity].map(formatShortest),
		['10000000000000000', '1E+17', '15000000000000000', '1E+21', '0.0001', '1E-05', '1.2345E-05', '123.456', '586.8000000000014', '-66.993100058', '123456789012345.6', '-0', 'NaN', '-Infinity']
	);
});
