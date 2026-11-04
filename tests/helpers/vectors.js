// Reading the spec's test vectors and comparing within their tolerances
// (docs/spec/11-test-vectors.md, Tolerances).
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import { fileURLToPath } from 'node:url';

const VECTORS = new URL('../../docs/spec/vectors/', import.meta.url);

/** the vectors' frozen clock, 2026-10-18 12:00:00 UTC, in UTC milliseconds */
export const VECTOR_NOW = Date.UTC(2026, 9, 18, 12, 0, 0);

/** @param {string} path relative to docs/spec/vectors */
export function vectorPath(path) {
	return fileURLToPath(new URL(path, VECTORS));
}

/**
 * a vector file parsed as JSON
 * @param {string} path relative to docs/spec/vectors
 * @returns {any}
 */
export function readVector(path) {
	return JSON.parse(readFileSync(vectorPath(path), 'utf8'));
}

/**
 * a vector text file with LF line endings, as the vectors store them
 * @param {string} path relative to docs/spec/vectors
 */
export function readVectorText(path) {
	return readFileSync(vectorPath(path), 'utf8');
}

/** text with CRLF and CR line endings turned into LF, for comparing with a vector */
export function toLf(/** @type {string} */ text) {
	return text.replace(/\r\n?/g, '\n');
}

/**
 * |actual − expected| ≤ tolerance
 * @param {number} actual
 * @param {number} expected
 * @param {number} tolerance
 * @param {string} [what] names the value in the failure message
 */
export function assertClose(actual, expected, tolerance, what = 'value') {
	const difference = Math.abs(actual - expected);
	assert.ok(difference <= tolerance, `${what}: ${actual} differs from ${expected} by ${difference} (tolerance ${tolerance})`);
}

/**
 * |actual − expected| ≤ tolerance × |expected|
 * @param {number} actual
 * @param {number} expected
 * @param {number} tolerance relative
 * @param {string} [what]
 */
export function assertRelativeClose(actual, expected, tolerance, what = 'value') {
	assertClose(actual, expected, tolerance * Math.abs(expected), what);
}

/**
 * angles in degrees within tolerance, compared modulo 360
 * @param {number} actual
 * @param {number} expected
 * @param {number} tolerance degrees
 * @param {string} [what]
 */
export function assertAngleClose(actual, expected, tolerance, what = 'angle') {
	const difference = Math.abs((((actual - expected) % 360) + 540) % 360 - 180);
	assert.ok(difference <= tolerance, `${what}: ${actual}° differs from ${expected}° by ${difference}° (tolerance ${tolerance}°)`);
}

/**
 * the instant of a vector's UTC timestamp, `2026-10-18T12:00:00Z`, optionally followed by
 * milliseconds the case adds: `2026-10-18T12:00:00Z (+999 ms)`
 * @param {string} text
 * @returns {number} UTC milliseconds
 */
export function utcInstant(text) {
	const match = /^(\S+Z)(?: \(\+(\d+) ms\))?$/.exec(text);
	assert.ok(match, `not a vector timestamp: ${text}`);
	const instant = Date.parse(match[1]);
	assert.ok(Number.isFinite(instant), `not a vector timestamp: ${text}`);
	return instant + Number(match[2] ?? 0);
}

/**
 * a UTC instant as the vectors write it, `2026-10-18T12:00:00Z`, for exact comparison
 * @param {number} instant UTC milliseconds
 */
export function isoUtc(instant) {
	return new Date(instant).toISOString().replace(/\.000Z$/, 'Z');
}
