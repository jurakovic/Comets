// A number field (`ValNum`): typing that would make the text invalid or out of range is refused
// in beforeinput, a `,` is typed as `.`, and ↑/↓ step an integer. The rules are core/input/number.js.
// docs/spec/09-settings-and-persistence.md, Number fields.
import {
	acceptsText, deleteFrom, inputModeOf, insertInto, matchesPattern, normalizeTyped, numberOf, stepText
} from '../../core/input/number.js';
import { el } from '../dom.js';

/** @typedef {import('../../core/input/number.js').NumberRange} NumberRange */

/**
 * @typedef {object} NumberField
 * @property {HTMLInputElement} element
 * @property {() => string} text
 * @property {() => number | null} value the number; null while empty, or out of range where that is marked
 * @property {(text: string) => void} setText as the owner sets it: no onInput
 * @property {(range: Readonly<NumberRange>) => void} setRange a new range for what is typed next
 */

/**
 * @param {object} options
 * @param {Readonly<NumberRange>} options.range
 * @param {string} [options.text]
 * @param {string} [options.label] the accessible name, when no label element names the field
 * @param {boolean} [options.step] whether ↑/↓ step the number: by default an integer field's do
 * @param {boolean} [options.markOutOfRange] keep the text to the pattern only and mark a number
 *   out of range, for a range that cannot be typed digit by digit (a JD)
 * @param {(text: string) => void} [options.onInput] after each change the user makes
 * @param {string} [options.className]
 * @returns {NumberField}
 */
export function createNumberField({ range, text = '', label, step, markOutOfRange = false, onInput, className = '' }) {
	let current = range;
	const element = el('input', {
		type: 'text', class: `number-field ${className}`.trim(), inputmode: inputModeOf(range),
		autocomplete: 'off', spellcheck: 'false', 'aria-label': label
	});
	element.value = text;
	// the text last accepted, to go back to after a change beforeinput could not foresee
	let accepted = text;

	/** @param {string} candidate */
	const allowed = candidate => markOutOfRange ? matchesPattern(candidate, current) : acceptsText(candidate, current);
	const inRange = () => acceptsText(element.value, current);

	function mark() {
		if (markOutOfRange && !inRange()) element.setAttribute('aria-invalid', 'true');
		else element.removeAttribute('aria-invalid');
	}

	function commit() {
		const changed = element.value !== accepted;
		accepted = element.value;
		mark();
		if (changed) onInput?.(accepted);
	}

	element.addEventListener('beforeinput', event => {
		const { inputType, data } = event;
		const start = element.selectionStart ?? element.value.length;
		const end = element.selectionEnd ?? start;
		/** @type {string | null} */
		let typed = null;
		/** @type {string} */
		let next;
		if (inputType === 'insertText' || inputType === 'insertFromPaste' || inputType === 'insertFromDrop' || inputType === 'insertReplacementText') {
			typed = data ?? event.dataTransfer?.getData('text/plain') ?? '';
			next = insertInto(element.value, start, end, typed);
		} else if (inputType === 'deleteContentBackward' || inputType === 'deleteContentForward') {
			next = deleteFrom(element.value, start, end, inputType === 'deleteContentBackward');
		} else {
			// composition, word and line deletions: checked once they are made
			return;
		}
		if (!allowed(next)) {
			event.preventDefault();
			return;
		}
		if (typed !== null && typed !== normalizeTyped(typed)) {
			event.preventDefault();
			element.setRangeText(normalizeTyped(typed), start, end, 'end');
			commit();
		}
	});
	element.addEventListener('input', () => {
		if (allowed(element.value)) {
			commit();
			return;
		}
		const caret = Math.min(element.selectionStart ?? accepted.length, accepted.length);
		element.value = accepted;
		element.setSelectionRange(caret, caret);
	});
	element.addEventListener('keydown', event => {
		const stepping = step ?? current.decimals === 0;
		if (!stepping || event.altKey || event.ctrlKey || event.metaKey || event.shiftKey) return;
		if (event.key !== 'ArrowUp' && event.key !== 'ArrowDown') return;
		const next = stepText(element.value, current, event.key === 'ArrowUp' ? 1 : -1);
		if (next === null) return;
		event.preventDefault();
		if (!allowed(next)) return;
		element.value = next;
		commit();
	});

	return Object.freeze({
		element,
		text: () => element.value,
		value: () => (markOutOfRange && !inRange() ? null : numberOf(element.value)),
		setText(/** @type {string} */ value) {
			element.value = value;
			accepted = value;
			mark();
		},
		setRange(/** @type {Readonly<NumberRange>} */ range) {
			current = range;
			element.inputMode = inputModeOf(range);
			mark();
		}
	});
}
