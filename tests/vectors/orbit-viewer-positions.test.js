// orbit-viewer-positions.json: heliocentric ecliptic-of-date comet positions (spec 04, Comet position).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertClose, readVector } from '../helpers/vectors.js';
import { cometXyz } from '../../app/js/core/astro/position.js';

const { cases } = readVector('orbit-viewer-positions.json');
const sample = new Map(readVector('comets.json').comets.map((/** @type {any} */ comet) => [comet.full, comet]));

test('comet positions at the vector dates', () => {
	assert.equal(cases.length, 136);
	for (const { comet, utc, jd, x, y, z, r } of cases) {
		const elements = sample.get(comet);
		assert.ok(elements, comet);
		const position = cometXyz(elements, jd);
		const what = `${comet} at ${utc}`;
		assertClose(position.x, x, 1e-8, `${what} x`);
		assertClose(position.y, y, 1e-8, `${what} y`);
		assertClose(position.z, z, 1e-8, `${what} z`);
		assertClose(position.r, r, 1e-8, `${what} r`);
	}
});
