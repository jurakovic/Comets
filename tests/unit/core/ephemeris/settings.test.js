import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	calculationSteps, DEFAULT_INTERVAL, defaultEphemerisSettings, ephemerisTitle, INTERVAL_INPUT, intervalMs, LONG_CALCULATION_QUESTION,
	LONG_CALCULATION_STEPS, REQUIREMENTS, validateEphemerisSettings
} from '../../../../app/js/core/ephemeris/settings.js';
import { defaultTimespan, timespanErrors, wholeSteps } from '../../../../app/js/core/ephemeris/timespan.js';
import { COLUMNS } from '../../../../app/js/core/ephemeris/columns.js';

const utc = (/** @type {string} */ text) => Date.parse(text);
const DAY = 86_400_000;

test('the default dates: the 1st of last month and of five months ahead, at 00:00 UTC', () => {
	assert.deepEqual({ ...defaultTimespan(utc('2026-10-18T12:00:00Z')) }, { start: utc('2026-09-01T00:00:00Z'), end: utc('2027-03-01T00:00:00Z') });
	// AddMonths clamps the day: 31 March − 1 month is 28 February
	assert.deepEqual({ ...defaultTimespan(utc('2026-03-31T23:59:59Z')) }, { start: utc('2026-02-01T00:00:00Z'), end: utc('2026-08-01T00:00:00Z') });
	assert.deepEqual({ ...defaultTimespan(utc('2026-01-01T00:00:00Z')) }, { start: utc('2025-12-01T00:00:00Z'), end: utc('2026-06-01T00:00:00Z') });
});

test('a new form: 1 day, Local Time, the default columns, Min magnitude 12', () => {
	const settings = defaultEphemerisSettings(utc('2026-10-18T12:00:00Z'));
	assert.equal(settings.start, utc('2026-09-01T00:00:00Z'));
	assert.deepEqual(settings.interval, DEFAULT_INTERVAL);
	assert.deepEqual({ ...DEFAULT_INTERVAL }, { days: 1, hours: 0, minutes: 0 });
	assert.equal(settings.localTime, true);
	assert.deepEqual({ ...settings.columns }, {
		RA: true, Dec: true, Alt: true, Az: true, EcLon: false, EcLat: false, Elongation: true, SunDist: true, EarthDist: true, Magnitude: true
	});
	assert.deepEqual(JSON.parse(JSON.stringify(settings.requirements)), {
		maxSunDist: { checked: false, value: null }, maxEarthDist: { checked: false, value: null }, minMagnitude: { checked: true, value: 12 }
	});
	assert.ok(Object.isFrozen(settings) && Object.isFrozen(settings.columns) && Object.isFrozen(settings.requirements.minMagnitude));
});

test('the columns and requirements of spec 06, in order', () => {
	assert.deepEqual(COLUMNS.map(c => c.label), [
		'Right ascension (RA)', 'Declination (Dec)', 'Altitude (Alt)', 'Azimuth (Az)', 'Ecliptic Longitude',
		'Ecliptic Latitude', 'Elongation', 'Sun distance', 'Earth distance', 'Magnitude'
	]);
	assert.deepEqual(REQUIREMENTS.map(r => [r.label, r.input.min, r.input.max, r.input.decimals]), [
		['Max Sun distance', 0, 9.99, 2], ['Max Earth distance', 0, 9.99, 2], ['Min magnitude', -20, 40, 2]
	]);
});

test('the interval fields take whole days 0–3652, hours 0–23, minutes 0–59', () => {
	assert.deepEqual(JSON.parse(JSON.stringify(INTERVAL_INPUT)), {
		days: { min: 0, max: 3652, decimals: 0 }, hours: { min: 0, max: 23, decimals: 0 }, minutes: { min: 0, max: 59, decimals: 0 }
	});
});

test('the interval in milliseconds; 0 means 1 day', () => {
	assert.equal(intervalMs({ days: 1, hours: 0, minutes: 0 }), DAY);
	assert.equal(intervalMs({ days: 0, hours: 2, minutes: 30 }), 9_000_000);
	assert.equal(intervalMs({ days: 3652, hours: 23, minutes: 59 }), ((3652 * 24 + 23) * 60 + 59) * 60_000);
	assert.equal(intervalMs({ days: 0, hours: 0, minutes: 0 }), DAY);
});

