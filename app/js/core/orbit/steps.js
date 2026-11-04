// The orbit viewer's simulation steps: the step menu and the single steps (|<, >|).
// docs/spec/08-orbit-viewer.md, Simulation.
import { addDays, addHours, addMonths, addYears, instantToParts, partsToInstant } from '../time/calendar.js';

/**
 * @typedef {object} SimulationStep
 * @property {string} label as the step menu shows it
 * @property {number} years
 * @property {number} months
 * @property {number} days
 * @property {number} hours
 */

/**
 * @param {string} label
 * @param {number} years
 * @param {number} months
 * @param {number} days
 * @param {number} hours
 * @returns {SimulationStep}
 */
function step(label, years, months, days, hours) {
	return Object.freeze({ label, years, months, days, hours });
}

/** the step menu, smallest first; faster and slower move along it */
export const SIMULATION_STEPS = Object.freeze([
	step('1 Hour', 0, 0, 0, 1),
	step('6 Hours', 0, 0, 0, 6),
	step('12 Hours', 0, 0, 0, 12),
	step('1 Day', 0, 0, 1, 0),
	step('3 Days', 0, 0, 3, 0),
	step('10 Days', 0, 0, 10, 0),
	step('1 Month', 0, 1, 0, 0),
	step('3 Months', 0, 3, 0, 0),
	step('6 Months', 0, 6, 0, 0),
	step('1 Year', 1, 0, 0, 0),
	step('3 Years', 3, 0, 0, 0),
	step('10 Years', 10, 0, 0, 0)
]);

/**
 * @param {string} label
 * @returns {SimulationStep}
 */
export function simulationStep(label) {
	const found = SIMULATION_STEPS.find(entry => entry.label === label);
	if (!found) throw new Error(`no simulation step ${label}`);
	return found;
}

/**
 * one single step with calendar arithmetic: years, then months, then days, then hours, from the
 * instant's whole second; the caller clamps the result to the date range
 * @param {number} instant UTC milliseconds
 * @param {SimulationStep} span
 * @param {boolean} forward
 * @returns {number} UTC milliseconds, whole seconds
 */
export function stepDate(instant, span, forward) {
	const sign = forward ? 1 : -1;
	let t = partsToInstant(instantToParts(instant));
	t = addYears(t, sign * span.years);
	t = addMonths(t, sign * span.months);
	t = addDays(t, sign * span.days);
	t = addHours(t, sign * span.hours);
	const parts = instantToParts(t);
	// the desktop's ATime skips the days the Gregorian reform removed (1582-10-05 to 10-14)
	if (parts.year === 1582 && parts.month === 10 && parts.day > 4 && parts.day < 15) {
		return partsToInstant({ ...parts, day: forward ? 15 : 4 });
	}
	return t;
}
