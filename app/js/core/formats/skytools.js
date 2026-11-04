// SkyTools (Soft15Cmt): three decimals of the day fraction. docs/spec/03-element-formats.md,
// SkyTools.
import { compileFormat } from '../format/composite.js';
import { finish, rebuiltName } from './record.js';
import { column, dbl, int, item, pad4, sub } from './text.js';

/** @type {import('./registry.js').Reader} */
export const reader = {
	first: 0,
	step: 1,
	size: 1,
	parse(record) {
		const line = item(record, 0);
		return finish({
			...rebuiltName(column(line, 2, 41)),
			Ty: int(column(line, 54, 4)),
			Tm: int(column(line, 59, 2)),
			Td: int(column(line, 62, 2)),
			// `976 ` → `9760`
			Th: int(pad4(sub(line, 65, 4))),
			q: dbl(column(line, 70, 9)),
			e: dbl(column(line, 82, 8)),
			w: dbl(column(line, 91, 8)),
			N: dbl(column(line, 99, 8)),
			i: dbl(column(line, 107, 7)),
			g: dbl(column(line, 115, 5)),
			k: dbl(column(line, 122, 4))
		});
	}
};

const LINE = compileFormat('C {0,-40} {1} {2:00} {3:00} {4,4} {5:00} {6:00}.{7:000}  {8,9:0.000000}   {9:0.000000} {10,7:0.000} {11,7:0.000} {12,7:0.000}  {13,4:0.0}  {14,4:0.0} 0.002000 MPC 00000');

/** @type {import('./registry.js').Writer} */
export const writer = {
	// Th div 10 drops the last digit without rounding
	lines: (c, today) => [LINE([
		c.full.replaceAll('(', '').replaceAll(')', ''), today.year, today.month, today.day,
		c.Ty, c.Tm, c.Td, Math.trunc(c.Th / 10), c.q, c.e, c.w, c.N, c.i, c.g, c.k
	])]
};
