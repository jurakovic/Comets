// Local time: date parts of an instant in a named time zone, through Intl.
// docs/spec/04-astronomy.md, Local time.

/**
 * a function giving the date parts of an instant in the time zone, with its daylight-saving rules
 * for that instant; create it once per zone, since Intl formatters are costly to build
 * @param {string} timeZone an IANA name such as `Europe/Zagreb`; throws RangeError for an unknown one
 * @returns {(instant: number) => import('./calendar.js').DateParts}
 */
export function localPartsIn(timeZone) {
	const formatter = new Intl.DateTimeFormat('en-US', {
		timeZone, calendar: 'gregory', numberingSystem: 'latn', hourCycle: 'h23', era: 'short',
		year: 'numeric', month: 'numeric', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric'
	});
	return instant => {
		/** @type {Record<string, string>} */
		const fields = {};
		for (const { type, value } of formatter.formatToParts(instant)) fields[type] = value;
		const year = Number(fields.year);
		return {
			// Intl writes 147 BC for the astronomical year −146
			year: fields.era === 'BC' ? 1 - year : year,
			month: Number(fields.month),
			day: Number(fields.day),
			hour: Number(fields.hour),
			minute: Number(fields.minute),
			second: Number(fields.second)
		};
	};
}

