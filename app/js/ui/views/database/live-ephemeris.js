// The Ephemeris tab's live block: a date picker whose instant ticks once a second, on the system
// clock's second boundary, and the comet's values at it with their trends. What the rows say is
// core/comets/details.js. docs/spec/05-database-and-update.md, Details: Ephemeris tab.
import { ephemeris } from '../../../core/astro/ephemeris.js';
import { liveRows } from '../../../core/comets/details.js';
import { instantToJd, jdToInstant } from '../../../core/time/julian.js';
import { createDatePicker } from '../../components/date-picker.js';
import { el } from '../../dom.js';

/** @typedef {import('../../../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../../../core/astro/ephemeris.js').Ephemeris} Ephemeris */

/** how long after the second boundary a tick runs, as the desktop's sync window (15–35 ms) */
const AFTER_BOUNDARY = 20;

/** @param {number} instant */
const wholeSecond = instant => Math.floor(instant / 1000) * 1000;

/**
 * @typedef {object} LiveEphemeris
 * @property {HTMLElement} element
 * @property {(comet: Comet | null, Tn: number | null) => void} show the comet, its `Tn` for the
 *   Perihelion Date preset; none for null, which stops the ticks
 * @property {() => void} start the owner is shown: ticking resumes
 * @property {() => void} stop the owner is hidden: ticking stops
 */

/**
 * @param {object} options
 * @param {import('../../../services/ports.js').Clock} options.clock
 * @param {() => import('../../../core/astro/ephemeris.js').Observer} options.observer
 * @returns {LiveEphemeris}
 */
export function createLiveEphemeris({ clock, observer }) {
	// the instant shown, whole seconds; it follows the clock until a date is picked
	let shownAt = wholeSecond(clock.now());
	let following = true;
	/** @type {Comet | null} */
	let comet = null;
	/** @type {Ephemeris | null} */
	let previous = null;
	let started = false;
	/** @type {ReturnType<typeof setTimeout> | null} */
	let timer = null;

	const rows = el('dl', { class: 'comet-details__rows live-ephemeris__rows', 'aria-live': 'off' });
	const picker = createDatePicker({
		value: shownAt,
		now: () => clock.now(),
		timeZone: () => clock.timeZone(),
		label: 'Ephemeris date',
		onChange: instant => {
			// picking a date restarts the clock from it
			shownAt = instant;
			following = false;
			computeFirst();
			restart();
		}
	});
	const element = el('div', { class: 'live-ephemeris' }, el('div', { class: 'live-ephemeris__date' }, picker.element), rows);

	/** @param {number} instant */
	const at = instant => ephemeris(/** @type {Comet} */ (comet), instantToJd(instant), observer());

	function render() {
		if (!comet || !previous) {
			rows.replaceChildren();
			return;
		}
		const record = at(shownAt);
		rows.replaceChildren(...liveRows(record, previous, observer().latitude).flatMap(({ key, label, value, unit, trend }) => [
			el('dt', { class: 'comet-details__label', 'data-key': key }, label),
			el('dd', { class: 'comet-details__value', 'data-key': key },
				el('span', { class: 'comet-details__number' }, value),
				unit ? el('span', { class: 'comet-details__unit' }, unit) : '',
				el('span', { class: `live-ephemeris__trend live-ephemeris__trend--${trend.tone}`, 'data-trend': trend.tone }, trend.arrow))
		]));
		previous = record;
	}

	// the first values compare with the instant 1 s earlier
	function computeFirst() {
		picker.setValue(shownAt);
		previous = comet ? at(shownAt - 1000) : null;
		render();
	}

	function tick() {
		shownAt += 1000;
		picker.setValue(shownAt);
		render();
	}

	const running = () => started && comet !== null && document.visibilityState === 'visible';

	function schedule() {
		const wait = 1000 - (clock.now() % 1000) + AFTER_BOUNDARY;
		timer = setTimeout(() => {
			timer = null;
			if (!running()) return;
			tick();
			schedule();
		}, wait);
	}

	function halt() {
		if (timer !== null) clearTimeout(timer);
		timer = null;
	}

	function restart() {
		halt();
		if (running()) schedule();
	}

	// back from a pause: following the clock jumps to now, a picked date goes on where it stopped
	function resume() {
		if (!running()) return;
		if (following) shownAt = wholeSecond(clock.now());
		computeFirst();
		restart();
	}

	document.addEventListener('visibilitychange', () => {
		if (document.visibilityState === 'visible') resume();
		else halt();
	});

	return Object.freeze({
		element,
		show(/** @type {Comet | null} */ next, /** @type {number | null} */ Tn) {
			comet = next;
			picker.setPerihelionDate(Tn === null ? null : jdToInstant(Tn));
			computeFirst();
			if (comet) restart();
			else halt();
		},
		start() {
			started = true;
			resume();
		},
		stop() {
			started = false;
			halt();
		}
	});
}
