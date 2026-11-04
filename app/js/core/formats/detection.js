// Which format a file is in (`GetImportType`): headers first, then record parsers on the last
// record. docs/spec/03-element-formats.md, Detection.
import { formatByKey } from './registry.js';
import { attempt, splitLines } from './text.js';

/** no file was chosen */
export const NO_FILE_SELECTED = 'NoFileSelected';
/** the file has no lines */
export const EMPTY_FILE = 'EmptyFile';
/** no format reads it */
export const UNKNOWN = 'Unknown';

/**
 * the order of the checks, which matters: the first match wins
 * @type {Readonly<{ headers: ReadonlyArray<string>, records: ReadonlyArray<string> }>}
 */
export const DETECTION_ORDER = Object.freeze({
	headers: Object.freeze(['HomePlanet', 'MyStars', 'StarryNight', 'DeepSpace', 'DanceOfThePlanets', 'VoyagerII', 'CometForWindows']),
	records: Object.freeze([
		'MPC', 'SkyMap', 'xephem', 'HomePlanet', 'MyStars', 'TheSky', 'StarryNight', 'PCTCS', 'EarthCenteredUniverse',
		'DanceOfThePlanets', 'MegaStarV4', 'SkyChartIII', 'VoyagerII', 'SkyTools', 'NASA', 'DeepSpace', 'Guide'
	])
});

/**
 * whether the reader's record parser accepts the file's last record
 * @param {import('./registry.js').Reader} reader
 * @param {ReadonlyArray<string>} lines
 */
function readsLastRecord(reader, lines) {
	if (lines.length < reader.size) return false;
	const record = lines.slice(lines.length - reader.size);
	return attempt(() => reader.parse(record)) !== null;
}

/**
 * the format key of a file's text, or NoFileSelected, EmptyFile or Unknown. A file with no lines
 * (nothing but a BOM) is empty; the desktop crashes on it, as on a one-line file that matches no
 * header (spec 10, D4)
 * @param {string | null} text null when no file was chosen
 * @returns {string}
 */
export function detectFormat(text) {
	if (text === null) return NO_FILE_SELECTED;
	const lines = splitLines(text);
	if (lines.length === 0) return EMPTY_FILE;
	for (const key of DETECTION_ORDER.headers) {
		if (formatByKey(key).reader?.header?.(lines)) return key;
	}
	for (const key of DETECTION_ORDER.records) {
		const { reader } = formatByKey(key);
		if (reader && readsLastRecord(reader, lines)) return key;
	}
	return UNKNOWN;
}
