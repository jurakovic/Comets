import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	createLiveClock, followedSecond, pickLiveClock, resumeLiveClock, tickDelay, tickLiveClock, TICK_AFTER_BOUNDARY
} from '../../../../app/js/core/time/live-clock.js';

const at = (/** @type {string} */ iso) => Date.parse(iso);

test('a new clock shows the current second, and follows the system clock', () => {
	assert.equal(TICK_AFTER_BOUNDARY, 20);
	assert.deepEqual({ ...createLiveClock(at('2026-11-05T06:19:11.500Z')) }, { shownAt: at('2026-11-05T06:19:11Z'), following: true });
	// within 20 ms of a boundary its tick counts as run
	assert.equal(followedSecond(at('2026-11-05T06:19:11.000Z')), at('2026-11-05T06:19:11Z'));
	assert.equal(followedSecond(at('2026-11-05T06:19:11.010Z')), at('2026-11-05T06:19:11Z'));
	assert.equal(followedSecond(at('2026-11-05T06:19:10.999Z')), at('2026-11-05T06:19:10Z'));
	assert.equal(followedSecond(at('1969-12-31T23:59:59.500Z')), at('1969-12-31T23:59:59Z'), 'before the epoch too');
	assert.ok(Object.isFrozen(createLiveClock(0)));
});

test('the next tick is 20 ms past the next boundary more than 20 ms away', () => {
	assert.equal(tickDelay(at('2026-11-05T06:19:11.500Z')), 520);
	assert.equal(tickDelay(at('2026-11-05T06:19:11.021Z')), 999);
	assert.equal(tickDelay(at('2026-11-05T06:19:11.020Z')), 1000);
	assert.equal(tickDelay(at('2026-11-05T06:19:10.999Z')), 21);
	assert.equal(tickDelay(at('1969-12-31T23:59:59.500Z')), 520, 'before the epoch too');
	// just before the tick point: a coarse clock reading a tick's own moment, or a resume
	assert.equal(tickDelay(at('2026-11-05T06:19:11.000Z')), 1020);
	assert.equal(tickDelay(at('2026-11-05T06:19:11.010Z')), 1010);
	assert.equal(tickDelay(at('2026-11-05T06:19:11.017Z')), 1003);
});

test('a coarse clock, or one stepped back by up to 20 ms, never brings a second tick within the second', () => {
	for (const reading of [
		(/** @type {number} */ real) => Math.floor(real / 100) * 100,
		(/** @type {number} */ real) => Math.floor(real / (50 / 3)) * (50 / 3),
		(/** @type {number} */ real) => real - 20
	]) {
		let real = at('2026-11-05T06:19:11.700Z');
		let clock = createLiveClock(reading(real));
		for (let i = 0; i < 10; i += 1) {
			real += tickDelay(reading(real));
			clock = tickLiveClock(clock);
			assert.equal(clock.shownAt, Math.floor(real / 1000) * 1000, `tick ${i} at ${new Date(real).toISOString()}`);
		}
	}
});

test('ticks and the delay stay in step with the system clock while it is followed', () => {
	// a clock created at any moment, ticked at each tickDelay, shows the followed second
	for (const start of [at('2026-11-05T06:19:11.000Z'), at('2026-11-05T06:19:11.005Z'), at('2026-11-05T06:19:11.700Z')]) {
		let clock = createLiveClock(start);
		let now = start;
		for (let i = 0; i < 5; i += 1) {
			now += tickDelay(now);
			clock = tickLiveClock(clock);
			assert.equal(clock.shownAt, followedSecond(now), new Date(start).toISOString());
		}
	}
});

test('a tick is exactly a second, however late', () => {
	const clock = tickLiveClock(pickLiveClock(at('2061-08-01T12:31:23Z')));
	assert.deepEqual({ ...clock }, { shownAt: at('2061-08-01T12:31:24Z'), following: false });
});

test('resuming: a followed clock shows now, a picked date stays where it stopped', () => {
	const followed = createLiveClock(at('2026-11-05T06:19:11.500Z'));
	assert.equal(resumeLiveClock(followed, at('2026-11-05T06:20:00.300Z')).shownAt, at('2026-11-05T06:20:00Z'));
	const picked = pickLiveClock(at('2061-08-01T12:31:23Z'));
	assert.equal(resumeLiveClock(picked, at('2026-11-05T06:20:00.300Z')), picked);
});
