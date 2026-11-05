// The Help view: the controls, listed from the command registries, and About.
// docs/spec/01-product.md, Help.
import { bindingLabel } from '../../shell/shortcuts.js';
import { el } from '../../dom.js';
import { APP_VERSION, COPYRIGHT, CREDITS, LICENCE, REFERENCES, REPOSITORY } from './about.js';

/** @param {import('./about.js').Line} line */
const lineOf = line => line.map(part => typeof part === 'string'
	? part
	: el('a', { href: part[1], target: '_blank', rel: 'noopener' }, part[0]));

/**
 * the commands of a registry as rows of keys and actions
 * @param {ReadonlyArray<import('../../shell/shortcuts.js').Command>} commands
 */
function controlsTable(commands) {
	return el('table', { class: 'help-view__controls' },
		el('thead', {}, el('tr', {}, el('th', { scope: 'col' }, 'Keys'), el('th', { scope: 'col' }, 'Action'))),
		el('tbody', {}, ...commands.filter(c => c.keys.length > 0).map(command => el('tr', {},
			el('td', {}, ...command.keys.flatMap((binding, index) => [index > 0 ? ', ' : '', el('kbd', {}, bindingLabel(binding))])),
			el('td', {}, command.label)))));
}

/**
 * @param {import('../../shell/context.js').AppContext} context
 * @returns {import('../../shell/router.js').View}
 */
export function createHelpView({ commands }) {
	const controls = el('div', {});
	const element = el('section', { class: 'help-view', 'data-view': 'help', 'aria-labelledby': 'view-title-help' },
		el('h1', { id: 'view-title-help' }, 'Help'),
		el('h2', {}, 'Controls'),
		el('h3', {}, 'Everywhere'),
		controls,
		el('h2', {}, 'About'),
		el('p', { class: 'help-view__version' }, `Comets ${APP_VERSION}`),
		el('p', {}, COPYRIGHT),
		el('p', {}, el('a', { href: REPOSITORY, target: '_blank', rel: 'noopener' }, REPOSITORY)),
		el('h3', {}, 'Credits'),
		...CREDITS.map(line => el('p', {}, ...lineOf(line))),
		el('h3', {}, 'References'),
		...REFERENCES.flatMap(({ title, lines }) => [el('h4', {}, title), el('ul', {}, ...lines.map(line => el('li', {}, ...lineOf(line))))]),
		el('h3', {}, 'Licence'),
		el('p', {}, LICENCE));

	return {
		element,
		// the registry can change while the app runs, so the list is built afresh
		show() { controls.replaceChildren(controlsTable(commands.list())); },
		hide() {}
	};
}
