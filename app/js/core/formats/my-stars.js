// MyStars! (Soft05Cmt), tab separated, with the perihelion as an MJD and the longitude of
// perihelion. docs/spec/03-element-formats.md, MyStars!.
import { compileFormat } from '../format/composite.js';
import { finish, keptName } from './record.js';
import { dbl, dec, int, item, jdDate, pad4, trim } from './text.js';

const MJD = 2400000.5;

/** @type {import('./registry.js').Reader} */
export const reader = {
	header: lines => lines[0].startsWith('RDPC'),
	first: 1,
	step: 1,
	size: 1,
	parse(record) {
		const p = item(record, 0).split('\t');
		const time = item(p, 1).split('.');
		const date = jdDate(dec(item(time, 0)) + MJD);
		const N = dbl(item(p, 6));
		return finish({
			// the `;` that ends the name column stays (spec 10, Q3)
			...keptName(trim(item(p, 0))),
			Ty: date.year,
			Tm: date.month,
			Td: date.day,
			Th: int(pad4(item(time, 1))),
			e: dbl(item(p, 3)),
			q: dbl(item(p, 4)),
			i: dbl(item(p, 5)),
			N,
			// a remainder with the dividend's sign, as % is in C#
			w: (dbl(item(p, 2)) - N + 360) % 360,
			g: dbl(item(p, 7)),
			k: dbl(item(p, 8))
		});
	}
};

const LINE = compileFormat('{0};\t{1:0.0000}\t{2:0.0000}\t{3:0.000000}\t{4:0.000000}\t{5:0.0000}\t{6:0.0000}\t{7:0.0}\t{8:0.0}\tMPC00000\t{9:0.0}');

/** @type {import('./registry.js').Writer} */
export const writer = {
	header: comets => [`RDPC\t${comets.length}`],
	// the longitude of perihelion, w + N
	lines: (c, today) => [LINE([c.full, c.T - MJD, (c.w + c.N) % 360, c.e, c.q, c.i, c.N, c.g, c.k, today.jd - MJD])]
};
