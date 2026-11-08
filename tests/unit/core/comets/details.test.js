import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readVector } from '../../../helpers/vectors.js';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { elementsRows, jplUrl, LIVE_VALUES, liveRows, perihelionRows } from '../../../../app/js/core/comets/details.js';
import { cometsWithResult, IMPORT_RESULT_CHOICES } from '../../../../app/js/core/comets/merge.js';
import { indexAfterChange } from '../../../../app/js/core/comets/selection.js';

const { comets } = readVector('comets.json');
/** @param {string} full */
const vectorComet = full => comets.find((/** @type {{ full: string }} */ c) => c.full === full);

/** a comet record from comets.json's fields */
function cometOf(/** @type {string} */ full) {
	const v = vectorComet(full);
	const comet = createComet({ full: v.full, id: v.id, name: v.name, fragment: v.fragment, Ty: v.Ty, Tm: v.Tm, Td: v.Td, Th: v.Th, q: v.q, e: v.e, w: v.w, N: v.N, i: v.i, g: v.g, k: v.k });
	assert.ok(comet);
	return comet;
}

/** @param {ReadonlyArray<import('../../../../app/js/core/comets/details.js').DetailRow>} rows */
const table = rows => rows.map(r => [r.label, r.symbol, r.value, r.unit]);

test('the Orbital elements tab of a periodic comet', () => {
	assert.deepEqual(table(elementsRows(cometOf('1P/Halley'))), [
		['Name', '', '1P/Halley', ''],
		['Elements perihelion date', 'T', '2061-08-01.5218', ''],
		['Period', 'P', '75.470540', 'years'],
		['Mean motion', 'n', '0.013060', '°/day'],
		['Perihelion distance', 'q', '0.570945', 'AU'],
		['Aphelion distance', 'Q', '35.146603', 'AU'],
		['Semi-major axis', 'a', '17.858774', 'AU'],
		['Eccentricity', 'e', '0.968030', ''],
		['Ascending node', 'Ω', '59.2610', '°'],
		['Inclination', 'i', '162.1917', '°'],
		['Argument of pericenter', 'ω', '112.1616', '°'],
		['Magnitude parameters', 'g, k', '5.5, 3.2', ''],
		['Equinox', '', '2000.0', '']
	]);
});

test('P, n, Q and a are left empty from a period of 1000 years', () => {
	const hyperbolic = comets.find((/** @type {{ P: number }} */ c) => c.P >= 1000);
	const rows = elementsRows(cometOf(hyperbolic.full));
	for (const key of ['P', 'n', 'Q', 'a']) assert.equal(rows.find(r => r.key === key)?.value, '', key);
	assert.notEqual(rows.find(r => r.key === 'q')?.value, '');
	const atLimit = createComet({ full: 'X/limit', id: 'C/2000 A1', q: 100, e: 0.9 });
	assert.ok(atLimit);
	assert.ok(atLimit.P >= 1000);
	assert.equal(elementsRows(atLimit).find(r => r.key === 'P')?.value, '');
});

test('the date pads the month, day and fraction, and keeps a negative year', () => {
	const old = createComet({ full: 'C/-146 P1', id: 'C/-146 P1', Ty: -146, Tm: 3, Td: 7, Th: 12, q: 0.5, e: 1 });
	assert.ok(old);
	assert.equal(elementsRows(old)[1].value, '-146-03-07.0012');
});

test('the Ephemeris tab\'s perihelion values', () => {
	const v = vectorComet('1P/Halley');
	assert.deepEqual(table(perihelionRows(cometOf('1P/Halley'), v)), [
		['Name', '', '1P/Halley', ''],
		['Perihelion date', '', '01.08.2061. 12:31:23', ''],
		['Period', '', '75.470540', 'years'],
		['Aphelion distance', '', '35.146603', 'AU'],
		['Perihelion distance', '', '0.570945', 'AU'],
		['Perihelion distance from Earth', '', '0.495975', 'AU'],
		['Perihelion magnitude', '', '2.03', '']
	]);
});

test('JPL Info looks the id up, without the fragment, URL-encoded', () => {
	assert.equal(jplUrl(cometOf('1P/Halley')), 'https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=1P');
	const fragment = createComet({ full: 'C/2023 R1-B', id: 'C/2023 R1', fragment: 'B', q: 1, e: 0.5 });
	assert.ok(fragment);
	assert.equal(jplUrl(fragment), 'https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=C%2F2023%20R1');
});

