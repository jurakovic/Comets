// The output columns of an ephemeris, in the order the text writes them, with their header parts
// and cells. docs/spec/06-ephemeris.md, Settings and Text output.
import { angle, hms } from '../format/angles.js';
import { compileFormat } from '../format/composite.js';
import { elongationSide } from '../astro/ephemeris.js';

/** @typedef {import('../astro/ephemeris.js').Ephemeris} Ephemeris */

/**
 * @typedef {object} Column
 * @property {string} key
 * @property {string} label as the settings form shows it
 * @property {boolean} on whether a new settings form has it on
 * @property {string} header its part of the header line, spaces significant
 * @property {(ep: Ephemeris) => string} cell its part of a row line
 */

const DEGREES = compileFormat('{0,8:0.0°}');
const ELONGATION = compileFormat('{0,8:0.0°} {1}');
const DISTANCE = compileFormat('{0,9:0.0000}');
const MAGNITUDE = compileFormat('{0,6:0.0}');

/**
 * @param {string} key
 * @param {string} label
 * @param {boolean} on
 * @param {string} header
 * @param {Column['cell']} cell
 * @returns {Column}
 */
const column = (key, label, on, header, cell) => Object.freeze({ key, label, on, header, cell });

/** the columns after the time, in output order */
export const COLUMNS = Object.freeze([
	column('RA', 'Right ascension (RA)', true, '   R.A.   ', ep => `  ${hms(ep.RA / 15)}`),
	column('Dec', 'Declination (Dec)', true, '   Dec   ', ep => `  ${angle(ep.Dec, false, true)}`),
	column('Alt', 'Altitude (Alt)', true, '   Alt  ', ep => DEGREES([ep.Alt])),
	column('Az', 'Azimuth (Az)', true, '   Az   ', ep => DEGREES([ep.Az])),
	column('EcLon', 'Ecliptic Longitude', false, ' Ecl.Lon ', ep => `  ${angle(ep.EcLon, true, true)}`),
	column('EcLat', 'Ecliptic Latitude', false, ' Ecl.Lat ', ep => `  ${angle(ep.EcLat, false, true)}`),
	column('Elongation', 'Elongation', true, '   Elong. ', ep => ELONGATION([ep.Elongation, elongationSide(ep)])),
	column('SunDist', 'Sun distance', true, '    r    ', ep => DISTANCE([ep.SunDist])),
	column('EarthDist', 'Earth distance', true, '    d    ', ep => DISTANCE([ep.EarthDist])),
	column('Magnitude', 'Magnitude', true, ' Mag.', ep => MAGNITUDE([ep.Magnitude]))
]);

/** the time column's header part, for Local Time and for Universal Time */
export const TIME_HEADER = Object.freeze({ local: '     Local Time  ', universal: ' Universal Time  ' });
