// The sort menu (`SortMenuControl`): the sort properties, then Ascending and Descending, the
// current choice marked. docs/spec/02-data-model.md, Sorting.
import { chooseSort, SORT_PROPERTIES } from '../../core/comets/sort.js';
import { createMenu } from './menu.js';

/** @typedef {import('../../core/comets/sort.js').Sort} Sort */

/**
 * @param {object} options
 * @param {() => Readonly<Sort>} options.sort the current sort, read as the menu opens
 * @param {(sort: Readonly<Sort>) => void} options.onSort a choice that sorts
 * @param {string} [options.label]
 * @returns {import('./menu.js').Menu}
 */
export function createSortMenu({ sort, onSort, label = 'Sort by' }) {
	/** @param {Parameters<typeof chooseSort>[1]} choice */
	const choose = choice => {
		const next = chooseSort(sort(), choice);
		if (next) onSort(next);
	};
	return createMenu({
		label,
		name: label,
		className: 'sort-menu',
		entries() {
			const { property, ascending } = sort();
			return [
				...SORT_PROPERTIES.map(p => ({ label: p.label, checked: p.key === property, run: () => choose({ property: p.key }) })),
				{ separator: /** @type {const} */ (true) },
				{ label: 'Ascending', checked: ascending, run: () => choose({ ascending: true }) },
				{ label: 'Descending', checked: !ascending, run: () => choose({ ascending: false }) }
			];
		}
	});
}
