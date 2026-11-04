// elements.json: the derived elements P, a, n and Q (spec 02, Derived elements).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { assertRelativeClose, readVector } from '../helpers/vectors.js';
import { derivedElements } from '../../app/js/core/comets/comet.js';

test('P, a, Q and n for ellipses, parabolas and hyperbolas', () => {
	const { cases } = readVector('elements.json');
	assert.equal(cases.length, 9);
	for (const { q, e, ...expected } of cases) {
		const derived = derivedElements(q, e);
		for (const key of /** @type {const} */ (['P', 'a', 'Q', 'n'])) {
			assertRelativeClose(derived[key], expected[key], 1e-12, `${key}(q=${q}, e=${e})`);
		}
	}
});
