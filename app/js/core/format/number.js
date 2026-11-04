// .NET custom number formats ("0.000000", "00", "0.0000##", "0.0°") over float64 values.
// docs/spec/03-element-formats.md, Number formatting.

/**
 * a number as integer digits and a decimal scale: |x| = digits × 10^−scale
 * @typedef {{ digits: string, scale: number }} Decimal
 */

/**
 * a parsed pattern
 * @typedef {object} NumberPattern
 * @property {number} intZeros the integer part's minimum digits
 * @property {number} minDecimals
 * @property {number} maxDecimals
 * @property {string} suffix literal text after the digits
 */

// characters .NET gives a meaning in a custom format, which these patterns do not support
const SPECIAL = /[0#.,;%‰'"\\eE]/;

/**
 * @param {string} pattern
 * @returns {NumberPattern}
 */
export function parseNumberPattern(pattern) {
	const match = /^(0+)(?:\.(0*)(#*))?(.*)$/u.exec(pattern);
	if (!match || SPECIAL.test(match[4])) throw new Error(`unsupported number format ${pattern}`);
	const minDecimals = (match[2] ?? '').length;
	return Object.freeze({
		intZeros: match[1].length,
		minDecimals,
		maxDecimals: minDecimals + (match[3] ?? '').length,
		suffix: match[4]
	});
}

/**
 * the exact decimal value of a finite non-negative float64, from its bits
 * @param {number} value
 * @returns {Decimal}
 */
export function exactDecimal(value) {
	const bits = new DataView(new ArrayBuffer(8));
	bits.setFloat64(0, value);
	const biased = (bits.getUint32(0) >>> 20) & 0x7ff;
	const high = (bits.getUint32(0) & 0xfffff) | (biased === 0 ? 0 : 0x100000);
	const low = bits.getUint32(4);
	if (high === 0 && low === 0) return { digits: '0', scale: 0 };
	let exponent = biased === 0 ? -1074 : biased - 1075;
	// drop the mantissa's trailing zero bits, so 0.5 is 5 × 10^−1 and not 53 digits
	const trailing = low !== 0 ? 31 - Math.clz32(low & -low) : 32 + 31 - Math.clz32(high & -high);
	const shift = Math.min(trailing, Math.max(-exponent, 0));
	const mantissa = ((BigInt(high) << 32n) | BigInt(low)) >> BigInt(shift);
	exponent += shift;
	if (exponent >= 0) return { digits: (mantissa << BigInt(exponent)).toString(), scale: 0 };
	// m × 2^−k = m × 5^k × 10^−k
	return { digits: (mantissa * 5n ** BigInt(-exponent)).toString(), scale: -exponent };
}

/**
 * digits + 1 in the last place, as a digit string
 * @param {string} digits
 */
function increment(digits) {
	return digits === '' ? '1' : (BigInt(digits) + 1n).toString().padStart(digits.length, '0');
}

/**
 * the decimal cut to `keep` digits, rounding on the digits dropped
 * @param {Decimal} decimal
 * @param {number} keep how many leading digits stay
 * @param {(kept: string, dropped: string) => boolean} roundUp
 * @returns {Decimal}
 */
function cut({ digits, scale }, keep, roundUp) {
	const drop = digits.length - keep;
	if (drop <= 0) return { digits, scale };
	const kept = digits.slice(0, Math.max(keep, 0));
	const dropped = keep < 0 ? '0'.repeat(-keep) + digits : digits.slice(keep);
	return { digits: roundUp(kept, dropped) ? increment(kept) : kept || '0', scale: scale - drop };
}

/**
 * rounds to 15 significant digits with ties to even on the exact value, as .NET does before a
 * custom format (JavaScript's toPrecision rounds those ties up)
 * @param {Decimal} decimal
 * @returns {Decimal}
 */
function toFifteenDigits(decimal) {
	return cut(decimal, 15, (kept, dropped) => {
		if (dropped[0] !== '5') return dropped[0] > '5';
		if (/[1-9]/.test(dropped.slice(1))) return true;
		return Number(kept[kept.length - 1]) % 2 === 1;
	});
}

/**
 * x as .NET formats it with a custom pattern: 15 significant digits first, then the pattern's
 * decimals rounded half away from zero, with "-" for every negative x, −0 included (-0.04 → -0.0)
 * @param {number} x
 * @param {string | NumberPattern} pattern
 * @returns {string}
 */
export function formatNumber(x, pattern) {
	const { intZeros, minDecimals, maxDecimals, suffix } = typeof pattern === 'string' ? parseNumberPattern(pattern) : pattern;
	if (Number.isNaN(x)) return 'NaN';
	if (!Number.isFinite(x)) return x > 0 ? 'Infinity' : '-Infinity';
	const negative = x < 0 || Object.is(x, -0);

	let decimal = toFifteenDigits(exactDecimal(Math.abs(x)));
	if (decimal.scale > maxDecimals) {
		decimal = cut(decimal, decimal.digits.length - (decimal.scale - maxDecimals), (_kept, dropped) => dropped[0] >= '5');
	}
	const { digits, scale } = decimal;
	let integer, fraction;
	if (scale > 0) {
		const padded = digits.padStart(scale + 1, '0');
		integer = padded.slice(0, padded.length - scale);
		fraction = padded.slice(padded.length - scale);
	} else {
		integer = digits + '0'.repeat(-scale);
		fraction = '';
	}
	integer = integer.replace(/^0+/, '').padStart(intZeros, '0');
	fraction = fraction.replace(/0+$/, '').padEnd(minDecimals, '0');
	return (negative ? '-' : '') + integer + (fraction ? '.' + fraction : '') + suffix;
}

/**
 * x as .NET prints a double without a format string: the shortest digits that read back as x,
 * in E notation for exponents ≥ 17 or ≤ −5 (1E+17, 1.2345E-05)
 * @param {number} x
 * @returns {string}
 */
export function formatShortest(x) {
	if (Number.isNaN(x)) return 'NaN';
	if (!Number.isFinite(x)) return x > 0 ? 'Infinity' : '-Infinity';
	const sign = x < 0 || Object.is(x, -0) ? '-' : '';
	const [mantissa, exponentText] = Math.abs(x).toExponential().split('e');
	const exponent = Number(exponentText);
	const digits = mantissa.replace('.', '');
	if (exponent >= 17 || exponent <= -5) {
		const shown = digits.length > 1 ? `${digits[0]}.${digits.slice(1)}` : digits;
		return `${sign}${shown}E${exponent < 0 ? '-' : '+'}${String(Math.abs(exponent)).padStart(2, '0')}`;
	}
	if (exponent < 0) return `${sign}0.${'0'.repeat(-exponent - 1)}${digits}`;
	if (digits.length <= exponent + 1) return sign + digits + '0'.repeat(exponent + 1 - digits.length);
	return `${sign}${digits.slice(0, exponent + 1)}.${digits.slice(exponent + 1)}`;
}
