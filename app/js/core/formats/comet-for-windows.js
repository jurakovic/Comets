// Comet for Windows (.dat), import only: records of 13 `key=value` lines from line 6.
// docs/spec/03-element-formats.md, Comet for Windows.
import { dayAndFraction, finish, keptName } from './record.js';
import { dbl, int, item, trim } from './text.js';

/**
 * the value of a record's `key=value` line: the text between the first `=` and the next
 * @param {ReadonlyArray<string>} record
 * @param {number} index the line within the record
 * @returns {string}
 */
function value(record, index) {
	return item(item(record, index).split('='), 1);
}

/** @type {import('./registry.js').Reader} */
export const reader = {
	header: lines => lines.length >= 2 && lines[1] === '[File]',
	first: 6,
	step: 13,
	size: 12,
	parse(record) {
		const date = value(record, 3).split(' ');
		const gk = value(record, 11).split(' ');
		return finish({
			...keptName(trim(value(record, 0))),
			Ty: int(item(date, 0)),
			Tm: int(item(date, 1)),
			...dayAndFraction(item(date, 2)),
			q: dbl(value(record, 4)),
			e: dbl(value(record, 5)),
			w: dbl(value(record, 6)),
			N: dbl(value(record, 7)),
			i: dbl(value(record, 8)),
			g: dbl(item(gk, 0)),
			k: dbl(item(gk, 1)) / 2.5
		});
	}
};
