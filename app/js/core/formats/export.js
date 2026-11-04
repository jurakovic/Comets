// Writing a collection in a format (`ExportMain`), with CRLF after every line.
// docs/spec/03-element-formats.md, Export rules.
import { instantToParts } from '../time/calendar.js';
import { jd0 } from '../time/julian.js';
import { formatByKey } from './registry.js';

/**
 * the text of the comets in a format: its header, then each comet's lines in collection order
 * @param {string} key a format with a writer
 * @param {ReadonlyArray<import('../comets/comet.js').Comet>} comets
 * @param {number} now UTC milliseconds; some formats write today's UTC date
 * @returns {string}
 */
export function exportElements(key, comets, now) {
	const { writer } = formatByKey(key);
	if (!writer) throw new Error(`${key} cannot be exported`);
	const { year, month, day } = instantToParts(now);
	const today = Object.freeze({ year, month, day, jd: jd0(year, month, day, 0) });
	const lines = writer.header ? writer.header(comets) : [];
	for (const comet of comets) {
		for (const line of writer.lines(comet, today)) lines.push(line);
	}
	return lines.length === 0 ? '' : `${lines.join('\r\n')}\r\n`;
}
