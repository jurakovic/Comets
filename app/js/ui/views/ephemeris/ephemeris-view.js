// The Ephemeris view (`FormEphemerisSettings`, `FormEphemeris`): the settings form, the calculation
// through the jobs service with its question, progress and Cancel, and a tab per result with Edit
// settings, Save, Copy and Close. INTERNALS.md, Views.
import { asksLongCalculation, calculationInput, cometsOf, ephemerisFileName } from '../../../core/ephemeris/form.js';
import { defaultEphemerisSettings, ephemerisTitle, LONG_CALCULATION_QUESTION } from '../../../core/ephemeris/settings.js';
import { NO_CHOICE } from '../../../core/input/comet-picker.js';
import { localPartsIn } from '../../../core/time/local.js';
import { createResultTabs } from '../../components/result-tabs.js';
import { el, saveFile } from '../../dom.js';
import { isAbort } from '../../shell/errors.js';
import { createEphemerisForm } from './ephemeris-form.js';

/** @typedef {import('./ephemeris-form.js').EphemerisDraft} EphemerisDraft */

/**
 * a result tab's: its title, the draft it was calculated from, and the text, with CRLF line endings
 * @typedef {object} EphemerisResult
 * @property {string} title
 * @property {Readonly<EphemerisDraft>} draft
 * @property {string} text
 */

/** what Copy says when the browser refuses the clipboard */
const COPY_FAILED = 'The ephemeris could not be copied.';

/**
 * @param {import('../../shell/context.js').AppContext} context
 * @returns {import('../../shell/router.js').View}
 */
export function createEphemerisView({ selection, database, settings, observations, jobs, dialogs, notify, clock }) {
	const comets = () => database.state.get().comets;
	// a new form's settings and the dates of the Default presets, taken once for the view
	const initial = defaultEphemerisSettings(clock.now());

	/** @returns {Readonly<EphemerisDraft>} a new form's: a copy of the app-wide selection, the brightest chosen */
	function newDraft() {
		const { comets: list, filters, sort } = selection.state.get();
		return Object.freeze({ list: Object.freeze({ comets: list, filters, sort }), choice: NO_CHOICE, settings: initial, database: comets() });
	}

	const form = createEphemerisForm({
		draft: newDraft(),
		database: comets,
		value: observations.value,
		now: clock.now,
		timeZone: clock.timeZone,
		defaults: initial,
		onCalculate: draft => { calculate(draft); },
		onCancel: () => running?.abort(new DOMException('The calculation was cancelled', 'AbortError')),
		onNew: stopEditing
	});
	const tabs = createResultTabs({ label: 'Results', name: 'Settings', content: form.element, onClose: closed });
	const element = el('section', { class: 'ephemeris-view', 'data-view': 'ephemeris', 'aria-labelledby': 'view-title-ephemeris' },
		el('h1', { id: 'view-title-ephemeris' }, 'Ephemeris'),
		tabs.element);

	/** @type {Map<number, EphemerisResult>} by tab id */
	const results = new Map();
	/** @type {number | null} the result the form edits, which Calculate replaces */
	let editing = null;
	/** @type {Readonly<EphemerisDraft> | null} the new form's draft, kept while a result is edited */
	let kept = null;
	/** @type {AbortController | null} */
	let running = null;

	/** @param {Readonly<EphemerisDraft>} draft validated */
	async function calculate(draft) {
		if (running) return;
		const { list, choice, settings: chosen } = draft;
		// the observer is the location when Calculate is pressed
		const { location, showLongCalculationConfirmation } = settings.settings.get();
		const over = cometsOf(choice, list);
		if (asksLongCalculation(chosen, over.length, showLongCalculationConfirmation) && !await dialogs.confirm({ message: LONG_CALCULATION_QUESTION })) return;
		const input = calculationInput({ comets: over, settings: chosen, observer: location, timeZone: clock.timeZone(), nearest: observations.nearestPerihelion });
		const target = editing;
		const controller = new AbortController();
		running = controller;
		form.setRunning(true);
		/** @type {string} */
		let text;
		try {
			text = /** @type {string} */ (await jobs.run('ephemeris', input, { signal: controller.signal }));
		} catch (error) {
			// Cancel discards everything
			if (isAbort(error)) return;
			throw error;
		} finally {
			running = null;
			form.setRunning(false);
		}
		const result = { title: ephemerisTitle({ multiple: choice.all, comets: list.comets, selected: choice.comet }), draft, text };
		if (target !== null && tabs.has(target)) {
			results.set(target, result);
			tabs.replace(target, result.title, resultContent(result));
			if (editing === target) stopEditing();
		} else {
			results.set(tabs.add(result.title, resultContent(result)), result);
		}
	}

	/**
	 * a result's panel: its actions over the text, which keeps its TABs and does not wrap
	 * @param {EphemerisResult} result
	 */
	function resultContent({ text }) {
		/**
		 * @param {string} label
		 * @param {(id: number) => void} run on the result shown, whose panel this is
		 */
		const action = (label, run) => {
			const button = el('button', { type: 'button', class: 'button' }, label);
			button.addEventListener('click', () => {
				const id = tabs.selected();
				if (id !== null) run(id);
			});
			return button;
		};
		return el('div', { class: 'ephemeris-view__result' },
			el('div', { class: 'ephemeris-view__actions' },
				action('Edit settings', edit), action('Save', saveResult), action('Copy', copy), action('Close', tabs.close)),
			// shown with LF line endings, which browsers draw as the line breaks they are
			el('pre', { class: 'ephemeris-view__text', tabindex: '0' }, text.replaceAll('\r\n', '\n')));
	}

	/** @param {number} id */
	function edit(id) {
		const result = results.get(id);
		if (!result) return;
		kept ??= form.draft();
		editing = id;
		form.fill(result.draft);
		form.setEditing(result.title);
		tabs.select(null);
	}

	// back to the new form as it was before the edit
	function stopEditing() {
		if (editing === null) return;
		editing = null;
		form.setEditing(null);
		if (kept) form.fill(kept);
		kept = null;
	}

	/** @param {number} id */
	function closed(id) {
		results.delete(id);
		if (editing === id) stopEditing();
	}

	/** @param {number} id */
	function saveResult(id) {
		const result = results.get(id);
		if (!result) return;
		saveFile(ephemerisFileName(localPartsIn(clock.timeZone())(clock.now())), new Blob([result.text], { type: 'text/plain' }));
	}

	/** @param {number} id */
	async function copy(id) {
		const result = results.get(id);
		if (!result) return;
		try {
			await navigator.clipboard.writeText(result.text.replaceAll('\r\n', '\n'));
		} catch {
			notify(COPY_FAILED, { kind: 'error' });
		}
	}

	/** @type {(() => void) | null} */
	let unsubscribe = null;
	return {
		element,
		show() {
			// the picker hears of no import or deletion: its list is read again over the database
			unsubscribe ??= database.state.subscribe(() => form.follow());
			form.follow();
		},
		hide() {
			unsubscribe?.();
			unsubscribe = null;
		},
		save() {
			const id = tabs.selected();
			if (id !== null) saveResult(id);
		}
	};
}
