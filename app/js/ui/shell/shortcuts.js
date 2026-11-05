// The app's commands: each one an id, a label, its keys and an enabled test, registered once, so
// that key handling, the text-field guard and the Help's list are written against the registry.
// docs/spec/01-product.md, Keyboard shortcuts; docs/spec/12-architecture.md, UI.

/**
 * a key that runs a command; `mod` is Ctrl, or ⌘ on a Mac
 * @typedef {object} KeyBinding
 * @property {string} key KeyboardEvent.key: `s`, `Escape`; letters match in either case
 * @property {boolean} [mod]
 * @property {boolean} [shift]
 * @property {boolean} [alt]
 */

/**
 * @typedef {object} Command
 * @property {string} id
 * @property {string} label what it does, as the Help lists it
 * @property {ReadonlyArray<KeyBinding>} keys
 * @property {boolean} [inTextFields] runs while a text field has focus too, as the app-wide ones do
 * @property {() => boolean} enabled
 * @property {() => void} run
 */

/**
 * what key handling reads of a keydown
 * @typedef {object} KeyPress
 * @property {string} key
 * @property {boolean} ctrlKey
 * @property {boolean} metaKey
 * @property {boolean} shiftKey
 * @property {boolean} altKey
 * @property {unknown} target
 */

/**
 * whether a key press is a binding: the key, and exactly its modifiers
 * @param {Readonly<KeyBinding>} binding
 * @param {Readonly<KeyPress>} press
 * @returns {boolean}
 */
export function matchesBinding(binding, press) {
	const key = press.key.length === 1 ? press.key.toLowerCase() : press.key;
	return key === binding.key
		&& (press.ctrlKey || press.metaKey) === Boolean(binding.mod)
		&& press.shiftKey === Boolean(binding.shift)
		&& press.altKey === Boolean(binding.alt);
}

/** key names as the Help shows them */
const KEY_NAMES = Object.freeze(/** @type {Record<string, string>} */ ({ Escape: 'Esc', ArrowUp: '↑', ArrowDown: '↓', ArrowLeft: '←', ArrowRight: '→', ' ': 'Space' }));

/**
 * a binding as the Help shows it: `Ctrl/⌘+S`, `Esc`
 * @param {Readonly<KeyBinding>} binding
 * @returns {string}
 */
export function bindingLabel(binding) {
	const key = KEY_NAMES[binding.key] ?? (binding.key.length === 1 ? binding.key.toUpperCase() : binding.key);
	return [binding.mod ? 'Ctrl/⌘' : '', binding.alt ? 'Alt' : '', binding.shift ? 'Shift' : '', key].filter(Boolean).join('+');
}

/** the input types that take typing; the rest (checkbox, button, range…) do not */
const TEXT_INPUTS = new Set(['text', 'search', 'url', 'email', 'tel', 'password', 'number', 'date', 'datetime-local', 'time', 'month', 'week']);

/**
 * whether keys typed at a target belong to it: a text input, a text area, a menu or an editable element
 * @param {unknown} target
 * @returns {boolean}
 */
export function isTextField(target) {
	if (typeof target !== 'object' || target === null) return false;
	const { tagName, type, isContentEditable } = /** @type {{ tagName?: unknown, type?: unknown, isContentEditable?: unknown }} */ (target);
	if (isContentEditable === true) return true;
	if (tagName === 'TEXTAREA' || tagName === 'SELECT') return true;
	return tagName === 'INPUT' && TEXT_INPUTS.has(String(type).toLowerCase());
}

/**
 * @typedef {object} CommandRegistry
 * @property {(command: Command) => () => void} register adds a command; returns its removal
 * @property {() => ReadonlyArray<Command>} list in registration order
 * @property {(press: Readonly<KeyPress>) => Command | null} commandFor the enabled command a key press runs
 */

/**
 * @returns {CommandRegistry}
 */
export function createCommandRegistry() {
	/** @type {Map<string, Command>} */
	const commands = new Map();
	return Object.freeze({
		register(/** @type {Command} */ command) {
			if (commands.has(command.id)) throw new Error(`the command ${command.id} is already registered`);
			commands.set(command.id, Object.freeze({ ...command, keys: Object.freeze(command.keys.map(k => Object.freeze({ ...k }))) }));
			return () => { commands.delete(command.id); };
		},
		list: () => Object.freeze([...commands.values()]),
		commandFor(/** @type {Readonly<KeyPress>} */ press) {
			const typing = isTextField(press.target);
			for (const command of commands.values()) {
				if (typing && !command.inTextFields) continue;
				if (command.keys.some(binding => matchesBinding(binding, press)) && command.enabled()) return command;
			}
			return null;
		}
	});
}

/**
 * runs the command a keydown is for, instead of what the browser would do with it
 * @param {CommandRegistry} registry
 * @param {Document} [target]
 */
export function listenForShortcuts(registry, target = document) {
	target.addEventListener('keydown', event => {
		if (event.defaultPrevented || event.isComposing) return;
		const command = registry.commandFor(event);
		if (!command) return;
		event.preventDefault();
		command.run();
	});
}
