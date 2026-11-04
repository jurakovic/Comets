// TheSky (Soft06Cmt), and Autostar (Soft16Cmt), which is the same format. Separated by `|`, with
// k × 2.5. docs/spec/03-element-formats.md, TheSky and Autostar.
import { compileFormat } from '../format/composite.js';
import { finish, keptName } from './record.js';
import { dbl, int, item, pad4, padZeros, sub, trim } from './text.js';

/** @type {import('./registry.js').Reader} */
export const reader = {
	first: 0,
	step: 1,
	size: 1,
	parse(record) {
		const p = item(record, 0).split('|');
		// yyyyMMdd.ffff
		const date = padZeros(trim(item(p, 2)), 13);
		return finish({
			...keptName(trim(item(p, 0))),
			Ty: int(sub(date, 0, 4)),
			Tm: int(sub(date, 4, 2)),
			Td: int(sub(date, 6, 2)),
			Th: int(pad4(sub(date, 9, 4))),
			q: dbl(item(p, 3)),
			e: dbl(item(p, 4)),
			w: dbl(item(p, 5)),
			N: dbl(item(p, 6)),
			i: dbl(item(p, 7)),
			g: dbl(item(p, 8)),
			k: dbl(item(p, 9)) / 2.5
		});
	}
};

const LINE = compileFormat('{0,-39}|2000|{1,4}{2:00}{3:00}.{4:0000} |{5,9:0.000000} |{6:0.000000} |{7,8:0.0000} |{8,8:0.0000} |{9,8:0.0000} |{10,4:0.0} |{11,4:0.0} | MPC 00000');

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines: c => [LINE([c.full, c.Ty, c.Tm, c.Td, c.Th, c.q, c.e, c.w, c.N, c.i, c.g, c.k * 2.5])]
};
