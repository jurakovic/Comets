// The views and their routes (docs/spec/01-product.md, Views). The router and
// the navigation read this table; a new view is a new entry.

/** @typedef {import('./router.js').View} View */

/**
 * @typedef {object} Route
 * @property {string} key the route is #/<key>
 * @property {string} title the navigation label and the page title
 * @property {'views' | 'app'} group the views, or the app's own pages beside them in the navigation
 * @property {(context: import('./context.js').AppContext) => Promise<View>} load creates the view on its first visit
 */

/** the route an empty or unknown hash opens */
export const DEFAULT_ROUTE = 'database';

/** @type {readonly Route[]} */
export const ROUTES = Object.freeze(/** @type {Route[]} */ ([
	{ key: 'database', title: 'Database', group: 'views', load: () => placeholder('Database', 'database') },
	{ key: 'ephemeris', title: 'Ephemeris', group: 'views', load: () => placeholder('Ephemeris', 'ephemeris') },
	{ key: 'graph', title: 'Graph', group: 'views', load: () => placeholder('Graph', 'graph') },
	{ key: 'orbit', title: 'Orbit viewer', group: 'views', load: () => placeholder('Orbit Viewer', 'orbit') },
	{ key: 'elements', title: 'Elements', group: 'views', load: () => placeholder('Orbital Elements', 'elements') },
	{ key: 'update', title: 'Update', group: 'app', load: () => placeholder('Update', 'update') },
	{ key: 'settings', title: 'Settings', group: 'app', load: () => placeholder('Settings', 'settings') },
	{ key: 'help', title: 'Help', group: 'app', load: async context => (await import('../views/help/help-view.js')).createHelpView(context) }
]).map(route => Object.freeze(route)));

/**
 * a view still to be built
 * @param {string} title
 * @param {string} key
 * @returns {Promise<View>}
 */
async function placeholder(title, key) {
	const { createPlaceholderView } = await import('../views/placeholder-view.js');
	return createPlaceholderView(title, key);
}
