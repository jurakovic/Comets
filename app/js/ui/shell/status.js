// The status area: the comet count, which opens the Database view, and a running calculation's
// progress with Cancel. docs/spec/01-product.md, Shell.
import { statusCount } from '../../core/comets/counts.js';
import { el } from '../dom.js';

/**
 * @param {object} options
 * @param {import('../../lib/store.js').ReadableStore<Readonly<import('../../services/selection.js').Selection>>} options.selection
 * @param {Pick<import('../../services/jobs.js').JobsService, 'running' | 'cancel'>} options.jobs
 * @returns {{ element: HTMLElement }}
 */
export function createStatus({ selection, jobs }) {
	const count = el('a', { class: 'shell-status__count', href: '#/database' });
	const progress = el('progress', { class: 'shell-status__progress', 'aria-label': 'Calculation progress' });
	const cancel = el('button', { type: 'button', class: 'shell-status__cancel' }, 'Cancel');
	const job = el('div', { class: 'shell-status__job', hidden: true }, progress, cancel);
	const element = el('div', { class: 'shell-status__bar' }, count, job);

	/** @type {number | null} */
	let shownJob = null;
	cancel.addEventListener('click', () => { if (shownJob !== null) jobs.cancel(shownJob); });

	/** @param {Readonly<import('../../services/selection.js').Selection>} state */
	const showCount = ({ comets, total }) => { count.textContent = statusCount(comets.length, total); };

	/** @param {ReadonlyArray<import('../../services/jobs.js').RunningJob>} running the latest is shown */
	function showJob(running) {
		const latest = running.at(-1);
		job.hidden = !latest;
		shownJob = latest ? latest.id : null;
		if (!latest) return;
		if (latest.progress && latest.progress.total > 0) {
			progress.max = latest.progress.total;
			progress.value = latest.progress.done;
		} else {
			// no total yet: indeterminate
			progress.removeAttribute('value');
		}
	}

	showCount(selection.get());
	selection.subscribe(showCount);
	showJob(jobs.running.get());
	jobs.running.subscribe(showJob);
	return { element };
}
