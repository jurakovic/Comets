import { test } from 'node:test';
import assert from 'node:assert/strict';
import { decodeBytes } from '../../../app/js/adapters/text-decoding.js';

const TEXT = 'C/1995 O1 (Hale-Bopp)\r\nŠ€😀\n';

/** @param {string} text @param {boolean} littleEndian */
function utf16(text, littleEndian) {
	const bytes = new Uint8Array(2 + text.length * 2);
	const view = new DataView(bytes.buffer);
	view.setUint16(0, 0xfeff, littleEndian);
	for (let i = 0; i < text.length; i += 1) view.setUint16(2 + i * 2, text.charCodeAt(i), littleEndian);
	return bytes;
}

/** @param {string} text @param {boolean} littleEndian */
function utf32(text, littleEndian) {
	const points = [0xfeff, ...[...text].map(c => /** @type {number} */ (c.codePointAt(0)))];
	const bytes = new Uint8Array(points.length * 4);
	const view = new DataView(bytes.buffer);
	points.forEach((point, i) => view.setUint32(i * 4, point, littleEndian));
	return bytes;
}

const utf8 = (/** @type {string} */ text) => new TextEncoder().encode(text);

test('UTF-8, with or without its byte-order mark', () => {
	assert.equal(decodeBytes(utf8(TEXT)), TEXT);
	assert.equal(decodeBytes(new Uint8Array([0xef, 0xbb, 0xbf, ...utf8(TEXT)])), TEXT);
	assert.equal(decodeBytes(new Uint8Array()), '');
	assert.equal(decodeBytes(new Uint8Array([0xef, 0xbb, 0xbf])), '');
});

test('invalid UTF-8 becomes U+FFFD', () => {
	assert.equal(decodeBytes(new Uint8Array([0x41, 0xff, 0x42, 0xc3])), 'A�B�');
});

test('UTF-16 in either byte order, by its byte-order mark', () => {
	assert.equal(decodeBytes(utf16(TEXT, true)), TEXT);
	assert.equal(decodeBytes(utf16(TEXT, false)), TEXT);
});

test('UTF-32 in either byte order, checked before UTF-16', () => {
	assert.equal(decodeBytes(utf32(TEXT, true)), TEXT);
	assert.equal(decodeBytes(utf32(TEXT, false)), TEXT);
	const long = 'x'.repeat(20000);
	assert.equal(decodeBytes(utf32(long, true)), long);
});

test('UTF-32 that is not a code point, or is cut short, becomes U+FFFD', () => {
	const bytes = new Uint8Array([0xff, 0xfe, 0, 0, 0x41, 0, 0, 0, 0, 0xd8, 0, 0, 0, 0, 0x11, 0, 0x42, 0]);
	assert.equal(decodeBytes(bytes), 'A���');
});
