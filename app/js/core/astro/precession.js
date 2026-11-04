// Orbital elements precessed from the equinox of an epoch to J2000 (Lieske 1977), for the NASA
// importer. docs/spec/04-astronomy.md, Element precession.

/**
 * @typedef {{ i: number, N: number, w: number }} AngularElements degrees
 */

/**
 * i, N and w from the ecliptic and equinox of epoch to J2000, by rotating the orbit's pole and
 * perihelion directions (`PrecessElementsToJ2000`)
 * @param {number} epochMjd the epoch as a modified JD
 * @param {AngularElements} elements
 * @returns {AngularElements}
 */
export function precessToJ2000(epochMjd, { i, N, w }) {
	const toRad = Math.PI / 180;
	const toDeg = 180 / Math.PI;
	const T = (epochMjd + 2400000.5 - 2451545.0) / 36525;
	const t = -T;
	if (Math.abs(t) < 1e-8) return { i, N, w };

	const eta = ((47.0029 - (0.06603 - 0.000598 * T) * T) * t - (0.03302 - 0.000598 * T) * t * t + 0.000060 * t * t * t) / 3600;
	const pi = 174.876384 + ((3289.4789 + 0.60622 * T) * t - (869.8089 + 0.50491 * T) * t * t + 0.03536 * t * t * t) / 3600;
	const p = ((5029.0966 + (2.2226 - 0.000042 * T) * T) * t + (1.1152 - 0.000042 * T) * t * t - 0.000052 * t * t * t) / 3600;

	const iR = i * toRad;
	const NR = N * toRad;
	const wR = w * toRad;
	// the orbit's pole and the direction of perihelion
	const n = [Math.sin(iR) * Math.sin(NR), -Math.sin(iR) * Math.cos(NR), Math.cos(iR)];
	const q = [
		Math.cos(NR) * Math.cos(wR) - Math.sin(NR) * Math.sin(wR) * Math.cos(iR),
		Math.sin(NR) * Math.cos(wR) + Math.cos(NR) * Math.sin(wR) * Math.cos(iR),
		Math.sin(iR) * Math.sin(wR)
	];

	// R = Rz(−Φ)·Rx(η)·Rz(Π), Φ = Π + p
	const sEta = Math.sin(eta * toRad), cEta = Math.cos(eta * toRad);
	const sPi = Math.sin(pi * toRad), cPi = Math.cos(pi * toRad);
	const sPhi = Math.sin((pi + p) * toRad), cPhi = Math.cos((pi + p) * toRad);
	const R = [
		[cPhi * cPi + sPhi * cEta * sPi, -cPhi * sPi + sPhi * cEta * cPi, -sPhi * sEta],
		[-sPhi * cPi + cPhi * cEta * sPi, sPhi * sPi + cPhi * cEta * cPi, -cPhi * sEta],
		[sEta * sPi, sEta * cPi, cEta]
	];
	const turn = (/** @type {number[]} */ v) => R.map(row => row[0] * v[0] + row[1] * v[1] + row[2] * v[2]);
	const [nx, ny, nz] = turn(n);
	const [px, py, pz] = turn(q);

	const i2 = Math.acos(Math.max(-1, Math.min(1, nz)));
	const N2 = Math.atan2(nx, -ny);
	const w2 = Math.atan2(pz / Math.sin(i2), px * Math.cos(N2) + py * Math.sin(N2));
	return {
		i: i2 * toDeg,
		N: ((N2 * toDeg) % 360 + 360) % 360,
		w: ((w2 * toDeg) % 360 + 360) % 360
	};
}
