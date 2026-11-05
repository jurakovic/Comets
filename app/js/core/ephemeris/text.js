// The ephemeris text (`GenerateEphemerisAsync`): per comet a heading, the header line and a line
// per row, with CRLF line endings. docs/spec/06-ephemeris.md, Text output.
import { compileFormat } from '../format/composite.js';
import { compileDateFormat, DATE_EPHEMERIS, DATE_PERIHELION } from '../format/dates.js';
import { formatNumber } from '../format/number.js';
import { instantToParts } from '../time/calendar.js';
import { jdToInstant } from '../time/julian.js';
import { localPartsIn } from '../time/local.js';
import { COLUMNS, TIME_HEADER } from './columns.js';

const CRLF = '\r\n';
const PERIHELION_DATE = compileDateFormat(DATE_PERIHELION);
const ROW_DATE = compileDateFormat(DATE_EPHEMERIS);
const DISTANCE = compileFormat('Perihelion distance: \t{0:0.000000} AU');

/** rows between two yields, so that a runner can report and cancel within a long comet */
const LINES_PER_YIELD = 2000;

/**
 * writing the text of a calculation's results; yields `{ done, total }` in comets written, also
 * between rows of a long comet, as the desktop reports and checks for a cancel per comet
 * @param {ReadonlyArray<import('./calculation.js').EphemerisResult>} results
 * @param {import('./settings.js').EphemerisSettings} settings
 * @param {string} timeZone for Local Time
 * @returns {Generator<import('../../lib/rpc.js').Progress, string, void>}
 */
export function* writeEphemeris(results, settings, timeZone) {
	const columns = COLUMNS.filter(c => settings.columns[c.key]);
	const header = (settings.localTime ? TIME_HEADER.local : TIME_HEADER.universal) + columns.map(c => c.header).join('');
	// rows show JDToDateTime of their JD, in local time when asked
	const parts = settings.localTime ? localPartsIn(timeZone) : instantToParts;
	const total = results.length;
	/** @type {string[]} */
	const lines = [];
	for (let index = 0; index < total; index++) {
		const { comet, Tn, rows } = results[index];
		lines.push(
			`Comet:               \t${comet.full}`,
			`Perihelion date:     \t${PERIHELION_DATE(instantToParts(jdToInstant(Tn)))}`,
			DISTANCE([comet.q]),
			`Period:              \t${comet.P < 10000 ? `${formatNumber(comet.P, '0.000000')} years` : '-'}`,
			'',
			header
		);
		for (let k = 0; k < rows.length; k++) {
			if (k > 0 && k % LINES_PER_YIELD === 0) yield { done: index, total };
			const ep = rows[k];
			lines.push(ROW_DATE(parts(jdToInstant(ep.JD))) + columns.map(c => c.cell(ep)).join(''));
		}
		lines.push('', '', '');
		yield { done: index + 1, total };
	}
	return lines.map(line => line + CRLF).join('');
}

/**
 * the text of a calculation's results at once
 * @param {ReadonlyArray<import('./calculation.js').EphemerisResult>} results
 * @param {import('./settings.js').EphemerisSettings} settings
 * @param {string} timeZone for Local Time
 * @returns {string}
 */
export function ephemerisText(results, settings, timeZone) {
	const writing = writeEphemeris(results, settings, timeZone);
	let next = writing.next();
	while (!next.done) next = writing.next();
	return next.value;
}
