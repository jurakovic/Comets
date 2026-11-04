// Arithmetic as .NET does it where JavaScript differs.

/**
 * the nearest integer, halves to the even one (.NET Math.Round; Math.round rounds halves up)
 * @param {number} x
 * @returns {number}
 */
export function roundEven(x) {
	const up = Math.round(x);
	// Math.round(x) − x is exact here, and 0.5 only for a half that went up
	return up - x === 0.5 && up % 2 !== 0 ? up - 1 : up;
}
