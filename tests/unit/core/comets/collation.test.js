import { test } from 'node:test';
import assert from 'node:assert/strict';
import { execFileSync } from 'node:child_process';
import { compareText } from '../../../../app/js/core/comets/collation.js';

const MODULE = new URL('../../../../app/js/core/comets/collation.js', import.meta.url).href;
// strings that Czech, Danish and Lithuanian tailorings order differently from the root
const WORDS = ['z', 'aa', 'y', 'i', 'ch', 'h', 'Halley', 'halley', 'Ä'];
const ROOT = ['Ä', 'aa', 'ch', 'h', 'halley', 'Halley', 'i', 'y', 'z'];

test('the root order: case and accents after the letter', () => {
	assert.deepEqual(WORDS.toSorted(compareText), ROOT);
});

for (const locale of ['cs_CZ.UTF-8', 'da_DK.UTF-8', 'lt_LT.UTF-8']) {
	test(`the same order on a host whose locale is ${locale}`, () => {
		const code = `import { compareText } from ${JSON.stringify(MODULE)};
			console.log(JSON.stringify(${JSON.stringify(WORDS)}.toSorted(compareText)));`;
		const output = execFileSync(process.execPath, ['--input-type=module', '-e', code], { env: { ...process.env, LC_ALL: locale, LANG: locale } });
		assert.deepEqual(JSON.parse(String(output)), ROOT);
	});
}
