import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createQueue } from '../../../app/js/lib/queue.js';

test('tasks run one at a time, in order', async () => {
	const queue = createQueue();
	/** @type {string[]} */
	const log = [];
	/** @type {Array<() => void>} */
	const finish = [];
	const task = (/** @type {string} */ name) => () => new Promise(resolve => {
		log.push(`start ${name}`);
		finish.push(() => { log.push(`end ${name}`); resolve(); });
	});
	const a = queue(task('a'));
	const b = queue(task('b'));
	await new Promise(resolve => setTimeout(resolve, 0));
	assert.deepEqual(log, ['start a']);
	finish.shift()?.();
	await a;
	await new Promise(resolve => setTimeout(resolve, 0));
	finish.shift()?.();
	await b;
	assert.deepEqual(log, ['start a', 'end a', 'start b', 'end b']);
});

test('a task that fails rejects its own promise and not the later tasks', async () => {
	const queue = createQueue();
	/** @type {string[]} */
	const log = [];
	const failing = queue(async () => { throw new Error('broken'); });
	const next = queue(async () => { log.push('next'); });
	await assert.rejects(failing, /broken/);
	await next;
	assert.deepEqual(log, ['next']);
});
