import { test } from 'node:test';
import assert from 'node:assert/strict';
import {
	createLiveClock, followedSecond, pickLiveClock, resumeLiveClock, tickDelay, tickLiveClock, TICK_AFTER_BOUNDARY
} from '../../../../app/js/core/time/live-clock.js';

const at = (/** @type {string} */ iso) => Date.parse(iso);

test('a new clock shows the second whose tick has run, and follows the system clock', () => {
	assert.equal(TICK_AFTER_BOUNDARY, 20);
	assert.deepEqual({ ...createLiveClock(at('2026-11-05T06:19:11.500Z')) }, { shownAt: at('2026-11-05T06:19:11Z'), following: true });
	// within 20 ms of a boundary its tick is still to come, and will move it on
	assert.equal(followedSecond(at('2026-11-05T06:19:11.010Z')), at('2026-11-05T06:19:10Z'));
	assert.equal(followedSecond(at('2026-11-05T06:19:11.020Z')), at('2026-11-05T06:19:11Z'));
	assert.ok(Object.isFrozen(createLiveClock(0)));
});

test('the next tick is 20 ms past the next boundary, and never now', () => {
	assert.equal(tickDelay(at('2026-11-05T06:19:11.500Z')), 520);
	assert.equal(tickDelay(at('2026-11-05T06:19:11.010Z')), 10);
	assert.equal(tickDelay(at('2026-11-05T06:19:11.020Z')), 1000);
	assert.equal(tickDelay(at('2026-11-05T06:19:11.021Z')), 999);
	assert.equal(tickDelay(at('2026-11-05T06:19:11.000Z')), 20);
	assert.equal(tickDelay(at('1969-12-31T23:59:59.500Z')), 520, 'before the epoch too');
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
