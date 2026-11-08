// A comet's details: the Ephemeris tab (first) with its live block, and the Orbital elements tab
// with JPL Info. What the rows say is core/comets/details.js.
// docs/spec/05-database-and-update.md, Details.
import { elementsRows, jplUrl, perihelionRows } from '../../../core/comets/details.js';
import { el } from '../../dom.js';
import { createLiveEphemeris } from './live-ephemeris.js';

/** @typedef {import('../../../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../../../core/comets/details.js').DetailRow} DetailRow */

/**
 * @typedef {object} CometDetails
 * @property {HTMLElement} element
 * @property {(comet: Comet | null) => void} show the comet's details; none for null
 * @property {() => void} switchTab the other tab, as a double click on the list does
 * @property {() => void} start the owner is shown: the live clock ticks
 * @property {() => void} stop the owner is hidden: the live clock stops
 */

let ids = 0;

/**
 * rows as a description list: label and symbol, then value and unit
 * @param {HTMLElement} list
 * @param {ReadonlyArray<DetailRow>} rows
 */
export function showRows(list, rows) {
	list.replaceChildren(...rows.flatMap(({ key, label, symbol, value, unit }) => [
		el('dt', { class: 'comet-details__label', 'data-key': key }, label, symbol ? el('span', { class: 'comet-details__symbol' }, symbol) : ''),
		el('dd', { class: 'comet-details__value', 'data-key': key }, el('span', { class: 'comet-details__number' }, value), unit && value ? el('span', { class: 'comet-details__unit' }, unit) : '')
	]));
}

/**
 * @param {object} options
 * @param {import('../../../core/comets/properties.js').ValueAccessor} options.value the session
 *   values (`Tn`, `Perih*`) for the Ephemeris tab
 * @param {import('../../../services/ports.js').Clock} options.clock for the live block
 * @param {() => import('../../../core/astro/ephemeris.js').Observer} options.observer the location
 * @returns {CometDetails}
 */
export function createCometDetails({ value, clock, observer }) {
	const prefix = `comet-details-${++ids}`;
	const perihelion = el('dl', { class: 'comet-details__rows' });
	const elements = el('dl', { class: 'comet-details__rows' });
	const jpl = el('a', { class: 'button comet-details__jpl', href: '', target: '_blank', rel: 'noopener' }, 'JPL Info');
	const live = createLiveEphemeris({ clock, observer });

	const tabs = [
		{ name: 'Ephemeris', panel: el('div', {}, perihelion, live.element) },
		{ name: 'Orbital elements', panel: el('div', {}, elements, el('p', { class: 'comet-details__actions' }, jpl)) }
	].map(({ name, panel }, index) => {
		const tab = el('button', {
			type: 'button', role: 'tab', id: `${prefix}-tab-${index}`, class: 'comet-details__tab',
			'aria-controls': `${prefix}-panel-${index}`, 'aria-selected': 'false', tabindex: '-1'
		}, name);
		panel.id = `${prefix}-panel-${index}`;
		panel.className = 'comet-details__panel';
		panel.setAttribute('role', 'tabpanel');
		panel.setAttribute('aria-labelledby', tab.id);
		return { tab, panel };
	});
	const tablist = el('div', { class: 'comet-details__tabs', role: 'tablist', 'aria-label': 'Details' }, ...tabs.map(t => t.tab));
	const element = el('div', { class: 'comet-details' }, tablist, ...tabs.map(t => t.panel));
	let active = 0;

	/** @param {number} index */
	function choose(index) {
		active = (index + tabs.length) % tabs.length;
		tabs.forEach(({ tab, panel }, i) => {
			tab.setAttribute('aria-selected', String(i === active));
			tab.tabIndex = i === active ? 0 : -1;
			panel.hidden = i !== active;
		});
	}

	tabs.forEach(({ tab }, index) => tab.addEventListener('click', () => choose(index)));
	tablist.addEventListener('keydown', event => {
		/** @type {Record<string, () => number>} */
		const moves = { ArrowRight: () => active + 1, ArrowLeft: () => active - 1, Home: () => 0, End: () => tabs.length - 1 };
		const move = moves[event.key];
		if (!move) return;
		event.preventDefault();
		choose(move());
		tabs[active].tab.focus();
	});
	choose(0);

	return Object.freeze({
		element,
		show(/** @type {Comet | null} */ comet) {
			element.classList.toggle('comet-details--empty', !comet);
			if (!comet) {
				perihelion.replaceChildren();
				elements.replaceChildren();
				jpl.hidden = true;
				live.show(null, null);
				return;
			}
			const Tn = Number(value(comet, 'Tn'));
			live.show(comet, Tn);
			showRows(perihelion, perihelionRows(comet, {
				Tn,
				PerihEarthDist: Number(value(comet, 'PerihEarthDist')),
				PerihMag: Number(value(comet, 'PerihMag'))
			}));
			showRows(elements, elementsRows(comet));
			jpl.hidden = false;
			jpl.href = jplUrl(comet);
		},
		switchTab: () => choose(active + 1),
		start: live.start,
		stop: live.stop
	});
}
