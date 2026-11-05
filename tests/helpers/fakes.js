// Fakes of the ports (docs/spec/12-architecture.md, Services and ports) for service tests.

/**
 * a Clock that stands still until a test moves it
 * @param {number} start UTC milliseconds
 * @param {string} [zone] the IANA time zone
 */
export function createFakeClock(start, zone = 'UTC') {
	let instant = start;
	return {
		now: () => instant,
		timeZone: () => zone,
		/** moves the clock by ms, which may be negative */
		advance(/** @type {number} */ ms) { instant += ms; },
		/** puts the clock at an instant */
		set(/** @type {number} */ to) { instant = to; }
	};
}
