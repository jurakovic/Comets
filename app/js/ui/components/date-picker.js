// The date picker (`SelectDateControl`): a button with the UTC instant and its local time in the
// tooltip, which opens the editor, and the presets menu beside it.
// docs/spec/09-settings-and-persistence.md, Date picker.
import { buttonText, datePresets, LOCAL_TIME, localText } from '../../core/input/date.js';
import { localPartsIn } from '../../core/time/local.js';
import { el } from '../dom.js';
import { editDate } from './date-editor.js';
import { createMenu } from './menu.js';

/**
 * @typedef {object} DatePicker
 * @property {HTMLElement} element
 * @property {() => number} value the date shown, UTC milliseconds
 * @property {(instant: number) => void} setValue as the owner sets it: no onChange
 * @property {(instant: number | null) => void} setPerihelionDate the Perihelion Date preset; null hides it
 * @property {() => Promise<void>} edit opens the editor, as the button does
 */

/** local date parts by time zone, since a formatter is costly to build */
const zones = new Map();

/**
 * @param {string} timeZone
 * @returns {(instant: number) => import('../../core/time/calendar.js').DateParts}
 */
function localIn(timeZone) {
	let local = zones.get(timeZone);
	if (!local) zones.set(timeZone, local = localPartsIn(timeZone));
	return local;
}

/**
 * @param {object} options
 * @param {number} options.value UTC milliseconds
 * @param {() => number} options.now for Today and the year presets
 * @param {() => string} options.timeZone local time's zone, read each time it is shown
 * @param {(instant: number) => void} options.onChange after the editor's OK or a preset
 * @param {number | null} [options.defaultDate] the Default preset; none without one
 * @param {number | null} [options.perihelionDate] the Perihelion Date preset; none without one
 * @param {string} [options.label] names the picker, as the field it is for
 * @returns {DatePicker}
 */
export function createDatePicker({ value, now, timeZone, onChange, defaultDate = null, perihelionDate = null, label }) {
	let current = value;
	let perihelion = perihelionDate;
	const button = el('button', { type: 'button', class: 'date-picker__button', 'aria-haspopup': 'dialog' });
	const context = () => ({ now: now(), defaultDate, perihelionDate: perihelion });

	/** @param {number} instant */
	function show(instant) {
		current = instant;
		button.textContent = buttonText(instant);
		button.title = `${LOCAL_TIME}\n${localText(instant, localIn(timeZone()))}`;
	}

	/** @param {number} instant */
	function pick(instant) {
		show(instant);
		onChange(instant);
	}

	const menu = createMenu({
		label: el('span', { class: 'date-picker__presets-icon' }),
		name: 'Date presets',
		className: 'date-picker__presets',
		entries: () => datePresets({ ...context(), value: current }).map(preset => 'separator' in preset
			? preset
			: { label: preset.label, run: () => pick(preset.value) })
	});

	async function edit() {
		const result = await editDate({ value: current, local: localIn(timeZone()), presets: context });
		if (result !== null) pick(result);
	}

	button.addEventListener('click', () => { edit(); });
	// the tooltip follows the time zone the clock is in now
	button.addEventListener('pointerenter', () => show(current));
	show(value);

	const element = el('div', { class: 'date-picker', role: 'group', 'aria-label': label }, button, menu.element);
	return Object.freeze({
		element,
		value: () => current,
		setValue: show,
		setPerihelionDate(/** @type {number | null} */ instant) { perihelion = instant; },
		edit
	});
}
