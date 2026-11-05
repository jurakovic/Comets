// Hash routes to views. A view is created on its first visit and kept, so it
// keeps its state for the session. See docs/spec/12-architecture.md, UI.
import { createStore, readonly } from '../../lib/store.js';

/** @typedef {import('./routes.js').Route} Route */

/**
 * @typedef {object} View
 * @property {HTMLElement} element
 * @property {(params: URLSearchParams) => void} show runs each time the view becomes the current one
 * @property {() => void} hide runs when another view takes its place: clocks and animation stop
 * @property {() => void} [save] what Ctrl/⌘+S runs while the view is shown
 */

/**
 * the route a location hash names (`#/graph`, `#/graph?tab=2`); any other
 * hash names the fallback and is not canonical
 * @param {string} hash
 * @param {readonly string[]} keys
 * @param {string} fallback
 * @returns {{ key: string, params: URLSearchParams, canonical: boolean }}
 */
export function parseRoute(hash, keys, fallback) {
	const match = /^#\/([a-z]+)(?:\?(.*))?$/.exec(hash);
	if (match && keys.includes(match[1])) {
		return { key: match[1], params: new URLSearchParams(match[2] ?? ''), canonical: true };
	}
	return { key: fallback, params: new URLSearchParams(), canonical: false };
}

/**
 * @param {object} options
 * @param {readonly Route[]} options.routes
 * @param {string} options.fallback the route an empty or unknown hash opens
 * @param {HTMLElement} options.outlet where the views' elements go
 * @param {import('./context.js').AppContext} options.context what a view is created with
 */
export function createRouter({ routes, fallback, outlet, context }) {
	const keys = routes.map(route => route.key);
	/** @type {Map<string, Promise<View>>} */
	const views = new Map();
	const current = createStore(/** @type {string | null} */ (null));
	/** @type {View | null} */
	let shown = null;
	let navigation = 0;

	/** @param {Route} route */
	function viewOf(route) {
		let view = views.get(route.key);
		if (!view) {
			// a failed load is not kept, so the next visit tries again
			view = route.load(context).catch(error => {
				views.delete(route.key);
				throw error;
			});
			views.set(route.key, view);
		}
		return view;
	}

	async function go() {
		const { key, params, canonical } = parseRoute(location.hash, keys, fallback);
		if (!canonical) history.replaceState(null, '', `#/${key}`);
		const route = routes.find(candidate => candidate.key === key);
		if (!route) throw new Error(`no route ${key}`);
		const ticket = ++navigation;
		const view = await viewOf(route);
		// a later navigation finished first
		if (ticket !== navigation) return;
		if (shown !== view) {
			if (shown) {
				shown.hide();
				shown.element.hidden = true;
			}
			if (!view.element.isConnected) outlet.append(view.element);
			view.element.hidden = false;
			shown = view;
		}
		view.show(params);
		document.title = `${route.title} - Comets`;
		current.set(key);
	}

	return Object.freeze({
		/** the key of the route shown */
		current: readonly(current),
		/** the view shown, for Ctrl/⌘+S */
		shown: () => shown,
		/** follows the hash from now on, and shows the route it names */
		start() {
			addEventListener('hashchange', () => { go(); });
			return go();
		}
	});
}