test('timespan errors point at Start', () => {
	const start = utc('2026-01-01T00:00:00Z');
	assert.deepEqual(timespanErrors(start, start + 1000), []);
	assert.deepEqual(timespanErrors(start, start), [{ field: 'start', message: 'End date must be greater than start date' }]);
	assert.deepEqual(timespanErrors(start, start - 1000), [{ field: 'start', message: 'End date must be greater than start date' }]);
	const limit = 300 * 365.25 * DAY;
	assert.deepEqual(timespanErrors(start, start + limit - 1000), []);
	assert.deepEqual(timespanErrors(start, start + limit), [{ field: 'start', message: 'Timespan must be less than 300 years' }]);
});

test('validation: the requirements first, in order, then the timespan', () => {
	const base = defaultEphemerisSettings(utc('2026-10-18T12:00:00Z'));
	assert.deepEqual(validateEphemerisSettings(base), { ok: true, value: base });
	const broken = {
		...base,
		end: base.start,
		requirements: { maxSunDist: { checked: true, value: null }, maxEarthDist: { checked: false, value: null }, minMagnitude: { checked: true, value: null } }
	};
	const result = validateEphemerisSettings(broken);
	assert.equal(result.ok, false);
	assert.deepEqual(!result.ok && result.errors, [
		{ field: 'maxSunDist', message: 'Please enter Maximum Sun distance value' },
		{ field: 'minMagnitude', message: 'Please enter Minimum magnitude value' },
		{ field: 'start', message: 'End date must be greater than start date' }
	]);
	const earth = validateEphemerisSettings({ ...base, requirements: { ...base.requirements, maxEarthDist: { checked: true, value: null } } });
	assert.deepEqual(!earth.ok && earth.errors, [{ field: 'maxEarthDist', message: 'Please enter Maximum Earth distance value' }]);
	// an unchecked requirement may be empty, and a checked one may be 0
	assert.equal(validateEphemerisSettings({ ...base, requirements: { ...base.requirements, maxSunDist: { checked: true, value: 0 } } }).ok, true);
});

test('whole steps are exact: a step on End counts', () => {
	const start = utc('2026-01-01T00:00:00Z');
	assert.equal(wholeSteps(start, start + DAY, 3_600_000), 24);
	assert.equal(wholeSteps(start, start + DAY - 1000, 3_600_000), 23);
	assert.equal(wholeSteps(start, start, 3_600_000), 0);
	assert.equal(wholeSteps(start, start - DAY, 3_600_000), -1);
	assert.equal(wholeSteps(start, start + 300 * 365.25 * DAY - 1000, 60_000), 157_787_999);
});

test('the long-calculation count: steps times the comets', () => {
	const start = utc('2026-01-01T00:00:00Z');
	const settings = { ...defaultEphemerisSettings(start), start, end: start + 100 * DAY, interval: { days: 0, hours: 1, minutes: 0 } };
	assert.equal(calculationSteps(settings, 1), 2400);
	assert.equal(calculationSteps(settings, 963), 2_311_200);
	assert.equal(calculationSteps({ ...settings, end: start - DAY }, 5), 0);
	assert.equal(LONG_CALCULATION_STEPS, 100_000);
	assert.equal(LONG_CALCULATION_QUESTION, 'Calculation using selected settings could take a while, depending on your PC performances. Are you sure you want to continue?');
});

test('the title names the comet, or the size of the list', () => {
	const halley = { full: '1P/Halley' };
	assert.equal(ephemerisTitle({ multiple: false, comets: [halley, halley], selected: halley }), 'Ephemeris - 1P/Halley');
	assert.equal(ephemerisTitle({ multiple: true, comets: [halley, halley], selected: halley }), 'Ephemeris - 2 comets');
	assert.equal(ephemerisTitle({ multiple: true, comets: [halley], selected: null }), 'Ephemeris - 1 comets');
});
