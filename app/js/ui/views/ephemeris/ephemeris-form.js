// The Ephemeris view's settings form (`FormEphemerisSettings`): the comet picker, Start and End,
// the interval, the output data and the requirements, each error by its field, and Calculate. What
// the fields give is core/ephemeris/form.js. INTERNALS.md, Views.
import { COLUMNS } from '../../../core/ephemeris/columns.js';
import { canCalculate, fieldsOf, fieldText, INTERVAL_FIELDS, settingsOf, typedRequirement } from '../../../core/ephemeris/form.js';
import { DEFAULT_INTERVAL, INTERVAL_INPUT, REQUIREMENTS, validateEphemerisSettings } from '../../../core/ephemeris/settings.js';
import { pickerListOver } from '../../../core/input/comet-picker.js';
import { createCometPicker } from '../../components/comet-picker.js';
import { createDatePicker } from '../../components/date-picker.js';
import { createNumberField } from '../../components/number-field.js';
import { el } from '../../dom.js';

/** @typedef {import('../../../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../../../core/ephemeris/settings.js').EphemerisSettings} EphemerisSettings */
/** @typedef {import('../../../core/input/comet-picker.js').PickerChoice} PickerChoice */
/** @typedef {import('../../../core/input/comet-picker.js').PickerList} PickerList */
/** @typedef {import('../../../core/validation.js').ValidationError} ValidationError */
/** @typedef {import('../../components/number-field.js').NumberField} NumberField */

/**
 * what the form holds, to calculate from or to show again: the picker's list and choice, the
 * settings the fields give, and the database the list was read over
 * @typedef {object} EphemerisDraft
 * @property {Readonly<PickerList>} list
 * @property {Readonly<PickerChoice>} choice
 * @property {EphemerisSettings} settings validated when Calculate hands them over
 * @property {ReadonlyArray<Comet>} database
 */

/**
 * @typedef {object} EphemerisForm
 * @property {HTMLFormElement} element
 * @property {() => Readonly<EphemerisDraft>} draft what the form holds now
 * @property {(draft: Readonly<EphemerisDraft>) => void} fill shows a draft, its list read again
 *   if the database has changed since; no error shows
 * @property {() => void} follow reads the picker's list again if the database has changed since
 * @property {(running: boolean) => void} setRunning Calculate waits, and Cancel shows, while a
 *   calculation runs
 * @property {(title: string | null) => void} setEditing the result Calculate replaces; null for a new one
 */

let ids = 0;

/**
 * a checkbox or radio button with its label after it
 * @param {string} label
 * @param {Record<string, string>} [attributes]
 * @returns {{ element: HTMLElement, input: HTMLInputElement }}
 */
function check(label, attributes = { type: 'checkbox' }) {
	const input = el('input', { class: 'ephemeris-form__box', ...attributes });
	return { input, element: el('label', { class: 'ephemeris-form__check' }, input, label) };
}

/**
 * @param {string} legend
 * @param {string} modifier
 * @param {...(Node | string)} children
 */
const group = (legend, modifier, ...children) =>
	el('fieldset', { class: `ephemeris-form__group ephemeris-form__group--${modifier}` }, el('legend', {}, legend), ...children);

/**
 * @param {object} options
 * @param {Readonly<EphemerisDraft>} options.draft what the form shows first
 * @param {() => ReadonlyArray<Comet>} options.database the main collection, as it is now
 * @param {import('../../../core/comets/properties.js').ValueAccessor} options.value
 * @param {() => number} options.now
 * @param {() => string} options.timeZone
 * @param {Readonly<{ start: number, end: number }>} options.defaults the dates of Start's and End's Default preset
 * @param {(draft: Readonly<EphemerisDraft>) => void} options.onCalculate with settings that validate
 * @param {() => void} options.onCancel Cancel, while a calculation runs
 * @param {() => void} options.onNew New ephemeris, while a result is edited
 * @returns {EphemerisForm}
 */
