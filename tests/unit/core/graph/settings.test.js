import { test } from 'node:test';
import assert from 'node:assert/strict';
import { CHART_TYPES, chartTypeOf, defaultGraphSettings, graphCalculationSteps, graphTitle, RANGE_INPUT, validateGraphSettings } from '../../../../app/js/core/graph/settings.js';

const now = Date.parse('2026-10-18T12:00:00Z');
const base = defaultGraphSettings(now);
/** @param {object} changes */
const errorsOf = changes => {
	const result = validateGraphSettings({ ...base, ...changes });
	return result.ok ? [] : result.errors;
};

test('a new form: Magnitude, the default colours and lines, Maximum 12 (spec 10, Q20)', () => {
	assert.deepEqual(JSON.parse(JSON.stringify(base)), {
		start: Date.parse('2026-09-01T00:00:00Z'), end: Date.parse('2027-03-01T00:00:00Z'), chartType: 'Magnitude', valueColor: '#FF0000',
		perihelionLine: { checked: true, color: '#4169E1' }, nowLine: { checked: true, color: '#32CD32' },
		minimum: { checked: false, value: null }, maximum: { checked: true, value: 12 }
	});
	assert.ok(Object.isFrozen(base) && Object.isFrozen(base.maximum));
	assert.deepEqual({ ...RANGE_INPUT }, { min: -20, max: 40, decimals: 2 });
});

test('the chart types of spec 07', () => {
	assert.deepEqual(CHART_TYPES.map(t => [t.key, t.label, t.axisTitle, t.margin, t.multipleMaximum]), [
		['Magnitude', 'Magnitude', 'Magnitude', 0.2, 15],
		['SunDistance', 'Sun distance', 'Sun distance (AU)', 0, 2],
		['EarthDistance', 'Earth distance', 'Earth distance (AU)', 0, 2]
	]);
	const ep = /** @type {any} */ ({ Magnitude: 7.25, SunDist: 1.5, EarthDist: 0.75 });
	assert.deepEqual(CHART_TYPES.map(t => t.value(ep)), [7.25, 1.5, 0.75]);
	assert.equal(chartTypeOf('Magnitude').tooltip(7.255), 'Magnitude: 7.26');
	assert.equal(chartTypeOf('EarthDistance').tooltip(0.123456), 'Distance: 0.1235 AU');
	assert.throws(() => chartTypeOf('Brightness'), /unknown chart type/);
});

test('validation: the value range, then the timespan', () => {
	assert.deepEqual(errorsOf({}), []);
	assert.deepEqual(errorsOf({ minimum: { checked: true, value: null } }), [{ field: 'minimum', message: 'Enter Minimum value' }]);
	assert.deepEqual(errorsOf({ maximum: { checked: true, value: null } }), [{ field: 'maximum', message: 'Enter Maximum value' }]);
	assert.deepEqual(errorsOf({ minimum: { checked: true, value: 12 } }), [{ field: 'maximum', message: 'Minimum value must be lower than Maximum value' }]);
	assert.deepEqual(errorsOf({ minimum: { checked: true, value: 11.99 } }), []);
	// only with both checked
	assert.deepEqual(errorsOf({ minimum: { checked: true, value: 13 }, maximum: { checked: false, value: 12 } }), []);
	// an empty field is its own error, not also a comparison with 0
	assert.deepEqual(errorsOf({ minimum: { checked: true, value: 1 }, maximum: { checked: true, value: null } }), [{ field: 'maximum', message: 'Enter Maximum value' }]);
	assert.deepEqual(errorsOf({ end: base.start }), [{ field: 'start', message: 'End date must be greater than start date' }]);
});

test('the long-calculation count: the steps, times the comets', () => {
	assert.equal(graphCalculationSteps({ ...base, end: base.start + 86_400_000 }, 1), 100);
	assert.equal(graphCalculationSteps({ ...base, end: base.start + 1000 * 86_400_000 }, 3), 1500);
});

test('the title: chart type, then the comet or the size of the list', () => {
	const halley = { full: '1P/Halley' };
	assert.equal(graphTitle(base, { multiple: false, comets: [halley], selected: halley }), 'Magnitude graph - 1P/Halley');
	assert.equal(graphTitle({ ...base, chartType: 'SunDistance' }, { multiple: true, comets: [halley, halley], selected: null }), 'Sun distance graph - 2 comets');
	assert.equal(graphTitle({ ...base, chartType: 'EarthDistance' }, { multiple: false, comets: [], selected: halley }), 'Earth distance graph - 1P/Halley');
});
