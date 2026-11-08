// The date picker's editor (`FormDateTime`): D M Y H M S in UTC, the local time, the JD, ↑/↓
// steps in every field, the presets, and OK, which clamps to the date range. The rules are
// core/input/date.js. docs/spec/09-settings-and-persistence.md, Date picker.
import {
	collectFields, DATE_FIELDS, datePresets, EDITOR_TITLE, fieldTexts, JD_RANGE, jdFieldDate, localText, okDate, partsOf, stepDate
} from '../../core/input/date.js';
import { el } from '../dom.js';
import { createMenu } from './menu.js';
import { createNumberField } from './number-field.js';

/** @typedef {import('../../core/time/calendar.js').DateParts} DateParts */

/**
 * @typedef {object} EditOptions
 * @property {number} value the date to start from, UTC milliseconds
 * @property {(instant: number) => DateParts} local local date parts, for the Local Time field
 * @property {() => Omit<import('../../core/input/date.js').PresetContext, 'value'>} presets now and
 *   the dates the presets offer, read as the presets menu opens
 */

let ids = 0;

/**
 * opens the editor over the page
 * @param {EditOptions} options
 * @returns {Promise<number | null>} the date OK keeps, or null for Cancel and Esc
 */
export function editDate({ value, local, presets }) {
	return new Promise(resolve => {
		const previous = document.activeElement;
		const titleId = `date-editor-${++ids}`;
		let selected = value;

		/** @type {Map<keyof DateParts, import('./number-field.js').NumberField>} */
		const fields = new Map();
		const jd = createNumberField({ range: JD_RANGE, label: 'JD', step: false, markOutOfRange: true, className: 'date-editor__jd', onInput: typedJd });
		const localField = el('input', { type: 'text', class: 'text-field date-editor__local', readonly: true, 'aria-label': 'Local Time', tabindex: '-1' });

		/** @param {DateField} field */
		const fieldOf = field => {
			const input = createNumberField({ range: field.range(partsOf(fieldTexts(selected))), label: field.name, step: false, className: 'date-editor__part', onInput: () => typed(field.key) });
			fields.set(field.key, input);
			stepWith(input.element, field.key);
			return el('label', { class: 'date-editor__field' }, el('span', { class: 'date-editor__letter', 'aria-hidden': 'true' }, field.label), input.element);
		};
		const [dateFields, timeFields] = [DATE_FIELDS.slice(0, 3), DATE_FIELDS.slice(3)].map(group => group.map(fieldOf));
		stepWith(jd.element, 'jd');

		const menu = createMenu({
			label: el('span', { class: 'date-editor__presets-icon' }),
			name: 'Date presets',
			className: 'date-picker__presets',
			entries: () => datePresets({ ...presets(), value: selected }).map(preset => 'separator' in preset
				? preset
				: { label: preset.label, run: () => { selected = preset.value; show(); } })
		});

		const ok = el('button', { type: 'submit', class: 'button button--primary', autofocus: true }, 'OK');
		const cancel = el('button', { type: 'button', class: 'button' }, 'Cancel');
		const form = el('form', { class: 'date-editor__form', method: 'dialog' },
			el('h2', { id: titleId, class: 'date-editor__title' }, EDITOR_TITLE),
			el('div', { class: 'date-editor__grid' },
				el('span', { class: 'date-editor__label' }, 'Date:'), el('div', { class: 'date-editor__parts' }, ...dateFields),
				el('span', { class: 'date-editor__label' }, 'Time:'), el('div', { class: 'date-editor__parts' }, ...timeFields),
				el('span', { class: 'date-editor__label' }, 'Local Time:'), localField,
				el('span', { class: 'date-editor__label' }, 'JD:'), el('div', { class: 'date-editor__jd-row' }, jd.element, menu.element)),
			el('div', { class: 'date-editor__actions' }, ok, cancel));
		const dialog = el('dialog', { class: 'date-editor', 'aria-labelledby': titleId }, form);

		/** the day's range from the month and year as typed, its text within it */
		function fitDay() {
			const day = /** @type {import('./number-field.js').NumberField} */ (fields.get('day'));
			const range = DATE_FIELDS[0].range(partsOf(texts()));
			day.setRange(range);
			const number = day.value();
			if (number !== null && number > range.max) day.setText(String(range.max));
		}

		/** @returns {Record<keyof DateParts, string>} */
		function texts() {
			/** @param {keyof DateParts} key */
			const text = key => fields.get(key)?.text() ?? '';
			return { day: text('day'), month: text('month'), year: text('year'), hour: text('hour'), minute: text('minute'), second: text('second') };
		}

		/**
		 * every field from the selected date, except the one being typed in
		 * @param {keyof DateParts | 'jd' | null} [typing]
		 */
		function show(typing = null) {
			const shown = fieldTexts(selected);
			for (const [key, field] of fields) if (key !== typing) field.setText(shown[key]);
			if (typing !== 'jd') jd.setText(shown.jd);
			fitDay();
			localField.value = localText(selected, local);
		}

		/** @param {keyof DateParts} key */
		function typed(key) {
			if (key === 'month' || key === 'year') fitDay();
			const collected = collectFields(texts());
			if (collected === null) return;
			selected = collected;
			show(key);
		}

		function typedJd() {
			const date = jdFieldDate(jd.text());
			if (date === null) return;
			selected = date;
			show('jd');
		}

		/**
		 * @param {HTMLInputElement} input
		 * @param {keyof DateParts | 'jd'} key
		 */
		function stepWith(input, key) {
			input.addEventListener('keydown', event => {
				if (event.key !== 'ArrowUp' && event.key !== 'ArrowDown') return;
				if (event.altKey || event.ctrlKey || event.metaKey || event.shiftKey) return;
				event.preventDefault();
				const next = stepDate(selected, key, event.key === 'ArrowUp' ? 1 : -1);
				if (next === null) return;
				selected = next;
				show();
			});
		}

		let done = false;
		/** @param {number | null} result */
		function finish(result) {
			if (done) return;
			done = true;
			if (dialog.open) dialog.close();
			dialog.remove();
			if (previous instanceof HTMLElement && previous.isConnected) previous.focus();
			resolve(result);
		}

		form.addEventListener('submit', event => {
			event.preventDefault();
			finish(okDate(selected));
		});
		cancel.addEventListener('click', () => finish(null));
		// Esc closes the dialog natively
		dialog.addEventListener('close', () => finish(null));

		show();
		document.body.append(dialog);
		dialog.showModal();
		ok.focus();
	});
}

/** @typedef {import('../../core/input/date.js').DateField} DateField */
