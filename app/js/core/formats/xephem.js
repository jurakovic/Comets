// xephem (Soft03Cmt): ellipses with a mean anomaly at an epoch, parabolas, hyperbolas.
// docs/spec/03-element-formats.md, xephem.
import { derivedElements } from '../comets/comet.js';
import { compileFormat } from '../format/composite.js';
import { jd0 } from '../time/julian.js';
import { dayAndFraction, finish, keptName } from './record.js';
import { dayFraction, dbl, dec, fail, from, int, item, jdDate, round6, trim } from './text.js';

/**
 * a `month/day.fraction/year` date
 * @param {string} text
 * @returns {{ Ty: number, Tm: number, Td: number, Th: number }}
 */
function slashDate(text) {
	const parts = text.split('/');
	return { Tm: int(item(parts, 0)), Ty: int(item(parts, 2)), ...dayAndFraction(item(parts, 1)) };
}

/**
 * an ellipse: its perihelion from the mean anomaly M at the epoch, unless M is 0
 * @param {ReadonlyArray<string>} p
 */
function ellipse(p) {
	const a = dbl(item(p, 5));
	const n = dec(item(p, 6));
	const e = dbl(item(p, 7));
	const M = dec(item(p, 8));
	const epoch = slashDate(item(p, 9));
	const fields = {
		i: dbl(item(p, 2)),
		N: dbl(item(p, 3)),
		w: dbl(item(p, 4)),
		e,
		g: dbl(from(item(p, 11), 2)),
		k: dbl(item(p, 12)),
		q: round6(a * (1 - e))
	};
	if (M === 0) return { ...fields, ...epoch };
	if (n === 0) fail('mean motion 0');
	let t = jd0(epoch.Ty, epoch.Tm, epoch.Td, epoch.Th) - M / n;
	// past aphelion, the next perihelion; the desktop's decimal holds P to 15 digits
	if (M >= 180) t += Number(derivedElements(fields.q, e).P.toPrecision(15)) * 365.25;
	const date = jdDate(t);
	return { ...fields, Ty: date.year, Tm: date.month, Td: date.day, Th: dayFraction(date) };
}

/** @type {import('./registry.js').Reader} */
export const reader = {
	first: 1,
	step: 2,
	size: 1,
	parse(record) {
		const p = item(record, 0).split(',');
		const name = keptName(trim(item(p, 0)));
		switch (item(p, 1)) {
			case 'e':
				return finish({ ...name, ...ellipse(p) });
			case 'p':
				return finish({
					...name,
					...slashDate(item(p, 2)),
					i: dbl(item(p, 3)),
					w: dbl(item(p, 4)),
					q: dbl(item(p, 5)),
					N: dbl(item(p, 6)),
					g: dbl(item(p, 8)),
					k: dbl(item(p, 9)),
					e: 1
				});
			case 'h':
				return finish({
					...name,
					...slashDate(item(p, 2)),
					i: dbl(item(p, 3)),
					N: dbl(item(p, 4)),
					w: dbl(item(p, 5)),
					e: dbl(item(p, 6)),
					q: dbl(item(p, 7)),
					g: dbl(item(p, 9)),
					k: dbl(item(p, 10))
				});
			default:
				return fail('not an xephem orbit type');
		}
	}
};

const ELLIPSE = compileFormat('{0},e,{1:0.0000},{2:0.0000},{3:0.0000##},{4:0.0000##},{5:0.0000000},{6:0.00000000},0.0000,{7:00}/{8:00}.{9:0000}/{10},2000,g {11,4:0.0},{12:0.0}');
const PARABOLA = compileFormat('{0},p,{1:00}/{2:00.000#}/{3},{4:0.000#},{5:0.000#},{6:0.00000#},{7:0.000#},2000,{8:0.0},{9:0.0}');
const HYPERBOLA = compileFormat('{0},h,{1:00}/{2:00}.{3:0000}/{4},{5:0.0000},{6:0.0000},{7:0.0000},{8:0.000000},{9:0.000000},2000,{10:0.0},{11:0.0}');

/**
 * the full name, with `/` after every comet without a name, meant for `282P/` (spec 10, Q2)
 * @param {import('../comets/comet.js').Comet} c
 * @returns {string}
 */
export function nameWithSlash(c) {
	return c.name === '' ? `${c.full}/` : c.full;
}

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines(c) {
		const s = nameWithSlash(c);
		let line;
		if (c.e < 1) line = ELLIPSE([s, c.i, c.N, c.w, c.a, c.n, c.e, c.Tm, c.Td, c.Th, c.Ty, c.g, c.k]);
		else if (c.e === 1) line = PARABOLA([s, c.Tm, c.Td + c.Th / 10000, c.Ty, c.i, c.w, c.q, c.N, c.g, c.k]);
		else line = HYPERBOLA([s, c.Tm, c.Td, c.Th, c.Ty, c.i, c.N, c.w, c.e, c.q, c.g, c.k]);
		return ['# From MPC 00000', line];
	}
};
