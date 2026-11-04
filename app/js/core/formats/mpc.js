// MPC (Soft00Cmt), also the database's storage format. docs/spec/03-element-formats.md, MPC.
import { compileFormat } from '../format/composite.js';
import { finish, keptName } from './record.js';
import { dbl, int, item, pad4, sub, trim } from './text.js';

/** @type {import('./registry.js').Reader} */
export const reader = {
	first: 0,
	step: 1,
	size: 1,
	parse(record) {
		const line = item(record, 0);
		return finish({
			...keptName(trim(sub(line, 102, 55))),
			Ty: int(sub(line, 14, 4)),
			Tm: int(sub(line, 19, 2)),
			Td: int(sub(line, 22, 2)),
			Th: int(pad4(sub(line, 25, 4))),
			q: dbl(sub(line, 30, 9)),
			e: dbl(sub(line, 41, 8)),
			w: dbl(sub(line, 51, 8)),
			N: dbl(sub(line, 61, 8)),
			i: dbl(sub(line, 71, 8)),
			g: dbl(sub(line, 91, 4)),
			k: dbl(sub(line, 96, 4))
		});
	}
};

// the epoch-of-osculation column gets today's date (spec 10, Q12)
const LINE = compileFormat('              {0,4} {1:00} {2,2}.{3:0000} {4,9:0.000000}  {5:0.000000}  {6,8:0.0000}  {7,8:0.0000}  {8,8:0.0000}  {9}{10:00}{11:00}  {12,4:0.0} {13,4:0.0}  {14,-56} MPC 00000');

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines: (c, today) => [LINE([c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, today.year, today.month, today.day, c.g, c.k, c.full])]
};
