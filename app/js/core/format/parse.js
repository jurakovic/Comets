// Reading text as .NET does: String.Trim's white space, and numbers as Convert.ToInt32,
// Convert.ToDouble and Convert.ToDecimal read them with the invariant culture.
// docs/spec/03-element-formats.md, Field access.

// .NET's String.Trim removes Char.IsWhiteSpace, which has U+0085 and lacks U+FEFF, unlike
// String.prototype.trim
const SPACES = new Set([
	0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x20, 0x85, 0xa0, 0x1680, 0x2000, 0x2001, 0x2002, 0x2003, 0x2004,
	0x2005, 0x2006, 0x2007, 0x2008, 0x2009, 0x200a, 0x2028, 0x2029, 0x202f, 0x205f, 0x3000
]);

/**
 * the text without leading and trailing white space, as .NET trims
 * @param {string} text
 * @returns {string}
 */
export function trim(text) {
	let start = 0;
	let end = text.length;
	while (start < end && SPACES.has(text.charCodeAt(start))) start += 1;
	while (end > start && SPACES.has(text.charCodeAt(end - 1))) end -= 1;
	return text.slice(start, end);
}

/**
 * the text without trailing white space, as .NET's TrimEnd
 * @param {string} text
 * @returns {string}
 */
export function trimEnd(text) {
	let end = text.length;
	while (end > 0 && SPACES.has(text.charCodeAt(end - 1))) end -= 1;
	return text.slice(0, end);
}

// the white space .NET number parsing skips (IsWhite): space and U+0009–U+000D
const W = '[\\t-\\r ]*';
const DIGITS = '(?:[0-9][0-9,]*(?:\\.[0-9]*)?|\\.[0-9]+)';
// .NET also takes NUL characters at the very end
const END = `${W}\\0*$`;
const INTEGER = new RegExp(`^${W}([+-]?[0-9]+)${END}`);
const DOUBLE = new RegExp(`^${W}([+-]?${DIGITS}(?:[eE][+-]?[0-9]+)?)${END}`);
const DECIMAL_LEADING = new RegExp(`^${W}([+-]?)(${DIGITS})${END}`);
const DECIMAL_TRAILING = new RegExp(`^${W}(${DIGITS})${W}([+-])${END}`);
// a decimal's largest magnitude is 2^96 − 1, which float64 holds as 2^96
const DECIMAL_MAX = 2 ** 96;
const INT32_MIN = -2_147_483_648;
const INT32_MAX = 2_147_483_647;

/**
 * an integer as Convert.ToInt32 reads text: white space, an optional sign, digits, white space,
 * within 32 bits; never −0
 * @param {string} text
 * @returns {number | null} null where .NET throws
 */
export function parseInteger(text) {
	const match = INTEGER.exec(text);
	if (!match) return null;
	const value = Number(match[1]) + 0;
	return value >= INT32_MIN && value <= INT32_MAX ? value : null;
}

/**
 * a number as Convert.ToDouble reads text: white space, an optional sign, digits with commas
 * among the integer digits ignored, an optional fraction (`.5` and `5.` are valid), an optional
 * exponent, white space. NaN and Infinity, which .NET also reads, are rejected (spec 03 allows it)
 * @param {string} text
 * @returns {number | null} null where .NET throws
 */
export function parseDouble(text) {
	const match = DOUBLE.exec(text);
	return match ? Number(match[1].replaceAll(',', '')) : null;
}

/**
 * a number as Convert.ToDecimal reads text: as parseDouble with no exponent, and the sign may
 * trail instead (`5-`), below 2^96; the desktop's exact decimal becomes a float64
 * @param {string} text
 * @returns {number | null} null where .NET throws
 */
export function parseDecimal(text) {
	const leading = DECIMAL_LEADING.exec(text);
	const [sign, digits] = leading ? [leading[1], leading[2]] : (DECIMAL_TRAILING.exec(text) ?? []).slice(1).reverse();
	if (digits === undefined) return null;
	const value = Number(digits.replaceAll(',', ''));
	if (value > DECIMAL_MAX) return null;
	return sign === '-' ? -value : value;
}
