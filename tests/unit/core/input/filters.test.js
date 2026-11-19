import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFilter, defaultFilters, MAX_FILTERS } from '../../../../app/js/core/comets/filter.js';
import { addRow, canAddRow, editRow, newRow, panelRows, removeRow, rowFilters } from '../../../../app/js/core/input/filters.js';
import { instantToJd } from '../../../../app/js/core/time/julian.js';

const NOW = Date.parse('2026-11-05T06:19:11Z');
const START = Date.parse('2026-10-01T00:00:00Z');

test('a new row: the property\'s default compare, unchecked, empty, the default start date', () => {
	assert.deepEqual({ ...newRow('full', NOW) }, { property: 'full', checked: false, compare: 'contains', text: '', number: '', date: START });
	assert.deepEqual({ ...newRow('q', NOW) }, { property: 'q', checked: false, compare: 'lessThan', text: '', number: '', date: START });
	assert.equal(newRow('Tn', NOW).compare, 'greaterThan');
	assert.ok(Object.isFrozen(newRow('e', NOW)));
});

test('rows from filters put each text in the editor of its type, and give the same filters back', () => {
	const filters = [
		createFilter('full', { checked: true, text: 'halley, encke', compare: 'startsWith' }),
		createFilter('q', { checked: false, text: '1.5', compare: 'equals' }),
		createFilter('Tn', { checked: true, text: '2461406.5', compare: 'lessThan' })
	];
	const rows = panelRows(filters, NOW);
	assert.deepEqual(rows.map(r => ({ ...r })), [
		{ property: 'full', checked: true, compare: 'startsWith', text: 'halley, encke', number: '', date: START },
		{ property: 'q', checked: false, compare: 'equals', text: '', number: '1.5', date: START },
		{ property: 'Tn', checked: true, compare: 'lessThan', text: '', number: '', date: Date.parse('2027-01-01T00:00:00Z') }
	]);
	assert.deepEqual(rowFilters(rows), filters);
	assert.deepEqual(rowFilters(panelRows(defaultFilters(NOW), NOW)), defaultFilters(NOW));
});

test('the filters take the shown editor\'s value, trimmed; a date as its JD', () => {
	const row = { ...newRow('full', NOW), text: '  halley ', number: '3', date: Date.parse('2026-10-18T12:00:00Z') };
	assert.equal(rowFilters([row])[0].text, 'halley');
	assert.equal(rowFilters([{ ...row, property: 'P' }])[0].text, '3');
	assert.equal(rowFilters([{ ...row, property: 'Tn' }])[0].text, '2461332');
	assert.equal(rowFilters([{ ...row, property: 'Tn' }])[0].text, String(instantToJd(row.date)));
});

test('a value makes its row checked, and emptying it unchecks it', () => {
	let rows = panelRows(defaultFilters(NOW), NOW);
	rows = editRow(rows, 0, { text: 'atlas' });
	assert.equal(rows[0].checked, true);
	rows = editRow(rows, 0, { text: '   ' });
	assert.equal(rows[0].checked, false);
	assert.equal(rows[0].text, '   ');
	rows = editRow(rows, 2, { number: '0.5' });
	assert.equal(rows[2].checked, true);
	rows = editRow(rows, 2, { number: '' });
	assert.equal(rows[2].checked, false);
	assert.ok(Object.isFrozen(rows) && Object.isFrozen(rows[2]));
});

test('a date makes its row checked, even the same date', () => {
	const rows = panelRows(defaultFilters(NOW), NOW);
	const edited = editRow(rows, 1, { date: rows[1].date });
	assert.equal(edited[1].checked, true);
	assert.equal(edited[1].date, START);
});

test('a new property takes its default compare and keeps the checkbox and every editor\'s value', () => {
	let rows = [{ ...newRow('full', NOW), checked: true, text: 'atlas', compare: 'endsWith' }];
	rows = editRow(rows, 0, { number: '2' });
	rows = editRow(rows, 0, { property: 'q' });
	assert.deepEqual({ ...rows[0] }, { property: 'q', checked: true, compare: 'lessThan', text: 'atlas', number: '2', date: START });
	rows = editRow(rows, 0, { compare: 'equals' });
	rows = editRow(rows, 0, { property: 'e' });
	assert.equal(rows[0].compare, 'lessThan');
	rows = editRow(rows, 0, { property: 'name' });
	assert.equal(rows[0].compare, 'contains');
	assert.equal(rowFilters(rows)[0].text, 'atlas');
});

test('the checkbox and the compare change alone', () => {
	const rows = editRow(editRow([newRow('q', NOW)], 0, { checked: true }), 0, { compare: 'greaterThan' });
	assert.deepEqual({ ...rows[0] }, { ...newRow('q', NOW), checked: true, compare: 'greaterThan' });
	assert.throws(() => editRow(rows, 0, /** @type {any} */ ({ colour: 'red' })), /unknown filter row edit/);
});

test('an edit changes only its row', () => {
	const rows = panelRows(defaultFilters(NOW), NOW);
	const edited = editRow(rows, 3, { number: '12' });
	assert.equal(edited[3].number, '12');
	for (const i of [0, 1, 2]) assert.equal(edited[i], rows[i]);
});

test('+ adds Full name rows up to ten; remove takes out the row at its index', () => {
	let rows = panelRows(defaultFilters(NOW), NOW);
	while (canAddRow(rows)) rows = addRow(rows, NOW);
	assert.equal(rows.length, MAX_FILTERS);
	assert.deepEqual({ ...rows[9] }, { ...newRow('full', NOW) });
	assert.equal(addRow(rows, NOW), rows);
	rows = removeRow(rows, 1);
	assert.equal(rows.length, 9);
	assert.deepEqual(rows.slice(0, 3).map(r => r.property), ['full', 'q', 'CurrentMag']);
	assert.ok(canAddRow(rows));
	assert.deepEqual(removeRow([newRow('q', NOW)], 0), []);
});
