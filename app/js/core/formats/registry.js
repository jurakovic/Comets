// The element formats, in the desktop's order, each with what reads and writes it.
// docs/spec/03-element-formats.md.
import * as mpc from './mpc.js';

/**
 * @typedef {import('../comets/comet.js').Comet} Comet
 */

/**
 * how a format's records are read
 * @typedef {object} Reader
 * @property {(lines: ReadonlyArray<string>) => boolean} [header] whether the file starts with the
 *   format's own header, which detection checks before any record
 * @property {number} first the line the first record starts at
 * @property {number} step lines from the start of one record to the next
 * @property {number} size the lines of a record; detection tries the last `size` lines
 * @property {(record: ReadonlyArray<string>) => Comet} parse the record parser; throws
 *   InvalidRecord (core/formats/text.js) for an invalid record, which is skipped
 * @property {boolean} [duplicates] keeps a record whose full name came before (Voyager II)
 */

/**
 * the current UTC date, which some formats write
 * @typedef {object} ExportDay
 * @property {number} year
 * @property {number} month
 * @property {number} day
 * @property {number} jd the JD of its 00:00 UTC
 */

/**
 * how a format is written: the header, then each comet's lines, in collection order
 * @typedef {object} Writer
 * @property {(comets: ReadonlyArray<Comet>) => string[]} [header]
 * @property {(comet: Comet, today: ExportDay) => string[]} lines
 */

/**
 * what a format module exports: the reader, the writer or both
 * @typedef {object} Codec
 * @property {Reader} [reader]
 * @property {Writer} [writer]
 */

/**
 * an element format
 * @typedef {object} ElementFormat
 * @property {string} key the desktop's enum name: `MPC`, `DanceOfThePlanets`
 * @property {string} name shown in the UI: `MPC (Soft00Cmt)`
 * @property {string} software the program it is for, in the Elements view's title
 * @property {string} extension of a saved file, with its dot
 * @property {Reader | null} reader null for the export-only formats
 * @property {Writer | null} writer null for the import-only formats
 */

/** @type {Array<[string, string, string, string, Codec]>} */
const ENTRIES = [
	['MPC', 'MPC (Soft00Cmt)', 'MPC', '.txt', mpc],
	['SkyMap', 'SkyMap (Soft01Cmt)', 'SkyMap', '.txt', {}],
	['Guide', 'Guide (Soft02Cmt)', 'Guide', '.txt', {}],
	['xephem', 'xephem (Soft03Cmt)', 'xephem', '.txt', {}],
	['HomePlanet', 'Home Planet (Soft04Cmt)', 'Home Planet', '.csv', {}],
	['MyStars', 'MyStars! (Soft05Cmt)', 'MyStars!', '.txt', {}],
	['TheSky', 'TheSky (Soft06Cmt)', 'TheSky', '.txt', {}],
	['StarryNight', 'Starry Night (Soft07Cmt)', 'Starry Night', '.txt', {}],
	['DeepSpace', 'Deep Space (Soft08Cmt)', 'Deep Space', '.txt', {}],
	['PCTCS', 'PC-TCS (Soft09Cmt)', 'PC-TCS', '.txt', {}],
	['EarthCenteredUniverse', 'Earth Centered Universe (Soft10Cmt)', 'Earth Centered Universe', '.txt', {}],
	['DanceOfThePlanets', 'Dance of the Planets (Soft11Cmt)', 'Dance of the Planets', '.txt', {}],
	['MegaStarV4', 'MegaStar V4.x (Soft12Cmt)', 'MegaStar V4.x', '.txt', {}],
	['SkyChartIII', 'SkyChart III (Soft13Cmt)', 'SkyChart III', '.txt', {}],
	['VoyagerII', 'Voyager II (Soft14Cmt)', 'Voyager II', '.txt', {}],
	['SkyTools', 'SkyTools (Soft15Cmt)', 'SkyTools', '.txt', {}],
	['Autostar', 'Autostar (Soft16Cmt)', 'Autostar', '.txt', {}],
	['SpaceEngine', 'SpaceEngine', 'SpaceEngine', '.sc', {}],
	['Celestia', 'Celestia', 'Celestia', '.ssc', {}],
	['CometForWindows', 'Comet for Windows', 'Comet for Windows', '.dat', {}],
	['NASA', 'NASA (ELEMENTS.COMET)', 'NASA', '.comet', {}]
];

/** the 21 formats, in the desktop's order */
export const FORMATS = Object.freeze(ENTRIES.map(([key, name, software, extension, codec]) => Object.freeze({
	key,
	name,
	software,
	extension,
	reader: codec.reader ? Object.freeze(codec.reader) : null,
	writer: codec.writer ? Object.freeze(codec.writer) : null
})));

const BY_KEY = new Map(FORMATS.map(format => [format.key, format]));

/**
 * @param {string} key
 * @returns {ElementFormat}
 */
export function formatByKey(key) {
	const format = BY_KEY.get(key);
	if (!format) throw new Error(`unknown element format ${key}`);
	return format;
}

/** the formats the Elements view lists, in order (spec 10, D13) */
export const EXPORT_FORMATS = Object.freeze(FORMATS.filter(format => format.writer));
