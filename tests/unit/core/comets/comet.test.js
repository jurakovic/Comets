import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createComet, derivedElements, nearestPerihelion, sameComet } from '../../../../app/js/core/comets/comet.js';
import { MAXIMUM_JD, MINIMUM_JD } from '../../../../app/js/core/time/range.js';

const halley = createComet({ full: '1P/Halley', id: '1P', name: 'Halley', Ty: 2061, Tm: 8, Td: 1, Th: 5218, q: 0.570945, e: 0.96803, w: 112.1616, N: 59.261, i: 162.1917, g: 5.5, k: 3.2 });

test('a comet is frozen, with T from JD0, defaults, and its sort key', () => {
	assert.ok(halley);
	assert.ok(Object.isFrozen(halley));
	assert.equal(halley.T, 2474038.0218);
	assert.equal(halley.sortkey, '000010000000000');
	const bare = createComet({ id: '2P' });
	assert.deepEqual(bare && [bare.full, bare.name, bare.fragment, bare.q, bare.g, bare.Ty], ['', '', '', 0, 0, 0]);
});

test('a format may give T and the sort key itself (Voyager II has none)', () => {
	const voyager = createComet({ full: 'Somebody', name: 'Somebody', T: 2460000.5, sortkey: '' });
	assert.equal(voyager?.T, 2460000.5);
	assert.equal(voyager?.sortkey, '');
});

test('an id that gives no sort key makes the record invalid', () => {
	assert.equal(createComet({ full: 'x', id: '' }), null);
	assert.equal(createComet({ full: 'C/1995O1', id: 'C/1995O1' }), null);
});

test('parabolic comets get finite P, a and Q from e = 0.999999 (spec 10, Q6)', () => {
	const { P, a, Q, n } = derivedElements(0.8, 1);
	assert.ok(Number.isFinite(P) && a > 799_999 && Q > a && n === 0);
});

test('comets are the same when the name, T and the elements are exactly equal', () => {
	assert.ok(halley);
	const again = createComet({ ...halley });
	assert.ok(again && sameComet(halley, again));
	const moved = createComet({ ...halley, T: undefined, Th: 5219 });
	assert.ok(moved && !sameComet(halley, moved));
	const brighter = createComet({ ...halley, g: 5.4 });
	assert.ok(brighter && !sameComet(halley, brighter));
});

/** the desktop's Tn loop, transcribed, with each candidate computed afresh */
function desktopTn(/** @type {number} */ T, /** @type {number} */ P, /** @type {number} */ now) {
	const period = Number(P.toPrecision(15)) * 365.25;
	let m = 0;
	while (T + (m - 1) * period > MINIMUM_JD) m -= 1;
	const candidates = [];
	for (; T + m * period < MAXIMUM_JD; m++) candidates.push(T + m * period);
	if (candidates.length === 0) return T;
	return candidates.reduce((best, t) => Math.abs(t - now) < Math.abs(best - now) ? t : best);
}

/** a periodic comet with the given T and period */
function periodic(/** @type {number} */ T, /** @type {number} */ P) {
	const comet = createComet({ id: '9P', T, q: 1, e: 0 });
	assert.ok(comet);
	return { ...comet, P };
}

test('the nearest perihelion agrees with the desktop loop', () => {
	let seed = 20261018;
	const random = () => (seed = (seed * 1_103_515_245 + 12_345) % 2_147_483_648) / 2_147_483_648;
	for (let n = 0; n < 3000; n++) {
		const T = 2_200_000 + random() * 500_000;
		const P = n % 3 === 0 ? 0.5 + random() * 20 : 3 + random() * 900;
		const now = 2_300_000 + random() * 300_000;
		assert.equal(nearestPerihelion(periodic(T, P), now), desktopTn(T, P, now), `T=${T} P=${P} now=${now}`);
	}
});

test('the nearest perihelion is T when the comet is not periodic, has no candidate, or no period', () => {
	const now = 2461332;
	const comet = createComet({ id: 'C/1995 O1', T: 2450536.5, q: 0.9, e: 0.995 });
	assert.ok(comet);
	assert.equal(nearestPerihelion(comet, now), 2450536.5);
	// D6: after 2300 and more than the range away from 1700
	assert.equal(nearestPerihelion(periodic(MAXIMUM_JD + 10, 700), now), MAXIMUM_JD + 10);
	assert.equal(nearestPerihelion(periodic(2460000, 0), now), 2460000);
	assert.equal(nearestPerihelion(periodic(2460000, NaN), now), 2460000);
});

test('a perihelion before 1700 is a candidate when the comet cannot step back from it', () => {
	assert.equal(nearestPerihelion(periodic(MINIMUM_JD - 50, 1000), 2461332), MINIMUM_JD - 50);
});

test('equally near perihelia give the earlier one', () => {
	const period = 10 * 365.25;
	assert.equal(nearestPerihelion(periodic(2460000, 10), 2460000 + period / 2), 2460000);
});
