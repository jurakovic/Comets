import { test } from 'node:test';
import assert from 'node:assert/strict';
import { parseRoute } from '../../../app/js/ui/shell/router.js';
import { DEFAULT_ROUTE, ROUTES } from '../../../app/js/ui/shell/routes.js';

const keys = ['database', 'graph'];

test('a known route with its parameters', () => {
	const route = parseRoute('#/graph?tab=2', keys, 'database');
	assert.equal(route.key, 'graph');
	assert.equal(route.params.get('tab'), '2');
	assert.ok(route.canonical);
});

test('an empty, unknown or malformed hash opens the fallback', () => {
	for (const hash of ['', '#', '#/', '#/nope', '#graph', '#/Graph', '#/graph/x']) {
		const route = parseRoute(hash, keys, 'database');
		assert.equal(route.key, 'database', hash);
		assert.equal(route.canonical, false, hash);
	}
});

test('the route table: unique keys the parser accepts, and the default among them', () => {
	const tableKeys = ROUTES.map(route => route.key);
	assert.equal(new Set(tableKeys).size, tableKeys.length);
	assert.ok(tableKeys.includes(DEFAULT_ROUTE));
	for (const key of tableKeys) assert.equal(parseRoute(`#/${key}`, tableKeys, DEFAULT_ROUTE).key, key);
});
