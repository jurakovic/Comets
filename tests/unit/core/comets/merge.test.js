import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createComet } from '../../../../app/js/core/comets/comet.js';
import { countResults, mergeComets } from '../../../../app/js/core/comets/merge.js';

/**
 * @param {string} full
 * @param {string} id
 * @param {number} [q]
 * @param {string} [sortkey]
 */
function comet(full, id, q = 1, sortkey) {
	const made = createComet({ full, id, Ty: 2026, Tm: 1, Td: 1, q, e: 0.5 }, sortkey === undefined ? {} : { sortkey });
	assert.ok(made);
	return made;
}

/** @param {import('../../../../app/js/core/comets/merge.js').Merge} merge */
const rows = merge => merge.comets.map((c, index) => `${c.full} ${c.q} ${merge.results[index]}`);

test('into an empty database every comet is New, in sort key order', () => {
	const merge = mergeComets([], [comet('2P/Encke', '2P'), comet('1P/Halley', '1P')]);
	assert.deepEqual(rows(merge), ['1P/Halley 1 New', '2P/Encke 1 New']);
	assert.ok(Object.isFrozen(merge) && Object.isFrozen(merge.comets) && Object.isFrozen(merge.results));
});

test('a changed comet is an Update, an equal one NoChanges, one the file lacks stays', () => {
	const database = [comet('1P/Halley', '1P'), comet('2P/Encke', '2P'), comet('3D/Biela', '3D')];
	const merge = mergeComets(database, [comet('2P/Encke', '2P', 0.3387), comet('1P/Halley', '1P'), comet('4P/Faye', '4P')]);
	assert.deepEqual(rows(merge), ['1P/Halley 1 NoChanges', '2P/Encke 0.3387 Update', '3D/Biela 1 NoChanges', '4P/Faye 1 New']);
	assert.deepEqual({ ...countResults(merge) }, { New: 1, Update: 1, NoChanges: 2, total: 4 });
});

test('a file with no comets leaves the database as it is', () => {
	const database = [comet('2P/Encke', '2P'), comet('1P/Halley', '1P')];
	assert.deepEqual(rows(mergeComets(database, [])), ['1P/Halley 1 NoChanges', '2P/Encke 1 NoChanges']);
	assert.deepEqual(rows(mergeComets([], [])), []);
});

test('equal sort keys keep their order: the sort is stable', () => {
	const voyager = ['Halley', 'Encke', 'Biela'].map(name => comet(name, '', 1, ''));
	const merge = mergeComets([comet('1P/Halley', '1P')], voyager, true);
	assert.deepEqual(rows(merge), ['Halley 1 New', 'Encke 1 New', 'Biela 1 New', '1P/Halley 1 NoChanges']);
});

test('duplicates: each file comet replaces the first old one of its name; Voyager II keeps them all', () => {
	const old = [comet('Halley', '', 1, ''), comet('Halley', '', 2, '')];
	// without duplicates the second old Halley is still there, so the file's is not added
	assert.deepEqual(rows(mergeComets(old, [comet('Halley', '', 3, '')])), ['Halley 2 NoChanges']);
	assert.deepEqual(rows(mergeComets(old, [comet('Halley', '', 3, '')], true)), ['Halley 2 NoChanges', 'Halley 3 Update']);
	assert.deepEqual(rows(mergeComets(old, [comet('Halley', '', 1, ''), comet('Halley', '', 2, '')], true)), ['Halley 1 NoChanges', 'Halley 2 NoChanges']);
});
