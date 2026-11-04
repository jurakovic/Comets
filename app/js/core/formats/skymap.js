// SkyMap (Soft01Cmt). docs/spec/03-element-formats.md, SkyMap.
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
			...rebuiltName(column(line, 0, 44)),
			Ty: int(column(line, 47, 4)),
			Tm: int(column(line, 52, 2)),
			Td: int(column(line, 55, 2)),
			Th: int(pad4(sub(line, 58, 4))),
			q: dbl(column(line, 63, 10)),
			e: dbl(column(line, 78, 10)),
			w: dbl(column(line, 88, 9)),
			N: dbl(column(line, 97, 9)),
			i: dbl(column(line, 106, 9)),
			g: dbl(column(line, 115, 5)),
			k: dbl(column(line, 121, 5))
		});
	}
};

const LINE = compileFormat('{0,-47}{1,4} {2:00} {3:00}.{4:0000} {5,9:0.000000}       {6:0.000000} {7,8:0.0000} {8,8:0.0000} {9,8:0.0000}  {10,4:0.0}  {11,4:0.0}');
const TYPE_LETTERS = 'PCXDIA';

/**
 * the name column: the name alone without an id, otherwise the full name without parentheses,
 * and with spaces for slashes after a numbered id
 * @param {import('../comets/comet.js').Comet} c
 */
function nameColumn(c) {
	if (c.id === '') return c.name;
	const bare = c.full.replaceAll('(', '').replaceAll(')', '');
	return TYPE_LETTERS.includes(c.id[0]) ? bare : bare.replaceAll('/', ' ');
}

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines: c => [LINE([nameColumn(c), c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k])]
};
