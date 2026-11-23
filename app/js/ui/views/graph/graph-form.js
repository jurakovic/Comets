// The Graph view's settings form (`FormGraphSettings`): the comet picker, Start and End, the chart
// type, the chart options with their colours and the value range, each error by its field, and
// Calculate. What the fields give is core/graph/form.js. INTERNALS.md, Views.
import { canCalculate, fieldsOf, settingsOf, typedLimit } from '../../../core/graph/form.js';
import { CHART_TYPES, RANGE_INPUT, validateGraphSettings } from '../../../core/graph/settings.js';
import { pickerListOver } from '../../../core/input/comet-picker.js';
import { createCometPicker } from '../../components/comet-picker.js';
import { createDatePicker } from '../../components/date-picker.js';
import { createNumberField } from '../../components/number-field.js';
import { el } from '../../dom.js';

/** @typedef {import('../../../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../../../core/graph/settings.js').GraphSettings} GraphSettings */
/** @typedef {import('../../../core/input/comet-picker.js').PickerChoice} PickerChoice */
/** @typedef {import('../../../core/input/comet-picker.js').PickerList} PickerList */
/** @typedef {import('../../../core/validation.js').ValidationError} ValidationError */
/** @typedef {import('../../components/number-field.js').NumberField} NumberField */

/**
 * an end of the value range: its checkbox, its field and where its error shows
 * @typedef {object} Limit
 * @property {{ element: HTMLElement, input: HTMLInputElement }} box
 * @property {NumberField} field
 * @property {HTMLElement} message
 */

/**
 * what the form holds, to calculate from or to show again: the picker's list and choice, the
 * settings the fields give, and the database the list was read over
 * @typedef {object} GraphDraft
 * @property {Readonly<PickerList>} list
 * @property {Readonly<PickerChoice>} choice
 * @property {GraphSettings} settings validated when Calculate hands them over
 * @property {ReadonlyArray<Comet>} database
 */

/**
 * @typedef {object} GraphForm
 * @property {HTMLFormElement} element
 * @property {() => Readonly<GraphDraft>} draft what the form holds now
 * @property {(draft: Readonly<GraphDraft>) => void} fill shows a draft, its list read again if the
 *   database has changed since; no error shows
 * @property {() => void} follow reads the picker's list again if the database has changed since
 * @property {(running: boolean) => void} setRunning Calculate waits, and Cancel shows, while a
 *   calculation runs
 * @property {(title: string | null) => void} setEditing the result Calculate replaces; null for a new one
 */

/** the value range's ends, in the form's order */
const LIMITS = Object.freeze([
	Object.freeze({ key: 'minimum', label: 'Minimum' }),
	Object.freeze({ key: 'maximum', label: 'Maximum' })
]);

let ids = 0;

/**
 * a checkbox or radio button with its label after it
 * @param {string} label
 * @param {Record<string, string>} [attributes]
 * @returns {{ element: HTMLElement, input: HTMLInputElement }}
 */
function check(label, attributes = { type: 'checkbox' }) {
	const input = el('input', { class: 'graph-form__box', ...attributes });
	return { input, element: el('label', { class: 'graph-form__check' }, input, label) };
}

/** @param {string} label names the colour, for the input has no label of its own */
const colour = label => el('input', { type: 'color', class: 'graph-form__colour', 'aria-label': label });

/**
 * @param {string} legend
 * @param {string} modifier
 * @param {...(Node | string)} children
 */
const group = (legend, modifier, ...children) =>
	el('fieldset', { class: `graph-form__group graph-form__group--${modifier}` }, el('legend', {}, legend), ...children);

/**
 * @param {object} options
 * @param {Readonly<GraphDraft>} options.draft what the form shows first
 * @param {() => ReadonlyArray<Comet>} options.database the main collection, as it is now
 * @param {import('../../../core/comets/properties.js').ValueAccessor} options.value
 * @param {() => number} options.now
 * @param {() => string} options.timeZone
 * @param {Readonly<{ start: number, end: number }>} options.defaults the dates of Start's and End's Default preset
 * @param {(draft: Readonly<GraphDraft>) => void} options.onCalculate with settings that validate
 * @param {() => void} options.onCancel Cancel, while a calculation runs
 * @param {() => void} options.onNew New graph, while a result is edited
 * @returns {GraphForm}
 */
