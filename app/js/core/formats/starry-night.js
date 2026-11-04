// Starry Night (Soft07Cmt): a 15-line header, the perihelion as a JD, k × 2.5.
// docs/spec/03-element-formats.md, Starry Night.
import { compileFormat } from '../format/composite.js';
import { finish, rebuiltName } from './record.js';
import { column, dayFraction, dbl, dec, item, jdDate } from './text.js';

const HEADER = Object.freeze([
	'NOTE: If viewing this file and it appears confused, make the window very wide!',
	'',
	'   The numbers are all in the proper format for easy use in Starry Night\'s',
	'orbit editor. Just click on the word Sun in the planet floater and then',
	'click on add. In the first window that appears select the comet as the type',
	'of object you want to add. Please see the manual for more information.',
	'',
	'   The orbital information should have the reference plane set at Ecliptic',
	' 2000 and the Style should be pericentric. Don\'t forget to use copy and',
	' paste to ease the input of the orbital data into Starry Night.',
	'',
	'This file kindly prepared by the IAU Minor Planet Center & Central Bureau for Astronomical Telegrams.',
	'',
	'Num  Name                          Mag.   Diam      e            q        Node         w         i         Tp           Epoch       k   Desig         Reference',
	''
]);

/** @type {import('./registry.js').Reader} */
export const reader = {
	header: lines => lines[0].startsWith('NOTE: If'),
	first: HEADER.length,
	step: 1,
	size: 1,
	parse(record) {
		const line = item(record, 0);
		const g = dbl(column(line, 34, 6));
		const e = dbl(column(line, 48, 10));
		const q = dbl(column(line, 59, 11));
		const N = dbl(column(line, 72, 10));
		const w = dbl(column(line, 82, 10));
		const i = dbl(column(line, 92, 10));
		const date = jdDate(dec(column(line, 102, 14)));
		const k = dbl(column(line, 129, 6)) / 2.5;
		return finish({
			// the id, then the name
			...rebuiltName(`${column(line, 136, 14)} ${column(line, 5, 29)}`),
			Ty: date.year,
			Tm: date.month,
			Td: date.day,
			Th: dayFraction(date),
			q, e, w, N, i, g, k
		});
	}
};

// the fragment is not written (spec 10, Q1)
const LINE = compileFormat('     {0,-29} {1,4:0.0}    0.0   {2:0.000000}   {3,9:0.000000}    {4,8:0.0000}  {5,8:0.0000}  {6,8:0.0000}  {7,12:0.0000}    {8,9:0.0}  {9,4:0.0}  {10,-13} MPC 00000');

/** @type {import('./registry.js').Writer} */
export const writer = {
	header: () => [...HEADER],
	lines: (c, today) => [LINE([c.name, c.g, c.e, c.q, c.N, c.w, c.i, c.T, today.jd, c.k * 2.5, c.id])]
};
