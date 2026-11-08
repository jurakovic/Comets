import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	buttonText, collectFields, DATE_FIELDS, datePresets, fieldTexts, jdFieldDate, JD_RANGE, localText, okDate, stepDate
} from '../../../../app/js/core/input/date.js';
import { partsToInstant } from '../../../../app/js/core/time/calendar.js';
import { localPartsIn } from '../../../../app/js/core/time/local.js';
import { MAXIMUM_DATE, MINIMUM_DATE } from '../../../../app/js/core/time/range.js';

/** @param {string} iso */
const at = iso => Date.parse(iso);
const iso = (/** @type {number} */ instant) => new Date(instant).toISOString().replace('.000Z', 'Z');

test('the button shows the UTC instant, the tooltip local time', () => {
	const instant = at('2026-11-05T06:19:11Z');
	assert.equal(buttonText(instant), '05.11.2026. 06:19:11');
	assert.equal(localText(instant, localPartsIn('Europe/Zagreb')), '05.11.2026. 07:19:11');
	assert.equal(localText(at('2026-07-05T06:19:11Z'), localPartsIn('Europe/Zagreb')), '05.07.2026. 08:19:11');
});

test('the fields: integers without padding, the JD as 0.0####', () => {
	assert.deepEqual({ ...fieldTexts(at('2000-01-01T12:00:00Z')) },
		{ day: '1', month: '1', year: '2000', hour: '12', minute: '0', second: '0', jd: '2451545.0' });
	assert.equal(fieldTexts(at('2026-11-05T06:19:11Z')).jd, '2461349.76332');
	assert.deepEqual(DATE_FIELDS.map(f => f.label), ['D', 'M', 'Y', 'H', 'M', 'S']);
	assert.deepEqual(DATE_FIELDS.map(f => f.name), ['Day', 'Month', 'Year', 'Hour', 'Minute', 'Second']);
});

test('the day\'s maximum follows the month and year', () => {
	const day = DATE_FIELDS[0];
	/** @param {number} year @param {number} month */
	const max = (year, month) => day.range({ year, month, day: 1, hour: 0, minute: 0, second: 0 }).max;
	assert.equal(max(2024, 2), 29);
	assert.equal(max(2026, 2), 28);
	assert.equal(max(1900, 2), 28);
	assert.equal(max(2000, 2), 29);
	assert.equal(max(2026, 4), 30);
	assert.equal(max(2026, 12), 31);
	assert.equal(max(2026, 0), 31);
	assert.equal(max(0, 2), 31);
});

test('the fields make an instant only when each is a number in its range', () => {
	const texts = { day: '29', month: '2', year: '2024', hour: '23', minute: '59', second: '59' };
	assert.equal(iso(/** @type {number} */ (collectFields(texts))), '2024-02-29T23:59:59Z');
	assert.equal(collectFields({ ...texts, year: '2023' }), null);
	assert.equal(collectFields({ ...texts, day: '' }), null);
	assert.equal(collectFields({ ...texts, hour: '24' }), null);
	assert.equal(collectFields({ ...texts, second: '' }), null);
	assert.equal(collectFields({ ...texts, month: '13' }), null);
	assert.equal(collectFields({ ...texts, year: '0' }), null);
	assert.equal(iso(/** @type {number} */ (collectFields({ ...texts, year: '1', month: '1', day: '1' }))), '0001-01-01T23:59:59Z');
});

test('the JD field sets the date through JDToDateTime, within the date range only', () => {
	assert.equal(iso(/** @type {number} */ (jdFieldDate('2451545'))), '2000-01-01T12:00:00Z');
	assert.equal(iso(/** @type {number} */ (jdFieldDate('2451545.12345'))), '2000-01-01T14:57:46Z');
	assert.equal(jdFieldDate(''), null);
	assert.equal(jdFieldDate('245'), null);
	assert.equal(jdFieldDate(String(JD_RANGE.max + 0.00001)), null);
	assert.equal(iso(/** @type {number} */ (jdFieldDate(String(JD_RANGE.min)))), '1700-01-01T00:00:00Z');
});

