// The compute Worker in a real browser: a job's result equals its vector, and a cancelled job
// stops (docs/spec/12-architecture.md, Long computations; spec 11, Conformance plan, level 2).
import { expect, test } from './fixtures.js';

const APP = '/Comets/app/js/';
const VECTORS = '/Comets/docs/spec/vectors/';

test('an ephemeris runs in the Worker and equals its vector', async ({ page }) => {
	await page.goto('./');
	const result = await page.evaluate(async ({ APP, VECTORS }) => {
		const { createWorkerJobRunner } = await import(`${APP}adapters/job-runners.js`);
		const { importElements } = await import(`${APP}core/formats/import.js`);
		const { nearestPerihelion } = await import(`${APP}core/comets/comet.js`);
		const { instantToJd } = await import(`${APP}core/time/julian.js`);
		const sample = importElements([], 'MPC', await (await fetch(`${VECTORS}import/sample.txt`)).text()).comets;
		const comet = sample.find((/** @type {any} */ c) => c.full === 'C/2023 A3 (Tsuchinshan-ATLAS)');
		const columns = { RA: true, Dec: true, Alt: true, Az: true, EcLon: true, EcLat: true, Elongation: true, SunDist: true, EarthDist: true, Magnitude: true };
		const none = { checked: false, value: null };
		const settings = {
			start: Date.UTC(2024, 9, 1), end: Date.UTC(2024, 9, 20), interval: { days: 1, hours: 0, minutes: 0 }, localTime: false, columns,
			requirements: { maxSunDist: none, maxEarthDist: none, minMagnitude: none }
		};
		const input = { comets: [comet], nearest: [nearestPerihelion(comet, instantToJd(Date.UTC(2026, 9, 18, 12)))], settings, observer: { latitude: 0, longitude: 0 }, timeZone: 'UTC' };
		const runner = createWorkerJobRunner(new URL(`${APP}workers/compute.js`, location.origin));
		const text = await runner.run('ephemeris', input, {});
		const expected = await (await fetch(`${VECTORS}ephemeris-text/01-single-ut-all-columns.txt`)).text();
		return { text: text.replace(/\r\n/g, '\n'), expected };
	}, { APP, VECTORS });
	expect(result.text).toBe(result.expected);
});

test('a cancelled job stops in the Worker, which goes on serving', async ({ page }) => {
	await page.goto('./');
	const result = await page.evaluate(async ({ APP }) => {
		const { createComet } = await import(`${APP}core/comets/comet.js`);
		const comet = createComet({ full: '2P/Encke', id: '2P', Ty: 2026, Tm: 10, Td: 22, q: 0.33, e: 0.847, w: 186.8, N: 334.2, i: 11.4, g: 11.5, k: 4 });
		const columns = { RA: true, Dec: false, Alt: false, Az: false, EcLon: false, EcLat: false, Elongation: false, SunDist: false, EarthDist: false, Magnitude: false };
		const none = { checked: false, value: null };
		/** @param {number} days @param {object} minMagnitude */
		const input = (days, minMagnitude) => ({
			comets: [comet], nearest: [comet.T], observer: { latitude: 0, longitude: 0 }, timeZone: 'UTC',
			settings: {
				start: Date.UTC(2026, 0, 1), end: Date.UTC(2026, 0, 1) + days * 86_400_000, interval: { days: 0, hours: 0, minutes: 1 }, localTime: false, columns,
				// no row passes, so twenty years of minutes take time but no memory
				requirements: { maxSunDist: none, maxEarthDist: none, minMagnitude }
			}
		});
		// the protocol by hand, to see every message the Worker sends
		const worker = new Worker(new URL(`${APP}workers/compute.js`, location.origin), { type: 'module' });
		/** @type {any[]} */
		const replies = [];
		worker.addEventListener('message', event => replies.push(event.data));
		const until = (/** @type {(reply: any) => boolean} */ test) => new Promise(resolve => {
			const check = () => replies.some(test) ? resolve(undefined) : setTimeout(check, 10);
			check();
		});
		worker.postMessage({ type: 'run', id: 1, name: 'ephemeris', input: input(20 * 365, { checked: true, value: -20 }) });
		await until(r => r.id === 1 && r.type === 'progress');
		worker.postMessage({ type: 'cancel', id: 1 });
		worker.postMessage({ type: 'run', id: 2, name: 'ephemeris', input: input(1, none) });
		await until(r => r.id === 2 && r.type === 'result');
		const after = replies.length;
		await new Promise(resolve => setTimeout(resolve, 300));
		const late = replies.slice(after).filter(r => r.id === 1);
		worker.terminate();

		// and through the adapter: the run rejects with an AbortError
		const { createWorkerJobRunner } = await import(`${APP}adapters/job-runners.js`);
		const runner = createWorkerJobRunner(new URL(`${APP}workers/compute.js`, location.origin));
		const controller = new AbortController();
		let error = null;
		try {
			await runner.run('ephemeris', input(20 * 365, { checked: true, value: -20 }), { signal: controller.signal, onProgress: () => controller.abort() });
		} catch (caught) {
			error = /** @type {Error} */ (caught).name;
		}
		return {
			short: replies.find(r => r.id === 2 && r.type === 'result')?.value.split('\r\n').length,
			firstResult: replies.some(r => r.id === 1 && r.type === 'result'),
			late: late.length,
			error
		};
	}, { APP });
	// 1441 rows, the heading and the header, three empty lines and the last line break
	expect(result.short).toBe(1441 + 6 + 3 + 1);
	expect(result.firstResult).toBe(false);
	expect(result.late).toBe(0);
	expect(result.error).toBe('AbortError');
});

test('a Worker that does not load rejects its jobs and every later one', async ({ page }) => {
	await page.goto('./');
	const errors = await page.evaluate(async ({ APP }) => {
		const { createWorkerJobRunner } = await import(`${APP}adapters/job-runners.js`);
		const runner = createWorkerJobRunner(new URL(`${APP}workers/missing.js`, location.origin));
		const outcome = (/** @type {Promise<unknown>} */ run) => run.then(() => 'resolved', error => /** @type {Error} */ (error).message);
		const first = await outcome(runner.run('ephemeris', null, {}));
		const second = await outcome(runner.run('ephemeris', null, {}));
		return [first, second];
	}, { APP });
	expect(errors).toEqual(['the compute worker failed: it did not load', 'the compute worker failed: it did not load']);
});
