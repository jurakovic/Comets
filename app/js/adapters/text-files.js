// The TextFileReader port: a chosen or dropped file read whole and decoded by its byte-order
// mark. docs/spec/05-database-and-update.md, Local file.
import { decodeBytes } from './text-decoding.js';

/**
 * @returns {import('../services/ports.js').TextFileReader}
 */
export function createBlobTextReader() {
	return Object.freeze({
		readText: async (/** @type {Blob} */ file) => decodeBytes(new Uint8Array(await file.arrayBuffer()))
	});
}
