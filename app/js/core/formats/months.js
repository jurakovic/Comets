// The month abbreviations Voyager II and Celestia write (`CometManager.Month`).
// docs/spec/03-element-formats.md, Voyager II.

/** `Jan` … `Dec` */
export const MONTHS = Object.freeze(['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']);

/**
 * a month's abbreviation; a month outside 1–12, which the desktop cannot write, as its number
 * @param {number} month
 * @returns {string}
 */
export function monthAbbreviation(month) {
	return MONTHS[month - 1] ?? String(month);
}
