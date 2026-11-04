// Voyager II (Soft14Cmt): a 23-line header, an id or a name but not both, no g and k, and
// duplicates allowed. docs/spec/03-element-formats.md, Voyager II.
import { compileFormat } from '../format/composite.js';
import { MONTHS, monthAbbreviation } from './months.js';
import { dayAndFraction, finish } from './record.js';
import { column, dbl, fail, int, item, sub } from './text.js';

const HEADER = Object.freeze([
	'NOTE TO VOYAGER II USERS:',
	'',
	'   The following table will link the symbols below with the names used in',
	'the Voyager II "Define New Orbit..." dialog for comets.',
	'',
	'     q        perihelion distance (astronomical units)',
	'     e        eccentricity (no units)',
	'     i        inclination of orbit to ecliptic (degrees)',
	'     Node     longitude of ascending node (degrees)',
	'     w        argument of perihelion (degrees)',
	'     L        mean anomaly (this is 0 at perihelion) (degrees)',
	'     Date     epoch of orbit',
	'     Equinox  reference equinox (usually 2000.0)',
	'',
	'Save this page as plain text from your browser and use the table to input',
	'the orbital elements for the comets that you would like to plot and',
	'follow.  If you have any question, consult your software manual or the',
	'Carina web site: <a href="http://www.carinasoft.com">http://www.carinasoft.com</a>',
	'',
	'Thanks to the IAU Minor Planet Center & Central Bureau for Astronomical',
	'Telegrams for providing this information.',
	'',
	'Name                            q          e         i        Node         w       L      T(Date)    Equinox'
]);

const TYPE_LETTERS = 'PCXDIA';
// Char.IsDigit
const DIGIT = /^\p{Nd}/u;

/**
 * the name column as an id (`C/2023 R1`, `1P`) or a name; a type letter alone fails, as the
 * desktop reads the character after it
 * @param {string} text
 * @returns {{ full: string, id: string, name: string }}
 */
function idOrName(text) {
	if (text === '') fail('no name');
	const isType = TYPE_LETTERS.includes(text[0]);
	if (isType && text.length < 2) fail(`no character after ${text}`);
	return (isType && text[1] === '/') || DIGIT.test(text) ? { full: text, id: text, name: '' } : { full: text, id: '', name: text };
}

/** @type {import('./registry.js').Reader} */
export const reader = {
	header: lines => lines[0].startsWith('NOTE TO VOYAGER II USERS'),
	first: HEADER.length,
	step: 1,
	size: 1,
	duplicates: true,
	parse(record) {
		const line = item(record, 0);
		const month = MONTHS.indexOf(sub(line, 91, 3)) + 1;
		if (month === 0) fail('no month');
		return finish({
			...idOrName(column(line, 0, 27)),
			q: dbl(column(line, 27, 9)),
			e: dbl(column(line, 39, 8)),
			i: dbl(column(line, 49, 8)),
			N: dbl(column(line, 60, 8)),
			w: dbl(column(line, 71, 8)),
			Ty: int(column(line, 87, 4)),
			Tm: month,
			...dayAndFraction(column(line, 94, 7))
		}, { sortkey: '' });
	}
};

const DATE = compileFormat('{0,4}{1}{2}.{3:0000}');
const LINE = compileFormat('{0,-26} {1,9:0.000000}   {2:0.000000}  {3,8:0.0000}   {4,8:0.0000}   {5,8:0.0000}   0.0  {6,-14} 2000.0');

/** @type {import('./registry.js').Writer} */
export const writer = {
	header: () => [...HEADER],
	lines: c => [LINE([c.name !== '' ? c.name : c.id, c.q, c.e, c.i, c.N, c.w, DATE([c.Ty, monthAbbreviation(c.Tm), c.Td, c.Th])])]
};
