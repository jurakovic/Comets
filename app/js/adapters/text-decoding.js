// Bytes to text as File.ReadAllLines reads them: UTF-8, or UTF-16 or UTF-32 by a byte-order
// mark, which is dropped; invalid bytes become U+FFFD. docs/spec/03-element-formats.md, Text conventions.

const REPLACEMENT = 0xfffd;

/**
 * UTF-32 code units to text, which TextDecoder does not read
 * @param {Uint8Array} bytes without the byte-order mark
 * @param {boolean} littleEndian
 * @returns {string}
 */
function decodeUtf32(bytes, littleEndian) {
	const view = new DataView(bytes.buffer, bytes.byteOffset, bytes.byteLength);
	const whole = bytes.byteLength - (bytes.byteLength % 4);
	/** @type {number[]} */
	const points = [];
	for (let at = 0; at < whole; at += 4) {
		const point = view.getUint32(at, littleEndian);
		const valid = point <= 0x10ffff && (point < 0xd800 || point > 0xdfff);
		points.push(valid ? point : REPLACEMENT);
	}
	// a last unit cut short
	if (whole < bytes.byteLength) points.push(REPLACEMENT);
	let text = '';
	// in pieces, since String.fromCodePoint takes its points as arguments
	for (let at = 0; at < points.length; at += 8192) text += String.fromCodePoint(...points.slice(at, at + 8192));
	return text;
}

/**
 * @param {Uint8Array} bytes
 * @param {ReadonlyArray<number>} mark
 * @returns {boolean}
 */
const startsWith = (bytes, mark) => bytes.length >= mark.length && mark.every((byte, index) => bytes[index] === byte);

/**
 * a file's or a download's bytes as text, decoded by its byte-order mark: UTF-32 before UTF-16,
 * since FF FE 00 00 starts with FF FE, as .NET checks them
 * @param {Uint8Array} bytes
 * @returns {string}
 */
export function decodeBytes(bytes) {
	if (startsWith(bytes, [0xff, 0xfe, 0x00, 0x00])) return decodeUtf32(bytes.subarray(4), true);
	if (startsWith(bytes, [0x00, 0x00, 0xfe, 0xff])) return decodeUtf32(bytes.subarray(4), false);
	if (startsWith(bytes, [0xff, 0xfe])) return new TextDecoder('utf-16le').decode(bytes);
	if (startsWith(bytes, [0xfe, 0xff])) return new TextDecoder('utf-16be').decode(bytes);
	// TextDecoder drops a UTF-8 byte-order mark itself
	return new TextDecoder('utf-8').decode(bytes);
}
