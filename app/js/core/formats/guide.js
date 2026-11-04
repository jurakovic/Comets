// Guide (Soft02Cmt): the name first, read with pattern 02. docs/spec/03-element-formats.md, Guide.
import { parseNameFirst } from '../comets/designation.js';
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
			...rebuiltName(column(line, 0, 42), parseNameFirst),
			Td: int(column(line, 43, 2)),
			Th: int(pad4(sub(line, 46, 4))),
			Tm: int(column(line, 52, 2)),
			Ty: int(column(line, 55, 5)),
			q: dbl(column(line, 73, 10)),
			e: dbl(column(line, 85, 10)),
			i: dbl(column(line, 95, 10)),
			w: dbl(column(line, 107, 10)),
			N: dbl(column(line, 119, 10)),
			g: dbl(column(line, 140, 5)),
			k: dbl(column(line, 145, 5))
		});
	}
};

// the fragment is not written (spec 10, Q1)
const LINE = compileFormat('{0,-43}{1,2}.{2:0000}  {3,2}  {4,4}  0.0        {5,9:0.000000}    {6:0.000000}  {7,8:0.0000}    {8,8:0.0000}    {9,8:0.0000}    2000.0   {10,4:0.0} {11,4:0.0}    MPC 00000');
// Char.IsNumber
const NUMBER = /^\p{N}/u;

/**
 * the name, then the id in parentheses, with `P/` before the name of a numbered comet
 * @param {import('../comets/comet.js').Comet} c
 */
function nameColumn(c) {
	if (c.name === '') return c.id;
	if (c.id === '') return c.name;
	return `${NUMBER.test(c.id) ? 'P/' : ''}${c.name} (${c.id})`;
}

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines: c => [LINE([nameColumn(c), c.Td, c.Th, c.Tm, c.Ty, c.q, c.e, c.i, c.w, c.N, c.g, c.k])]
};
