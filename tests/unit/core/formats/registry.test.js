import { test } from 'node:test';
import assert from 'node:assert/strict';
import { DETECTION_ORDER } from '../../../../app/js/core/formats/detection.js';
import { EXPORT_FORMATS, FORMATS, formatByKey } from '../../../../app/js/core/formats/registry.js';

// spec 03's table: key, name shown, software, extension
const TABLE = [
	['MPC', 'MPC (Soft00Cmt)', 'MPC', '.txt'],
	['SkyMap', 'SkyMap (Soft01Cmt)', 'SkyMap', '.txt'],
	['Guide', 'Guide (Soft02Cmt)', 'Guide', '.txt'],
	['xephem', 'xephem (Soft03Cmt)', 'xephem', '.txt'],
	['HomePlanet', 'Home Planet (Soft04Cmt)', 'Home Planet', '.csv'],
	['MyStars', 'MyStars! (Soft05Cmt)', 'MyStars!', '.txt'],
	['TheSky', 'TheSky (Soft06Cmt)', 'TheSky', '.txt'],
	['StarryNight', 'Starry Night (Soft07Cmt)', 'Starry Night', '.txt'],
	['DeepSpace', 'Deep Space (Soft08Cmt)', 'Deep Space', '.txt'],
	['PCTCS', 'PC-TCS (Soft09Cmt)', 'PC-TCS', '.txt'],
	['EarthCenteredUniverse', 'Earth Centered Universe (Soft10Cmt)', 'Earth Centered Universe', '.txt'],
	['DanceOfThePlanets', 'Dance of the Planets (Soft11Cmt)', 'Dance of the Planets', '.txt'],
	['MegaStarV4', 'MegaStar V4.x (Soft12Cmt)', 'MegaStar V4.x', '.txt'],
	['SkyChartIII', 'SkyChart III (Soft13Cmt)', 'SkyChart III', '.txt'],
	['VoyagerII', 'Voyager II (Soft14Cmt)', 'Voyager II', '.txt'],
	['SkyTools', 'SkyTools (Soft15Cmt)', 'SkyTools', '.txt'],
	['Autostar', 'Autostar (Soft16Cmt)', 'Autostar', '.txt'],
	['SpaceEngine', 'SpaceEngine', 'SpaceEngine', '.sc'],
	['Celestia', 'Celestia', 'Celestia', '.ssc'],
	['CometForWindows', 'Comet for Windows', 'Comet for Windows', '.dat'],
	['NASA', 'NASA (ELEMENTS.COMET)', 'NASA', '.comet']
];

test('the 21 formats of spec 03, in order', () => {
	assert.deepEqual(FORMATS.map(({ key, name, software, extension }) => [key, name, software, extension]), TABLE);
	assert.ok(Object.isFrozen(FORMATS) && FORMATS.every(Object.isFrozen));
});

test('formats by key; an unknown key is a bug', () => {
	assert.equal(formatByKey('HomePlanet').extension, '.csv');
	assert.throws(() => formatByKey('Unknown'), /unknown element format/);
});

test('what each format reads and writes, as spec 03\'s table says', () => {
	// Autostar files are read as TheSky; SpaceEngine and Celestia are export only
	const importOnly = ['CometForWindows', 'NASA'];
	const exportOnly = ['Autostar', 'SpaceEngine', 'Celestia'];
	for (const { key, reader, writer } of FORMATS) {
		assert.equal(Boolean(reader), !exportOnly.includes(key), `${key} reader`);
		assert.equal(Boolean(writer), !importOnly.includes(key), `${key} writer`);
	}
	assert.equal(formatByKey('Autostar').writer, formatByKey('TheSky').writer);
	assert.deepEqual(EXPORT_FORMATS.map(f => f.key), FORMATS.slice(0, 19).map(f => f.key));
});

test('detection tries every format with a reader: headers where they have one', () => {
	assert.ok(DETECTION_ORDER.headers.every(key => formatByKey(key).reader?.header), 'headers');
	const tried = new Set([...DETECTION_ORDER.headers, ...DETECTION_ORDER.records]);
	assert.deepEqual(FORMATS.filter(f => f.reader).map(f => f.key).filter(key => !tried.has(key)), []);
	assert.deepEqual([...tried].filter(key => !formatByKey(key).reader), []);
});
