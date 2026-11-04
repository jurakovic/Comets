// Dance of the Planets (Soft11Cmt): a 5-line header, no g and k, and q ≥ 10 AU written as stars.
// docs/spec/03-element-formats.md, Dance of the Planets.
import { compileFormat } from '../format/composite.js';
import { formatNumber } from '../format/number.js';
import { spacedId } from './pc-tcs.js';
import { finish, rebuiltName } from './record.js';
import { column, dbl, from, int, item, pad4, sub, trim } from './text.js';

// the first line as MPC's own file has it, not the desktop's mojibake (spec 10, D2)
const HEADER = Object.freeze([
	'Comet      peri(au)   e         ix       jx       wx     peridate     name',
	'(In order to be recognised by Dance of the Planets, this file)',
	'(must have a .cmt extension.)',
	'(File prepared by IAU Minor Planet Center/Central Bureau)',
	'(for Astronomical Telegrams.)'
]);

/** @type {import('./registry.js').Reader} */
export const reader = {
	header: lines => lines[0].startsWith('Comet      peri(au)'),
	first: HEADER.length,
	step: 1,
	size: 1,
	parse(record) {
		const line = item(record, 0);
		const id = spacedId(column(line, 0, 11));
		const fields = {
			// `********` for q ≥ 10 AU does not parse, so such comets are lost (spec 10, Q15)
			q: dbl(column(line, 11, 9)),
			e: dbl(column(line, 20, 9)),
			i: dbl(column(line, 29, 9)),
			N: dbl(column(line, 38, 9)),
			w: dbl(column(line, 47, 9)),
			Ty: int(column(line, 56, 4)),
			Tm: int(column(line, 61, 2)),
			Td: int(column(line, 63, 2)),
			Th: int(pad4(sub(line, 65, 4)))
		};
		const name = line.length === 69 ? '' : trim(from(line, 70));
		return finish({ ...rebuiltName(`${id} ${name}`), ...fields });
	}
};

const LINE = compileFormat('{0,-11}{1} {2:0.000000} {3,8:0.0000} {4,8:0.0000} {5,8:0.0000} {6,4}.{7:00}{8:00}{9:0000} {10}');

/** @type {import('./registry.js').Writer} */
export const writer = {
	header: () => [...HEADER],
	// the fragment is not written (spec 10, Q1)
	lines: c => [LINE([
		c.id.replaceAll(' ', ''), c.q < 10 ? formatNumber(c.q, '0.000000') : '********',
		c.e, c.i, c.N, c.w, c.Ty, c.Tm, c.Td, c.Th, c.name
	])]
};
