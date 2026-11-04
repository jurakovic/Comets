// One ephemeris record: where a comet is seen from a place on Earth at a JD, and how bright.
// docs/spec/04-astronomy.md, Ephemeris.
import { acosd, asind, atan2d, cosd, D0, J2000, log10, obliquity, rev, rev2, sind, tand } from './math.js';
import { cometXyz, sunXyz } from './position.js';

/**
 * where the observer stands; latitude positive north, longitude positive east, degrees
 * @typedef {{ latitude: number, longitude: number }} Observer
 */

/**
 * an ephemeris record; angles in degrees, distances in AU
 * @typedef {object} Ephemeris
 * @property {number} JD
 * @property {number} RA right ascension, [0, 360)
 * @property {number} Dec declination, [−90, 90]
 * @property {number} Alt altitude, [−90, 90]
 * @property {number} Az azimuth from north through east, [0, 360)
 * @property {number} EcLon geocentric ecliptic longitude, [0, 360)
 * @property {number} EcLat geocentric ecliptic latitude
 * @property {number} Elongation the angle between the comet and the Sun
 * @property {number} PositionAngle of the comet as seen from the Sun's direction, [0, 360)
 * @property {number} SunDist r
 * @property {number} EarthDist Δ
 * @property {number} Magnitude g + 5 log Δ + 2.5 k log r
 */

/**
 * altitude and azimuth of an equatorial position (`RaDecToAltAz`)
 * @param {number} ra degrees
 * @param {number} dec degrees
 * @param {number} jd
 * @param {Observer} observer
 */
function altAz(ra, dec, jd, { latitude, longitude }) {
	// Greenwich mean sidereal time, Meeus 12.4
	const theta0 = 280.46061837 + 360.98564736629 * (jd - J2000);
	const H = rev(theta0 + longitude - ra);
	const alt = asind(sind(latitude) * sind(dec) + cosd(latitude) * cosd(dec) * cosd(H));
	const az = atan2d(sind(H), cosd(H) * sind(latitude) - tand(dec) * cosd(latitude));
	return { alt, az: rev(az + 180) };
}

/**
 * the ephemeris of a comet (`GetEphemeris`)
 * @param {import('./position.js').Elements & { g: number, k: number }} comet
 * @param {number} jd
 * @param {Observer} observer
 * @returns {Ephemeris}
 */
export function ephemeris(comet, jd, observer) {
	const sun = sunXyz(jd);
	const body = cometXyz(comet, jd);
	const d = jd - D0;
	const epsilon = obliquity(d);

	// geocentric ecliptic
	const gx = body.x + sun.x;
	const gy = body.y + sun.y;
	const gz = body.z;
	const EcLon = rev(atan2d(gy, gx));
	const EcLat = atan2d(gz, Math.sqrt(gx * gx + gy * gy));

	// geocentric equatorial
	const x1 = gx;
	const y1 = gy * cosd(epsilon) - gz * sind(epsilon);
	const z1 = gy * sind(epsilon) + gz * cosd(epsilon);
	const RA = rev(atan2d(y1, x1));
	const Dec = atan2d(z1, Math.sqrt(x1 * x1 + y1 * y1));
	const EarthDist = Math.sqrt(x1 * x1 + y1 * y1 + z1 * z1);
	// the solver's distance, as the desktop takes it
	const SunDist = body.r;
	const { alt: Alt, az: Az } = altAz(RA, Dec, jd, observer);
	const Magnitude = comet.g + 5 * log10(EarthDist) + 2.5 * comet.k * log10(SunDist);

	// the Sun's RA and Dec (`SunAlt`)
	const xe = sun.x;
	const ye = sun.y * cosd(epsilon);
	const ze = sun.y * sind(epsilon);
	const sunRa = rev(atan2d(ye, xe));
	const sunDec = atan2d(ze, Math.sqrt(xe * xe + ye * ye));

	// the separation from the Sun (`Separation`), Meeus 17.1, and 17.2 for small angles
	let Elongation = acosd(sind(Dec) * sind(sunDec) + cosd(Dec) * cosd(sunDec) * cosd(RA - sunRa));
	if (Elongation < 0.1) {
		const across = rev2(RA - sunRa) * cosd((Dec + sunDec) / 2);
		const along = Dec - sunDec;
		Elongation = Math.sqrt(across * across + along * along);
	}
	const PositionAngle = rev(atan2d(sind(RA - sunRa), cosd(sunDec) * tand(Dec) - sind(sunDec) * cosd(RA - sunRa)));

	return { JD: jd, RA, Dec, Alt, Az, EcLon, EcLat, Elongation, PositionAngle, SunDist, EarthDist, Magnitude };
}

/**
 * which side of the Sun the comet is: W for a position angle of 180° or more, otherwise E
 * @param {Ephemeris} record
 * @returns {'W' | 'E'}
 */
export function elongationSide(record) {
	return record.PositionAngle >= 180 ? 'W' : 'E';
}