export function createEphemerisForm({ draft, database, value, now, timeZone, defaults, onCalculate, onCancel, onNew }) {
	const prefix = `ephemeris-form-${++ids}`;
	const start = createDatePicker({ value: draft.settings.start, now, timeZone, defaultDate: defaults.start, label: 'Start', onChange: changed });
	const end = createDatePicker({ value: draft.settings.end, now, timeZone, defaultDate: defaults.end, label: 'End', onChange: changed });
	const picker = createCometPicker({ list: draft.list, choice: draft.choice, database, value, now, timeZone, datePickers: [start, end], onChange: changed });

	/** @type {Map<string, NumberField>} */
	const interval = new Map(INTERVAL_FIELDS.map(({ key, name }) =>
		[key, createNumberField({ range: INTERVAL_INPUT[key], label: name, className: 'ephemeris-form__part', onInput: changed })]));
	const defaultInterval = el('button', { type: 'button', class: 'button' }, 'Default');

	const radio = { name: `${prefix}-time`, type: 'radio' };
	const localTime = check('Local Time', { ...radio, value: 'local' });
	const universalTime = check('Universal Time', { ...radio, value: 'universal' });
	const columns = new Map(COLUMNS.map(({ key, label }) => [key, check(label)]));

	const requirements = new Map(REQUIREMENTS.map(({ key, label, input }) => {
		const box = check(label);
		const field = createNumberField({
			range: input, label, className: 'ephemeris-form__value',
			// typing checks the box while the field holds text, as on the desktop
			onInput: text => {
				box.input.checked = typedRequirement(text).checked;
				changed();
			}
		});
		const message = el('p', { id: `${prefix}-${key}-error`, class: 'ephemeris-form__error', role: 'alert', hidden: true });
		box.input.addEventListener('change', changed);
		return [key, { box, field, message }];
	}));

	const startError = el('p', { id: `${prefix}-start-error`, class: 'ephemeris-form__error', role: 'alert', hidden: true });
	const startButton = /** @type {HTMLButtonElement} */ (start.element.querySelector('button'));
	/** @type {Map<string, { control: HTMLElement, message: HTMLElement }>} where each field's error shows */
	const slots = new Map();
	slots.set('start', { control: startButton, message: startError });
	for (const [key, { field, message }] of requirements) slots.set(key, { control: field.element, message });

	const editingTitle = el('strong', {});
	const newButton = el('button', { type: 'button', class: 'button' }, 'New ephemeris');
	const editing = el('p', { class: 'ephemeris-form__editing', hidden: true }, 'Editing ', editingTitle, newButton);
	const calculate = el('button', { type: 'submit', class: 'button button--primary' }, 'Calculate');
	const cancel = el('button', { type: 'button', class: 'button', hidden: true }, 'Cancel');

	const element = el('form', { class: 'ephemeris-form', 'aria-label': 'Ephemeris settings', novalidate: true },
		editing,
		el('div', { class: 'ephemeris-form__groups' },
			group('Comet', 'comet', picker.element),
			group('Timespan (Universal Time)', 'timespan',
				el('div', { class: 'ephemeris-form__row' }, el('span', { class: 'ephemeris-form__label', 'aria-hidden': 'true' }, 'Start:'), start.element, startError),
				el('div', { class: 'ephemeris-form__row' }, el('span', { class: 'ephemeris-form__label', 'aria-hidden': 'true' }, 'End:'), end.element)),
			group('Interval', 'interval',
				el('div', { class: 'ephemeris-form__row' },
					...INTERVAL_FIELDS.map(({ key, letter }) => el('label', { class: 'ephemeris-form__part-label' },
						el('span', { 'aria-hidden': 'true' }, letter), /** @type {NumberField} */ (interval.get(key)).element))),
				el('div', { class: 'ephemeris-form__row' }, defaultInterval)),
			group('Output data', 'output',
				el('div', { class: 'ephemeris-form__times', role: 'radiogroup', 'aria-label': 'Time' }, localTime.element, universalTime.element),
				el('div', { class: 'ephemeris-form__columns' }, ...[...columns.values()].map(c => c.element))),
			group('Requirements', 'requirements',
				...[...requirements.values()].map(({ box, field, message }) =>
					el('div', { class: 'ephemeris-form__requirement' }, box.element, field.element, message)))),
		el('div', { class: 'ephemeris-form__actions' }, calculate, cancel));

	/** @type {ReadonlyArray<Comet>} the database the picker's list was read over */
	let listDatabase = draft.database;
	let running = false;
	/** whether errors show, so that each change shows what is still wrong */
	let showingErrors = false;

	/** @returns {EphemerisSettings} */
	function settings() {
		/** @param {string} key */
		const text = key => /** @type {NumberField} */ (interval.get(key)).text();
		return settingsOf({
			start: start.value(),
			end: end.value(),
			interval: { days: text('days'), hours: text('hours'), minutes: text('minutes') },
			localTime: localTime.input.checked,
			columns: Object.fromEntries([...columns].map(([key, { input }]) => [key, input.checked])),
			requirements: Object.fromEntries([...requirements].map(([key, { box, field }]) => [key, { checked: box.input.checked, text: field.text() }]))
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
			const result = validateEphemerisSettings(settings());
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

	/** @returns {Readonly<EphemerisDraft>} */
	const current = () => Object.freeze({ list: picker.list(), choice: picker.choice(), settings: settings(), database: listDatabase });

	/** @param {Readonly<EphemerisDraft>} next */
	function fill(next) {
		const fields = fieldsOf(next.settings);
		start.setValue(fields.start);
		end.setValue(fields.end);
		for (const { key } of INTERVAL_FIELDS) interval.get(key)?.setText(fields.interval[key]);
		localTime.input.checked = fields.localTime;
		universalTime.input.checked = !fields.localTime;
		for (const [key, { input }] of columns) input.checked = fields.columns[key];
		for (const [key, { box, field }] of requirements) {
			box.input.checked = fields.requirements[key].checked;
			field.setText(fields.requirements[key].text);
		}
		listDatabase = next.database;
		picker.setList(next.list, next.choice);
		follow();
		showErrors([], false);
		enable();
	}

	defaultInterval.addEventListener('click', () => {
		for (const { key } of INTERVAL_FIELDS) interval.get(key)?.setText(fieldText(DEFAULT_INTERVAL[key]));
		changed();
	});
	for (const { input } of [localTime, universalTime, ...columns.values()]) input.addEventListener('change', changed);
	newButton.addEventListener('click', onNew);
	cancel.addEventListener('click', onCancel);
	element.addEventListener('submit', event => {
		event.preventDefault();
		if (running || !canCalculate(picker.choice())) return;
		const result = validateEphemerisSettings(settings());
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
