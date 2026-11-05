// Quick selection in the comet pickers (`GetCometBySelectionType`), and which comet stays selected
// when a picker gets a new list. docs/spec/02-data-model.md, Quick selection.
import { sameComet } from './comet.js';
import { compareNumbers } from './sort.js';

/** @typedef {import('./comet.js').Comet} Comet */
/** @typedef {import('./properties.js').ValueAccessor} ValueAccessor */

/**
 * @typedef {object} QuickSelection
 * @property {string} key
 * @property {string} label as the quick-select menu shows it
 * @property {(comet: Comet, value: ValueAccessor, nowJd: number) => number} measure the smallest wins
 */

/**
 * @param {string} key
 * @param {string} label
 * @param {QuickSelection['measure']} measure
 * @returns {QuickSelection}
 */
const entry = (key, label, measure) => Object.freeze({ key, label, measure });

/** the quick-select menu, in its order */
export const QUICK_SELECTIONS = Object.freeze([
	entry('Brightest', 'Brightest', (comet, value) => Number(value(comet, 'CurrentMag'))),
	entry('ClosestToPerihelion', 'Closest to perihelion', (comet, value, nowJd) => Math.abs(Number(value(comet, 'Tn')) - nowJd)),
	entry('ClosestToEarth', 'Closest to Earth', (comet, value) => Number(value(comet, 'CurrentEarthDist'))),
	entry('ClosestToSun', 'Closest to Sun', (comet, value) => Number(value(comet, 'CurrentSunDist')))
]);

/**
 * the comet with the smallest measure, the first in list order on a tie
 * @param {ReadonlyArray<Comet>} comets
 * @param {string} key a QuickSelection key
 * @param {ValueAccessor} value
 * @param {number} nowJd the JD of now, its milliseconds dropped
 * @returns {Comet | null} null for an empty list
 */
export function quickSelect(comets, key, value, nowJd) {
	const selection = QUICK_SELECTIONS.find(s => s.key === key);
	if (!selection) throw new Error(`unknown quick selection: ${key}`);
	/** @type {Comet | null} */
	let best = null;
	let smallest = 0;
	for (const comet of comets) {
		const measure = selection.measure(comet, value, nowJd);
		// strictly smaller, so the first of equal ones stays; NaN orders first, as in .NET
		if (best === null || compareNumbers(measure, smallest) < 0) {
			best = comet;
			smallest = measure;
		}
	}
	return best;
}

/**
 * the comet a picker selects when it gets a new list: the one selected before when an equal comet
 * is in the list, otherwise the brightest
 * @param {ReadonlyArray<Comet>} comets
 * @param {Comet | null} previous
 * @param {ValueAccessor} value
 * @param {number} nowJd
 * @returns {Comet | null} the list's own record; null for an empty list
 */
export function rebindSelection(comets, previous, value, nowJd) {
	const kept = previous && comets.find(comet => sameComet(comet, previous));
	return kept ?? quickSelect(comets, 'Brightest', value, nowJd);
}
