// The observer's latitude and longitude as the Settings view edits them (`FormSettings`): the
// size in a number field, and the hemisphere in a selector beside it.
import { formatNumber } from '../format/number.js';
import { numberOf } from './number.js';

/**
 * @typedef {object} Coordinate
 * @property {string} label
 * @property {Readonly<import('./number.js').NumberRange>} range the size's field
 * @property {readonly [string, string]} hemispheres the positive one, then the negative one
 */

/** @type {Readonly<Coordinate>} */
export const LATITUDE = Object.freeze({
	label: 'Latitude', range: Object.freeze({ min: 0, max: 90, decimals: 6 }), hemispheres: /** @type {const} */ (['North', 'South'])
});

/** @type {Readonly<Coordinate>} */
export const LONGITUDE = Object.freeze({
	label: 'Longitude', range: Object.freeze({ min: 0, max: 180, decimals: 6 }), hemispheres: /** @type {const} */ (['East', 'West'])
});

/**
 * a coordinate as its fields show it: the size as `0.000000`, and whether it is south or west;
 * 0 is north and east (`FormSettings_Load`)
 * @param {number} value degrees, south and west negative
 * @returns {Readonly<{ text: string, negative: boolean }>}
 */
export function coordinateFields(value) {
	return Object.freeze({ text: formatNumber(Math.abs(value), '0.000000'), negative: value < 0 });
}

/**
 * the coordinate its fields make: the size as the field reads it, 0 while it is empty (the
 * desktop's `Double()`), negative for south and west
 * @param {string} text the size's field
 * @param {boolean} negative south or west
 * @returns {number} degrees, never −0
 */
export function coordinateValue(text, negative) {
	const size = numberOf(text) ?? 0;
	return negative && size !== 0 ? -size : size;
}
