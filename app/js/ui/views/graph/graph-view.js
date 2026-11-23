// The Graph view (`FormGraphSettings`, `FormGraph`): the settings form, the calculation through the
// jobs service with its question, progress and Cancel, and a tab per result with its chart, Edit
// settings, Save as PNG or JPEG, and Close. INTERNALS.md, Views.
import { LONG_CALCULATION_QUESTION } from '../../../core/ephemeris/settings.js';
import { asksLongCalculation, calculationInput, cometsOf, graphFileName, IMAGE_FORMATS } from '../../../core/graph/form.js';
import { defaultGraphSettings, graphTitle } from '../../../core/graph/settings.js';
import { NO_CHOICE } from '../../../core/input/comet-picker.js';
import { localPartsIn } from '../../../core/time/local.js';
import { createResultTabs } from '../../components/result-tabs.js';
import { el, saveFile } from '../../dom.js';
import { isAbort } from '../../shell/errors.js';
import { createGraphChart } from './graph-chart.js';
import { createGraphForm } from './graph-form.js';

/** @typedef {import('./graph-form.js').GraphDraft} GraphDraft */
/** @typedef {import('../../../core/graph/form.js').ImageFormat} ImageFormat */

/**
 * a result tab's: its title, the draft it was calculated from, and its chart
 * @typedef {object} GraphResult
 * @property {string} title
 * @property {Readonly<GraphDraft>} draft
 * @property {import('./graph-chart.js').GraphChart} chart
 */

/**
 * @param {import('../../shell/context.js').AppContext} context
 * @returns {import('../../shell/router.js').View}
 */
export function createGraphView({ selection, database, settings, observations, jobs, dialogs, clock }) {
	const comets = () => database.state.get().comets;
	// a new form's settings and the dates of the Default presets, taken once for the view
	const initial = defaultGraphSettings(clock.now());

	/** @returns {Readonly<GraphDraft>} a new form's: a copy of the app-wide selection, the brightest chosen */
	function newDraft() {
		const { comets: list, filters, sort } = selection.state.get();
		return Object.freeze({ list: Object.freeze({ comets: list, filters, sort }), choice: NO_CHOICE, settings: initial, database: comets() });
	}

	const form = createGraphForm({
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
	const element = el('section', { class: 'graph-view', 'data-view': 'graph', 'aria-labelledby': 'view-title-graph' },
		el('h1', { id: 'view-title-graph' }, 'Graph'),
		tabs.element);

	/** @type {Map<number, GraphResult>} by tab id */
	const results = new Map();
	/** @type {number | null} the result the form edits, which Calculate replaces */
	let editing = null;
	/** @type {Readonly<GraphDraft> | null} the new form's draft, kept while a result is edited */
	let kept = null;
	/** @type {AbortController | null} */
	let running = null;

	/** @param {Readonly<GraphDraft>} draft validated */
	async function calculate(draft) {
		if (running) return;
		const { list, choice, settings: chosen } = draft;
		// the observer is the location when Calculate is pressed
		const { location, showLongCalculationConfirmation } = settings.settings.get();
		const over = cometsOf(choice, list);
		if (asksLongCalculation(chosen, over.length, showLongCalculationConfirmation) && !await dialogs.confirm({ message: LONG_CALCULATION_QUESTION })) return;
		const input = calculationInput({
			comets: over, settings: chosen, multiple: choice.all, observer: location, now: clock.now(), nearest: observations.nearestPerihelion
		});
		const target = editing;
		const controller = new AbortController();
		running = controller;
		form.setRunning(true);
		/** @type {import('../../../core/graph/model.js').GraphModel} */
		let model;
		try {
			model = /** @type {import('../../../core/graph/model.js').GraphModel} */ (await jobs.run('graph', input, { signal: controller.signal }));
		} catch (error) {
			// Cancel discards everything
			if (isAbort(error)) return;
			throw error;
		} finally {
			running = null;
			form.setRunning(false);
		}
		const result = { title: graphTitle(chosen, { multiple: choice.all, comets: list.comets, selected: choice.comet }), draft, chart: createGraphChart(model) };
		if (target !== null && tabs.has(target)) {
			results.get(target)?.chart.destroy();
			results.set(target, result);
			tabs.replace(target, result.title, resultContent(result));
			if (editing === target) stopEditing();
		} else {
			results.set(tabs.add(result.title, resultContent(result)), result);
		}
	}

	/**
	 * a result's panel: its actions over the chart, which fills the rest
	 * @param {GraphResult} result
	 */
	function resultContent({ chart }) {
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
		return el('div', { class: 'graph-view__result' },
			el('div', { class: 'graph-view__actions' },
				action('Edit settings', edit),
				...IMAGE_FORMATS.map(format => action(format.label, id => { saveResult(id, format); })),
				action('Close', tabs.close)),
			el('div', { class: 'graph-view__chart' }, chart.element));
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
		results.get(id)?.chart.destroy();
		results.delete(id);
		if (editing === id) stopEditing();
	}

	/**
	 * the chart as it shows, named for the local time of the save
	 * @param {number} id
	 * @param {Readonly<ImageFormat>} format
	 */
	async function saveResult(id, format) {
		const result = results.get(id);
		if (!result) return;
		const name = graphFileName(localPartsIn(clock.timeZone())(clock.now()), format);
		const image = await result.chart.image(format.type);
		if (image) saveFile(name, image);
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
			if (id !== null) saveResult(id, IMAGE_FORMATS[0]);
		}
	};
}
