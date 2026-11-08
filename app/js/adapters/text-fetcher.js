// The TextFetcher port over fetch: the body read as a stream with progress, decoded by its
// byte-order mark. docs/spec/05-database-and-update.md, Download.
import { decodeBytes } from './text-decoding.js';

/**
 * the bytes to expect, when the response says so reliably: a length without a Content-Encoding,
 * which would count compressed bytes. A cross-origin response hides its Content-Encoding unless
 * the server exposes it, so a length the body outgrows is dropped while reading.
 * @param {Headers} headers
 * @returns {number | undefined}
 */
function expectedLength(headers) {
	const length = Number(headers.get('content-length') ?? '');
	if (headers.get('content-encoding') || !Number.isSafeInteger(length) || length <= 0) return undefined;
	return length;
}

/**
 * @param {typeof fetch} [fetchImpl]
 * @returns {import('../services/ports.js').TextFetcher}
 */
export function createFetchTextFetcher(fetchImpl = (input, init) => fetch(input, init)) {
	return Object.freeze({
		async fetchText(/** @type {string} */ url, /** @type {{ signal?: AbortSignal, onProgress?: (progress: import('../services/ports.js').FetchProgress) => void }} */ { signal, onProgress } = {}) {
			/** @type {Response} */
			let response;
			try {
				// revalidated: the elements change daily, and a cached copy would hide that
				response = await fetchImpl(url, { signal, cache: 'no-cache' });
			} catch (error) {
				if (signal?.aborted) throw signal.reason;
				throw new Error('network error', { cause: error });
			}
			if (!response.ok) throw new Error(`HTTP ${response.status}${response.statusText ? ` ${response.statusText}` : ''}`);
			let total = expectedLength(response.headers);
			onProgress?.({ received: 0, total });
			if (!response.body) return decodeBytes(new Uint8Array(await response.arrayBuffer()));

			/** @type {Uint8Array[]} */
			const chunks = [];
			let received = 0;
			const reader = response.body.getReader();
			try {
				for (;;) {
					const { done, value } = await reader.read();
					if (done) break;
					chunks.push(value);
					received += value.byteLength;
					if (total !== undefined && received > total) total = undefined;
					onProgress?.({ received, total });
				}
			} catch (error) {
				if (signal?.aborted) throw signal.reason;
				throw new Error('network error', { cause: error });
			}
			const bytes = new Uint8Array(received);
			let at = 0;
			for (const chunk of chunks) {
				bytes.set(chunk, at);
				at += chunk.byteLength;
			}
			return decodeBytes(bytes);
		}
	});
}
