// Earth Centered Universe (Soft10Cmt): records of two lines, the full name first, k × 2.5.
// docs/spec/03-element-formats.md, Earth Centered Universe.
import { compileFormat } from '../format/composite.js';
import { dayAndFraction, finish, keptName } from './record.js';
import { dbl, int, item, trim } from './text.js';

/** @type {import('./registry.js').Reader} */
export const reader = {
	first: 0,
	step: 2,
	size: 2,
	parse(record) {
		const p = item(record, 1).split(' ');
		return finish({
			...keptName(trim(item(record, 0))),
			Ty: int(item(p, 3)),
			Tm: int(item(p, 4)),
			...dayAndFraction(item(p, 5)),
			q: dbl(item(p, 6)),
			e: dbl(item(p, 7)),
			w: dbl(item(p, 8)),
			N: dbl(item(p, 9)),
			i: dbl(item(p, 10)),
			g: dbl(item(p, 11)),
			k: dbl(item(p, 12)) / 2.5
		});
	}
};

const LINE = compileFormat('E C 2000 {0} {1:00} {2:00}.{3:0000} {4:0.000000} {5:0.000000} {6:0.0000} {7:0.0000} {8:0.0000} {9:0.0} {10:0.0}');

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines: c => [c.full, LINE([c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k * 2.5])]
};
