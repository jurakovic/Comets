// Dates as .NET custom date formats print them, invariant culture.
// docs/spec/04-astronomy.md, Date text.

/** `dd.MM.yyyy. HH:mm:ss`: date pickers, the database view, graph tooltips */
export const DATE_FULL = 'dd.MM.yyyy. HH:mm:ss';
/** `dd MMM yyyy HH:mm:ss`: perihelion dates in texts, date preset labels */
export const DATE_PERIHELION = 'dd MMM yyyy HH:mm:ss';
/** `dd.MM.yyyy HH:mm`: ephemeris rows */
export const DATE_EPHEMERIS = 'dd.MM.yyyy HH:mm';
/** `dd MMM yyyy HH:mm`: graph axis labels up to 3 days */
export const DATE_GRAPH_LONG = 'dd MMM yyyy HH:mm';
/** `dd MMM yyyy`: graph axis labels over 3 days */
export const DATE_GRAPH_SHORT = 'dd MMM yyyy';
/** `yyyy-MM-dd_HHmmss`: file names */
export const DATE_FILENAME = 'yyyy-MM-dd_HHmmss';

const MONTHS = Object.freeze(['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']);

/**
 * @param {number} n
 * @param {number} width
 */
function pad(n, width) {
	return (n < 0 ? '-' : '') + String(Math.abs(n)).padStart(width, '0');
}

/** @type {Record<string, (parts: import('../time/calendar.js').DateParts) => string>} */
const FIELDS = {
	yyyy: parts => pad(parts.year, 4),
	MMM: parts => MONTHS[parts.month - 1],
	MM: parts => pad(parts.month, 2),
	dd: parts => pad(parts.day, 2),
	HH: parts => pad(parts.hour, 2),
	mm: parts => pad(parts.minute, 2),
	ss: parts => pad(parts.second, 2)
};

/**
 * a pattern turned into a function of date parts; fields are yyyy MMM MM dd HH mm ss, and
 * `. : - _` and spaces are literal
 * @param {string} pattern
 * @returns {(parts: import('../time/calendar.js').DateParts) => string}
 */
export function compileDateFormat(pattern) {
	/** @type {Array<string | ((parts: import('../time/calendar.js').DateParts) => string)>} */
	const pieces = [];
	const token = /yyyy|MMM|MM|dd|HH|mm|ss|[.:\-_ ]+/y;
	while (token.lastIndex < pattern.length) {
		const at = token.lastIndex;
		const match = token.exec(pattern);
		if (!match) throw new Error(`unsupported date format at ${at} in ${pattern}`);
		pieces.push(FIELDS[match[0]] ?? match[0]);
	}
	return parts => pieces.map(piece => typeof piece === 'string' ? piece : piece(parts)).join('');
}

/**
 * date parts as the pattern prints them; years ≤ 0 print as astronomical years (`-0146`)
 * @param {import('../time/calendar.js').DateParts} parts
 * @param {string} pattern
 * @returns {string}
 */
export function formatDate(parts, pattern) {
	return compileDateFormat(pattern)(parts);
}
