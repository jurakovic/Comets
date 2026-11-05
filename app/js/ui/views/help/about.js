// What About says: the version, the copyright, the repository, and the credits and references of
// the repository README. docs/spec/01-product.md, Help.

/** the web app's version */
export const APP_VERSION = 'dev';

export const COPYRIGHT = 'Copyright © 2026 jurakovic';

export const REPOSITORY = 'https://github.com/jurakovic/Comets';

export const LICENCE = 'GPL-3.0';

/**
 * text with links: plain strings, and [text, url] pairs
 * @typedef {ReadonlyArray<string | readonly [string, string]>} Line
 */

/** @type {ReadonlyArray<Line>} */
export const CREDITS = Object.freeze([
	['Inspired by the ', ['Comet for Windows', 'http://www.aerith.net/project/comet.html'], ' by Seiichi Yoshida.'],
	['Includes improved version of ', ['OrbitViewer.NET', 'https://github.com/jurakovic/OrbitViewer.NET'], ' (C# rewrite of ',
		['OrbitViewer', 'https://www.astroarts.co.jp/products/orbitviewer/index.html'], ' by Osamu Ajiki and Ron Baalke).'],
	['Dark theme thanks to ', ['Dark-Mode-Forms', 'https://github.com/BlueMystical/Dark-Mode-Forms'], ' by BlueMystical (my fork ',
		['here', 'https://github.com/jurakovic/Dark-Mode-Forms'], ').']
]);

/** @type {ReadonlyArray<{ title: string, lines: ReadonlyArray<Line> }>} */
export const REFERENCES = Object.freeze([
	{
		title: 'Books',
		lines: [
			[['Astronomical Algorithms', 'https://www.amazon.com/Astronomical-Algorithms-Jean-Meeus/dp/0943396352/'], ' by Jean Meeus (Willmann-Bell, 1998)'],
			[['Astronomical Formulae for Calculators', 'https://www.amazon.com/Astronomical-Formulae-Calculators-Jean-Meeus/dp/0943396220/'], ' by Jean Meeus (Willmann-Bell, 1988)'],
			[['Practical Astronomy with your Calculator or Spreadsheet', 'https://www.amazon.com/Practical-Astronomy-your-Calculator-Spreadsheet/dp/1108436072/'],
				' by Peter Duffett-Smith and Jonathan Zwart (Cambridge, 2011)']
		]
	},
	{
		title: 'Web',
		lines: [
			['Computing planetary positions (', ['#1', 'https://stjarnhimlen.se/comp/tutorial.html'], ', ', ['#2', 'https://stjarnhimlen.se/comp/ppcomp.html'], ') by Paul Schlyter']
		]
	},
	{
		title: 'Software',
		lines: [
			[['Cartes du Ciel (SkyChart)', 'https://github.com/pchev/skychart'], ' by Patrick Chevalley'],
			[['Celestia', 'https://github.com/CelestiaProject/Celestia'], ' by Chris Laurel ', ['et al.', 'https://github.com/CelestiaProject/Celestia#contributions'],
				' (my fork ', ['here', 'https://github.com/jurakovic/Celestia'], ')'],
			[['Stellarium', 'https://github.com/Stellarium/stellarium'], ' by Fabien Chereau ', ['et al.', 'https://github.com/Stellarium/stellarium/blob/master/CREDITS.md']],
			[['OrbitViewer', 'https://www.astroarts.co.jp/products/orbitviewer/index.html'], ' by Osamu Ajiki and Ron Baalke'],
			[['OrbitViewer', 'https://ssd.jpl.nasa.gov/tools/orbit_viewer.html'], ' by Kevin Gill et al. (NASA/JPL)'],
			[['PlanetDroid', 'https://www.strickling.net/android_en.htm#PlanetDroid'], ' by Wolfgang Strickling'],
			[['AstroLib', 'https://mhuss.com/AstroLib/'], ' by Mark Huss'],
			[['Javascript AstroTools', 'https://web.archive.org/web/20150923180018/http://www.ngc7000.org/astrotools/ephemtool.html'], ' by Ole Nielsen'],
			[['ProjectPluto', 'https://www.projectpluto.com/source.htm'], ' by Bill Gray'],
			[['AA+', 'http://www.naughter.com/aa.html'], ' by PJ Naughter']
		]
	}
]);
