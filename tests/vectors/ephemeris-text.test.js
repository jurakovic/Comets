// ephemeris-text/: the calculation and the text output for 5 settings cases, UTC and
// Europe/Zagreb, at the vector clock (spec 06).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { EPHEMERIS_CASES, ephemerisCase } from '../helpers/ephemeris.js';
import { drain } from '../helpers/generators.js';
import { readVectorText, toLf } from '../helpers/vectors.js';
import { calculateEphemerides } from '../../app/js/core/ephemeris/calculation.js';
import { ephemerisTitle, validateEphemerisSettings } from '../../app/js/core/ephemeris/settings.js';
import { ephemerisText } from '../../app/js/core/ephemeris/text.js';

test('5 cases', () => {
	assert.equal(EPHEMERIS_CASES.length, 5);
});

for (const { id, file, settings: vector } of EPHEMERIS_CASES) {
	test(id, () => {
		const { settings, selection, input } = ephemerisCase(vector);
		assert.ok(validateEphemerisSettings(settings).ok);
		assert.equal(ephemerisTitle(selection), vector.title);
		const { yielded, result } = drain(calculateEphemerides(input));
		assert.deepEqual(yielded.at(-1), { done: input.comets.length, total: input.comets.length });
		const text = ephemerisText(result, settings, input.timeZone);
		assert.ok(/^(?:[^\r\n]*\r\n)*$/.test(text), 'every line ends with CRLF');
		assert.equal(toLf(text), readVectorText(file));
	});
}
