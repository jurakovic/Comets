// Deep Space (Soft08Cmt): records of two lines, the name first (pattern 02), k × 2.5.
// docs/spec/03-element-formats.md, Deep Space.
import { parseNameFirst } from '../comets/designation.js';
import { compileFormat } from '../format/composite.js';
import { dayAndFraction, finish, rebuiltName } from './record.js';
import { dbl, int, item } from './text.js';

const HEADER = Object.freeze([
	'Type C: Equinox Year Month Day q e Peri Node i Mag k',
	'Type A: Equinox Year Month Day a M e Peri Node i H G'
]);

/** @type {import('./registry.js').Reader} */
export const reader = {
	header: lines => lines[0].startsWith('Type C:'),
	first: HEADER.length,
	step: 2,
	size: 2,
	parse(record) {
		const p = item(record, 1).split(' ');
		return finish({
			// the name line is read untrimmed
			...rebuiltName(item(record, 0), parseNameFirst),
			Ty: int(item(p, 2)),
			Tm: int(item(p, 3)),
			...dayAndFraction(item(p, 4)),
			q: dbl(item(p, 5)),
			e: dbl(item(p, 6)),
			w: dbl(item(p, 7)),
			N: dbl(item(p, 8)),
			i: dbl(item(p, 9)),
			g: dbl(item(p, 10)),
			k: dbl(item(p, 11)) / 2.5
		});
	}
};

const LINE = compileFormat('C J2000 {0} {1:00} {2:00}.{3:0000} {4:0.000000} {5:0.000000} {6:0.0000} {7:0.0000} {8:0.0000} {9:0.0} {10:0.0}');

/** @type {import('./registry.js').Writer} */
export const writer = {
	header: () => [...HEADER],
	// the fragment is not written (spec 10, Q1)
	lines: c => [`${c.name} (${c.id})`, LINE([c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k * 2.5])]
};
