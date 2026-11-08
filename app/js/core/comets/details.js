// What the Database view's details show of a comet: the Orbital elements tab, the Ephemeris tab's
// perihelion values and live block with its trends, and the JPL link.
// docs/spec/05-database-and-update.md, Details.
import { elongationSide } from '../astro/ephemeris.js';
import { angle, hms } from '../format/angles.js';
import { DATE_FULL, formatDate } from '../format/dates.js';
import { formatNumber } from '../format/number.js';
import { jdToParts } from '../time/julian.js';

/** @typedef {import('./comet.js').Comet} Comet */

/**
 * @typedef {object} DetailRow
 * @property {string} key
 * @property {string} label
 * @property {string} symbol the element's letter (`P`, `Ω`); "" for none
 * @property {string} value "" where it does not apply
 * @property {string} unit "" for none
 */

/** P, Q, a and n are shown only below this period, in years */
const MAX_PERIOD = 1000;

/**
 * @param {string} key
 * @param {string} label
 * @param {string} symbol
 * @param {string} value
 * @param {string} [unit]
 * @returns {DetailRow}
 */
const row = (key, label, symbol, value, unit = '') => Object.freeze({ key, label, symbol, value, unit });

/** @param {number} x */
const six = x => formatNumber(x, '0.000000');
/** @param {number} x */
const four = x => formatNumber(x, '0.0000');
/** @param {number} n @param {number} width */
const padded = (n, width) => String(n).padStart(width, '0');

/**
 * the Orbital elements tab (`ElementsControl.DataBind`)
 * @param {Comet} comet
 * @returns {ReadonlyArray<DetailRow>}
 */
export function elementsRows(comet) {
	const periodic = comet.P < MAX_PERIOD;
	/** @param {string} text */
	const ifPeriodic = text => (periodic ? text : '');
	return Object.freeze([
		row('full', 'Name', '', comet.full),
		row('T', 'Elements perihelion date', 'T', `${comet.Ty}-${padded(comet.Tm, 2)}-${padded(comet.Td, 2)}.${padded(comet.Th, 4)}`),
		row('P', 'Period', 'P', ifPeriodic(six(comet.P)), 'years'),
		row('n', 'Mean motion', 'n', ifPeriodic(six(comet.n)), '°/day'),
		row('q', 'Perihelion distance', 'q', six(comet.q), 'AU'),
		row('Q', 'Aphelion distance', 'Q', ifPeriodic(six(comet.Q)), 'AU'),
		row('a', 'Semi-major axis', 'a', ifPeriodic(six(comet.a)), 'AU'),
		row('e', 'Eccentricity', 'e', six(comet.e)),
		row('N', 'Ascending node', 'Ω', four(comet.N), '°'),
		row('i', 'Inclination', 'i', four(comet.i), '°'),
		row('w', 'Argument of pericenter', 'ω', four(comet.w), '°'),
		row('gk', 'Magnitude parameters', 'g, k', `${formatNumber(comet.g, '0.0')}, ${formatNumber(comet.k, '0.0')}`),
		row('equinox', 'Equinox', '', '2000.0')
	]);
}

/**
 * the session values the Ephemeris tab shows at perihelion
 * @typedef {object} PerihelionValues
 * @property {number} Tn the nearest perihelion's JD
 * @property {number} PerihEarthDist
 * @property {number} PerihMag
 */

/**
 * the Ephemeris tab above the live block (`EphemerisControl.DataBind`)
 * @param {Comet} comet
 * @param {Readonly<PerihelionValues>} values
 * @returns {ReadonlyArray<DetailRow>}
 */
export function perihelionRows(comet, { Tn, PerihEarthDist, PerihMag }) {
	const periodic = comet.P < MAX_PERIOD;
	return Object.freeze([
		row('full', 'Name', '', comet.full),
		row('Tn', 'Perihelion date', '', formatDate(jdToParts(Tn), DATE_FULL)),
		row('P', 'Period', '', periodic ? six(comet.P) : '', 'years'),
		row('Q', 'Aphelion distance', '', periodic ? six(comet.Q) : '', 'AU'),
		row('q', 'Perihelion distance', '', six(comet.q), 'AU'),
		row('PerihEarthDist', 'Perihelion distance from Earth', '', six(PerihEarthDist), 'AU'),
		row('PerihMag', 'Perihelion magnitude', '', formatNumber(PerihMag, '0.00'))
	]);
}

