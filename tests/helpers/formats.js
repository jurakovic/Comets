// Comparing imported comets with the import-expected vectors, within the tolerances of
// docs/spec/11-test-vectors.md.
import assert from 'node:assert/strict';
import { assertClose, assertRelativeClose, readVector, readVectorText } from './vectors.js';
import { detectFormat } from '../../app/js/core/formats/detection.js';
import { importElements } from '../../app/js/core/formats/import.js';
import { FORMATS } from '../../app/js/core/formats/registry.js';

const TEXT = /** @type {const} */ (['full', 'id', 'name', 'fragment', 'sortkey']);
const INTEGERS = /** @type {const} */ (['Ty', 'Tm', 'Td', 'Th']);
const ELEMENTS = /** @type {const} */ (['q', 'e', 'w', 'N', 'i', 'g', 'k']);
const DERIVED = /** @type {const} */ (['P', 'a', 'Q', 'n']);

/**
 * comets equal to a vector's: strings and integers exactly, parsed elements exactly, elements a
 * format computes within 1e-9, T within 1e-8 day, derived elements within 1e-12 relative
 * @param {ReadonlyArray<import('../../app/js/core/comets/comet.js').Comet>} actual
 * @param {ReadonlyArray<any>} expected
 * @param {{ computed?: ReadonlyArray<string>, label?: string }} [options]
 */
export function assertComets(actual, expected, { computed = [], label = '' } = {}) {
	assert.deepEqual(actual.map(c => c.full), expected.map(c => c.full), `${label} full names in order`);
	actual.forEach((comet, index) => {
		const vector = expected[index];
		const what = `${label} ${vector.full}`;
		for (const key of [...TEXT, ...INTEGERS]) assert.equal(comet[key], vector[key], `${what} ${key}`);
		for (const key of ELEMENTS) {
			if (computed.includes(key)) assertClose(comet[key], vector[key], 1e-9, `${what} ${key}`);
			else assert.equal(comet[key], vector[key], `${what} ${key}`);
		}
		assertClose(comet.T, vector.T, 1e-8, `${what} T`);
		for (const key of DERIVED) assertRelativeClose(comet[key], vector[key], 1e-12, `${what} ${key}`);
	});
}

/**
 * an import-expected vector: the input file's detected type and its import into an empty database
 * @param {string} name the file in import-expected/
 * @param {{ computed?: ReadonlyArray<string> }} [options] elements the format computes
 */
export function assertImportExpected(name, options = {}) {
	const vector = readVector(`import-expected/${name}`);
	const text = readVectorText(vector.file);
	const key = detectFormat(text);
	assert.equal(key, vector.detectedType, `${name} detected type`);
	const format = FORMATS.find(f => f.key === key);
	assert.equal(format?.name ?? null, vector.typeName, `${name} type name`);
	const comets = format ? importElements([], key, text).comets : [];
	assertComets(comets, vector.comets, { ...options, label: name });
}