export function createGraphForm({ draft, database, value, now, timeZone, defaults, onCalculate, onCancel, onNew }) {
	const prefix = `graph-form-${++ids}`;
	const start = createDatePicker({ value: draft.settings.start, now, timeZone, defaultDate: defaults.start, label: 'Start', onChange: changed });
	const end = createDatePicker({ value: draft.settings.end, now, timeZone, defaultDate: defaults.end, label: 'End', onChange: changed });
	const picker = createCometPicker({ list: draft.list, choice: draft.choice, database, value, now, timeZone, datePickers: [start, end], onChange: changed });

	const types = new Map(CHART_TYPES.map(({ key, label }) => [key, check(label, { type: 'radio', name: `${prefix}-type`, value: key })]));
	const valueColour = colour('Value colour');
	const lines = {
		perihelionLine: { box: check('Perihelion line'), colour: colour('Perihelion line colour') },
		nowLine: { box: check('Now line'), colour: colour('Now line colour') }
	};

	/** @type {Map<string, Limit>} */
	const limits = new Map(LIMITS.map(({ key, label }) => {
		const box = check(label);
		const field = createNumberField({
			range: RANGE_INPUT, label, className: 'graph-form__value',
			// typing checks the box while the field holds text, as on the desktop
			onInput: text => {
				box.input.checked = typedLimit(text).checked;
				changed();
			}
		});
		const message = el('p', { id: `${prefix}-${key}-error`, class: 'graph-form__error', role: 'alert', hidden: true });
		box.input.addEventListener('change', changed);
		return [key, { box, field, message }];
	}));
	const limit = (/** @type {string} */ key) => /** @type {Limit} */ (limits.get(key));

	const startError = el('p', { id: `${prefix}-start-error`, class: 'graph-form__error', role: 'alert', hidden: true });
	const startButton = /** @type {HTMLButtonElement} */ (start.element.querySelector('button'));
	/** @type {Map<string, { control: HTMLElement, message: HTMLElement }>} where each field's error shows */
	const slots = new Map();
	slots.set('start', { control: startButton, message: startError });
	for (const [key, { field, message }] of limits) slots.set(key, { control: field.element, message });

	const editingTitle = el('strong', {});
	const newButton = el('button', { type: 'button', class: 'button' }, 'New graph');
	const editing = el('p', { class: 'graph-form__editing', hidden: true }, 'Editing ', editingTitle, newButton);
	const calculate = el('button', { type: 'submit', class: 'button button--primary' }, 'Calculate');
	const cancel = el('button', { type: 'button', class: 'button', hidden: true }, 'Cancel');

	const element = el('form', { class: 'graph-form', 'aria-label': 'Graph settings', novalidate: true },
		editing,
		el('div', { class: 'graph-form__groups' },
			group('Comet', 'comet', picker.element),
			group('Timespan (Universal Time)', 'timespan',
				el('div', { class: 'graph-form__row' }, el('span', { class: 'graph-form__label', 'aria-hidden': 'true' }, 'Start:'), start.element, startError),
				el('div', { class: 'graph-form__row' }, el('span', { class: 'graph-form__label', 'aria-hidden': 'true' }, 'End:'), end.element)),
			group('Chart type', 'type', ...[...types.values()].map(t => t.element)),
			group('Chart options', 'options',
				el('div', { class: 'graph-form__line' }, el('span', { class: 'graph-form__check' }, 'Value'), valueColour),
				...Object.values(lines).map(line => el('div', { class: 'graph-form__line' }, line.box.element, line.colour))),
			group('Value range', 'range',
				...[...limits.values()].map(({ box, field, message }) => el('div', { class: 'graph-form__limit' }, box.element, field.element, message)))),
		el('div', { class: 'graph-form__actions' }, calculate, cancel));

	/** @type {ReadonlyArray<Comet>} the database the picker's list was read over */
	let listDatabase = draft.database;
	let running = false;
	/** whether errors show, so that each change shows what is still wrong */
	let showingErrors = false;

	/** @returns {GraphSettings} */
	function settings() {
		/** @param {string} key */
		const range = key => ({ checked: limit(key).box.input.checked, text: limit(key).field.text() });
		const line = (/** @type {keyof typeof lines} */ key) => ({ checked: lines[key].box.input.checked, color: lines[key].colour.value });
		return settingsOf({
			start: start.value(),
			end: end.value(),
			chartType: [...types].find(([, t]) => t.input.checked)?.[0] ?? CHART_TYPES[0].key,
			valueColor: valueColour.value,
			perihelionLine: line('perihelionLine'),
			nowLine: line('nowLine'),
			minimum: range('minimum'),
			maximum: range('maximum')
		});
	}

	/**
	 * each error by its field, and the focus on the first when asked
	 * @param {ReadonlyArray<ValidationError>} errors
	 * @param {boolean} focus
	 */
	function showErrors(errors, focus) {
		showingErrors = errors.length > 0;
		for (const [field, { control, message }] of slots) {
			const error = errors.find(e => e.field === field);
			message.textContent = error?.message ?? '';
			message.hidden = !error;
			if (error) {
				control.setAttribute('aria-invalid', 'true');
				control.setAttribute('aria-describedby', message.id);
			} else {
				control.removeAttribute('aria-invalid');
				control.removeAttribute('aria-describedby');
			}
		}
		if (focus && errors.length > 0) slots.get(errors[0].field)?.control.focus();
	}

	function enable() {
		calculate.disabled = running || !canCalculate(picker.choice());
	}

	// a change: Calculate follows the choice, and errors that show follow the fields
	function changed() {
		enable();
		if (showingErrors) {
			const result = validateGraphSettings(settings());
			showErrors(result.ok ? [] : result.errors, false);
		}
	}

	function follow() {
		const comets = database();
		if (comets === listDatabase) return;
		listDatabase = comets;
		picker.setList(pickerListOver(picker.list(), comets, value));
		enable();
	}

	/** @returns {Readonly<GraphDraft>} */
	const current = () => Object.freeze({ list: picker.list(), choice: picker.choice(), settings: settings(), database: listDatabase });

	/** @param {Readonly<GraphDraft>} next */
	function fill(next) {
		const fields = fieldsOf(next.settings);
		start.setValue(fields.start);
		end.setValue(fields.end);
		for (const [key, { input }] of types) input.checked = key === fields.chartType;
		valueColour.value = fields.valueColor;
		for (const key of /** @type {const} */ (['perihelionLine', 'nowLine'])) {
			lines[key].box.input.checked = fields[key].checked;
			lines[key].colour.value = fields[key].color;
		}
		for (const { key } of LIMITS) {
			const { checked, text } = fields[/** @type {'minimum' | 'maximum'} */ (key)];
			limit(key).box.input.checked = checked;
			limit(key).field.setText(text);
		}
		listDatabase = next.database;
		picker.setList(next.list, next.choice);
		follow();
		showErrors([], false);
		enable();
	}

	for (const { input } of types.values()) input.addEventListener('change', changed);
	for (const { box } of Object.values(lines)) box.input.addEventListener('change', changed);
	newButton.addEventListener('click', onNew);
	cancel.addEventListener('click', onCancel);
	element.addEventListener('submit', event => {
		event.preventDefault();
		if (running || !canCalculate(picker.choice())) return;
		const result = validateGraphSettings(settings());
		showErrors(result.ok ? [] : result.errors, true);
		if (result.ok) onCalculate(current());
	});

	fill(draft);
	return Object.freeze({
		element,
		draft: current,
		fill,
		follow,
		setRunning(/** @type {boolean} */ on) {
			running = on;
			cancel.hidden = !on;
			element.setAttribute('aria-busy', String(on));
			enable();
		},
		setEditing(/** @type {string | null} */ title) {
			editing.hidden = title === null;
			editingTitle.textContent = title ?? '';
		}
	});
}
