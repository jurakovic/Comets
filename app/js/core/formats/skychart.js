// SkyChart III (Soft13Cmt): tab separated, q stored negated. docs/spec/03-element-formats.md,
// SkyChart III.
import { compileFormat } from '../format/composite.js';
import { dayAndFraction, finish, keptName } from './record.js';
import { dbl, int, item, trim } from './text.js';

/** @type {import('./registry.js').Reader} */
export const reader = {
	first: 0,
	step: 1,
	size: 1,
	parse(record) {
		const p = item(record, 0).split('\t');
		// year/month/day.fraction
		const date = item(p, 8).split('/');
		const gk = item(p, 9).split(' ');
		return finish({
			...keptName(trim(item(p, 12).split(';')[0])),
			q: Math.abs(dbl(item(p, 2))),
			e: dbl(item(p, 3)),
			i: dbl(item(p, 4)),
			w: dbl(item(p, 5)),
			N: dbl(item(p, 6)),
			Ty: int(item(date, 0)),
			Tm: int(item(date, 1)),
			...dayAndFraction(item(date, 2)),
			g: dbl(item(gk, 0)),
			k: dbl(item(gk, 1))
		});
	}
};

const LINE = compileFormat('P11\t2000.0\t-{0:0.000000}\t{1:0.000000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t0\t{5}/{6:00}/{7:00}.{8:0000}\t{9:0.0} {10:0.0}\t0\t0\t{11}; MPC 00000');

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines: c => [LINE([c.q, c.e, c.i, c.w, c.N, c.Ty, c.Tm, c.Td, c.Th, c.g, c.k, c.full])]
};
