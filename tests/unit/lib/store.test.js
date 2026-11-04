import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createStore, readonly } from '../../../app/js/lib/store.js';

test('a store gives its value and tells listeners of changes', () => {
	const store = createStore(1);
	const heard = [];
	store.subscribe(value => heard.push(value));
	store.set(2);
	store.update(value => value * 10);
	assert.equal(store.get(), 20);
	assert.deepEqual(heard, [2, 20]);
});

test('setting the same value tells no one', () => {
	const list = [1];
	const store = createStore(list);
	let calls = 0;
	store.subscribe(() => calls++);
	store.set(list);
	assert.equal(calls, 0);
});

test('an unsubscribed listener hears nothing more, even when it leaves during a change', () => {
	const store = createStore(0);
	const heard = [];
	const stop = store.subscribe(value => { heard.push(`a${value}`); stop(); });
	store.subscribe(value => heard.push(`b${value}`));
	store.set(1);
	store.set(2);
	assert.deepEqual(heard, ['a1', 'b1', 'b2']);
});

test('the read side cannot change the value', () => {
	const view = readonly(createStore('x'));
	assert.deepEqual(Object.keys(view).sort(), ['get', 'subscribe']);
	assert.ok(Object.isFrozen(view));
});
