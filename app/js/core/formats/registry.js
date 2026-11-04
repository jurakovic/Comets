// The element formats, in the desktop's order, each with what reads and writes it.
// docs/spec/03-element-formats.md.
import * as celestia from './celestia.js';
import * as cometForWindows from './comet-for-windows.js';
import * as danceOfThePlanets from './dance-of-the-planets.js';
import * as deepSpace from './deep-space.js';
import * as earthCenteredUniverse from './earth-centered-universe.js';
import * as guide from './guide.js';
import * as homePlanet from './home-planet.js';
import * as megaStar from './megastar.js';
import * as mpc from './mpc.js';
import * as myStars from './my-stars.js';
import * as nasa from './nasa.js';
import * as pcTcs from './pc-tcs.js';
import * as skyChart from './skychart.js';
import * as skyMap from './skymap.js';
import * as skyTools from './skytools.js';
import * as spaceEngine from './space-engine.js';
import * as starryNight from './starry-night.js';
import * as theSky from './the-sky.js';
import * as voyager from './voyager.js';
import * as xephem from './xephem.js';

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
	['SkyMap', 'SkyMap (Soft01Cmt)', 'SkyMap', '.txt', skyMap],
	['Guide', 'Guide (Soft02Cmt)', 'Guide', '.txt', guide],
	['xephem', 'xephem (Soft03Cmt)', 'xephem', '.txt', xephem],
	['HomePlanet', 'Home Planet (Soft04Cmt)', 'Home Planet', '.csv', homePlanet],
	['MyStars', 'MyStars! (Soft05Cmt)', 'MyStars!', '.txt', myStars],
	['TheSky', 'TheSky (Soft06Cmt)', 'TheSky', '.txt', theSky],
	['StarryNight', 'Starry Night (Soft07Cmt)', 'Starry Night', '.txt', starryNight],
	['DeepSpace', 'Deep Space (Soft08Cmt)', 'Deep Space', '.txt', deepSpace],
	['PCTCS', 'PC-TCS (Soft09Cmt)', 'PC-TCS', '.txt', pcTcs],
	['EarthCenteredUniverse', 'Earth Centered Universe (Soft10Cmt)', 'Earth Centered Universe', '.txt', earthCenteredUniverse],
	['DanceOfThePlanets', 'Dance of the Planets (Soft11Cmt)', 'Dance of the Planets', '.txt', danceOfThePlanets],
	['MegaStarV4', 'MegaStar V4.x (Soft12Cmt)', 'MegaStar V4.x', '.txt', megaStar],
	['SkyChartIII', 'SkyChart III (Soft13Cmt)', 'SkyChart III', '.txt', skyChart],
	['VoyagerII', 'Voyager II (Soft14Cmt)', 'Voyager II', '.txt', voyager],
	['SkyTools', 'SkyTools (Soft15Cmt)', 'SkyTools', '.txt', skyTools],
	// files in this format are detected and read as TheSky
	['Autostar', 'Autostar (Soft16Cmt)', 'Autostar', '.txt', { writer: theSky.writer }],
	['SpaceEngine', 'SpaceEngine', 'SpaceEngine', '.sc', spaceEngine],
	['Celestia', 'Celestia', 'Celestia', '.ssc', celestia],
	['CometForWindows', 'Comet for Windows', 'Comet for Windows', '.dat', cometForWindows],
	['NASA', 'NASA (ELEMENTS.COMET)', 'NASA', '.comet', nasa]
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
