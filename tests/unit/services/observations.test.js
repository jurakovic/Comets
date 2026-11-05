import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFakeClock } from '../../helpers/fakes.js';
import { assertClose, readVector, VECTOR_NOW } from '../../helpers/vectors.js';
import { createComet, nearestPerihelion } from '../../../app/js/core/comets/comet.js';
import { currentValues, perihelionValues } from '../../../app/js/core/comets/observer-values.js';
import { CURRENT_REFRESH_MS, createObservationsService } from '../../../app/js/services/observations.js';
import { instantToJd } from '../../../app/js/core/time/julian.js';

const { comets: vectors } = readVector('comets.json');
const EQUATOR = Object.freeze({ latitude: 0, longitude: 0 });
const YEAR_MS = 365.25 * 86_400_000;

/** @param {any} vector */
function fromVector(vector) {
	const { full, id, name, fragment, Ty, Tm, Td, Th, q, e, w, N, i, g, k } = vector;
	const comet = createComet({ full, id, name, fragment, Ty, Tm, Td, Th, q, e, w, N, i, g, k });
	assert.ok(comet, full);
	return comet;
}

const halley = fromVector(vectors.find((/** @type {any} */ v) => v.full === '1P/Halley'));
const encke = fromVector(vectors.find((/** @type {any} */ v) => v.full === '2P/Encke'));

/** a service over a fake clock at the vector clock, counting how often it reads the observer */
function setup(start = VECTOR_NOW) {
	const clock = createFakeClock(start);
	const reads = { count: 0 };
	const service = createObservationsService({ clock, observer: () => { reads.count += 1; return EQUATOR; } });
	return { clock, service, reads };
}

test('the session values of every sample comet at the vector clock', () => {
	const { service } = setup();
	for (const vector of vectors) {
		const comet = fromVector(vector);
		for (const key of ['Tn', 'PerihEarthDist', 'PerihMag', 'CurrentSunDist', 'CurrentEarthDist', 'CurrentMag']) {
			assertClose(/** @type {number} */ (service.value(comet, key)), vector[key], 1e-8, `${vector.full} ${key}`);
		}
	}
});

test('the other properties come from the comet', () => {
	const { service } = setup();
	assert.equal(service.value(halley, 'full'), '1P/Halley');
	assert.equal(service.value(halley, 'q'), halley.q);
	assert.equal(service.value(halley, 'sortkey'), halley.sortkey);
	assert.throws(() => service.value(halley, 'nonsense'), /not a comet field/);
});

test('Tn keeps the clock of its first use for the session (spec 10, Q11)', () => {
	const { clock, service } = setup();
	const first = service.nearestPerihelion(halley);
	clock.advance(100 * YEAR_MS);
	const later = nearestPerihelion(halley, instantToJd(clock.now()));
	assert.notEqual(later, first, 'a century on, another perihelion is the nearest');
	assert.equal(service.nearestPerihelion(halley), first);
	assert.equal(service.value(halley, 'Tn'), first);
});

test('Tn is computed on first use, against the clock at that moment', () => {
	const { clock, service } = setup();
	clock.advance(100 * YEAR_MS);
	assert.equal(service.value(halley, 'Tn'), nearestPerihelion(halley, instantToJd(clock.now())));
});

test('Perih* are computed once, at the Tn of the session (spec 10, Q11)', () => {
	const { clock, reads, service } = setup();
	const Tn = service.nearestPerihelion(encke);
	clock.advance(10 * YEAR_MS);
	const expected = perihelionValues(encke, Tn, EQUATOR);
	assert.equal(service.value(encke, 'PerihMag'), expected.PerihMag);
	assert.equal(service.value(encke, 'PerihEarthDist'), expected.PerihEarthDist);
	clock.advance(10 * YEAR_MS);
	assert.equal(service.value(encke, 'PerihMag'), expected.PerihMag);
	assert.equal(reads.count, 1);
});

test('Current* are kept for 5 minutes and then recomputed with the clock of then', () => {
	const { clock, reads, service } = setup();
	const first = currentValues(halley, instantToJd(VECTOR_NOW), EQUATOR);
	assert.equal(service.value(halley, 'CurrentMag'), first.CurrentMag);
	clock.advance(CURRENT_REFRESH_MS);
	// exactly 5 minutes old is not more than 5 minutes
	assert.equal(service.value(halley, 'CurrentMag'), first.CurrentMag);
	assert.equal(service.value(halley, 'CurrentEarthDist'), first.CurrentEarthDist);
	assert.equal(reads.count, 1);
	clock.advance(1);
	const second = currentValues(halley, instantToJd(clock.now()), EQUATOR);
	assert.notEqual(second.CurrentEarthDist, first.CurrentEarthDist);
	assert.equal(service.value(halley, 'CurrentEarthDist'), second.CurrentEarthDist);
	assert.equal(service.value(halley, 'CurrentSunDist'), second.CurrentSunDist);
	assert.equal(service.value(halley, 'CurrentMag'), second.CurrentMag);
	// the observer is read again for the new ephemeris, as the desktop reads its settings
	assert.equal(reads.count, 2);
});

test('each comet\'s Current* are as old as their own first use', () => {
	const { clock, service } = setup();
	service.value(halley, 'CurrentMag');
	clock.advance(CURRENT_REFRESH_MS);
	const enckeFirst = service.value(encke, 'CurrentSunDist');
	clock.advance(1);
	// Halley's are past 5 minutes, Encke's are not
	assert.equal(service.value(halley, 'CurrentSunDist'), currentValues(halley, instantToJd(clock.now()), EQUATOR).CurrentSunDist);
	assert.equal(service.value(encke, 'CurrentSunDist'), enckeFirst);
});

test('a re-imported comet is a new record with values of its own', () => {
	const { clock, service } = setup();
	const Tn = service.nearestPerihelion(halley);
	clock.advance(100 * YEAR_MS);
	const again = fromVector(vectors.find((/** @type {any} */ v) => v.full === '1P/Halley'));
	assert.notEqual(service.nearestPerihelion(again), Tn);
});

test('the clock in milliseconds: Current* use the JD of now with its milliseconds dropped', () => {
	const { service } = setup(VECTOR_NOW + 999);
	assert.equal(service.value(halley, 'CurrentEarthDist'), currentValues(halley, instantToJd(VECTOR_NOW), EQUATOR).CurrentEarthDist);
});
