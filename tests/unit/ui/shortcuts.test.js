import { test } from 'node:test';
import assert from 'node:assert/strict';
import { bindingLabel, createCommandRegistry, isTextField, matchesBinding } from '../../../app/js/ui/shell/shortcuts.js';

/**
 * a keydown as key handling reads it
 * @param {string} key
 * @param {{ ctrl?: boolean, meta?: boolean, shift?: boolean, alt?: boolean, target?: unknown }} [options]
 */
const press = (key, { ctrl = false, meta = false, shift = false, alt = false, target = null } = {}) =>
	({ key, ctrlKey: ctrl, metaKey: meta, shiftKey: shift, altKey: alt, target });

const SAVE = { key: 's', mod: true };

test('a binding matches its key with exactly its modifiers; Ctrl and ⌘ are the same', () => {
	assert.equal(matchesBinding(SAVE, press('s', { ctrl: true })), true);
	assert.equal(matchesBinding(SAVE, press('s', { meta: true })), true);
	assert.equal(matchesBinding(SAVE, press('S', { ctrl: true })), true, 'caps lock');
	assert.equal(matchesBinding(SAVE, press('s')), false);
	assert.equal(matchesBinding(SAVE, press('s', { ctrl: true, shift: true })), false);
	assert.equal(matchesBinding(SAVE, press('s', { ctrl: true, alt: true })), false);
	assert.equal(matchesBinding({ key: 'Escape' }, press('Escape')), true);
	assert.equal(matchesBinding({ key: 'Escape' }, press('Escape', { ctrl: true })), false);
	assert.equal(matchesBinding({ key: 'S', shift: true }, press('S', { shift: true })), false, 'letters are given in lower case');
});

test('bindings as the Help shows them', () => {
	assert.equal(bindingLabel(SAVE), 'Ctrl/⌘+S');
	assert.equal(bindingLabel({ key: 'Escape' }), 'Esc');
	assert.equal(bindingLabel({ key: 'ArrowUp' }), '↑');
	assert.equal(bindingLabel({ key: 'f', shift: true, alt: true }), 'Alt+Shift+F');
	assert.equal(bindingLabel({ key: 'F1' }), 'F1');
});

test('text fields: inputs that take typing, text areas, menus and editable elements', () => {
	for (const type of ['text', 'search', 'number', 'url', 'TEXT']) assert.equal(isTextField({ tagName: 'INPUT', type }), true, type);
	for (const type of ['checkbox', 'radio', 'button', 'range', 'file']) assert.equal(isTextField({ tagName: 'INPUT', type }), false, type);
	assert.equal(isTextField({ tagName: 'TEXTAREA' }), true);
	assert.equal(isTextField({ tagName: 'SELECT' }), true);
	assert.equal(isTextField({ tagName: 'DIV', isContentEditable: true }), true);
	assert.equal(isTextField({ tagName: 'BUTTON' }), false);
	assert.equal(isTextField(null), false);
	assert.equal(isTextField(undefined), false);
});

test('the registry finds the enabled command a key press runs, skipping view commands while typing', () => {
	const registry = createCommandRegistry();
	let canSave = true;
	const ran = [];
	registry.register({ id: 'save', label: 'Save', keys: [SAVE], inTextFields: true, enabled: () => canSave, run: () => ran.push('save') });
	const removeNext = registry.register({ id: 'next', label: 'Next', keys: [{ key: 'n' }], enabled: () => true, run: () => ran.push('next') });
	const field = { tagName: 'INPUT', type: 'text' };
	assert.equal(registry.commandFor(press('s', { ctrl: true }))?.id, 'save');
	assert.equal(registry.commandFor(press('s', { ctrl: true, target: field }))?.id, 'save');
	assert.equal(registry.commandFor(press('n'))?.id, 'next');
	assert.equal(registry.commandFor(press('n', { target: field })), null);
	canSave = false;
	assert.equal(registry.commandFor(press('s', { ctrl: true })), null);
	removeNext();
	assert.equal(registry.commandFor(press('n')), null);
	assert.deepEqual(registry.list().map(c => c.id), ['save']);
	assert.deepEqual(ran, []);
});

test('an id is registered once, and a registered command is frozen', () => {
	const registry = createCommandRegistry();
	registry.register({ id: 'save', label: 'Save', keys: [SAVE], enabled: () => true, run: () => {} });
	assert.throws(() => registry.register({ id: 'save', label: 'Again', keys: [], enabled: () => true, run: () => {} }), /already registered/);
	const [command] = registry.list();
	assert.ok(Object.isFrozen(command) && Object.isFrozen(command.keys) && Object.isFrozen(command.keys[0]));
});