test('the index kept after the list changes', () => {
	const [a, b, c] = ['1P/Halley', '2P/Encke', '29P/Schwassmann-Wachmann'].map(cometOf);
	assert.equal(indexAfterChange([a, b, c], b, 0), 1, 'the selected comet, where it is now');
	assert.equal(indexAfterChange([c, a], b, 0), 0, 'gone: the index given');
	assert.equal(indexAfterChange([a, c], null, 1), 1, 'after Delete: the same index');
	assert.equal(indexAfterChange([a, c], null, 2), 1, 'clamped to the list');
	assert.equal(indexAfterChange([], a, 0), -1);
	// an import replaces the records: the comet is found again by its full name
	const [a2, b2, c2] = ['1P/Halley', '2P/Encke', '29P/Schwassmann-Wachmann'].map(cometOf);
	assert.notEqual(c2, c);
	assert.equal(indexAfterChange([a2, b2, c2], c, 0), 2);
	assert.equal(indexAfterChange([a], null, -1), 0);
});

/**
 * an ephemeris record with the values the live block reads
 * @param {Partial<import('../../../../app/js/core/astro/ephemeris.js').Ephemeris>} values
 * @returns {import('../../../../app/js/core/astro/ephemeris.js').Ephemeris}
 */
const record = values => ({
	JD: 0, RA: 76.85, Dec: 45.5, Alt: 12.345, Az: 270.004, EcLon: 0, EcLat: 0, Elongation: 30.125, PositionAngle: 200,
	SunDist: 1.2345675, EarthDist: 0.5, Magnitude: 7.125, ...values
});

test('the live block: its values at the instant, as the desktop writes them', () => {
	const rows = liveRows(record({}), record({}), 45);
	assert.deepEqual(rows.map(r => [r.label, r.value, r.unit]), [
		['Current distance from Sun', '1.234568', 'AU'],
		['Current distance from Earth', '0.500000', 'AU'],
		['Current magnitude', '7.13', ''],
		['Right Ascension', '05h07m24', ''],
		['Declination', '+45°30\'', ''],
		['Altitude', '12.35°', ''],
		['Azimuth', '270.00°', ''],
		['Elongation', '30.13° W', '']
	]);
	assert.equal(liveRows(record({ PositionAngle: 179.99 }), record({}), 45)[7].value, '30.13° E');
	assert.deepEqual(LIVE_VALUES.map(v => v.key), ['SunDist', 'EarthDist', 'Magnitude', 'RA', 'Dec', 'Alt', 'Az', 'Elongation']);
});

test('the trends: ▲ from an equal value up, and their colours by what is better', () => {
	const trends = (/** @type {number} */ change, /** @type {number} */ latitude) => {
		const previous = record({});
		const next = record(Object.fromEntries(LIVE_VALUES.map(({ key }) => [key, /** @type {number} */ (previous[key]) + change])));
		return liveRows(next, previous, latitude).map(r => `${r.trend.arrow} ${r.trend.tone}`);
	};
	assert.deepEqual(trends(1, 45), ['▲ worse', '▲ worse', '▲ worse', '▲ neutral', '▲ better', '▲ better', '▲ neutral', '▲ better']);
	assert.deepEqual(trends(-1, 45), ['▼ better', '▼ better', '▼ better', '▼ neutral', '▼ worse', '▼ worse', '▼ neutral', '▼ worse']);
	assert.deepEqual(trends(0, 45), trends(1, 45), 'an unchanged value counts as up');
	// in the south a falling declination is better
	assert.equal(trends(1, -0.000001)[4], '▲ worse');
	assert.equal(trends(-1, -10)[4], '▼ better');
	assert.equal(trends(1, 0)[4], '▲ better');
});

test('the import result browser\'s choices and lists, in merged order', () => {
	assert.deepEqual(IMPORT_RESULT_CHOICES.map(c => c.label), ['New comets', 'Updates', 'No changes', 'All comets']);
	const [a, b, c] = ['1P/Halley', '2P/Encke', '29P/Schwassmann-Wachmann'].map(cometOf);
	const merge = { comets: [a, b, c], results: /** @type {const} */ (['NoChanges', 'New', 'NoChanges']) };
	assert.deepEqual(cometsWithResult(merge, 'New'), [b]);
	assert.deepEqual(cometsWithResult(merge, 'NoChanges'), [a, c]);
	assert.deepEqual(cometsWithResult(merge, 'Update'), []);
	assert.deepEqual(cometsWithResult(merge, 'All'), [a, b, c]);
	assert.ok(Object.isFrozen(cometsWithResult(merge, 'All')));
});
