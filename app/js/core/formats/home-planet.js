// Home Planet (Soft04Cmt), comma separated. docs/spec/03-element-formats.md, Home Planet.
import { compileFormat } from '../format/composite.js';
import { dayAndFraction, finish, keptName } from './record.js';
import { dbl, int, item, trim } from './text.js';
import { nameWithSlash } from './xephem.js';

const HEADER = 'Name,Perihelion time,Perihelion AU,Eccentricity,Long. perihelion,Long. node,Inclination,Semimajor axis,Period';

/** @type {import('./registry.js').Reader} */
export const reader = {
	header: lines => lines[0].startsWith('Name,Perihelion'),
	first: 1,
	step: 1,
	size: 1,
	parse(record) {
		const p = item(record, 0).split(',');
		// year-month-day.fraction; a negative year cannot be read
		const date = item(p, 1).split('-');
		return finish({
			...keptName(trim(item(p, 0))),
			Ty: int(item(date, 0)),
			Tm: int(item(date, 1)),
			...dayAndFraction(item(date, 2)),
			q: dbl(item(p, 2)),
			e: dbl(item(p, 3)),
			w: dbl(item(p, 4)),
			N: dbl(item(p, 5)),
			i: dbl(item(p, 6))
		});
	}
};

const LINE = compileFormat('{0},{1}-{2}-{3}.{4:0000},{5:0.000000},{6:0.000000},{7:0.0000},{8:0.0000},{9:0.0000},{10:0.00000},{11:0.00} years, MPC 00000');
const LINE_NO_PERIOD = compileFormat('{0},{1}-{2}-{3}.{4:0000},{5:0.000000},{6:0.000000},{7:0.0000},{8:0.0000},{9:0.0000},,, MPC 00000');

/** @type {import('./registry.js').Writer} */
export const writer = {
	header: () => [HEADER],
	// hyperbolic orbits pass P < 30000 too (spec 10, Q4)
	lines: c => [c.P < 30000
		? LINE([nameWithSlash(c), c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.a, c.P])
		: LINE_NO_PERIOD([nameWithSlash(c), c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i])]
};
