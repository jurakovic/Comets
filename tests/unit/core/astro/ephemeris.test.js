import { test } from 'node:test';
import assert from 'node:assert/strict';
import { elongationSide, ephemeris } from '../../../../app/js/core/astro/ephemeris.js';
import { D0 } from '../../../../app/js/core/astro/math.js';
import { cometXyz, sunXyz } from '../../../../app/js/core/astro/position.js';

const jd = 2461332;
const observer = { latitude: 45.8, longitude: 15.97 };

/** a comet on a circle of 2 AU in the ecliptic, at perihelion at jd, `offset` degrees past the Sun */
function behindTheSun(/** @type {number} */ offset) {
	const N = sunXyz(jd).lon - 3.82394e-5 * (jd - D0) + offset;
	return { T: jd, q: 2, e: 0, w: 0, N, i: 0, g: 5, k: 4 };
}

/** the angle between the comet and the Sun as seen from the Earth, from the vectors */
function separation(/** @type {ReturnType<typeof behindTheSun>} */ comet) {
	const sun = sunXyz(jd);
	const body = cometXyz(comet, jd);
	const geo = [body.x + sun.x, body.y + sun.y, body.z];
	const s = [sun.x, sun.y, sun.z];
	const cross = [geo[1] * s[2] - geo[2] * s[1], geo[2] * s[0] - geo[0] * s[2], geo[0] * s[1] - geo[1] * s[0]];
	const dot = geo[0] * s[0] + geo[1] * s[1] + geo[2] * s[2];
	return Math.atan2(Math.hypot(...cross), dot) * 180 / Math.PI;
}

test('small elongations use the flat-sky formula, accurate where acos is not', () => {
	for (const offset of [0.01, 0.05, 0.12]) {
		const comet = behindTheSun(offset);
		const { Elongation } = ephemeris(comet, jd, observer);
		assert.ok(Math.abs(Elongation - separation(comet)) < 1e-6, `${offset}: ${Elongation} vs ${separation(comet)}`);
	}
	assert.ok(ephemeris(behindTheSun(0.05), jd, observer).Elongation < 0.1);
});

test('a comet east of the Sun is E, west of it W', () => {
	assert.equal(elongationSide(ephemeris(behindTheSun(30), jd, observer)), 'E');
	assert.equal(elongationSide(ephemeris(behindTheSun(-30), jd, observer)), 'W');
	assert.equal(elongationSide({ ...ephemeris(behindTheSun(30), jd, observer), PositionAngle: 180 }), 'W');
});

test('the record carries its JD and the magnitude law', () => {
	const comet = behindTheSun(40);
	const record = ephemeris(comet, jd, observer);
	assert.equal(record.JD, jd);
	const expected = 5 + 5 * Math.log10(record.EarthDist) + 2.5 * 4 * Math.log10(record.SunDist);
	assert.ok(Math.abs(record.Magnitude - expected) < 1e-12);
	assert.ok(Math.abs(record.SunDist - 2) < 1e-12);
});
