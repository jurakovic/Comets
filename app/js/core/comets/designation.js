// Designations: parsing full names into id, name and fragment, building them back, sort keys,
// and whether a comet is periodic. docs/spec/02-data-model.md, Designations and full names.

/**
 * @typedef {object} Designation
 * @property {string} id the designation without fragment: `1P`, `C/1995 O1`
 * @property {string} name the discoverer or common name, possibly empty
 * @property {string} fragment the fragment without its hyphen, possibly empty
 */

// pattern 00, designation first; the desktop's `(?<name>…)*` written as `?`, which is the same
const PATTERN_00 = /^(?<id>[0-9]+[PCXDIA]|[PCXDIA]\/-*[0-9]+ *[a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)[ /]*\(*(?<name>['`a-zA-Z0-9\- ]+)?/;

// what follows the name in pattern 02, matched where the name part ends
const REST_02 = /\(*(?<id>[0-9]+[PCXDIA]|[PCXDIA]\/-*[0-9]+ [a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)/y;

const NAME_CHARACTER = /^['`a-zA-Z\- ]$/;
const NAME_OR_DIGIT = /^['`a-zA-Z\- 0-9]$/;
const TOKEN = /['`a-zA-Z\- ]+[0-9]*/g;
const TYPE_LETTERS = 'PCXDIA';
// Char.IsDigit is any Unicode decimal digit
const LEADING_DIGIT = /^\p{Nd}/u;

/**
 * @param {RegExpExecArray | null} match
 * @returns {Designation | null}
 */
function designation(match) {
	if (!match?.groups) return null;
	const { id = '', name = '', fragment = '' } = match.groups;
	return { id, name, fragment };
}

/**
 * pattern 00, designation first: `1P/Halley`, `C/1995 O1 (Hale-Bopp)`, `3D-A/Biela`
 * @param {string} text
 * @returns {Designation | null} null where the desktop throws "Error parsing comet name"
 */
export function parseDesignationFirst(text) {
	return designation(PATTERN_00.exec(text));
}

/**
 * the last token of a name part, each token a run of name characters and then a run of digits
 * @param {string} text name characters and digits, starting with a name character
 * @returns {string}
 */
export function lastToken(text) {
	const tokens = text.match(TOKEN);
	return tokens ? tokens[tokens.length - 1] : '';
}

/**
 * pattern 02, name first: `Vaisala (C/1942 EA)`, `P/PANSTARRS 2 (470P-A)`; the linear procedure
 * that stands in for the desktop regex, which backtracks exponentially (spec 10, D3)
 * @param {string} text
 * @returns {Designation | null}
 */
export function parseNameFirst(text) {
	let prefixes = 0;
	while (2 * prefixes + 1 < text.length && TYPE_LETTERS.includes(text[2 * prefixes]) && text[2 * prefixes + 1] === '/') {
		prefixes += 1;
	}
	for (let k = prefixes; k >= 0; k--) {
		const t = text.slice(2 * k);
		let run = 0;
		while (run < t.length && NAME_OR_DIGIT.test(t[run])) run += 1;
		// name parts end with a space, longest first, then no name part at all
		const lengths = [];
		if (t.length > 0 && NAME_CHARACTER.test(t[0])) {
			for (let j = run - 1; j >= 0; j--) if (t[j] === ' ') lengths.push(j + 1);
		}
		lengths.push(0);
		for (const m of lengths) {
			REST_02.lastIndex = m;
			const rest = designation(REST_02.exec(t));
			if (rest) return { ...rest, name: m === 0 ? '' : lastToken(t.slice(0, m - 1)) };
		}
	}
	return null;
}

/**
 * the full name of the parts (`GetFullFromIdName`): `C/1995 O1 (Hale-Bopp)`, `73P-BT/Name`
 * @param {string} id
 * @param {string} name
 * @param {string} fragment
 * @returns {string}
 */
export function fullName(id, name, fragment) {
	let full = id;
	if (fragment !== '') full += `-${fragment}`;
	if (id.includes('/') && name !== '') full += ` (${name})`;
	else if (name !== '') full += `/${name}`;
	return full;
}

/**
 * leading letters and the digits after them, each left-padded with 0
 * @param {string} code
 * @param {number} width
 */
function lettersAndDigits(code, width) {
	const [, letters, digits] = /^([a-zA-Z]*)([0-9]*)/.exec(code) ?? ['', '', ''];
	return letters.padStart(width, '0') + digits.padStart(width, '0');
}

// a number as .NET's Double.TryParse reads it here (spec 03, dbl): sign, thousands commas,
// fraction and exponent
const LENIENT_NUMBER = /^\s*[+-]?(?:[0-9][0-9,]*(?:\.[0-9]*)?|\.[0-9]+)(?:[eE][+-]?[0-9]+)?\s*$/;

/**
 * the year text of an id as the desktop reads it (`(int)Double()`): truncated, 0 when it does not parse
 * @param {string} text
 */
function yearOf(text) {
	return LENIENT_NUMBER.test(text) ? Math.trunc(Number(text.replaceAll(',', ''))) + 0 : 0;
}

/**
 * the database ordering key (`GetSortkey`): periodic numbers, then negative years, then
 * positive years, then interstellar objects; 15 characters for every well-formed id
 * @param {string} id
 * @param {string} fragment
 * @returns {string | null} null where the desktop throws, which skips the record (spec 03)
 */
export function sortKey(id, fragment) {
	if (id === '') return null;
	let key;
	if (LEADING_DIGIT.test(id)) {
		const group = id.endsWith('I') ? '3' : '0';
		key = group + id.slice(0, -1).padStart(4, '0') + '000000';
	} else {
		if (id.length < 3) return null;
		const spaced = id.split(' ');
		const yearText = spaced[0].split('/')[1];
		if (yearText === undefined || spaced.length < 2) return null;
		let year = yearOf(yearText);
		if (year < 0) year += 10_000;
		key = (id[2] === '-' ? '1' : '2') + String(year).padStart(4, '0') + lettersAndDigits(spaced[1], 3);
	}
	return key + (fragment === '' ? '0000' : lettersAndDigits(fragment, 2));
}

/**
 * numbered periodic comets (`1P`, `73P-BT`) and `P/` designations
 * @param {string} id
 * @returns {boolean}
 */
export function isPeriodic(id) {
	return (id !== '' && LEADING_DIGIT.test(id) && (id.endsWith('P') || id.includes('P-'))) || id.startsWith('P/');
}
