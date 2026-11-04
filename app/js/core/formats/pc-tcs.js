// PC-TCS (Soft09Cmt): separated by spaces, the id written without its space, k × 2.5.
// docs/spec/03-element-formats.md, PC-TCS.
import { compileFormat } from '../format/composite.js';
import { dayAndFraction, finish, rebuiltName } from './record.js';
import { dbl, fail, int, item, trim, trimEnd } from './text.js';

// Char.IsLetter
const LETTER = /\p{L}/u;

/**
 * an id written without its space given it back, before the first letter from index 2:
 * `C/1942EA` → `C/1942 EA`, `C/-146P1` → `C/-146 P1`; ids without `/` stay as they are
 * @param {string} id
 * @returns {string}
 */
export function spacedId(id) {
	if (!id.includes('/')) return id;
	let letter = 2;
	while (letter < id.length && !LETTER.test(id[letter])) letter += 1;
	if (letter >= id.length) fail(`no letter in ${id}`);
	return `${id.slice(0, letter)} ${id.slice(letter)}`;
}

/** @type {import('./registry.js').Reader} */
export const reader = {
	first: 0,
	step: 1,
	size: 1,
	parse(record) {
		const p = trimEnd(item(record, 0)).split(' ');
		const id = spacedId(item(p, 0));
		return finish({
			...rebuiltName(`${id} ${trim(p.slice(11).join(' '))}`),
			q: dbl(item(p, 1)),
			e: dbl(item(p, 2)),
			i: dbl(item(p, 3)),
			w: dbl(item(p, 4)),
			N: dbl(item(p, 5)),
			Ty: int(item(p, 6)),
			Tm: int(item(p, 7)),
			...dayAndFraction(item(p, 8)),
			g: dbl(item(p, 9)),
			k: dbl(item(p, 10)) / 2.5
		});
	}
};

const LINE = compileFormat('{0} {1:0.000000} {2:0.000000} {3:0.0000} {4:0.0000} {5:0.0000} {6,4} {7:00} {8:00}.{9:0000} {10:0.0} {11:0.0} {12}');

/** @type {import('./registry.js').Writer} */
export const writer = {
	// the fragment is not written (spec 10, Q1)
	lines: c => [LINE([c.id.replaceAll(' ', ''), c.q, c.e, c.i, c.w, c.N, c.Ty, c.Tm, c.Td, c.Th, c.g, c.k * 2.5, c.name]).padEnd(126)]
};
