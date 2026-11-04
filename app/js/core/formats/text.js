// Text conventions and field access: lines as File.ReadAllLines gives them, and the operations
// the record parsers read fields with. docs/spec/03-element-formats.md, Text conventions.
import { roundEven } from '../../lib/math.js';
import { daysInMonth } from '../time/calendar.js';
import { jdToParts } from '../time/julian.js';

/** a field access failed, which makes the record invalid; record parsers throw nothing else */
export class InvalidRecord extends Error {}

/**
 * @param {string} what names the failure, for debugging
 * @returns {never}
 */
export function fail(what) {
	throw new InvalidRecord(what);
}

/**
 * the record parser's result, or null where a field access failed; other errors are bugs and
 * propagate
 * @template T
 * @param {() => T} parse
 * @returns {T | null}
 */
export function attempt(parse) {
	try {
		return parse();
	} catch (error) {
		if (error instanceof InvalidRecord) return null;
		throw error;
	}
}

/**
 * the lines of a text, split on CRLF, LF or CR; a final terminator gives no empty last line, and
 * a leading BOM the decoder kept is dropped
 * @param {string} text
 * @returns {string[]}
 */
export function splitLines(text) {
	const lines = (text.charCodeAt(0) === 0xfeff ? text.slice(1) : text).split(/\r\n|\n|\r/);
	if (lines[lines.length - 1] === '') lines.pop();
	return lines;
}

/**
 * characters [start, start + length) of a line; fails on a shorter line, where slice would not
 * @param {string} line
 * @param {number} start
 * @param {number} length
 * @returns {string}
 */
export function sub(line, start, length) {
	if (line.length < start + length) fail(`line shorter than ${start + length}`);
	return line.slice(start, start + length);
}

/**
 * the characters from start to the end; fails on a line shorter than start
 * @param {string} line
 * @param {number} start
 * @returns {string}
 */
export function from(line, start) {
	if (line.length < start) fail(`line shorter than ${start}`);
	return line.slice(start);
}

/**
 * the field at index of a split, or of a record's lines; fails where there is none
 * @param {ReadonlyArray<string>} parts
 * @param {number} index
 * @returns {string}
 */
export function item(parts, index) {
	if (index >= parts.length) fail(`no field ${index}`);
	return parts[index];
}

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
 * the text right-padded with 0 to width characters; longer text stays as it is
 * @param {string} text
 * @param {number} width
 * @returns {string}
 */
export function padZeros(text, width) {
	return text.padEnd(width, '0');
}

/**
 * a day fraction's digits as Th reads them: trimmed, right-padded with 0 to 4 (`912` → `9120`)
 * @param {string} text
 * @returns {string}
 */
export function pad4(text) {
	return padZeros(trim(text), 4);
}

// the white space .NET number parsing skips (IsWhite): space and U+0009–U+000D
const W = '[\\t-\\r ]*';
const DIGITS = '(?:[0-9][0-9,]*(?:\\.[0-9]*)?|\\.[0-9]+)';
const INTEGER = new RegExp(`^${W}([+-]?[0-9]+)${W}$`);
const DOUBLE = new RegExp(`^${W}([+-]?${DIGITS}(?:[eE][+-]?[0-9]+)?)${W}$`);
const DECIMAL_LEADING = new RegExp(`^${W}([+-]?)(${DIGITS})${W}$`);
const DECIMAL_TRAILING = new RegExp(`^${W}(${DIGITS})${W}([+-])${W}$`);
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
 * trail instead (`5-`); the desktop's exact decimal becomes a float64
 * @param {string} text
 * @returns {number | null} null where .NET throws
 */
export function parseDecimal(text) {
	const leading = DECIMAL_LEADING.exec(text);
	const [sign, digits] = leading ? [leading[1], leading[2]] : (DECIMAL_TRAILING.exec(text) ?? []).slice(1).reverse();
	if (digits === undefined) return null;
	const value = Number(digits.replaceAll(',', ''));
	return sign === '-' ? -value : value;
}

/**
 * `int` of spec 03: parseInteger, failing the record
 * @param {string} text
 * @returns {number}
 */
export function int(text) {
	return parseInteger(text) ?? fail(`not an integer: ${text}`);
}

/**
 * `dbl` of spec 03: parseDouble, failing the record
 * @param {string} text
 * @returns {number}
 */
export function dbl(text) {
	return parseDouble(text) ?? fail(`not a number: ${text}`);
}

/**
 * `dec` of spec 03: parseDecimal, failing the record
 * @param {string} text
 * @returns {number}
 */
export function dec(text) {
	return parseDecimal(text) ?? fail(`not a decimal: ${text}`);
}

/**
 * x rounded to 6 decimals, ties to even (.NET Math.Round(x, 6))
 * @param {number} x
 * @returns {number}
 */
export function round6(x) {
	return roundEven(x * 1e6) / 1e6;
}

/**
 * the date parts of a JD as the importers get them from JDToDateTime: they fail where the
 * desktop's DateTime throws, for years before 1 or after 9999, a Julian 29 February the Gregorian
 * calendar lacks, and hour 24 (spec 10, D5 covers the rest of the app)
 * @param {number} jd
 * @returns {import('../time/calendar.js').DateParts}
 */
export function jdDate(jd) {
	const parts = jdToParts(jd);
	const { year, month, day, hour } = parts;
	if (year < 1 || year > 9999 || hour > 23 || day > daysInMonth(year, month)) fail(`no DateTime for JD ${jd}`);
	return parts;
}

/**
 * the day fraction of a time of day in units of 1e-4 day, as the importers that read a JD round it
 * @param {import('../time/calendar.js').DateParts} parts
 * @returns {number}
 */
export function dayFraction({ hour, minute, second }) {
	return roundEven(((hour + minute / 60 + second / 3600) / 24) * 10000);
}
