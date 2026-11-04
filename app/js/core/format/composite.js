// .NET composite formatting: "{index[,alignment][:format]}" items over numbers and strings.
// docs/spec/03-element-formats.md, Export rules and Number formatting.
import { formatNumber, formatShortest, parseNumberPattern } from './number.js';

/**
 * @typedef {(args: ReadonlyArray<number | string>) => string} CompiledFormat
 */

/**
 * one argument as its format item prints it, before alignment
 * @param {number | string} value
 * @param {import('./number.js').NumberPattern | null} pattern
 * @returns {string}
 */
function itemText(value, pattern) {
	if (typeof value === 'string') return value;
	if (pattern) return formatNumber(value, pattern);
	// integers print plainly (an integer has no −0), other numbers as .NET prints a double
	return Number.isInteger(value) ? String(value + 0) : formatShortest(value);
}

/**
 * a template turned into a function of its arguments; `{{` and `}}` are literal braces
 * @param {string} template
 * @returns {CompiledFormat}
 */
export function compileFormat(template) {
	/** @type {Array<string | { index: number, width: number, pattern: import('./number.js').NumberPattern | null }>} */
	const parts = [];
	let literal = '';
	let position = 0;
	const item = /\{(\d+)(?:,(-?\d+))?(?::([^{}]*))?\}/y;
	while (position < template.length) {
		const char = template[position];
		const next = template[position + 1];
		if ((char === '{' && next === '{') || (char === '}' && next === '}')) {
			literal += char;
			position += 2;
		} else if (char === '{') {
			item.lastIndex = position;
			const match = item.exec(template);
			if (!match) throw new Error(`bad format item at ${position} in ${template}`);
			if (literal) parts.push(literal);
			literal = '';
			parts.push({
				index: Number(match[1]),
				width: match[2] === undefined ? 0 : Number(match[2]),
				pattern: match[3] === undefined || match[3] === '' ? null : parseNumberPattern(match[3])
			});
			position = item.lastIndex;
		} else if (char === '}') {
			throw new Error(`unmatched } at ${position} in ${template}`);
		} else {
			literal += char;
			position += 1;
		}
	}
	if (literal) parts.push(literal);

	return args => {
		let text = '';
		for (const part of parts) {
			if (typeof part === 'string') {
				text += part;
				continue;
			}
			if (part.index >= args.length) throw new Error(`format item {${part.index}} has no argument in ${template}`);
			const value = itemText(args[part.index], part.pattern);
			// alignment pads and never truncates
			text += part.width < 0 ? value.padEnd(-part.width) : value.padStart(part.width);
		}
		return text;
	};
}

/**
 * the template with its items filled in
 * @param {string} template
 * @param {ReadonlyArray<number | string>} args
 * @returns {string}
 */
export function format(template, args) {
	return compileFormat(template)(args);
}
