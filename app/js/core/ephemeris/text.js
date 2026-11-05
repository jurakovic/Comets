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

/**
 * the text of a calculation's results
 * @param {ReadonlyArray<import('./calculation.js').EphemerisResult>} results
 * @param {import('./settings.js').EphemerisSettings} settings
 * @param {string} timeZone for Local Time
 * @returns {string}
 */
export function ephemerisText(results, settings, timeZone) {
	const columns = COLUMNS.filter(c => settings.columns[c.key]);
	const header = (settings.localTime ? TIME_HEADER.local : TIME_HEADER.universal) + columns.map(c => c.header).join('');
	// rows show JDToDateTime of their JD, in local time when asked
	const parts = settings.localTime ? localPartsIn(timeZone) : instantToParts;
	/** @type {string[]} */
	const lines = [];
	for (const { comet, Tn, rows } of results) {
		lines.push(
			`Comet:               \t${comet.full}`,
			`Perihelion date:     \t${PERIHELION_DATE(instantToParts(jdToInstant(Tn)))}`,
			DISTANCE([comet.q]),
			`Period:              \t${comet.P < 10000 ? `${formatNumber(comet.P, '0.000000')} years` : '-'}`,
			'',
			header
		);
		for (const ep of rows) {
			lines.push(ROW_DATE(parts(jdToInstant(ep.JD))) + columns.map(c => c.cell(ep)).join(''));
		}
		lines.push('', '', '');
	}
	return lines.map(line => line + CRLF).join('');
}
