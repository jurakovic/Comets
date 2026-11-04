import { test } from 'node:test';
import assert from 'node:assert/strict';
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

test('the Elements view lists only formats with a writer', () => {
	assert.ok(EXPORT_FORMATS.every(format => format.writer));
});
