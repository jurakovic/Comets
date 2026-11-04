// JPL's ELEMENTS.COMET, import only, with its angles precessed from the osculation epoch to J2000
// (spec 10, Q7). docs/spec/03-element-formats.md, NASA ELEMENTS.COMET.
import { roundEven } from '../../lib/math.js';
import { precessToJ2000 } from '../astro/precession.js';
import { finish, keptName } from './record.js';
import { column, dbl, fail, int, item, padZeros } from './text.js';

/** @type {import('./registry.js').Reader} */
export const reader = {
	first: 2,
	step: 1,
	size: 1,
	parse(record) {
		const line = item(record, 0);
		const epoch = dbl(column(line, 44, 7));
		const angles = precessToJ2000(epoch, {
			i: dbl(column(line, 75, 9)),
			N: dbl(column(line, 95, 9)),
			w: dbl(column(line, 85, 9))
		});
		// five decimals of the day, rounded to four as Convert.ToInt32 rounds: ties to even
		const Th = roundEven(dbl(padZeros(column(line, 114, 5), 5)) / 10);
		if (!(Math.abs(Th) <= 2_147_483_647)) fail('Th out of range');
		return finish({
			...keptName(column(line, 0, 43)),
			q: dbl(column(line, 52, 11)),
			e: dbl(column(line, 64, 10)),
			...angles,
			Ty: int(column(line, 105, 4)),
			Tm: int(column(line, 109, 2)),
			Td: int(column(line, 111, 2)),
			Th
		});
	}
};
