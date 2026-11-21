// The filters panel (`FilterControl`): up to ten rows, each with a checkbox, a property, a compare,
// the property's editor and unit, and a remove button; + adds a row. Every edit applies the rows,
// as do Apply and Enter. The rules are core/input/filters.js. INTERNALS.md, How the UI is written.
import { COMPARES_BY_TYPE } from '../../core/comets/compares.js';
import { PROPERTIES, property } from '../../core/comets/properties.js';
import { addRow, canAddRow, editRow, panelRows, removeRow, rowFilters } from '../../core/input/filters.js';
import { el } from '../dom.js';
import { createDatePicker } from './date-picker.js';
import { createNumberField } from './number-field.js';

/** @typedef {import('../../core/comets/filter.js').Filter} Filter */
/** @typedef {import('../../core/input/filters.js').PanelRow} PanelRow */
/** @typedef {import('../../core/input/filters.js').RowEdit} RowEdit */
/** @typedef {import('../../core/validation.js').Validation<ReadonlyArray<Filter>>} FiltersValidation */

/**
 * @typedef {object} FiltersPanel
 * @property {HTMLFormElement} element
 * @property {(filters: ReadonlyArray<Filter>) => void} setFilters shows filters as the owner sets
 *   them: nothing is applied, and no error is shown
 */

/**
 * one row's controls
 * @typedef {object} RowView
 * @property {HTMLElement} element
 * @property {(row: Readonly<PanelRow>) => void} sync the checkbox, the compares, the editor shown and the unit
 * @property {(index: number) => void} place names the row by its place
 * @property {(message: string | null) => void} error shows a message by the row, or none
 * @property {() => void} focusEditor
 * @property {() => void} focusRemove
 * @property {() => void} focusProperty
 */

/** a number field's range while its row is not a number */
const NO_RANGE = Object.freeze({ min: 0, max: 0, decimals: 0 });

let ids = 0;

/**
 * @param {object} options
 * @param {ReadonlyArray<Filter>} options.filters the rows to start with
 * @param {() => number} options.now for new rows' dates, and the date pickers' presets
 * @param {() => string} options.timeZone the date pickers' local time
 * @param {(filters: ReadonlyArray<Filter>) => FiltersValidation} options.apply after every edit,
 *   Apply and Enter; an error is shown by its row
 * @param {() => void} options.onClose Close
 * @param {string} [options.label]
 * @returns {FiltersPanel}
 */