test('↑ and ↓ add one unit of the field with calendar arithmetic, a day in the JD', () => {
	const jan31 = at('2024-01-31T10:20:30Z');
	assert.equal(iso(/** @type {number} */ (stepDate(jan31, 'month', 1))), '2024-02-29T10:20:30Z');
	assert.equal(iso(/** @type {number} */ (stepDate(at('2025-01-31T10:20:30Z'), 'month', 1))), '2025-02-28T10:20:30Z');
	assert.equal(iso(/** @type {number} */ (stepDate(at('2024-02-29T00:00:00Z'), 'year', 1))), '2025-02-28T00:00:00Z');
	assert.equal(iso(/** @type {number} */ (stepDate(jan31, 'day', 1))), '2024-02-01T10:20:30Z');
	assert.equal(iso(/** @type {number} */ (stepDate(jan31, 'hour', -1))), '2024-01-31T09:20:30Z');
	assert.equal(iso(/** @type {number} */ (stepDate(at('2024-12-31T23:59:59Z'), 'second', 1))), '2025-01-01T00:00:00Z');
	assert.equal(iso(/** @type {number} */ (stepDate(at('2024-12-31T23:59:30Z'), 'minute', 1))), '2025-01-01T00:00:30Z');
	assert.equal(iso(/** @type {number} */ (stepDate(jan31, 'jd', -1))), '2024-01-30T10:20:30Z');
	assert.equal(stepDate(partsToInstant({ year: 9999, month: 12, day: 31, hour: 0, minute: 0, second: 0 }), 'day', 1), null);
	assert.equal(stepDate(partsToInstant({ year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0 }), 'year', -1), null);
	assert.throws(() => stepDate(jan31, /** @type {any} */ ('week'), 1), /no date field/);
});

test('OK clamps to 1700-01-01 … 2300-01-01 UTC', () => {
	assert.equal(okDate(at('1600-05-05T00:00:00Z')), MINIMUM_DATE);
	assert.equal(okDate(at('2400-05-05T00:00:00Z')), MAXIMUM_DATE);
	assert.equal(okDate(at('2026-05-05T00:00:00Z')), at('2026-05-05T00:00:00Z'));
	assert.equal(okDate(MAXIMUM_DATE), MAXIMUM_DATE);
});

test('the presets, in order; Default and Perihelion Date only when set; none clamped', () => {
	const now = at('2026-11-05T06:19:11Z');
	const value = at('2299-11-30T01:02:03Z');
	const shown = (/** @type {ReadonlyArray<import('../../../../app/js/core/input/date.js').DatePreset>} */ presets) =>
		presets.map(p => ('separator' in p ? '—' : `${p.label} = ${iso(p.value)}`));
	assert.deepEqual(shown(datePresets({ now, value, defaultDate: null, perihelionDate: null })), [
		'Today = 2026-11-05T00:00:00Z',
		'—',
		'01 Jan 2025 00:00:00 = 2025-01-01T00:00:00Z',
		'01 Jan 2026 00:00:00 = 2026-01-01T00:00:00Z',
		'01 Jan 2027 00:00:00 = 2027-01-01T00:00:00Z',
		'01 Jan 2028 00:00:00 = 2028-01-01T00:00:00Z',
		'—',
		'Add 3 months = 2300-02-28T01:02:03Z',
		'Add 6 months = 2300-05-30T01:02:03Z',
		'Add 1 year = 2300-11-30T01:02:03Z',
		'—',
		'Subtract 3 months = 2299-08-30T01:02:03Z',
		'Subtract 6 months = 2299-05-30T01:02:03Z',
		'Subtract 1 year = 2298-11-30T01:02:03Z'
	]);
	const both = shown(datePresets({ now, value, defaultDate: at('2026-11-01T00:00:00Z'), perihelionDate: at('2024-09-27T18:00:00Z') }));
	assert.deepEqual(both.slice(0, 6), [
		'Default = 2026-11-01T00:00:00Z', '—', 'Today = 2026-11-05T00:00:00Z', '—', 'Perihelion Date = 2024-09-27T18:00:00Z', '—'
	]);
	assert.equal(both.length, 18);
});