/**
 * JPL's small-body lookup for the comet: its id, without the fragment, URL-encoded (spec 10, D16)
 * @param {Comet} comet
 * @returns {string}
 */
export function jplUrl(comet) {
	return `https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=${encodeURIComponent(comet.id)}`;
}

/** @typedef {import('../astro/ephemeris.js').Ephemeris} Ephemeris */

/**
 * how a change reads: better, worse, or neither (RA, azimuth)
 * @typedef {'better' | 'worse' | 'neutral'} TrendTone
 */

/**
 * a live value with its trend since the previous computation: ▲ for a value at least the
 * previous one, ▼ for a smaller one
 * @typedef {DetailRow & { trend: Readonly<{ arrow: '▲' | '▼', tone: TrendTone }> }} LiveRow
 */

/**
 * @typedef {object} LiveValue
 * @property {keyof Ephemeris} key the ephemeris value the trend compares
 * @property {string} label
 * @property {(record: Ephemeris) => string} format
 * @property {string} unit
 * @property {(up: boolean, latitude: number) => TrendTone} tone
 */

/** @type {LiveValue['tone']} a lower value is better: distances and magnitude */
const lowerIsBetter = up => (up ? 'worse' : 'better');
/** @type {LiveValue['tone']} */
const higherIsBetter = up => (up ? 'better' : 'worse');
/** @type {LiveValue['tone']} */
const neutral = () => 'neutral';

/**
 * the live block's values in order, with the colours of their trends (`EphemerisControl`): a
 * declination rising is better in the north, falling in the south, where it stands higher
 * @type {ReadonlyArray<LiveValue>}
 */
export const LIVE_VALUES = Object.freeze(/** @type {LiveValue[]} */ ([
	{ key: 'SunDist', label: 'Current distance from Sun', format: r => six(r.SunDist), unit: 'AU', tone: lowerIsBetter },
	{ key: 'EarthDist', label: 'Current distance from Earth', format: r => six(r.EarthDist), unit: 'AU', tone: lowerIsBetter },
	{ key: 'Magnitude', label: 'Current magnitude', format: r => formatNumber(r.Magnitude, '0.00'), unit: '', tone: lowerIsBetter },
	{ key: 'RA', label: 'Right Ascension', format: r => hms(r.RA / 15), unit: '', tone: neutral },
	{ key: 'Dec', label: 'Declination', format: r => angle(r.Dec, false, true), unit: '', tone: (up, latitude) => (up === (latitude >= 0) ? 'better' : 'worse') },
	{ key: 'Alt', label: 'Altitude', format: r => `${formatNumber(r.Alt, '0.00')}°`, unit: '', tone: higherIsBetter },
	{ key: 'Az', label: 'Azimuth', format: r => `${formatNumber(r.Az, '0.00')}°`, unit: '', tone: neutral },
	{ key: 'Elongation', label: 'Elongation', format: r => `${formatNumber(r.Elongation, '0.00')}° ${elongationSide(r)}`, unit: '', tone: higherIsBetter }
]).map(entry => Object.freeze(entry)));

/**
 * the live block at an instant, each value against the previous computation
 * @param {Ephemeris} record at the instant shown
 * @param {Ephemeris} previous at the previous tick, or 1 s before on the first computation
 * @param {number} latitude the observer's, for the declination's colour
 * @returns {ReadonlyArray<LiveRow>}
 */
export function liveRows(record, previous, latitude) {
	return Object.freeze(LIVE_VALUES.map(({ key, label, format, unit, tone }) => {
		const up = record[key] >= previous[key];
		return Object.freeze({
			...row(key, label, '', format(record), unit),
			trend: Object.freeze({ arrow: up ? /** @type {const} */ ('▲') : /** @type {const} */ ('▼'), tone: tone(up, latitude) })
		});
	}));
}
