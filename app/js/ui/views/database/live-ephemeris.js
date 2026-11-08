// The Ephemeris tab's live block: a date picker whose instant ticks once a second, and the comet's
// values at it with their trends. The clock's rules are core/time/live-clock.js, the rows'
// core/comets/details.js. docs/spec/05-database-and-update.md, Details: Ephemeris tab.
import { ephemeris } from '../../../core/astro/ephemeris.js';
import { liveRows } from '../../../core/comets/details.js';
import { instantToJd, jdToInstant } from '../../../core/time/julian.js';
import { createLiveClock, pickLiveClock, resumeLiveClock, tickDelay, tickLiveClock } from '../../../core/time/live-clock.js';
import { createDatePicker } from '../../components/date-picker.js';
import { el } from '../../dom.js';

/** @typedef {import('../../../core/comets/comet.js').Comet} Comet */
/** @typedef {import('../../../core/astro/ephemeris.js').Ephemeris} Ephemeris */

/**
 * @typedef {object} LiveEphemeris
 * @property {HTMLElement} element
 * @property {(comet: Comet | null, Tn: number | null) => void} show the comet, its `Tn` for the
 *   Perihelion Date preset; none for null, which stops the ticks
 * @property {() => void} start the owner is shown: ticking resumes
 * @property {() => void} stop the owner is hidden or gone: ticking stops, and nothing is left listening
 */

/**
 * @param {object} options
 * @param {import('../../../services/ports.js').Clock} options.clock
 * @param {() => import('../../../core/astro/ephemeris.js').Observer} options.observer
 * @returns {LiveEphemeris}
 */
export function createLiveEphemeris({ clock, observer }) {
	let live = createLiveClock(clock.now());
	/** @type {Comet | null} */
	let comet = null;
	/** @type {Ephemeris | null} */
	let previous = null;
	let started = false;
	/** @type {ReturnType<typeof setTimeout> | null} */
	let timer = null;

	const rows = el('dl', { class: 'comet-details__rows live-ephemeris__rows', 'aria-live': 'off' });
	const picker = createDatePicker({
		value: live.shownAt,
		now: () => clock.now(),
		timeZone: () => clock.timeZone(),
		label: 'Ephemeris date',
		onChange: instant => {
			// picking a date restarts the clock from it; the ticks keep their boundaries
			live = pickLiveClock(instant);
			computeFirst();
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
		const record = at(live.shownAt);
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
		picker.setValue(live.shownAt);
		previous = comet ? at(live.shownAt - 1000) : null;
		render();
	}

	const running = () => started && comet !== null && document.visibilityState === 'visible';

	// a tick pending is never cancelled while the clock runs: that would lose its second
	function keepTicking() {
		if (!running()) {
			if (timer !== null) clearTimeout(timer);
			timer = null;
			return;
		}
		if (timer !== null) return;
		timer = setTimeout(() => {
			timer = null;
			if (!running()) return;
			live = tickLiveClock(live);
			picker.setValue(live.shownAt);
			render();
			keepTicking();
		}, tickDelay(clock.now()));
	}

	// back from a pause, or to a new comet: following the clock shows now, a picked date stays
	function resume() {
		live = resumeLiveClock(live, clock.now());
		computeFirst();
		keepTicking();
	}

	const onVisibility = () => {
		if (document.visibilityState === 'visible') resume();
		else keepTicking();
	};

	return Object.freeze({
		element,
		show(/** @type {Comet | null} */ next, /** @type {number | null} */ Tn) {
			comet = next;
			picker.setPerihelionDate(Tn === null ? null : jdToInstant(Tn));
			resume();
		},
		start() {
			if (!started) document.addEventListener('visibilitychange', onVisibility);
			started = true;
			resume();
		},
		stop() {
			started = false;
			document.removeEventListener('visibilitychange', onVisibility);
			keepTicking();
		}
	});
}
