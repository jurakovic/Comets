// import-expected/*.json: each input file's detected type and its import into an empty database,
// including the round trips of every export file (spec 03, Import rules).
import { test } from 'node:test';
import { assertImportExpected } from '../helpers/formats.js';

// the vector files, with the elements their format computes rather than reads
/** @type {Array<[string, ReadonlyArray<string>]>} */
const CASES = [
	['Soft00Cmt.txt.json', []],
	['roundtrip-00-MPC.txt.json', []],
	['Soft01Cmt.txt.json', []],
	['roundtrip-01-SkyMap.txt.json', []],
	['Soft02Cmt.txt.json', []],
	['roundtrip-02-Guide.txt.json', []],
	['Soft03Cmt.txt.json', []],
	['roundtrip-03-xephem.txt.json', []],
	['Soft04Cmt.txt.json', []],
	['roundtrip-04-HomePlanet.txt.json', []],
	['Soft05Cmt.txt.json', []],
	['roundtrip-05-MyStars.txt.json', []],
	['Soft06Cmt.txt.json', []],
	['roundtrip-06-TheSky.txt.json', []],
	['Soft07Cmt.txt.json', []],
	['roundtrip-07-StarryNight.txt.json', []],
	['Soft08Cmt.txt.json', []],
	['roundtrip-08-DeepSpace.txt.json', []],
	['Soft09Cmt.txt.json', []],
	['roundtrip-09-PCTCS.txt.json', []],
	['Soft10Cmt.txt.json', []],
	['roundtrip-10-EarthCenteredUniverse.txt.json', []],
	['Soft11Cmt.txt.json', []],
	['roundtrip-11-DanceOfThePlanets.txt.json', []],
	['Soft12Cmt.txt.json', []],
	['roundtrip-12-MegaStarV4.txt.json', []],
	['Soft13Cmt.txt.json', []],
	['roundtrip-13-SkyChartIII.txt.json', []],
	['Soft14Cmt.txt.json', []],
	['roundtrip-14-VoyagerII.txt.json', []],
	['Soft15Cmt.txt.json', []],
	['roundtrip-15-SkyTools.txt.json', []],
	['Soft16Cmt.txt.json', []],
	['roundtrip-16-Autostar.txt.json', []],
	['roundtrip-17-SpaceEngine.txt.json', []],
	['roundtrip-18-Celestia.txt.json', []]
];

for (const [name, computed] of CASES) {
	test(name, () => assertImportExpected(name, { computed }));
}
