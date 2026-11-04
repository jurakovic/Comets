// MegaStar V4.x (Soft12Cmt): the name, then the id, k × 2.5.
// docs/spec/03-element-formats.md, MegaStar V4.x.
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
			...rebuiltName(`${column(line, 30, 12)} ${column(line, 0, 30)}`),
			Ty: int(column(line, 42, 4)),
			Tm: int(column(line, 47, 2)),
			Td: int(column(line, 51, 2)),
			Th: int(pad4(sub(line, 54, 4))),
			q: dbl(column(line, 59, 12)),
			e: dbl(column(line, 73, 8)),
			w: dbl(column(line, 85, 8)),
			N: dbl(column(line, 97, 8)),
			i: dbl(column(line, 109, 8)),
			g: dbl(column(line, 119, 6)),
			k: dbl(column(line, 126, 6)) / 2.5
		});
	}
};

// the fragment is not written (spec 10, Q1)
const LINE = compileFormat('{0,-30}{1,-12}{2,4} {3:00}  {4:00}.{5:0000}   {6,9:0.000000}   {7:0.000000}    {8,8:0.0000}    {9,8:0.0000}    {10,8:0.0000}   {11,4:0.0}   {12,4:0.0}    2000 MPC 00000');

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines: c => [LINE([c.name, c.id, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k * 2.5])]
};
