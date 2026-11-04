import { test } from 'node:test';
import assert from 'node:assert/strict';
import { earthXyz, sunXyz } from '../../../../app/js/core/astro/position.js';
import { J2000 } from '../../../../app/js/core/astro/math.js';

test('the Sun at J2000 is near 280.4° at 0.983 AU, in the ecliptic', () => {
	const sun = sunXyz(J2000);
	assert.ok(Math.abs(sun.lon - 280.4) < 0.1, String(sun.lon));
	assert.ok(Math.abs(sun.r - 0.9833) < 0.0005, String(sun.r));
	assert.equal(sun.z, 0);
	assert.ok(Math.abs(Math.hypot(sun.x, sun.y) - sun.r) < 1e-15);
});

test('the Earth is the Sun turned round', () => {
	const jd = 2461332;
	const sun = sunXyz(jd);
	assert.deepEqual(earthXyz(jd), { x: -sun.x, y: -sun.y, z: 0, r: sun.r });
});