export function createFiltersPanel({ filters, now, timeZone, apply, onClose, label = 'Filters' }) {
	const prefix = `filters-panel-${++ids}`;
	const list = el('div', { class: 'filters-panel__rows' });
	const add = el('button', { type: 'button', class: 'button filters-panel__add', 'aria-label': 'Add filter', title: 'Add filter' }, '+');
	const close = el('button', { type: 'button', class: 'button' }, 'Close');
	const element = el('form', { class: 'filters-panel', 'aria-label': label, novalidate: true },
		list, add,
		el('div', { class: 'filters-panel__actions' }, el('button', { type: 'submit', class: 'button button--primary' }, 'Apply'), close));

	/** @type {ReadonlyArray<Readonly<PanelRow>>} */
	let rows = [];
	/** @type {RowView[]} */
	let views = [];

	/**
	 * applies the rows and shows the error by its row; an explicit apply also focuses its field
	 * @param {boolean} explicit
	 */
	function run(explicit) {
		const result = apply(rowFilters(rows));
		const errors = result.ok ? [] : result.errors;
		views.forEach((view, i) => view.error(errors.find(e => e.index === i)?.message ?? null));
		if (explicit && errors.length > 0) views[errors[0].index ?? 0]?.focusEditor();
	}

	/**
	 * @param {HTMLElement} element the row's
	 * @param {Readonly<RowEdit>} edit
	 */
	function edited(element, edit) {
		const index = views.findIndex(v => v.element === element);
		// a row the owner's setFilters replaced while its date editor was open
		if (index < 0) return;
		rows = editRow(rows, index, edit);
		views[index].sync(rows[index]);
		run(false);
	}

	/** @param {HTMLElement} element the row's */
	function removed(element) {
		const index = views.findIndex(v => v.element === element);
		if (index < 0) return;
		rows = removeRow(rows, index);
		views.splice(index, 1);
		element.remove();
		layout();
		// the focus goes to the row that took its place, or the last one, or +
		const next = views[Math.min(index, views.length - 1)];
		if (next) next.focusRemove();
		else add.focus();
		run(false);
	}

	function layout() {
		views.forEach((view, i) => view.place(i));
		add.hidden = !canAddRow(rows);
	}

	/**
	 * @param {Readonly<PanelRow>} row
	 * @returns {RowView}
	 */
	function rowView(row) {
		const id = `${prefix}-${++ids}`;
		const checkbox = el('input', { type: 'checkbox', class: 'filters-panel__checked', 'aria-label': 'Active' });
		const propertySelect = el('select', { class: 'filters-panel__property', 'aria-label': 'Property' },
			...PROPERTIES.map(p => el('option', { value: p.key }, p.label)));
		const compareSelect = el('select', { class: 'filters-panel__compare', 'aria-label': 'Compare' });
		const text = el('input', { type: 'text', class: 'text-field filters-panel__text', 'aria-label': 'Value', autocomplete: 'off' });
		text.value = row.text;
		const number = createNumberField({
			range: property(row.property).input ?? NO_RANGE, text: row.number, label: 'Value', className: 'filters-panel__number',
			onInput: value => edited(rowElement, { number: value })
		});
		const date = createDatePicker({ value: row.date, now, timeZone, label: 'Value', onChange: instant => edited(rowElement, { date: instant }) });
		const unit = el('span', { class: 'filters-panel__unit' });
		const remove = el('button', { type: 'button', class: 'button filters-panel__remove', 'aria-label': 'Remove filter', title: 'Remove filter' }, '−');
		const message = el('p', { id: `${id}-error`, class: 'filters-panel__error', role: 'alert', hidden: true });
		const editors = { string: text, number: number.element, date: date.element };
		const rowElement = el('div', { class: 'filters-panel__row', role: 'group' },
			checkbox, propertySelect, compareSelect,
			el('div', { class: 'filters-panel__value' }, text, number.element, date.element),
			unit, remove, message);

		checkbox.addEventListener('change', () => edited(rowElement, { checked: checkbox.checked }));
		propertySelect.addEventListener('change', () => edited(rowElement, { property: propertySelect.value }));
		compareSelect.addEventListener('change', () => edited(rowElement, { compare: compareSelect.value }));
		text.addEventListener('input', () => edited(rowElement, { text: text.value }));
		remove.addEventListener('click', () => removed(rowElement));

		/** @type {string | null} the type whose compares the menu holds */
		let shownType = null;
		/** @returns {HTMLElement} the editor of the row's type */
		const editor = () => editors[property(propertySelect.value).type];

		/** @type {RowView} */
		const view = {
			element: rowElement,
			sync(/** @type {Readonly<PanelRow>} */ current) {
				const { type, unit: unitText, input } = property(current.property);
				checkbox.checked = current.checked;
				propertySelect.value = current.property;
				if (type !== shownType) {
					compareSelect.replaceChildren(...COMPARES_BY_TYPE[type].map(c => el('option', { value: c.key }, c.label)));
					shownType = type;
				}
				compareSelect.value = current.compare;
				for (const [kind, node] of Object.entries(editors)) node.hidden = kind !== type;
				if (input) number.setRange(input);
				unit.textContent = unitText;
			},
			place(/** @type {number} */ index) {
				rowElement.setAttribute('aria-label', `Filter ${index + 1}`);
			},
			error(/** @type {string | null} */ shown) {
				message.textContent = shown ?? '';
				message.hidden = shown === null;
				for (const node of [text, number.element]) {
					node.removeAttribute('aria-invalid');
					node.removeAttribute('aria-describedby');
				}
				if (shown === null) return;
				const field = editor();
				field.setAttribute('aria-invalid', 'true');
				field.setAttribute('aria-describedby', message.id);
			},
			focusEditor() {
				const field = editor();
				(field.matches('button, input') ? field : field.querySelector('button'))?.focus();
			},
			focusRemove: () => remove.focus(),
			focusProperty: () => propertySelect.focus()
		};
		view.sync(row);
		return view;
	}

	/** @param {ReadonlyArray<Readonly<PanelRow>>} next */
	function build(next) {
		rows = next;
		views = rows.map(rowView);
		list.replaceChildren(...views.map(v => v.element));
		layout();
	}

	add.addEventListener('click', () => {
		rows = addRow(rows, now());
		const view = rowView(rows[rows.length - 1]);
		views.push(view);
		list.append(view.element);
		layout();
		view.focusProperty();
		run(false);
	});
	close.addEventListener('click', onClose);
	element.addEventListener('submit', event => {
		event.preventDefault();
		run(true);
	});

	build(panelRows(filters, now()));
	return Object.freeze({
		element,
		setFilters(/** @type {ReadonlyArray<Filter>} */ next) {
			build(panelRows(next, now()));
		}
	});
}
