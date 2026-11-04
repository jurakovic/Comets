// Text conventions and field access: lines as File.ReadAllLines gives them, and the operations
// the record parsers read fields with. docs/spec/03-element-formats.md, Text conventions.
import { roundEven } from '../../lib/math.js';
import { parseDecimal, parseDouble, parseInteger, trim } from '../format/parse.js';
import { daysInMonth } from '../time/calendar.js';
import { jdToParts } from '../time/julian.js';

export { trim, trimEnd } from '../format/parse.js';

/**
 * a field access failed, which makes the record invalid; record parsers throw nothing else. Not
 * an Error: an invalid record is expected input, and capturing a stack for each costs more than
 * reading the record.
 */
export class InvalidRecord {
	/** @param {string} what names the failure, for debugging */
	constructor(what) {
		this.what = what;
	}
}

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
 * a fixed-width column, trimmed, as the desktop reads one before converting it
 * @param {string} line
 * @param {number} start
 * @param {number} length
 * @returns {string}
 */
export function column(line, start, length) {
	return trim(sub(line, start, length));
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
	// .NET leaves values this large as they are
	if (Math.abs(x) >= 1e16) return x;
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
	// a NaN or infinite JD (a mean motion of 0) is where the desktop's decimal division throws
	if (!Number.isFinite(jd)) fail(`no DateTime for JD ${jd}`);
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
