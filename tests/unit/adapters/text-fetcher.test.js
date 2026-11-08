import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createFetchTextFetcher } from '../../../app/js/adapters/text-fetcher.js';

/**
 * a response whose body comes in the chunks given
 * @param {Uint8Array[]} chunks
 * @param {ResponseInit} [init]
 */
function streamed(chunks, init) {
	const body = new ReadableStream({
		start(controller) {
			for (const chunk of chunks) controller.enqueue(chunk);
			controller.close();
		}
	});
	return new Response(body, init);
}

const bytes = (/** @type {string} */ text) => new TextEncoder().encode(text);

/**
 * a fetcher over a fake fetch that answers with response, recording each request
 * @param {(url: string, init?: RequestInit) => Promise<Response>} answer
 */
function fetcherWith(answer) {
	/** @type {Array<{ url: string, init?: RequestInit }>} */
	const requests = [];
	const fetcher = createFetchTextFetcher(async (input, init) => {
		requests.push({ url: String(input), init });
		return answer(String(input), init);
	});
	return { fetcher, requests };
}

test('the body as text, with progress against Content-Length, revalidated', async () => {
	const { fetcher, requests } = fetcherWith(async () => streamed([bytes('abc'), bytes('def')], { headers: { 'content-length': '6' } }));
	/** @type {unknown[]} */
	const progress = [];
	const text = await fetcher.fetchText('https://example.org/x.txt', { onProgress: p => progress.push({ ...p }) });
	assert.equal(text, 'abcdef');
	assert.deepEqual(progress, [{ received: 0, total: 6 }, { received: 3, total: 6 }, { received: 6, total: 6 }]);
	assert.equal(requests[0].url, 'https://example.org/x.txt');
	assert.equal(requests[0].init?.cache, 'no-cache');
});

test('no total with a Content-Encoding, without a length, or once the body outgrows the length', async () => {
	/** @param {Record<string, string>} headers */
	const totals = async headers => {
		const { fetcher } = fetcherWith(async () => streamed([bytes('abc'), bytes('def')], { headers }));
		/** @type {Array<number | undefined>} */
		const seen = [];
		await fetcher.fetchText('u', { onProgress: p => seen.push(p.total) });
		return seen;
	};
	assert.deepEqual(await totals({ 'content-length': '6', 'content-encoding': 'gzip' }), [undefined, undefined, undefined]);
	assert.deepEqual(await totals({}), [undefined, undefined, undefined]);
	assert.deepEqual(await totals({ 'content-length': '4' }), [4, 4, undefined]);
	assert.deepEqual(await totals({ 'content-length': 'x' }), [undefined, undefined, undefined]);
});

test('decoded by the byte-order mark, as a file is', async () => {
	const { fetcher } = fetcherWith(async () => new Response(new Uint8Array([0xff, 0xfe, 0x41, 0, 0x42, 0])));
	assert.equal(await fetcher.fetchText('u', {}), 'AB');
});

test('an HTTP error names its status', async () => {
	const { fetcher } = fetcherWith(async () => new Response('gone', { status: 404, statusText: 'Not Found' }));
	await assert.rejects(fetcher.fetchText('u', {}), { message: 'HTTP 404 Not Found' });
	const bare = fetcherWith(async () => new Response('', { status: 500 }));
	await assert.rejects(bare.fetcher.fetchText('u', {}), { message: 'HTTP 500' });
});

test('a network failure is a network error; an abort is the signal\'s reason', async () => {
	const { fetcher } = fetcherWith(async () => { throw new TypeError('Failed to fetch'); });
	await assert.rejects(fetcher.fetchText('u', {}), { message: 'network error' });

	const controller = new AbortController();
	const aborting = fetcherWith(async (_url, init) => {
		controller.abort(new DOMException('The download was cancelled', 'AbortError'));
		throw init?.signal?.reason;
	});
	await assert.rejects(aborting.fetcher.fetchText('u', { signal: controller.signal }), { name: 'AbortError' });
});

test('a body that fails while read is a network error, or the abort', async () => {
	const failing = () => new Response(new ReadableStream({
		pull(controller) { controller.error(new TypeError('connection reset')); }
	}));
	const { fetcher } = fetcherWith(async () => failing());
	await assert.rejects(fetcher.fetchText('u', {}), { message: 'network error' });

	const controller = new AbortController();
	const aborted = fetcherWith(async () => {
		controller.abort(new DOMException('The download was cancelled', 'AbortError'));
		return failing();
	});
	await assert.rejects(aborted.fetcher.fetchText('u', { signal: controller.signal }), { name: 'AbortError' });
});
